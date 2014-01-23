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


public delegate void ItemCategoryNameTextChanged(object sender, EventArgs e);
public partial class UserControls_ItemCategoryControl : System.Web.UI.UserControl
{
    public event ItemCategoryNameTextChanged ItemCategoryNameTextChanged;
    protected override void OnPreRender(EventArgs e) {
        base.OnPreRender(e);
        if (IsReadOnly) {
            this.txtDisplayItemCategoryName.Text = this.txtItemCategoryName.Text;
        }

        this.txtDisplayItemCategoryName.ReadOnly = this.IsReadOnly;
        this.txtItemCategoryName.Style["display"] = "none";
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
            return this.txtDisplayItemCategoryName.CssClass;
        }
        set {
            this.txtDisplayItemCategoryName.CssClass = value;
        }
    }


    public Unit Height {
        get {
            return this.txtDisplayItemCategoryName.Height;
        }
        set {
            this.txtDisplayItemCategoryName.Height = value;
        }
    }

    public Unit Width {
        get {
            return this.txtDisplayItemCategoryName.Width;
        }
        set {
            this.txtDisplayItemCategoryName.Width = value;
        }
    }

    public bool AutoPostBack {
        get {
            return this.txtItemCategoryName.AutoPostBack;
        }
        set {
            this.txtItemCategoryName.AutoPostBack = value;
        }
    }

    public string ItemCategoryName {
        get {
            return this.txtDisplayItemCategoryName.Text.Trim();
        }
        set {
            this.txtDisplayItemCategoryName.Text = value;
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

    public string ItemCategoryID {
        get {
            return this.ItemCategoryIDCtl.Value.ToString();
        }
        set {
            if ((!(value == string.Empty) && (!object.ReferenceEquals(value, DBNull.Value)))) {
                this.ItemCategoryIDCtl.Value = value;
                MasterData.ItemCategoryRow ItemCategory = new MasterDataBLL().GetItemCategoryById(int.Parse(value));
                this.ItemCategoryNameCtl.Value = ItemCategory.ItemCategoryName;
                this.txtItemCategoryName.Text = ItemCategory.ItemCategoryName;
            } else {
                this.ItemCategoryIDCtl.Value = "";
                this.ItemCategoryNameCtl.Value = "";
                this.txtItemCategoryName.Text = "";
            }
        }
    }


    #endregion


    #region script

    protected string GetShowDlgScript() {
        StringBuilder script = new StringBuilder();
        string strWebSiteUrl = System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"];
        string url = strWebSiteUrl + @"/Dialog/ItemCategorySearch.aspx";
        string getDisplayName = string.Empty;
        if (!AutoPostBack) {
            getDisplayName = @"document.getElementById('" + this.txtDisplayItemCategoryName.ClientID + @"').value = DialogValue[1];";
        }
        script.Append(@"var url = '" + url + @"';                        
                        var DialogValue = window.showModalDialog(url,window,'dialogHeight: 652px; dialogWidth: 880px; edge: Raised; center: Yes; help: No; resizable: No; status: No;');
                        if (DialogValue != null) {
                            document.getElementById('" + this.ItemCategoryIDCtl.ClientID + @"').value = DialogValue[0];
                            document.getElementById('" + this.ItemCategoryNameCtl.ClientID + @"').value = DialogValue[1];" + getDisplayName +
                            @"var ItemCategoryNameCtl = document.getElementById('" + this.txtItemCategoryName.ClientID + @"');
                            ItemCategoryNameCtl.value = DialogValue[1];
                            if (ItemCategoryNameCtl.onchange) {
                                document.getElementById('" + this.txtItemCategoryName.ClientID + @"').onchange();
                            }
                        }");
        return script.ToString();
    }

    protected string GetResetScript() {
        StringBuilder script = new StringBuilder();
        string getDisplayName = string.Empty;
        if (!AutoPostBack) {
            getDisplayName = @"document.getElementById('" + this.txtDisplayItemCategoryName.ClientID + @"').value = '';";
        }
        script.Append(@"document.getElementById('" + this.ItemCategoryIDCtl.ClientID + @"').value = '';" + getDisplayName +
                        @"document.getElementById('" + this.ItemCategoryNameCtl.ClientID + @"').value = '';
                        var ItemCategoryNameCtl = document.getElementById('" + this.txtItemCategoryName.ClientID + @"');
                        ItemCategoryNameCtl.value = '';
                        if (ItemCategoryNameCtl.onchange) {
                            document.getElementById('" + this.txtItemCategoryName.ClientID + @"').onchange();
                        }");
        return script.ToString();
    }
    #endregion

    protected void txtItemCategoryName_TextChanged(object sender, EventArgs e) {

        if (ItemCategoryNameTextChanged != null) {
            ItemCategoryNameTextChanged(sender, e);
        }
    }
}