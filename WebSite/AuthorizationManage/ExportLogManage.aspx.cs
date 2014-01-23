using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AuthorizationManage_ExportLogManage : BasePage {
    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!IsPostBack) {
            PageUtility.SetContentTitle(this, "导出数据日志", "list of Export Log");
            this.odsApplyList.SelectParameters["queryExpression"].DefaultValue = "1!=1";
            gvApplyList.DataBind();
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e) {
        if (!string.IsNullOrEmpty(UCStartDate.SelectedDate) && !string.IsNullOrEmpty(ucEnddate.SelectedDate)) {
            if (DateTime.Parse(UCStartDate.SelectedDate) > DateTime.Parse(ucEnddate.SelectedDate)) {
                PageUtility.ShowModelDlg(this.Page, "");
                return;
            }
        }
        this.odsApplyList.SelectParameters["queryExpression"].DefaultValue = GetWhere();
        gvApplyList.DataBind();
    }
    private string GetWhere() {
        string where = " 1=1 ";
        if (!string.IsNullOrEmpty(UCStartDate.SelectedDate)) {
            where += " and ExportDate>='" + UCStartDate.SelectedDate + "'";
        }
        if (!string.IsNullOrEmpty(ucEnddate.SelectedDate)) {
            where += " and ExportDate-1<='" + ucEnddate.SelectedDate + "'";
        }
        if (!string.IsNullOrEmpty(ddltype.SelectedValue)) {
            where += " and ExportType ='" + ddltype.SelectedValue + "'";
        }
        return where;
    }
    protected void lblErrorDetail_Click(object sender, EventArgs e) {
        //获取LinkButton对象
        LinkButton linkbtn = (LinkButton)sender;
        //引用imgbtnDelete控件的父控件上一级控件
        GridViewRow gvr = (GridViewRow)linkbtn.Parent.Parent;
        //获取ID
        string id = gvApplyList.DataKeys[gvr.RowIndex].Value.ToString();
        gvFaillist.Visible = true;
        this.odsFailList.SelectParameters["logId"].DefaultValue = id;
        gvFaillist.DataBind();
    }
    protected void gvApplyList_RowDataBound(object sender, GridViewRowEventArgs e) {
        if (e.Row.RowType == DataControlRowType.DataRow) {
            Label lblExportType = (Label)e.Row.Cells[1].FindControl("lblExportType");
            string importtype = lblExportType.Text;
            switch (importtype) {
                case "1":
                    lblExportType.Text = "个人费用报销";
                    break;
                case "2":
                    lblExportType.Text = "差旅费报销";
                    break;
                case "3":
                    lblExportType.Text = "PV";
                    break;
                case "4":
                    lblExportType.Text = "SALE";
                    break;
                case "5":
                    lblExportType.Text = "Marketing";
                    break;
                case "6":
                    lblExportType.Text = "R&D";
                    break;
                case "8":
                    lblExportType.Text = "Vendor";
                    break;
                case "9":
                    lblExportType.Text = "PO";
                    break;
            }
            LinkButton lblErrorDetail = (LinkButton)e.Row.Cells[7].FindControl("lblErrorDetail");
            Label lblFailCount = (Label)e.Row.Cells[6].FindControl("lblFailCount");
            if (lblFailCount.Text == "0") {
                lblErrorDetail.Text = "";
            }
        }
    }
}