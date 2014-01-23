using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObjects;

public partial class BaseData_City : BasePage
{
    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!this.IsPostBack) {
            String title = this.GetLocalResourceObject("titleLabel.Text").ToString();
            PageUtility.SetContentTitle(this, title);
            this.Page.Title = title;

            int opViewId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.CityType, SystemEnums.OperateEnum.View);
            int opManageId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.CityType, SystemEnums.OperateEnum.Manage);
            AuthorizationDS.PositionRow position = (AuthorizationDS.PositionRow)this.Session["Position"];
            PositionRightBLL positionRightBLL = new PositionRightBLL();
            if (!positionRightBLL.CheckPositionRight(position.PositionId, opViewId) && !positionRightBLL.CheckPositionRight(position.PositionId, opManageId)) {
                Response.Redirect("~/ErrorPage/NoRightErrorPage.aspx");
                return;
            }
        } else {
            PageUtility.CloseModelDlg(this);
        }
    }


    #region CityType event

    protected void gvCityType_SelectedIndexChanged(object sender, EventArgs e) {
        if (this.gvCityType.SelectedIndex >= 0) {
            this.gvCity.Visible = true;
            this.gvCity.EditIndex = -1;
            this.gvCity.SelectedIndex = -1;
            this.fvCity.Visible = true;
            this.odsCity.SelectParameters["CityTypeId"].DefaultValue = gvCityType.SelectedValue.ToString();
            this.gvCity.DataBind();
        } else {
            this.gvCity.Visible = false;
            this.fvCity.Visible = false;
        }
        this.upCity.Update();
    }

    protected void gvCityType_RowDeleted(object sender, GridViewDeletedEventArgs e) {
        this.gvCityType.SelectedIndex = -1;
        this.gvCityType.EditIndex = -1;
        this.gvCityType_SelectedIndexChanged(this.gvCityType, null);
    }

    protected void odsCityType_Inserted(object sender, ObjectDataSourceStatusEventArgs e) {
        if (e.Exception != null) {
            PageUtility.DealWithException(this, e.Exception);
            e.ExceptionHandled = true;
        }
    }

    protected void odsCityType_Updated(object sender, ObjectDataSourceStatusEventArgs e) {
        if (e.Exception != null) {
            PageUtility.DealWithException(this, e.Exception);
            e.ExceptionHandled = true;
        }
    }

    #endregion

    #region City event

    protected void odsCity_Selecting(object sender, ObjectDataSourceMethodEventArgs e) {
        e.InputParameters["CityTypeId"] = gvCityType.SelectedValue;
    }

    protected void odsCity_Inserting(object sender, ObjectDataSourceMethodEventArgs e) {
        e.InputParameters["CityTypeId"] = gvCityType.SelectedValue;
    }

    protected void fvCity_ItemInserting(object sender, FormViewInsertEventArgs e) {
        e.Values["CityTypeId"] = gvCityType.SelectedValue;
    }

    protected void odsCity_Inserted(object sender, ObjectDataSourceStatusEventArgs e) {
        if (e.Exception != null) {
            PageUtility.DealWithException(this, e.Exception);
            e.ExceptionHandled = true;
        }
    }

    protected void odsCity_Updated(object sender, ObjectDataSourceStatusEventArgs e) {
        if (e.Exception != null) {
            PageUtility.DealWithException(this, e.Exception);
            e.ExceptionHandled = true;
        }
    }

    #endregion
}