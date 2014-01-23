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

public delegate void ItemNameTextChanged(object sender, EventArgs e);
public partial class UserControls_ItemControl : System.Web.UI.UserControl {
    public event ItemNameTextChanged ItemNameTextChanged;
    protected override void OnPreRender(EventArgs e) {
        base.OnPreRender(e);
        if (IsReadOnly) {
            this.txtDisplayItemName.Text = this.txtItemName.Text;
        }

        this.txtDisplayItemName.ReadOnly = this.IsReadOnly;
        this.txtItemName.Style["display"] = "none";
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
            return this.txtDisplayItemName.CssClass;
        }
        set {
            this.txtDisplayItemName.CssClass = value;
        }
    }


    public Unit Height {
        get {
            return this.txtDisplayItemName.Height;
        }
        set {
            this.txtDisplayItemName.Height = value;
        }
    }

    public Unit Width {
        get {
            return this.txtDisplayItemName.Width;
        }
        set {
            this.txtDisplayItemName.Width = value;
        }
    }

    public bool AutoPostBack {
        get {
            return this.txtItemName.AutoPostBack;
        }
        set {
            this.txtItemName.AutoPostBack = value;
        }
    }

    public string ItemName {
        get {
            return this.txtDisplayItemName.Text.Trim();
        }
        set {
            this.txtDisplayItemName.Text = value;
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

    public string ItemID {
        get {
            return this.ItemIDCtl.Value.ToString();
        }
        set {
            if ((!(value == string.Empty) && (!object.ReferenceEquals(value, DBNull.Value)))) {
                this.ItemIDCtl.Value = value;
                MasterData.ItemRow item = new BusinessObjects.MasterDataTableAdapters.ItemTableAdapter().GetDataByID(int.Parse(value))[0];
                this.ItemNameCtl.Value = item.ItemName;
                this.txtItemName.Text = item.ItemName;
            } else {
                this.ItemIDCtl.Value = "";
                this.ItemNameCtl.Value = "";
                this.txtItemName.Text = "";
            }
        }
    }

    private string _ItemCategoryID = "0";
    public string ItemCategoryID {
        get {
            return _ItemCategoryID;
        }
        set {
            if (!string.IsNullOrEmpty(value)) {
                this._ItemCategoryID = value;
            } else {
                _ItemCategoryID = "0";
            }
        }
    }


    #endregion


    #region script

    protected string GetShowDlgScript() {
        StringBuilder script = new StringBuilder();
        string strWebSiteUrl = System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"];
        string url = strWebSiteUrl + @"/Dialog/ItemSearch.aspx?ItemCategoryID=" + this.ItemCategoryID;
        string getDisplayName = string.Empty;
        if (!AutoPostBack) {
            getDisplayName = @"document.getElementById('" + this.txtDisplayItemName.ClientID + @"').value = DialogValue[1];";
        }
        script.Append(@"var url = '" + url + @"';                        
                        var DialogValue = window.showModalDialog(url,window,'dialogHeight: 652px; dialogWidth: 880px; edge: Raised; center: Yes; help: No; resizable: No; status: No;');
                        if (DialogValue != null) {
                            document.getElementById('" + this.ItemIDCtl.ClientID + @"').value = DialogValue[0];
                            document.getElementById('" + this.ItemNameCtl.ClientID + @"').value = DialogValue[1];" + getDisplayName +
                            @"var ItemNameCtl = document.getElementById('" + this.txtItemName.ClientID + @"');
                            ItemNameCtl.value = DialogValue[1];
                            if (ItemNameCtl.onchange) {
                                document.getElementById('" + this.txtItemName.ClientID + @"').onchange();
                            }
                        }");
        return script.ToString();
    }

    protected string GetResetScript() {
        StringBuilder script = new StringBuilder();
        string getDisplayName = string.Empty;
        if (!AutoPostBack) {
            getDisplayName = @"document.getElementById('" + this.txtDisplayItemName.ClientID + @"').value = '';";
        }
        script.Append(@"document.getElementById('" + this.ItemIDCtl.ClientID + @"').value = '';" + getDisplayName +
                        @"document.getElementById('" + this.ItemNameCtl.ClientID + @"').value = '';
                        var ItemNameCtl = document.getElementById('" + this.txtItemName.ClientID + @"');
                        ItemNameCtl.value = '';
                        if (ItemNameCtl.onchange) {
                            document.getElementById('" + this.txtItemName.ClientID + @"').onchange();
                        }");
        return script.ToString();
    }
    #endregion

    protected void txtItemName_TextChanged(object sender, EventArgs e) {

        if (ItemNameTextChanged != null) {
            ItemNameTextChanged(sender, e);
        }
    }
}