using System;
using System.Collections.Generic;
using System.Text;
using BusinessObjects.QueryDSTableAdapters;
using System.Data;
using System.Data.SqlClient;

namespace BusinessObjects {
    public class FormQueryBLL {

        #region 属性

        private FormViewTableAdapter m_FormViewTA;
        public FormViewTableAdapter TAFormView {
            get {
                if (m_FormViewTA == null) {
                    m_FormViewTA = new FormViewTableAdapter();
                }
                return m_FormViewTA;
            }
        }

        private FormSaleApplyViewTableAdapter m_FormSaleApplyViewTA;
        public FormSaleApplyViewTableAdapter TAFormSaleApplyView {
            get {
                if (m_FormSaleApplyViewTA == null) {
                    m_FormSaleApplyViewTA = new FormSaleApplyViewTableAdapter();
                }
                return m_FormSaleApplyViewTA;
            }
        }

        private FormSaleSettlementViewTableAdapter m_FormSaleSettlementViewTA;
        public FormSaleSettlementViewTableAdapter TAFormSaleSettlementView {
            get {
                if (m_FormSaleSettlementViewTA == null) {
                    m_FormSaleSettlementViewTA = new FormSaleSettlementViewTableAdapter();
                }
                return m_FormSaleSettlementViewTA;
            }
        }

        private FormSalePaymentViewTableAdapter m_FormSalePaymentViewTA;
        public FormSalePaymentViewTableAdapter TAFormSalePaymentView {
            get {
                if (m_FormSalePaymentViewTA == null) {
                    m_FormSalePaymentViewTA = new FormSalePaymentViewTableAdapter();
                }
                return m_FormSalePaymentViewTA;
            }
        }

        private FormPRViewTableAdapter m_FormPRViewTA;
        public FormPRViewTableAdapter TAFormPRView {
            get {
                if (m_FormPRViewTA == null) {
                    m_FormPRViewTA = new FormPRViewTableAdapter();
                }
                return m_FormPRViewTA;
            }
        }

        private FormPOViewTableAdapter m_FormPOViewTA;
        public FormPOViewTableAdapter TAFormPOView {
            get {
                if (m_FormPOViewTA == null) {
                    m_FormPOViewTA = new FormPOViewTableAdapter();
                }
                return m_FormPOViewTA;
            }
        }

        private FormPRPOViewTableAdapter m_FormPRPOViewTA;
        public FormPRPOViewTableAdapter TAFormPRPOView {
            get {
                if (m_FormPRPOViewTA == null) {
                    m_FormPRPOViewTA = new FormPRPOViewTableAdapter();
                }
                return m_FormPRPOViewTA;
            }
        }

        private FormPVViewTableAdapter m_FormPVViewTA;
        public FormPVViewTableAdapter TAFormPVView {
            get {
                if (m_FormPVViewTA == null) {
                    m_FormPVViewTA = new FormPVViewTableAdapter();
                }
                return m_FormPVViewTA;
            }
        }

        private FormPersonalPaymentViewTableAdapter _TAFormPersonalPaymentView;
        public FormPersonalPaymentViewTableAdapter TAFormPersonalPaymentView {
            get {
                if (_TAFormPersonalPaymentView == null) {
                    _TAFormPersonalPaymentView = new FormPersonalPaymentViewTableAdapter();
                }
                return _TAFormPersonalPaymentView;
            }
        }

        private FormMarketingApplyViewTableAdapter _TAFormMarketingApplyView;
        public FormMarketingApplyViewTableAdapter TAFormMarketingApplyView {
            get {
                if (_TAFormMarketingApplyView == null) {
                    _TAFormMarketingApplyView = new FormMarketingApplyViewTableAdapter();
                }
                return _TAFormMarketingApplyView;
            }
        }

        private FormMarketingPaymentViewTableAdapter _TAFormMarketingPaymentView;
        public FormMarketingPaymentViewTableAdapter TAFormMarketingPaymentView {
            get {
                if (_TAFormMarketingPaymentView == null) {
                    _TAFormMarketingPaymentView = new FormMarketingPaymentViewTableAdapter();
                }
                return _TAFormMarketingPaymentView;
            }
        }

        private FormRDApplyViewTableAdapter _TAFormRDApplyView;
        public FormRDApplyViewTableAdapter TAFormRDApplyView {
            get {
                if (_TAFormRDApplyView == null) {
                    _TAFormRDApplyView = new FormRDApplyViewTableAdapter();
                }
                return _TAFormRDApplyView;
            }
        }

        private FormRDPaymentViewTableAdapter _TAFormRDPaymentView;
        public FormRDPaymentViewTableAdapter TAFormRDPaymentView {
            get {
                if (_TAFormRDPaymentView == null) {
                    _TAFormRDPaymentView = new FormRDPaymentViewTableAdapter();
                }
                return _TAFormRDPaymentView;
            }
        }

        #endregion

        #region Form

        public QueryDS.FormViewDataTable GetPagedFormView(string queryExpression, int startRowIndex, int maximumRows, string sortExpression) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return null;
            }
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "SubmitDate DESC";
            }
            return this.TAFormView.GetPagedFormView(sortExpression, startRowIndex, maximumRows, queryExpression);
        }

        public int QueryFormViewCount(string queryExpression) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return 0;
            }
            return (int)this.TAFormView.QueryFormViewCount(queryExpression);
        }

        public QueryDS.FormViewRow GetFormViewByID(int FormID) {
            return this.TAFormView.GetDataByID(FormID)[0];
        }
        public void UpdateFormbyID(int FormID, Boolean IsExportLock, string FormNo, DateTime? CreateVoucherDate, DateTime? SubmitDate, Boolean? IsCreateVoucher) {
            FormMarketingBLL formmarkbll = new FormMarketingBLL();
            FormDS.FormDataTable l_dtform = formmarkbll.GetFormByID(FormID);
            if (l_dtform.Rows.Count > 0) {
                FormDS.FormRow l_drform = l_dtform[0];
                l_drform.IsExportLock = IsExportLock;
                formmarkbll.TAForm.Update(l_drform);
            }
        }
        #endregion

        #region FormSaleApplyView

        public QueryDS.FormSaleApplyViewDataTable GetPagedFormSaleApplyView(string queryExpression, int startRowIndex, int maximumRows, string sortExpression) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return null;
            }
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "SubmitDate DESC";
            }
            return this.TAFormSaleApplyView.GetPagedData("FormSaleApplyView", sortExpression, startRowIndex, maximumRows, queryExpression);
        }

        public int QueryFormSaleApplyViewCount(string queryExpression) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return 0;
            }
            return (int)this.TAFormSaleApplyView.QueryDataCount("FormSaleApplyView", queryExpression);
        }

        public QueryDS.FormSaleApplyViewRow GetFormSaleApplyViewByID(int FormID) {
            return this.TAFormSaleApplyView.GetDataByID(FormID)[0];
        }

        public QueryDS.FormSaleApplyViewDataTable GetPagedFormSaleApplyViewByRight(string queryExpression, int startRowIndex, int maximumRows, string sortExpression, int UserID, int PositionID) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return null;
            }
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "SubmitDate DESC";
            }
            return this.TAFormSaleApplyView.GetPagedFormSaleApplyViewByRight(sortExpression, startRowIndex, maximumRows, queryExpression, UserID, PositionID);
        }

        public int QueryFormSaleApplyViewCountByRight(string queryExpression, int UserID, int PositionID) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return 0;
            }
            return (int)this.TAFormSaleApplyView.QueryFormSaleApplyViewCountByRight(queryExpression, UserID, PositionID);
        }


        #endregion

        #region FormRDApplyView

        public QueryDS.FormRDApplyViewRow GetFormRDApplyViewByID(int FormID) {
            return this.TAFormRDApplyView.GetDataByID(FormID)[0];
        }

        public QueryDS.FormRDApplyViewDataTable GetPagedFormRDApplyView(string queryExpression, int startRowIndex, int maximumRows, string sortExpression) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return null;
            }
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "SubmitDate DESC";
            }
            return this.TAFormRDApplyView.GetDataPagedRDApplyView("FormRDApplyView", sortExpression, startRowIndex, maximumRows, queryExpression);
        }

        public int QueryFormRDApplyViewCount(string queryExpression) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return 0;
            }
            return (int)this.TAFormRDApplyView.QueryDataCount("FormRDApplyView", queryExpression);
        }

        public QueryDS.FormMarketingApplyViewRow GetFormMarketingApplyViewByID(int FormID) {
            return this.TAFormMarketingApplyView.GetDataByID(FormID)[0];
        }

        public QueryDS.FormRDApplyViewDataTable GetPagedFormRDApplyViewByRight(string queryExpression, int startRowIndex, int maximumRows, string sortExpression, int UserID, int PositionID) {
            if (queryExpression == null || queryExpression.Length == 0) {
                queryExpression = "FormID in not null";
            }
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "SubmitDate DESC";
            }
            return this.TAFormRDApplyView.GetDataPagedRDApplyViewByRight(sortExpression, startRowIndex, maximumRows, queryExpression, UserID, PositionID);
        }

        public int QueryFormRDApplyViewCountByRight(string queryExpression, int UserID, int PositionID) {
            if (queryExpression == null || queryExpression.Length == 0) {
                queryExpression = "FormID is not null";
            }
            return (int)this.TAFormRDApplyView.QueryFormRDApplyViewCountByRight(queryExpression, UserID, PositionID);
        }

        #endregion

        //#region FormRDPaymentView

        //public QueryDS.FormRDPaymentViewRow GetFormRDPaymentViewByID(int FormID) {
        //    return this.TAFormRDPaymentView.GetDataByID(FormID)[0];
        //}

        //public QueryDS.FormRDPaymentViewDataTable GetPagedFormRDPaymentViewByRight(string queryExpression, int startRowIndex, int maximumRows, string sortExpression, int UserID, int PositionID) {
        //    if (queryExpression == null || queryExpression.Length == 0) {
        //        return null;
        //    }
        //    if (sortExpression == null || sortExpression.Length == 0) {
        //        sortExpression = "Form.SubmitDate DESC";
        //    }
        //    return this.TAFormRDPaymentView.GetPagedFormRDPaymentViewByRight(sortExpression, startRowIndex, maximumRows, queryExpression, UserID, PositionID);
        //}

        //public int QueryFormRDPaymentViewCountByRight(string queryExpression, int UserID, int PositionID) {
        //    if (queryExpression == null || queryExpression.Length == 0) {
        //        return 0;
        //    }
        //    return (int)this.TAFormRDPaymentView.QueryFormRDPaymentViewCountByRight(queryExpression, UserID, PositionID);
        //}

        //#endregion

        #region FormPRView

        public QueryDS.FormPRViewDataTable GetPagedFormPRView(string queryExpression, int startRowIndex, int maximumRows, string sortExpression) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return null;
            }
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "SubmitDate DESC";
            }
            return this.TAFormPRView.GetPagedData("FormPRView", sortExpression, startRowIndex, maximumRows, queryExpression);
        }

        public int QueryFormPRViewCount(string queryExpression) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return 0;
            }
            return (int)this.TAFormPRView.QueryDataCount("FormPRView", queryExpression);
        }

        public QueryDS.FormPRViewRow GetFormPRViewByID(int FormID) {
            return this.TAFormPRView.GetDataByID(FormID)[0];
        }

        public QueryDS.FormPRViewDataTable GetPagedFormPRViewByRight(string queryExpression, int startRowIndex, int maximumRows, string sortExpression, int UserID, int PositionID) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return null;
            }
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "Form.SubmitDate DESC";
            }
            return this.TAFormPRView.GetPagedFormPRViewByRight(sortExpression, startRowIndex, maximumRows, queryExpression, UserID, PositionID);
        }

        public int QueryFormPRViewCountByRight(string queryExpression, int UserID, int PositionID) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return 0;
            }
            return (int)this.TAFormPRView.QueryFormPRViewCountByRight(queryExpression, UserID, PositionID);
        }

        #endregion

        #region FormPOView

        public QueryDS.FormPOViewRow GetFormPOViewByID(int FormID) {
            return this.TAFormPOView.GetDataByID(FormID)[0];
        }

        public QueryDS.FormPOViewDataTable GetPagedFormPOViewByRight(string queryExpression, int startRowIndex, int maximumRows, string sortExpression, int UserID, int PositionID) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return null;
            }
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "Form.SubmitDate DESC";
            }
            return this.TAFormPOView.GetPagedFormPOViewByRight(sortExpression, startRowIndex, maximumRows, queryExpression, UserID, PositionID);
        }

        public int QueryFormPOViewCountByRight(string queryExpression, int UserID, int PositionID) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return 0;
            }
            return (int)this.TAFormPOView.QueryFormPOViewCountByRight(queryExpression, UserID, PositionID);
        }

        public QueryDS.FormPOViewDataTable GetPagedFormPOView(string sortExpression, int startRowIndex, int maximumRows, string queryExpression) {
            return this.TAFormPOView.GetPagedPOView("FormPOView", sortExpression, startRowIndex, maximumRows, queryExpression);
        }

        public int QueryFormPOViewCount(string TableName, string QueryExpression) {
            return (int)this.TAFormPOView.QueryDataCount(TableName, QueryExpression);
        }

        public QueryDS.FormPOViewDataTable GetPagedPOViewByMAAID(int MAAID, string sortExpression, int maximumRows, int startRowIndex) {
            if (string.IsNullOrEmpty(sortExpression)) {
                sortExpression = "FormNo";
            }
            return this.GetPagedFormPOView(sortExpression, startRowIndex, maximumRows, string.Format("StatusID=2 and ParentFormID={0}", MAAID));
        }

        public int QueryPOViewCountByMAAID(int MAAID) {
            return this.QueryFormPOViewCount("FormPOView", string.Format("StatusID=2 and ParentFormID={0}", MAAID));
        }

        public QueryDS.FormPOViewDataTable GetPagedPOViewBySettlementID(int SettlementID, string sortExpression, int maximumRows, int startRowIndex) {
            string FormApplyIds = this.TAFormSaleSettlementView.GetDataByID(SettlementID)[0].FormApplyIds;
            if (!string.IsNullOrEmpty(FormApplyIds)) {
                FormApplyIds = FormApplyIds.Replace("PP", ",");
                FormApplyIds = FormApplyIds.Replace("P", "");
            }
            if (string.IsNullOrEmpty(sortExpression)) {
                sortExpression = "FormNo";
            }
            return this.GetPagedFormPOView(sortExpression, startRowIndex, maximumRows, string.Format("StatusID=2 and ParentFormID in ({0})", FormApplyIds));
        }

        public int QueryPOViewCountBySettlementID(int SettlementID) {
            string FormApplyIds = this.TAFormSaleSettlementView.GetDataByID(SettlementID)[0].FormApplyIds;
            if (!string.IsNullOrEmpty(FormApplyIds)) {
                FormApplyIds = FormApplyIds.Replace("PP", ",");
                FormApplyIds = FormApplyIds.Replace("P", "");
            }
            return this.QueryFormPOViewCount("FormPOView", string.Format("StatusID=2 and ParentFormID in ({0})", FormApplyIds));
        }
        

        #endregion

        #region FormPVView

        public QueryDS.FormPVViewRow GetFormPVViewByID(int FormID) {
            return this.TAFormPVView.GetDataByID(FormID)[0];
        }

        public QueryDS.FormPVViewDataTable GetPagedFormPVViewByRight(string queryExpression, int startRowIndex, int maximumRows, string sortExpression, int UserID, int PositionID) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return null;
            }
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "Form.SubmitDate DESC";
            }
            return this.TAFormPVView.GetPagedFormPVViewByRight(sortExpression, startRowIndex, maximumRows, queryExpression, UserID, PositionID);
        }

        public int QueryFormPVViewCountByRight(string queryExpression, int UserID, int PositionID) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return 0;
            }
            return (int)this.TAFormPVView.QueryFormPVViewCountByRight(queryExpression, UserID, PositionID);
        }

        #endregion

        //#region FormInvitationApplyView

        //public QueryDS.FormInvitationApplyViewDataTable GetPagedFormInvitationApplyView(string queryExpression, int startRowIndex, int maximumRows, string sortExpression) {
        //    if (queryExpression == null || queryExpression.Length == 0) {
        //        return null;
        //    }
        //    if (sortExpression == null || sortExpression.Length == 0) {
        //        sortExpression = "SubmitDate DESC";
        //    }
        //    return this.TAFormInvitationApplyView.GetPagedData("FormInvitationApplyView", sortExpression, startRowIndex, maximumRows, queryExpression);
        //}

        //public int QueryFormInvitationApplyViewCount(string queryExpression) {
        //    if (queryExpression == null || queryExpression.Length == 0) {
        //        return 0;
        //    }
        //    return (int)this.TAFormSaleApplyView.QueryDataCount("FormInvitationApplyView", queryExpression);
        //}

        //#endregion

        #region FormPRPOView

        public QueryDS.FormPRPOViewDataTable GetPagedFormPRPOView(string queryExpression, int startRowIndex, int maximumRows, string sortExpression) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return null;
            }
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "SubmitDate DESC";
            }
            return this.TAFormPRPOView.GetPagedData("FormPRPOView", sortExpression, startRowIndex, maximumRows, queryExpression);
        }

        public int QueryFormPRPOViewCount(string queryExpression) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return 0;
            }
            return (int)this.TAFormPRPOView.QueryDataCount("FormPRPOView", queryExpression);
        }

        public QueryDS.FormPRPOViewRow GetFormPRPOViewByID(int FormID) {
            return this.TAFormPRPOView.GetDataByID(FormID)[0];
        }

        #endregion

        #region FormPersonalReimburseView

        public QueryDS.FormPersonalPaymentViewDataTable GetPagedFormPersonalPaymentViewByRight(string queryExpression, int startRowIndex, int maximumRows, string sortExpression, int UserID, int PositionID) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return null;
            }
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "SubmitDate DESC";
            }
            queryExpression += " and Form.FormTypeID in (1,2,3,4) and StatusID<>0";
            return this.TAFormPersonalPaymentView.GetPagedFormPersonalReimburseByRight(sortExpression, startRowIndex, maximumRows, queryExpression, UserID, PositionID);
        }

        public int QueryFormPersonalPaymentViewCountByRight(string queryExpression, int UserID, int PositionID) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return 0;
            }
            queryExpression += " and Form.FormTypeID in (1,2,3,4) and StatusID<>0";
            return (int)this.TAFormPersonalPaymentView.QueryFormPersonalPaymentViewCountByRight(queryExpression, UserID, PositionID);
        }

        public QueryDS.FormPersonalPaymentViewRow GetFormPersonalPaymentViewByID(int FormID) {
            return this.TAFormPersonalPaymentView.GetDataByID(FormID)[0];
        }

        #endregion

        #region FormSaleSettlementView

        public QueryDS.FormSaleSettlementViewDataTable GetPagedFormSaleSettlementView(string queryExpression, int startRowIndex, int maximumRows, string sortExpression) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return null;
            }
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "SubmitDate DESC";
            }
            return this.TAFormSaleSettlementView.GetPagedData("FormSaleSettlementView", sortExpression, startRowIndex, maximumRows, queryExpression);
        }

        public int QueryFormSaleSettlementViewCount(string queryExpression) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return 0;
            }
            return (int)this.TAFormSaleSettlementView.QueryDataCount("FormSaleSettlementView", queryExpression);
        }

        public QueryDS.FormSaleSettlementViewRow GetFormSaleSettlementViewByID(int FormID) {
            return this.TAFormSaleSettlementView.GetDataByID(FormID)[0];
        }

        public QueryDS.FormSaleSettlementViewDataTable GetPagedFormSaleSettlementViewByRight(string queryExpression, int startRowIndex, int maximumRows, string sortExpression, int UserID, int PositionID) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return null;
            }
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "SubmitDate DESC";
            }
            return this.TAFormSaleSettlementView.GetPagedFormSaleSettlementViewByRight(sortExpression, startRowIndex, maximumRows, queryExpression, UserID, PositionID);
        }

        public int QueryFormSaleSettlementViewCountByRight(string queryExpression, int UserID, int PositionID) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return 0;
            }
            return (int)this.TAFormSaleSettlementView.QueryFormSaleSettlementViewCountByRight(queryExpression, UserID, PositionID);
        }

        #endregion

        #region FormSalePaymentView

        public QueryDS.FormSalePaymentViewRow GetFormSalePaymentViewByID(int FormID) {
            return this.TAFormSalePaymentView.GetDataByID(FormID)[0];
        }

        public QueryDS.FormSalePaymentViewDataTable GetPagedFormSalePaymentViewByRight(string queryExpression, int startRowIndex, int maximumRows, string sortExpression, int UserID, int PositionID) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return null;
            }
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "Form.SubmitDate DESC";
            }
            return this.TAFormSalePaymentView.GetPagedFormSalePaymentViewByRight(sortExpression, startRowIndex, maximumRows, queryExpression, UserID, PositionID);
        }

        public int QueryFormSalePaymentViewCountByRight(string queryExpression, int UserID, int PositionID) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return 0;
            }
            return (int)this.TAFormSalePaymentView.QueryFormSalePaymentViewCountByRight(queryExpression, UserID, PositionID);
        }

        #endregion

        #region FormMarketingApplyView

        public QueryDS.FormMarketingApplyViewDataTable GetPagedFormMarketingApplyView(string queryExpression, int startRowIndex, int maximumRows, string sortExpression) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return null;
            }
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "SubmitDate DESC";
            }
            return this.TAFormMarketingApplyView.GetDataPaged("FormMarketingApplyView", sortExpression, startRowIndex, maximumRows, queryExpression);
        }

        public int QueryFormMarketingApplyViewCount(string queryExpression) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return 0;
            }
            return (int)this.TAFormMarketingApplyView.QueryDataCount("FormMarketingApplyView", queryExpression);
        }

        public QueryDS.FormMarketingApplyViewDataTable GetPagedFormMarketingApplyViewByRight(string queryExpression, int startRowIndex, int maximumRows, string sortExpression, int UserID, int PositionID) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return null;
            }
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "Form.SubmitDate DESC";
            }
            return this.TAFormMarketingApplyView.GetPagedFormMaketingApplyByRight(sortExpression, startRowIndex, maximumRows, queryExpression, UserID, PositionID);
        }

        public int QueryFormMarketingApplyViewCountByRight(string queryExpression, int UserID, int PositionID) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return 0;
            }
            return (int)this.TAFormMarketingApplyView.QueryFormMarketingApplyViewCountByRight(queryExpression, UserID, PositionID);
        }

        #endregion

        #region FormMarketingPaymentView

        public QueryDS.FormMarketingPaymentViewRow GetFormMarketingPaymentViewByID(int FormID) {
            return this.TAFormMarketingPaymentView.GetDataByID(FormID)[0];
        }

        public QueryDS.FormMarketingPaymentViewDataTable GetPagedFormMarketingPaymentViewByRight(string queryExpression, int startRowIndex, int maximumRows, string sortExpression, int UserID, int PositionID) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return null;
            }
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "Form.SubmitDate DESC";
            }
            return this.TAFormMarketingPaymentView.GetPagedMarketingPaymentByRight(sortExpression, startRowIndex, maximumRows, queryExpression, UserID, PositionID);
        }

        public int QueryFormMarketingPaymentViewCountByRight(string queryExpression, int UserID, int PositionID) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return 0;
            }
            return (int)this.TAFormMarketingPaymentView.QueryFormMarketingPaymentViewCountByRight(queryExpression, UserID, PositionID);
        }

        #endregion

        #region FormRDPaymentView

        public QueryDS.FormRDPaymentViewRow GetFormRDPaymentViewByID(int FormID) {
            return this.TAFormRDPaymentView.GetDataByID(FormID)[0];
        }

        public QueryDS.FormRDPaymentViewDataTable GetPagedFormRDPaymentViewByRight(string queryExpression, int startRowIndex, int maximumRows, string sortExpression, int UserID, int PositionID) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return null;
            }
            if (sortExpression == null || sortExpression.Length == 0) {
                sortExpression = "Form.SubmitDate DESC";
            }
            return this.TAFormRDPaymentView.GetPagedRDPaymentViewByRight(sortExpression, startRowIndex, maximumRows, queryExpression, UserID, PositionID);
        }

        public int QueryFormRDPaymentViewCountByRight(string queryExpression, int UserID, int PositionID) {
            if (queryExpression == null || queryExpression.Length == 0) {
                return 0;
            }
            return (int)this.TAFormRDPaymentView.QueryFormRDPaymentViewCountByRight(queryExpression, UserID, PositionID);
        }

        #endregion

    }
}
