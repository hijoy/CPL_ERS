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
using System.Collections.Generic;
using System.Text;

public partial class BaseData_SKU : BasePage {
    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!this.IsPostBack) {
            String title = this.GetLocalResourceObject("titleLabel.Text").ToString();
            PageUtility.SetContentTitle(this, title);
            this.Page.Title = title;
            int opViewId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.SKU, SystemEnums.OperateEnum.View);
            int opManageId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.SKU, SystemEnums.OperateEnum.Manage);
            AuthorizationDS.PositionRow position = (AuthorizationDS.PositionRow)this.Session["Position"];
            PositionRightBLL positionRightBLL = new PositionRightBLL();

            this.HasManageRight = positionRightBLL.CheckPositionRight(position.PositionId, opManageId);
            if (!HasManageRight && !positionRightBLL.CheckPositionRight(position.PositionId, opViewId)) {
                Response.Redirect("~/ErrorPage/NoRightErrorPage.aspx");
                return;
            }

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

        temp = this.ddlSKUTypeBySearch.SelectedValue;
        if (temp != null && (!temp.Trim().Equals(""))) {
            searchStr += " and SKUTypeID = " + temp;
        }

        temp = this.ddlSKUCategoryBySearch.SelectedValue;
        if (temp != null && (!temp.Trim().Equals(""))) {
            searchStr += " and SKUCategoryID = " + temp;
        }


        return searchStr;


    }

    protected void SearchButton_Click(object sender, EventArgs e) {
        // 重新绑定，进行查询处理。
        this.SKUObjectDataSource.SelectParameters["queryExpression"].DefaultValue = getSearchCondition();
        SKUGridView.DataBind();
        divSKUPrice.Visible = false;
        SKUPriceUpdatePanel.Update();
        SKUFormView.Visible = false;
        SKUAddUpdatePanel.Update();
    }

    #region SKU event

    protected void SKUGridView_SelectedIndexChanged(object sender, EventArgs e) {
        // 将选中的“编码”传给 Formview, 在FormView中显示。
        if (SKUGridView.SelectedValue == null) {
            SKUAddObjectDataSource.SelectParameters["SKUID"].DefaultValue = "-1";

        } else {
            SKUAddObjectDataSource.SelectParameters["SKUID"].DefaultValue = SKUGridView.SelectedValue.ToString();
        }
        SKUFormView.Visible = true;
        this.SKUFormView.DataBind();
        this.SKUAddUpdatePanel.Update();

        odsSKUPrice.SelectParameters["SKUID"].DefaultValue = SKUGridView.SelectedValue.ToString();
        divSKUPrice.Visible = true;
        this.SKUPriceUpdatePanel.Update();

    }
    protected void EditLinkButton_Click(object sender, EventArgs e) {
        // 将Formview状态更改为 Edit
        SKUFormView.ChangeMode(FormViewMode.Edit);
    }
    protected void AddLinkButton_Click(object sender, EventArgs e) {
        // 将Formview状态更改为 Insert
        SKUFormView.ChangeMode(FormViewMode.Insert);
    }
    protected void BackButton_Click(object sender, EventArgs e) {
        this.SKUAddObjectDataSource.SelectParameters["SKUID"].DefaultValue = "0";
    }
    protected void InsertButton_Click(object sender, EventArgs e) {
        SKUGridView.DataSourceID = "SKUObjectDataSource";
    }
    protected void SKULinkButton_Click(object sender, EventArgs e) {

        // 将Formview状态更改为 ReadOnly
        SKUFormView.ChangeMode(FormViewMode.ReadOnly);
    }

    protected void SKUAddObjectDataSource_Updated(object sender, ObjectDataSourceStatusEventArgs e) {
        // 捕获异常
        if (e.Exception != null) {
            PageUtility.DealWithException(this, e.Exception);
            //SKUFormView.ChangeMode(FormViewMode.Edit);
            e.ExceptionHandled = true;
        } else {
            this.SKUGridView.DataBind();
            this.SKUUpdatePanel.Update();
        }
    }
    protected void SKUAddObjectDataSource_Inserted(object sender, ObjectDataSourceStatusEventArgs e) {
        // 捕获异常
        if (e.Exception != null) {
            PageUtility.DealWithException(this, e.Exception);
            //SKUFormView.ChangeMode(FormViewMode.Insert);
            e.ExceptionHandled = true;
        } else {
            this.SKUGridView.DataBind();
            this.SKUUpdatePanel.Update();
        }
    }

    #endregion

    #region SKUPrice event

    protected void odsSKUPrice_Inserting(object sender, ObjectDataSourceMethodEventArgs e) {
        e.InputParameters["SKUID"] = SKUGridView.SelectedValue.ToString();

    }
    protected void odsSKUPrice_Updating(object sender, ObjectDataSourceMethodEventArgs e) {
        e.InputParameters["SKUID"] = SKUGridView.SelectedValue.ToString();
    }

    #endregion

    public string GetBrandByID(object ID) {

        return new MasterDataBLL().GetBrandById((int)ID)[0].BrandName;

    }

    public string GetSKUTypeByID(object ID) {
        return new MasterDataBLL().GetSKUTypeById((int)ID)[0].SKUTypeName;
    }

    public string GetSKUCategoryByID(object ID) {
        return new MasterDataBLL().GetSKUCategoryById((int)ID)[0].SKUCategoryName;
    }

    public string GetCompanyNameByID(object ID) {
        return new MasterDataBLL().GetCompanyById((int)ID).CompanyName;
    }

    public string GetCustomerChannelByID(object ID) {
        return new MasterDataBLL().GetCustomerChannelById((int)ID)[0].CustomerChannelName;
    }

    public string GetCustomerTypeByID(object ID) {
        return new MasterDataBLL().GetCustomerTypeById((int)ID).CustomerTypeName;
    }

    protected void SKUFormView_DataBound(object sender, EventArgs e) {
        DataRowView drDetail = (DataRowView)SKUFormView.DataItem;
        if (drDetail != null) {
            MasterData.SKURow rowSKU = (MasterData.SKURow)drDetail.Row;
            DropDownList ddlSKUType = (DropDownList)SKUFormView.FindControl("ddlSKUType");

            if (ddlSKUType != null && rowSKU.IsSKUTypeIDNull()) {
                ddlSKUType.SelectedValue = "0";
            } else {
                ddlSKUType.SelectedValue = rowSKU.SKUTypeID.ToString();
            }
        }
    }

    protected void SKUGridView_RowDataBound(object sender, GridViewRowEventArgs e) {
        if (e.Row.RowType == DataControlRowType.DataRow) {
            DataRowView drDetail = (DataRowView)e.Row.DataItem;
            MasterData.SKURow rowSKU = (MasterData.SKURow)drDetail.Row;
            Label lblSKUType = (Label)e.Row.FindControl("lblSKUType");
            if (lblSKUType != null && !rowSKU.IsSKUTypeIDNull()) {
                lblSKUType.Text = GetSKUTypeByID(rowSKU.SKUTypeID);
            }
        }
    }
}