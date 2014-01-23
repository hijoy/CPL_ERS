<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Culture="auto" UICulture="auto" CodeFile="InvoiceStatus.aspx.cs" Inherits="BaseData_InvoiceStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="title">
        <asp:Label ID="Label6" runat="server" meta:resourcekey="Label_Title"></asp:Label></div>
    <asp:UpdatePanel ID="InvoiceStatusUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <gc:GridView ID="InvoiceStatusView" CssClass="GridView" runat="server" DataSourceID="InvoiceStatusObjectDataSource"
                AllowPaging="false" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="InvoiceStatusID"
                CellPadding="0">
                <Columns>
                    <asp:TemplateField HeaderText="<%$Resources:Common,Form_InvoiceStatus %>" SortExpression="Name">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtInvoiceStatusName" runat="server" CssClass="InputText" MaxLength="20"
                                Text='<%# Bind("Name") %>' Width="800px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="InvoiceStatusNameRequiredFieldValidator" runat="server"
                                ControlToValidate="txtInvoiceStatusName" Display="None" meta:resourcekey="RequiredFieldValidator_Name"
                                SetFocusOnError="True" ValidationGroup="EDIT"></asp:RequiredFieldValidator>
                        </EditItemTemplate>
                        <ItemStyle Width="988px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblInvoiceStatusName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="NeedInvoice" meta:resourcekey="InvoiceStatusGridView_TemplateField_NeedInvoiceEdit">
                        <EditItemTemplate>
                            <asp:CheckBox runat="server" ID="chkNeedInvoiceByEdit" Checked='<%# Bind("NeedInvoice") %>' />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="chkNeedInvoiceEdit1" Enabled="false" Checked='<%# Bind("NeedInvoice") %>' />
                        </ItemTemplate>
                        <ItemStyle Width="140px" HorizontalAlign="Center" />
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
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td scope="col" style="width: 988px">
                                <asp:Label ID="Label1" runat="server" Text="<%$Resources:Common,Form_InvoiceStatus %>"></asp:Label>
                            </td>
                            <td scope="col" style="width: 140px">
                                <asp:Label ID="Label4" runat="server" meta:resourcekey="InvoiceStatus_Label_NeedInvoice"></asp:Label>
                            </td>
                            <td scope="col" style="width: 100px">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
            </gc:GridView>
            <asp:FormView ID="InvoiceStatusFormView" runat="server" DataKeyNames="InvoiceStatusID"
                CellPadding="0" CellSpacing="0" DataSourceID="InvoiceStatusObjectDataSource"
                DefaultMode="Insert" Visible="false">
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr>
                            <td align="center" style="width: 988px">
                                <asp:TextBox ID="txtInvoiceStatusName" runat="server" Text='<%# Bind("Name") %>'
                                    CssClass="InputText" Width="800px" ValidationGroup="INS"></asp:TextBox>
                            </td>
                            <td align="center" style="height: 22px; width: 140px;">
                                <asp:CheckBox runat="server" ID="chkNeedInvoiceByAdd" Checked='<%# Bind("NeedInvoice") %>' />
                            </td>
                            <td align="center" style="width: 100px">
                                <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    Text='<%$Resources:Common,Button_Add %>' ValidationGroup="INS"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                    <asp:RequiredFieldValidator ID="InvoiceStatusNameRequiredFieldValidator" runat="server"
                        ControlToValidate="txtInvoiceStatusName" Display="None" meta:resourcekey="RequiredFieldValidator_Name"
                        SetFocusOnError="True" ValidationGroup="INS"></asp:RequiredFieldValidator>
                    <asp:ValidationSummary ID="ValidationSummaryInsert" runat="server" ShowMessageBox="True"
                        ShowSummary="False" ValidationGroup="INS" />
                </InsertItemTemplate>
            </asp:FormView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="InvoiceStatusObjectDataSource" runat="server" InsertMethod="InsertInvoiceStatus"
        SelectMethod="GetInvoiceStatus" TypeName="BusinessObjects.MasterDataBLL" UpdateMethod="UpdateInvoiceStatus">
        <UpdateParameters>
            <asp:Parameter Name="InvoiceStatusID" Type="Int32" />
            <asp:Parameter Name="Name" Type="String" />
            <asp:Parameter Name="NeedInvoice" Type="Boolean" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="Name" Type="String" />
            <asp:Parameter Name="NeedInvoice" Type="Boolean" />
        </InsertParameters>
    </asp:ObjectDataSource>
</asp:Content>
