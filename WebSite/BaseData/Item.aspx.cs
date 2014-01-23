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

public partial class BaseData_Item : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        if (!this.IsPostBack) {
            String title = this.GetLocalResourceObject("titleLabel.Text").ToString();
            PageUtility.SetContentTitle(this, title);
            this.Page.Title = title;
            int opViewId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.Item, SystemEnums.OperateEnum.View);           
            int opManageId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.Item, SystemEnums.OperateEnum.Manage);
            AuthorizationDS.PositionRow position = (AuthorizationDS.PositionRow)this.Session["Position"];
            PositionRightBLL positionRightBLL = new PositionRightBLL();
           
            this.HasManageRight = positionRightBLL.CheckPositionRight(position.PositionId, opManageId);
            if (!HasManageRight && !positionRightBLL.CheckPositionRight(position.PositionId, opViewId)) {
                Response.Redirect("~/ErrorPage/NoRightErrorPage.aspx");
                return;
            }
            this.ItemObjectDataSource.SelectParameters["queryExpression"].DefaultValue = "";
            
        }

    }


    protected bool HasManageRight {
        get {
            return (bool)this.ViewState["HasManageRight"];
        }
        set {
            this.ViewState["HasManageRight"] = value;
        }
    }


   
    protected void lbtnSearch_Click(object sender, EventArgs e) {
        String searchStr = "1=1";
        String temp = this.txtItemCodeBySearch.Text;
        if (temp != null && (!temp.Trim().Equals(""))) {
            searchStr += " and ItemCode like '%" + temp + "%'";
        }
        temp = this.txtItemNameBySearch.Text;
        if (temp != null && (!temp.Trim().Equals(""))) {
            searchStr += " and ItemName like '%" + temp + "%'";
        }
        temp = this.dplItemCategoryBySearch.SelectedValue;
        if (temp != null && (!temp.Trim().Equals(""))) {
            searchStr += " and ItemCategoryID = " + temp;
        }
        temp = this.txtUOMBySearch.Text;
        if (temp != null && (!temp.Trim().Equals(""))) {
            searchStr += " and UOM = " + temp;
        }
        temp = this.txtPackageBySearch.Text;
        if (temp != null && (!temp.Trim().Equals(""))) {
            searchStr += " and Package like '%" + temp + "%'";
        }
        temp = this.dplActiveBySearch.SelectedValue;
        if (temp != "3") {
            searchStr += " and IsActive = " + temp;
        }
        this.ItemObjectDataSource.SelectParameters["queryExpression"].DefaultValue = searchStr;
        this.ItemView.DataBind();
        this.upItem.Update();
    }

    public string GetItemCategoryByID(object ID) {
        return new MasterDataBLL().GetItemCategoryById((int)ID).ItemCategoryName;
    }
}