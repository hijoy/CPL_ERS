﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="PVSpecialApplyFirst.aspx.cs" Inherits="FormPurchase_PVSpecialApplyFirst" Culture="Auto"
    UICulture="Auto" %>

<%@ Register Src="../UserControls/VendorControl.ascx" TagName="UCVendor" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/ItemCategoryControl.ascx" TagName="UCItemCategory" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <script src="../Script/DateInput.js" type="text/javascript"></script>
    <div class="title">
        <asp:Label ID="Label_Title"  runat="server" meta:resourcekey="Label_Title" /></div>
    <div style="width: 1220px; background-color: #F6F6F6;padding-left:20px">
        <table style="padding-left:20px">
            <tr>
                <td style="width: 240px">
                    <div class="field_title">
                        <asp:Label ID="Form_Period" Text="<%$Resources:Common,Form_Period %>" runat="server" />
                        <span class="requiredLable">*</span>
                    </div>
                    <div>
                        <asp:DropDownList ID="PeriodDDL" runat="server" DataSourceID="odsPeriod" DataTextField="PeriodPurchase" DataValueField="PeriodPurchaseID" Width="200px" >
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="odsPeriod" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                            SelectCommand=" select 0 PeriodPurchaseID, ' Please select' PeriodPurchase Union SELECT PeriodPurchaseID, convert(varchar(50),year(PeriodPurchase))+'-'+convert(varchar(50),month(PeriodPurchase)) PeriodPurchase FROM [PeriodPurchase] ">
                        </asp:SqlDataSource>
                    </div>
                </td>
                <td style="width: 280px">
                    <div class="field_title">
                        <asp:Label ID="Form_VendorName" Text="<%$Resources:Common,Form_VendorName %>" runat="server" />
                        <span class="requiredLable">*</span>
                    </div>
                    <div>
                        <uc1:UCVendor ID="UCVendor" runat="server" Width="220px" IsNoClear="true" IsLimited="true"/>
                    </div>
                </td>
                <td style="width: 360px">
                    <div class="field_title">
                        <asp:Label ID="Form_ItemCategory" Text="<%$Resources:Common,Form_ItemCategory %>" runat="server" />
                        <span class="requiredLable">*</span>
                    </div>
                    <div>
                        <uc2:UCItemCategory ID="UCItemCategory" runat="server" Width="220px" IsNoClear="true"/>  
                    </div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_BudgetType" Text="<%$Resources:Common,Form_BudgetType %>" runat="server" />
                        <span class="requiredLable">*</span>
                    </div>
                    <div>
                        <asp:DropDownList ID="PurchaseBudgetTypeDDL" runat="server" DataSourceID="odsPurchaseBudgetTypeID" 
                                DataTextField="PurchaseBudgetTypeName" DataValueField="PurchaseBudgetTypeID" Width="170px" Enabled="false">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="odsPurchaseBudgetTypeID" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                            SelectCommand=" SELECT PurchaseBudgetTypeID, PurchaseBudgetTypeName FROM [PurchaseBudgetType] order by PurchaseBudgetTypeID desc">
                        </asp:SqlDataSource>
                    </div>
                </td>
                <td style="width: 120px">
                    <div class="field_title">
                        <asp:Label ID="Form_Currency" Text="<%$Resources:Common,Form_Currency %>" runat="server" />
                        <span class="requiredLable">*</span>
                    </div>
                    <div>
                        <asp:DropDownList ID="CurrencyDDL" runat="server" DataSourceID="odsCurrency" DataTextField="CurrencyShortName" DataValueField="CurrencyID" Width="80px" >
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
