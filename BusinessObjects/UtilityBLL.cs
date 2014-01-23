using System;
using System.Collections.Generic;
using System.Text;
using BusinessObjects.FormDSTableAdapters;
using lib.wf;

namespace BusinessObjects {
    public class UtilityBLL {
        public UtilityBLL() {
        }

        private FormNoTableAdapter m_FormNoAdapter;
        public FormNoTableAdapter TAFormNo {
            get {
                if (m_FormNoAdapter == null) {
                    m_FormNoAdapter = new FormNoTableAdapter();
                }
                return m_FormNoAdapter;
            }
        }

        public string GetFormTypeString(int FormTypeID) {
            string returnStr = string.Empty;
            switch (FormTypeID) {
                case (int)SystemEnums.FormType.TravelReimburseApply:
                case (int)SystemEnums.FormType.FormInvitationApply:
                case (int)SystemEnums.FormType.FormInvitationReimburse:
                case (int)SystemEnums.FormType.PersonalReimburseApply:
                    returnStr = "TE";
                    break;
                case (int)SystemEnums.FormType.SaleApply:
                    returnStr = "MA";
                    break;
                case (int)SystemEnums.FormType.SaleSettlement:
                    returnStr = "SA";
                    break;
                case (int)SystemEnums.FormType.SaleAdvancedPayment:
                    returnStr = "AP";
                    break;
                case (int)SystemEnums.FormType.SalePayment:
                    returnStr = "PA";
                    break;
                case (int)SystemEnums.FormType.VendorApply:
                    returnStr = "VDC";
                    break;
                case (int)SystemEnums.FormType.RDApply:
                    returnStr = "RDAA";
                    break;
                case (int)SystemEnums.FormType.RDPayment:
                    returnStr = "PD";
                    break;
                case (int)SystemEnums.FormType.PRApply:
                    returnStr = "PR";
                    break;
                case (int)SystemEnums.FormType.POApply:
                    returnStr = "PO";
                    break;
                case (int)SystemEnums.FormType.PVApply:
                    returnStr = "PV";
                    break;
                case (int)SystemEnums.FormType.FormMarketingApply:
                    returnStr = "MAA";
                    break;
                case (int)SystemEnums.FormType.FormMarketingPayment:
                    returnStr = "PM";
                    break;
            }
            return returnStr;
        }

        public string GetFormNo(string FormType) {
            DateTime? YearAndMonth = DateTime.Now;
            int? SequenceNo = new int();
            this.TAFormNo.GenerateFormNo(FormType, ref YearAndMonth, ref SequenceNo);
            if (YearAndMonth.HasValue && SequenceNo.HasValue)
                return FormType + YearAndMonth.Value.ToString("yyMM") + SequenceNo.Value.ToString().PadLeft(4, '0');
            else
                return null;
        }

    }
}
