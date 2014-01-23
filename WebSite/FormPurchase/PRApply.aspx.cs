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

public partial class FormPurchase_PRApply : BasePage {
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

            this.StuffNameCtl.Text = CommonUtility.GetStaffFullName(stuffUser);
            this.PositionNameCtl.Text = rowUserPosition.PositionName;
            this.DepartmentNameCtl.Text = new OUTreeBLL().GetOrganizationUnitById(rowUserPosition.OrganizationUnitId).OrganizationUnitName;
            this.ViewState["DepartmentID"] = rowUserPosition.OrganizationUnitId;
            this.StuffNoCtl.Text = stuffUser.IsStuffNoNull() ? "" : stuffUser.StuffNo;
            this.AttendDateCtl.Text = stuffUser.AttendDate.ToShortDateString();

            if (this.Request["RejectObjectID"] != null) {
                this.ViewState["RejectedObjectID"] = int.Parse(this.Request["RejectObjectID"].ToString());
            }

            //如果是草稿进行赋值
            if (Request["ObjectId"] != null) {
                this.ViewState["ObjectId"] = int.Parse(Request["ObjectId"]);
                if (this.Request["RejectObjectID"] == null) {
                    this.DeleteBtn.Visible = true;
                } else {
                    this.DeleteBtn.Visible = false;
                }
                OpenForm(int.Parse(this.ViewState["ObjectId"].ToString()));
            } else {
                this.DeleteBtn.Visible = false;
                if (Request["PeriodPurchaseID"] != null) {
                    DateTime Period = new MasterDataBLL().GetPeriodPurchaseById(int.Parse(Request["PeriodPurchaseID"].ToString())).PeriodPurchase;
                    this.ViewState["Period"] = Period.ToShortDateString();
                } else {
                    this.Session["ErrorInfor"] = "没有费用期间，请联系管理员";
                    Response.Redirect("~/ErrorPage/SystemErrorPage.aspx");
                }
                if (Request["VendorID"] != null) {
                    this.ViewState["VendorID"] = Request["VendorID"];
                } else {
                    this.Session["ErrorInfor"] = "未找到Vendor，请联系管理员";
                    Response.Redirect("~/ErrorPage/SystemErrorPage.aspx");
                }
                if (Request["ItemCategoryID"] != null) {
                    this.ViewState["ItemCategoryID"] = Request["ItemCategoryID"];
                } else {
                    this.Session["ErrorInfor"] = "未找到Item Category，请联系管理员";
                    Response.Redirect("~/ErrorPage/SystemErrorPage.aspx");
                }
                if (Request["PurchaseBudgetTypeID"] != null) {
                    this.ViewState["PurchaseBudgetTypeID"] = Request["PurchaseBudgetTypeID"];
                } else {
                    this.Session["ErrorInfor"] = "未找到预算类型，请联系管理员";
                    Response.Redirect("~/ErrorPage/SystemErrorPage.aspx");
                }
                if (Request["CurrencyID"] != null) {
                    this.ViewState["CurrencyID"] = Request["CurrencyID"];
                } else {
                    this.Session["ErrorInfor"] = "未找到币种，请联系管理员";
                    Response.Redirect("~/ErrorPage/SystemErrorPage.aspx");
                }
                this.PeriodCtl.Text = DateTime.Parse(this.ViewState["Period"].ToString()).ToString("yyyy-MM");
                this.PaymentTermCtl.Text = new MasterDataBLL().GetPaymentTermById(new VendorTableAdapter().GetDataByID(int.Parse(this.ViewState["VendorID"].ToString()))[0].PaymentTermID)[0].PaymentTermName;

            }
            MasterData.VendorRow vendor = new VendorTableAdapter().GetDataByID(int.Parse(this.ViewState["VendorID"].ToString()))[0];
            this.VendorCodeCtl.Text = vendor.VendorCode;
            this.VendorNameCtl.Text = vendor.VendorName;
            this.VendorAddressCtl.Text = vendor.VendorAddress;
            this.ItemCategoryCtl.Text = new ItemCategoryTableAdapter().GetDataByID(int.Parse(this.ViewState["ItemCategoryID"].ToString()))[0].ItemCategoryName;
            this.CurrencyCtl.Text = new CurrencyTableAdapter().GetDataByID(int.Parse(this.ViewState["CurrencyID"].ToString()))[0].CurrencyShortName;
            this.PurchaseBudgetTypeCtl.Text = new PurchaseBudgetTypeTableAdapter().GetDataByID(int.Parse(this.ViewState["PurchaseBudgetTypeID"].ToString()))[0].PurchaseBudgetTypeName;

            this.ViewState["ExchangeRate"] = new MasterDataBLL().GetExchangeRateByPeriod(int.Parse(ViewState["CurrencyID"].ToString()), DateTime.Parse(ViewState["Period"].ToString()));
            if (decimal.Parse(this.ViewState["ExchangeRate"].ToString()) == 0) {
                this.Session["ErrorInfor"] = "未找到汇率，请联系管理员";
                Response.Redirect("~/ErrorPage/SystemErrorPage.aspx");
            }
            this.ExchangeRateCtl.Text = this.ViewState["ExchangeRate"].ToString();

            //判断费用期间是否正确
            MasterDataBLL bll = new MasterDataBLL();
            if (!new MasterDataBLL().IsValidPeriodPurchase(DateTime.Parse(this.ViewState["Period"].ToString()))) {
                this.SubmitBtn.Visible = false;
                PageUtility.ShowModelDlg(this, "不允许申请本月项目，请删除草稿并联系财务部!");
                return;
            }

            //预算信息
            decimal[] calculateAssistant = new decimal[6];
            calculateAssistant = new BudgetBLL().GetManagingBudgetByParameter(rowUserPosition.PositionId, DateTime.Parse(ViewState["Period"].ToString()), int.Parse(this.ViewState["PurchaseBudgetTypeID"].ToString()));
            this.TotalBudgetCtl.Text = calculateAssistant[0].ToString("N");
            this.ApprovedAmountCtl.Text = calculateAssistant[1].ToString("N");
            this.ApprovingAmountCtl.Text = calculateAssistant[2].ToString("N");
            this.ReimbursedAmountCtl.Text = calculateAssistant[3].ToString("N");
            this.NonReimbursedAmountCtl.Text = calculateAssistant[4].ToString("N");
            this.RemainBudgetCtl.Text = calculateAssistant[5].ToString("N");

        }
    }

    protected void OpenForm(int formID) {

        PurchaseDS.FormRow rowForm = this.FormPurchaseBLL.GetFormByID(formID)[0];
        PurchaseDS.FormPRRow rowFormPR = this.FormPurchaseBLL.GetFormPRByID(formID);
        //赋值
        this.ViewState["Period"] = rowFormPR.FPeriod.ToShortDateString();
        this.PeriodCtl.Text = rowFormPR.FPeriod.ToString("yyyy-MM");
        this.ViewState["VendorID"] = rowFormPR.VendorID.ToString();
        this.ViewState["ItemCategoryID"] = rowFormPR.ItemCategoryID.ToString();
        this.ViewState["PurchaseBudgetTypeID"] = rowFormPR.PurchaseBudgetTypeID.ToString();
        this.ViewState["CurrencyID"] = rowFormPR.CurrencyID.ToString();


        this.PurchaseTypeDDL.SelectedValue = rowFormPR.PurchaseTypeID.ToString();
        this.ShippingTermDDL.SelectedValue = rowFormPR.ShippingTermID.ToString();
        this.PaymentTermCtl.Text = rowFormPR.IsPaymentTermsNull() ? "" : rowFormPR.PaymentTerms;
        if (!rowFormPR.IsCompanyIDNull()) {
            this.CompanyDDL.SelectedValue = rowFormPR.CompanyID.ToString();
        }
        this.RealDeliveryAddressCtl.Text = rowFormPR.IsRealDeliveryAddressNull() ? "" : rowFormPR.RealDeliveryAddress;
        this.RemarkCtl.Text = rowFormPR.IsRemarkNull() ? "" : rowFormPR.Remark;
        if (!rowFormPR.IsAttachedFileNameNull())
            this.UCFileUpload.AttachmentFileName = rowFormPR.AttachedFileName;
        if (!rowFormPR.IsRealAttachedFileNameNull())
            this.UCFileUpload.RealAttachmentFileName = rowFormPR.RealAttachedFileName;

        // 打开明细表
        new FormPRDetailTableAdapter().FillByFormPRID(this.InnerDS.FormPRDetail, formID);

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
        this.Page.Response.Redirect("~/Home.aspx");
    }

    protected void DeleteBtn_Click(object sender, EventArgs e) {
        //删除草稿
        int formID = (int)this.ViewState["ObjectId"];
        this.FormPurchaseBLL.DeleteFormPRByFormID(formID);
        this.Page.Response.Redirect("~/Home.aspx");
    }

    protected void SaveFormPR(SystemEnums.FormStatus StatusID) {

        decimal TotalBudget = 0;
        decimal ApprovedAmount = 0;
        decimal ApprovingAmount = 0;
        decimal ReimbursedAmount = 0;
        decimal NonReimbursedAmount = 0;
        decimal RemainBudget = 0;

        if (StatusID == SystemEnums.FormStatus.Awaiting) {//提交时检查，保存草稿不检查

            decimal[] calculateAssistant = new decimal[6];
            calculateAssistant = new BudgetBLL().GetManagingBudgetByParameter((int)this.ViewState["PositionID"], DateTime.Parse(ViewState["Period"].ToString()), int.Parse(this.ViewState["PurchaseBudgetTypeID"].ToString()));
            if (calculateAssistant[5] < decimal.Parse(this.ViewState["TotalApplyAmount"].ToString())) {
                PageUtility.ShowModelDlg(this.Page, "本次申请金额超过可用预算，不能提交", "the amount of this application is more than remain budget");
                return;
            } else {
                TotalBudget = calculateAssistant[0];
                ApprovedAmount = calculateAssistant[1];
                ApprovingAmount = calculateAssistant[2];
                ReimbursedAmount = calculateAssistant[3];
                NonReimbursedAmount = calculateAssistant[4];
                RemainBudget = calculateAssistant[5];
            }
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
        int OrganizationUnitID = (int)this.ViewState["DepartmentID"];
        int PositionID = (int)this.ViewState["PositionID"];
        DateTime Period = DateTime.Parse(this.ViewState["Period"].ToString());
        int VendorID = int.Parse(this.ViewState["VendorID"].ToString());
        int ItemCategoryID = int.Parse(this.ViewState["ItemCategoryID"].ToString());
        int CurrencyID = int.Parse(this.ViewState["CurrencyID"].ToString());
        decimal ExchangeRate = decimal.Parse(this.ViewState["ExchangeRate"].ToString());
        int PurchaseBudgetTypeID = int.Parse(this.ViewState["PurchaseBudgetTypeID"].ToString());
        int PurchaseTypeID = int.Parse(this.PurchaseTypeDDL.SelectedValue);
        int? CompanyID = null;
        if (this.CompanyDDL.SelectedValue != "0") {
            CompanyID = int.Parse(this.CompanyDDL.SelectedValue);
        }
        int ShippingTermID = int.Parse(this.ShippingTermDDL.SelectedValue);
        string PaymentTerms = this.PaymentTermCtl.Text;
        string Remark = this.RemarkCtl.Text;
        string AttachedFileName = this.UCFileUpload.AttachmentFileName;
        string RealAttachedFileName = this.UCFileUpload.RealAttachmentFileName;
        string RealDeliveryAddress = this.RealDeliveryAddressCtl.Text;
        string ItemCategoryName = ItemCategoryCtl.Text.Trim();
        if (ItemCategoryName.IndexOf("劳保") > 0){
            ItemCategoryName = "劳保";
        }else if (ItemCategoryName == "" || ItemCategoryName == "" || ItemCategoryName == ""){
            ItemCategoryName = "其他三项";
        }else{
            ItemCategoryName = "Other";
        }
        try {
            if (this.ViewState["ObjectId"] == null || RejectedFormID != null) {
                this.FormPurchaseBLL.AddPRApply(RejectedFormID, UserID, ProxyStuffUserId, null, OrganizationUnitID, PositionID, SystemEnums.FormType.PRApply, StatusID, Period,
                        VendorID, ItemCategoryID, CurrencyID, ExchangeRate, PurchaseBudgetTypeID, PurchaseTypeID, CompanyID, ShippingTermID, PaymentTerms,
                        Remark, AttachedFileName, RealAttachedFileName, TotalBudget, ApprovedAmount, ApprovingAmount, ReimbursedAmount, NonReimbursedAmount, RemainBudget, ItemCategoryName, RealDeliveryAddress);
            } else {
                int FormID = (int)this.ViewState["ObjectId"];
                this.FormPurchaseBLL.UpdatePRApply(FormID, StatusID, ExchangeRate, PurchaseTypeID, CompanyID, ShippingTermID, PaymentTerms,
                        Remark, AttachedFileName, RealAttachedFileName, TotalBudget, ApprovedAmount, ApprovingAmount, ReimbursedAmount, NonReimbursedAmount, RemainBudget, ItemCategoryName, RealDeliveryAddress);
            }
            this.Page.Response.Redirect("~/Home.aspx");
        } catch (Exception ex) {
            PageUtility.DealWithException(this.Page, ex);
        }
    }

    public bool IsSubmitValid() {

        if (this.CompanyDDL.SelectedValue == "0") {
            PageUtility.ShowModelDlg(this.Page, "请选择地址地址!", "please select Company address");
            return false;
        }
        if (this.RealDeliveryAddressCtl.Text == string.Empty) {
            PageUtility.ShowModelDlg(this.Page, "请录入实际送货地址!", "please key in delivery address");
            return false;
        }

        if (this.PaymentTermCtl.Text == string.Empty) {
            PageUtility.ShowModelDlg(this.Page, "请录入payment terms!", "please key in payment terms");
            return false;
        }

        if (this.gvDetails.Rows.Count == 0) {
            PageUtility.ShowModelDlg(this.Page, "必须录入费用明细信息", "please key in detail info");
            return false;
        }

        return true;
    }

    protected void SaveBtn_Click(object sender, EventArgs e) {

        SaveFormPR(SystemEnums.FormStatus.Draft);
    }

    protected void SubmitBtn_Click(object sender, EventArgs e) {

        if (!IsSubmitValid())
            return;
        SaveFormPR(SystemEnums.FormStatus.Awaiting);
    }

    protected void CompanyDDL_SelectedIndexChanged(object sender, EventArgs e) {
        if (this.CompanyDDL.SelectedValue != "0" && this.RealDeliveryAddressCtl.Text == "") {
            this.RealDeliveryAddressCtl.Text = new MasterDataBLL().GetCompanyById(int.Parse(CompanyDDL.SelectedValue)).CompanyAddress;
        } 
    }

    protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e) {

        if (e.Row.RowType == DataControlRowType.DataRow) {
            if ((e.Row.RowState & DataControlRowState.Edit) != DataControlRowState.Edit) {
                DataRowView drvDetail = (DataRowView)e.Row.DataItem;
                PurchaseDS.FormPRDetailRow row = (PurchaseDS.FormPRDetailRow)drvDetail.Row;
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
        e.InputParameters["ExchangeRate"] = decimal.Parse(this.ViewState["ExchangeRate"].ToString());
        if (this.ViewState["ObjectId"] != null) {
            e.InputParameters["FormPRID"] = int.Parse(this.ViewState["ObjectId"].ToString());
        }
    }

    protected void odsDetails_Updating(object sender, ObjectDataSourceMethodEventArgs e) {
        e.InputParameters["ExchangeRate"] = decimal.Parse(this.ViewState["ExchangeRate"].ToString());
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