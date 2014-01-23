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

public partial class DocumentPrint : System.Web.UI.Page {
    protected void Page_Load(object sender, System.EventArgs e) {
        if(!IsPostBack){
            showReport();
        }
    }

    private void showReport() {
        Microsoft.Reporting.WebForms.ReportParameter[] ps = new Microsoft.Reporting.WebForms.ReportParameter[1];
        string reportName = Request["ReportName"];
        if(string.IsNullOrEmpty(reportName)){
            return;
        }
        string formID = this.Request["FormID"].ToString();
        //获取审批流程人员及审批时间
        APFlowBLL ap = new APFlowBLL();
        //string approverName = ap.GetApprovalInfoByFormID(Int16.Parse(formID));
        ps[0] = new Microsoft.Reporting.WebForms.ReportParameter("FormID", formID);
        //ps[1] = new Microsoft.Reporting.WebForms.ReportParameter("ApproverName", approverName);

        //load report 
        this.ReportViewer.LoadReport(reportName, ps);
    }
}
