using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObjects;

public partial class BaseData_ExpenseSubCategory : BasePage {
    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!this.IsPostBack) {
            String title = this.GetLocalResourceObject("titleLabel.Text").ToString();
            PageUtility.SetContentTitle(this, title);
            this.Page.Title = title;

            int opViewId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.ExpenseItem, SystemEnums.OperateEnum.View);
            int opManageId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.ExpenseItem, SystemEnums.OperateEnum.Manage);
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

    #region ExpenseItem event

    protected void odsExpenseItem_Inserted(object sender, ObjectDataSourceStatusEventArgs e) {
        if (e.Exception != null) {
            PageUtility.DealWithException(this, e.Exception);
            e.ExceptionHandled = true;
        }
    }

    protected void odsExpenseItem_Updated(object sender, ObjectDataSourceStatusEventArgs e) {
        if (e.Exception != null) {
            PageUtility.DealWithException(this, e.Exception);
            e.ExceptionHandled = true;
        }
    }

    protected string getExpenseSubCategoryName(object id) {
        return new MasterDataBLL().GetExpenseSubCategoryById((int)id).ExpenseSubCategoryName;
    }

    protected string getSearchCondition() {
        String searchStr="1=1";
        String temp=string.Empty;
        temp=this.txtAccountingCode.Text;
        if (temp.Length != 0 && temp != "") {
            searchStr += " AND AccountingCode like '%" + temp + "%'";
        }
        temp = this.txtExpenseItemName.Text;
        if (temp.Length != 0 && temp != "") {
            searchStr += " AND ExpenseItemName like '%" + temp + "%'";
        }
        temp = this.ddlActive.SelectedItem.Value;
        if (temp.Length != 0 && temp != "") {
            searchStr += " AND IsActive = " + temp ;
        }
        temp = this.ddlExpenseSubCategory.SelectedValue;
        if ( temp != "0") {
            searchStr += " AND ExpenseSubCategoryID = " + temp;
        }

        return searchStr;
    }

    protected void SearchButton_Click(object sender, EventArgs e) {
        //查询方法
        this.odsExpenseItem.SelectParameters["queryExpression"].DefaultValue = this.getSearchCondition();
        this.gvExpenseItem.DataBind();
    }
    #endregion
}