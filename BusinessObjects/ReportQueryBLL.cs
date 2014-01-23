using System;
using System.Collections.Generic;
using BusinessObjects.ReportDSTableAdapters;
using System.Linq;
using System.Text;

namespace BusinessObjects {
    public class ReportQueryBLL {
        /**
         * 销售核销明细
         **/
        private GetReportSalesReimburseDetailTableAdapter m_ReportSalesReimburseDetailViewTA;
        public GetReportSalesReimburseDetailTableAdapter TAReportSalesReimburseDetailView {
            get {
                if (m_ReportSalesReimburseDetailViewTA == null) {
                    m_ReportSalesReimburseDetailViewTA = new GetReportSalesReimburseDetailTableAdapter();
                }
                return m_ReportSalesReimburseDetailViewTA;
            }
        }

        public ReportDS.GetReportSalesReimburseDetailDataTable GetPagedSaleReimburseDetailViewByRight(string Period) {
            return this.TAReportSalesReimburseDetailView.GetData(Period);
        }

        public int QuerySaleReimburseDetailViewCountByRight(string Period) {
            if (Period == null || Period.Length == 0) {
                return 0;
            }
            return (int)this.TAReportSalesReimburseDetailView.GetData(Period).Count;
        }

        /**
        * PV明细导出
        **/
        private GetPagedFormPVViewByRightExportTableAdapter m_PagedFormPVViewByRightExportViewTA;
        public GetPagedFormPVViewByRightExportTableAdapter TAPagedFormPVViewByRightExportView {
            get {
                if (m_PagedFormPVViewByRightExportViewTA == null) {
                    m_PagedFormPVViewByRightExportViewTA = new GetPagedFormPVViewByRightExportTableAdapter();
                }
                return m_PagedFormPVViewByRightExportViewTA;
            }
        }

        public ReportDS.GetPagedFormPVViewByRightExportDataTable GetPagedFormPVViewByRightExportViewByRight(string queryExpression, int StuffUserID, int PositionID) {
            return this.TAPagedFormPVViewByRightExportView.GetData(queryExpression,StuffUserID,PositionID);
        }

        public int QueryPagedFormPVViewByRightExportViewCountByRight(string queryExpression, int StuffUserID, int PositionID) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return 0;
            }
            return (int)this.TAPagedFormPVViewByRightExportView.GetData( queryExpression, StuffUserID, PositionID).Count;
        }
        /**
        * PR明细导出
        **/
        private GetPagedFormPRViewByRightExportTableAdapter m_PagedFormPRViewByRightExportViewTA;
        public GetPagedFormPRViewByRightExportTableAdapter TAPagedFormPRViewByRightExportView {
            get {
                if (m_PagedFormPRViewByRightExportViewTA == null) {
                    m_PagedFormPRViewByRightExportViewTA = new GetPagedFormPRViewByRightExportTableAdapter();
                }
                return m_PagedFormPRViewByRightExportViewTA;
            }
        }

        public int QueryPagedFormPRViewByRightExportViewCountByRight(string queryExpression, int StuffUserID, int PositionID) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return 0;
            }
            return (int)this.TAPagedFormPRViewByRightExportView.GetData(queryExpression, StuffUserID, PositionID).Count;
        }

        /**
        * PO明细导出
        **/
        private GetPagedFormPOViewByRightExportTableAdapter m_PagedFormPOViewByRightExportViewTA;
        public GetPagedFormPOViewByRightExportTableAdapter TAPagedFormPOViewByRightExportView {
            get {
                if (m_PagedFormPOViewByRightExportViewTA == null) {
                    m_PagedFormPOViewByRightExportViewTA = new GetPagedFormPOViewByRightExportTableAdapter();
                }
                return m_PagedFormPOViewByRightExportViewTA;
            }
        }

        public int QueryPagedFormPOViewByRightExportViewCountByRight(string queryExpression, int StuffUserID, int PositionID) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return 0;
            }
            return (int)this.TAPagedFormPOViewByRightExportView.GetData(queryExpression, StuffUserID, PositionID).Count;
        }

        /**
        * RD申请单明细导出
        **/
        private GetPagedFormRDApplyViewByRightExportTableAdapter m_PagedFormRDApplyViewByRightExportViewTA;
        public GetPagedFormRDApplyViewByRightExportTableAdapter TAPagedFormRDApplyViewByRightExportView {
            get {
                if (m_PagedFormRDApplyViewByRightExportViewTA == null) {
                    m_PagedFormRDApplyViewByRightExportViewTA = new GetPagedFormRDApplyViewByRightExportTableAdapter();
                }
                return m_PagedFormRDApplyViewByRightExportViewTA;
            }
        }

        public int QueryPagedFormRDApplyViewByRightExportViewCountByRight(string queryExpression, int StuffUserID, int PositionID) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return 0;
            }
            return (int)this.TAPagedFormRDApplyViewByRightExportView.GetData(queryExpression, StuffUserID, PositionID).Count;
        }

        /**
        * RD报销单明细导出
        **/
        private GetPagedFormRDPaymentViewByRightExportTableAdapter m_PagedFormRDPaymentViewByRightExportViewTA;
        public GetPagedFormRDPaymentViewByRightExportTableAdapter TAPagedFormRDPaymentViewByRightExportView {
            get {
                if (m_PagedFormRDPaymentViewByRightExportViewTA == null) {
                    m_PagedFormRDPaymentViewByRightExportViewTA = new GetPagedFormRDPaymentViewByRightExportTableAdapter();
                }
                return m_PagedFormRDPaymentViewByRightExportViewTA;
            }
        }

        public int QueryPagedFormRDPaymentViewByRightExportViewCountByRight(string queryExpression, int StuffUserID, int PositionID) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return 0;
            }
            return (int)this.TAPagedFormRDPaymentViewByRightExportView.GetData(queryExpression, StuffUserID, PositionID).Count;
        }

        /**
        *个人费用报销明细导出
        **/
        private GetPagedFormPersonalReimburseViewByRightExportTableAdapter m_PagedFormPersonalReimburseViewByRightExportViewTA;
        public GetPagedFormPersonalReimburseViewByRightExportTableAdapter TAPagedFormPersonalReimburseViewByRightExportView {
            get {
                if (m_PagedFormPersonalReimburseViewByRightExportViewTA == null) {
                    m_PagedFormPersonalReimburseViewByRightExportViewTA = new GetPagedFormPersonalReimburseViewByRightExportTableAdapter();
                }
                return m_PagedFormPersonalReimburseViewByRightExportViewTA;
            }
        }

        public int QueryPagedFormPersonalReimburseViewByRightExportViewCountByRight(string queryExpression, int StuffUserID, int PositionID) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return 0;
            }
            return (int)this.TAPagedFormPersonalReimburseViewByRightExportView.GetData(queryExpression, StuffUserID, PositionID).Count;
        }

        /**
        *销售部方案申请单明细导出
        **/
        private GetPagedFormSaleApplyViewByRightExportTableAdapter m_PagedFormSaleApplyViewByRightExportViewTA;
        public GetPagedFormSaleApplyViewByRightExportTableAdapter TAPagedFormSaleApplyViewByRightExportView {
            get {
                if (m_PagedFormSaleApplyViewByRightExportViewTA == null) {
                    m_PagedFormSaleApplyViewByRightExportViewTA = new GetPagedFormSaleApplyViewByRightExportTableAdapter();
                }
                return m_PagedFormSaleApplyViewByRightExportViewTA;
            }
        }

        public int QueryPagedFormSaleApplyViewByRightExportViewCountByRight(string queryExpression, int StuffUserID, int PositionID) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return 0;
            }
            return (int)this.TAPagedFormSaleApplyViewByRightExportView.GetData(queryExpression, StuffUserID, PositionID).Count;
        }

        /**
        *销售部方案结案单明细导出
        **/
        private GetPagedFormSaleSettlementViewByRightExportTableAdapter m_PagedFormSaleSettlementViewByRightExportViewTA;
        public GetPagedFormSaleSettlementViewByRightExportTableAdapter TAPagedFormSaleSettlementViewByRightExportView {
            get {
                if (m_PagedFormSaleSettlementViewByRightExportViewTA == null) {
                    m_PagedFormSaleSettlementViewByRightExportViewTA = new GetPagedFormSaleSettlementViewByRightExportTableAdapter();
                }
                return m_PagedFormSaleSettlementViewByRightExportViewTA;
            }
        }

        public int QueryPagedFormSaleSettlementViewByRightExportViewCountByRight(string queryExpression, int StuffUserID, int PositionID) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return 0;
            }
            return (int)this.TAPagedFormSaleSettlementViewByRightExportView.GetData(queryExpression, StuffUserID, PositionID).Count;
        }
        /**
        *销售部方案报销单明细导出
        **/
        private GetPagedFormSalePaymentViewByRightExportTableAdapter m_PagedFormSalePaymentViewByRightExportViewTA;
        public GetPagedFormSalePaymentViewByRightExportTableAdapter TAPagedFormSalePaymentViewByRightExportView {
            get {
                if (m_PagedFormSalePaymentViewByRightExportViewTA == null) {
                    m_PagedFormSalePaymentViewByRightExportViewTA = new GetPagedFormSalePaymentViewByRightExportTableAdapter();
                }
                return m_PagedFormSalePaymentViewByRightExportViewTA;
            }
        }

        public int QueryPagedFormSalePaymentViewByRightExportViewCountByRight(string queryExpression, int StuffUserID, int PositionID) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return 0;
            }
            return (int)this.TAPagedFormSalePaymentViewByRightExportView.GetData(queryExpression, StuffUserID, PositionID).Count;
        }
        /**
        *市场部方案申请单明细导出
        **/
        private GetPagedFormMarketingApplyViewByRightExportTableAdapter m_PagedFormMarketingApplyViewByRightExportViewTA;
        public GetPagedFormMarketingApplyViewByRightExportTableAdapter TAPagedFormMarketingApplyViewByRightExportView {
            get {
                if (m_PagedFormMarketingApplyViewByRightExportViewTA == null) {
                    m_PagedFormMarketingApplyViewByRightExportViewTA = new GetPagedFormMarketingApplyViewByRightExportTableAdapter();
                }
                return m_PagedFormMarketingApplyViewByRightExportViewTA;
            }
        }

        public int QueryPagedFormMarketingApplyViewByRightExportViewCountByRight(string queryExpression, int StuffUserID, int PositionID) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return 0;
            }
            return (int)this.TAPagedFormMarketingApplyViewByRightExportView.GetData(queryExpression, StuffUserID, PositionID).Count;
        }
        /**
        *市场部方案报销单明细导出
        **/
        private GetPagedFormMarketingPaymentViewByRightExportTableAdapter m_PagedFormMarketingPaymentViewByRightExportViewTA;
        public GetPagedFormMarketingPaymentViewByRightExportTableAdapter TAPagedFormMarketingPaymentViewByRightExportView {
            get {
                if (m_PagedFormMarketingPaymentViewByRightExportViewTA == null) {
                    m_PagedFormMarketingPaymentViewByRightExportViewTA = new GetPagedFormMarketingPaymentViewByRightExportTableAdapter();
                }
                return m_PagedFormMarketingPaymentViewByRightExportViewTA;
            }
        }

        public int QueryPagedFormMarketingPaymentViewByRightExportViewCountByRight(string queryExpression, int StuffUserID, int PositionID) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return 0;
            }
            return (int)this.TAPagedFormMarketingPaymentViewByRightExportView.GetData(queryExpression, StuffUserID, PositionID).Count;
        }
    }
}
