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

public partial class ErrorPage_NoRightErrorPage : Page {
    protected void Page_Load(object sender, EventArgs e) {
        if (!this.IsPostBack) {
            String title = this.GetLocalResourceObject("titleLabel.Text").ToString();
            PageUtility.SetContentTitle(this, title);

            string errorMessage = Request.Params["errorMessage"];
            if (errorMessage != null && errorMessage.Length > 0) {
                this.lblError.Text = Request.Params["errorMessage"];
            }
        }
    }
}
