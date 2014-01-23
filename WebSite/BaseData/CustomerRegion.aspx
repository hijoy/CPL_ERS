<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Culture="auto" UICulture="auto" CodeFile="CustomerRegion.aspx.cs" Inherits="BaseData_CustomerRegion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

  <div class="title">
        <asp:Label ID="Label6" runat="server" meta:resourcekey="Label_Title"></asp:Label></div>
            <asp:UpdatePanel ID="CustomerRegionUpdatePanel" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <gc:GridView ID="CustomerRegionView" CssClass="GridView" runat="server" DataSourceID="CustomerRegionObjectDataSource"
                        AllowPaging="false" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="CustomerRegionID" CellPadding="0">
                        <Columns>
                            <asp:TemplateField HeaderText="<%$Resources:Common,Form_CustomerRegion %>" SortExpression="CustomerRegionName">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtCustomerRegionName" runat="server" CssClass="InputText" MaxLength="20"
                                        Text='<%# Bind("CustomerRegionName") %>' Width="800px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="CustomerRegionNameRequiredFieldValidator" runat="server"
                                        ControlToValidate="txtCustomerRegionName" Display="None" meta:resourcekey="RequiredFieldValidator_Name"
                                        SetFocusOnError="True" ValidationGroup="EDIT"></asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <ItemStyle Width="1098px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblCustomerRegionName" runat="server" Text='<%# Bind("CustomerRegionName") %>'></asp:Label>
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
                                        <asp:Label ID="Label1" runat="server" Text="<%$Resources:Common,Form_CustomerRegion %>"></asp:Label>
                                    </td>
                                    <td scope="col" style="width: 140px">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                    </gc:GridView>
                    <asp:FormView ID="CustomerRegionFormView" runat="server" DataKeyNames="CustomerRegionID"
                        CellPadding="0" CellSpacing="0" DataSourceID="CustomerRegionObjectDataSource" DefaultMode="Insert" >
                        <InsertItemTemplate>
                            <table class="FormView">
                                <tr>
                                    <td align="center" style="width: 1098px">
                                        <asp:TextBox ID="txtCustomerRegionName" runat="server" Text='<%# Bind("CustomerRegionName") %>'
                                            CssClass="InputText" Width="800px" ValidationGroup="INS"></asp:TextBox>
                                    </td>
                                    <td align="center" style="width: 140px">
                                        <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                                            Text='<%$Resources:Common,Button_Add %>' ValidationGroup="INS"></asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                            <asp:RequiredFieldValidator ID="CustomerRegionNameRequiredFieldValidator" runat="server"
                                ControlToValidate="txtCustomerRegionName" Display="None" meta:resourcekey="RequiredFieldValidator_Name"
                                SetFocusOnError="True" ValidationGroup="INS"></asp:RequiredFieldValidator>
                            <asp:ValidationSummary ID="ValidationSummaryInsert" runat="server" ShowMessageBox="True"
                                ShowSummary="False" ValidationGroup="INS" />
                        </InsertItemTemplate>
                    </asp:FormView>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:ObjectDataSource ID="CustomerRegionObjectDataSource" runat="server" InsertMethod="InsertCustomerRegion"
                SelectMethod="GetCustomerRegion" TypeName="BusinessObjects.MasterDataBLL"
                UpdateMethod="UpdateCustomerRegion">
                <UpdateParameters>
                    <asp:Parameter Name="CustomerRegionID" Type="Int32" />
                    <asp:Parameter Name="CustomerRegionName" Type="String" />
                </UpdateParameters>
                <InsertParameters>
                    <asp:Parameter Name="CustomerRegionName" Type="String" />
                </InsertParameters>
            </asp:ObjectDataSource>

</asp:Content>

