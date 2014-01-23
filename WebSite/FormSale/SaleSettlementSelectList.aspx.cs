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

public partial class FormSale_SaleSettlementSelectList : BasePage {

    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!IsPostBack) {
            String title = this.GetLocalResourceObject("titleLabel.Text").ToString();
            PageUtility.SetContentTitle(this, title);

            int stuffuserID = ((AuthorizationDS.StuffUserRow)Session["StuffUser"]).StuffUserId;
            this.ViewState["DefaultFilter"] = "(UserID=" + stuffuserID + " or exists (select * from ProxyReimburse where ProxyReimburse.UserID = FormSaleSettlementView.UserID and ProxyReimburse.ProxyUserID =" + stuffuserID + " and ProxyReimburse.EndDate>FormSaleSettlementView.SubmitDate) )"
                + " And IsClose = 0 and StatusID = 2 and PageType<>27 and FormSaleSettlementView.PaymentTypeID not in (2,5) ";
            if (Request["Search"] == null) {
                this.odsSettlementList.SelectParameters["queryExpression"].DefaultValue = this.ViewState["DefaultFilter"].ToString();
            }
        }
    }

    protected override void OnLoadComplete(EventArgs e) {
        if (!IsPostBack) {
            if (Request["Search"] != null) {
                if (this.Request["FormNo"] != null)
                    this.txtFormNo.Text = this.Request["FormNo"].ToString();
                if (this.Request["StuffName"] != null)
                    this.txtStuffUser.Text = this.Request["StuffName"].ToString();
                if (this.Request["CustomerID"] != null)
                    this.UCCustomer.CustomerID = this.Request["CustomerID"].ToString();
                this.ExpenseSubCategoryDDL.DataBind();
                if (this.Request["ExpenseSubCategoryID"] != null)
                    this.ExpenseSubCategoryDDL.SelectedValue = this.Request["ExpenseSubCategoryID"].ToString();
                this.BrandDDL.DataBind();
                if (this.Request["BrandID"] != null)
                    this.BrandDDL.SelectedValue = this.Request["BrandID"].ToString();
                if (this.Request["SubmitDateStart"] != null)
                    this.UCDateInputBeginDate.SelectedDate = this.Request["SubmitDateStart"].ToString();
                if (this.Request["SubmitDateEnd"] != null)
                    this.UCDateInputEndDate.SelectedDate = this.Request["SubmitDateEnd"].ToString();
                if (this.Request["ApplyFormNo"] != null)
                    this.txtApplyFormNo.Text = this.Request["ApplyFormNo"].ToString();
                btnSearch_Click(null, null);
            }
        }
    }

    protected void gvSettlementList_RowDataBound(object sender, GridViewRowEventArgs e) {
        // 对数据列进行赋值
        if (e.Row.RowType == DataControlRowType.DataRow) {
            if ((e.Row.RowState & DataControlRowState.Edit) != DataControlRowState.Edit) {
                DataRowView drvDetail = (DataRowView)e.Row.DataItem;
                QueryDS.FormSaleSettlementViewRow row = (QueryDS.FormSaleSettlementViewRow)drvDetail.Row;

                LinkButton lblFormNo = (LinkButton)e.Row.FindControl("lblFormNo");
                if (this.ViewState["SearchCondition"] != null) {
                    lblFormNo.PostBackUrl = CommonUtility.GetFormPostBackUrl(row.FormID, row.PageType, row.StatusID, "&Source=" + HttpUtility.UrlEncode("~/FormSale/SaleSettlementSelectList.aspx?" + this.ViewState["SearchCondition"].ToString() + "&startIndex=" + this.gvSettlementList.PageIndex));
                } else {
                    lblFormNo.PostBackUrl = CommonUtility.GetFormPostBackUrl(row.FormID, row.PageType, row.StatusID, "&Source=~/FormSale/SaleSettlementSelectList.aspx");
                }

                LinkButton lbCash = (LinkButton)e.Row.FindControl("lbCash");
                if (this.ViewState["SearchCondition"] != null) {
                    lbCash.PostBackUrl = "~/FormSale/PaymentCashApply.aspx?FormSaleSettlementID=" + row.FormID + "&Source=" + HttpUtility.UrlEncode("~/FormSale/SaleSettlementSelectList.aspx?" + this.ViewState["SearchCondition"].ToString());
                } else {
                    lbCash.PostBackUrl = "~/FormSale/PaymentCashApply.aspx?FormSaleSettlementID=" + row.FormID + "&Source=" + HttpUtility.UrlEncode("~/FormSale/SaleSettlementSelectList.aspx");
                }

                LinkButton lbFreeGoods = (LinkButton)e.Row.FindControl("lbFreeGoods");
                if (this.ViewState["SearchCondition"] != null) {
                    lbFreeGoods.PostBackUrl = "~/FormSale/PaymentFreeGoodsApply.aspx?FormSaleSettlementID=" + row.FormID + "&Source=" + HttpUtility.UrlEncode("~/FormSale/SaleSettlementSelectList.aspx?" + this.ViewState["SearchCondition"].ToString());
                } else {
                    lbFreeGoods.PostBackUrl = "~/FormSale/PaymentFreeGoodsApply.aspx?FormSaleSettlementID=" + row.FormID + "&Source=" + HttpUtility.UrlEncode("~/FormSale/SaleSettlementSelectList.aspx");
                }

            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e) {
        if (!checkSearchConditionValid()) {
            return;
        } else {
            if (this.Request["StartIndex"] != null) {
                string start = this.Request["StartIndex"].ToString();
                this.gvSettlementList.PageIndex = int.Parse(start);
            }
            this.odsSettlementList.SelectParameters["queryExpression"].DefaultValue = getSearchCondition();
            this.gvSettlementList.DataBind();
        }
    }

    private string getSearchCondition() {
        int stuffuserID = ((AuthorizationDS.StuffUserRow)Session["StuffUser"]).StuffUserId;
        string filterStr = this.ViewState["DefaultFilter"].ToString();
        this.ViewState["SearchCondition"] = "Search=true";
        //申请单编号
        if (txtFormNo.Text.Trim() != string.Empty) {
            filterStr += " AND FormNo like '%" + this.txtFormNo.Text.Trim() + "%'";
            this.ViewState["SearchCondition"] += "&FormNo=" + this.txtFormNo.Text.Trim();
        }

        //申请人姓名
        if (txtStuffUser.Text.Trim() != string.Empty) {
            filterStr += " AND StuffName like '%" + this.txtStuffUser.Text.Trim() + "%'";
            this.ViewState["SearchCondition"] += "&StuffName=" + this.txtStuffUser.Text.Trim();
        }

        //客户
        if (this.UCCustomer.CustomerID != string.Empty && this.UCCustomer.CustomerID!=null) {
            filterStr += " AND CustomerID = " + this.UCCustomer.CustomerID;
            this.ViewState["SearchCondition"] += "&CustomerID=" + this.UCCustomer.CustomerID;
        }

        //费用小类
        if (!this.ExpenseSubCategoryDDL.SelectedValue.Equals("0")) {
            filterStr += " AND ExpenseSubCategoryID = " + ExpenseSubCategoryDDL.SelectedValue;
            this.ViewState["SearchCondition"] += "&ExpenseSubCategoryID=" + ExpenseSubCategoryDDL.SelectedValue;
        }

        //支付方式
        if (!this.BrandDDL.SelectedValue.Equals("0")) {
            filterStr += " AND BrandID = " + BrandDDL.SelectedValue;
            this.ViewState["SearchCondition"] += "&BrandID=" + BrandDDL.SelectedValue;
        }

        //申请日期
        string start = ((TextBox)(this.UCDateInputBeginDate.FindControl("txtDate"))).Text.Trim();
        if (start != null && start != string.Empty) {
            string end = ((TextBox)(this.UCDateInputEndDate.FindControl("txtDate"))).Text.Trim();
            filterStr += " AND SubmitDate >='" + start + "' AND dateadd(day,-1,SubmitDate)<='" + end + "'";
            this.ViewState["SearchCondition"] += "&SubmitDateStart=" + start + "&SubmitDateEnd=" + end;
        }

        //申请单编号
        if (txtApplyFormNo.Text.Trim() != string.Empty) {
            filterStr += " And charindex('P" + txtApplyFormNo.Text + "P',FormApplyNos)>0";
            this.ViewState["SearchCondition"] += "&ApplyFormNo=" + this.txtApplyFormNo.Text.Trim();
        }

        return filterStr;
    }

    protected bool checkSearchConditionValid() {
        string start = ((TextBox)(this.UCDateInputBeginDate.FindControl("txtDate"))).Text.Trim();
        string end = ((TextBox)(this.UCDateInputEndDate.FindControl("txtDate"))).Text.Trim();

        if (start == null || start == string.Empty) {
            if (end != null && end != string.Empty) {
                PageUtility.ShowModelDlg(this, "请选择申请提交起始日期!");
                return false;
            }
        } else {
            if (end == null || end == string.Empty) {
                PageUtility.ShowModelDlg(this, "请选择申请提交截止日期!");
                return false;
            } else {
                DateTime dtStart = DateTime.Parse(start);
                DateTime dtEnd = DateTime.Parse(end);
                if (dtStart > dtEnd) {
                    PageUtility.ShowModelDlg(this, "起始日期大于截止日期！");
                    return false;
                }
            }
        }

        return true;
    }

}
