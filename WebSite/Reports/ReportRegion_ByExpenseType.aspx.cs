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
using System.Security.Principal;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using BusinessObjects;

public partial class ReportView : System.Web.UI.Page {
    protected void Page_Load(object sender, System.EventArgs e) {
        String title = this.GetLocalResourceObject("titleLabel.Text").ToString();
        PageUtility.SetContentTitle(this, title);
        this.Page.Title = title;

        int opViewId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.ReportRegion_ByExpenseType, SystemEnums.OperateEnum.View);
        AuthorizationDS.PositionRow position = (AuthorizationDS.PositionRow)this.Session["Position"];
        PositionRightBLL positionRightBLL = new PositionRightBLL();
        bool HasViewRight = positionRightBLL.CheckPositionRight(position.PositionId, opViewId);
        if (!HasViewRight) {
            Response.Redirect("~/ErrorPage/NoRightErrorPage.aspx");
            return;
        }
    }

    private void LoadReport() {
        
        Microsoft.Reporting.WebForms.ReportParameter[] ps = new Microsoft.Reporting.WebForms.ReportParameter[2];
        string reportName = "ReportRegion_ByExpenseType";
        ps[0] = new Microsoft.Reporting.WebForms.ReportParameter("Period", this.FinanceYear.Text.Trim());
        //用户信息
        AuthorizationDS.StuffUserRow stuffUser = (AuthorizationDS.StuffUserRow)Session["StuffUser"];
        ps[1] = new Microsoft.Reporting.WebForms.ReportParameter("USERID", stuffUser.StuffUserId+"");
        //load report 
        this.ReportViewer.LoadReport(reportName, ps);
    }

    protected void btn_search_Click(object sender, EventArgs e) {
        if (this.FinanceYear.Text.Trim() == string.Empty) {
            PageUtility.ShowModelDlg(this, "请输入财年！");
            return;
        }
        LoadReport();
    }

}
