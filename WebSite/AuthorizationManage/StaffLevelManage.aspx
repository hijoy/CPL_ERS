<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="StaffLevelManage.aspx.cs" Inherits="AuthorizationManage_StaffLevelManage"
    Culture="Auto" UICulture="Auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="title">
        <asp:Label ID="Label3" runat="server" meta:resourcekey="StaffLevel_Label_Title"></asp:Label></div>
    <asp:UpdatePanel ID="StaffLevelUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <gc:GridView ID="StaffLevelGridView" CssClass="GridView" runat="server" DataSourceID="StaffLevelObjectDataSource"
                AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="StaffLevelID" CellPadding="0">
                <Columns>
                    <asp:TemplateField SortExpression="StaffLevelName" meta:resourcekey="StaffLevelGridView_TemplateField_StaffLevelName">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtStaffLevelName" runat="server" CssClass="InputText" MaxLength="20"
                                Text='<%# Bind("StaffLevelName") %>' Width="380px" ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="StaffLevelNameRequiredFieldValidator" runat="server"
                                ControlToValidate="txtStaffLevelName" Display="None" meta:resourcekey="RequiredFieldValidator_Name"
                                SetFocusOnError="True" ValidationGroup="EDIT"></asp:RequiredFieldValidator>
                        </EditItemTemplate>
                        <ItemStyle Width="400px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblStaffLevelName" runat="server" Text='<%# Bind("StaffLevelName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="Remark" HeaderText="<%$Resources:Common,Form_Remark %>">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtRemark" runat="server" Text='<%# Bind("Remark") %>' CssClass="InputText"
                                Width="500px" MaxLength="20"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle Width="600px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblRemark" runat="server" Text='<%# Bind("Remark") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="IsPlane" meta:resourcekey="StaffLevelGridView_IsPlane">
                        <EditItemTemplate>
                            <asp:CheckBox runat="server" ID="chkIsPlaneByEdit" Checked='<%# Bind("IsPlane") %>' />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="chkIsPlaneByEdit1" Enabled="False" Checked='<%# Bind("IsPlane") %>' />
                        </ItemTemplate>
                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="IsActive" HeaderText="<%$Resources:Common,Form_IsActive %>">
                        <EditItemTemplate>
                            <asp:CheckBox runat="server" ID="chkActiveByEdit" Checked='<%# Bind("IsActive") %>' />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="chkActiveByEdit1" Enabled="False" Checked='<%# Bind("IsActive") %>' />
                        </ItemTemplate>
                        <ItemStyle Width="66px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField >
                        <EditItemTemplate>
                            <asp:LinkButton ID="LinkButton1" Visible="<%# HasManageRight %>" runat="server" ValidationGroup="skuEdit"
                                CommandName="Update" Text='<%$Resources:Common,Button_Update %>'></asp:LinkButton>
                            <asp:LinkButton ID="LinkButton3" Visible="<%# HasManageRight %>" runat="server" CausesValidation="False"
                                CommandName="Cancel" Text='<%$Resources:Common,Button_Cancel %>'></asp:LinkButton>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton2" Visible="<%# HasManageRight %>" runat="server" CausesValidation="False"
                                CommandName="Edit" Text='<%$Resources:Common,Button_Edit %>'></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td scope="col" style="width: 400px" class="Empty1">
                                <asp:Label ID="Label1" runat="server" meta:resourcekey="StaffLevel_Label_StaffLevelName"></asp:Label>
                            </td>
                            <td scope="col" style="width: 600px">
                                <asp:Label ID="Label2" runat="server" Text="<%$Resources:Common,Form_Remark %>"></asp:Label>
                            </td>
                             <td scope="col" style="width: 80px">
                                <asp:Label ID="Label4" runat="server" meta:resourcekey="StaffLevel_Label_IsPlane"></asp:Label>
                            </td>
                            <td scope="col" style="width: 66px">
                                <asp:Label ID="Label3" runat="server" Text="<%$Resources:Common,Form_IsActive %>"></asp:Label>
                            </td>
                            <td scope="col" style="width: 90px">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
            </gc:GridView>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="StaffLevelFormView" EventName="ItemInserted" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="StaffLevelUpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:FormView ID="StaffLevelFormView" runat="server" DataKeyNames="StaffLevelID"
                CellPadding="0" DataSourceID="StaffLevelObjectDataSource" DefaultMode="Insert"
                Visible="<%# HasManageRight %>" >
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr>
                            <td align="center" style="width: 400px">
                                <asp:TextBox ID="txtStaffLevelName" runat="server" Text='<%# Bind("StaffLevelName") %>'
                                    CssClass="InputText" Width="380px" ValidationGroup="INS"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 600px">
                                <asp:TextBox ID="txtRemark" runat="server" Text='<%# Bind("Remark") %>' CssClass="InputText"
                                    Width="500px" ValidationGroup="INS" MaxLength="100"></asp:TextBox>
                            </td>
                            <td align="center" style="height: 22px; width: 80px;">
                                <asp:CheckBox runat="server" ID="ckbIsPlane" Checked='<%# Bind("IsPlane") %>' />
                            </td>
                            <td align="center" style="height: 22px; width: 66px;">
                                <asp:CheckBox runat="server" ID="chkActiveByAdd" Checked='<%# Bind("IsActive") %>' />
                            </td>
                            <td align="center" style="width: 90px">
                                <asp:LinkButton ID="InsertButton" runat="server" CommandName="Insert" Text='<%$Resources:Common,Button_Add %>'
                                    ValidationGroup="INS"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                    <asp:RequiredFieldValidator ID="StaffLevelNameRequiredFieldValidator" runat="server"
                        ControlToValidate="txtStaffLevelName" Display="None" meta:resourcekey="RequiredFieldValidator_Name"
                        SetFocusOnError="True" ValidationGroup="INS"></asp:RequiredFieldValidator>
                    <asp:ValidationSummary ID="ValidationSummaryInsert" runat="server" ShowMessageBox="True"
                        ShowSummary="False" ValidationGroup="INS" />
                </InsertItemTemplate>
            </asp:FormView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="StaffLevelObjectDataSource" runat="server" InsertMethod="InsertStaffLevel"
        SelectMethod="GetStaffLevel" TypeName="BusinessObjects.AuthorizationBLL" UpdateMethod="UpdateStaffLevel">
        <UpdateParameters>
            <asp:Parameter Name="StaffLevelID" Type="Int32" />
            <asp:Parameter Name="StaffLevelName" Type="String" />
            <asp:Parameter Name="Remark" Type="String" />
            <asp:Parameter Name="IsPlane" Type="Boolean" />
            <asp:Parameter Name="IsActive" Type="Boolean" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="StaffLevelName" Type="String" />
            <asp:Parameter Name="Remark" Type="String" />
            <asp:Parameter Name="IsPlane" Type="Boolean" />
            <asp:Parameter Name="IsActive" Type="Boolean" />
        </InsertParameters>
    </asp:ObjectDataSource>
</asp:Content>
