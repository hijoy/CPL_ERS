<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="SystemRoleManage.aspx.cs" Inherits="AuthorizationManage_SystemRoleManage"
     UICulture="Auto" Culture="Auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="1240px" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td valign="top" style="width:700px;padding-left:10px;">
                <div class="title2" style="width: 690px;">
                    <asp:Label runat="server" meta:resourcekey="Title_Label"></asp:Label>
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <gc:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                            CssClass="GridView" DataSourceID="SystemRoleDS" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                            DataKeyNames="SystemRoleId" OnRowDeleted="GridView1_RowDeleted" CellPadding="0"
                            CellSpacing="0">
                            <HeaderStyle CssClass="Header" />
                            <Columns>
                                <asp:BoundField DataField="SystemRoleId" meta:resourcekey="SystemRoleGridView_Template_SystemRoleId" InsertVisible="False"
                                    ReadOnly="True" SortExpression="SystemRoleId">
                                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:TemplateField SortExpression="SystemRoleName" meta:resourcekey="SystemRoleGridView_Template_SystemRoleName">
                                    <EditItemTemplate>
                                        <asp:TextBox Width="230px" CssClass="InputText" ID="TextBox1" MaxLength="20" runat="server"
                                            Text='<%# Bind("SystemRoleName") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1"
                                            Display="None" meta:resourcekey="RequiredFieldValidator_RoleName" ValidationGroup="EditSystemRole"></asp:RequiredFieldValidator><asp:ValidationSummary
                                                ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False"
                                                ValidationGroup="EditSystemRole" />
                                    </EditItemTemplate>
                                    <ItemStyle Width="250px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="SystemRoleNameCtl" runat="server" CommandName="Select" Text='<%# Bind("SystemRoleName") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="Description" meta:resourcekey="SystemRoleGridView_Template_Remark">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox2" CssClass="InputText" Width="230px" MaxLength="80" runat="server"
                                            Text='<%# Bind("Description") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="270px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="False">
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Update"  Text='<%$Resources:Common,Button_Update %>' ValidationGroup="EditSystemRole"></asp:LinkButton>
                                        <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel"
                                            Text='<%$Resources:Common,Button_Cancel %>'></asp:LinkButton>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False" CommandName="Edit"
                                            Text='<%$Resources:Common,Button_Edit %>'></asp:LinkButton>
                                        <asp:LinkButton ID="LinkButton4" runat="server" CausesValidation="False" CommandName="Delete"
                                            Text='<%$Resources:Common,Button_Delete %>'></asp:LinkButton>
                                        <cc1:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" meta:resourcekey="ConfirmButtonExtender_Delete"
                                            TargetControlID="LinkButton4">
                                        </cc1:ConfirmButtonExtender>
                                    </ItemTemplate>
                                    <ItemStyle Width="76px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                            <SelectedRowStyle BackColor="#E0E0E0" />
                        </gc:GridView>
                        <asp:FormView ID="FormView1" runat="server" DataSourceID="SystemRoleDS" DefaultMode="Insert"
                            CellPadding="0">
                            <InsertItemTemplate>
                                <table class="FormView">
                                    <tr>
                                        <td style="width: 100px;">
                                            &nbsp;
                                        </td>
                                        <td align="center" style="width: 250px;">
                                            <asp:TextBox Width="230px" CssClass="InputText" MaxLength="20" ID="SystemRoleNameCtl"
                                                runat="server" Text='<%# Bind("SystemRoleName") %>'></asp:TextBox>
                                        </td>
                                        <td align="center" style="width: 270px;">
                                            <asp:TextBox Width="240px" CssClass="InputText" MaxLength="80" ID="DescriptionCtl"
                                                runat="server" Text='<%# Bind("Description") %>'></asp:TextBox>
                                        </td>
                                        <td align="center" style="width: 76px;">
                                            <asp:LinkButton ID="InsertBtn" runat="server" CommandName="Insert" Text='<%$Resources:Common,Button_Add %>' ValidationGroup="InsertSystemRole" />
                                        </td>
                                    </tr>
                                </table>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="SystemRoleNameCtl"
                                    Display="None" meta:resourcekey="RequiredFieldValidator_RoleName" ValidationGroup="InsertSystemRole"></asp:RequiredFieldValidator>
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                    ShowSummary="False" ValidationGroup="InsertSystemRole" />
                            </InsertItemTemplate>
                        </asp:FormView>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:ObjectDataSource ID="SystemRoleDS" runat="server" DeleteMethod="DeleteSystemRole"
                    InsertMethod="InsertSystemRole" SelectMethod="GetSystemRoles" TypeName="BusinessObjects.AuthorizationBLL"
                    UpdateMethod="UpdateSystemRole" OnInserted="SystemRoleDS_Inserted" OnUpdated="SystemRoleDS_Updated"
                    OnDeleting="SystemRoleDS_Deleting" OnInserting="SystemRoleDS_Inserting">
                    <DeleteParameters>
                        <asp:Parameter Name="stuffUser" Type="Object" />
                        <asp:Parameter Name="position" Type="Object" />
                        <asp:Parameter Name="systemRoleId" Type="Int32" />
                    </DeleteParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="systemRoleId" Type="Int32" />
                        <asp:Parameter Name="systemRoleName" Type="String" />
                        <asp:Parameter Name="description" Type="String" />
                    </UpdateParameters>
                    <InsertParameters>
                        <asp:Parameter Name="stuffUser" Type="Object" />
                        <asp:Parameter Name="position" Type="Object" />
                        <asp:Parameter Name="systemRoleName" Type="String" />
                        <asp:Parameter Name="description" Type="String" />
                    </InsertParameters>
                </asp:ObjectDataSource>
            </td>
            <td style="width: 500px; padding-left:15px;" valign="top">
                <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <div class="title3" style="width: 482px">
                            <asp:Label runat="server" meta:resourcekey="Title_Label1"></asp:Label>
                        </div>
                        <div id="BusinessOperateArea" runat="server">
                            <div style="width: 492px; height:450px; overflow-y: auto;">
                                <gc:GridView ID="BusinessOperateGridView" AutoGenerateColumns="false" CssClass="GridView"
                                    DataKeyNames="BusinessUseCaseId" runat="server" OnRowDataBound="BusinessOperateGridView_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="BusinessUseCaseIdCtl" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField meta:resourcekey="SystemRoleGridView_Template_BusinessUseCaseName">
                                            <ItemTemplate>
                                                <asp:Label ID="BusinessUseCaseNameCtl" Text='<%# Eval("BusinessUseCaseName") %>'
                                                    runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="200px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField meta:resourcekey="SystemRoleGridView_Template_BusinessOperateName">
                                            <ItemTemplate>
                                                <asp:CheckBoxList RepeatDirection="Horizontal" ID="BusinessOperateCtl" runat="server">
                                                </asp:CheckBoxList>
                                            </ItemTemplate>
                                            <ItemStyle Width="292px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="Header" />
                                </gc:GridView>
                            </div>
                            <div style="text-align: center;">
                                <asp:Button CssClass="button_big" ID="SetSystemRoleOperateBtn" runat="server"  meta:resourcekey="SystemRole_Button_Save"
                                    OnClick="SetSystemRoleOperateBtn_Click" Enabled="false" />
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
