using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObjects;

public partial class UserControls_PrintReport : System.Web.UI.UserControl {

    #region Property

    private int _FormID = 0;
    public int FormID {
        get {
            return _FormID;
        }
        set {
            _FormID = value;
        }
    }

    private string _Text = "";
    public string Text {
        get {
            return _Text;
        }
        set {
            _Text = value;
        }
    }

    #endregion

    protected override void OnPreRender(EventArgs e) {
        base.OnPreRender(e);
        if (!IsPostBack) {
            QueryDS.FormViewRow rowForm = new FormQueryBLL().GetFormViewByID(FormID);
            if (rowForm == null || rowForm.StatusID == (int)SystemEnums.FormStatus.Scrap || rowForm.StatusID == (int)SystemEnums.FormStatus.Rejected) {
                this.btnPrint.Visible = false;
                return;
            }
            this.btnPrint.Visible = true;
            if (!string.IsNullOrEmpty(Text)) {
                this.btnPrint.Text = Text;
            } else {
                this.btnPrint.Text = Resources.Common.Form_Print;
            }
            string Param = "'height=600,width=800,toolbar=no,menubar=no,scrollbars=Yes, resizable=no,location=no, status=no,location=no'";
            this.btnPrint.OnClientClick = "window.open('" + CommonUtility.GetReportPostBackUrl(FormID, rowForm.PageType) + "','PrintReport'," + Param + ");return false;";
        }
    }
}