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

public partial class FormPurchase_PVApply : BasePage {

    decimal InvoiceFeeTotal = 0;

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
            if (Request["ObjectId"] != null) {
                this.ViewState["ObjectId"] = int.Parse(Request["ObjectId"]);
                if (this.Request["RejectObjectID"] == null) {
                    this.DeleteBtn.Visible = true;
                } else {
                    this.DeleteBtn.Visible = false;
                }
                PurchaseDS.FormPVRow rowFormPV = this.FormPurchaseBLL.GetFormPVByID(int.Parse(this.ViewState["ObjectId"].ToString()));
                this.ViewState["PVType"] = rowFormPV.PVType;

                this.PeriodCtl.Text = rowFormPV.FPeriod.ToString("yyyy-MM");
                MasterData.VendorRow vendor = new VendorTableAdapter().GetDataByID(rowFormPV.VendorID)[0];
                this.VendorCodeCtl.Text = vendor.VendorCode;
                this.VendorNameCtl.Text = vendor.VendorName;
                this.VendorAddressCtl.Text = vendor.VendorAddress;
                this.ItemCategoryCtl.Text = new ItemCategoryTableAdapter().GetDataByID(rowFormPV.ItemCategoryID)[0].ItemCategoryName;
                this.CurrencyCtl.Text = new CurrencyTableAdapter().GetDataByID(rowFormPV.CurrencyID)[0].CurrencyShortName;
                this.ExchangeRateCtl.Text = rowFormPV.ExchangeRate.ToString();
                this.ViewState["ExchangeRate"] = rowFormPV.ExchangeRate;
                this.PurchaseBudgetTypeCtl.Text = new PurchaseBudgetTypeTableAdapter().GetDataByID(rowFormPV.PurchaseBudgetTypeID)[0].PurchaseBudgetTypeName;
                this.PurchaseTypeCtl.Text = new PurchaseTypeTableAdapter().GetDataByID(rowFormPV.PurchaseTypeID)[0].PurchaseTypeName;
                this.ParentFormNoCtl.Text = rowFormPV.ParentFormNo.ToString();
                this.ViewState["ParentFormNo"] = rowFormPV.ParentFormNo.ToString();
                this.ShippingTermCtl.Text = new ShippingTermTableAdapter().GetDataByID(rowFormPV.ShippingTermID)[0].ShippingTermName;
                this.PaymentTermCtl.Text = rowFormPV.PaymentTerms;
                this.RealDeliveryAddressCtl.Text = rowFormPV.RealDeliveryAddress;
                this.ApplyAmountCtl.Text = rowFormPV.ApplyAmount.ToString("N");
                if (rowFormPV.PVType == (int)SystemEnums.PVType.PR) {
                    this.ViewState["ParentFormID"] = rowFormPV.FormPRID;
                    this.ParentFormNoCtl.NavigateUrl = "javascript:window.showModalDialog('" + System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"] + "/FormPurchase/PRApproval.aspx?ShowDialog=1&ObjectId=" + rowFormPV.FormPRID + "','', 'dialogWidth:1000px;dialogHeight:750px;resizable:yes;')";
                    this.PayedAmountCtl.Text = this.FormPurchaseBLL.GetReimbursedPVAmountByPRID(rowFormPV.FormPRID).ToString("N");
                    new FormPRPODetailViewTableAdapter().FillByPRID(this.InnerDS.FormPRPODetailView, rowFormPV.FormPRID);
                } else {
                    this.ViewState["ParentFormID"] = rowFormPV.FormPOID;
                    this.ParentFormNoCtl.NavigateUrl = "javascript:window.showModalDialog('" + System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"] + "/FormPurchase/POApproval.aspx?ShowDialog=1&ObjectId=" + rowFormPV.FormPOID + "','', 'dialogWidth:1000px;dialogHeight:750px;resizable:yes;')";
                    this.PayedAmountCtl.Text = this.FormPurchaseBLL.GetReimbursedPVAmountByPOID(rowFormPV.FormPOID).ToString("N");
                    new FormPRPODetailViewTableAdapter().FillByPOID(this.InnerDS.FormPRPODetailView, rowFormPV.FormPOID);
                }
                //PV本身的
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

                this.AMTBeforeTaxCtl.Text = rowFormPV.AMTBeforeTax.ToString();
                this.AMTTaxCtl.Text = rowFormPV.AMTTax.ToString();
                this.AmountCtl.Text = rowFormPV.Amount.ToString("N");
                this.AmountRMBCtl.Text = rowFormPV.AmountRMB.ToString("N");
                if (!rowFormPV.IsVatRateIDNull()) {
                    this.VATRateDDL.SelectedValue = rowFormPV.VatRateID.ToString();
                }
                //invoice 明细
                new FormInvoiceTableAdapter().FillByFormID(this.InnerDS.FormInvoice, rowFormPV.FormPVID);

            } else {
                this.DeleteBtn.Visible = false;
                this.ViewState["PVType"] = Request["PVType"];
                this.ViewState["ParentFormID"] = Request["ParentFormID"];
                this.ViewState["ParentFormNo"] = Request["ParentFormNo"];
                PurchaseDS.FormPRRow rowFormPR;
                PurchaseDS.FormPORow rowFormPO;

                //如果是PR类
                if (int.Parse(Request["PVType"].ToString()) == (int)SystemEnums.PVType.PR) {
                    rowFormPR = this.FormPurchaseBLL.GetFormPRByID(int.Parse(this.ViewState["ParentFormID"].ToString()));
                    this.ShippingTermCtl.Text = new ShippingTermTableAdapter().GetDataByID(rowFormPR.ShippingTermID)[0].ShippingTermName;
                    this.PaymentTermCtl.Text = rowFormPR.PaymentTerms;
                    this.RealDeliveryAddressCtl.Text = rowFormPR.IsRealDeliveryAddressNull() ? "" : rowFormPR.RealDeliveryAddress;
                    this.ParentFormNoCtl.NavigateUrl = "javascript:window.showModalDialog('" + System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"] + "/FormPurchase/PRApproval.aspx?ShowDialog=1&ObjectId=" + rowFormPR.FormPRID + "','', 'dialogWidth:1000px;dialogHeight:750px;resizable:yes;')";
                    this.ApplyAmountCtl.Text = rowFormPR.AmountRMB.ToString("N");
                    this.PayedAmountCtl.Text = this.FormPurchaseBLL.GetReimbursedPVAmountByPRID(rowFormPR.FormPRID).ToString("N");
                    //明细
                    new FormPRPODetailViewTableAdapter().FillByPRID(this.InnerDS.FormPRPODetailView, rowFormPR.FormPRID);
                } else {
                    rowFormPO = this.FormPurchaseBLL.GetFormPOByID(int.Parse(this.ViewState["ParentFormID"].ToString()));
                    rowFormPR = this.FormPurchaseBLL.GetFormPRByID(rowFormPO.ParentFormID);
                    this.ShippingTermCtl.Text = new ShippingTermTableAdapter().GetDataByID(rowFormPO.ShippingTermID)[0].ShippingTermName;
                    this.PaymentTermCtl.Text = rowFormPO.PaymentTerms;
                    this.RealDeliveryAddressCtl.Text = rowFormPO.IsRealDeliveryAddressNull() ? "" : rowFormPO.RealDeliveryAddress;
                    this.ParentFormNoCtl.NavigateUrl = "javascript:window.showModalDialog('" + System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"] + "/FormPurchase/POApproval.aspx?ShowDialog=1&ObjectId=" + rowFormPO.FormPOID + "','', 'dialogWidth:1000px;dialogHeight:750px;resizable:yes;')";
                    this.ApplyAmountCtl.Text = rowFormPO.AmountRMB.ToString("N");
                    this.PayedAmountCtl.Text = this.FormPurchaseBLL.GetReimbursedPVAmountByPOID(rowFormPO.FormPOID).ToString("N");
                    //明细
                    new FormPRPODetailViewTableAdapter().FillByPOID(this.InnerDS.FormPRPODetailView, rowFormPO.FormPOID);
                }
                this.PeriodCtl.Text = rowFormPR.FPeriod.ToString("yyyy-MM");
                MasterData.VendorRow vendor = new VendorTableAdapter().GetDataByID(rowFormPR.VendorID)[0];
                this.VendorCodeCtl.Text = vendor.VendorCode;
                this.VendorNameCtl.Text = vendor.VendorName;
                this.VendorAddressCtl.Text = vendor.VendorAddress;
                this.ItemCategoryCtl.Text = new ItemCategoryTableAdapter().GetDataByID(rowFormPR.ItemCategoryID)[0].ItemCategoryName;
                this.CurrencyCtl.Text = new CurrencyTableAdapter().GetDataByID(rowFormPR.CurrencyID)[0].CurrencyShortName;
                this.ExchangeRateCtl.Text = rowFormPR.ExchangeRate.ToString();
                this.ViewState["ExchangeRate"] = rowFormPR.ExchangeRate;
                this.PurchaseBudgetTypeCtl.Text = new PurchaseBudgetTypeTableAdapter().GetDataByID(rowFormPR.PurchaseBudgetTypeID)[0].PurchaseBudgetTypeName;
                this.PurchaseTypeCtl.Text = new PurchaseTypeTableAdapter().GetDataByID(rowFormPR.PurchaseTypeID)[0].PurchaseTypeName;
                this.ParentFormNoCtl.Text = this.ViewState["ParentFormNo"].ToString();
            }
            VATRateDDL_SelectedIndexChanged(null, null);
        }
        this.AMTBeforeTaxCtl.Attributes.Add("onchange", "ParameterChanged('" + AMTBeforeTaxCtl.ClientID + "','" + AMTTaxCtl.ClientID + "','" + AmountCtl.ClientID + "','" + AmountRMBCtl.ClientID + "','" + this.ViewState["ExchangeRate"].ToString() + "')");
        this.AMTTaxCtl.Attributes.Add("onchange", "ParameterChanged('" + AMTBeforeTaxCtl.ClientID + "','" + AMTTaxCtl.ClientID + "','" + AmountCtl.ClientID + "','" + AmountRMBCtl.ClientID + "','" + this.ViewState["ExchangeRate"].ToString() + "')");
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
        this.FormPurchaseBLL.DeleteFormPVByFormID(formID);
        this.Page.Response.Redirect("~/Home.aspx");
    }

    protected void SaveFormPV(SystemEnums.FormStatus StatusID) {

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
        //PV中内容
        PurchaseDS.FormPRRow rowFormPR;
        PurchaseDS.FormPORow rowFormPO;
        int? FormPRID = null;
        int? FormPOID = null;
        string ParentFormNo = this.ViewState["ParentFormNo"].ToString();
        int? CompanyID = null;
        int ShippingTermID;
        string PaymentTerms;
        string Remark = this.RemarkCtl.Text;
        string AttachedFileName = this.UCFileUpload.AttachmentFileName;
        string RealAttachedFileName = this.UCFileUpload.RealAttachmentFileName;
        int MethodPaymentID = int.Parse(this.MethodPaymentDDL.SelectedValue);
        DateTime? ExpectPaymentDate = null;
        if (UCExpectPaymentDateCtl.SelectedDate != string.Empty) {
            ExpectPaymentDate = DateTime.Parse(UCExpectPaymentDateCtl.SelectedDate);
        }
        decimal ApplyAmount;
        decimal PayedAmount;
        decimal AMTBeforeTax;
        if (this.AMTBeforeTaxCtl.Text == string.Empty) {
            AMTBeforeTax = 0;
        } else {
            AMTBeforeTax = decimal.Parse(this.AMTBeforeTaxCtl.Text);
        }
        decimal AMTTax;
        if (this.AMTTaxCtl.Text == string.Empty) {
            AMTTax = 0;
        } else {
            AMTTax = decimal.Parse(this.AMTTaxCtl.Text);
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
        int PVType;
        if (int.Parse(ViewState["PVType"].ToString()) == (int)SystemEnums.PVType.PR) {
            PurchaseDS.FormRow formRow = FormPurchaseBLL.GetFormByID(int.Parse(this.ViewState["ParentFormID"].ToString()))[0];
            if (formRow.StatusID != 2) {
                PageUtility.ShowModelDlg(this, "不能提交，对应的PR申请单不是审批完成状态！");
                return;
            }
            rowFormPR = this.FormPurchaseBLL.GetFormPRByID(int.Parse(this.ViewState["ParentFormID"].ToString()));
            FormPRID = int.Parse(this.ViewState["ParentFormID"].ToString());
            CompanyID = rowFormPR.CompanyID;
            ShippingTermID = rowFormPR.ShippingTermID;
            PaymentTerms = rowFormPR.PaymentTerms;
            ApplyAmount = rowFormPR.AmountRMB;
            PayedAmount = this.FormPurchaseBLL.GetReimbursedPVAmountByPRID(rowFormPR.FormPRID);
            PVType = (int)SystemEnums.PVType.PR;
        } else {
            rowFormPO = this.FormPurchaseBLL.GetFormPOByID(int.Parse(this.ViewState["ParentFormID"].ToString()));
            rowFormPR = this.FormPurchaseBLL.GetFormPRByID(rowFormPO.ParentFormID);
            PurchaseDS.FormRow formPORow = FormPurchaseBLL.GetFormByID(int.Parse(this.ViewState["ParentFormID"].ToString()))[0];
            if (formPORow.StatusID != 2) {
                PageUtility.ShowModelDlg(this, "不能提交，对应的PO申请单不是审批完成状态！");
                return;
            }
            PurchaseDS.FormRow formPRRow = FormPurchaseBLL.GetFormByID(rowFormPR.FormPRID)[0];
            if (formPORow.StatusID != 2) {
                PageUtility.ShowModelDlg(this, "不能提交，对应的PR申请单不是审批完成状态！");
                return;
            }
            FormPRID = rowFormPR.FormPRID;
            FormPOID = int.Parse(this.ViewState["ParentFormID"].ToString());
            CompanyID = rowFormPO.CompanyID;
            ShippingTermID = rowFormPO.ShippingTermID;
            PaymentTerms = rowFormPO.PaymentTerms;
            ApplyAmount = rowFormPO.AmountRMB;
            PayedAmount = this.FormPurchaseBLL.GetReimbursedPVAmountByPOID(rowFormPO.FormPOID);
            PVType = (int)SystemEnums.PVType.PO;
        }

        DateTime FPeriod = rowFormPR.FPeriod;
        int VendorID = rowFormPR.VendorID;
        int ItemCategoryID = rowFormPR.ItemCategoryID;
        int CurrencyID = rowFormPR.CurrencyID;
        decimal ExchangeRate = rowFormPR.ExchangeRate;
        int PurchaseBudgetTypeID = rowFormPR.PurchaseBudgetTypeID;
        int PurchaseTypeID = rowFormPR.PurchaseTypeID;
        int? VATRateID = null;
        if (this.VATRateDDL.SelectedValue != "0") {
            VATRateID = int.Parse(this.VATRateDDL.SelectedValue);
        }
        string RealDeliveryAddress = this.RealDeliveryAddressCtl.Text;
        decimal AmountRMB = decimal.Round(decimal.Parse(this.ViewState["ExchangeRate"].ToString()) * (AMTBeforeTax + AMTTax), 2);
        if (StatusID == SystemEnums.FormStatus.Awaiting) {//提交时检查，保存草稿不检查
            if (AmountRMB <= 0) {
                PageUtility.ShowModelDlg(this.Page, "本次支付金额必须大于0", "the amount of this payment should be more than zero ");
                return;
            }
            if (AmountRMB > (ApplyAmount - PayedAmount)) {
                PageUtility.ShowModelDlg(this.Page, "本次支付金额超过可支付金额，不能提交", "the amount of this payment should be less than the remain amount ");
                return;
            }
        }
        string ItemCategoryName = ItemCategoryCtl.Text.Trim();
        if (ItemCategoryName.IndexOf("劳保") > 0) {
            ItemCategoryName = "劳保";
        } else if (ItemCategoryName == "" || ItemCategoryName == "" || ItemCategoryName == "") {
            ItemCategoryName = "其他三项";
        } else {
            ItemCategoryName = "Other";
        }
        try {
            if (this.ViewState["ObjectId"] == null || RejectedFormID != null) {
                this.FormPurchaseBLL.AddPVApply(RejectedFormID, UserID, ProxyStuffUserId, null, OrganizationUnitID, PositionID, SystemEnums.FormType.PVApply, StatusID,
                        FormPRID, FormPOID, ParentFormNo, FPeriod, VendorID, ItemCategoryID, CurrencyID, ExchangeRate, PurchaseBudgetTypeID, PurchaseTypeID,
                        CompanyID, ShippingTermID, PaymentTerms, Remark, AttachedFileName, RealAttachedFileName, MethodPaymentID, ExpectPaymentDate,
                        ApplyAmount, PayedAmount, AMTBeforeTax, AMTTax, IsUrgent, IsPublic, InvoiceStatusID, PVType, VATRateID, ItemCategoryName, RealDeliveryAddress);
            } else {
                int FormID = (int)this.ViewState["ObjectId"];
                this.FormPurchaseBLL.UpdatePVApply(FormID, StatusID, Remark, AttachedFileName, RealAttachedFileName, MethodPaymentID, ExpectPaymentDate,
                        ApplyAmount, PayedAmount, AMTBeforeTax, AMTTax, IsUrgent, IsPublic, InvoiceStatusID, VATRateID, ItemCategoryName);
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
        } else {
            PageUtility.ShowModelDlg(this.Page, "请选择税率类型!", "please select VAT Rate");
            return false;
        }
        if (this.AmountCtl.Text == string.Empty) {
            PageUtility.ShowModelDlg(this.Page, "请录入本次支付金额!", "please key in payment amount");
            return false;
        }
        if (this.UCExpectPaymentDateCtl.SelectedDate == "") {
            PageUtility.ShowModelDlg(this.Page, "请录入期望支付日期!", "please key in expect payment date ");
            return false;
        }
        //判断是否录入了发票
        MasterData.InvoiceStatusRow row = new InvoiceStatusTableAdapter().GetDataByID(int.Parse(this.InvoiceStatusDDL.SelectedValue))[0];
        if (row.NeedInvoice) {
            if (this.gvInvoice.Rows.Count == 0) {
                PageUtility.ShowModelDlg(this.Page, "请录入发票信息!", "please key in invoice info");
                return false;
            } else {
                decimal AmountRMB = decimal.Round(decimal.Parse(this.ViewState["ExchangeRate"].ToString()) * decimal.Parse(this.AmountCtl.Text), 2);
                if (decimal.Parse(this.ViewState["InvoiceFeeTotal"].ToString()) < AmountRMB) {
                    PageUtility.ShowModelDlg(this.Page, "发票金额不得小于支付金额!", "the amount of invoice should not be less than the payment");
                    return false;
                }
            }
        }
        if (string.IsNullOrEmpty(RemarkCtl.Text)) {
            PageUtility.ShowModelDlg(this.Page, "请录入备注!", "please key in remark ");
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

    protected void VATRateDDL_SelectedIndexChanged(object sender, EventArgs e) {
        if (this.VATRateDDL.SelectedValue != "0") {
            MasterData.VatTypeRow VATRate = new MasterDataBLL().GetVatTypeById(int.Parse(this.VATRateDDL.SelectedValue))[0];
            if (VATRate.HasTax) {
                this.AMTTaxCtl.ReadOnly = false;
                if (this.AMTTaxCtl.Text == "0") {
                    this.AMTTaxCtl.Text = "";
                    if (AMTBeforeTaxCtl.Text != string.Empty) {
                        this.AmountCtl.Text = this.AMTBeforeTaxCtl.Text;
                        this.AmountRMBCtl.Text = decimal.Round(decimal.Parse(this.ViewState["ExchangeRate"].ToString()) * decimal.Parse(this.AMTBeforeTaxCtl.Text), 2).ToString();
                    }
                }
            } else {
                this.AMTTaxCtl.ReadOnly = true;
                this.AMTTaxCtl.Text = "0";
                if (this.AMTBeforeTaxCtl.Text == "") {
                    this.AmountCtl.Text = "0";
                    this.AmountRMBCtl.Text = "0";
                } else {
                    this.AmountCtl.Text = this.AMTBeforeTaxCtl.Text;
                    this.AmountRMBCtl.Text = decimal.Round(decimal.Parse(this.ViewState["ExchangeRate"].ToString()) * decimal.Parse(this.AMTBeforeTaxCtl.Text), 2).ToString();
                }
            }
        } else {
            this.AMTTaxCtl.ReadOnly = true;
            this.AMTTaxCtl.Text = "0";
            if (this.AMTBeforeTaxCtl.Text == "") {
                this.AmountCtl.Text = "0";
                this.AmountRMBCtl.Text = "0";
            } else {
                this.AmountCtl.Text = this.AMTBeforeTaxCtl.Text;
                this.AmountRMBCtl.Text = decimal.Round(decimal.Parse(this.ViewState["ExchangeRate"].ToString()) * decimal.Parse(this.AMTBeforeTaxCtl.Text), 2).ToString();
            }
        }
    }

    protected void odsDetails_ObjectCreated(object sender, ObjectDataSourceEventArgs e) {
        FormPurchaseBLL bll = (FormPurchaseBLL)e.ObjectInstance;
        bll.PurchaseDataSet = this.InnerDS;
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
}