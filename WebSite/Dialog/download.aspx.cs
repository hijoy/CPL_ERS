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
using System.IO;
using System.Text;

public partial class Dialog_download : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        if (!IsPostBack)
        {
            Download();
        }
    }

    private void Download()
    {
        try
        {
            string fileName = Request.QueryString["FileName"];

            string pathName = CommonUtility.GetPathName();
            string path = Server.MapPath(@"~/" + pathName); // this.Request.ApplicationPath + @"\" + pathName;
            string serverFileFullName = path + @"\" + fileName;

            if (System.IO.File.Exists(serverFileFullName))
            {
                System.IO.FileInfo fileInfo = new FileInfo(serverFileFullName);
                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(fileInfo.Name.ToString()));
                //Response.AddHeader("Content-Disposition", "attachment; filename=" + fileInfo.Name);
                Response.AddHeader("Content-Length", fileInfo.Length.ToString());
                Response.ContentType = "application/octet-stream";
                Response.ContentEncoding = System.Text.Encoding.Default;
                Response.WriteFile(serverFileFullName);

                string scriptContent = "<script>window.close();</script>";
                this.RegisterStartupScript("closePageByWebUIUtility", scriptContent);
            }
            else
            {
                throw (new Exception("文件不存在！"));
            }
        }
        catch (Exception ex)
        {
            PageUtility.ShowModelDlg(this, ex.Message.ToString());
        }
    }
}
