<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Culture="auto" UICulture="auto" CodeFile="GLAccount.aspx.cs" Inherits="BaseData_GLAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="title">
        <asp:Label ID="Label6" runat="server" meta:resourcekey="Label_Title"></asp:Label></div>
    <asp:UpdatePanel ID="GLAccountUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <gc:GridView ID="GLAccountView" CssClass="GridView" runat="server" DataSourceID="GLAccountObjectDataSource"
                AllowPaging="false" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="GLAccountID"
                CellPadding="0">
                <Columns>
                    <asp:TemplateField meta:resourcekey="GLAccountGridView_TemplateField_GLAccountName"
                        SortExpression="GLAccountName">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtGLAccountName" runat="server" CssClass="InputText" MaxLength="20"
                                Text='<%# Bind("GLAccountName") %>' Width="440px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="GLAccountNameRequiredFieldValidator" runat="server"
                                ControlToValidate="txtGLAccountName" Display="None" meta:resourcekey="RequiredFieldValidator_GLAccountName"
                                SetFocusOnError="True" ValidationGroup="EDIT"></asp:RequiredFieldValidator>
                        </EditItemTemplate>
                        <ItemStyle Width="688px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblGLAccountName" runat="server" Text='<%# Bind("GLAccountName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="GLAccountGridView_TemplateField_GLAccountCode"
                        SortExpression="GLAccountCode">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtGLAccountCode" runat="server" Text='<%# Bind("GLAccountCode") %>'
                                CssClass="InputText" Width="380px" MaxLength="10"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="GLAccountCodeRequiredFieldValidator" runat="server"
                                ControlToValidate="txtGLAccountCode" Display="None" meta:resourcekey="RequiredFieldValidator_GLAccountCode"
                                SetFocusOnError="True" ValidationGroup="EDIT"></asp:RequiredFieldValidator>
                        </EditItemTemplate>
                        <ItemStyle Width="400px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblGLAccountCode" runat="server" Text='<%# Bind("GLAccountCode") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <EditItemTemplate>
                            <asp:LinkButton ID="LinkButton1" Visible="<%# HasManageRight %>" runat="server" CausesValidation="True"
                                ValidationGroup="skuEdit" CommandName="Update" Text='<%$Resources:Common,Button_Update %>'></asp:LinkButton>
                            <asp:LinkButton ID="LinkButton3" Visible="<%# HasManageRight %>" runat="server" CausesValidation="false"
                                CommandName="Cancel" Text='<%$Resources:Common,Button_Cancel %>'></asp:LinkButton>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton2" Visible="<%# HasManageRight %>" runat="server" CausesValidation="false"
                                CommandName="Edit" Text='<%$Resources:Common,Button_Edit %>'></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="150px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td scope="col" style="width: 688px" class="Empty1">
                                <asp:Label ID="Label1" runat="server" meta:resourcekey="GLAccount_Label_GLAccountName"></asp:Label>
                            </td>
                            <td scope="col" style="width: 400px">
                                <asp:Label ID="Label2" runat="server" meta:resourcekey="GLAccount_Label_GLAccountCode"></asp:Label>
                            </td>
                            <td scope="col" style="width: 150px">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
            </gc:GridView>
            <asp:FormView ID="GLAccountFormView" runat="server" DataKeyNames="GLAccountID" CellPadding="0"
                CellSpacing="0" DataSourceID="GLAccountObjectDataSource" DefaultMode="Insert"
                Visible="<%# HasManageRight %>">
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr>
                            <td align="center" style="width: 688px">
                                <asp:TextBox ID="txtGLAccountName" runat="server" Text='<%# Bind("GLAccountName") %>'
                                    CssClass="InputText" Width="440px" ValidationGroup="INS"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 400px">
                                <asp:TextBox ID="txtGLAccountCode" runat="server" Text='<%# Bind("GLAccountCode") %>'
                                    CssClass="InputText" Width="380px" ValidationGroup="INS" MaxLength="10"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 150px">
                                <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    Text='<%$Resources:Common,Button_Add %>' ValidationGroup="INS"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                    <asp:RequiredFieldValidator ID="GLAccountNameRequiredFieldValidator" runat="server"
                        ControlToValidate="txtGLAccountName" Display="None" meta:resourcekey="RequiredFieldValidator_GLAccountName"
                        SetFocusOnError="True" ValidationGroup="INS"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="GLAccountCodeRequiredFieldValidator" runat="server"
                        ControlToValidate="txtGLAccountCode" Display="None" meta:resourcekey="RequiredFieldValidator_GLAccountCode"
                        SetFocusOnError="True" ValidationGroup="INS"></asp:RequiredFieldValidator>
                    <asp:ValidationSummary ID="ValidationSummaryInsert" runat="server" ShowMessageBox="True"
                        ShowSummary="False" ValidationGroup="INS" />
                </InsertItemTemplate>
            </asp:FormView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="GLAccountObjectDataSource" runat="server" InsertMethod="InsertGLAccount"
        SelectMethod="GetGLAccount" TypeName="BusinessObjects.MasterDataBLL" UpdateMethod="UpdateGLAccount">
        <UpdateParameters>
            <asp:Parameter Name="GLAccountID" Type="Int32" />
            <asp:Parameter Name="GLAccountCode" Type="String" />
            <asp:Parameter Name="GLAccountName" Type="String" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="GLAccountCode" Type="String" />
            <asp:Parameter Name="GLAccountName" Type="String" />
        </InsertParameters>
    </asp:ObjectDataSource>
</asp:Content>
