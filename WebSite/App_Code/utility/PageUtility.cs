using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Diagnostics;
using System.Data.SqlClient;
using BusinessObjects;
using System.Threading;

/// <summary>
/// Summary description for PageUtility
/// </summary>
public class PageUtility {
    public PageUtility() {
        
    }

    public static void ShowModelDlg(Page page, string[] msgs) {
        UpdatePanel upModelDlg = (UpdatePanel)page.Master.FindControl("ModelDlgUpdatePanel");
        upModelDlg.Visible = true;
        Literal messsageLiteral = (Literal)page.Master.FindControl("ModelDlgContentLiteral");
        StringBuilder sb = new StringBuilder();
        sb.Append("<ul>");
        foreach (string msg in msgs) {
            sb.Append("<li>");
            sb.Append(page.Server.HtmlEncode(msg));
            sb.Append("</li>");
        }
        sb.Append("</ul>");
        messsageLiteral.Text = sb.ToString();
        HtmlControl panel = (HtmlControl)page.Master.FindControl("ModelDlg");
        panel.Style["display"] = "block";
        //panel.Visible = true;
        UpdatePanel dlgUpdatePanel = (UpdatePanel)page.Master.FindControl("ModelDlgUpdatePanel");
        dlgUpdatePanel.Update();
    }

    public static void ShowModelDlg(Page page, string msg) {
        ShowModelDlg(page, new string[] { msg });
    }

    public static void ShowModelDlg(Page page, string chineseMsg, string englishMsg) {
        if (page.UICulture.Contains("English")) {
            ShowModelDlg(page, new string[] { englishMsg });
        } else {
            ShowModelDlg(page, new string[] { chineseMsg });
        }
    }

    public static string TransferLanguage(Page page,string chineseMsg, string englishMsg) {
        if (page.UICulture.Contains("English")) {
            return englishMsg ;
        } else {
            return chineseMsg;
        }

    }

    public static void SetContentTitle(Page page, string title) {
        Label titleLabel = (Label)page.Master.FindControl("ContentTitleLabel");
        titleLabel.Text = Resources.Common.Button_Location + title;
    }

    public static void SetContentTitle(Page page, string chineseTitle,string englishTitle) {
        Label titleLabel = (Label)page.Master.FindControl("ContentTitleLabel");
        if (page.UICulture.Contains("English")) {
            titleLabel.Text = Resources.Common.Button_Location + englishTitle;
        } else {
            titleLabel.Text = Resources.Common.Button_Location + chineseTitle;
        }
    }

    public static void SelectTreeNodeByNodeValue(TreeView treeView, string nodeValue) {
        
        foreach (TreeNode rootNode in treeView.Nodes) {
            bool isSelected = SelectTreeNodeByNodeValue(treeView, rootNode, nodeValue);
            if (isSelected) break;
        }
    }

    private static bool SelectTreeNodeByNodeValue(TreeView treeView, TreeNode node, string nodeValue) {
        if (node.Value.Equals(nodeValue)) {
            treeView.CollapseAll();
            TreeNode parentNode = node.Parent;
            while (parentNode != null) {
                parentNode.Expand();
                parentNode = parentNode.Parent;
            }
            node.Select();                        
            return true;
        } else {
            foreach (TreeNode childNode in node.ChildNodes) {
                if (SelectTreeNodeByNodeValue(treeView, childNode, nodeValue)) {
                    return true;
                }
            }
            return false;
        }
    }

    public static string AddPixel(string pixelA, string pixelB) {
        return "" + (PixelValue(pixelA) + PixelValue(pixelB)) + "px";
    }

    public static string AddPixel(string pixel, int addValue) {
        return "" + (PixelValue(pixel) + addValue) + "px";
    }

    public static int PixelValue(string pixel) {
        return int.Parse(pixel.Substring(0, pixel.Length - 2));
    }

    public static string SafeSqlLiteral(string inputSQL) {
        string s = inputSQL.Replace("'", "''");
        s = s.Replace("[", "[[]");
        s = s.Replace("%", "[%]");
        s = s.Replace("_", "[_]");
        s = s.Replace(";", "[;]");
        return s;
    }

    public static void DealWithException(Page page, Exception ex) {
        Exception innerException = ex;
        while (innerException.InnerException != null) {
            innerException = innerException.InnerException;
        }
        if (innerException is ApplicationException) {
            ShowModelDlg(page, innerException.Message);
        } else if (innerException is MyException) {
                ShowModelDlg(page, ((MyException)innerException).ErrorMsg);
        }else{
            throw innerException;
        }
    }

    public static void ShowLoadingDlg(Page page) {
        HtmlControl panel = (HtmlControl)page.Master.FindControl("divLoading");
        panel.Style["display"] = "";
        //panel.Visible = true;
        UpdatePanel dlgUpdatePanel = (UpdatePanel)page.Master.FindControl("upLoading");
        dlgUpdatePanel.Update();
    }

    public static void CloseLoadingDlg(Page page) {
        HtmlControl panel = (HtmlControl)page.Master.FindControl("divLoading");
        panel.Style["display"] = "none";
        //panel.Visible = true;
        UpdatePanel dlgUpdatePanel = (UpdatePanel)page.Master.FindControl("upLoading");
        dlgUpdatePanel.Update();
    }

    public static void CloseModelDlg(Page page){
        HtmlControl panel = (HtmlControl)page.Master.FindControl("ModelDlg");
        panel.Style["display"] = "none";
    }
}
