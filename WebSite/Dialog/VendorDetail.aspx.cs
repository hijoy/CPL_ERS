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
using System.Diagnostics;
using BusinessObjects;
using System.Text;

public partial class Dialog_VendorDetail : BasePage {
    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        String title = this.GetLocalResourceObject("titleLabel.Text").ToString();
        this.Page.Title = title;

        int VendorID = int.Parse(Request["VendorID"]);
        this.ViewState["VendorID"] = VendorID;

        MasterData.VendorRow vendorRow = new MasterDataBLL().GetVendorByID(VendorID);
        txtVendorName.Text = vendorRow.VendorName;
        txtVendorAddress.Text = vendorRow.VendorAddress;
        txtCity.Text = vendorRow.City;
        txtPostal.Text = vendorRow.Postal;
        txtContactName.Text = vendorRow.ContactName;
        MasterDataBLL masterDataBLL = new MasterDataBLL();
        MasterData.VendorTypeRow VendorTypeRow = masterDataBLL.GetVendorTypeById(vendorRow.VendorTypeID);
        txtVendorType.Text = VendorTypeRow.VendorTypeName;
        txtCurrency.Text = masterDataBLL.GetCurrencyByID(VendorTypeRow.CurrencyID).CurrencyFullName;
        txtCompany.Text = masterDataBLL.GetCompanyById(VendorTypeRow.CompanyID).CompanyName;
        txtCompanyCode.Text = masterDataBLL.GetCompanyById(VendorTypeRow.CompanyID).CompanyCode;
        this.txtPhoneNumber.Text = vendorRow.PhoneNumber;
        this.txtOneTimeVendor.Text = vendorRow.OneTimeVendor ? "Y" : "N";
        this.txtHoldVendor.Text = vendorRow.HoldVendor ? "Y" : "N";
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
        this.txtBankCode.Text = vendorRow.BankCode;
        this.txtMethodPayment.Text = masterDataBLL.GetMethodPaymentById(vendorRow.MethodPaymentID)[0].MethodPaymentName;
        this.txtPaymentTerm.Text = masterDataBLL.GetPaymentTermById(vendorRow.PaymentTermID)[0].PaymentTermName;
        this.txtTransType.Text = masterDataBLL.GetTransTypeById(vendorRow.TransTypeID)[0].TransTypeName;
        txtVATRate.Text = masterDataBLL.GetVatTypeById(vendorRow.VATTypeID)[0].VatTypeName;
        txtBankName.Text = vendorRow.BankName.ToString();
        if (!vendorRow.IsAccountNoNull()) {
            txtAccountNo.Text = vendorRow.AccountNo;
        }
        if (!vendorRow.IsBankNoNull()) {
            this.txtBankNo.Text = vendorRow.BankNo;
        }
        if (!vendorRow.IsACTypeIDNull()) {
            this.txtACType.Text = masterDataBLL.GetACTypeById(vendorRow.ACTypeID)[0].ACTypeName;
        }
        if (!vendorRow.IsRemarkNull()) {
            this.txtRemark.Text = vendorRow.Remark;
        }
    }

    public void RaisePostBackEvent(string eventArgument) {

    }
}