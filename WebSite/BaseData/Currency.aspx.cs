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

public partial class BaseData_Currency : BasePage {
    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!this.IsPostBack) {
            String title = this.GetLocalResourceObject("titleLabel.Text").ToString();
            PageUtility.SetContentTitle(this, title);
            this.Page.Title = title;
            int opViewId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.Currency, SystemEnums.OperateEnum.View);
            int opManageId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.Currency, SystemEnums.OperateEnum.Manage);
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

    #region Currency event

    protected void gvCurrency_SelectedIndexChanged(object sender, EventArgs e) {
        if (this.gvCurrency.SelectedIndex >= 0) {
            this.gvExchangeRate.Visible = true;
            if (this.HasManageRight) {
                this.fvExchangeRate.Visible = true;
            } else {
                this.fvExchangeRate.Visible = false;
            }
            this.gvExchangeRate.EditIndex = -1;
            this.gvExchangeRate.SelectedIndex = -1;

            this.odsExchangeRate.SelectParameters["CurrencyID"].DefaultValue = gvCurrency.SelectedValue.ToString();
            this.gvExchangeRate.DataBind();
        } else {
            this.gvExchangeRate.Visible = false;
            this.fvExchangeRate.Visible = false;
        }
        this.upExchangeRate.Update();
        this.upExchangeRate1.Update();
    }

    #endregion

    #region ExchangeRate event

    protected void odsExchangeRate_Selecting(object sender, ObjectDataSourceMethodEventArgs e) {
        e.InputParameters["CurrencyID"] = gvCurrency.SelectedValue.ToString();
    }

    protected void odsExchangeRate_Inserting(object sender, ObjectDataSourceMethodEventArgs e) {
        e.InputParameters["CurrencyID"] = gvCurrency.SelectedValue.ToString();
    }
    protected void odsExchangeRate_Updating(object sender, ObjectDataSourceMethodEventArgs e) {
        e.InputParameters["CurrencyID"] = gvCurrency.SelectedValue.ToString();
    }

    public string GetCurrencyNameByID(object Id) {
        return new MasterDataBLL().GetCurrencyByID((int)Id).CurrencyShortName;
    }

    #endregion
    
}