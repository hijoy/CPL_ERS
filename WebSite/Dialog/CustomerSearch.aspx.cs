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

public partial class Dialog_CustomerSearch : BasePage {
    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!IsPostBack) {
            String title = this.GetLocalResourceObject("titleLabel.Text").ToString();
            this.Page.Title = title;
            //去掉IsActive=0的数据
            String searchStr = "1=1";
            searchStr += " and IsActive = 1";
            this.CustomerObjectDataSource.SelectParameters["queryExpression"].DefaultValue = searchStr;
            this.gvCustomer.DataBind();
        }
    }

    protected void SearchButton_Click(object sender, EventArgs e) {
        String searchStr = "1=1";
        String temp = this.txtCustNoBySearch.Text;
        if (temp != null && (!temp.Trim().Equals(""))) {
            searchStr += " and CustomerNo like '%" + temp + "%'";
        }
        temp = this.txtCustNameBySearch.Text;
        if (temp != null && (!temp.Trim().Equals(""))) {
            searchStr += " and CustomerName like '%" + temp + "%'";
        }
        temp = this.txtCity.Text;
        if (temp != null && (!temp.Trim().Equals(""))) {
            searchStr += " and City like '%" + temp + "%'";
        }
        temp = this.ddlCustomerTypeBySearch.SelectedValue;
        if (temp != null && (!temp.Trim().Equals(""))) {
            searchStr += " and CustomerTypeID = " + temp;
        }
        temp = this.ddlCustomerRegion.SelectedValue;
        if (temp != null && (!temp.Trim().Equals(""))) {
            searchStr += " and CustomerRegionID = " + temp;
        }
        temp = this.ddlCustomerChannel.SelectedValue;
        if (temp != null && (!temp.Trim().Equals(""))) {
            searchStr += " and CustomerChannelID = " + temp;
        }
        searchStr += " and IsActive = 1";
        this.CustomerObjectDataSource.SelectParameters["queryExpression"].DefaultValue = searchStr;
        this.gvCustomer.DataBind();
    }

    protected void gvCustomer_RowDataBound(object sender, GridViewRowEventArgs e) {
        Label lblCustomerNo = (Label)e.Row.FindControl("lblCustomerNo");
        if (lblCustomerNo != null) {
            if (lblCustomerNo.Text != string.Empty) {
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
        gvCustomer.EditIndex = ((GridViewSelectEventArgs)e).NewSelectedIndex;
        string CustomerID = gvCustomer.DataKeys[((GridViewSelectEventArgs)e).NewSelectedIndex].Value.ToString();
        Label lblCustomerName = (Label)gvCustomer.Rows[((GridViewSelectEventArgs)e).NewSelectedIndex].FindControl("lblCustomerName");
        Label lblCustomerNo = (Label)gvCustomer.Rows[((GridViewSelectEventArgs)e).NewSelectedIndex].FindControl("lblCustomerNo");
        string returnValue = lblCustomerName.Text;
        if (Request["ShowNo"] != null) {
            returnValue = lblCustomerNo.Text + " - " + lblCustomerName.Text;
        }
        Response.Write(@"<script language='javascript'>var arryReturn=new Array();arryReturn[0]='" + CustomerID + "';" + "arryReturn[1]='" + returnValue + "';" +
            "window.returnValue=arryReturn;</script>");
        Response.Write(@"<script language='javascript'>window.close();</script>");
    }

    public string GetCustomerChannelByID(object ID) {
        return new MasterDataBLL().GetCustomerChannelById((int)ID)[0].CustomerChannelName;
    }

    public string GetCustomerTypeByID(object ID) {
        return new MasterDataBLL().GetCustomerTypeById((int)ID).CustomerTypeName;
    }

    public string GetCustomerRegionByID(object ID) {
        return new MasterDataBLL().GetCustomerRegionById((int)ID).CustomerRegionName;
    }
}