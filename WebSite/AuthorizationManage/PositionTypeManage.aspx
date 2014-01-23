<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="PositionTypeManage.aspx.cs" Inherits="AuthorizationManage_PositionTypeManage"
    Culture="Auto" UICulture="Auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="title">
        <asp:Label runat="server" meta:resourcekey="Title_Label"></asp:Label></div>
    <asp:UpdatePanel ID="upCustomerType" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <gc:GridView ID="gvCustomerType" CssClass="GridView" runat="server" DataSourceID="odsPositionType"
                CellPadding="0" AutoGenerateColumns="False" DataKeyNames="PositionTypeId" AllowPaging="false"
                EnableModelValidation="True">
                <Columns>
                    <asp:TemplateField SortExpression="PositionTypeName" meta:resourcekey="PositionTypeGridView_Template_PositionTypeName">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtPositionTypeName" runat="server" Text='<%# Bind("PositionTypeName") %>'
                                Width="550px" CssClass="InputText" MaxLength="20"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblPositionTypeName" runat="server" Text='<%# Bind("PositionTypeName") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="577px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="PositionTypeCode" meta:resourcekey="PositionTypeGridView_Template_PositionTypeCode">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtPositionTypeCode" runat="server" Text='<%# Bind("PositionTypeCode") %>'
                                Width="550px" CssClass="InputText" MaxLength="20"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblPositionTypeCode" runat="server" Text='<%# Bind("PositionTypeCode") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="570px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <EditItemTemplate>
                            <asp:LinkButton ID="lknSave" runat="server" CommandName="Update" Text='<%$Resources:Common,Button_Update %>'></asp:LinkButton>
                            <asp:LinkButton ID="linCancel" runat="server" CommandName="Cancel" Text='<%$Resources:Common,Button_Cancel %>'></asp:LinkButton>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="lknEdit" runat="server" CausesValidation="false" CommandName="Edit"
                                Text='<%$Resources:Common,Button_Edit %>'></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td style="width: 577px;" class="Empty1">
                                <asp:Label  runat="server" meta:resourcekey="Label_PositionTypeName"/>
                            </td>
                            <td style="width: 570px;">
                                <asp:Label  runat="server" meta:resourcekey="Label_PositionTypeCode"/>
                            </td>
                            <td style="width: 90px;">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <SelectedRowStyle CssClass="SelectedRow" />
            </gc:GridView>
            <asp:FormView ID="fvPositionType" runat="server" DataKeyNames="PositionTypeId" DataSourceID="odsPositionType"
                DefaultMode="Insert" EnableModelValidation="True" CellPadding="0">
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr>
                            <td align="center" style="height: 22px; width: 577px;">
                                <asp:TextBox ID="txtPositionTypeName" runat="server" Text='<%# Bind("PositionTypeName") %>'
                                    Width="550px" CssClass="InputText" MaxLength="20"></asp:TextBox>
                            </td>
                            <td align="center" style="height: 22px; width: 570px;">
                                <asp:TextBox ID="txtPositionTypeCode" runat="server" Text='<%# Bind("PositionTypeCode") %>'
                                    Width="550px" CssClass="InputText" MaxLength="20"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 90px;">
                                <asp:LinkButton ID="InsertLinkButton" runat="server" CommandName="Insert" Text="<%$Resources:Common,Button_Add %>"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </InsertItemTemplate>
            </asp:FormView>
            <asp:ObjectDataSource ID="odsPositionType" runat="server" TypeName="BusinessObjects.AuthorizationBLL"
                InsertMethod="InsertPositionType" SelectMethod="GetPositionTypes" UpdateMethod="UpdatePositionType"
                EnablePaging="false" OnInserting="odsPositionType_Inserting" OnUpdating="odsPositionType_Updating">
                <UpdateParameters>
                    <asp:Parameter Name="PositionTypeName" Type="String" />
                    <asp:Parameter Name="PositionTypeCode" Type="String" />
                </UpdateParameters>
                <InsertParameters>
                    <asp:Parameter Name="PositionTypeName" Type="String" />
                    <asp:Parameter Name="PositionTypeCode" Type="String" />
                </InsertParameters>
            </asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
