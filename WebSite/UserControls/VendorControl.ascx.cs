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

public delegate void VendorNameTextChanged(object sender, EventArgs e);
public partial class UserControls_VendorControl : System.Web.UI.UserControl {

    public event VendorNameTextChanged VendorNameTextChanged;
    protected override void OnPreRender(EventArgs e) {
        base.OnPreRender(e);
        if (IsReadOnly) {
            this.txtDisplayVendorName.Text = this.VendorName;
        }

        this.txtDisplayVendorName.ReadOnly = this.IsReadOnly;
        this.txtVendorName.Style["display"] = "none";
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
            return this.txtDisplayVendorName.CssClass;
        }
        set {
            this.txtDisplayVendorName.CssClass = value;
        }
    }

    public Unit Height {
        get {
            return this.txtDisplayVendorName.Height;
        }
        set {
            this.txtDisplayVendorName.Height = value;
        }
    }

    public Unit Width {
        get {
            return this.txtDisplayVendorName.Width;
        }
        set {
            this.txtDisplayVendorName.Width = value;
        }
    }

    public bool AutoPostBack {
        get {
            return this.txtVendorName.AutoPostBack;
        }
        set {
            this.txtVendorName.AutoPostBack = value;
        }
    }

    public string VendorName {
        get {
            return this.txtVendorName.Text.Trim();
        }
        set {
            this.txtVendorName.Text = value;
        }
    }

    private string _isNoClear = "";

    public string IsNoClear {
        get {
            return _isNoClear;
        }
        set {
            this._isNoClear = value.ToString().Equals("true", StringComparison.CurrentCultureIgnoreCase) ? "none" : "inline";
        }
    }

    public string VendorID {
        get {
            return this.VendorIDCtl.Value.ToString();
        }
        set {
            if ((!(value == string.Empty) && (!object.ReferenceEquals(value, DBNull.Value)))) {
                this.VendorIDCtl.Value = value;
                MasterData.VendorRow vendor = new MasterDataBLL().GetVendorByID(int.Parse(value));
                this.VendorNameCtl.Value = vendor.VendorName;
                this.txtVendorName.Text = vendor.VendorName;


            } else {
                this.VendorIDCtl.Value = "";
                this.VendorNameCtl.Value = "";
                this.txtVendorName.Text = "";


            }
        }
    }

    private bool _IsLimited;
    public bool IsLimited {
        get { return _IsLimited; }
        set { _IsLimited = value; }
    }

    #endregion


    #region script

    protected string GetShowDlgScript() {
        StringBuilder script = new StringBuilder();
        string strWebSiteUrl = System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"];
        string url = strWebSiteUrl + @"/Dialog/VendorSearch.aspx";
        if (IsLimited) {
            url += "?IsLimited=true";
        }
        string getDisplayName = string.Empty;
        if (!AutoPostBack) {
            getDisplayName = @"document.getElementById('" + this.txtDisplayVendorName.ClientID + @"').value = DialogValue[1];";
        }
        script.Append(@"var url = '" + url + @"';                        
                        var DialogValue = window.showModalDialog(url,window,'dialogHeight: 652px; dialogWidth: 880px; edge: Raised; center: Yes; help: No; resizable: No; status: No;');
                        if (DialogValue != null) {
                            document.getElementById('" + this.VendorIDCtl.ClientID + @"').value = DialogValue[0];
                            document.getElementById('" + this.VendorNameCtl.ClientID + @"').value = DialogValue[1];" + getDisplayName +
                            @"var VendorNameCtl = document.getElementById('" + this.txtVendorName.ClientID + @"');
                            VendorNameCtl.value = DialogValue[1];
                            if (VendorNameCtl.onchange) {
                                document.getElementById('" + this.txtVendorName.ClientID + @"').onchange();
                            }
                        }");
        return script.ToString();
    }

    protected string GetResetScript() {
        StringBuilder script = new StringBuilder();
        string getDisplayName = string.Empty;
        if (!AutoPostBack) {
            getDisplayName = @"document.getElementById('" + this.txtDisplayVendorName.ClientID + @"').value = '';";
        }
        script.Append(@"document.getElementById('" + this.VendorIDCtl.ClientID + @"').value = '';" + getDisplayName +
                        @"document.getElementById('" + this.VendorNameCtl.ClientID + @"').value = '';
                        var VendorNameCtl = document.getElementById('" + this.txtVendorName.ClientID + @"');
                        VendorNameCtl.value = '';
                        if (VendorNameCtl.onchange) {
                            document.getElementById('" + this.txtVendorName.ClientID + @"').onchange();
                        }");
        return script.ToString();
    }
    #endregion

    protected void txtVendorName_TextChanged(object sender, EventArgs e) {

        if (VendorNameTextChanged != null) {
            VendorNameTextChanged(sender, e);
        }
    }
}