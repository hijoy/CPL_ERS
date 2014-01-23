using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects.BudgetTableAdapters;
using System.Data.SqlClient;

namespace BusinessObjects {
    public class BudgetBLL {

        #region TableAdapter

        private BudgetManageFeeTableAdapter _TABudgetManageFee;
        public BudgetManageFeeTableAdapter TABudgetManageFee {
            get {
                if (this._TABudgetManageFee == null) {
                    this._TABudgetManageFee = new BudgetManageFeeTableAdapter();
                }
                return this._TABudgetManageFee;
            }
        }

        private BudgetManageFeeHistoryTableAdapter _TABudgetManageFeeHistory;
        public BudgetManageFeeHistoryTableAdapter TABudgetManageFeeHistory {
            get {
                if (this._TABudgetManageFeeHistory == null) {
                    this._TABudgetManageFeeHistory = new BudgetManageFeeHistoryTableAdapter();
                }
                return this._TABudgetManageFeeHistory;
            }
        }

        private BudgetSaleTableAdapter _TABudgetSale;
        public BudgetSaleTableAdapter TABudgetSale {
            get {
                if (this._TABudgetSale == null) {
                    this._TABudgetSale = new BudgetSaleTableAdapter();
                }
                return this._TABudgetSale;
            }
        }

        private BudgetSaleHistoryTableAdapter _TABudgetSaleHistory;
        public BudgetSaleHistoryTableAdapter TABudgetSaleHistory {
            get {
                if (this._TABudgetSaleHistory == null) {
                    this._TABudgetSaleHistory = new BudgetSaleHistoryTableAdapter();
                }
                return this._TABudgetSaleHistory;
            }
        }

        private BudgetMarketingTableAdapter _TABudgetMarketing;
        public BudgetMarketingTableAdapter TABudgetMarketing {
            get {
                if (this._TABudgetMarketing == null) {
                    this._TABudgetMarketing = new BudgetMarketingTableAdapter();
                }
                return this._TABudgetMarketing;
            }
        }

        private BudgetMarketingHistoryTableAdapter _TABudgetMarketingHistory;
        public BudgetMarketingHistoryTableAdapter TABudgetMarketingHistory {
            get {
                if (this._TABudgetMarketingHistory == null) {
                    this._TABudgetMarketingHistory = new BudgetMarketingHistoryTableAdapter();
                }
                return this._TABudgetMarketingHistory;
            }
        }

        private BudgetRDTableAdapter _TABudgetRD;
        public BudgetRDTableAdapter TABudgetRD {
            get {
                if (this._TABudgetRD == null) {
                    this._TABudgetRD = new BudgetRDTableAdapter();
                }
                return this._TABudgetRD;
            }
        }

        private BudgetRDHistoryTableAdapter _TABudgetRDHistory;
        public BudgetRDHistoryTableAdapter TABudgetRDHistory {
            get {
                if (this._TABudgetRDHistory == null) {
                    this._TABudgetRDHistory = new BudgetRDHistoryTableAdapter();
                }
                return this._TABudgetRDHistory;
            }
        }

        private BudgetPurchaseTableAdapter _TABudgetPurchase;
        public BudgetPurchaseTableAdapter TABudgetPurchase {
            get {
                if (this._TABudgetPurchase == null) {
                    this._TABudgetPurchase = new BudgetPurchaseTableAdapter();
                }
                return this._TABudgetPurchase;
            }
        }

        private BudgetPurchaseHistoryTableAdapter _TABudgetPurchaseHistory;
        public BudgetPurchaseHistoryTableAdapter TABudgetPurchaseHistory {
            get {
                if (this._TABudgetPurchaseHistory == null) {
                    this._TABudgetPurchaseHistory = new BudgetPurchaseHistoryTableAdapter();
                }
                return this._TABudgetPurchaseHistory;
            }
        }

        #endregion

        #region BudgetManageFee

        public Budget.BudgetManageFeeRow GetBudgetManageFeeByID(int BudgetManageFeeID) {
            return this.TABudgetManageFee.GetDataByID(BudgetManageFeeID)[0];
        }

        public void InsertBudgetManageFee(int OrganizationUnitID, int Year, decimal AOPBudget, decimal AOPRBudget, string ModifyReason, int UserID, int PositionID) {
            SqlTransaction transaction = null;
            try {

                //事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TABudgetManageFee);
                TableAdapterHelper.SetTransaction(this.TABudgetManageFeeHistory, transaction);

                //验证预算是否已存在，或者有父子部门，已设置过预算
                if ((int)this.TABudgetManageFee.SearchBudgetManageByIns(GetTreePathByOUID(OrganizationUnitID), Year) > 0) {
                    throw new MyException("预算重复设置或者上级或下级部门已经设置预算，不能增加！", "Insert failed, budget exists!");
                }

                // 父表
                Budget.BudgetManageFeeDataTable table = new Budget.BudgetManageFeeDataTable();
                Budget.BudgetManageFeeRow row = table.NewBudgetManageFeeRow();

                row.OrganizationUnitID = OrganizationUnitID;
                row.Year = Year;
                row.AOPBudget = AOPBudget;
                row.AOPRBudget = AOPRBudget;
                if (ModifyReason != null) {
                    row.ModifyReason = ModifyReason;
                }

                table.AddBudgetManageFeeRow(row);
                this.TABudgetManageFee.Update(table);

                // 子表
                Budget.BudgetManageFeeHistoryDataTable tableHistory = new Budget.BudgetManageFeeHistoryDataTable();
                Budget.BudgetManageFeeHistoryRow rowHistory = tableHistory.NewBudgetManageFeeHistoryRow();

                rowHistory.OrganizationUnitID = OrganizationUnitID;
                rowHistory.Year = Year;
                rowHistory.AOPBudget = AOPBudget;
                rowHistory.AOPRBudget = AOPRBudget;
                rowHistory.Action = "Create";
                rowHistory.ModifyDate = DateTime.Now;
                rowHistory.PositionID = PositionID;
                rowHistory.UserID = UserID;
                if (ModifyReason != null) {
                    rowHistory.ModifyReason = ModifyReason;
                }
                rowHistory.BudgetManageFeeID = row.BudgetManageFeeID;

                tableHistory.AddBudgetManageFeeHistoryRow(rowHistory);
                this.TABudgetManageFeeHistory.Update(tableHistory);

                transaction.Commit();
            } catch (Exception ex) {
                transaction.Rollback();
                throw ex;
            } finally {
                transaction.Dispose();
            }
        }

        public void UpdateBudgetManageFee(int BudgetManageFeeID, decimal AOPRBudget, int UserID, int PositionID, string ModifyReason) {
            SqlTransaction transaction = null;
            try {
                ////事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TABudgetManageFee);
                TableAdapterHelper.SetTransaction(this.TABudgetManageFeeHistory, transaction);

                // 父表                
                Budget.BudgetManageFeeRow row = this.TABudgetManageFee.GetDataByID(BudgetManageFeeID)[0];

                if (row.AOPRBudget > AOPRBudget) {
                    decimal[] calculateAssistant = this.GetPersonalBudgetByOUID(row.OrganizationUnitID, row.Year);
                    if (row.AOPRBudget - AOPRBudget > calculateAssistant[3]) {
                        throw new MyException("本次修改导致原有记录超预算，不能修改，目前部门剩余预算为：" + calculateAssistant[3], "Over budget,modify failed,current remain budget is :" + calculateAssistant[3]);
                    }
                }

                row.AOPRBudget = AOPRBudget;
                row.ModifyReason = ModifyReason;

                this.TABudgetManageFee.Update(row);

                // 子表
                Budget.BudgetManageFeeHistoryDataTable tableHistory = new Budget.BudgetManageFeeHistoryDataTable();
                Budget.BudgetManageFeeHistoryRow rowHistory = tableHistory.NewBudgetManageFeeHistoryRow();

                rowHistory.OrganizationUnitID = row.OrganizationUnitID;
                rowHistory.Year = row.Year;
                rowHistory.AOPBudget = row.AOPRBudget;
                rowHistory.AOPRBudget = row.AOPRBudget;
                rowHistory.AdjustBudget = row.AdjustBudget;
                rowHistory.Action = "Modify";
                rowHistory.ModifyDate = DateTime.Now;
                rowHistory.PositionID = PositionID;
                rowHistory.UserID = UserID;
                rowHistory.ModifyReason = ModifyReason;
                rowHistory.BudgetManageFeeID = row.BudgetManageFeeID;
                tableHistory.AddBudgetManageFeeHistoryRow(rowHistory);
                this.TABudgetManageFeeHistory.Update(tableHistory);

                transaction.Commit();
            } catch (Exception ex) {
                transaction.Rollback();
                throw ex;
            } finally {
                transaction.Dispose();
            }
        }

        public Budget.BudgetManageFeeDataTable GetPagedBudgetManageFee(string queryExpression, int startRowIndex, int maximumRows, string sortExpression) {
            return this.TABudgetManageFee.GetDataPaged("BudgetManageFee", sortExpression, startRowIndex, maximumRows, queryExpression);
        }

        public int QueryBudgetManageFeeTotalCount(string queryExpression) {
            return (int)this.TABudgetManageFee.QueryDataCount("BudgetManageFee", queryExpression);
        }

        public Budget.BudgetManageFeeHistoryDataTable GetBudgetManageFeeHistoryByParentID(int BudgetManageFeeID) {
            return this.TABudgetManageFeeHistory.GetDataByBudgetManageFeeID(BudgetManageFeeID);
        }

        public void DeleteBudgetManageFeeByID(int BudgetManageFeeID) {
            SqlTransaction transaction = null;
            try {
                transaction = TableAdapterHelper.BeginTransaction(TABudgetManageFee);
                TableAdapterHelper.SetTransaction(TABudgetManageFeeHistory, transaction);

                Budget.BudgetManageFeeRow row = this.TABudgetManageFee.GetDataByID(BudgetManageFeeID)[0];

                decimal[] temp = this.GetPersonalBudgetByOUID(row.OrganizationUnitID, row.Year);
                if (row.AOPRBudget > temp[3]) {
                    throw new MyException("删除将导致已有单据超预算，不能删除，目前部门剩余预算为：" + temp[3], "Over budget,delete failed,current remain budget is :" + temp[3]);
                }
                this.TABudgetManageFeeHistory.DeleteByBudgetManageFeeID(BudgetManageFeeID);
                this.TABudgetManageFee.DeleteByID(BudgetManageFeeID);
                transaction.Commit();
            } catch (Exception) {
                transaction.Rollback();
                throw;
            } finally {
                transaction.Dispose();
            }
        }

        public decimal[] GetPersonalBudgetByParameter(int PositionID, int Year) {

            decimal? TotalBudget = 0;
            decimal? ApprovedFee = 0;
            decimal? ApprovingFee = 0;
            decimal? RemainFee = 0;
            decimal[] calculateAssistant = new decimal[4];

            this.TABudgetManageFee.GetPersonalBudgetByParameter(PositionID, Year, ref TotalBudget, ref ApprovedFee, ref ApprovingFee, ref RemainFee);
            calculateAssistant[0] = TotalBudget.GetValueOrDefault();
            calculateAssistant[1] = ApprovedFee.GetValueOrDefault();
            calculateAssistant[2] = ApprovingFee.GetValueOrDefault();
            calculateAssistant[3] = RemainFee.GetValueOrDefault();

            return calculateAssistant;
        }

        public decimal[] GetPersonalBudgetByOUID(int OUID, int Year) {

            decimal? TotalBudget = 0;
            decimal? ApprovedFee = 0;
            decimal? ApprovingFee = 0;
            decimal? RemainFee = 0;
            decimal[] calculateAssistant = new decimal[4];

            this.TABudgetManageFee.GetPersonalBudgetByOUID(OUID, Year, ref TotalBudget, ref ApprovedFee, ref ApprovingFee, ref RemainFee);
            calculateAssistant[0] = TotalBudget.GetValueOrDefault();
            calculateAssistant[1] = ApprovedFee.GetValueOrDefault();
            calculateAssistant[2] = ApprovingFee.GetValueOrDefault();
            calculateAssistant[3] = RemainFee.GetValueOrDefault();

            return calculateAssistant;
        }

        #endregion

        #region BudgetSale

        public Budget.BudgetSaleRow GetBudgetSaleByID(int BudgetSaleID) {
            return this.TABudgetSale.GetDataByID(BudgetSaleID)[0];
        }

        public void InsertBudgetSale(int OrganizationUnitID, int CustomerChannelID, int BrandID, int ExpenseCategoryID,
            string FPeriod, decimal? AOPBudget, decimal? AOPRBudget, decimal? ProjectionBudget, object AdjustBudgetStr, string ModifyReason, int UserID,
            int PositionID) {

            SqlTransaction transaction = null;
            try {
                ////事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TABudgetSale);
                TableAdapterHelper.SetTransaction(this.TABudgetSaleHistory, transaction);

                //增加重复验证
                if ((int)this.TABudgetSale.SearchBudgetSaleByTreePath(GetTreePathByOUID(OrganizationUnitID), DateTime.Parse(FPeriod.Substring(0, 4) + "-" + FPeriod.Substring(4, 2) + "-01"),OrganizationUnitID) > 0) {
                    throw new MyException("本财月预算重复（上级或下级部门已设置预算）！", "Budget of this month exists!");
                }

                //增加上级或下级预算重复验证
                if ((int)this.TABudgetSale.SearchBudgetSaleByIns(OrganizationUnitID, CustomerChannelID, BrandID, ExpenseCategoryID, DateTime.Parse(FPeriod.Substring(0, 4) + "-" + FPeriod.Substring(4, 2) + "-01")) > 0) {
                    throw new MyException("预算重复设置", "budget exists ");
                }

                // 父表
                Budget.BudgetSaleDataTable table = new Budget.BudgetSaleDataTable();
                Budget.BudgetSaleRow row = table.NewBudgetSaleRow();

                decimal? AdjustBudget = null;
                if (AdjustBudgetStr != null && !string.IsNullOrEmpty(AdjustBudgetStr.ToString())) {
                    AdjustBudget = decimal.Parse(AdjustBudgetStr.ToString());
                }

                row.OrganizationUnitID = OrganizationUnitID;
                row.CustomerChannelID = CustomerChannelID;
                row.BrandID = BrandID;
                row.ExpenseCategoryID = ExpenseCategoryID;
                row.FPeriod = DateTime.Parse(FPeriod.Substring(0, 4) + "-" + FPeriod.Substring(4, 2) + "-01");
                row.AOPBudget = AOPBudget.GetValueOrDefault();
                row.AOPRBudget = AOPRBudget.GetValueOrDefault();
                row.ProjectionBudget = ProjectionBudget.GetValueOrDefault();
                row.AdjustBudget = AdjustBudget.GetValueOrDefault();
                if (ModifyReason != null) {
                    row.ModifyReason = ModifyReason;
                }
                table.AddBudgetSaleRow(row);
                this.TABudgetSale.Update(table);

                // 子表
                Budget.BudgetSaleHistoryDataTable tableDetail = new Budget.BudgetSaleHistoryDataTable();
                Budget.BudgetSaleHistoryRow rowDetail = tableDetail.NewBudgetSaleHistoryRow();

                rowDetail.OrganizationUnitID = row.OrganizationUnitID;
                rowDetail.CustomerChannelID = row.CustomerChannelID;
                rowDetail.BrandID = row.BrandID;
                rowDetail.ExpenseCategoryID = row.ExpenseCategoryID;
                rowDetail.FPeriod = row.FPeriod;
                rowDetail.AOPBudget = row.AOPBudget;
                rowDetail.AOPRBudget = row.AOPRBudget;
                rowDetail.ProjectionBudget = row.ProjectionBudget;
                rowDetail.AdjustBudget = row.AdjustBudget;
                rowDetail.AdjustBudget = AdjustBudget.GetValueOrDefault();
                rowDetail.Action = "Create";
                rowDetail.ModifyDate = DateTime.Now;
                rowDetail.PositionID = PositionID;
                rowDetail.UserID = UserID;
                if (ModifyReason != null) {
                    rowDetail.ModifyReason = ModifyReason;
                }
                rowDetail.BudgetSaleID = row.BudgetSaleID;
                tableDetail.AddBudgetSaleHistoryRow(rowDetail);
                this.TABudgetSaleHistory.Update(tableDetail);

                transaction.Commit();
            } catch (Exception ex) {
                transaction.Rollback();
                throw ex;
            } finally {
                transaction.Dispose();
            }
        }

        public void UpdateBudgetSale(int BudgetSaleID, decimal? ProjectionBudget, decimal? AdjustBudget, string ModifyReason,
            int UserID, int PositionID) {

            SqlTransaction transaction = null;
            try {
                ////事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TABudgetSale);
                TableAdapterHelper.SetTransaction(this.TABudgetSaleHistory, transaction);


                // 父表
                Budget.BudgetSaleRow row = this.TABudgetSale.GetDataByID(BudgetSaleID)[0];

                if (row.ProjectionBudget > ProjectionBudget) {
                    decimal[] calculateAssistant = this.GetSalesBudgetByOUID(row.OrganizationUnitID, row.FPeriod, row.ExpenseCategoryID, row.CustomerChannelID, row.BrandID);
                    if (row.ProjectionBudget - ProjectionBudget > calculateAssistant[5]) {
                        throw new MyException("本次修改导致原有记录超预算，不能修改，目前部门剩余预算为：" + calculateAssistant[5], "Over budget,modify failed,current remain budget is :" + calculateAssistant[5]);
                    }
                }

                row.ProjectionBudget = ProjectionBudget.GetValueOrDefault();
                row.AdjustBudget = AdjustBudget.GetValueOrDefault();
                if (ModifyReason != null) {
                    row.ModifyReason = ModifyReason;
                }
                this.TABudgetSale.Update(row);

                // 子表
                Budget.BudgetSaleHistoryDataTable tableDetail = new Budget.BudgetSaleHistoryDataTable();
                Budget.BudgetSaleHistoryRow rowDetail = tableDetail.NewBudgetSaleHistoryRow();

                rowDetail.OrganizationUnitID = row.OrganizationUnitID;
                rowDetail.CustomerChannelID = row.CustomerChannelID;
                rowDetail.BrandID = row.BrandID;
                rowDetail.ExpenseCategoryID = row.ExpenseCategoryID;
                rowDetail.FPeriod = row.FPeriod;
                rowDetail.AOPBudget = row.AOPBudget;
                rowDetail.AOPRBudget = row.AOPRBudget;
                rowDetail.ProjectionBudget = row.ProjectionBudget;
                rowDetail.AdjustBudget = row.AdjustBudget;
                rowDetail.AdjustBudget = AdjustBudget.GetValueOrDefault();
                rowDetail.Action = "Modify";
                rowDetail.ModifyDate = DateTime.Now;
                rowDetail.PositionID = PositionID;
                rowDetail.UserID = UserID;
                if (ModifyReason != null) {
                    rowDetail.ModifyReason = ModifyReason;
                }
                rowDetail.BudgetSaleID = row.BudgetSaleID;
                tableDetail.AddBudgetSaleHistoryRow(rowDetail);
                this.TABudgetSaleHistory.Update(tableDetail);

                transaction.Commit();
            } catch (Exception ex) {
                transaction.Rollback();
                throw ex;
            } finally {
                transaction.Dispose();
            }
        }

        public Budget.BudgetSaleDataTable GetPagedBudgetSale(string queryExpression, int startRowIndex, int maximumRows, string sortExpression) {
            if (queryExpression == null || queryExpression.Length == 0) {
                queryExpression = "BudgetSaleID is not null";
            }
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "FPeriod Desc";
            }
            return this.TABudgetSale.GetDataPaged("BudgetSale", sortExpression, startRowIndex, maximumRows, queryExpression);
        }

        public int QueryBudgetSaleTotalCount(string queryExpression) {
            return (int)this.TABudgetManageFee.QueryDataCount("BudgetSale", queryExpression);
        }

        public void DeleteBudgetSaleByID(int BudgetSaleID) {
            SqlTransaction transaction = null;
            try {
                transaction = TableAdapterHelper.BeginTransaction(TABudgetSale);
                TableAdapterHelper.SetTransaction(TABudgetSaleHistory, transaction);

                Budget.BudgetSaleRow row = this.TABudgetSale.GetDataByID(BudgetSaleID)[0];

                decimal[] temp = this.GetSalesBudgetByOUID(row.OrganizationUnitID, row.FPeriod, row.ExpenseCategoryID, row.CustomerChannelID, row.BrandID);
                if (row.ProjectionBudget > temp[5]) {
                    throw new MyException("删除将导致已有单据超预算，不能删除，目前部门剩余预算为：" + temp[5], "Over budget,delete failed,current remain budget is :" + temp[5]);
                }
                TABudgetSaleHistory.DeleteByBudgetSaleID(BudgetSaleID);
                TABudgetSale.Delete(BudgetSaleID);
                transaction.Commit();
            } catch (Exception) {
                transaction.Rollback();
                throw;
            } finally {
                transaction.Dispose();
            }
        }

        public Budget.BudgetSaleHistoryDataTable GetBudgetSaleHistory(int BudgetSaleID) {
            return TABudgetSaleHistory.GetDataByBudgetSaleID(BudgetSaleID);
        }

        public decimal[] GetSalesBudgetByParameter(int PositionID, DateTime FPeriod, int ExpenseCategoryID, int CustomerChannelID, int BrandID) {
            decimal? TotalBudget = 0;
            decimal? ApprovedAmount = 0;
            decimal? ApprovingAmount = 0;
            decimal? CompletedAmount = 0;
            decimal? ReimbursedAmount = 0;
            decimal? RemainBudget = 0;
            decimal[] calculateAssistant = new decimal[6];
            this.TABudgetSale.GetSaleBudgetByParameter(PositionID, FPeriod, ExpenseCategoryID, CustomerChannelID, BrandID, ref TotalBudget, ref ApprovedAmount,
                ref ApprovingAmount, ref CompletedAmount, ref ReimbursedAmount, ref RemainBudget);
            calculateAssistant[0] = TotalBudget.GetValueOrDefault();
            calculateAssistant[1] = ApprovedAmount.GetValueOrDefault();
            calculateAssistant[2] = ApprovingAmount.GetValueOrDefault();
            calculateAssistant[3] = CompletedAmount.GetValueOrDefault();
            calculateAssistant[4] = ReimbursedAmount.GetValueOrDefault();
            calculateAssistant[5] = RemainBudget.GetValueOrDefault();

            return calculateAssistant;
        }

        public decimal[] GetSalesBudgetByOUID(int OUID, DateTime FPeriod, int ExpenseSubCategoryID, int CustomerChannelID, int BrandID) {
            decimal? TotalBudget = 0;
            decimal? ApprovedAmount = 0;
            decimal? ApprovingAmount = 0;
            decimal? CompletedAmount = 0;
            decimal? ReimbursedAmount = 0;
            decimal? RemainBudget = 0;
            decimal[] calculateAssistant = new decimal[6];
            this.TABudgetSale.GetSaleBudgetByOUID(OUID, FPeriod, ExpenseSubCategoryID, CustomerChannelID, BrandID, ref TotalBudget, ref ApprovedAmount,
                ref ApprovingAmount, ref CompletedAmount, ref ReimbursedAmount, ref RemainBudget);
            calculateAssistant[0] = TotalBudget.GetValueOrDefault();
            calculateAssistant[1] = ApprovedAmount.GetValueOrDefault();
            calculateAssistant[2] = ApprovingAmount.GetValueOrDefault();
            calculateAssistant[3] = CompletedAmount.GetValueOrDefault();
            calculateAssistant[4] = ReimbursedAmount.GetValueOrDefault();
            calculateAssistant[5] = RemainBudget.GetValueOrDefault();
            return calculateAssistant;
        }

        #endregion

        #region BudgetMarketing

        public Budget.BudgetMarketingRow GetBudgetMarketingByID(int BudgetMarketingID) {
            return this.TABudgetMarketing.GetDataByID(BudgetMarketingID)[0];
        }

        public void InsertBudgetMarketing(int OrganizationUnitID, int CustomerChannelID, int BrandID, int ExpenseCategoryID,
            string FPeriod, decimal? AOPBudget, decimal? AOPRBudget, decimal? ProjectionBudget, object AdjustBudgetStr, string ModifyReason, int UserID,
            int PositionID) {

            SqlTransaction transaction = null;
            try {
                ////事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TABudgetMarketing);
                TableAdapterHelper.SetTransaction(this.TABudgetMarketingHistory, transaction);

                //增加重复验证
                if ((int)this.TABudgetMarketing.SearchBudgetMarketingByIns(OrganizationUnitID, CustomerChannelID, BrandID, DateTime.Parse(FPeriod.Substring(0, 4) + "-" + FPeriod.Substring(4, 2) + "-01"), ExpenseCategoryID) > 0) {
                    throw new MyException("预算重复设置", "budget exists ");
                }

                //增加上级或下级部门重复验证
                if ((int)this.TABudgetMarketing.SearchBudgetMarketingByTreePath(GetTreePathByOUID(OrganizationUnitID), DateTime.Parse(FPeriod.Substring(0, 4) + "-" + FPeriod.Substring(4, 2) + "-01"), OrganizationUnitID) > 0) {
                    throw new MyException("本财月预算重复（上级或下级部门已设置预算）！", "Budget of this month exists!");
                }

                // 父表
                Budget.BudgetMarketingDataTable table = new Budget.BudgetMarketingDataTable();
                Budget.BudgetMarketingRow row = table.NewBudgetMarketingRow();

                decimal? AdjustBudget = null;
                if (AdjustBudgetStr != null && !string.IsNullOrEmpty(AdjustBudgetStr.ToString())) {
                    AdjustBudget = decimal.Parse(AdjustBudgetStr.ToString());
                }

                row.OrganizationUnitID = OrganizationUnitID;
                row.CustomerChannelID = CustomerChannelID;
                row.BrandID = BrandID;
                row.ExpenseCategoryID = ExpenseCategoryID;
                row.FPeriod = DateTime.Parse(FPeriod.Substring(0, 4) + "-" + FPeriod.Substring(4, 2) + "-01");
                row.AOPBudget = AOPBudget.GetValueOrDefault();
                row.AOPRBudget = AOPRBudget.GetValueOrDefault();
                row.ProjectionBudget = ProjectionBudget.GetValueOrDefault();
                row.AdjustBudget = AdjustBudget.GetValueOrDefault();
                if (ModifyReason != null) {
                    row.ModifyReason = ModifyReason;
                }
                table.AddBudgetMarketingRow(row);
                this.TABudgetMarketing.Update(table);

                // 子表
                Budget.BudgetMarketingHistoryDataTable tableDetail = new Budget.BudgetMarketingHistoryDataTable();
                Budget.BudgetMarketingHistoryRow rowDetail = tableDetail.NewBudgetMarketingHistoryRow();

                rowDetail.OrganizationUnitID = row.OrganizationUnitID;
                rowDetail.CustomerChannelID = row.CustomerChannelID;
                rowDetail.BrandID = row.BrandID;
                rowDetail.ExpenseCategoryID = row.ExpenseCategoryID;
                rowDetail.FPeriod = row.FPeriod;
                rowDetail.AOPBudget = row.AOPBudget;
                rowDetail.AOPRBudget = row.AOPRBudget;
                rowDetail.ProjectionBudget = row.ProjectionBudget;
                rowDetail.AdjustBudget = row.AdjustBudget;
                rowDetail.Action = "Create";
                rowDetail.ModifyDate = DateTime.Now;
                rowDetail.PositionID = PositionID;
                rowDetail.UserID = UserID;
                if (ModifyReason != null) {
                    rowDetail.ModifyReason = ModifyReason;
                }
                rowDetail.BudgetMarketingID = row.BudgetMarketingID;
                tableDetail.AddBudgetMarketingHistoryRow(rowDetail);
                this.TABudgetMarketingHistory.Update(tableDetail);

                transaction.Commit();
            } catch (Exception ex) {
                transaction.Rollback();
                throw ex;
            } finally {
                transaction.Dispose();
            }
        }

        public void UpdateBudgetMarketing(int BudgetMarketingID, decimal? ProjectionBudget, decimal? AdjustBudget, string ModifyReason,
            int UserID, int PositionID) {

            SqlTransaction transaction = null;
            try {
                //事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TABudgetMarketing);
                TableAdapterHelper.SetTransaction(this.TABudgetMarketingHistory, transaction);

                // 父表
                Budget.BudgetMarketingRow row = this.TABudgetMarketing.GetDataByID(BudgetMarketingID)[0];

                if (row.ProjectionBudget > ProjectionBudget) {
                    decimal[] calculateAssistant = this.GetMarketingBudgetByOUID(row.OrganizationUnitID, row.FPeriod, row.CustomerChannelID, row.BrandID, row.ExpenseCategoryID);
                    if (row.ProjectionBudget - ProjectionBudget > calculateAssistant[4]) {
                        throw new MyException("本次修改导致原有记录超预算，不能修改，目前部门剩余预算为：" + calculateAssistant[4], "Over budget,modify failed,current remain budget is :" + calculateAssistant[4]);
                    }
                }

                row.ProjectionBudget = ProjectionBudget.GetValueOrDefault();
                row.AdjustBudget = AdjustBudget.GetValueOrDefault();
                if (ModifyReason != null) {
                    row.ModifyReason = ModifyReason;
                }
                this.TABudgetMarketing.Update(row);

                // 子表
                Budget.BudgetMarketingHistoryDataTable tableDetail = new Budget.BudgetMarketingHistoryDataTable();
                Budget.BudgetMarketingHistoryRow rowDetail = tableDetail.NewBudgetMarketingHistoryRow();

                rowDetail.OrganizationUnitID = row.OrganizationUnitID;
                rowDetail.CustomerChannelID = row.CustomerChannelID;
                rowDetail.BrandID = row.BrandID;
                rowDetail.ExpenseCategoryID = row.ExpenseCategoryID;
                rowDetail.FPeriod = row.FPeriod;
                rowDetail.AOPBudget = row.AOPBudget;
                rowDetail.AOPRBudget = row.AOPRBudget;
                rowDetail.ProjectionBudget = row.ProjectionBudget;
                rowDetail.AdjustBudget = row.AdjustBudget;
                rowDetail.Action = "Modify";
                rowDetail.ModifyDate = DateTime.Now;
                rowDetail.PositionID = PositionID;
                rowDetail.UserID = UserID;
                if (ModifyReason != null) {
                    rowDetail.ModifyReason = ModifyReason;
                }
                rowDetail.BudgetMarketingID = row.BudgetMarketingID;

                tableDetail.AddBudgetMarketingHistoryRow(rowDetail);
                this.TABudgetMarketingHistory.Update(tableDetail);

                transaction.Commit();
            } catch (Exception ex) {
                transaction.Rollback();
                throw ex;
            } finally {
                transaction.Dispose();
            }
        }

        public Budget.BudgetMarketingDataTable GetPagedBudgetMarketing(string queryExpression, int startRowIndex, int maximumRows, string sortExpression) {
            return this.TABudgetMarketing.GetDataPaged("BudgetMarketing", sortExpression, startRowIndex, maximumRows, queryExpression);
        }

        public int QueryBudgetMarketingTotalCount(string queryExpression) {
            return (int)this.TABudgetManageFee.QueryDataCount("BudgetMarketing", queryExpression);
        }

        public void DeleteBudgetMarketingByID(int BudgetMarketingID) {
            SqlTransaction transaction = null;
            try {
                transaction = TableAdapterHelper.BeginTransaction(TABudgetMarketing);
                TableAdapterHelper.SetTransaction(TABudgetMarketingHistory, transaction);

                Budget.BudgetMarketingRow row = this.TABudgetMarketing.GetDataByID(BudgetMarketingID)[0];

                decimal[] temp = this.GetMarketingBudgetByOUID(row.OrganizationUnitID, row.FPeriod, row.CustomerChannelID, row.BrandID, row.ExpenseCategoryID);
                if (row.ProjectionBudget > temp[4]) {
                    throw new MyException("删除将导致已有单据超预算，不能删除，目前部门剩余预算为：" + temp[4], "Over budget,delete failed,current remain budget is :" + temp[4]);
                }
                TABudgetMarketingHistory.DeleteByBudgetMarketingID(BudgetMarketingID);
                TABudgetMarketing.DeleteByID(BudgetMarketingID);
                transaction.Commit();
            } catch (Exception) {
                transaction.Rollback();
                throw;
            } finally {
                transaction.Dispose();
            }
        }

        public Budget.BudgetMarketingHistoryDataTable GetBudgetMarketingHistory(int BudgetMarketingID) {
            return TABudgetMarketingHistory.GetDataByBudgetMarketingID(BudgetMarketingID);
        }

        public decimal[] GetMarketingBudgetByParameter(int PositionID, DateTime FPeriod, int CustomerChannelID, int BrandID, int ExpenseCategoryID) {
            decimal? TotalBudget = 0;
            decimal? ApprovedAmount = 0;
            decimal? ApprovingAmount = 0;
            decimal? ReimbursedAmount = 0;
            decimal? RemainBudget = 0;
            decimal[] calculateAssistant = new decimal[5];
            this.TABudgetMarketing.GetMarketingBudgetByParameter(PositionID, FPeriod, CustomerChannelID, BrandID, ExpenseCategoryID, ref TotalBudget, ref ApprovedAmount,
                ref ApprovingAmount, ref ReimbursedAmount, ref RemainBudget);
            calculateAssistant[0] = TotalBudget.GetValueOrDefault();
            calculateAssistant[1] = ApprovedAmount.GetValueOrDefault();
            calculateAssistant[2] = ApprovingAmount.GetValueOrDefault();
            calculateAssistant[3] = ReimbursedAmount.GetValueOrDefault();
            calculateAssistant[4] = RemainBudget.GetValueOrDefault();

            return calculateAssistant;
        }

        public decimal[] GetMarketingBudgetByOUID(int OUID, DateTime FPeriod, int CustomerChannelID, int BrandID, int ExpenseCategoryID) {
            decimal? TotalBudget = 0;
            decimal? ApprovedAmount = 0;
            decimal? ApprovingAmount = 0;
            decimal? ReimbursedAmount = 0;
            decimal? RemainBudget = 0;
            decimal[] calculateAssistant = new decimal[5];
            this.TABudgetMarketing.GetMarketingBudgetByOUID(OUID, FPeriod, CustomerChannelID, BrandID, ExpenseCategoryID, ref TotalBudget, ref ApprovedAmount,
                ref ApprovingAmount, ref ReimbursedAmount, ref RemainBudget);
            calculateAssistant[0] = TotalBudget.GetValueOrDefault();
            calculateAssistant[1] = ApprovedAmount.GetValueOrDefault();
            calculateAssistant[2] = ApprovingAmount.GetValueOrDefault();
            calculateAssistant[3] = ReimbursedAmount.GetValueOrDefault();
            calculateAssistant[4] = RemainBudget.GetValueOrDefault();

            return calculateAssistant;
        }

        #endregion

        #region BudgetPurchase

        public Budget.BudgetPurchaseRow GetBudgetPurchaseByID(int BudgetPurchaseID) {
            return this.TABudgetPurchase.GetDataByID(BudgetPurchaseID)[0];
        }

        public void InsertBudgetPurchase(int OrganizationUnitID, int PurchaseBudgetTypeID, int FYear, decimal? AOPBudget, decimal? AOPRBudget, int UserID, int PositionID, string ModifyReason) {

            SqlTransaction transaction = null;
            try {
                ////事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TABudgetPurchase);
                TableAdapterHelper.SetTransaction(this.TABudgetPurchaseHistory, transaction);

                //增加重复验证
                if ((int)this.TABudgetPurchase.SearchBudgetPurchaseByIns(OrganizationUnitID, FYear, PurchaseBudgetTypeID) > 0) {
                    throw new MyException("预算重复设置", "budget exists ");
                }

                //增加上级或下级部门重复验证
                if ((int)this.TABudgetPurchase.SearchBudgetPurchaseByTreePath(GetTreePathByOUID(OrganizationUnitID), FYear, OrganizationUnitID) > 0) {
                    throw new MyException("本财月预算重复（上级或下级部门已设置预算）！", "Budget of this month exists!");
                }

                // 父表
                Budget.BudgetPurchaseDataTable table = new Budget.BudgetPurchaseDataTable();
                Budget.BudgetPurchaseRow row = table.NewBudgetPurchaseRow();

                row.OrganizationUnitID = OrganizationUnitID;
                row.PurchaseBudgetTypeID = PurchaseBudgetTypeID;
                row.FYear = FYear;
                row.AOPBudget = AOPBudget.GetValueOrDefault();
                row.AOPRBudget = AOPRBudget.GetValueOrDefault();
                if (ModifyReason != null) {
                    row.ModifyReason = ModifyReason;
                }
                table.AddBudgetPurchaseRow(row);
                this.TABudgetPurchase.Update(table);

                // 子表
                Budget.BudgetPurchaseHistoryDataTable tableDetail = new Budget.BudgetPurchaseHistoryDataTable();
                Budget.BudgetPurchaseHistoryRow rowDetail = tableDetail.NewBudgetPurchaseHistoryRow();

                rowDetail.OrganizationUnitID = row.OrganizationUnitID;
                rowDetail.PurchaseBudgetTypeID = row.PurchaseBudgetTypeID;
                rowDetail.FYear = row.FYear;
                rowDetail.AOPBudget = row.AOPBudget;
                rowDetail.AOPRBudget = row.AOPRBudget;
                rowDetail.AdjustBudget = row.AdjustBudget;
                rowDetail.Action = "Create";
                rowDetail.ModifyDate = DateTime.Now;
                rowDetail.PositionID = PositionID;
                rowDetail.UserID = UserID;
                if (ModifyReason != null) {
                    rowDetail.ModifyReason = ModifyReason;
                }
                rowDetail.BudgetPurchaseID = row.BudgetPurchaseID;
                tableDetail.AddBudgetPurchaseHistoryRow(rowDetail);
                this.TABudgetPurchaseHistory.Update(tableDetail);

                transaction.Commit();
            } catch (Exception ex) {
                transaction.Rollback();
                throw ex;
            } finally {
                transaction.Dispose();
            }
        }

        public void UpdateBudgetPurchase(int BudgetPurchaseID, decimal? AOPBudget, decimal? AOPRBudget,
            int UserID, int PositionID, string ModifyReason) {

            SqlTransaction transaction = null;
            try {
                //事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TABudgetPurchase);
                TableAdapterHelper.SetTransaction(this.TABudgetPurchaseHistory, transaction);

                // 父表
                Budget.BudgetPurchaseRow row = this.TABudgetPurchase.GetDataByID(BudgetPurchaseID)[0];

                //删除前检查是否会超预算
                if (row.AOPRBudget > AOPRBudget) {
                    decimal[] calculateAssistant = this.GetManagingBudgetByOUID(row.OrganizationUnitID, row.FYear, row.PurchaseBudgetTypeID);
                    if (row.AOPRBudget - AOPRBudget > calculateAssistant[5]) {
                        throw new MyException("本次修改导致原有记录超预算，不能修改，目前部门剩余预算为：" + calculateAssistant[5], "Over budget,modify failed,current remain budget is :" + calculateAssistant[5]);
                    }
                }


                row.AOPBudget = AOPBudget.GetValueOrDefault();
                row.AOPRBudget = AOPRBudget.GetValueOrDefault();
                if (ModifyReason != null) {
                    row.ModifyReason = ModifyReason;
                }
                this.TABudgetPurchase.Update(row);

                // 子表
                Budget.BudgetPurchaseHistoryDataTable tableDetail = new Budget.BudgetPurchaseHistoryDataTable();
                Budget.BudgetPurchaseHistoryRow rowDetail = tableDetail.NewBudgetPurchaseHistoryRow();

                rowDetail.OrganizationUnitID = row.OrganizationUnitID;
                rowDetail.PurchaseBudgetTypeID = row.PurchaseBudgetTypeID;
                rowDetail.FYear = row.FYear;
                rowDetail.AOPBudget = row.AOPBudget;
                rowDetail.AOPRBudget = row.AOPRBudget;
                rowDetail.AdjustBudget = row.AdjustBudget;
                rowDetail.Action = "Modify";
                rowDetail.ModifyDate = DateTime.Now;
                rowDetail.PositionID = PositionID;
                rowDetail.UserID = UserID;
                if (ModifyReason != null) {
                    rowDetail.ModifyReason = ModifyReason;
                }
                rowDetail.BudgetPurchaseID = row.BudgetPurchaseID;

                tableDetail.AddBudgetPurchaseHistoryRow(rowDetail);
                this.TABudgetPurchaseHistory.Update(tableDetail);

                transaction.Commit();
            } catch (Exception ex) {
                transaction.Rollback();
                throw ex;
            } finally {
                transaction.Dispose();
            }
        }

        public Budget.BudgetPurchaseDataTable GetPagedBudgetPurchase(string queryExpression, int startRowIndex, int maximumRows, string sortExpression) {
            if (queryExpression == null || queryExpression.Length == 0) {
                queryExpression = "BudgetPurchaseID is not null";
            }
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "FYear Desc";
            }
            return this.TABudgetPurchase.GetDataPaged("BudgetPurchase", sortExpression, startRowIndex, maximumRows, queryExpression);
        }

        public int QueryBudgetPurchaseTotalCount(string queryExpression) {
            return (int)this.TABudgetManageFee.QueryDataCount("BudgetPurchase", queryExpression);
        }

        public void DeleteBudgetPurchaseByID(int BudgetPurchaseID) {

            SqlTransaction transaction = null;
            try {
                transaction = TableAdapterHelper.BeginTransaction(TABudgetPurchase);
                TableAdapterHelper.SetTransaction(TABudgetPurchaseHistory, transaction);

                Budget.BudgetPurchaseRow row = this.TABudgetPurchase.GetDataByID(BudgetPurchaseID)[0];

                decimal[] calculateAssistant = this.GetManagingBudgetByOUID(row.OrganizationUnitID, row.FYear, row.PurchaseBudgetTypeID);
                //删除前检查是否会超预算
                if (row.AOPRBudget > calculateAssistant[5]) {
                    throw new MyException("删除将导致已有单据超预算，不能删除，目前部门剩余预算为：" + calculateAssistant[5], "Over budget,delete failed,current remain budget is :" + calculateAssistant[5]);
                }
                TABudgetPurchaseHistory.DeleteByBudgetPurchaseID(BudgetPurchaseID);
                TABudgetPurchase.DeleteByID(BudgetPurchaseID);
                transaction.Commit();
            } catch (Exception) {
                transaction.Rollback();
                throw;
            } finally {
                transaction.Dispose();
            }
        }

        public Budget.BudgetPurchaseHistoryDataTable GetBudgetPurchaseHistory(int BudgetPurchaseID) {
            return TABudgetPurchaseHistory.GetDataByBudgetPurchaseID(BudgetPurchaseID);
        }

        public decimal[] GetPurchaseBudgetByParameter(int PositionID, DateTime Period, int PurchaseBudgetTypeID) {
            decimal? TotalBudget = 0;
            decimal? ApprovedAmount = 0;
            decimal? ApprovingAmount = 0;
            decimal? ReimbursedAmount = 0;
            decimal? NonReimbursedAmount = 0;
            decimal? RemainBudget = 0;
            decimal[] calculateAssistant = new decimal[6];
            this.TABudgetPurchase.GetPurchaseBudgetByParameter(PositionID, Period, PurchaseBudgetTypeID, ref TotalBudget, ref ApprovedAmount,
                ref ApprovingAmount, ref ReimbursedAmount, ref NonReimbursedAmount, ref RemainBudget);
            calculateAssistant[0] = TotalBudget.GetValueOrDefault();
            calculateAssistant[1] = ApprovedAmount.GetValueOrDefault();
            calculateAssistant[2] = ApprovingAmount.GetValueOrDefault();
            calculateAssistant[3] = ReimbursedAmount.GetValueOrDefault();
            calculateAssistant[4] = NonReimbursedAmount.GetValueOrDefault();
            calculateAssistant[5] = RemainBudget.GetValueOrDefault();
            return calculateAssistant;
        }

        public decimal[] GetPurchaseBudgetByOUID(int OUID, int Year, int PurchaseBudgetTypeID) {
            decimal? TotalBudget = 0;
            decimal? ApprovedAmount = 0;
            decimal? ApprovingAmount = 0;
            decimal? ReimbursedAmount = 0;
            decimal? NonReimbursedAmount = 0;
            decimal? RemainBudget = 0;
            decimal[] calculateAssistant = new decimal[6];
            this.TABudgetPurchase.GetPurchaseBudgetByOUID(OUID, DateTime.Parse(Year + "-01-01"), PurchaseBudgetTypeID, ref TotalBudget, ref ApprovedAmount,
                ref ApprovingAmount, ref ReimbursedAmount, ref NonReimbursedAmount, ref RemainBudget);
            calculateAssistant[0] = TotalBudget.GetValueOrDefault();
            calculateAssistant[1] = ApprovedAmount.GetValueOrDefault();
            calculateAssistant[2] = ApprovingAmount.GetValueOrDefault();
            calculateAssistant[3] = ReimbursedAmount.GetValueOrDefault();
            calculateAssistant[4] = NonReimbursedAmount.GetValueOrDefault();
            calculateAssistant[5] = RemainBudget.GetValueOrDefault();
            return calculateAssistant;
        }

        public decimal[] GetManagingBudgetByParameter(int PositionID, DateTime Period, int PurchaseBudgetTypeID) {
            decimal? TotalBudget = 0;
            decimal? ApprovedAmount = 0;
            decimal? ApprovingAmount = 0;
            decimal? ReimbursedAmount = 0;
            decimal? NonReimbursedAmount = 0;
            decimal? RemainBudget = 0;
            decimal[] calculateAssistant = new decimal[6];
            this.TABudgetPurchase.GetManagingBudgetByParameter(PositionID, Period, PurchaseBudgetTypeID, ref TotalBudget, ref ApprovedAmount,
                ref ApprovingAmount, ref ReimbursedAmount, ref NonReimbursedAmount, ref RemainBudget);
            calculateAssistant[0] = TotalBudget.GetValueOrDefault();
            calculateAssistant[1] = ApprovedAmount.GetValueOrDefault();
            calculateAssistant[2] = ApprovingAmount.GetValueOrDefault();
            calculateAssistant[3] = ReimbursedAmount.GetValueOrDefault();
            calculateAssistant[4] = NonReimbursedAmount.GetValueOrDefault();
            calculateAssistant[5] = RemainBudget.GetValueOrDefault();
            return calculateAssistant;
        }

        public decimal[] GetManagingBudgetByOUID(int OUID, int Year, int PurchaseBudgetTypeID) {
            decimal? TotalBudget = 0;
            decimal? ApprovedAmount = 0;
            decimal? ApprovingAmount = 0;
            decimal? ReimbursedAmount = 0;
            decimal? NonReimbursedAmount = 0;
            decimal? RemainBudget = 0;
            decimal[] calculateAssistant = new decimal[6];
            this.TABudgetPurchase.GetManagingBudgetByOUID(OUID, DateTime.Parse(Year + "-01-01"), PurchaseBudgetTypeID, ref TotalBudget, ref ApprovedAmount,
                ref ApprovingAmount, ref ReimbursedAmount, ref NonReimbursedAmount, ref RemainBudget);
            calculateAssistant[0] = TotalBudget.GetValueOrDefault();
            calculateAssistant[1] = ApprovedAmount.GetValueOrDefault();
            calculateAssistant[2] = ApprovingAmount.GetValueOrDefault();
            calculateAssistant[3] = ReimbursedAmount.GetValueOrDefault();
            calculateAssistant[4] = NonReimbursedAmount.GetValueOrDefault();
            calculateAssistant[5] = RemainBudget.GetValueOrDefault();
            return calculateAssistant;
        }


        #endregion

        #region BudgetRD

        public Budget.BudgetRDRow GetBudgetRDByID(int BudgetRDID) {
            return this.TABudgetRD.GetDataByID(BudgetRDID)[0];
        }

        public void InsertBudgetRD(int UserID, int PositionID, int OrganizationUnitID, int CustomerChannelID, int BrandID, int ExpenseSubCategoryID,
            string FPeriod, decimal? AOPBudget, decimal? AOPRBudget, decimal? ProjectionBudget, object AdjustBudgetStr, string ModifyReason) {

            SqlTransaction transaction = null;
            try {
                ////事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TABudgetRD);
                TableAdapterHelper.SetTransaction(this.TABudgetRDHistory, transaction);

                //增加重复验证
                if ((int)this.TABudgetRD.SearchBudgetRDByIns(OrganizationUnitID, CustomerChannelID, BrandID, DateTime.Parse(FPeriod.Substring(0, 4) + "-" + FPeriod.Substring(4, 2) + "-01"), ExpenseSubCategoryID) > 0) {
                    throw new MyException("预算重复设置", "budget exists ");
                }

                //增加重复验证
                if ((int)this.TABudgetRD.SearchBudgetRDByTreePath(GetTreePathByOUID(OrganizationUnitID), DateTime.Parse(FPeriod.Substring(0, 4) + "-" + FPeriod.Substring(4, 2) + "-01"), OrganizationUnitID) > 0) {
                    throw new MyException("预算重复设置或者上级或下级部门已经设置预算", "budget exists");
                }

                // 父表
                Budget.BudgetRDDataTable table = new Budget.BudgetRDDataTable();
                Budget.BudgetRDRow row = table.NewBudgetRDRow();

                decimal? AdjustBudget = null;
                if (AdjustBudgetStr != null && !string.IsNullOrEmpty(AdjustBudgetStr.ToString())) {
                    AdjustBudget = decimal.Parse(AdjustBudgetStr.ToString());
                }

                row.OrganizationUnitID = OrganizationUnitID;
                row.CustomerChannelID = CustomerChannelID;
                row.BrandID = BrandID;
                row.ExpenseSubCategoryID = ExpenseSubCategoryID;
                row.FPeriod = DateTime.Parse(FPeriod.Substring(0, 4) + "-" + FPeriod.Substring(4, 2) + "-01");
                row.AOPBudget = AOPBudget.GetValueOrDefault();
                row.AOPRBudget = AOPRBudget.GetValueOrDefault();
                row.ProjectionBudget = ProjectionBudget.GetValueOrDefault();
                row.AdjustBudget = AdjustBudget.GetValueOrDefault();
                if (ModifyReason != null) {
                    row.ModifyReason = ModifyReason;
                }
                table.AddBudgetRDRow(row);
                this.TABudgetRD.Update(table);

                // 子表
                Budget.BudgetRDHistoryDataTable tableDetail = new Budget.BudgetRDHistoryDataTable();
                Budget.BudgetRDHistoryRow rowDetail = tableDetail.NewBudgetRDHistoryRow();

                rowDetail.OrganizationUnitID = row.OrganizationUnitID;
                rowDetail.CustomerChannelID = row.CustomerChannelID;
                rowDetail.BrandID = row.BrandID;
                rowDetail.ExpenseSubCategoryID = row.ExpenseSubCategoryID;
                rowDetail.FPeriod = row.FPeriod;
                rowDetail.AOPBudget = row.AOPBudget;
                rowDetail.AOPRBudget = row.AOPRBudget;
                rowDetail.ProjectionBudget = row.ProjectionBudget;
                rowDetail.AdjustBudget = row.AdjustBudget;
                rowDetail.Action = "Create";
                rowDetail.ModifyDate = DateTime.Now;
                rowDetail.PositionID = PositionID;
                rowDetail.UserID = UserID;
                if (ModifyReason != null) {
                    rowDetail.ModifyReason = ModifyReason;
                }
                rowDetail.BudgetRDID = row.BudgetRDID;
                tableDetail.AddBudgetRDHistoryRow(rowDetail);
                this.TABudgetRDHistory.Update(tableDetail);

                transaction.Commit();
            } catch (Exception ex) {
                transaction.Rollback();
                throw ex;
            } finally {
                transaction.Dispose();
            }
        }

        public void UpdateBudgetRD(int BudgetRDID, decimal? ProjectionBudget, decimal? AdjustBudget, string ModifyReason,
            int UserID, int PositionID) {

            SqlTransaction transaction = null;
            try {
                //事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TABudgetRD);
                TableAdapterHelper.SetTransaction(this.TABudgetRDHistory, transaction);

                // 父表
                Budget.BudgetRDRow row = this.TABudgetRD.GetDataByID(BudgetRDID)[0];

                if (row.ProjectionBudget > ProjectionBudget) {
                    decimal[] calculateAssistant = this.GetRDBudgetByOUID(row.OrganizationUnitID, row.FPeriod, row.CustomerChannelID, row.BrandID, row.ExpenseSubCategoryID);
                    if (row.ProjectionBudget - ProjectionBudget > calculateAssistant[4]) {
                        throw new MyException("本次修改导致原有记录超预算，不能修改，目前部门剩余预算为：" + calculateAssistant[4], "Over budget,modify failed,current remain budget is :" + calculateAssistant[4]);
                    }
                }

                row.ProjectionBudget = ProjectionBudget.GetValueOrDefault();
                row.AdjustBudget = AdjustBudget.GetValueOrDefault();
                if (ModifyReason != null) {
                    row.ModifyReason = ModifyReason;
                }
                this.TABudgetRD.Update(row);

                // 子表
                Budget.BudgetRDHistoryDataTable tableDetail = new Budget.BudgetRDHistoryDataTable();
                Budget.BudgetRDHistoryRow rowDetail = tableDetail.NewBudgetRDHistoryRow();

                rowDetail.OrganizationUnitID = row.OrganizationUnitID;
                rowDetail.CustomerChannelID = row.CustomerChannelID;
                rowDetail.BrandID = row.BrandID;
                rowDetail.ExpenseSubCategoryID = row.ExpenseSubCategoryID;
                rowDetail.FPeriod = row.FPeriod;
                rowDetail.AOPBudget = row.AOPBudget;
                rowDetail.AOPRBudget = row.AOPRBudget;
                rowDetail.ProjectionBudget = row.ProjectionBudget;
                rowDetail.AdjustBudget = row.AdjustBudget;
                rowDetail.Action = "Modify";
                rowDetail.ModifyDate = DateTime.Now;
                rowDetail.PositionID = PositionID;
                rowDetail.UserID = UserID;
                if (ModifyReason != null) {
                    rowDetail.ModifyReason = ModifyReason;
                }
                rowDetail.BudgetRDID = row.BudgetRDID;

                tableDetail.AddBudgetRDHistoryRow(rowDetail);
                this.TABudgetRDHistory.Update(tableDetail);

                transaction.Commit();
            } catch (Exception ex) {
                transaction.Rollback();
                throw ex;
            } finally {
                transaction.Dispose();
            }
        }

        public Budget.BudgetRDDataTable GetPagedBudgetRD(string queryExpression, int startRowIndex, int maximumRows, string sortExpression) {
            return this.TABudgetRD.GetDataPaged("BudgetRD", sortExpression, startRowIndex, maximumRows, queryExpression);
        }

        public int QueryBudgetRDTotalCount(string queryExpression) {
            return (int)this.TABudgetRD.QueryDataCount("BudgetRD", queryExpression);
        }

        public void DeleteBudgetRDByID(int BudgetRDID) {
            SqlTransaction transaction = null;
            try {
                transaction = TableAdapterHelper.BeginTransaction(TABudgetRD);
                TableAdapterHelper.SetTransaction(TABudgetRDHistory, transaction);

                Budget.BudgetRDRow row = this.TABudgetRD.GetDataByID(BudgetRDID)[0];

                decimal[] temp = this.GetRDBudgetByOUID(row.OrganizationUnitID, row.FPeriod, row.CustomerChannelID, row.BrandID, row.ExpenseSubCategoryID);
                if (row.ProjectionBudget > temp[4]) {
                    throw new MyException("删除将导致已有单据超预算，不能删除，目前部门剩余预算为：" + temp[4], "Over budget,delete failed,current remain budget is :" + temp[4]);
                }
                TABudgetRDHistory.DeleteByRDID(BudgetRDID);
                TABudgetRD.DeleteByID(BudgetRDID);
                transaction.Commit();
            } catch (Exception) {
                transaction.Rollback();
                throw;
            } finally {
                transaction.Dispose();
            }
        }

        public Budget.BudgetRDHistoryDataTable GetBudgetRDHistory(int BudgetRDID) {
            return TABudgetRDHistory.GetDataByRDID(BudgetRDID);
        }

        public decimal[] GetRDBudgetByParameter(int PositionID, DateTime FPeriod, int CustomerChannelID, int BrandID, int ExpenseSubCategoryID) {
            decimal? TotalBudget = 0;
            decimal? ApprovedAmount = 0;
            decimal? ApprovingAmount = 0;
            decimal? ReimbursedAmount = 0;
            decimal? RemainBudget = 0;
            decimal[] calculateAssistant = new decimal[5];
            this.TABudgetRD.GetRDBudgetByParameter(PositionID, FPeriod, CustomerChannelID, BrandID, ExpenseSubCategoryID, ref TotalBudget, ref ApprovedAmount,
                ref ApprovingAmount, ref ReimbursedAmount, ref RemainBudget);
            calculateAssistant[0] = TotalBudget.GetValueOrDefault();
            calculateAssistant[1] = ApprovedAmount.GetValueOrDefault();
            calculateAssistant[2] = ApprovingAmount.GetValueOrDefault();
            calculateAssistant[3] = ReimbursedAmount.GetValueOrDefault();
            calculateAssistant[4] = RemainBudget.GetValueOrDefault();

            return calculateAssistant;
        }

        public decimal[] GetRDBudgetByOUID(int OUID, DateTime FPeriod, int CustomerChannelID, int BrandID, int ExpenseSubCategoryID) {
            decimal? TotalBudget = 0;
            decimal? ApprovedAmount = 0;
            decimal? ApprovingAmount = 0;
            decimal? ReimbursedAmount = 0;
            decimal? RemainBudget = 0;
            decimal[] calculateAssistant = new decimal[5];
            this.TABudgetRD.GetRDBudgetByOUID(OUID, FPeriod, CustomerChannelID, BrandID, ExpenseSubCategoryID, ref TotalBudget, ref ApprovedAmount,
                ref ApprovingAmount, ref ReimbursedAmount, ref RemainBudget);
            calculateAssistant[0] = TotalBudget.GetValueOrDefault();
            calculateAssistant[1] = ApprovedAmount.GetValueOrDefault();
            calculateAssistant[2] = ApprovingAmount.GetValueOrDefault();
            calculateAssistant[3] = ReimbursedAmount.GetValueOrDefault();
            calculateAssistant[4] = RemainBudget.GetValueOrDefault();

            return calculateAssistant;
        }

        #endregion

        public string GetTreePathByOUID(int OUID) {
            String TreePath = string.Empty;
            AuthorizationDS.OrganizationUnitRow OU = new OUTreeBLL().GetOrganizationUnitById(OUID);
            if (!OU.IsOrganizationTreePathNull()) {
                TreePath = OU.OrganizationTreePath + "P" + OUID + "P ";
            } else {
                TreePath = "P" + OUID + "P ";
            }
            return TreePath;
        }
    }
}
