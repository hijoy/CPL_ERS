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

public partial class FormSale_SaleApplyFirst : BasePage {

    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!this.IsPostBack) {
            String title = this.GetLocalResourceObject("titleLabel.Text").ToString();
            PageUtility.SetContentTitle(this.Page, title);
        }
    }

    protected void NextButton_Click(object sender, EventArgs e) {

        if (PeriodDDL.SelectedValue == "0") {
            PageUtility.ShowModelDlg(this, "请选择费用期间!", "please select period");
            return;
        }
        if (string.IsNullOrEmpty(UCCustomer.CustomerID)) {
            PageUtility.ShowModelDlg(this, "请选择客户!", "please select customer");
            return;
        }
        if (BrandDDL.SelectedValue == "0") {
            PageUtility.ShowModelDlg(this, "请选择Brand!", "please select brand");
            return;
        }
        if (ddlExpenseCategory.SelectedValue == "0") {
            PageUtility.ShowModelDlg(this, "请选择费用大类!", "please select Expense Category");
            return;
        }
        if (ExpenseSubCategoryDDL.SelectedValue == "0") {
            PageUtility.ShowModelDlg(this, "请选择费用小类!", "please select Expense Sub Category");
            return;
        }
        MasterData.ExpenseSubCategoryRow subCategory = new MasterDataBLL().GetExpenseSubCategoryById(int.Parse(this.ExpenseSubCategoryDDL.SelectedValue));

        switch (subCategory.PageType) {
            case (int)SystemEnums.PageType.ActivityApply:
                this.Response.Redirect("~/FormSale/ActivityApply.aspx?PeriodSaleID=" + this.PeriodDDL.SelectedValue + "&CustomerID=" + this.UCCustomer.CustomerID + "&BrandID=" + BrandDDL.SelectedValue + "&ExpenseSubCategoryID=" + ExpenseSubCategoryDDL.SelectedValue + "&CurrencyID=" + this.CurrencyDDL.SelectedValue);
                break;
            case (int)SystemEnums.PageType.NoActivityApply:
                this.Response.Redirect("~/FormSale/NoActivityApply.aspx?PeriodSaleID=" + this.PeriodDDL.SelectedValue + "&CustomerID=" + this.UCCustomer.CustomerID + "&BrandID=" + BrandDDL.SelectedValue + "&ExpenseSubCategoryID=" + ExpenseSubCategoryDDL.SelectedValue + "&CurrencyID=" + this.CurrencyDDL.SelectedValue);
                break;
            case (int)SystemEnums.PageType.SaleSampleRequest:
                this.Response.Redirect("~/SampleRequest/SaleSampleRequestApply.aspx?PeriodSaleID=" + this.PeriodDDL.SelectedValue + "&CustomerID=" + this.UCCustomer.CustomerID + "&BrandID=" + BrandDDL.SelectedValue + "&ExpenseSubCategoryID=" + ExpenseSubCategoryDDL.SelectedValue + "&CurrencyID=" + this.CurrencyDDL.SelectedValue);
                break;
        }
    }

    protected void ddlExpenseCategory_SelectedIndexChanged(object sender, EventArgs e) {
        odsExpenseSubCategory.SelectCommand = "select 0 ExpenseSubCategoryID, ' Please select' ExpenseSubCategoryName Union SELECT ExpenseSubCategoryID, ExpenseSubCategoryName FROM [ExpenseSubCategory] join ExpenseCategory on ExpenseSubCategory.ExpenseCategoryID = ExpenseCategory.ExpenseCategoryID where BusinessType = 1 and ExpenseSubCategory.ExpenseCategoryID='" + ddlExpenseCategory.SelectedValue + "' order by ExpenseSubCategoryName";
        odsExpenseSubCategory.DataBind();
    }
}