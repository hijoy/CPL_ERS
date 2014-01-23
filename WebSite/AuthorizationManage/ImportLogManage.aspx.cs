using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AuthorizationManage_ImportLogManage : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        if (!IsPostBack)
        {
            PageUtility.SetContentTitle(this, "数据导入日志", "List Of Import Log");
            this.odsApplyList.SelectParameters["queryExpression"].DefaultValue = "1!=1";
            gvApplyList.DataBind();
        }

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(UCStartDate.SelectedDate) && !string.IsNullOrEmpty(ucEnddate.SelectedDate))
        {
            if (DateTime.Parse(UCStartDate.SelectedDate) > DateTime.Parse(ucEnddate.SelectedDate))
            {
                PageUtility.ShowModelDlg(this.Page, "");
                return;
            }
        }
        this.odsApplyList.SelectParameters["queryExpression"].DefaultValue = GetWhere();
        gvApplyList.DataBind();
    }
    private string GetWhere()
    {
        string where = " 1=1 ";
        if (!string.IsNullOrEmpty(UCStartDate.SelectedDate))
        {
            where += " and importDate>='"+UCStartDate.SelectedDate+"'";
        }
        if (!string.IsNullOrEmpty(ucEnddate.SelectedDate))
        {
            where += " and importDate-1<='" + ucEnddate.SelectedDate + "'";
        }
        if (!string.IsNullOrEmpty(ddltype.SelectedValue))
        {
            where += " and ImportType ='" + ddltype.SelectedValue + "'";
        }
        return where;
    }
    protected void lblErrorDetail_Click(object sender, EventArgs e)
    {
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
    protected void gvApplyList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblImportType = (Label)e.Row.Cells[1].FindControl("lblImportType");
            string importtype=lblImportType.Text;
            switch (importtype)
            {
                case "0":
                    lblImportType.Text = "SKU";
                    break;
                case "1":
                    lblImportType.Text = "Item";
                    break;
                case "2":
                    lblImportType.Text = "Customer";
                    break;
                case "3":
                    lblImportType.Text = "Delivery Goods";
                    break;
                case "4":
                    lblImportType.Text = "支付信息";
                    break;
            }
            LinkButton lblErrorDetail = (LinkButton)e.Row.Cells[7].FindControl("lblErrorDetail");
            Label lblFailCount = (Label)e.Row.Cells[6].FindControl("lblFailCount");
            if (lblFailCount.Text == "0")
            {
                lblErrorDetail.Text = "";
            }
        }
    }
}