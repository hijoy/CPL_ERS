<%@ Page Title="" Language="C#" MasterPageFile="~/DialogMasterPage.master" AutoEventWireup="true"
    CodeFile="SKUSearch.aspx.cs" Culture="auto" UICulture="auto" Inherits="Dialog_SKUSearch" %>

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
    <table style="background-color: #F6F6F6; vertical-align: top; width: 830px;">
        <tr style="vertical-align: top; height: 40px">
            <td style="width: 200px;">
                <div class="field_title">
                    <asp:Label ID="Label_SKUNo" runat="server" meta:resourcekey="Label_SKUNo" />
                </div>
                <asp:TextBox runat="server" ID="txtSKUNoBySearch" Width="160px"></asp:TextBox>
            </td>
            <td style="width: 200px;">
                <div class="field_title">
                    <asp:Label ID="Form_ProductName" runat="server" Text="<%$Resources:Common,Form_ProductName %>" /></div>
                <asp:TextBox runat="server" ID="txtSKUNameBySearch" Width="160px"></asp:TextBox>
            </td>
            <td style="width: 200px;">
                <div class="field_title">
                    <asp:Label ID="Form_Brand" runat="server" Text="<%$Resources:Common,Form_Brand %>" /></div>
                <asp:DropDownList runat="server" ID="ddlBrandBySearch" DataSourceID="sdsBrand" DataTextField="BrandName"
                    DataValueField="BrandID" Width="160px" AppendDataBoundItems="true">
                    <asp:ListItem Selected="True" Text="ALL" Value=""></asp:ListItem>
                </asp:DropDownList>
                <asp:SqlDataSource ID="sdsBrand" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                    SelectCommand="select BrandID,BrandName from Brand order by BrandName"></asp:SqlDataSource>
            </td>
            <td style="width: 50px;">
                &nbsp;
            </td>
            <td  align="left" valign="middle">
                <input type="hidden" id="btnclicked" name="btnclicked" value="0" />
                <asp:Button ID="SearchButton" runat="server" CssClass="button_nor" Text="<%$Resources:Common,Button_Search %>" OnClick="SearchButton_Click" />&nbsp;&nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div class="title1" style="width: 842px;">
        <asp:Label  runat="server" Text="<%$Resources:Common,Label_ProductTitle %>" /></div>
    <gc:GridView ID="gvSKU" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
        DataKeyNames="SKUID" DataSourceID="odsSKU" PageSize="20" CssClass="GridView"
        OnRowDataBound="gvSKU_RowDataBound">
        <Columns>
            <asp:TemplateField meta:resourcekey="TemplateField_SKUNo" SortExpression="SKUNo">
                <ItemTemplate>
                    <asp:Label ID="lblSKUNo" runat="server" Text='<%# Bind("SKUNo") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="150px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Common,Form_ProductName %>" SortExpression="SKUName">
                <ItemTemplate>
                    <asp:Label ID="lblSKUName" runat="server" Text='<%# Bind("SKUName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="280px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Common,Form_Brand %>">
                <ItemTemplate>
                    <asp:Label ID="lblBrand" runat="server" Text='<%# GetBrandByID(Eval("BrandID")) %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="100px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField meta:resourcekey="TemplateField_SKUType">
                <ItemTemplate>
                    <asp:Label ID="lblSKUType" runat="server" Text='<%# GetSKUTypeByID(Eval("SKUTypeID")) %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="150px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField meta:resourcekey="TemplateField_SKUCategory">
                <ItemTemplate>
                    <asp:Label ID="lblSKUCategory" runat="server" Text='<%# GetSKUCategoryByID(Eval("SKUCategoryID"))%>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="100px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField meta:resourcekey="TemplateField_PackPerCase" SortExpression="PackPerCase">
                <ItemTemplate>
                    <asp:Label ID="lblPackPerCase" runat="server" Text='<%# Bind("PackPerCase") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="100px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField meta:resourcekey="TemplateField_StandardCost" SortExpression="StandardCost">
                <ItemTemplate>
                    <asp:Label ID="lblStandardCost" runat="server" Text='<%# (Convert.ToDouble(Eval("StandardCost"))*1.17*1.1).ToString("N")%>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="80px" HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            <table>
                <tr class="Header">
                    <th style="width: 150px;" class="Empty1">
                        <asp:Label ID="Label1" runat="server" meta:resourcekey="Label_SKUNo" />
                    </th>
                    <th style="width: 280px;">
                        <asp:Label ID="Label2" runat="server" Text="<%$Resources:Common,Form_ProductName %>" />
                    </th>
                    <th style="width: 300px;">
                        <asp:Label ID="Label3" runat="server" Text="<%$Resources:Common,Form_Brand %>" />
                    </th>
                    <th style="width: 100px;">
                        <asp:Label ID="Label4" runat="server" meta:resourcekey="Label_SKUType" />
                    </th>
                    <th style="width: 100px;">
                        <asp:Label ID="Label5" runat="server" meta:resourcekey="Label_SKUCategory" />
                    </th>
                    <th style="width: 100px;">
                        <asp:Label ID="Label6" runat="server" meta:resourcekey="Label_PackPerCase" />
                    </th>
                    <th style="width: 80px;">
                        <asp:Label ID="Label7" runat="server" meta:resourcekey="Label_StandardCost" />
                    </th>
                </tr>
                <tr>
                    <td colspan="6" class="Empty2 noneLabel">
                        <asp:Label ID="Label_None"  runat="server" Text="<%$Resources:Common,Label_None %>" />
                    </td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <SelectedRowStyle CssClass="SelectedRow" />
        <HeaderStyle CssClass="Header" />
    </gc:GridView>
    <asp:ObjectDataSource ID="odsSKU" runat="server" TypeName="BusinessObjects.MasterDataBLL"
        SelectMethod="GetSKUPaged" SelectCountMethod="QuerySKUCount" SortParameterName="sortExpression"
        EnablePaging="true">
        <SelectParameters>
            <asp:Parameter Name="queryExpression" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
