<%@ Page Title="" Language="C#" MasterPageFile="~/DialogMasterPage.master" AutoEventWireup="true"
    CodeFile="ItemSearch.aspx.cs" Inherits="Dialog_ItemSearch" Culture="auto" UICulture="auto" %>

<%@ Implements Interface="System.Web.UI.IPostBackEventHandler" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphTop" runat="Server">
    <script type="text/javascript">
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
                    <asp:Label ID="Label_ItemCode" runat="server" meta:resourcekey="Label_ItemCode" /></div>
                <asp:TextBox runat="server" ID="txtItemCodeBySearch" Width="160px"></asp:TextBox>
            </td>
            <td style="width: 200px;">
                <div class="field_title">
                    <asp:Label ID="Label_ItemName" runat="server" meta:resourcekey="Label_ItemName" /></div>
                <asp:TextBox runat="server" ID="txtItemNameBySearch" Width="160px"></asp:TextBox>
            </td>
            <td style="width: 200px;">
                <div class="field_title">
                    <asp:Label ID="Label_UOM" runat="server" meta:resourcekey="Label_UOM" /></div>
                <asp:TextBox runat="server" ID="txtUOMBySearch" Width="160px"></asp:TextBox>
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
        <asp:Label ID="Label_Title" runat="server" meta:resourcekey="Label_Title" /></div>
    <gc:GridView ID="ItemView" CssClass="GridView" runat="server" DataSourceID="ItemObjectDataSource"
        AllowPaging="true" PageSize="20" AllowSorting="True" AutoGenerateColumns="False"
        OnRowDataBound="ItemView_RowDataBound" DataKeyNames="ItemID" CellPadding="0">
        <Columns>
            <asp:TemplateField meta:resourcekey="TemplateField_ItemName" SortExpression="ItemName">
                <ItemStyle Width="150px" HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:Label ID="lblItemName" runat="server" Text='<%# Bind("ItemName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField meta:resourcekey="TemplateField_ItemCode" SortExpression="ItemCode">
                <ItemStyle Width="150px" HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:Label ID="lblItemCode" runat="server" Text='<%# Bind("ItemCode") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField meta:resourcekey="TemplateField_ItemCategory" SortExpression="ItemCode">
                <ItemStyle Width="100px" HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:Label ID="lblItemCategory" runat="server" Text='<%# GetItemCategoryByID(Eval("ItemCategoryID")) %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField meta:resourcekey="TemplateField_UOM" SortExpression="UOM">
                <ItemStyle Width="60px" HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:Label ID="lblUOM" runat="server" Text='<%# Bind("UOM") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField meta:resourcekey="TemplateField_Package" SortExpression="Package">
                <ItemStyle Width="100px" HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:Label ID="lblPackage" runat="server" Text='<%# Bind("Package") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField meta:resourcekey="TemplateField_UnitPrice" SortExpression="UnitPrice">
                <ItemStyle Width="70px" HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:Label ID="lblUnitPrice" runat="server" Text='<%# Bind("UnitPrice") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField meta:resourcekey="TemplateField_Description" SortExpression="Description">
                <ItemStyle Width="210px" HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <HeaderStyle CssClass="Header" />
        <EmptyDataTemplate>
            <table>
                <tr class="Header">
                    <td scope="col" style="width: 150px" class="Empty1">
                        <asp:Label ID="Label1" runat="server" meta:resourcekey="Label_ItemName" />
                    </td>
                    <td scope="col" style="width: 150px">
                        <asp:Label ID="Label2" runat="server" meta:resourcekey="Label_ItemCode" />
                    </td>
                    <td scope="col" style="width: 100px">
                        <asp:Label ID="Label3" runat="server" meta:resourcekey="Label_ItemCategory" />
                    </td>
                    <td scope="col" style="width: 60px">
                        <asp:Label ID="Label4" runat="server" meta:resourcekey="Label_UOM" />
                    </td>
                    <td scope="col" style="width: 100px">
                        <asp:Label ID="Label5" runat="server" meta:resourcekey="Label_Package" />
                    </td>
                    <td scope="col" style="width: 70px">
                        <asp:Label ID="Label6" runat="server" meta:resourcekey="Label_UnitPrice" />
                    </td>
                    <td scope="col" style="width: 210px">
                        <asp:Label ID="Label7" runat="server" meta:resourcekey="Label_Description" />
                    </td>
                </tr>
                <tr>
                    <td colspan="7" class="Empty2 noneLabel">
                        <asp:Label ID="Label_None"  runat="server" Text="<%$Resources:Common,Label_None %>" />
                    </td>
                </tr>
            </table>
        </EmptyDataTemplate>
    </gc:GridView>
    <asp:ObjectDataSource ID="ItemObjectDataSource" runat="server" EnablePaging="true"
        SelectMethod="GetItemPaged" SelectCountMethod="ItemCount" SortParameterName="sortExpression"
        TypeName="BusinessObjects.MasterDataBLL">
        <SelectParameters>
            <asp:Parameter Name="queryExpression" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
