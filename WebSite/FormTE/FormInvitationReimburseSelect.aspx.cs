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

public partial class FormTE_FormInvitationReimburseSelect : BasePage {

    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!IsPostBack) {
            String title = GetLocalResourceObject("titleLabel.Text").ToString();
            PageUtility.SetContentTitle(this, title, "Form Invitation Reimburse Select");
            this.Page.Title = title;

            int stuffuserID = ((AuthorizationDS.StuffUserRow)Session["StuffUser"]).StuffUserId;
            this.ViewState["DefaultFilter"] = " UserID=" + stuffuserID + " and StatusID=2 and FormID not in (select FormInvitationApplyID from FormInvitationReimburse inner join Form on Form.FormID=FormInvitationReimburse.FormInvitationReimburseID where StatusID in (0,1,2,3) and FormInvitationApplyID is not null)";
            if (Request["Search"] == null) {
                this.odsApplyList.SelectParameters["queryExpression"].DefaultValue = this.ViewState["DefaultFilter"].ToString();
            }
        }
    }

    protected override void OnLoadComplete(EventArgs e) {
        if (!IsPostBack) {
            if (Request["Search"] != null) {
                if (this.Request["FormNo"] != null) {
                    this.txtFormNo.Text = this.Request["FormNo"].ToString();
                }
                if (this.Request["CustomerName"] != null) {
                    this.txtCustomerName.Text = this.Request["CustomerName"].ToString();
                }
                if (this.Request["PeriodStart"] != null) {
                    this.UCPeriodBegin.SelectedDate = this.Request["PeriodStart"].ToString();
                }
                if (this.Request["PeriodEnd"] != null) {
                    this.UCPeriodEnd.SelectedDate = this.Request["PeriodEnd"].ToString();
                }
                if (this.Request["SubmitDateStart"] != null) {
                    this.UCDateInputBeginDate.SelectedDate = this.Request["SubmitDateStart"].ToString();
                }
                if (this.Request["SubmitDateEnd"] != null) {
                    this.UCDateInputEndDate.SelectedDate = this.Request["SubmitDateEnd"].ToString();
                }
                btnSearch_Click(null, null);
            }
        }
    }

    protected void gvApplyList_RowDataBound(object sender, GridViewRowEventArgs e) {
        // 对数据列进行赋值
        if (e.Row.RowType == DataControlRowType.DataRow) {
            if ((e.Row.RowState & DataControlRowState.Edit) != DataControlRowState.Edit) {
                DataRowView drvDetail = (DataRowView)e.Row.DataItem;
                QueryDS.FormInvitationApplyViewRow row = (QueryDS.FormInvitationApplyViewRow)drvDetail.Row;

                LinkButton lbtnFormNo = (LinkButton)e.Row.FindControl("lbtnFormNo");
                LinkButton lbtnReimburse = (LinkButton)e.Row.FindControl("lbtnReimburse");

                string url = string.Empty;
                if (this.ViewState["SearchCondition"] != null) {
                    lbtnReimburse.PostBackUrl = "~/FormTE/FormInvitationReimburseApply.aspx?FormInvitationApplyID=" + row.FormID +
                        "&Source=" + HttpUtility.UrlEncode("~/FormTE/FormInvitationReimburseSelect.aspx?" + this.ViewState["SearchCondition"].ToString() + "&startIndex=" + this.gvApplyList.PageIndex);
                    lbtnFormNo.PostBackUrl = "~/FormTE/FormInvitationApproval.aspx?ObjectID=" + row.FormID +
                         "&Source=" + HttpUtility.UrlEncode("~/FormTE/FormInvitationReimburseSelect.aspx?" + this.ViewState["SearchCondition"].ToString() + "&startIndex=" + this.gvApplyList.PageIndex);
                } else {
                    lbtnReimburse.PostBackUrl = "~/FormTE/FormInvitationReimburseApply.aspx?FormInvitationApplyID=" + row.FormID +
                         "&Source=~/FormTE/FormInvitationReimburseSelect.aspx";
                    lbtnFormNo.PostBackUrl = "~/FormTE/FormInvitationApproval.aspx?ObjectID=" + row.FormID +
                         "&Source=~/FormTE/FormInvitationReimburseSelect.aspx";
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

        //客户
        if (this.txtCustomerName.Text.Trim() != string.Empty) {
            filterStr += " AND CustomerName like '%" + this.txtCustomerName.Text.Trim() + "%'";
            this.ViewState["SearchCondition"] += "&CustomerName=" + this.txtCustomerName.Text.Trim();
        }

        //费用期间
        string startPeriod = ((TextBox)(this.UCPeriodBegin.FindControl("txtDate"))).Text.Trim();
        if (startPeriod != null && startPeriod != string.Empty) {
            string endPeriod = ((TextBox)(this.UCPeriodEnd.FindControl("txtDate"))).Text.Trim();
            filterStr += " AND Period >='" + startPeriod.Substring(0, 4) + "-" + startPeriod.Substring(4, 2) + "-01'" +
                " AND Period<='" + endPeriod.Substring(0, 4) + "-" + endPeriod.Substring(4, 2) + "-01'";
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

    public string GetStaffNameByID(object UserID) {
        return new AuthorizationBLL().GetStuffUserById((int)UserID).StuffName;
    }
}
