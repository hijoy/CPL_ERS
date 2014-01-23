<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="OrganizationManage.aspx.cs" Inherits="AuthorizationManage_OrganizationManage"
    Title="组织结构" Culture="Auto" UICulture="Auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="padding-left: 100px;">
        <asp:Button ID="ShowAllOU" runat="server" OnClick="ShowAllOU_Click" CssClass="button_big"
            meta:resourcekey="OU_Button_ViewAll" /></div>
    <br />
    <div style="width: 1200px;">
        <table width="100%" border="0">
            <tr>
                <td valign="top">
                    <div style="width: 600px; border-top-width: thin; border-left-width: thin; border-bottom-width: thin;
                        border-right-width: thin; padding-left: 100px;">
                        <asp:TreeView ID="OrganizationTreeView" runat="server" OnSelectedNodeChanged="OrganizationTreeView_SelectedNodeChanged"
                            ShowCheckBoxes="All" ShowLines="True" ExpandDepth="1">
                            <HoverNodeStyle BackColor="Silver" />
                            <SelectedNodeStyle BackColor="Navy" ForeColor="White" />
                        </asp:TreeView>
                    </div>
                </td>
                <td>
                    <div style="width: 500px;">
                        <asp:Panel ID="AddRootOUPanelArea" CssClass="BorderedArea" Visible="false" runat="server">
                            <div class="title3" style="width: 482px;">
                                <asp:Label runat="server" meta:resourcekey="Title_RootOUAdd"></asp:Label>
                            </div>
                            <table style="background-color: #f6f6f6; width: 492px;" cellpadding="0">
                                <tr>
                                    <td style="width: 100px;">
                                        <asp:Label runat="server" meta:resourcekey="OU_Label_OUName"></asp:Label>
                                    </td>
                                    <td style="width: 240px;">
                                        <asp:TextBox ID="RootUnitNameCtl" runat="server" CssClass="InputText" Width="240px"
                                            MaxLength="50"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" meta:resourcekey="OU_Label_OUCode"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="RootUnitCodeCtl" runat="server" CssClass="InputText" Width="240px"
                                            MaxLength="50"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" meta:resourcekey="OU_Label_OUType"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="RootUnitTypeCtl" runat="server" DataSourceID="OrganizationUnitTypeSqlDataSource"
                                            DataTextField="OrganizationUnitTypeName" DataValueField="OrganizationUnitTypeId"
                                            OnDataBound="RootUnitTypeCtl_DataBound" CssClass="InputCombo" Width="240px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" meta:resourcekey="OU_Label_CostCenter"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="RootCostCenterDDL" runat="server" DataSourceID="odsCostCenter"
                                            DataTextField="CostCenterCode" DataValueField="CostCenterID" CssClass="InputCombo"
                                            Width="240px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100px;">
                                        <asp:Label ID="Label2" runat="server" meta:resourcekey="Label_FlowParameter"></asp:Label>
                                    </td>
                                    <td style="width: 240px;">
                                        <asp:TextBox ID="RootFlowParameterCtl" runat="server" CssClass="InputText" Width="240px"
                                            MaxLength="50"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Button ID="AddRootOrganizationUnitBtn" runat="server" Text="<%$Resources:Common,Button_Add %>" OnClick="AddRootOrganizationUnitBtn_Click"
                                            CssClass="button_nor" ValidationGroup="AddRootOU" />
                                    </td>
                                </tr>
                            </table>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="RootUnitNameCtl"
                                Display="None" meta:resourcekey="RequiredFieldValidator_UnitName" ValidationGroup="AddRootOU"></asp:RequiredFieldValidator>
                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
                                ShowSummary="False" ValidationGroup="AddRootOU" />
                        </asp:Panel>
                        <br />
                        <asp:Panel ID="EditOUPanelArea" runat="server" CssClass="BorderedArea">
                            <div class="title3" style="width: 482px;">
                                <asp:Label runat="server" meta:resourcekey="Title_OUEdit"></asp:Label>
                            </div>
                            <table style="background-color: #f6f6f6; width: 492px;">
                                <tr>
                                    <td style="width: 100px;">
                                        <asp:Label runat="server" meta:resourcekey="OU_Label_OUName"></asp:Label>
                                    </td>
                                    <td style="width: 240px;">
                                        <asp:TextBox ID="UnitNameCtl" runat="server" CssClass="InputText" Width="240px" MaxLength="50"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" meta:resourcekey="OU_Label_OUCode"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="UnitCodeCtl" runat="server" CssClass="InputText" Width="240px" MaxLength="50"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" meta:resourcekey="OU_Label_OUType"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="UnitTypeCtl" runat="server" DataSourceID="OrganizationUnitTypeSqlDataSource"
                                            DataTextField="OrganizationUnitTypeName" DataValueField="OrganizationUnitTypeId"
                                            OnDataBound="UnitTypeCtl_DataBound" CssClass="InputCombo" Width="240px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" meta:resourcekey="OU_Label_CostCenter"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="CostCenterDDL" runat="server" DataSourceID="odsCostCenter"
                                            DataTextField="CostCenterCode" DataValueField="CostCenterID" CssClass="InputCombo"
                                            Width="240px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100px;">
                                        <asp:Label ID="Label4" runat="server" meta:resourcekey="Label_FlowParameter"></asp:Label>
                                    </td>
                                    <td style="width: 240px;">
                                        <asp:TextBox ID="FlowParameterCtl" runat="server" CssClass="InputText" Width="240px"
                                            MaxLength="50"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <asp:CheckBox ID="UnitIsActiveCtl" meta:resourcekey="CheckBox_IsActive" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Button ID="ChangeParentUnitBtn" runat="server" meta:resourcekey="Button_ChangeParentUnit" OnClick="ChangeParentUnitBtn_Click"
                                            CausesValidation="False" CssClass="button_big" />
                                        <asp:Button ID="UpdataOrganizationUnitBtn" runat="server" Text="<%$Resources:Common,Button_Update %>" OnClick="UpdataOrganizationUnitBtn_Click"
                                            CssClass="button_nor" ValidationGroup="EditOU" />
                                        <asp:Button ID="DeleteOrganizationUnitBtn" runat="server" Text="<%$Resources:Common,Button_Delete %>" OnClick="DeleteOrganizationUnitBtn_Click"
                                            CausesValidation="False" CssClass="button_nor" />
                                    </td>
                                </tr>
                            </table>
                            <asp:SqlDataSource ID="OrganizationUnitTypeSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                SelectCommand="SELECT [OrganizationUnitTypeId], [OrganizationUnitTypeName] FROM [OrganizationUnitType]">
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="odsCostCenter" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                SelectCommand="select 0 [CostCenterID],' Empty' CostCenterCode union SELECT [CostCenterID], [CostCenterCode]+'-'+[CompanyName] as CostCenterCode FROM [CostCenter] join Company on CostCenter.CompanyID = Company.CompanyID  where IsActive = 1 and IsMAA=0 order by CostCenterCode">
                            </asp:SqlDataSource>
                            <cc1:ConfirmButtonExtender TargetControlID="DeleteOrganizationUnitBtn" meta:resourcekey="ConfirmButtonExtender_DeleteUnit"
                                ID="ConfirmButtonExtender1" runat="server">
                            </cc1:ConfirmButtonExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="UnitNameCtl"
                                Display="None" meta:resourcekey="RequiredFieldValidator_UnitName" ValidationGroup="EditOU"></asp:RequiredFieldValidator>
                            <asp:ValidationSummary ID="ValidationSummary3" runat="server" ShowMessageBox="True"
                                ShowSummary="False" ValidationGroup="EditOU" />
                        </asp:Panel>
                        <br />
                        <asp:Panel ID="NewOUPanelArea" runat="server" CssClass="BorderedArea">
                            <div class="title3" style="width: 482px;">
                                <asp:Label runat="server" meta:resourcekey="Title_OUAdd"></asp:Label>
                            </div>
                            <table style="background-color: #f6f6f6; width: 492px;">
                                <tr>
                                    <td style="width: 100px;">
                                        <asp:Label runat="server" meta:resourcekey="OU_Label_OUName"></asp:Label>
                                    </td>
                                    <td style="width: 240px;">
                                        <asp:TextBox ID="NewUnitNameCtl" runat="server" CssClass="InputText" Width="240px"
                                            MaxLength="50"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" meta:resourcekey="OU_Label_OUCode"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="NewUnitCodeCtl" runat="server" CssClass="InputText" Width="240px"
                                            MaxLength="50"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" meta:resourcekey="OU_Label_OUType"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="NewUnitTypeCtl" runat="server" DataSourceID="OrganizationUnitTypeSqlDataSource"
                                            DataTextField="OrganizationUnitTypeName" DataValueField="OrganizationUnitTypeId"
                                            OnDataBound="NewUnitTypeCtl_DataBound" CssClass="InputCombo" Width="240px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" meta:resourcekey="OU_Label_CostCenter"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="NewCostCenterDDL" runat="server" DataSourceID="odsCostCenter"
                                            DataTextField="CostCenterCode" DataValueField="CostCenterID" CssClass="InputCombo"
                                            Width="240px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>    
                                <tr>
                                    <td style="width: 100px;">
                                        <asp:Label ID="Label6" runat="server" meta:resourcekey="Label_FlowParameter"></asp:Label>
                                    </td>
                                    <td style="width: 240px;">
                                        <asp:TextBox ID="NewFlowParameterCtl" runat="server" CssClass="InputText" Width="240px"
                                            MaxLength="50"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Button ID="AddOrganizationUnitBtn" runat="server" meta:resourcekey="Button_AddUnit" OnClick="AddOrganizationUnitBtn_Click"
                                            CssClass="button_big" ValidationGroup="NewOU" />
                                    </td>
                                </tr>
                            </table>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="NewUnitNameCtl"
                                Display="None" meta:resourcekey="RequiredFieldValidator_UnitName" ValidationGroup="NewOU"></asp:RequiredFieldValidator>
                            <asp:ValidationSummary ID="ValidationSummary4" runat="server" ShowMessageBox="True"
                                ShowSummary="False" ValidationGroup="NewOU" />
                        </asp:Panel>
                        <br />
                        <asp:Panel ID="NewPositionPanelArea" runat="server" CssClass="BorderedArea">
                            <div class="title3" style="width: 482px;">
                                <asp:Label runat="server" meta:resourcekey="Title_PositionAdd"></asp:Label>
                            </div>
                            <table style="background-color: #f6f6f6; width: 492px;">
                                <tr>
                                    <td style="width: 100px;">
                                        <asp:Label runat="server" meta:resourcekey="Position_Label_PositionName"></asp:Label>
                                    </td>
                                    <td style="width: 240px;">
                                        <asp:TextBox ID="NewPositionNameCtl" runat="server" CssClass="InputText" Width="240px"
                                            MaxLength="50"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100px;">
                                        可查看部门单据
                                    </td>
                                    <td style="width: 240px;">
                                        <asp:CheckBox ID="NewIsManagerCtl" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100px;">
                                        <asp:Label ID="label8" runat="server" Text="流程角色级别"></asp:Label>
                                    </td>
                                    <td style="width: 240px;">
                                        <asp:TextBox ID="NewFlowLevelCtl" runat="server" CssClass="InputText" Width="240px"
                                            MaxLength="2"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" meta:resourcekey="Position_Label_WorkflowRole"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:CheckBoxList ID="NewPositionTypeCtl" runat="server" DataSourceID="PositionTypeSqlDataSource"
                                            DataTextField="PositionTypeName" DataValueField="PositionTypeId" />
                                        <%--<asp:ListBox ID="NewPositionTypeCtl" runat="server" DataSourceID="PositionTypeSqlDataSource"
                                                DataTextField="PositionTypeName" DataValueField="PositionTypeId" AppendDataBoundItems="true"
                                                SelectionMode="Multiple" Width="240px">
                                                <asp:ListItem Value="" Text="Please select" />
                                                </asp:ListBox>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Button ID="AddPositionBtn" runat="server" meta:resourcekey="Button_AddPosition" OnClick="AddPositionBtn_Click"
                                            CssClass="button_nor" ValidationGroup="NewPosition" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                            <asp:SqlDataSource ID="PositionTypeSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                SelectCommand="SELECT [PositionTypeId], [PositionTypeName] FROM [PositionType]">
                            </asp:SqlDataSource>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="NewPositionNameCtl"
                                Display="None" meta:resourcekey="RequiredFieldValidator_PositionName" ValidationGroup="NewPosition"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="NewFlowLevelCtl"
                                Display="None"  ErrorMessage="请录入流程角色级别" ValidationGroup="NewPosition"></asp:RequiredFieldValidator>
                            <asp:ValidationSummary ID="ValidationSummary5" runat="server" ShowMessageBox="True"
                                ShowSummary="False" ValidationGroup="NewPosition" />
                        </asp:Panel>
                        <br />
                        <asp:Panel ID="EditPositionPanelArea" runat="server" CssClass="BorderedArea">
                            <div class="title3" style="width: 482px;">
                                <asp:Label runat="server" meta:resourcekey="Title_PositionEdit"></asp:Label>
                            </div>
                            <table style="background-color: #f6f6f6; width: 492px;">
                                <tr>
                                    <td style="width: 100px;">
                                        <asp:Label runat="server" meta:resourcekey="Position_Label_PositionName"></asp:Label>
                                    </td>
                                    <td style="width: 240px;">
                                        <asp:TextBox ID="PositionNameCtl" runat="server" CssClass="InputText" Width="240px"
                                            MaxLength="50"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100px;">
                                        可查看部门单据
                                    </td>
                                    <td style="width: 240px;">
                                        <asp:CheckBox ID="IsManagerCtl" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100px;">
                                        <asp:Label ID="label9" runat="server" Text="流程角色级别"></asp:Label>
                                    </td>
                                    <td style="width: 240px;">
                                        <asp:TextBox ID="FlowLevelCtl" runat="server" CssClass="InputText" Width="240px"
                                            MaxLength="2"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <asp:Label ID="Label_PositionType" runat="server" meta:resourcekey="Label_PositionType"/>
                                    </td>
                                    <td>
                                        <asp:CheckBoxList ID="PositionTypeCtl" runat="server" DataSourceID="PositionTypeSqlDataSource"
                                            DataTextField="PositionTypeName" DataValueField="PositionTypeId" />
                                        <%--<asp:ListBox ID="PositionTypeCtl" runat="server" DataSourceID="PositionTypeSqlDataSource"
                                                DataTextField="PositionTypeName" DataValueField="PositionTypeId" OnDataBound="PositionTypeCtl_DataBound"
                                                SelectionMode="Multiple" Width="240px"></asp:ListBox>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="PositionIsActiveCtl" meta:resourcekey="CheckBox_IsActive" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Button ID="ChangeOrganizationUnitBtn" runat="server" meta:resourcekey="Button_ChangeParentUnit" OnClick="ChangeOrganizationUnitBtn_Click"
                                            CausesValidation="False" CssClass="button_big" />
                                        <asp:Button ID="UpdatePositionBtn" runat="server" Text="<%$Resources:Common,Button_Update %>" OnClick="UpdatePositionBtn_Click"
                                            CssClass="button_nor" ValidationGroup="EditPosition" />
                                        <asp:Button ID="DeletePositionBtn" runat="server" Text="<%$Resources:Common,Button_Delete %>" OnClick="DeletePositionBtn_Click"
                                            CausesValidation="False" CssClass="button_nor" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="PositionNameCtl"
                                            Display="None" meta:resourcekey="RequiredFieldValidator_PositionName" ValidationGroup="EditPosition"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="FlowLevelCtl"
                                            Display="None"  ErrorMessage="请录入流程角色级别" ValidationGroup="EditPosition"></asp:RequiredFieldValidator>
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                            ShowSummary="False" ValidationGroup="EditPosition" />
                                    </td>
                                </tr>
                            </table>
                            <cc1:ConfirmButtonExtender TargetControlID="DeletePositionBtn" meta:resourcekey="ConfirmButtonExtender_DeletePosition"
                                ID="ConfirmButtonExtender2" runat="server">
                            </cc1:ConfirmButtonExtender>
                        </asp:Panel>
                        <br />
                        <asp:Panel ID="StuffPanel" runat="server" CssClass="BorderedArea">
                            <div class="title3" style="width: 482px;">
                                <asp:Label ID="Label_Title" runat="server" meta:resourcekey="Label_Title"/>
                            </div>
                            <gc:GridView ID="StuffGridView" Width="492px" CssClass="GridView" runat="server"
                                AutoGenerateColumns="False" DataKeyNames="StuffUserId" DataSourceID="PositionStuffDS"
                                CellPadding="0">
                                <HeaderStyle CssClass="Header" />
                                <Columns>
                                    <asp:BoundField DataField="StuffUserId" HeaderText="StuffUserId" InsertVisible="False"
                                        ReadOnly="True" SortExpression="StuffUserId" Visible="False" />
                                    <asp:BoundField DataField="UserName" ItemStyle-HorizontalAlign="Center" meta:resourcekey="CheckBoxField_UserName"
                                        SortExpression="UserName" />
                                    <asp:BoundField DataField="StuffName" ItemStyle-HorizontalAlign="Center" meta:resourcekey="CheckBoxField_StuffName"
                                        SortExpression="StuffName" />
                                    <asp:BoundField DataField="StuffId" ItemStyle-HorizontalAlign="Center" HeaderText="<%$Resources:Common,Form_StaffNo %>"
                                        SortExpression="StuffId" />
                                    <asp:CheckBoxField DataField="IsActive" ItemStyle-HorizontalAlign="Center" meta:resourcekey="CheckBoxField_IsActive"
                                        SortExpression="IsActive" />
                                </Columns>
                                <EmptyDataTemplate>
                                    <table width="370px">
                                        <tr class="Header">
                                            <td class="Empty1">
                                                <asp:Label  runat="server" meta:resourcekey="Label_UserName"/>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label1" runat="server" meta:resourcekey="Label_StuffName"/>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label3" runat="server" Text="<%$Resources:Common,Form_StaffNo %>"/>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label5" runat="server" meta:resourcekey="Label_IsActive"/>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="text-align: center;" class="Empty2 noneLabel">
                                                <asp:Label ID="Label7" runat="server" meta:resourcekey="Label_NoStuff"/>
                                            </td>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                            </gc:GridView>
                            <asp:ObjectDataSource ID="PositionStuffDS" runat="server" SelectMethod="GetDataByPositionId"
                                TypeName="BusinessObjects.AuthorizationDSTableAdapters.StuffUserTableAdapter">
                                <SelectParameters>
                                    <asp:Parameter Name="PositionId" Type="Int32" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </asp:Panel>
                        <br />
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
