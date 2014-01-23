<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="MarketingPaymentList.aspx.cs" Culture="Auto" UICulture="Auto" Inherits="FormQuery_MarketingPaymentList" %>

<%@ Register Src="~/UserControls/UCDateInput.ascx" TagName="UCDateInput" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/OUSelect.ascx" TagName="OUSelect" TagPrefix="uc2" %>
<%@ Register Src="~/UserControls/CustomerControl.ascx" TagName="UCCustomer" TagPrefix="uc3" %>
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
                <td  style="width: 200px;">
                    <div class="field_title">
                        <asp:Label ID="Form_FormNo" runat="server" Text="<%$Resources:Common,Form_FormNo %>" /></div>
                    <asp:TextBox ID="txtFormNo" MaxLength="20" runat="server" Width="170px"></asp:TextBox>
                </td>
                <td style="width: 200px;">
                    <div class="field_title">
                        <asp:Label ID="Label_ApplyFormNo" runat="server" meta:resourcekey="Label_ApplyFormNo" /></div>
                    <asp:TextBox ID="txtApplyFormNo" MaxLength="20" runat="server" Width="170px"></asp:TextBox>
                </td>
                <td style="width: 200px;">
                    <div class="field_title">
                        <asp:Label ID="Form_ProjectName" runat="server" Text="<%$Resources:Common,Form_ProjectName %>" /></div>
                    <asp:TextBox ID="txtProjectName" runat="server" Width="170px"></asp:TextBox>
                </td>
                <td style="width: 200px;">
                    <div class="field_title">
                        <asp:Label ID="Form_ApplyUser" runat="server" Text="<%$Resources:Common,Form_ApplyUser %>" /></div>
                    <asp:TextBox ID="txtStuffUser" MaxLength="50" runat="server" Width="170px"></asp:TextBox>
                </td>


                <td colspan="2" style="width: 400px;">
                    <div class="field_title">
                        <asp:Label ID="Form_Organization" runat="server" Text="<%$Resources:Common,Form_Organization %>" /></div>
                    <uc2:OUSelect ID="UCOU" runat="server" Width="220px" />
                </td>
            </tr>
            <tr>
                <td  style="width: 200px;">
                    <div class="field_title">
                        <asp:Label ID="Form_Brand" runat="server" Text="<%$Resources:Common,Form_Brand %>" /></div>
                    <asp:DropDownList ID="BrandDDL" runat="server" DataSourceID="odsBrand" DataTextField="BrandName"
                        DataValueField="BrandID" Width="180px">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="odsBrand" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                        SelectCommand=" select 0 BrandID, ' All' BrandName Union SELECT [BrandID], BrandName FROM [Brand] order by BrandName">
                    </asp:SqlDataSource>
                </td>
                <td style="width: 200px;">
                    <div class="field_title">
                        <asp:Label ID="Form_ExpenseCategory" runat="server" Text="<%$Resources:Common,Form_ExpenseCategory %>" /></div>
                        <asp:DropDownList ID="ExpenseCategoryDDL" runat="server" DataSourceID="odsExpenseCategory" DataTextField="ExpenseCategoryName" DataValueField="ExpenseCategoryID" Width="170px" >
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="odsExpenseCategory" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                            SelectCommand=" select 0 ExpenseCategoryID, ' All' ExpenseCategoryName Union SELECT ExpenseCategoryID, ExpenseCategoryName FROM [ExpenseCategory]  where BusinessType = 2  order by ExpenseCategoryName">
                        </asp:SqlDataSource>
                </td>
                <td colspan="2" style="width: 400px;">
                    <div class="field_title">
                        <asp:Label ID="Form_Period" runat="server" Text="<%$Resources:Common,Form_Period %>" /></div>
                    <nobr>
                        <uc4:YearAndMonthUserControl ID="UCPeriodBegin" runat="server" IsReadOnly="false" IsExpensePeriod="true" />
                        <asp:Label ID="lblSignPeriod" runat="server">~~</asp:Label>
                        <uc4:YearAndMonthUserControl ID="UCPeriodEnd" runat="server" IsReadOnly="false" IsExpensePeriod="true" />
                    </nobr>
                </td>
                <td colspan="2" style="width: 400px;" >
                    <div class="field_title">
                        <asp:Label ID="Form_SubmitDate" runat="server" Text="<%$Resources:Common,Form_SubmitDate %>" /></div>
                    <nobr>
                    <uc1:UCDateInput ID="UCDateInputBeginDate" runat="server" IsReadOnly="false" />
                    <asp:Label ID="lbSign" runat="server">~~</asp:Label>
                    <uc1:UCDateInput ID="UCDateInputEndDate" runat="server" IsReadOnly="false" />
                    </nobr>
                </td>
            </tr>
            <tr style="height: 40px">
                <td style="width: 200px;" >
                    <div class="field_title">
                        <asp:Label ID="Form_PaymentType" runat="server" Text="<%$Resources:Common,Form_PaymentType %>" /></div>
                    <asp:DropDownList ID="PaymentTypeDDL" runat="server" DataSourceID="odsPaymentType"
                        DataTextField="PaymentTypeName" DataValueField="PaymentTypeID" Width="180px">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="odsPaymentType" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                        SelectCommand=" select 0 PaymentTypeID,' All' PaymentTypeName Union SELECT PaymentTypeID, PaymentTypeName FROM [PaymentType] order by PaymentTypeID ">
                    </asp:SqlDataSource>
                </td>
                <td style="width: 200px;" >
                    <div class="field_title">
                        <asp:Label ID="Form_InvoiceStatus" runat="server" Text="<%$Resources:Common,Form_InvoiceStatus %>" /></div>
                    <asp:DropDownList ID="InvoiceStatusDDL" runat="server" DataSourceID="odsInvoiceStatus"
                        DataTextField="Name" DataValueField="InvoiceStatusID" Width="170px">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="odsInvoiceStatus" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                        SelectCommand=" select 0 InvoiceStatusID,' All' Name Union SELECT InvoiceStatusID, Name FROM InvoiceStatus ">
                    </asp:SqlDataSource>
                </td>
                <td style="width: 200px;" >
                    <div class="field_title">
                        <asp:Label ID="Label14" runat="server" Text="发票是否收回" /></div>
                    <asp:DropDownList ID="IsInvoiceReturnDDL" runat="server" CssClass="InputCombo" Width="170px">
                        <asp:ListItem Text="All" Value="3" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="发票已收回" Value="1"></asp:ListItem>
                        <asp:ListItem Text="发票未收回" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </td>

                <td style="width: 600px;" colspan="3" valign="middle">
                    <span class="field_title">
                        <asp:Label ID="Form_FormStatus" runat="server" Text="<%$Resources:Common,Form_FormStatus %>" /></span>&nbsp;&nbsp;
                    <asp:CheckBox ID="chkAwaiting" runat="server" Text="<%$Resources:Common,Form_FormAwaiting %>"
                        Checked="false"></asp:CheckBox>&nbsp;&nbsp;
                    <asp:CheckBox ID="chkApproveCompleted" runat="server" Text="<%$Resources:Common,Form_FormApproveCompleted %>"
                        Checked="false" />&nbsp;&nbsp;
                    <asp:CheckBox ID="chkRejected" runat="server" Text="<%$Resources:Common,Form_FormRejected %>"
                        Checked="false" />&nbsp;&nbsp;
                    <asp:CheckBox ID="chkScrap" runat="server" Text="<%$Resources:Common,Form_Scrap %>"
                        Checked="false" />
                </td>
                <td></td>
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
        PageSize="20" OnRowDataBound="gvApplyList_RowDataBound" 
        onrowcommand="gvApplyList_RowCommand">
        <Columns>
            <asp:TemplateField Visible="False">
                <ItemTemplate>
                    <asp:Label ID="lblFormApplyID" runat="server" Text='<%# Bind("FormID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField SortExpression="FormNo" HeaderText="<%$Resources:Common,Form_FormNo %>">
                <ItemTemplate>
                    <asp:LinkButton ID="lblFormNo" runat="server" CausesValidation="False" CommandName="Select"
                        Text='<%# Bind("FormNo") %>'></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle Width="90px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="StatusID" HeaderText="<%$Resources:Common,Form_FormStatus %>">
                <ItemTemplate>
                    <asp:Label ID="lblStatus" runat="server"></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="75px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="MarketingProjectName" HeaderText="<%$Resources:Common,Form_ProjectName %>">
                <ItemTemplate>
                    <asp:Label ID="lblMarketingProjectName" runat="server" Text='<%# Eval("MarketingProjectName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="280px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="ExpenseCategoryName" HeaderText="<%$Resources:Common,Form_ExpenseCategory %>">
                <ItemTemplate>
                    <asp:Label ID="lblExpenseCategoryName" runat="server" Text='<%# Eval("ExpenseCategoryName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="120px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="CustomerChannelName" HeaderText="<%$Resources:Common,Form_CustomerChannel %>">
                <ItemTemplate>
                    <asp:Label ID="lblCustomerChannelName" runat="server" Text='<%# Eval("CustomerChannelName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="80px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="BrandName" HeaderText="<%$Resources:Common,Form_Brand %>">
                <ItemTemplate>
                    <asp:Label ID="lblBrandName" runat="server" Text='<%# Eval("BrandName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="80px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="FormMarketingPayment.AmountRMB" meta:resourcekey="TemplateField_AmountRMB">
                <ItemTemplate>
                    <asp:Label ID="lblAmountRMB" runat="server" Text='<%# Bind("AmountRMB","{0:N}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="80px" HorizontalAlign="right" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="FPeriod" HeaderText="<%$Resources:Common,Form_Period %>">
                <ItemTemplate>
                    <asp:Label ID="lblPeriod" Text='<%# Bind("FPeriod", "{0:yyyyMM}") %>' runat="server"></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="60px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="CurrencyShortName" HeaderText="<%$Resources:Common,Form_Currency %>">
                <ItemTemplate>
                    <asp:Label ID="lblCurrency" runat="server" Text='<%# Bind("CurrencyShortName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="50px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="StuffName" HeaderText="<%$Resources:Common,Form_ApplyUser %>">
                <ItemTemplate>
                    <asp:Label ID="lblStuffName" runat="server" Text='<%# Eval("StuffName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="60px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="PaymentTypeName" HeaderText="<%$Resources:Common,Form_PaymentType %>">
                <ItemTemplate>
                    <asp:Label ID="lblPaymentType" runat="server" Text='<%# Bind("PaymentTypeName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="75px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="InvoiceStatus.Name" HeaderText="<%$Resources:Common,Form_InvoiceStatus %>">
                <ItemTemplate>
                    <asp:Label ID="lblInvoiceStatusName" runat="server" Text='<%# Bind("InvoiceStatusName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="75px" HorizontalAlign="Center" />
            </asp:TemplateField>

            <asp:TemplateField SortExpression="SubmitDate" HeaderText="<%$Resources:Common,Form_SubmitDate %>">
                <ItemTemplate>
                    <asp:Label ID="lblSubmitDate" runat="server" Text='<%# Bind("SubmitDate", "{0:yyyy-MM-dd}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="75px" HorizontalAlign="Center" />
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
                    <td style="width: 90px;" class="Empty1">
                        <asp:Label ID="Label1" runat="server" Text="<%$Resources:Common,Form_FormNo %>" />
                    </td>
                    <td style="width: 75px;">
                        <asp:Label ID="Label2" runat="server" Text="<%$Resources:Common,Form_FormStatus %>" />
                    </td>
                    <td style="width: 280px;">
                        <asp:Label ID="Label3" runat="server" Text="<%$Resources:Common,Form_ProjectName %>" />
                    </td>
                    <td style="width: 120px;">
                        <asp:Label ID="Label9" runat="server" Text="<%$Resources:Common,Form_ExpenseCategory %>" />
                    </td>
                    <td style="width: 80px;">
                        <asp:Label ID="Label4" runat="server" Text="<%$Resources:Common,Form_CustomerChannel %>" />
                    </td>
                    <td style="width: 80px;">
                        <asp:Label ID="Label5" runat="server" Text="<%$Resources:Common,Form_Brand %>" />
                    </td>
                    <td style="width: 80px;">
                        <asp:Label ID="Label6" runat="server" meta:resourcekey="Label_AmountRMB" />
                    </td>
                    <td style="width: 60px;">
                        <asp:Label ID="Label7" runat="server" Text="<%$Resources:Common,Form_Period %>" />
                    </td>
                    <td style="width: 50px;">
                        <asp:Label ID="Label8" runat="server" Text="<%$Resources:Common,Form_Currency %>" />
                    </td>
                    <td style="width: 60px;">
                        <asp:Label ID="Label12" runat="server" Text="<%$Resources:Common,Form_ApplyUser %>" />
                    </td>
                    <td style="width: 75px;">
                        <asp:Label ID="Label10" runat="server" Text="<%$Resources:Common,Form_PaymentType %>" />
                    </td>
                    <td style="width: 75px;">
                        <asp:Label ID="Label11" runat="server" Text="<%$Resources:Common,Form_InvoiceStatus %>" />
                    </td>
                    <td style="width: 75px;">
                        <asp:Label ID="Label13" runat="server" Text="<%$Resources:Common,Form_SubmitDate %>" />
                    </td>
                </tr>
                <tr>
                    <td colspan="13" class="Empty2 noneLabel">
                        <asp:Label ID="Label_None" runat="server" Text="<%$Resources:Common,Label_None %>" />
                    </td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <SelectedRowStyle CssClass="SelectedRow" />
    </gc:GridView>
    <asp:DataGrid ID="ExportDataGrid" runat="server" Visible="true" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundColumn HeaderText="<%$Resources:Common,Form_FormNo %>" DataField="FormNo" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="<%$Resources:Common,Form_FormStatus %>" DataField="StatusName" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="<%$Resources:Common,Form_ProjectName %>" DataField="MarketingProjectName" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="<%$Resources:Common,Form_ExpenseCategory %>" DataField="ExpenseCategoryName" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="<%$Resources:Common,Form_CustomerChannel %>" DataField="CustomerChannelName" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="<%$Resources:Common,Form_Brand %>" DataField="BrandName" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn meta:resourcekey="TemplateField_AmountRMB" DataField="AmountRMB" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="<%$Resources:Common,Form_Period %>" DataField="FPeriodShow" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="<%$Resources:Common,Form_Currency %>" DataField="CurrencyShortName" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="<%$Resources:Common,Form_ApplyUser %>" DataField="StuffName" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="<%$Resources:Common,Form_SubmitDate %>" DataField="SubmitDateShow" HeaderStyle-Font-Bold="true" />
        </Columns>
    </asp:DataGrid>
    <asp:ObjectDataSource ID="odsApplyList" runat="server" TypeName="BusinessObjects.FormQueryBLL"
        SelectMethod="GetPagedFormMarketingPaymentViewByRight" EnablePaging="True" SelectCountMethod="QueryFormMarketingPaymentViewCountByRight"
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
