using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using lib.wf;
using BusinessObjects.MasterDataTableAdapters;
using BusinessObjects.PurchaseDSTableAdapters;

namespace BusinessObjects {
    public class FormPurchaseBLL {

        #region 属性

        private PurchaseDS m_PurchaseDS;
        public PurchaseDS PurchaseDataSet {
            get {
                if (this.m_PurchaseDS == null) {
                    this.m_PurchaseDS = new PurchaseDS();
                }
                return this.m_PurchaseDS;
            }
            set {
                this.m_PurchaseDS = value;
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

        private FormPRTableAdapter m_FormPRAdapter;
        public FormPRTableAdapter TAFormPR {
            get {
                if (this.m_FormPRAdapter == null) {
                    this.m_FormPRAdapter = new FormPRTableAdapter();
                }
                return this.m_FormPRAdapter;
            }
        }

        private FormPRDetailTableAdapter m_FormPRDetailAdapter;
        public FormPRDetailTableAdapter TAFormPRDetail {
            get {
                if (this.m_FormPRDetailAdapter == null) {
                    this.m_FormPRDetailAdapter = new FormPRDetailTableAdapter();
                }
                return this.m_FormPRDetailAdapter;
            }
        }

        private FormPOTableAdapter m_FormPOAdapter;
        public FormPOTableAdapter TAFormPO {
            get {
                if (this.m_FormPOAdapter == null) {
                    this.m_FormPOAdapter = new FormPOTableAdapter();
                }
                return this.m_FormPOAdapter;
            }
        }

        private FormPODetailTableAdapter m_FormPODetailAdapter;
        public FormPODetailTableAdapter TAFormPODetail {
            get {
                if (this.m_FormPODetailAdapter == null) {
                    this.m_FormPODetailAdapter = new FormPODetailTableAdapter();
                }
                return this.m_FormPODetailAdapter;
            }
        }

        private FormPVTableAdapter m_FormPVAdapter;
        public FormPVTableAdapter TAFormPV {
            get {
                if (this.m_FormPVAdapter == null) {
                    this.m_FormPVAdapter = new FormPVTableAdapter();
                }
                return this.m_FormPVAdapter;
            }
        }

        private FormPVDetailTableAdapter m_FormPVDetailAdapter;
        public FormPVDetailTableAdapter TAFormPVDetail {
            get {
                if (this.m_FormPVDetailAdapter == null) {
                    this.m_FormPVDetailAdapter = new FormPVDetailTableAdapter();
                }
                return this.m_FormPVDetailAdapter;
            }
        }

        private FormPRPODetailViewTableAdapter m_FormPRPODetailViewAdapter;
        public FormPRPODetailViewTableAdapter TAFormPRPODetailView {
            get {
                if (this.m_FormPRPODetailViewAdapter == null) {
                    this.m_FormPRPODetailViewAdapter = new FormPRPODetailViewTableAdapter();
                }
                return this.m_FormPRPODetailViewAdapter;
            }
        }

        private FormInvoiceTableAdapter m_FormInvoiceAdapter;
        public FormInvoiceTableAdapter TAFormInvoice {
            get {
                if (this.m_FormInvoiceAdapter == null) {
                    this.m_FormInvoiceAdapter = new FormInvoiceTableAdapter();
                }
                return this.m_FormInvoiceAdapter;
            }
        }

        private FormInvoiceReverseTableAdapter m_FormInvoiceReverseAdapter;
        public FormInvoiceReverseTableAdapter TAFormInvoiceReverse {
            get {
                if (this.m_FormInvoiceReverseAdapter == null) {
                    this.m_FormInvoiceReverseAdapter = new FormInvoiceReverseTableAdapter();
                }
                return this.m_FormInvoiceReverseAdapter;
            }
        }

        #endregion

        public PurchaseDS.FormDataTable GetFormByID(int FormID) {
            return this.TAForm.GetDataByID(FormID);
        }

        #region PRDetail

        public PurchaseDS.FormPRDetailDataTable GetPRDetailByFormPRID(int FormPRID) {
            return this.TAFormPRDetail.GetDataByFormPRID(FormPRID);
        }

        public PurchaseDS.FormPRDetailDataTable GetPRDetail() {
            return this.PurchaseDataSet.FormPRDetail;
        }

        public void AddFormPRDetail(int? FormPRID, int ItemID, decimal FinalPrice, decimal Quantity, decimal ExchangeRate, DateTime DeliveryDate, string DeliveryAddress) {
            PurchaseDS.FormPRDetailRow rowDetail = this.PurchaseDataSet.FormPRDetail.NewFormPRDetailRow();
            rowDetail.FormPRID = FormPRID.GetValueOrDefault();
            MasterData.ItemRow item = new ItemTableAdapter().GetDataByID(ItemID)[0];
            rowDetail.ItemID = ItemID;
            rowDetail.ItemCode = item.ItemCode;
            rowDetail.ItemName = item.ItemName;
            if (!item.IsDescriptionNull()) {
                rowDetail.ItemDescription = item.Description;
            }
            if (!item.IsPackageNull()) {
                rowDetail.Package = item.Package;
            }
            rowDetail.UnitPrice = item.UnitPrice;
            rowDetail.FinalPrice = FinalPrice;
            rowDetail.Quantity = Quantity;
            rowDetail.Amount = decimal.Round(FinalPrice * Quantity, 2);
            rowDetail.AmountRMB = decimal.Round(rowDetail.Amount * ExchangeRate, 2);
            rowDetail.DeliveryDate = DeliveryDate;
            if (DeliveryAddress != null) {
                rowDetail.DeliveryAddress = DeliveryAddress;
            }
            PurchaseDataSet.FormPRDetail.AddFormPRDetailRow(rowDetail);
        }

        public void UpdateFormPRDetail(int FormPRDetailID, int ItemID, decimal FinalPrice, decimal Quantity, decimal ExchangeRate, DateTime DeliveryDate, string DeliveryAddress) {
            PurchaseDS.FormPRDetailRow rowDetail = this.PurchaseDataSet.FormPRDetail.FindByFormPRDetailID(FormPRDetailID);
            MasterData.ItemRow item = new ItemTableAdapter().GetDataByID(ItemID)[0];
            rowDetail.ItemID = ItemID;
            rowDetail.ItemCode = item.ItemCode;
            rowDetail.ItemName = item.ItemName;
            if (!item.IsDescriptionNull()) {
                rowDetail.ItemDescription = item.Description;
            }
            if (!item.IsPackageNull()) {
                rowDetail.Package = item.Package;
            }
            rowDetail.UnitPrice = item.UnitPrice;
            rowDetail.FinalPrice = FinalPrice;
            rowDetail.Quantity = Quantity;
            rowDetail.Amount = decimal.Round(FinalPrice * Quantity, 2);
            rowDetail.AmountRMB = decimal.Round(rowDetail.Amount * ExchangeRate, 2);
            rowDetail.DeliveryDate = DeliveryDate;
            if (DeliveryAddress != null) {
                rowDetail.DeliveryAddress = DeliveryAddress;
            }
        }

        public void DeleteFormPRDetailByID(int FormPRDetailID) {
            for (int index = 0; index < this.PurchaseDataSet.FormPRDetail.Count; index++) {
                if ((int)this.PurchaseDataSet.FormPRDetail.Rows[index]["FormPRDetailID"] == FormPRDetailID) {
                    this.PurchaseDataSet.FormPRDetail.Rows[index].Delete();
                    break;
                }
            }
        }

        #endregion

        #region PR

        public PurchaseDS.FormPRRow GetFormPRByID(int FormPRID) {
            return this.TAFormPR.GetDataByID(FormPRID)[0];
        }

        public void AddPRApply(int? RejectedFormID, int UserID, int? ProxyUserID, int? ProxyPositionID, int OrganizationUnitID, int PositionID, SystemEnums.FormType FormTypeID, SystemEnums.FormStatus StatusID,
                        DateTime FPeriod, int VendorID, int ItemCategoryID, int CurrencyID, decimal ExchangeRate, int PurchaseBudgetTypeID, int PurchaseTypeID, int? CompanyID, int ShippingTermID,
                        string PaymentTerms, string Remark, string AttachedFileName, string RealAttachedFileName, decimal TotalBudget, decimal ApprovedAmount, decimal ApprovingAmount, decimal ReimbursedAmount,
                        decimal NonReimbursedAmount, decimal RemainBudget, string ItemCategoryName, string RealDeliveryAddress) {

            SqlTransaction transaction = null;

            try {
                //事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormPR, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormPRDetail, transaction);

                //处理单据的内容
                PurchaseDS.FormRow formRow = this.PurchaseDataSet.Form.NewFormRow();
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
                formRow.PageType = (int)SystemEnums.PageType.PRApply;
                formRow.CostCenterID = new StuffUserBLL().GetCostCenterIDByPositionID(PositionID);
                this.PurchaseDataSet.Form.AddFormRow(formRow);
                this.TAForm.Update(formRow);

                //处理申请表的内容
                PurchaseDS.FormPRRow formPRRow = this.PurchaseDataSet.FormPR.NewFormPRRow();
                formPRRow.FormPRID = formRow.FormID;
                formPRRow.FPeriod = DateTime.Parse(FPeriod.Year.ToString() + "-" + FPeriod.Month.ToString() + "-01");
                formPRRow.VendorID = VendorID;
                formPRRow.ItemCategoryID = ItemCategoryID;
                formPRRow.CurrencyID = CurrencyID;
                formPRRow.ExchangeRate = ExchangeRate;
                formPRRow.PurchaseBudgetTypeID = PurchaseBudgetTypeID;
                formPRRow.PurchaseTypeID = PurchaseTypeID;
                if (CompanyID != null) {
                    formPRRow.CompanyID = CompanyID.GetValueOrDefault();
                    formPRRow.DeliveryAddress = new MasterDataBLL().GetCompanyById(CompanyID.GetValueOrDefault()).CompanyAddress;
                }
                formPRRow.ShippingTermID = ShippingTermID;
                if (PaymentTerms != null) {
                    formPRRow.PaymentTerms = PaymentTerms;
                }
                if (Remark != null) {
                    formPRRow.Remark = Remark;
                }
                if (AttachedFileName != null && AttachedFileName != string.Empty) {
                    formPRRow.AttachedFileName = AttachedFileName;
                }
                if (RealAttachedFileName != null && RealAttachedFileName != string.Empty) {
                    formPRRow.RealAttachedFileName = RealAttachedFileName;
                }

                formPRRow.TotalBudget = TotalBudget;
                formPRRow.ApprovedAmount = ApprovedAmount;
                formPRRow.ApprovingAmount = ApprovingAmount;
                formPRRow.ReimbursedAmount = ReimbursedAmount;
                formPRRow.NonReimbursedAmount = NonReimbursedAmount;
                formPRRow.RemainBudget = RemainBudget;
                formPRRow.IsClose = false;
                formPRRow.AmountRMB = 0;
                formPRRow.RealDeliveryAddress = RealDeliveryAddress;

                this.PurchaseDataSet.FormPR.AddFormPRRow(formPRRow);
                this.TAFormPR.Update(formPRRow);

                //处理明细
                decimal totalAmountRMB = 0;
                if (RejectedFormID != null) {
                    PurchaseDS.FormPRDetailDataTable newPRDetailTable = new PurchaseDS.FormPRDetailDataTable();
                    foreach (PurchaseDS.FormPRDetailRow detailRow in this.PurchaseDataSet.FormPRDetail) {
                        if (detailRow.RowState != DataRowState.Deleted) {
                            PurchaseDS.FormPRDetailRow newPRDetailRow = newPRDetailTable.NewFormPRDetailRow();
                            newPRDetailRow.FormPRID = formRow.FormID;
                            newPRDetailRow.ItemID = detailRow.ItemID;
                            newPRDetailRow.ItemCode = detailRow.ItemCode;
                            newPRDetailRow.ItemName = detailRow.ItemName;
                            if (!detailRow.IsItemDescriptionNull()) {
                                newPRDetailRow.ItemDescription = detailRow.ItemDescription;
                            }
                            if (!detailRow.IsPackageNull()) {
                                newPRDetailRow.Package = detailRow.Package;
                            }
                            newPRDetailRow.UnitPrice = detailRow.UnitPrice;
                            newPRDetailRow.FinalPrice = detailRow.FinalPrice;
                            newPRDetailRow.Quantity = detailRow.Quantity;
                            newPRDetailRow.Amount = detailRow.Amount;
                            newPRDetailRow.AmountRMB = decimal.Round(detailRow.Amount * ExchangeRate, 2);
                            newPRDetailRow.DeliveryDate = detailRow.DeliveryDate;
                            if (!detailRow.IsDeliveryAddressNull()) {
                                newPRDetailRow.DeliveryAddress = detailRow.DeliveryAddress;
                            }

                            newPRDetailTable.AddFormPRDetailRow(newPRDetailRow);
                            totalAmountRMB = totalAmountRMB + newPRDetailRow.AmountRMB;
                        }
                    }
                    this.TAFormPRDetail.Update(newPRDetailTable);

                } else {
                    foreach (PurchaseDS.FormPRDetailRow detailRow in this.PurchaseDataSet.FormPRDetail) {
                        if (detailRow.RowState != DataRowState.Deleted) {
                            detailRow.FormPRID = formRow.FormID;
                            detailRow.AmountRMB = decimal.Round(detailRow.Amount * ExchangeRate, 2);
                            totalAmountRMB = totalAmountRMB + detailRow.AmountRMB;
                        }
                    }
                    this.TAFormPRDetail.Update(this.PurchaseDataSet.FormPRDetail);
                }

                formPRRow.AmountRMB = totalAmountRMB;
                this.TAFormPR.Update(formPRRow);

                //作废之前的单据
                if (RejectedFormID != null) {
                    PurchaseDS.FormRow oldRow = this.TAForm.GetDataByID(RejectedFormID.GetValueOrDefault())[0];
                    if (oldRow.StatusID == (int)SystemEnums.FormStatus.Rejected) {
                        oldRow.StatusID = (int)SystemEnums.FormStatus.Scrap;
                        this.TAForm.Update(oldRow);
                    }
                }

                // 正式提交
                if (StatusID == SystemEnums.FormStatus.Awaiting) {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic["TTLCost"] = totalAmountRMB;
                    dic["ItemCategory"] = ItemCategoryName;
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

        public void UpdatePRApply(int FormID, SystemEnums.FormStatus StatusID, decimal ExchangeRate, int PurchaseTypeID, int? CompanyID, int ShippingTermID,
                        string PaymentTerms, string Remark, string AttachedFileName, string RealAttachedFileName, decimal TotalBudget, decimal ApprovedAmount, decimal ApprovingAmount,
                        decimal ReimbursedAmount, decimal NonReimbursedAmount, decimal RemainBudget, string ItemCategoryName, string RealDeliveryAddress) {

            SqlTransaction transaction = null;

            try {
                //事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormPR, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormPRDetail, transaction);

                PurchaseDS.FormRow formRow = this.TAForm.GetDataByID(FormID)[0];
                PurchaseDS.FormPRRow formPRRow = this.TAFormPR.GetDataByID(FormID)[0];

                //处理单据的内容
                UtilityBLL utility = new UtilityBLL();
                if (StatusID == SystemEnums.FormStatus.Awaiting) {
                    string formTypeString = utility.GetFormTypeString(formRow.FormTypeID);
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

                //处理申请表的内容
                formPRRow.ExchangeRate = ExchangeRate;
                formPRRow.PurchaseTypeID = PurchaseTypeID;
                if (CompanyID != null) {
                    formPRRow.CompanyID = CompanyID.GetValueOrDefault();
                    formPRRow.DeliveryAddress = new MasterDataBLL().GetCompanyById(CompanyID.GetValueOrDefault()).CompanyAddress;
                }
                formPRRow.ShippingTermID = ShippingTermID;
                if (PaymentTerms != null) {
                    formPRRow.PaymentTerms = PaymentTerms;
                }
                if (Remark != null) {
                    formPRRow.Remark = Remark;
                }
                if (AttachedFileName != null && AttachedFileName != string.Empty) {
                    formPRRow.AttachedFileName = AttachedFileName;
                } else {
                    formPRRow.SetAttachedFileNameNull();
                }
                if (RealAttachedFileName != null && RealAttachedFileName != string.Empty) {
                    formPRRow.RealAttachedFileName = RealAttachedFileName;
                } else {
                    formPRRow.SetRealAttachedFileNameNull();
                }

                formPRRow.TotalBudget = TotalBudget;
                formPRRow.ApprovedAmount = ApprovedAmount;
                formPRRow.ApprovingAmount = ApprovingAmount;
                formPRRow.ReimbursedAmount = ReimbursedAmount;
                formPRRow.NonReimbursedAmount = NonReimbursedAmount;
                formPRRow.RemainBudget = RemainBudget;
                formPRRow.AmountRMB = 0;
                formPRRow.RealDeliveryAddress = RealDeliveryAddress;

                this.TAFormPR.Update(formPRRow);

                //处理明细
                decimal totalAmountRMB = 0;
                foreach (PurchaseDS.FormPRDetailRow detailRow in this.PurchaseDataSet.FormPRDetail) {
                    if (detailRow.RowState != DataRowState.Deleted) {
                        detailRow.FormPRID = formRow.FormID;
                        detailRow.AmountRMB = decimal.Round(detailRow.Amount * ExchangeRate, 2);
                        totalAmountRMB = totalAmountRMB + detailRow.AmountRMB;
                    }
                }
                this.TAFormPRDetail.Update(this.PurchaseDataSet.FormPRDetail);

                formPRRow.AmountRMB = totalAmountRMB;
                this.TAFormPR.Update(formPRRow);

                // 正式提交
                if (StatusID == SystemEnums.FormStatus.Awaiting) {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic["TTLCost"] = totalAmountRMB;
                    dic["ItemCategory"] = ItemCategoryName;
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

        public void DeleteFormPRByFormID(int FormID) {
            SqlTransaction transaction = null;
            try {
                //事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAFormPRDetail);
                TableAdapterHelper.SetTransaction(this.TAFormPR, transaction);
                TableAdapterHelper.SetTransaction(this.TAForm, transaction);

                this.TAFormPRDetail.DeleteByFormPRID(FormID);
                this.TAFormPR.DeleteByID(FormID);
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

        public decimal GetReimbursedPVAmountByPRID(int FormPRID) {
            return this.TAFormPR.GetReimbursedPVAmountByPRID(FormPRID).GetValueOrDefault();
        }

        public string GetProcessingPVNoByFormPRID(int FormPRID) {
            Object ProcessingPVNo = this.TAFormPR.GetProcessingPVNoByFormPRID(FormPRID);
            return ProcessingPVNo == null ? null : ProcessingPVNo.ToString();
        }

        public string GetProcessingPONoByFormPRID(int FormPRID) {
            Object ProcessingPONo = this.TAFormPR.GetProcessingPONoByFormPRID(FormPRID);
            return ProcessingPONo == null ? null : ProcessingPONo.ToString();
        }

        public string GetPONoByPRID(int PRID) {
            string POFormNo = (string)this.TAFormPR.GetPONoByPRID(PRID);
            if (POFormNo == null) {
                POFormNo = "";
            }
            return POFormNo;
        }

        public string GetPVNoByPRID(int PRID) {
            string PVFormNo = (string)this.TAFormPR.GetPVNoByPRID(PRID);
            if (PVFormNo == null) {
                PVFormNo = "";
            }
            return PVFormNo;
        }


        public void CloseFormPR(int FormPRID) {
            SqlTransaction transaction = null;
            try {
                //事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAFormPR);
                PurchaseDS.FormPRRow formPRRow = this.TAFormPR.GetDataByID(FormPRID)[0];
                formPRRow.IsClose = true;
                this.TAFormPR.Update(formPRRow);
                transaction.Commit();
            } catch (Exception ex) {
                transaction.Rollback();
                throw new ApplicationException("Save Fail!" + ex.ToString());
            } finally {
                transaction.Dispose();
            }
        }

        #endregion

        #region PODetail

        public PurchaseDS.FormPODetailDataTable GetPODetailByFormPOID(int FormPOID) {
            return this.TAFormPODetail.GetDataByFormPOID(FormPOID);
        }

        public PurchaseDS.FormPODetailDataTable GetPODetail() {
            return this.PurchaseDataSet.FormPODetail;
        }

        public void AddFormPODetail(int? FormPOID, int ItemID, decimal FinalPrice, decimal Quantity, decimal ExchangeRate, DateTime DeliveryDate, string DeliveryAddress) {
            PurchaseDS.FormPODetailRow rowDetail = this.PurchaseDataSet.FormPODetail.NewFormPODetailRow();
            rowDetail.FormPOID = FormPOID.GetValueOrDefault();
            MasterData.ItemRow item = new ItemTableAdapter().GetDataByID(ItemID)[0];
            rowDetail.ItemID = ItemID;
            rowDetail.ItemCode = item.ItemCode;
            rowDetail.ItemName = item.ItemName;
            if (!item.IsDescriptionNull()) {
                rowDetail.ItemDescription = item.Description;
            }
            if (!item.IsPackageNull()) {
                rowDetail.Package = item.Package;
            }
            rowDetail.UnitPrice = item.UnitPrice;
            rowDetail.FinalPrice = FinalPrice;
            rowDetail.Quantity = Quantity;
            rowDetail.Amount = decimal.Round(FinalPrice * Quantity, 2);
            rowDetail.AmountRMB = decimal.Round(rowDetail.Amount * ExchangeRate, 2);
            rowDetail.DeliveryDate = DeliveryDate;
            if (DeliveryAddress != null) {
                rowDetail.DeliveryAddress = DeliveryAddress;
            }
            PurchaseDataSet.FormPODetail.AddFormPODetailRow(rowDetail);
        }

        public void UpdateFormPODetail(int FormPODetailID, int ItemID, decimal FinalPrice, decimal Quantity, decimal ExchangeRate, DateTime DeliveryDate, string DeliveryAddress) {
            PurchaseDS.FormPODetailRow rowDetail = this.PurchaseDataSet.FormPODetail.FindByFormPODetailID(FormPODetailID);
            MasterData.ItemRow item = new ItemTableAdapter().GetDataByID(ItemID)[0];
            rowDetail.ItemID = ItemID;
            rowDetail.ItemCode = item.ItemCode;
            rowDetail.ItemName = item.ItemName;
            if (!item.IsDescriptionNull()) {
                rowDetail.ItemDescription = item.Description;
            }
            if (!item.IsPackageNull()) {
                rowDetail.Package = item.Package;
            }
            rowDetail.UnitPrice = item.UnitPrice;
            rowDetail.FinalPrice = FinalPrice;
            rowDetail.Quantity = Quantity;
            rowDetail.Amount = decimal.Round(FinalPrice * Quantity, 2);
            rowDetail.AmountRMB = decimal.Round(rowDetail.Amount * ExchangeRate, 2);
            rowDetail.DeliveryDate = DeliveryDate;
            if (DeliveryAddress != null) {
                rowDetail.DeliveryAddress = DeliveryAddress;
            }
        }

        public void DeleteFormPODetailByID(int FormPODetailID) {
            for (int index = 0; index < this.PurchaseDataSet.FormPODetail.Count; index++) {
                if ((int)this.PurchaseDataSet.FormPODetail.Rows[index]["FormPODetailID"] == FormPODetailID) {
                    this.PurchaseDataSet.FormPODetail.Rows[index].Delete();
                    break;
                }
            }
        }

        #endregion

        #region PO

        public PurchaseDS.FormPORow GetFormPOByID(int FormPOID) {
            return this.TAFormPO.GetDataByID(FormPOID)[0];
        }

        public PurchaseDS.FormPODataTable GetFormPOByParentFormID(int ParentFormID) {
            return this.TAFormPO.GetDataByParentFormID(ParentFormID);
        }

        public void AddPOApply(int? RejectedFormID, int UserID, int? ProxyUserID, int? ProxyPositionID, int OrganizationUnitID, int PositionID, SystemEnums.FormType FormTypeID, SystemEnums.FormStatus StatusID,
                        int ParentFormID, int CompanyID, int ShippingTermID, string PaymentTerms, string Remark, string AttachedFileName, string RealAttachedFileName, int POType, bool IsChanged, string ItemCategoryName, string RealDeliveryAddress) {

            SqlTransaction transaction = null;

            try {
                //事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormPO, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormPODetail, transaction);

                //处理单据的内容
                PurchaseDS.FormRow formRow = this.PurchaseDataSet.Form.NewFormRow();
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
                formRow.PageType = (int)SystemEnums.PageType.POApply;
                formRow.CostCenterID = new StuffUserBLL().GetCostCenterIDByPositionID(PositionID);
                formRow.IsCreateVoucher = false;
                formRow.IsExportLock = false;
                formRow.IsCompletePayment = false;

                this.PurchaseDataSet.Form.AddFormRow(formRow);
                this.TAForm.Update(formRow);

                //处理申请表的内容
                PurchaseDS.FormPORow formPORow = this.PurchaseDataSet.FormPO.NewFormPORow();
                PurchaseDS.FormPRRow formPRRow = this.TAFormPR.GetDataByID(ParentFormID)[0];
                formPORow.FormPOID = formRow.FormID;
                formPORow.ParentFormID = ParentFormID;
                formPORow.ParentFormNo = this.TAForm.GetDataByID(ParentFormID)[0].FormNo;
                formPORow.ApplyAmountRMB = formPRRow.AmountRMB;
                formPORow.FPeriod = formPRRow.FPeriod;
                formPORow.VendorID = formPRRow.VendorID;
                formPORow.ItemCategoryID = formPRRow.ItemCategoryID;
                formPORow.CurrencyID = formPRRow.CurrencyID;
                formPORow.ExchangeRate = formPRRow.ExchangeRate;
                formPORow.PurchaseBudgetTypeID = formPRRow.PurchaseBudgetTypeID;
                formPORow.PurchaseTypeID = formPRRow.PurchaseTypeID;
                formPORow.CompanyID = CompanyID;
                formPORow.DeliveryAddress = new MasterDataBLL().GetCompanyById(CompanyID).CompanyAddress;
                formPORow.ShippingTermID = ShippingTermID;
                if (PaymentTerms != null) {
                    formPORow.PaymentTerms = PaymentTerms;
                }
                if (Remark != null) {
                    formPORow.Remark = Remark;
                }
                if (AttachedFileName != null && AttachedFileName != string.Empty) {
                    formPORow.AttachedFileName = AttachedFileName;
                }
                if (RealAttachedFileName != null && RealAttachedFileName != string.Empty) {
                    formPORow.RealAttachedFileName = RealAttachedFileName;
                }

                formPORow.POType = POType;
                formPORow.IsChanged = IsChanged;
                formPORow.AmountRMB = 0;
                formPORow.RealDeliveryAddress = RealDeliveryAddress;

                this.PurchaseDataSet.FormPO.AddFormPORow(formPORow);
                this.TAFormPO.Update(formPORow);

                //处理明细
                decimal totalAmountRMB = 0;
                PurchaseDS.FormPODetailDataTable newPODetailTable = new PurchaseDS.FormPODetailDataTable();
                foreach (PurchaseDS.FormPODetailRow detailRow in this.PurchaseDataSet.FormPODetail) {
                    if (detailRow.RowState != DataRowState.Deleted) {
                        PurchaseDS.FormPODetailRow newPODetailRow = newPODetailTable.NewFormPODetailRow();
                        newPODetailRow.FormPOID = formRow.FormID;
                        newPODetailRow.ItemID = detailRow.ItemID;
                        newPODetailRow.ItemCode = detailRow.ItemCode;
                        newPODetailRow.ItemName = detailRow.ItemName;
                        if (!detailRow.IsItemDescriptionNull()) {
                            newPODetailRow.ItemDescription = detailRow.ItemDescription;
                        }
                        if (!detailRow.IsPackageNull()) {
                            newPODetailRow.Package = detailRow.Package;
                        }
                        newPODetailRow.UnitPrice = detailRow.UnitPrice;
                        newPODetailRow.FinalPrice = detailRow.FinalPrice;
                        newPODetailRow.Quantity = detailRow.Quantity;
                        newPODetailRow.Amount = detailRow.Amount;
                        newPODetailRow.AmountRMB = detailRow.AmountRMB;
                        newPODetailRow.DeliveryDate = detailRow.DeliveryDate;
                        if (!detailRow.IsDeliveryAddressNull()) {
                            newPODetailRow.DeliveryAddress = detailRow.DeliveryAddress;
                        }

                        newPODetailTable.AddFormPODetailRow(newPODetailRow);
                        totalAmountRMB = totalAmountRMB + newPODetailRow.AmountRMB;
                    }
                }
                this.TAFormPODetail.Update(newPODetailTable);

                //if (RejectedFormID != null) {
                //    PurchaseDS.FormPODetailDataTable newPODetailTable = new PurchaseDS.FormPODetailDataTable();
                //    foreach (PurchaseDS.FormPODetailRow detailRow in this.PurchaseDataSet.FormPODetail) {
                //        if (detailRow.RowState != DataRowState.Deleted) {
                //            PurchaseDS.FormPODetailRow newPODetailRow = newPODetailTable.NewFormPODetailRow();
                //            newPODetailRow.FormPOID = formRow.FormID;
                //            newPODetailRow.ItemID = detailRow.ItemID;
                //            newPODetailRow.ItemCode = detailRow.ItemCode;
                //            newPODetailRow.ItemName = detailRow.ItemName;
                //            if (!detailRow.IsItemDescriptionNull()) {
                //                newPODetailRow.ItemDescription = detailRow.ItemDescription;
                //            }
                //            if (!detailRow.IsPackageNull()) {
                //                newPODetailRow.Package = detailRow.Package;
                //            }
                //            newPODetailRow.UnitPrice = detailRow.UnitPrice;
                //            newPODetailRow.FinalPrice = detailRow.FinalPrice;
                //            newPODetailRow.Quantity = detailRow.Quantity;
                //            newPODetailRow.Amount = detailRow.Amount;
                //            newPODetailRow.AmountRMB = detailRow.AmountRMB;
                //            newPODetailRow.DeliveryDate = detailRow.DeliveryDate;
                //            if (!detailRow.IsDeliveryAddressNull()) {
                //                newPODetailRow.DeliveryAddress = detailRow.DeliveryAddress;
                //            }

                //            newPODetailTable.AddFormPODetailRow(newPODetailRow);
                //            totalAmountRMB = totalAmountRMB + newPODetailRow.AmountRMB;
                //        }
                //    }
                //    this.TAFormPODetail.Update(newPODetailTable);

                //} else {
                //    foreach (PurchaseDS.FormPODetailRow detailRow in this.PurchaseDataSet.FormPODetail) {
                //        if (detailRow.RowState != DataRowState.Deleted) {
                //            detailRow.FormPOID = formRow.FormID;                            
                //            totalAmountRMB = totalAmountRMB + detailRow.AmountRMB;
                //        }
                //    }
                //    this.TAFormPODetail.Update(this.PurchaseDataSet.FormPODetail);
                //}

                formPORow.AmountRMB = totalAmountRMB;
                this.TAFormPO.Update(formPORow);

                //作废之前的单据
                if (RejectedFormID != null) {
                    PurchaseDS.FormRow oldRow = this.TAForm.GetDataByID(RejectedFormID.GetValueOrDefault())[0];
                    if (oldRow.StatusID == (int)SystemEnums.FormStatus.Rejected) {
                        oldRow.StatusID = (int)SystemEnums.FormStatus.Scrap;
                        this.TAForm.Update(oldRow);
                    }
                }

                // 正式提交
                if (StatusID == SystemEnums.FormStatus.Awaiting) {
                    if (!IsChanged) {//如果没有改变的话应该自动审批通过
                        formRow.StatusID = 2;
                        formRow.ApprovedDate = DateTime.Now;
                    } else {
                        Dictionary<string, object> dic = new Dictionary<string, object>();
                        dic["TTLCost"] = totalAmountRMB;
                        dic["ItemCategory"] = ItemCategoryName;
                        dic["POType"] = POType;

                        APResult result = new APFlowBLL().CreateProcess(new APParameter(formRow.FormID, formRow.FormNo, formRow.OrganizationUnitID, formRow.UserID, (int)StatusID, formRow.FormTypeID, formRow.PositionID, dic));
                        if (result != null) {
                            formRow.InTurnPositionIds = result.InTurnPositionIds;
                            formRow.InTurnUserIds = result.InTurnUserIds;
                            formRow.SubmitDate = result.ApprovedDate.GetValueOrDefault();
                            formRow.StatusID = result.StatusID;
                            formRow.ProcID = result.ProcID;
                        }
                    }
                    this.TAForm.Update(formRow);
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

        public void UpdatePOApply(int FormID, SystemEnums.FormStatus StatusID, int CompanyID, int ShippingTermID, string PaymentTerms, string Remark, string AttachedFileName,
                    string RealAttachedFileName, bool IsChanged, string ItemCategoryName, string RealDeliveryAddress) {

            SqlTransaction transaction = null;

            try {
                //事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormPO, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormPODetail, transaction);

                PurchaseDS.FormRow formRow = this.TAForm.GetDataByID(FormID)[0];
                PurchaseDS.FormPORow formPORow = this.TAFormPO.GetDataByID(FormID)[0];

                //处理单据的内容
                UtilityBLL utility = new UtilityBLL();
                if (StatusID == SystemEnums.FormStatus.Awaiting) {
                    string formTypeString = utility.GetFormTypeString(formRow.FormTypeID);
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

                //处理申请表的内容
                formPORow.CompanyID = CompanyID;
                formPORow.DeliveryAddress = new MasterDataBLL().GetCompanyById(CompanyID).CompanyAddress;
                formPORow.ShippingTermID = ShippingTermID;
                if (PaymentTerms != null) {
                    formPORow.PaymentTerms = PaymentTerms;
                }
                if (Remark != null) {
                    formPORow.Remark = Remark;
                }
                if (AttachedFileName != null && AttachedFileName != string.Empty) {
                    formPORow.AttachedFileName = AttachedFileName;
                } else {
                    formPORow.SetAttachedFileNameNull();
                }
                if (RealAttachedFileName != null && RealAttachedFileName != string.Empty) {
                    formPORow.RealAttachedFileName = RealAttachedFileName;
                } else {
                    formPORow.SetRealAttachedFileNameNull();
                }

                formPORow.IsChanged = IsChanged;
                formPORow.AmountRMB = 0;
                formPORow.RealDeliveryAddress = RealDeliveryAddress;

                this.TAFormPO.Update(formPORow);

                //处理明细
                decimal totalAmountRMB = 0;
                foreach (PurchaseDS.FormPODetailRow detailRow in this.PurchaseDataSet.FormPODetail) {
                    if (detailRow.RowState != DataRowState.Deleted) {
                        detailRow.FormPOID = formRow.FormID;
                        totalAmountRMB = totalAmountRMB + detailRow.AmountRMB;
                    }
                }
                this.TAFormPODetail.Update(this.PurchaseDataSet.FormPODetail);

                formPORow.AmountRMB = totalAmountRMB;
                this.TAFormPO.Update(formPORow);

                // 正式提交
                if (StatusID == SystemEnums.FormStatus.Awaiting) {
                    if (!IsChanged) {//如果没有改变的话应该自动审批通过
                        formRow.StatusID = 2;
                        formRow.ApprovedDate = DateTime.Now;
                    } else {
                        Dictionary<string, object> dic = new Dictionary<string, object>();
                        dic["TTLCost"] = totalAmountRMB;
                        dic["ItemCategory"] = ItemCategoryName;
                        dic["POType"] = formPORow.POType;

                        APResult result = new APFlowBLL().CreateProcess(new APParameter(formRow.FormID, formRow.FormNo, formRow.OrganizationUnitID, formRow.UserID, (int)StatusID, formRow.FormTypeID, formRow.PositionID, dic));
                        if (result != null) {
                            formRow.InTurnPositionIds = result.InTurnPositionIds;
                            formRow.InTurnUserIds = result.InTurnUserIds;
                            formRow.SubmitDate = result.ApprovedDate.GetValueOrDefault();
                            formRow.StatusID = result.StatusID;
                            formRow.ProcID = result.ProcID;
                        }
                    }
                    this.TAForm.Update(formRow);
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

        public void DeleteFormPOByFormID(int FormID) {
            SqlTransaction transaction = null;
            try {
                //事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAFormPODetail);
                TableAdapterHelper.SetTransaction(this.TAFormPO, transaction);
                TableAdapterHelper.SetTransaction(this.TAForm, transaction);

                this.TAFormPODetail.DeleteByFormPOID(FormID);
                this.TAFormPO.DeleteByID(FormID);
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

        public decimal GetReimbursedPVAmountByPOID(int FormPOID) {
            return this.TAFormPO.GetReimbursedPVAmountByPOID(FormPOID).GetValueOrDefault();
        }

        public void AddPOSpecialApply(int? RejectedFormID, int UserID, int? ProxyUserID, int? ProxyPositionID, int OrganizationUnitID, int PositionID, SystemEnums.FormType FormTypeID, SystemEnums.FormStatus StatusID,
                        int ParentFormID, string ParentFormNo, decimal ApplyAmountRMB, DateTime FPeriod, int VendorID, int ItemCategoryID, int CurrencyID, decimal ExchangeRate, int PurchaseBudgetTypeID, int PurchaseTypeID,
                        int? CompanyID, int ShippingTermID, string PaymentTerms, string Remark, string AttachedFileName, string RealAttachedFileName, int POType, bool IsChanged, string RealDeliveryAddress) {

            SqlTransaction transaction = null;

            try {
                //事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormPO, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormPODetail, transaction);

                //处理单据的内容
                PurchaseDS.FormRow formRow = this.PurchaseDataSet.Form.NewFormRow();
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
                formRow.PageType = (int)SystemEnums.PageType.SpecialPOApply;
                formRow.CostCenterID = new StuffUserBLL().GetCostCenterIDByPositionID(PositionID);
                formRow.IsCreateVoucher = false;
                formRow.IsExportLock = false;
                formRow.IsCompletePayment = false;

                this.PurchaseDataSet.Form.AddFormRow(formRow);
                this.TAForm.Update(formRow);

                //处理申请表的内容
                PurchaseDS.FormPORow formPORow = this.PurchaseDataSet.FormPO.NewFormPORow();
                formPORow.FormPOID = formRow.FormID;
                formPORow.ParentFormID = ParentFormID;
                formPORow.ParentFormNo = ParentFormNo;
                formPORow.ApplyAmountRMB = ApplyAmountRMB;
                formPORow.FPeriod = FPeriod;
                formPORow.VendorID = VendorID;
                formPORow.ItemCategoryID = ItemCategoryID;
                formPORow.CurrencyID = CurrencyID;
                formPORow.ExchangeRate = ExchangeRate;
                formPORow.PurchaseBudgetTypeID = PurchaseBudgetTypeID;
                formPORow.PurchaseTypeID = PurchaseTypeID;
                if (CompanyID != null) {
                    formPORow.CompanyID = CompanyID.GetValueOrDefault();
                    formPORow.DeliveryAddress = new MasterDataBLL().GetCompanyById(CompanyID.GetValueOrDefault()).CompanyAddress;
                }
                formPORow.ShippingTermID = ShippingTermID;
                if (PaymentTerms != null) {
                    formPORow.PaymentTerms = PaymentTerms;
                }
                if (Remark != null) {
                    formPORow.Remark = Remark;
                }
                if (AttachedFileName != null && AttachedFileName != string.Empty) {
                    formPORow.AttachedFileName = AttachedFileName;
                }
                if (RealAttachedFileName != null && RealAttachedFileName != string.Empty) {
                    formPORow.RealAttachedFileName = RealAttachedFileName;
                }
                formPORow.RealDeliveryAddress = RealDeliveryAddress;
                formPORow.POType = POType;
                formPORow.IsChanged = IsChanged;
                formPORow.AmountRMB = 0;

                this.PurchaseDataSet.FormPO.AddFormPORow(formPORow);
                this.TAFormPO.Update(formPORow);

                //处理明细
                decimal totalAmountRMB = 0;
                if (RejectedFormID != null) {
                    PurchaseDS.FormPODetailDataTable newPODetailTable = new PurchaseDS.FormPODetailDataTable();
                    foreach (PurchaseDS.FormPODetailRow detailRow in this.PurchaseDataSet.FormPODetail) {
                        if (detailRow.RowState != DataRowState.Deleted) {
                            PurchaseDS.FormPODetailRow newPODetailRow = newPODetailTable.NewFormPODetailRow();
                            newPODetailRow.FormPOID = formRow.FormID;
                            newPODetailRow.ItemID = detailRow.ItemID;
                            newPODetailRow.ItemCode = detailRow.ItemCode;
                            newPODetailRow.ItemName = detailRow.ItemName;
                            if (!detailRow.IsItemDescriptionNull()) {
                                newPODetailRow.ItemDescription = detailRow.ItemDescription;
                            }
                            if (!detailRow.IsPackageNull()) {
                                newPODetailRow.Package = detailRow.Package;
                            }
                            newPODetailRow.UnitPrice = detailRow.UnitPrice;
                            newPODetailRow.FinalPrice = detailRow.FinalPrice;
                            newPODetailRow.Quantity = detailRow.Quantity;
                            newPODetailRow.Amount = detailRow.Amount;
                            newPODetailRow.AmountRMB = detailRow.AmountRMB;
                            newPODetailRow.DeliveryDate = detailRow.DeliveryDate;
                            if (!detailRow.IsDeliveryAddressNull()) {
                                newPODetailRow.DeliveryAddress = detailRow.DeliveryAddress;
                            }

                            newPODetailTable.AddFormPODetailRow(newPODetailRow);
                            totalAmountRMB = totalAmountRMB + newPODetailRow.AmountRMB;
                        }
                    }
                    this.TAFormPODetail.Update(newPODetailTable);

                } else {
                    foreach (PurchaseDS.FormPODetailRow detailRow in this.PurchaseDataSet.FormPODetail) {
                        if (detailRow.RowState != DataRowState.Deleted) {
                            detailRow.FormPOID = formRow.FormID;
                            totalAmountRMB = totalAmountRMB + detailRow.AmountRMB;
                        }
                    }
                    this.TAFormPODetail.Update(this.PurchaseDataSet.FormPODetail);
                }

                formPORow.AmountRMB = totalAmountRMB;
                this.TAFormPO.Update(formPORow);

                //作废之前的单据
                if (RejectedFormID != null) {
                    PurchaseDS.FormRow oldRow = this.TAForm.GetDataByID(RejectedFormID.GetValueOrDefault())[0];
                    if (oldRow.StatusID == (int)SystemEnums.FormStatus.Rejected) {
                        oldRow.StatusID = (int)SystemEnums.FormStatus.Scrap;
                        this.TAForm.Update(oldRow);
                    }
                }

                // 正式提交
                if (StatusID == SystemEnums.FormStatus.Awaiting) {
                    if (!IsChanged) {//如果没有改变的话应该自动审批通过
                        formRow.StatusID = 2;
                    } else {
                        Dictionary<string, object> dic = new Dictionary<string, object>();
                        dic["TotalAmount"] = totalAmountRMB;
                        dic["POType"] = POType;
                        APResult result = new APFlowBLL().CreateProcess(new APParameter(formRow.FormID, formRow.FormNo, formRow.OrganizationUnitID, formRow.UserID, (int)StatusID, formRow.FormTypeID, formRow.PositionID, dic));
                        if (result != null) {
                            formRow.InTurnPositionIds = result.InTurnPositionIds;
                            formRow.InTurnUserIds = result.InTurnUserIds;
                            formRow.SubmitDate = result.ApprovedDate.GetValueOrDefault();
                            formRow.StatusID = result.StatusID;
                            formRow.ProcID = result.ProcID;
                        }
                    }
                    this.TAForm.Update(formRow);
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

        public void UpdatePOSpecialApply(int FormID, SystemEnums.FormStatus StatusID, int PurchaseTypeID, int? CompanyID, int ShippingTermID,
                            string PaymentTerms, string Remark, string AttachedFileName, string RealAttachedFileName, string RealDeliveryAddress) {

            SqlTransaction transaction = null;

            try {
                //事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormPO, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormPODetail, transaction);

                PurchaseDS.FormRow formRow = this.TAForm.GetDataByID(FormID)[0];
                PurchaseDS.FormPORow formPORow = this.TAFormPO.GetDataByID(FormID)[0];

                //处理单据的内容
                UtilityBLL utility = new UtilityBLL();
                if (StatusID == SystemEnums.FormStatus.Awaiting) {
                    string formTypeString = utility.GetFormTypeString(formRow.FormTypeID);
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

                //处理申请表的内容
                formPORow.PurchaseTypeID = PurchaseTypeID;
                if (CompanyID != null) {
                    formPORow.CompanyID = CompanyID.GetValueOrDefault();
                    formPORow.DeliveryAddress = new MasterDataBLL().GetCompanyById(CompanyID.GetValueOrDefault()).CompanyAddress;
                }
                formPORow.ShippingTermID = ShippingTermID;
                if (PaymentTerms != null) {
                    formPORow.PaymentTerms = PaymentTerms;
                }
                if (Remark != null) {
                    formPORow.Remark = Remark;
                }
                if (AttachedFileName != null && AttachedFileName != string.Empty) {
                    formPORow.AttachedFileName = AttachedFileName;
                } else {
                    formPORow.SetAttachedFileNameNull();
                }
                if (RealAttachedFileName != null && RealAttachedFileName != string.Empty) {
                    formPORow.RealAttachedFileName = RealAttachedFileName;
                } else {
                    formPORow.SetRealAttachedFileNameNull();
                }

                formPORow.AmountRMB = 0;
                formPORow.RealDeliveryAddress = RealDeliveryAddress;
                this.TAFormPO.Update(formPORow);

                //处理明细
                decimal totalAmountRMB = 0;
                foreach (PurchaseDS.FormPODetailRow detailRow in this.PurchaseDataSet.FormPODetail) {
                    if (detailRow.RowState != DataRowState.Deleted) {
                        detailRow.FormPOID = formRow.FormID;
                        totalAmountRMB = totalAmountRMB + detailRow.AmountRMB;
                    }
                }
                this.TAFormPODetail.Update(this.PurchaseDataSet.FormPODetail);

                formPORow.AmountRMB = totalAmountRMB;
                this.TAFormPO.Update(formPORow);

                // 正式提交
                if (StatusID == SystemEnums.FormStatus.Awaiting) {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic["TotalAmount"] = totalAmountRMB;
                    dic["POType"] = formPORow.POType;
                    APResult result = new APFlowBLL().CreateProcess(new APParameter(formRow.FormID, formRow.FormNo, formRow.OrganizationUnitID, formRow.UserID, (int)StatusID, formRow.FormTypeID, formRow.PositionID, dic));
                    if (result != null) {
                        formRow.InTurnPositionIds = result.InTurnPositionIds;
                        formRow.InTurnUserIds = result.InTurnUserIds;
                        formRow.SubmitDate = result.ApprovedDate.GetValueOrDefault();
                        formRow.StatusID = result.StatusID;
                        formRow.ProcID = result.ProcID;
                    }
                    this.TAForm.Update(formRow);
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

        public int QueryPOCountByParentFormID(int FormID) {
            return (int)this.TAFormPO.QueryCountByParentFormID(FormID);
        }

        public string GetPVNoByPOID(int POID) {
            string PVFormNo = (string)this.TAFormPO.GetPVNoByPOID(POID);
            if (PVFormNo == null) {
                PVFormNo = "";
            }
            return PVFormNo;
        }

        #endregion

        #region PV

        public PurchaseDS.FormPVRow GetFormPVByID(int FormPVID) {
            return this.TAFormPV.GetDataByID(FormPVID)[0];
        }

        public PurchaseDS.FormPRPODetailViewDataTable GetFormPRPODetailView() {
            return this.PurchaseDataSet.FormPRPODetailView;
        }

        public void AddPVApply(int? RejectedFormID, int UserID, int? ProxyUserID, int? ProxyPositionID, int OrganizationUnitID, int PositionID, SystemEnums.FormType FormTypeID, SystemEnums.FormStatus StatusID,
                        int? FormPRID, int? FormPOID, string ParentFormNo, DateTime FPeriod, int VendorID, int ItemCategoryID, int CurrencyID, decimal ExchangeRate, int PurchaseBudgetTypeID, int PurchaseTypeID,
                        int? CompanyID, int ShippingTermID, string PaymentTerms, string Remark, string AttachedFileName, string RealAttachedFileName, int MethodPaymentID, DateTime? ExpectPaymentDate,
                        decimal ApplyAmount, decimal PayedAmount, decimal AMTBeforeTax, decimal AMTTax, bool IsUrgent, bool IsPublic, int InvoiceStatusID, int PVType, int? VATRateID, string ItemCategoryName, string RealDeliveryAddress) {

            SqlTransaction transaction = null;

            try {
                //事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormPV, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormInvoice, transaction);

                //处理单据的内容
                PurchaseDS.FormRow formRow = this.PurchaseDataSet.Form.NewFormRow();
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
                formRow.PageType = (int)SystemEnums.PageType.NormalPV;
                formRow.CostCenterID = new StuffUserBLL().GetCostCenterIDByPositionID(PositionID);
                formRow.IsCreateVoucher = false;
                formRow.IsExportLock = false;
                formRow.IsCompletePayment = false;

                this.PurchaseDataSet.Form.AddFormRow(formRow);
                this.TAForm.Update(formRow);

                //处理申请表的内容
                PurchaseDS.FormPVRow formPVRow = this.PurchaseDataSet.FormPV.NewFormPVRow();
                formPVRow.FormPVID = formRow.FormID;
                if (FormPRID != null) {
                    formPVRow.FormPRID = FormPRID.GetValueOrDefault();
                }
                if (FormPOID != null) {
                    formPVRow.FormPOID = FormPOID.GetValueOrDefault();
                }
                formPVRow.ParentFormNo = ParentFormNo;
                formPVRow.FPeriod = FPeriod;
                formPVRow.VendorID = VendorID;
                formPVRow.ItemCategoryID = ItemCategoryID;
                formPVRow.CurrencyID = CurrencyID;
                formPVRow.ExchangeRate = ExchangeRate;
                formPVRow.PurchaseBudgetTypeID = PurchaseBudgetTypeID;
                formPVRow.PurchaseTypeID = PurchaseTypeID;
                if (CompanyID != null) {
                    formPVRow.CompanyID = CompanyID.GetValueOrDefault();
                    formPVRow.DeliveryAddress = new MasterDataBLL().GetCompanyById(CompanyID.GetValueOrDefault()).CompanyAddress;
                }
                formPVRow.ShippingTermID = ShippingTermID;
                formPVRow.PaymentTerms = PaymentTerms;
                formPVRow.Remark = Remark;
                if (AttachedFileName != null && AttachedFileName != string.Empty) {
                    formPVRow.AttachedFileName = AttachedFileName;
                }
                if (RealAttachedFileName != null && RealAttachedFileName != string.Empty) {
                    formPVRow.RealAttachedFileName = RealAttachedFileName;
                }
                formPVRow.MethodPaymentID = MethodPaymentID;
                if (ExpectPaymentDate != null) {
                    formPVRow.ExpectPaymentDate = ExpectPaymentDate.GetValueOrDefault();
                }
                formPVRow.ApplyAmount = ApplyAmount;
                formPVRow.PayedAmount = PayedAmount;
                formPVRow.AMTBeforeTax = AMTBeforeTax;
                formPVRow.AMTTax = AMTTax;
                formPVRow.Amount = AMTBeforeTax + AMTTax;
                formPVRow.AmountRMB = decimal.Round(formPVRow.Amount * ExchangeRate, 2);
                formPVRow.IsUrgent = IsUrgent;
                formPVRow.IsPublic = IsPublic;
                formPVRow.InvoiceStatusID = InvoiceStatusID;
                formPVRow.PVType = PVType;
                if (VATRateID != null) {
                    formPVRow.VatRateID = VATRateID.GetValueOrDefault();
                }
                formPVRow.RealDeliveryAddress = RealDeliveryAddress;
                formPVRow.FinalItemCategoryID = ItemCategoryID;
                //payment term是否改变，只要在此判断即可，是从PO或者PR带入的
                string defaultPT = new MasterDataBLL().GetPaymentTermById(new VendorTableAdapter().GetDataByID(VendorID)[0].PaymentTermID)[0].PaymentTermName;
                if (defaultPT != PaymentTerms) {
                    formPVRow.IsPTChanged = true;
                } else {
                    formPVRow.IsPTChanged = false;
                }
                this.PurchaseDataSet.FormPV.AddFormPVRow(formPVRow);
                this.TAFormPV.Update(formPVRow);

                //发票
                if (RejectedFormID != null) {

                    PurchaseDS.FormInvoiceDataTable newInvoiceTable = new PurchaseDS.FormInvoiceDataTable();
                    foreach (PurchaseDS.FormInvoiceRow invoiceRow in this.PurchaseDataSet.FormInvoice) {
                        if (invoiceRow.RowState != DataRowState.Deleted) {
                            PurchaseDS.FormInvoiceRow newInvoiceRow = newInvoiceTable.NewFormInvoiceRow();
                            newInvoiceRow.FormID = formRow.FormID;
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
                    foreach (PurchaseDS.FormInvoiceRow invoiceRow in this.PurchaseDataSet.FormInvoice) {
                        // 与父表绑定
                        if (invoiceRow.RowState != DataRowState.Deleted) {
                            invoiceRow.FormID = formRow.FormID;
                        }
                    }
                }
                this.TAFormInvoice.Update(this.PurchaseDataSet.FormInvoice);

                //作废之前的单据
                if (RejectedFormID != null) {
                    PurchaseDS.FormRow oldRow = this.TAForm.GetDataByID(RejectedFormID.GetValueOrDefault())[0];
                    if (oldRow.StatusID == (int)SystemEnums.FormStatus.Rejected) {
                        oldRow.StatusID = (int)SystemEnums.FormStatus.Scrap;
                        this.TAForm.Update(oldRow);
                    }
                }

                // 正式提交
                if (StatusID == SystemEnums.FormStatus.Awaiting) {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic["TTLCost"] = formPVRow.AmountRMB;
                    dic["ItemCategory"] = ItemCategoryName;
                    dic["InvoiceStatus"] = formPVRow.InvoiceStatusID;
                    dic["IsUrgent"] = formPVRow.IsUrgent;
                    APResult result = new APFlowBLL().CreateProcess(new APParameter(formRow.FormID, formRow.FormNo, formRow.OrganizationUnitID, formRow.UserID, (int)StatusID, formRow.FormTypeID, formRow.PositionID, dic));
                    if (result != null) {
                        formRow.InTurnPositionIds = result.InTurnPositionIds;
                        formRow.InTurnUserIds = result.InTurnUserIds;
                        formRow.SubmitDate = result.ApprovedDate.GetValueOrDefault();
                        formRow.StatusID = result.StatusID;
                        formRow.ProcID = result.ProcID;
                        formRow.FinanceRemark = formPVRow.Remark;
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

        public void UpdatePVApply(int FormID, SystemEnums.FormStatus StatusID, string Remark, string AttachedFileName, string RealAttachedFileName, int MethodPaymentID, DateTime? ExpectPaymentDate,
                decimal ApplyAmount, decimal PayedAmount, decimal AMTBeforeTax, decimal AMTTax, bool IsUrgent, bool IsPublic, int InvoiceStatusID, int? VATRateID, string ItemCategoryName) {

            SqlTransaction transaction = null;

            try {
                //事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormPV, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormInvoice, transaction);

                PurchaseDS.FormRow formRow = this.TAForm.GetDataByID(FormID)[0];
                PurchaseDS.FormPVRow formPVRow = this.TAFormPV.GetDataByID(FormID)[0];

                //处理单据的内容
                UtilityBLL utility = new UtilityBLL();
                if (StatusID == SystemEnums.FormStatus.Awaiting) {
                    string formTypeString = utility.GetFormTypeString(formRow.FormTypeID);
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

                //处理申请表的内容

                if (Remark != null) {
                    formPVRow.Remark = Remark;
                }
                if (AttachedFileName != null && AttachedFileName != string.Empty) {
                    formPVRow.AttachedFileName = AttachedFileName;
                } else {
                    formPVRow.SetAttachedFileNameNull();
                }
                if (RealAttachedFileName != null && RealAttachedFileName != string.Empty) {
                    formPVRow.RealAttachedFileName = RealAttachedFileName;
                } else {
                    formPVRow.SetRealAttachedFileNameNull();
                }
                formPVRow.MethodPaymentID = MethodPaymentID;
                if (ExpectPaymentDate != null) {
                    formPVRow.ExpectPaymentDate = ExpectPaymentDate.GetValueOrDefault();
                }
                formPVRow.ApplyAmount = ApplyAmount;
                formPVRow.PayedAmount = PayedAmount;
                formPVRow.AMTBeforeTax = AMTBeforeTax;
                formPVRow.AMTTax = AMTTax;
                formPVRow.Amount = AMTBeforeTax + AMTTax;
                formPVRow.AmountRMB = decimal.Round(formPVRow.Amount * formPVRow.ExchangeRate, 2);
                formPVRow.IsUrgent = IsUrgent;
                formPVRow.IsPublic = IsPublic;
                formPVRow.InvoiceStatusID = InvoiceStatusID;
                if (VATRateID != null) {
                    formPVRow.VatRateID = VATRateID.GetValueOrDefault();
                }

                this.TAFormPV.Update(formPVRow);

                //发票
                foreach (PurchaseDS.FormInvoiceRow invoiceRow in this.PurchaseDataSet.FormInvoice) {
                    // 与父表绑定
                    if (invoiceRow.RowState != DataRowState.Deleted) {
                        invoiceRow.FormID = formRow.FormID;
                    }
                }
                this.TAFormInvoice.Update(this.PurchaseDataSet.FormInvoice);

                // 正式提交
                if (StatusID == SystemEnums.FormStatus.Awaiting) {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic["TTLCost"] = formPVRow.AmountRMB;
                    dic["ItemCategory"] = ItemCategoryName;
                    dic["InvoiceStatus"] = formPVRow.InvoiceStatusID;
                    dic["IsUrgent"] = formPVRow.IsUrgent;
                    APResult result = new APFlowBLL().CreateProcess(new APParameter(formRow.FormID, formRow.FormNo, formRow.OrganizationUnitID, formRow.UserID, (int)StatusID, formRow.FormTypeID, formRow.PositionID, dic));
                    if (result != null) {
                        formRow.InTurnPositionIds = result.InTurnPositionIds;
                        formRow.InTurnUserIds = result.InTurnUserIds;
                        formRow.SubmitDate = result.ApprovedDate.GetValueOrDefault();
                        formRow.StatusID = result.StatusID;
                        formRow.ProcID = result.ProcID;
                        formRow.FinanceRemark = formPVRow.Remark;
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

        public void DeleteFormPVByFormID(int FormID) {
            SqlTransaction transaction = null;
            try {
                //事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAFormPV);
                TableAdapterHelper.SetTransaction(this.TAFormInvoice, transaction);
                TableAdapterHelper.SetTransaction(this.TAForm, transaction);
                this.TAFormPV.DeleteByID(FormID);
                this.TAFormInvoice.DeleteByFormID(FormID);
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

        public PurchaseDS.FormInvoiceDataTable GetFormInvoice() {
            return this.PurchaseDataSet.FormInvoice;
        }

        public PurchaseDS.FormInvoiceDataTable GetFormInvoiceByFormID(int FormID) {
            return this.TAFormInvoice.GetDataByFormID(FormID);
        }

        public void UpdateFormInvoice(int FormInvoiceID, string InvoiceNo, DateTime InvoiceDate, decimal InvoiceAmount, string Remark) {

            PurchaseDS.FormInvoiceDataTable table = this.PurchaseDataSet.FormInvoice;
            PurchaseDS.FormInvoiceRow rowDetail = table.FindByFormInvoiceID(FormInvoiceID);
            if (rowDetail == null)
                return;
            rowDetail.InvoiceNo = InvoiceNo;
            rowDetail.InvoiceDate = InvoiceDate;
            rowDetail.InvoiceAmount = InvoiceAmount;
            rowDetail.Remark = Remark;

        }

        public void AddFormInvoice(int? FormID, string InvoiceNo, DateTime InvoiceDate, decimal InvoiceAmount, string Remark) {

            PurchaseDS.FormInvoiceRow rowDetail = this.PurchaseDataSet.FormInvoice.NewFormInvoiceRow();
            rowDetail.FormID = FormID.GetValueOrDefault();
            rowDetail.InvoiceNo = InvoiceNo;
            rowDetail.InvoiceDate = InvoiceDate;
            rowDetail.InvoiceAmount = InvoiceAmount;
            rowDetail.Remark = Remark;
            string systemInfo = new FormSaleBLL().GetRepeatedInvoiceFormNosByInvioceNo(InvoiceNo);
            if (systemInfo != string.Empty) {
                rowDetail.SystemInfo = "发票号码重复，报销单编号为:" + systemInfo;
            }
            // 填加行并进行更新处理
            this.PurchaseDataSet.FormInvoice.AddFormInvoiceRow(rowDetail);

        }

        public void DeleteFormInvoiceByID(int FormInvoiceID) {
            for (int index = 0; index < this.PurchaseDataSet.FormInvoice.Count; index++) {
                if ((int)this.PurchaseDataSet.FormInvoice.Rows[index]["FormInvoiceID"] == FormInvoiceID) {
                    this.PurchaseDataSet.FormInvoice.Rows[index].Delete();
                    break;
                }
            }
        }

        #endregion

        #region PVDetail

        public PurchaseDS.FormPVDetailDataTable GetPVDetailByFormPVID(int FormPVID) {
            return this.TAFormPVDetail.GetDataByFormPVID(FormPVID);
        }

        public PurchaseDS.FormPVDetailDataTable GetPVDetail() {
            return this.PurchaseDataSet.FormPVDetail;
        }

        public void AddFormPVDetail(int? FormPVID, int ItemID, decimal FinalPrice, decimal Quantity, decimal ExchangeRate, DateTime DeliveryDate) {
            PurchaseDS.FormPVDetailRow rowDetail = this.PurchaseDataSet.FormPVDetail.NewFormPVDetailRow();
            rowDetail.FormPVID = FormPVID.GetValueOrDefault();
            MasterData.ItemRow item = new ItemTableAdapter().GetDataByID(ItemID)[0];
            rowDetail.ItemID = ItemID;
            rowDetail.ItemCode = item.ItemCode;
            rowDetail.ItemName = item.ItemName;
            if (!item.IsDescriptionNull()) {
                rowDetail.ItemDescription = item.Description;
            }
            if (!item.IsPackageNull()) {
                rowDetail.Package = item.Package;
            }
            rowDetail.UnitPrice = item.UnitPrice;
            rowDetail.FinalPrice = FinalPrice;
            rowDetail.Quantity = Quantity;
            rowDetail.Amount = decimal.Round(FinalPrice * Quantity, 2);
            rowDetail.AmountRMB = decimal.Round(rowDetail.Amount * ExchangeRate, 2);
            rowDetail.DeliveryDate = DeliveryDate;
            PurchaseDataSet.FormPVDetail.AddFormPVDetailRow(rowDetail);
        }

        public void UpdateFormPVDetail(int FormPVDetailID, int ItemID, decimal FinalPrice, decimal Quantity, decimal ExchangeRate, DateTime DeliveryDate) {
            PurchaseDS.FormPVDetailRow rowDetail = this.PurchaseDataSet.FormPVDetail.FindByFormPVDetailID(FormPVDetailID);
            MasterData.ItemRow item = new ItemTableAdapter().GetDataByID(ItemID)[0];
            rowDetail.ItemID = ItemID;
            rowDetail.ItemCode = item.ItemCode;
            rowDetail.ItemName = item.ItemName;
            if (!item.IsDescriptionNull()) {
                rowDetail.ItemDescription = item.Description;
            }
            if (!item.IsPackageNull()) {
                rowDetail.Package = item.Package;
            }
            rowDetail.UnitPrice = item.UnitPrice;
            rowDetail.FinalPrice = FinalPrice;
            rowDetail.Quantity = Quantity;
            rowDetail.Amount = decimal.Round(FinalPrice * Quantity, 2);
            rowDetail.AmountRMB = decimal.Round(rowDetail.Amount * ExchangeRate, 2);
            rowDetail.DeliveryDate = DeliveryDate;
        }

        public void DeleteFormPVDetailByID(int FormPVDetailID) {
            for (int index = 0; index < this.PurchaseDataSet.FormPVDetail.Count; index++) {
                if ((int)this.PurchaseDataSet.FormPVDetail.Rows[index]["FormPVDetailID"] == FormPVDetailID) {
                    this.PurchaseDataSet.FormPVDetail.Rows[index].Delete();
                    break;
                }
            }
        }

        #endregion

        #region Special PV

        public void AddPVSpecialApply(int? RejectedFormID, int UserID, int? ProxyUserID, int? ProxyPositionID, int OrganizationUnitID, int PositionID, SystemEnums.FormType FormTypeID, SystemEnums.FormStatus StatusID,
                        DateTime FPeriod, int VendorID, int ItemCategoryID, int CurrencyID, decimal ExchangeRate, int PurchaseBudgetTypeID, int PurchaseTypeID, string Remark,
                        string AttachedFileName, string RealAttachedFileName, int MethodPaymentID, DateTime? ExpectPaymentDate, decimal TotalBudget, decimal ApprovedAmount, decimal ApprovingAmount,
                        decimal ReimbursedAmount, decimal NonReimbursedAmount, decimal RemainBudget, bool IsUrgent, bool IsPublic, int InvoiceStatusID, int PVType, int? VATRateID, decimal AMTTax, string PaymentTerms, bool IsPTChanged) {

            SqlTransaction transaction = null;

            try {
                //事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormPV, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormInvoice, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormPVDetail, transaction);

                //处理单据的内容
                PurchaseDS.FormRow formRow = this.PurchaseDataSet.Form.NewFormRow();
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
                formRow.PageType = (int)SystemEnums.PageType.SpecialPV;
                formRow.CostCenterID = new StuffUserBLL().GetCostCenterIDByPositionID(PositionID);
                formRow.IsCreateVoucher = false;
                formRow.IsExportLock = false;
                formRow.IsCompletePayment = false;
                this.PurchaseDataSet.Form.AddFormRow(formRow);
                this.TAForm.Update(formRow);

                //处理申请表的内容
                PurchaseDS.FormPVRow formPVRow = this.PurchaseDataSet.FormPV.NewFormPVRow();
                formPVRow.FormPVID = formRow.FormID;
                formPVRow.FPeriod = FPeriod;
                formPVRow.VendorID = VendorID;
                formPVRow.ItemCategoryID = ItemCategoryID;
                formPVRow.CurrencyID = CurrencyID;
                formPVRow.ExchangeRate = ExchangeRate;
                formPVRow.PurchaseBudgetTypeID = PurchaseBudgetTypeID;
                formPVRow.PurchaseTypeID = PurchaseTypeID;
                formPVRow.Remark = Remark;
                if (AttachedFileName != null && AttachedFileName != string.Empty) {
                    formPVRow.AttachedFileName = AttachedFileName;
                }
                if (RealAttachedFileName != null && RealAttachedFileName != string.Empty) {
                    formPVRow.RealAttachedFileName = RealAttachedFileName;
                }
                formPVRow.MethodPaymentID = MethodPaymentID;
                if (ExpectPaymentDate != null) {
                    formPVRow.ExpectPaymentDate = ExpectPaymentDate.GetValueOrDefault();
                }
                formPVRow.ApplyAmount = 0;
                formPVRow.PayedAmount = 0;
                formPVRow.Amount = 0;
                formPVRow.AmountRMB = 0;
                formPVRow.AMTBeforeTax = 0;
                formPVRow.AMTTax = AMTTax;
                formPVRow.TotalBudget = TotalBudget;
                formPVRow.ApprovedAmount = ApprovedAmount;
                formPVRow.ApprovingAmount = ApprovingAmount;
                formPVRow.ReimbursedAmount = ReimbursedAmount;
                formPVRow.NonReimbursedAmount = NonReimbursedAmount;
                formPVRow.RemainBudget = RemainBudget;
                formPVRow.IsUrgent = IsUrgent;
                formPVRow.IsPublic = IsPublic;
                formPVRow.InvoiceStatusID = InvoiceStatusID;
                formPVRow.PVType = PVType;
                if (VATRateID != null) {
                    formPVRow.VatRateID = VATRateID.GetValueOrDefault();
                }
                formPVRow.PaymentTerms = PaymentTerms;
                formPVRow.IsPTChanged = IsPTChanged;
                formPVRow.FinalItemCategoryID = ItemCategoryID;
                this.PurchaseDataSet.FormPV.AddFormPVRow(formPVRow);
                this.TAFormPV.Update(formPVRow);

                //发票
                if (RejectedFormID != null) {
                    PurchaseDS.FormInvoiceDataTable newInvoiceTable = new PurchaseDS.FormInvoiceDataTable();
                    foreach (PurchaseDS.FormInvoiceRow invoiceRow in this.PurchaseDataSet.FormInvoice) {
                        if (invoiceRow.RowState != DataRowState.Deleted) {
                            PurchaseDS.FormInvoiceRow newInvoiceRow = newInvoiceTable.NewFormInvoiceRow();
                            newInvoiceRow.FormID = formRow.FormID;
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
                    foreach (PurchaseDS.FormInvoiceRow invoiceRow in this.PurchaseDataSet.FormInvoice) {
                        // 与父表绑定
                        if (invoiceRow.RowState != DataRowState.Deleted) {
                            invoiceRow.FormID = formRow.FormID;
                        }
                    }
                }
                this.TAFormInvoice.Update(this.PurchaseDataSet.FormInvoice);

                //处理明细
                decimal totalAmount = 0;
                decimal totalAmountRMB = 0;
                if (RejectedFormID != null) {
                    PurchaseDS.FormPVDetailDataTable newPVDetailTable = new PurchaseDS.FormPVDetailDataTable();
                    foreach (PurchaseDS.FormPVDetailRow detailRow in this.PurchaseDataSet.FormPVDetail) {
                        if (detailRow.RowState != DataRowState.Deleted) {
                            PurchaseDS.FormPVDetailRow newPVDetailRow = newPVDetailTable.NewFormPVDetailRow();
                            newPVDetailRow.FormPVID = formRow.FormID;
                            newPVDetailRow.ItemID = detailRow.ItemID;
                            newPVDetailRow.ItemCode = detailRow.ItemCode;
                            newPVDetailRow.ItemName = detailRow.ItemName;
                            if (!detailRow.IsItemDescriptionNull()) {
                                newPVDetailRow.ItemDescription = detailRow.ItemDescription;
                            }
                            if (!detailRow.IsPackageNull()) {
                                newPVDetailRow.Package = detailRow.Package;
                            }
                            newPVDetailRow.UnitPrice = detailRow.UnitPrice;
                            newPVDetailRow.FinalPrice = detailRow.FinalPrice;
                            newPVDetailRow.Quantity = detailRow.Quantity;
                            newPVDetailRow.Amount = detailRow.Amount;
                            newPVDetailRow.AmountRMB = decimal.Round(detailRow.Amount * ExchangeRate, 2);
                            newPVDetailRow.DeliveryDate = detailRow.DeliveryDate;

                            newPVDetailTable.AddFormPVDetailRow(newPVDetailRow);
                            totalAmount = totalAmountRMB + newPVDetailRow.Amount;
                            totalAmountRMB = totalAmountRMB + newPVDetailRow.AmountRMB;
                        }
                    }
                    this.TAFormPVDetail.Update(newPVDetailTable);

                } else {
                    foreach (PurchaseDS.FormPVDetailRow detailRow in this.PurchaseDataSet.FormPVDetail) {
                        if (detailRow.RowState != DataRowState.Deleted) {
                            detailRow.FormPVID = formRow.FormID;
                            detailRow.AmountRMB = decimal.Round(detailRow.Amount * ExchangeRate, 2);
                            totalAmountRMB = totalAmountRMB + detailRow.AmountRMB;
                            totalAmount = totalAmount + detailRow.Amount;
                        }
                    }
                    this.TAFormPVDetail.Update(this.PurchaseDataSet.FormPVDetail);
                }

                formPVRow.AmountRMB = totalAmountRMB;
                formPVRow.Amount = totalAmount;
                formPVRow.AMTBeforeTax = totalAmount - AMTTax;
                this.TAFormPV.Update(formPVRow);

                //作废之前的单据
                if (RejectedFormID != null) {
                    PurchaseDS.FormRow oldRow = this.TAForm.GetDataByID(RejectedFormID.GetValueOrDefault())[0];
                    if (oldRow.StatusID == (int)SystemEnums.FormStatus.Rejected) {
                        oldRow.StatusID = (int)SystemEnums.FormStatus.Scrap;
                        this.TAForm.Update(oldRow);
                    }
                }

                // 正式提交
                if (StatusID == SystemEnums.FormStatus.Awaiting) {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic["TotalAmount"] = totalAmountRMB;
                    dic["InvoiceStatus"] = formPVRow.InvoiceStatusID;
                    dic["IsUrgent"] = formPVRow.IsUrgent;
                    APResult result = new APFlowBLL().CreateProcess(new APParameter(formRow.FormID, formRow.FormNo, formRow.OrganizationUnitID, formRow.UserID, (int)StatusID, formRow.FormTypeID, formRow.PositionID, dic));
                    if (result != null) {
                        formRow.InTurnPositionIds = result.InTurnPositionIds;
                        formRow.InTurnUserIds = result.InTurnUserIds;
                        formRow.SubmitDate = result.ApprovedDate.GetValueOrDefault();
                        formRow.StatusID = result.StatusID;
                        formRow.ProcID = result.ProcID;
                        formRow.FinanceRemark = formPVRow.Remark;
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

        public void UpdatePVSpecialApply(int FormID, SystemEnums.FormStatus StatusID, decimal ExchangeRate, int PurchaseTypeID, string Remark, string AttachedFileName,
                        string RealAttachedFileName, int MethodPaymentID, DateTime? ExpectPaymentDate, decimal TotalBudget, decimal ApprovedAmount,
                        decimal ApprovingAmount, decimal ReimbursedAmount, decimal NonReimbursedAmount, decimal RemainBudget, bool IsUrgent, bool IsPublic, int InvoiceStatusID, int? VATRateID, decimal AMTTax, string PaymentTerms, bool IsPTChanged) {

            SqlTransaction transaction = null;

            try {
                //事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormPV, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormInvoice, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormPVDetail, transaction);

                PurchaseDS.FormRow formRow = this.TAForm.GetDataByID(FormID)[0];
                PurchaseDS.FormPVRow formPVRow = this.TAFormPV.GetDataByID(FormID)[0];

                //处理单据的内容
                UtilityBLL utility = new UtilityBLL();
                if (StatusID == SystemEnums.FormStatus.Awaiting) {
                    string formTypeString = utility.GetFormTypeString(formRow.FormTypeID);
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

                //处理申请表的内容
                formPVRow.ExchangeRate = ExchangeRate;
                formPVRow.PurchaseTypeID = PurchaseTypeID;
                if (Remark != null) {
                    formPVRow.Remark = Remark;
                }
                if (AttachedFileName != null && AttachedFileName != string.Empty) {
                    formPVRow.AttachedFileName = AttachedFileName;
                } else {
                    formPVRow.SetAttachedFileNameNull();
                }
                if (RealAttachedFileName != null && RealAttachedFileName != string.Empty) {
                    formPVRow.RealAttachedFileName = RealAttachedFileName;
                } else {
                    formPVRow.SetRealAttachedFileNameNull();
                }
                formPVRow.MethodPaymentID = MethodPaymentID;
                if (ExpectPaymentDate != null) {
                    formPVRow.ExpectPaymentDate = ExpectPaymentDate.GetValueOrDefault();
                }
                formPVRow.TotalBudget = TotalBudget;
                formPVRow.ApprovedAmount = ApprovedAmount;
                formPVRow.ApprovingAmount = ApprovingAmount;
                formPVRow.ReimbursedAmount = ReimbursedAmount;
                formPVRow.NonReimbursedAmount = NonReimbursedAmount;
                formPVRow.RemainBudget = RemainBudget;
                formPVRow.IsUrgent = IsUrgent;
                formPVRow.IsPublic = IsPublic;
                formPVRow.InvoiceStatusID = InvoiceStatusID;
                formPVRow.IsUrgent = IsUrgent;
                formPVRow.IsPublic = IsPublic;
                formPVRow.InvoiceStatusID = InvoiceStatusID;
                formPVRow.AMTTax = AMTTax;
                if (VATRateID != null) {
                    formPVRow.VatRateID = VATRateID.GetValueOrDefault();
                }
                formPVRow.PaymentTerms = PaymentTerms;
                formPVRow.IsPTChanged = IsPTChanged;

                this.TAFormPV.Update(formPVRow);

                //发票
                foreach (PurchaseDS.FormInvoiceRow invoiceRow in this.PurchaseDataSet.FormInvoice) {
                    // 与父表绑定
                    if (invoiceRow.RowState != DataRowState.Deleted) {
                        invoiceRow.FormID = formRow.FormID;
                    }
                }
                this.TAFormInvoice.Update(this.PurchaseDataSet.FormInvoice);

                //处理明细
                decimal totalAmount = 0;
                decimal totalAmountRMB = 0;
                foreach (PurchaseDS.FormPVDetailRow detailRow in this.PurchaseDataSet.FormPVDetail) {
                    if (detailRow.RowState != DataRowState.Deleted) {
                        detailRow.FormPVID = formRow.FormID;
                        detailRow.AmountRMB = decimal.Round(detailRow.Amount * ExchangeRate, 2);
                        totalAmountRMB = totalAmountRMB + detailRow.AmountRMB;
                        totalAmount = totalAmount + detailRow.Amount;
                    }
                }
                this.TAFormPVDetail.Update(this.PurchaseDataSet.FormPVDetail);

                formPVRow.AmountRMB = totalAmountRMB;
                formPVRow.Amount = totalAmount;
                formPVRow.AMTBeforeTax = totalAmount - AMTTax;
                this.TAFormPV.Update(formPVRow);

                // 正式提交
                if (StatusID == SystemEnums.FormStatus.Awaiting) {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic["TotalAmount"] = totalAmountRMB;
                    dic["InvoiceStatus"] = formPVRow.InvoiceStatusID;
                    dic["IsUrgent"] = formPVRow.IsUrgent;
                    APResult result = new APFlowBLL().CreateProcess(new APParameter(formRow.FormID, formRow.FormNo, formRow.OrganizationUnitID, formRow.UserID, (int)StatusID, formRow.FormTypeID, formRow.PositionID, dic));
                    if (result != null) {
                        formRow.InTurnPositionIds = result.InTurnPositionIds;
                        formRow.InTurnUserIds = result.InTurnUserIds;
                        formRow.SubmitDate = result.ApprovedDate.GetValueOrDefault();
                        formRow.StatusID = result.StatusID;
                        formRow.ProcID = result.ProcID;
                        formRow.FinanceRemark = formPVRow.Remark;
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

        public void DeleteFormPVSpecialByFormID(int FormID) {
            SqlTransaction transaction = null;
            try {
                //事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAFormPV);
                TableAdapterHelper.SetTransaction(this.TAFormInvoice, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormPVDetail, transaction);
                TableAdapterHelper.SetTransaction(this.TAForm, transaction);
                this.TAFormPVDetail.DeleteByFormPVID(FormID);
                this.TAFormPV.DeleteByID(FormID);
                this.TAFormInvoice.DeleteByFormID(FormID);
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

        public void UpdatePVItemCategory(int FormID, int FinalItemCategoryID) {
            PurchaseDS.FormPVRow formPVRow = this.TAFormPV.GetDataByID(FormID)[0];
            formPVRow.FinalItemCategoryID = FinalItemCategoryID;
            this.TAFormPV.Update(formPVRow);
        }

        #endregion

        #region FormInvoiceReverse

        public PurchaseDS.FormInvoiceReverseDataTable GetFormInvoiceReverseByFormID(int FormID) {
            return this.TAFormInvoiceReverse.GetDataByFormID(FormID);
        }

        public void UpdateFormInvoiceReverse(int FormInvoiceReverseID, string InvoiceNo, DateTime InvoiceDate, decimal InvoiceAmount, string Remark) {

            PurchaseDS.FormInvoiceReverseRow rowDetail = this.TAFormInvoiceReverse.GetDataByID(FormInvoiceReverseID)[0];
            if (rowDetail == null)
                return;
            rowDetail.InvoiceNo = InvoiceNo;
            rowDetail.InvoiceDate = InvoiceDate;
            rowDetail.InvoiceAmount = InvoiceAmount;
            rowDetail.Remark = Remark;
            rowDetail.Status = 1;//待审批
            rowDetail.InputDate = DateTime.Now;
            this.TAFormInvoiceReverse.Update(rowDetail);
        }

        public void AddFormInvoiceReverse(int FormID, string InvoiceNo, DateTime InvoiceDate, decimal InvoiceAmount, string Remark) {

            PurchaseDS.FormInvoiceReverseDataTable newtable = new PurchaseDS.FormInvoiceReverseDataTable();
            PurchaseDS.FormInvoiceReverseRow rowDetail = newtable.NewFormInvoiceReverseRow();
            rowDetail.FormID = FormID;
            rowDetail.InvoiceNo = InvoiceNo;
            rowDetail.InvoiceDate = InvoiceDate;
            rowDetail.InvoiceAmount = InvoiceAmount;
            rowDetail.Remark = Remark;
            rowDetail.InputDate = DateTime.Now;
            rowDetail.Status = 1;//待审批
            // 填加行并进行更新处理
            newtable.AddFormInvoiceReverseRow(rowDetail);
            this.TAFormInvoiceReverse.Update(newtable);

        }

        public void DeleteFormInvoiceReverseByID(int FormInvoiceReverseID) {
            this.TAFormInvoiceReverse.DeleteByID(FormInvoiceReverseID);
        }

        public void ApproveFormInvoiceReverse(int FormInvoiceReverseID, int ApproverID) {
            PurchaseDS.FormInvoiceReverseRow rowDetail = this.TAFormInvoiceReverse.GetDataByID(FormInvoiceReverseID)[0];
            if (rowDetail == null)
                return;
            rowDetail.Status = 2;//审批完成
            rowDetail.ApproveDate = DateTime.Now;
            rowDetail.ApproverID = ApproverID;
            this.TAFormInvoiceReverse.Update(rowDetail);
        }

        public void RejectFormInvoiceReverse(int FormInvoiceReverseID, int ApproverID) {
            PurchaseDS.FormInvoiceReverseRow rowDetail = this.TAFormInvoiceReverse.GetDataByID(FormInvoiceReverseID)[0];
            if (rowDetail == null)
                return;
            rowDetail.Status = 3;//审批完成
            rowDetail.ApproveDate = DateTime.Now;
            rowDetail.ApproverID = ApproverID;
            this.TAFormInvoiceReverse.Update(rowDetail);
        }

        #endregion
    }
}
