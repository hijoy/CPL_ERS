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

public partial class FormPurchase_PVSpecialApply : BasePage {

    decimal InvoiceFeeTotal = 0;
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
            VATRateDDL.DataBind();
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
            if (decimal.Parse( this.ViewState["ExchangeRate"].ToString()) == 0) {
                this.Session["ErrorInfor"] = "未找到汇率，请联系管理员";
                Response.Redirect("~/ErrorPage/SystemErrorPage.aspx");
            }
            this.ExchangeRateCtl.Text = this.ViewState["ExchangeRate"].ToString();
            
            //判断费用期间是否正确
            MasterDataBLL bll = new MasterDataBLL();
            if (!new MasterDataBLL().IsValidPeriodPurchase(DateTime.Parse(this.ViewState["Period"].ToString()))) {
                this.SubmitBtn.Visible = false;
                PageUtility.ShowModelDlg(this, "不允许申请本月的PV，请删除草稿并联系财务部!");
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
        VATRateDDL_SelectedIndexChanged(null, null);
        //隐藏预算信息,若需要则隐藏
    }

    protected void OpenForm(int formID) {

        PurchaseDS.FormPVRow rowFormPV = this.FormPurchaseBLL.GetFormPVByID(formID);
        //赋值
        this.ViewState["Period"] = rowFormPV.FPeriod.ToShortDateString();
        this.PeriodCtl.Text = rowFormPV.FPeriod.ToString("yyyy-MM");
        this.ViewState["VendorID"] = rowFormPV.VendorID.ToString();
        this.ViewState["ItemCategoryID"] = rowFormPV.ItemCategoryID.ToString();
        this.ViewState["PurchaseBudgetTypeID"] = rowFormPV.PurchaseBudgetTypeID.ToString();
        this.ViewState["CurrencyID"] = rowFormPV.CurrencyID.ToString();

        this.PurchaseTypeDDL.SelectedValue = rowFormPV.PurchaseTypeID.ToString();
        this.MethodPaymentDDL.SelectedValue = rowFormPV.MethodPaymentID.ToString();
        if (!rowFormPV.IsExpectPaymentDateNull()) {
            UCExpectPaymentDateCtl.SelectedDate = rowFormPV.ExpectPaymentDate.ToString("yyyy-MM-dd");
        }
        this.IsUrgentDDL.SelectedValue = rowFormPV.IsUrgent ? "1" : "0";
        this.IsPublicDDL.SelectedValue = rowFormPV.IsPublic ? "1" : "0";
        this.InvoiceStatusDDL.SelectedValue = rowFormPV.InvoiceStatusID.ToString();
        this.RemarkCtl.Text = rowFormPV.IsRemarkNull() ? "" : rowFormPV.Remark;
        if (!rowFormPV.IsAttachedFileNameNull())
            this.UCFileUpload.AttachmentFileName = rowFormPV.AttachedFileName;
        if (!rowFormPV.IsRealAttachedFileNameNull())
            this.UCFileUpload.RealAttachmentFileName = rowFormPV.RealAttachedFileName;

        this.AMTTaxCtl.Text = rowFormPV.AMTTax.ToString();
        if (!rowFormPV.IsVatRateIDNull()) {
            this.VATRateDDL.SelectedValue = rowFormPV.VatRateID.ToString();
        }
        if (!rowFormPV.IsPaymentTermsNull()) {
            this.PaymentTermCtl.Text = rowFormPV.PaymentTerms;
        }
        // 打开明细表
        new FormPVDetailTableAdapter().FillByFormPVID(this.InnerDS.FormPVDetail, formID);
        //invoice 明细
        new FormInvoiceTableAdapter().FillByFormID(this.InnerDS.FormInvoice, rowFormPV.FormPVID);

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
        this.FormPurchaseBLL.DeleteFormPVByFormID(formID);
        this.Page.Response.Redirect("~/Home.aspx");
    }

    protected void SaveFormPV(SystemEnums.FormStatus StatusID) {

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
                PageUtility.ShowModelDlg(this.Page, "本次申请金额超过可用预算，不能提交","the amount of this application is more than remain budget");
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
        int MethodPaymentID = int.Parse(this.MethodPaymentDDL.SelectedValue);
        DateTime? ExpectPaymentDate = null;
        if (UCExpectPaymentDateCtl.SelectedDate != string.Empty) {
            ExpectPaymentDate = DateTime.Parse(UCExpectPaymentDateCtl.SelectedDate);
        }
        bool IsUrgent = false;
        if (this.IsUrgentDDL.SelectedValue == "1") {
            IsUrgent = true;
        }
        bool IsPublic = false;
        if (this.IsPublicDDL.SelectedValue == "1") {
            IsPublic = true;
        }
        int InvoiceStatusID = int.Parse(this.InvoiceStatusDDL.SelectedValue);
        string Remark = this.RemarkCtl.Text;
        string AttachedFileName = this.UCFileUpload.AttachmentFileName;
        string RealAttachedFileName = this.UCFileUpload.RealAttachmentFileName;
        int PVType = (int)SystemEnums.PVType.None;
        decimal AMTTax;
        if (this.AMTTaxCtl.Text == string.Empty) {
            AMTTax = 0;
        } else {
            AMTTax = decimal.Parse(this.AMTTaxCtl.Text);
        }
        int? VATRateID = null;
        if (this.VATRateDDL.SelectedValue != "0") {
            VATRateID = int.Parse(this.VATRateDDL.SelectedValue);
        }
        string PaymentTerms = this.PaymentTermCtl.Text;
        bool IsPTChanged = false;
        if (StatusID == SystemEnums.FormStatus.Awaiting) {//提交时判断即可            
            string defaultPT = new MasterDataBLL().GetPaymentTermById(new VendorTableAdapter().GetDataByID(VendorID)[0].PaymentTermID)[0].PaymentTermName;
            if (defaultPT != PaymentTerms) {
                IsPTChanged = true;
            } else {
                IsPTChanged = false;
            }
        }
        try {
            if (this.ViewState["ObjectId"] == null || RejectedFormID != null) {
                this.FormPurchaseBLL.AddPVSpecialApply(RejectedFormID, UserID, ProxyStuffUserId, null, OrganizationUnitID, PositionID, SystemEnums.FormType.PVApply, StatusID, Period,
                        VendorID, ItemCategoryID, CurrencyID, ExchangeRate, PurchaseBudgetTypeID, PurchaseTypeID, Remark, AttachedFileName, RealAttachedFileName, MethodPaymentID, ExpectPaymentDate,
                        TotalBudget, ApprovedAmount, ApprovingAmount, ReimbursedAmount, NonReimbursedAmount, RemainBudget, IsUrgent, IsPublic, InvoiceStatusID, PVType, VATRateID, AMTTax,PaymentTerms,IsPTChanged);
            } else {
                int FormID = (int)this.ViewState["ObjectId"];
                this.FormPurchaseBLL.UpdatePVSpecialApply(FormID, StatusID, ExchangeRate, PurchaseTypeID, Remark, AttachedFileName, RealAttachedFileName, MethodPaymentID,
                        ExpectPaymentDate, TotalBudget, ApprovedAmount, ApprovingAmount, ReimbursedAmount, NonReimbursedAmount, RemainBudget, IsUrgent, IsPublic, InvoiceStatusID, VATRateID, AMTTax, PaymentTerms, IsPTChanged);
            }
            this.Page.Response.Redirect("~/Home.aspx");
        } catch (Exception ex) {
            PageUtility.DealWithException(this.Page, ex);
        }
    }

    public bool IsSubmitValid() {

        if (this.VATRateDDL.SelectedValue != "0") {
            MasterData.VatTypeRow VATRate = new MasterDataBLL().GetVatTypeById(int.Parse(this.VATRateDDL.SelectedValue))[0];
            if (VATRate.HasTax && (this.AMTTaxCtl.Text == string.Empty || this.AMTTaxCtl.Text == "0")) {
                PageUtility.ShowModelDlg(this.Page, "请录入税金!", "please key in tax amount");
                return false;
            }
            if (!VATRate.HasTax && this.AMTTaxCtl.Text != string.Empty && this.AMTTaxCtl.Text != "0") {
                PageUtility.ShowModelDlg(this.Page, "您选择的税率类型没有税金!", "there is no tax regarding the VAT rate you selected");
                return false;
            }
        } else {
            PageUtility.ShowModelDlg(this.Page, "请选择税率类型!", "please select VAT Rate");
            return false;
        }
        if (this.UCExpectPaymentDateCtl.SelectedDate == "") {
            PageUtility.ShowModelDlg(this.Page, "请录入期望支付日期!", "please key in expect payment date ");
            return false;
        }

        if (this.gvDetails.Rows.Count == 0) {
            PageUtility.ShowModelDlg(this.Page, "必须录入费用明细信息","please key in detail info");
            return false;
        }
        //判断是否录入了发票
        MasterData.InvoiceStatusRow row = new InvoiceStatusTableAdapter().GetDataByID(int.Parse(this.InvoiceStatusDDL.SelectedValue))[0];
        if (row.NeedInvoice) {
            if (this.gvInvoice.Rows.Count == 0) {
                PageUtility.ShowModelDlg(this.Page, "请录入发票信息!", "please key in invoice info");
                return false;
            } else {
                if (decimal.Parse(this.ViewState["InvoiceFeeTotal"].ToString()) < decimal.Parse(this.ViewState["TotalApplyAmount"].ToString())) {
                    PageUtility.ShowModelDlg(this.Page, "发票金额不得小于支付金额!", "the amount of invoice should not be less than the payment");
                    return false;
                }
            }
        }
        if (this.PaymentTermCtl.Text == string.Empty) {
            PageUtility.ShowModelDlg(this.Page, "请录入账期!", "please key in payment terms");
            return false;
        }
        if (string.IsNullOrEmpty(this.RemarkCtl.Text)) {
            PageUtility.ShowModelDlg(this.Page, "请录入备注!", "please key in remark");
            return false;
        }
        return true;
    }

    protected void SaveBtn_Click(object sender, EventArgs e) {

        SaveFormPV(SystemEnums.FormStatus.Draft);
    }

    protected void SubmitBtn_Click(object sender, EventArgs e) {

        if (!IsSubmitValid())
            return;
        SaveFormPV(SystemEnums.FormStatus.Awaiting);
    }

    protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e) {

        if (e.Row.RowType == DataControlRowType.DataRow) {
            if ((e.Row.RowState & DataControlRowState.Edit) != DataControlRowState.Edit) {
                DataRowView drvDetail = (DataRowView)e.Row.DataItem;
                PurchaseDS.FormPVDetailRow row = (PurchaseDS.FormPVDetailRow)drvDetail.Row;
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
            e.InputParameters["FormPVID"] = int.Parse(this.ViewState["ObjectId"].ToString());
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

    protected void odsInvoice_ObjectCreated(object sender, ObjectDataSourceEventArgs e) {
        FormPurchaseBLL bll = (FormPurchaseBLL)e.ObjectInstance;
        bll.PurchaseDataSet = this.InnerDS;
    }

    protected void gvInvoice_RowDataBound(object sender, GridViewRowEventArgs e) {
        // 对数据列进行赋值
        if (e.Row.RowType == DataControlRowType.DataRow) {
            if ((e.Row.RowState & DataControlRowState.Edit) != DataControlRowState.Edit) {
                DataRowView drvDetail = (DataRowView)e.Row.DataItem;
                PurchaseDS.FormInvoiceRow row = (PurchaseDS.FormInvoiceRow)drvDetail.Row;
                InvoiceFeeTotal = decimal.Round((InvoiceFeeTotal + row.InvoiceAmount), 2);
            }
        }

        this.ViewState["InvoiceFeeTotal"] = InvoiceFeeTotal;

        if (e.Row.RowType == DataControlRowType.Footer) {
            Label lblInvoiceFeeTotal = (Label)e.Row.FindControl("lblInvoiceFeeTotal");
            lblInvoiceFeeTotal.Text = InvoiceFeeTotal.ToString("N");

        }
    }

    protected void VATRateDDL_SelectedIndexChanged(object sender, EventArgs e) {
        if (this.VATRateDDL.SelectedValue != "0") {
            MasterData.VatTypeRow VATRate = new MasterDataBLL().GetVatTypeById(int.Parse(this.VATRateDDL.SelectedValue))[0];
            if (VATRate.HasTax) {
                this.AMTTaxCtl.ReadOnly = false;
                if (this.AMTTaxCtl.Text == "0") {
                    this.AMTTaxCtl.Text = "";
                }
            } else {
                this.AMTTaxCtl.ReadOnly = true;
                this.AMTTaxCtl.Text = "0";
            }
        } else {
            this.AMTTaxCtl.ReadOnly = true;
            this.AMTTaxCtl.Text = "0";
        }
    }
}