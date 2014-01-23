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

public partial class FormQuery_POList : BasePage {
    protected string saveFilePath = System.Configuration.ConfigurationSettings.AppSettings["UploadDirectory"];
    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!IsPostBack) {
            String title = this.GetLocalResourceObject("titleLabel.Text").ToString();
            PageUtility.SetContentTitle(this, title);

            int stuffuserID = ((AuthorizationDS.StuffUserRow)Session["StuffUser"]).StuffUserId;
            int opScrapID = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.PO, SystemEnums.OperateEnum.Scrap);
            AuthorizationDS.PositionRow position = (AuthorizationDS.PositionRow)this.Session["Position"];
            if (!new PositionRightBLL().CheckPositionRight(position.PositionId, opScrapID)) {
                this.gvApplyList.Columns[14].Visible = false;
            }

            if (Request["Search"] == null) {
                this.odsApplyList.SelectParameters["queryExpression"].DefaultValue = "1!=1";
                this.odsApplyList.SelectParameters["UserID"].DefaultValue = stuffuserID.ToString();
                this.odsApplyList.SelectParameters["PositionID"].DefaultValue = ((AuthorizationDS.PositionRow)Session["Position"]).PositionId.ToString();
            }

            int opManageId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.PO, SystemEnums.OperateEnum.Manage);
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
                if (this.Request["ParentFormNo"] != null)
                    this.txtParentFormNo.Text = this.Request["ParentFormNo"].ToString();
                if (this.Request["StuffName"] != null)
                    this.txtStuffUser.Text = this.Request["StuffName"].ToString();
                if (this.Request["UCOUID"] != null)
                    this.UCOU.OUId = int.Parse(this.Request["UCOUID"].ToString());
                if (this.Request["VendorID"] != null)
                    this.UCVendor.VendorID = this.Request["VendorID"].ToString();
                if (this.Request["ItemCategoryID"] != null)
                    this.UCItemCategory.ItemCategoryID = this.Request["ItemCategoryID"].ToString();
                this.PurchaseTypeDDL.DataBind();
                if (this.Request["PurchaseTypeID"] != null)
                    this.PurchaseTypeDDL.SelectedValue = this.Request["PurchaseTypeID"].ToString();
                this.PurchaseBudgetTypeDDL.DataBind();
                if (this.Request["PurchaseBudgetTypeID"] != null)
                    this.PurchaseBudgetTypeDDL.SelectedValue = this.Request["PurchaseBudgetTypeID"].ToString();
                if (this.Request["PeriodStart"] != null)
                    this.UCPeriodBegin.SelectedDate = this.Request["PeriodStart"].ToString();
                if (this.Request["PeriodEnd"] != null)
                    this.UCPeriodEnd.SelectedDate = this.Request["PeriodEnd"].ToString();
                if (this.Request["SubmitDateStart"] != null)
                    this.UCDateInputBeginDate.SelectedDate = this.Request["SubmitDateStart"].ToString();
                if (this.Request["SubmitDateEnd"] != null)
                    this.UCDateInputEndDate.SelectedDate = this.Request["SubmitDateEnd"].ToString();

                if (this.Request["chkAwaiting"] != null)
                    this.chkAwaiting.Checked = true;
                if (this.Request["chkApproveCompleted"] != null)
                    this.chkApproveCompleted.Checked = true;
                if (this.Request["chkRejected"] != null)
                    this.chkRejected.Checked = true;
                if (this.Request["chkScrap"] != null)
                    this.chkScrap.Checked = true;

                if (this.Request["chkPurchase"] != null)
                    this.chkPurchase.Checked = true;
                if (this.Request["chkSale"] != null)
                    this.chkSale.Checked = true;
                if (this.Request["chkMarketing"] != null)
                    this.chkMarketing.Checked = true;
                if (this.Request["chkRD"] != null)
                    this.chkRD.Checked = true;
               
                btnSearch_Click(null, null);
            }
        }
    }

    protected void gvApplyList_RowDataBound(object sender, GridViewRowEventArgs e) {
        // 对数据列进行赋值
        if (e.Row.RowType == DataControlRowType.DataRow) {
            if ((e.Row.RowState & DataControlRowState.Edit) != DataControlRowState.Edit) {
                DataRowView drvDetail = (DataRowView)e.Row.DataItem;
                QueryDS.FormPOViewRow row = (QueryDS.FormPOViewRow)drvDetail.Row;

                LinkButton lblFormNo = (LinkButton)e.Row.FindControl("lblFormNo");
                if (this.ViewState["SearchCondition"] != null) {
                    lblFormNo.PostBackUrl = CommonUtility.GetFormPostBackUrl(row.FormID, row.PageType, row.StatusID, "&Source=" + HttpUtility.UrlEncode("~/FormQuery/POList.aspx?" + this.ViewState["SearchCondition"].ToString() + "&startIndex=" + this.gvApplyList.PageIndex));
                } else {
                    lblFormNo.PostBackUrl = CommonUtility.GetFormPostBackUrl(row.FormID, row.PageType, row.StatusID, "&Source=~/FormQuery/POList.aspx");
                }

                if (row.StatusID != 2) {
                    e.Row.FindControl("lbtnScrap").Visible = false;
                }

                Label lblStatus = (Label)e.Row.Cells[1].FindControl("lblStatus");
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
            int stuffID = ((AuthorizationDS.StuffUserRow)Session["StuffUser"]).StuffUserId;
            int positionID = ((AuthorizationDS.PositionRow)Session["Position"]).PositionId;
            int limit = int.Parse(System.Configuration.ConfigurationManager.AppSettings["ExportCountLimit"]);
            ReportQueryBLL bll = new ReportQueryBLL();
            int count = bll.QueryPagedFormPOViewByRightExportViewCountByRight(queryExpession, stuffID, positionID);
            if (count > limit) {
                PageUtility.ShowModelDlg(this.Page, "导出记录数不得超过" + limit + "条，请缩小查询条件");
                return;
            }
            try {
                Session.Timeout = 30;
                string fileID = Guid.NewGuid().ToString();
                string outFile = Server.MapPath(@"~/" + saveFilePath) + @"\" + fileID + ".xls";
                System.Diagnostics.Debug.WriteLine("outFile := " + outFile);
                this.ExportDataGrid.DataSource = new GetPagedFormPOViewByRightExportTableAdapter().GetData(queryExpession, stuffID, positionID);
                this.ExportDataGrid.DataBind();
                string fileName = "FormPO" + DateTime.Now.ToString("yyyyMMddHHmmss");
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
        //PO编号
        if (txtFormNo.Text.Trim() != string.Empty) {
            filterStr += " AND Form.FormNo like '%" + this.txtFormNo.Text.Trim() + "%'";
            this.ViewState["SearchCondition"] += "&FormNo=" + this.txtFormNo.Text.Trim();
        }
        //申请单编号
        if (txtParentFormNo.Text.Trim() != string.Empty) {
            filterStr += " AND FormPO.ParentFormNo like '%" + this.txtParentFormNo.Text.Trim() + "%'";
            this.ViewState["SearchCondition"] += "&ParentFormNo=" + this.txtParentFormNo.Text.Trim();
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

        if (this.UCVendor.VendorID != string.Empty && this.UCVendor.VendorID != null) {
            filterStr += " AND Vendor.VendorID = " + this.UCVendor.VendorID;
            this.ViewState["SearchCondition"] += "&VendorID=" + this.UCVendor.VendorID;
        }

        if (!this.UCItemCategory.ItemCategoryID.Equals("")) {
            filterStr += " AND ItemCategory.ItemCategoryID = " + UCItemCategory.ItemCategoryID;
            this.ViewState["SearchCondition"] += "&ItemCategoryID=" + UCItemCategory.ItemCategoryID;
        }

        if (!this.PurchaseTypeDDL.SelectedValue.Equals("0")) {
            filterStr += " AND PurchaseType.PurchaseTypeID = " + PurchaseTypeDDL.SelectedValue;
            this.ViewState["SearchCondition"] += "&PurchaseTypeID=" + PurchaseTypeDDL.SelectedValue;
        }

        if (!this.PurchaseBudgetTypeDDL.SelectedValue.Equals("0")) {
            filterStr += " AND PurchaseBudgetType.PurchaseBudgetTypeID = " + PurchaseBudgetTypeDDL.SelectedValue;
            this.ViewState["SearchCondition"] += "&PurchaseBudgetTypeID=" + PurchaseBudgetTypeDDL.SelectedValue;
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
        filterStr += getPOType();
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

    private string getPOType() {
        string strPOType = string.Empty;
        if (chkPurchase.Checked == true ||chkSale.Checked == true ||chkMarketing.Checked == true || chkRD.Checked == true) {
            if (chkPurchase.Checked == true) {
                this.ViewState["SearchCondition"] += "&chkPurchase=true";
                if (strPOType == string.Empty) {
                    strPOType = ((int)SystemEnums.POType.Purchase).ToString();
                } else {
                    strPOType += "," + ((int)SystemEnums.POType.Purchase).ToString();
                }
            }

            if (chkSale.Checked == true) {
                this.ViewState["SearchCondition"] += "&chkSale=true";
                if (strPOType == string.Empty) {
                    strPOType = ((int)SystemEnums.POType.Sale).ToString();
                } else {
                    strPOType += "," + ((int)SystemEnums.POType.Sale).ToString();
                }
            }

            if (chkMarketing.Checked == true) {
                this.ViewState["SearchCondition"] += "&chkMarketing=true";
                if (strPOType == string.Empty) {
                    strPOType = ((int)SystemEnums.POType.Marketing).ToString();
                } else {
                    strPOType += "," + ((int)SystemEnums.POType.Marketing).ToString();
                }
            }

            if (chkRD.Checked == true) {
                this.ViewState["SearchCondition"] += "&chkRD=true";
                if (strPOType == string.Empty) {
                    strPOType = ((int)SystemEnums.POType.RD).ToString();
                } else {
                    strPOType += "," + ((int)SystemEnums.POType.RD).ToString();
                }
            }

            strPOType = " AND FormPO.POType IN (" + strPOType + ")";
        }

        return strPOType;
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
        if (e.CommandName == "scrap") {
            int FormID = Int32.Parse(e.CommandArgument.ToString());
            FormDS.FormRow row = new FormTEBLL().GetFormByID(FormID)[0];
            if (row.StatusID != 2) {
                PageUtility.ShowModelDlg(this, "只能作废审批完成状态的单据！");
                return;
            }
            string PVNo = new FormPurchaseBLL().GetPVNoByPOID(FormID);
            if (PVNo != "") {
                PageUtility.ShowModelDlg(this.Page, "申请单已经创建过PV，PV编号为：" + PVNo);
                return;
            }
            new APFlowBLL().ScrapForm(FormID);
            gvApplyList.DataBind();
        }
    }
}
