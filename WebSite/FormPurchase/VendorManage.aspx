<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="VendorManage.aspx.cs" Culture="Auto" UICulture="Auto" Inherits="FormPurchase_VendorManage" %>

<%@ Register Src="~/UserControls/UCDateInput.ascx" TagName="UCDateInput" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/OUSelect.ascx" TagName="OUSelect" TagPrefix="uc2" %>
<%@ Register Src="~/UserControls/YearAndMonthUserControl.ascx" TagName="YearAndMonthUserControl"
    TagPrefix="uc4" %>
<%@ Register Src="../UserControls/VendorTypeControl.ascx" TagName="VendorTypeControl"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <script src="../Script/DateInput.js" type="text/javascript"></script>
    <div class="title">
        <asp:Label ID="Label_SearchCondition" runat="server" Text="<%$Resources:Common,Label_SearchCondition %>" /></div>
    <div class="searchDiv">
        <table class="searchTable" cellpadding="0" cellspacing="0">
            <tr style="vertical-align: top; height: 40px">
                <td style="width: 200px;">
                    <div class="field_title">
                        <asp:Label ID="Label_VendorCode" runat="server" meta:resourcekey="Label_VendorCode" />
                    </div>
                    <asp:TextBox ID="txtVendorCode" MaxLength="50" runat="server" Width="170px"></asp:TextBox>
                </td>
                <td style="width: 200px;">
                    <div class="field_title">
                        <asp:Label ID="Form_VendorName" runat="server" Text="<%$Resources:Common,Form_VendorName %>" /></div>
                    <asp:TextBox ID="txtVendorName" MaxLength="20" runat="server" Width="170px"></asp:TextBox>
                </td>
                <td colspan="2" style="width: 400px;">
                    <div class="field_title">
                        <asp:Label ID="Label1" runat="server" meta:resourcekey="Label_VendorType" />
                    </div>
                    <uc1:VendorTypeControl ID="VendorTypeControl" runat="server" Width="270px" />
                </td>
                <td style="width: 200px;">
                    <div class="field_title">
                        <asp:Label ID="Form_PaymentTerms" runat="server" Text="<%$Resources:Common,Form_PaymentTerms %>" />
                    </div>
                    <asp:DropDownList ID="ddlPaymentTerm" runat="server" DataSourceID="sdsPaymentTerm"
                        DataTextField="PaymentTermName" DataValueField="PaymentTermID" Width="170px">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="sdsPaymentTerm" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                        SelectCommand="select 0 PaymentTermID,' All' PaymentTermName union select PaymentTermID,PaymentTermName from PaymentTerm">
                    </asp:SqlDataSource>
                </td>
                <td valign="bottom" style="width:200px;">
                    <asp:Button ID="btnSearch" runat="server" CssClass="button_nor" Text="<%$Resources:Common,Button_Search %>"
                        OnClick="btnSearch_Click" />&nbsp;
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div class="title">
        <asp:Label ID="Label_Title" runat="server" meta:resourcekey="Label_Title" />
    </div>
    <gc:GridView CssClass="GridView" ID="gvVendorList" runat="server" DataSourceID="odsVendorList"
        AutoGenerateColumns="False" DataKeyNames="VendorID" AllowPaging="True" AllowSorting="True"
        PageSize="20" OnRowDataBound="gvApplyList_RowDataBound" OnSelectedIndexChanged="gvVendorList_SelectedIndexChanged">
        <Columns>
            <asp:TemplateField Visible="False">
                <ItemTemplate>
                    <asp:Label ID="lblVendorID" runat="server" Text='<%# Bind("VendorID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField meta:resourcekey="TemplateField_VendorCode" SortExpression="VendorCode">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnVendorCode" runat="server" CausesValidation="False" CommandName="Select"
                        Text='<%# Bind("VendorCode") %>'></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle Width="68px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Common,Form_VendorName %>" SortExpression="VendorName">
                <ItemTemplate>
                    <asp:Label ID="lblVendorName" runat="server" Text='<%# Eval("VendorName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="250px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Common,Form_City %>" SortExpression="City">
                <ItemTemplate>
                    <asp:Label ID="lblCity" runat="server" Text='<%# Eval("City") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="100px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField meta:resourcekey="TemplateField_VendorType" SortExpression="VendorTypeID">
                <ItemTemplate>
                    <asp:Label ID="lblVendorTypeName" runat="server" Text='<%# getVendorTypeNameByID(Eval("VendorTypeID")) %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="100px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField meta:resourcekey="TemplateField_ContactName" SortExpression="ContactName">
                <ItemTemplate>
                    <asp:Label ID="lblContactName" runat="server" Text='<%# Eval("ContactName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="100px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Common,Form_PaymentType %>" SortExpression="PaymentTypeID">
                <ItemTemplate>
                    <asp:Label ID="lblMethodPayment" runat="server" Text='<%# getMethodPaymentNameByID(Eval("MethodPaymentID")) %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="100px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Common,Form_PaymentTerms %>" SortExpression="PaymentTerm">
                <ItemTemplate>
                    <asp:Label ID="lblPaymentTerm" runat="server" Text='<%# getPaymentTermNameByID( Eval("PaymentTermID")) %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="100px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="OneTimeVendor" meta:resourcekey="TemplateField_OneTimeVendor">
                <ItemTemplate>
                    <asp:CheckBox runat="server" ID="chkOneTimeVendor" Enabled="false" Checked='<%# Bind("OneTimeVendor") %>' />
                </ItemTemplate>
                <ItemStyle Width="100px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="HoldVendor" meta:resourcekey="TemplateField_HoldVendor">
                <ItemTemplate>
                    <asp:CheckBox runat="server" ID="chkHoldVendor" Enabled="false" Checked='<%# Bind("HoldVendor") %>' />
                </ItemTemplate>
                <ItemStyle Width="70px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="IsActive" HeaderText="<%$Resources:Common,Form_IsActive %>">
                <ItemTemplate>
                    <asp:CheckBox runat="server" ID="chkActiveBy" Enabled="false" Checked='<%# Bind("IsActive") %>' />
                </ItemTemplate>
                <ItemStyle Width="70px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:HyperLink ID="hlDetail" runat="server" CausesValidation="false" Text="<%$Resources:Common,Button_Detail %>"></asp:HyperLink>
                    <asp:LinkButton ID="lbtnEdit" runat="server" CausesValidation="false" CommandName="edit"
                        meta:resourcekey="LinkButton_Modify"></asp:LinkButton>
                    <asp:LinkButton ID="lbtnDelete" runat="server" CausesValidation="false" CommandName="delete"
                        Text="<%$Resources:Common,Button_Delete %>"></asp:LinkButton>
                    <asp:LinkButton ID="lbtnReactive" runat="server" CausesValidation="false" CommandName="active"
                        Text="<%$Resources:Common,Button_Reactive %>"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle Width="160px" HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
        <HeaderStyle CssClass="Header" />
        <EmptyDataTemplate>
            <table>
                <tr class="Header">
                    <td style="width: 68px;">
                        <asp:Label ID="Label1" runat="server" meta:resourcekey="Label_VendorCode" />
                    </td>
                    <td style="width: 180px;">
                        <asp:Label ID="Label2" runat="server" Text="<%$Resources:Common,Form_VendorName %>" />
                    </td>
                    <td style="width: 80px;">
                        <asp:Label ID="Label3" runat="server" Text="<%$Resources:Common,Form_City %>" />
                    </td>
                    <td style="width: 90px;">
                        <asp:Label ID="Label4" runat="server" meta:resourcekey="Label_VendorType" />
                    </td>
                    <td style="width: 100px;">
                        <asp:Label ID="Label6" runat="server" meta:resourcekey="Label_ContactName" />
                    </td>
                    <td style="width: 120px;">
                        <asp:Label ID="Label7" runat="server" meta:resourcekey="Label_Email" />
                    </td>
                    <td style="width: 100px;">
                        <asp:Label ID="Label8" runat="server" Text="<%$Resources:Common,Form_PaymentType %>" />
                    </td>
                    <td style="width: 100px;">
                        <asp:Label ID="Label9" runat="server" Text="<%$Resources:Common,Form_PaymentTerms %>" />
                    </td>
                    <td style="width: 130px;">
                        <asp:Label ID="Label10" runat="server" meta:resourcekey="Label_ExpirationDate" />
                    </td>
                    <td style="width: 80px;">
                        <asp:Label ID="Label11" runat="server" Text="<%$Resources:Common,Form_IsActive %>" />
                    </td>
                    <td style="width: 80px;">
                    </td>
                </tr>
                <tr>
                    <td colspan="12" class="Empty2 noneLabel" align="center">
                        <asp:Label ID="Label12" runat="server" Text="<%$Resources:Common,Label_None %>" />
                    </td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <SelectedRowStyle CssClass="SelectedRow" />
    </gc:GridView>
    <asp:ObjectDataSource ID="odsVendorList" runat="server" TypeName="BusinessObjects.MasterDataBLL"
        SelectMethod="GetVendorPaged" EnablePaging="True" SelectCountMethod="QueryVendorCount"
        SortParameterName="sortExpression">
        <SelectParameters>
            <asp:Parameter Name="queryExpression" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <br />
    <div class="title">
        <asp:Label ID="Label_OpeateHistoryTitle" runat="server" meta:resourcekey="Label_OpeateHistoryTitle" />
    </div>
    <gc:GridView CssClass="GridView" ID="gvHistory" runat="server" DataSourceID="odsHistory"
        AutoGenerateColumns="False" DataKeyNames="FormVendorID" Visible="false" OnRowDataBound="gvHistory_RowDataBound">
        <Columns>
            <asp:TemplateField Visible="False">
                <ItemTemplate>
                    <asp:Label ID="lblVendorID" runat="server" Text='<%# Eval("VendorID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Common,Form_FormNo %>" SortExpression="FormNo">
                <ItemTemplate>
                    <asp:HyperLink ID="lblFormNo" runat="server" Text='<%# Eval("FormNo") %>'></asp:HyperLink>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="120px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Common,Form_ApplyUser %>" SortExpression="StuffName">
                <ItemTemplate>
                    <asp:Label ID="lblStuffUserID" runat="server" Text='<%# Eval("StuffName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="180px" />
            </asp:TemplateField>
            <asp:TemplateField meta:resourcekey="TemplateField_ActionType" SortExpression="ActionType">
                <ItemTemplate>
                    <asp:Label ID="lblActionType" runat="server" Text='<%# Eval("ActionType") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="120px" />
            </asp:TemplateField>
            <asp:TemplateField meta:resourcekey="TemplateField_ActionDate" SortExpression="SubmitDate">
                <ItemTemplate>
                    <asp:Label ID="lblSubmitDate" runat="server" Text='<%# Eval("SubmitDate","{0:yyyy-MM-dd hh:mm}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="180px" />
            </asp:TemplateField>
            <asp:TemplateField meta:resourcekey="TemplateField_ModifyReason" SortExpression="ModifyReason">
                <ItemTemplate>
                    <asp:Label ID="lblModifyReason" runat="server" Text='<%# Eval("ModifyReason") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="635px" />
            </asp:TemplateField>
        </Columns>
        <HeaderStyle CssClass="Header" />
        <EmptyDataTemplate>
            <table>
                <tr>
                    <td style="width: 100%;" colspan="8" class="Empty2 noneLabel" align="center">
                        <asp:Label runat="server" Text="<%$Resources:Common,Label_None %>" />
                    </td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <SelectedRowStyle CssClass="SelectedRow" />
    </gc:GridView>
    <asp:ObjectDataSource ID="odsHistory" runat="server" TypeName="BusinessObjects.FormVendorBLL"
        SelectMethod="GetFormVendorByVendorID" SortParameterName="sortExpression">
        <SelectParameters>
            <asp:Parameter Name="VendorID" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
