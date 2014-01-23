<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="FormPersonalPaymentList.aspx.cs" Culture="Auto" UICulture="Auto" Inherits="FormQuery_FormPersonalPaymentList" %>

<%@ Register Src="~/UserControls/UCDateInput.ascx" TagName="UCDateInput" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/OUSelect.ascx" TagName="OUSelect" TagPrefix="uc2" %>
<%@ Register Src="~/UserControls/YearAndMonthUserControl.ascx" TagName="YearAndMonthUserControl"
    TagPrefix="uc4" %>
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
                <td colspan="4">
                    <div class="field_title">
                        <asp:Label ID="Form_Period" runat="server" Text="<%$Resources:Common,Form_Period %>" /></div>
                    <nobr>
                        <uc4:YearAndMonthUserControl ID="UCPeriodBegin" runat="server" IsReadOnly="false" IsExpensePeriod="true" />
                        <asp:Label ID="lblSignPeriod" runat="server">~~</asp:Label>
                        <uc4:YearAndMonthUserControl ID="UCPeriodEnd" runat="server" IsReadOnly="false" IsExpensePeriod="true" />
                    </nobr>
                </td>
                <td colspan="4">
                    <div class="field_title">
                        <asp:Label ID="Form_SubmitDate" runat="server" Text="<%$Resources:Common,Form_SubmitDate %>" /></div>
                    <nobr>
                    <uc1:UCDateInput ID="UCDateInputBeginDate" runat="server" IsReadOnly="false" />
                    <asp:Label ID="lbSign" runat="server">~~</asp:Label>
                    <uc1:UCDateInput ID="UCDateInputEndDate" runat="server" IsReadOnly="false" />
                    </nobr>
                </td>
            </tr>
            <tr>
                <tr>
                    <td colspan="3">
                        <div class="field_title">
                            <asp:Label ID="Form_Organization" runat="server" Text="<%$Resources:Common,Form_Organization %>" /></div>
                        <uc2:OUSelect ID="ucOU" Width="170px" runat="server" />
                    </td>
                    <td colspan="4" valign="middle" style="padding-top: 10px;">
                        <span class="field_title"><asp:Label ID="Form_FormStatus" runat="server" Text="<%$Resources:Common,Form_FormStatus %>" /></span>
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
                <asp:Button ID="btnExport" runat="server" CssClass="button_nor" Text="<%$Resources:Common,Button_Export %>" OnClick="btnExport_Click" />&nbsp;
            </td>
        </tr>
    </table>
    <br />
    <div class="title">
        <asp:Label ID="Label_Title" runat="server" meta:resourcekey="Label_Title" /></div>
    <gc:GridView CssClass="GridView" ID="gvApplyList" runat="server" DataSourceID="odsApplyList"
        AutoGenerateColumns="False" DataKeyNames="FormID" AllowPaging="True" AllowSorting="True"
        PageSize="20" OnRowDataBound="gvApplyList_RowDataBound" OnRowCommand="gvApplyList_RowCommand">
        <Columns>
            <asp:TemplateField Visible="False">
                <ItemTemplate>
                    <asp:Label ID="lblFormApplyID" runat="server" Text='<%# Bind("FormID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField SortExpression="FormNo" HeaderText="<%$Resources:Common,Form_FormNo %>">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnFormNo" runat="server" CausesValidation="False" CommandName="Select"
                        Text='<%# Bind("FormNo") %>'></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle Width="150px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField meta:resourcekey="TemplateField_FormType" SortExpression="FormTypeName">
                <ItemTemplate>
                    <asp:Label ID="lblFormType" runat="server" Text='<%# Eval("FormTypeName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="120px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Common,Form_FormStatus %>" SortExpression="StatusID">
                <ItemTemplate>
                    <asp:Label ID="lblFormStatus" runat="server"></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="120px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Common,Form_Period %>" SortExpression="(CASE WHEN dbo.FormTravelReimburse.Period IS NOT NULL THEN dbo.FormTravelReimburse.Period  WHEN dbo.FormPersonalReimburse.Period IS NOT NULL THEN dbo.FormPersonalReimburse.Period END)">
                <ItemTemplate>
                    <asp:Label ID="lblPeriod" runat="server" Text='<%# Eval("Period","{0:yyyyMM}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="100px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Common,Form_AmountRMB %>" SortExpression="(CASE WHEN dbo.FormTravelReimburse.Amount IS NOT NULL THEN dbo.FormTravelReimburse.Amount WHEN dbo.FormPersonalReimburse.Amount IS NOT NULL THEN dbo.FormPersonalReimburse.Amount END)">
                <ItemTemplate>
                    <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount","{0:N}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="150px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Common,Form_ApplyUser %>" SortExpression="UserID">
                <ItemTemplate>
                    <asp:Label ID="lblStuffName" runat="server" Text='<%# GetStaffNameByID(Eval("UserID")) %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="120px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="SubmitDate" HeaderText="<%$Resources:Common,Form_SubmitDate %>">
                <ItemTemplate>
                    <asp:Label ID="lblSubmitDate" runat="server" Text='<%# Bind("SubmitDate", "{0:yyyy-MM-dd}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="150px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="PaymentDate" HeaderText="<%$Resources:Common,Form_PaymentDate %>">
                <ItemTemplate>
                    <asp:Label ID="lblPaymentDate" runat="server" Text='<%# Bind("PaymentDate", "{0:yyyy-MM-dd}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="150px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Common,Form_PaymentAmount %>" SortExpression="PaymentAmount">
                <ItemTemplate>
                    <asp:Label ID="lblPaymentAmount" runat="server" Text='<%# Eval("PaymentAmount","{0:N}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="150px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnScrap" runat="server" Text="<%$Resources:Common,Button_Scrap %>" CommandName="scrap" CommandArgument='<%# Bind("FormID") %>'></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle Width="40px" HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
        <HeaderStyle CssClass="Header" />
        <EmptyDataTemplate>
            <table>
                <tr class="Header">
                    <td style="width: 150px;">
                        <asp:Label ID="Label1" runat="server" Text="<%$Resources:Common,Form_FormNo %>" />
                    </td>
                    <td style="width: 120px;">
                        <asp:Label ID="Label2" runat="server" meta:resourcekey="Label_FormType" />
                    </td>
                    <td style="width: 120px;">
                        <asp:Label ID="Label3" runat="server" Text="<%$Resources:Common,Form_FormStatus %>" />
                    </td>
                    <td style="width: 100px;">
                        <asp:Label ID="Label4" runat="server" Text="<%$Resources:Common,Form_Period %>" />
                    </td>
                    <td style="width: 150px;">
                        <asp:Label ID="Label5" runat="server" Text="<%$Resources:Common,Form_AmountRMB %>" />
                    </td>
                    <td style="width: 120px;">
                        <asp:Label ID="Label6" runat="server" Text="<%$Resources:Common,Form_ApplyUser %>" />
                    </td>
                    <td style="width: 150px;">
                        <asp:Label ID="Label7" runat="server" Text="<%$Resources:Common,Form_SubmitDate %>" />
                    </td>
                    <td style="width: 150px;">
                        <asp:Label ID="Label8" runat="server" Text="<%$Resources:Common,Form_PaymentDate %>" />
                    </td>
                    <td style="width: 150px;">
                        <asp:Label ID="Label9" runat="server" Text="<%$Resources:Common,Form_PaymentAmount %>" />
                    </td>
                </tr>
                <tr>
                    <td colspan="8" class="Empty2 noneLabel">
                        <asp:Label ID="Label_None" runat="server" Text="<%$Resources:Common,Label_None %>" />
                    </td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <SelectedRowStyle CssClass="SelectedRow" />
    </gc:GridView>
    <asp:DataGrid ID="ExportDataGrid" CssClass="GridView" runat="server" Visible="true" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundColumn HeaderText="<%$Resources:Common,Form_FormNo %>" DataField="FormNo" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn meta:resourcekey="TemplateField_FormType" DataField="FormTypeName" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="<%$Resources:Common,Form_FormStatus %>" DataField="StatusName" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="<%$Resources:Common,Form_Period %>" DataField="FPeriodShow" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="<%$Resources:Common,Form_AmountRMB %>" DataField="Amount" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="<%$Resources:Common,Form_ApplyUser %>" DataField="StuffName" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="<%$Resources:Common,Form_SubmitDate %>" DataField="SubmitDateShow" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="<%$Resources:Common,Form_Remark %>"  HeaderStyle-Font-Bold="true" />
        </Columns>
    </asp:DataGrid>
    <asp:ObjectDataSource ID="odsApplyList" runat="server" TypeName="BusinessObjects.FormQueryBLL"
        SelectMethod="GetPagedFormPersonalPaymentViewByRight" EnablePaging="True" SelectCountMethod="QueryFormPersonalPaymentViewCountByRight"
        SortParameterName="sortExpression">
        <SelectParameters>
            <asp:Parameter Name="queryExpression" Type="String" />
            <asp:Parameter Name="UserID" Type="Int32" />
            <asp:Parameter Name="PositionID" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
     <br />
    <br />
</asp:Content>
