using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObjects;

public partial class BaseData_CostLimit : BasePage{

    protected void Page_Load(object sender, EventArgs e){
        base.Page_Load(sender, e);
        if (!this.IsPostBack) {
            String title = this.GetLocalResourceObject("titleLabel.Text").ToString();
            PageUtility.SetContentTitle(this, title);
            this.Page.Title = title;

            int opViewId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.CostLimit, SystemEnums.OperateEnum.View);
            int opManageId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.CostLimit, SystemEnums.OperateEnum.Manage);
            AuthorizationDS.PositionRow position = (AuthorizationDS.PositionRow)this.Session["Position"];
            PositionRightBLL positionRightBLL = new PositionRightBLL();
            this.HasViewRight = positionRightBLL.CheckPositionRight(position.PositionId, opViewId);
            this.HasManageRight = positionRightBLL.CheckPositionRight(position.PositionId, opManageId);
            if (!this.HasViewRight && !HasManageRight) {
                Response.Redirect("~/ErrorPage/NoRightErrorPage.aspx");
                return;
            }
        }
    }

    protected bool HasViewRight {
        get {
            return (bool)this.ViewState["HasViewRight"];
        }
        set {
            this.ViewState["HasViewRight"] = value;
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

    protected string getSearchCondition() {
        String searchStr = "1=1";
        String temp = this.ddlSearchCityType.SelectedValue;
        if (temp != "-1") {
            searchStr += " and CityTypeID =" + temp ;
        }
        temp = this.ddlSearchManageExpenseItem.SelectedValue;
        if (temp != "0") {
            searchStr += " and ManageExpenseItemID =" + temp ;
        }
        temp = this.ddlSearchStaffLevel.SelectedValue;
        if (temp != "0") {
            searchStr += " and StaffLevelID = " + temp;
        }

        return searchStr;

    }

    protected void SearchButton_Click(object sender, EventArgs e) {
        // 重新绑定，进行查询处理。
        this.odsCostLimit.SelectParameters["queryExpression"].DefaultValue = getSearchCondition();
        this.gvCostLimit.DataBind();
        this.fvCostLimit.Visible = false;
        this.upCostLimit.Update();

    }


    public string GetCityTypeNameByID(object CityTypeID) {
        if ((int)CityTypeID == 0) {
            return "不适用";
        } else {
            return new MasterDataBLL().GetCityTypeById((int)CityTypeID).CityTypeName;
        }
    }

    public string GetStaffLevelNameByID(object StaffLevelID) {
        return new AuthorizationBLL().GetStaffLevelById((int)StaffLevelID).StaffLevelName;              
    }

    public string GetManageExpenseItemNameByID(object ManageExpenseItemID) {
        return new MasterDataBLL().GetManageExpenseItemById((int)ManageExpenseItemID).ManageExpenseItemName;
    }
}