<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Culture="auto" UICulture="auto" CodeFile="VatType.aspx.cs" Inherits="BaseData_VatType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="title">
        <asp:Label ID="Label6" runat="server" meta:resourcekey="Label_Title"></asp:Label>
    </div>
    <asp:UpdatePanel ID="VatTypeUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <gc:GridView ID="VatTypeView" CssClass="GridView" runat="server" DataSourceID="VatTypeObjectDataSource"
                AllowPaging="false" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="VatTypeID"
                CellPadding="0">
                <Columns>
                    <asp:TemplateField meta:resourcekey="VatTypeGridView_TemplateField_VatTypeName" SortExpression="VatTypeName">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtVatTypeName" runat="server" CssClass="InputText" MaxLength="20"
                                Text='<%# Bind("VatTypeName") %>' Width="250px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="VatTypeNameRequiredFieldValidator" runat="server" ControlToValidate="txtVatTypeName"
                                Display="None" meta:resourcekey="RequiredFieldValidator_Name" SetFocusOnError="True" ValidationGroup="EDIT"></asp:RequiredFieldValidator>
                        </EditItemTemplate>
                        <ItemStyle Width="300px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblVatTypeName" runat="server" Text='<%# Bind("VatTypeName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="VatTypeGridView_TemplateField_Description" SortExpression="Description">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtDescription" runat="server" Text='<%# Bind("Description") %>' CssClass="InputText"
                                Width="450px" MaxLength="10"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle Width="500px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblVatTypeNo" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="HasTax" HeaderText="是否有税金">
                        <EditItemTemplate>
                            <asp:CheckBox runat="server" ID="chkHasTaxByEdit" Checked='<%# Bind("HasTax") %>' />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="chkHasTax" Enabled="false" Checked='<%# Bind("HasTax") %>' />
                        </ItemTemplate>
                        <ItemStyle Width="150px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="IsActive" HeaderText="<%$ Resources:Common,Form_IsActive %>">
                        <EditItemTemplate>
                            <asp:CheckBox runat="server" ID="chkIsActiveByEdit" Checked='<%# Bind("IsActive") %>' />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="chkIsActiveEdit1" Enabled="false" Checked='<%# Bind("IsActive") %>' />
                        </ItemTemplate>
                        <ItemStyle Width="150px" HorizontalAlign="Center" />
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
                        <ItemStyle Width="140px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td scope="col" style="width: 300px" class="Empty1">
                                <asp:Label ID="Label1" runat="server"  meta:resourcekey="VatType_Label_VatTypeName"></asp:Label>
                            </td>
                            <td scope="col" style="width: 500px">
                                <asp:Label ID="Label2" runat="server" meta:resourcekey="VatType_Label_Description"></asp:Label>
                            </td>
                            <td scope="col" style="width: 150px">
                                <asp:Label ID="Label3" runat="server" Text="是否有税金"></asp:Label>
                            </td>
                            <td scope="col" style="width: 150px">
                                <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Common,Form_IsActive %>"></asp:Label>
                            </td>
                            <td scope="col" style="width: 140px">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
            </gc:GridView>
            <asp:FormView ID="VatTypeFormView" runat="server" DataKeyNames="VatTypeID" CellPadding="0"
                CellSpacing="0" DataSourceID="VatTypeObjectDataSource" DefaultMode="Insert" >
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr>
                            <td align="center" style="width: 300px">
                                <asp:TextBox ID="txtVatTypeName" runat="server" Text='<%# Bind("VatTypeName") %>' CssClass="InputText"
                                    Width="250px" ValidationGroup="INS"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 500px">
                                <asp:TextBox ID="txtDescription" runat="server" Text='<%# Bind("Description") %>' CssClass="InputText"
                                    Width="450px" ValidationGroup="INS" MaxLength="10"></asp:TextBox>
                            </td>
                            <td align="center" style="height: 22px; width: 150px;">
                                <asp:CheckBox runat="server" ID="chkHasTaxByAdd" Checked='<%# Bind("HasTax") %>' />
                            </td>
                            <td align="center" style="height: 22px; width: 150px;">
                                <asp:CheckBox runat="server" ID="chkIsActiveByAdd" Checked='<%# Bind("IsActive") %>' />
                            </td>
                            <td align="center" style="width: 140px">
                                <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    Text='<%$Resources:Common,Button_Add %>' ValidationGroup="INS"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                    <asp:RequiredFieldValidator ID="VatTypeNameRequiredFieldValidator" runat="server" ControlToValidate="txtVatTypeName"
                        Display="None" meta:resourcekey="RequiredFieldValidator_Name" SetFocusOnError="True" ValidationGroup="INS"></asp:RequiredFieldValidator>
                    <asp:ValidationSummary ID="ValidationSummaryInsert" runat="server" ShowMessageBox="True"
                        ShowSummary="False" ValidationGroup="INS" />
                </InsertItemTemplate>
            </asp:FormView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="VatTypeObjectDataSource" runat="server" InsertMethod="InsertVatType"
        SelectMethod="GetVatType" TypeName="BusinessObjects.MasterDataBLL" UpdateMethod="UpdateVatType">
        <UpdateParameters>
            <asp:Parameter Name="VatTypeID" Type="Int32" />
            <asp:Parameter Name="VatTypeName" Type="String" />
            <asp:Parameter Name="Description" Type="String" />
            <asp:Parameter Name="HasTax" Type="Boolean" />
            <asp:Parameter Name="IsActive" Type="Boolean" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="VatTypeName" Type="String" />
            <asp:Parameter Name="Description" Type="String" />
            <asp:Parameter Name="HasTax" Type="Boolean" />
            <asp:Parameter Name="IsActive" Type="Boolean" />
        </InsertParameters>
    </asp:ObjectDataSource>
</asp:Content>
