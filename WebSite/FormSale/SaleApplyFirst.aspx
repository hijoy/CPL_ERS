<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="SaleApplyFirst.aspx.cs" UICulture="Auto" Culture="Auto" Inherits="FormSale_SaleApplyFirst"%>

<%@ Register Src="~/UserControls/CustomerControl.ascx" TagName="UCCustomer" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <script src="../Script/DateInput.js" type="text/javascript"></script>
    <div class="title">
        <asp:Label ID="Label5" runat="server" meta:resourcekey="Label_Title" />
    </div>
    <div class="searchDiv">
        <table class="searchTable">
            <tr>
                <td style="width: 120px">
                    <div class="field_title">
                        <asp:Label runat="server" Text="<%$Resources:Common,Form_Period %>" />
                        <span class="requiredLable">*</span>
                    </div>
                    <div>
                        <asp:DropDownList ID="PeriodDDL" runat="server" DataSourceID="odsPeriod" DataTextField="PeriodSale"
                            DataValueField="PeriodSaleID" Width="100px" >
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="odsPeriod" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                            SelectCommand=" select 0 PeriodSaleID, ' Please select' PeriodSale Union SELECT PeriodSaleID, convert(varchar(50),year(PeriodSale))+'-'+convert(varchar(50),month(PeriodSale)) PeriodSale FROM [PeriodSale] ">
                        </asp:SqlDataSource>
                    </div>
                </td>
                <td style="width: 350px" colspan="2">
                    <div class="field_title">
                        <asp:Label ID="Label1" Text="<%$Resources:Common,Form_Customer %>" runat="server" />
                        <span class="requiredLable">*</span>
                    </div>
                    <div>
                        <uc1:uccustomer id="UCCustomer" runat="server" width="230px" ShowNo="true"/>
                    </div>
                </td>
                <td style="width: 150px">
                    <div class="field_title">
                        <asp:Label ID="Label2" Text="<%$Resources:Common,Form_Brand %>" runat="server" />
                        <span class="requiredLable">*</span>
                    </div>
                    <div>
                        <asp:DropDownList ID="BrandDDL" runat="server" DataSourceID="odsBrand" DataTextField="BrandName"
                            DataValueField="BrandID" Width="130px">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="odsBrand" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                            SelectCommand=" select 0 BrandID, ' Please select' BrandName Union SELECT [BrandID], BrandName FROM [Brand] order by BrandName">
                        </asp:SqlDataSource>
                    </div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label6" Text="<%$Resources:Common,Form_ExpenseCategory %>" runat="server" />
                        <span class="requiredLable">*</span>
                    </div>
                    <div>
                        <asp:DropDownList ID="ddlExpenseCategory" runat="server" 
                            DataSourceID="odsExpenseCategory" DataTextField="ExpenseCategoryName"
                            DataValueField="ExpenseCategoryID" Width="180px" 
                            onselectedindexchanged="ddlExpenseCategory_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="odsExpenseCategory" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                            SelectCommand=" select 0 ExpenseCategoryID, ' Please select' ExpenseCategoryName Union SELECT [ExpenseCategoryID], ExpenseCategoryName FROM [ExpenseCategory] where BusinessType = 1">
                        </asp:SqlDataSource>
                    </div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label3" Text="<%$Resources:Common,Form_ExpenseSubCategory %>" runat="server" />
                        <span class="requiredLable">*</span>
                    </div>
                    <div>
                        <asp:DropDownList ID="ExpenseSubCategoryDDL" runat="server" DataSourceID="odsExpenseSubCategory"
                            DataTextField="ExpenseSubCategoryName" DataValueField="ExpenseSubCategoryID" Width="180px">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="odsExpenseSubCategory" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                            SelectCommand=" select 0 ExpenseSubCategoryID, ' Please select' ExpenseSubCategoryName">
                        </asp:SqlDataSource>
                    </div>
                </td>
                <td style="width: 120px">
                    <div class="field_title">
                        <asp:Label ID="Label4" Text="<%$Resources:Common,Form_Currency %>" runat="server" />
                        <span class="requiredLable">*</span>
                    </div>
                    <div>
                        <asp:DropDownList ID="CurrencyDDL" runat="server" DataSourceID="odsCurrency" DataTextField="CurrencyShortName"
                            DataValueField="CurrencyID" Width="100px" >
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="odsCurrency" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                            SelectCommand=" SELECT CurrencyID, CurrencyShortName FROM [Currency] where IsActive = 1 order by CurrencyID">
                        </asp:SqlDataSource>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div style="margin-top: 20px; width: 1250px; text-align: right">
        <asp:Button ID="NextButton" runat="server" CssClass="button_nor" Text="<%$Resources:Common,Button_Next %>" OnClick="NextButton_Click"
            Width="100px" />&nbsp;&nbsp;
    </div>
    <br />
</asp:Content>
