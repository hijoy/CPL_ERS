<%@ Page Title="" Language="C#" MasterPageFile="~/DialogMasterPage.master" AutoEventWireup="true"
    CodeFile="POSearch_MultiApply.aspx.cs" Culture="auto" UICulture="auto" Inherits="Dialog_POSearch_MultiApply" %>

<%@ Implements Interface="System.Web.UI.IPostBackEventHandler" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphTop" runat="Server">
    <script language="javascript">
        var _oldColor;
        function SetNewColor(source) {
            _oldColor = source.style.backgroundColor;
            source.style.backgroundColor = '#C0C0C0';
        }

        function SetOldColor(source) {
            source.style.backgroundColor = _oldColor;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div class="title1">
        <asp:Label ID="Label2" runat="server" meta:resourcekey="Label_Title" /></div>
    <gc:GridView CssClass="GridView" ID="gvPO" runat="server" DataSourceID="odsPO" AutoGenerateColumns="False"
        DataKeyNames="FormID" AllowPaging="True" AllowSorting="True" PageSize="20" OnRowDataBound="gvPO_RowDataBound">
        <Columns>
            <asp:TemplateField Visible="False">
                <ItemTemplate>
                    <asp:Label ID="lblFormApplyID" runat="server" Text='<%# Bind("FormID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField SortExpression="FormNo" HeaderText="<%$Resources:Common,Form_FormNo %>">
                <ItemTemplate>
                    <asp:Label ID="lblFormNo" runat="server" Text='<%# Bind("FormNo") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="90px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="BPCSPONo" HeaderText="BPCS PO No">
                <ItemTemplate>
                    <asp:Label ID="lblBPCSPONo" runat="server" Text='<%# Eval("BPCSPONo") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="90px" HorizontalAlign="Center" />
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
                <ItemStyle Width="120px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="CurrencyShortName" HeaderText="<%$Resources:Common,Form_Currency %>">
                <ItemTemplate>
                    <asp:Label ID="lblCurrencyShortName" runat="server" Text='<%# Eval("CurrencyShortName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="35px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="FormPO.AmountRMB" HeaderText="<%$Resources:Common,Form_AmountRMB %>">
                <ItemTemplate>
                    <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("AmountRMB","{0:N}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="80px" HorizontalAlign="right" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="FormPO.FPeriod" HeaderText="<%$Resources:Common,Form_Period %>">
                <ItemTemplate>
                    <asp:Label ID="lblPeriod" Text='<%# Bind("FPeriod", "{0:yyyyMM}") %>' runat="server"></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="60px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="PaymentTerms" HeaderText="<%$Resources:Common,Form_PaymentTerms %>">
                <ItemTemplate>
                    <asp:Label ID="lblPaymentTerms" runat="server" Text='<%# Bind("PaymentTerms") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="35px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="SubmitDate" HeaderText="<%$Resources:Common,Form_SubmitDate %>">
                <ItemTemplate>
                    <asp:Label ID="lblSubmitDate" runat="server" Text='<%# Bind("SubmitDate", "{0:yyyy-MM-dd}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="75px" HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
        <HeaderStyle CssClass="Header" />
        <EmptyDataTemplate>
            <table>
                <tr class="Header">
                    <td style="width: 90px;" class="Empty1">
                        <asp:Label ID="Label1" runat="server" Text="<%$Resources:Common,Form_FormNo %>" />
                    </td>
                    <td style="width: 90px;">
                        <asp:Label ID="Label6" runat="server" Text="BPCS PO No" />
                    </td>
                    <td style="width: 210px;">
                        <asp:Label ID="Label3" runat="server" Text="<%$Resources:Common,Form_VendorName %>" />
                    </td>
                    <td style="width: 120px;">
                        <asp:Label ID="Label4" runat="server" Text="<%$Resources:Common,Form_ItemCategory %>" />
                    </td>
                    <td style="width: 35px;">
                        <asp:Label ID="Label5" runat="server" Text="<%$Resources:Common,Form_Currency %>" />
                    </td>
                    <td style="width: 80px;">
                        <asp:Label ID="Label8" runat="server" Text="<%$Resources:Common,Form_AmountRMB %>" />
                    </td>
                    <td style="width: 60px;">
                        <asp:Label ID="Label9" runat="server" Text="<%$Resources:Common,Form_Period %>" />
                    </td>
                    <td style="width: 35px;">
                        <asp:Label ID="Label11" runat="server" Text="<%$Resources:Common,Form_PaymentTerms %>" />
                    </td>
                    <td style="width: 75px;">
                        <asp:Label ID="Label13" runat="server" Text="<%$Resources:Common,Form_SubmitDate %>" />
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
    <asp:ObjectDataSource ID="odsPO" runat="server" TypeName="BusinessObjects.FormQueryBLL"
        SelectMethod="GetPagedPOViewBySettlementID" EnablePaging="True" SelectCountMethod="QueryPOViewCountBySettlementID"
        SortParameterName="sortExpression">
        <SelectParameters>
            <asp:Parameter Name="SettlementID" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource> 
</asp:Content>
