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

namespace BusinessObjects {
    public class FormMarketingBLL {

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

        private FormTableAdapter m_FormAdapter;
        public FormTableAdapter TAForm {
            get {
                if (this.m_FormAdapter == null) {
                    this.m_FormAdapter = new FormTableAdapter();
                }
                return this.m_FormAdapter;
            }
        }

        private FormMarketingApplyTableAdapter _TAFormMarketingApply;
        public FormMarketingApplyTableAdapter TAFormMarketingApply {
            get {
                if (this._TAFormMarketingApply == null) {
                    this._TAFormMarketingApply = new FormMarketingApplyTableAdapter();
                }
                return this._TAFormMarketingApply;
            }
        }

        private FormMarketingApplyDetailTableAdapter _TAFormMarketingApplyDetail;
        public FormMarketingApplyDetailTableAdapter TAFormMarketingApplyDetail {
            get {
                if (this._TAFormMarketingApplyDetail == null) {
                    this._TAFormMarketingApplyDetail = new FormMarketingApplyDetailTableAdapter();
                }
                return this._TAFormMarketingApplyDetail;
            }
        }

        private FormMarketingPaymentTableAdapter _TAFormMarketingPayment;
        public FormMarketingPaymentTableAdapter TAFormMarketingPayment {
            get {
                if (this._TAFormMarketingPayment == null) {
                    this._TAFormMarketingPayment = new FormMarketingPaymentTableAdapter();
                }
                return this._TAFormMarketingPayment;
            }
        }

        private FormMarketingPaymentDetailTableAdapter _TAFormMarketingPaymentDetail;
        public FormMarketingPaymentDetailTableAdapter TAFormMarketingPaymentDetail {
            get {
                if (this._TAFormMarketingPaymentDetail == null) {
                    this._TAFormMarketingPaymentDetail = new FormMarketingPaymentDetailTableAdapter();
                }
                return this._TAFormMarketingPaymentDetail;
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
        public FormDS.FormDataTable GetFormByExport(bool IsExportLock, bool IsCreateVoucher) {
            return this.TAForm.GetDataByExport(IsExportLock, IsCreateVoucher);
        }
        public FormDS.FormMarketingApplyDataTable GetFormMarketingApplyByID(int FormMarketingApplyID) {
            return this.TAFormMarketingApply.GetDataByID(FormMarketingApplyID);
        }

        #endregion

        #region FormMarketingApply

        public void AddMarketingApply(int? RejectedFormID, int UserID, int? ProxyUserID, int? ProxyPositionID, int OrganizationUnitID, int PositionID, SystemEnums.FormType FormTypeID, SystemEnums.FormStatus StatusID,
                        DateTime FPeriod, int BrandID, int CustomerChannelID, int CurrencyID, decimal ExchangeRate, int MarketingProjectID, string ProjectDesc,
                        string ApplyFileName, string ApplyRealFileName, DateTime? ActivityBeginDate, DateTime? ActivityEndDate, int? CostCenterID, int ExpenseCategoryID,
                        decimal TotalBudget, decimal ApprovedAmount, decimal ApprovingAmount, decimal ReimbursedAmount, decimal RemainBudget) {

            SqlTransaction transaction = null;
            try {
                //事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormMarketingApply, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormMarketingApplyDetail, transaction);

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
                formRow.PageType = (int)SystemEnums.PageType.FormMarketingApply;
                if (CostCenterID != null) {
                    formRow.CostCenterID = CostCenterID.GetValueOrDefault();
                }
                this.FormDataSet.Form.AddFormRow(formRow);
                this.TAForm.Update(formRow);

                //处理申请表的内容
                FormDS.FormMarketingApplyRow formApplyRow = this.FormDataSet.FormMarketingApply.NewFormMarketingApplyRow();
                formApplyRow.FormMarketingApplyID = formRow.FormID;
                formApplyRow.FPeriod = DateTime.Parse(FPeriod.Year.ToString() + "-" + FPeriod.Month.ToString() + "-01");
                formApplyRow.BrandID = BrandID;
                formApplyRow.CustomerChannelID = CustomerChannelID;
                formApplyRow.CurrencyID = CurrencyID;
                formApplyRow.ExchangeRate = ExchangeRate;
                if (MarketingProjectID > 0) {
                    formApplyRow.MarketingProjectID = MarketingProjectID;
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
                formApplyRow.ExpenseCategoryID = ExpenseCategoryID;
                formApplyRow.TotalBudget = TotalBudget;
                formApplyRow.ApprovedAmount = ApprovedAmount;
                formApplyRow.ApprovingAmount = ApprovingAmount;
                formApplyRow.ReimbursedAmount = ReimbursedAmount;
                formApplyRow.RemainBudget = RemainBudget;
                formApplyRow.AmountRMB = 0;
                formApplyRow.IsClose = false;

                this.FormDataSet.FormMarketingApply.AddFormMarketingApplyRow(formApplyRow);
                this.TAFormMarketingApply.Update(formApplyRow);

                //处理明细
                decimal totalAmountRMB = 0;

                if (RejectedFormID != null) {
                    FormDS.FormMarketingApplyDetailDataTable newDetailTable = new FormDS.FormMarketingApplyDetailDataTable();
                    foreach (FormDS.FormMarketingApplyDetailRow detailRow in this.FormDataSet.FormMarketingApplyDetail) {
                        if (detailRow.RowState != DataRowState.Deleted) {
                            FormDS.FormMarketingApplyDetailRow newDetailRow = newDetailTable.NewFormMarketingApplyDetailRow();
                            newDetailRow.FormMarketingApplyID = formApplyRow.FormMarketingApplyID;
                            newDetailRow.VendorID = detailRow.VendorID;
                            newDetailRow.ExpenseItemID = detailRow.ExpenseItemID;
                            newDetailRow.SKUID = detailRow.SKUID;
                            newDetailRow.Amount = detailRow.Amount;
                            newDetailRow.AmountRMB = decimal.Round(detailRow.Amount * ExchangeRate, 2);
                            if (!detailRow.IsRemarkNull()) {
                                newDetailRow.Remark = detailRow.Remark;
                            }
                            newDetailTable.AddFormMarketingApplyDetailRow(newDetailRow);
                            totalAmountRMB = totalAmountRMB + newDetailRow.AmountRMB;
                        }
                        this.TAFormMarketingApplyDetail.Update(newDetailTable);
                    }
                } else {
                    foreach (FormDS.FormMarketingApplyDetailRow detailRow in this.FormDataSet.FormMarketingApplyDetail) {
                        if (detailRow.RowState != DataRowState.Deleted) {
                            detailRow.FormMarketingApplyID = formApplyRow.FormMarketingApplyID;
                            detailRow.AmountRMB = decimal.Round(detailRow.Amount * ExchangeRate, 2);
                            totalAmountRMB = totalAmountRMB + detailRow.AmountRMB;
                        }
                    }
                    this.TAFormMarketingApplyDetail.Update(this.FormDataSet.FormMarketingApplyDetail);
                }

                formApplyRow.AmountRMB = totalAmountRMB;
                TAFormMarketingApply.Update(formApplyRow);
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

        public void UpdateMarketingApply(int FormID, SystemEnums.FormType FormTypeID, SystemEnums.FormStatus StatusID,
                        decimal ExchangeRate, int MarketingProjectID, string ProjectDesc,
                        string ApplyFileName, string ApplyRealFileName, DateTime? ActivityBeginDate, DateTime? ActivityEndDate, int? CostCenterID,
                        decimal TotalBudget, decimal ApprovedAmount, decimal ApprovingAmount, decimal ReimbursedAmount, decimal RemainBudget) {

            SqlTransaction transaction = null;

            try {
                //事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormMarketingApply, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormMarketingApplyDetail, transaction);

                FormDS.FormRow formRow = this.TAForm.GetDataByID(FormID)[0];
                FormDS.FormMarketingApplyRow formApplyRow = this.TAFormMarketingApply.GetDataByID(FormID)[0];

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
                formApplyRow.MarketingProjectID = MarketingProjectID;
                if (ProjectDesc != null)
                    formApplyRow.ProjectDesc = ProjectDesc;
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

                this.TAFormMarketingApply.Update(formApplyRow);

                //处理明细
                decimal totalAmountRMB = 0;
                foreach (FormDS.FormMarketingApplyDetailRow detailRow in this.FormDataSet.FormMarketingApplyDetail) {
                    if (detailRow.RowState != DataRowState.Deleted) {
                        detailRow.FormMarketingApplyID = formApplyRow.FormMarketingApplyID;
                        detailRow.AmountRMB = decimal.Round(detailRow.Amount * ExchangeRate, 2);
                        totalAmountRMB = totalAmountRMB + detailRow.AmountRMB;
                    }
                }
                this.TAFormMarketingApplyDetail.Update(this.FormDataSet.FormMarketingApplyDetail);
                formApplyRow.AmountRMB = totalAmountRMB;
                TAFormMarketingApply.Update(formApplyRow);

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

        public void DeleteMarketingApplyByFormID(int FormID) {
            SqlTransaction transaction = null;
            try {
                //事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormMarketingApply, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormMarketingApplyDetail, transaction);

                this.TAFormMarketingApplyDetail.DeleteByMarketingApplyID(FormID);
                this.TAFormMarketingApply.DeleteByID(FormID);
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

        public void CloseMarketingApplyByFormID(int FormID) {
            try {
                FormDS.FormMarketingApplyRow ApplyRow = this.TAFormMarketingApply.GetDataByID(FormID)[0];
                ApplyRow.IsClose = true;
                ApplyRow.CloseDate = DateTime.Now;
                this.TAFormMarketingApply.Update(ApplyRow);
            } catch (Exception) {
                throw new MyException("不能关闭，有相关单据正在处理！", "Can't close, there are related form in processing!");
            }
        }

        public string GetPaymentFormNoByMarketingApplyID(int FormMarketingApplyID) {
            string PaymentFormNo = (string)this.TAFormMarketingApply.GetPaymentFormNoByMarketingApplyID(FormMarketingApplyID);
            if (PaymentFormNo == null) {
                PaymentFormNo = "";
            }
            return PaymentFormNo;
        }

        public bool IsNeedPOByMarketingApplyID(int FormMarketingApplyID) {
            if (this.TAFormMarketingApply.QueryCountForIsNeedPOByApplyID(FormMarketingApplyID).GetValueOrDefault() == 0) {
                return false;
            } else {
                return true;
            }
        }
        #endregion

        #region FormMarketingApplyDetail

        public FormDS.FormMarketingApplyDetailDataTable GetFormMarketingApplyDetail() {
            return this.FormDataSet.FormMarketingApplyDetail;
        }

        public FormDS.FormMarketingApplyDetailRow GetFormMarketingApplyDetailByID(int FormMarketingApplyDetaiID) {
            return this.TAFormMarketingApplyDetail.GetDataByID(FormMarketingApplyDetaiID)[0];
        }

        public FormDS.FormMarketingApplyDetailDataTable GetFormMarketingApplyDetailByMarketingApplyID(int FormMarketingApplyID) {
            return this.TAFormMarketingApplyDetail.GetDataByMarketingApplyID(FormMarketingApplyID);
        }

        public void AddFormMarketingApplyDetail(int? FormMarketingApplyID, int? VendorID, int? SKUID, int ExpenseItemID, decimal Amount, decimal AmountRMB, string Remark) {

            FormDS.FormMarketingApplyDetailRow rowDetail = this.FormDataSet.FormMarketingApplyDetail.NewFormMarketingApplyDetailRow();
            rowDetail.FormMarketingApplyID = FormMarketingApplyID.GetValueOrDefault();
            rowDetail.VendorID = VendorID.GetValueOrDefault();
            rowDetail.ExpenseItemID = ExpenseItemID;
            rowDetail.Amount = Amount;
            rowDetail.AmountRMB = AmountRMB;
            rowDetail.Remark = Remark;
            if (SKUID != null) {
                rowDetail.SKUID = SKUID.GetValueOrDefault();
            }
            this.FormDataSet.FormMarketingApplyDetail.AddFormMarketingApplyDetailRow(rowDetail);
        }

        public void UpdateFormMarketingApplyDetail(int FormMarketingApplyDetailID, int? VendorID, int? SKUID, int ExpenseItemID, decimal Amount, decimal AmountRMB, string Remark) {

            FormDS.FormMarketingApplyDetailRow rowDetail = this.FormDataSet.FormMarketingApplyDetail.FindByFormMarketingApplyDetailID(FormMarketingApplyDetailID);
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

        public void DeleteFormMarketingApplyDetailByID(int FormMarketingApplyDetailID) {
            for (int index = 0; index < this.FormDataSet.FormMarketingApplyDetail.Count; index++) {
                if ((int)this.FormDataSet.FormMarketingApplyDetail.Rows[index]["FormMarketingApplyDetailID"] == FormMarketingApplyDetailID) {
                    this.FormDataSet.FormMarketingApplyDetail.Rows[index].Delete();
                    break;
                }
            }
        }

        #endregion

        #region FormMarketingPayment

        public FormDS.FormMarketingPaymentRow GetFormMarketingPaymentByID(int FormMarketingPaymentID) {
            return this.TAFormMarketingPayment.GetDataByID(FormMarketingPaymentID)[0];
        }

        public FormDS.FormMarketingPaymentDetailDataTable GetFormMarketingPaymentDetail() {
            return this.FormDataSet.FormMarketingPaymentDetail;
        }

        public FormDS.FormMarketingPaymentDetailDataTable GetFormMarketingPaymentDetailByPaymentID(int FormMarketingPaymentID) {
            return this.TAFormMarketingPaymentDetail.GetDataByMarketingPaymentID(FormMarketingPaymentID);
        }

        public FormDS.FormMarketingPaymentDetailRow GetFormMarketingPaymentDetailByID(int FormMarketingPaymentDetailID) {
            return this.TAFormMarketingPaymentDetail.GetDataByID(FormMarketingPaymentDetailID)[0];
        }

        public decimal GetPaidAmountByFormMarketingDetailID(int FormMarketingApplyDetailID) {
            return (decimal)this.TAFormMarketingPaymentDetail.GetPaidAmountByMarketingApplyDetailID(FormMarketingApplyDetailID);
        }

        public void AddMarketingPayment(int? RejectedFormID, int UserID, int? ProxyUserID, int? ProxyPositionID, int OrganizationUnitID, int PositionID, SystemEnums.FormType FormTypeID, SystemEnums.FormStatus StatusID,
                        SystemEnums.PageType PageType, int FormMarketingApplyID, int InvoiceStatusID, int PaymentTypeID, string PaymentFileName, string PaymentRealFileName, string Remark, int CostCenterID, int? VATTypeID) {

            SqlTransaction transaction = null;
            try {
                //事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormMarketingPayment, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormMarketingPaymentDetail, transaction);
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
                FormDS.FormMarketingPaymentRow formPaymentRow = this.FormDataSet.FormMarketingPayment.NewFormMarketingPaymentRow();
                formPaymentRow.FormMarketingPaymentID = formRow.FormID;
                formPaymentRow.FormMarketingApplyID = FormMarketingApplyID;
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

                this.FormDataSet.FormMarketingPayment.AddFormMarketingPaymentRow(formPaymentRow);
                this.TAFormMarketingPayment.Update(formPaymentRow);

                //发票
                if (RejectedFormID != null) {
                    FormDS.FormInvoiceDataTable newInvoiceTable = new FormDS.FormInvoiceDataTable();
                    foreach (FormDS.FormInvoiceRow invoiceRow in this.FormDataSet.FormInvoice) {
                        if (invoiceRow.RowState != DataRowState.Deleted) {
                            FormDS.FormInvoiceRow newInvoiceRow = newInvoiceTable.NewFormInvoiceRow();
                            newInvoiceRow.FormID = formPaymentRow.FormMarketingPaymentID;
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
                            invoiceRow.FormID = formPaymentRow.FormMarketingPaymentID;
                        }
                    }
                }
                this.TAFormInvoice.Update(this.FormDataSet.FormInvoice);

                //处理明细
                //先取得方案申请单
                FormDS.FormMarketingApplyRow rowMarketingApply = this.TAFormMarketingApply.GetDataByID(FormMarketingApplyID)[0];
                FormDS.FormRow rowApplyForm = this.TAForm.GetDataByID(FormMarketingApplyID)[0];
                decimal totalAmountRMB = 0;
                decimal totalAmountBeforeTaxtRMB = 0;
                FormDS.FormMarketingPaymentDetailDataTable newDetailTable = new FormDS.FormMarketingPaymentDetailDataTable();
                foreach (FormDS.FormMarketingPaymentDetailRow detailRow in this.FormDataSet.FormMarketingPaymentDetail) {
                    // 与父表绑定
                    if (detailRow.RowState != DataRowState.Deleted) {
                        totalAmountRMB += detailRow.AmountRMB;
                        totalAmountBeforeTaxtRMB += detailRow.IsAmountBeforeTaxNull() ? 0 : detailRow.AmountBeforeTax;
                        FormDS.FormMarketingPaymentDetailRow newDetailRow = newDetailTable.NewFormMarketingPaymentDetailRow();
                        newDetailRow.FormMarketingPaymentID = formPaymentRow.FormMarketingPaymentID;
                        newDetailRow.FormMarketingApplyDetailID = detailRow.FormMarketingApplyDetailID;
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
                        if (!detailRow.IsPOFormIDNull()) {
                            newDetailRow.POFormID = detailRow.POFormID;
                        }
                        if (!detailRow.IsPOFormNoNull()) {
                            newDetailRow.POFormNo = detailRow.POFormNo;
                        }
                        if (!detailRow.IsPOBPCSNoNull()) {
                            newDetailRow.POBPCSNo = detailRow.POBPCSNo;
                        }
                        newDetailTable.AddFormMarketingPaymentDetailRow(newDetailRow);
                    }
                }
                this.TAFormMarketingPaymentDetail.Update(newDetailTable);

                formPaymentRow.AmountRMB = totalAmountRMB;
                formPaymentRow.AmountBeforeTaxRMB = totalAmountBeforeTaxtRMB;
                this.TAFormMarketingPayment.Update(formPaymentRow);

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

        public void UpdateMarketingPayment(int FormID, SystemEnums.FormType FormTypeID, SystemEnums.FormStatus StatusID, int InvoiceStatusID, int PaymentTypeID, string PaymentFileName, string PaymentRealFileName, string Remark, int? VATTypeID) {

            SqlTransaction transaction = null;
            try {
                //事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormMarketingPayment, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormMarketingPaymentDetail, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormInvoice, transaction);

                FormDS.FormRow formRow = this.TAForm.GetDataByID(FormID)[0];
                FormDS.FormMarketingPaymentRow formPaymentRow = this.TAFormMarketingPayment.GetDataByID(FormID)[0];

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
                this.TAFormMarketingPayment.Update(formPaymentRow);

                //发票
                foreach (FormDS.FormInvoiceRow invoiceRow in this.FormDataSet.FormInvoice) {
                    // 与父表绑定
                    if (invoiceRow.RowState != DataRowState.Deleted) {
                        invoiceRow.FormID = formPaymentRow.FormMarketingPaymentID;
                    }
                }
                this.TAFormInvoice.Update(this.FormDataSet.FormInvoice);

                //处理明细
                decimal totalAmountRMB = 0;
                decimal totalAmountBeforeTaxtRMB = 0;
                foreach (FormDS.FormMarketingPaymentDetailRow detailRow in this.FormDataSet.FormMarketingPaymentDetail) {
                    if (detailRow.RowState != DataRowState.Deleted) {
                        totalAmountRMB += detailRow.AmountRMB;
                        totalAmountBeforeTaxtRMB += detailRow.IsAmountBeforeTaxNull() ? 0 : detailRow.AmountBeforeTax;
                        detailRow.FormMarketingPaymentID = formPaymentRow.FormMarketingPaymentID;
                    }
                }
                this.TAFormMarketingPaymentDetail.Update(this.FormDataSet.FormMarketingPaymentDetail);

                formPaymentRow.AmountRMB = totalAmountRMB;
                formPaymentRow.AmountBeforeTaxRMB = totalAmountBeforeTaxtRMB;

                this.TAFormMarketingPayment.Update(formPaymentRow);
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

        public void DeleteFormMarketingPaymentByFormID(int FormID) {
            SqlTransaction transaction = null;
            try {
                //事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormMarketingPayment, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormInvoice, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormMarketingPaymentDetail, transaction);

                this.TAFormMarketingPaymentDetail.DeleteByMarketingPaymentID(FormID);
                this.TAFormInvoice.DeleteByFormID(FormID);
                this.TAFormMarketingPayment.DeleteByID(FormID);
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

        public string QueryProcessingMarketingPaymentNoByApplyID(int MarketingApplyID) {
            Object result = this.TAFormMarketingPayment.QueryProcessingMarketingPaymentNoByApplyID(MarketingApplyID);
            return result == null ? null : result.ToString();
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
