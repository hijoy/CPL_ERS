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
using BusinessObjects.FormDSTableAdapters;

public partial class FormPurchase_FormVendorApply : BasePage {

    private FormVendorBLL m_FormVendorBLL;
    protected FormVendorBLL FormVendorBLL {
        get {
            if (this.m_FormVendorBLL == null) {
                this.m_FormVendorBLL = new FormVendorBLL();
            }
            return this.m_FormVendorBLL;
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
            this.ViewState["ActionType"] = 1;
            if (Request["ActionType"] != null) {
                this.trModifyReason.Visible = true;
                this.ViewState["VendorID"] = int.Parse(Request["VendorID"]);
                this.ViewState["ActionType"] = int.Parse(Request["ActionType"]);
                FillFormByVendorID(int.Parse(this.ViewState["VendorID"].ToString()));
            }
            if ((int)this.ViewState["ActionType"] == 1) {
                this.ddlHoldVendor.Enabled = false;
            }
            if ((int)this.ViewState["ActionType"] == 2) {
                this.odsBankCode.SelectCommand = "select BankCodeID,BankCode+'-'+Description BankCode from BankCode order by BankCode";
                this.odsTransType.SelectCommand = "select TransTypeID,TransTypeName+'-'+Description TransTypeName from TransType order by TransTypeName";
            } else {
                this.odsBankCode.SelectCommand = "select BankCodeID,BankCode+'-'+Description BankCode from BankCode where IsActive = 1 order by BankCode";
                this.odsTransType.SelectCommand = "select TransTypeID,TransTypeName+'-'+Description TransTypeName from TransType where IsActive = 1 order by TransTypeName";
            }
            //如果是草稿或者是修改Vendor申请时
            if (Request["ObjectId"] != null) {
                this.ViewState["ObjectId"] = int.Parse(Request["ObjectId"]);
                //当退回修改时删除按钮隐藏
                if (this.Request["RejectObjectID"] != null) {
                    this.DeleteBtn.Visible = false;
                } else {
                    this.DeleteBtn.Visible = true;
                }
                OpenForm(int.Parse(this.ViewState["ObjectId"].ToString()));
            } else {
                this.DeleteBtn.Visible = false;
            }
        }
    }

    protected void OpenForm(int formID) {
        PurchaseDS.FormVendorRow FormVendorTR = FormVendorBLL.GetFormVendorByID(formID)[0];
        if (!FormVendorTR.IsVendorNameNull()) {
            txtVendorName.Text = FormVendorTR.VendorName;
        }
        if (!FormVendorTR.IsVendorAddressNull()) {
            txtVendorAddress.Text = FormVendorTR.VendorAddress;
        }
        if (!FormVendorTR.IsCityNull()) {
            txtCity.Text = FormVendorTR.City;
        }
        if (!FormVendorTR.IsPostalNull()) {
            txtPostal.Text = FormVendorTR.Postal;
        }
        if (!FormVendorTR.IsContactNameNull()) {
            txtContactName.Text = FormVendorTR.ContactName;
        }
        if (!FormVendorTR.IsVendorTypeIDNull()) {
            this.VendorTypeControl.VendorTypeID = FormVendorTR.VendorTypeID.ToString();
            MasterDataBLL ma = new MasterDataBLL();
            MasterData.VendorTypeRow VendorTypeRow = ma.GetVendorTypeById(FormVendorTR.VendorTypeID);
            VendorTypeControl.VendorTypeName = VendorTypeRow.VendorTypeName;
            txtCurrency.Text = ma.GetCurrencyByID(VendorTypeRow.CurrencyID).CurrencyFullName;
            txtCompany.Text = ma.GetCompanyById(VendorTypeRow.CompanyID).CompanyName;
            txtCompanyCode.Text = ma.GetCompanyById(VendorTypeRow.CompanyID).CompanyCode;
        }
        if (!FormVendorTR.IsPhoneNumberNull()) {
            this.txtPhoneNumber.Text = FormVendorTR.PhoneNumber;
        }
        if (!FormVendorTR.IsOneTimeVendorNull()) {
            this.ddlOneTimeVendor.SelectedValue = Convert.ToInt32(FormVendorTR.OneTimeVendor).ToString();
        }
        if (!FormVendorTR.IsHoldVendorNull()) {
            this.ddlHoldVendor.SelectedValue = Convert.ToInt32(FormVendorTR.HoldVendor).ToString();
        }
        if (!FormVendorTR.IsPurchaseingPostalCodeNull()) {
            this.txtPurchaseingPostalCode.Text = FormVendorTR.PurchaseingPostalCode;
        }
        if (!FormVendorTR.IsAlphaSearchKeyNull()) {
            this.txtAlphaSearchKey.Text = FormVendorTR.AlphaSearchKey;
        }
        if (!FormVendorTR.IsPurchasingCityNull()) {
            this.txtPurchasingCity.Text = FormVendorTR.PurchasingCity;
        }
        if (!FormVendorTR.IsPurchasingContactNull()) {
            this.txtPurchasingContact.Text = FormVendorTR.PurchasingContact;
        }
        if (!FormVendorTR.IsPurchasingAddressNull()) {
            this.txtPurchasingAddress.Text = FormVendorTR.PurchasingAddress;
        }
        if (!FormVendorTR.IsPurchasePhoneNumberNull()) {
            this.txtPurchasePhoneNumber.Text = FormVendorTR.PurchasePhoneNumber;
        }
        if (!FormVendorTR.IsBankCodeIDNull()) {
            this.ddlBankCode.DataBind();
            this.ddlBankCode.SelectedValue = FormVendorTR.BankCodeID.ToString();
        }
        if (!FormVendorTR.IsPaymentTermIDNull()) {
            this.ddlPaymentTerm.SelectedValue = FormVendorTR.PaymentTermID.ToString();
        }
        if (!FormVendorTR.IsTransTypeIDNull()) {
            this.ddlTransType.SelectedValue = FormVendorTR.TransTypeID.ToString();
        }
        if (!FormVendorTR.IsVATTypeIDNull()) {
            ddlVatRate.SelectedValue = FormVendorTR.VATTypeID.ToString();
        }
        if (!FormVendorTR.IsBankNameNull()) {
            txtBankName.Text = FormVendorTR.BankName.ToString();
        }
        if (!FormVendorTR.IsAccountNoNull()) {
            txtAccountNo.Text = FormVendorTR.AccountNo;
        }
        if (!FormVendorTR.IsBankNoNull()) {
            txtBankNo.Text = FormVendorTR.BankNo;
        }
        if (!FormVendorTR.IsACTypeIDNull()) {
            ddlACType.SelectedValue = FormVendorTR.ACTypeID.ToString();
        }
        if (!FormVendorTR.IsModifyReasonNull()) {
            this.txtModifyReason.Text = FormVendorTR.ModifyReason;
        }
        if (!FormVendorTR.IsVendorIDNull()) {
            this.trModifyReason.Visible = true;
        }
        if (!FormVendorTR.IsAttachmentFileNameNull()) {
            this.UCFileUpload.AttachmentFileName = FormVendorTR.AttachmentFileName;
            this.UCFileUpload.RealAttachmentFileName = FormVendorTR.RealAttachmentFileName;
        }
        if (!FormVendorTR.IsRemarkNull()) {
            this.txtRemark.Text = FormVendorTR.Remark;
        }
        if (int.Parse(this.ViewState["ActionType"].ToString()) == (int)SystemEnums.VendorActionType.Delete) {
            DisableWhenDelete();
        }
    }

    protected override void OnLoadComplete(EventArgs e) {
        this.txtCity.Attributes.Add("onchange", "CopyValue('" + this.txtCity.ClientID + "','" + this.txtPurchasingCity.ClientID + "')");
        this.txtContactName.Attributes.Add("onchange", "CopyValue('" + this.txtContactName.ClientID + "','" + this.txtPurchasingContact.ClientID + "')");
        this.txtPhoneNumber.Attributes.Add("onchange", "CopyValue('" + this.txtPhoneNumber.ClientID + "','" + this.txtPurchasePhoneNumber.ClientID + "')");
        this.txtVendorAddress.Attributes.Add("onchange", "CopyValue('" + this.txtVendorAddress.ClientID + "','" + this.txtPurchasingAddress.ClientID + "')");
    }

    public void FillFormByVendorID(int VendorID) {
        MasterData.VendorRow vendorRow = new MasterDataBLL().GetVendorByID(VendorID);
        txtVendorName.Text = vendorRow.VendorName;
        txtVendorAddress.Text = vendorRow.VendorAddress;
        txtCity.Text = vendorRow.City;
        txtPostal.Text = vendorRow.Postal;
        txtContactName.Text = vendorRow.ContactName;
        this.VendorTypeControl.VendorTypeID = vendorRow.VendorTypeID.ToString();
        MasterDataBLL ma = new MasterDataBLL();
        MasterData.VendorTypeRow VendorTypeRow = ma.GetVendorTypeById(vendorRow.VendorTypeID);
        VendorTypeControl.VendorTypeName = VendorTypeRow.VendorTypeName;
        txtCurrency.Text = ma.GetCurrencyByID(VendorTypeRow.CurrencyID).CurrencyFullName;
        txtCompany.Text = ma.GetCompanyById(VendorTypeRow.CompanyID).CompanyName;
        txtCompanyCode.Text = ma.GetCompanyById(VendorTypeRow.CompanyID).CompanyCode;
        if (!vendorRow.IsPhoneNumberNull()) {
            this.txtPhoneNumber.Text = vendorRow.PhoneNumber;
        }
        if (!vendorRow.IsOneTimeVendorNull()) {
            this.ddlOneTimeVendor.SelectedValue = Convert.ToInt32(vendorRow.OneTimeVendor).ToString();
        }
        if (!vendorRow.IsHoldVendorNull()) {
            this.ddlHoldVendor.SelectedValue = Convert.ToInt32(vendorRow.HoldVendor).ToString();
        }
        if (!vendorRow.IsPurchaseingPostalCodeNull()) {
            this.txtPurchaseingPostalCode.Text = vendorRow.PurchaseingPostalCode;
        }
        if (!vendorRow.IsAlphaSearchKeyNull()) {
            this.txtAlphaSearchKey.Text = vendorRow.AlphaSearchKey;
        }
        if (!vendorRow.IsPurchasingCityNull()) {
            this.txtPurchasingCity.Text = vendorRow.PurchasingCity;
        }
        if (!vendorRow.IsPurchasingContactNull()) {
            this.txtPurchasingContact.Text = vendorRow.PurchasingContact;
        }
        if (!vendorRow.IsPurchasingAddressNull()) {
            this.txtPurchasingAddress.Text = vendorRow.PurchasingAddress;
        }
        if (!vendorRow.IsPurchasePhoneNumberNull()) {
            this.txtPurchasePhoneNumber.Text = vendorRow.PurchasePhoneNumber;
        }
        this.ddlPaymentTerm.SelectedValue = vendorRow.PaymentTermID.ToString();
        if (!vendorRow.IsTransTypeIDNull()) {
            this.ddlTransType.SelectedValue = vendorRow.TransTypeID.ToString();
        }
        this.ddlBankCode.DataBind();
        if (!vendorRow.IsBankCodeIDNull()) {
            this.ddlBankCode.SelectedValue = vendorRow.BankCodeID.ToString();
        }
        ddlVatRate.SelectedValue = vendorRow.VATTypeID.ToString();

        txtBankName.Text = vendorRow.BankName.ToString();
        txtAccountNo.Text = vendorRow.AccountNo;
        if (!vendorRow.IsRemarkNull()) {
            this.txtRemark.Text = vendorRow.Remark;
        }
        if (!vendorRow.IsBankNoNull()) {
            this.txtBankNo.Text = vendorRow.BankNo;
        }
        ddlACType.SelectedValue = vendorRow.ACTypeID.ToString();

        if (int.Parse(this.ViewState["ActionType"].ToString()) == (int)SystemEnums.VendorActionType.Add) {
            this.trModifyReason.Visible = true;
        }
        if (int.Parse(this.ViewState["ActionType"].ToString()) == (int)SystemEnums.VendorActionType.Delete || int.Parse(this.ViewState["ActionType"].ToString()) == (int)SystemEnums.VendorActionType.Reactive) {
            DisableWhenDelete();
        }
    }

    public void DisableWhenDelete() {
        txtVendorName.ReadOnly = true;
        txtVendorAddress.ReadOnly = true;
        txtCity.ReadOnly = true;
        txtPostal.ReadOnly = true;
        txtContactName.ReadOnly = true;
        this.VendorTypeControl.IsVisible = "none";
        this.VendorTypeControl.IsNoClear = "true";
        this.UCFileUpload.IsView = true;

        this.txtPhoneNumber.ReadOnly = true;
        this.ddlOneTimeVendor.Enabled = false;
        this.ddlHoldVendor.Enabled = false;
        this.txtPurchaseingPostalCode.ReadOnly = true;
        this.txtAlphaSearchKey.ReadOnly = true;
        this.txtPurchasingCity.ReadOnly = true;
        this.txtPurchasingContact.ReadOnly = true;
        this.txtPurchasingAddress.ReadOnly = true;
        this.txtPurchasePhoneNumber.ReadOnly = true;
        this.ddlPaymentTerm.Enabled = false;
        this.ddlTransType.Enabled = false;
        this.ddlBankCode.Enabled = false;
        ddlVatRate.Enabled = false;
        txtBankName.ReadOnly = true;
        txtAccountNo.ReadOnly = true;
        this.txtRemark.ReadOnly = true;
        this.txtBankNo.ReadOnly = true;
        ddlACType.Enabled = false;
    }

    #endregion

    #region ClientID

    public string Postal_ClientID {
        get {
            return this.txtPostal.ClientID;
        }
    }

    public string PurchaseingPostal_ClientID {
        get {
            return this.txtPurchaseingPostalCode.ClientID;
        }
    }

    public string ContactName_ClientID {
        get {
            return this.txtContactName.ClientID;
        }
    }

    public string PurchasingContactName_ClientID {
        get {
            return this.txtPurchasingContact.ClientID;
        }
    }

    public string VendorAddress_ClientID {
        get {
            return this.txtVendorAddress.ClientID;
        }
    }

    public string PurchasingAddress_ClientID {
        get {
            return this.txtPurchasingAddress.ClientID;
        }
    }

    public string VendorCity_ClientID {
        get {
            return this.txtCity.ClientID;
        }
    }

    public string PurchasingCity_ClientID {
        get {
            return this.txtPurchasingCity.ClientID;
        }
    }

    public string PhoneNumber_ClientID {
        get {
            return this.txtPhoneNumber.ClientID;
        }
    }

    public string PurchasingPhoneNumber_ClientID {
        get {
            return this.txtPurchasePhoneNumber.ClientID;
        }
    }

    #endregion

    protected void SaveFormVendor(SystemEnums.FormStatus StatusID) {
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

        string VendorName = this.txtVendorName.Text;
        string VendorAddress = this.txtVendorAddress.Text;
        string City = this.txtCity.Text;
        string Country = this.txtPostal.Text;
        string ContactName = this.txtContactName.Text;
        int? VendorTypeID = null;
        if (!string.IsNullOrEmpty(this.VendorTypeControl.VendorTypeID)) {
            VendorTypeID = int.Parse(this.VendorTypeControl.VendorTypeID);
        }
        string VendorTypeName = this.VendorTypeControl.VendorTypeName;
        string PhoneNumber = this.txtPhoneNumber.Text;
        int OneTimeVendor = int.Parse(this.ddlOneTimeVendor.SelectedValue);
        int HoldVendor = int.Parse(this.ddlHoldVendor.SelectedValue);
        string PurchasingPostalCode = this.txtPurchaseingPostalCode.Text;
        string AlphaSearchKey = this.txtAlphaSearchKey.Text;
        string PurchasingContact = this.txtPurchasingContact.Text;
        string PurchasingAddress = this.txtPurchasingAddress.Text;
        string PurchasingCity = this.txtPurchasingCity.Text;
        string PurchasingPhoneNumber = this.txtPurchasePhoneNumber.Text;
        int? BankCodeID = null;
        if (!string.IsNullOrEmpty(this.ddlBankCode.SelectedValue)) {
            BankCodeID = int.Parse(this.ddlBankCode.SelectedValue);
        }
        int? MethodPaymentID = 1;
        int? PaymentTermID = null;
        if (!string.IsNullOrEmpty(ddlPaymentTerm.SelectedValue)) {
            PaymentTermID = int.Parse(this.ddlPaymentTerm.SelectedValue);
        }
        int? TransTypeID = null;
        if (!string.IsNullOrEmpty(this.ddlTransType.SelectedValue)) {
            TransTypeID = int.Parse(this.ddlTransType.SelectedValue);
        }
        int? VatRateID = null;
        if (!string.IsNullOrEmpty(ddlVatRate.SelectedValue)) {
            VatRateID = int.Parse(this.ddlVatRate.SelectedValue);
        }
        string BankName = this.txtBankName.Text;
        string AccountNo = this.txtAccountNo.Text;
        string BankNo = this.txtBankNo.Text;
        int? ACTypeID = null;
        if (!string.IsNullOrEmpty(ddlACType.SelectedValue)) {
            ACTypeID = int.Parse(this.ddlACType.SelectedValue); ;
        }
        string AttachmentFileName = this.UCFileUpload.AttachmentFileName;
        string RealAttachmentFileName = this.UCFileUpload.RealAttachmentFileName;
        string Remark = this.txtRemark.Text;
        string ModifyReason = this.txtModifyReason.Text;

        try {
            //提交或者退回修改后提交
            int ActionType = int.Parse(this.ViewState["ActionType"].ToString());
            int VendorID = 0;
            if (this.ViewState["VendorID"] != null && this.ViewState["VendorID"] != "") {
                VendorID = int.Parse(this.ViewState["VendorID"].ToString());
            }
            if (this.ViewState["ObjectId"] == null || RejectedFormID != null || Request["VendorFormId"] != null) {
                this.FormVendorBLL.AddFormVendorApply(RejectedFormID, UserID, ProxyStuffUserId, null, OrganizationUnitID, PositionID,
                 SystemEnums.FormType.VendorApply, StatusID, VendorID, VendorName, VendorAddress, City, Country, ContactName, VendorTypeID, PhoneNumber, OneTimeVendor,
                 HoldVendor, PurchasingPostalCode, AlphaSearchKey, PurchasingContact, PurchasingAddress, PurchasingCity, PurchasingPhoneNumber, BankCodeID, MethodPaymentID, PaymentTermID,
                 TransTypeID, VatRateID, BankName, AccountNo, BankNo, ACTypeID, AttachmentFileName, RealAttachmentFileName, Remark, ModifyReason, ActionType);
            } else {
                //草稿
                int FormID = (int)this.ViewState["ObjectId"];
                this.FormVendorBLL.UpdateFormVendorApply(FormID, StatusID, VendorID, VendorName, VendorAddress, City, Country, ContactName, VendorTypeID, PhoneNumber, OneTimeVendor,
                 HoldVendor, PurchasingPostalCode, AlphaSearchKey, PurchasingContact, PurchasingAddress, PurchasingCity, PurchasingPhoneNumber, BankCodeID, MethodPaymentID, PaymentTermID,
                 TransTypeID, VatRateID, BankName, AccountNo, BankNo, ACTypeID, AttachmentFileName, RealAttachmentFileName, Remark, ModifyReason);
            }
            this.Page.Response.Redirect("~/Home.aspx");
        } catch (Exception ex) {
            PageUtility.DealWithException(this.Page, ex);
        }
    }

    protected void SubmitBtn_Click(object sender, EventArgs e) {
        if (!IsSubmitValid())
            return;
        SaveFormVendor(SystemEnums.FormStatus.Awaiting);
    }

    protected void SaveBtn_Click(object sender, EventArgs e) {
        SaveFormVendor(SystemEnums.FormStatus.Draft);
    }

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
        this.FormVendorBLL.DeleteFormVendorApply(formID);
        this.Page.Response.Redirect("~/Home.aspx");
    }

    public bool IsSubmitValid() {

        if (string.IsNullOrEmpty(this.txtVendorName.Text)) {
            PageUtility.ShowModelDlg(this.Page, "请录入Vendor Name!", "please key in the Vendor Name！");
            return false;
        }

        if (string.IsNullOrEmpty(this.txtVendorAddress.Text)) {
            PageUtility.ShowModelDlg(this.Page, "请录入Vendor Address!", "please key in the Vendor Address！");
            return false;
        }

        if (string.IsNullOrEmpty(this.txtCity.Text)) {
            PageUtility.ShowModelDlg(this.Page, "请录入城市!", "please key in the City！");
            return false;
        }

        if (string.IsNullOrEmpty(this.txtPostal.Text)) {
            PageUtility.ShowModelDlg(this.Page, "请录入Postal!", "please key in the Postal！");
            return false;
        }

        if (string.IsNullOrEmpty(this.txtContactName.Text) || this.txtContactName.Text.Trim().ToUpper().Equals("NA")) {
            PageUtility.ShowModelDlg(this.Page, "请录入Contact Name!", "please key in the Contact Name！");
            return false;
        }

        if (string.IsNullOrEmpty(this.VendorTypeControl.VendorTypeName)) {
            PageUtility.ShowModelDlg(this.Page, "请选择供应商类型!", "please key in the VendorType！");
            return false;
        }

        if (string.IsNullOrEmpty(this.txtPhoneNumber.Text)) {
            PageUtility.ShowModelDlg(this.Page, "请选择供应商Phone Number!", "please key in the phone number！");
            return false;
        }

        if (string.IsNullOrEmpty(this.txtPurchaseingPostalCode.Text)) {
            PageUtility.ShowModelDlg(this.Page, "请选录入Purchasing Postal!", "please key in the purchasing postal！");
            return false;
        }

        if (string.IsNullOrEmpty(this.txtAlphaSearchKey.Text)) {
            PageUtility.ShowModelDlg(this.Page, "请录入Alpha Search Key!", "please key in the Alpha Search Key！");
            return false;
        }

        if (string.IsNullOrEmpty(this.txtPurchasingContact.Text) || this.txtPurchasingContact.Text.Trim().ToUpper().Equals("NA")) {
            PageUtility.ShowModelDlg(this.Page, "请录入Purchasing Contact!", "please key in the Purchasing Contact！");
            return false;
        }

        if (string.IsNullOrEmpty(this.txtPurchasingAddress.Text)) {
            PageUtility.ShowModelDlg(this.Page, "请录入Purchasing Address!", "please key in the Purchasing Address！");
            return false;
        }

        if (string.IsNullOrEmpty(this.txtPurchasingCity.Text)) {
            PageUtility.ShowModelDlg(this.Page, "请录入Purchasing City!", "please key in the Purchasing City！");
            return false;
        }

        if (string.IsNullOrEmpty(this.txtPurchasePhoneNumber.Text)) {
            PageUtility.ShowModelDlg(this.Page, "请录入Purchasing Phone Number!", "please key in the Purchasing Phone Number！");
            return false;
        }

        if (string.IsNullOrEmpty(this.ddlBankCode.SelectedValue)) {
            PageUtility.ShowModelDlg(this.Page, "请选择Bank Code!", "please select the Bank Code！");
            return false;
        }

        if (string.IsNullOrEmpty(this.ddlPaymentTerm.SelectedValue)) {
            PageUtility.ShowModelDlg(this.Page, "请选择Payment Term!", "please key in the Payment Term！");
            return false;
        }

        if (string.IsNullOrEmpty(this.ddlTransType.SelectedValue)) {
            PageUtility.ShowModelDlg(this.Page, "请选择Trans Type", "please key in the Trans Type！");
            return false;
        }

        if (string.IsNullOrEmpty(this.ddlACType.SelectedValue)) {
            PageUtility.ShowModelDlg(this.Page, "请选择AC Type", "please key in the AC Type！");
            return false;
        }

        if (this.ddlVatRate.Text == "0") {
            PageUtility.ShowModelDlg(this.Page, "请录入VAT Type!", "please key in the VAT Type！");
            return false;
        }

        if (string.IsNullOrEmpty(this.txtBankName.Text)) {
            PageUtility.ShowModelDlg(this.Page, "请录入Bank Name", "please key in the Bank Name！");
            return false;
        }

        if (string.IsNullOrEmpty(this.txtAccountNo.Text)) {
            PageUtility.ShowModelDlg(this.Page, "请录入Account No", "please key in the Account No！");
            return false;
        }
        if (string.IsNullOrEmpty(this.txtBankNo.Text)) {
            PageUtility.ShowModelDlg(this.Page, "请录入Bank No", "please key in the Bank No！");
            return false;
        }
        if (this.ViewState["VendorID"] != null && this.ViewState["VendorID"].ToString() != "") {
            string FormVendorNo = this.FormVendorBLL.QueryProcessingFormVendorNoByVendorID(int.Parse(this.ViewState["VendorID"].ToString()));
            if (!string.IsNullOrEmpty(FormVendorNo)) {
                PageUtility.ShowModelDlg(this.Page, "有正在审批的单据，单号为:" + FormVendorNo);
                return false;
            }
        }
        return true;
    }

    protected void txtVendorTypeName_TextChanged(object sender, EventArgs e) {
        MasterDataBLL ma = new MasterDataBLL();
        if (this.VendorTypeControl.VendorTypeID != string.Empty) {
            txtCurrency.Text = ma.GetCurrencyByID(ma.GetVendorTypeById(int.Parse(VendorTypeControl.VendorTypeID)).CurrencyID).CurrencyFullName;
            txtCompany.Text = ma.GetCompanyById(ma.GetVendorTypeById(int.Parse(VendorTypeControl.VendorTypeID)).CompanyID).CompanyName;
            txtCompanyCode.Text = ma.GetCompanyById(ma.GetVendorTypeById(int.Parse(VendorTypeControl.VendorTypeID)).CompanyID).CompanyCode;
        } else {
            txtCurrency.Text = "";
            txtCompany.Text = "";
            txtCompanyCode.Text = "";
        }
    }
}