using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lib.wf {

    public class APResult {
        public APResult() { }
        private string _ProcID;
        private string _InTurnUserIds;
        private string _InTurnPositionIds;
        private int _StatusID;
        private DateTime? _ApprovedDate;
        private string _LastApprover;
        private string _ApproverIds;
        private bool _IsSuccess;
        private string _ErrorMsg;

        public string ProcID {
            get { return _ProcID; }
            set { _ProcID = value; }
        }

        public string InTurnUserIds {
            get { return _InTurnUserIds; }
            set { _InTurnUserIds = value; }
        }

        public string InTurnPositionIds {
            get { return _InTurnPositionIds; }
            set { _InTurnPositionIds = value; }
        }

        public int StatusID {
            get { return _StatusID; }
            set { _StatusID = value; }
        }

        public DateTime? ApprovedDate {
            get { return _ApprovedDate; }
            set { _ApprovedDate = value; }
        }

        public string LastApprover {
            get { return _LastApprover; }
            set { _LastApprover = value; }
        }

        public string ApproverIds {
            get { return _ApproverIds; }
            set { _ApproverIds = value; }
        }

        public bool IsSuccess {
            get { return _IsSuccess; }
            set { _IsSuccess = value; }
        }

        public string ErrorMsg {
            get { return _ErrorMsg; }
            set { _ErrorMsg = value; }
        }
    }

    public class APParameter {
        public APParameter() { }
        public APParameter(int FormID, String FormNo, int OrganizationUnitID, int UserID, int StatusID, int FormTypeID,int PositionID, Dictionary<string, Object> Dic) {
            this.FormID = FormID;
            this.FormNo = FormNo;
            this.OrganizationUnitID = OrganizationUnitID;
            this.UserID = UserID;
            this.StatusID = StatusID;
            this.FormTypeID = FormTypeID;
            this.PositionID = PositionID;
            this.Dic = Dic;
        }

        private int _FormID;
        private string _FormNo;
        private int _OrganizationUnitID;
        private int _UserID;
        private int _StatusID;
        private int _FormTypeID;
        private int _PositionID;
        private Dictionary<string, object> _Dic;
        public int FormID {
            get { return _FormID; }
            set { _FormID = value; }
        }

        public string FormNo {
            get { return _FormNo; }
            set { _FormNo = value; }
        }

        public int UserID {
            get { return _UserID; }
            set { _UserID = value; }
        }

        public int OrganizationUnitID {
            get { return _OrganizationUnitID; }
            set { _OrganizationUnitID = value; }
        }

        public int StatusID {
            get { return _StatusID; }
            set { _StatusID = value; }
        }

        public int FormTypeID {
            get { return _FormTypeID; }
            set { _FormTypeID = value; }
        }

        public int PositionID {
            get { return _PositionID; }
            set { _PositionID = value; }
        }

        public Dictionary<string, object> Dic {
            get { return _Dic; }
            set { _Dic = value; }
        }
    }
}
