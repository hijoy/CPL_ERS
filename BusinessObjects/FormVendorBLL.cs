using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects.PurchaseDSTableAdapters;
using System.Web.Security;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using lib.wf;
using BusinessObjects.MasterDataTableAdapters;

namespace BusinessObjects {
    public class FormVendorBLL {

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

        private FormVendorTableAdapter _TAFormVendor;
        public FormVendorTableAdapter TAFormVendor {
            get {
                if (this._TAFormVendor == null) {
                    this._TAFormVendor = new BusinessObjects.PurchaseDSTableAdapters.FormVendorTableAdapter();
                }
                return this._TAFormVendor;
            }
        }

        private QueryDSTableAdapters.FormVendorViewTableAdapter _TAFormVendorView;
        public QueryDSTableAdapters.FormVendorViewTableAdapter TAFormVendorView {
            get {
                if (this._TAFormVendorView == null) {
                    this._TAFormVendorView = new QueryDSTableAdapters.FormVendorViewTableAdapter();
                }
                return this._TAFormVendorView;
            }
        }

        #endregion

        #region 获取数据

        public PurchaseDS.FormDataTable GetFormByID(int FormID) {
            return this.TAForm.GetDataByID(FormID);
        }

        public PurchaseDS.FormVendorDataTable GetFormVendorByID(int FormVendorID) {
            return this.TAFormVendor.GetDataByID(FormVendorID);
        }

        public QueryDS.FormVendorViewDataTable GetFormVendorByVendorID(int VendorID, String sortExpression) {
            if (sortExpression == null) {
                sortExpression = "SubmitDate desc";
            }
            return this.TAFormVendorView.GetFormVendorViewByVendorID(VendorID);
        }
        public QueryDS.FormVendorViewDataTable GetApproveFormVendorByVendorID(int VendorID) {
            return this.TAFormVendorView.GetApproveFormVendorByVendorID(VendorID);
        }
        #endregion

        #region FormVendorApply Operate

        public void AddFormVendorApply(int? RejectedFormID, int UserID, int? ProxyStuffUserId, int? ProxyStuffPositionID, int OrganizationUnitID, int PositionID,
                 SystemEnums.FormType FormTypeID, SystemEnums.FormStatus StatusID, int VendorID, string VendorName, string VendorAddress, string City, string Country, string ContactName, int? VendorTypeID,
             string PhoneNumber, int OneTimeVendor, int HoldVendor, string PurchasingPostalCode, string AlphaSearchKey, string PurchasingContact, string PurchasingAddress,
            string PurchasingCity, string PurchasingPhoneNumber, int? BankCodeID, int? MethodPaymentID, int? PaymentTermID,
                 int? TransTypeID, int? VatTypeID, string BankName, string AccountNo, string BankNo, int? ACTypeID, string AttachmentFileName, string RealAttachmentFileName, string Remark, string ModifyReason, int ActionType) {

            SqlTransaction transaction = null;
            try {
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormVendor, transaction);

                PurchaseDS.FormRow formRow = this.PurchaseDataSet.Form.NewFormRow();
                if (RejectedFormID != null) {
                    formRow.RejectedFormID = RejectedFormID.GetValueOrDefault();
                }
                formRow.UserID = UserID;
                UtilityBLL utility = new UtilityBLL();

                string formTypeString = utility.GetFormTypeString((int)FormTypeID);
                formRow.FormNo = utility.GetFormNo(formTypeString);

                if (ProxyStuffUserId != null) {
                    formRow.ProxyUserID = ProxyStuffUserId.GetValueOrDefault();
                }
                if (ProxyStuffPositionID != null) {
                    formRow.ProxyPositionID = ProxyStuffPositionID.GetValueOrDefault();
                }
                formRow.InTurnUserIds = "P";//待改动
                formRow.InTurnPositionIds = "P";//待改动
                formRow.OrganizationUnitID = OrganizationUnitID;
                formRow.PositionID = PositionID;
                formRow.FormTypeID = (int)FormTypeID;
                formRow.StatusID = (int)StatusID;
                formRow.SubmitDate = DateTime.Now;
                formRow.LastModified = DateTime.Now;
                formRow.PageType = (int)SystemEnums.PageType.VendorApply;
                this.PurchaseDataSet.Form.AddFormRow(formRow);
                this.TAForm.Update(formRow);

                //处理申请表的内容
                PurchaseDS.FormVendorDataTable tbFormVendor = new PurchaseDS.FormVendorDataTable();
                PurchaseDS.FormVendorRow FormVendorRow = tbFormVendor.NewFormVendorRow();

                FormVendorRow.FormVendorID = formRow.FormID;
                if (ActionType != 1 && VendorID > 0) {
                    FormVendorRow.VendorID = VendorID;
                    FormVendorRow.VendorCode = new MasterDataBLL().GetVendorByID(VendorID).VendorCode;
                }
                FormVendorRow.VendorName = VendorName;
                FormVendorRow.VendorAddress = VendorAddress;
                FormVendorRow.City = City;

                FormVendorRow.Postal = Country;
                FormVendorRow.ContactName = ContactName;
                if (VendorTypeID != null) {
                    FormVendorRow.VendorTypeID = VendorTypeID.GetValueOrDefault();
                }
                FormVendorRow.PhoneNumber = PhoneNumber;
                FormVendorRow.OneTimeVendor = OneTimeVendor == 1;
                FormVendorRow.HoldVendor = HoldVendor == 1;
                FormVendorRow.PurchaseingPostalCode = PurchasingPostalCode;
                FormVendorRow.AlphaSearchKey = AlphaSearchKey;
                FormVendorRow.PurchasingContact = PurchasingContact;
                FormVendorRow.PurchasingAddress = PurchasingAddress;
                FormVendorRow.PurchasingCity = PurchasingCity;
                FormVendorRow.PurchasePhoneNumber = PurchasingPhoneNumber;
                if (BankCodeID != null && BankCodeID != 0) {
                    FormVendorRow.BankCodeID = BankCodeID.GetValueOrDefault();
                    MasterData.BankCodeRow rowBankCode = new MasterDataBLL().GetBankCodeById(BankCodeID.GetValueOrDefault())[0];
                    FormVendorRow.BankCode = rowBankCode.BankCode + rowBankCode.Description;
                }
                if (MethodPaymentID != null) {
                    FormVendorRow.MethodPaymentID = MethodPaymentID.GetValueOrDefault();
                }
                if (PaymentTermID != null) {
                    FormVendorRow.PaymentTermID = PaymentTermID.GetValueOrDefault();
                }
                if (TransTypeID != null) {
                    FormVendorRow.TransTypeID = TransTypeID.GetValueOrDefault();
                }
                if (VatTypeID != null) {
                    FormVendorRow.VATTypeID = VatTypeID.GetValueOrDefault();
                }
                FormVendorRow.BankName = BankName;
                FormVendorRow.AccountNo = AccountNo;
                FormVendorRow.BankNo = BankNo;
                if (ACTypeID != null) {
                    FormVendorRow.ACTypeID = ACTypeID.GetValueOrDefault();
                }
                FormVendorRow.AttachmentFileName = AttachmentFileName;
                FormVendorRow.RealAttachmentFileName = RealAttachmentFileName;
                FormVendorRow.Remark = Remark;
                FormVendorRow.ModifyReason = ModifyReason;
                FormVendorRow.ActionType = ActionType;

                tbFormVendor.AddFormVendorRow(FormVendorRow);
                this.TAFormVendor.Update(tbFormVendor);

                // 正式提交或草稿
                if (StatusID == SystemEnums.FormStatus.Awaiting) {

                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic["ActionType"] = FormVendorRow.ActionType;
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

                //作废之前的单据
                if (RejectedFormID != null) {
                    PurchaseDS.FormRow oldRow = TAForm.GetDataByID(RejectedFormID.GetValueOrDefault())[0];
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

        public void UpdateFormVendorApply(int FormID, SystemEnums.FormStatus StatusID, int VendorID, string VendorName, string VendorAddress, string City, string Country, string ContactName, int? VendorTypeID,
             string PhoneNumber, int OneTimeVendor, int HoldVendor, string PurchasingPostalCode, string AlphaSearchKey, string PurchasingContact, string PurchasingAddress,
            string PurchasingCity, string PurchasingPhoneNumber, int? BankCodeID, int? MethodPaymentID, int? PaymentTermID,
                 int? TransTypeID, int? VatTypeID, string BankName, string AccountNo, string BankNo, int? ACTypeID, string AttachmentFileName, string RealAttachmentFileName, string Remark, string ModifyReason) {

            SqlTransaction transaction = null;
            try {
                transaction = TableAdapterHelper.BeginTransaction(this.TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormVendor, transaction);

                PurchaseDS.FormRow formRow = this.TAForm.GetDataByID(FormID)[0];

                if (StatusID == SystemEnums.FormStatus.Awaiting) {
                    UtilityBLL utility = new UtilityBLL();
                    string formTypeString = utility.GetFormTypeString((int)formRow.FormTypeID);
                    formRow.FormNo = utility.GetFormNo(formTypeString);
                }
                formRow.StatusID = (int)StatusID;
                formRow.SubmitDate = DateTime.Now;
                formRow.LastModified = DateTime.Now;
                this.TAForm.Update(formRow);

                PurchaseDS.FormVendorRow FormVendorRow = this.TAFormVendor.GetDataByID(FormID)[0];
                if (VendorID > 0) {
                    FormVendorRow.VendorID = VendorID;
                }
                FormVendorRow.VendorName = VendorName;
                FormVendorRow.VendorAddress = VendorAddress;
                FormVendorRow.City = City;

                FormVendorRow.Postal = Country;
                FormVendorRow.ContactName = ContactName;
                if (VendorTypeID != null) {
                    FormVendorRow.VendorTypeID = VendorTypeID.GetValueOrDefault();
                }

                FormVendorRow.PhoneNumber = PhoneNumber;
                FormVendorRow.OneTimeVendor = OneTimeVendor == 1;
                FormVendorRow.HoldVendor = HoldVendor == 1;
                FormVendorRow.PurchaseingPostalCode = PurchasingPostalCode;
                FormVendorRow.AlphaSearchKey = AlphaSearchKey;
                FormVendorRow.PurchasingContact = PurchasingContact;
                FormVendorRow.PurchasingAddress = PurchasingAddress;
                FormVendorRow.PurchasingCity = PurchasingCity;
                FormVendorRow.PurchasePhoneNumber = PurchasingPhoneNumber;
                if (BankCodeID != null) {
                    FormVendorRow.BankCodeID = BankCodeID.GetValueOrDefault();
                    MasterData.BankCodeRow rowBankCode = new MasterDataBLL().GetBankCodeById(BankCodeID.GetValueOrDefault())[0];
                    FormVendorRow.BankCode = rowBankCode.BankCode + rowBankCode.Description;
                }
                if (MethodPaymentID != null) {
                    FormVendorRow.MethodPaymentID = MethodPaymentID.GetValueOrDefault();
                }
                if (PaymentTermID != null) {
                    FormVendorRow.PaymentTermID = PaymentTermID.GetValueOrDefault();
                }
                if (TransTypeID != null) {
                    FormVendorRow.TransTypeID = TransTypeID.GetValueOrDefault();
                }
                if (VatTypeID != null) {
                    FormVendorRow.VATTypeID = VatTypeID.GetValueOrDefault();
                }
                FormVendorRow.BankName = BankName;
                FormVendorRow.AccountNo = AccountNo;
                FormVendorRow.BankNo = BankNo;
                if (ACTypeID != null) {
                    FormVendorRow.ACTypeID = ACTypeID.GetValueOrDefault();
                }
                FormVendorRow.AttachmentFileName = AttachmentFileName;
                FormVendorRow.RealAttachmentFileName = RealAttachmentFileName;
                FormVendorRow.Remark = Remark;
                if (ModifyReason != null && ModifyReason != "") {
                    FormVendorRow.ModifyReason = ModifyReason;
                }
                this.TAFormVendor.Update(FormVendorRow);

                // 正式提交或草稿
                if (StatusID == SystemEnums.FormStatus.Awaiting) {

                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic["ActionType"] = FormVendorRow.ActionType;
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

        public void DeleteFormVendorApply(int FormID) {
            SqlTransaction transaction = null;
            try {
                transaction = TableAdapterHelper.BeginTransaction(TAForm);
                TableAdapterHelper.SetTransaction(this.TAFormVendor, transaction);
                this.TAFormVendor.DeleteByID(FormID);
                this.TAForm.DeleteByID(FormID);
                transaction.Commit();
            } catch (Exception) {
                throw new ApplicationException();
            }
        }

        public string QueryProcessingFormVendorNoByVendorID(int VendorID) {
            string result = this.TAFormVendor.QueryProcessingFormVendorNoByVendorID(VendorID);
            if (string.IsNullOrEmpty(result)) {
                result = "";
            }
            return result;
        }

        public void UpdateFormVendorApply(int FormID, int VendorTypeID) {
            PurchaseDS.FormVendorRow rowVendor = this.GetFormVendorByID(FormID)[0];
            rowVendor.VendorTypeID = VendorTypeID;
            this.TAFormVendor.Update(rowVendor);
        }

        #endregion

    }
}
