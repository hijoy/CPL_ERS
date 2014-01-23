<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Culture="auto" UICulture="auto" CodeFile="MethodPayment.aspx.cs" Inherits="BaseData_MethodPayment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="title">
        <asp:Label ID="Label6" runat="server" meta:resourcekey="Label_Title"></asp:Label>
    </div>
    <asp:UpdatePanel ID="MethodPaymentUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <gc:GridView ID="MethodPaymentView" CssClass="GridView" runat="server" DataSourceID="MethodPaymentObjectDataSource"
                AllowPaging="false" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="MethodPaymentID"
                CellPadding="0">
                <Columns>
                    <asp:TemplateField meta:resourcekey="MethodPaymentGridView_TemplateField_MethodPaymentName" SortExpression="MethodPaymentName">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtMethodPaymentName" runat="server" CssClass="InputText" MaxLength="20"
                                Text='<%# Bind("MethodPaymentName") %>' Width="400px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="MethodPaymentNameRequiredFieldValidator" runat="server" ControlToValidate="txtMethodPaymentName"
                                Display="None" meta:resourcekey="RequiredFieldValidator_Name" SetFocusOnError="True" ValidationGroup="EDIT"></asp:RequiredFieldValidator>
                        </EditItemTemplate>
                        <ItemStyle Width="600px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblMethodPaymentName" runat="server" Text='<%# Bind("MethodPaymentName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="MethodPaymentGridView_TemplateField_Description" SortExpression="Description">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtDescription" runat="server" Text='<%# Bind("Description") %>' CssClass="InputText"
                                Width="330px" MaxLength="10"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle Width="350px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblMethodPaymentNo" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="IsActive" HeaderText="<%$ Resources:Common,Form_IsActive %>">
                        <EditItemTemplate>
                            <asp:CheckBox runat="server" ID="chkIsActiveByEdit" Checked='<%# Bind("IsActive") %>' />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="chkIsActiveEdit1" Enabled="false" Checked='<%# Bind("IsActive") %>' />
                        </ItemTemplate>
                        <ItemStyle Width="140px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <EditItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True"
                                ValidationGroup="skuEdit" CommandName="Update" Text='<%$Resources:Common,Button_Update %>'></asp:LinkButton>
                            <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="false"
                                CommandName="Cancel" Text='<%$Resources:Common,Button_Cancel %>'></asp:LinkButton>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="false"
                                CommandName="Edit" Text='<%$Resources:Common,Button_Edit %>'></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="150px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td scope="col" style="width: 600px" class="Empty1">
                                <asp:Label ID="Label1" runat="server"  meta:resourcekey="MethodPayment_Label_MethodPaymentName"></asp:Label>
                            </td>
                            <td scope="col" style="width: 350px">
                                <asp:Label ID="Label2" runat="server" meta:resourcekey="MethodPayment_Label_Description"></asp:Label>
                            </td>
                            <td scope="col" style="width: 140px">
                                <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Common,Form_IsActive %>"></asp:Label>
                            </td>
                            <td scope="col" style="width: 150px">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
            </gc:GridView>
            <asp:FormView ID="MethodPaymentFormView" runat="server" DataKeyNames="MethodPaymentID" CellPadding="0"
                CellSpacing="0" DataSourceID="MethodPaymentObjectDataSource" DefaultMode="Insert">
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr>
                            <td align="center" style="width: 600px">
                                <asp:TextBox ID="txtMethodPaymentName" runat="server" Text='<%# Bind("MethodPaymentName") %>' CssClass="InputText"
                                    Width="400px" ValidationGroup="INS"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 350px">
                                <asp:TextBox ID="txtDescription" runat="server" Text='<%# Bind("Description") %>' CssClass="InputText"
                                    Width="330px" ValidationGroup="INS" MaxLength="10"></asp:TextBox>
                            </td>
                            <td align="center" style="height: 22px; width: 140px;">
                                <asp:CheckBox runat="server" ID="chkIsActiveByAdd" Checked='<%# Bind("IsActive") %>' />
                            </td>
                            <td align="center" style="width: 150px">
                                <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    Text='<%$Resources:Common,Button_Add %>' ValidationGroup="INS"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                    <asp:RequiredFieldValidator ID="MethodPaymentNameRequiredFieldValidator" runat="server" ControlToValidate="txtMethodPaymentName"
                        Display="None" meta:resourcekey="RequiredFieldValidator_Name" SetFocusOnError="True" ValidationGroup="INS"></asp:RequiredFieldValidator>
                    <asp:ValidationSummary ID="ValidationSummaryInsert" runat="server" ShowMessageBox="True"
                        ShowSummary="False" ValidationGroup="INS" />
                </InsertItemTemplate>
            </asp:FormView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="MethodPaymentObjectDataSource" runat="server" InsertMethod="InsertMethodPayment"
        SelectMethod="GetMethodPayment" TypeName="BusinessObjects.MasterDataBLL" UpdateMethod="UpdateMethodPayment">
        <UpdateParameters>
            <asp:Parameter Name="MethodPaymentID" Type="Int32" />
            <asp:Parameter Name="MethodPaymentName" Type="String" />
            <asp:Parameter Name="Description" Type="String" />
            <asp:Parameter Name="IsActive" Type="Boolean" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="MethodPaymentName" Type="String" />
            <asp:Parameter Name="Description" Type="String" />
            <asp:Parameter Name="IsActive" Type="Boolean" />
        </InsertParameters>
    </asp:ObjectDataSource>
</asp:Content>
