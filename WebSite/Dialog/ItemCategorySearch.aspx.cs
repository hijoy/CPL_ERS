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

public partial class Dialog_ItemCategorySearch : BasePage {
    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        this.Page.Title = "Item Category";
    }

    protected void gvItemCategory_RowDataBound(object sender, GridViewRowEventArgs e) {
        Label lblItemCategoryCode = (Label)e.Row.FindControl("lblItemCategoryCode");
        if (lblItemCategoryCode != null) {
            if (lblItemCategoryCode.Text != string.Empty) {
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
        gvItemCategory.EditIndex = ((GridViewSelectEventArgs)e).NewSelectedIndex;
        string ItemCategoryID = gvItemCategory.DataKeys[((GridViewSelectEventArgs)e).NewSelectedIndex].Value.ToString();
        Label lblItemCategoryName = (Label)gvItemCategory.Rows[((GridViewSelectEventArgs)e).NewSelectedIndex].FindControl("lblItemCategoryName");
        string returnValue = lblItemCategoryName.Text;
        Response.Write(@"<script language='javascript'>var arryReturn=new Array();arryReturn[0]='" + ItemCategoryID + "';" + "arryReturn[1]='" + returnValue + "';" +
            "window.returnValue=arryReturn;</script>");
        Response.Write(@"<script language='javascript'>window.close();</script>");
    }


}