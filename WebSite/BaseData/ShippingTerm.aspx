<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Culture="auto" UICulture="auto" CodeFile="ShippingTerm.aspx.cs" Inherits="BaseData_ShippingTerm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="title">
        <asp:Label ID="Label6" runat="server" meta:resourcekey="Label_Title"></asp:Label></div>
    <asp:UpdatePanel ID="ShippingTermUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <gc:GridView ID="ShippingTermView" CssClass="GridView" runat="server" DataSourceID="ShippingTermObjectDataSource"
                AllowPaging="false" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="ShippingTermID" CellPadding="0">
                <Columns>
                    <asp:TemplateField meta:resourcekey="ShippingTermGridView_TemplateField_ShippingTermName"
                        SortExpression="ShippingTermName">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtShippingTermName" runat="server" CssClass="InputText" MaxLength="20"
                                Text='<%# Bind("ShippingTermName") %>' Width="440px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="ShippingTermNameRequiredFieldValidator" runat="server"
                                ControlToValidate="txtShippingTermName" Display="None" meta:resourcekey="RequiredFieldValidator_Name"
                                SetFocusOnError="True" ValidationGroup="EDIT"></asp:RequiredFieldValidator>
                        </EditItemTemplate>
                        <ItemStyle Width="520px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblShippingTermName" runat="server" Text='<%# Bind("ShippingTermName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="ShippingTermGridView_TemplateField_ShippingTermCode"
                        SortExpression="ShippingTermCode">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtShippingTermCode" runat="server" Text='<%# Bind("ShippingTermCode") %>'
                                CssClass="InputText" Width="400px" MaxLength="10"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="ShippingTermCodeRequiredFieldValidator" runat="server"
                                ControlToValidate="txtShippingTermCode" Display="None" meta:resourcekey="RequiredFieldValidator_Code"
                                SetFocusOnError="True" ValidationGroup="EDIT"></asp:RequiredFieldValidator>
                        </EditItemTemplate>
                        <ItemStyle Width="520px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblShippingTermCode" runat="server" Text='<%# Bind("ShippingTermCode") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="IsActive" HeaderText="<%$ Resources:Common,Form_IsActive %>">
                        <EditItemTemplate>
                            <asp:CheckBox runat="server" ID="chkActiveByEdit" Checked='<%# Bind("IsActive") %>' />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="chkActiveByEdit1" Enabled="false" Checked='<%# Bind("IsActive") %>' />
                        </ItemTemplate>
                        <ItemStyle Width="70px" HorizontalAlign="Center" />
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
                        <ItemStyle Width="127px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td scope="col" style="width: 520px" class="Empty1">
                                <asp:Label ID="Label1" runat="server" meta:resourcekey="ShippingTerm_Label_ShippingTermName"></asp:Label>
                            </td>
                            <td scope="col" style="width: 520px">
                                <asp:Label ID="Label2" runat="server" meta:resourcekey="ShippingTerm_Label_ShippingTermCode"></asp:Label>
                            </td>
                            <td scope="col" style="width: 70px">
                                <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Common,Form_IsActive %>"></asp:Label>
                            </td>
                            <td scope="col" style="width: 127px">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
            </gc:GridView>
            <asp:FormView ID="ShippingTermFormView" runat="server" DataKeyNames="ShippingTermID"
                CellPadding="0" CellSpacing="0" DataSourceID="ShippingTermObjectDataSource" DefaultMode="Insert">
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr>
                            <td align="center" style="width: 520px">
                                <asp:TextBox ID="txtShippingTermName" runat="server" Text='<%# Bind("ShippingTermName") %>'
                                    CssClass="InputText" Width="400px" ValidationGroup="INS"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 520px">
                                <asp:TextBox ID="txtShippingTermCode" runat="server" Text='<%# Bind("ShippingTermCode") %>'
                                    CssClass="InputText" Width="400px" ValidationGroup="INS" MaxLength="10"></asp:TextBox>
                            </td>
                            <td align="center" style="height: 22px; width: 70px;">
                                <asp:CheckBox runat="server" ID="chkActiveByAdd" Checked='<%# Bind("IsActive") %>' />
                            </td>
                            <td align="center" style="width: 127px">
                                <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    Text='<%$Resources:Common,Button_Add %>' ValidationGroup="INS"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                    <asp:RequiredFieldValidator ID="ShippingTermNameRequiredFieldValidator" runat="server"
                        ControlToValidate="txtShippingTermName" Display="None" meta:resourcekey="RequiredFieldValidator_Name"
                        SetFocusOnError="True" ValidationGroup="INS"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="ShippingTermCodeRequiredFieldValidator" runat="server"
                        ControlToValidate="txtShippingTermCode" Display="None" meta:resourcekey="RequiredFieldValidator_Code"
                        SetFocusOnError="True" ValidationGroup="INS"></asp:RequiredFieldValidator>
                    <asp:ValidationSummary ID="ValidationSummaryInsert" runat="server" ShowMessageBox="True"
                        ShowSummary="False" ValidationGroup="INS" />
                </InsertItemTemplate>
            </asp:FormView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="ShippingTermObjectDataSource" runat="server" InsertMethod="InsertShippingTerm"
        SelectMethod="GetShippingTerm" TypeName="BusinessObjects.MasterDataBLL" UpdateMethod="UpdateShippingTerm">
        <UpdateParameters>
            <asp:Parameter Name="ShippingTermID" Type="Int32" />
            <asp:Parameter Name="ShippingTermCode" Type="String" />
            <asp:Parameter Name="ShippingTermName" Type="String" />
            <asp:Parameter Name="IsActive" Type="Boolean" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="ShippingTermCode" Type="String" />
            <asp:Parameter Name="ShippingTermName" Type="String" />
            <asp:Parameter Name="IsActive" Type="Boolean" />
        </InsertParameters>
    </asp:ObjectDataSource>
</asp:Content>
