<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Announcement.aspx.cs" Inherits="BaseData_Announcement" Culture="auto" UICulture="auto" %>

<%@ Register Src="../UserControls/MultiAttachmentFile.ascx" TagName="UCFlie" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="title" style="width: 1230px;">
        <asp:Label runat="server" meta:resourcekey="titleLabel"></asp:Label></div>
    <asp:GridView CssClass="GridView" ID="BulletinGridView" runat="server" DataSourceID="BulletinObjectDataSource"
        AutoGenerateColumns="False" DataKeyNames="BulletinID" AllowPaging="True" AllowSorting="True"
        ShowFooter="true" PageSize="10" OnRowDeleted="BulletinGridView_RowDeleted" CellPadding="0"
        CellSpacing="0">
        <Columns>
            <asp:TemplateField InsertVisible="False" SortExpression="SystemId" HeaderText="ID"
                Visible="False">
                <ItemTemplate>
                    <asp:Label ID="BulletinIDLabel" runat="server" Text='<%# Bind("BulletinID") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="60px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="BulletinTitle" meta:resourcekey="BulletinGridView_TemplateField_Title">
                <ItemStyle Width="805px" HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:LinkButton CommandName="Select" ID="TitleCtl" runat="server" Text='<%# Bind("BulletinTitle") %>'
                        OnClick="TitleCtl_Click" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField SortExpression="CreateTime" meta:resourcekey="BulletinGridView_TemplateField_CreateOn">
                <ItemTemplate>
                    <asp:Label ID="CreateTimeLabel" runat="server" Text='<%# Bind("CreateTime") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="250px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="IsActive" HeaderText="<%$ Resources:Common,Form_IsActive %>">
                <ItemTemplate>
                    <asp:CheckBox Enabled="false" ID="IsActiveCtl" Checked='<%# Bind("IsActive") %>'
                        runat="server" />
                </ItemTemplate>
                <ItemStyle Width="60" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="IsHot" meta:resourcekey="BulletinGridView_TemplateField_IsHot">
                <ItemTemplate>
                    <asp:CheckBox Enabled="false" ID="IsHotCtl" Checked='<%# Bind("IsHot") %>' runat="server" />
                </ItemTemplate>
                <ItemStyle Width="40" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:LinkButton ID="EditBtn" Visible="<%# HasManageRight %>" Text='<%$Resources:Common,Button_Edit %>' CommandName="Select"
                        runat="server" OnClick="Edit_Click" />
                    <asp:LinkButton ID="DeleteBtn" Visible="<%# HasManageRight %>" Text='<%$Resources:Common,Button_Delete %>' OnClientClick="return confirm('确定删除此行数据吗？');"
                        CommandName="Delete" runat="server" />
                </ItemTemplate>
                <FooterTemplate>
                    <asp:LinkButton ID="NewBtn" Visible="<%# HasManageRight %>" Text='<%$Resources:Common,Button_Add %>' runat="server"
                        OnClick="NewBtn_Click" />
                </FooterTemplate>
                <ItemStyle Width="80" HorizontalAlign="Center" />
                <FooterStyle Width="80" HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
        <HeaderStyle CssClass="Header" />
        <EmptyDataTemplate>
            <table>
                <tr class="Header" style="height: 22px;">
                    <th style="width: 60px;" class="Empty1">
                        ID
                    </th>
                    <th style="width: 830px;">
                        <asp:Label runat="server" meta:resourcekey="Bulletin_Label_Title"></asp:Label>
                    </th>
                    <th style="width: 250px;">
                        <asp:Label runat="server" meta:resourcekey="Bulletin_Label_CreateOn"></asp:Label>
                    </th>
                    <th style="width: 60px;">
                        <asp:Label runat="server" meta:resourcekey="Bulletin_Label_IsHot"></asp:Label>
                    </th>
                    <th style="width: 40px;">
                        <asp:Label runat="server" Text="<%$ Resources:Common,Form_IsActive %>"></asp:Label>
                    </th>
                    <th style="width: 50px;">
                        &nbsp;
                    </th>
                </tr>
                <tr>
                    <td colspan="6">
                        <asp:LinkButton ID="NewBtn" Visible="<%# HasManageRight %>" runat="server"
                            OnClick="NewBtn_Click" Text='<%$Resources:Common,Button_Add %>' />
                    </td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <SelectedRowStyle CssClass="SelectedRow" />
    </asp:GridView>
    <div runat="server" id="Opdiv">
        <table style="width: 1240px; background-color: #F6F6F6">
            <tr>
                <td colspan="2" style="text-align: center;">
                    <asp:Label ID="Label1" runat="server" meta:resourcekey="Bulletin_Label_IsActive"></asp:Label><asp:CheckBox ID="EditIsActiveCheckBox" runat="server" />
                    IsHot<asp:CheckBox ID="EditIsHotCheckBox" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="width: 50px;">
                    <asp:Label ID="Label2" runat="server" meta:resourcekey="Bulletin_Label_Title"></asp:Label>
                </td>
                <td>
                    <asp:TextBox Width="1150px" ID="EditBulletinTitleTextBox" MaxLength="40" runat="server">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 50px; vertical-align: top;">
                    <asp:Label ID="Label3" runat="server" meta:resourcekey="Bulletin_Label_Content"></asp:Label>
                </td>
                <td>
                    <asp:TextBox Width="1150px" TextMode="multiLine" Rows="30" ID="EditBulletinContentTextBox"
                        runat="server">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" valign="top" align="center">
                    <uc1:UCFlie ID="EditUCFileUpload" runat="server" Width="320px" />
                </td>
            </tr>
            <tr id="tr1" runat="server">
                <td colspan="2" align="center">
                    <asp:Button ID="btnSave" runat="server" Visible="<%# HasManageRight %>" OnClick="btnSave_Click"
                        Text='<%$Resources:Common,Button_Save %>' CssClass="button_nor" />
                    <asp:Button ID="btnCancel" runat="server" Visible="<%# HasManageRight %>" OnClick="btnCancel_Click"
                        Text='<%$Resources:Common,Button_Cancel %>' CssClass="button_nor" />
                </td>
            </tr>
            <asp:Label ID="EditID" runat="server" Visible="false"></asp:Label>
        </table>
    </div>
    <asp:ObjectDataSource ID="BulletinObjectDataSource" runat="server" DeleteMethod="DeleteBulletin"
        SelectMethod="GetPage" TypeName="BusinessObjects.MasterDataBLL" SelectCountMethod="TotalCount"
        SortParameterName="sortExpression" EnablePaging="true">
        <SelectParameters>
            <asp:Parameter Name="queryExpression" Type="string" />
        </SelectParameters>
        <DeleteParameters>
            <asp:Parameter Name="bulletinId" Type="int32" />
        </DeleteParameters>
    </asp:ObjectDataSource>
</asp:Content>
