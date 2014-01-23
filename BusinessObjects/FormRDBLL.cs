using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects.FormDSTableAdapters;
using System.Web.Security;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using lib.wf;
using BusinessObjects.MasterDataTableAdapters;
using BusinessObjects.FormDSTableAdapters;

namespace BusinessObjects {
    public class FormRDBLL {

        #region 属性

        private FormDS m_FormDS;
        public FormDS FormDataSet {
            get {
                if (this.m_FormDS == null) {
                    this.m_FormDS = new FormDS();
                }
                return this.m_FormDS;
            }
            set {
                this.m_FormDS = value;
            }
        }

        private FormTableAdapter _TAForm;
        public FormTableAdapter TAForm {
            get {
                if (this._TAForm == null) {
                    this._TAForm = new FormTableAdapter();
                }
                return this._TAForm;
            }
        }

        private FormRDApplyTableAdapter _TAFormRDApply;
        public FormRDApplyTableAdapter TAFormRDApply {
            get {
                if (this._TAFormRDApply == null) {
                    this._TAFormRDApply = new FormRDApplyTableAdapter();
                }
                return this._TAFormRDApply;
            }
        }

        private FormRDApplyDetailTableAdapter _TAFormRDApplyDetail;
        public FormRDApplyDetailTableAdapter TAFormRDApplyDetail {
            get {
                if (this._TAFormRDApplyDetail == null) {
                    this._TAFormRDApplyDetail = new FormRDApplyDetailTableAdapter();
                }
                return this._TAFormRDApplyDetail;
            }
        }

        private FormRDPaymentTableAdapter _TAFormRDPayment;
        public FormRDPaymentTableAdapter TAFormRDPayment {
            get {
                if (this._TAFormRDPayment == null) {
                    this._TAFormRDPayment = new FormRDPaymentTableAdapter();
                }
                return this._TAFormRDPayment;
            }
        }

        private FormRDPaymentDetailTableAdapter _TAFormRDPaymentDetail;
        public FormRDPaymentDetailTableAdapter TAFormRDPaymentDetail {
            get {
                if (this._TAFormRDPaymentDetail == null) {
                    this._TAFormRDPaymentDetail = new FormRDPaymentDetailTableAdapter();
                }
                return this._TAFormRDPaymentDetail;
            }
        }

        private FormInvoiceTableAdapter _TAFormInvoice;
        public FormInvoiceTableAdapter TAFormInvoice {
            get {
                if (this._TAFormInvoice == null) {
                    this._TAFormInvoice = new FormInvoiceTableAdapter();
                }
                return this._TAFormInvoice;
            }
        }

        #endregion

        #region 获取数据

        public FormDS.FormDataTable GetFormByID(int FormID) {
            return this.TAForm.GetDataByID(FormID);
        }

        public FormDS.FormRDApplyDataTable GetFormRDApplyByID(int FormRDApplyID) {
            return this.TAFormRDApply.GetDataByID(FormRDApplyID);
        }

        #endregion

        #region FormRDApply

        public void AddRDApply(int? RejectedFormID, int UserID, int? ProxyUserID, int? ProxyPositionID, int OrganizationUnitID, int PositionID, SystemEnums.FormType FormTypeID, SystemEnums.FormStatus StatusID,
                        DateTime FPeriod, int BrandID, int CustomerChannelID, int CurrencyID, decimal ExchangeRate, string ProjectName, string ProjectDesc,
                        string ApplyFileName, string ApplyRealFileName, DateTime? ActivityBeginDate, DateTime? ActivityEndDate, int? CostCenterID, int ExpenseSubCategoryID,
                        decimal TotalBudget, decimal ApprovedAmount, decimal ApprovingAmount, decimal ReimbursedAmount, decimal RemainBudget) {

            SqlTransaction transaction = null;
            try {
                //事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormRDApply, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormRDApplyDetail, transaction);

                //处理单据的内容
                FormDS.FormRow formRow = this.FormDataSet.Form.NewFormRow();
                if (RejectedFormID != null) {
                    formRow.RejectedFormID = RejectedFormID.GetValueOrDefault();
                }
                formRow.UserID = UserID;

                UtilityBLL utility = new UtilityBLL();
                if (StatusID == SystemEnums.FormStatus.Awaiting) {
                    string formTypeString = utility.GetFormTypeString((int)FormTypeID);
                    formRow.FormNo = utility.GetFormNo(formTypeString);
                } else {
                    formRow.SetFormNoNull();
                }

                if (ProxyUserID != null) {
                    formRow.ProxyUserID = ProxyUserID.GetValueOrDefault();
                }
                if (ProxyPositionID != null) {
                    formRow.ProxyPositionID = ProxyPositionID.GetValueOrDefault();
                }
                formRow.OrganizationUnitID = OrganizationUnitID;
                formRow.PositionID = PositionID;
                formRow.FormTypeID = (int)FormTypeID;
                formRow.StatusID = (int)StatusID;
                formRow.SubmitDate = DateTime.Now;
                formRow.LastModified = DateTime.Now;
                formRow.InTurnUserIds = "P";//待改动
                formRow.InTurnPositionIds = "P";//待改动
                formRow.PageType = (int)SystemEnums.PageType.RDApply;
                if (CostCenterID != null) {
                    formRow.CostCenterID = CostCenterID.GetValueOrDefault();
                }
                this.FormDataSet.Form.AddFormRow(formRow);
                this.TAForm.Update(formRow);

                //处理申请表的内容
                FormDS.FormRDApplyRow formApplyRow = this.FormDataSet.FormRDApply.NewFormRDApplyRow();
                formApplyRow.FormRDApplyID = formRow.FormID;
                formApplyRow.FPeriod = DateTime.Parse(FPeriod.Year.ToString() + "-" + FPeriod.Month.ToString() + "-01");
                formApplyRow.BrandID = BrandID;
                formApplyRow.CustomerChannelID = CustomerChannelID;
                formApplyRow.CurrencyID = CurrencyID;
                formApplyRow.ExchangeRate = ExchangeRate;
                if (!string.IsNullOrEmpty(ProjectName)) {
                    formApplyRow.ProjectName = ProjectName;
                }
                if (ProjectDesc != null)
                    formApplyRow.ProjectDesc = ProjectDesc;
                if (ApplyFileName != null && ApplyFileName != string.Empty) {
                    formApplyRow.ApplyFileName = ApplyFileName;
                }
                if (ApplyRealFileName != null && ApplyRealFileName != string.Empty) {
                    formApplyRow.ApplyRealFileName = ApplyRealFileName;
                }
                if (ActivityBeginDate != null) {
                    formApplyRow.ActivityBeginDate = ActivityBeginDate.GetValueOrDefault();
                }
                if (ActivityEndDate != null) {
                    formApplyRow.ActivityEndDate = ActivityEndDate.GetValueOrDefault();
                }
                formApplyRow.ExpenseSubCategoryID = ExpenseSubCategoryID;
                formApplyRow.TotalBudget = TotalBudget;
                formApplyRow.ApprovedAmount = ApprovedAmount;
                formApplyRow.ApprovingAmount = ApprovingAmount;
                formApplyRow.ReimbursedAmount = ReimbursedAmount;
                formApplyRow.RemainBudget = RemainBudget;
                formApplyRow.AmountRMB = 0;
                formApplyRow.IsClose = false;

                this.FormDataSet.FormRDApply.AddFormRDApplyRow(formApplyRow);
                this.TAFormRDApply.Update(formApplyRow);

                //处理明细
                decimal totalAmountRMB = 0;

                if (RejectedFormID != null) {
                    FormDS.FormRDApplyDetailDataTable newDetailTable = new FormDS.FormRDApplyDetailDataTable();
                    foreach (FormDS.FormRDApplyDetailRow detailRow in this.FormDataSet.FormRDApplyDetail) {
                        if (detailRow.RowState != DataRowState.Deleted) {
                            FormDS.FormRDApplyDetailRow newDetailRow = newDetailTable.NewFormRDApplyDetailRow();
                            newDetailRow.FormRDApplyID = formApplyRow.FormRDApplyID;
                            newDetailRow.VendorID = detailRow.VendorID;
                            newDetailRow.ExpenseItemID = detailRow.ExpenseItemID;
                            newDetailRow.SKUID = detailRow.SKUID;
                            newDetailRow.Amount = detailRow.Amount;
                            newDetailRow.AmountRMB = decimal.Round(detailRow.Amount * ExchangeRate, 2);
                            if (!detailRow.IsRemarkNull()) {
                                newDetailRow.Remark = detailRow.Remark;
                            }
                            newDetailTable.AddFormRDApplyDetailRow(newDetailRow);
                            totalAmountRMB = totalAmountRMB + newDetailRow.AmountRMB;
                        }
                        this.TAFormRDApplyDetail.Update(newDetailTable);
                    }
                } else {
                    foreach (FormDS.FormRDApplyDetailRow detailRow in this.FormDataSet.FormRDApplyDetail) {
                        if (detailRow.RowState != DataRowState.Deleted) {
                            detailRow.FormRDApplyID = formApplyRow.FormRDApplyID;
                            detailRow.AmountRMB = decimal.Round(detailRow.Amount * ExchangeRate, 2);
                            totalAmountRMB = totalAmountRMB + detailRow.AmountRMB;
                        }
                    }
                    this.TAFormRDApplyDetail.Update(this.FormDataSet.FormRDApplyDetail);
                }

                formApplyRow.AmountRMB = totalAmountRMB;
                TAFormRDApply.Update(formApplyRow);
                //作废之前的单据
                if (RejectedFormID != null) {
                    FormDS.FormRow oldRow = this.TAForm.GetDataByID(RejectedFormID.GetValueOrDefault())[0];
                    if (oldRow.StatusID == (int)SystemEnums.FormStatus.Rejected) {
                        oldRow.StatusID = (int)SystemEnums.FormStatus.Scrap;
                        this.TAForm.Update(oldRow);
                    }
                }

                // 正式提交
                if (StatusID == SystemEnums.FormStatus.Awaiting) {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic["TotalAmount"] = totalAmountRMB;
                    APResult result = new APFlowBLL().CreateProcess(new APParameter(formRow.FormID, formRow.FormNo, formRow.OrganizationUnitID, formRow.UserID, (int)StatusID, formRow.FormTypeID, formRow.PositionID, dic));
                    if (result != null) {
                        formRow.InTurnPositionIds = result.InTurnPositionIds;
                        formRow.InTurnUserIds = result.InTurnUserIds;
                        formRow.SubmitDate = result.ApprovedDate.GetValueOrDefault();
                        formRow.StatusID = result.StatusID;
                        formRow.ProcID = result.ProcID;
                        this.TAForm.Update(formRow);
                    }
                }

                transaction.Commit();
            } catch (ApplicationException ex) {
                transaction.Rollback();
                throw ex;
            } catch (Exception ex) {
                transaction.Rollback();
                throw new ApplicationException("Save Fail!" + ex.ToString());
            } finally {
                transaction.Dispose();
            }
        }

        public void UpdateRDApply(int FormID, SystemEnums.FormType FormTypeID, SystemEnums.FormStatus StatusID,
                        decimal ExchangeRate, string ProjectName, string ProjectDesc,
                        string ApplyFileName, string ApplyRealFileName, DateTime? ActivityBeginDate, DateTime? ActivityEndDate, int? CostCenterID,
                        decimal TotalBudget, decimal ApprovedAmount, decimal ApprovingAmount, decimal ReimbursedAmount, decimal RemainBudget) {

            SqlTransaction transaction = null;

            try {
                //事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormRDApply, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormRDApplyDetail, transaction);

                FormDS.FormRow formRow = this.TAForm.GetDataByID(FormID)[0];
                FormDS.FormRDApplyRow formApplyRow = this.TAFormRDApply.GetDataByID(FormID)[0];

                //处理单据的内容
                UtilityBLL utility = new UtilityBLL();
                if (StatusID == SystemEnums.FormStatus.Awaiting) {
                    string formTypeString = utility.GetFormTypeString((int)FormTypeID);
                    formRow.FormNo = utility.GetFormNo(formTypeString);
                    formRow.InTurnUserIds = "P";//待改动
                    formRow.InTurnPositionIds = "P";//待改动
                } else {
                    formRow.SetFormNoNull();
                }
                formRow.StatusID = (int)StatusID;
                formRow.SubmitDate = DateTime.Now;
                formRow.LastModified = DateTime.Now;
                if (CostCenterID != null) {
                    formRow.CostCenterID = CostCenterID.GetValueOrDefault();
                }

                this.TAForm.Update(formRow);

                //处理申请表的内容
                formApplyRow.ExchangeRate = ExchangeRate;
                if (ProjectName != null) {
                    formApplyRow.ProjectName = ProjectName;
                }
                if (ProjectDesc != null) {
                    formApplyRow.ProjectDesc = ProjectDesc;
                }
                if (ApplyFileName != null && ApplyFileName != string.Empty) {
                    formApplyRow.ApplyFileName = ApplyFileName;
                }
                if (ApplyRealFileName != null && ApplyRealFileName != string.Empty) {
                    formApplyRow.ApplyRealFileName = ApplyRealFileName;
                }
                if (ActivityBeginDate != null)
                    formApplyRow.ActivityBeginDate = ActivityBeginDate.GetValueOrDefault();
                if (ActivityEndDate != null)
                    formApplyRow.ActivityEndDate = ActivityEndDate.GetValueOrDefault();

                formApplyRow.TotalBudget = TotalBudget;
                formApplyRow.ApprovedAmount = ApprovedAmount;
                formApplyRow.ApprovingAmount = ApprovingAmount;
                formApplyRow.ReimbursedAmount = ReimbursedAmount;
                formApplyRow.RemainBudget = RemainBudget;

                this.TAFormRDApply.Update(formApplyRow);

                //处理明细
                decimal totalAmountRMB = 0;
                foreach (FormDS.FormRDApplyDetailRow detailRow in this.FormDataSet.FormRDApplyDetail) {
                    if (detailRow.RowState != DataRowState.Deleted) {
                        detailRow.FormRDApplyID = formApplyRow.FormRDApplyID;
                        detailRow.AmountRMB = decimal.Round(detailRow.Amount * ExchangeRate, 2);
                        totalAmountRMB = totalAmountRMB + detailRow.AmountRMB;
                    }
                }
                this.TAFormRDApplyDetail.Update(this.FormDataSet.FormRDApplyDetail);
                formApplyRow.AmountRMB = totalAmountRMB;
                TAFormRDApply.Update(formApplyRow);

                // 正式提交
                if (StatusID == SystemEnums.FormStatus.Awaiting) {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic["TotalAmount"] = totalAmountRMB;
                    APResult result = new APFlowBLL().CreateProcess(new APParameter(formRow.FormID, formRow.FormNo, formRow.OrganizationUnitID, formRow.UserID, (int)StatusID, formRow.FormTypeID, formRow.PositionID, dic));
                    if (result != null) {
                        formRow.InTurnPositionIds = result.InTurnPositionIds;
                        formRow.InTurnUserIds = result.InTurnUserIds;
                        formRow.SubmitDate = result.ApprovedDate.GetValueOrDefault();
                        formRow.StatusID = result.StatusID;
                        formRow.ProcID = result.ProcID;
                        this.TAForm.Update(formRow);
                    }
                }
                transaction.Commit();
            } catch (ApplicationException ex) {
                transaction.Rollback();
                throw ex;
            } catch (Exception ex) {
                transaction.Rollback();
                throw new ApplicationException("Save Fail!" + ex.ToString());
            } finally {
                transaction.Dispose();
            }
        }

        public void DeleteRDApplyByFormID(int FormID) {
            SqlTransaction transaction = null;
            try {
                //事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormRDApply, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormRDApplyDetail, transaction);

                this.TAFormRDApplyDetail.DeleteByApplyID(FormID);
                this.TAFormRDApply.DeleteByID(FormID);
                this.TAForm.DeleteByID(FormID);

                transaction.Commit();
            } catch (Exception ex) {
                transaction.Rollback();
                throw new ApplicationException("Save Fail!" + ex.ToString());
            } finally {
                if (transaction != null) {
                    transaction.Dispose();
                }
            }
        }

        public void CloseRDApplyByFormID(int FormID) {
            FormDS.FormRDApplyRow ApplyRow = this.TAFormRDApply.GetDataByID(FormID)[0];
            ApplyRow.IsClose = true;
            ApplyRow.CloseDate = DateTime.Now;
            this.TAFormRDApply.Update(ApplyRow);
        }

        public string GetPaymentFormNoByRDApplyID(int FormRDApplyID) {
            string PaymentFormNo = (string)this.TAFormRDApply.QueryPaymentNoByApplyID(FormRDApplyID);
            if (PaymentFormNo == null) {
                PaymentFormNo = "";
            }
            return PaymentFormNo;
        }

        public bool IsNeedPOByRDApplyID(int FormRDApplyID) {
            if (this.TAFormRDApply.IfNeedCreatePO(FormRDApplyID).GetValueOrDefault() == 0) {
                return false;
            } else {
                return true;
            }
        }

        #endregion

        #region FormRDApplyDetail

        public FormDS.FormRDApplyDetailDataTable GetFormRDApplyDetail() {
            return this.FormDataSet.FormRDApplyDetail;
        }

        public FormDS.FormRDApplyDetailRow GetFormRDApplyDetailByID(int FormRDApplyDetaiID) {
            return this.TAFormRDApplyDetail.GetDataByID(FormRDApplyDetaiID)[0];
        }

        public FormDS.FormRDApplyDetailDataTable GetFormRDApplyDetailByRDApplyID(int FormRDApplyID) {
            return this.TAFormRDApplyDetail.GetDataByRDApplyID(FormRDApplyID);
        }

        public void AddFormRDApplyDetail(int? FormRDApplyID, int? VendorID, int? SKUID, int ExpenseItemID, decimal Amount, decimal AmountRMB, string Remark) {

            FormDS.FormRDApplyDetailRow rowDetail = this.FormDataSet.FormRDApplyDetail.NewFormRDApplyDetailRow();
            rowDetail.FormRDApplyID = FormRDApplyID.GetValueOrDefault();
            rowDetail.VendorID = VendorID.GetValueOrDefault();
            rowDetail.ExpenseItemID = ExpenseItemID;
            rowDetail.Amount = Amount;
            rowDetail.AmountRMB = AmountRMB;
            rowDetail.Remark = Remark;
            if (SKUID != null) {
                rowDetail.SKUID = SKUID.GetValueOrDefault();
            }
            this.FormDataSet.FormRDApplyDetail.AddFormRDApplyDetailRow(rowDetail);
        }

        public void UpdateFormRDApplyDetail(int FormRDApplyDetailID, int? VendorID, int? SKUID, int ExpenseItemID, decimal Amount, decimal AmountRMB, string Remark) {

            FormDS.FormRDApplyDetailRow rowDetail = this.FormDataSet.FormRDApplyDetail.FindByFormRDApplyDetailID(FormRDApplyDetailID);
            if (rowDetail == null)
                return;
            rowDetail.VendorID = VendorID.GetValueOrDefault();
            rowDetail.ExpenseItemID = ExpenseItemID;
            rowDetail.Amount = Amount;
            rowDetail.AmountRMB = AmountRMB;
            rowDetail.Remark = Remark;
            if (SKUID != null) {
                rowDetail.SKUID = SKUID.GetValueOrDefault();
            }
        }

        public void DeleteFormRDApplyDetailByID(int FormRDApplyDetailID) {
            for (int index = 0; index < this.FormDataSet.FormRDApplyDetail.Count; index++) {
                if ((int)this.FormDataSet.FormRDApplyDetail.Rows[index]["FormRDApplyDetailID"] == FormRDApplyDetailID) {
                    this.FormDataSet.FormRDApplyDetail.Rows[index].Delete();
                    break;
                }
            }
        }

        #endregion

        #region FormRDPayment

        public FormDS.FormRDPaymentRow GetFormRDPaymentByID(int FormRDPaymentID) {
            return this.TAFormRDPayment.GetDataByID(FormRDPaymentID)[0];
        }

        public FormDS.FormRDPaymentDetailDataTable GetFormRDPaymentDetail() {
            return this.FormDataSet.FormRDPaymentDetail;
        }

        public FormDS.FormRDPaymentDetailDataTable GetFormRDPaymentDetailByPaymentID(int FormRDPaymentID) {
            return this.TAFormRDPaymentDetail.GetDataByRDPaymentID(FormRDPaymentID);
        }

        public FormDS.FormRDPaymentDetailRow GetFormRDPaymentDetailByID(int FormRDPaymentDetailID) {
            return this.TAFormRDPaymentDetail.GetDataByID(FormRDPaymentDetailID)[0];
        }

        public decimal GetPaidAmountByFormRDDetailID(int FormRDApplyDetailID) {
            return (decimal)this.TAFormRDPaymentDetail.GetPaiedAmountByRDApplyDetailID(FormRDApplyDetailID);
        }

        public void AddRDPayment(int? RejectedFormID, int UserID, int? ProxyUserID, int? ProxyPositionID, int OrganizationUnitID, int PositionID, SystemEnums.FormType FormTypeID, SystemEnums.FormStatus StatusID,
                        SystemEnums.PageType PageType, int FormRDApplyID, int InvoiceStatusID, int PaymentTypeID, string PaymentFileName, string PaymentRealFileName, string Remark, int CostCenterID, int? VATTypeID, int FormPOID) {

            SqlTransaction transaction = null;
            try {
                //事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormRDPayment, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormRDPaymentDetail, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormInvoice, transaction);

                //处理Form的内容
                FormDS.FormRow formRow = this.FormDataSet.Form.NewFormRow();
                if (RejectedFormID != null) {
                    formRow.RejectedFormID = RejectedFormID.GetValueOrDefault();
                }
                formRow.UserID = UserID;

                UtilityBLL utility = new UtilityBLL();
                if (StatusID == SystemEnums.FormStatus.Awaiting) {
                    string formTypeString = utility.GetFormTypeString((int)FormTypeID);
                    formRow.FormNo = utility.GetFormNo(formTypeString);
                } else {
                    formRow.SetFormNoNull();
                }
                if (ProxyUserID != null) {
                    formRow.ProxyUserID = ProxyUserID.GetValueOrDefault();
                }
                if (ProxyPositionID != null) {
                    formRow.ProxyPositionID = ProxyPositionID.GetValueOrDefault();
                }
                formRow.OrganizationUnitID = OrganizationUnitID;
                formRow.PositionID = PositionID;
                formRow.FormTypeID = (int)FormTypeID;
                formRow.StatusID = (int)StatusID;
                formRow.SubmitDate = DateTime.Now;
                formRow.LastModified = DateTime.Now;
                formRow.InTurnUserIds = "P";//待改动
                formRow.InTurnPositionIds = "P";//待改动
                formRow.PageType = (int)PageType;
                formRow.CostCenterID = CostCenterID;
                formRow.IsCreateVoucher = false;
                formRow.IsExportLock = false;
                formRow.IsCompletePayment = false;
                formRow.IsInvoiceReturned = false;
                this.FormDataSet.Form.AddFormRow(formRow);
                this.TAForm.Update(formRow);

                //处理Payment的内容
                FormDS.FormRDPaymentRow formPaymentRow = this.FormDataSet.FormRDPayment.NewFormRDPaymentRow();
                formPaymentRow.FormRDPaymentID = formRow.FormID;
                formPaymentRow.FormRDApplyID = FormRDApplyID;
                formPaymentRow.InvoiceStatusID = InvoiceStatusID;
                formPaymentRow.PaymentTypeID = PaymentTypeID;
                if (PaymentFileName != null && PaymentFileName != string.Empty) {
                    formPaymentRow.PaymentFileName = PaymentFileName;
                }
                if (PaymentRealFileName != null && PaymentRealFileName != string.Empty) {
                    formPaymentRow.PaymentRealFileName = PaymentRealFileName;
                }
                if (Remark != null && Remark != string.Empty) {
                    formPaymentRow.Remark = Remark;
                }
                formPaymentRow.AmountRMB = 0;
                if (VATTypeID != null) {
                    formPaymentRow.VATTypeID = VATTypeID.GetValueOrDefault();
                }
                if (FormPOID > 0) {
                    formPaymentRow.FormPOID = FormPOID;
                }

                this.FormDataSet.FormRDPayment.AddFormRDPaymentRow(formPaymentRow);
                this.TAFormRDPayment.Update(formPaymentRow);

                //发票
                if (RejectedFormID != null) {
                    FormDS.FormInvoiceDataTable newInvoiceTable = new FormDS.FormInvoiceDataTable();
                    foreach (FormDS.FormInvoiceRow invoiceRow in this.FormDataSet.FormInvoice) {
                        if (invoiceRow.RowState != DataRowState.Deleted) {
                            FormDS.FormInvoiceRow newInvoiceRow = newInvoiceTable.NewFormInvoiceRow();
                            newInvoiceRow.FormID = formPaymentRow.FormRDPaymentID;
                            newInvoiceRow.InvoiceNo = invoiceRow.InvoiceNo;
                            newInvoiceRow.InvoiceDate = invoiceRow.InvoiceDate;
                            newInvoiceRow.InvoiceAmount = invoiceRow.InvoiceAmount;
                            newInvoiceRow.Remark = invoiceRow.IsRemarkNull() ? "" : invoiceRow.Remark;
                            newInvoiceRow.SystemInfo = invoiceRow.IsSystemInfoNull() ? "" : invoiceRow.SystemInfo;
                            newInvoiceTable.AddFormInvoiceRow(newInvoiceRow);
                        }
                        this.TAFormInvoice.Update(newInvoiceTable);
                    }
                } else {
                    foreach (FormDS.FormInvoiceRow invoiceRow in this.FormDataSet.FormInvoice) {
                        // 与父表绑定
                        if (invoiceRow.RowState != DataRowState.Deleted) {
                            invoiceRow.FormID = formPaymentRow.FormRDPaymentID;
                        }
                    }
                }
                this.TAFormInvoice.Update(this.FormDataSet.FormInvoice);

                //处理明细
                //先取得方案申请单
                FormDS.FormRDApplyRow rowRDApply = this.TAFormRDApply.GetDataByID(FormRDApplyID)[0];
                FormDS.FormRow rowApplyForm = this.TAForm.GetDataByID(FormRDApplyID)[0];
                decimal totalAmountRMB = 0;
                decimal totalAmountBeforeTaxtRMB = 0;
                FormDS.FormRDPaymentDetailDataTable newDetailTable = new FormDS.FormRDPaymentDetailDataTable();
                foreach (FormDS.FormRDPaymentDetailRow detailRow in this.FormDataSet.FormRDPaymentDetail) {
                    // 与父表绑定
                    if (detailRow.RowState != DataRowState.Deleted) {
                        totalAmountRMB += detailRow.AmountRMB;
                        totalAmountBeforeTaxtRMB += detailRow.IsAmountBeforeTaxNull() ? 0 : detailRow.AmountBeforeTax;
                        FormDS.FormRDPaymentDetailRow newDetailRow = newDetailTable.NewFormRDPaymentDetailRow();
                        newDetailRow.FormRDPaymentID = formPaymentRow.FormRDPaymentID;
                        newDetailRow.FormRDApplyDetailID = detailRow.FormRDApplyDetailID;
                        newDetailRow.VendorID = detailRow.VendorID;
                        newDetailRow.ExpenseItemID = detailRow.ExpenseItemID;
                        newDetailRow.SKUID = detailRow.SKUID;
                        newDetailRow.PaiedAmount = detailRow.PaiedAmount;
                        newDetailRow.ApplyAmount = detailRow.ApplyAmount;
                        newDetailRow.ApplyAmountRMB = detailRow.ApplyAmountRMB;
                        newDetailRow.RemainAmount = detailRow.RemainAmount;
                        newDetailRow.AmountBeforeTax = detailRow.AmountBeforeTax;
                        newDetailRow.TaxAmount = detailRow.TaxAmount;
                        newDetailRow.AmountRMB = detailRow.AmountRMB;

                        newDetailTable.AddFormRDPaymentDetailRow(newDetailRow);
                    }
                }
                this.TAFormRDPaymentDetail.Update(newDetailTable);

                formPaymentRow.AmountRMB = totalAmountRMB;
                formPaymentRow.AmountBeforeTaxRMB = totalAmountBeforeTaxtRMB;
                this.TAFormRDPayment.Update(formPaymentRow);

                //作废之前的单据
                if (RejectedFormID != null) {
                    FormDS.FormRow oldRow = this.TAForm.GetDataByID(RejectedFormID.GetValueOrDefault())[0];
                    if (oldRow.StatusID == (int)SystemEnums.FormStatus.Rejected) {
                        oldRow.StatusID = (int)SystemEnums.FormStatus.Scrap;
                        this.TAForm.Update(oldRow);
                    }
                }

                // 正式提交
                if (StatusID == SystemEnums.FormStatus.Awaiting) {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic["TotalAmount"] = totalAmountRMB;
                    dic["PaymentTypeID"] = PaymentTypeID;
                    dic["InvoiceStatus"] = formPaymentRow.InvoiceStatusID;
                    APResult result = new APFlowBLL().CreateProcess(new APParameter(formRow.FormID, formRow.FormNo, formRow.OrganizationUnitID, formRow.UserID, (int)StatusID, formRow.FormTypeID, formRow.PositionID, dic));
                    if (result != null) {
                        formRow.InTurnPositionIds = result.InTurnPositionIds;
                        formRow.InTurnUserIds = result.InTurnUserIds;
                        formRow.SubmitDate = result.ApprovedDate.GetValueOrDefault();
                        formRow.StatusID = result.StatusID;
                        formRow.ProcID = result.ProcID;
                        formRow.FinanceRemark = formPaymentRow.Remark;
                        this.TAForm.Update(formRow);
                    }
                }
                transaction.Commit();
            } catch (Exception ex) {
                transaction.Rollback();
                throw new ApplicationException("Save Fail!" + ex.ToString());
            } finally {
                transaction.Dispose();
            }
        }

        public void UpdateRDPayment(int FormID, SystemEnums.FormType FormTypeID, SystemEnums.FormStatus StatusID, int InvoiceStatusID, int PaymentTypeID, string PaymentFileName, string PaymentRealFileName, string Remark, int? VATTypeID, int FormPOID) {

            SqlTransaction transaction = null;
            try {
                //事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormRDPayment, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormRDPaymentDetail, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormInvoice, transaction);

                FormDS.FormRow formRow = this.TAForm.GetDataByID(FormID)[0];
                FormDS.FormRDPaymentRow formPaymentRow = this.TAFormRDPayment.GetDataByID(FormID)[0];

                //处理单据的内容
                UtilityBLL utility = new UtilityBLL();
                if (StatusID == SystemEnums.FormStatus.Awaiting) {
                    string formTypeString = utility.GetFormTypeString((int)FormTypeID);
                    formRow.FormNo = utility.GetFormNo(formTypeString);
                    formRow.InTurnUserIds = "P";//待改动
                    formRow.InTurnPositionIds = "P";//待改动
                } else {
                    formRow.SetFormNoNull();
                }
                formRow.StatusID = (int)StatusID;
                formRow.SubmitDate = DateTime.Now;
                formRow.LastModified = DateTime.Now;

                this.TAForm.Update(formRow);

                //处理Payment的内容
                if (PaymentFileName != null && PaymentFileName != string.Empty) {
                    formPaymentRow.PaymentFileName = PaymentFileName;
                }
                if (PaymentRealFileName != null && PaymentRealFileName != string.Empty) {
                    formPaymentRow.PaymentRealFileName = PaymentRealFileName;
                }
                if (Remark != null && Remark != string.Empty) {
                    formPaymentRow.Remark = Remark;
                }
                formPaymentRow.InvoiceStatusID = InvoiceStatusID;
                formPaymentRow.PaymentTypeID = PaymentTypeID;
                if (VATTypeID != null) {
                    formPaymentRow.VATTypeID = VATTypeID.GetValueOrDefault();
                }
                if (FormPOID > 0) {
                    formPaymentRow.FormPOID = FormPOID;
                }
                this.TAFormRDPayment.Update(formPaymentRow);

                //发票
                foreach (FormDS.FormInvoiceRow invoiceRow in this.FormDataSet.FormInvoice) {
                    // 与父表绑定
                    if (invoiceRow.RowState != DataRowState.Deleted) {
                        invoiceRow.FormID = formPaymentRow.FormRDPaymentID;
                    }
                }
                this.TAFormInvoice.Update(this.FormDataSet.FormInvoice);

                //处理明细
                decimal totalAmountRMB = 0;
                decimal totalAmountBeforeTaxtRMB = 0;
                foreach (FormDS.FormRDPaymentDetailRow detailRow in this.FormDataSet.FormRDPaymentDetail) {
                    if (detailRow.RowState != DataRowState.Deleted) {
                        totalAmountRMB += detailRow.AmountRMB;
                        totalAmountBeforeTaxtRMB += detailRow.IsAmountBeforeTaxNull() ? 0 : detailRow.AmountBeforeTax;
                        detailRow.FormRDPaymentID = formPaymentRow.FormRDPaymentID;
                    }
                }
                this.TAFormRDPaymentDetail.Update(this.FormDataSet.FormRDPaymentDetail);

                formPaymentRow.AmountRMB = totalAmountRMB;
                formPaymentRow.AmountBeforeTaxRMB = totalAmountBeforeTaxtRMB;

                this.TAFormRDPayment.Update(formPaymentRow);
                // 正式提交
                if (StatusID == SystemEnums.FormStatus.Awaiting) {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic["TotalAmount"] = totalAmountRMB;
                    dic["PaymentTypeID"] = PaymentTypeID;
                    dic["InvoiceStatus"] = formPaymentRow.InvoiceStatusID;
                    APResult result = new APFlowBLL().CreateProcess(new APParameter(formRow.FormID, formRow.FormNo, formRow.OrganizationUnitID, formRow.UserID, (int)StatusID, formRow.FormTypeID, formRow.PositionID, dic));
                    if (result != null) {
                        formRow.InTurnPositionIds = result.InTurnPositionIds;
                        formRow.InTurnUserIds = result.InTurnUserIds;
                        formRow.SubmitDate = result.ApprovedDate.GetValueOrDefault();
                        formRow.StatusID = result.StatusID;
                        formRow.ProcID = result.ProcID;
                        formRow.FinanceRemark = formPaymentRow.Remark;
                        this.TAForm.Update(formRow);
                    }
                }
                transaction.Commit();

            } catch (Exception ex) {
                transaction.Rollback();
                throw new ApplicationException("Save Fail!" + ex.ToString());
            } finally {
                transaction.Dispose();
            }
        }

        public void DeleteFormRDPaymentByFormID(int FormID) {
            SqlTransaction transaction = null;
            try {
                //事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormRDPayment, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormInvoice, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormRDPaymentDetail, transaction);

                this.TAFormRDPaymentDetail.DeleteByRDPaymentID(FormID);
                this.TAFormInvoice.DeleteByFormID(FormID);
                this.TAFormRDPayment.DeleteByID(FormID);
                this.TAForm.DeleteByID(FormID);

                transaction.Commit();
            } catch (Exception ex) {
                transaction.Rollback();
                throw new ApplicationException("Save Fail!" + ex.ToString());
            } finally {
                if (transaction != null) {
                    transaction.Dispose();
                }
            }
        }

        #endregion

        #region FormInvoice

        public FormDS.FormInvoiceDataTable GetFormInvoice() {
            return this.FormDataSet.FormInvoice;
        }

        public FormDS.FormInvoiceDataTable GetFormInvoiceByFormID(int FormID) {
            return this.TAFormInvoice.GetDataByFormID(FormID);
        }

        public string GetRepeatedInvoiceFormNosByInvioceNo(string InvoiceNo) {
            string formNos = "";
            this.TAFormInvoice.GetRepeatedInvoiceByInvoiceNo(InvoiceNo, ref formNos);
            return formNos;
        }

        public void UpdateFormInvoice(int FormInvoiceID, string InvoiceNo, DateTime InvoiceDate, decimal InvoiceAmount, string Remark) {

            FormDS.FormInvoiceDataTable table = this.FormDataSet.FormInvoice;
            FormDS.FormInvoiceRow rowDetail = table.FindByFormInvoiceID(FormInvoiceID);
            if (rowDetail == null)
                return;
            rowDetail.InvoiceNo = InvoiceNo;
            rowDetail.InvoiceDate = InvoiceDate;
            rowDetail.InvoiceAmount = InvoiceAmount;
            rowDetail.Remark = Remark;

        }

        public void AddFormInvoice(int? FormID, string InvoiceNo, DateTime InvoiceDate, decimal InvoiceAmount, string Remark) {

            FormDS.FormInvoiceRow rowDetail = this.FormDataSet.FormInvoice.NewFormInvoiceRow();
            rowDetail.FormID = FormID.GetValueOrDefault();
            rowDetail.InvoiceNo = InvoiceNo;
            rowDetail.InvoiceDate = InvoiceDate;
            rowDetail.InvoiceAmount = InvoiceAmount;
            rowDetail.Remark = Remark;
            string systemInfo = this.GetRepeatedInvoiceFormNosByInvioceNo(InvoiceNo);
            if (systemInfo != string.Empty) {
                rowDetail.SystemInfo = "发票号码重复，报销单编号为:" + systemInfo;
            }
            // 填加行并进行更新处理
            this.FormDataSet.FormInvoice.AddFormInvoiceRow(rowDetail);

        }

        public void DeleteFormInvoiceByID(int FormInvoiceID) {
            for (int index = 0; index < this.FormDataSet.FormInvoice.Count; index++) {
                if ((int)this.FormDataSet.FormInvoice.Rows[index]["FormInvoiceID"] == FormInvoiceID) {
                    this.FormDataSet.FormInvoice.Rows[index].Delete();
                    break;
                }
            }
        }

        #endregion
    }
}
