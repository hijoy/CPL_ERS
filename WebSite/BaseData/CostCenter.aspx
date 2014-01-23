<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="CostCenter.aspx.cs" Culture="auto" UICulture="auto" Inherits="CostCenter"  %>

<%@ Register Src="../UserControls/UCDateInput.ascx" TagName="UCDateInput" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <script src="../Script/DateInput.js" type="text/javascript"></script>
    <div class="title"><asp:Label ID="Label_Title" runat="server" Text="<%$ Resources:Common,Form_CostCenter %>"/></div>
    <asp:UpdatePanel ID="upCostCenter" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <gc:GridView ID="gvCostCenter" CssClass="GridView" runat="server" DataSourceID="odsCostCenter"
                CellPadding="0" AutoGenerateColumns="False" DataKeyNames="CostCenterID" AllowPaging="True"
                AllowSorting="True" PageSize="20" EnableModelValidation="True">
                <Columns>
                    <asp:TemplateField SortExpression="CostCenterName" meta:resourcekey="TemplateField_CostCenterName">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCostCenterName" runat="server" Text='<%# Bind("CostCenterName") %>'
                                Width="170px" CssClass="InputText" MaxLength="50"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblCostCenterName" runat="server" Text='<%# Bind("CostCenterName") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="200px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="CostCenterCode" meta:resourcekey="TemplateField_CostCenterCode">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCostCenterCode" runat="server" Text='<%# Bind("CostCenterCode") %>'
                                Width="170px" CssClass="InputText" MaxLength="50"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblCostCenterCode" runat="server" Text='<%# Bind("CostCenterCode") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="200px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="AccrualCode" meta:resourcekey="TemplateField_AccrualCode">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtAccrualCode" runat="server" Text='<%# Bind("AccrualCode") %>'
                                Width="170px" CssClass="InputText" MaxLength="50"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblAccrualCode" runat="server" Text='<%# Bind("AccrualCode") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="200px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="CompanyID" meta:resourcekey="TemplateField_Company">
                        <EditItemTemplate>
                            <asp:DropDownList runat="server" ID="ddlCompany" DataTextField="CompanyName"
                                    DataValueField="CompanyID" Width="280px" DataSourceID="odsCompany"
                                    SelectedValue='<%# Bind("CompanyID") %>'></asp:DropDownList>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblCompany" runat="server" Text='<%# GetCompanyNameByID(Eval("CompanyID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="300px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="Region" HeaderText="<%$ Resources:Common,Form_CustomerRegion %>">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtRegion" runat="server" Text='<%# Bind("Region") %>'
                                Width="120px" CssClass="InputText" MaxLength="50"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblRegion" runat="server" Text='<%# Bind("Region") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="150px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="IsMAA" meta:resourcekey="TemplateField_IsMAA">
                        <EditItemTemplate>
                            <asp:CheckBox runat="server" ID="chkIsMAAByEdit" Checked='<%# Bind("IsMAA") %>' />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="chkIsMAA" Enabled="false" Checked='<%# Bind("IsMAA") %>' />
                        </ItemTemplate>
                        <ItemStyle Width="70px" HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField SortExpression="IsActive" HeaderText="<%$ Resources:Common,Form_IsActive %>">
                        <EditItemTemplate>
                            <asp:CheckBox runat="server" ID="chkActiveByEdit" Checked='<%# Bind("IsActive") %>' />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="chkActiveByEdit1" Enabled="false" Checked='<%# Bind("IsActive") %>' />
                        </ItemTemplate>
                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <EditItemTemplate>
                            <asp:RequiredFieldValidator ID="RFCostCenterName" runat="server" ControlToValidate="txtCostCenterName"
                                Display="None" meta:resourcekey="RequiredFieldValidator_CostCenterName" SetFocusOnError="True" ValidationGroup="CostCenterEdit"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RFCostCenterCode" runat="server" ControlToValidate="txtCostCenterCode"
                                Display="None" meta:resourcekey="RequiredFieldValidator_CostCenterCode" SetFocusOnError="True" ValidationGroup="CostCenterEdit"></asp:RequiredFieldValidator>
                            <asp:ValidationSummary ID="vsCostCenterEdit" runat="server" ShowMessageBox="True" ShowSummary="False"
                                ValidationGroup="CostCenterEdit" />
                            <asp:LinkButton ID="LinkButton1" Visible="<%# HasManageRight %>" runat="server" CausesValidation="True"
                                ValidationGroup="CostCenterEdit" CommandName="Update" Text="<%$Resources:Common,Button_Update %>"></asp:LinkButton>
                            <asp:LinkButton ID="LinkButton3" Visible="<%# HasManageRight %>" runat="server" CausesValidation="false"
                                CommandName="Cancel" Text="<%$Resources:Common,Button_Cancel %>"></asp:LinkButton>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" Visible="<%# HasManageRight %>" runat="server" CausesValidation="false"
                                CommandName="Edit" Text="<%$Resources:Common,Button_Edit %>"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header" style="height: 22px;">
                            <td style="width:200px;" class="Empty1">
                                <asp:Label ID="Label1" runat="server" meta:resourcekey="Label_CostCenterName"/>
                            </td>
                            <td style=" width:200px;">
                                <asp:Label ID="Label2" runat="server" meta:resourcekey="Label_CostCenterCode"/>
                            </td>
                            <td style=" width:200px;">
                                <asp:Label ID="Label7" runat="server" meta:resourcekey="Label_AccrualCode"/>
                            </td>
                            <td style=" width:300px;">
                                <asp:Label ID="Label3" runat="server" meta:resourcekey="Label_Company"/>
                            </td>
                            <td style=" width:150px;">
                                <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Common,Form_CustomerRegion %>"/>
                            </td>
                            <td style=" width:70px;">
                                <asp:Label ID="Label5" runat="server" meta:resourcekey="Label_IsMAA"/>
                            </td>
                            <td style="width: 60px;">
                                <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Common,Form_IsActive %>"/>
                            </td>
                            <td style="width: 60px;">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <SelectedRowStyle CssClass="SelectedRow" />
            </gc:GridView>
            <asp:FormView ID="fvCostCenter" runat="server" DataKeyNames="CostCenterID" DataSourceID="odsCostCenter"
                DefaultMode="Insert" Visible="<%# HasManageRight %>" EnableModelValidation="True" CellPadding="0">
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr>
                            <td align="center" style="height: 22px; width: 200px;">
                                <asp:TextBox ID="txtCostCenterNameByAdd" runat="server" Text='<%# Bind("CostCenterName") %>'
                                    Width="170px" CssClass="InputText" ValidationGroup="CostCenterINS" MaxLength="50"></asp:TextBox>
                            </td>
                            <td align="center" style="height: 22px; width: 200px;">
                                <asp:TextBox ID="txtCostCenterCodeByAdd" runat="server" Text='<%# Bind("CostCenterCode") %>'
                                    Width="170px" CssClass="InputText" ValidationGroup="CostCenterINS" MaxLength="50"></asp:TextBox>
                            </td>
                            <td align="center" style="height: 22px; width: 200px;">
                                <asp:TextBox ID="txtAccrualCodeAdd" runat="server" Text='<%# Bind("AccrualCode") %>'
                                    Width="170px" CssClass="InputText" MaxLength="50"></asp:TextBox>
                            </td>
                            <td align="center" style="height: 22px; width: 300px;">
                                <asp:DropDownList runat="server" ID="ddlCompanyAdd" DataTextField="CompanyName"
                                        DataValueField="CompanyID" Width="280px" DataSourceID="odsCompany"
                                        SelectedValue='<%# Bind("CompanyID") %>'></asp:DropDownList>
                            </td>
                            <td align="center" style="height: 22px; width: 150px;">
                                <asp:TextBox ID="txtRegionByAdd" runat="server" Text='<%# Bind("Region") %>'
                                    Width="120px" CssClass="InputText"  MaxLength="50"></asp:TextBox>
                            </td>
                            <td align="center" style="height: 22px; width: 70px;">
                                <asp:CheckBox runat="server" ID="chkIsMAAByAdd" Checked='<%# Bind("IsMAA") %>' />
                            </td>
                            <td align="center" style="height: 22px; width: 60px;">
                                <asp:CheckBox runat="server" ID="chkActiveByAdd" Checked='<%# Bind("IsActive") %>' />
                            </td>
                            <td align="center" style="width: 60px;">
                                <asp:RequiredFieldValidator ID="RFCostCenterName" runat="server" ControlToValidate="txtCostCenterNameByAdd"
                                    Display="None" meta:resourcekey="RequiredFieldValidator_CostCenterName" SetFocusOnError="True" ValidationGroup="CostCenterAdd"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RFCostCenterCode" runat="server" ControlToValidate="txtCostCenterCodeByAdd"
                                Display="None" meta:resourcekey="RequiredFieldValidator_CostCenterCode" SetFocusOnError="True" ValidationGroup="CostCenterAdd"></asp:RequiredFieldValidator>
                                <asp:ValidationSummary ID="vsCostCenterAdd" runat="server" ShowMessageBox="True" ShowSummary="False"
                                    ValidationGroup="CostCenterAdd" />
                                <asp:LinkButton ID="InsertLinkButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    Text="<%$Resources:Common,Button_Add %>" ValidationGroup="CostCenterAdd"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                    <asp:ValidationSummary ID="CostCenterInsertValidationSummary" ValidationGroup="CostCenterINS"
                        ShowMessageBox="true" ShowSummary="false" EnableClientScript="true" runat="server" />
                </InsertItemTemplate>
            </asp:FormView>
            <asp:ObjectDataSource ID="odsCostCenter" runat="server" TypeName="BusinessObjects.MasterDataBLL"
                DeleteMethod="DeleteCostCenter" InsertMethod="InsertCostCenter" SelectMethod="GetCostCenterPaged" SelectCountMethod="CostCenterTotalCount"
                SortParameterName="sortExpression" UpdateMethod="UpdateCostCenter" OnDeleting="odsCostCenter_Deleting"
                OnInserting="odsCostCenter_Inserting" OnUpdating="odsCostCenter_Updating" EnablePaging="true">
                <DeleteParameters>
                    <asp:Parameter Name="UserID" Type="Int32" />
                    <asp:Parameter Name="PositionID" Type="Int32" />
                </DeleteParameters>
                <UpdateParameters>
                    <asp:Parameter Name="CostCenterID" Type="Int32" />
                    <asp:Parameter Name="UserID" Type="Int32" />
                    <asp:Parameter Name="PositionID" Type="Int32" />
                    <asp:Parameter Name="CostCenterName" Type="String" />
                    <asp:Parameter Name="CostCenterCode" Type="String" />
                    <asp:Parameter Name="AccrualCode" Type="String" />
                    <asp:Parameter Name="CompanyID" Type="Int32" />
                    <asp:Parameter Name="Region" Type="String" />
                    <asp:Parameter Name="IsMAA" Type="Boolean" />
                    <asp:Parameter Name="IsActive" Type="Boolean" />
                </UpdateParameters>
                <InsertParameters>
                    <asp:Parameter Name="CostCenterName" Type="String" />
                    <asp:Parameter Name="CostCenterCode" Type="String" />
                    <asp:Parameter Name="AccrualCode" Type="String" />
                    <asp:Parameter Name="CompanyID" Type="Int32" />
                    <asp:Parameter Name="Region" Type="String" />
                    <asp:Parameter Name="IsMAA" Type="Boolean" />
                    <asp:Parameter Name="IsActive" Type="Boolean" />
                </InsertParameters>
                <SelectParameters>
                    <asp:Parameter Name="queryExpression" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:SqlDataSource ID="odsCompany" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                SelectCommand="select CompanyID,CompanyCode+'-'+CompanyName as CompanyName from Company">
            </asp:SqlDataSource>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
