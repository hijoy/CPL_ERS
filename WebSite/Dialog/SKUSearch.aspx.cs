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

public partial class Dialog_SKUSearch : BasePage {
    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        String title = this.GetLocalResourceObject("titleLabel.Text").ToString();
        this.Page.Title = title;
        if (!IsPostBack) {
            this.odsSKU.SelectParameters["queryExpression"].DefaultValue = "IsActive = 1";
            this.gvSKU.DataBind();
        }
    }

    protected void SearchButton_Click(object sender, EventArgs e) {
        String searchStr = "IsActive=1";
        String temp = this.txtSKUNoBySearch.Text;
        if (temp != null && (!temp.Trim().Equals(""))) {
            searchStr += " and SKUNo like '%" + temp + "%'";
        }
        temp = this.txtSKUNameBySearch.Text;
        if (temp != null && (!temp.Trim().Equals(""))) {
            searchStr += " and SKUName like '%" + temp + "%'";
        }
        temp = this.ddlBrandBySearch.SelectedValue;
        if (temp != null && (!temp.Trim().Equals(""))) {
            searchStr += " and BrandID = " + temp;
        }
        this.odsSKU.SelectParameters["queryExpression"].DefaultValue = searchStr;
        this.gvSKU.DataBind();
    }

    protected void gvSKU_RowDataBound(object sender, GridViewRowEventArgs e) {
        Label lblSKUNo = (Label)e.Row.FindControl("lblSKUNo");
        if (lblSKUNo != null) {
            if (lblSKUNo.Text != string.Empty) {
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
        gvSKU.EditIndex = ((GridViewSelectEventArgs)e).NewSelectedIndex;
        string SKUID = gvSKU.DataKeys[((GridViewSelectEventArgs)e).NewSelectedIndex].Value.ToString();
        Label lblSKUName = (Label)gvSKU.Rows[((GridViewSelectEventArgs)e).NewSelectedIndex].FindControl("lblSKUName");
        string returnValue = lblSKUName.Text;
        Response.Write(@"<script language='javascript'>var arryReturn=new Array();arryReturn[0]='" + SKUID + "';" + "arryReturn[1]='" + returnValue + "';" +
            "window.returnValue=arryReturn;</script>");
        Response.Write(@"<script language='javascript'>window.close();</script>");
    }

    public string GetBrandByID(object ID) {
        return new MasterDataBLL().GetBrandById((int)ID)[0].BrandName;
    }

    public string GetSKUTypeByID(object ID) {
        int TypeID = 0;
        if (int.TryParse(ID == null ? "" : ID.ToString(), out TypeID)) {
            return new MasterDataBLL().GetSKUTypeById(TypeID)[0].SKUTypeName;
        }
        return "";
    }

    public string GetSKUCategoryByID(object ID) {
        return new MasterDataBLL().GetSKUCategoryById((int)ID)[0].SKUCategoryName;
    }
}