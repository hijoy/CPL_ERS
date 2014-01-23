using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
        if (ddlBrand.SelectedValue == "0") {
            PageUtility.ShowModelDlg(this, "请选择Brand!", "please select brand");
            return;
        }
        if (ddlCustomerChannel.SelectedValue == "0") {
            PageUtility.ShowModelDlg(this, "请选择渠道!", "please select customer channel");
            return;
        }
        if (ExpenseSubCategoryDDL.SelectedValue == "0") {
            PageUtility.ShowModelDlg(this, "请选择费用小类!", "please select Sub Expense Category");
            return;
        }
        this.Response.Redirect("~/FormRD/RDApply.aspx?PeriodSaleID=" + this.ddlPeriod.SelectedValue + "&ExpenseSubCategoryID=" + ExpenseSubCategoryDDL.SelectedValue + "&BrandID=" + ddlBrand.SelectedValue + "&CustomerChannelID=" + this.ddlCustomerChannel.SelectedValue + "&CurrencyID=" + this.CurrencyDDL.SelectedValue);
    }
}