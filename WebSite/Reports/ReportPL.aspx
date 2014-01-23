<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ReportPL.aspx.cs" Inherits="ReportView" Culture="Auto" UICulture="Auto"%>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="../UserControls/ReportViewer.ascx" TagName="ReportViewer" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/YearAndMonthUserControl.ascx" TagName="YearAndMonthUserControl" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <script src="../Script/DateInput.js" type="text/javascript"></script>
    <div class="title" style="width: 1260px;">
        <asp:Label ID="Label_SearchCondition" runat="server" Text="<%$Resources:Common,Label_SearchCondition %>" /></div>
    <div class="searchDiv" style="width: 1240px;">
        <table class="searchTable" cellpadding="0" cellspacing="0">
            <tr >
                <td width="80px" align="right">
                    <asp:Label ID="Label_FinanceYear" runat="server" Text="<%$Resources:Common,Form_Period %>" />
                    &nbsp;
                </td>
                <td style="width: 150px;">
                    <uc4:YearAndMonthUserControl ID="Period" runat="server" IsReadOnly="false" IsExpensePeriod="true" />
                </td>
                <td valign="bottom" style="width: 200px;">
                    <asp:Button ID="btn_search" runat="server" CssClass="button_nor" Text="<%$Resources:Common,Button_Search %>" OnClick="btn_search_Click" />&nbsp;
                </td>
            </tr>
        </table>
    </div>
    <br />
    <uc1:ReportViewer ID="ReportViewer" runat="server" />
</asp:Content>
