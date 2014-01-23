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

public partial class FormPurchase_PRApplyFirst : BasePage {

    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!this.IsPostBack) {
            String title = this.GetLocalResourceObject("titleLabel.Text").ToString();
            PageUtility.SetContentTitle(this.Page, title);
        }
    }

    protected void NextButton_Click(object sender, EventArgs e) {
        
        if ( PeriodDDL.SelectedValue == "0") {
            PageUtility.ShowModelDlg(this, "请选择费用期间!","please select period");
            return;
        }
        if (UCVendor.VendorID == string.Empty) {
            PageUtility.ShowModelDlg(this, "请选择Vendor!","please select Vendor");
            return;
        }
        if (UCItemCategory.ItemCategoryID == string.Empty) {
            PageUtility.ShowModelDlg(this, "请选择Item Category!", "please select item category");
            return;
        }
        if (this.PurchaseBudgetTypeDDL.SelectedValue == "0") {
            PageUtility.ShowModelDlg(this, "请选择预算类型!", "please select budget type");
            return;
        }

        this.Response.Redirect("~/FormPurchase/PRApply.aspx?PeriodPurchaseID=" + this.PeriodDDL.SelectedValue + "&VendorID=" + this.UCVendor.VendorID + "&ItemCategoryID=" + UCItemCategory.ItemCategoryID + "&PurchaseBudgetTypeID=" + this.PurchaseBudgetTypeDDL.SelectedValue + "&CurrencyID=" + this.CurrencyDDL.SelectedValue);

    }

}