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
using BusinessObjects;
using BusinessObjects.AuthorizationDSTableAdapters;

public partial class AuthorizationManage_MyInfoManage : BasePage {
    protected void Page_Load(object sender, EventArgs e) {
        base.Page_Load(sender, e);
        if (!this.IsPostBack) {
            String title = this.GetLocalResourceObject("titleLabel.Text").ToString();
            PageUtility.SetContentTitle(this, title);
            this.Page.Title = title;

            AuthorizationDS.StuffUserRow stuffUser = (AuthorizationDS.StuffUserRow)Session["StuffUser"];
            this.StuffNameCtl.Text = stuffUser.StuffName;
            this.UserNameCtl.Text = stuffUser.UserName;
            this.StuffIdCtl.Text = stuffUser.StuffId;
            this.LastSetPasswordTimeCtl.Text = stuffUser.LastSetPasswordTime.ToShortDateString();
            this.EmailCtl.Text = stuffUser.EMail;
            if (!stuffUser.IsTelephoneNull()) this.TelphoneNoCtl.Text = stuffUser.Telephone;
            this.PasswordCtl.Text = "!!!!!!";
            this.RepeatPasswordCtl.Text = "!!!!!!";
            
        }
    }
    protected void SaveBtn_Click(object sender, EventArgs e) {
        if (!this.PasswordCtl.Text.Equals(this.RepeatPasswordCtl.Text)) {
            PageUtility.ShowModelDlg(this, "����������������������");
            return;
        }
        if (this.PasswordCtl.Text.Length < 6) {
            PageUtility.ShowModelDlg(this, "���볤��Ӧ���ڵ���6");
            return;
        }
        AuthorizationDS.StuffUserRow stuffUser = (AuthorizationDS.StuffUserRow)Session["StuffUser"];
        if (!this.PasswordCtl.Text.Equals("!!!!!!")) {
            string newPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(this.PasswordCtl.Text.Trim(), "MD5");
            stuffUser.UserPassword = newPassword;
            stuffUser.LastSetPasswordTime = DateTime.Now;
        }
        stuffUser.EMail = this.EmailCtl.Text;
        stuffUser.Telephone = this.TelphoneNoCtl.Text;
        
        new StuffUserTableAdapter().Update(stuffUser);


        this.LastSetPasswordTimeCtl.Text = stuffUser.LastSetPasswordTime.ToShortDateString();
        this.PasswordCtl.Text = "!!!!!!";
        this.RepeatPasswordCtl.Text = "!!!!!!";
        //this.Response.Redirect("~/Home.aspx");

        PageUtility.ShowModelDlg(this, "���óɹ�");
    }

    protected void CancelBtn_Click(object sender, EventArgs e) {
        AuthorizationDS.StuffUserRow stuffUser = (AuthorizationDS.StuffUserRow)Session["StuffUser"];        
        this.EmailCtl.Text = stuffUser.EMail;
        if (!stuffUser.IsTelephoneNull()) this.TelphoneNoCtl.Text = stuffUser.Telephone;
        this.Response.Redirect("~/Home.aspx");
    }

    protected override void OnPreRender(EventArgs e) {
        base.OnPreRender(e);
        this.PasswordCtl.Attributes["value"] = this.PasswordCtl.Text;
        this.RepeatPasswordCtl.Attributes["value"] = this.RepeatPasswordCtl.Text;
    }
}
