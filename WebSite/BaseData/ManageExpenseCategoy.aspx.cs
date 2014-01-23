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


public partial class BaseData_ManageExpenseCategoy : BasePage {
    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!this.IsPostBack) {
            String title = this.GetLocalResourceObject("titleLabel.Text").ToString();
            PageUtility.SetContentTitle(this, title);
            this.Page.Title = title;

            int opViewId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.ExpenseManageType, SystemEnums.OperateEnum.View);
            int opManageId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.ExpenseManageType, SystemEnums.OperateEnum.Manage);
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

    protected void gvManageExpenseCategoy_SelectedIndexChanged(object sender, EventArgs e) {
        if (this.gvManageExpenseCategoy.SelectedIndex >= 0) {
            this.gvManageExpenseItem.Visible = true;
            if (this.HasManageRight) {
                this.fvManageExpenseItem.Visible = true;
            } else {
                this.fvManageExpenseItem.Visible = false;
            }
            this.gvManageExpenseItem.EditIndex = -1;
            this.gvManageExpenseItem.SelectedIndex = -1;

            this.odsManageExpenseItem.SelectParameters["ManageExpenseCategoyID"].DefaultValue = gvManageExpenseCategoy.SelectedValue.ToString();
            this.gvManageExpenseItem.DataBind();
            //会计科目
            this.gvAccounting.Visible = false;
            this.fvAccounting.Visible = false;
        } else {
            this.gvManageExpenseItem.Visible = false;
            this.fvManageExpenseItem.Visible = false;
            this.gvAccounting.Visible = false;
            this.fvAccounting.Visible = false;

        }
        this.upManageExpenseItem.Update();
        this.upAccounting.Update();
    }


    #region ManageExpenseItem event

    protected void odsManageExpenseItem_Selecting(object sender, ObjectDataSourceMethodEventArgs e) {
        e.InputParameters["ManageExpenseCategoyID"] = gvManageExpenseCategoy.SelectedValue;
    }

    protected void odsManageExpenseItem_Inserting(object sender, ObjectDataSourceMethodEventArgs e) {
        e.InputParameters["ManageExpenseCategoyID"] = gvManageExpenseCategoy.SelectedValue;
    }

    protected void odsManageExpenseItem_Updating(object sender, ObjectDataSourceMethodEventArgs e) {
        e.InputParameters["ManageExpenseCategoyID"] = gvManageExpenseCategoy.SelectedValue;
    }


    public String getPageType(Object pageType) {
        int pageTypeId = int.Parse(pageType.ToString());
        if (pageTypeId == 1) {
            return "差旅费报销";
        } else if (pageTypeId == 3) {
            return "招待费报销";
        } else if (pageTypeId == 4) {
            return "日常费用报销";
        } else {
            return "";
        }
    }
    #endregion


    protected void gvManageExpenseItem_SelectedIndexChanged(object sender, EventArgs e) {
        if (this.gvManageExpenseItem.SelectedIndex >= 0) {
            this.gvAccounting.Visible = true;
            if (this.HasManageRight) {
                this.fvAccounting.Visible = true;
            } else {
                this.fvAccounting.Visible = false;
            }
            this.gvAccounting.EditIndex = -1;
            this.gvAccounting.SelectedIndex = -1;

            this.odsAccounting.SelectParameters["ManageExpenseItemID"].DefaultValue = gvManageExpenseItem.SelectedValue.ToString();
            this.gvAccounting.DataBind();
        } else {
            this.gvAccounting.Visible = false;
            this.fvAccounting.Visible = false;
        }
        this.upAccounting.Update();
    }

    protected void odsAccounting_Inserting(object sender, ObjectDataSourceMethodEventArgs e) {
        e.InputParameters["ManageExpenseItemID"] = gvManageExpenseItem.SelectedValue;
    }

    public String GetCostCenterNameById(Object CostCenterID) {
        int id = int.Parse(CostCenterID.ToString());
        MasterData.CostCenterRow cc = new MasterDataBLL().GetCostCenterById(id);
        return cc.CostCenterCode+"-"+cc.CostCenterName;
    }

}