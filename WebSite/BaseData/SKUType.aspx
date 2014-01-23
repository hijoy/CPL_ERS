<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Culture="auto" UICulture="auto" CodeFile="SKUType.aspx.cs" Inherits="BaseData_SKUType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="title">
        <asp:Label ID="Label6" runat="server" meta:resourcekey="Label_Title"></asp:Label></div>
    <asp:UpdatePanel ID="SKUTypeUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <gc:GridView ID="SKUTypeView" CssClass="GridView" runat="server" DataSourceID="SKUTypeObjectDataSource"
                AllowPaging="false" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="SKUTypeID" CellPadding="0">
                <Columns>
                    <asp:TemplateField meta:resourcekey="SKUTypeGridView_TemplateField_SKUTypeName" SortExpression="SKUTypeName">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtSKUTypeName" runat="server" CssClass="InputText" MaxLength="20"
                                Text='<%# Bind("SKUTypeName") %>' Width="800px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="SKUTypeNameRequiredFieldValidator" runat="server"
                                ControlToValidate="txtSKUTypeName" Display="None" meta:resourcekey="RequiredFieldValidator_Name"
                                SetFocusOnError="True" ValidationGroup="EDIT"></asp:RequiredFieldValidator>
                        </EditItemTemplate>
                        <ItemStyle Width="1098px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblSKUTypeName" runat="server" Text='<%# Bind("SKUTypeName") %>'></asp:Label>
                        </ItemTemplate>
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
                            <td scope="col" style="width: 1098px" class="Empty1">
                                <asp:Label ID="Label1" runat="server" meta:resourcekey="SKUType_Label_SKUTypeName"></asp:Label>
                            </td>
                            <td scope="col" style="width: 140px">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
            </gc:GridView>
            <asp:FormView ID="SKUTypeFormView" runat="server" DataKeyNames="SKUTypeID" CellPadding="0"
                CellSpacing="0" DataSourceID="SKUTypeObjectDataSource" DefaultMode="Insert" >
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr>
                            <td align="center" style="width: 1098px">
                                <asp:TextBox ID="txtSKUTypeName" runat="server" Text='<%# Bind("SKUTypeName") %>'
                                    CssClass="InputText" Width="800px" ValidationGroup="INS"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 140px">
                                <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    Text='<%$Resources:Common,Button_Add %>' ValidationGroup="INS"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                    <asp:RequiredFieldValidator ID="SKUTypeNameRequiredFieldValidator" runat="server"
                        ControlToValidate="txtSKUTypeName" Display="None" meta:resourcekey="RequiredFieldValidator_Name"
                        SetFocusOnError="True" ValidationGroup="INS"></asp:RequiredFieldValidator>
                    <asp:ValidationSummary ID="ValidationSummaryInsert" runat="server" ShowMessageBox="True"
                        ShowSummary="False" ValidationGroup="INS" />
                </InsertItemTemplate>
            </asp:FormView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="SKUTypeObjectDataSource" runat="server" InsertMethod="InsertSKUType"
        SelectMethod="GetSKUType" TypeName="BusinessObjects.MasterDataBLL" UpdateMethod="UpdateSKUType">
        <UpdateParameters>
            <asp:Parameter Name="SKUTypeID" Type="Int32" />
            <asp:Parameter Name="SKUTypeName" Type="String" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="SKUTypeName" Type="String" />
        </InsertParameters>
    </asp:ObjectDataSource>
</asp:Content>
