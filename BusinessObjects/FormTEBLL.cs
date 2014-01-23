using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using BusinessObjects.FormDSTableAdapters;
using lib.wf;

namespace BusinessObjects {
    public class FormTEBLL {

        #region Tableadapters

        private FormTableAdapter _TAForm;
        public FormTableAdapter TAForm {
            get {
                if (this._TAForm == null) {
                    this._TAForm = new FormTableAdapter();
                }
                return this._TAForm;
            }
        }

        private FormInvitationApplyTableAdapter _TAFormInvitationApply;
        public FormInvitationApplyTableAdapter TAFormInvitationApply {
            get {
                if (this._TAFormInvitationApply == null) {
                    this._TAFormInvitationApply = new FormInvitationApplyTableAdapter();
                }
                return this._TAFormInvitationApply;
            }
        }

        private FormInvitationReimburseTableAdapter _TAFormInvitationReimburse;
        public FormInvitationReimburseTableAdapter TAFormInvitationReimburse {
            get {
                if (this._TAFormInvitationReimburse == null) {
                    this._TAFormInvitationReimburse = new FormInvitationReimburseTableAdapter();
                }
                return this._TAFormInvitationReimburse;
            }
        }

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

        private FormTravelReimburseTableAdapter m_FormTravelReimburseTableAdapter;
        public FormTravelReimburseTableAdapter TAFormTravelReimburse {
            get {
                if (this.m_FormTravelReimburseTableAdapter == null) {
                    this.m_FormTravelReimburseTableAdapter = new FormTravelReimburseTableAdapter();
                }
                return this.m_FormTravelReimburseTableAdapter;
            }
        }

        private FormTravelReimburseDetailTableAdapter m_FormTravelReimburseDetailTableAdapter;
        public FormTravelReimburseDetailTableAdapter TAFormTravelReimburseDetail {
            get {
                if (this.m_FormTravelReimburseDetailTableAdapter == null) {
                    this.m_FormTravelReimburseDetailTableAdapter = new FormTravelReimburseDetailTableAdapter();
                }
                return this.m_FormTravelReimburseDetailTableAdapter;
            }
        }

        private FormPersonalReimburseTableAdapter m_FormPersonalReimburseTableAdapter;
        public FormPersonalReimburseTableAdapter TAFormPersonalReimburse {
            get {
                if (this.m_FormPersonalReimburseTableAdapter == null) {
                    this.m_FormPersonalReimburseTableAdapter = new FormPersonalReimburseTableAdapter();
                }
                return this.m_FormPersonalReimburseTableAdapter;
            }
        }

        private FormPersonalReimburseDetailTableAdapter m_FormPersonalReimburseDetailTableAdapter;
        public FormPersonalReimburseDetailTableAdapter TAFormPersonalReimburseDetail {
            get {
                if (this.m_FormPersonalReimburseDetailTableAdapter == null) {
                    this.m_FormPersonalReimburseDetailTableAdapter = new FormPersonalReimburseDetailTableAdapter();
                }
                return this.m_FormPersonalReimburseDetailTableAdapter;
            }
        }
        #endregion

        #region GetData

        public FormDS.FormDataTable GetFormByID(int FormID) {
            return this.TAForm.GetDataByID(FormID);
        }

        public FormDS.FormPersonalReimburseDataTable GetFormPersonalReimburseByID(int FormPersonalReimburseID) {
            return this.TAFormPersonalReimburse.GetDataByFormPersonalReimburseID(FormPersonalReimburseID);
        }

        public FormDS.FormPersonalReimburseDetailDataTable GetFormPersonalReimburseDetailByFormPersonalReimburseID(int FormPersonalReimburseID) {
            return this.TAFormPersonalReimburseDetail.GetDataByFormPersonalReimburseID(FormPersonalReimburseID);
        }

        public FormDS.FormPersonalReimburseDetailDataTable GetFormPersonalReimburseDetail() {
            return this.FormDataSet.FormPersonalReimburseDetail;
        }

        public FormDS.FormTravelReimburseDataTable GetFormTravelReimburseByID(int FormTravelReimburseID) {
            return this.TAFormTravelReimburse.GetDataByFormTravelReimburseID(FormTravelReimburseID);
        }

        public FormDS.FormTravelReimburseDetailDataTable GetFormTravelReimburseDetailByFormTravelReimburseID(int FormTravelReimburselID) {
            return this.TAFormTravelReimburseDetail.GetDataByFormTravelReimburseID(FormTravelReimburselID);
        }

        public FormDS.FormTravelReimburseDetailDataTable GetFormTravelReimburseDetail() {
            return this.FormDataSet.FormTravelReimburseDetail;
        }

        #endregion

        #region FormInvitationApply Operate

        public FormDS.FormInvitationApplyRow GetByID(int ID) {
            return this.TAFormInvitationApply.GetDataByID(ID)[0];
        }

        public void AddFormInvitationApply(int? RejectedFormID, int UserID, int? ProxyUserID, int? ProxyPositionID, int OrganizationUnitID, int PositionID, SystemEnums.FormType FormTypeID,
                SystemEnums.FormStatus StatusID, DateTime? Period, string Remark, String CustomerName, String AttenderNames, int AttenderCount, String BusinessRelation, String Place, DateTime? OccuredDate, String Purpose, String InvitationType, int CurrencyID, decimal? ExchangeRate, decimal? Amount,
            decimal? TotalBudget, decimal? ApprovingAmount, decimal? ApprovedAmount, decimal? RemainBudget) {

            SqlTransaction transaction = null;
            try {
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormInvitationApply, transaction);

                FormDS.FormDataTable tbForm = new FormDS.FormDataTable();
                FormDS.FormRow formRow = tbForm.NewFormRow();
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
                formRow.PageType = (int)SystemEnums.PageType.FormInvitationApply;
                formRow.CostCenterID = new StuffUserBLL().GetCostCenterIDByPositionID(PositionID);
                tbForm.AddFormRow(formRow);
                this.TAForm.Update(tbForm);

                //处理申请表的内容
                FormDS.FormInvitationApplyDataTable tbFormInvitation = new FormDS.FormInvitationApplyDataTable();
                FormDS.FormInvitationApplyRow FormInvitationApplyRow = tbFormInvitation.NewFormInvitationApplyRow();
                FormInvitationApplyRow.FormInvitationApplyID = formRow.FormID;
                if (Period != null) {
                    FormInvitationApplyRow.Period = Period.GetValueOrDefault();
                }
                FormInvitationApplyRow.CustomerName = CustomerName;
                FormInvitationApplyRow.Remark = Remark;
                FormInvitationApplyRow.AttenderNames = AttenderNames;
                FormInvitationApplyRow.AttenderCount = AttenderCount;
                FormInvitationApplyRow.BusinessRelation = BusinessRelation;
                FormInvitationApplyRow.Place = Place;
                if (OccuredDate != null) {
                    FormInvitationApplyRow.OccuredDate = OccuredDate.GetValueOrDefault();
                }
                FormInvitationApplyRow.Purpose = Purpose;
                FormInvitationApplyRow.InvitationType = InvitationType;
                FormInvitationApplyRow.CurrencyID = CurrencyID;
                FormInvitationApplyRow.ExchangeRate = ExchangeRate.GetValueOrDefault();
                FormInvitationApplyRow.Amount = Amount.GetValueOrDefault();

                FormInvitationApplyRow.TotalBudget = TotalBudget.GetValueOrDefault();
                FormInvitationApplyRow.ApprovingAmount = ApprovingAmount.GetValueOrDefault();
                FormInvitationApplyRow.ApprovedAmount = ApprovedAmount.GetValueOrDefault();
                FormInvitationApplyRow.RemainBudget = RemainBudget.GetValueOrDefault();

                tbFormInvitation.AddFormInvitationApplyRow(FormInvitationApplyRow);
                this.TAFormInvitationApply.Update(tbFormInvitation);

                //作废之前的单据
                if (RejectedFormID != null) {
                    FormDS.FormRow oldRow = TAForm.GetDataByID(RejectedFormID.GetValueOrDefault())[0];
                    if (oldRow.StatusID == (int)SystemEnums.FormStatus.Rejected) {
                        new APFlowBLL().ScrapForm(oldRow.FormID);
                    }
                }

                // 正式提交或草稿
                if (StatusID == SystemEnums.FormStatus.Awaiting) {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic["TotalAmount"] = FormInvitationApplyRow.AmountRMB;
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
                throw new ApplicationException(ex.Message);
            } finally {
                transaction.Dispose();
            }
        }

        public void UpdateFormInvitationApply(int RejectedFormID, int FormID, int FormTypeID, SystemEnums.FormStatus StatusID, DateTime? Period, string Remark, String CustomerName, String AttenderNames,
            int AttenderCount, String BusinessRelation, String Place, DateTime? OccuredDate, String Purpose, String InvitationType, int CurrencyID, decimal? ExchangeRate, decimal? Amount,
            decimal? TotalBudget, decimal? ApprovingAmount, decimal? ApprovedAmount, decimal? RemainBudget) {
            SqlTransaction transaction = null;
            try {
                //事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormInvitationApply, transaction);

                FormDS.FormRow formRow = this.TAForm.GetDataByID(FormID)[0];
                FormDS.FormInvitationApplyRow FormInvitationApplyRow = this.TAFormInvitationApply.GetDataByID(FormID)[0];

                //处理单据内容
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

                //处理申请表的内容

                if (Period != null) {
                    FormInvitationApplyRow.Period = Period.GetValueOrDefault();
                }
                FormInvitationApplyRow.CustomerName = CustomerName;
                FormInvitationApplyRow.Remark = Remark;
                FormInvitationApplyRow.AttenderNames = AttenderNames;
                FormInvitationApplyRow.AttenderCount = AttenderCount;
                FormInvitationApplyRow.BusinessRelation = BusinessRelation;
                FormInvitationApplyRow.Place = Place;
                if (OccuredDate != null) {
                    FormInvitationApplyRow.OccuredDate = OccuredDate.GetValueOrDefault();
                }
                FormInvitationApplyRow.Purpose = Purpose;
                FormInvitationApplyRow.InvitationType = InvitationType;
                FormInvitationApplyRow.CurrencyID = CurrencyID;
                FormInvitationApplyRow.ExchangeRate = ExchangeRate.GetValueOrDefault();
                FormInvitationApplyRow.Amount = Amount.GetValueOrDefault();

                FormInvitationApplyRow.TotalBudget = TotalBudget.GetValueOrDefault();
                FormInvitationApplyRow.ApprovingAmount = ApprovingAmount.GetValueOrDefault();
                FormInvitationApplyRow.ApprovedAmount = ApprovedAmount.GetValueOrDefault();
                FormInvitationApplyRow.RemainBudget = RemainBudget.GetValueOrDefault();

                this.TAFormInvitationApply.Update(FormInvitationApplyRow);

                // 正式提交或草稿
                if (StatusID == SystemEnums.FormStatus.Awaiting) {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic["TotalAmount"] = FormInvitationApplyRow.AmountRMB;
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
                throw new ApplicationException(ex.Message);
            } finally {
                transaction.Dispose();
            }
        }

        public void DeleteFormInvitationApply(int FormID) {
            SqlTransaction transaction = null;
            try {
                transaction = TableAdapterHelper.BeginTransaction(TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormInvitationApply, transaction);
                this.TAFormInvitationApply.DeleteByID(FormID);
                this.TAForm.DeleteByID(FormID);
                transaction.Commit();
            } catch (Exception) {
                throw new ApplicationException(); ;
            }
        }

        #endregion

        #region FormInvitationReimburseApply Operate

        public FormDS.FormInvitationReimburseRow GetFormInvitationReimburseRowByID(int FormInvitationReimburseID) {
            return this.TAFormInvitationReimburse.GetDataByID(FormInvitationReimburseID)[0];
        }

        public void AddFormInvitationReimburseApply(int? FormInvitationApplyID, int? RejectedFormID, int UserID, int? ProxyUserID, int? ProxyPositionID, int OrganizationUnitID, int PositionID, SystemEnums.FormType FormTypeID,
                SystemEnums.FormStatus StatusID, DateTime? Period, string Remark, String CustomerName, String AttenderNames, int AttenderCount, String BusinessRelation, String Place, DateTime? OccuredDate, String Purpose, String InvitationType, int CurrencyID, decimal? ExchangeRate, decimal? Amount,
            decimal? TotalBudget, decimal? ApprovingAmount, decimal? ApprovedAmount, decimal? RemainBudget, int ManageExpenseItemID) {

            SqlTransaction transaction = null;
            try {
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormInvitationApply, transaction);

                FormDS.FormDataTable tbForm = new FormDS.FormDataTable();
                FormDS.FormRow formRow = tbForm.NewFormRow();
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
                formRow.PageType = (int)SystemEnums.PageType.FormInvitationReimburse;
                formRow.CostCenterID = new StuffUserBLL().GetCostCenterIDByPositionID(PositionID);
                tbForm.AddFormRow(formRow);
                this.TAForm.Update(tbForm);

                //处理申请表的内容
                FormDS.FormInvitationReimburseDataTable tbFormInvitationReimburse = new FormDS.FormInvitationReimburseDataTable();
                FormDS.FormInvitationReimburseRow FormInvitationReimburseRow = tbFormInvitationReimburse.NewFormInvitationReimburseRow();
                FormInvitationReimburseRow.FormInvitationReimburseID = formRow.FormID;
                if (Period != null) {
                    FormInvitationReimburseRow.Period = Period.GetValueOrDefault();
                }
                FormInvitationReimburseRow.CustomerName = CustomerName;
                FormInvitationReimburseRow.Remark = Remark;
                FormInvitationReimburseRow.AttenderNames = AttenderNames;
                FormInvitationReimburseRow.AttenderCount = AttenderCount;
                FormInvitationReimburseRow.BusinessRelation = BusinessRelation;
                FormInvitationReimburseRow.Place = Place;
                if (OccuredDate != null) {
                    FormInvitationReimburseRow.OccuredDate = OccuredDate.GetValueOrDefault();
                }
                FormInvitationReimburseRow.Purpose = Purpose;
                FormInvitationReimburseRow.InvitationType = InvitationType;
                FormInvitationReimburseRow.CurrencyID = CurrencyID;
                FormInvitationReimburseRow.ExchangeRate = ExchangeRate.GetValueOrDefault();
                FormInvitationReimburseRow.Amount = Amount.GetValueOrDefault();
                FormInvitationReimburseRow.ManageExpenseItemID = ManageExpenseItemID;

                if (FormInvitationApplyID != null) {
                    FormInvitationReimburseRow.FormInvitationApplyID = FormInvitationApplyID.GetValueOrDefault();
                }

                FormInvitationReimburseRow.TotalBudget = TotalBudget.GetValueOrDefault();
                FormInvitationReimburseRow.ApprovingAmount = ApprovingAmount.GetValueOrDefault();
                FormInvitationReimburseRow.ApprovedAmount = ApprovedAmount.GetValueOrDefault();
                FormInvitationReimburseRow.RemainBudget = RemainBudget.GetValueOrDefault();

                tbFormInvitationReimburse.AddFormInvitationReimburseRow(FormInvitationReimburseRow);
                this.TAFormInvitationReimburse.Update(tbFormInvitationReimburse);

                //作废之前的单据
                if (RejectedFormID != null) {
                    FormDS.FormRow oldRow = TAForm.GetDataByID(RejectedFormID.GetValueOrDefault())[0];
                    if (oldRow.StatusID == (int)SystemEnums.FormStatus.Rejected) {
                        new APFlowBLL().ScrapForm(oldRow.FormID);
                    }
                }

                // 正式提交或草稿
                if (StatusID == SystemEnums.FormStatus.Awaiting) {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic["TotalAmount"] = FormInvitationReimburseRow.AmountRMB;
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
                throw new ApplicationException(ex.Message);
            } finally {
                transaction.Dispose();
            }
        }

        public void UpdateFormInvitationReimburseApply(int RejectedFormID, int FormID, int FormTypeID, SystemEnums.FormStatus StatusID, DateTime? Period, string Remark, String CustomerName, String AttenderNames,
            int AttenderCount, String BusinessRelation, String Place, DateTime? OccuredDate, String Purpose, String InvitationType, int CurrencyID, decimal? ExchangeRate, decimal? Amount,
            decimal? TotalBudget, decimal? ApprovingAmount, decimal? ApprovedAmount, decimal? RemainBudget, int ManageExpenseItemID) {
            SqlTransaction transaction = null;
            try {
                //开始事务
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormInvitationApply, transaction);

                FormDS.FormRow formRow = this.TAForm.GetDataByID(FormID)[0];
                FormDS.FormInvitationReimburseRow FormInvitationReimburseRow = this.TAFormInvitationReimburse.GetDataByID(FormID)[0];

                //处理表单内容
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

                //处理申请表的内容
                if (Period != null) {
                    FormInvitationReimburseRow.Period = Period.GetValueOrDefault();
                }
                FormInvitationReimburseRow.CustomerName = CustomerName;
                FormInvitationReimburseRow.Remark = Remark;
                FormInvitationReimburseRow.AttenderNames = AttenderNames;
                FormInvitationReimburseRow.AttenderCount = AttenderCount;
                FormInvitationReimburseRow.BusinessRelation = BusinessRelation;
                FormInvitationReimburseRow.Place = Place;
                if (OccuredDate != null) {
                    FormInvitationReimburseRow.OccuredDate = OccuredDate.GetValueOrDefault();
                }
                FormInvitationReimburseRow.Purpose = Purpose;
                FormInvitationReimburseRow.InvitationType = InvitationType;
                FormInvitationReimburseRow.CurrencyID = CurrencyID;
                FormInvitationReimburseRow.ExchangeRate = ExchangeRate.GetValueOrDefault();
                FormInvitationReimburseRow.Amount = Amount.GetValueOrDefault();
                FormInvitationReimburseRow.ManageExpenseItemID = ManageExpenseItemID;

                FormInvitationReimburseRow.TotalBudget = TotalBudget.GetValueOrDefault();
                FormInvitationReimburseRow.ApprovingAmount = ApprovingAmount.GetValueOrDefault();
                FormInvitationReimburseRow.ApprovedAmount = ApprovedAmount.GetValueOrDefault();
                FormInvitationReimburseRow.RemainBudget = RemainBudget.GetValueOrDefault();

                this.TAFormInvitationReimburse.Update(FormInvitationReimburseRow);

                // 正式提交或草稿
                if (StatusID == SystemEnums.FormStatus.Awaiting) {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic["TotalAmount"] = FormInvitationReimburseRow.AmountRMB;
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
                throw new ApplicationException(ex.Message);
            } finally {
                transaction.Dispose();
            }
        }

        public void DeleteFormInvitationReimburseApply(int FormID) {
            SqlTransaction transaction = null;
            try {
                transaction = TableAdapterHelper.BeginTransaction(TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormInvitationReimburse, transaction);
                this.TAFormInvitationReimburse.DeleteByID(FormID);
                this.TAForm.DeleteByID(FormID);
                transaction.Commit();
            } catch (Exception) {
                throw new ApplicationException(); ;
            }
        }

        public int QueryEnabledInvitationReimburseCount(int FormInvitationApplyID) {
            return this.TAFormInvitationReimburse.SelectEnabledInvitationReimburse(FormInvitationApplyID).GetValueOrDefault();
        }

        #endregion

        #region FormPersonalReimburseDetail

        public void AddFormPersonalReimburseDetail(DateTime OccurDate, int ManageExpenseItemID, int CurrencyID, decimal ApplyAmount, decimal ExchangeRate, string Remark) {

            FormDS.FormPersonalReimburseDetailRow rowDetail = this.FormDataSet.FormPersonalReimburseDetail.NewFormPersonalReimburseDetailRow();
            rowDetail.FormPersonalReimburseID = 0;
            rowDetail.OccurDate = OccurDate;
            rowDetail.ManageExpenseItemID = ManageExpenseItemID;
            rowDetail.ApplyAmount = ApplyAmount;
            rowDetail.Remark = Remark;
            rowDetail.CurrencyID = CurrencyID;
            rowDetail.ExchangeRate = ExchangeRate;
            rowDetail.RMB = decimal.Round(ApplyAmount * ExchangeRate, 2);
            // 填加行并进行更新处理
            this.FormDataSet.FormPersonalReimburseDetail.AddFormPersonalReimburseDetailRow(rowDetail);

        }

        public void UpdateFormPersonalReimburseDetail(int FormPersonalReimburseDetailID, DateTime OccurDate, int ManageExpenseItemID, int CurrencyID, decimal ApplyAmount, decimal ExchangeRate, string Remark) {
            FormDS.FormPersonalReimburseDetailRow rowDetail = this.FormDataSet.FormPersonalReimburseDetail.FindByFormPersonalReimburseDetailID(FormPersonalReimburseDetailID);
            rowDetail.OccurDate = OccurDate;
            rowDetail.ManageExpenseItemID = ManageExpenseItemID;
            rowDetail.ApplyAmount = ApplyAmount;
            rowDetail.Remark = Remark;
            rowDetail.CurrencyID = CurrencyID;
            rowDetail.RMB = decimal.Round(ApplyAmount * ExchangeRate, 2);
            rowDetail.ExchangeRate = ExchangeRate;
        }

        public void DeleteFormPersonalReimburseDetailByID(int FormPersonalReimburseDetailID) {
            for (int index = 0; index < this.FormDataSet.FormPersonalReimburseDetail.Count; index++) {
                if ((int)this.FormDataSet.FormPersonalReimburseDetail.Rows[index]["FormPersonalReimburseDetailID"] == FormPersonalReimburseDetailID) {
                    this.FormDataSet.FormPersonalReimburseDetail.Rows[index].Delete();
                    break;
                }
            }
        }

        #endregion

        #region FormPersonalReimburse

        public void AddFormPersonalReimburse(int? RejectedFormID, int UserID, int? ProxyUserID, int? ProxyPositionID, int OrganizationUnitID, int PositionID, SystemEnums.FormType FormTypeID,
                SystemEnums.FormStatus StatusID, DateTime? Period, string Remark, decimal TotalBudget, decimal ApprovedAmount, decimal ApprovingAmount, decimal RemainAmount, string AttachedFileName, string RealAttachedFileName) {
            SqlTransaction transaction = null;
            try {
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormPersonalReimburse, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormPersonalReimburseDetail, transaction);

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
                formRow.InTurnUserIds = "P";//待改动
                formRow.InTurnPositionIds = "P";//待改动
                formRow.OrganizationUnitID = OrganizationUnitID;
                formRow.PositionID = PositionID;
                formRow.FormTypeID = (int)FormTypeID;
                formRow.StatusID = (int)StatusID;
                formRow.SubmitDate = DateTime.Now;
                formRow.LastModified = DateTime.Now;
                formRow.PageType = (int)SystemEnums.PageType.FormPersonalReimburse;
                formRow.CostCenterID = new StuffUserBLL().GetCostCenterIDByPositionID(PositionID);
                formRow.IsCreateVoucher = false;
                formRow.IsExportLock = false;
                formRow.IsCompletePayment = false;
                this.FormDataSet.Form.AddFormRow(formRow);
                this.TAForm.Update(formRow);

                //处理申请表的内容
                FormDS.FormPersonalReimburseRow formPersonalReimburseRow = this.FormDataSet.FormPersonalReimburse.NewFormPersonalReimburseRow();
                formPersonalReimburseRow.FormPersonalReimburseID = formRow.FormID;
                if (Period != null) {
                    formPersonalReimburseRow.Period = Period.GetValueOrDefault();
                }
                formPersonalReimburseRow.Amount = decimal.Zero;
                formPersonalReimburseRow.Remark = Remark;

                formPersonalReimburseRow.TotalBudget = TotalBudget;
                formPersonalReimburseRow.ApprovedAmount = ApprovedAmount;
                formPersonalReimburseRow.ApprovingAmount = ApprovingAmount;
                formPersonalReimburseRow.RemainAmount = RemainAmount;
                if (AttachedFileName != null && AttachedFileName != string.Empty) {
                    formPersonalReimburseRow.AttachedFileName = AttachedFileName;
                }
                if (RealAttachedFileName != null && RealAttachedFileName != string.Empty) {
                    formPersonalReimburseRow.RealAttachedFileName = RealAttachedFileName;
                }

                this.FormDataSet.FormPersonalReimburse.AddFormPersonalReimburseRow(formPersonalReimburseRow);
                this.TAFormPersonalReimburse.Update(formPersonalReimburseRow);

                //明细表
                decimal totalAmountRMB = 0;//计算总申请金额

                if (RejectedFormID != null) {
                    FormDS.FormPersonalReimburseDetailDataTable newDetailTable = new FormDS.FormPersonalReimburseDetailDataTable();
                    foreach (FormDS.FormPersonalReimburseDetailRow detailRow in this.FormDataSet.FormPersonalReimburseDetail) {
                        if (detailRow.RowState != System.Data.DataRowState.Deleted) {
                            FormDS.FormPersonalReimburseDetailRow newDetailRow = newDetailTable.NewFormPersonalReimburseDetailRow();
                            newDetailRow.FormPersonalReimburseID = formPersonalReimburseRow.FormPersonalReimburseID;
                            newDetailRow.OccurDate = detailRow.OccurDate;
                            newDetailRow.ManageExpenseItemID = detailRow.ManageExpenseItemID;
                            newDetailRow.ApplyAmount = detailRow.ApplyAmount;
                            newDetailRow.RMB = detailRow.RMB;
                            newDetailRow.CurrencyID = detailRow.CurrencyID;
                            newDetailRow.ExchangeRate = detailRow.ExchangeRate;

                            if (!detailRow.IsRemarkNull()) {
                                newDetailRow.Remark = detailRow.Remark;
                            }
                            totalAmountRMB += newDetailRow.RMB;
                            newDetailTable.AddFormPersonalReimburseDetailRow(newDetailRow);
                        }
                    }
                    this.TAFormPersonalReimburseDetail.Update(newDetailTable);
                } else {
                    foreach (FormDS.FormPersonalReimburseDetailRow detailRow in this.FormDataSet.FormPersonalReimburseDetail) {
                        // 与父表绑定
                        if (detailRow.RowState != DataRowState.Deleted) {
                            detailRow.FormPersonalReimburseID = formPersonalReimburseRow.FormPersonalReimburseID;
                            totalAmountRMB += detailRow.RMB;
                        }
                    }
                    this.TAFormPersonalReimburseDetail.Update(this.FormDataSet.FormPersonalReimburseDetail);
                }

                formPersonalReimburseRow.Amount = totalAmountRMB;
                this.TAFormPersonalReimburse.Update(formPersonalReimburseRow);
                // 正式提交或草稿
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
                        formRow.FinanceRemark = formPersonalReimburseRow.Remark;
                        this.TAForm.Update(formRow);
                    }
                }

                //作废之前的单据
                if (RejectedFormID != null) {
                    FormDS.FormRow oldRow = TAForm.GetDataByID(RejectedFormID.GetValueOrDefault())[0];
                    if (oldRow.StatusID == (int)SystemEnums.FormStatus.Rejected) {
                        new APFlowBLL().ScrapForm(oldRow.FormID);
                    }
                }
                transaction.Commit();
            } catch (Exception ex) {
                transaction.Rollback();
                throw new ApplicationException(ex.Message);
            } finally {
                transaction.Dispose();
            }
        }

        public void UpdateFormPersonalReimburse(int FormID, SystemEnums.FormStatus StatusID, SystemEnums.FormType FormTypeID, DateTime? Period, string Remark,
                decimal TotalBudget, decimal ApprovedAmount, decimal ApprovingAmount, decimal RemainAmount, string AttachedFileName, string RealAttachedFileName) {
            SqlTransaction transaction = null;
            try {
                ////事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormPersonalReimburse, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormPersonalReimburseDetail, transaction);

                FormDS.FormRow formRow = this.TAForm.GetDataByID(FormID)[0];
                UtilityBLL utility = new UtilityBLL();
                if (StatusID == SystemEnums.FormStatus.Awaiting) {
                    string formTypeString = utility.GetFormTypeString((int)FormTypeID);
                    formRow.FormNo = utility.GetFormNo(formTypeString);
                } else {
                    formRow.SetFormNoNull();
                }
                formRow.StatusID = (int)StatusID;
                formRow.SubmitDate = DateTime.Now;
                formRow.LastModified = DateTime.Now;
                this.TAForm.Update(formRow);

                //处理申请表的内容
                FormDS.FormPersonalReimburseRow formPersonalReimburseRow = this.TAFormPersonalReimburse.GetDataByFormPersonalReimburseID(FormID)[0];
                if (Period != null) {
                    formPersonalReimburseRow.Period = Period.GetValueOrDefault();
                }
                formPersonalReimburseRow.Amount = decimal.Zero;
                formPersonalReimburseRow.Remark = Remark;

                formPersonalReimburseRow.TotalBudget = TotalBudget;
                formPersonalReimburseRow.ApprovedAmount = ApprovedAmount;
                formPersonalReimburseRow.ApprovingAmount = ApprovingAmount;
                formPersonalReimburseRow.RemainAmount = RemainAmount;
                if (AttachedFileName != null && AttachedFileName != string.Empty) {
                    formPersonalReimburseRow.AttachedFileName = AttachedFileName;
                } else {
                    formPersonalReimburseRow.SetAttachedFileNameNull();
                }
                if (RealAttachedFileName != null && RealAttachedFileName != string.Empty) {
                    formPersonalReimburseRow.RealAttachedFileName = RealAttachedFileName;
                } else {
                    formPersonalReimburseRow.SetRealAttachedFileNameNull();
                }
                this.TAFormPersonalReimburse.Update(formPersonalReimburseRow);

                //明细表
                decimal totalAmountRMB = 0;//计算总申请金额
                foreach (FormDS.FormPersonalReimburseDetailRow detailRow in this.FormDataSet.FormPersonalReimburseDetail) {
                    // 与父表绑定
                    if (detailRow.RowState != DataRowState.Deleted) {
                        detailRow.FormPersonalReimburseID = formPersonalReimburseRow.FormPersonalReimburseID;
                        totalAmountRMB += detailRow.RMB;
                    }
                }
                this.TAFormPersonalReimburseDetail.Update(this.FormDataSet.FormPersonalReimburseDetail);

                formPersonalReimburseRow.Amount = totalAmountRMB;
                this.TAFormPersonalReimburse.Update(formPersonalReimburseRow);

                // 正式提交或草稿
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
                        formRow.FinanceRemark = formPersonalReimburseRow.Remark;
                        this.TAForm.Update(formRow);
                    }
                }
                transaction.Commit();
            } catch (Exception ex) {
                transaction.Rollback();
                throw new ApplicationException(ex.Message);
            } finally {
                transaction.Dispose();
            }
        }

        public void DeleteFormPersonalReimburse(int FormID) {
            SqlTransaction transaction = null;
            try {
                //事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormPersonalReimburse, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormPersonalReimburseDetail, transaction);
                this.TAFormPersonalReimburseDetail.DeleteByFormPersonalReimburseID(FormID);
                this.TAFormPersonalReimburse.DeleteByFormPersonalReimburseID(FormID);
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

        #region FormTravelReimburseDetail

        public void AddFormTravelReimburseDetail(DateTime OccurDate, int ManageExpenseItemID, int CityID, string Destination, int CurrencyID, decimal ExchangeRate, decimal UnitPrice, int Frequency, string PayMan, string Remark) {
            FormDS.FormTravelReimburseDetailRow rowDetail = this.FormDataSet.FormTravelReimburseDetail.NewFormTravelReimburseDetailRow();
            rowDetail.FormTravelReimburseID = 0;
            rowDetail.OccurDate = OccurDate;
            rowDetail.ManageExpenseItemID = ManageExpenseItemID;
            rowDetail.Cost = decimal.Round(Frequency * UnitPrice * ExchangeRate, 2);
            rowDetail.Remark = Remark;
            rowDetail.CurrencyID = CurrencyID;
            rowDetail.CityID = CityID;
            rowDetail.Destination = Destination;
            rowDetail.Frequency = Frequency;
            rowDetail.UnitPrice = UnitPrice;
            rowDetail.ExchangeRate = ExchangeRate;
            rowDetail.PayMan = int.Parse(PayMan);
            // 填加行并进行更新处理
            this.FormDataSet.FormTravelReimburseDetail.AddFormTravelReimburseDetailRow(rowDetail);
        }

        public void UpdateFormTravelReimburseDetail(int FormTravelReimburseDetailID, DateTime OccurDate, int ManageExpenseItemID, int CityID, string Destination, int CurrencyID, decimal ExchangeRate, decimal UnitPrice, int Frequency, string PayMan, string Remark) {
            FormDS.FormTravelReimburseDetailRow rowDetail = this.FormDataSet.FormTravelReimburseDetail.FindByFormTravelReimburseDetailID(FormTravelReimburseDetailID);
            rowDetail.FormTravelReimburseID = 0;
            rowDetail.OccurDate = OccurDate;
            rowDetail.ManageExpenseItemID = ManageExpenseItemID;
            rowDetail.Cost = decimal.Round(Frequency * UnitPrice * ExchangeRate, 2);
            rowDetail.Remark = Remark;
            rowDetail.CurrencyID = CurrencyID;
            rowDetail.CityID = CityID;
            rowDetail.Destination = Destination;
            rowDetail.Frequency = Frequency;
            rowDetail.UnitPrice = UnitPrice;
            rowDetail.ExchangeRate = ExchangeRate;
            rowDetail.PayMan = int.Parse(PayMan);
        }

        public void DeleteFormTravelReimburseDetailByID(int FormTravelReimburseDetailID) {
            for (int index = 0; index < this.FormDataSet.FormTravelReimburseDetail.Count; index++) {
                if ((int)this.FormDataSet.FormTravelReimburseDetail.Rows[index]["FormTravelReimburseDetailID"] == FormTravelReimburseDetailID) {
                    this.FormDataSet.FormTravelReimburseDetail.Rows[index].Delete();
                    break;
                }
            }
        }

        #endregion

        #region FormTravelReimburse

        public void AddFormTravelReimburse(int? RejectedFormID, int UserID, int? ProxyUserID, int? ProxyPositionID, int OrganizationUnitID, int PositionID, SystemEnums.FormType FormTypeID,
                SystemEnums.FormStatus StatusID, DateTime? Period, string Remark, decimal TotalBudget, decimal ApprovedAmount, decimal ApprovingAmount, decimal RemainAmount, string AttachedFileName, string RealAttachedFileName) {

            SqlTransaction transaction = null;
            try {
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormTravelReimburse, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormTravelReimburseDetail, transaction);

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
                formRow.InTurnUserIds = "P";//待改动
                formRow.InTurnPositionIds = "P";//待改动
                formRow.OrganizationUnitID = OrganizationUnitID;
                formRow.PositionID = PositionID;
                formRow.FormTypeID = (int)FormTypeID;
                formRow.StatusID = (int)StatusID;
                formRow.SubmitDate = DateTime.Now;
                formRow.LastModified = DateTime.Now;
                formRow.PageType = (int)SystemEnums.PageType.TravelReimburseApply;
                formRow.CostCenterID = new StuffUserBLL().GetCostCenterIDByPositionID(PositionID);
                formRow.IsCreateVoucher = false;
                formRow.IsExportLock = false;
                formRow.IsCompletePayment = false;
                this.FormDataSet.Form.AddFormRow(formRow);
                this.TAForm.Update(formRow);

                //处理申请表的内容
                FormDS.FormTravelReimburseRow formTravelReimburseRow = this.FormDataSet.FormTravelReimburse.NewFormTravelReimburseRow();
                formTravelReimburseRow.FormTravelReimburseID = formRow.FormID;
                if (Period != null) {
                    formTravelReimburseRow.Period = Period.GetValueOrDefault();
                }

                formTravelReimburseRow.Amount = decimal.Zero;
                formTravelReimburseRow.Remark = Remark;
                formTravelReimburseRow.TotalBudget = TotalBudget;
                formTravelReimburseRow.ApprovedAmount = ApprovedAmount;
                formTravelReimburseRow.ApprovingAmount = ApprovingAmount;
                formTravelReimburseRow.RemainAmount = RemainAmount;
                if (AttachedFileName != null && AttachedFileName != string.Empty) {
                    formTravelReimburseRow.AttachedFileName = AttachedFileName;
                }
                if (RealAttachedFileName != null && RealAttachedFileName != string.Empty) {
                    formTravelReimburseRow.RealAttachedFileName = RealAttachedFileName;
                }
                this.FormDataSet.FormTravelReimburse.AddFormTravelReimburseRow(formTravelReimburseRow);
                this.TAFormTravelReimburse.Update(formTravelReimburseRow);

                //明细表
                decimal totalAmountRMB = 0;//计算总申请金额

                if (RejectedFormID != null) {
                    FormDS.FormTravelReimburseDetailDataTable newDetailTable = new FormDS.FormTravelReimburseDetailDataTable();
                    foreach (FormDS.FormTravelReimburseDetailRow detailRow in this.FormDataSet.FormTravelReimburseDetail) {
                        if (detailRow.RowState != System.Data.DataRowState.Deleted) {
                            FormDS.FormTravelReimburseDetailRow newDetailRow = newDetailTable.NewFormTravelReimburseDetailRow();
                            newDetailRow.FormTravelReimburseID = formTravelReimburseRow.FormTravelReimburseID;
                            newDetailRow.OccurDate = detailRow.OccurDate;
                            newDetailRow.ManageExpenseItemID = detailRow.ManageExpenseItemID;
                            newDetailRow.Cost = detailRow.Cost;
                            newDetailRow.CityID = detailRow.CityID;
                            newDetailRow.CurrencyID = detailRow.CurrencyID;
                            newDetailRow.Destination = detailRow.Destination;
                            newDetailRow.Frequency = detailRow.Frequency;
                            newDetailRow.UnitPrice = detailRow.UnitPrice;
                            newDetailRow.ExchangeRate = detailRow.ExchangeRate;
                            newDetailRow.PayMan = detailRow.PayMan;

                            if (!detailRow.IsRemarkNull()) {
                                newDetailRow.Remark = detailRow.Remark;
                            }
                            totalAmountRMB += newDetailRow.Cost;
                            newDetailTable.AddFormTravelReimburseDetailRow(newDetailRow);
                        }
                    }
                    this.TAFormTravelReimburseDetail.Update(newDetailTable);
                } else {
                    foreach (FormDS.FormTravelReimburseDetailRow detailRow in this.FormDataSet.FormTravelReimburseDetail) {
                        // 与父表绑定
                        if (detailRow.RowState != DataRowState.Deleted) {
                            detailRow.FormTravelReimburseID = formTravelReimburseRow.FormTravelReimburseID;
                            totalAmountRMB += detailRow.Cost;
                        }
                    }
                    this.TAFormTravelReimburseDetail.Update(this.FormDataSet.FormTravelReimburseDetail);
                }

                formTravelReimburseRow.Amount = totalAmountRMB;
                this.TAFormTravelReimburse.Update(formTravelReimburseRow);

                // 正式提交或草稿
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
                        formRow.FinanceRemark = formTravelReimburseRow.Remark;
                        this.TAForm.Update(formRow);
                    }
                }

                //作废之前的单据
                if (RejectedFormID != null) {
                    FormDS.FormRow oldRow = TAForm.GetDataByID(RejectedFormID.GetValueOrDefault())[0];
                    if (oldRow.StatusID == (int)SystemEnums.FormStatus.Rejected) {
                        new APFlowBLL().ScrapForm(oldRow.FormID);
                    }
                }
                transaction.Commit();
            } catch (Exception ex) {
                transaction.Rollback();
                throw new ApplicationException(ex.Message);
            } finally {
                transaction.Dispose();
            }
        }

        public void UpdateFormTravelReimburse(int FormID, SystemEnums.FormStatus StatusID, SystemEnums.FormType FormTypeID, DateTime? Period, string Remark,
                decimal TotalBudget, decimal ApprovedAmount, decimal ApprovingAmount, decimal RemainAmount, string AttachedFileName, string RealAttachedFileName) {
            SqlTransaction transaction = null;
            try {
                ////事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormTravelReimburse, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormTravelReimburseDetail, transaction);

                FormDS.FormRow formRow = this.TAForm.GetDataByID(FormID)[0];
                UtilityBLL utility = new UtilityBLL();
                if (StatusID == SystemEnums.FormStatus.Awaiting) {
                    string formTypeString = utility.GetFormTypeString((int)FormTypeID);
                    formRow.FormNo = utility.GetFormNo(formTypeString);
                } else {
                    formRow.SetFormNoNull();
                }
                formRow.StatusID = (int)StatusID;
                formRow.SubmitDate = DateTime.Now;
                formRow.LastModified = DateTime.Now;
                this.TAForm.Update(formRow);

                //处理申请表的内容
                FormDS.FormTravelReimburseRow formTravelReimburseRow = this.TAFormTravelReimburse.GetDataByFormTravelReimburseID(FormID)[0];

                if (Period != null) {
                    formTravelReimburseRow.Period = Period.GetValueOrDefault();
                }
                formTravelReimburseRow.Amount = decimal.Zero;
                formTravelReimburseRow.Remark = Remark;
                formTravelReimburseRow.TotalBudget = TotalBudget;
                formTravelReimburseRow.ApprovedAmount = ApprovedAmount;
                formTravelReimburseRow.ApprovingAmount = ApprovingAmount;
                formTravelReimburseRow.RemainAmount = RemainAmount;
                if (AttachedFileName != null && AttachedFileName != string.Empty) {
                    formTravelReimburseRow.AttachedFileName = AttachedFileName;
                } else {
                    formTravelReimburseRow.SetAttachedFileNameNull();
                }
                if (RealAttachedFileName != null && RealAttachedFileName != string.Empty) {
                    formTravelReimburseRow.RealAttachedFileName = RealAttachedFileName;
                } else {
                    formTravelReimburseRow.SetRealAttachedFileNameNull();
                }

                this.TAFormTravelReimburse.Update(formTravelReimburseRow);

                //明细表
                decimal totalAmount = 0;//计算总申请金额
                foreach (FormDS.FormTravelReimburseDetailRow detailRow in this.FormDataSet.FormTravelReimburseDetail) {
                    // 与父表绑定
                    if (detailRow.RowState != DataRowState.Deleted) {
                        detailRow.FormTravelReimburseID = formTravelReimburseRow.FormTravelReimburseID;
                        totalAmount += detailRow.Cost;
                    }
                }
                this.TAFormTravelReimburseDetail.Update(this.FormDataSet.FormTravelReimburseDetail);

                formTravelReimburseRow.Amount = totalAmount;
                this.TAFormTravelReimburse.Update(formTravelReimburseRow);

                // 正式提交或草稿
                if (StatusID == SystemEnums.FormStatus.Awaiting) {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic["TotalAmount"] = totalAmount;
                    APResult result = new APFlowBLL().CreateProcess(new APParameter(formRow.FormID, formRow.FormNo, formRow.OrganizationUnitID, formRow.UserID, (int)StatusID, formRow.FormTypeID, formRow.PositionID, dic));
                    if (result != null) {
                        formRow.InTurnPositionIds = result.InTurnPositionIds;
                        formRow.InTurnUserIds = result.InTurnUserIds;
                        formRow.SubmitDate = result.ApprovedDate.GetValueOrDefault();
                        formRow.StatusID = result.StatusID;
                        formRow.ProcID = result.ProcID;
                        formRow.FinanceRemark = formTravelReimburseRow.Remark;
                        this.TAForm.Update(formRow);
                    }
                }
                transaction.Commit();
            } catch (Exception ex) {
                transaction.Rollback();
                throw new ApplicationException(ex.Message);
            } finally {
                transaction.Dispose();
            }
        }

        public void DeleteFormTravelReimburse(int FormID) {
            SqlTransaction transaction = null;
            try {
                //事务开始
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormTravelReimburse, transaction);
                TableAdapterHelper.SetTransaction(this.TAFormTravelReimburseDetail, transaction);
                this.TAFormTravelReimburseDetail.DeleteByFormID(FormID);
                this.TAFormTravelReimburse.DeleteByID(FormID);
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

        public decimal[] CheckInvitationCostLimit(int UserID, int PositionID, DateTime Period, int InvitationTypeID) {
            decimal? CostLimit = 0;
            decimal? ApprovingAmount = 0;
            decimal? ApprovedAmount = 0;
            decimal? RemainAmount = 0;
            decimal[] calculateAssistant = new decimal[4];
            this.TAFormInvitationReimburse.CheckInvitationCostLimit(UserID, PositionID, Period, InvitationTypeID, ref CostLimit, ref ApprovingAmount, ref ApprovedAmount, ref RemainAmount);
            calculateAssistant[0] = CostLimit.GetValueOrDefault();
            calculateAssistant[1] = ApprovingAmount.GetValueOrDefault();
            calculateAssistant[2] = ApprovedAmount.GetValueOrDefault();
            calculateAssistant[3] = RemainAmount.GetValueOrDefault();
            return calculateAssistant;
        }

        #region Form General

        public void AddFinanceRemark(int FormID, string FinanceRemark) {
            SqlTransaction transaction = null;
            try {
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                FormDS.FormRow formRow = this.TAForm.GetDataByID(FormID)[0];
                formRow.FinanceRemark = FinanceRemark;
                this.TAForm.Update(formRow);
                transaction.Commit();
            } catch (Exception ex) {
                transaction.Rollback();
                throw new ApplicationException(ex.Message);
            } finally {
                transaction.Dispose();
            }
        }

        public void InvoiceReturned(int FormID) {
            SqlTransaction transaction = null;
            try {
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                FormDS.FormRow formRow = this.TAForm.GetDataByID(FormID)[0];
                formRow.IsInvoiceReturned = true;
                this.TAForm.Update(formRow);
                transaction.Commit();
            } catch (Exception ex) {
                transaction.Rollback();
                throw new ApplicationException(ex.Message);
            } finally {
                transaction.Dispose();
            }

        }

        public void ModifyPeriodForPersonalReimburse(int FormID, DateTime Period) {
            SqlTransaction transaction = null;
            try {
                transaction = TableAdapterHelper.BeginTransaction(this.TAFormPersonalReimburse);
                FormDS.FormPersonalReimburseRow row = this.TAFormPersonalReimburse.GetDataByFormPersonalReimburseID(FormID)[0];
                row.Period = Period;
                this.TAFormPersonalReimburse.Update(row);
                transaction.Commit();
            } catch (Exception ex) {
                transaction.Rollback();
                throw new ApplicationException(ex.Message);
            } finally {
                transaction.Dispose();
            }
        }

        public void ModifyPeriodForTravelReimburse(int FormID, DateTime Period) {
            SqlTransaction transaction = null;
            try {
                transaction = TableAdapterHelper.BeginTransaction(this.TAFormTravelReimburse);
                FormDS.FormTravelReimburseRow row = this.TAFormTravelReimburse.GetDataByFormTravelReimburseID(FormID)[0];
                row.Period = Period;
                this.TAFormTravelReimburse.Update(row);
                transaction.Commit();
            } catch (Exception ex) {
                transaction.Rollback();
                throw new ApplicationException(ex.Message);
            } finally {
                transaction.Dispose();
            }
        }

        #endregion

    }
}
