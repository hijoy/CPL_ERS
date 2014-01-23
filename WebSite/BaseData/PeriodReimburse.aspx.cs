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

public partial class BaseData_PeriodReimburse : BasePage
{
    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!this.IsPostBack) {
            String title = this.GetLocalResourceObject("titleLabel.Text").ToString();
            PageUtility.SetContentTitle(this, title);
            this.Page.Title = title;

            int opManageId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.PeriodReimburse, SystemEnums.OperateEnum.Manage);
            AuthorizationDS.PositionRow position = (AuthorizationDS.PositionRow)this.Session["Position"];
            PositionRightBLL positionRightBLL = new PositionRightBLL();
            if (!positionRightBLL.CheckPositionRight(position.PositionId, opManageId)) {
                Response.Redirect("~/ErrorPage/NoRightErrorPage.aspx");
                return;
            }
        }
    }

    #region PeriodReimburse event

    protected void odsPeriodReimburse_Inserting(object sender, ObjectDataSourceMethodEventArgs e) {
        UserControls_YearAndMonthUserControl ucNewPeriod = (UserControls_YearAndMonthUserControl)this.fvPeriodReimburse.FindControl("ucNewPeriod");
        string period = ((TextBox)(ucNewPeriod.FindControl("txtDate"))).Text.Trim();
        if (period == string.Empty) {
            PageUtility.ShowModelDlg(this.Page, "请录入费用期间!");
            e.Cancel = true;
            return;
        } else {
            DateTime yearAndMonth = DateTime.Parse(period.Substring(0, 4) + "-" + period.Substring(4, 2) + "-01");
            e.InputParameters["PeriodReimburse"] = yearAndMonth;
        }
    }

    protected void odsPeriodReimburse_Inserted(object sender, ObjectDataSourceStatusEventArgs e) {
        if (e.Exception != null) {
            PageUtility.DealWithException(this, e.Exception);
            e.ExceptionHandled = true;
        }
    }

    #endregion
}