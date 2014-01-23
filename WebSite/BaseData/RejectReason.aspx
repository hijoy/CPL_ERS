<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="RejectReason.aspx.cs" Inherits="RejectReason" Culture="Auto" UICulture="Auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="title">
        <asp:Label ID="Label_Title" runat="server" meta:resourcekey="Label_Title"/>
    </div>
    <asp:UpdatePanel ID="RejectReasonUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <gc:GridView ID="RejectReasonGridView" CssClass="GridView" runat="server" DataSourceID="RejectReasonObjectDataSource"
                AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="RejectReasonId"
                OnDataBound="RejectReasonGridView_DataBound" PageSize="20" CellPadding="0">
                <Columns>
                    <asp:TemplateField meta:resourcekey="TemplateField_ReasonIndex" SortExpression="RejectReasonIndex">
                        <EditItemTemplate>
                            <asp:TextBox ID="RejectReasonIndexTextBox" runat="server" CssClass="InputText" Text='<%# Bind("RejectReasonIndex") %>'
                                Width="100px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RejectReasonIndexRequiredFieldValidator" runat="server"
                                ControlToValidate="RejectReasonIndexTextBox" Display="None" meta:resourcekey="RequiredFieldValidator_ReasonIndex"
                                SetFocusOnError="True" ValidationGroup="EDIT"></asp:RequiredFieldValidator>
                            <asp:ValidationSummary ID="ValidationSummaryEDIT" runat="server" ShowMessageBox="True"
                                ShowSummary="False" ValidationGroup="EDIT" />
                        </EditItemTemplate>
                        <ItemStyle Width="120px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="RejectReasonIndexLabel" runat="server" Text='<%# Bind("RejectReasonIndex") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateField_ReasonTitle" SortExpression="RejectReasonTitle">
                        <EditItemTemplate>
                            <asp:TextBox ID="RejectReasonTitleTextBox" runat="server" CssClass="InputText" MaxLength="100"
                                Text='<%# Bind("RejectReasonTitle") %>' Width="330px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RejectReasonTitleRequiredFieldValidator" runat="server"
                                ControlToValidate="RejectReasonTitleTextBox" Display="None" meta:resourcekey="RequiredFieldValidator_ReasonTitle"
                                SetFocusOnError="True" ValidationGroup="EDIT"></asp:RequiredFieldValidator>
                        </EditItemTemplate>
                        <ItemStyle Width="350px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="RejectReasonTitleLabel" runat="server" Text='<%# Bind("RejectReasonTitle") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateField_ReasonContent" SortExpression="RejectReasonContent">
                        <EditItemTemplate>
                            <asp:TextBox ID="RejectReasonContentTextBox" runat="server" Text='<%# Bind("RejectReasonContent") %>'
                                CssClass="InputText" Width="640px" MaxLength="500"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RejectReasonContentRequiredFieldValidator" runat="server"
                                ControlToValidate="RejectReasonContentTextBox" Display="None" meta:resourcekey="RequiredFieldValidator_ReasonContent"
                                SetFocusOnError="True" ValidationGroup="EDIT"></asp:RequiredFieldValidator>
                        </EditItemTemplate>
                        <ItemStyle Width="665px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="RejectReasonContentLabel" runat="server" Text='<%# Bind("RejectReasonContent") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="IsActive" HeaderText="<%$ Resources:Common,Form_IsActive %>">
                        <EditItemTemplate>
                            <asp:CheckBox runat="server" ID="chkActiveByEdit" Checked='<%# Bind("IsActive") %>' />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="chkActiveByEdit1" Enabled="false" Checked='<%# Bind("IsActive") %>' />
                        </ItemTemplate>
                        <ItemStyle Width="40px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <EditItemTemplate>
                            <asp:LinkButton ID="LinkButton1" Visible="<%# HasManageRight %>" runat="server" CausesValidation="True"
                                ValidationGroup="skuEdit" CommandName="Update" Text="<%$Resources:Common,Button_Update %>"></asp:LinkButton>
                            <asp:LinkButton ID="LinkButton3" Visible="<%# HasManageRight %>" runat="server" CausesValidation="false"
                                CommandName="Cancel" Text="<%$Resources:Common,Button_Cancel %>"></asp:LinkButton>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton2" Visible="<%# HasManageRight %>" runat="server" CausesValidation="false"
                                CommandName="Edit" Text="<%$Resources:Common,Button_Edit %>"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td scope="col" style="width: 120px" class="Empty1">
                                <asp:Label ID="Label1" runat="server" meta:resourcekey="Label_ReasonIndex"/>
                            </td>
                            <td scope="col" style="width: 350px">
                                <asp:Label ID="Label2" runat="server" meta:resourcekey="Label_ReasonTitle"/>
                            </td>
                            <td scope="col" style="width: 665px">
                                <asp:Label ID="Label3" runat="server" meta:resourcekey="Label_ReasonContent"/>
                            </td>
                            <td scope="col" style="width: 40px">
                                <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Common,Form_IsActive %>"/>
                            </td>
                            <td scope="col" style="width: 50px">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
            </gc:GridView>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="RejectReasonFormView" EventName="ItemInserted" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="RejectReasonAddUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:FormView ID="RejectReasonFormView" runat="server" DataKeyNames="RejectReasonId" CellPadding="0" CellSpacing="0"
                DataSourceID="RejectReasonObjectDataSource" DefaultMode="Insert" Visible="<%# HasManageRight %>">
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr>
                            <td align="center" style="width: 120px">
                                <asp:TextBox ID="RejectReasonIndexTextBox" runat="server" Text='<%# Bind("RejectReasonIndex") %>'
                                    CssClass="InputText" Width="100px" ValidationGroup="INS"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 350px">
                                <asp:TextBox ID="RejectReasonTitleTextBox" runat="server" Text='<%# Bind("RejectReasonTitle") %>'
                                    CssClass="InputText" Width="330px" ValidationGroup="INS" MaxLength="100"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 665px">
                                <asp:TextBox ID="RejectReasonContentTextBox" runat="server" Text='<%# Bind("RejectReasonContent") %>'
                                    CssClass="InputText" Width="640px" ValidationGroup="INS" MaxLength="500"></asp:TextBox>
                            </td>
                            <td align="center" style="height: 22px; width: 40px;">
                                <asp:CheckBox runat="server" ID="chkActiveByAdd" Checked='<%# Bind("IsActive") %>' />
                            </td>
                            <td align="center" style="width: 60px">
                                <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    Text="<%$ Resources:Common,Button_Add %>" ValidationGroup="INS"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                    <asp:RequiredFieldValidator ID="RejectReasonIndexRequiredFieldValidator" runat="server"
                        ControlToValidate="RejectReasonIndexTextBox" Display="None" meta:resourcekey="RequiredFieldValidator_ReasonIndex"
                        SetFocusOnError="True" ValidationGroup="INS"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RejectReasonTitleRequiredFieldValidator" runat="server"
                        ControlToValidate="RejectReasonTitleTextBox" Display="None" meta:resourcekey="RequiredFieldValidator_ReasonTitle"
                        SetFocusOnError="True" ValidationGroup="INS"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RejectReasonContentRequiredFieldValidator" runat="server"
                        ControlToValidate="RejectReasonContentTextBox" Display="None" meta:resourcekey="RequiredFieldValidator_ReasonContent"
                        SetFocusOnError="True" ValidationGroup="INS"></asp:RequiredFieldValidator>
                    <asp:ValidationSummary ID="ValidationSummaryInsert" runat="server" ShowMessageBox="True"
                        ShowSummary="False" ValidationGroup="INS" />
                </InsertItemTemplate>
            </asp:FormView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="RejectReasonObjectDataSource" runat="server" DeleteMethod="DeleteById"
        InsertMethod="InsertRejectReason" OldValuesParameterFormatString="{0}" SelectMethod="GetRejectReasonPaged"
        TypeName="BusinessObjects.MasterDataBLL" EnablePaging="true" UpdateMethod="UpdateRejectReason"
        SelectCountMethod="QueryTotalCount" SortParameterName="sortExpression">
        <DeleteParameters>
            <asp:Parameter Name="RejectReasonId" Type="Int32" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="RejectReasonId" Type="Int32" />
            <asp:Parameter Name="RejectReasonIndex" Type="Int32" />
            <asp:Parameter Name="RejectReasonTitle" Type="String" />
            <asp:Parameter Name="RejectReasonContent" Type="String" />
            <asp:Parameter Name="IsActive" Type="Boolean" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="RejectReasonIndex" Type="Int32" />
            <asp:Parameter Name="RejectReasonTitle" Type="String" />
            <asp:Parameter Name="RejectReasonContent" Type="String" />
            <asp:Parameter Name="IsActive" Type="Boolean" />
        </InsertParameters>
    </asp:ObjectDataSource>
</asp:Content>
