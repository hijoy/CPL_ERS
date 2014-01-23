<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="FormPRPOReport.aspx.cs" Inherits="Reports_DemoReport" Culture="Auto" UICulture="Auto"%>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="../UserControls/ReportViewer.ascx" TagName="ReportViewer" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/OUSelect.ascx" TagName="OUSelect" TagPrefix="uc2" %>
<%@ Register Src="~/UserControls/UCDateInput.ascx" TagName="YearAndMonthUserControl"
    TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <script src="../Script/DateInput.js" type="text/javascript"></script>
    <div class="title" style="width: 1240px;">
        <asp:Label ID="Label_SearchCondition" runat="server" Text="<%$Resources:Common,Label_SearchCondition %>" /></div>
    <div class="searchDiv" style="width: 1240px;">
        <table class="searchTable" cellpadding="0" cellspacing="0">
            <tr style="vertical-align: top; height: 40px">
                <td width="200px">
                    <div class="field_title">
                        <asp:Label ID="Label_BeginDate" runat="server" meta:resourcekey="Label_BeginDate" />
                    </div>
                    <uc3:YearAndMonthUserControl ID="UCDateInputBeginDate" IsExpensePeriod="true" runat="server" IsReadOnly="false" />
                </td>
                <td style="width: 200px;">
                    <div class="field_title">
                        <asp:Label ID="Label_EndDate" runat="server" meta:resourcekey="Label_EndDate" />
                    </div>
                    <uc3:YearAndMonthUserControl ID="UCDateInputEndDate" IsExpensePeriod="true" runat="server" IsReadOnly="false" />
                </td>
                <td style="width: 300px;">
                    <div class="field_title">
                        <asp:Label ID="Label_UCOU" runat="server" meta:resourcekey="Label_UCOU" />
                    </div>
                    <uc2:OUSelect ID="UCOU" runat="server" Width="200px" />
                </td>
                <td style="width: 180px;">
                    <div class="field_title">
                        <asp:Label ID="Label_UserId" runat="server" meta:resourcekey="Label_UserId" />
                    </div>
                    <asp:TextBox ID="userId" MaxLength="20" runat="server" Width="150px"></asp:TextBox>
                </td>
                <td style="width: 180px;">
                    <div class="field_title">
                        <asp:Label ID="Label_Type" runat="server" meta:resourcekey="Label_Type" />
                    </div>
                    <asp:CheckBox ID="PR" runat="server" Text="PR" Checked="false" />&nbsp;&nbsp;
                    <asp:CheckBox ID="PO" runat="server" Text="PO" Checked="false" />&nbsp;&nbsp;
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
