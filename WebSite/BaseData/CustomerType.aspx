<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Culture="auto" UICulture="auto" CodeFile="CustomerType.aspx.cs" Inherits="BaseData_CustomerType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="title">
        <asp:Label ID="Label6" runat="server" meta:resourcekey="Label_Title"></asp:Label></div>
    <asp:UpdatePanel ID="CustomerTypeUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <gc:GridView ID="CustomerTypeView" CssClass="GridView" runat="server" DataSourceID="CustomerTypeObjectDataSource"
                AllowPaging="false" AutoGenerateColumns="False" DataKeyNames="CustomerTypeID"
                CellPadding="0">
                <Columns>
                    <asp:TemplateField meta:resourcekey="CustomerTypeGridView_TemplateField_CustomerTypeName">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCustomerTypeName" runat="server" CssClass="InputText" MaxLength="20"
                                Text='<%# Bind("CustomerTypeName") %>' Width="800px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="CustomerTypeNameRequiredFieldValidator" runat="server"
                                ControlToValidate="txtCustomerTypeName" Display="None" meta:resourcekey="RequiredFieldValidator_Name"
                                SetFocusOnError="True" ValidationGroup="EDIT"></asp:RequiredFieldValidator>
                        </EditItemTemplate>
                        <ItemStyle Width="997px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblCustomerTypeName" runat="server" Text='<%# Bind("CustomerTypeName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="CustomerTypeGridView_TemplateField_IsActive">
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
                            <asp:LinkButton ID="LinkButton1"  runat="server" CausesValidation="True"
                                ValidationGroup="skuEdit" CommandName="Update" Text='<%$Resources:Common,Button_Update %>'></asp:LinkButton>
                            <asp:LinkButton ID="LinkButton3"  runat="server" CausesValidation="false"
                                CommandName="Cancel" Text='<%$Resources:Common,Button_Cancel %>'></asp:LinkButton>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="false"
                                CommandName="Edit" Text='<%$Resources:Common,Button_Edit %>'></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td scope="col" style="width: 997px">
                                <asp:Label ID="Label1" runat="server" meta:resourcekey="CustomerType_Label_CustomerTypeName"></asp:Label>
                            </td>
                            <td scope="col" style="width: 140px">
                                <asp:Label ID="Label4" runat="server" meta:resourcekey="CustomerType_Label_IsActive"></asp:Label>
                            </td>
                            <td scope="col" style="width: 100px">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
            </gc:GridView>
            <asp:FormView ID="CustomerTypeFormView" runat="server" DataKeyNames="CustomerTypeID"
                CellPadding="0" CellSpacing="0" DataSourceID="CustomerTypeObjectDataSource" DefaultMode="Insert">
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr>
                            <td align="center" style="width: 997px">
                                <asp:TextBox ID="txtCustomerTypeName" runat="server" Text='<%# Bind("CustomerTypeName") %>'
                                    CssClass="InputText" Width="800px" ValidationGroup="INS"></asp:TextBox>
                            </td>
                            <td align="center" style="height: 22px; width: 140px;">
                                <asp:CheckBox runat="server" ID="chkIsActiveByAdd" Checked='<%# Bind("IsActive") %>' />
                            </td>
                            <td align="center" style="width: 100px">
                                <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    Text='<%$Resources:Common,Button_Add %>' ValidationGroup="INS"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                    <asp:RequiredFieldValidator ID="CustomerTypeNameRequiredFieldValidator" runat="server"
                        ControlToValidate="txtCustomerTypeName" Display="None" meta:resourcekey="RequiredFieldValidator_Name"
                        SetFocusOnError="True" ValidationGroup="INS"></asp:RequiredFieldValidator>
                    <asp:ValidationSummary ID="ValidationSummaryInsert" runat="server" ShowMessageBox="True"
                        ShowSummary="False" ValidationGroup="INS" />
                </InsertItemTemplate>
            </asp:FormView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="CustomerTypeObjectDataSource" runat="server" InsertMethod="InsertCustomerType"
        SelectMethod="GetCustomerType" TypeName="BusinessObjects.MasterDataBLL" UpdateMethod="UpdateCustomerType">
        <UpdateParameters>
            <asp:Parameter Name="CustomerTypeID" Type="Int32" />
            <asp:Parameter Name="CustomerTypeName" Type="String" />
            <asp:Parameter Name="IsActive" Type="Boolean" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="CustomerTypeName" Type="String" />
            <asp:Parameter Name="IsActive" Type="Boolean" />
        </InsertParameters>
    </asp:ObjectDataSource>
</asp:Content>
