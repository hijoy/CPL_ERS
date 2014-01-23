<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="PVList.aspx.cs" Culture="Auto" UICulture="Auto" Inherits="FormQuery_PVList"  %>

<%@ Register Src="~/UserControls/UCDateInput.ascx" TagName="UCDateInput" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/OUSelect.ascx" TagName="OUSelect" TagPrefix="uc2" %>
<%@ Register Src="~/UserControls/VendorControl.ascx" TagName="UCVendor" TagPrefix="uc3" %>
<%@ Register Src="~/UserControls/YearAndMonthUserControl.ascx" TagName="YearAndMonthUserControl" TagPrefix="uc4" %>
<%@ Register Src="../UserControls/ItemCategoryControl.ascx" TagName="UCItemCategory" TagPrefix="uc5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" src="../Script/js.js" type="text/javascript"></script>
    <script language="javascript" src="../Script/DateInput.js" type="text/javascript"></script>
    <div class="title" ><asp:Label ID="Label_SearchCondition" runat="server" Text="<%$Resources:Common,Label_SearchCondition %>" /></div>
    <div class="searchDiv">
        <table class="searchTable" cellpadding="0" cellspacing="0" width="1240px">
            <tr style="vertical-align: top; height: 40px">
                <td style="width: 200px">
                    <div class="field_title">
                       <asp:Label ID="Form_FormNo" runat="server" Text="<%$Resources:Common,Form_FormNo %>" /></div>
                    <asp:TextBox ID="txtFormNo" MaxLength="20" runat="server" Width="170px"></asp:TextBox>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                       <asp:Label ID="Label_ParentFormNo" runat="server" meta:resourcekey="Label_ParentFormNo" /></div>
                    <asp:TextBox ID="txtParentFormNo" MaxLength="20" runat="server" Width="170px"></asp:TextBox>
                </td>
                <td colspan="2" style="width: 400px">
                    <div class="field_title">
                        <asp:Label ID="Form_ItemCategory" runat="server" Text="<%$Resources:Common,Form_ItemCategory %>" /></div>
                    <uc5:UCItemCategory ID="UCItemCategory" runat="server" Width="220px" /> 
                </td>
                <td colspan="2" style="width: 400px">
                    <div class="field_title">
                        <asp:Label ID="Form_Organization" runat="server" Text="<%$Resources:Common,Form_Organization %>" /></div>
                    <uc2:OUSelect ID="UCOU" runat="server" Width="220px" />
                </td>
            </tr>
            <tr>
                <td colspan="2" style="width: 400px">
                    <div class="field_title">
                        <asp:Label ID="Form_VendorName" runat="server" Text="<%$Resources:Common,Form_VendorName %>" /></div>
                        <uc3:UCVendor ID="UCVendor" runat="server"  Width="260px"/>
                </td>
                <td  colspan="2" style="width: 400px">
                    <div class="field_title"><asp:Label ID="Form_Period" runat="server" Text="<%$Resources:Common,Form_Period %>" /></div>
                    <nobr>
                        <uc4:YearAndMonthUserControl ID="UCPeriodBegin" runat="server" IsReadOnly="false" IsExpensePeriod="true" />
                        <asp:Label ID="lblSignPeriod" runat="server">~~</asp:Label>
                        <uc4:YearAndMonthUserControl ID="UCPeriodEnd" runat="server" IsReadOnly="false" IsExpensePeriod="true" />
                    </nobr>
                </td>
                <td colspan="2" style="width: 400px">
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
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_ApplyUser" runat="server" Text="<%$Resources:Common,Form_ApplyUser %>" /></div>
                    <asp:TextBox ID="txtStuffUser" MaxLength="50" runat="server" Width="170px"></asp:TextBox>
                </td>

                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_BudgetType" runat="server" Text="<%$Resources:Common,Form_BudgetType %>" /></div>
                    <div>
                        <asp:DropDownList ID="PurchaseBudgetTypeDDL" runat="server" DataSourceID="odsPurchaseBudgetTypeID" DataTextField="PurchaseBudgetTypeName" DataValueField="PurchaseBudgetTypeID" Width="170px" >
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="odsPurchaseBudgetTypeID" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                            SelectCommand=" select 0 PurchaseBudgetTypeID, ' All' PurchaseBudgetTypeName Union SELECT [PurchaseBudgetTypeID], PurchaseBudgetTypeName FROM [PurchaseBudgetType] order by PurchaseBudgetTypeName">
                        </asp:SqlDataSource>
                    </div>
                </td>
                <td style="width: 200px">
                    <div class="field_title"><asp:Label ID="Form_PurchaseBudgetType" runat="server" Text="<%$Resources:Common,Form_PurchaseBudgetType %>" /></div>
                        <asp:DropDownList ID="PurchaseTypeDDL" runat="server" DataSourceID="odsPurchaseType" DataTextField="PurchaseTypeName" DataValueField="PurchaseTypeID" Width="180px" >
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="odsPurchaseType" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                            SelectCommand=" select 0 PurchaseTypeID, ' All' PurchaseTypeName Union SELECT [PurchaseTypeID], PurchaseTypeName FROM [PurchaseType] order by PurchaseTypeName">
                        </asp:SqlDataSource>
                </td>
                <td style="width: 200px;" >
                    <div class="field_title"><asp:Label ID="Form_PaymentType" runat="server" Text="<%$Resources:Common,Form_PaymentType %>" /></div>
                        <asp:DropDownList ID="MethodPaymentDDL" runat="server" DataSourceID="odsMethodPayment" DataTextField="MethodPaymentName" DataValueField="MethodPaymentID" Width="180px" >
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="odsMethodPayment" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                            SelectCommand="  select 0 MethodPaymentID,' All' MethodPaymentName Union SELECT MethodPaymentID, MethodPaymentName+'-'+Description MethodPaymentName FROM [MethodPayment] where IsActive=1 ">
                        </asp:SqlDataSource>
                </td>
                <td style="width: 200px;" >
                    <div class="field_title"><asp:Label ID="Form_InvoiceStatus" runat="server" Text="<%$Resources:Common,Form_InvoiceStatus %>" /></div>
                    <asp:DropDownList ID="InvoiceStatusDDL" runat="server" DataSourceID="odsInvoiceStatus"
                        DataTextField="Name" DataValueField="InvoiceStatusID" Width="180px">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="odsInvoiceStatus" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                        SelectCommand=" select 0 InvoiceStatusID,' All' Name Union SELECT InvoiceStatusID, Name FROM InvoiceStatus ">
                    </asp:SqlDataSource>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label2" runat="server" Text="<%$Resources:Common,Form_VATRate %>" /></div>
                    <div>
                        <asp:DropDownList ID="VATRateDDL" runat="server" DataSourceID="odsVATRate" DataTextField="VATTypeName" DataValueField="VATTypeID" Width="180px" >
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="odsVATRate" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                            SelectCommand="select 0 VATTypeID,' ALL' VATTypeName Union  SELECT VATTypeID, VATTypeName+'-'+[Description] VATTypeName FROM VATType where IsActive=1 ">
                        </asp:SqlDataSource>
                    </div>
                </td>
            </tr>
            <tr style=" height:40px">
                <td style="width: 600px;" colspan="3" valign="middle">
                    <span class="field_title"><asp:Label ID="Form_FormStatus" runat="server" Text="<%$Resources:Common,Form_FormStatus %>" /></span>&nbsp;&nbsp;
                    <asp:CheckBox ID="chkAwaiting" runat="server" Text="<%$Resources:Common,Form_FormAwaiting %>" Checked="false"></asp:CheckBox>&nbsp;&nbsp;
                    <asp:CheckBox ID="chkApproveCompleted" runat="server" Text="<%$Resources:Common,Form_FormApproveCompleted %>" Checked="false" />&nbsp;&nbsp;
                    <asp:CheckBox ID="chkRejected" runat="server" Text="<%$Resources:Common,Form_FormRejected %>" Checked="false" />&nbsp;&nbsp;
                    <asp:CheckBox ID="chkScrap" runat="server" Text="<%$Resources:Common,Form_Scrap %>" Checked="false" />
                </td>
                <td style="width: 600px;" colspan="3" valign="middle">
                    <span class="field_title"><asp:Label ID="Label_PVType" runat="server" meta:resourcekey="Label_PVType" /></span>&nbsp;&nbsp;
                    <asp:CheckBox ID="chkPR" runat="server" meta:resourcekey="CheckBox_PR" Checked="false"></asp:CheckBox>&nbsp;&nbsp;
                    <asp:CheckBox ID="chkPO" runat="server" meta:resourcekey="CheckBox_PO" Checked="false" />&nbsp;&nbsp;
                    <asp:CheckBox ID="chkNone" runat="server" meta:resourcekey="CheckBox_None" Checked="false" />&nbsp;&nbsp;
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
    <gc:GridView CssClass="GridView" ID="gvApplyList" runat="server" DataSourceID="odsApplyList" AutoGenerateColumns="False" DataKeyNames="FormID" AllowPaging="True"
            AllowSorting="True" PageSize="20" OnRowDataBound="gvApplyList_RowDataBound"  OnRowCommand="gvApplyList_RowCommand">
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
                <ItemStyle Width="60px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="VendorName" HeaderText="<%$Resources:Common,Form_VendorName %>">
                <ItemTemplate>
                    <asp:Label ID="lblVendorName" runat="server" Text='<%# Eval("VendorName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="210px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="ItemCategoryName" HeaderText="<%$Resources:Common,Form_ItemCategory %>">
                <ItemTemplate>
                    <asp:Label ID="lblItemCategoryName" runat="server" Text='<%# Eval("ItemCategoryName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="160px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="CurrencyShortName" HeaderText="<%$Resources:Common,Form_Currency %>">
                <ItemTemplate>
                    <asp:Label ID="lblCurrencyShortName" runat="server" Text='<%# Eval("CurrencyShortName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="50px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="PurchaseBudgetTypeName" HeaderText="<%$Resources:Common,Form_BudgetType %>">
                <ItemTemplate>
                    <asp:Label ID="lblPurchaseBudgetType" runat="server" Text='<%# Eval("PurchaseBudgetTypeName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="75px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="PurchaseTypeName" HeaderText="<%$Resources:Common,Form_PurchaseBudgetType %>">
                <ItemTemplate>
                    <asp:Label ID="lblPurchaseTypeName" runat="server" Text='<%# Eval("PurchaseTypeName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="75px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="ApplyAmount" HeaderText="<%$Resources:Common,Form_AmountRMB %>">
                <ItemTemplate>
                    <asp:Label ID="lblApplyAmount" runat="server" Text='<%# Bind("ApplyAmount","{0:N}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="80px" HorizontalAlign="right" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="FormPV.AmountRMB" meta:resourcekey="TemplateField_Amount">
                <ItemTemplate>
                    <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("AmountRMB","{0:N}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="80px" HorizontalAlign="right" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="FormPV.FPeriod" HeaderText="<%$Resources:Common,Form_Period %>">
                <ItemTemplate>
                    <asp:Label ID="lblPeriod" Text='<%# Bind("FPeriod", "{0:yyyyMM}") %>' runat="server"></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="60px" HorizontalAlign="Center"  />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="MethodPaymentName" HeaderText="<%$Resources:Common,Form_PaymentType %>">
                <ItemTemplate>
                    <asp:Label ID="lblMethodPayment" runat="server" Text='<%# Bind("MethodPaymentName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="60px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="InvoiceStatus.Name" HeaderText="<%$Resources:Common,Form_InvoiceStatus %>">
                <ItemTemplate>
                    <asp:Label ID="lblInvoiceStatusName" runat="server" Text='<%# Bind("InvoiceStatusName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="60px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField  HeaderText="<%$Resources:Common,Form_ApplyUser %>">
                <ItemTemplate>
                    <asp:Label ID="lblStuffName" runat="server" Text='<%# Eval("StuffName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="60px" HorizontalAlign="Center" />
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
                    <td style="width: 60px;">
                        <asp:Label ID="Label2" runat="server" Text="<%$Resources:Common,Form_FormStatus %>" />
                    </td>
                    <td style="width: 210px;">
                        <asp:Label ID="Label3" runat="server" Text="<%$Resources:Common,Form_VendorName %>" />
                    </td>
                    <td style="width: 160px;">
                        <asp:Label ID="Label4" runat="server" Text="<%$Resources:Common,Form_ItemCategory %>" />
                    </td>
                    <td style="width: 50px;">
                        <asp:Label ID="Label5" runat="server" Text="<%$Resources:Common,Form_Currency %>" />
                    </td>
                    <td style="width: 75px;">
                       <asp:Label ID="Label6" runat="server" Text="<%$Resources:Common,Form_BudgetType %>" />
                    </td>
                    <td style="width: 75px;">
                        <asp:Label ID="Label7" runat="server" Text="<%$Resources:Common,Form_PurchaseBudgetType %>" />
                    </td>
                    <td style="width: 80px;">
                       <asp:Label ID="Label8" runat="server" Text="<%$Resources:Common,Form_AmountRMB %>" />
                    </td>
                    <td style="width: 80px;">
                       <asp:Label ID="Label9" runat="server" meta:resourcekey="Label_Amount" />
                    </td>
                    <td style="width: 60px;">
                        <asp:Label ID="Label10" runat="server" Text="<%$Resources:Common,Form_Period %>" />
                    </td>
                    <td style="width: 60px;">
                        <asp:Label ID="Label11" runat="server" Text="<%$Resources:Common,Form_PaymentType %>" />
                    </td>
                    <td style="width: 60px;">
                        <asp:Label ID="Label12" runat="server" Text="<%$Resources:Common,Form_InvoiceStatus %>" />
                    </td>
                    <td style="width: 60px;">
                        <asp:Label ID="Label13" runat="server" Text="<%$Resources:Common,Form_ApplyUser %>" />
                    </td>
                    <td style="width: 75px;">
                        <asp:Label ID="Label14" runat="server" Text="<%$Resources:Common,Form_SubmitDate %>" />
                    </td>
                </tr>
                <tr>
                    <td colspan="14" class="Empty2 noneLabel">
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
            <asp:BoundColumn HeaderText="<%$Resources:Common,Form_VendorName %>" DataField="VendorName" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="<%$Resources:Common,Form_ItemCategory %>" DataField="ItemCategoryName" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="<%$Resources:Common,Form_Currency %>" DataField="CurrencyShortName" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="<%$Resources:Common,Form_BudgetType %>" DataField="PurchaseBudgetTypeName" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="<%$Resources:Common,Form_PurchaseBudgetType %>" DataField="PurchaseTypeName" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="<%$Resources:Common,Form_AmountRMB %>" DataField="ApplyAmount" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn meta:resourcekey="TemplateField_Amount" DataField="AmountRMB" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="<%$Resources:Common,Form_Period %>" DataField="FPeriodShow" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="<%$Resources:Common,Form_PaymentTerms %>" DataField="MethodPaymentName" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="<%$Resources:Common,Form_InvoiceStatus %>" DataField="InvoiceStatusName" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="<%$Resources:Common,Form_ApplyUser %>" DataField="StuffName" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="<%$Resources:Common,Form_SubmitDate %>" DataField="SubmitDateShow" HeaderStyle-Font-Bold="true" />
        </Columns>
    </asp:DataGrid>
    <asp:ObjectDataSource ID="odsApplyList" runat="server" TypeName="BusinessObjects.FormQueryBLL"
        SelectMethod="GetPagedFormPVViewByRight" EnablePaging="True" SelectCountMethod="QueryFormPVViewCountByRight"
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
