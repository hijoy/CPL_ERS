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

public partial class FormMarketing_MarketingApplyFirst : BasePage {

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
        if (ExpenseCategoryDDL.SelectedValue == "0") {
            PageUtility.ShowModelDlg(this, "请选择费用大类!", "please select Expense Category");
            return;
        }
        this.Response.Redirect("~/FormMarketing/MarketingApply.aspx?PeriodSaleID=" + this.ddlPeriod.SelectedValue +"&ExpenseCategoryID="+ExpenseCategoryDDL.SelectedValue+ "&BrandID=" + ddlBrand.SelectedValue + "&CustomerChannelID=" + this.ddlCustomerChannel.SelectedValue + "&CurrencyID=" + this.CurrencyDDL.SelectedValue);
    }
}