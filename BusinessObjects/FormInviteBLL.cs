using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using BusinessObjects.FormDSTableAdapters;

namespace BusinessObjects {
    public class FormInviteBLL {
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

        private FormInviteApplyTableAdapter _TAFormInviteApply;
        public FormInviteApplyTableAdapter TAFormInviteApply {
            get {
                if (this._TAFormInviteApply == null) {
                    this._TAFormInviteApply = new FormInviteApplyTableAdapter();
                }
                return this._TAFormInviteApply;
            }
        }

        private FormInviteReimburseTableAdapter _TAFormInviteReimburse;
        public FormInviteReimburseTableAdapter TAFormInviteReimburse {
            get {
                if (this._TAFormInviteReimburse == null) {
                    this._TAFormInviteReimburse = new FormInviteReimburseTableAdapter();
                }
                return this._TAFormInviteReimburse;
            }
        }
        #endregion

        #region FormInviteApply Operate
        public void AddFormTravelReimburse(int? RejectedFormID, int UserID, int? ProxyUserID, int? ProxyPositionID, int OrganizationUnitID, int PositionID, SystemEnums.FormType FormTypeID,
                SystemEnums.FormStatus StatusID, DateTime? Period, string Remark,String
CustomerName,String
AttenderNames,String
BusinessRelation,String
Place,String
OccuredDate, DateTime
Purpose,String
InviteType,String
CurrencyID,decimal?
ExchangeRate,decimal?
AmountRMB) {

            SqlTransaction transaction = null;
            try {
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormInviteApply, transaction);

                FormDS.FormDataTable tbForm=new FormDS.FormDataTable();
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
                formRow.PageType = (int)SystemEnums.PageType.TravelReimburseApply;
                this.TAForm.Update(formRow);

                //处理申请表的内容
                FormDS.FormInviteApplyDataTable tbFormInvite = new FormDS.FormInviteApplyDataTable();
                FormDS.FormInviteApplyRow formInviteApplyRow = tbFormInvite.NewFormInviteApplyRow();
                //formInviteApplyRow.FormTravelReimburseID = formRow.FormID;
                //formInviteApplyRow.Period = Period.GetValueOrDefault();
                //formInviteApplyRow.Amount = decimal.Zero;
                //formInviteApplyRow.Remark = Remark;

                //decimal[] calculateAssistant = this.GetPersonalBudgetByOUID(OrganizationUnitID, Period);
                //formTravelReimburseRow.TotalBudget = calculateAssistant[0];
                //formTravelReimburseRow.ApprovedAmount = calculateAssistant[1];
                //formTravelReimburseRow.ApprovingAmount = calculateAssistant[2];
                //formTravelReimburseRow.RemainAmount = calculateAssistant[3];

                //this.FormDataSet.FormTravelReimburse.AddFormTravelReimburseRow(formTravelReimburseRow);
                //this.TAFormTravelReimburse.Update(formTravelReimburseRow);

                ////明细表
                //decimal totalAmount = 0;//计算总申请金额

                //if (RejectedFormID != null) {
                //    FormDS.FormTravelReimburseDetailDataTable newDetailTable = new FormDS.FormTravelReimburseDetailDataTable();
                //    foreach (FormDS.FormTravelReimburseDetailRow detailRow in this.FormDataSet.FormTravelReimburseDetail) {
                //        if (detailRow.RowState != System.Data.DataRowState.Deleted) {
                //            FormDS.FormTravelReimburseDetailRow newDetailRow = newDetailTable.NewFormTravelReimburseDetailRow();
                //            newDetailRow.FormTravelReimburseID = formTravelReimburseRow.FormTravelReimburseID;
                //            newDetailRow.OccurDate = detailRow.OccurDate;
                //            newDetailRow.ManageExpenseItemID = detailRow.ManageExpenseItemID;
                //            newDetailRow.Cost = detailRow.Cost;
                //            newDetailRow.CityID = detailRow.CityID;
                //            newDetailRow.CurrencyID = detailRow.CurrencyID;
                //            newDetailRow.Destination = detailRow.Destination;
                //            newDetailRow.Frequency = detailRow.Frequency;
                //            newDetailRow.UnitPrice = detailRow.UnitPrice;
                //            newDetailRow.ExchangeRate = detailRow.ExchangeRate;
                //            newDetailRow.PayMan = detailRow.PayMan;


                //            if (!detailRow.IsRemarkNull()) {
                //                newDetailRow.Remark = detailRow.Remark;
                //            }
                //            totalAmount += newDetailRow.Cost;
                //            newDetailTable.AddFormTravelReimburseDetailRow(newDetailRow);
                //        }
                //    }
                //    this.TAFormTravelReimburseDetail.Update(newDetailTable);
                //} 


                //// 正式提交或草稿
                //if (StatusID == SystemEnums.FormStatus.Awaiting) {

                //    //如果申请总额大于可用余额，不能提交
                //    if (formTravelReimburseRow.Amount > calculateAssistant[3]) {//如果是减少预算,要做检查
                //        throw new ApplicationException("申请报销总额超出部门可用余额，不能提交！");
                //    }
                //}
                //formTravelReimburseRow.Amount = totalAmount;
                //this.TAFormTravelReimburse.Update(formTravelReimburseRow);
                //Dictionary<string, object> dic = new Dictionary<string, object>();
                //dic["Apply_Amount"] = totalAmount;//金额

                //APHelper AP = new APHelper();
                //new APFlowBLL().ApplyForm(AP, TAForm, RejectedFormID, formRow, OrganizationUnitID, FlowTemplate, StatusID, dic);

                transaction.Commit();
            } catch (Exception ex) {
                transaction.Rollback();
                throw new ApplicationException(ex.Message);
            } finally {
                transaction.Dispose();
            }
        }
        #endregion

        #region FormInviteReimburse Operate
        #endregion
    }
}
