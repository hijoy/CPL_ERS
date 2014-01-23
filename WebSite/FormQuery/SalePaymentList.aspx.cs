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

public partial class FormQuery_SalePaymentList : BasePage {
    protected string saveFilePath = System.Configuration.ConfigurationSettings.AppSettings["UploadDirectory"];
    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!IsPostBack) {
            String title = this.GetLocalResourceObject("titleLabel.Text").ToString();
            PageUtility.SetContentTitle(this, title);
            int stuffuserID = ((AuthorizationDS.StuffUserRow)Session["StuffUser"]).StuffUserId;
            int opScrapID = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.SalesPayment, SystemEnums.OperateEnum.Scrap);
            AuthorizationDS.PositionRow position = (AuthorizationDS.PositionRow)this.Session["Position"];
            if (!new PositionRightBLL().CheckPositionRight(position.PositionId, opScrapID)) {
                this.gvPaymentList.Columns[14].Visible = false;
            }

            if (Request["Search"] == null) {
                this.odsPaymentList.SelectParameters["queryExpression"].DefaultValue = "1!=1";
                this.odsPaymentList.SelectParameters["UserID"].DefaultValue = stuffuserID.ToString();
                this.odsPaymentList.SelectParameters["PositionID"].DefaultValue = ((AuthorizationDS.PositionRow)Session["Position"]).PositionId.ToString();
            }

            int opManageId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.SalesPayment, SystemEnums.OperateEnum.Manage);
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

                if (this.Request["IsAdvanced"] != null) {
                    if (this.Request["IsAdvanced"].ToString().Equals("3")) {
                        this.IsAdvancedDDL.SelectedValue = "3";
                    } else if (this.Request["IsAdvanced"].ToString().Equals("1")) {
                        this.IsAdvancedDDL.SelectedValue = "1";
                    } else if (this.Request["IsAdvanced"].ToString().Equals("0")) {
                        this.IsAdvancedDDL.SelectedValue = "0";
                    }
                }

                this.PaymentTypeDDL.DataBind();
                if (this.Request["PaymentTypeID"] != null)
                    this.PaymentTypeDDL.SelectedValue = this.Request["PaymentTypeID"].ToString();

                this.InvoiceStatusDDL.DataBind();
                if (this.Request["InvoiceStatusID"] != null)
                    this.InvoiceStatusDDL.SelectedValue = this.Request["InvoiceStatusID"].ToString();

                if (this.Request["IsInvoiceReturned"] != null) {
                    if (this.Request["IsInvoiceReturned"].ToString().Equals("3")) {
                        this.IsInvoiceReturnDDL.SelectedValue = "3";
                    } else if (this.Request["IsInvoiceReturned"].ToString().Equals("1")) {
                        this.IsInvoiceReturnDDL.SelectedValue = "1";
                    } else if (this.Request["IsInvoiceReturned"].ToString().Equals("0")) {
                        this.IsInvoiceReturnDDL.SelectedValue = "0";
                    }
                }

                btnSearch_Click(null, null);
            }
        }
    }

    protected void gvPaymentList_RowDataBound(object sender, GridViewRowEventArgs e) {
        // 对数据列进行赋值
        if (e.Row.RowType == DataControlRowType.DataRow) {
            if ((e.Row.RowState & DataControlRowState.Edit) != DataControlRowState.Edit) {
                DataRowView drvDetail = (DataRowView)e.Row.DataItem;
                QueryDS.FormSalePaymentViewRow row = (QueryDS.FormSalePaymentViewRow)drvDetail.Row;

                LinkButton lblFormNo = (LinkButton)e.Row.FindControl("lblFormNo");
                if (this.ViewState["SearchCondition"] != null) {
                    lblFormNo.PostBackUrl = CommonUtility.GetFormPostBackUrl(row.FormID, row.PageType, row.StatusID, "&Source=" + HttpUtility.UrlEncode("~/FormQuery/SalePaymentList.aspx?" + this.ViewState["SearchCondition"].ToString() + "&startIndex=" + this.gvPaymentList.PageIndex));
                } else {
                    lblFormNo.PostBackUrl = CommonUtility.GetFormPostBackUrl(row.FormID, row.PageType, row.StatusID, "&Source=~/FormQuery/SalePaymentList.aspx");
                }
                Label lblStatus = (Label)e.Row.FindControl("lblStatus");
                lblStatus.Text = CommonUtility.GetStatusName(row.StatusID);

                if (row.StatusID != 2) {
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
                this.gvPaymentList.PageIndex = int.Parse(start);
            }
            this.odsPaymentList.SelectParameters["queryExpression"].DefaultValue = getSearchCondition();
            this.odsPaymentList.SelectParameters["UserID"].DefaultValue = ((AuthorizationDS.StuffUserRow)Session["StuffUser"]).StuffUserId.ToString();
            this.odsPaymentList.SelectParameters["PositionID"].DefaultValue = ((AuthorizationDS.PositionRow)Session["Position"]).PositionId.ToString();
            this.gvPaymentList.DataBind();
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
            int count = bll.QueryPagedFormSalePaymentViewByRightExportViewCountByRight(queryExpession, stuffID, positionID);
            if (count > limit) {
                PageUtility.ShowModelDlg(this.Page, "导出记录数不得超过" + limit + "条，请缩小查询条件");
                return;
            }
            try {
                Session.Timeout = 30;
                string fileID = Guid.NewGuid().ToString();
                string outFile = Server.MapPath(@"~/" + saveFilePath) + @"\" + fileID + ".xls";
                System.Diagnostics.Debug.WriteLine("outFile := " + outFile);
                this.ExportDataGrid.DataSource = new GetPagedFormSalePaymentViewByRightExportTableAdapter().GetData(queryExpession, stuffID, positionID);
                this.ExportDataGrid.DataBind();
                string fileName = "FormSalePayment" + DateTime.Now.ToString("yyyyMMddHHmmss");
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
            filterStr += " AND Form.FormNo like '%" + this.txtFormNo.Text.Trim() + "%'";
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
            filterStr += " AND Customer.CustomerID = " + this.UCCustomer.CustomerID;
            this.ViewState["SearchCondition"] += "&CustomerID=" + this.UCCustomer.CustomerID;
        }

        //费用小类
        if (!this.ExpenseSubCategoryDDL.SelectedValue.Equals("0")) {
            filterStr += " AND ExpenseSubCategory.ExpenseSubCategoryID = " + ExpenseSubCategoryDDL.SelectedValue;
            this.ViewState["SearchCondition"] += "&ExpenseSubCategoryID=" + ExpenseSubCategoryDDL.SelectedValue;
        }

        //支付方式
        if (!this.BrandDDL.SelectedValue.Equals("0")) {
            filterStr += " AND Brand.BrandID = " + BrandDDL.SelectedValue;
            this.ViewState["SearchCondition"] += "&BrandID=" + BrandDDL.SelectedValue;
        }

        //申请日期
        string start = ((TextBox)(this.UCDateInputBeginDate.FindControl("txtDate"))).Text.Trim();
        if (start != null && start != string.Empty) {
            string end = ((TextBox)(this.UCDateInputEndDate.FindControl("txtDate"))).Text.Trim();
            filterStr += " AND Form.SubmitDate >='" + start + "' AND dateadd(day,-1,Form.SubmitDate)<='" + end + "'";
            this.ViewState["SearchCondition"] += "&SubmitDateStart=" + start + "&SubmitDateEnd=" + end;
        }

        //申请单编号
        if (txtApplyFormNo.Text.Trim() != string.Empty) {
            filterStr += " And charindex('P" + txtApplyFormNo.Text + "P',IsNull(FormSaleSettlement.FormApplyNos,'P'+ApplyForm.FormNo+'P'))>0";
            this.ViewState["SearchCondition"] += "&ApplyFormNo=" + this.txtApplyFormNo.Text.Trim();
        }

        //是否预付款
        if (this.IsAdvancedDDL.SelectedValue != "3") {
            if (this.IsAdvancedDDL.SelectedValue == "1") {
                this.ViewState["SearchCondition"] += "&IsAdvanced=1";
                filterStr = filterStr + " And FormSalePayment.IsAdvanced = 1";
            } else if (this.IsAdvancedDDL.SelectedValue == "0") {
                this.ViewState["SearchCondition"] += "&IsAdvanced=0";
                filterStr = filterStr + " And FormSalePayment.IsAdvanced=0";
            }
        }

        //支付方式
        if (!this.PaymentTypeDDL.SelectedValue.Equals("0")) {
            filterStr += " AND PaymentType.PaymentTypeID = " + PaymentTypeDDL.SelectedValue;
            this.ViewState["SearchCondition"] += "&PaymentTypeID=" + PaymentTypeDDL.SelectedValue;
        }

        //发票状态
        if (!this.InvoiceStatusDDL.SelectedValue.Equals("0")) {
            filterStr += " AND InvoiceStatus.InvoiceStatusID = " + InvoiceStatusDDL.SelectedValue;
            this.ViewState["SearchCondition"] += "&InvoiceStatusID=" + InvoiceStatusDDL.SelectedValue;
        }
        
        //发票是否收回
        if (this.IsInvoiceReturnDDL.SelectedValue != "3") {
            if (this.IsInvoiceReturnDDL.SelectedValue == "1") {
                this.ViewState["SearchCondition"] += "&IsInvoiceReturned=1";
                filterStr = filterStr + " And Form.IsInvoiceReturned = 1";
            } else if (this.IsInvoiceReturnDDL.SelectedValue == "0") {
                this.ViewState["SearchCondition"] += "&IsInvoiceReturned=0";
                filterStr = filterStr + " And Form.IsInvoiceReturned=0";
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

            strStatusID = " AND Form.StatusId IN (" + strStatusID + ")";
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


    protected void gvPaymentList_RowCommand(object sender, GridViewCommandEventArgs e) {
        if (e.CommandName == "scrap") {
            int FormID = Int32.Parse(e.CommandArgument.ToString());
            FormDS.FormRow row = new FormTEBLL().GetFormByID(FormID)[0];
            if (row.StatusID != 2) {
                PageUtility.ShowModelDlg(this, "只能作废审批完成状态的单据！");
                return;
            }
            new APFlowBLL().ScrapForm(FormID);
            this.gvPaymentList.DataBind();
        }
    }

}
