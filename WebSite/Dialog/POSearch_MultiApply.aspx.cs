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

public partial class Dialog_POSearch_MultiApply : BasePage {
    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        String title = this.GetLocalResourceObject("titleLabel.Text").ToString();
        this.Page.Title = title;
        if (!IsPostBack) {
            int MAAID = 0;
            if (Request["SettlementID"] != null) {
                if (int.TryParse(Request["SettlementID"].ToString(), out MAAID)) {
                    this.odsPO.SelectParameters["SettlementID"].DefaultValue = MAAID.ToString();
                    this.gvPO.DataBind();
                }
            }
        }
    }

    protected void gvPO_RowDataBound(object sender, GridViewRowEventArgs e) {
        Label lblVendorName = (Label)e.Row.FindControl("lblVendorName");
        Label lblBPCSPONo = (Label)e.Row.FindControl("lblBPCSPONo");
        if (lblVendorName != null) {
            if (lblVendorName.Text != string.Empty) {
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
        gvPO.EditIndex = ((GridViewSelectEventArgs)e).NewSelectedIndex;
        string FormPOID = gvPO.DataKeys[((GridViewSelectEventArgs)e).NewSelectedIndex].Value.ToString();
        Label PONo = (Label)gvPO.Rows[((GridViewSelectEventArgs)e).NewSelectedIndex].FindControl("lblFormNo");
        string returnValue = PONo.Text;
        Response.Write(@"<script language='javascript'>var arryReturn=new Array();arryReturn[0]='" + FormPOID + "';" + "arryReturn[1]='" + returnValue + "';" +
            "window.returnValue=arryReturn;</script>");
        Response.Write(@"<script language='javascript'>window.close();</script>");
    }
}