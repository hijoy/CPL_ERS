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

public partial class BaseData_VendorType : BasePage
{
    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!this.IsPostBack) {
            String title = this.GetLocalResourceObject("titleLabel.Text").ToString();
            PageUtility.SetContentTitle(this, title);
            this.Page.Title = title;
           
            int opManageId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.VendorType, SystemEnums.OperateEnum.Manage);
            AuthorizationDS.PositionRow position = (AuthorizationDS.PositionRow)this.Session["Position"];
            if (!new PositionRightBLL().CheckPositionRight(position.PositionId, opManageId)) {
                Response.Redirect("~/ErrorPage/NoRightErrorPage.aspx");
                return;
            }
        }
    }

    protected void SearchButton_Click(object sender, EventArgs e) {
        this.VendorTypeObjectDataSource.SelectParameters["queryExpression"].DefaultValue = getSearchCondition();
        VendorTypeView.DataBind();
    }

    protected string getSearchCondition() {

        String searchStr = "1=1";
        String temp = this.txtVendorTypeNameBySearch.Text;
        if (temp != null && (!temp.Trim().Equals(""))) {
            searchStr += " and VendorTypeName like '%" + temp + "%'";
        }

        temp = this.ddlCompany.SelectedValue;
        if (temp != null && (!temp.Trim().Equals(""))) {
            searchStr += " and CompanyID = " + temp;
        }


        temp = this.ddlCurrency.SelectedValue;
        if (temp != null && (!temp.Trim().Equals(""))) {
            searchStr += " and CurrencyID = " + temp;
        }



        return searchStr;

    }


    public string GetCurrencyByID(object ID) {
        return new MasterDataBLL().GetCurrencyByID((int)ID).CurrencyFullName;
    }

    public string GetCompanyNameByID(object ID) {
        return new MasterDataBLL().GetCompanyById((int)ID).CompanyName;
    }
   
}