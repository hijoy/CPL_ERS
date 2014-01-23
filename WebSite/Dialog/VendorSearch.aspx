<%@ Page Title="" Language="C#" MasterPageFile="~/DialogMasterPage.master" AutoEventWireup="true"
    CodeFile="VendorSearch.aspx.cs" Culture="auto" UICulture="auto" Inherits="Dialog_VendorSearch" %>

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
    <div class="title1" style="width: 842px;">
        <asp:Label ID="Label_SearchCondition" runat="server" Text="<%$Resources:Common,Label_SearchCondition %>" /></div>
    <table style="background-color: #F6F6F6; vertical-align: top; width: 842px;">
        <tr style="vertical-align: top; height: 40px">
            <td style="width: 200px;">
                <div class="field_title">
                    <asp:Label ID="Label_VendorCode" runat="server" meta:resourcekey="Label_VendorCode" />
                </div>
                <asp:TextBox runat="server" ID="txtVendorCodeBySearch" Width="170px"></asp:TextBox>
            </td>
            <td style="width: 200px;">
                <div class="field_title">
                    <asp:Label ID="Form_VendorName" runat="server" Text="<%$Resources:Common,Form_VendorName %>" /></div>
                <asp:TextBox runat="server" ID="txtVendorName" Width="170px"></asp:TextBox>
            </td>
            <td style="width: 200px;">
            </td>
            <td style="width: 50px;">
                &nbsp;
            </td>
            <td colspan="2" align="left" valign="middle">
                <input type="hidden" id="btnclicked" name="btnclicked" value="0" />
                <asp:Button ID="SearchButton" runat="server" CssClass="button_nor" Text="<%$Resources:Common,Button_Search %>" OnClick="SearchButton_Click" />&nbsp;&nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div class="title1" style="width: 842px;">
        <asp:Label ID="Label_Title" runat="server" meta:resourcekey="Label_Title" />
    </div>
    <gc:GridView ID="gvVendor" CssClass="GridView" runat="server" DataSourceID="VendorObjectDataSource"
        AllowPaging="true" PageSize="20" AllowSorting="True" AutoGenerateColumns="False"
        OnRowDataBound="gvVendor_RowDataBound" DataKeyNames="VendorID" CellPadding="0">
        <Columns>
            <asp:TemplateField meta:resourcekey="TemplateField_VendorCode" SortExpression="VendorCode">
                <ItemTemplate>
                    <asp:Label ID="lblVendorCode" runat="server" Text='<%# Bind("VendorCode") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="100px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Common,Form_VendorName %>" SortExpression="VendorName">
                <ItemTemplate>
                    <asp:Label ID="lblVendorName" runat="server" Text='<%# Bind("VendorName")  %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="200px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField meta:resourcekey="TemplateField_VendorAddress" SortExpression="VendorAddress">
                <ItemTemplate>
                    <asp:Label ID="lblVendorAddress" runat="server" Text='<%# Bind("VendorAddress")  %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="220px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Common,Form_City %>" SortExpression="City">
                <ItemTemplate>
                    <asp:Label ID="lblCity" runat="server" Text='<%# Bind("City")  %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="100px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField meta:resourcekey="TemplateField_VendorType" SortExpression="VendorTypeID">
                <ItemTemplate>
                    <asp:Label ID="lblVendorType" runat="server" Text='<%# GetVendorTypeByID(Eval("VendorTypeID")) %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="100px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField meta:resourcekey="TemplateField_CompanyCode"  SortExpression="VendorTypeID">
                <ItemTemplate>
                    <asp:Label ID="lblCompanyCode" runat="server" Text='<%# GetCompanyCode(Eval("VendorTypeID")) %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="80px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField meta:resourcekey="TemplateField_ContactName" SortExpression="ContactName">
                <ItemTemplate>
                    <asp:Label ID="lblContactName" runat="server" Text='<%# Bind("ContactName")  %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="80px" HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
        <HeaderStyle CssClass="Header" />
        <EmptyDataTemplate>
            <table>
                <tr class="Header">
                    <td scope="col" style="width: 100px" class="Empty1">
                        <asp:Label ID="Label1" runat="server" meta:resourcekey="Label_VendorCode" />
                    </td>
                    <td scope="col" style="width: 100px">
                        <asp:Label ID="Label2" runat="server" Text="<%$Resources:Common,Form_VendorName %>" />
                    </td>
                    <td scope="col" style="width: 180px">
                        <asp:Label ID="Label3" runat="server" meta:resourcekey="Label_VendorAddress" />
                    </td>
                    <td scope="col" style="width: 100px">
                        <asp:Label ID="Label4" runat="server" Text="<%$Resources:Common,Form_City %>" />
                    </td>
                    <td scope="col" style="width: 100px">
                        <asp:Label ID="Label5" runat="server" meta:resourcekey="Label_VendorType" />
                    </td>
                    <td scope="col" style="width: 100px">
                        <asp:Label ID="Label6" runat="server" Text="<%$Resources:Common,Form_PaymentType %>" />
                    </td>
                    <td scope="col" style="width: 200px">
                        <asp:Label ID="Label7" runat="server" meta:resourcekey="Label_ContactName" />
                    </td>
                </tr>
            </table>
        </EmptyDataTemplate>
    </gc:GridView>
    <asp:ObjectDataSource ID="VendorObjectDataSource" runat="server" SelectMethod="GetVendorPaged"
        TypeName="BusinessObjects.MasterDataBLL" SelectCountMethod="QueryVendorCount"
        SortParameterName="sortExpression" EnablePaging="true">
        <SelectParameters>
            <asp:Parameter Name="queryExpression" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>