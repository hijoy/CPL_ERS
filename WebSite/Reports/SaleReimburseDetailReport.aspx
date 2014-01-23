<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="SaleReimburseDetailReport.aspx.cs" Inherits="SaleReimburseDetail"  Culture="Auto" UICulture="Auto"%>

<%@ Register Src="~/UserControls/UCDateInput.ascx" TagName="UCDateInput" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/OUSelect.ascx" TagName="OUSelect" TagPrefix="uc2" %>
<%@ Register Src="~/UserControls/VendorControl.ascx" TagName="UCVendor" TagPrefix="uc3" %>
<%@ Register Src="~/UserControls/YearAndMonthUserControl.ascx" TagName="YearAndMonthUserControl" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" src="../Script/js.js" type="text/javascript"></script>
    <script language="javascript" src="../Script/DateInput.js" type="text/javascript"></script>
    <div class="title" ><asp:Label ID="Label_SearchCondition" runat="server" Text="<%$Resources:Common,Label_SearchCondition %>" /></div>
    <div class="searchDiv">
        <table class="searchTable" cellpadding="0" cellspacing="0" width="1240px">
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_Period" runat="server" Text="<%$Resources:Common,Form_Period %>" />
                    </div>
                    <uc1:UCDateInput ID="UCDateInputBeginDate" runat="server" IsReadOnly="false" />
                </td>
                <td style="width: 200px">
                </td>
                <td  colspan="2" style="width: 400px">
                </td>
                <td colspan="2" style="width: 400px">
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
    <div class="title" >
        <asp:Label ID="Label1" runat="server" meta:resourcekey="Label_Title" />
    </div>
    <gc:GridView CssClass="GridView" ID="gvApplyList" runat="server" 
        AutoGenerateColumns="false" DataKeyNames="FormNO" AllowPaging="false">
        <Columns>
            <asp:TemplateField Visible="False">
                <ItemTemplate>
                    <asp:Label ID="lblFormApplyNo" runat="server" Text='<%# Bind("FormNo") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Common,Form_FormNo %>">
                <ItemTemplate>
                     <asp:Label ID="lblFormNo" runat="server" Text='<%# Bind("FormNo") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="100px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Common,Form_ApplyUser %>">
                <ItemTemplate>
                    <asp:Label ID="lblStuffName" runat="server" Text='<%# Bind("StuffName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="100px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Common,Form_Organization %>">
                <ItemTemplate>
                    <asp:Label ID="lblOrganizationUnitName" runat="server" Text='<%# Bind("OrganizationUnitName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="60px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Common,Form_Period %>">
                <ItemTemplate>
                    <asp:Label ID="lblFPeriod" runat="server" Text='<%# Bind("FPeriod") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="200px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField meta:resourcekey="TemplateField_ExpenseCategory" >
                <ItemTemplate>
                    <asp:Label ID="lblExpenseCategoryName" runat="server" Text='<%# Bind("ExpenseCategoryName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="150px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Common,Form_ExpenseSubCategory %>">
                <ItemTemplate>
                    <asp:Label ID="lblExpenseSubCategoryName" runat="server" Text='<%# Bind("ExpenseSubCategoryName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="80px" HorizontalAlign="right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Common,Form_CustomerChannel %>">
                <ItemTemplate>
                    <asp:Label ID="lblCustomerChannelName" Text='<%# Bind("CustomerChannelName") %>' runat="server"></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="60px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Common,Form_Brand %>">
                <ItemTemplate>
                    <asp:Label ID="lblBrandName" runat="server" Text='<%# Bind("BrandName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="140px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Common,Form_CustomerRegion %>">
                <ItemTemplate>
                    <asp:Label ID="lblProvinceName" runat="server" Text='<%# Bind("ProvinceName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="60px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Common,Form_City %>">
                <ItemTemplate>
                    <asp:Label ID="lblCityName" runat="server" Text='<%# Bind("CityName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="70px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField meta:resourcekey="TemplateField_ShopName">
                <ItemTemplate>
                    <asp:Label ID="lblShopName" runat="server" Text='<%# Bind("ShopName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="80px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField meta:resourcekey="TemplateField_ShopCount">
                <ItemTemplate>
                    <asp:Label ID="lblShopCount" runat="server" Text='<%# Bind("ShopCount") %>' />
                </ItemTemplate>
                <ItemStyle Width="60px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField meta:resourcekey="TemplateField_DisplayType">
                <ItemTemplate>
                    <asp:Label ID="lblDisplayTypeName" Text='<%# Bind("DisplayTypeName") %>'
                        runat="server"></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="60px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField meta:resourcekey="TemplateField_DisplayArea">
                <ItemTemplate>
                    <asp:Label ID="lblDisplayArea" runat="server" Text='<%# Bind("DisplayArea") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="70px" HorizontalAlign="right" />
            </asp:TemplateField>
            <asp:TemplateField meta:resourcekey="TemplateField_IsDM">
                <ItemTemplate>
                    <asp:Label ID="lblIsDM" runat="server" Text='<%# Bind("IsDM") %>'/>
                </ItemTemplate>
                <ItemStyle Width="60px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField meta:resourcekey="TemplateField_DiscountType">
                <ItemTemplate>
                    <asp:Label ID="lblDiscountTypeName" runat="server" Text='<%# Bind("DiscountTypeName") %>'/>
                </ItemTemplate>
                <ItemStyle Width="40px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField meta:resourcekey="TemplateField_ActivityBeginDate">
                <ItemTemplate>
                    <asp:Label ID="lblActivityBeginDate" runat="server" Text='<%# Bind("ActivityBeginDate") %>'/>
                </ItemTemplate>
                <ItemStyle Width="40px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField meta:resourcekey="TemplateField_ActivityEndDate">
                <ItemTemplate>
                    <asp:Label ID="lblActivityEndDate" runat="server" Text='<%# Bind("ActivityEndDate") %>'/>
                </ItemTemplate>
                <ItemStyle Width="40px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField meta:resourcekey="TemplateField_IsClose">
                <ItemTemplate>
                    <asp:Label ID="lblisClose" runat="server" Text='<%# Bind("isClose") %>'/>
                </ItemTemplate>
                <ItemStyle Width="40px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField meta:resourcekey="TemplateField_IsCompleted">
                <ItemTemplate>
                    <asp:Label ID="lblIsCompleted" runat="server" Text='<%# Bind("IsCompleted") %>'/>
                </ItemTemplate>
                <ItemStyle Width="40px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Common,Form_ExpenseItem %>">
                <ItemTemplate>
                    <asp:Label ID="lblExpenseItemName" runat="server" Text='<%# Bind("ExpenseItemName") %>'/>
                </ItemTemplate>
                <ItemStyle Width="40px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Common,Form_AmountRMB %>">
                <ItemTemplate>
                    <asp:Label ID="lblAmountRMB" runat="server" Text='<%# Bind("AmountRMB") %>'/>
                </ItemTemplate>
                <ItemStyle Width="40px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField meta:resourcekey="TemplateField_RemainAmount">
                <ItemTemplate>
                    <asp:Label ID="lblRemainAmount" runat="server" Text='<%# Bind("RemainAmount") %>'/>
                </ItemTemplate>
                <ItemStyle Width="40px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField meta:resourcekey="TemplateField_ApplySubmitDate">
                <ItemTemplate>
                    <asp:Label ID="lblApplySubmitDate" runat="server" Text='<%# Bind("ApplySubmitDate") %>'/>
                </ItemTemplate>
                <ItemStyle Width="40px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField meta:resourcekey="TemplateField_PaymentSubmitDate">
                <ItemTemplate>
                    <asp:Label ID="lblSalePaymentSubmitDate" runat="server" Text='<%# Bind("SalePaymentSubmitDate") %>'/>
                </ItemTemplate>
                <ItemStyle Width="40px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField meta:resourcekey="TemplateField_PaymentStuff">
                <ItemTemplate>
                    <asp:Label ID="lblSalePaymentStuffName" runat="server" Text='<%# Bind("SalePaymentStuffName") %>'/>
                </ItemTemplate>
                <ItemStyle Width="40px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField meta:resourcekey="TemplateField_PaymentFormNo">
                <ItemTemplate>
                    <asp:Label ID="lblSalePaymentFormNo" runat="server" Text='<%# Bind("SalePaymentFormNo") %>'/>
                </ItemTemplate>
                <ItemStyle Width="40px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField meta:resourcekey="TemplateField_IsAdvanced">
                <ItemTemplate>
                    <asp:Label ID="lblIsAdvanced" runat="server" Text='<%# Bind("IsAdvanced") %>'/>
                </ItemTemplate>
                <ItemStyle Width="40px" HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
        <HeaderStyle CssClass="Header" />
        <EmptyDataTemplate>
            <table>
                <tr class="Header">
                    <td style="width: 120px;" class="Empty1">
                        <asp:Label ID="Label1" runat="server" Text="<%$Resources:Common,Form_FormNo %>" />
                    </td>
                    <td style="width: 60px;">
                        <asp:Label ID="Label2" runat="server" Text="<%$Resources:Common,Form_ApplyUser %>" />
                    </td>
                    <td style="width: 200px;">
                        <asp:Label ID="Label3" runat="server" Text="<%$Resources:Common,Form_Organization %>" />
                    </td>
                    <td style="width: 100px;">
                        <asp:Label ID="Label4" runat="server" Text="<%$Resources:Common,Form_Period %>" />
                    </td>
                    <td style="width: 60px;">
                        <asp:Label ID="Label5" runat="server" meta:resourcekey="Label_ExpenseCategory"/>
                    </td>
                    <td style="width: 60px;">
                       <asp:Label ID="Label6" runat="server" Text="<%$Resources:Common,Form_ExpenseSubCategory %>" />
                    </td>
                    <td style="width: 80px;">
                        <asp:Label ID="Label7" runat="server" Text="<%$Resources:Common,Form_CustomerChannel %>" />
                    </td>
                    <td style="width: 100px;">
                       <asp:Label ID="Label8" runat="server" Text="<%$Resources:Common,Form_Brand %>" />
                    </td>
                    <td style="width: 60px;">
                        <asp:Label ID="Label9" runat="server" Text="<%$Resources:Common,Form_CustomerRegion %>" />
                    </td>
                    <td style="width: 150px;">
                        <asp:Label ID="Label10" runat="server" Text="<%$Resources:Common,Form_City %>" />
                    </td>
                    <td style="width: 150px;">
                        <asp:Label ID="Label11" runat="server" meta:resourcekey="Label_ShopName"/>
                    </td>
                    <td style="width: 60px;">
                        <asp:Label ID="Label12" runat="server" meta:resourcekey="Label_ShopCount"/>
                    </td>
                    <td style="width: 80px;">
                        <asp:Label ID="Label13" runat="server" meta:resourcekey="Label_DisplayType"/>
                    </td>
                    <td style="width: 80px;">
                        <asp:Label ID="Label14" runat="server" meta:resourcekey="Label_DisplayArea" />
                    </td>
                    <td style="width: 80px;">
                        <asp:Label ID="Label15" runat="server" meta:resourcekey="Label_IsDM" />
                    </td>
                    <td style="width: 80px;">
                        <asp:Label ID="Label16" runat="server" meta:resourcekey="Label_DiscountType" />
                    </td>
                    <td style="width: 80px;">
                        <asp:Label ID="Label17" runat="server" meta:resourcekey="Label_ActivityBeginDate" />
                    </td>
                    <td style="width: 80px;">
                        <asp:Label ID="Label18" runat="server" meta:resourcekey="Label_ActivityEndDate" />
                    </td>
                    <td style="width: 80px;">
                        <asp:Label ID="Label19" runat="server" meta:resourcekey="Label_IsClose" />
                    </td>
                    <td style="width: 80px;">
                        <asp:Label ID="Label20" runat="server" meta:resourcekey="Label_IsCompleted" />
                    </td>
                    <td style="width: 80px;">
                        <asp:Label ID="Label21" runat="server"  Text="<%$Resources:Common,Form_ExpenseItem %>"/>
                    </td>
                    <td style="width: 80px;">
                        <asp:Label ID="Label22" runat="server"  Text="<%$Resources:Common,Form_AmountRMB %>"/>
                    </td>
                    <td style="width: 80px;">
                        <asp:Label ID="Label23" runat="server" meta:resourcekey="Label_RemainAmount" />
                    </td>
                    <td style="width: 80px;">
                        <asp:Label ID="Label24" runat="server" meta:resourcekey="Label_ApplySubmitDate" />
                    </td>
                    <td style="width: 80px;">
                        <asp:Label ID="Label25" runat="server" meta:resourcekey="Label_PaymentSubmitDate" />
                    </td>
                    <td style="width: 80px;">
                        <asp:Label ID="Label26" runat="server" meta:resourcekey="Label_PaymentStuff" />
                    </td>
                    <td style="width: 80px;">
                        <asp:Label ID="Label27" runat="server" meta:resourcekey="Label_PaymentFormNo" />
                    </td>
                    <td style="width: 80px;">
                        <asp:Label ID="Label28" runat="server" meta:resourcekey="Label_IsAdvanced" />
                    </td>
                </tr>
                <tr>
                    <td colspan="28" class="Empty2 noneLabel">
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
            <asp:BoundColumn HeaderText="<%$Resources:Common,Form_ApplyUser %>" DataField="StuffName" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="<%$Resources:Common,Form_Organization %>" DataField="OrganizationUnitName" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="<%$Resources:Common,Form_Period %>" DataField="FPeriod" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn meta:resourcekey="TemplateField_ExpenseCategory" DataField="ExpenseCategoryName" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="<%$Resources:Common,Form_ExpenseSubCategory %>" DataField="ExpenseSubCategoryName" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="<%$Resources:Common,Form_CustomerChannel %>" DataField="CustomerChannelName" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="<%$Resources:Common,Form_Brand %>" DataField="BrandName" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="<%$Resources:Common,Form_CustomerRegion %>" DataField="ProvinceName" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="<%$Resources:Common,Form_City %>" DataField="CityName" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn meta:resourcekey="TemplateField_ShopName" DataField="ShopName" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn meta:resourcekey="TemplateField_ShopCount" DataField="ShopCount" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn meta:resourcekey="TemplateField_DisplayType" DataField="DisplayTypeName" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn meta:resourcekey="TemplateField_DisplayArea" DataField="DisplayArea" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn meta:resourcekey="TemplateField_IsDM" DataField="IsDM" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn meta:resourcekey="TemplateField_DiscountType" DataField="DiscountTypeName" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn meta:resourcekey="TemplateField_ActivityBeginDate" DataField="ActivityBeginDate" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn meta:resourcekey="TemplateField_ActivityEndDate" DataField="ActivityEndDate" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn meta:resourcekey="TemplateField_IsClose" DataField="isClose" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn meta:resourcekey="TemplateField_IsCompleted" DataField="IsCompleted" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="<%$Resources:Common,Form_ExpenseItem %>" DataField="ExpenseItemName" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="<%$Resources:Common,Form_AmountRMB %>" DataField="AmountRMB" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn meta:resourcekey="TemplateField_RemainAmount" DataField="RemainAmount" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn meta:resourcekey="TemplateField_ApplySubmitDate" DataField="ApplySubmitDate" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn meta:resourcekey="TemplateField_PaymentSubmitDate" DataField="SalePaymentSubmitDate" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn meta:resourcekey="TemplateField_PaymentStuff" DataField="SalePaymentStuffName" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn meta:resourcekey="TemplateField_PaymentFormNo" DataField="SalePaymentFormNo" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn meta:resourcekey="TemplateField_IsAdvanced" DataField="IsAdvanced" HeaderStyle-Font-Bold="true" />
        </Columns>
    </asp:DataGrid>
    <asp:ObjectDataSource ID="odsApplyList" runat="server" TypeName="BusinessObjects.ReportQueryBLL"
        SelectMethod="GetPagedSaleReimburseDetailViewByRight" >
        <SelectParameters>
            <asp:Parameter Name="Period" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
