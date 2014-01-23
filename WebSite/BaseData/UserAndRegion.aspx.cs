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

public partial class Base_UserAndRegion : BasePage {
    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!this.IsPostBack) {
            String title = this.GetLocalResourceObject("titleLabel.Text").ToString();
            PageUtility.SetContentTitle(this, title);

            int opManageId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.UserAndRegion, SystemEnums.OperateEnum.Manage);
            AuthorizationDS.PositionRow position = (AuthorizationDS.PositionRow)this.Session["Position"];
            if (!new PositionRightBLL().CheckPositionRight(position.PositionId, opManageId)) {
                Response.Redirect("~/ErrorPage/NoRightErrorPage.aspx");
                return;
            }
        }
    }

    #region ProxyReimburse event

    protected void lbtnSearch_Click(object sender, EventArgs e) {
        string searchStr = "1=1";
        if (!string.IsNullOrEmpty(this.txtUserName.Text)) {
            searchStr += " and StuffUserID in (select StuffUserID from StuffUser where StuffName like '%" + this.txtUserName.Text + "%')";
        }
        if (this.ddlCustomerRegion.SelectedValue != "0") {
            searchStr += " and CustomerRegionID=" + this.ddlCustomerRegion.SelectedValue;
        }
        this.odsUserAndRegion.SelectParameters["queryExpression"].DefaultValue = searchStr;
        this.gvUserAndRegion.DataBind();
        this.upUserAndRegion.Update();
    }

    public string GetUserNameByID(object ID) {
        return new AuthorizationBLL().GetStuffUserById((int)ID).StuffName;
    }

    public string GetCustomerRegionNameByID(object CustomerRegionID) {
        return new MasterDataBLL().GetCustomerRegionById((int)CustomerRegionID).CustomerRegionName;
    }

    protected void odsUserAndRegion_Inserting(object sender, ObjectDataSourceMethodEventArgs e) {
    }
    

    #endregion

}
