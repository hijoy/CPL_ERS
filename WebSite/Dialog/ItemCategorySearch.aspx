<%@ Page Title="" Language="C#" MasterPageFile="~/DialogMasterPage.master" AutoEventWireup="true"
    CodeFile="ItemCategorySearch.aspx.cs" Inherits="Dialog_ItemCategorySearch" Culture="auto" UICulture="auto" %>

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
    <div class="title1" style="width: 842px;">
        <asp:Label ID="Label_Title" runat="server" meta:resourcekey="Label_Title" /></div>
    <gc:GridView ID="gvItemCategory" runat="server" AllowPaging="false" AllowSorting="false"
        OnRowDataBound="gvItemCategory_RowDataBound" AutoGenerateColumns="False" DataKeyNames="ItemCategoryID"
        DataSourceID="ItemCategoryObjectDataSource" PageSize="20" CssClass="GridView">
        <Columns>
            <asp:TemplateField HeaderText="编号">
                <ItemTemplate>
                    <asp:Label ID="lblItemCategoryCode" runat="server" Text='<%# Bind("ItemCategoryCode") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="120px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="名称" >
                <ItemTemplate>
                    <asp:Label ID="lblItemCategoryName" runat="server" Text='<%# Bind("ItemCategoryName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="250px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="描述">
                <ItemTemplate>
                    <asp:Label ID="lblRemark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="470px" HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
        <SelectedRowStyle CssClass="SelectedRow" />
        <HeaderStyle CssClass="Header" />
    </gc:GridView>
    <asp:ObjectDataSource ID="ItemCategoryObjectDataSource" runat="server" TypeName="BusinessObjects.MasterDataBLL"
        SelectMethod="GetItemCategory" EnablePaging="false">
    </asp:ObjectDataSource>
</asp:Content>
