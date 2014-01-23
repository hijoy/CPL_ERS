<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="City.aspx.cs" Inherits="BaseData_City" Culture="Auto" UICulture="Auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <script src="../Script/DateInput.js" type="text/javascript"></script>
    <div class="title">
        <asp:Label ID="Label5" runat="server" meta:resourcekey="CityTypeTitle"></asp:Label>
    </div>
    <asp:UpdatePanel ID="upCityType" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <gc:GridView ID="gvCityType" CssClass="GridView" runat="server" DataSourceID="odsCityType"
                CellPadding="0" AutoGenerateColumns="False" DataKeyNames="CityTypeID" AllowPaging="True"
                AllowSorting="True" PageSize="20" OnSelectedIndexChanged="gvCityType_SelectedIndexChanged"
                OnRowDeleted="gvCityType_RowDeleted" EnableModelValidation="True">
                <Columns>
                    <asp:TemplateField SortExpression="CityTypeName" meta:resourcekey="CityTypeGridView_TemplateField_CityTypeName">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCityTypeName" runat="server" Text='<%# Bind("CityTypeName") %>'
                                Width="1050px" CssClass="InputText" MaxLength="50"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnCityTypeName" runat="server" CausesValidation="False" CommandName="Select"
                                Text='<%# Bind("CityTypeName") %>'></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="1077px" HorizontalAlign="Center" />
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
                    <asp:TemplateField ShowHeader="False">
                        <EditItemTemplate>
                            <asp:RequiredFieldValidator ID="RFProvName" runat="server" ControlToValidate="txtCityTypeName"
                                Display="None" meta:resourcekey="RequiredFieldValidator_CityType" SetFocusOnError="True" ValidationGroup="provEdit"></asp:RequiredFieldValidator>
                            <asp:ValidationSummary ID="vsProvEdit" runat="server" ShowMessageBox="True" ShowSummary="False"
                                ValidationGroup="provEdit" />
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True"
                                ValidationGroup="provEdit" CommandName="Update" Text='<%$Resources:Common,Button_Update %>'></asp:LinkButton>
                            <asp:LinkButton ID="LinkButton3"  runat="server" CausesValidation="false"
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
                                 <asp:Label ID="Label1" runat="server" meta:resourcekey="CityType_Label_CityTypeName"></asp:Label>
                            </td>
                            <td align="center" style="width: 70px;">
                                <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Common,Form_IsActive %>"></asp:Label>
                            </td>
                            <td style="width: 90px;">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <SelectedRowStyle CssClass="SelectedRow" />
            </gc:GridView>
            <asp:FormView ID="fvCityType" runat="server" DataKeyNames="CityTypeID" DataSourceID="odsCityType"
                DefaultMode="Insert"  EnableModelValidation="True" CellPadding="0">
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr>
                            <td align="center" style="height: 22px; width: 1077px;">
                                <asp:TextBox ID="txtCityTypeNameByAdd" runat="server" Text='<%# Bind("CityTypeName") %>'
                                    Width="1050px" CssClass="InputText" ValidationGroup="ProvINS" MaxLength="50"></asp:TextBox>
                            </td>
                            <td align="center" style="height: 22px; width: 70px;">
                                <asp:CheckBox runat="server" ID="chkActiveByAdd" Checked='<%# Bind("IsActive") %>' />
                            </td>
                            <td align="center" style="width: 90px;">
                                <asp:RequiredFieldValidator ID="RFProvName" runat="server" ControlToValidate="txtCityTypeNameByAdd"
                                    Display="None" meta:resourcekey="RequiredFieldValidator_CityType" SetFocusOnError="True" ValidationGroup="provAdd"></asp:RequiredFieldValidator>
                                <asp:ValidationSummary ID="vsProvAdd" runat="server" ShowMessageBox="True" ShowSummary="False"
                                    ValidationGroup="provAdd" />
                                <asp:LinkButton ID="InsertLinkButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    Text='<%$Resources:Common,Button_Add %>' ValidationGroup="provAdd"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                    <asp:ValidationSummary ID="ProvInsertValidationSummary" ValidationGroup="ProvINS"
                        ShowMessageBox="true" ShowSummary="false" EnableClientScript="true" runat="server" />
                </InsertItemTemplate>
            </asp:FormView>
            <asp:ObjectDataSource ID="odsCityType" runat="server" TypeName="BusinessObjects.MasterDataBLL"
                InsertMethod="InsertCityType" SelectMethod="GetCityTypePaged" UpdateMethod="UpdateCityType"
                OnInserted="odsCityType_Inserted" OnUpdated="odsCityType_Updated">
                <DeleteParameters>
                    <asp:Parameter Name="stuffUser" Type="object" />
                    <asp:Parameter Name="position" Type="object" />
                </DeleteParameters>
                <UpdateParameters>
                    <asp:Parameter Name="CityTypeName" Type="String" Size="50" />
                </UpdateParameters>
                <InsertParameters>
                    <asp:Parameter Name="CityTypeName" Type="String" Size="50" />
                </InsertParameters>
            </asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <div class="title">
        <asp:Label ID="Label4" runat="server" meta:resourcekey="CityTitle"></asp:Label>
    </div>
    <asp:UpdatePanel ID="upCity" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <gc:GridView Visible="false" ID="gvCity" CssClass="GridView" runat="server" DataSourceID="odsCity"
                AutoGenerateColumns="False" CellPadding="0" AllowSorting="True" AllowPaging="True"
                PageSize="20" DataKeyNames="CityId">
                <HeaderStyle CssClass="Header" />
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td align="center" style="width: 1006px;" class="Empty1">
                                <asp:Label ID="Label1" runat="server" meta:resourcekey="CityType_Label_CityName"></asp:Label>
                            </td>
                            <td align="center" style="width: 80px;">
                                <asp:Label ID="Label3" runat="server" meta:resourcekey="CityType_Label_IsAutoComplete"></asp:Label>
                            </td>
                            <td align="center" style="width: 90px;">
                                <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Common,Form_IsActive %>"></asp:Label>
                            </td>
                            <td style="width: 60px;">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <Columns>
                    <asp:TemplateField meta:resourcekey="CityGridView_TemplateField_CityName" SortExpression="CityName">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCityNameByEdit" runat="server" Text='<%# Bind("CityName") %>'
                                CssClass="InputText" Width="960px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblCityNameByEdit" runat="server" Text='<%# Bind("CityName") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="986px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="IsAutoComplete" meta:resourcekey="CityGridView_TemplateField_IsAutoComplete">
                        <EditItemTemplate>
                            <asp:CheckBox runat="server" ID="chkAutoCompleteByEdit" Checked='<%# Bind("IsAutoComplete") %>' />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="chkAutoCompleteByEdit1" Enabled="false" Checked='<%# Bind("IsAutoComplete") %>' />
                        </ItemTemplate>
                        <ItemStyle Width="90px" HorizontalAlign="Center" />
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
                    <asp:TemplateField ShowHeader="False">
                        <EditItemTemplate>
                            <asp:RequiredFieldValidator ID="RFCityName" runat="server" ControlToValidate="txtCityNameByEdit"
                                Display="None" meta:resourcekey="RequiredFieldValidator_CityName" SetFocusOnError="True" ValidationGroup="cityEdit"></asp:RequiredFieldValidator>
                            <asp:ValidationSummary ID="vsCityEdit" runat="server" ShowMessageBox="True" ShowSummary="False"
                                ValidationGroup="cityEdit" />
                            <asp:LinkButton ID="LinkButton1" runat="server" ValidationGroup="cityEdit"
                                CausesValidation="True" CommandName="Update" Text='<%$Resources:Common,Button_Update %>'></asp:LinkButton>
                            <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False"
                                CommandName="Cancel" Text='<%$Resources:Common,Button_Cancel %>'></asp:LinkButton>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False"
                                CommandName="Edit" Text='<%$Resources:Common,Button_Edit %>'></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
            </gc:GridView>
            <asp:FormView ID="fvCity" Visible="false" runat="server" DataKeyNames="CityId" DataSourceID="odsCity"
                DefaultMode="Insert" OnItemInserting="fvCity_ItemInserting" CellPadding="0" CellSpacing="0">
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr>
                            <td align="center" style="width: 986px;">
                                <asp:TextBox ID="txtCityNameByAdd" runat="server" Text='<%# Bind("CityName") %>'
                                    CssClass="InputText" ValidationGroup="CityInsert" Width="960px"></asp:TextBox>
                            </td>
                            <td align="center" style="height: 22px; width: 90px;">
                                <asp:CheckBox runat="server" ID="chkAutoCompleteByAdd" Checked='<%# Bind("IsAutoComplete") %>' />
                            </td>
                            <td align="center" style="height: 22px; width: 70px;">
                                <asp:CheckBox runat="server" ID="chkActiveByAdd" Checked='<%# Bind("IsActive") %>' />
                            </td>
                            <td align="center" style="width: 90px;">
                                <asp:RequiredFieldValidator ID="RFCityName" runat="server" ControlToValidate="txtCityNameByAdd"
                                    Display="None" meta:resourcekey="RequiredFieldValidator_CityName" SetFocusOnError="True" ValidationGroup="cityAdd"></asp:RequiredFieldValidator>
                                <asp:ValidationSummary ID="vsCityAdd" runat="server" ShowMessageBox="True" ShowSummary="False"
                                    ValidationGroup="cityAdd" />
                                <asp:LinkButton ID="InsertLinkButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    Text='<%$Resources:Common,Button_Add %>' ValidationGroup="cityAdd"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                    <asp:ValidationSummary ID="CityInsertValidationSummary" ValidationGroup="CityInsert"
                        runat="server" ShowMessageBox="true" ShowSummary="false" />
                </InsertItemTemplate>
            </asp:FormView>
            <asp:ObjectDataSource ID="odsCity" runat="server" TypeName="BusinessObjects.MasterDataBLL"
                InsertMethod="InsertCity" SelectMethod="GetCityPaged" OnInserted="odsCity_Inserted"
                OnUpdated="odsCity_Updated" EnablePaging="true" SelectCountMethod="CityTotalCount"
                UpdateMethod="UpdateCity" SortParameterName="sortExpression" OnSelecting="odsCity_Selecting"
                OnInserting="odsCity_Inserting">
                <DeleteParameters>
                    <asp:Parameter Name="stuffUser" Type="object" />
                    <asp:Parameter Name="position" Type="object" />
                </DeleteParameters>
                <UpdateParameters>
                    <asp:Parameter Name="CityName" Type="String" Size="50" />
                </UpdateParameters>
                <InsertParameters>
                    <asp:Parameter Name="CityName" Type="String" Size="50" />
                    <asp:Parameter Name="CityTypeID" Type="Int32" />
                </InsertParameters>
                <SelectParameters>
                    <asp:Parameter Name="CityTypeID" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
