<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="RefSalePaymentList.aspx.cs" Culture="Auto" UICulture="Auto" Inherits="FormSale_RefSalePaymentList"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" src="../Script/js.js" type="text/javascript"></script>
    <script language="javascript" src="../Script/DateInput.js" type="text/javascript"></script>
    <div class="title" ><asp:Label ID="Label_SearchCondition" runat="server" Text="reference Payment List " /></div>
    <div class="searchDiv">
        <table class="searchTable" cellpadding="0" cellspacing="0" width="1240px">
            <tr id="ApplyTR" runat="server" style="vertical-align: top; height: 40px">
                <td style="width: 200px;">
                    <div class="field_title">申请单编号</div>
                    <asp:TextBox ID="txtApplyFormNo" MaxLength="20" runat="server" Width="170px" ReadOnly="true"></asp:TextBox>
                </td>
                <td style="width: 200px;">
                    <div class="field_title">客户</div>
                    <asp:TextBox ID="txtApplyCustomer" MaxLength="20" runat="server" Width="170px" ReadOnly="true"></asp:TextBox>
                </td>
                <td style="width: 200px;">
                    <div class="field_title">费用小类</div>
                    <asp:TextBox ID="txtApplyCategory" MaxLength="50" runat="server" Width="170px" ReadOnly="true"></asp:TextBox>
                </td>
                <td style="width: 200px;">
                    <div class="field_title">申请金额</div>
                    <asp:TextBox ID="txtApplyAmount" MaxLength="50" runat="server" Width="170px" ReadOnly="true"></asp:TextBox>
                </td>
                <td style="width: 200px;">
                    <div class="field_title">方案名称</div>
                    <asp:TextBox ID="txtApplyProjectName" MaxLength="50" runat="server" Width="170px" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr id="SettlementTR" runat="server" style="vertical-align: top; height: 40px">
                <td style="width: 200px;">
                    <div class="field_title">结案单编号</div>
                    <asp:TextBox ID="txtSettleFormNo" MaxLength="20" runat="server" Width="170px" ReadOnly="true"></asp:TextBox>
                </td>
                <td style="width: 200px;">
                    <div class="field_title">客户</div>
                    <asp:TextBox ID="txtSettleCustomer" MaxLength="20" runat="server" Width="170px" ReadOnly="true"></asp:TextBox>
                </td>
                <td style="width: 200px;">
                    <div class="field_title">
                        费用小类</div>
                    <asp:TextBox ID="txtSettleCategory" MaxLength="50" runat="server" Width="170px" ReadOnly="true"></asp:TextBox>
                </td>
                <td style="width: 200px;">
                    <div class="field_title">
                        结案金额</div>
                    <asp:TextBox ID="txtSettleAmount" MaxLength="50" runat="server" Width="170px" ReadOnly="true"></asp:TextBox>
                </td>
                <td style="width: 200px;">
                    <div class="field_title">
                        品牌</div>
                    <asp:TextBox ID="txtSettleBrand" MaxLength="50" runat="server" Width="170px" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>

        </table>
    </div>

    <br />
    <div class="title" ><asp:Label ID="Label_Title" runat="server" meta:resourcekey="Label_Title" /></div>
    <gc:GridView CssClass="GridView" ID="gvPaymentList" runat="server" DataSourceID="odsPaymentList" AutoGenerateColumns="False" DataKeyNames="FormID" AllowPaging="True"
            AllowSorting="True" PageSize="20" OnRowDataBound="gvPaymentList_RowDataBound" >
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
                <ItemStyle Width="140px" HorizontalAlign="Center" />
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
            <asp:TemplateField SortExpression="CurrencyID" HeaderText="<%$Resources:Common,Form_Currency %>">
                <ItemTemplate>
                    <asp:Label ID="lblCurrency" runat="server" Text='<%# Bind("CurrencyShortName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="60px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="AmountRMB" HeaderText="报销金额">
                <ItemTemplate>
                    <asp:Label ID="lblAmountRMB" runat="server" Text='<%# Bind("AmountRMB","{0:N}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="100px" HorizontalAlign="right" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="PaymentTypeName" HeaderText="<%$Resources:Common,Form_PaymentType %>">
                <ItemTemplate>
                    <asp:Label ID="lblPaymentType" runat="server" Text='<%# Bind("PaymentTypeName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="60px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="InvoiceStatusName" HeaderText="<%$Resources:Common,Form_InvoiceStatus %>">
                <ItemTemplate>
                    <asp:Label ID="lblInvoiceStatusName" runat="server" Text='<%# Bind("InvoiceStatusName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="60px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="IsAdvanced" HeaderText="是否预付款">
                <ItemTemplate>
                    <asp:CheckBox ID="ckIsAdvanced" runat="server" Checked='<%# Bind("IsAdvanced") %>' Enabled="false" />
                </ItemTemplate>
                <ItemStyle Width="60px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField  HeaderText="<%$Resources:Common,Form_ApplyUser %>">
                <ItemTemplate>
                    <asp:Label ID="lblStuffName" runat="server" Text='<%# Eval("StuffName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="80px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="SubmitDate" HeaderText="<%$Resources:Common,Form_SubmitDate %>">
                <ItemTemplate>
                    <asp:Label ID="lblSubmitDate" runat="server" Text='<%# Bind("SubmitDate", "{0:yyyy-MM-dd}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="80px" HorizontalAlign="Center" />
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
                    <td style="width: 220px;">
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
                        <asp:Label ID="Label8" runat="server" HeaderText="报销金额" />
                    </td>
                    <td style="width: 60px;">
                        <asp:Label ID="Label9" runat="server" Text="<%$Resources:Common,Form_PaymentType %>" />
                    </td>
                    <td style="width: 60px;">
                        <asp:Label ID="Label10" runat="server" Text="<%$Resources:Common,Form_InvoiceStatus %>" />
                    </td>
                    <td style="width: 60px;">
                        <asp:Label ID="Label11" runat="server" HeaderText="是否预付款" />
                    </td>
                    <td style="width: 80px;">
                        <asp:Label ID="Label12" runat="server" Text="<%$Resources:Common,Form_ApplyUser %>" />
                    </td>
                    <td style="width: 80px;">
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
    <asp:ObjectDataSource ID="odsPaymentList" runat="server" TypeName="BusinessObjects.FormQueryBLL"
        SelectMethod="GetPagedFormSalePaymentViewByRight" EnablePaging="True" SelectCountMethod="QueryFormSalePaymentViewCountByRight"
        SortParameterName="sortExpression">
        <SelectParameters>
            <asp:Parameter Name="queryExpression" Type="String" />
            <asp:Parameter Name="UserID" Type="Int32" />
            <asp:Parameter Name="PositionID" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>

</asp:Content>
