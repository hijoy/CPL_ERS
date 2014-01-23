<%@ Page Title="" Language="C#" MasterPageFile="~/DialogMasterPage.master" AutoEventWireup="true"
    CodeFile="VendorTypeSearch.aspx.cs" Culture="auto" UICulture="auto" Inherits="Dialog_VendorTypeSearch" %>

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
                    <asp:Label ID="Label_VendorType" runat="server" meta:resourcekey="Label_VendorType" /></div>
                <asp:TextBox runat="server" ID="txtVendorTypeNameBySearch" Width="170px"></asp:TextBox>
            </td>
            <td style="width: 200px;">
                <div class="field_title">
                    <asp:Label ID="Label_Company" runat="server" meta:resourcekey="Label_Company" /></div>
                <asp:DropDownList runat="server" ID="ddlCompany" DataSourceID="odsCompany" DataTextField="CompanyName"
                    DataValueField="CompanyID" Width="170px" AppendDataBoundItems="true">
                    <asp:ListItem Selected="True" Text="All" Value=""></asp:ListItem>
                </asp:DropDownList>
                <asp:SqlDataSource ID="odsCompany" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                    SelectCommand="SELECT [CompanyID], [CompanyName] FROM [Company] "></asp:SqlDataSource>
            </td>
            <td style="width: 200px;">
                <div class="field_title">
                    <asp:Label ID="Form_Currency" runat="server" Text="<%$Resources:Common,Form_Currency %>" /></div>
                <asp:DropDownList runat="server" ID="ddlCurrency" DataTextField="CurrencyFullName"
                    DataValueField="CurrencyID" Width="170px" DataSourceID="odsCurrency" AppendDataBoundItems="true">
                    <asp:ListItem Selected="True" Text="All" Value=""></asp:ListItem>
                </asp:DropDownList>
                <asp:SqlDataSource ID="odsCurrency" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                    SelectCommand="select CurrencyID,CurrencyFullName from dbo.Currency where IsActive=1 order by CurrencyFullName">
                </asp:SqlDataSource>
            </td>
            <td style="width: 50px;">
                &nbsp;
            </td>
            <td colspan="2" align="left" valign="middle">
                <input type="hidden" id="btnclicked" name="btnclicked" value="0" />
                <asp:Button ID="SearchButton" runat="server" CssClass="button_nor" Text="<%$Resources:Common,Button_Search %>"
                    OnClick="SearchButton_Click" />&nbsp;&nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div class="title1" style="width: 842px;">
        <asp:Label ID="Label_Title" runat="server" meta:resourcekey="Label_Title" /></div>
    <gc:GridView ID="gvVendorType" CssClass="GridView" runat="server" DataSourceID="VendorTypeObjectDataSource"
        AllowPaging="true" PageSize="20" AllowSorting="True" AutoGenerateColumns="False"
        OnRowDataBound="gvVendorType_RowDataBound" DataKeyNames="VendorTypeID" CellPadding="0">
        <Columns>
            <asp:TemplateField meta:resourcekey="TemplateField_VendorType" SortExpression="VendorTypeName">
                <ItemTemplate>
                    <asp:Label ID="lblVendorTypeName" runat="server" Text='<%# Bind("VendorTypeName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="100px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Common,Label_CompanyCode %>">
                <ItemTemplate>
                    <asp:Label ID="lblCompanyCode" runat="server" Text='<%# GetCompanyCodeByID(Eval("CompanyID")) %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="60px" />
            </asp:TemplateField>
            <asp:TemplateField meta:resourcekey="TemplateField_Company" SortExpression="CompanyID">
                <ItemTemplate>
                    <asp:Label ID="lblCompany" runat="server" Text='<%# GetCompanyNameByID(Eval("CompanyID")) %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="277px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Common,Form_Currency %>" SortExpression="CurrencyID">
                <ItemTemplate>
                    <asp:Label ID="lblCurrency" runat="server" Text='<%# GetCurrencyByID(Eval("CurrencyID")) %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="100px" />
            </asp:TemplateField>
            <asp:TemplateField meta:resourcekey="TemplateField_Description" SortExpression="CurrencyID">
                <ItemTemplate>
                    <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="300px" />
            </asp:TemplateField>
        </Columns>
        <HeaderStyle CssClass="Header" />
        <EmptyDataTemplate>
            <table>
                <tr class="Header">
                    <td scope="col" style="width: 100px" class="Empty1">
                        <asp:Label ID="Label1" runat="server" meta:resourcekey="Label_VendorType"></asp:Label>
                    </td>
                    <td scope="col" style="width: 60px">
                        <asp:Label ID="Label2" runat="server" Text="<%$Resources:Common,Label_CompanyCode %>"></asp:Label>
                    </td>
                    <td scope="col" style="width: 277px">
                        <asp:Label ID="Label5" runat="server" meta:resourcekey="Label_Company"></asp:Label>
                    </td>
                    <td scope="col" style="width: 100px">
                        <asp:Label ID="Label3" runat="server" Text="<%$Resources:Common,Form_Currency %>"></asp:Label>
                    </td>
                    <td scope="col" style="width: 300px">
                        <asp:Label ID="Label4" runat="server" meta:resourcekey="Label_Description"></asp:Label>
                    </td>
                </tr>
            </table>
        </EmptyDataTemplate>
    </gc:GridView>
    <asp:ObjectDataSource ID="VendorTypeObjectDataSource" runat="server" SelectMethod="GetVendorTypePaged"
        TypeName="BusinessObjects.MasterDataBLL" SelectCountMethod="VendorTypeCount"
        SortParameterName="sortExpression" EnablePaging="true">
        <SelectParameters>
            <asp:Parameter Name="queryExpression" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
