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
using BusinessObjects.ReportDSTableAdapters;
using System.Collections.Generic;

public partial class FormQuery_RDApplyList : BasePage {
    protected string saveFilePath = System.Configuration.ConfigurationSettings.AppSettings["UploadDirectory"];
    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!IsPostBack) {
            String title = this.GetLocalResourceObject("titleLabel.Text").ToString();
            PageUtility.SetContentTitle(this, title);

            int opViewId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.ReportRDApplyDetail, SystemEnums.OperateEnum.View);
            AuthorizationDS.PositionRow position = (AuthorizationDS.PositionRow)this.Session["Position"];
            PositionRightBLL positionRightBLL = new PositionRightBLL();
            bool HasViewRight = positionRightBLL.CheckPositionRight(position.PositionId, opViewId);
            if (!HasViewRight) {
                Response.Redirect("~/ErrorPage/NoRightErrorPage.aspx");
                return;
            }
        }
    }

    protected override void OnLoadComplete(EventArgs e) {
        if (!IsPostBack) {
            if (Request["Search"] != null) {
                if (this.Request["FormNo"] != null) {
                    this.txtFormNo.Text = this.Request["FormNo"].ToString();
                }
                if (this.Request["StuffName"] != null) {
                    this.txtStuffUser.Text = this.Request["StuffName"].ToString();
                }
                if (this.Request["UCOUID"] != null) {
                    this.ucOU.OUId = int.Parse(this.Request["UCOUID"].ToString());
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
                if (this.Request["chkAwaiting"] != null) {
                    this.chkAwaiting.Checked = true;
                }
                if (this.Request["chkApproveCompleted"] != null) {
                    this.chkApproveCompleted.Checked = true;
                }
                if (this.Request["chkRejected"] != null) {
                    this.chkRejected.Checked = true;
                }
                if (this.Request["chkScrap"] != null) {
                    this.chkScrap.Checked = true;
                }
                btnSearch_Click(null, null);
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e) {
        if (!checkSearchConditionValid()) {
            return;
        } else {
            Microsoft.Reporting.WebForms.ReportParameter[] ps = new Microsoft.Reporting.WebForms.ReportParameter[3];
            string reportName = "ReportDetailRDApply";
            ps[0] = new Microsoft.Reporting.WebForms.ReportParameter("queryExpression", getSearchCondition());
            ps[1] = new Microsoft.Reporting.WebForms.ReportParameter("StuffUserID", ((AuthorizationDS.StuffUserRow)Session["StuffUser"]).StuffUserId.ToString());
            ps[2] = new Microsoft.Reporting.WebForms.ReportParameter("PositionID", ((AuthorizationDS.PositionRow)Session["Position"]).PositionId.ToString());
            //load report 
            this.ReportViewer.LoadReport(reportName, ps);
        }
    }

    private string getSearchCondition() {
        int stuffuserID = ((AuthorizationDS.StuffUserRow)Session["StuffUser"]).StuffUserId;
        string filterStr = " StatusId >=" + ((int)SystemEnums.FormStatus.Awaiting).ToString();
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

        //项目类型
        if (txtProjectType.Text.Trim() != string.Empty) {
            filterStr += " AND ProjectType like '%" + this.txtProjectType.Text.Trim() + "%'";
            this.ViewState["SearchCondition"] += "&ProjectType=" + this.txtProjectType.Text.Trim();
        }

        //采购预算类型
        if (ddlPurchaseBudgetType.SelectedValue != string.Empty && ddlPurchaseBudgetType.SelectedValue != "0") {
            filterStr += " AND BudgetRD.PurchaseBudgetTypeID = " + ddlPurchaseBudgetType.SelectedValue;
            this.ViewState["SearchCondition"] += "&PurchaseBudgetTypeID=" + ddlPurchaseBudgetType.SelectedValue;
        }

        //费用期间
        string startPeriod = ((TextBox)(this.UCPeriodBegin.FindControl("txtDate"))).Text.Trim();
        if (startPeriod != null && startPeriod != string.Empty) {
            startPeriod = startPeriod.Substring(0, 4) + "-" + startPeriod.Substring(4, 2) + "-01";
            string endPeriod = ((TextBox)(this.UCPeriodEnd.FindControl("txtDate"))).Text.Trim();
            endPeriod = endPeriod.Substring(0, 4) + "-" + endPeriod.Substring(4, 2) + "-01";
            filterStr += " AND FPeriod >='" + startPeriod + "'";
            filterStr += " AND FPeriod <='" + endPeriod + "'";
            this.ViewState["SearchCondition"] += "&PeriodStart=" + startPeriod
               + "&PeriodEnd=" + endPeriod;
        }

        //申请人组织机构
        if (this.ucOU.OUId != null) {
            filterStr += " AND charindex('P" + this.ucOU.OUId + "P',Position.OrganizationTreePath) > 0";
            this.ViewState["SearchCondition"] += "&UCOUID=" + this.ucOU.OUId;
        }

        //申请日期
        string start = ((TextBox)(this.UCDateInputBeginDate.FindControl("txtDate"))).Text.Trim();
        if (start != null && start != string.Empty) {
            string end = ((TextBox)(this.UCDateInputEndDate.FindControl("txtDate"))).Text.Trim();
            filterStr += " AND SubmitDate >='" + start + "' AND dateadd(day,-1,SubmitDate)<='" + end + "'";
            this.ViewState["SearchCondition"] += "&SubmitDateStart=" + start + "&SubmitDateEnd=" + end;
        }

        //单据状态
        filterStr += getStatusID();

        return filterStr;
    }

    private string getStatusID() {
        string strStatusID = string.Empty;
        //单据状态
        if (chkAwaiting.Checked == true ||
            chkApproveCompleted.Checked == true ||
            chkRejected.Checked == true ||
            chkScrap.Checked == true) {
            if (chkAwaiting.Checked == true) {
                this.ViewState["SearchCondition"] += "&chkAwaiting=true";
                if (strStatusID == string.Empty) {
                    strStatusID = ((int)SystemEnums.FormStatus.Awaiting).ToString();
                } else {
                    strStatusID += "," + ((int)SystemEnums.FormStatus.Awaiting).ToString();
                }
            }

            if (chkApproveCompleted.Checked == true) {
                this.ViewState["SearchCondition"] += "&chkApproveCompleted=true";
                if (strStatusID == string.Empty) {
                    strStatusID = ((int)SystemEnums.FormStatus.ApproveCompleted).ToString();
                } else {
                    strStatusID += "," + ((int)SystemEnums.FormStatus.ApproveCompleted).ToString();
                }
            }

            if (chkRejected.Checked == true) {
                this.ViewState["SearchCondition"] += "&chkRejected=true";
                if (strStatusID == string.Empty) {
                    strStatusID = ((int)SystemEnums.FormStatus.Rejected).ToString();
                } else {
                    strStatusID += "," + ((int)SystemEnums.FormStatus.Rejected).ToString();
                }
            }

            if (chkScrap.Checked == true) {
                this.ViewState["SearchCondition"] += "&chkScrap=true";
                if (strStatusID == string.Empty) {
                    strStatusID = ((int)SystemEnums.FormStatus.Scrap).ToString();
                } else {
                    strStatusID += "," + ((int)SystemEnums.FormStatus.Scrap).ToString();
                }
            }
            strStatusID = " AND StatusId IN (" + strStatusID + ")";
        }

        return strStatusID;
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

}
