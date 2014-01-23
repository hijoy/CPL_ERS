<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="PaymentType.aspx.cs" Culture="auto" UICulture="auto" Inherits="BaseData_PaymentType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="title">
        <asp:Label ID="Label6" runat="server" meta:resourcekey="Label_Title"></asp:Label></div>
    <asp:UpdatePanel ID="PaymentTypeUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <gc:GridView ID="PaymentTypeView" CssClass="GridView" runat="server" DataSourceID="PaymentTypeObjectDataSource"
                AllowPaging="false" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="PaymentTypeID"
                CellPadding="0">
                <Columns>
                    <asp:TemplateField HeaderText="<%$Resources:Common,Form_PaymentType %>" SortExpression="PaymentTypeName">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtPaymentTypeName" runat="server" CssClass="InputText" MaxLength="20"
                                Text='<%# Bind("PaymentTypeName") %>' Width="800px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="PaymentTypeNameRequiredFieldValidator" runat="server"
                                ControlToValidate="txtPaymentTypeName" Display="None" meta:resourcekey="RequiredFieldValidator_Name"
                                SetFocusOnError="True" ValidationGroup="EDIT"></asp:RequiredFieldValidator>
                        </EditItemTemplate>
                        <ItemStyle Width="1098px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblPaymentTypeName" runat="server" Text='<%# Bind("PaymentTypeName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <EditItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" ValidationGroup="skuEdit"
                                CommandName="Update" Text='<%$Resources:Common,Button_Update %>'></asp:LinkButton>
                            <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="false" CommandName="Cancel"
                                Text='<%$Resources:Common,Button_Cancel %>'></asp:LinkButton>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="false" CommandName="Edit"
                                Text='<%$Resources:Common,Button_Edit %>'></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="140px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td scope="col" style="width: 1098px" class="Empty1">
                                <asp:Label ID="Label1" runat="server" Text="<%$Resources:Common,Form_PaymentType %>"></asp:Label>
                            </td>
                            <td scope="col" style="width: 140px">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
            </gc:GridView>
            <asp:FormView ID="PaymentTypeFormView" runat="server" DataKeyNames="PaymentTypeID"
                Visible="false" CellPadding="0" CellSpacing="0" DataSourceID="PaymentTypeObjectDataSource"
                DefaultMode="Insert">
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr>
                            <td align="center" style="width: 1098px">
                                <asp:TextBox ID="txtPaymentTypeName" runat="server" Text='<%# Bind("PaymentTypeName") %>'
                                    CssClass="InputText" Width="800px" ValidationGroup="INS"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 140px">
                                <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    Text='<%$Resources:Common,Button_Add %>' ValidationGroup="INS"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                    <asp:RequiredFieldValidator ID="PaymentTypeNameRequiredFieldValidator" runat="server"
                        ControlToValidate="txtPaymentTypeName" Display="None" meta:resourcekey="RequiredFieldValidator_Name"
                        SetFocusOnError="True" ValidationGroup="INS"></asp:RequiredFieldValidator>
                    <asp:ValidationSummary ID="ValidationSummaryInsert" runat="server" ShowMessageBox="True"
                        ShowSummary="False" ValidationGroup="INS" />
                </InsertItemTemplate>
            </asp:FormView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="PaymentTypeObjectDataSource" runat="server" InsertMethod="InsertPaymentType"
        SelectMethod="GetPaymentType" TypeName="BusinessObjects.MasterDataBLL" UpdateMethod="UpdatePaymentType">
    </asp:ObjectDataSource>
</asp:Content>
