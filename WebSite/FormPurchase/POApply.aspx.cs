using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BusinessObjects;
using BusinessObjects.PurchaseDSTableAdapters;
using BusinessObjects.MasterDataTableAdapters;

public partial class FormPurchase_POApply : BasePage {


    decimal AmountTotal = 0;
    decimal AmountRMBTotal = 0;

    private FormPurchaseBLL m_FormPurchaseBLL;
    protected FormPurchaseBLL FormPurchaseBLL {
        get {
            if (this.m_FormPurchaseBLL == null) {
                this.m_FormPurchaseBLL = new FormPurchaseBLL();
            }
            return this.m_FormPurchaseBLL;
        }
    }

    private PurchaseDS m_InnerDS;
    public PurchaseDS InnerDS {
        get {
            if (this.ViewState["PurchaseDS"] == null) {
                this.ViewState["PurchaseDS"] = new PurchaseDS();
            }
            return (PurchaseDS)this.ViewState["PurchaseDS"];
        }
    }

    protected string CategoryID {
        get {
            return this.ViewState["ItemCategoryID"].ToString();
        }
        set {
            this.ViewState["ItemCategoryID"] = value;
        }
    }

    #region 页面初始化及事件处理

    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!this.IsPostBack) {
            String title = this.GetLocalResourceObject("titleLabel.Text").ToString();
            PageUtility.SetContentTitle(this.Page, title);

            // 用户信息，职位信息
            AuthorizationDS.StuffUserRow stuffUser = (AuthorizationDS.StuffUserRow)Session["StuffUser"];
            AuthorizationDS.PositionRow rowUserPosition = (AuthorizationDS.PositionRow)Session["Position"];

            if (new StuffUserBLL().GetCostCenterIDByPositionID(rowUserPosition.PositionId) == 0) {
                this.Session["ErrorInfor"] = "未找到成本中心，请联系管理员";
                Response.Redirect("~/ErrorPage/SystemErrorPage.aspx");
            }
            this.ViewState["StuffUserID"] = stuffUser.StuffUserId;
            this.ViewState["PositionID"] = rowUserPosition.PositionId;

            this.StuffNameCtl.Text =  CommonUtility.GetStaffFullName(stuffUser);
            this.PositionNameCtl.Text = rowUserPosition.PositionName;
            this.DepartmentNameCtl.Text = new OUTreeBLL().GetOrganizationUnitById(rowUserPosition.OrganizationUnitId).OrganizationUnitName;
            this.ViewState["DepartmentID"] = rowUserPosition.OrganizationUnitId;
            this.StuffNoCtl.Text = stuffUser.IsStuffNoNull() ? "" : stuffUser.StuffNo;
            this.AttendDateCtl.Text = stuffUser.AttendDate.ToShortDateString();

            if (this.Request["RejectObjectID"] != null) {
                this.ViewState["RejectedObjectID"] = int.Parse(this.Request["RejectObjectID"].ToString());
            }

            PurchaseDS.FormRow rowForm ;
            PurchaseDS.FormPRRow rowFormPR ;
            if (Request["ObjectId"] != null) {
                this.ViewState["ObjectId"] = int.Parse(Request["ObjectId"]);
                if (this.Request["RejectObjectID"] == null) {
                    this.DeleteBtn.Visible = true;
                } else {
                    this.DeleteBtn.Visible = false;
                }
                PurchaseDS.FormPORow rowFormPO = this.FormPurchaseBLL.GetFormPOByID(int.Parse(this.ViewState["ObjectId"].ToString()));
                this.ViewState["ParentFormID"] = rowFormPO.ParentFormID;
                rowForm = this.FormPurchaseBLL.GetFormByID(rowFormPO.ParentFormID)[0];
                rowFormPR = this.FormPurchaseBLL.GetFormPRByID(rowFormPO.ParentFormID);

                this.ShippingTermDDL.SelectedValue = rowFormPO.ShippingTermID.ToString();
                this.PaymentTermCtl.Text = rowFormPO.IsPaymentTermsNull() ? "" : rowFormPO.PaymentTerms;
                this.CompanyDDL.SelectedValue = rowFormPO.CompanyID.ToString();
                this.RemarkCtl.Text = rowFormPO.IsRemarkNull() ? "" : rowFormPO.Remark;
                if (!rowFormPO.IsAttachedFileNameNull())
                    this.UCFileUpload.AttachmentFileName = rowFormPO.AttachedFileName;
                if (!rowFormPO.IsRealAttachedFileNameNull())
                    this.UCFileUpload.RealAttachmentFileName = rowFormPO.RealAttachedFileName;
                this.RealDeliveryAddressCtl.Text = rowFormPO.IsRealDeliveryAddressNull() ? "" : rowFormPO.RealDeliveryAddress;
                new FormPODetailTableAdapter().FillByFormPOID(this.InnerDS.FormPODetail, rowFormPO.FormPOID);
                //如果是草稿，那么就根据草稿的ischanged判断是否改变过
                this.ViewState["IsChanged"] = rowFormPO.IsChanged;
            } else {
                this.DeleteBtn.Visible = false;
                if (Request["ParentFormID"] != null) {
                    this.ViewState["ParentFormID"] = Request["ParentFormID"];
                } else {
                    this.Session["ErrorInfor"] = "没有找到PR，请联系管理员";
                    Response.Redirect("~/ErrorPage/SystemErrorPage.aspx");
                }
                rowForm = this.FormPurchaseBLL.GetFormByID(int.Parse(this.ViewState["ParentFormID"].ToString()))[0];
                rowFormPR = this.FormPurchaseBLL.GetFormPRByID(int.Parse(this.ViewState["ParentFormID"].ToString()));
                
                this.ShippingTermDDL.SelectedValue = rowFormPR.ShippingTermID.ToString();
                this.PaymentTermCtl.Text = rowFormPR.IsPaymentTermsNull() ? "" : rowFormPR.PaymentTerms;
                this.CompanyDDL.SelectedValue = rowFormPR.CompanyID.ToString();
                this.RealDeliveryAddressCtl.Text = rowFormPR.IsRealDeliveryAddressNull() ? "" : rowFormPR.RealDeliveryAddress;
                new FormPODetailTableAdapter().FillByPRID(this.InnerDS.FormPODetail, int.Parse(this.ViewState["ParentFormID"].ToString()));
                //如果是新建，那么首先赋值为没有改变
                if (this.ViewState["IsChanged"] == null) {
                    this.ViewState["IsChanged"] = false;
                }
            }
            this.FormNoCtl.Text = rowForm.FormNo;
            this.FormNoCtl.NavigateUrl = "javascript:window.showModalDialog('" + System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"] + "/FormPurchase/PRApproval.aspx?ShowDialog=1&ObjectId=" + rowForm.FormID + "','', 'dialogWidth:1000px;dialogHeight:750px;resizable:yes;')";
            MasterData.VendorRow vendor = new VendorTableAdapter().GetDataByID(rowFormPR.VendorID)[0];
            this.VendorCodeCtl.Text = vendor.VendorCode;
            this.VendorNameCtl.Text = vendor.VendorName;
            this.VendorAddressCtl.Text = vendor.VendorAddress;
            this.ItemCategoryCtl.Text = new ItemCategoryTableAdapter().GetDataByID(rowFormPR.ItemCategoryID)[0].ItemCategoryName;
            this.CurrencyCtl.Text = new CurrencyTableAdapter().GetDataByID(rowFormPR.CurrencyID)[0].CurrencyShortName;
            this.ExchangeRateCtl.Text = rowFormPR.ExchangeRate.ToString();
            this.ViewState["ExchangeRate"] = rowFormPR.ExchangeRate;
            this.PeriodCtl.Text = rowFormPR.FPeriod.ToString("yyyy-MM");
            this.PurchaseBudgetTypeCtl.Text = new PurchaseBudgetTypeTableAdapter().GetDataByID(rowFormPR.PurchaseBudgetTypeID)[0].PurchaseBudgetTypeName;
            this.PurchaseTypeCtl.Text = new PurchaseTypeTableAdapter().GetDataByID(rowFormPR.PurchaseTypeID)[0].PurchaseTypeName; 
            this.PRAmountCtl.Text = rowFormPR.AmountRMB.ToString("N");
            this.ViewState["ItemCategoryID"] = rowFormPR.ItemCategoryID;
        } 
    }

    protected override void OnLoadComplete(EventArgs e) {
        base.OnLoadComplete(e);
        UserControls_ItemControl ucItemControl = (UserControls_ItemControl)this.fvDetails.FindControl("NewUCItem");
        if (ucItemControl != null) {
            ucItemControl.ItemCategoryID = this.ViewState["ItemCategoryID"].ToString();
        }
    }

    #endregion

    protected void CancelBtn_Click(object sender, EventArgs e) {
        if (this.Request["Source"] != null) {
            this.Response.Redirect(this.Request["Source"].ToString());
        } else {
            this.Response.Redirect("~/Home.aspx");
        }
    }

    protected void DeleteBtn_Click(object sender, EventArgs e) {
        //删除草稿
        int formID = (int)this.ViewState["ObjectId"];
        this.FormPurchaseBLL.DeleteFormPOByFormID(formID);
        this.Page.Response.Redirect("~/Home.aspx");
    }

    protected void SaveFormPO(SystemEnums.FormStatus StatusID) {

        PurchaseDS.FormPRRow rowFormPR = this.FormPurchaseBLL.GetFormPRByID(int.Parse(this.ViewState["ParentFormID"].ToString()));
        PurchaseDS.FormRow rowForm = this.FormPurchaseBLL.GetFormByID(rowFormPR.FormPRID)[0];
        if (StatusID == SystemEnums.FormStatus.Awaiting) {//提交时检查，保存草稿不检查
            if (rowFormPR.AmountRMB < decimal.Parse(this.ViewState["TotalApplyAmount"].ToString())) {
                PageUtility.ShowModelDlg(this.Page, "本次PO申请金额超过PR金额，不能提交","the amount of this application should be less than the amount of PR ");
                return;
            } 
        }

        if (rowForm.StatusID != 2) {
            PageUtility.ShowModelDlg(this, "不能提交，对应的PR申请单不是审批完成状态！");
            return;
        }

        this.FormPurchaseBLL.PurchaseDataSet = this.InnerDS;
        int? RejectedFormID = null;
        if (this.ViewState["RejectedObjectID"] != null) {
            RejectedFormID = (int)this.ViewState["RejectedObjectID"];
        }

        int UserID = (int)this.ViewState["StuffUserID"];
        int? ProxyStuffUserId = null;
        if (Session["ProxyStuffUserId"] != null) {
            ProxyStuffUserId = int.Parse(Session["ProxyStuffUserId"].ToString());
        }
        string ItemCategoryName = ItemCategoryCtl.Text.Trim();
        if (ItemCategoryName.IndexOf("劳保") > 0){
            ItemCategoryName = "劳保";
        }else if (ItemCategoryName == "" || ItemCategoryName == "" || ItemCategoryName == ""){
            ItemCategoryName = "其他三项";
        }else{
            ItemCategoryName = "Other";
        }
        int OrganizationUnitID = (int)this.ViewState["DepartmentID"];
        int PositionID = (int)this.ViewState["PositionID"];
        int ParentFormID = rowFormPR.FormPRID;
        int CompanyID = int.Parse(this.CompanyDDL.SelectedValue);
        int ShippingTermID = int.Parse(this.ShippingTermDDL.SelectedValue);
        string PaymentTerms = this.PaymentTermCtl.Text;
        string Remark = this.RemarkCtl.Text;
        string AttachedFileName = this.UCFileUpload.AttachmentFileName;
        string RealAttachedFileName = this.UCFileUpload.RealAttachmentFileName;
        string RealDeliveryAddress = this.RealDeliveryAddressCtl.Text;
        //判断payment terms和shipping term、送货地址是否改过
        if (ShippingTermID != rowFormPR.ShippingTermID || PaymentTerms != rowFormPR.PaymentTerms || RealDeliveryAddress!=rowFormPR.RealDeliveryAddress) {
            this.ViewState["IsChanged"] = true;
        }
        try {
            if (this.ViewState["ObjectId"] == null || RejectedFormID != null) {
                this.FormPurchaseBLL.AddPOApply(RejectedFormID, UserID, ProxyStuffUserId, null, OrganizationUnitID, PositionID, SystemEnums.FormType.POApply, StatusID,rowFormPR.FormPRID,
                        CompanyID, ShippingTermID, PaymentTerms, Remark, AttachedFileName, RealAttachedFileName, (int)SystemEnums.POType.Purchase, (bool)this.ViewState["IsChanged"], ItemCategoryName, RealDeliveryAddress);
            } else {
                int FormID = (int)this.ViewState["ObjectId"];
                this.FormPurchaseBLL.UpdatePOApply(FormID, StatusID, CompanyID, ShippingTermID, PaymentTerms,
                        Remark, AttachedFileName, RealAttachedFileName, (bool)this.ViewState["IsChanged"], ItemCategoryName, RealDeliveryAddress);
            }
            this.Page.Response.Redirect("~/Home.aspx");
        } catch (Exception ex) {
            PageUtility.DealWithException(this.Page, ex);
        }
    }

    public bool IsSubmitValid() {

        if (this.PaymentTermCtl.Text == string.Empty) {
            PageUtility.ShowModelDlg(this.Page, "请录入payment terms!","please key in payment terms");
            return false;
        }
        if (this.RealDeliveryAddressCtl.Text == string.Empty) {
            PageUtility.ShowModelDlg(this.Page, "请录入实际送货地址!", "please key in delivery address");
            return false;
        }

        if (this.gvDetails.Rows.Count == 0) {
            PageUtility.ShowModelDlg(this.Page, "必须录入费用明细信息","please key in detail info");
            return false;
        }

        return true;
    }

    protected void SaveBtn_Click(object sender, EventArgs e) {

        SaveFormPO(SystemEnums.FormStatus.Draft);
    }

    protected void SubmitBtn_Click(object sender, EventArgs e) {

        if (!IsSubmitValid())
            return;
        SaveFormPO(SystemEnums.FormStatus.Awaiting);
    }

    protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e) {

        if (e.Row.RowType == DataControlRowType.DataRow) {
            if ((e.Row.RowState & DataControlRowState.Edit) != DataControlRowState.Edit) {
                DataRowView drvDetail = (DataRowView)e.Row.DataItem;
                PurchaseDS.FormPODetailRow row = (PurchaseDS.FormPODetailRow)drvDetail.Row;
                AmountTotal = decimal.Round((AmountTotal + row.Amount), 2);
                AmountRMBTotal = decimal.Round((AmountRMBTotal + row.AmountRMB), 2);
            }
        }

        this.ViewState["TotalApplyAmount"] = AmountRMBTotal;

        if (e.Row.RowType == DataControlRowType.Footer) {
            if (e.Row.RowType == DataControlRowType.Footer) {
                Label lblAmountTotal = (Label)e.Row.FindControl("lblAmountTotal");
                lblAmountTotal.Text = AmountTotal.ToString("N");
                Label lblAmountRMBTotal = (Label)e.Row.FindControl("lblAmountRMBTotal");
                lblAmountRMBTotal.Text = AmountRMBTotal.ToString("N");
            }
        }

    }

    protected void odsDetails_Inserting(object sender, ObjectDataSourceMethodEventArgs e) {
        e.InputParameters["ExchangeRate"] =decimal.Parse(this.ViewState["ExchangeRate"].ToString());
        if (this.ViewState["ObjectId"] != null) {
            e.InputParameters["FormPOID"] = int.Parse(this.ViewState["ObjectId"].ToString());
        }
        this.ViewState["IsChanged"] = true;
    }

    protected void odsDetails_Updating(object sender, ObjectDataSourceMethodEventArgs e) {
        e.InputParameters["ExchangeRate"] = decimal.Parse(this.ViewState["ExchangeRate"].ToString());
        this.ViewState["IsChanged"] = true;
    }

    protected void odsDetails_Deleting(object sender, ObjectDataSourceMethodEventArgs e) {
        this.ViewState["IsChanged"] = true;
    }

    protected void odsDetails_ObjectCreated(object sender, ObjectDataSourceEventArgs e) {
        FormPurchaseBLL bll = (FormPurchaseBLL)e.ObjectInstance;
        bll.PurchaseDataSet = this.InnerDS;
    }

    protected void NewUCItem_SelectedIndexChanged(object sender, EventArgs e) {
        UserControls_ItemControl NewUCItem = (UserControls_ItemControl)this.fvDetails.FindControl("NewUCItem");
        TextBox NewPackageCtl = (TextBox)this.fvDetails.FindControl("NewPackageCtl");
        TextBox NewUnitPriceCtl = (TextBox)this.fvDetails.FindControl("NewUnitPriceCtl");
        TextBox NewFinalPriceCtl = (TextBox)this.fvDetails.FindControl("NewFinalPriceCtl");
        TextBox NewQuantityCtl = (TextBox)this.fvDetails.FindControl("NewQuantityCtl");
        TextBox NewAmountCtl = (TextBox)this.fvDetails.FindControl("NewAmountCtl");
        TextBox NewAmountRMBCtl = (TextBox)this.fvDetails.FindControl("NewAmountRMBCtl");
        if (NewUCItem.ItemID != string.Empty) {
            MasterData.ItemRow item = new ItemTableAdapter().GetDataByID(int.Parse(NewUCItem.ItemID))[0];
            NewPackageCtl.Text = item.Package;
            NewUnitPriceCtl.Text = item.UnitPrice.ToString("N");
            NewFinalPriceCtl.Text = item.UnitPrice.ToString("N");
            NewUCItem.ItemCategoryID = this.CategoryID;
            if (NewQuantityCtl.Text != string.Empty) {
                decimal quantity = decimal.Parse(NewQuantityCtl.Text);
                NewAmountCtl.Text = decimal.Round(item.UnitPrice * quantity, 2).ToString();
                NewAmountRMBCtl.Text = decimal.Round(item.UnitPrice * quantity * decimal.Parse(this.ViewState["ExchangeRate"].ToString()), 2).ToString();
            }
        } else {
            NewPackageCtl.Text = "";
            NewUnitPriceCtl.Text = "";
        }
    }

    protected void UCItem_SelectedIndexChanged(object sender, EventArgs e) {
        UserControls_ItemControl UCItem = (UserControls_ItemControl)this.gvDetails.Rows[gvDetails.EditIndex].FindControl("UCItem");
        TextBox PackageCtl = (TextBox)this.gvDetails.Rows[gvDetails.EditIndex].FindControl("PackageCtl");
        TextBox UnitPriceCtl = (TextBox)this.gvDetails.Rows[gvDetails.EditIndex].FindControl("UnitPriceCtl");
        TextBox FinalPriceCtl = (TextBox)this.gvDetails.Rows[gvDetails.EditIndex].FindControl("FinalPriceCtl");
        TextBox QuantityCtl = (TextBox)this.gvDetails.Rows[gvDetails.EditIndex].FindControl("QuantityCtl");
        TextBox AmountCtl = (TextBox)this.gvDetails.Rows[gvDetails.EditIndex].FindControl("AmountCtl");
        TextBox AmountRMBCtl = (TextBox)this.gvDetails.Rows[gvDetails.EditIndex].FindControl("AmountRMBCtl");

        if (UCItem.ItemID != string.Empty) {
            MasterData.ItemRow item = new ItemTableAdapter().GetDataByID(int.Parse(UCItem.ItemID))[0];
            PackageCtl.Text = item.Package;
            UnitPriceCtl.Text = item.UnitPrice.ToString("N");
            FinalPriceCtl.Text = item.UnitPrice.ToString("N");
            UCItem.ItemCategoryID = this.CategoryID;
            if (QuantityCtl.Text != string.Empty) {
                decimal quantity = decimal.Parse(QuantityCtl.Text);
                AmountCtl.Text = decimal.Round(item.UnitPrice * quantity, 2).ToString();
                AmountRMBCtl.Text = decimal.Round(item.UnitPrice * quantity * decimal.Parse(this.ViewState["ExchangeRate"].ToString()), 2).ToString();
            }
        } else {
            PackageCtl.Text = "";
            UnitPriceCtl.Text = "";
        }
    }

    protected void gvDetails_OnDataBound(object sender, EventArgs e) {

        if (this.gvDetails.EditIndex >= 0) {
            TextBox FinalPriceCtl = (TextBox)this.gvDetails.Rows[gvDetails.EditIndex].FindControl("FinalPriceCtl");
            TextBox QuantityCtl = (TextBox)this.gvDetails.Rows[gvDetails.EditIndex].FindControl("QuantityCtl");
            TextBox AmountCtl = (TextBox)this.gvDetails.Rows[gvDetails.EditIndex].FindControl("AmountCtl");
            TextBox AmountRMBCtl = (TextBox)this.gvDetails.Rows[gvDetails.EditIndex].FindControl("AmountRMBCtl");
            FinalPriceCtl.Attributes.Add("onchange", "ParameterChanged('" + FinalPriceCtl.ClientID + "','" + QuantityCtl.ClientID + "','" + AmountCtl.ClientID + "','" + AmountRMBCtl.ClientID + "','" + this.ViewState["ExchangeRate"].ToString() + "')");
            QuantityCtl.Attributes.Add("onchange", "ParameterChanged('" + FinalPriceCtl.ClientID + "','" + QuantityCtl.ClientID + "','" + AmountCtl.ClientID + "','" + AmountRMBCtl.ClientID + "','" + this.ViewState["ExchangeRate"].ToString() + "')");
            UserControls_ItemControl UCItem = (UserControls_ItemControl)this.gvDetails.Rows[gvDetails.EditIndex].FindControl("UCItem");
            UCItem.ItemCategoryID = this.CategoryID;

        }
    }

    protected void fvDetails_OnDataBound(object sender, EventArgs e) {

        //先是插入行加入脚本
        TextBox NewFinalPriceCtl = (TextBox)this.fvDetails.FindControl("NewFinalPriceCtl");
        TextBox NewQuantityCtl = (TextBox)this.fvDetails.FindControl("NewQuantityCtl");
        TextBox NewAmountCtl = (TextBox)this.fvDetails.FindControl("NewAmountCtl");
        TextBox NewAmountRMBCtl = (TextBox)this.fvDetails.FindControl("NewAmountRMBCtl");

        NewFinalPriceCtl.Attributes.Add("onchange", "ParameterChanged('" + NewFinalPriceCtl.ClientID + "','" + NewQuantityCtl.ClientID + "','" + NewAmountCtl.ClientID + "','" + NewAmountRMBCtl.ClientID + "','" + this.ViewState["ExchangeRate"].ToString() + "')");
        NewQuantityCtl.Attributes.Add("onchange", "ParameterChanged('" + NewFinalPriceCtl.ClientID + "','" + NewQuantityCtl.ClientID + "','" + NewAmountCtl.ClientID + "','" + NewAmountRMBCtl.ClientID + "','" + this.ViewState["ExchangeRate"].ToString() + "')");

        UserControls_ItemControl NewUCItem = (UserControls_ItemControl)this.fvDetails.FindControl("NewUCItem");
        NewUCItem.ItemCategoryID = this.CategoryID;

    }


}