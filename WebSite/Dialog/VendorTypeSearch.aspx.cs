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

public partial class Dialog_VendorTypeSearch : BasePage {
    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        String title = this.GetLocalResourceObject("titleLabel.Text").ToString();
        this.Page.Title = title;
    }

    protected void SearchButton_Click(object sender, EventArgs e) {
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

        searchStr += " and IsActive=1 ";

        this.VendorTypeObjectDataSource.SelectParameters["queryExpression"].DefaultValue = searchStr;

        this.gvVendorType.DataBind();
    }

    protected void gvVendorType_RowDataBound(object sender, GridViewRowEventArgs e) {
        Label lblVendorTypeName = (Label)e.Row.FindControl("lblVendorTypeName");
        if (lblVendorTypeName != null) {
            if (lblVendorTypeName.Text != string.Empty) {
                e.Row.Attributes.Add("onMouseOver", "SetNewColor(this);");
                e.Row.Attributes.Add("onMouseOut", "SetOldColor(this);");
                e.Row.Attributes.Add("onDblClick", Page.GetPostBackEventReference(this, e.Row.DataItemIndex.ToString()));
            }
        }
    }

    public void RaisePostBackEvent(string eventArgument) {
        GridViewSelectEventArgs e = null;
        int selectedRowIndex = -1;
        if (!string.IsNullOrEmpty(eventArgument)) {
            string[] args = eventArgument.Split('$');
            Int32.TryParse(args[0], out selectedRowIndex);
            e = new GridViewSelectEventArgs(selectedRowIndex);
            OnDblClick(e);
        }
    }

    protected virtual void OnDblClick(EventArgs e) {
        gvVendorType.EditIndex = ((GridViewSelectEventArgs)e).NewSelectedIndex;
        string VendorTypeID = gvVendorType.DataKeys[((GridViewSelectEventArgs)e).NewSelectedIndex].Value.ToString();
        Label lblVendorTypeName = (Label)gvVendorType.Rows[((GridViewSelectEventArgs)e).NewSelectedIndex].FindControl("lblVendorTypeName");
        string returnValue = lblVendorTypeName.Text;
        Response.Write(@"<script language='javascript'>var arryReturn=new Array();arryReturn[0]='" + VendorTypeID + "';" + "arryReturn[1]='" + returnValue + "';" +
            "window.returnValue=arryReturn;</script>");
        Response.Write(@"<script language='javascript'>window.close();</script>");
    }

    public string GetCurrencyByID(object ID) {
        return new MasterDataBLL().GetCurrencyByID((int)ID).CurrencyFullName;
    }

    public string GetCompanyNameByID(object ID) {
        return new MasterDataBLL().GetCompanyById((int)ID).CompanyName;
    }

    public string GetCompanyCodeByID(object ID) {
        return new MasterDataBLL().GetCompanyById((int)ID).CompanyCode;
    }
}