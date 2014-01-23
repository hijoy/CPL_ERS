using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessObjects;
using System.Text;
using System.Data.Sql;
using System.Data.SqlClient;
using BusinessObjects.LogDSTableAdapters;
using BusinessObjects.FormDSTableAdapters;
using System.Data.OleDb;

public partial class Import_ImportSettlementPayment : BasePage {
    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!this.IsPostBack) {
            PageUtility.SetContentTitle(this, "报销记录导入");
            this.Page.Title = "报销记录导入";
            int opImportId = BusinessUtility.GetBusinessOperateId(SystemEnums.BusinessUseCase.ImportSettlementPayment, SystemEnums.OperateEnum.Manage);
            AuthorizationDS.PositionRow position = (AuthorizationDS.PositionRow)this.Session["Position"];
            PositionRightBLL positionRightBLL = new PositionRightBLL();
            this.HasImportRight = positionRightBLL.CheckPositionRight(position.PositionId, opImportId);
            //临时增加
            this.HasImportRight = true;
            if (!this.HasImportRight) {
                Response.Redirect("~/ErrorPage/NoRightErrorPage.aspx");
                return;
            }
        }
    }

    protected bool HasImportRight {
        get {
            return (bool)this.ViewState["HasImportRight"];
        }
        set {
            this.ViewState["HasImportRight"] = value;
        }
    }

    public string GetOUNameByOuID(object ouID) {
        int id = Convert.ToInt32(ouID);
        return new OUTreeBLL().GetOrganizationUnitById(id).OrganizationUnitName;
    }

    protected void btnSearch_Click(object sender, EventArgs e) {
        if (!checkSearchConditionValid()) {
            return;
        } else {
            StringBuilder queryExpression = new StringBuilder();

            string start = ((TextBox)(this.UCDateInputBeginDate.FindControl("txtDate"))).Text.Trim();

            if (start != null && start != string.Empty) {
                queryExpression.Append(" ImportDate>= '" + start + "'");
            }

            string end = ((TextBox)(this.UCDateInputEndDate.FindControl("txtDate"))).Text.Trim();
            if (end != null && end != string.Empty) {
                if (queryExpression.Length > 0) {
                    queryExpression.Append(" and ");
                }
                queryExpression.Append(" dateadd(day,-1,ImportDate)<= '" + end + "'");
            }

            if (queryExpression.ToString() != string.Empty) {
                queryExpression.Append(" And ImportType = 2");
            } else {
                queryExpression.Append(" ImportType = 2");
            }
            this.FormLogObjectDataSource.SelectParameters["queryExpression"].DefaultValue = queryExpression.ToString();
            this.FormLogGridView.DataBind();
            this.FormLogUpdatePanel.Update();
        }
    }

    public string GetStuffNameByID(object stuffUserID) {
        int id = Convert.ToInt32(stuffUserID);
        StuffUserBLL bll = new StuffUserBLL();
        if (bll.GetStuffUserById(id).Count != 0) {
            return bll.GetStuffUserById(id)[0].StuffName;
        } else {
            return string.Empty;
        }
    }

    protected bool checkSearchConditionValid() {
        string startPeriod = ((TextBox)(this.UCDateInputBeginDate.FindControl("txtDate"))).Text.Trim();
        string endPeriod = ((TextBox)(this.UCDateInputEndDate.FindControl("txtDate"))).Text.Trim();

        if (startPeriod == null || startPeriod == string.Empty) {
            if (endPeriod != null && endPeriod != string.Empty) {
                PageUtility.ShowModelDlg(this, "请选择起始上传时间!");
                return false;
            }
        } else {
            if (endPeriod == null || endPeriod == string.Empty) {
                PageUtility.ShowModelDlg(this, "请选择截止上传时间!");
                return false;
            } else {
                DateTime dtstartPeriod = DateTime.Parse(startPeriod);
                DateTime dtendPeriod = DateTime.Parse(endPeriod);
                if (dtstartPeriod > dtendPeriod) {
                    PageUtility.ShowModelDlg(this, "起始上传时间大于截止上传时间！");
                    return false;
                }
            }
        }
        return true;
    }

    protected void FormLogGridView_SelectedIndexChanged(object sender, EventArgs e) {
        if (this.FormLogGridView.SelectedIndex >= 0) {
            this.FormLogDetailGridView.Visible = true;
            this.FormLogDetailObjectDataSource.SelectParameters["queryExpression"].DefaultValue = "LogID = " + FormLogGridView.SelectedDataKey.Value.ToString();
            this.FormLogDetailGridView.DataBind();
        } else {
            this.FormLogDetailGridView.Visible = false;
        }
        this.FormLogDetailUpdatePanel.Update();
    }

    public string GetUserNameByID(object UserID) {
        int id = Convert.ToInt32(UserID);
        return new StuffUserBLL().GetStuffUserById(id)[0].StuffName;
    }

    public string GetPositionNameByID(object PositionID) {
        int id = Convert.ToInt32(PositionID);
        return new OUTreeBLL().GetPositionById(id).PositionName;
    }
    protected void btnImport_Click(object sender, EventArgs e) {
        if (this.fileUpLoad.Value.Equals("") | this.fileUpLoad.Value == string.Empty) {
            PageUtility.ShowModelDlg(this, "请选择文件!");
            return;
        }
        ReadDataFromClient();
    }

    private void ReadDataFromClient() {
        string filename = this.fileUpLoad.PostedFile.FileName.ToString();
        string excelFileExtension = string.Empty;
        if (filename.IndexOf(".") > 0) {
            excelFileExtension = filename.Substring(filename.LastIndexOf(".") + 1);
            if (excelFileExtension != "xls" && excelFileExtension != "xlsx") {
                PageUtility.ShowModelDlg(this, "请选择Excel文件");
                return;
            } else {
                string tmpFile = filename.Remove(0, filename.LastIndexOf("\\") + 1);
                tmpFile = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + tmpFile;
                string tmpfilename = tmpFile;
                string pathName = CommonUtility.GetPathName();
                string path = Server.MapPath(@"~/" + pathName);
                string fullName = path + @"\" + tmpFile;
                this.fileUpLoad.PostedFile.SaveAs(fullName);
                this.SaveDataToDB(fullName, tmpfilename, excelFileExtension);
            }
        }
    }

    public void SaveDataToDB(string FullPath, string FileName, string excelFileExtension) {
        SqlTransaction transaction = null;
        try {
            DataTable dt = null;
            dt = this.GetDataSet(FullPath, excelFileExtension).Tables[0];
            if (dt.Rows.Count <= 1) {
                PageUtility.ShowModelDlg(this.Page, "文件中没有任何记录，请重新选择");
                return;
            }

            ImportFormLogTableAdapter TAImportFormLog = new ImportFormLogTableAdapter();
            ImportFormLogDetailTableAdapter TAImportFormLogDetail = new ImportFormLogDetailTableAdapter();

            transaction = TableAdapterHelper.BeginTransaction(TAImportFormLog);
            TableAdapterHelper.SetTransaction(TAImportFormLogDetail, transaction);
            //存储FormLog信息
            LogDS.ImportFormLogDataTable FormLogTable = new LogDS.ImportFormLogDataTable();
            LogDS.ImportFormLogRow FormLogRow = FormLogTable.NewImportFormLogRow();

            int stuffUserID = ((AuthorizationDS.StuffUserRow)Session["StuffUser"]).StuffUserId;
            string fullname = this.fileUpLoad.PostedFile.FileName.ToString();
            string tmpFile = fullname.Remove(0, fullname.LastIndexOf("\\") + 1);
            FormLogRow.FileName = tmpFile;
            FormLogRow.ImportDate = DateTime.Now;
            FormLogRow.ImportUserID = stuffUserID;
            FormLogRow.ImportType = 2;
            FormLogRow.TotalCount = dt.Rows.Count - 1;
            FormLogRow.SuccessCount = dt.Rows.Count - 1;
            FormLogRow.FailCount = 0;
            FormLogTable.AddImportFormLogRow(FormLogRow);
            TAImportFormLog.Update(FormLogTable);

            //处理每条明细
            LogDS.ImportFormLogDetailDataTable tbImportFormLogDetail = new LogDS.ImportFormLogDetailDataTable();

            int row_count = dt.Rows.Count;
            string errorInfor = string.Empty;
            //开始处理每条明细
            for (int i = 1; i <= row_count - 1; i++) {
                LogDS.ImportFormLogDetailRow ImportDetailRow = tbImportFormLogDetail.NewImportFormLogDetailRow();
                ImportDetailRow.LogID = FormLogRow.LogID;
                ImportDetailRow.Line = i + 1;
                ImportDetailRow.Error = GenerateFormAndReturnError(i, dt.Rows[i], ImportDetailRow);
                tbImportFormLogDetail.AddImportFormLogDetailRow(ImportDetailRow);
                if (!string.IsNullOrEmpty(ImportDetailRow.Error)) {
                    FormLogRow.FailCount = FormLogRow.FailCount + 1;
                    FormLogRow.SuccessCount = FormLogRow.SuccessCount - 1;
                }
                TAImportFormLog.Update(FormLogRow);
            }

            TAImportFormLogDetail.Update(tbImportFormLogDetail);
            transaction.Commit();
            string returnString = "成功导入" + FormLogRow.SuccessCount.ToString() + "条信息，\r\n" + "导入失败" + FormLogRow.FailCount.ToString() + "条信息";
            PageUtility.ShowModelDlg(this.Page, returnString);
        } catch (Exception ex) {
            if (transaction != null) {
                transaction.Rollback();
            } PageUtility.ShowModelDlg(this.Page, "Save Fail!" + ex.ToString());
        } finally {
            if (transaction != null) {
                transaction.Dispose();
            }
        }
    }


    public string GenerateFormAndReturnError(int i, DataRow row, LogDS.ImportFormLogDetailRow logDetailRow) {
        SqlTransaction transaction = null;
        string errorInfor = string.Empty;
        try {
            FormTableAdapter TAForm = new FormTableAdapter();
            FormSaleSettlementTableAdapter TAFormSettlement = new FormSaleSettlementTableAdapter();
            FormSalePaymentTableAdapter TASalePayment = new FormSalePaymentTableAdapter();
            FormSalePaymentDetailTableAdapter TASalePaymentDetail = new FormSalePaymentDetailTableAdapter();
            FormSettlementExpenseDetailTableAdapter TAFormSettlementExpenseDetail = new FormSettlementExpenseDetailTableAdapter();

            transaction = TableAdapterHelper.BeginTransaction(TAForm);
            TableAdapterHelper.SetTransaction(TASalePayment, transaction);
            TableAdapterHelper.SetTransaction(TASalePaymentDetail, transaction);

            FormDS.FormDataTable tbForm = new FormDS.FormDataTable();
            FormDS.FormSalePaymentDataTable tbPayment = new FormDS.FormSalePaymentDataTable();
            FormDS.FormSalePaymentDetailDataTable tbPaymentDetail = new FormDS.FormSalePaymentDetailDataTable();
            FormDS.FormRow rowForm = null;
            FormDS.FormSalePaymentRow rowPayment = null;
            FormDS.FormSalePaymentDetailRow rowPaymentDetail = null;

            string settlementFormNo = string.Empty;
            DateTime SubmitDate = DateTime.Now;
            decimal PaymentAmount = 0;
            bool IsValid = true;
            if (CheckData(row) != null) {
                errorInfor = "第" + (i + 1) + "行有错：" + CheckData(row);
                IsValid = false;
            } else {
                settlementFormNo = row[0].ToString().Trim();
                SubmitDate = DateTime.Parse(row[1].ToString());
                PaymentAmount = decimal.Parse(row[2].ToString().Trim());

                logDetailRow.SettlementFormNo = settlementFormNo;
                logDetailRow.PaymentAmount = PaymentAmount;

                FormDS.FormDataTable tbSettlement = TAForm.GetDataByFormNo(settlementFormNo);
                if (tbSettlement.Rows.Count <= 0) {
                    errorInfor = "第" + (i + 1) + "行有错：系统中找不到结案单《" + settlementFormNo + "》";
                    IsValid = false;
                }
                FormDS.FormRow settlementForm = tbSettlement[0];
                FormDS.FormSaleSettlementRow rowSettlement = TAFormSettlement.GetDataByID(settlementForm.FormID)[0];

                if (rowSettlement.PaymentTypeID != 2 && rowSettlement.PaymentTypeID != 5) {
                    errorInfor = "第" + (i + 1) + "行有错：该单据支付方式，不是票扣或者调整因子《" + settlementFormNo + "》";
                    IsValid = false;
                }

                if (rowSettlement.IsClose) {
                    errorInfor = "第" + (i + 1) + "行有错：该单据已标记为支付完成《" + settlementFormNo + "》";
                    IsValid = false;
                }

                if (rowSettlement.PaymentTypeID == 2 && PaymentAmount > rowSettlement.AmountRMB) {
                    errorInfor = "第" + (i + 1) + "行有错：支付金额超过结案金额《" + settlementFormNo + "》";
                    IsValid = false;
                }
                if (IsValid) {
                    //生成单据
                    rowForm = tbForm.NewFormRow();
                    rowPayment = tbPayment.NewFormSalePaymentRow();

                    //生成Form
                    rowForm.SetRejectedFormIDNull();
                    //申请人取结案单申请人
                    rowForm.UserID = settlementForm.UserID;
                    UtilityBLL utility = new UtilityBLL();
                    rowForm.FormNo = utility.GetFormNo(utility.GetFormTypeString((int)SystemEnums.FormType.SalePayment));
                    rowForm.SetProxyUserIDNull();
                    rowForm.SetProxyPositionIDNull();
                    //申请人部门取结案单申请人所在部门
                    rowForm.OrganizationUnitID = settlementForm.OrganizationUnitID;
                    rowForm.PositionID = settlementForm.PositionID;
                    rowForm.FormTypeID = (int)SystemEnums.FormType.SalePayment;
                    rowForm.StatusID = (int)SystemEnums.FormStatus.ApproveCompleted;
                    rowForm.SubmitDate = SubmitDate;
                    rowForm.LastModified = SubmitDate;
                    rowForm.InTurnUserIds = "P";//待改动
                    rowForm.InTurnPositionIds = "P";//待改动
                    rowForm.PageType = (int)SystemEnums.PageType.PaymentCash;
                    rowForm.CostCenterID = settlementForm.CostCenterID;
                    rowForm.ApprovedDate = SubmitDate;
                    //是否创建凭证？
                    rowForm.IsCreateVoucher = false;
                    rowForm.IsExportLock = false;
                    rowForm.IsCompletePayment = false;
                    rowForm.IsInvoiceReturned = false;
                    tbForm.AddFormRow(rowForm);
                    TAForm.Update(rowForm);

                    //生成FormPayment
                    rowPayment.FormSalePaymentID = rowForm.FormID;
                    rowPayment.FormSaleSettlementID = settlementForm.FormID;
                    //生成时，不考虑预付款
                    rowPayment.SetFormSaleApplyIDNull();
                    rowPayment.InvoiceStatusID = (int)SystemEnums.InvoiceStatus.No;
                    rowPayment.PaymentTypeID = rowSettlement.PaymentTypeID;
                    //报销申请单备注
                    rowPayment.Remark = "此单据为自动生成单据。";
                    rowPayment.SetAttachedFileNameNull();
                    rowPayment.SetRealAttachedFileNameNull();
                    //报销金额
                    rowPayment.AmountRMB = PaymentAmount;
                    rowPayment.VatTypeID = 1;
                    rowPayment.AmountBeforeTax = rowPayment.AmountRMB;
                    rowPayment.TaxAmount = 0;
                    rowPayment.IsAdvanced = false;
                    //VendorID设置什么值
                    rowPayment.SetVendorIDNull();
                    tbPayment.AddFormSalePaymentRow(rowPayment);
                    TASalePayment.Update(rowPayment);

                    //生成FormPaymentDetail
                    decimal UsedAmount = 0;
                    FormDS.FormSettlementExpenseDetailDataTable tbSettlementExpenseDetail = TAFormSettlementExpenseDetail.GetDataByFormSaleSettlementID(rowSettlement.FormSaleSettlementID);
                    FormDS.FormSettlementExpenseDetailRow rowSettlementExpenseDetail = null;
                    for (int j = 0; j < tbSettlementExpenseDetail.Rows.Count; j++) {
                        rowSettlementExpenseDetail = (FormDS.FormSettlementExpenseDetailRow)tbSettlementExpenseDetail.Rows[j];
                        rowPaymentDetail = tbPaymentDetail.NewFormSalePaymentDetailRow();

                        rowPaymentDetail.FormSalePaymentID = rowPayment.FormSalePaymentID;
                        rowPaymentDetail.FormSaleApplyID = rowSettlementExpenseDetail.FormSaleApplyID;
                        rowPaymentDetail.FormSaleExpenseDetailID = rowSettlementExpenseDetail.FormSaleExpenseDetailID;
                        rowPaymentDetail.ApplyFormNo = rowSettlementExpenseDetail.ApplyFormNo;
                        rowPaymentDetail.ApplyPeriod = rowSettlementExpenseDetail.ApplyPeriod;
                        rowPaymentDetail.ApplyProjectName = rowSettlementExpenseDetail.ApplyProjectName;
                        rowPaymentDetail.ExpenseItemID = rowSettlementExpenseDetail.ExpenseItemID;
                        if (!rowSettlementExpenseDetail.IsShopNameNull()) {
                            rowPaymentDetail.ShopName = rowSettlementExpenseDetail.ShopName;
                        }
                        rowPaymentDetail.SKUID = rowSettlementExpenseDetail.SKUID;
                        rowPaymentDetail.ApplyAmount = rowSettlementExpenseDetail.ApplyAmount;
                        rowPaymentDetail.ApplyAmountRMB = rowSettlementExpenseDetail.ApplyAmountRMB;
                        rowPaymentDetail.SettlementAmount = rowSettlementExpenseDetail.AmountRMB;
                        rowPaymentDetail.TaxAmount = 0;

                        //待改动
                        if (j == tbSettlementExpenseDetail.Rows.Count - 1) {
                            rowPaymentDetail.AmountRMB = PaymentAmount - UsedAmount;
                            rowPaymentDetail.AmountBeforeTax = rowPaymentDetail.AmountRMB;
                            rowPaymentDetail.TaxAmount = rowPaymentDetail.TaxAmount;
                            rowPaymentDetail.PayedAmount = 0;
                            rowPaymentDetail.RemainAmount = 0;
                            UsedAmount += rowPaymentDetail.AmountRMB;
                        } else {
                            rowPaymentDetail.AmountRMB = decimal.Round((rowSettlementExpenseDetail.AmountRMB / rowSettlement.AmountRMB) * PaymentAmount, 2);
                            rowPaymentDetail.AmountBeforeTax = rowPaymentDetail.AmountRMB;
                            rowPaymentDetail.TaxAmount = 0;
                            rowPaymentDetail.PayedAmount = 0;
                            rowPaymentDetail.RemainAmount = 0;
                            UsedAmount += rowPaymentDetail.AmountRMB;
                        }
                        tbPaymentDetail.AddFormSalePaymentDetailRow(rowPaymentDetail);
                        TASalePaymentDetail.Update(tbPaymentDetail);
                    }
                    logDetailRow.PaymentFormNo = rowForm.FormNo;
                }
            }
            transaction.Commit();
        } catch (Exception e) {
            transaction.Rollback();
            errorInfor = e.Message.ToString();
        } finally {
            transaction.Dispose();
        }
        return errorInfor;
    }

    public DataSet GetDataSet(string filepath, string excelFileExtension) {

        try {
            System.Data.OleDb.OleDbConnection oledbcon = null;
            string strConn = string.Empty;
            switch (excelFileExtension.Trim()) {
                case "xls":
                    oledbcon = new System.Data.OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filepath + ";Extended Properties=\"Excel 8.0;HDR=No;IMEX=1;MaxScanRows=0;\"");
                    strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + filepath + ";" + "Extended Properties=Excel 8.0;";
                    break;
                case "xlsx":
                    oledbcon = new System.Data.OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filepath
                   + ";Extended Properties='Excel 12.0;HDR=No;IMEX=1'");
                    strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filepath
                   + ";Extended Properties=Excel 12.0;";
                    break;
            }

            //excel
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();
            DataTable dtSheetName = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "Table" });
            string sheetName = dtSheetName.Rows[0]["TABLE_NAME"].ToString();
            System.Data.OleDb.OleDbDataAdapter oledbAdaptor = new System.Data.OleDb.OleDbDataAdapter("SELECT * FROM [" + sheetName + "]", oledbcon);
            //select 
            DataSet ds = new DataSet();
            oledbAdaptor.Fill(ds);
            oledbcon.Close();
            return ds;
        } catch (Exception ex) {
            throw ex;
        }
    }

    private string CheckData(DataRow row) {
        //先检查普通错误
        string SettlementFormNo = row[0].ToString().Trim();
        string SubmiteDate = row[1].ToString();
        string PaymentAmount = row[2].ToString().Trim();

        if (string.IsNullOrEmpty(SettlementFormNo)) {
            return "结案单号不能为空";
        }

        if (string.IsNullOrEmpty(SubmiteDate)) {
            return "申请日期不能为空";
        } else {
            try {
                DateTime.Parse(SubmiteDate);
            } catch (Exception) {
                return "申请日期不是日期类型！";
            }
        }

        if (string.IsNullOrEmpty(PaymentAmount)) {
            return "付款金额不能为空";
        } else {
            try {
                decimal tPeriodYear = decimal.Parse(PaymentAmount);
            } catch (Exception) {
                return "付款金额不是数字";
            }
        }
        return null;
    }
}