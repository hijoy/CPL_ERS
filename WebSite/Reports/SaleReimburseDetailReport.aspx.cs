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
using BusinessObjects.ReportDSTableAdapters;

public partial class SaleReimburseDetail : BasePage {
    protected string saveFilePath = System.Configuration.ConfigurationSettings.AppSettings["UploadDirectory"];
    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!IsPostBack) {
            String title = this.GetLocalResourceObject("titleLabel.Text").ToString();
            PageUtility.SetContentTitle(this, title, "Sale Reimburse Detail");

            int opViewId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.DemoReport, SystemEnums.OperateEnum.View);
            AuthorizationDS.PositionRow position = (AuthorizationDS.PositionRow)this.Session["Position"];
            PositionRightBLL positionRightBLL = new PositionRightBLL();
            bool HasViewRight = positionRightBLL.CheckPositionRight(position.PositionId, opViewId);
            if (!HasViewRight) {
                Response.Redirect("~/ErrorPage/NoRightErrorPage.aspx");
                return;
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e) {
        if (!checkSearchConditionValid()) {
            return;
        } else {
            string start = ((TextBox)(this.UCDateInputBeginDate.FindControl("txtDate"))).Text.Trim();
            this.gvApplyList.DataSourceID = this.odsApplyList.ID;
            this.odsApplyList.SelectParameters["Period"].DefaultValue = start;
            this.gvApplyList.DataBind();
        }
    }

    protected void btnExport_Click(object sender, EventArgs e) {
        if (!checkSearchConditionValid()) {
            return;
        } else {
            int limit = int.Parse(System.Configuration.ConfigurationManager.AppSettings["ExportCountLimit"]);
            ReportQueryBLL bll = new ReportQueryBLL();
            string start = ((TextBox)(this.UCDateInputBeginDate.FindControl("txtDate"))).Text.Trim();
            int count = bll.QuerySaleReimburseDetailViewCountByRight(start);
            if (count > limit) {
                PageUtility.ShowModelDlg(this.Page, "导出记录数不得超过" + limit + "条，请缩小查询条件");
                return;
            }
            try {
                Session.Timeout = 30;
                string fileID = Guid.NewGuid().ToString();
                string outFile = Server.MapPath(@"~/" + saveFilePath) + @"\" + fileID + ".xls";
                System.Diagnostics.Debug.WriteLine("outFile := " + outFile);
                this.ExportDataGrid.DataSource = new GetReportSalesReimburseDetailTableAdapter().GetData(start);
                this.ExportDataGrid.DataBind();
                string fileName = "SaleReimburseDetail" + DateTime.Now.ToString("yyyyMMddHHmmss");
                ToExcel(this.ExportDataGrid, fileName);
                Session.Timeout = 3;
            } catch (Exception ex) {
                PageUtility.DealWithException(this, ex);
            }
        }
    }

    public void ToExcel(System.Web.UI.Control ctl, String fileName) {
        Response.Clear();
        Response.Buffer = false;
        Response.Charset = "GB2312";
        HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + fileName + ".xls");
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
        Response.ContentType = "application/ms-excel";
        Response.Write("<meta http-equiv=Content-Type content=\"text/html; charset=GB2312\">");
        this.EnableViewState = false;
        System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
        HtmlTextWriter oHtmlTextWriter = new HtmlTextWriter(oStringWriter);
        ctl.RenderControl(oHtmlTextWriter);
        Response.Write(oStringWriter.ToString());
        Response.End();
    }

    protected bool checkSearchConditionValid() {
        string start = ((TextBox)(this.UCDateInputBeginDate.FindControl("txtDate"))).Text.Trim();
        if (start == null || start == string.Empty) {
            PageUtility.ShowModelDlg(this, "请选择费用期间日期!");
            return false;
        }
        return true;
    }

}
