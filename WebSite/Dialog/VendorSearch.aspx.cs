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

public partial class Dialog_VendorSearch : BasePage {
    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        String title = this.GetLocalResourceObject("titleLabel.Text").ToString();
        this.Page.Title = title;
        this.VendorObjectDataSource.SelectParameters["queryExpression"].DefaultValue = "IsActive = 1";
        if (Request["IsLimited"] != null && Request["IsLimited"] == "true") {
            this.VendorObjectDataSource.SelectParameters["queryExpression"].DefaultValue += (" and CompanyID=" + GetCompanyID());
        }
    }

    protected void SearchButton_Click(object sender, EventArgs e) {
        String searchStr = "1=1";
        String temp = this.txtVendorCodeBySearch.Text;
        if (temp != null && (!temp.Trim().Equals(""))) {
            searchStr += " and VendorCode like '%" + temp + "%'";
        }

        temp = this.txtVendorName.Text;
        if (temp != null && (!temp.Trim().Equals(""))) {
            searchStr += " and VendorName like '%" + temp + "%'";
        }

        searchStr += " and IsActive=1 ";

        if (Request["IsLimited"] != null && Request["IsLimited"] == "true") {
            searchStr += "and CompanyID=" + GetCompanyID();
        }

        this.VendorObjectDataSource.SelectParameters["queryExpression"].DefaultValue = searchStr;

        this.gvVendor.DataBind();
    }

    protected void gvVendor_RowDataBound(object sender, GridViewRowEventArgs e) {
        Label lblVendorName = (Label)e.Row.FindControl("lblVendorName");
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
        gvVendor.EditIndex = ((GridViewSelectEventArgs)e).NewSelectedIndex;
        string VendorID = gvVendor.DataKeys[((GridViewSelectEventArgs)e).NewSelectedIndex].Value.ToString();
        Label lblVendorName = (Label)gvVendor.Rows[((GridViewSelectEventArgs)e).NewSelectedIndex].FindControl("lblVendorName");
        string returnValue = lblVendorName.Text;
        Response.Write(@"<script language='javascript'>var arryReturn=new Array();arryReturn[0]='" + VendorID + "';" + "arryReturn[1]='" + returnValue + "';" +
            "window.returnValue=arryReturn;</script>");
        Response.Write(@"<script language='javascript'>window.close();</script>");
    }

    public string GetVendorTypeByID(object ID) {
        return new MasterDataBLL().GetVendorTypeById((int)ID).VendorTypeName;
    }

    public string GetCompanyCode(object ID) {
        MasterDataBLL msdBLL = new MasterDataBLL();
        MasterData.VendorTypeRow vType = msdBLL.GetVendorTypeById((int)ID);
        MasterData.CompanyRow company = msdBLL.GetCompanyById(vType.CompanyID);
        return company.CompanyCode;
    }

    public string GetCompanyID() {
        MasterDataBLL mdBLL = new MasterDataBLL();
        OUTreeBLL otBLL = new OUTreeBLL();
        AuthorizationDS.PositionRow rowPosition = (AuthorizationDS.PositionRow)Session["Position"];
        AuthorizationDS.OrganizationUnitRow rowOU = GetNearestCostCenter(rowPosition.OrganizationUnitId);
        MasterData.CostCenterRow rowCostCenter = mdBLL.GetCostCenterById(rowOU.CostCenterID);
        return rowCostCenter.CompanyID.ToString();
    }

    public AuthorizationDS.OrganizationUnitRow GetNearestCostCenter(int OUID) {
        OUTreeBLL otBLL = new OUTreeBLL();
        AuthorizationDS.OrganizationUnitRow rowOU = otBLL.GetOrganizationUnitById(OUID);
        if (rowOU.IsCostCenterIDNull()) {
            return GetNearestCostCenter(rowOU.ParentOrganizationUnitId);
        } else {
            return rowOU;
        }
    }

}