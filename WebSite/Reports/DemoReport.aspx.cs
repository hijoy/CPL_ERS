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

public partial class Reports_DemoReport : System.Web.UI.Page {
    protected void Page_Load(object sender, System.EventArgs e) {
        PageUtility.SetContentTitle(this, "费用统计报表");
        this.Page.Title = "费用统计报表";
        int opViewId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.DemoReport, SystemEnums.OperateEnum.View);
        AuthorizationDS.PositionRow position = (AuthorizationDS.PositionRow)this.Session["Position"];
        PositionRightBLL positionRightBLL = new PositionRightBLL();
        bool HasViewRight = positionRightBLL.CheckPositionRight(position.PositionId, opViewId);
        if (!HasViewRight) {
            Response.Redirect("~/ErrorPage/NoRightErrorPage.aspx");
            return;
        }
    }

    private void LoadReport() {
        string reportName = string.Empty;
        Microsoft.Reporting.WebForms.ReportParameter[] ps = new Microsoft.Reporting.WebForms.ReportParameter[1];
        reportName = "Report1";
        ps[0] = new Microsoft.Reporting.WebForms.ReportParameter("URL", "http://localhost:26798/WebSite/Reports/GetApprovalInfo.asmx");
        //load report 
        this.ReportViewer.LoadReport(reportName, ps);
    }

    protected void btn_search_Click(object sender, EventArgs e) {
        if (this.UCOU.OUId == null || this.UCOU.OUId <= 0) {
            PageUtility.ShowModelDlg(this, "请选择部门！");
            return;
        }
        if (this.UCDateInputEndDate.SelectedDate == string.Empty) {
            PageUtility.ShowModelDlg(this, "请选择截止日期！");
            return;
        }
        LoadReport();
    }

}
