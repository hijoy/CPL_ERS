<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StaffSearch.aspx.cs" MasterPageFile="~/DialogMasterPage.master"
    Culture="auto" UICulture="auto" Inherits="Dialog_StaffSearch" %>

<%@ Implements Interface="System.Web.UI.IPostBackEventHandler" %>
<%@ Register Src="../UserControls/OUSelect.ascx" TagName="OUSelect" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphTop" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
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
        <asp:Label ID="Label_SearchCondition" runat="server" Text="<%$Resources:Common,Label_SearchCondition %>" />
    </div>

    <table style="background-color: #F6F6F6; vertical-align: top; width: 842px;">
            <tr style="vertical-align: top; height: 40px">
                <td style="width: 200px;">
                    <div class="field_title">
                        <asp:Label ID="Label_UserName" runat="server" meta:resourcekey="Label_UserName" />
                    </div>
                    <asp:TextBox ID="UserAccountTextBox" runat="server" Width="160px"></asp:TextBox>
                </td>
                <td style="width: 200px;">
                    <div class="field_title">
                        <asp:Label ID="Label_StuffName" runat="server" meta:resourcekey="Label_StuffName" />
                    </div>
                    <asp:TextBox ID="StuffNameTextBox" runat="server" Width="160px"></asp:TextBox>
                </td>
                <td style="width: 200px;">
                    <div class="field_title">
                        <asp:Label ID="Form_StaffNo" runat="server" Text="<%$Resources:Common,Form_StaffNo %>" /></div>
                    <asp:TextBox ID="EmployeeNoTextBox" runat="server" Width="160px"></asp:TextBox>
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
    <gc:GridView ID="gvStaff" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="StuffUserId" DataSourceID="odsStuffUser"
        PageSize="20" ShowFooter="True" CssClass="GridView" OnRowDataBound="gvStaff_RowDataBound">
        <Columns>
            <asp:TemplateField meta:resourcekey="TemplateField_UserName" SortExpression="UserName">
                <ItemTemplate>
                    <asp:Label ID="lblUserName" runat="server" Text='<%# Bind("UserName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="150px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField meta:resourcekey="TemplateField_StuffName" SortExpression="StuffName">
                <ItemTemplate>
                    <asp:Label ID="lblStuffName" runat="server" Text='<%# Bind("StuffName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="100px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Common,Form_Position %>">
                <ItemTemplate>
                    <asp:Label ID="PositionsLabel" runat="server"></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="300px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Common,Form_StaffNo %>" SortExpression="StuffId">
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("StuffId") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="100px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Common,Form_AttendDate %>" SortExpression="AttendDate">
                <ItemTemplate>
                    <asp:Label ID="Label6" runat="server" Text='<%# Eval("AttendDate","{0:yyyy-MM-dd}" ) %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="100px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Common,Form_IsActive %>" SortExpression="IsActive">
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox2" runat="server" Checked='<%# Bind("IsActive") %>' Enabled="false" />
                </ItemTemplate>
                <ItemStyle Width="100px" HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            <table>
                <tr class="Header">
                    <th style="width: 150px;" class="Empty1">
                        <asp:Label ID="Label1" runat="server" meta:resourcekey="Label_UserName" />
                    </th>
                    <th style="width: 100px;">
                        <asp:Label ID="Label3" runat="server" meta:resourcekey="Label_StuffName" />
                    </th>
                    <th style="width: 300px;">
                        <asp:Label ID="Label4" runat="server" Text="<%$Resources:Common,Form_Position %>" />
                    </th>
                    <th style="width: 100px;">
                        <asp:Label ID="Label5" runat="server" Text="<%$Resources:Common,Form_StaffNo %>" />
                    </th>
                    <th style="width: 100px;">
                        <asp:Label ID="Label7" runat="server" Text="<%$Resources:Common,Form_AttendDate %>" />
                    </th>
                    <th style="width: 100px;">
                        <asp:Label ID="Label8" runat="server" Text="<%$Resources:Common,Form_IsActive %>" />
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
        <FooterStyle CssClass="Footer" />
    </gc:GridView>

    <asp:ObjectDataSource ID="odsStuffUser" runat="server" TypeName="BusinessObjects.StuffUserBLL"
        OldValuesParameterFormatString="{0}" SelectMethod="GetStuffUserPaged"
        SelectCountMethod="TotalCount" SortParameterName="sortExpression" EnablePaging="true">
        <SelectParameters>
            <asp:Parameter Name="queryExpression" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
