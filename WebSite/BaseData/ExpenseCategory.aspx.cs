using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObjects;

public partial class BaseData_ExpenseSubCategory : BasePage
{
    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!this.IsPostBack) {
            String title = this.GetLocalResourceObject("titleLabel.Text").ToString();
            PageUtility.SetContentTitle(this, title);
            this.Page.Title = title;

            int opViewId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.ExpenseCategory, SystemEnums.OperateEnum.View);
            int opManageId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.ExpenseCategory, SystemEnums.OperateEnum.Manage);
            AuthorizationDS.PositionRow position = (AuthorizationDS.PositionRow)this.Session["Position"];
            PositionRightBLL positionRightBLL = new PositionRightBLL();
            this.HasViewRight = positionRightBLL.CheckPositionRight(position.PositionId, opViewId);
            this.HasManageRight = positionRightBLL.CheckPositionRight(position.PositionId, opManageId);
            if (!this.HasViewRight && !HasManageRight) {
                Response.Redirect("~/ErrorPage/NoRightErrorPage.aspx");
                return;
            }
        } else {
            PageUtility.CloseModelDlg(this);
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

    #region ExpenseCategory event

    protected void gvExpenseCategory_SelectedIndexChanged(object sender, EventArgs e) {
        if (this.gvExpenseCategory.SelectedIndex >= 0) {
            this.gvExpenseSubCategory.Visible = true;
            if (this.HasManageRight) {
                this.fvExpenseSubCategory.Visible = true;
            } else {
                this.fvExpenseSubCategory.Visible = false;
            }
            this.gvExpenseSubCategory.EditIndex = -1;
            this.gvExpenseSubCategory.SelectedIndex = -1;

            this.odsExpenseSubCategory.SelectParameters["ExpenseCategoryId"].DefaultValue = gvExpenseCategory.SelectedValue.ToString();
            this.gvExpenseSubCategory.DataBind();
        } else {
            this.gvExpenseSubCategory.Visible = false;
            this.fvExpenseSubCategory.Visible = false;
        }
        this.upExpenseSubCategory.Update();
    }

    protected void odsExpenseCategory_Inserted(object sender, ObjectDataSourceStatusEventArgs e) {
        if (e.Exception != null) {
            PageUtility.DealWithException(this, e.Exception);
            e.ExceptionHandled = true;
        }
    }

    protected void odsExpenseCategory_Updated(object sender, ObjectDataSourceStatusEventArgs e) {
        if (e.Exception != null) {
            PageUtility.DealWithException(this, e.Exception);
            e.ExceptionHandled = true;
        }
    }

    #endregion

    #region ExpenseSubCategory event

    protected void odsExpenseSubCategory_Selecting(object sender, ObjectDataSourceMethodEventArgs e) {
        e.InputParameters["ExpenseCategoryID"] = gvExpenseCategory.SelectedValue;
    }

    protected void odsExpenseSubCategory_Inserting(object sender, ObjectDataSourceMethodEventArgs e) {
        e.InputParameters["ExpenseCategoryID"] = gvExpenseCategory.SelectedValue;
    }

    protected void fvExpenseSubCategory_ItemInserting(object sender, FormViewInsertEventArgs e) {
        e.Values["ExpenseCategoryID"] = gvExpenseCategory.SelectedValue;
    }

    protected void odsExpenseSubCategory_Inserted(object sender, ObjectDataSourceStatusEventArgs e) {
        if (e.Exception != null) {
            PageUtility.DealWithException(this, e.Exception);
            e.ExceptionHandled = true;
        }
    }

    protected void odsExpenseSubCategory_Updated(object sender, ObjectDataSourceStatusEventArgs e) {
        if (e.Exception != null) {
            PageUtility.DealWithException(this, e.Exception);
            e.ExceptionHandled = true;
        }
    }

    protected String getPageType(object PageType) {
        string result = string.Empty;
        switch ((SystemEnums.PageType)PageType) {
            case SystemEnums.PageType.ActivityApply:
                //result = "活动申请";
                result = GetLocalResourceObject("ListItem_PageType1.Text").ToString();
                break;
            case SystemEnums.PageType.NoActivityApply:
                //result = "非活动申请";
                result = GetLocalResourceObject("ListItem_PageType2.Text").ToString();
                break;
            case SystemEnums.PageType.RDApply:
                //result = "RD申请";
                result = GetLocalResourceObject("ListItem_PageType4.Text").ToString();
                break;
            case SystemEnums.PageType.FormMarketingApply:
                //result = "市场部申请";
                result = GetLocalResourceObject("ListItem_PageType3.Text").ToString();
                break;
            case SystemEnums.PageType.SaleSampleRequest:
                //result = "销售部SampleRequest";
                result = GetLocalResourceObject("ListItem_PageType5.Text").ToString();
                break;
            case SystemEnums.PageType.MarketingSampleRequest:
                //result = "市场部SampleRequest";
                result = GetLocalResourceObject("ListItem_PageType6.Text").ToString();
                break;
        }
        return result;
    }

    protected String getBusinessType(object BusinessType) {
        string result = string.Empty ;
        switch ((SystemEnums.BusinessType)BusinessType) {
            case SystemEnums.BusinessType.Sales:
                //result= "销售部";
                result = GetLocalResourceObject("ListItem1.Text").ToString();
                break;
            case SystemEnums.BusinessType.Marketing:
                //result= "市场部";
                result = GetLocalResourceObject("ListItem2.Text").ToString();
                break;
            case SystemEnums.BusinessType.RD:
                //result = "R&D";
                result = GetLocalResourceObject("ListItem3.Text").ToString();
                break;
        }
        return result;
    }
    #endregion
}