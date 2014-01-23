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

public delegate void VendorTypeNameTextChanged(object sender, EventArgs e);
public partial class UserControls_VendorTypeControl : System.Web.UI.UserControl {
    public event VendorTypeNameTextChanged VendorTypeNameTextChanged;
    protected override void OnPreRender(EventArgs e) {
        base.OnPreRender(e);
        if (IsReadOnly) {
            this.txtDisplayVendorTypeName.Text = this.VendorTypeName;
        }
        this.txtDisplayVendorTypeName.ReadOnly = this.IsReadOnly;
        this.txtVendorTypeName.Style["display"] = "none";
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
            return this.txtDisplayVendorTypeName.CssClass;
        }
        set {
            this.txtDisplayVendorTypeName.CssClass = value;
        }
    }

    public Unit Height {
        get {
            return this.txtDisplayVendorTypeName.Height;
        }
        set {
            this.txtDisplayVendorTypeName.Height = value;
        }
    }

    public Unit Width {
        get {
            return this.txtDisplayVendorTypeName.Width;
        }
        set {
            this.txtDisplayVendorTypeName.Width = value;
        }
    }

    public bool AutoPostBack {
        get {
            return this.txtVendorTypeName.AutoPostBack;
        }
        set {
            this.txtVendorTypeName.AutoPostBack = value;
        }
    }

    public string VendorTypeName {
        get {
            return this.txtVendorTypeName.Text.Trim();
        }
        set {
            this.txtVendorTypeName.Text = value;
        }
    }

    private string _isNoClear = "inline";
    public string IsNoClear {
        get {
            return _isNoClear;
        }
        set {
            if (!IsVisible.Equals("true",StringComparison.CurrentCultureIgnoreCase)) {
                this._isNoClear = "none";
            }
            this._isNoClear = value.ToString().Equals("true", StringComparison.CurrentCultureIgnoreCase) ? "none" : "inline";
        }
    }

    public string VendorTypeID {
        get {
            return this.VendorTypeIDCtl.Value.ToString();
        }
        set {
            if ((!(value == string.Empty) && (!object.ReferenceEquals(value, DBNull.Value)))) {
                this.VendorTypeIDCtl.Value = value;
                MasterData.VendorTypeRow vendorType = new MasterDataBLL().GetVendorTypeById(int.Parse(value));
                this.VendorTypeNameCtl.Value = vendorType.VendorTypeName;
                this.txtVendorTypeName.Text = vendorType.VendorTypeName;

            } else {
                this.VendorTypeIDCtl.Value = "";
                this.VendorTypeNameCtl.Value = "";
                this.txtVendorTypeName.Text = "";
            }
        }
    }

    #endregion


    #region script

    protected string GetShowDlgScript() {
        StringBuilder script = new StringBuilder();
        string strWebSiteUrl = System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"];
        string url = strWebSiteUrl + @"/Dialog/VendorTypeSearch.aspx";
        string getDisplayName = string.Empty;
        if (!AutoPostBack) {
            getDisplayName = @"document.getElementById('" + this.txtDisplayVendorTypeName.ClientID + @"').value = DialogValue[1];";
        }
        script.Append(@"var url = '" + url + @"';                        
                        var DialogValue = window.showModalDialog(url,window,'dialogHeight: 652px; dialogWidth: 880px; edge: Raised; center: Yes; help: No; resizable: No; status: No;');
                        if (DialogValue != null) {
                            document.getElementById('" + this.VendorTypeIDCtl.ClientID + @"').value = DialogValue[0];
                            document.getElementById('" + this.VendorTypeNameCtl.ClientID + @"').value = DialogValue[1];" + getDisplayName +
                            @"var VendorTypeNameCtl = document.getElementById('" + this.txtVendorTypeName.ClientID + @"');
                            VendorTypeNameCtl.value = DialogValue[1];
                            if (VendorTypeNameCtl.onchange) {
                                document.getElementById('" + this.txtVendorTypeName.ClientID + @"').onchange();
                            }
                        }");
        return script.ToString();
    }

    protected string GetResetScript() {
        StringBuilder script = new StringBuilder();
        string getDisplayName = string.Empty;
        if (!AutoPostBack) {
            getDisplayName = @"document.getElementById('" + this.txtDisplayVendorTypeName.ClientID + @"').value = '';";
        }
        script.Append(@"document.getElementById('" + this.VendorTypeIDCtl.ClientID + @"').value = '';" + getDisplayName +
                        @"document.getElementById('" + this.VendorTypeNameCtl.ClientID + @"').value = '';
                        var VendorTypeNameCtl = document.getElementById('" + this.txtVendorTypeName.ClientID + @"');
                        VendorTypeNameCtl.value = '';
                        if (VendorTypeNameCtl.onchange) {
                            document.getElementById('" + this.txtVendorTypeName.ClientID + @"').onchange();
                        }");
        return script.ToString();
    }
    #endregion

    protected void txtVendorTypeName_TextChanged(object sender, EventArgs e) {

        if (VendorTypeNameTextChanged != null) {
            VendorTypeNameTextChanged(sender, e);
        }
    }
}