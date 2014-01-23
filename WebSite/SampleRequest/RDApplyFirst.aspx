<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="RDApplyFirst.aspx.cs" Culture="Auto" UICulture="Auto" Inherits="FormRD_RDApplyFirst" %>

<%@ Register Src="~/UserControls/CustomerControl.ascx" TagName="UCCustomer" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="title">
        <asp:Label ID="Label_Title" runat="server" meta:resourcekey="Label_Title" /></div>
    <div class="searchDiv">
        <table class="searchTable">
            <tr>
                <td style="width: 240px">
                    <div class="field_title">
                        <asp:Label ID="Label1" Text="<%$ Resources:Common,Form_Period %>" runat="server" /></div>
                    <div>
                        <asp:DropDownList ID="ddlPeriod" runat="server" DataSourceID="odsPeriod" DataTextField="PeriodSale"
                            DataValueField="PeriodSaleID" Width="200px">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="odsPeriod" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                            SelectCommand=" select 0 PeriodSaleID, ' Please select' PeriodSale Union SELECT PeriodSaleID, convert(varchar(50),year(PeriodSale))+'-'+convert(varchar(50),month(PeriodSale)) PeriodSale FROM [PeriodSale] ">
                        </asp:SqlDataSource>
                    </div>
                </td>
                <td style="width: 240px">
                    <div class="field_title">
                        <asp:Label ID="Label4" Text="<%$ Resources:Common,Form_Currency %>" runat="server" /></div>
                    <div>
                        <asp:DropDownList ID="CurrencyDDL" runat="server" DataSourceID="odsCurrency" DataTextField="CurrencyShortName"
                            DataValueField="CurrencyID" Width="200px">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="odsCurrency" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                            SelectCommand=" SELECT CurrencyID, CurrencyShortName FROM [Currency] where IsActive = 1 order by CurrencyID">
                        </asp:SqlDataSource>
                    </div>
                </td>
                <td style="width: 300px" colspan="2">
                    <div class="field_title">
                        <asp:Label ID="Label3" Text="<%$Resources:Common,Form_Customer %>" runat="server" />
                        <span class="requiredLable">*</span>
                    </div>
                    <div>
                        <uc1:UCCustomer ID="UCCustomer" runat="server" Width="180px" />
                    </div>
                </td>
                <td style="width: 240px">
                    <div class="field_title">
                        <asp:Label ID="Label5" Text="<%$ Resources:Common,Form_Brand %>" runat="server" /></div>
                    <div>
                        <asp:DropDownList ID="ddlBrand" runat="server" DataSourceID="odsBrand" DataTextField="BrandName"
                            DataValueField="BrandID" Width="200px">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="odsBrand" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                            SelectCommand=" select 0 BrandID, ' Please select' BrandName Union SELECT [BrandID], BrandName FROM [Brand] order by BrandName">
                        </asp:SqlDataSource>
                    </div>
                </td>
                <td style="width: 250px">
                    <div class="field_title">
                        <asp:Label ID="Label2" Text="<%$Resources:Common,Form_ExpenseSubCategory %>" runat="server" /></div>
                    <div>
                        <asp:DropDownList ID="ExpenseSubCategoryDDL" runat="server" DataSourceID="odsExpenseSubCategory"
                            DataTextField="ExpenseSubCategoryName" DataValueField="ExpenseSubCategoryID"
                            Width="180px">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="odsExpenseSubCategory" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                            SelectCommand=" select 0 ExpenseSubCategoryID, ' Please select' ExpenseSubCategoryName Union SELECT ExpenseSubCategoryID, ExpenseSubCategoryName FROM [ExpenseSubCategory] join ExpenseCategory on ExpenseSubCategory.ExpenseCategoryID = ExpenseCategory.ExpenseCategoryID where BusinessType = 3  order by ExpenseSubCategoryName">
                        </asp:SqlDataSource>
                    </div>
                </td>
                <td valign="bottom">
                    <asp:Button ID="NextButton" runat="server" CssClass="button_nor" Text="<%$ Resources:Common,Button_Next %>"
                        OnClick="NextButton_Click" Width="100px" />
                </td>
            </tr>
        </table>
    </div>
    <br />
</asp:Content>
