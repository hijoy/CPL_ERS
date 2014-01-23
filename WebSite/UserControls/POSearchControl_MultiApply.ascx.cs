using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Diagnostics;
using BusinessObjects;

public delegate void FormNoTextChanged_MultiApply(object sender, EventArgs e);
public partial class UserControls_POSearchControl_MultiApply : System.Web.UI.UserControl {
    public event FormNoTextChanged_MultiApply FormNoTextChanged;
    protected override void OnPreRender(EventArgs e) {
        base.OnPreRender(e);
        if (IsReadOnly) {
            this.txtDisplayFormNo.Text = this.FormNo;
        }
        this.txtDisplayFormNo.ReadOnly = this.IsReadOnly;
        this.txtFormNo.Style["display"] = "none";
    }

    #region property

    private bool _isReadOnly = true;
    public bool IsReadOnly {
        get {
            return _isReadOnly;
        }
        set {
            this._isReadOnly = value;
        }
    }

    private string _isVisible = "";
    public string IsVisible {
        get {
            return _isVisible;
        }
        set {
            this._isVisible = value;
        }
    }

    public string CssClass {
        get {
            return this.txtDisplayFormNo.CssClass;
        }
        set {
            this.txtDisplayFormNo.CssClass = value;
        }
    }

    public Unit Height {
        get {
            return this.txtDisplayFormNo.Height;
        }
        set {
            this.txtDisplayFormNo.Height = value;
        }
    }

    public Unit Width {
        get {
            return this.txtDisplayFormNo.Width;
        }
        set {
            this.txtDisplayFormNo.Width = value;
        }
    }

    public bool AutoPostBack {
        get {
            return this.txtFormNo.AutoPostBack;
        }
        set {
            this.txtFormNo.AutoPostBack = value;
        }
    }

    public string FormNo {
        get {
            return this.txtFormNo.Text.Trim();
        }
        set {
            this.txtFormNo.Text = value;
        }
    }

    private string _isNoClear = "inline";
    public string IsNoClear {
        get {
            return _isNoClear;
        }
        set {
            this._isNoClear = value.ToString().Equals("true", StringComparison.CurrentCultureIgnoreCase) ? "none" : "inline";
        }
    }

    public string FormID {
        get {
            return this.FormIDCtl.Value.ToString();
        }
        set {
            if ((!(value == string.Empty) && (!object.ReferenceEquals(value, DBNull.Value)))) {
                this.FormIDCtl.Value = value;
                QueryDS.FormViewRow FormViewRow = new FormQueryBLL().GetFormViewByID(int.Parse(value));
                this.FormNoCtl.Value = FormViewRow.FormNo;
                this.txtFormNo.Text = FormViewRow.FormNo;

            } else {
                this.FormIDCtl.Value = "";
                this.FormNoCtl.Value = "";
                this.txtFormNo.Text = "";
            }
        }
    }

    private int _SettlementID;
    public int SettlementID {
        get {
            return _SettlementID;
        }
        set {
            _SettlementID = value;
        }
    }

    #endregion


    #region script

    protected string GetShowDlgScript() {
        StringBuilder script = new StringBuilder();
        string strWebSiteUrl = System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"];
        string url = strWebSiteUrl + @"/Dialog/POSearch_MultiApply.aspx?SettlementID=" + SettlementID;
        string getDisplayName = string.Empty;
        if (!AutoPostBack) {
            getDisplayName = @"document.getElementById('" + this.txtDisplayFormNo.ClientID + @"').value = DialogValue[1];";
        }
        script.Append(@"var url = '" + url + @"';                        
                        var DialogValue = window.showModalDialog(url,window,'dialogHeight: 452px; dialogWidth: 880px; edge: Raised; center: Yes; help: No; resizable: No; status: No;');
                        if (DialogValue != null) {
                            document.getElementById('" + this.FormIDCtl.ClientID + @"').value = DialogValue[0];
                            document.getElementById('" + this.FormNoCtl.ClientID + @"').value = DialogValue[1];" + getDisplayName +
                            @"var FormNoCtl = document.getElementById('" + this.txtFormNo.ClientID + @"');
                            FormNoCtl.value = DialogValue[1];
                            if (FormNoCtl.onchange) {
                                document.getElementById('" + this.txtFormNo.ClientID + @"').onchange();
                            }
                        }");
        return script.ToString();
    }

    protected string GetResetScript() {
        StringBuilder script = new StringBuilder();
        string getDisplayName = string.Empty;
        if (!AutoPostBack) {
            getDisplayName = @"document.getElementById('" + this.txtDisplayFormNo.ClientID + @"').value = '';";
        }
        script.Append(@"document.getElementById('" + this.FormIDCtl.ClientID + @"').value = '';" + getDisplayName +
                        @"document.getElementById('" + this.FormNoCtl.ClientID + @"').value = '';
                        var FormNoCtl = document.getElementById('" + this.txtFormNo.ClientID + @"');
                        FormNoCtl.value = '';
                        if (FormNoCtl.onchange) {
                            document.getElementById('" + this.txtFormNo.ClientID + @"').onchange();
                        }");
        return script.ToString();
    }
    #endregion

    protected void txtFormNo_TextChanged(object sender, EventArgs e) {

        if (FormNoTextChanged != null) {
            FormNoTextChanged(sender, e);
        }
    }
}