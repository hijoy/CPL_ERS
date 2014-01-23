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

public partial class FormPurchase_POSpecialApply : BasePage {


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


            if (Request["ObjectId"] != null) {
                this.ViewState["ObjectId"] = int.Parse(Request["ObjectId"]);
                if (this.Request["RejectObjectID"] == null) {
                    this.DeleteBtn.Visible = true;
                } else {
                    this.DeleteBtn.Visible = false;
                }
                PurchaseDS.FormPORow rowFormPO = this.FormPurchaseBLL.GetFormPOByID(int.Parse(this.ViewState["ObjectId"].ToString()));
                this.ViewState["ParentFormID"] = rowFormPO.ParentFormID;
                this.ViewState["Period"] = rowFormPO.FPeriod.ToShortDateString();
                this.PeriodCtl.Text = rowFormPO.FPeriod.ToString("yyyy-MM");
                this.ViewState["VendorID"] = rowFormPO.VendorID.ToString();
                this.ViewState["ItemCategoryID"] = rowFormPO.ItemCategoryID.ToString();
                this.ViewState["PurchaseBudgetTypeID"] = rowFormPO.PurchaseBudgetTypeID.ToString();
                this.ViewState["CurrencyID"] = rowFormPO.CurrencyID.ToString();

                this.PurchaseTypeDDL.SelectedValue = rowFormPO.PurchaseTypeID.ToString();
                this.ShippingTermDDL.SelectedValue = rowFormPO.ShippingTermID.ToString();
                this.PaymentTermCtl.Text = rowFormPO.IsPaymentTermsNull() ? "" : rowFormPO.PaymentTerms;
                if (!rowFormPO.IsCompanyIDNull()) {
                    this.CompanyDDL.SelectedValue = rowFormPO.CompanyID.ToString();
                }
                this.RemarkCtl.Text = rowFormPO.IsRemarkNull() ? "" : rowFormPO.Remark;
                if (!rowFormPO.IsAttachedFileNameNull())
                    this.UCFileUpload.AttachmentFileName = rowFormPO.AttachedFileName;
                if (!rowFormPO.IsRealAttachedFileNameNull())
                    this.UCFileUpload.RealAttachmentFileName = rowFormPO.RealAttachedFileName;

                this.RealDeliveryAddressCtl.Text = rowFormPO.IsRealDeliveryAddressNull() ? "" : rowFormPO.RealDeliveryAddress;

                new FormPODetailTableAdapter().FillByFormPOID(this.InnerDS.FormPODetail, rowFormPO.FormPOID);
            } else {
                this.DeleteBtn.Visible = false;
                if (Request["ParentFormID"] != null) {
                    this.ViewState["ParentFormID"] = Request["ParentFormID"];
                } else {
                    this.Session["ErrorInfor"] = "未找到申请单，请联系管理员";
                    Response.Redirect("~/ErrorPage/SystemErrorPage.aspx");
                }
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
                MasterData.VendorRow vendor = new MasterDataBLL().GetVendorByID(int.Parse(this.ViewState["VendorID"].ToString()));
                MasterData.PaymentTermRow paymentTerm = new MasterDataBLL().GetPaymentTermById(vendor.PaymentTermID)[0];
                if (paymentTerm != null && !paymentTerm.IsPaymentTermNameNull()) {
                    this.PaymentTermCtl.Text = paymentTerm.PaymentTermName;
                }
            }
            MasterData.VendorRow vendor1 = new VendorTableAdapter().GetDataByID(int.Parse(this.ViewState["VendorID"].ToString()))[0];
            this.VendorCodeCtl.Text = vendor1.VendorCode;
            this.VendorNameCtl.Text = vendor1.VendorName;
            this.VendorAddressCtl.Text = vendor1.VendorAddress;

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
            //申请单编号和申请单金额
            PurchaseDS.FormRow applyFormRow = this.FormPurchaseBLL.GetFormByID(int.Parse(this.ViewState["ParentFormID"].ToString()))[0];
            this.ApplyFormNoCtl.Text = applyFormRow.FormNo;
            this.ViewState["ParentFormNo"] = applyFormRow.FormNo;
            string url = string.Empty;
            switch (applyFormRow.PageType) {
                case (int)SystemEnums.PageType.ActivityApply:
                    url = "/FormSale/ActivityApproval.aspx?ShowDialog=1&ObjectId=" + applyFormRow.FormID;
                    this.ViewState["POType"] = SystemEnums.POType.Sale;
                    this.ViewState["ApplyAmountRMB"] = new FormSaleBLL().GetFormSaleApplyByID(applyFormRow.FormID)[0].AmountRMB;
                    break;
                case (int)SystemEnums.PageType.NoActivityApply:
                    url = "/FormSale/NoActivityApproval.aspx?ShowDialog=1&ObjectId=" + applyFormRow.FormID;
                    this.ViewState["POType"] = SystemEnums.POType.Sale;
                    this.ViewState["ApplyAmountRMB"] = new FormSaleBLL().GetFormSaleApplyByID(applyFormRow.FormID)[0].AmountRMB;
                    break;
                case (int)SystemEnums.PageType.FormMarketingApply:
                    url = "/FormMarketing/MarketingApproval.aspx?ShowDialog=1&ObjectId=" + applyFormRow.FormID;
                    this.ViewState["POType"] = SystemEnums.POType.Marketing;
                    this.ViewState["ApplyAmountRMB"] = new FormMarketingBLL().GetFormMarketingApplyByID(applyFormRow.FormID)[0].AmountRMB;
                    break;
                case (int)SystemEnums.PageType.RDApply:
                    url = "/FormRD/RDApproval.aspx?ShowDialog=1&ObjectId=" + applyFormRow.FormID;
                    this.ViewState["POType"] = SystemEnums.POType.RD;
                    this.ViewState["ApplyAmountRMB"] = new FormRDBLL().GetFormRDApplyByID(applyFormRow.FormID)[0].AmountRMB;
                    break;
            }

            this.ApplyFormNoCtl.NavigateUrl = "javascript:window.showModalDialog('" + System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"] + url + "','', 'dialogWidth:1000px;dialogHeight:750px;resizable:yes;')";
            this.ApplyAmountCtl.Text = this.ViewState["ApplyAmountRMB"].ToString();


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
        this.Page.Response.Redirect("~/Home.aspx");
    }

    protected void DeleteBtn_Click(object sender, EventArgs e) {
        //删除草稿
        int formID = (int)this.ViewState["ObjectId"];
        this.FormPurchaseBLL.DeleteFormPOByFormID(formID);
        this.Page.Response.Redirect("~/Home.aspx");
    }

    protected void SaveFormPO(SystemEnums.FormStatus StatusID) {

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
        int ParentFormID = int.Parse(this.ViewState["ParentFormID"].ToString());
        PurchaseDS.FormRow formRow = FormPurchaseBLL.GetFormByID(ParentFormID)[0];
        if (formRow.StatusID != 2) {
            PageUtility.ShowModelDlg(this, "不能提交，对应的申请单不是审批完成状态！");
            return;
        }
        string ParentFormNo = this.ViewState["ParentFormNo"].ToString();
        decimal ApplyAmountRMB = decimal.Parse(this.ViewState["ApplyAmountRMB"].ToString());
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

        try {
            if (this.ViewState["ObjectId"] == null || RejectedFormID != null) {
                this.FormPurchaseBLL.AddPOSpecialApply(RejectedFormID, UserID, ProxyStuffUserId, null, OrganizationUnitID, PositionID, SystemEnums.FormType.POApply, StatusID, ParentFormID, ParentFormNo, ApplyAmountRMB,
                      Period, VendorID, ItemCategoryID, CurrencyID, ExchangeRate, PurchaseBudgetTypeID, PurchaseTypeID, CompanyID, ShippingTermID, PaymentTerms, Remark, AttachedFileName, RealAttachedFileName, (int)this.ViewState["POType"], true, RealDeliveryAddress);
            } else {
                int FormID = (int)this.ViewState["ObjectId"];
                this.FormPurchaseBLL.UpdatePOSpecialApply(FormID, StatusID, PurchaseTypeID, CompanyID, ShippingTermID, PaymentTerms,
                        Remark, AttachedFileName, RealAttachedFileName, RealDeliveryAddress);
            }
            this.Page.Response.Redirect("~/Home.aspx");
        } catch (Exception ex) {
            PageUtility.DealWithException(this.Page, ex);
        }
    }

    public bool IsSubmitValid() {

        if (this.CompanyDDL.SelectedValue == "0") {
            PageUtility.ShowModelDlg(this.Page, "请选择公司地址!", "please select Company address");
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

        SaveFormPO(SystemEnums.FormStatus.Draft);
    }

    protected void SubmitBtn_Click(object sender, EventArgs e) {

        if (!IsSubmitValid())
            return;
        SaveFormPO(SystemEnums.FormStatus.Awaiting);
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
        e.InputParameters["ExchangeRate"] = decimal.Parse(this.ViewState["ExchangeRate"].ToString());
        if (this.ViewState["ObjectId"] != null) {
            e.InputParameters["FormPOID"] = int.Parse(this.ViewState["ObjectId"].ToString());
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