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

public partial class FormRD_RDApplyFirst : BasePage {

    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!this.IsPostBack) {
            String title = this.GetLocalResourceObject("titleLabel.Text").ToString();
            PageUtility.SetContentTitle(this.Page, title);
        }
    }

    protected void NextButton_Click(object sender, EventArgs e) {
        if (ddlPeriod.SelectedValue == "0") {
            PageUtility.ShowModelDlg(this, "请选择费用期间!", "please select period");
            return;
        }
        if (string.IsNullOrEmpty(UCCustomer.CustomerID)) {
            PageUtility.ShowModelDlg(this, "请选择客户!", "please select customer");
            return;
        }
        if (ddlBrand.SelectedValue == "0") {
            PageUtility.ShowModelDlg(this, "请选择Brand!", "please select brand");
            return;
        }
        if (ExpenseSubCategoryDDL.SelectedValue == "0") {
            PageUtility.ShowModelDlg(this, "请选择费用小类!", "please select Expense Sub Category");
            return;
        }

        this.Response.Redirect("~/SampleRequest/RDSampleRequestApply.aspx?PeriodSaleID=" + this.ddlPeriod.SelectedValue + "&CurrencyID=" + this.CurrencyDDL.SelectedValue + "&CustomerID=" + this.UCCustomer.CustomerID + "&BrandID=" + ddlBrand.SelectedValue + "&ExpenseSubCategoryID=" + ExpenseSubCategoryDDL.SelectedValue);
    }
}