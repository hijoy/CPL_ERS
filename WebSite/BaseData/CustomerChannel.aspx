<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Culture="auto" UICulture="auto" CodeFile="CustomerChannel.aspx.cs" Inherits="BaseData_CustomerChannel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="title">
        <asp:Label ID="Label6" runat="server" meta:resourcekey="Label_Title"></asp:Label></div>
    <asp:UpdatePanel ID="CustomerChannelUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <gc:GridView ID="CustomerChannelView" CssClass="GridView" runat="server" DataSourceID="CustomerChannelObjectDataSource"
                AllowPaging="false" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="CustomerChannelID"
                CellPadding="0">
                <Columns>
                    <asp:TemplateField HeaderText="客户渠道编码" SortExpression="CustomerChannelCode">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCustomerChannelCode" runat="server" CssClass="InputText" MaxLength="20"
                                Text='<%# Bind("CustomerChannelCode") %>' Width="400px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="CustomerChannelCodeRequiredFieldValidator" runat="server"
                                ControlToValidate="txtCustomerChannelCode" Display="None" meta:resourcekey="RequiredFieldValidator_Code"
                                SetFocusOnError="True" ValidationGroup="EDIT"></asp:RequiredFieldValidator>
                        </EditItemTemplate>
                        <ItemStyle Width="500px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblCustomerChannelCode" runat="server" Text='<%# Bind("CustomerChannelCode") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="渠道名称" SortExpression="CustomerChannelName">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCustomerChannelName" runat="server" CssClass="InputText" MaxLength="20"
                                Text='<%# Bind("CustomerChannelName") %>' Width="400px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="CustomerChannelNameRequiredFieldValidator" runat="server"
                                ControlToValidate="txtCustomerChannelName" Display="None" meta:resourcekey="RequiredFieldValidator_Name"
                                SetFocusOnError="True" ValidationGroup="EDIT"></asp:RequiredFieldValidator>
                        </EditItemTemplate>
                        <ItemStyle Width="500px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblCustomerChannelName" runat="server" Text='<%# Bind("CustomerChannelName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="IsActive" HeaderText="<%$Resources:Common,Form_IsActive %>">
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
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td scope="col" style="width: 500px">
                                <asp:Label ID="Label2" runat="server" Text="客户渠道编码" ></asp:Label>
                            </td>
                            <td scope="col" style="width: 500px">
                                <asp:Label ID="Label1" runat="server" Text="<%$Resources:Common,Form_CustomerChannel %>"></asp:Label>
                            </td>
                            <td scope="col" style="width: 140px">
                                <asp:Label ID="Label4" runat="server" Text="<%$Resources:Common,Form_IsActive %>"></asp:Label>
                            </td>
                            <td scope="col" style="width: 100px">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
            </gc:GridView>
            <asp:FormView ID="CustomerChannelFormView" runat="server" DataKeyNames="CustomerChannelID"
                CellPadding="0" CellSpacing="0" DataSourceID="CustomerChannelObjectDataSource">
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr>
                            <td align="center" style="width: 500px">
                                <asp:TextBox ID="txtNewCustomerChannelCode" runat="server" Text='<%# Bind("CustomerChannelCode") %>'
                                    CssClass="InputText" Width="400px" ValidationGroup="INS"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 500px">
                                <asp:TextBox ID="txtNewCustomerChannelName" runat="server" Text='<%# Bind("CustomerChannelName") %>'
                                    CssClass="InputText" Width="400px" ValidationGroup="INS"></asp:TextBox>
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
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                        ControlToValidate="txtNewCustomerChannelCode" Display="None" meta:resourcekey="RequiredFieldValidator_Code"
                        SetFocusOnError="True" ValidationGroup="INS"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="CustomerChannelNameRequiredFieldValidator" runat="server"
                        ControlToValidate="txtNewCustomerChannelName" Display="None" meta:resourcekey="RequiredFieldValidator_Name"
                        SetFocusOnError="True" ValidationGroup="INS"></asp:RequiredFieldValidator>
                    <asp:ValidationSummary ID="ValidationSummaryInsert" runat="server" ShowMessageBox="True"
                        ShowSummary="False" ValidationGroup="INS" />
                </InsertItemTemplate>
            </asp:FormView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="CustomerChannelObjectDataSource" runat="server" InsertMethod="InsertCustomerChannel"
        SelectMethod="GetCustomerChannel" TypeName="BusinessObjects.MasterDataBLL" UpdateMethod="UpdateCustomerChannel">
        <UpdateParameters>
            <asp:Parameter Name="CustomerChannelID" Type="Int32" />
            <asp:Parameter Name="CustomerChannelCode" Type="String" />
            <asp:Parameter Name="CustomerChannelName" Type="String" />
            <asp:Parameter Name="IsActive" Type="Boolean" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="CustomerChannelCode" Type="String" />
            <asp:Parameter Name="CustomerChannelName" Type="String" />
            <asp:Parameter Name="IsActive" Type="Boolean" />
        </InsertParameters>
    </asp:ObjectDataSource>
</asp:Content>
