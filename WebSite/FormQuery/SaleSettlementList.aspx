<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="SaleSettlementList.aspx.cs" Culture="Auto" UICulture="Auto" Inherits="FormQuery_SaleSettlementList"  %>

<%@ Register Src="~/UserControls/UCDateInput.ascx" TagName="UCDateInput" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/OUSelect.ascx" TagName="OUSelect" TagPrefix="uc2" %>
<%@ Register Src="~/UserControls/CustomerControl.ascx" TagName="UCCustomer" TagPrefix="uc3" %>
<%@ Register Src="~/UserControls/YearAndMonthUserControl.ascx" TagName="YearAndMonthUserControl" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" src="../Script/js.js" type="text/javascript"></script>
    <script language="javascript" src="../Script/DateInput.js" type="text/javascript"></script>
    <div class="title" ><asp:Label ID="Label_SearchCondition" runat="server" Text="<%$Resources:Common,Label_SearchCondition %>" /></div>
    <div class="searchDiv">
        <table class="searchTable" cellpadding="0" cellspacing="0" width="1240px">
            <tr style="vertical-align: top; height: 40px">
                <td style="width: 200px;">
                    <div class="field_title">
                        <asp:Label ID="Label_FormNo" runat="server" meta:resourcekey="Label_FormNo" /></div>
                    <asp:TextBox ID="txtFormNo" MaxLength="20" runat="server" Width="170px"></asp:TextBox>
                </td>
                <td style="width: 200px;">
                    <div class="field_title">
                        <asp:Label ID="Label_ApplyFormNo" runat="server" meta:resourcekey="Label_ApplyFormNo" /></div>
                    <asp:TextBox ID="txtApplyFormNo" MaxLength="20" runat="server" Width="170px"></asp:TextBox>
                </td>
                <td style="width: 200px;">
                    <div class="field_title">
                        <asp:Label ID="Form_ApplyUser" runat="server" Text="<%$Resources:Common,Form_ApplyUser %>" /></div>
                    <asp:TextBox ID="txtStuffUser" MaxLength="50" runat="server" Width="170px"></asp:TextBox>
                </td>
                <td style="width: 200px;">
                    <div class="field_title">
                        <asp:Label ID="Form_ExpenseSubCategory" runat="server" Text="<%$Resources:Common,Form_ExpenseSubCategory %>" /></div>
                        <asp:DropDownList ID="ExpenseSubCategoryDDL" runat="server" DataSourceID="odsExpenseSubCategory" DataTextField="ExpenseSubCategoryName" DataValueField="ExpenseSubCategoryID" Width="180px" >
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="odsExpenseSubCategory" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                            SelectCommand=" select 0 ExpenseSubCategoryID, ' All' ExpenseSubCategoryName Union SELECT ExpenseSubCategoryID, ExpenseSubCategoryName FROM [ExpenseSubCategory] join ExpenseCategory on ExpenseSubCategory.ExpenseCategoryID = ExpenseCategory.ExpenseCategoryID where BusinessType = 1  order by ExpenseSubCategoryName">
                        </asp:SqlDataSource>
                </td>
                <td style="width: 200px;">
                    <div class="field_title">
                        <asp:Label ID="Form_Brand" runat="server" Text="<%$Resources:Common,Form_Brand %>" /></div>
                        <asp:DropDownList ID="BrandDDL" runat="server" DataSourceID="odsBrand" DataTextField="BrandName" DataValueField="BrandID" Width="180px" >
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="odsBrand" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                            SelectCommand=" select 0 BrandID, ' All' BrandName Union SELECT [BrandID], BrandName FROM [Brand] order by BrandName">
                        </asp:SqlDataSource>
                </td>
                <td style="width: 200px;">
                    <div class="field_title">
                        <asp:Label ID="Label_IsClose" runat="server" meta:resourcekey="Label_IsClose" /></div>
                    <asp:DropDownList ID="IsCloseDDL" runat="server" CssClass="InputCombo" Width="170px">
                        <asp:ListItem meta:resourcekey="ListItem_All" Value="3" Selected="True"></asp:ListItem>
                        <asp:ListItem meta:resourcekey="ListItem_Close" Value="1"></asp:ListItem>
                        <asp:ListItem meta:resourcekey="ListItem_NoClose" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </td>

            </tr>
            <tr style="vertical-align: top; height: 40px">
                <td colspan="2" style="width: 400px;">
                    <div class="field_title">
                        <asp:Label ID="Form_Customer" runat="server" Text="<%$Resources:Common,Form_Customer %>" /></div>
                        <uc3:UCCustomer ID="UCCustomer" runat="server"  Width="240px"/>
                </td>
                <td colspan="2" style="width: 400px;">
                    <div class="field_title">
                        <asp:Label ID="Form_Organization" runat="server" Text="<%$Resources:Common,Form_Organization %>" /></div>
                    <uc2:OUSelect ID="UCOU" runat="server" Width="220px" />
                </td>

                <td colspan="2" style="width: 400px;">
                    <div class="field_title">
                        <asp:Label ID="Form_SubmitDate" runat="server" Text="<%$Resources:Common,Form_SubmitDate %>" /></div>
                    <nobr>
                    <uc1:UCDateInput ID="UCDateInputBeginDate" runat="server" IsReadOnly="false" />
                    <asp:Label ID="lbSign" runat="server">~~</asp:Label>
                    <uc1:UCDateInput ID="UCDateInputEndDate" runat="server" IsReadOnly="false" />
                    </nobr>
                </td>
            </tr>
            <tr style=" height:40px">
                <td  colspan="6" valign="middle">
                    <span class="field_title"><asp:Label ID="Form_FormStatus" runat="server" Text="<%$Resources:Common,Form_FormStatus %>" /></span>&nbsp;&nbsp;
                    <asp:CheckBox ID="chkAwaiting" runat="server" Text="<%$Resources:Common,Form_FormAwaiting %>" Checked="false"></asp:CheckBox>&nbsp;&nbsp;
                    <asp:CheckBox ID="chkApproveCompleted" runat="server" Text="<%$Resources:Common,Form_FormApproveCompleted %>" Checked="false" />&nbsp;&nbsp;
                    <asp:CheckBox ID="chkRejected" runat="server" Text="<%$Resources:Common,Form_FormRejected %>" Checked="false" />&nbsp;&nbsp;
                    <asp:CheckBox ID="chkScrap" runat="server" Text="<%$Resources:Common,Form_Scrap %>" Checked="false" />
                </td>
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
    <div class="title" ><asp:Label ID="Label_Title" runat="server" meta:resourcekey="Label_Title" /></div>
    <gc:GridView CssClass="GridView" ID="gvSettlementList" runat="server" DataSourceID="odsSettlementList" AutoGenerateColumns="False" DataKeyNames="FormID" AllowPaging="True"
            AllowSorting="True" PageSize="20" OnRowDataBound="gvSettlementList_RowDataBound" OnRowCommand="gvSettlementList_RowCommand">
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
                <ItemStyle Width="160px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="StatusID" HeaderText="<%$Resources:Common,Form_FormStatus %>">
                <ItemTemplate>
                    <asp:Label ID="lblStatus" runat="server"></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="80px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="CustomerName" HeaderText="<%$Resources:Common,Form_Customer %>">
                <ItemTemplate>
                    <asp:Label ID="lblCustomerName" runat="server" Text='<%# Eval("CustomerName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="220px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="CustomerChannelName" HeaderText="<%$Resources:Common,Form_CustomerChannel %>">
                <ItemTemplate>
                    <asp:Label ID="lblCustomerChannelName" runat="server" Text='<%# Eval("CustomerChannelName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="100px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="BrandName" HeaderText="<%$Resources:Common,Form_Brand %>">
                <ItemTemplate>
                    <asp:Label ID="lblBrandName" runat="server" Text='<%# Eval("BrandName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="100px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="ExpenseSubCategoryName" HeaderText="<%$Resources:Common,Form_ExpenseSubCategory %>">
                <ItemTemplate>
                    <asp:Label ID="lblSubCategory" runat="server" Text='<%# Eval("ExpenseSubCategoryName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="100px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="CurrencyShortName" HeaderText="<%$Resources:Common,Form_Currency %>">
                <ItemTemplate>
                    <asp:Label ID="lblCurrency" runat="server" Text='<%# Bind("CurrencyShortName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="60px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="ApplyAmountRMB" HeaderText="<%$Resources:Common,Form_AmountRMB %>">
                <ItemTemplate>
                    <asp:Label ID="lblApplyAmount" runat="server" Text='<%# Bind("ApplyAmountRMB","{0:N}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="100px" HorizontalAlign="right" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="AmountRMB" meta:resourcekey="TemplateField_AmountRMB">
                <ItemTemplate>
                    <asp:Label ID="lblAmountRMB" runat="server" Text='<%# Bind("AmountRMB","{0:N}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="100px" HorizontalAlign="right" />
            </asp:TemplateField>
            <asp:TemplateField  HeaderText="<%$Resources:Common,Form_ApplyUser %>">
                <ItemTemplate>
                    <asp:Label ID="lblStuffName" runat="server" Text='<%# Eval("StuffName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="100px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="SubmitDate" HeaderText="<%$Resources:Common,Form_SubmitDate %>">
                <ItemTemplate>
                    <asp:Label ID="lblSubmitDate" runat="server" Text='<%# Bind("SubmitDate", "{0:yyyy-MM-dd}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="80px" HorizontalAlign="Center" />
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
                    <td style="width: 150px;" class="Empty1">
                        <asp:Label ID="Label1" runat="server" Text="<%$Resources:Common,Form_FormNo %>" />
                    </td>
                    <td style="width: 80px;">
                        <asp:Label ID="Label2" runat="server" Text="<%$Resources:Common,Form_FormStatus %>" />
                    </td>
                    <td style="width: 200px;">
                        <asp:Label ID="Label3" runat="server" Text="<%$Resources:Common,Form_Customer %>" />
                    </td>
                    <td style="width: 100px;">
                        <asp:Label ID="Label4" runat="server" Text="<%$Resources:Common,Form_CustomerChannel %>" />
                    </td>
                    <td style="width: 100px;">
                        <asp:Label ID="Label5" runat="server" Text="<%$Resources:Common,Form_Brand %>" />
                    </td>
                    <td style="width: 100px;">
                        <asp:Label ID="Label6" runat="server" Text="<%$Resources:Common,Form_ExpenseSubCategory %>" />
                    </td>
                    <td style="width: 60px;">
                        <asp:Label ID="Label7" runat="server" Text="<%$Resources:Common,Form_Currency %>" />
                    </td>
                    <td style="width: 100px;">
                        <asp:Label ID="Label8" runat="server" Text="<%$Resources:Common,Form_AmountRMB %>" />
                    </td>
                    <td style="width: 100px;">
                        <asp:Label ID="Label9" runat="server" meta:resourcekey="Label_AmountRMB" />
                    </td>
                    <td style="width: 100px;">
                        <asp:Label ID="Label10" runat="server" Text="<%$Resources:Common,Form_ApplyUser %>" />
                    </td>
                    <td style="width: 100px;">
                        <asp:Label ID="Label11" runat="server" Text="<%$Resources:Common,Form_SubmitDate %>" />
                    </td>
                </tr>
                <tr>
                    <td colspan="11" class="Empty2 noneLabel">
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
            <asp:BoundColumn HeaderText="<%$Resources:Common,Form_Customer %>" DataField="CustomerName" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="<%$Resources:Common,Form_CustomerChannel %>" DataField="CustomerChannelName" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="<%$Resources:Common,Form_Brand %>" DataField="BrandName" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="<%$Resources:Common,Form_ExpenseSubCategory %>" DataField="ExpenseSubCategoryName" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="<%$Resources:Common,Form_Currency %>" DataField="CurrencyShortName" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="<%$Resources:Common,Form_AmountRMB %>" DataField="ApplyAmountRMB" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn meta:resourcekey="TemplateField_AmountRMB" DataField="AmountRMB" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="<%$Resources:Common,Form_ApplyUser %>" DataField="StuffName" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="<%$Resources:Common,Form_SubmitDate %>" DataField="SubmitDateShow" HeaderStyle-Font-Bold="true" />
        </Columns>
    </asp:DataGrid>
    <asp:ObjectDataSource ID="odsSettlementList" runat="server" TypeName="BusinessObjects.FormQueryBLL"
        SelectMethod="GetPagedFormSaleSettlementViewByRight" EnablePaging="True" SelectCountMethod="QueryFormSaleSettlementViewCountByRight"
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
