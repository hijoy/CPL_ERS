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

public partial class Dialog_ItemSearch : BasePage
{
    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if(!IsPostBack){
            String title = this.GetLocalResourceObject("titleLabel.Text").ToString();
            this.Page.Title = title;
            SearchButton_Click(null,null);
        }
        
    }

    protected void SearchButton_Click(object sender, EventArgs e) {
        String searchStr = "1=1";
        String temp = this.txtItemCodeBySearch.Text;
        if (temp != null && (!temp.Trim().Equals(""))) {
            searchStr += " and ItemCode like '%" + temp + "%'";
        }
        temp = this.txtItemNameBySearch.Text;
        if (temp != null && (!temp.Trim().Equals(""))) {
            searchStr += " and ItemName like '%" + temp + "%'";
        }
        temp = this.txtUOMBySearch.Text;
        if (temp != null && (!temp.Trim().Equals(""))) {
            searchStr += " and UOM like '%" + temp + "%'";
        }

       
        if (Request["ItemCategoryID"]!=null) {
            searchStr += "and ItemCategoryID='" + Request["ItemCategoryID"].ToString() + "'";
        }

        searchStr += "and IsActive='1'";

        this.ItemObjectDataSource.SelectParameters["queryExpression"].DefaultValue = searchStr;

        this.ItemView.DataBind();
    }

    protected void ItemView_RowDataBound(object sender, GridViewRowEventArgs e) {


        Label lblItemName = (Label)e.Row.FindControl("lblItemName");
        if (lblItemName != null) {
            if (lblItemName.Text != string.Empty) {
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
        ItemView.EditIndex = ((GridViewSelectEventArgs)e).NewSelectedIndex;
        string ItemID = ItemView.DataKeys[((GridViewSelectEventArgs)e).NewSelectedIndex].Value.ToString();
        Label lblItemName = (Label)ItemView.Rows[((GridViewSelectEventArgs)e).NewSelectedIndex].FindControl("lblItemName");
        string returnValue = lblItemName.Text;
        Response.Write(@"<script language='javascript'>var arryReturn=new Array();arryReturn[0]='" + ItemID + "';" + "arryReturn[1]='" + returnValue + "';" +
            "window.returnValue=arryReturn;</script>");
        Response.Write(@"<script language='javascript'>window.close();</script>");
    }

    public string GetItemCategoryByID(object ID) {
        return new MasterDataBLL().GetItemCategoryById((int)ID).ItemCategoryName;
    }
}