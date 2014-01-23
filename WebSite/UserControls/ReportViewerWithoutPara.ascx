<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReportViewerWithoutPara.ascx.cs"
    Inherits="UserControls_ReportViewerWithoutPara" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<style>
    html, body, form
    {
        height: 100%;
    }
</style>
<rsweb:ReportViewer ID="rptViewer" runat="server" EnableTheming="True" ShowParameterPrompts="false"
    AsyncRendering="true" ShowFindControls="False" ShowExportControls="false">
    <ServerReport ReportServerUrl="" />
</rsweb:ReportViewer>

