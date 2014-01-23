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

public partial class BaseData_Customer : BasePage {
    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!this.IsPostBack) {
            String title = this.GetLocalResourceObject("titleLabel.Text").ToString();
            PageUtility.SetContentTitle(this, title);
            this.Page.Title = title;
            int opViewId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.Customer, SystemEnums.OperateEnum.View);
            int opManageId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.Customer, SystemEnums.OperateEnum.Manage);
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
        String temp = this.txtCustNoBySearch.Text;
        if (temp != null && (!temp.Trim().Equals(""))) {
            searchStr += " and CustomerNo like '%" + temp + "%'";
        }
        temp = this.txtCustNameBySearch.Text;
        if (temp != null && (!temp.Trim().Equals(""))) {
            searchStr += " and CustomerName like '%" + temp + "%'";
        }
        temp = this.ddlCustomerRegionBySearch.SelectedValue;
        if (temp != null && (!temp.Trim().Equals(""))) {
            searchStr += " and CustomerRegionID = " + temp;
        }

        temp = this.ddlCustomerTypeBySearch.SelectedValue;
        if (temp != null && (!temp.Trim().Equals(""))) {
            searchStr += " and CustomerTypeID = " + temp;
        }

        temp = this.ddlCustomerChannelBySearch.SelectedValue;
        if (temp != null && (!temp.Trim().Equals(""))) {
            searchStr += " and CustomerChannelID = " + temp;
        }

        temp = this.ddlActiveBySearch.SelectedValue;
        if (temp != null && (!temp.Trim().Equals(""))) {
            searchStr += " and IsActive = " + temp;
        }

        return searchStr;

    }

    protected void SearchButton_Click(object sender, EventArgs e) {
        // 重新绑定，进行查询处理。
        this.CustomerObjectDataSource.SelectParameters["queryExpression"].DefaultValue = getSearchCondition();
        CustomerGridView.DataBind();
        this.CustomerFormView.Visible = false;
        this.CustomerAddUpdatePanel.Update();

    }

    protected void CustomerGridView_SelectedIndexChanged(object sender, EventArgs e) {
        // 将选中的“编码”传给 Formview, 在FormView中显示。
        if (CustomerGridView.SelectedValue == null) {
            CustomerAddObjectDataSource.SelectParameters["CustomerID"].DefaultValue = "-1";

        } else {
            CustomerAddObjectDataSource.SelectParameters["CustomerID"].DefaultValue = CustomerGridView.SelectedValue.ToString();
        }
        this.CustomerFormView.Visible = true;
        this.CustomerFormView.DataBind();
        this.CustomerAddUpdatePanel.Update();
    }

    protected void EditLinkButton_Click(object sender, EventArgs e) {
        // 将Formview状态更改为 Edit
        CustomerFormView.ChangeMode(FormViewMode.Edit);
    }

    protected void AddLinkButton_Click(object sender, EventArgs e) {
        // 将Formview状态更改为 Insert
        CustomerFormView.ChangeMode(FormViewMode.Insert);
    }

    protected void BackButton_Click(object sender, EventArgs e) {
        this.CustomerAddObjectDataSource.SelectParameters["CustomerID"].DefaultValue = "0";
    }

    protected void InsertButton_Click(object sender, EventArgs e) {
        CustomerGridView.DataSourceID = "CustomerObjectDataSource";
    }

    protected void CustomerLinkButton_Click(object sender, EventArgs e) {
        // 将Formview状态更改为 ReadOnly
        CustomerFormView.ChangeMode(FormViewMode.ReadOnly);
    }

    protected void CustomerAddObjectDataSource_Updated(object sender, ObjectDataSourceStatusEventArgs e) {
        // 捕获异常
        if (e.Exception != null) {
            PageUtility.DealWithException(this, e.Exception);
            //CustomerFormView.ChangeMode(FormViewMode.Edit);
            e.ExceptionHandled = true;
        } else {
            this.CustomerGridView.DataBind();
            this.CustomerUpdatePanel.Update();
        }
    }

    protected void CustomerAddObjectDataSource_Inserted(object sender, ObjectDataSourceStatusEventArgs e) {
        // 捕获异常
        if (e.Exception != null) {
            PageUtility.DealWithException(this, e.Exception);
            //CustomerFormView.ChangeMode(FormViewMode.Insert);
            e.ExceptionHandled = true;
        } else {
            this.CustomerGridView.DataBind();
            this.CustomerUpdatePanel.Update();
        }
    }

    public string GetCustomerChannelByID(object ID) {
        return new MasterDataBLL().GetCustomerChannelById((int)ID)[0].CustomerChannelName;
    }

    public string GetCustomerTypeByID(object ID) {
        return new MasterDataBLL().GetCustomerTypeById((int)ID).CustomerTypeName;
    }

    public string GetCustomerRegionByID(object ID) {
        return new MasterDataBLL().GetCustomerRegionById((int)ID).CustomerRegionName;
    }

    public string GetProvinceNameByID(object ID) {
        return new MasterDataBLL().GetProvinceById((int)ID).ProvinceName;
    }
}