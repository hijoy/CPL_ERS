using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using BusinessObjects.FormDSTableAdapters;
using lib.wf;

namespace BusinessObjects {
    public class FormSampleRequestBLL {

        #region TableAdapters

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

        private FormSaleApplyTableAdapter _TAFormSaleApply;
        public FormSaleApplyTableAdapter TAFormSaleApply {
            get {
                if (this._TAFormSaleApply == null) {
                    this._TAFormSaleApply = new FormSaleApplyTableAdapter();
                }
                return this._TAFormSaleApply;
            }
        }

        private FormSaleSKUDetailTableAdapter _TAFormSaleSKUDetail;
        public FormSaleSKUDetailTableAdapter TAFormSaleSKUDetail {
            get {
                if (this._TAFormSaleSKUDetail == null) {
                    this._TAFormSaleSKUDetail = new FormSaleSKUDetailTableAdapter();
                }
                return this._TAFormSaleSKUDetail;
            }
        }

        private FormSaleExpenseDetailTableAdapter _TAFormSaleExpenseDetail;
        public FormSaleExpenseDetailTableAdapter TAFormSaleExpenseDetail {
            get {
                if (this._TAFormSaleExpenseDetail == null) {
                    this._TAFormSaleExpenseDetail = new FormSaleExpenseDetailTableAdapter();
                }
                return this._TAFormSaleExpenseDetail;
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

        private FormRDApplyTableAdapter _TARDApply;
        public FormRDApplyTableAdapter TARDApply {
            get {
                if (this._TARDApply == null) {
                    this._TARDApply = new FormRDApplyTableAdapter();
                }
                return this._TARDApply;
            }
        }

        private FormRDApplyDetailTableAdapter _TARDApplyDetail;
        public FormRDApplyDetailTableAdapter TARDApplyDetail {
            get {
                if (this._TARDApplyDetail == null) {
                    this._TARDApplyDetail = new FormRDApplyDetailTableAdapter();
                }
                return this._TARDApplyDetail;
            }
        }

        #endregion

        #region 获取数据

        public FormDS.FormDataTable GetFormByID(int FormID) {
            return this.TAForm.GetDataByID(FormID);
        }

        public FormDS.FormDataTable GetDataByFormNo(string FormNo) {
            return this.TAForm.GetDataByFormNo(FormNo);
        }

        public FormDS.FormSaleApplyDataTable GetFormSaleApplyByID(int FormSaleApplyID) {
            return this.TAFormSaleApply.GetDataByID(FormSaleApplyID);
        }

        #endregion

        #region FormSaleExpenseDetail

        public FormDS.FormSaleExpenseDetailDataTable GetFormSaleExpenseDetail() {
            return this.FormDataSet.FormSaleExpenseDetail;
        }

        public FormDS.FormSaleExpenseDetailRow GetFormSaleExpenseDetailByID(int FormSaleExpenseDetailID) {
            return this.TAFormSaleExpenseDetail.GetDataByID(FormSaleExpenseDetailID)[0];
        }

        public FormDS.FormSaleExpenseDetailDataTable GetFormSaleExpenseDetailByFormSaleApplyID(int FormSaleApplyID) {
            return this.TAFormSaleExpenseDetail.GetDataByFormSaleApplyID(FormSaleApplyID);
        }

        public void AddFormSaleExpenseDetail(int? FormSaleApplyID, int? SKUID, int ExpenseItemID, string ShopName, decimal Amount, decimal AmountRMB, string Remark, decimal DeliveryPrice, int DeliveryQuantity) {

            FormDS.FormSaleExpenseDetailRow rowDetail = this.FormDataSet.FormSaleExpenseDetail.NewFormSaleExpenseDetailRow();
            rowDetail.FormSaleApplyID = FormSaleApplyID.GetValueOrDefault();
            rowDetail.ExpenseItemID = ExpenseItemID;
            rowDetail.ShopName = ShopName;
            rowDetail.Amount = Amount;
            rowDetail.AmountRMB = AmountRMB;
            rowDetail.Remark = Remark;
            rowDetail.DeliveryPrice = DeliveryPrice;
            rowDetail.DeliveryQuantity = DeliveryQuantity;
            if (SKUID != null) {
                rowDetail.SKUID = SKUID.GetValueOrDefault();
            }
            this.FormDataSet.FormSaleExpenseDetail.AddFormSaleExpenseDetailRow(rowDetail);
        }

        public void UpdateFormSaleExpenseDetail(int FormSaleExpenseDetailID, int? SKUID, int ExpenseItemID, string ShopName, decimal Amount, decimal AmountRMB, string Remark, decimal DeliveryPrice, int DeliveryQuantity) {

            FormDS.FormSaleExpenseDetailRow rowDetail = this.FormDataSet.FormSaleExpenseDetail.FindByFormSaleExpenseDetailID(FormSaleExpenseDetailID);
            if (rowDetail == null)
                return;
            rowDetail.ExpenseItemID = ExpenseItemID;
            if (ShopName != null) {
                rowDetail.ShopName = ShopName;
            }
            rowDetail.Amount = Amount;
            rowDetail.AmountRMB = AmountRMB;
            rowDetail.Remark = Remark;
            rowDetail.DeliveryPrice = DeliveryPrice;
            rowDetail.DeliveryQuantity = DeliveryQuantity;
            if (SKUID != null) {
                rowDetail.SKUID = SKUID.GetValueOrDefault();
            }
        }

        public void DeleteFormSaleExpenseDetailByID(int FormSaleExpenseDetailID) {
            for (int index = 0; index < this.FormDataSet.FormSaleExpenseDetail.Count; index++) {
                if ((int)this.FormDataSet.FormSaleExpenseDetail.Rows[index]["FormSaleExpenseDetailID"] == FormSaleExpenseDetailID) {
                    this.FormDataSet.FormSaleExpenseDetail.Rows[index].Delete();
                    break;
                }
            }
        }

        #endregion

        #region SaleSampleRequest Apply

        public void AddSaleSampleRequestApply(int? RejectedFormID, int UserID, int? ProxyUserID, int? ProxyPositionID, int OrganizationUnitID, int PositionID, SystemEnums.FormType FormTypeID, SystemEnums.FormStatus StatusID,
                        DateTime FPeriod, int CustomerID, int BrandID, int ExpenseSubCategoryID, int CurrencyID, decimal ExchangeRate, string ShopName, int? ShopCount, string ProjectName, string ProjectDesc,
                        string ApplyFileName, string ApplyRealFileName, DateTime? ActivityBeginDate, DateTime? ActivityEndDate, int? CostCenterID,
                        decimal TotalBudget, decimal ApprovedAmount, decimal ApprovingAmount, decimal CompletedAmount, decimal ReimbursedAmount, decimal RemainBudget, DateTime? ExpectDeliveryDate, string DeliverAddress) {

            SqlTransaction transaction = null;

            try {
                //事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormSaleApply, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormSaleExpenseDetail, transaction);

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
                formRow.PageType = (int)SystemEnums.PageType.SaleSampleRequest;
                if (CostCenterID != null) {
                    formRow.CostCenterID = CostCenterID.GetValueOrDefault();
                }
                this.FormDataSet.Form.AddFormRow(formRow);
                this.TAForm.Update(formRow);

                //处理申请表的内容
                FormDS.FormSaleApplyRow formApplyRow = this.FormDataSet.FormSaleApply.NewFormSaleApplyRow();
                formApplyRow.FormSaleApplyID = formRow.FormID;
                formApplyRow.FPeriod = DateTime.Parse(FPeriod.Year.ToString() + "-" + FPeriod.Month.ToString() + "-01");
                formApplyRow.CustomerID = CustomerID;
                formApplyRow.BrandID = BrandID;
                formApplyRow.ExpenseSubCategoryID = ExpenseSubCategoryID;
                formApplyRow.CurrencyID = CurrencyID;
                formApplyRow.ExchangeRate = ExchangeRate;
                if (ShopName != null)
                    formApplyRow.ShopName = ShopName;
                if (ShopCount != null)
                    formApplyRow.ShopCount = ShopCount.GetValueOrDefault();
                if (ProjectName != null)
                    formApplyRow.ProjectName = ProjectName;
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
                formApplyRow.CompletedAmount = CompletedAmount;
                formApplyRow.ReimbursedAmount = ReimbursedAmount;
                formApplyRow.RemainBudget = RemainBudget;
                formApplyRow.AmountRMB = 0;//暂时
                formApplyRow.IsClose = false;
                formApplyRow.IsCompleted = false;

                if (ExpectDeliveryDate != null) {
                    formApplyRow.ExpectDeliveryDate = ExpectDeliveryDate.GetValueOrDefault();
                }
                if (!string.IsNullOrEmpty(DeliverAddress)) {
                    formApplyRow.DeliveryAddress = DeliverAddress;
                }
                formApplyRow.IsDeliveryComplete = false;

                this.FormDataSet.FormSaleApply.AddFormSaleApplyRow(formApplyRow);
                this.TAFormSaleApply.Update(formApplyRow);

                //处理明细
                decimal totalAmountRMB = 0;

                if (RejectedFormID != null) {
                    FormDS.FormSaleExpenseDetailDataTable newDetailTable = new FormDS.FormSaleExpenseDetailDataTable();
                    foreach (FormDS.FormSaleExpenseDetailRow detailRow in this.FormDataSet.FormSaleExpenseDetail) {
                        if (detailRow.RowState != DataRowState.Deleted) {
                            FormDS.FormSaleExpenseDetailRow newDetailRow = newDetailTable.NewFormSaleExpenseDetailRow();
                            newDetailRow.FormSaleApplyID = formApplyRow.FormSaleApplyID;
                            newDetailRow.ExpenseItemID = detailRow.ExpenseItemID;
                            newDetailRow.SKUID = detailRow.SKUID;
                            newDetailRow.DeliveryPrice = detailRow.DeliveryPrice;
                            newDetailRow.DeliveryQuantity = detailRow.DeliveryQuantity;
                            newDetailRow.Amount = detailRow.Amount;
                            newDetailRow.AmountRMB = decimal.Round(detailRow.Amount * ExchangeRate, 2);
                            if (!detailRow.IsRemarkNull()) {
                                newDetailRow.Remark = detailRow.Remark;
                            }
                            newDetailTable.AddFormSaleExpenseDetailRow(newDetailRow);
                            totalAmountRMB = totalAmountRMB + newDetailRow.AmountRMB;
                        }

                        this.TAFormSaleExpenseDetail.Update(newDetailTable);
                    }
                } else {
                    foreach (FormDS.FormSaleExpenseDetailRow detailRow in this.FormDataSet.FormSaleExpenseDetail) {
                        if (detailRow.RowState != DataRowState.Deleted) {
                            detailRow.FormSaleApplyID = formApplyRow.FormSaleApplyID;
                            detailRow.AmountRMB = decimal.Round(detailRow.Amount * ExchangeRate, 2);
                            totalAmountRMB = totalAmountRMB + detailRow.AmountRMB;
                        }
                    }
                    this.TAFormSaleExpenseDetail.Update(this.FormDataSet.FormSaleExpenseDetail);
                }


                formApplyRow.AmountRMB = totalAmountRMB;
                TAFormSaleApply.Update(formApplyRow);

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
                    MasterData.CustomerRow customer = new MasterDataBLL().GetCustomerById(CustomerID)[0];
                    dic["CustomerChannel"] = new MasterDataBLL().GetCustomerChannelById(customer.CustomerChannelID)[0].CustomerChannelCode;
                    dic["KAType"] = customer.IsKaTypeNull() ? "" : customer.KaType;
                    dic["IsSample"] = "true";
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

        public void UpdateSaleSampleRequestApply(int FormID, SystemEnums.FormType FormTypeID, SystemEnums.FormStatus StatusID,
                        decimal ExchangeRate, string ShopName, int? ShopCount, string ProjectName, string ProjectDesc,
                        string ApplyFileName, string ApplyRealFileName, DateTime? ActivityBeginDate, DateTime? ActivityEndDate, int? CostCenterID,
                        decimal TotalBudget, decimal ApprovedAmount, decimal ApprovingAmount, decimal CompletedAmount, decimal ReimbursedAmount, decimal RemainBudget, DateTime? ExpectDeliveryDate, string DeliverAddress) {

            SqlTransaction transaction = null;

            try {
                //事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormSaleApply, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormSaleExpenseDetail, transaction);

                FormDS.FormRow formRow = this.TAForm.GetDataByID(FormID)[0];
                FormDS.FormSaleApplyRow formApplyRow = this.TAFormSaleApply.GetDataByID(FormID)[0];

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
                if (ShopName != null)
                    formApplyRow.ShopName = ShopName;
                if (ShopCount != null)
                    formApplyRow.ShopCount = ShopCount.GetValueOrDefault();
                if (ProjectName != null)
                    formApplyRow.ProjectName = ProjectName;
                if (ProjectDesc != null)
                    formApplyRow.ProjectDesc = ProjectDesc;
                if (ApplyFileName != null && ApplyFileName != string.Empty) {
                    formApplyRow.ApplyFileName = ApplyFileName;
                } else {
                    formApplyRow.SetApplyFileNameNull();
                }
                if (ApplyRealFileName != null && ApplyRealFileName != string.Empty) {
                    formApplyRow.ApplyRealFileName = ApplyRealFileName;
                } else {
                    formApplyRow.SetApplyRealFileNameNull();
                }
                if (ActivityBeginDate != null)
                    formApplyRow.ActivityBeginDate = ActivityBeginDate.GetValueOrDefault();
                if (ActivityEndDate != null)
                    formApplyRow.ActivityEndDate = ActivityEndDate.GetValueOrDefault();

                formApplyRow.TotalBudget = TotalBudget;
                formApplyRow.ApprovedAmount = ApprovedAmount;
                formApplyRow.ApprovingAmount = ApprovingAmount;
                formApplyRow.CompletedAmount = CompletedAmount;
                formApplyRow.ReimbursedAmount = ReimbursedAmount;
                formApplyRow.RemainBudget = RemainBudget;

                if (ExpectDeliveryDate != null) {
                    formApplyRow.ExpectDeliveryDate = ExpectDeliveryDate.GetValueOrDefault();
                }
                if (!string.IsNullOrEmpty(DeliverAddress)) {
                    formApplyRow.DeliveryAddress = DeliverAddress;
                }

                this.TAFormSaleApply.Update(formApplyRow);

                //处理明细
                decimal totalAmountRMB = 0;
                foreach (FormDS.FormSaleExpenseDetailRow detailRow in this.FormDataSet.FormSaleExpenseDetail) {
                    if (detailRow.RowState != DataRowState.Deleted) {
                        detailRow.FormSaleApplyID = formApplyRow.FormSaleApplyID;
                        detailRow.AmountRMB = decimal.Round(detailRow.Amount * ExchangeRate, 2);
                        totalAmountRMB = totalAmountRMB + detailRow.AmountRMB;
                    }
                }
                this.TAFormSaleExpenseDetail.Update(this.FormDataSet.FormSaleExpenseDetail);
                formApplyRow.AmountRMB = totalAmountRMB;
                TAFormSaleApply.Update(formApplyRow);

                // 正式提交
                if (StatusID == SystemEnums.FormStatus.Awaiting) {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic["TotalAmount"] = totalAmountRMB;
                    MasterData.CustomerRow customer = new MasterDataBLL().GetCustomerById(formApplyRow.CustomerID)[0];
                    dic["CustomerChannel"] = new MasterDataBLL().GetCustomerChannelById(customer.CustomerChannelID)[0].CustomerChannelCode;
                    dic["KAType"] = customer.IsKaTypeNull() ? "" : customer.KaType;
                    dic["IsSample"] = "true";
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

        public void DeleteSaleSampleRequestApplyByFormID(int FormID) {
            SqlTransaction transaction = null;
            try {
                //事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormSaleApply, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormSaleExpenseDetail, transaction);

                this.TAFormSaleExpenseDetail.DeleteByFormSaleApplyID(FormID);
                this.TAFormSaleApply.DeleteByID(FormID);
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

        public void UpdateSaleApplyAfterDeliveryComplete(int FormID, int UserID) {
            SqlTransaction transaction = null;
            try {
                transaction = TableAdapterHelper.BeginTransaction(TAFormSaleApply);
                TableAdapterHelper.SetTransaction(TAFormSaleExpenseDetail, transaction);
                FormDS.FormSaleApplyRow rowApply = this.TAFormSaleApply.GetDataByID(FormID)[0];
                FormDS.FormDeliveryGoodsDataTable tbDelivery = new FormSaleBLL().GetFormDeliveryGoodByFormID(FormID);
                FormDS.FormSaleExpenseDetailDataTable tbApplyDetail = this.GetFormSaleExpenseDetailByFormSaleApplyID(FormID);
                //发货总金额
                decimal DeliveryAmount = 0;
                //申请总金额
                decimal ApplyAmount = rowApply.AmountRMB;
                //已分配金额，按比例分配的最后一项，要用申请金额-已分配金额
                decimal UsedAmount = 0;
                foreach (FormDS.FormDeliveryGoodsRow item in tbDelivery) {
                    DeliveryAmount += item.AmountRMB;
                }

                if (tbApplyDetail.Count > 0) {
                    FormDS.FormSaleExpenseDetailRow rowExpenseDetail = null;
                    for (int i = 0; i < tbApplyDetail.Count; i++) {
                        rowExpenseDetail = (FormDS.FormSaleExpenseDetailRow)tbApplyDetail[i];
                        if (i == tbApplyDetail.Count - 1) {
                            rowExpenseDetail.GoodAmount = DeliveryAmount - UsedAmount;
                        } else {
                            rowExpenseDetail.GoodAmount = decimal.Round(DeliveryAmount * (rowExpenseDetail.AmountRMB / ApplyAmount), 2);
                            UsedAmount += rowExpenseDetail.GoodAmount;
                        }
                    }
                }
                this.TAFormSaleExpenseDetail.Update(tbApplyDetail);
                rowApply.IsDeliveryComplete = true;
                rowApply.DeliveryCompleteDate = DateTime.Now;
                rowApply.DeliveryCompleteUserID = UserID;
                //rowApply.IsClose = true;
                //rowApply.CloseDate = DateTime.Now;
                this.TAFormSaleApply.Update(rowApply);
                transaction.Commit();
            } catch (Exception) {
                throw;
            }
        }

        #endregion

        #region MarketingSampleRequest

        public void AddMarketingSampleRequestApply(int? RejectedFormID, int UserID, int? ProxyUserID, int? ProxyPositionID, int OrganizationUnitID, int PositionID, SystemEnums.FormType FormTypeID, SystemEnums.FormStatus StatusID,
                        DateTime FPeriod, int BrandID, int CustomerChannelID, int CurrencyID, decimal ExchangeRate, int MarketingProjectID, string ProjectDesc,
                        string ApplyFileName, string ApplyRealFileName, DateTime? ActivityBeginDate, DateTime? ActivityEndDate, int? CostCenterID, int ExpenseSubCategoryID,
                        decimal TotalBudget, decimal ApprovedAmount, decimal ApprovingAmount, decimal ReimbursedAmount, decimal RemainBudget, DateTime? ExpectDeliveryDate, string DeliveryAddress, int CustomerID) {

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
                formRow.PageType = (int)SystemEnums.PageType.MarketingSampleRequest;
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
                formApplyRow.ExpenseSubCategoryID = ExpenseSubCategoryID;
                formApplyRow.ExpenseCategoryID = new MasterDataBLL().GetExpenseSubCategoryById(ExpenseSubCategoryID).ExpenseCategoryID;
                formApplyRow.TotalBudget = TotalBudget;
                formApplyRow.ApprovedAmount = ApprovedAmount;
                formApplyRow.ApprovingAmount = ApprovingAmount;
                formApplyRow.ReimbursedAmount = ReimbursedAmount;
                formApplyRow.RemainBudget = RemainBudget;
                formApplyRow.AmountRMB = 0;
                if (ExpectDeliveryDate != null) {
                    formApplyRow.ExpectDeliveryDate = ExpectDeliveryDate.GetValueOrDefault();
                }
                if (DeliveryAddress != null) {
                    formApplyRow.DeliveryAddress = DeliveryAddress;
                }
                formApplyRow.CustomerID = CustomerID;
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
                    dic["IsSample"] = "true";
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

        public void UpdateMarketingSampleRequestApply(int FormID, SystemEnums.FormType FormTypeID, SystemEnums.FormStatus StatusID,
                        decimal ExchangeRate, int MarketingProjectID, string ProjectDesc,string ApplyFileName, string ApplyRealFileName, DateTime? ActivityBeginDate, DateTime? ActivityEndDate, int? CostCenterID,
                        decimal TotalBudget, decimal ApprovedAmount, decimal ApprovingAmount, decimal ReimbursedAmount, decimal RemainBudget, DateTime? ExpectDeliveryDate, String DeliveryAddress) {

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
                if (ExpectDeliveryDate != null) {
                    formApplyRow.ExpectDeliveryDate = ExpectDeliveryDate.GetValueOrDefault();
                }
                if (DeliveryAddress != null) {
                    formApplyRow.DeliveryAddress = DeliveryAddress;
                }


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
                    dic["IsSample"] = "true";
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

        public void UpdateMarketingApplyAfterDeliveryComplete(int FormID, int UserID) {
            SqlTransaction transaction = null;
            try {
                transaction = TableAdapterHelper.BeginTransaction(TAFormMarketingApply);
                TableAdapterHelper.SetTransaction(TAFormMarketingApplyDetail, transaction);
                FormDS.FormMarketingApplyRow rowApply = this.TAFormMarketingApply.GetDataByID(FormID)[0];
                FormDS.FormDeliveryGoodsDataTable tbDelivery = new FormSaleBLL().GetFormDeliveryGoodByFormID(FormID);
                FormDS.FormMarketingApplyDetailDataTable tbApplyDetail = this.GetFormMarketingApplyDetailByMarketingApplyID(FormID);
                //发货总金额
                decimal DeliveryAmount = 0;
                //申请总金额
                decimal ApplyAmount = rowApply.AmountRMB;
                //已分配金额，按比例分配的最后一项，要用申请金额-已分配金额
                decimal UsedAmount = 0;
                foreach (FormDS.FormDeliveryGoodsRow item in tbDelivery) {
                    DeliveryAmount += item.AmountRMB;
                }

                if (tbApplyDetail.Count > 0) {
                    //FormDS.FormMarketingApplyDetailRow rowExpenseDetail = null;
                    for (int i = 0; i < tbApplyDetail.Count; i++) {
                        FormDS.FormMarketingApplyDetailRow rowExpenseDetail = tbApplyDetail[i];
                        if (i == tbApplyDetail.Count - 1) {
                            rowExpenseDetail.GoodAmount = DeliveryAmount - UsedAmount;
                        } else {
                            rowExpenseDetail.GoodAmount = decimal.Round(DeliveryAmount * (rowExpenseDetail.AmountRMB / ApplyAmount), 2);
                            UsedAmount += rowExpenseDetail.GoodAmount;
                        }
                    }
                }
                this.TAFormMarketingApplyDetail.Update(tbApplyDetail);
                rowApply.IsDeliveryComplete = true;
                rowApply.DeliveryCompleteDate = DateTime.Now;
                rowApply.DeliveryCompleteUserID = UserID;
                //rowApply.IsClose = true;
                //rowApply.CloseDate = DateTime.Now;
                this.TAFormMarketingApply.Update(rowApply);
                transaction.Commit();
            } catch (Exception) {
                throw;
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

        public void AddFormMarketingApplyDetail(int? FormMarketingApplyID, int? SKUID, int ExpenseItemID, string Remark, decimal DeliveryPrice, int DeliveryQuantity, decimal? Amount, decimal? AmountRMB) {

            FormDS.FormMarketingApplyDetailRow rowDetail = this.FormDataSet.FormMarketingApplyDetail.NewFormMarketingApplyDetailRow();
            rowDetail.FormMarketingApplyID = FormMarketingApplyID.GetValueOrDefault();
            rowDetail.ExpenseItemID = ExpenseItemID;
            rowDetail.DeliveryPrice = DeliveryPrice;
            rowDetail.DeliveryQuantity = DeliveryQuantity;
            rowDetail.Amount = Amount.GetValueOrDefault();
            rowDetail.AmountRMB = AmountRMB.GetValueOrDefault();
            rowDetail.Remark = Remark;
            if (SKUID != null) {
                rowDetail.SKUID = SKUID.GetValueOrDefault();
            }
            this.FormDataSet.FormMarketingApplyDetail.AddFormMarketingApplyDetailRow(rowDetail);
        }

        public void UpdateFormMarketingApplyDetail(int FormMarketingApplyDetailID, int? SKUID, int ExpenseItemID, decimal Amount, decimal AmountRMB, string Remark, decimal DeliveryPrice, int DeliveryQuantity) {

            FormDS.FormMarketingApplyDetailRow rowDetail = this.FormDataSet.FormMarketingApplyDetail.FindByFormMarketingApplyDetailID(FormMarketingApplyDetailID);
            if (rowDetail == null)
                return;
            rowDetail.ExpenseItemID = ExpenseItemID;
            rowDetail.DeliveryPrice = DeliveryPrice;
            rowDetail.DeliveryQuantity = DeliveryQuantity;
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

        #region FormRDApply

        public FormDS.FormRDApplyRow GetFormRDApplyByID(int FormRDApplyID) {
            return this.TARDApply.GetDataByID(FormRDApplyID)[0];
        }

        public void AddRDSampleRequestApply(int? RejectedFormID, int UserID, int? ProxyUserID, int? ProxyPositionID, int OrganizationUnitID, int PositionID, SystemEnums.FormType FormTypeID, SystemEnums.FormStatus StatusID,
                       DateTime FPeriod, int BrandID, int CustomerChannelID, int CurrencyID, decimal ExchangeRate, string ProjectName, string ProjectDesc,
                       string ApplyFileName, string ApplyRealFileName, DateTime? ActivityBeginDate, DateTime? ActivityEndDate, int? CostCenterID, int ExpenseSubCategoryID,
                       decimal TotalBudget, decimal ApprovedAmount, decimal ApprovingAmount, decimal ReimbursedAmount, decimal RemainBudget, DateTime? ExpectDeliveryDate, string DeliveryAddress, int CustomerID) {

            SqlTransaction transaction = null;
            try {
                //事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TARDApply, transaction);
                TableAdapterHelper.SetTransaction(this.TARDApplyDetail, transaction);

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
                formRow.PageType = (int)SystemEnums.PageType.RDSampleRequest;
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

                if (ProjectName != null) {
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
                if (ExpectDeliveryDate != null) {
                    formApplyRow.ExpectDeliveryDate = ExpectDeliveryDate.GetValueOrDefault();
                }
                if (DeliveryAddress != null) {
                    formApplyRow.DeliveryAddress = DeliveryAddress;
                }
                formApplyRow.CustomerID = CustomerID;
                formApplyRow.IsClose = false;

                this.FormDataSet.FormRDApply.AddFormRDApplyRow(formApplyRow);
                this.TARDApply.Update(formApplyRow);

                //处理明细
                decimal totalAmountRMB = 0;

                if (RejectedFormID != null) {
                    FormDS.FormRDApplyDetailDataTable newDetailTable = new FormDS.FormRDApplyDetailDataTable();
                    foreach (FormDS.FormRDApplyDetailRow detailRow in this.FormDataSet.FormRDApplyDetail) {
                        if (detailRow.RowState != DataRowState.Deleted) {
                            FormDS.FormRDApplyDetailRow newDetailRow = newDetailTable.NewFormRDApplyDetailRow();
                            newDetailRow.FormRDApplyID = formApplyRow.FormRDApplyID;
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
                        this.TARDApplyDetail.Update(newDetailTable);
                    }
                } else {
                    foreach (FormDS.FormRDApplyDetailRow detailRow in this.FormDataSet.FormRDApplyDetail) {
                        if (detailRow.RowState != DataRowState.Deleted) {
                            detailRow.FormRDApplyID = formApplyRow.FormRDApplyID;
                            detailRow.AmountRMB = decimal.Round(detailRow.Amount * ExchangeRate, 2);
                            totalAmountRMB = totalAmountRMB + detailRow.AmountRMB;
                        }
                    }
                    this.TARDApplyDetail.Update(this.FormDataSet.FormRDApplyDetail);
                }

                formApplyRow.AmountRMB = totalAmountRMB;
                TARDApply.Update(formApplyRow);
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
                    dic["IsSample"] = "true";
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

        public void UpdateRDSampleRequestApply(int FormID, SystemEnums.FormType FormTypeID, SystemEnums.FormStatus StatusID,
                        decimal ExchangeRate, string ProjectName, string ProjectDesc,
                        string ApplyFileName, string ApplyRealFileName, DateTime? ActivityBeginDate, DateTime? ActivityEndDate, int? CostCenterID,
                        decimal TotalBudget, decimal ApprovedAmount, decimal ApprovingAmount, decimal ReimbursedAmount, decimal RemainBudget, DateTime? ExpectDeliveryDate, String DeliveryAddress) {

            SqlTransaction transaction = null;

            try {
                //事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TARDApply, transaction);
                TableAdapterHelper.SetTransaction(this.TARDApplyDetail, transaction);

                FormDS.FormRow formRow = this.TAForm.GetDataByID(FormID)[0];
                FormDS.FormRDApplyRow formApplyRow = this.TARDApply.GetDataByID(FormID)[0];

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
                if (ExpectDeliveryDate != null) {
                    formApplyRow.ExpectDeliveryDate = ExpectDeliveryDate.GetValueOrDefault();
                }
                if (DeliveryAddress != null) {
                    formApplyRow.DeliveryAddress = DeliveryAddress;
                }


                this.TARDApply.Update(formApplyRow);

                //处理明细
                decimal totalAmountRMB = 0;
                foreach (FormDS.FormRDApplyDetailRow detailRow in this.FormDataSet.FormRDApplyDetail) {
                    if (detailRow.RowState != DataRowState.Deleted) {
                        detailRow.FormRDApplyID = formApplyRow.FormRDApplyID;
                        detailRow.AmountRMB = decimal.Round(detailRow.Amount * ExchangeRate, 2);
                        totalAmountRMB = totalAmountRMB + detailRow.AmountRMB;
                    }
                }
                this.TARDApplyDetail.Update(this.FormDataSet.FormRDApplyDetail);
                formApplyRow.AmountRMB = totalAmountRMB;
                TARDApply.Update(formApplyRow);

                // 正式提交
                if (StatusID == SystemEnums.FormStatus.Awaiting) {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic["TotalAmount"] = totalAmountRMB;
                    dic["IsSample"] = "true";
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


        public void UpdateRDApplyAfterDeliveryComplete(int FormRDApplyID, int UserID) {
            SqlTransaction transaction = null;
            try {
                transaction = TableAdapterHelper.BeginTransaction(TAFormMarketingApply);
                TableAdapterHelper.SetTransaction(TAFormMarketingApplyDetail, transaction);
                FormDS.FormRDApplyRow rowApply = this.TARDApply.GetDataByID(FormRDApplyID)[0];
                FormDS.FormDeliveryGoodsDataTable tbDelivery = new FormSaleBLL().GetFormDeliveryGoodByFormID(FormRDApplyID);
                FormDS.FormRDApplyDetailDataTable tbApplyDetail = this.GetFormRDApplyDetailByApplyID(FormRDApplyID);
                //发货总金额
                decimal DeliveryAmount = 0;
                //申请总金额
                decimal ApplyAmount = rowApply.AmountRMB;
                //已分配金额，按比例分配的最后一项，要用申请金额-已分配金额
                decimal UsedAmount = 0;
                foreach (FormDS.FormDeliveryGoodsRow item in tbDelivery) {
                    DeliveryAmount += item.AmountRMB;
                }

                if (tbApplyDetail.Count > 0) {
                    FormDS.FormRDApplyDetailRow rowExpenseDetail = null;
                    for (int i = 0; i < tbApplyDetail.Count; i++) {
                        rowExpenseDetail = (FormDS.FormRDApplyDetailRow)tbApplyDetail[i];
                        if (i == tbApplyDetail.Count - 1) {
                            rowExpenseDetail.GoodAmount = DeliveryAmount - UsedAmount;
                        } else {
                            rowExpenseDetail.GoodAmount = decimal.Round(DeliveryAmount * (rowExpenseDetail.AmountRMB / ApplyAmount), 2);
                            UsedAmount += rowExpenseDetail.GoodAmount;
                        }
                    }
                }
                this.TARDApplyDetail.Update(tbApplyDetail);
                rowApply.IsDeliveryComplete = true;
                rowApply.DeliveryCompleteDate = DateTime.Now;
                rowApply.DeliveryCompleteUserID = UserID;
                //rowApply.IsClose = true;
                //rowApply.CloseDate = DateTime.Now;
                this.TARDApply.Update(rowApply);
                transaction.Commit();
            } catch (Exception) {
                throw;
            }
        }

        #endregion

        #region FormRDApplyDetail

        public FormDS.FormRDApplyDetailDataTable GetFormRDApplyDetail() {
            return this.FormDataSet.FormRDApplyDetail;
        }

        public FormDS.FormRDApplyDetailRow GetFormRDApplyDetailByID(int FormRDApplyDetaiID) {
            return this.TARDApplyDetail.GetDataByID(FormRDApplyDetaiID)[0];
        }

        public FormDS.FormRDApplyDetailDataTable GetFormRDApplyDetailByApplyID(int FormRDApplyID) {
            return this.TARDApplyDetail.GetDataByRDApplyID(FormRDApplyID);
        }

        public void AddFormRDApplyDetail(int? FormRDApplyID, int? SKUID, int ExpenseItemID, string Remark, decimal DeliveryPrice, int DeliveryQuantity, decimal? Amount, decimal? AmountRMB) {

            FormDS.FormRDApplyDetailRow rowDetail = this.FormDataSet.FormRDApplyDetail.NewFormRDApplyDetailRow();
            rowDetail.FormRDApplyID = FormRDApplyID.GetValueOrDefault();
            rowDetail.ExpenseItemID = ExpenseItemID;
            rowDetail.DeliveryPrice = DeliveryPrice;
            rowDetail.DeliveryQuantity = DeliveryQuantity;
            rowDetail.Amount = Amount.GetValueOrDefault();
            rowDetail.AmountRMB = AmountRMB.GetValueOrDefault();
            rowDetail.Remark = Remark;
            if (SKUID != null) {
                rowDetail.SKUID = SKUID.GetValueOrDefault();
            }
            this.FormDataSet.FormRDApplyDetail.AddFormRDApplyDetailRow(rowDetail);
        }

        public void UpdateFormRDApplyDetail(int FormRDApplyDetailID, int? SKUID, int ExpenseItemID, decimal Amount, decimal AmountRMB, string Remark, decimal DeliveryPrice, int DeliveryQuantity) {

            FormDS.FormRDApplyDetailRow rowDetail = this.FormDataSet.FormRDApplyDetail.FindByFormRDApplyDetailID(FormRDApplyDetailID);
            if (rowDetail == null)
                return;
            rowDetail.ExpenseItemID = ExpenseItemID;
            rowDetail.DeliveryPrice = DeliveryPrice;
            rowDetail.DeliveryQuantity = DeliveryQuantity;
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

        public void UpdateFormAfterDeliveryComplete(int FormID, int StuffUserID) {
            FormDS.FormRow rowForm = this.GetFormByID(FormID)[0];
            switch (rowForm.PageType) {
                case (int)SystemEnums.PageType.PaymentFreeGoods:
                    new FormSaleBLL().UpdatePaymentAfterDeliveryComplete(FormID, StuffUserID);
                    break;
                case (int)SystemEnums.PageType.SaleSampleRequest:
                    this.UpdateSaleApplyAfterDeliveryComplete(FormID, StuffUserID);
                    break;
                case (int)SystemEnums.PageType.RDSampleRequest:
                    this.UpdateRDApplyAfterDeliveryComplete(FormID, StuffUserID);
                    break;
                case (int)SystemEnums.PageType.MarketingSampleRequest:
                    this.UpdateMarketingApplyAfterDeliveryComplete(FormID, StuffUserID);
                    break;
            }
        }
    }
}
