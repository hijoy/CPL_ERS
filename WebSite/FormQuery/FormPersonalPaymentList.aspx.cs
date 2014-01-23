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

public partial class FormQuery_FormPersonalPaymentList : BasePage {
    protected string saveFilePath = System.Configuration.ConfigurationSettings.AppSettings["UploadDirectory"];
    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!IsPostBack) {
            String title = this.GetLocalResourceObject("titleLabel.Text").ToString();
            PageUtility.SetContentTitle(this, title);

            int stuffuserID = ((AuthorizationDS.StuffUserRow)Session["StuffUser"]).StuffUserId;
            int opScrapID = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.FormPersonalReimburse, SystemEnums.OperateEnum.Scrap);
            AuthorizationDS.PositionRow position = (AuthorizationDS.PositionRow)this.Session["Position"];
            if (!new PositionRightBLL().CheckPositionRight(position.PositionId, opScrapID)) {
                this.gvApplyList.Columns[10].Visible = false;
            }

            if (Request["Search"] == null) {
                this.odsApplyList.SelectParameters["queryExpression"].DefaultValue = "1!=1";
                this.odsApplyList.SelectParameters["UserID"].DefaultValue = stuffuserID.ToString();
                this.odsApplyList.SelectParameters["PositionID"].DefaultValue = ((AuthorizationDS.PositionRow)Session["Position"]).PositionId.ToString();
            }

            int opManageId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.FormPersonalReimburse, SystemEnums.OperateEnum.Manage);
            if (!new PositionRightBLL().CheckPositionRight(position.PositionId, opManageId)) {
                this.btnExport.Visible = false;
            } else {
                this.btnExport.Visible = true;
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

    protected void gvApplyList_RowDataBound(object sender, GridViewRowEventArgs e) {
        // 对数据列进行赋值
        if (e.Row.RowType == DataControlRowType.DataRow) {
            if ((e.Row.RowState & DataControlRowState.Edit) != DataControlRowState.Edit) {
                DataRowView drvDetail = (DataRowView)e.Row.DataItem;
                QueryDS.FormPersonalPaymentViewRow row = (QueryDS.FormPersonalPaymentViewRow)drvDetail.Row;

                String sourceUrl = "";
                if (this.ViewState["SearchCondition"] != null) {
                    sourceUrl = "&Source=" + HttpUtility.UrlEncode("~/FormQuery/FormPersonalPaymentList.aspx?" + this.ViewState["SearchCondition"].ToString() + "&startIndex=" + this.gvApplyList.PageIndex);
                }

                LinkButton lbtnFormNo = (LinkButton)e.Row.FindControl("lbtnFormNo");
                lbtnFormNo.PostBackUrl = CommonUtility.GetFormPostBackUrl(row.FormID, row.PageType, (int)SystemEnums.FormStatus.Awaiting, sourceUrl);

                if (row.StatusID != 2) {
                    e.Row.FindControl("lbtnScrap").Visible = false;
                }

                Label lblStatus = (Label)e.Row.FindControl("lblFormStatus");
                lblStatus.Text = CommonUtility.GetStatusName(row.StatusID);
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
            this.odsApplyList.SelectParameters["UserID"].DefaultValue = ((AuthorizationDS.StuffUserRow)Session["StuffUser"]).StuffUserId.ToString();
            this.odsApplyList.SelectParameters["PositionID"].DefaultValue = ((AuthorizationDS.PositionRow)Session["Position"]).PositionId.ToString();
            this.gvApplyList.DataBind();
        }
    }

    protected void btnExport_Click(object sender, EventArgs e) {
        if (!checkSearchConditionValid()) {
            return;
        } else {
            string queryExpession = getSearchCondition();
            queryExpession += " and Form.FormTypeID in (1,2,3,4) and StatusID<>0";
            int stuffID = ((AuthorizationDS.StuffUserRow)Session["StuffUser"]).StuffUserId;
            int positionID = ((AuthorizationDS.PositionRow)Session["Position"]).PositionId;
            int limit = int.Parse(System.Configuration.ConfigurationManager.AppSettings["ExportCountLimit"]);
            ReportQueryBLL bll = new ReportQueryBLL();
            int count = bll.QueryPagedFormPersonalReimburseViewByRightExportViewCountByRight(queryExpession, stuffID, positionID);
            if (count > limit) {
                PageUtility.ShowModelDlg(this.Page, "导出记录数不得超过" + limit + "条，请缩小查询条件");
                return;
            }
            try {
                Session.Timeout = 30;
                string fileID = Guid.NewGuid().ToString();
                string outFile = Server.MapPath(@"~/" + saveFilePath) + @"\" + fileID + ".xls";
                System.Diagnostics.Debug.WriteLine("outFile := " + outFile);
                this.ExportDataGrid.DataSource = new GetPagedFormPersonalReimburseViewByRightExportTableAdapter().GetData(queryExpession, stuffID, positionID);
                this.ExportDataGrid.DataBind();
                string fileName = "FormPersonalReimburse" + DateTime.Now.ToString("yyyyMMddHHmmss");
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

    private string getSearchCondition() {
        int stuffuserID = ((AuthorizationDS.StuffUserRow)Session["StuffUser"]).StuffUserId;
        string filterStr = "Form.StatusId >=" + ((int)SystemEnums.FormStatus.Awaiting).ToString();
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

        //费用期间
        string startPeriod = ((TextBox)(this.UCPeriodBegin.FindControl("txtDate"))).Text.Trim();
        if (startPeriod != null && startPeriod != string.Empty) {
            startPeriod = startPeriod.Substring(0, 4) + "-" + startPeriod.Substring(4, 2) + "-01";
            string endPeriod = ((TextBox)(this.UCPeriodEnd.FindControl("txtDate"))).Text.Trim();
            endPeriod = endPeriod.Substring(0, 4) + "-" + endPeriod.Substring(4, 2) + "-01";
            string periodstr = "(CASE WHEN FormTravelReimburse.Period IS NOT NULL THEN FormTravelReimburse.Period WHEN FormPersonalReimburse.Period IS NOT NULL THEN FormPersonalReimburse.Period END)";
            filterStr += " AND " + periodstr + ">='" + startPeriod + "'";
            filterStr += " AND " + periodstr + " <='" + endPeriod + "'";
            this.ViewState["SearchCondition"] += "&PeriodStart=" + startPeriod
               + "&PeriodEnd=" + endPeriod;
        }

        //申请人组织机构
        if (this.ucOU.OUId != null) {
            filterStr += " AND charindex('P" + this.ucOU.OUId + "P',OrganizationTreePath) > 0";
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

    public string GetStaffNameByID(object UserID) {
        return new AuthorizationBLL().GetStuffUserById((int)UserID).StuffName;
    }

    protected void gvApplyList_RowCommand(object sender, GridViewCommandEventArgs e) {
        if (e.CommandName == "scrap") {
            int FormID = Int32.Parse(e.CommandArgument.ToString());
            FormDS.FormRow row = new FormTEBLL().GetFormByID(FormID)[0];
            if (row.StatusID != 2) {
                PageUtility.ShowModelDlg(this, "只能作废审批完成状态的单据！");
            } else {
                new APFlowBLL().ScrapForm(FormID);
                gvApplyList.DataBind();
            }
        }
    }
}
