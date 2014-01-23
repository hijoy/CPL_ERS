<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DocumentPrint.aspx.cs"
    Inherits="DocumentPrint" Culture="Auto" UICulture="Auto" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="../UserControls/ReportViewer.ascx" TagName="ReportViewer" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/tr/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
</head>
<body>
    <form id="Form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" AllowCustomErrorsRedirect="true" runat="server">
    </asp:ScriptManager>
    <table>
        <tr>
            <td>
                <uc1:ReportViewer ID="ReportViewer" runat="server" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
