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
    public class FormSaleBLL {

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

        private FormSaleApplyTableAdapter m_FormSaleApplyAdapter;
        public FormSaleApplyTableAdapter TAFormSaleApply {
            get {
                if (this.m_FormSaleApplyAdapter == null) {
                    this.m_FormSaleApplyAdapter = new FormSaleApplyTableAdapter();
                }
                return this.m_FormSaleApplyAdapter;
            }
        }

        private FormSaleSKUDetailTableAdapter m_FormSaleSKUDetailAdapter;
        public FormSaleSKUDetailTableAdapter TAFormSaleSKUDetail {
            get {
                if (this.m_FormSaleSKUDetailAdapter == null) {
                    this.m_FormSaleSKUDetailAdapter = new FormSaleSKUDetailTableAdapter();
                }
                return this.m_FormSaleSKUDetailAdapter;
            }
        }

        private FormSaleExpenseDetailTableAdapter m_FormSaleExpenseDetailAdapter;
        public FormSaleExpenseDetailTableAdapter TAFormSaleExpenseDetail {
            get {
                if (this.m_FormSaleExpenseDetailAdapter == null) {
                    this.m_FormSaleExpenseDetailAdapter = new FormSaleExpenseDetailTableAdapter();
                }
                return this.m_FormSaleExpenseDetailAdapter;
            }
        }

        private FormSaleSettlementTableAdapter m_FormSaleSettlementAdapter;
        public FormSaleSettlementTableAdapter TAFormSaleSettlement {
            get {
                if (this.m_FormSaleSettlementAdapter == null) {
                    this.m_FormSaleSettlementAdapter = new FormSaleSettlementTableAdapter();
                }
                return this.m_FormSaleSettlementAdapter;
            }
        }

        private FormSettlementExpenseDetailTableAdapter m_FormSettlementExpenseDetailAdapter;
        public FormSettlementExpenseDetailTableAdapter TAFormSettlementExpenseDetail {
            get {
                if (this.m_FormSettlementExpenseDetailAdapter == null) {
                    this.m_FormSettlementExpenseDetailAdapter = new FormSettlementExpenseDetailTableAdapter();
                }
                return this.m_FormSettlementExpenseDetailAdapter;
            }
        }

        private FormSettlementSKUDetailTableAdapter m_FormSettlementSKUDetailAdapter;
        public FormSettlementSKUDetailTableAdapter TAFormSettlementSKUDetail {
            get {
                if (this.m_FormSettlementSKUDetailAdapter == null) {
                    this.m_FormSettlementSKUDetailAdapter = new FormSettlementSKUDetailTableAdapter();
                }
                return this.m_FormSettlementSKUDetailAdapter;
            }
        }

        private FormSalePaymentTableAdapter m_FormSalePaymentAdapter;
        public FormSalePaymentTableAdapter TAFormSalePayment {
            get {
                if (this.m_FormSalePaymentAdapter == null) {
                    this.m_FormSalePaymentAdapter = new FormSalePaymentTableAdapter();
                }
                return this.m_FormSalePaymentAdapter;
            }
        }

        private FormSalePaymentDetailTableAdapter m_FormSalePaymentDetailAdapter;
        public FormSalePaymentDetailTableAdapter TAFormSalePaymentDetail {
            get {
                if (this.m_FormSalePaymentDetailAdapter == null) {
                    this.m_FormSalePaymentDetailAdapter = new FormSalePaymentDetailTableAdapter();
                }
                return this.m_FormSalePaymentDetailAdapter;
            }
        }

        private FormSalePaymentFreeGoodsTableAdapter m_FormSalePaymentFreeGoodsAdapter;
        public FormSalePaymentFreeGoodsTableAdapter TAFormSalePaymentFreeGoods {
            get {
                if (this.m_FormSalePaymentFreeGoodsAdapter == null) {
                    this.m_FormSalePaymentFreeGoodsAdapter = new FormSalePaymentFreeGoodsTableAdapter();
                }
                return this.m_FormSalePaymentFreeGoodsAdapter;
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

        private SaleExpenseSummaryTableAdapter m_SaleExpenseSummaryAdapter;
        public SaleExpenseSummaryTableAdapter TASaleExpenseSummary {
            get {
                if (this.m_SaleExpenseSummaryAdapter == null) {
                    this.m_SaleExpenseSummaryAdapter = new SaleExpenseSummaryTableAdapter();
                }
                return this.m_SaleExpenseSummaryAdapter;
            }
        }

        private FormDeliveryGoodsTableAdapter m_FormDeliveryGoodsAdapter;
        public FormDeliveryGoodsTableAdapter TAFormDeliveryGoods {
            get {
                if (this.m_FormDeliveryGoodsAdapter == null) {
                    this.m_FormDeliveryGoodsAdapter = new FormDeliveryGoodsTableAdapter();
                }
                return this.m_FormDeliveryGoodsAdapter;
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

        public FormDS.SaleExpenseSummaryDataTable GetSaleExpenseSummaryByFormID(int FormSaleApplyID) {
            return this.TASaleExpenseSummary.GetData(FormSaleApplyID);
        }

        #endregion

        #region SaleApply General

        public string GetPaymentFormNoBySaleApplyID(int FormSaleApplyID) {
            string PaymentFormNo = (string)this.TAFormSaleApply.GetPaymentFormNoBySaleApplyID(FormSaleApplyID);
            if (PaymentFormNo == null) {
                PaymentFormNo = "";
            }
            return PaymentFormNo;
        }


        public string GetSettledFormNoBySaleApplyID(int FormSaleApplyID) {
            string SettledFormNo = (string)this.TAFormSaleApply.GetSettledFormNoBySaleApplyID(FormSaleApplyID);
            if (SettledFormNo == null) {
                SettledFormNo = "";
            }
            return SettledFormNo;
        }

        public int GetValidSaleSettlementIDBySaleApplyID(int FormSaleApplyID) {
            return this.TAFormSaleApply.GetValidSaleSettlementIDBySaleApplyID(FormSaleApplyID.ToString()).GetValueOrDefault();
        }

        #endregion

        #region FormSaleSKUDetail

        public FormDS.FormSaleSKUDetailDataTable GetFormSaleSKUDetail() {
            return this.FormDataSet.FormSaleSKUDetail;
        }

        public FormDS.FormSaleSKUDetailDataTable GetFormSaleSKUDetailByFormSaleApplyID(int FormSaleApplyID) {
            return this.TAFormSaleSKUDetail.GetDataByFormSaleApplyID(FormSaleApplyID);
        }

        public void AddFormSaleSKUDetail(int? FormSaleApplyID, int SKUID, decimal? Discount, decimal? DiscountCampbell, decimal ForecastSaleQuantity, decimal ForecastOrderQuantity, decimal DeliveryPrice,
            decimal ForecastOrderAmount, decimal? PriceDiscountAmount, decimal? PriceDiscountAmountRMB, string Remark) {

            FormDS.FormSaleSKUDetailRow rowDetail = this.FormDataSet.FormSaleSKUDetail.NewFormSaleSKUDetailRow();
            rowDetail.FormSaleApplyID = FormSaleApplyID.GetValueOrDefault();
            rowDetail.SKUID = SKUID;
            rowDetail.Discount = Discount.GetValueOrDefault();
            rowDetail.DiscountCampbell = DiscountCampbell.GetValueOrDefault();
            rowDetail.ForecastSaleQuantity = ForecastSaleQuantity;
            rowDetail.ForecastOrderQuantity = ForecastOrderQuantity;
            rowDetail.DeliveryPrice = DeliveryPrice;
            rowDetail.ForecastOrderAmount = ForecastOrderQuantity * DeliveryPrice;
            rowDetail.PriceDiscountAmount = PriceDiscountAmount.GetValueOrDefault();
            rowDetail.PriceDiscountAmountRMB = PriceDiscountAmountRMB.GetValueOrDefault();
            rowDetail.Remark = Remark;
            this.FormDataSet.FormSaleSKUDetail.AddFormSaleSKUDetailRow(rowDetail);
        }

        public void UpdateFormSaleSKUDetail(int FormSaleSKUDetailID, int SKUID, decimal? Discount, decimal? DiscountCampbell, decimal ForecastSaleQuantity, decimal ForecastOrderQuantity, decimal DeliveryPrice,
            decimal ForecastOrderAmount, decimal? PriceDiscountAmount, decimal? PriceDiscountAmountRMB, string Remark) {

            FormDS.FormSaleSKUDetailRow rowDetail = this.FormDataSet.FormSaleSKUDetail.FindByFormSaleSKUDetailID(FormSaleSKUDetailID);
            if (rowDetail == null)
                return;
            rowDetail.SKUID = SKUID;
            rowDetail.Discount = Discount.GetValueOrDefault();
            rowDetail.DiscountCampbell = DiscountCampbell.GetValueOrDefault();
            rowDetail.ForecastSaleQuantity = ForecastSaleQuantity;
            rowDetail.ForecastOrderQuantity = ForecastOrderQuantity;
            rowDetail.DeliveryPrice = DeliveryPrice;
            rowDetail.ForecastOrderAmount = ForecastOrderQuantity * DeliveryPrice;
            rowDetail.PriceDiscountAmount = PriceDiscountAmount.GetValueOrDefault();
            rowDetail.PriceDiscountAmountRMB = PriceDiscountAmountRMB.GetValueOrDefault();
            rowDetail.Remark = Remark;
        }

        public void DeleteFormSaleSKUDetailByID(int FormSaleSKUDetailID) {
            for (int index = 0; index < this.FormDataSet.FormSaleSKUDetail.Count; index++) {
                if ((int)this.FormDataSet.FormSaleSKUDetail.Rows[index]["FormSaleSKUDetailID"] == FormSaleSKUDetailID) {
                    this.FormDataSet.FormSaleSKUDetail.Rows[index].Delete();
                    break;
                }
            }
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

        public void AddFormSaleExpenseDetail(int? FormSaleApplyID, int? SKUID, int ExpenseItemID, string ShopName, decimal Amount, decimal AmountRMB, string Remark) {

            FormDS.FormSaleExpenseDetailRow rowDetail = this.FormDataSet.FormSaleExpenseDetail.NewFormSaleExpenseDetailRow();
            rowDetail.FormSaleApplyID = FormSaleApplyID.GetValueOrDefault();
            rowDetail.ExpenseItemID = ExpenseItemID;
            rowDetail.ShopName = ShopName;
            rowDetail.Amount = Amount;
            rowDetail.AmountRMB = AmountRMB;
            rowDetail.Remark = Remark;
            if (SKUID != null) {
                rowDetail.SKUID = SKUID.GetValueOrDefault();
            }
            this.FormDataSet.FormSaleExpenseDetail.AddFormSaleExpenseDetailRow(rowDetail);
        }

        public void UpdateFormSaleExpenseDetail(int FormSaleExpenseDetailID, int? SKUID, int ExpenseItemID, string ShopName, decimal Amount, decimal AmountRMB, string Remark) {

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

        #region Activity Apply

        public void AddSaleActivityApply(int? RejectedFormID, int UserID, int? ProxyUserID, int? ProxyPositionID, int OrganizationUnitID, int PositionID, SystemEnums.FormType FormTypeID, SystemEnums.FormStatus StatusID,
                        DateTime FPeriod, int CustomerID, int BrandID, int ExpenseSubCategoryID, int CurrencyID, decimal ExchangeRate, string ShopName, int? ShopCount, string ProjectName, string ProjectDesc,
                        string ApplyFileName, string ApplyRealFileName, int? DisplayTypeID, decimal? DisplayArea, bool IsDM, int? DiscountTypeID, DateTime? ActivityBeginDate, DateTime? ActivityEndDate,
                        DateTime? DeliveryBeginDate, DateTime? DeliveryEndDate, int? CostCenterID, decimal TotalBudget, decimal ApprovedAmount, decimal ApprovingAmount, decimal CompletedAmount, decimal ReimbursedAmount, decimal RemainBudget) {

            SqlTransaction transaction = null;

            try {
                //事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormSaleApply, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormSaleSKUDetail, transaction);
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
                formRow.PageType = (int)SystemEnums.PageType.ActivityApply;
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
                if (DisplayTypeID != null)
                    formApplyRow.DisplayTypeID = DisplayTypeID.GetValueOrDefault();
                if (DisplayArea != null)
                    formApplyRow.DisplayArea = DisplayArea.GetValueOrDefault();
                formApplyRow.IsDM = IsDM;
                if (DiscountTypeID != null)
                    formApplyRow.DiscountTypeID = DiscountTypeID.GetValueOrDefault();
                if (ActivityBeginDate != null)
                    formApplyRow.ActivityBeginDate = ActivityBeginDate.GetValueOrDefault();
                if (ActivityEndDate != null)
                    formApplyRow.ActivityEndDate = ActivityEndDate.GetValueOrDefault();
                if (DeliveryBeginDate != null)
                    formApplyRow.DeliveryBeginDate = DeliveryBeginDate.GetValueOrDefault();
                if (DeliveryEndDate != null)
                    formApplyRow.DeliveryEndDate = DeliveryEndDate.GetValueOrDefault();

                formApplyRow.TotalBudget = TotalBudget;
                formApplyRow.ApprovedAmount = ApprovedAmount;
                formApplyRow.ApprovingAmount = ApprovingAmount;
                formApplyRow.CompletedAmount = CompletedAmount;
                formApplyRow.ReimbursedAmount = ReimbursedAmount;
                formApplyRow.RemainBudget = RemainBudget;
                formApplyRow.AmountRMB = 0;//暂时
                formApplyRow.IsClose = false;
                formApplyRow.IsCompleted = false;

                this.FormDataSet.FormSaleApply.AddFormSaleApplyRow(formApplyRow);
                this.TAFormSaleApply.Update(formApplyRow);

                //处理明细,先产品表
                decimal totalPriceDiscountAmountRMB = 0;
                decimal totalForecastOrderAmount = 0;
                decimal maxDiscount = 0;//找到最大的折扣
                if (RejectedFormID != null) {
                    FormDS.FormSaleSKUDetailDataTable newSKUDetailTable = new FormDS.FormSaleSKUDetailDataTable();
                    foreach (FormDS.FormSaleSKUDetailRow skuRow in this.FormDataSet.FormSaleSKUDetail) {
                        if (skuRow.RowState != DataRowState.Deleted) {
                            FormDS.FormSaleSKUDetailRow newSKURow = newSKUDetailTable.NewFormSaleSKUDetailRow();
                            newSKURow.FormSaleApplyID = formApplyRow.FormSaleApplyID;
                            newSKURow.SKUID = skuRow.SKUID;
                            if (!skuRow.IsDiscountNull()) {
                                newSKURow.Discount = skuRow.Discount;
                                if (skuRow.Discount > maxDiscount) {
                                    maxDiscount = skuRow.Discount;
                                }
                            }
                            if (!skuRow.IsDiscountCampbellNull()) {
                                newSKURow.DiscountCampbell = skuRow.DiscountCampbell;
                            }
                            newSKURow.ForecastSaleQuantity = skuRow.ForecastSaleQuantity;
                            newSKURow.ForecastOrderQuantity = skuRow.ForecastOrderQuantity;
                            newSKURow.DeliveryPrice = skuRow.DeliveryPrice;
                            newSKURow.ForecastOrderAmount = skuRow.ForecastOrderAmount;
                            if (!skuRow.IsPriceDiscountAmountNull()) {
                                newSKURow.PriceDiscountAmount = skuRow.PriceDiscountAmount;
                                newSKURow.PriceDiscountAmountRMB = decimal.Round(skuRow.PriceDiscountAmount * ExchangeRate, 2);
                            } else {
                                newSKURow.PriceDiscountAmount = 0;
                                newSKURow.PriceDiscountAmountRMB = 0;
                            }
                            if (!skuRow.IsRemarkNull()) {
                                newSKURow.Remark = skuRow.Remark;
                            }
                            newSKUDetailTable.AddFormSaleSKUDetailRow(newSKURow);
                            totalPriceDiscountAmountRMB = totalPriceDiscountAmountRMB + newSKURow.PriceDiscountAmountRMB;
                            totalForecastOrderAmount = totalForecastOrderAmount + newSKURow.ForecastOrderAmount;
                            //如果有折扣的话，要生成费用项
                            if (newSKURow.PriceDiscountAmountRMB != 0) {
                                if (StatusID == SystemEnums.FormStatus.Awaiting && new ExpenseItemTableAdapter().GetPriceDiscountExpenseItem().Count != 0) {
                                    FormDS.FormSaleExpenseDetailRow newExpenseDetail = this.FormDataSet.FormSaleExpenseDetail.NewFormSaleExpenseDetailRow();
                                    newExpenseDetail.FormSaleApplyID = formApplyRow.FormSaleApplyID;
                                    newExpenseDetail.ExpenseItemID = new ExpenseItemTableAdapter().GetPriceDiscountExpenseItem()[0].ExpenseItemID;
                                    newExpenseDetail.SKUID = newSKURow.SKUID;
                                    newExpenseDetail.Amount = newSKURow.PriceDiscountAmount;
                                    newExpenseDetail.AmountRMB = decimal.Round(newSKURow.PriceDiscountAmount * ExchangeRate, 2);
                                    if (!newSKURow.IsRemarkNull()) {
                                        newExpenseDetail.Remark = newSKURow.Remark;
                                    }
                                    this.FormDataSet.FormSaleExpenseDetail.AddFormSaleExpenseDetailRow(newExpenseDetail);
                                }
                            }
                        }
                        this.TAFormSaleSKUDetail.Update(newSKUDetailTable);
                    }
                } else {
                    foreach (FormDS.FormSaleSKUDetailRow skuRow in this.FormDataSet.FormSaleSKUDetail) {
                        if (skuRow.RowState != DataRowState.Deleted) {
                            skuRow.FormSaleApplyID = formApplyRow.FormSaleApplyID;
                            skuRow.PriceDiscountAmountRMB = decimal.Round(skuRow.PriceDiscountAmount * ExchangeRate, 2);
                            totalPriceDiscountAmountRMB = totalPriceDiscountAmountRMB + skuRow.PriceDiscountAmountRMB;
                            totalForecastOrderAmount = totalForecastOrderAmount + skuRow.ForecastOrderAmount;
                            if (skuRow.Discount > maxDiscount) {
                                maxDiscount = skuRow.Discount;
                            }
                            //如果有折扣的话，要生成费用项
                            if (skuRow.PriceDiscountAmountRMB != 0) {
                                if (StatusID == SystemEnums.FormStatus.Awaiting && new ExpenseItemTableAdapter().GetPriceDiscountExpenseItem().Count != 0) {
                                    FormDS.FormSaleExpenseDetailRow newExpenseDetail = this.FormDataSet.FormSaleExpenseDetail.NewFormSaleExpenseDetailRow();
                                    newExpenseDetail.FormSaleApplyID = formApplyRow.FormSaleApplyID;
                                    newExpenseDetail.ExpenseItemID = new ExpenseItemTableAdapter().GetPriceDiscountExpenseItem()[0].ExpenseItemID;
                                    newExpenseDetail.SKUID = skuRow.SKUID;
                                    newExpenseDetail.Amount = skuRow.PriceDiscountAmount;
                                    newExpenseDetail.AmountRMB = decimal.Round(skuRow.PriceDiscountAmount * ExchangeRate, 2);
                                    if (!skuRow.IsRemarkNull()) {
                                        newExpenseDetail.Remark = skuRow.Remark;
                                    }
                                    this.FormDataSet.FormSaleExpenseDetail.AddFormSaleExpenseDetailRow(newExpenseDetail);
                                }
                            }
                        }
                    }
                    this.TAFormSaleSKUDetail.Update(this.FormDataSet.FormSaleSKUDetail);
                }
                //费用项
                decimal totalAmountRMB = 0;
                if (RejectedFormID != null) {
                    FormDS.FormSaleExpenseDetailDataTable newExpenseDetailTable = new FormDS.FormSaleExpenseDetailDataTable();
                    foreach (FormDS.FormSaleExpenseDetailRow expenseRow in this.FormDataSet.FormSaleExpenseDetail) {
                        if (expenseRow.RowState != DataRowState.Deleted) {
                            FormDS.FormSaleExpenseDetailRow newExpenseRow = newExpenseDetailTable.NewFormSaleExpenseDetailRow();
                            newExpenseRow.FormSaleApplyID = formApplyRow.FormSaleApplyID;
                            newExpenseRow.ExpenseItemID = expenseRow.ExpenseItemID;
                            if (!expenseRow.IsShopNameNull()) {
                                newExpenseRow.ShopName = expenseRow.ShopName;
                            }
                            if (!expenseRow.IsSKUIDNull()) {
                                newExpenseRow.SKUID = expenseRow.SKUID;
                            }
                            newExpenseRow.Amount = expenseRow.Amount;
                            newExpenseRow.AmountRMB = decimal.Round(expenseRow.Amount * ExchangeRate, 2);
                            if (!expenseRow.IsRemarkNull()) {
                                newExpenseRow.Remark = expenseRow.Remark;
                            }
                            newExpenseDetailTable.AddFormSaleExpenseDetailRow(newExpenseRow);
                            totalAmountRMB = totalAmountRMB + newExpenseRow.AmountRMB;
                        }
                    }
                    this.TAFormSaleExpenseDetail.Update(newExpenseDetailTable);

                } else {
                    foreach (FormDS.FormSaleExpenseDetailRow expenseRow in this.FormDataSet.FormSaleExpenseDetail) {
                        if (expenseRow.RowState != DataRowState.Deleted) {
                            expenseRow.FormSaleApplyID = formApplyRow.FormSaleApplyID;
                            expenseRow.AmountRMB = decimal.Round(expenseRow.Amount * ExchangeRate, 2);
                            totalAmountRMB = totalAmountRMB + expenseRow.AmountRMB;
                        }
                    }
                    this.TAFormSaleExpenseDetail.Update(this.FormDataSet.FormSaleExpenseDetail);
                }

                formApplyRow.PriceDiscountAmountRMB = totalPriceDiscountAmountRMB;
                formApplyRow.AmountRMB = totalAmountRMB;
                formApplyRow.OtherAmountRMB = totalAmountRMB - totalPriceDiscountAmountRMB;
                formApplyRow.ForecastOrderAmount = totalForecastOrderAmount;
                if (totalForecastOrderAmount != 0) {
                    formApplyRow.CostBenefitRate = totalAmountRMB / totalForecastOrderAmount * 100;
                } else {
                    formApplyRow.CostBenefitRate = 0;
                }
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
                    //获取Customer的信息
                    MasterData.CustomerRow customer = new MasterDataBLL().GetCustomerById(CustomerID)[0];
                    dic["CustomerChannel"] = new MasterDataBLL().GetCustomerChannelById(customer.CustomerChannelID)[0].CustomerChannelCode;
                    dic["KAType"] = customer.IsKaTypeNull() ? "" : customer.KaType;
                    dic["MaxDiscount"] = maxDiscount;
                    dic["DiscountType"] = DiscountTypeID;

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

        public void UpdateSaleActivityApply(int FormID, SystemEnums.FormType FormTypeID, SystemEnums.FormStatus StatusID, decimal ExchangeRate, string ShopName, int? ShopCount, string ProjectName, string ProjectDesc,
                        string ApplyFileName, string ApplyRealFileName, int? DisplayTypeID, decimal? DisplayArea, bool IsDM, int? DiscountTypeID, DateTime? ActivityBeginDate, DateTime? ActivityEndDate,
                        DateTime? DeliveryBeginDate, DateTime? DeliveryEndDate, int? CostCenterID, decimal TotalBudget, decimal ApprovedAmount, decimal ApprovingAmount, decimal CompletedAmount, decimal ReimbursedAmount, decimal RemainBudget) {

            SqlTransaction transaction = null;

            try {
                //事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormSaleApply, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormSaleSKUDetail, transaction);
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
                if (DisplayTypeID != null)
                    formApplyRow.DisplayTypeID = DisplayTypeID.GetValueOrDefault();
                if (DisplayArea != null)
                    formApplyRow.DisplayArea = DisplayArea.GetValueOrDefault();
                formApplyRow.IsDM = IsDM;
                if (DiscountTypeID != null)
                    formApplyRow.DiscountTypeID = DiscountTypeID.GetValueOrDefault();
                if (ActivityBeginDate != null)
                    formApplyRow.ActivityBeginDate = ActivityBeginDate.GetValueOrDefault();
                if (ActivityEndDate != null)
                    formApplyRow.ActivityEndDate = ActivityEndDate.GetValueOrDefault();
                if (DeliveryBeginDate != null)
                    formApplyRow.DeliveryBeginDate = DeliveryBeginDate.GetValueOrDefault();
                if (DeliveryEndDate != null)
                    formApplyRow.DeliveryEndDate = DeliveryEndDate.GetValueOrDefault();

                formApplyRow.TotalBudget = TotalBudget;
                formApplyRow.ApprovedAmount = ApprovedAmount;
                formApplyRow.ApprovingAmount = ApprovingAmount;
                formApplyRow.CompletedAmount = CompletedAmount;
                formApplyRow.ReimbursedAmount = ReimbursedAmount;
                formApplyRow.RemainBudget = RemainBudget;

                this.TAFormSaleApply.Update(formApplyRow);

                //处理明细,先产品表
                decimal totalPriceDiscountAmountRMB = 0;//
                decimal totalForecastOrderAmount = 0;
                decimal maxDiscount = 0;
                foreach (FormDS.FormSaleSKUDetailRow skuRow in this.FormDataSet.FormSaleSKUDetail) {
                    if (skuRow.RowState != DataRowState.Deleted) {
                        skuRow.FormSaleApplyID = formApplyRow.FormSaleApplyID;
                        skuRow.PriceDiscountAmountRMB = decimal.Round(skuRow.PriceDiscountAmount * ExchangeRate, 2);
                        totalPriceDiscountAmountRMB = totalPriceDiscountAmountRMB + skuRow.PriceDiscountAmountRMB;
                        totalForecastOrderAmount = totalForecastOrderAmount + skuRow.ForecastOrderAmount;
                        if (skuRow.Discount > maxDiscount) {
                            maxDiscount = skuRow.Discount;
                        }
                        //如果有折扣的话，要生成费用项
                        if (!skuRow.IsPriceDiscountAmountRMBNull()) {
                            if (StatusID == SystemEnums.FormStatus.Awaiting && new ExpenseItemTableAdapter().GetPriceDiscountExpenseItem().Count != 0) {
                                FormDS.FormSaleExpenseDetailRow newExpenseDetail = this.FormDataSet.FormSaleExpenseDetail.NewFormSaleExpenseDetailRow();
                                newExpenseDetail.FormSaleApplyID = formApplyRow.FormSaleApplyID;
                                newExpenseDetail.ExpenseItemID = new ExpenseItemTableAdapter().GetPriceDiscountExpenseItem()[0].ExpenseItemID;
                                newExpenseDetail.SKUID = skuRow.SKUID;
                                newExpenseDetail.Amount = skuRow.PriceDiscountAmount;
                                newExpenseDetail.AmountRMB = decimal.Round(skuRow.PriceDiscountAmount * ExchangeRate, 2);
                                if (!skuRow.IsRemarkNull()) {
                                    newExpenseDetail.Remark = skuRow.Remark;
                                }
                                this.FormDataSet.FormSaleExpenseDetail.AddFormSaleExpenseDetailRow(newExpenseDetail);
                            }
                        }
                    }
                }
                this.TAFormSaleSKUDetail.Update(this.FormDataSet.FormSaleSKUDetail);
                //费用项
                decimal totalAmountRMB = 0;
                foreach (FormDS.FormSaleExpenseDetailRow expenseRow in this.FormDataSet.FormSaleExpenseDetail) {
                    if (expenseRow.RowState != DataRowState.Deleted) {
                        expenseRow.FormSaleApplyID = formApplyRow.FormSaleApplyID;
                        expenseRow.AmountRMB = decimal.Round(expenseRow.Amount * ExchangeRate, 2);
                        totalAmountRMB = totalAmountRMB + expenseRow.AmountRMB;
                    }
                }
                this.TAFormSaleExpenseDetail.Update(this.FormDataSet.FormSaleExpenseDetail);

                formApplyRow.PriceDiscountAmountRMB = totalPriceDiscountAmountRMB;
                formApplyRow.AmountRMB = totalAmountRMB;
                formApplyRow.OtherAmountRMB = totalAmountRMB - totalPriceDiscountAmountRMB;
                formApplyRow.ForecastOrderAmount = totalForecastOrderAmount;
                if (totalForecastOrderAmount != 0) {
                    formApplyRow.CostBenefitRate = totalAmountRMB / totalForecastOrderAmount * 100;
                } else {
                    formApplyRow.CostBenefitRate = 0;
                }
                TAFormSaleApply.Update(formApplyRow);

                // 正式提交
                if (StatusID == SystemEnums.FormStatus.Awaiting) {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic["TotalAmount"] = totalAmountRMB;
                    MasterData.CustomerRow customer = new MasterDataBLL().GetCustomerById(formApplyRow.CustomerID)[0];
                    dic["CustomerChannel"] = new MasterDataBLL().GetCustomerChannelById(customer.CustomerChannelID)[0].CustomerChannelCode;
                    dic["KAType"] = customer.IsKaTypeNull() ? "" : customer.KaType;
                    dic["MaxDiscount"] = maxDiscount;
                    dic["DiscountType"] = DiscountTypeID;
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

        public void DeleteSaleActivityApplyByFormID(int FormID) {
            SqlTransaction transaction = null;
            try {
                //事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormSaleApply, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormSaleSKUDetail, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormSaleExpenseDetail, transaction);

                this.TAFormSaleExpenseDetail.DeleteByFormSaleApplyID(FormID);
                this.TAFormSaleSKUDetail.DeleteByFormSaleApplyID(FormID);
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

        #endregion

        #region NoActivity Apply

        public void AddSaleNoActivityApply(int? RejectedFormID, int UserID, int? ProxyUserID, int? ProxyPositionID, int OrganizationUnitID, int PositionID, SystemEnums.FormType FormTypeID, SystemEnums.FormStatus StatusID,
                        DateTime FPeriod, int CustomerID, int BrandID, int ExpenseSubCategoryID, int CurrencyID, decimal ExchangeRate, string ShopName, int? ShopCount, string ProjectName, string ProjectDesc,
                        string ApplyFileName, string ApplyRealFileName, DateTime? ActivityBeginDate, DateTime? ActivityEndDate, int? CostCenterID, int? DiscountTypeID,
                        decimal TotalBudget, decimal ApprovedAmount, decimal ApprovingAmount, decimal CompletedAmount, decimal ReimbursedAmount, decimal RemainBudget) {

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
                formRow.PageType = (int)SystemEnums.PageType.NoActivityApply;
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
                if (DiscountTypeID != null) {
                    formApplyRow.DiscountTypeID = DiscountTypeID.GetValueOrDefault();
                }
                formApplyRow.TotalBudget = TotalBudget;
                formApplyRow.ApprovedAmount = ApprovedAmount;
                formApplyRow.ApprovingAmount = ApprovingAmount;
                formApplyRow.CompletedAmount = CompletedAmount;
                formApplyRow.ReimbursedAmount = ReimbursedAmount;
                formApplyRow.RemainBudget = RemainBudget;
                formApplyRow.AmountRMB = 0;//暂时
                formApplyRow.IsClose = false;
                formApplyRow.IsCompleted = false;

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
                    dic["MaxDiscount"] = 0; ;
                    dic["DiscountType"] = DiscountTypeID;

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

        public void UpdateSaleNoActivityApply(int FormID, SystemEnums.FormType FormTypeID, SystemEnums.FormStatus StatusID,
                        decimal ExchangeRate, string ShopName, int? ShopCount, string ProjectName, string ProjectDesc,
                        string ApplyFileName, string ApplyRealFileName, DateTime? ActivityBeginDate, DateTime? ActivityEndDate, int? CostCenterID, int? DiscountTypeID,
                        decimal TotalBudget, decimal ApprovedAmount, decimal ApprovingAmount, decimal CompletedAmount, decimal ReimbursedAmount, decimal RemainBudget) {

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
                if (DiscountTypeID != null) {
                    formApplyRow.DiscountTypeID = DiscountTypeID.GetValueOrDefault();
                }

                formApplyRow.TotalBudget = TotalBudget;
                formApplyRow.ApprovedAmount = ApprovedAmount;
                formApplyRow.ApprovingAmount = ApprovingAmount;
                formApplyRow.CompletedAmount = CompletedAmount;
                formApplyRow.ReimbursedAmount = ReimbursedAmount;
                formApplyRow.RemainBudget = RemainBudget;

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
                    dic["MaxDiscount"] = 0;
                    dic["DiscountType"] = DiscountTypeID;

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

        public void DeleteSaleNoActivityApplyByFormID(int FormID) {
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

        #endregion

        #region Settlement

        public FormDS.FormSaleSettlementRow GetFormSaleSettlementByID(int FormSaleSettlementID) {
            return this.TAFormSaleSettlement.GetDataByID(FormSaleSettlementID)[0];
        }

        public FormDS.FormSettlementExpenseDetailDataTable GetFormSettlementExpenseDetail() {
            return this.FormDataSet.FormSettlementExpenseDetail;
        }

        public FormDS.FormSettlementSKUDetailDataTable GetFormSettlementSKUDetail() {
            return this.FormDataSet.FormSettlementSKUDetail;
        }

        public FormDS.FormSettlementExpenseDetailDataTable GetFormSettlementExpenseDetailBySettlementID(int FormSaleSettlementID) {
            return this.TAFormSettlementExpenseDetail.GetDataByFormSaleSettlementID(FormSaleSettlementID);
        }

        public FormDS.FormSettlementSKUDetailDataTable GetFormSettlementSKUDetailBySettlementID(int FormSaleSettlementID) {
            return this.TAFormSettlementSKUDetail.GetDataByFormSaleSettlementID(FormSaleSettlementID);
        }

        public FormDS.FormSettlementExpenseDetailRow GetFormSettlementExpenseDetailByID(int FormSettlementExpenseDetailID) {
            return this.TAFormSettlementExpenseDetail.GetDataByID(FormSettlementExpenseDetailID)[0];
        }

        public string GetSettledFormNoByApplyFormIds(string ApplyFormIds) {
            string SettledFormNo = (string)this.TAFormSaleApply.GetSettledFormNoByApplyFormIds(ApplyFormIds);
            if (SettledFormNo == null) {
                SettledFormNo = "";
            }
            return SettledFormNo;
        }

        public string GetProcessingPaymentNoByFormSaleSettlementID(int FormSaleSettlementID) {
            Object ProcessingPaymentNo = this.TAFormSaleSettlement.GetProcessingPaymentNoByFormSaleSettlementID(FormSaleSettlementID);
            return ProcessingPaymentNo == null ? null : ProcessingPaymentNo.ToString();
        }

        public string GetPaymentFormNoBySaleSettlementID(int FormSaleSettlementID) {
            string PaymentFormNo = (string)this.TAFormSaleSettlement.GetPaymentFormNoBySaleSettlementID(FormSaleSettlementID);
            if (PaymentFormNo == null) {
                PaymentFormNo = "";
            }
            return PaymentFormNo;
        }

        public void AddActivitySettlement(int? RejectedFormID, int UserID, int? ProxyUserID, int? ProxyPositionID, int OrganizationUnitID, int PositionID, SystemEnums.FormType FormTypeID, SystemEnums.FormStatus StatusID,
                        int CustomerID, int BrandID, int ExpenseSubCategoryID, int CurrencyID, string AttachedFileName, string RealAttachedFileName, string Remark, string FormApplyIds, string FormApplyNos, int CostCenterID, int PaymentTypeID) {

            SqlTransaction transaction = null;
            try {
                //事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormSaleSettlement, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormSettlementExpenseDetail, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormSettlementSKUDetail, transaction);

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
                formRow.PageType = (int)SystemEnums.PageType.ActivitySettlementApply;
                formRow.CostCenterID = CostCenterID;
                this.FormDataSet.Form.AddFormRow(formRow);
                this.TAForm.Update(formRow);

                //处理settlement的内容
                FormDS.FormSaleSettlementRow formSettleRow = this.FormDataSet.FormSaleSettlement.NewFormSaleSettlementRow();
                formSettleRow.FormSaleSettlementID = formRow.FormID;
                formSettleRow.CustomerID = CustomerID;
                formSettleRow.BrandID = BrandID;
                formSettleRow.ExpenseSubCategoryID = ExpenseSubCategoryID;
                formSettleRow.CurrencyID = CurrencyID;
                if (AttachedFileName != null && AttachedFileName != string.Empty) {
                    formSettleRow.AttachedFileName = AttachedFileName;
                }
                if (RealAttachedFileName != null && RealAttachedFileName != string.Empty) {
                    formSettleRow.RealAttachedFileName = RealAttachedFileName;
                }
                if (Remark != null && Remark != string.Empty) {
                    formSettleRow.Remark = Remark;
                }
                formSettleRow.AmountRMB = 0;
                formSettleRow.FormApplyIds = FormApplyIds;
                formSettleRow.FormApplyNos = FormApplyNos;
                formSettleRow.IsClose = false;
                formSettleRow.PaymentTypeID = PaymentTypeID;
                this.FormDataSet.FormSaleSettlement.AddFormSaleSettlementRow(formSettleRow);
                this.TAFormSaleSettlement.Update(formSettleRow);

                //处理明细,先产品表
                decimal totalForecastOrderAmount = 0;
                decimal totalActualOrderAmount = 0;
                FormDS.FormSettlementSKUDetailDataTable newSKUDetailTable = new FormDS.FormSettlementSKUDetailDataTable();
                foreach (FormDS.FormSettlementSKUDetailRow skuRow in this.FormDataSet.FormSettlementSKUDetail) {
                    if (skuRow.RowState != DataRowState.Deleted) {
                        FormDS.FormSettlementSKUDetailRow newSKURow = newSKUDetailTable.NewFormSettlementSKUDetailRow();
                        newSKURow.FormSaleSettlementID = formSettleRow.FormSaleSettlementID;
                        newSKURow.FormSaleSKUDetailID = skuRow.FormSaleSKUDetailID;
                        newSKURow.FormSaleApplyID = skuRow.FormSaleApplyID;
                        newSKURow.ApplyFormNo = skuRow.ApplyFormNo;
                        newSKURow.ApplyPeriod = skuRow.ApplyPeriod;
                        if (!skuRow.IsApplyProjectNameNull()) {
                            newSKURow.ApplyProjectName = skuRow.ApplyProjectName;
                        }
                        newSKURow.SKUID = skuRow.SKUID;
                        if (!skuRow.IsDiscountNull()) {
                            newSKURow.Discount = skuRow.Discount;
                        }
                        if (!skuRow.IsDiscountCampbellNull()) {
                            newSKURow.DiscountCampbell = skuRow.DiscountCampbell;
                        }
                        newSKURow.ForecastSaleQuantity = skuRow.ForecastSaleQuantity;
                        newSKURow.ForecastOrderQuantity = skuRow.ForecastOrderQuantity;
                        newSKURow.DeliveryPrice = skuRow.DeliveryPrice;
                        newSKURow.ForecastOrderAmount = skuRow.ForecastOrderAmount;
                        if (!skuRow.IsPriceDiscountAmountNull()) {
                            newSKURow.PriceDiscountAmount = skuRow.PriceDiscountAmount;
                        }
                        if (!skuRow.IsPriceDiscountAmountRMBNull()) {
                            newSKURow.PriceDiscountAmountRMB = skuRow.PriceDiscountAmountRMB;
                        }
                        if (!skuRow.IsRemarkNull()) {
                            newSKURow.Remark = skuRow.Remark;
                        }
                        if (!skuRow.IsActualOrderQuantityNull()) {
                            newSKURow.ActualOrderQuantity = skuRow.ActualOrderQuantity;
                        }
                        if (!skuRow.IsActualOrderAmountNull()) {
                            newSKURow.ActualOrderAmount = skuRow.ActualOrderAmount;
                        }
                        if (!skuRow.IsActualRateNull()) {
                            newSKURow.ActualRate = skuRow.ActualRate;
                        }
                        newSKUDetailTable.AddFormSettlementSKUDetailRow(newSKURow);
                        totalForecastOrderAmount = totalForecastOrderAmount + newSKURow.ForecastOrderAmount;
                        totalActualOrderAmount = totalActualOrderAmount + newSKURow.ActualOrderAmount;
                    }

                    this.TAFormSettlementSKUDetail.Update(newSKUDetailTable);
                }

                //费用项
                decimal totalAmountRMB = 0;
                decimal totalApplyAmountRMB = 0;
                FormDS.FormSettlementExpenseDetailDataTable newExpenseDetailTable = new FormDS.FormSettlementExpenseDetailDataTable();
                foreach (FormDS.FormSettlementExpenseDetailRow expenseRow in this.FormDataSet.FormSettlementExpenseDetail) {
                    if (expenseRow.RowState != DataRowState.Deleted) {
                        FormDS.FormSettlementExpenseDetailRow newExpenseRow = newExpenseDetailTable.NewFormSettlementExpenseDetailRow();
                        newExpenseRow.FormSaleSettlementID = formSettleRow.FormSaleSettlementID;
                        newExpenseRow.FormSaleExpenseDetailID = expenseRow.FormSaleExpenseDetailID;
                        newExpenseRow.FormSaleApplyID = expenseRow.FormSaleApplyID;
                        newExpenseRow.ApplyFormNo = expenseRow.ApplyFormNo;
                        newExpenseRow.ApplyPeriod = expenseRow.ApplyPeriod;
                        newExpenseRow.ApplyProjectName = expenseRow.ApplyProjectName;
                        newExpenseRow.ExpenseItemID = expenseRow.ExpenseItemID;
                        if (!expenseRow.IsShopNameNull()) {
                            newExpenseRow.ShopName = expenseRow.ShopName;
                        }
                        if (!expenseRow.IsSKUIDNull()) {
                            newExpenseRow.SKUID = expenseRow.SKUID;
                        }
                        newExpenseRow.ApplyAmount = expenseRow.ApplyAmount;
                        newExpenseRow.ApplyAmountRMB = expenseRow.ApplyAmountRMB;
                        if (!expenseRow.IsApplyRemarkNull()) {
                            newExpenseRow.ApplyRemark = expenseRow.ApplyRemark;
                        }
                        if (!expenseRow.IsAdvancedAmountNull()) {
                            newExpenseRow.AdvancedAmount = expenseRow.AdvancedAmount;
                        }
                        newExpenseRow.AmountRMB = expenseRow.AmountRMB;
                        if (!expenseRow.IsRemarkNull()) {
                            newExpenseRow.Remark = expenseRow.Remark;
                        }

                        newExpenseDetailTable.AddFormSettlementExpenseDetailRow(newExpenseRow);
                        totalAmountRMB = totalAmountRMB + newExpenseRow.AmountRMB;
                        totalApplyAmountRMB = totalApplyAmountRMB + newExpenseRow.ApplyAmountRMB;
                    }
                }
                this.TAFormSettlementExpenseDetail.Update(newExpenseDetailTable);

                formSettleRow.ForecastOrderAmount = totalForecastOrderAmount;
                formSettleRow.ApplyAmountRMB = totalApplyAmountRMB;
                if (totalForecastOrderAmount != 0) {
                    formSettleRow.CostBenefitRate = totalApplyAmountRMB / totalForecastOrderAmount * 100;
                } else {
                    formSettleRow.CostBenefitRate = 0;
                }
                formSettleRow.ActualOrderAmount = totalActualOrderAmount;
                formSettleRow.AmountRMB = totalAmountRMB;
                if (totalActualOrderAmount != 0) {
                    formSettleRow.ActualCostBenefitRate = totalAmountRMB / totalActualOrderAmount * 100;
                } else {
                    formSettleRow.ActualCostBenefitRate = 0;
                }

                this.TAFormSaleSettlement.Update(formSettleRow);

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
                    dic["PaymentTypeID"] = PaymentTypeID;

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
            } catch (Exception ex) {
                transaction.Rollback();
                throw new ApplicationException("Save Fail!" + ex.ToString());
            } finally {
                transaction.Dispose();
            }

        }

        public void UpdateActivitySettlement(int FormID, SystemEnums.FormType FormTypeID, SystemEnums.FormStatus StatusID, string AttachedFileName, string RealAttachedFileName, string Remark, int PaymentTypeID) {

            SqlTransaction transaction = null;
            try {
                //事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormSaleSettlement, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormSettlementExpenseDetail, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormSettlementSKUDetail, transaction);

                FormDS.FormRow formRow = this.TAForm.GetDataByID(FormID)[0];
                FormDS.FormSaleSettlementRow formSettleRow = this.TAFormSaleSettlement.GetDataByID(FormID)[0];

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

                //处理settlement的内容
                if (AttachedFileName != null && AttachedFileName != string.Empty) {
                    formSettleRow.AttachedFileName = AttachedFileName;
                } else {
                    formSettleRow.SetAttachedFileNameNull();
                }
                if (RealAttachedFileName != null && RealAttachedFileName != string.Empty) {
                    formSettleRow.RealAttachedFileName = RealAttachedFileName;
                } else {
                    formSettleRow.SetRealAttachedFileNameNull();
                }
                if (Remark != null && Remark != string.Empty) {
                    formSettleRow.Remark = Remark;
                }
                formSettleRow.PaymentTypeID = PaymentTypeID;
                this.TAFormSaleSettlement.Update(formSettleRow);

                //处理明细,先产品表
                decimal totalForecastOrderAmount = 0;
                decimal totalActualOrderAmount = 0;
                foreach (FormDS.FormSettlementSKUDetailRow skuRow in this.FormDataSet.FormSettlementSKUDetail) {
                    if (skuRow.RowState != DataRowState.Deleted) {
                        skuRow.FormSaleSettlementID = formSettleRow.FormSaleSettlementID;
                        totalForecastOrderAmount = totalForecastOrderAmount + skuRow.ForecastOrderAmount;
                        totalActualOrderAmount = totalActualOrderAmount + skuRow.ActualOrderAmount;
                    }
                }
                this.TAFormSettlementSKUDetail.Update(this.FormDataSet.FormSettlementSKUDetail);
                //费用项
                decimal totalAmountRMB = 0;
                decimal totalApplyAmountRMB = 0;
                foreach (FormDS.FormSettlementExpenseDetailRow expenseRow in this.FormDataSet.FormSettlementExpenseDetail) {
                    if (expenseRow.RowState != DataRowState.Deleted) {
                        expenseRow.FormSaleSettlementID = formSettleRow.FormSaleSettlementID;
                        totalAmountRMB = totalAmountRMB + expenseRow.AmountRMB;
                        totalApplyAmountRMB = totalApplyAmountRMB + expenseRow.ApplyAmountRMB;
                    }
                }
                this.TAFormSettlementExpenseDetail.Update(this.FormDataSet.FormSettlementExpenseDetail);

                formSettleRow.ForecastOrderAmount = totalForecastOrderAmount;
                formSettleRow.ApplyAmountRMB = totalApplyAmountRMB;
                if (totalForecastOrderAmount != 0) {
                    formSettleRow.CostBenefitRate = totalApplyAmountRMB / totalForecastOrderAmount * 100;
                } else {
                    formSettleRow.CostBenefitRate = 0;
                }
                formSettleRow.ActualOrderAmount = totalActualOrderAmount;
                formSettleRow.AmountRMB = totalAmountRMB;
                if (totalActualOrderAmount != 0) {
                    formSettleRow.ActualCostBenefitRate = totalAmountRMB / totalActualOrderAmount * 100;
                } else {
                    formSettleRow.ActualCostBenefitRate = 0;
                }

                this.TAFormSaleSettlement.Update(formSettleRow);

                // 正式提交
                if (StatusID == SystemEnums.FormStatus.Awaiting) {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic["TotalAmount"] = totalAmountRMB;
                    MasterData.CustomerRow customer = new MasterDataBLL().GetCustomerById(formSettleRow.CustomerID)[0];
                    dic["CustomerChannel"] = new MasterDataBLL().GetCustomerChannelById(customer.CustomerChannelID)[0].CustomerChannelCode;
                    dic["KAType"] = customer.IsKaTypeNull() ? "" : customer.KaType;
                    dic["PaymentTypeID"] = PaymentTypeID;

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
            } catch (Exception ex) {
                transaction.Rollback();
                throw new ApplicationException("Save Fail!" + ex.ToString());
            } finally {
                transaction.Dispose();
            }
        }

        public void DeleteSaleSettlementByFormID(int FormID) {
            SqlTransaction transaction = null;
            try {
                //事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormSaleSettlement, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormSettlementSKUDetail, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormSettlementExpenseDetail, transaction);

                this.TAFormSettlementExpenseDetail.DeleteByFormSaleSettlementID(FormID);
                this.TAFormSettlementSKUDetail.DeleteByFormSaleSettlementID(FormID);
                this.TAFormSaleSettlement.DeleteByID(FormID);
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

        public void AddNoActivitySettlement(int? RejectedFormID, int UserID, int? ProxyUserID, int? ProxyPositionID, int OrganizationUnitID, int PositionID, SystemEnums.FormType FormTypeID, SystemEnums.FormStatus StatusID,
                        int CustomerID, int BrandID, int ExpenseSubCategoryID, int CurrencyID, string AttachedFileName, string RealAttachedFileName, string Remark, string FormApplyIds, string FormApplyNos, int CostCenterID, int PaymentTypeID) {

            SqlTransaction transaction = null;
            try {
                //事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormSaleSettlement, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormSettlementExpenseDetail, transaction);

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
                formRow.PageType = (int)SystemEnums.PageType.NoActivitySettlementApply;
                formRow.CostCenterID = CostCenterID;
                this.FormDataSet.Form.AddFormRow(formRow);
                this.TAForm.Update(formRow);

                //处理settlement的内容
                FormDS.FormSaleSettlementRow formSettleRow = this.FormDataSet.FormSaleSettlement.NewFormSaleSettlementRow();
                formSettleRow.FormSaleSettlementID = formRow.FormID;
                formSettleRow.CustomerID = CustomerID;
                formSettleRow.BrandID = BrandID;
                formSettleRow.ExpenseSubCategoryID = ExpenseSubCategoryID;
                formSettleRow.CurrencyID = CurrencyID;
                if (AttachedFileName != null && AttachedFileName != string.Empty) {
                    formSettleRow.AttachedFileName = AttachedFileName;
                }
                if (RealAttachedFileName != null && RealAttachedFileName != string.Empty) {
                    formSettleRow.RealAttachedFileName = RealAttachedFileName;
                }
                if (Remark != null && Remark != string.Empty) {
                    formSettleRow.Remark = Remark;
                }
                formSettleRow.AmountRMB = 0;
                formSettleRow.FormApplyIds = FormApplyIds;
                formSettleRow.FormApplyNos = FormApplyNos;
                formSettleRow.IsClose = false;
                formSettleRow.PaymentTypeID = PaymentTypeID;
                this.FormDataSet.FormSaleSettlement.AddFormSaleSettlementRow(formSettleRow);
                this.TAFormSaleSettlement.Update(formSettleRow);

                //费用项
                decimal totalAmountRMB = 0;
                decimal totalApplyAmountRMB = 0;
                FormDS.FormSettlementExpenseDetailDataTable newExpenseDetailTable = new FormDS.FormSettlementExpenseDetailDataTable();
                foreach (FormDS.FormSettlementExpenseDetailRow expenseRow in this.FormDataSet.FormSettlementExpenseDetail) {
                    if (expenseRow.RowState != DataRowState.Deleted) {
                        FormDS.FormSettlementExpenseDetailRow newExpenseRow = newExpenseDetailTable.NewFormSettlementExpenseDetailRow();
                        newExpenseRow.FormSaleSettlementID = formSettleRow.FormSaleSettlementID;
                        newExpenseRow.FormSaleExpenseDetailID = expenseRow.FormSaleExpenseDetailID;
                        newExpenseRow.FormSaleApplyID = expenseRow.FormSaleApplyID;
                        newExpenseRow.ApplyFormNo = expenseRow.ApplyFormNo;
                        newExpenseRow.ApplyPeriod = expenseRow.ApplyPeriod;
                        newExpenseRow.ApplyProjectName = expenseRow.ApplyProjectName;
                        newExpenseRow.ExpenseItemID = expenseRow.ExpenseItemID;
                        if (!expenseRow.IsShopNameNull()) {
                            newExpenseRow.ShopName = expenseRow.ShopName;
                        }
                        if (!expenseRow.IsSKUIDNull()) {
                            newExpenseRow.SKUID = expenseRow.SKUID;
                        }
                        newExpenseRow.ApplyAmount = expenseRow.ApplyAmount;
                        newExpenseRow.ApplyAmountRMB = expenseRow.ApplyAmountRMB;
                        if (!expenseRow.IsApplyRemarkNull()) {
                            newExpenseRow.ApplyRemark = expenseRow.ApplyRemark;
                        }
                        if (!expenseRow.IsAdvancedAmountNull()) {
                            newExpenseRow.AdvancedAmount = expenseRow.AdvancedAmount;
                        }
                        newExpenseRow.AmountRMB = expenseRow.AmountRMB;
                        if (!expenseRow.IsRemarkNull()) {
                            newExpenseRow.Remark = expenseRow.Remark;
                        }

                        newExpenseDetailTable.AddFormSettlementExpenseDetailRow(newExpenseRow);
                        totalAmountRMB = totalAmountRMB + newExpenseRow.AmountRMB;
                        totalApplyAmountRMB = totalApplyAmountRMB + newExpenseRow.ApplyAmountRMB;
                    }
                }
                this.TAFormSettlementExpenseDetail.Update(newExpenseDetailTable);

                formSettleRow.ApplyAmountRMB = totalApplyAmountRMB;
                formSettleRow.AmountRMB = totalAmountRMB;

                this.TAFormSaleSettlement.Update(formSettleRow);

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
                    dic["PaymentTypeID"] = PaymentTypeID;
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

            } catch (Exception ex) {
                transaction.Rollback();
                throw new ApplicationException("Save Fail!" + ex.ToString());
            } finally {
                transaction.Dispose();
            }

        }

        public void UpdateNoActivitySettlement(int FormID, SystemEnums.FormType FormTypeID, SystemEnums.FormStatus StatusID, string AttachedFileName, string RealAttachedFileName, string Remark, int PaymentTypeID) {

            SqlTransaction transaction = null;
            try {
                //事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormSaleSettlement, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormSettlementExpenseDetail, transaction);

                FormDS.FormRow formRow = this.TAForm.GetDataByID(FormID)[0];
                FormDS.FormSaleSettlementRow formSettleRow = this.TAFormSaleSettlement.GetDataByID(FormID)[0];

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

                //处理settlement的内容
                if (AttachedFileName != null && AttachedFileName != string.Empty) {
                    formSettleRow.AttachedFileName = AttachedFileName;
                } else {
                    formSettleRow.SetAttachedFileNameNull();
                }
                if (RealAttachedFileName != null && RealAttachedFileName != string.Empty) {
                    formSettleRow.RealAttachedFileName = RealAttachedFileName;
                } else {
                    formSettleRow.SetRealAttachedFileNameNull();
                }
                if (Remark != null && Remark != string.Empty) {
                    formSettleRow.Remark = Remark;
                }
                formSettleRow.PaymentTypeID = PaymentTypeID;
                this.TAFormSaleSettlement.Update(formSettleRow);

                //费用项
                decimal totalAmountRMB = 0;
                decimal totalApplyAmountRMB = 0;
                foreach (FormDS.FormSettlementExpenseDetailRow expenseRow in this.FormDataSet.FormSettlementExpenseDetail) {
                    if (expenseRow.RowState != DataRowState.Deleted) {
                        expenseRow.FormSaleSettlementID = formSettleRow.FormSaleSettlementID;
                        totalAmountRMB = totalAmountRMB + expenseRow.AmountRMB;
                        totalApplyAmountRMB = totalApplyAmountRMB + expenseRow.ApplyAmountRMB;
                    }
                }
                this.TAFormSettlementExpenseDetail.Update(this.FormDataSet.FormSettlementExpenseDetail);

                formSettleRow.ApplyAmountRMB = totalApplyAmountRMB;
                formSettleRow.AmountRMB = totalAmountRMB;

                this.TAFormSaleSettlement.Update(formSettleRow);

                // 正式提交
                if (StatusID == SystemEnums.FormStatus.Awaiting) {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic["TotalAmount"] = totalAmountRMB;
                    MasterData.CustomerRow customer = new MasterDataBLL().GetCustomerById(formSettleRow.CustomerID)[0];
                    dic["CustomerChannel"] = new MasterDataBLL().GetCustomerChannelById(customer.CustomerChannelID)[0].CustomerChannelCode;
                    dic["KAType"] = customer.IsKaTypeNull() ? "" : customer.KaType;
                    dic["PaymentTypeID"] = PaymentTypeID;

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

            } catch (Exception ex) {
                transaction.Rollback();
                throw new ApplicationException("Save Fail!" + ex.ToString());
            } finally {
                transaction.Dispose();
            }

        }

        public void CloseFormSaleSettlement(int FormSaleSettlementID) {
            SqlTransaction transaction = null;
            try {
                //事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAFormSaleSettlement);
                FormDS.FormSaleSettlementRow formSettleRow = this.TAFormSaleSettlement.GetDataByID(FormSaleSettlementID)[0];

                //处理settlement的内容
                formSettleRow.IsClose = true;
                formSettleRow.CloseDate = DateTime.Now;
                this.TAFormSaleSettlement.Update(formSettleRow);
                this.TAFormSaleSettlement.CloseFormSaleApplyByApplyFormIds(formSettleRow.FormApplyIds);
                transaction.Commit();

            } catch (Exception ex) {
                transaction.Rollback();
                throw new ApplicationException("Save Fail!" + ex.ToString());
            } finally {
                transaction.Dispose();
            }
        }

        public void CloseFormSaleApply(int FormSaleSettlementID) {
            SqlTransaction transaction = null;
            try {
                //事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAFormSaleApply);
                FormDS.FormSaleApplyRow formSaleApplyRow = this.TAFormSaleApply.GetDataByID(FormSaleSettlementID)[0];

                //处理settlement的内容
                formSaleApplyRow.IsClose = true;
                formSaleApplyRow.CloseDate = DateTime.Now;
                this.TAFormSaleApply.Update(formSaleApplyRow);
                transaction.Commit();

            } catch (Exception ex) {
                transaction.Rollback();
                throw new ApplicationException("Save Fail!" + ex.ToString());
            } finally {
                transaction.Dispose();
            }
        }

        #endregion

        #region pre-Payment

        public FormDS.FormSalePaymentRow GetFormSalePaymentByID(int FormSalePaymentID) {
            return this.TAFormSalePayment.GetDataByID(FormSalePaymentID)[0];
        }

        public FormDS.FormSalePaymentDetailDataTable GetFormSalePaymentDetail() {
            return this.FormDataSet.FormSalePaymentDetail;
        }

        public FormDS.FormSalePaymentDetailDataTable GetFormSalePaymentDetailByPaymentID(int FormSalePaymentID) {
            return this.TAFormSalePaymentDetail.GetDataByFormSalePaymentID(FormSalePaymentID);
        }

        public FormDS.FormSalePaymentDetailRow GetFormSalePaymentDetailByID(int FormSalePaymentDetailID) {
            return this.TAFormSalePaymentDetail.GetDataByID(FormSalePaymentDetailID)[0];
        }

        public void AddAdvancedPayment(int? RejectedFormID, int UserID, int? ProxyUserID, int? ProxyPositionID, int OrganizationUnitID, int PositionID, SystemEnums.FormType FormTypeID, SystemEnums.FormStatus StatusID,
                        SystemEnums.PageType PageType, int FormSaleApplyID, int InvoiceStatusID, int PaymentTypeID, string Remark, string AttachedFileName, string RealAttachedFileName, int CostCenterID, int? VendorID, int FormPOID, int? VATTypeID) {

            SqlTransaction transaction = null;
            try {
                //事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormSalePayment, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormSalePaymentDetail, transaction);
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
                FormDS.FormSalePaymentRow formPaymentRow = this.FormDataSet.FormSalePayment.NewFormSalePaymentRow();
                formPaymentRow.FormSalePaymentID = formRow.FormID;
                formPaymentRow.FormSaleApplyID = FormSaleApplyID;
                formPaymentRow.InvoiceStatusID = InvoiceStatusID;
                formPaymentRow.PaymentTypeID = PaymentTypeID;
                if (Remark != null && Remark != string.Empty) {
                    formPaymentRow.Remark = Remark;
                }
                if (AttachedFileName != null && AttachedFileName != string.Empty) {
                    formPaymentRow.AttachedFileName = AttachedFileName;
                }
                if (RealAttachedFileName != null && RealAttachedFileName != string.Empty) {
                    formPaymentRow.RealAttachedFileName = RealAttachedFileName;
                }
                formPaymentRow.AmountRMB = 0;
                formPaymentRow.IsAdvanced = true;
                if (VendorID != null) {
                    formPaymentRow.VendorID = VendorID.GetValueOrDefault();
                }
                if (FormPOID > 0) {
                    formPaymentRow.FormPOID = FormPOID;
                }
                if (VATTypeID != null) {
                    formPaymentRow.VatTypeID = VATTypeID.GetValueOrDefault();
                }
                this.FormDataSet.FormSalePayment.AddFormSalePaymentRow(formPaymentRow);
                this.TAFormSalePayment.Update(formPaymentRow);

                //发票
                if (RejectedFormID != null) {
                    FormDS.FormInvoiceDataTable newInvoiceTable = new FormDS.FormInvoiceDataTable();
                    foreach (FormDS.FormInvoiceRow invoiceRow in this.FormDataSet.FormInvoice) {
                        if (invoiceRow.RowState != DataRowState.Deleted) {
                            FormDS.FormInvoiceRow newInvoiceRow = newInvoiceTable.NewFormInvoiceRow();
                            newInvoiceRow.FormID = formPaymentRow.FormSalePaymentID;
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
                            invoiceRow.FormID = formPaymentRow.FormSalePaymentID;
                        }
                    }
                }
                this.TAFormInvoice.Update(this.FormDataSet.FormInvoice);

                //处理明细
                //先取得方案申请单
                FormDS.FormSaleApplyRow rowSaleApply = this.TAFormSaleApply.GetDataByID(FormSaleApplyID)[0];
                FormDS.FormRow rowApplyForm = this.TAForm.GetDataByID(FormSaleApplyID)[0];
                decimal totalAmountBeforeTax = 0;
                decimal totalTaxAmount = 0;
                decimal totalAmountRMB = 0;
                FormDS.FormSalePaymentDetailDataTable newDetailTable = new FormDS.FormSalePaymentDetailDataTable();
                foreach (FormDS.FormSalePaymentDetailRow detailRow in this.FormDataSet.FormSalePaymentDetail) {
                    // 与父表绑定
                    if (detailRow.RowState != DataRowState.Deleted) {
                        totalAmountBeforeTax += detailRow.AmountBeforeTax;
                        totalTaxAmount += detailRow.TaxAmount;
                        totalAmountRMB += detailRow.AmountRMB;
                        FormDS.FormSalePaymentDetailRow newDetailRow = newDetailTable.NewFormSalePaymentDetailRow();
                        newDetailRow.FormSalePaymentID = formPaymentRow.FormSalePaymentID;
                        newDetailRow.FormSaleApplyID = rowSaleApply.FormSaleApplyID;
                        newDetailRow.FormSaleExpenseDetailID = detailRow.FormSaleExpenseDetailID;
                        newDetailRow.ApplyFormNo = rowApplyForm.FormNo;
                        newDetailRow.ApplyPeriod = rowSaleApply.FPeriod;
                        if (!rowSaleApply.IsProjectNameNull()) {
                            newDetailRow.ApplyProjectName = rowSaleApply.ProjectName;
                        }
                        newDetailRow.ExpenseItemID = detailRow.ExpenseItemID;
                        if (!detailRow.IsShopNameNull()) {
                            newDetailRow.ShopName = detailRow.ShopName;
                        }
                        if (!detailRow.IsSKUIDNull()) {
                            newDetailRow.SKUID = detailRow.SKUID;
                        }
                        newDetailRow.ApplyAmount = detailRow.ApplyAmount;
                        newDetailRow.ApplyAmountRMB = detailRow.ApplyAmountRMB;
                        if (!detailRow.IsSettlementAmountNull()) {
                            newDetailRow.SettlementAmount = detailRow.SettlementAmount;
                        }
                        if (!detailRow.IsPayedAmountNull()) {
                            newDetailRow.PayedAmount = detailRow.PayedAmount;
                        }
                        if (!detailRow.IsRemainAmountNull()) {
                            newDetailRow.RemainAmount = detailRow.RemainAmount;
                        }
                        newDetailRow.AmountBeforeTax = detailRow.AmountBeforeTax;
                        newDetailRow.TaxAmount = detailRow.TaxAmount;
                        newDetailRow.AmountRMB = detailRow.AmountRMB;

                        newDetailTable.AddFormSalePaymentDetailRow(newDetailRow);
                    }
                }
                this.TAFormSalePaymentDetail.Update(newDetailTable);

                formPaymentRow.TaxAmount = totalTaxAmount;
                formPaymentRow.AmountBeforeTax = totalAmountBeforeTax;
                formPaymentRow.AmountRMB = totalAmountRMB;
                this.TAFormSalePayment.Update(formPaymentRow);

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
                    MasterData.CustomerRow customer = new MasterDataBLL().GetCustomerById(rowSaleApply.CustomerID)[0];
                    dic["CustomerChannel"] = new MasterDataBLL().GetCustomerChannelById(customer.CustomerChannelID)[0].CustomerChannelCode;
                    dic["KAType"] = customer.IsKaTypeNull() ? "" : customer.KaType;

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

        public void UpdateAdvancedPayment(int FormID, SystemEnums.FormType FormTypeID, SystemEnums.FormStatus StatusID, int InvoiceStatusID, int PaymentTypeID, string Remark,
                string AttachedFileName, string RealAttachedFileName, int? VendorID, int FormPOID, int? VATTypeID) {

            SqlTransaction transaction = null;
            try {
                //事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormSalePayment, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormSalePaymentDetail, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormInvoice, transaction);

                FormDS.FormRow formRow = this.TAForm.GetDataByID(FormID)[0];
                FormDS.FormSalePaymentRow formPaymentRow = this.TAFormSalePayment.GetDataByID(FormID)[0];

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
                if (AttachedFileName != null && AttachedFileName != string.Empty) {
                    formPaymentRow.AttachedFileName = AttachedFileName;
                } else {
                    formPaymentRow.SetAttachedFileNameNull();
                }
                if (RealAttachedFileName != null && RealAttachedFileName != string.Empty) {
                    formPaymentRow.RealAttachedFileName = RealAttachedFileName;
                } else {
                    formPaymentRow.SetRealAttachedFileNameNull();
                }
                if (Remark != null && Remark != string.Empty) {
                    formPaymentRow.Remark = Remark;
                }
                formPaymentRow.InvoiceStatusID = InvoiceStatusID;
                formPaymentRow.PaymentTypeID = PaymentTypeID;
                if (VendorID != null) {
                    formPaymentRow.VendorID = VendorID.GetValueOrDefault();
                }
                if (FormPOID > 0) {
                    formPaymentRow.FormPOID = FormPOID;
                }
                if (VATTypeID != null) {
                    formPaymentRow.VatTypeID = VATTypeID.GetValueOrDefault();
                }

                this.TAFormSalePayment.Update(formPaymentRow);

                //发票
                foreach (FormDS.FormInvoiceRow invoiceRow in this.FormDataSet.FormInvoice) {
                    // 与父表绑定
                    if (invoiceRow.RowState != DataRowState.Deleted) {
                        invoiceRow.FormID = formPaymentRow.FormSalePaymentID;
                    }
                }
                this.TAFormInvoice.Update(this.FormDataSet.FormInvoice);

                //处理明细
                decimal totalAmountBeforeTax = 0;
                decimal totalTaxAmount = 0;
                decimal totalAmountRMB = 0;
                foreach (FormDS.FormSalePaymentDetailRow detailRow in this.FormDataSet.FormSalePaymentDetail) {
                    if (detailRow.RowState != DataRowState.Deleted) {
                        totalAmountBeforeTax += detailRow.AmountBeforeTax;
                        totalTaxAmount += detailRow.TaxAmount;
                        totalAmountRMB += detailRow.AmountRMB;
                        detailRow.FormSalePaymentID = formPaymentRow.FormSalePaymentID;
                    }
                }
                this.TAFormSalePaymentDetail.Update(this.FormDataSet.FormSalePaymentDetail);

                formPaymentRow.AmountBeforeTax = totalAmountBeforeTax;
                formPaymentRow.TaxAmount = totalTaxAmount;
                formPaymentRow.AmountRMB = totalAmountRMB;
                this.TAFormSalePayment.Update(formPaymentRow);
                // 正式提交
                if (StatusID == SystemEnums.FormStatus.Awaiting) {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic["TotalAmount"] = totalAmountRMB;
                    MasterData.CustomerRow customer = new MasterDataBLL().GetCustomerById(this.TAFormSaleApply.GetDataByID(formPaymentRow.FormSaleApplyID)[0].CustomerID)[0];
                    dic["CustomerChannel"] = new MasterDataBLL().GetCustomerChannelById(customer.CustomerChannelID)[0].CustomerChannelCode;
                    dic["KAType"] = customer.IsKaTypeNull() ? "" : customer.KaType;

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

        public void DeleteFormSalePaymentByFormID(int FormID) {
            SqlTransaction transaction = null;
            try {
                //事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormSalePayment, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormInvoice, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormSalePaymentDetail, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormSalePaymentFreeGoods, transaction);

                this.TAFormSalePaymentDetail.DeleteByFormSalePaymentID(FormID);
                this.TAFormSalePaymentFreeGoods.DeleteByFormSalePaymentID(FormID);
                this.TAFormInvoice.DeleteByFormID(FormID);
                this.TAFormSalePayment.DeleteByID(FormID);
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

        #region Payment

        public FormDS.FormSalePaymentFreeGoodsDataTable GetFormSalePaymentFreeGoods() {
            return this.FormDataSet.FormSalePaymentFreeGoods;
        }

        public FormDS.FormSalePaymentFreeGoodsRow GetFormSalePaymentFreeGoodsByID(int FormSalePaymentFreeGoodsID) {
            return this.TAFormSalePaymentFreeGoods.GetDataByID(FormSalePaymentFreeGoodsID)[0];
        }

        public FormDS.FormSalePaymentFreeGoodsDataTable GetFormSalePaymentFreeGoodsByPaymentID(int FormSalePaymentID) {
            return this.TAFormSalePaymentFreeGoods.GetDataByFormSalePaymentID(FormSalePaymentID);
        }

        public void AddFormSalePaymentFreeGoods(int? FormSalePaymentID, int SKUID, decimal DeliveryPrice, decimal Quantity, string Remark) {

            FormDS.FormSalePaymentFreeGoodsRow rowDetail = this.FormDataSet.FormSalePaymentFreeGoods.NewFormSalePaymentFreeGoodsRow();
            rowDetail.FormSalePaymentID = FormSalePaymentID.GetValueOrDefault();
            rowDetail.SKUID = SKUID;
            MasterData.SKURow sku = new MasterDataBLL().GetSKUById(SKUID)[0];
            if (!sku.IsPackPerCaseNull()) {
                rowDetail.PackPerCase = sku.PackPerCase;
            }
            rowDetail.DeliveryPrice = DeliveryPrice;
            rowDetail.Quantity = Quantity;
            rowDetail.AmountRMB = decimal.Round(Quantity * DeliveryPrice, 2);
            rowDetail.Remark = Remark;

            this.FormDataSet.FormSalePaymentFreeGoods.AddFormSalePaymentFreeGoodsRow(rowDetail);
        }

        public void UpdateFormSalePaymentFreeGoods(int FormSalePaymentFreeGoodsID, int SKUID, decimal DeliveryPrice, decimal Quantity, string Remark) {

            FormDS.FormSalePaymentFreeGoodsRow rowDetail = this.FormDataSet.FormSalePaymentFreeGoods.FindByFormSalePaymentFreeGoodsID(FormSalePaymentFreeGoodsID);
            rowDetail.SKUID = SKUID;
            MasterData.SKURow sku = new MasterDataBLL().GetSKUById(SKUID)[0];
            if (!sku.IsPackPerCaseNull()) {
                rowDetail.PackPerCase = sku.PackPerCase;
            }
            rowDetail.DeliveryPrice = DeliveryPrice;
            rowDetail.Quantity = Quantity;
            rowDetail.AmountRMB = decimal.Round(Quantity * DeliveryPrice, 2);
            rowDetail.Remark = Remark;
        }

        public void DeleteFormSalePaymentFreeGoodsByID(int FormSalePaymentFreeGoodsID) {
            for (int index = 0; index < this.FormDataSet.FormSalePaymentFreeGoods.Count; index++) {
                if ((int)this.FormDataSet.FormSalePaymentFreeGoods.Rows[index]["FormSalePaymentFreeGoodsID"] == FormSalePaymentFreeGoodsID) {
                    this.FormDataSet.FormSalePaymentFreeGoods.Rows[index].Delete();
                    break;
                }
            }
        }

        public decimal GetPayedAmountByFormSaleExpenseDetailID(int FormSaleExpenseDetailID) {
            return (decimal)this.TAFormSalePaymentDetail.GetPayedAmountByFormSaleExpenseDetailID(FormSaleExpenseDetailID);
        }

        public void AddPaymentCash(int? RejectedFormID, int UserID, int? ProxyUserID, int? ProxyPositionID, int OrganizationUnitID, int PositionID, SystemEnums.FormType FormTypeID, SystemEnums.FormStatus StatusID,
                        SystemEnums.PageType PageType, int FormSaleSettlementID, int InvoiceStatusID, int PaymentTypeID, string Remark, string AttachedFileName, string RealAttachedFileName, int CostCenterID, int? VendorID, int FormPOID, int? VatTypeID) {
            SqlTransaction transaction = null;
            try {
                //事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormSalePayment, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormSalePaymentDetail, transaction);
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
                FormDS.FormSalePaymentRow formPaymentRow = this.FormDataSet.FormSalePayment.NewFormSalePaymentRow();
                formPaymentRow.FormSalePaymentID = formRow.FormID;
                formPaymentRow.FormSaleSettlementID = FormSaleSettlementID;
                formPaymentRow.InvoiceStatusID = InvoiceStatusID;
                formPaymentRow.PaymentTypeID = PaymentTypeID;
                if (Remark != null && Remark != string.Empty) {
                    formPaymentRow.Remark = Remark;
                }
                if (AttachedFileName != null && AttachedFileName != string.Empty) {
                    formPaymentRow.AttachedFileName = AttachedFileName;
                }
                if (RealAttachedFileName != null && RealAttachedFileName != string.Empty) {
                    formPaymentRow.RealAttachedFileName = RealAttachedFileName;
                }
                formPaymentRow.AmountRMB = 0;
                formPaymentRow.IsAdvanced = false;
                if (VendorID != null) {
                    formPaymentRow.VendorID = VendorID.GetValueOrDefault();
                }
                if (FormPOID > 0) {
                    formPaymentRow.FormPOID = FormPOID;
                }
                if (VatTypeID != null) {
                    formPaymentRow.VatTypeID = VatTypeID.GetValueOrDefault();
                }
                this.FormDataSet.FormSalePayment.AddFormSalePaymentRow(formPaymentRow);
                this.TAFormSalePayment.Update(formPaymentRow);

                //发票
                if (RejectedFormID != null) {
                    FormDS.FormInvoiceDataTable newInvoiceTable = new FormDS.FormInvoiceDataTable();
                    foreach (FormDS.FormInvoiceRow invoiceRow in this.FormDataSet.FormInvoice) {
                        if (invoiceRow.RowState != DataRowState.Deleted) {
                            FormDS.FormInvoiceRow newInvoiceRow = newInvoiceTable.NewFormInvoiceRow();
                            newInvoiceRow.FormID = formPaymentRow.FormSalePaymentID;
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
                            invoiceRow.FormID = formPaymentRow.FormSalePaymentID;
                        }
                    }
                }
                this.TAFormInvoice.Update(this.FormDataSet.FormInvoice);

                //处理明细
                decimal totalAmountBeforeTax = 0;
                decimal totalTaxAmount = 0;
                decimal totalAmountRMB = 0;
                FormDS.FormSalePaymentDetailDataTable newDetailTable = new FormDS.FormSalePaymentDetailDataTable();
                foreach (FormDS.FormSalePaymentDetailRow detailRow in this.FormDataSet.FormSalePaymentDetail) {
                    // 与父表绑定
                    if (detailRow.RowState != DataRowState.Deleted) {
                        totalAmountBeforeTax += detailRow.AmountBeforeTax;
                        totalTaxAmount += detailRow.TaxAmount;
                        totalAmountRMB += detailRow.AmountRMB;
                        FormDS.FormSalePaymentDetailRow newDetailRow = newDetailTable.NewFormSalePaymentDetailRow();
                        newDetailRow.FormSalePaymentID = formPaymentRow.FormSalePaymentID;
                        newDetailRow.FormSaleApplyID = detailRow.FormSaleApplyID;
                        newDetailRow.FormSaleExpenseDetailID = detailRow.FormSaleExpenseDetailID;
                        newDetailRow.ApplyFormNo = detailRow.ApplyFormNo;
                        newDetailRow.ApplyPeriod = detailRow.ApplyPeriod;
                        if (!detailRow.IsApplyProjectNameNull()) {
                            newDetailRow.ApplyProjectName = detailRow.ApplyProjectName;
                        }
                        newDetailRow.ExpenseItemID = detailRow.ExpenseItemID;
                        if (!detailRow.IsShopNameNull()) {
                            newDetailRow.ShopName = detailRow.ShopName;
                        }
                        if (!detailRow.IsSKUIDNull()) {
                            newDetailRow.SKUID = detailRow.SKUID;
                        }
                        newDetailRow.ApplyAmount = detailRow.ApplyAmount;
                        newDetailRow.ApplyAmountRMB = detailRow.ApplyAmountRMB;
                        if (!detailRow.IsSettlementAmountNull()) {
                            newDetailRow.SettlementAmount = detailRow.SettlementAmount;
                        }
                        if (!detailRow.IsPayedAmountNull()) {
                            newDetailRow.PayedAmount = detailRow.PayedAmount;
                        }
                        if (!detailRow.IsRemainAmountNull()) {
                            newDetailRow.RemainAmount = detailRow.RemainAmount;
                        }
                        newDetailRow.AmountBeforeTax = detailRow.AmountBeforeTax;
                        newDetailRow.TaxAmount = detailRow.TaxAmount;
                        newDetailRow.AmountRMB = detailRow.AmountRMB;

                        newDetailTable.AddFormSalePaymentDetailRow(newDetailRow);
                    }
                }
                this.TAFormSalePaymentDetail.Update(newDetailTable);

                formPaymentRow.AmountBeforeTax = totalAmountBeforeTax;
                formPaymentRow.TaxAmount = totalTaxAmount;
                formPaymentRow.AmountRMB = totalAmountRMB;
                this.TAFormSalePayment.Update(formPaymentRow);
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
                    MasterData.CustomerRow customer = new MasterDataBLL().GetCustomerById(this.TAFormSaleSettlement.GetDataByID(FormSaleSettlementID)[0].CustomerID)[0];
                    dic["CustomerChannel"] = new MasterDataBLL().GetCustomerChannelById(customer.CustomerChannelID)[0].CustomerChannelCode;
                    dic["KAType"] = customer.IsKaTypeNull() ? "" : customer.KaType;
                    dic["InvoiceStatus"] = formPaymentRow.InvoiceStatusID;
                    dic["PaymentTypeID"] = PaymentTypeID;
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

        public void UpdatePaymentCash(int FormID, SystemEnums.FormType FormTypeID, SystemEnums.FormStatus StatusID, int InvoiceStatusID, int PaymentTypeID,
                string Remark, string AttachedFileName, string RealAttachedFileName, int? VendorID, int FormPOID, int? VatTypeID) {

            SqlTransaction transaction = null;
            try {
                //事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormSalePayment, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormSalePaymentDetail, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormInvoice, transaction);

                FormDS.FormRow formRow = this.TAForm.GetDataByID(FormID)[0];
                FormDS.FormSalePaymentRow formPaymentRow = this.TAFormSalePayment.GetDataByID(FormID)[0];

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
                if (AttachedFileName != null && AttachedFileName != string.Empty) {
                    formPaymentRow.AttachedFileName = AttachedFileName;
                } else {
                    formPaymentRow.SetAttachedFileNameNull();
                }
                if (RealAttachedFileName != null && RealAttachedFileName != string.Empty) {
                    formPaymentRow.RealAttachedFileName = RealAttachedFileName;
                } else {
                    formPaymentRow.SetRealAttachedFileNameNull();
                }
                if (Remark != null && Remark != string.Empty) {
                    formPaymentRow.Remark = Remark;
                }
                formPaymentRow.InvoiceStatusID = InvoiceStatusID;
                formPaymentRow.PaymentTypeID = PaymentTypeID;
                if (VendorID != null) {
                    formPaymentRow.VendorID = VendorID.GetValueOrDefault();
                }
                if (FormPOID > 0) {
                    formPaymentRow.FormPOID = FormPOID;
                }
                if (VatTypeID != null) {
                    formPaymentRow.VatTypeID = VatTypeID.GetValueOrDefault();
                }
                this.TAFormSalePayment.Update(formPaymentRow);

                //发票
                foreach (FormDS.FormInvoiceRow invoiceRow in this.FormDataSet.FormInvoice) {
                    // 与父表绑定
                    if (invoiceRow.RowState != DataRowState.Deleted) {
                        invoiceRow.FormID = formPaymentRow.FormSalePaymentID;
                    }
                }
                this.TAFormInvoice.Update(this.FormDataSet.FormInvoice);

                //处理明细
                decimal totalAmountBeforeTax = 0;
                decimal totalTaxAmount = 0;
                decimal totalAmountRMB = 0;

                foreach (FormDS.FormSalePaymentDetailRow detailRow in this.FormDataSet.FormSalePaymentDetail) {
                    if (detailRow.RowState != DataRowState.Deleted) {
                        totalAmountBeforeTax += detailRow.AmountBeforeTax;
                        totalTaxAmount += detailRow.TaxAmount;
                        totalAmountRMB += detailRow.AmountRMB;
                        detailRow.FormSalePaymentID = formPaymentRow.FormSalePaymentID;
                    }
                }
                this.TAFormSalePaymentDetail.Update(this.FormDataSet.FormSalePaymentDetail);

                formPaymentRow.AmountBeforeTax = totalAmountBeforeTax;
                formPaymentRow.TaxAmount = totalTaxAmount;
                formPaymentRow.AmountRMB = totalAmountRMB;
                this.TAFormSalePayment.Update(formPaymentRow);
                // 正式提交
                if (StatusID == SystemEnums.FormStatus.Awaiting) {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic["TotalAmount"] = totalAmountRMB;
                    MasterData.CustomerRow customer = new MasterDataBLL().GetCustomerById(this.TAFormSaleSettlement.GetDataByID(formPaymentRow.FormSaleSettlementID)[0].CustomerID)[0];
                    dic["CustomerChannel"] = new MasterDataBLL().GetCustomerChannelById(customer.CustomerChannelID)[0].CustomerChannelCode;
                    dic["KAType"] = customer.IsKaTypeNull() ? "" : customer.KaType;
                    dic["InvoiceStatus"] = formPaymentRow.InvoiceStatusID;
                    dic["PaymentTypeID"] = PaymentTypeID;
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

        public void AddPaymentFreeGoods(int? RejectedFormID, int UserID, int? ProxyUserID, int? ProxyPositionID, int OrganizationUnitID, int PositionID, SystemEnums.FormType FormTypeID, SystemEnums.FormStatus StatusID,
                        SystemEnums.PageType PageType, int FormSaleSettlementID, int InvoiceStatusID, int PaymentTypeID, string Remark, string AttachedFileName, string RealAttachedFileName, int CostCenterID, int? VendorID) {

            SqlTransaction transaction = null;
            try {
                //事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormSalePayment, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormSalePaymentDetail, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormSalePaymentFreeGoods, transaction);

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
                FormDS.FormSalePaymentRow formPaymentRow = this.FormDataSet.FormSalePayment.NewFormSalePaymentRow();
                formPaymentRow.FormSalePaymentID = formRow.FormID;
                formPaymentRow.FormSaleSettlementID = FormSaleSettlementID;
                formPaymentRow.InvoiceStatusID = InvoiceStatusID;
                formPaymentRow.PaymentTypeID = PaymentTypeID;
                if (Remark != null && Remark != string.Empty) {
                    formPaymentRow.Remark = Remark;
                }
                if (AttachedFileName != null && AttachedFileName != string.Empty) {
                    formPaymentRow.AttachedFileName = AttachedFileName;
                }
                if (RealAttachedFileName != null && RealAttachedFileName != string.Empty) {
                    formPaymentRow.RealAttachedFileName = RealAttachedFileName;
                }
                formPaymentRow.AmountRMB = 0;
                formPaymentRow.IsAdvanced = false;
                if (VendorID != null) {
                    formPaymentRow.VendorID = VendorID.GetValueOrDefault();
                }

                this.FormDataSet.FormSalePayment.AddFormSalePaymentRow(formPaymentRow);
                this.TAFormSalePayment.Update(formPaymentRow);

                //处理报销明细
                decimal totalAmountRMB = 0;
                FormDS.FormSalePaymentDetailDataTable newDetailTable = new FormDS.FormSalePaymentDetailDataTable();
                foreach (FormDS.FormSalePaymentDetailRow detailRow in this.FormDataSet.FormSalePaymentDetail) {
                    // 与父表绑定
                    if (detailRow.RowState != DataRowState.Deleted) {
                        totalAmountRMB += detailRow.AmountRMB;
                        FormDS.FormSalePaymentDetailRow newDetailRow = newDetailTable.NewFormSalePaymentDetailRow();
                        newDetailRow.FormSalePaymentID = formPaymentRow.FormSalePaymentID;
                        newDetailRow.FormSaleApplyID = detailRow.FormSaleApplyID;
                        newDetailRow.FormSaleExpenseDetailID = detailRow.FormSaleExpenseDetailID;
                        newDetailRow.ApplyFormNo = detailRow.ApplyFormNo;
                        newDetailRow.ApplyPeriod = detailRow.ApplyPeriod;
                        if (!detailRow.IsApplyProjectNameNull()) {
                            newDetailRow.ApplyProjectName = detailRow.ApplyProjectName;
                        }
                        newDetailRow.ExpenseItemID = detailRow.ExpenseItemID;
                        if (!detailRow.IsShopNameNull()) {
                            newDetailRow.ShopName = detailRow.ShopName;
                        }
                        if (!detailRow.IsSKUIDNull()) {
                            newDetailRow.SKUID = detailRow.SKUID;
                        }
                        newDetailRow.ApplyAmount = detailRow.ApplyAmount;
                        newDetailRow.ApplyAmountRMB = detailRow.ApplyAmountRMB;
                        if (!detailRow.IsSettlementAmountNull()) {
                            newDetailRow.SettlementAmount = detailRow.SettlementAmount;
                        }
                        if (!detailRow.IsPayedAmountNull()) {
                            newDetailRow.PayedAmount = detailRow.PayedAmount;
                        }
                        if (!detailRow.IsRemainAmountNull()) {
                            newDetailRow.RemainAmount = detailRow.RemainAmount;
                        }
                        newDetailRow.AmountRMB = detailRow.AmountRMB;

                        newDetailTable.AddFormSalePaymentDetailRow(newDetailRow);
                    }
                }
                this.TAFormSalePaymentDetail.Update(newDetailTable);

                formPaymentRow.AmountRMB = totalAmountRMB;
                this.TAFormSalePayment.Update(formPaymentRow);

                //处理free goods明细
                if (RejectedFormID != null) {
                    FormDS.FormSaleSettlementRow settlement = this.GetFormSaleSettlementByID(formPaymentRow.FormSaleSettlementID);
                    MasterData.CustomerRow customer = new MasterDataBLL().GetCustomerById(settlement.CustomerID)[0];
                    FormDS.FormSalePaymentFreeGoodsDataTable newFGTable = new FormDS.FormSalePaymentFreeGoodsDataTable();
                    foreach (FormDS.FormSalePaymentFreeGoodsRow detailFGRow in this.FormDataSet.FormSalePaymentFreeGoods) {
                        // 与父表绑定
                        if (detailFGRow.RowState != DataRowState.Deleted) {
                            FormDS.FormSalePaymentFreeGoodsRow newDetailFGRow = newFGTable.NewFormSalePaymentFreeGoodsRow();
                            newDetailFGRow.FormSalePaymentID = formPaymentRow.FormSalePaymentID;
                            newDetailFGRow.SKUID = detailFGRow.SKUID;
                            newDetailFGRow.PackPerCase = detailFGRow.PackPerCase;
                            decimal deliveryPrice = new MasterDataBLL().GetSKUPriceByParameter(detailFGRow.SKUID, customer.CustomerTypeID, customer.CustomerChannelID);
                            newDetailFGRow.DeliveryPrice = deliveryPrice;
                            newDetailFGRow.Quantity = detailFGRow.Quantity;
                            newDetailFGRow.AmountRMB = decimal.Round(newDetailFGRow.Quantity * newDetailFGRow.DeliveryPrice, 2);
                            if (!detailFGRow.IsRemarkNull()) {
                                newDetailFGRow.Remark = detailFGRow.Remark;
                            }
                            newFGTable.AddFormSalePaymentFreeGoodsRow(newDetailFGRow);
                        }
                    }
                    this.TAFormSalePaymentFreeGoods.Update(newFGTable);
                } else {
                    foreach (FormDS.FormSalePaymentFreeGoodsRow detailFGRow in this.FormDataSet.FormSalePaymentFreeGoods) {
                        // 与父表绑定
                        if (detailFGRow.RowState != DataRowState.Deleted) {
                            detailFGRow.FormSalePaymentID = formPaymentRow.FormSalePaymentID;
                        }
                    }
                    this.TAFormSalePaymentFreeGoods.Update(this.FormDataSet.FormSalePaymentFreeGoods);
                }

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
                    MasterData.CustomerRow customer = new MasterDataBLL().GetCustomerById(this.TAFormSaleSettlement.GetDataByID(FormSaleSettlementID)[0].CustomerID)[0];
                    dic["CustomerChannel"] = new MasterDataBLL().GetCustomerChannelById(customer.CustomerChannelID)[0].CustomerChannelCode;
                    dic["KAType"] = customer.IsKaTypeNull() ? "" : customer.KaType;
                    dic["PaymentTypeID"] = PaymentTypeID;
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

        public void UpdatePaymentFreeGoods(int FormID, SystemEnums.FormType FormTypeID, SystemEnums.FormStatus StatusID, int InvoiceStatusID, int PaymentTypeID,
                string Remark, string AttachedFileName, string RealAttachedFileName, int? VendorID) {

            SqlTransaction transaction = null;
            try {
                //事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormSalePayment, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormSalePaymentDetail, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormSalePaymentFreeGoods, transaction);

                FormDS.FormRow formRow = this.TAForm.GetDataByID(FormID)[0];
                FormDS.FormSalePaymentRow formPaymentRow = this.TAFormSalePayment.GetDataByID(FormID)[0];

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
                if (AttachedFileName != null && AttachedFileName != string.Empty) {
                    formPaymentRow.AttachedFileName = AttachedFileName;
                } else {
                    formPaymentRow.SetAttachedFileNameNull();
                }
                if (RealAttachedFileName != null && RealAttachedFileName != string.Empty) {
                    formPaymentRow.RealAttachedFileName = RealAttachedFileName;
                } else {
                    formPaymentRow.SetRealAttachedFileNameNull();
                }
                if (Remark != null && Remark != string.Empty) {
                    formPaymentRow.Remark = Remark;
                }
                formPaymentRow.InvoiceStatusID = InvoiceStatusID;
                formPaymentRow.PaymentTypeID = PaymentTypeID;
                if (VendorID != null) {
                    formPaymentRow.VendorID = VendorID.GetValueOrDefault();
                }
                this.TAFormSalePayment.Update(formPaymentRow);


                //处理明细
                decimal totalAmountRMB = 0;
                foreach (FormDS.FormSalePaymentDetailRow detailRow in this.FormDataSet.FormSalePaymentDetail) {
                    if (detailRow.RowState != DataRowState.Deleted) {
                        totalAmountRMB += detailRow.AmountRMB;
                        detailRow.FormSalePaymentID = formPaymentRow.FormSalePaymentID;
                    }
                }
                this.TAFormSalePaymentDetail.Update(this.FormDataSet.FormSalePaymentDetail);

                formPaymentRow.AmountRMB = totalAmountRMB;
                this.TAFormSalePayment.Update(formPaymentRow);

                foreach (FormDS.FormSalePaymentFreeGoodsRow detailFGRow in this.FormDataSet.FormSalePaymentFreeGoods) {
                    // 与父表绑定
                    if (detailFGRow.RowState != DataRowState.Deleted) {
                        detailFGRow.FormSalePaymentID = formPaymentRow.FormSalePaymentID;
                    }
                }
                this.TAFormSalePaymentFreeGoods.Update(this.FormDataSet.FormSalePaymentFreeGoods);

                // 正式提交
                if (StatusID == SystemEnums.FormStatus.Awaiting) {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic["TotalAmount"] = totalAmountRMB;
                    MasterData.CustomerRow customer = new MasterDataBLL().GetCustomerById(this.TAFormSaleSettlement.GetDataByID(formPaymentRow.FormSaleSettlementID)[0].CustomerID)[0];
                    dic["CustomerChannel"] = new MasterDataBLL().GetCustomerChannelById(customer.CustomerChannelID)[0].CustomerChannelCode;
                    dic["KAType"] = customer.IsKaTypeNull() ? "" : customer.KaType;
                    dic["PaymentTypeID"] = PaymentTypeID;
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

        public void UpdatePaymentAfterDeliveryComplete(int FormID, int UserID) {
            SqlTransaction transaction = null;
            try {
                transaction = TableAdapterHelper.BeginTransaction(TAFormSalePayment);
                TableAdapterHelper.SetTransaction(TAFormSalePaymentDetail, transaction);
                FormDS.FormSalePaymentRow rowPayment = this.TAFormSalePayment.GetDataByID(FormID)[0];
                FormDS.FormSaleSettlementRow rowSettlement = new FormSaleSettlementTableAdapter().GetDataByID(rowPayment.FormSaleSettlementID)[0];
                FormDS.FormDeliveryGoodsDataTable tbDelivery = this.GetFormDeliveryGoodByFormID(FormID);
                FormDS.FormSalePaymentDetailDataTable tbPaymentDetail = this.GetFormSalePaymentDetailByPaymentID(FormID);
                //发货总金额
                decimal DeliveryAmount = 0;
                //申请总金额
                decimal ApplyAmount = rowPayment.AmountRMB;
                //已分配金额，按比例分配的最后一项，要用申请金额-已分配金额
                decimal UsedAmount = 0;
                foreach (FormDS.FormDeliveryGoodsRow item in tbDelivery) {
                    DeliveryAmount += item.AmountRMB;
                }

                if (tbPaymentDetail.Count > 0) {
                    FormDS.FormSalePaymentDetailRow rowPaymentDetail = null;
                    for (int i = 0; i < tbPaymentDetail.Count; i++) {
                        rowPaymentDetail = (FormDS.FormSalePaymentDetailRow)tbPaymentDetail[i];
                        if (i == tbPaymentDetail.Count - 1) {
                            rowPaymentDetail.GoodAmount = DeliveryAmount - UsedAmount;
                        } else {
                            rowPaymentDetail.GoodAmount = decimal.Round(DeliveryAmount * (rowPaymentDetail.AmountRMB / rowPayment.AmountRMB), 2);
                            UsedAmount += rowPaymentDetail.GoodAmount;
                        }
                    }
                }
                this.TAFormSalePaymentDetail.Update(tbPaymentDetail);
                rowPayment.IsDeliveryComplete = true;
                rowPayment.DeliveryCompleteDate = DateTime.Now;
                rowPayment.DeliveryCompleteUserID = UserID;
                this.TAFormSalePayment.Update(rowPayment);
                transaction.Commit();
            } catch (Exception) {
                throw;
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

        #region FormDeliveryGoods

        public int InsertFormDeliveryGoods(int FormID, int SKUID, string DeliveryNo, DateTime DeliveryDate, decimal Price, decimal Quantity, decimal AmountRMB) {
            return this.TAFormDeliveryGoods.Insert(FormID, SKUID, DeliveryNo, DeliveryDate, Price, Quantity, AmountRMB);
        }

        public int UpdateFormDeliveryGoods(int FormDeliveryGoodsID, string DeliveryNo, DateTime DeliveryDate, decimal Price, decimal Quantity, decimal AmountRMB) {
            FormDS.FormDeliveryGoodsDataTable l_dtFormDeliveryGoods = this.TAFormDeliveryGoods.GetDataByID(FormDeliveryGoodsID);
            FormDS.FormDeliveryGoodsRow l_drFormDeliveryGoods = l_dtFormDeliveryGoods[0];
            l_drFormDeliveryGoods.DeliveryNo = DeliveryNo;
            l_drFormDeliveryGoods.DeliveryDate = DeliveryDate;
            l_drFormDeliveryGoods.MAAUnitPrice = Price;
            l_drFormDeliveryGoods.Quantity = Quantity;
            l_drFormDeliveryGoods.AmountRMB = AmountRMB;
            return this.TAFormDeliveryGoods.Update(l_drFormDeliveryGoods);
        }

        //根据报销单号，发货单号，发货产品判断是否重复
        public FormDS.FormDeliveryGoodsDataTable GetDataByDeliveryNoAndSkuID(string DeliveryNo, int SkuID, string FormNo) {
            return this.TAFormDeliveryGoods.GetDataByDeliveryNoAndSkuID(DeliveryNo, SkuID, FormNo);
        }

        public FormDS.FormDeliveryGoodsDataTable GetFormDeliveryGoodByFormID(int FormID) {
            return this.TAFormDeliveryGoods.GetDataByFormID(FormID);
        }

        #endregion

        #region 生成结案单

        //生成结案单
        public void GenerateSettlement(int FormSaleApplyID) {
            FormDS.FormSaleApplyRow applyRow = this.TAFormSaleApply.GetDataByID(FormSaleApplyID)[0];
            if (!applyRow.IsDiscountTypeIDNull() && applyRow.DiscountTypeID == (int)SystemEnums.DiscountType.AdjustmentFactor) {
                this.TAFormSaleApply.GenerateSettlementForm(FormSaleApplyID);
            }
        }

        #endregion

    }
}
