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

public partial class FormQuery_SaleSettlementList : BasePage {
    protected string saveFilePath = System.Configuration.ConfigurationSettings.AppSettings["UploadDirectory"];
    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!IsPostBack) {
            String title = this.GetLocalResourceObject("titleLabel.Text").ToString();
            PageUtility.SetContentTitle(this, title);
            int stuffuserID = ((AuthorizationDS.StuffUserRow)Session["StuffUser"]).StuffUserId;
            int opScrapID = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.SaleSettlement, SystemEnums.OperateEnum.Scrap);
            AuthorizationDS.PositionRow position = (AuthorizationDS.PositionRow)this.Session["Position"];
            if (!new PositionRightBLL().CheckPositionRight(position.PositionId, opScrapID)) {
                this.gvSettlementList.Columns[12].Visible = false;
            }
            if (Request["Search"] == null) {
                this.odsSettlementList.SelectParameters["queryExpression"].DefaultValue = "1!=1";
                this.odsSettlementList.SelectParameters["UserID"].DefaultValue = stuffuserID.ToString();
                this.odsSettlementList.SelectParameters["PositionID"].DefaultValue = ((AuthorizationDS.PositionRow)Session["Position"]).PositionId.ToString();
            }

            int opManageId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.SaleSettlement, SystemEnums.OperateEnum.Manage);
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
                if (this.Request["FormNo"] != null)
                    this.txtFormNo.Text = this.Request["FormNo"].ToString();
                if (this.Request["StuffName"] != null)
                    this.txtStuffUser.Text = this.Request["StuffName"].ToString();
                if (this.Request["UCOUID"] != null)
                    this.UCOU.OUId = int.Parse(this.Request["UCOUID"].ToString());
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
                if (this.Request["chkAwaiting"] != null)
                    this.chkAwaiting.Checked = true;
                if (this.Request["chkApproveCompleted"] != null)
                    this.chkApproveCompleted.Checked = true;
                if (this.Request["chkRejected"] != null)
                    this.chkRejected.Checked = true;
                if (this.Request["chkScrap"] != null)
                    this.chkScrap.Checked = true;

                if (this.Request["IsClose"] != null) {
                    if (this.Request["IsClose"].ToString().Equals("3")) {
                        this.IsCloseDDL.SelectedValue = "3";
                    } else if (this.Request["IsClose"].ToString().Equals("1")) {
                        this.IsCloseDDL.SelectedValue = "1";
                    } else if (this.Request["IsClose"].ToString().Equals("0")) {
                        this.IsCloseDDL.SelectedValue = "0";
                    }
                }

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
                    lblFormNo.PostBackUrl = CommonUtility.GetFormPostBackUrl(row.FormID, row.PageType, row.StatusID, "&Source=" + HttpUtility.UrlEncode("~/FormQuery/SaleSettlementList.aspx?" + this.ViewState["SearchCondition"].ToString() + "&startIndex=" + this.gvSettlementList.PageIndex));
                } else {
                    lblFormNo.PostBackUrl = CommonUtility.GetFormPostBackUrl(row.FormID, row.PageType, row.StatusID, "&Source=~/FormQuery/SaleSettlementList.aspx");
                }
                Label lblStatus = (Label)e.Row.FindControl("lblStatus");
                lblStatus.Text = CommonUtility.GetStatusName(row.StatusID);

                if (row.StatusID != 2 || row.IsClose == true) {//如果不是审批完成，或者已经关闭不能进行作废
                    LinkButton lbtnScrap = (LinkButton)e.Row.FindControl("lbtnScrap");
                    if (lbtnScrap != null) {
                        lbtnScrap.Visible = false;
                    }
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
            this.odsSettlementList.SelectParameters["UserID"].DefaultValue = ((AuthorizationDS.StuffUserRow)Session["StuffUser"]).StuffUserId.ToString();
            this.odsSettlementList.SelectParameters["PositionID"].DefaultValue = ((AuthorizationDS.PositionRow)Session["Position"]).PositionId.ToString();
            this.gvSettlementList.DataBind();
        }
    }

    protected void btnExport_Click(object sender, EventArgs e) {
        if (!checkSearchConditionValid()) {
            return;
        } else {
            string queryExpession = getSearchCondition();
            int stuffID = ((AuthorizationDS.StuffUserRow)Session["StuffUser"]).StuffUserId;
            int positionID = ((AuthorizationDS.PositionRow)Session["Position"]).PositionId;
            int limit = int.Parse(System.Configuration.ConfigurationManager.AppSettings["ExportCountLimit"]);
            ReportQueryBLL bll = new ReportQueryBLL();
            int count = bll.QueryPagedFormSaleSettlementViewByRightExportViewCountByRight(queryExpession, stuffID, positionID);
            if (count > limit) {
                PageUtility.ShowModelDlg(this.Page, "导出记录数不得超过" + limit + "条，请缩小查询条件");
                return;
            }
            try {
                Session.Timeout = 30;
                string fileID = Guid.NewGuid().ToString();
                string outFile = Server.MapPath(@"~/" + saveFilePath) + @"\" + fileID + ".xls";
                System.Diagnostics.Debug.WriteLine("outFile := " + outFile);
                this.ExportDataGrid.DataSource = new GetPagedFormSaleSettlementViewByRightExportTableAdapter().GetData(queryExpession, stuffID, positionID);
                this.ExportDataGrid.DataBind();
                string fileName = "FormSaleSettlement" + DateTime.Now.ToString("yyyyMMddHHmmss");
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
        //申请人组织机构
        if (this.UCOU.OUId != null) {
            filterStr += " AND charindex('P" + this.UCOU.OUId + "P',Position.OrganizationTreePath) > 0";
            this.ViewState["SearchCondition"] += "&UCOUID=" + this.UCOU.OUId;
        }

        //客户
        if (this.UCCustomer.CustomerID != string.Empty && this.UCCustomer.CustomerID!=null) {
            filterStr += " AND FormSaleSettlement.CustomerID = " + this.UCCustomer.CustomerID;
            this.ViewState["SearchCondition"] += "&CustomerID=" + this.UCCustomer.CustomerID;
        }

        //费用小类
        if (!this.ExpenseSubCategoryDDL.SelectedValue.Equals("0")) {
            filterStr += " AND FormSaleSettlement.ExpenseSubCategoryID = " + ExpenseSubCategoryDDL.SelectedValue;
            this.ViewState["SearchCondition"] += "&ExpenseSubCategoryID=" + ExpenseSubCategoryDDL.SelectedValue;
        }

        //支付方式
        if (!this.BrandDDL.SelectedValue.Equals("0")) {
            filterStr += " AND FormSaleSettlement.BrandID = " + BrandDDL.SelectedValue;
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

        //是否报销完成
        if (this.IsCloseDDL.SelectedValue != "3") {
            if (this.IsCloseDDL.SelectedValue == "1") {
                this.ViewState["SearchCondition"] += "&IsClose=1";
                filterStr = filterStr + " And FormSaleSettlement.IsClose = 1";
            } else if (this.IsCloseDDL.SelectedValue == "0") {
                this.ViewState["SearchCondition"] += "&IsClose=0";
                filterStr = filterStr + " And FormSaleSettlement.IsClose=0";
            }
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

        return true;
    }

    protected void gvSettlementList_RowCommand(object sender, GridViewCommandEventArgs e) {
        if (e.CommandName == "scrap") {
            int FormID = Int32.Parse(e.CommandArgument.ToString());
            QueryDS.FormSaleSettlementViewRow row = new FormQueryBLL().GetFormSaleSettlementViewByID(FormID);

            string PaymentFormNo = new FormSaleBLL().GetPaymentFormNoBySaleSettlementID(row.FormID);
            if (PaymentFormNo != "") {
                PageUtility.ShowModelDlg(this.Page, "申请单已经被付款过，付款单编号为：" + PaymentFormNo);
                return;
            }

            new APFlowBLL().ScrapForm(FormID);
            this.gvSettlementList.DataBind();
        }
    }

}
