<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="RDApplyDetail.aspx.cs" Culture="Auto" UICulture="Auto" Inherits="FormQuery_RDApplyList" %>

<%@ Register Src="~/UserControls/UCDateInput.ascx" TagName="UCDateInput" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/OUSelect.ascx" TagName="OUSelect" TagPrefix="uc2" %>
<%@ Register Src="~/UserControls/YearAndMonthUserControl.ascx" TagName="YearAndMonthUserControl"
    TagPrefix="uc4" %>
<%@ Register Src="../UserControls/ReportViewer.ascx" TagName="ReportViewer" TagPrefix="uc5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <script src="../Script/DateInput.js" type="text/javascript"></script>
    <div class="title">
        <asp:Label ID="Label_SearchCondition" runat="server" Text="<%$Resources:Common,Label_SearchCondition %>" /></div>
    <div class="searchDiv">
        <table class="searchTable" cellpadding="0" cellspacing="0" width="1240px">
            <tr style="vertical-align: top; height: 40px">
                <td colspan="2">
                    <div class="field_title">
                        <asp:Label ID="Form_FormNo" runat="server" Text="<%$Resources:Common,Form_FormNo %>" /></div>
                    <asp:TextBox ID="txtFormNo" MaxLength="20" runat="server" Width="170px"></asp:TextBox>
                </td>
                <td colspan="2">
                    <div class="field_title">
                        <asp:Label ID="Form_ApplyUser" runat="server" Text="<%$Resources:Common,Form_ApplyUser %>" /></div>
                    <asp:TextBox ID="txtStuffUser" MaxLength="50" runat="server" Width="170px"></asp:TextBox>
                </td>
                <td colspan="2">
                    <div class="field_title">
                        <asp:Label ID="Form_PeojectType" runat="server" Text="<%$Resources:Common,Form_PeojectType %>" /></div>
                    <asp:TextBox ID="txtProjectType" MaxLength="50" runat="server" Width="170px"></asp:TextBox>
                </td>
                <td colspan="2">
                    <div class="field_title">
                        <asp:Label ID="Form_PurchaseBudgetType" runat="server" Text="<%$Resources:Common,Form_PurchaseBudgetType %>" /></div>
                    <asp:DropDownList ID="ddlPurchaseBudgetType" runat="server" DataSourceID="sdsPurchaseBudgetType"
                        DataTextField="PurchaseBudgetTypeName" DataValueField="PurchaseBudgetTypeID"
                        Width="170px">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="sdsPurchaseBudgetType" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                        SelectCommand=" select 0 PurchaseBudgetTypeID, ' All' PurchaseBudgetTypeName Union SELECT [PurchaseBudgetTypeID], PurchaseBudgetTypeName FROM [PurchaseBudgetType] order by PurchaseBudgetTypeName">
                    </asp:SqlDataSource>
                </td>
                <td colspan="4">
                    <div class="field_title">
                        <asp:Label ID="Form_Period" runat="server" Text="<%$Resources:Common,Form_Period %>" /></div>
                    <nobr>
                        <uc4:YearAndMonthUserControl ID="UCPeriodBegin" runat="server" IsReadOnly="false" IsExpensePeriod="true" />
                        <asp:Label ID="lblSignPeriod" runat="server">~~</asp:Label>
                        <uc4:YearAndMonthUserControl ID="UCPeriodEnd" runat="server" IsReadOnly="false" IsExpensePeriod="true" />
                    </nobr>
                </td>
            </tr>
            <tr>
                <tr>
                    <td colspan="4">
                        <div class="field_title">
                            <asp:Label ID="Form_SubmitDate" runat="server" Text="<%$Resources:Common,Form_SubmitDate %>" /></div>
                        <nobr>
                    <uc1:UCDateInput ID="UCDateInputBeginDate" runat="server" IsReadOnly="false" />
                    <asp:Label ID="lbSign" runat="server">~~</asp:Label>
                    <uc1:UCDateInput ID="UCDateInputEndDate" runat="server" IsReadOnly="false" />
                    </nobr>
                    </td>
                    <td colspan="3">
                        <div class="field_title">
                            <asp:Label ID="Form_Organization" runat="server" Text="<%$Resources:Common,Form_Organization %>" /></div>
                        <uc2:OUSelect ID="ucOU" Width="170px" runat="server" />
                    </td>
                    <td colspan="4" valign="middle" style="padding-top: 10px;">
                        <span class="field_title">
                            <asp:Label ID="Form_FormStatus" runat="server" Text="<%$Resources:Common,Form_FormStatus %>" />
                        </span>
                        <asp:CheckBox ID="chkAwaiting" runat="server" Text="<%$Resources:Common,Form_FormAwaiting %>" Checked="false"></asp:CheckBox>&nbsp;&nbsp;
                        <asp:CheckBox ID="chkApproveCompleted" runat="server" Text="<%$Resources:Common,Form_FormApproveCompleted %>" Checked="false" />&nbsp;&nbsp;
                        <asp:CheckBox ID="chkRejected" runat="server" Text="<%$Resources:Common,Form_FormRejected %>" Checked="false" />&nbsp;&nbsp;
                        <asp:CheckBox ID="chkScrap" runat="server" Text="<%$Resources:Common,Form_Scrap %>" Checked="false" />
                    </td>
                </tr>
            </tr>
        </table>
    </div>
    <table width="1200px">
        <tr>
            <td align="right" valign="middle" colspan="6">
                <asp:Button ID="btnSearch" runat="server" CssClass="button_nor" Text="<%$Resources:Common,Button_Search %>" OnClick="btnSearch_Click" />&nbsp;
            </td>
        </tr>
    </table>
    <br />
    <div class="title">
        <asp:Label ID="Label_Title" runat="server" meta:resourcekey="Label_Title" />
    </div>
    <br />
    <uc5:ReportViewer ID="ReportViewer" runat="server" />
</asp:Content>
