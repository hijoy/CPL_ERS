<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Culture="auto" UICulture="auto"
    CodeFile="ItemCategory.aspx.cs" Inherits="BaseData_ItemCategory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="title">
        <asp:Label ID="Label_Title" runat="server" meta:resourcekey="Label_Title"></asp:Label></div>

            <asp:UpdatePanel ID="ItemCategoryUpdatePanel" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <gc:GridView ID="ItemCategoryView" CssClass="GridView" runat="server" DataSourceID="ItemCategoryObjectDataSource"
                        AutoGenerateColumns="False" DataKeyNames="ItemCategoryID" CellPadding="0">
                        <Columns>
                            <asp:TemplateField meta:resourcekey="ItemCategoryGridView_TemplateField_ItemCategoryName">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtItemCategoryName" runat="server" CssClass="InputText" MaxLength="20"
                                        Text='<%# Bind("ItemCategoryName") %>' Width="180px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="ItemCategoryNameRequiredFieldValidator" runat="server"
                                        ControlToValidate="txtItemCategoryName" Display="None" meta:resourcekey="RequiredFieldValidator_ItemCategoryName"
                                        SetFocusOnError="True" ValidationGroup="EDIT"></asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <ItemStyle Width="250px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblItemCategoryName" runat="server" Text='<%# Bind("ItemCategoryName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField meta:resourcekey="ItemCategoryGridView_TemplateField_ItemCategoryCode">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtItemCategoryCode" runat="server" Text='<%# Bind("ItemCategoryCode") %>'
                                        CssClass="InputText" Width="180px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="ItemCategoryCodeRequiredFieldValidator" runat="server"
                                        ControlToValidate="txtItemCategoryCode" Display="None" meta:resourcekey="RequiredFieldValidator_ItemCategoryCode"
                                        SetFocusOnError="True" ValidationGroup="EDIT"></asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <ItemStyle Width="150px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblItemCategoryCode" runat="server" Text='<%# Bind("ItemCategoryCode") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField meta:resourcekey="TemplateField_AccountingCode">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtAccountingCode" runat="server" Text='<%# Bind("AccountingCode") %>'
                                        CssClass="InputText" Width="180px" ></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="AccountingCodeRequiredFieldValidator" runat="server"
                                        ControlToValidate="txtAccountingCode" Display="None" meta:resourcekey="RequiredFieldValidator_AccountingCode"
                                        SetFocusOnError="True" ValidationGroup="EDIT"></asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <ItemStyle Width="150px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblAccountingCode" runat="server" Text='<%# Bind("AccountingCode") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField meta:resourcekey="TemplateField_AccountingName" >
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtAccountingName" runat="server" Text='<%# Bind("AccountingName") %>'
                                        CssClass="InputText" Width="180px" ></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="AccountingNameRequiredFieldValidator" runat="server"
                                        ControlToValidate="txtAccountingName" Display="None" meta:resourcekey="RequiredFieldValidator_AccountingName"
                                        SetFocusOnError="True" ValidationGroup="EDIT"></asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <ItemStyle Width="250px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblAccountingName" runat="server" Text='<%# Bind("AccountingName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:Common,Form_Remark %>" SortExpression="Remark">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtRemark" runat="server" CssClass="InputText" MaxLength="20" Text='<%# Bind("Remark") %>'
                                        Width="300px"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle Width="340px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblRemark" runat="server" Text='<%# Bind("Remark") %>'></asp:Label>
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
                                <ItemStyle Width="100px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="Header" />
                        <EmptyDataTemplate>
                            <table>
                                <tr class="Header">
                                    <td scope="col" style="width: 250px" >
                                        <asp:Label ID="Label1" runat="server" meta:resourcekey="ItemCategory_Label_ItemCategoryName"></asp:Label>
                                    </td>
                                    <td scope="col" style="width: 150px">
                                        <asp:Label ID="Label2" runat="server" meta:resourcekey="ItemCategory_Label_ItemCategoryCode"></asp:Label>
                                    </td>
                                    <td scope="col" style="width: 150px">
                                        <asp:Label ID="Label4" runat="server" meta:resourcekey="Label_AccountingCode"></asp:Label>
                                    </td>
                                    <td scope="col" style="width: 250px">
                                        <asp:Label ID="Label5" runat="server" meta:resourcekey="Label_AccountingName"></asp:Label>
                                    </td>
                                    <td scope="col" style="width: 340px">
                                        <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Common,Form_Remark %>"></asp:Label>
                                    </td>
                                    <td scope="col" style="width: 100px">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                    </gc:GridView>
                    <asp:FormView ID="ItemCategoryFormView" runat="server" DataKeyNames="ItemCategoryID"
                        CellPadding="0" CellSpacing="0" DataSourceID="ItemCategoryObjectDataSource" DefaultMode="Insert">
                        <InsertItemTemplate>
                            <table class="FormView">
                                <tr>
                                    <td align="center" style="width: 250px">
                                        <asp:TextBox ID="txtNewItemCategoryName" runat="server" Text='<%# Bind("ItemCategoryName") %>'
                                            CssClass="InputText" Width="220px" ValidationGroup="INS"></asp:TextBox>
                                    </td>
                                    <td align="center" style="width: 150px">
                                        <asp:TextBox ID="txtNewItemCategoryCode" runat="server" Text='<%# Bind("ItemCategoryCode") %>'
                                            CssClass="InputText" Width="120px" ValidationGroup="INS" ></asp:TextBox>
                                    </td>
                                    <td align="center" style="width: 150px">
                                        <asp:TextBox ID="txtNewAccountingCode" runat="server" Text='<%# Bind("AccountingCode") %>'
                                            CssClass="InputText" Width="120px" ValidationGroup="INS" ></asp:TextBox>
                                    </td>
                                    <td align="center" style="width: 250px">
                                        <asp:TextBox ID="txtNewAccountingName" runat="server" Text='<%# Bind("AccountingName") %>'
                                            CssClass="InputText" Width="220px" ValidationGroup="INS" ></asp:TextBox>
                                    </td>
                                    <td align="center" style="width: 340px">
                                        <asp:TextBox ID="txtRemark" runat="server" Text='<%# Bind("Remark") %>' CssClass="InputText"
                                            Width="300px" ValidationGroup="INS" MaxLength="100"></asp:TextBox>
                                    </td>
                                    <td align="center" style="width: 100px">
                                        <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                                            Text='<%$Resources:Common,Button_Add %>' ValidationGroup="INS"></asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                            <asp:RequiredFieldValidator ID="ItemCategoryNameRequiredFieldValidator" runat="server"
                                ControlToValidate="txtNewItemCategoryName" Display="None" meta:resourcekey="RequiredFieldValidator_ItemCategoryName"
                                SetFocusOnError="True" ValidationGroup="INS"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="ItemCategoryCodeRequiredFieldValidator" runat="server"
                                ControlToValidate="txtNewItemCategoryCode" Display="None" meta:resourcekey="RequiredFieldValidator_ItemCategoryCode"
                                SetFocusOnError="True" ValidationGroup="INS"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                ControlToValidate="txtNewAccountingCode" Display="None" meta:resourcekey="RequiredFieldValidator_AccountingCode"
                                SetFocusOnError="True" ValidationGroup="INS"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                ControlToValidate="txtNewAccountingName" Display="None" meta:resourcekey="RequiredFieldValidator_AccountingName"
                                SetFocusOnError="True" ValidationGroup="INS"></asp:RequiredFieldValidator>
                            <asp:ValidationSummary ID="ValidationSummaryInsert" runat="server" ShowMessageBox="True"
                                ShowSummary="False" ValidationGroup="INS" />
                        </InsertItemTemplate>
                    </asp:FormView>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:ObjectDataSource ID="ItemCategoryObjectDataSource" runat="server" InsertMethod="InsertItemCategory"
                SelectMethod="GetItemCategory" TypeName="BusinessObjects.MasterDataBLL" UpdateMethod="UpdateItemCategory">
                <UpdateParameters>
                    <asp:Parameter Name="ItemCategoryID" Type="Int32" />
                    <asp:Parameter Name="ItemCategoryCode" Type="String" />
                    <asp:Parameter Name="ItemCategoryName" Type="String" />
                    <asp:Parameter Name="Remark" Type="String" />
                    <asp:Parameter Name="AccountingCode" Type="String" />
                    <asp:Parameter Name="AccountingName" Type="String" />
                </UpdateParameters>
                <InsertParameters>
                    <asp:Parameter Name="ItemCategoryCode" Type="String" />
                    <asp:Parameter Name="ItemCategoryName" Type="String" />
                    <asp:Parameter Name="Remark" Type="String" />
                    <asp:Parameter Name="AccountingCode" Type="String" />
                    <asp:Parameter Name="AccountingName" Type="String" />
                </InsertParameters>
            </asp:ObjectDataSource>

</asp:Content>
