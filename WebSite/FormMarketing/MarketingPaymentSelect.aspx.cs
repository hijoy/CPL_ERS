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

public partial class FormMarketing_MarketingPaymentSelect : BasePage {

    private FormPurchaseBLL _FormPurchaseBLL;
    protected FormPurchaseBLL FormPurchaseBLL {
        get {
            if (_FormPurchaseBLL == null) {
                _FormPurchaseBLL = new FormPurchaseBLL();
            }
            return _FormPurchaseBLL;
        }
    }

    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!IsPostBack) {
            String title = this.GetLocalResourceObject("titleLabel.Text").ToString();
            PageUtility.SetContentTitle(this, title);

            int stuffuserID = ((AuthorizationDS.StuffUserRow)Session["StuffUser"]).StuffUserId;
            this.ViewState["DefaultFilter"] = "(UserID=" + stuffuserID + " or exists (select * from ProxyReimburse where ProxyReimburse.UserID = FormMarketingApplyView.UserID and ProxyReimburse.ProxyUserID =" + stuffuserID + " and ProxyReimburse.EndDate>FormMarketingApplyView.SubmitDate) )"
                + " And IsClose = 0 and StatusID = 2 and PageType<>43 ";//没有被结案的单据
            if (Request["Search"] == null) {
                this.odsApplyList.SelectParameters["queryExpression"].DefaultValue = this.ViewState["DefaultFilter"].ToString();
            }
        }
    }

    protected override void OnLoadComplete(EventArgs e) {
        if (!IsPostBack) {
            if (Request["Search"] != null) {
                if (this.Request["FormNo"] != null)
                    this.txtFormNo.Text = this.Request["FormNo"].ToString();
                this.BrandDDL.DataBind();
                if (this.Request["BrandID"] != null)
                    this.BrandDDL.SelectedValue = this.Request["BrandID"].ToString();
                if (this.Request["PeriodStart"] != null)
                    this.UCPeriodBegin.SelectedDate = this.Request["PeriodStart"].ToString();
                if (this.Request["PeriodEnd"] != null)
                    this.UCPeriodEnd.SelectedDate = this.Request["PeriodEnd"].ToString();
                if (this.Request["SubmitDateStart"] != null)
                    this.UCDateInputBeginDate.SelectedDate = this.Request["SubmitDateStart"].ToString();
                if (this.Request["SubmitDateEnd"] != null)
                    this.UCDateInputEndDate.SelectedDate = this.Request["SubmitDateEnd"].ToString();
                btnSearch_Click(null, null);
            }
        }
    }

    protected void gvApplyList_RowDataBound(object sender, GridViewRowEventArgs e) {
        // 对数据列进行赋值
        if (e.Row.RowType == DataControlRowType.DataRow) {
            if ((e.Row.RowState & DataControlRowState.Edit) != DataControlRowState.Edit) {
                DataRowView drvDetail = (DataRowView)e.Row.DataItem;
                QueryDS.FormMarketingApplyViewRow row = (QueryDS.FormMarketingApplyViewRow)drvDetail.Row;

                Label lblProjectName = (Label)e.Row.FindControl("lblProjectName");
                if (row.MarketingProjectName.Length >= 20) {
                    lblProjectName.Text = row.MarketingProjectName.Substring(0, 20) + "...";
                } else {
                    lblProjectName.Text = row.MarketingProjectName;
                }

                Label lblProjectDesc = (Label)e.Row.FindControl("lblProjectDesc");
                if (!row.IsProjectDescNull()) {
                    if (row.ProjectDesc.Length >= 20) {
                        lblProjectDesc.Text = row.ProjectDesc.Substring(0, 20) + "...";
                    } else {
                        lblProjectDesc.Text = row.ProjectDesc;
                    }
                }
                LinkButton lblFormNo = (LinkButton)e.Row.FindControl("lblFormNo");
                if (this.ViewState["SearchCondition"] != null) {
                    lblFormNo.PostBackUrl = CommonUtility.GetFormPostBackUrl(row.FormID, row.PageType, row.StatusID, "&Source=" + HttpUtility.UrlEncode("~/FormMarketing/MarketingPaymentSelect.aspx?" + this.ViewState["SearchCondition"].ToString() + "&startIndex=" + this.gvApplyList.PageIndex));
                } else {
                    lblFormNo.PostBackUrl = CommonUtility.GetFormPostBackUrl(row.FormID, row.PageType, row.StatusID, "&Source=~/FormMarketing/MarketingPaymentSelect.aspx");
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
                this.gvApplyList.PageIndex = int.Parse(start);
            }
            this.odsApplyList.SelectParameters["queryExpression"].DefaultValue = getSearchCondition();
            this.gvApplyList.DataBind();
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

        //品牌
        if (!this.BrandDDL.SelectedValue.Equals("0")) {
            filterStr += " AND BrandID = " + BrandDDL.SelectedValue;
            this.ViewState["SearchCondition"] += "&BrandID=" + BrandDDL.SelectedValue;
        }

        //费用期间
        string startPeriod = ((TextBox)(this.UCPeriodBegin.FindControl("txtDate"))).Text.Trim();
        if (startPeriod != null && startPeriod != string.Empty) {
            string endPeriod = ((TextBox)(this.UCPeriodEnd.FindControl("txtDate"))).Text.Trim();
            filterStr += " AND FPeriod >='" + startPeriod.Substring(0, 4) + "-" + startPeriod.Substring(4, 2) + "-01'" +
                " AND FPeriod<='" + endPeriod.Substring(0, 4) + "-" + endPeriod.Substring(4, 2) + "-01'";
            this.ViewState["SearchCondition"] += "&PeriodStart=" + startPeriod.Substring(0, 4) + "-" + startPeriod.Substring(4, 2) + "-01"
               + "&PeriodEnd=" + endPeriod.Substring(0, 4) + "-" + endPeriod.Substring(4, 2) + "-01";
        }

        //申请日期
        string start = ((TextBox)(this.UCDateInputBeginDate.FindControl("txtDate"))).Text.Trim();
        if (start != null && start != string.Empty) {
            string end = ((TextBox)(this.UCDateInputEndDate.FindControl("txtDate"))).Text.Trim();
            filterStr += " AND SubmitDate >='" + start + "' AND dateadd(day,-1,SubmitDate)<='" + end + "'";
            this.ViewState["SearchCondition"] += "&SubmitDateStart=" + start + "&SubmitDateEnd=" + end;
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

        string startPeriod = ((TextBox)(this.UCPeriodBegin.FindControl("txtDate"))).Text.Trim();
        string endPeriod = ((TextBox)(this.UCPeriodEnd.FindControl("txtDate"))).Text.Trim();

        if (startPeriod == null || startPeriod == string.Empty) {
            if (endPeriod != null && endPeriod != string.Empty) {
                PageUtility.ShowModelDlg(this, "请选择起始费用期间!");
                return false;
            }
        } else {
            if (endPeriod == null || endPeriod == string.Empty) {
                PageUtility.ShowModelDlg(this, "请选择截止费用期间!");
                return false;
            } else {
                DateTime dtstartPeriod = DateTime.Parse(startPeriod.Substring(0, 4) + "-" + startPeriod.Substring(4, 2) + "-01");
                DateTime dtendPeriod = DateTime.Parse(endPeriod.Substring(0, 4) + "-" + endPeriod.Substring(4, 2) + "-01");
                if (dtstartPeriod > dtendPeriod) {
                    PageUtility.ShowModelDlg(this, "起始费用期间大于截止费用期间！");
                    return false;
                }
            }
        }
        return true;
    }

    protected void gvApplyList_RowCommand(object sender, GridViewCommandEventArgs e) {
        if (e.CommandName == "Apply") {
            String[] param = e.CommandArgument.ToString().Split(',');
            int FormID = Convert.ToInt32(param[0]);
            decimal ApplyAmount = Convert.ToDecimal(param[1]);
            decimal NeedCreatePOAmount = Convert.ToDecimal(ConfigurationManager.AppSettings["NeedCreatePOAmount"]);
            
            if (ApplyAmount > NeedCreatePOAmount && new FormMarketingBLL().IsNeedPOByMarketingApplyID(FormID)) {
                int POCount = FormPurchaseBLL.QueryPOCountByParentFormID(FormID);
                if (POCount <= 0) {
                    PageUtility.ShowModelDlg(this, String.Format("申请金额大于{0}，必须先创建PO!", NeedCreatePOAmount), String.Format("You need create po first when amount more than {0}!", NeedCreatePOAmount));
                    return;
                }
            }
            if (this.ViewState["SearchCondition"] != null) {
                this.Response.Redirect("~/FormMarketing/MarketingPaymentApply.aspx?FormMarketingApplyID=" + FormID + "&Source=" + HttpUtility.UrlEncode("~/FormMarketing/MarketingPaymentSelect.aspx?" + this.ViewState["SearchCondition"].ToString() + "&startIndex=" + this.gvApplyList.PageIndex));
            } else {
                this.Response.Redirect("~/FormMarketing/MarketingPaymentApply.aspx?FormMarketingApplyID=" + FormID + "&Source=" + HttpUtility.UrlEncode("~/FormMarketing/MarketingPaymentSelect.aspx"));
            }
        }
    }

    public string GetStaffNameByID(object UserID) {
        return new AuthorizationBLL().GetStuffUserById((int)UserID).StuffName;
    }

}
