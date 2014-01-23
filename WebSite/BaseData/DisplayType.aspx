<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="DisplayType.aspx.cs" Inherits="BaseData_DisplayType" Culture="Auto"
    UICulture="Auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="title">
        <asp:Label runat="server" meta:resourcekey="Label_Title"></asp:Label>
    </div>
    <asp:UpdatePanel ID="upDisplayType" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <gc:GridView ID="gvDisplayType" CssClass="GridView" runat="server" DataSourceID="odsDisplayType"
                CellPadding="0" AutoGenerateColumns="False" DataKeyNames="DisplayTypeID" AllowPaging="True"
                AllowSorting="True" PageSize="20" EnableModelValidation="True">
                <Columns>
                    <asp:TemplateField SortExpression="DisplayTypeName" meta:resourcekey="TemplateField_DisplayTypeName">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtDisplayTypeName" runat="server" Text='<%# Bind("DisplayTypeName") %>'
                                Width="250px" CssClass="InputText" MaxLength="50"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblDisplayTypeName" Text='<%# Eval("DisplayTypeName")%>' />
                        </ItemTemplate>
                        <ItemStyle Width="1077px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="IsActive" HeaderText="<%$Resources:Common,Form_IsActive %>">
                        <EditItemTemplate>
                            <asp:CheckBox runat="server" ID="chkActiveByEdit" Checked='<%# Bind("IsActive") %>' />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="chkActiveByEdit1" Enabled="false" Checked='<%# Bind("IsActive") %>' />
                        </ItemTemplate>
                        <ItemStyle Width="70px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <EditItemTemplate>
                            <asp:RequiredFieldValidator ID="RFItemName" runat="server" ControlToValidate="txtDisplayTypeName"
                                Display="None" meta:resourcekey="RequiredFieldValidator_DisplayTypeName" SetFocusOnError="True" ValidationGroup="ItemEdit"></asp:RequiredFieldValidator>
                            <asp:ValidationSummary ID="vsItemEdit" runat="server" ShowMessageBox="True" ShowSummary="False"
                                ValidationGroup="ItemEdit" />
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True"
                                ValidationGroup="ItemEdit" CommandName="Update" Text='<%$Resources:Common,Button_Update %>'></asp:LinkButton>
                            <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="false"
                                CommandName="Cancel" Text='<%$Resources:Common,Button_Cancel %>'></asp:LinkButton>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false"
                                CommandName="Edit" Text='<%$Resources:Common,Button_Edit %>'></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header" style="height: 22px;">
                            <td align="center" style="width: 1077px;" class="Empty1">
                                <asp:Label ID="Label5" runat="server" meta:resourcekey="Label_DisplayTypeName"></asp:Label>
                            </td>
                            <td align="center" style="width: 70px;">
                                <asp:Label ID="Label4" runat="server" Text="<%$Resources:Common,Form_IsActive %>"></asp:Label>
                            </td>
                            <td style="width: 90px;">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <SelectedRowStyle CssClass="SelectedRow" />
            </gc:GridView>
            <asp:FormView ID="fvDisplayType" runat="server" DataKeyNames="DisplayTypeID" DataSourceID="odsDisplayType"
                DefaultMode="Insert" EnableModelValidation="True" CellPadding="0">
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr>
                            <td align="center" style="height: 22px; width: 1077px;">
                                <asp:TextBox ID="txtDisplayTypeNameByAdd" runat="server" Text='<%# Bind("DisplayTypeName") %>'
                                    Width="250px" CssClass="InputText" ValidationGroup="ItemINS" MaxLength="50"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 70px;">
                                <asp:CheckBox runat="server" ID="chkActiveByAdd" Checked='<%# Bind("IsActive") %>' />
                            </td>
                            <td align="center" style="width: 90px;">
                                <asp:RequiredFieldValidator ID="RFItemName" runat="server" ControlToValidate="txtDisplayTypeNameByAdd"
                                    Display="None" meta:resourcekey="RequiredFieldValidator_DisplayTypeName" SetFocusOnError="True" ValidationGroup="ItemAdd"></asp:RequiredFieldValidator>
                                <asp:ValidationSummary ID="vsItemAdd" runat="server" ShowMessageBox="True" ShowSummary="False"
                                    ValidationGroup="ItemAdd" />
                                <asp:LinkButton ID="InsertLinkButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    Text='<%$Resources:Common,Button_Add %>' ValidationGroup="ItemAdd"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                    <asp:ValidationSummary ID="ItemINSertValidationSummary" ValidationGroup="ItemINS"
                        ShowMessageBox="true" ShowSummary="false" EnableClientScript="true" runat="server" />
                </InsertItemTemplate>
            </asp:FormView>
            <asp:ObjectDataSource ID="odsDisplayType" runat="server" EnablePaging="true" TypeName="BusinessObjects.MasterDataBLL"
                InsertMethod="InsertDisplayType" SelectMethod="GetPagedDisplayType" SortParameterName="sortExpression"
                SelectCountMethod="DisplayTypeTotalCount" UpdateMethod="UpdateDisplayType" OnInserted="odsDisplayType_Inserted"
                OnUpdated="odsDisplayType_Updated">
                <SelectParameters>
                    <asp:Parameter Name="queryExpression" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
