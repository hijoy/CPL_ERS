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
        String title = this.GetLocalResourceObject("titleLabel.Text").ToString();
        PageUtility.SetContentTitle(this, title);
        this.Page.Title = title;

        int opViewId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.FormPVDelayReport, SystemEnums.OperateEnum.View);
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
        Microsoft.Reporting.WebForms.ReportParameter[] ps = new Microsoft.Reporting.WebForms.ReportParameter[4];
        reportName = "FormPVDelayReport";
        ps[0] = new Microsoft.Reporting.WebForms.ReportParameter("BeginDate", this.UCDateInputBeginDate.SelectedDate);
        ps[1] = new Microsoft.Reporting.WebForms.ReportParameter("EndDate", this.UCDateInputEndDate.SelectedDate);
        ps[2] = new Microsoft.Reporting.WebForms.ReportParameter("OUID", this.UCOU.OUId.ToString());
        ps[3] = new Microsoft.Reporting.WebForms.ReportParameter("StaffName", this.userId.Text.Trim());
        //load report 
        this.ReportViewer.LoadReport(reportName, ps);
    }

    protected void btn_search_Click(object sender, EventArgs e) {
        string start = this.UCDateInputBeginDate.SelectedDate;
        string end = this.UCDateInputEndDate.SelectedDate;
        if (start == null || start == string.Empty) {
            //if (end != null && end != string.Empty) {
                PageUtility.ShowModelDlg(this, "请选择起始日期!");
                return;
            //}
        } else {
            if (end == null || end == string.Empty) {
                PageUtility.ShowModelDlg(this, "请选择截止日期!");
                return;
            } else {
                DateTime dtStart = DateTime.Parse(start);
                DateTime dtEnd = DateTime.Parse(end);
                if (dtStart > dtEnd) {
                    PageUtility.ShowModelDlg(this, "起始日期大于截止日期！");
                    return;
                }
            }
        }
        if (this.UCOU.OUId == null || this.UCOU.OUId <= 0) {
            PageUtility.ShowModelDlg(this, "请选择部门！");
            return;
        }
        LoadReport();
    }

}
