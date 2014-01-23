<%@ Page Title="" Language="C#" MasterPageFile="~/DialogMasterPage.master" AutoEventWireup="true"
    CodeFile="CustomerSearch.aspx.cs" Inherits="Dialog_CustomerSearch" Culture="auto" UICulture="auto" %>

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
                    <asp:Label ID="Label_CustomerNo" runat="server" meta:resourcekey="Label_CustomerNo" />
                </div>
                <asp:TextBox runat="server" ID="txtCustNoBySearch" Width="160px"></asp:TextBox>
            </td>
            <td style="width: 200px;">
                <div class="field_title">
                    <asp:Label ID="Form_Customer" runat="server" Text="<%$Resources:Common,Form_Customer %>" /></div>
                <asp:TextBox runat="server" ID="txtCustNameBySearch" Width="160px"></asp:TextBox>
            </td>
            <td style="width: 200px;">
                <div class="field_title">
                    <asp:Label ID="Form_City" runat="server" Text="<%$Resources:Common,Form_City %>" /></div>
                <asp:TextBox runat="server" ID="txtCity" Width="160px"></asp:TextBox>
            </td>
            <td style="width: 200px;">
                <div class="field_title">
                    <asp:Label ID="Label_CustomerType" runat="server" meta:resourcekey="Label_CustomerType" />
                </div>
                <asp:DropDownList runat="server" ID="ddlCustomerTypeBySearch" DataTextField="CustomerTypeName"
                    DataValueField="CustomerTypeID" Width="160px" DataSourceID="odsCustomerType"
                    AppendDataBoundItems="true">
                    <asp:ListItem Selected="True" Text="ALL" Value=""></asp:ListItem>
                </asp:DropDownList>
                <asp:SqlDataSource ID="odsCustomerType" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                    SelectCommand="select CustomerTypeID,CustomerTypeName from dbo.CustomerType where IsActive=1  order by CustomerTypeName">
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td style="width: 200px;">
                <div class="field_title">
                    <asp:Label ID="Form_CustomerRegion" runat="server" Text="<%$Resources:Common,Form_CustomerRegion %>" /></div>
                <asp:DropDownList runat="server" ID="ddlCustomerRegion" DataTextField="CustomerRegionName"
                    DataValueField="CustomerRegionID" Width="160px" DataSourceID="sdsCustomerRegion"
                    AppendDataBoundItems="true">
                    <asp:ListItem Selected="True" Text="ALL" Value=""></asp:ListItem>
                </asp:DropDownList>
                <asp:SqlDataSource ID="sdsCustomerRegion" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                    SelectCommand="select CustomerRegionID,CustomerRegionName from dbo.CustomerRegion order by CustomerRegionName">
                </asp:SqlDataSource>
            </td>
            <td style="width: 200px;">
                <div class="field_title">
                    <asp:Label ID="Form_CustomerChannel" runat="server" Text="<%$Resources:Common,Form_CustomerChannel %>" /></div>
                <asp:DropDownList runat="server" ID="ddlCustomerChannel" DataTextField="CustomerChannelName"
                    DataValueField="CustomerChannelID" Width="160px" DataSourceID="sdsCustomerChannel"
                    AppendDataBoundItems="true">
                    <asp:ListItem Selected="True" Text="ALL" Value=""></asp:ListItem>
                </asp:DropDownList>
                <asp:SqlDataSource ID="sdsCustomerChannel" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                    SelectCommand="select CustomerChannelID,CustomerChannelName from dbo.CustomerChannel where IsActive=1  order by CustomerChannelName">
                </asp:SqlDataSource>
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
    <gc:GridView ID="gvCustomer" runat="server" AllowPaging="True" AllowSorting="True"
        OnRowDataBound="gvCustomer_RowDataBound" AutoGenerateColumns="False" DataKeyNames="CustomerID"
        DataSourceID="CustomerObjectDataSource" PageSize="20" CssClass="GridView">
        <Columns>
            <asp:TemplateField meta:resourcekey="TemplateField_CustomerNo" SortExpression="CustomerNo">
                <ItemTemplate>
                    <asp:Label ID="lblCustomerNo" runat="server" Text='<%# Bind("CustomerNo") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="100px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Common,Form_Customer %>" SortExpression="CustomerName">
                <ItemTemplate>
                    <asp:Label ID="lblCustomerName" runat="server" Text='<%# Bind("CustomerName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="250px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Common,Form_CustomerChannel %>">
                <ItemTemplate>
                    <asp:Label ID="lblCustomerChannel" runat="server" Text='<%# GetCustomerChannelByID(Eval("CustomerChannelID")) %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="100px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField meta:resourcekey="TemplateField_CustomerType">
                <ItemTemplate>
                    <asp:Label ID="lblCustomerType" runat="server" Text='<%# GetCustomerTypeByID(Eval("CustomerTypeID")) %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="100px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Common,Form_CustomerRegion %>">
                <ItemTemplate>
                    <asp:Label ID="lblCustomerRegion" runat="server" Text='<%# GetCustomerRegionByID(Eval("CustomerRegionID"))%>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="100px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Common,Form_City %>">
                <ItemTemplate>
                    <asp:Label ID="lblCity" runat="server" Text='<%# Bind("City") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="100px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Common,Form_KAType %>">
                <ItemTemplate>
                    <asp:Label ID="lblKAType" runat="server" Text='<%# Bind("KAType") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="100px" HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            <table>
                <tr class="Header">
                    <td style="width: 120px;" class="Empty1">
                        <asp:Label ID="Label1" runat="server" meta:resourcekey="Label_CustomerNo" />
                    </td>
                    <td style="width: 250px;">
                        <asp:Label ID="Label2" runat="server" Text="<%$Resources:Common,Form_Customer %>" />
                    </td>
                    <td style="width: 120px;">
                        <asp:Label ID="Label3" runat="server" Text="<%$Resources:Common,Form_CustomerChannel %>" />
                    </td>
                    <td style="width: 120px;">
                        <asp:Label ID="Label4" runat="server" meta:resourcekey="Label_CustomerType" />
                    </td>
                    <td style="width: 120px;">
                        <asp:Label ID="Label5" runat="server" Text="<%$Resources:Common,Form_CustomerRegion %>" />
                    </td>
                    <td style="width: 120px;">
                        <asp:Label ID="Label6" runat="server" Text="<%$Resources:Common,Form_City %>" />
                    </td>
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
    <asp:ObjectDataSource ID="CustomerObjectDataSource" runat="server" TypeName="BusinessObjects.MasterDataBLL"
        SelectMethod="GetCustomerPaged" SelectCountMethod="CustomerCount" SortParameterName="sortExpression"
        EnablePaging="true">
        <SelectParameters>
            <asp:Parameter Name="queryExpression" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
