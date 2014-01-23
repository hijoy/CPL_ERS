<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="FormInvitationReimburseSelect.aspx.cs" Inherits="FormTE_FormInvitationReimburseSelect"
    Culture="Auto" UICulture="Auto" meta:resourcekey="PageResource1" %>

<%@ Register Src="~/UserControls/UCDateInput.ascx" TagName="UCDateInput" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/OUSelect.ascx" TagName="OUSelect" TagPrefix="uc2" %>
<%@ Register Src="~/UserControls/CustomerControl.ascx" TagName="UCCustomer" TagPrefix="uc3" %>
<%@ Register Src="~/UserControls/YearAndMonthUserControl.ascx" TagName="YearAndMonthUserControl"
    TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <script src="../Script/DateInput.js" type="text/javascript"></script>
    <div class="title">
        <asp:Label ID="LabelSearchCondition" Text='<%$Resources:Common,Label_SearchCondition %>' runat="server" />
    </div>
    <div class="searchDiv">
        <table class="searchTable" cellpadding="0" cellspacing="0" width="1240px">
            <tr style="vertical-align: top; height: 40px">
                <td>
                    <div class="field_title">
                        <asp:Label ID="Label_FormNo" runat="server" Text="<%$Resources:Common,Form_FormNo %>" />
                    </div>
                    <asp:TextBox ID="txtFormNo" MaxLength="20" runat="server" Width="170px"></asp:TextBox>
                </td>
                <td colspan="2">
                    <div class="field_title">
                        <asp:Label ID="Label_CustomerName" runat="server" Text="<%$Resources:Common,Form_Customer %>" />
                    </div>
                    <asp:TextBox ID="txtCustomerName" MaxLength="20" runat="server" Width="170px"></asp:TextBox>
                </td>
                <td colspan="2">
                    <div class="field_title">
                        <asp:Label ID="Label_Period" runat="server" Text="<%$Resources:Common,Form_Period %>" />
                    </div>
                    <nobr>
                        <uc4:YearAndMonthUserControl ID="UCPeriodBegin" runat="server" IsReadOnly="false" IsExpensePeriod="true" />
                        <asp:Label ID="lblSignPeriod" runat="server" >~~</asp:Label>
                        <uc4:YearAndMonthUserControl ID="UCPeriodEnd" runat="server" IsReadOnly="false" IsExpensePeriod="true" />
                    </nobr>
                </td>
                <td colspan="2">
                    <div class="field_title">
                        <asp:Label ID="Label_SubmitDate" runat="server" Text="<%$Resources:Common,Form_SubmitDate %>" />
                    </div>
                    <nobr>
                    <uc1:UCDateInput ID="UCDateInputBeginDate" runat="server" IsReadOnly="false" />
                    <asp:Label ID="lbSign" runat="server">~~</asp:Label>
                    <uc1:UCDateInput ID="UCDateInputEndDate" runat="server" IsReadOnly="false" />
                    </nobr>
                </td>
            </tr>
        </table>
    </div>
    <table width="1200px">
        <tr>
            <td align="right" valign="middle" colspan="6">
                <asp:Button ID="btnSearch" runat="server" CssClass="button_nor" Text="<%$Resources:Common,Button_Search %>" 
                    OnClick="btnSearch_Click" />&nbsp;
            </td>
        </tr>
    </table>
    <br />
    <div class="title">
        <asp:Label ID="Labeltitle" meta:resourcekey="Labeltitle" runat="server" />
    </div>
    <gc:GridView CssClass="GridView" ID="gvApplyList" runat="server" DataSourceID="odsApplyList"
        AutoGenerateColumns="False" DataKeyNames="FormID" AllowPaging="True" AllowSorting="True"
        PageSize="20" OnRowDataBound="gvApplyList_RowDataBound" 
        meta:resourcekey="gvApplyListResource1">
        <Columns>
            <asp:TemplateField Visible="False">
                <ItemTemplate>
                    <asp:Label ID="lblFormApplyID" runat="server" Text='<%# Bind("FormID") %>' ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField SortExpression="FormNo" HeaderText="<%$Resources:Common,Form_FormNo %>">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnFormNo" runat="server" CausesValidation="False" CommandName="Select"
                        Text='<%# Bind("FormNo") %>'></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle Width="120px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="CustomerName"  HeaderText="<%$Resources:Common,Form_Customer %>">
                <ItemTemplate>
                    <asp:Label ID="lblCustomerName" runat="server" Text='<%# Eval("CustomerName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="210px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="OccuredDate" meta:resourcekey="TemplateFieldResource4">
                <ItemTemplate>
                    <asp:Label ID="lblOccuredDate" runat="server" Text='<%# Eval("OccuredDate", "{0:yyyy-MM-dd}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="100px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="Place" meta:resourcekey="TemplateFieldResource5">
                <ItemTemplate>
                    <asp:Label ID="lblPlace" runat="server" Text='<%# Eval("Place") %>' ></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="320px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="Amount" HeaderText="<%$Resources:Common,Form_AmountRMB %>">
                <ItemTemplate>
                    <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("AmountRMB","{0:N}") %>' ></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="100px" HorizontalAlign="right" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="Period" HeaderText="<%$Resources:Common,Form_Period %>">
                <ItemTemplate>
                    <asp:Label ID="lblPeriod" Text='<%# Bind("Period", "{0:yyyyMM}") %>' runat="server" ></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="80px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="CurrencyID" HeaderText="<%$Resources:Common,Form_Currency %>">
                <ItemTemplate>
                    <asp:Label ID="lblCurrency" runat="server" Text='<%# Bind("CurrencyShortName") %>' ></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="60px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Common,Form_ApplyUser %>">
                <ItemTemplate>
                    <asp:Label ID="lblStuffName" runat="server" Text='<%# GetStaffNameByID(Eval("UserID")) %>' ></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="60px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="SubmitDate" HeaderText="<%$Resources:Common,Form_SubmitDate %>">
                <ItemTemplate>
                    <asp:Label ID="lblSubmitDate" runat="server" Text='<%# Bind("SubmitDate", "{0:yyyy-MM-dd}") %>' ></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="100px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField meta:resourcekey="TemplateFieldResource11">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnReimburse" runat="server" meta:resourcekey="lbtnReimburseResource1"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle Width="80px" HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
        <HeaderStyle CssClass="Header" />
        <EmptyDataTemplate>
            <table>
                <tr class="Header">
                    <td style="width: 50px;" class="Empty1">
                    </td>
                    <td style="width: 160px;">
                        <asp:Label ID="Label1" runat="server" Text="<%$Resources:Common,Form_FormNo %>" />
                    </td>
                    <td style="width: 250px;">
                        <asp:Label ID="Label2" runat="server" Text="<%$Resources:Common,Form_Customer %>" />
                    </td>
                    <td style="width: 100px;">
                        <asp:Label ID="Label3" runat="server" meta:resourcekey="Label_OccuredDate" />
                    </td>
                    <td style="width: 200px;">
                        <asp:Label ID="Label4" runat="server" meta:resourcekey="Label_Place" />
                    </td>
                    <td style="width: 100px;">
                        <asp:Label ID="Label5" runat="server" Text="<%$Resources:Common,Form_AmountRMB %>" />
                    </td>
                    <td style="width: 80px;">
                        <asp:Label ID="Label6" runat="server" Text="<%$Resources:Common,Form_Period %>" />
                    </td>
                    <td style="width: 100px;">
                        <asp:Label ID="Label7" runat="server" Text="<%$Resources:Common,Form_Currency %>" />
                    </td>
                    <td style="width: 60px;">
                        <asp:Label ID="Label9" runat="server" Text="<%$Resources:Common,Form_ApplyUser %>" />
                    </td>
                    <td style="width: 100px;">
                        <asp:Label ID="Label10" runat="server" Text="<%$Resources:Common,Form_SubmitDate %>" />
                    </td>
                </tr>
                <tr>
                    <td colspan="10" class="Empty2 noneLabel">
                        <asp:Label ID="Label11" runat="server" Text="<%$Resources:Common,Label_None %>" />
                    </td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <SelectedRowStyle CssClass="SelectedRow" />
    </gc:GridView>
    <asp:ObjectDataSource ID="odsApplyList" runat="server" TypeName="BusinessObjects.FormQueryBLL"
        SelectMethod="GetPagedFormInvitationApplyView" EnablePaging="True" SelectCountMethod="QueryFormInvitationApplyViewCount"
        SortParameterName="sortExpression">
        <SelectParameters>
            <asp:Parameter Name="queryExpression" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
