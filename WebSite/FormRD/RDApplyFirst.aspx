﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" Culture="auto" UICulture="auto"  AutoEventWireup="true" CodeFile="RDApplyFirst.aspx.cs" Inherits="FormRD_RDApplyFirst" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <script src="../Script/DateInput.js" type="text/javascript"></script>
    <div class="title">
        <asp:Label ID="Label1" runat="server" meta:resourcekey="Label1Resource1" />
    </div>
    <div class="searchDiv">
        <table class="searchTable">
            <tr>
                <td style="width: 240px">
                    <div class="field_title">
                        <asp:Label ID="lblPeriod" Text="<%$ Resources:Common,Form_Period %>" runat="server" /></div>
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
                        <asp:Label ID="Label2" Text="<%$ Resources:Common,Form_Brand %>" runat="server" /></div>
                    <div>
                        <asp:DropDownList ID="ddlBrand" runat="server" DataSourceID="odsBrand" DataTextField="BrandName"
                            DataValueField="BrandID" Width="200px" >
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="odsBrand" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                            SelectCommand=" select 0 BrandID, ' Please select' BrandName Union SELECT [BrandID], BrandName FROM [Brand] order by BrandName">
                        </asp:SqlDataSource>
                    </div>
                </td>
                <td style="width: 240px">
                    <div class="field_title">
                        <asp:Label ID="Label3" Text="<%$ Resources:Common,Form_CustomerChannel %>" runat="server" /></div>
                    <div>
                        <asp:DropDownList ID="ddlCustomerChannel" runat="server" DataSourceID="odsCustomerChannel"
                            DataTextField="CustomerChannelName" DataValueField="CustomerChannelID" 
                            Width="200px" >
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="odsCustomerChannel" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                            SelectCommand=" select 0 CustomerChannelID, ' Please select' CustomerChannelName Union SELECT CustomerChannelID, CustomerChannelName FROM [CustomerChannel] where IsActive = 1  order by CustomerChannelName">
                        </asp:SqlDataSource>
                    </div>
                </td>
                <td style="width: 240px">
                    <div class="field_title">
                        <asp:Label ID="Label_ExpenseSubCategory" runat="server" Text="<%$Resources:Common,Form_ExpenseSubCategory %>" /></div>
                    <asp:DropDownList ID="ExpenseSubCategoryDDL" runat="server" DataSourceID="odsExpenseSubCategory"
                        DataTextField="ExpenseSubCategoryName" DataValueField="ExpenseSubCategoryID" Width="200px">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="odsExpenseSubCategory" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                        SelectCommand=" select 0 ExpenseSubCategoryID, ' Please select' ExpenseSubCategoryName Union SELECT ExpenseSubCategoryID, ExpenseSubCategoryName FROM [ExpenseSubCategory]  where ExpenseCategoryID=62  order by ExpenseSubCategoryName">
                    </asp:SqlDataSource>
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
                <td valign="bottom">
                </td>
            </tr>
        </table>
    </div>
    <div style="margin-top: 20px; width: 1240px; text-align: right">
            <asp:Button ID="NextButton" runat="server" CssClass="button_nor" Text="<%$ Resources:Common,Button_Next %>" OnClick="NextButton_Click"
                Width="100px" />&nbsp;&nbsp;&nbsp;
    </div>
    <br />
</asp:Content>
