<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PositionManage.aspx.cs" Inherits="PositionManagePage"
    MasterPageFile="~/MasterPage.master"  Culture="Auto" UICulture="Auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="title">
        <asp:Label runat="server" Text='<%$Resources:Common,Label_SearchCondition %>'></asp:Label>
    </div>
    <div class="searchDiv">
        <table class="searchTable" cellpadding="0" cellspacing="0">
            <tr style="vertical-align: top; height: 40px">
                <td style="width: 200px;">
                    <div class="field_title">
                        <asp:Label runat="server" meta:resourcekey="StaffUser_Label_LoginId"></asp:Label></div>
                    <asp:TextBox ID="UserAccountTextBox" runat="server" CssClass="InputText" Width="180px"></asp:TextBox>
                </td>
                <td style="width: 200px;">
                    <div class="field_title">
                        <asp:Label runat="server" meta:resourcekey="StaffUser_Label_Username"></asp:Label></div>
                    <asp:TextBox ID="StuffNameTextBox" runat="server" CssClass="InputText" Width="180px"></asp:TextBox>
                </td>
                <td style="width: 200px;">
                    <div class="field_title">
                        <asp:Label runat="server" Text="<%$Resources:Common,Form_StaffNo %>"></asp:Label></div>
                    <asp:TextBox ID="EmployeeNoTextBox" runat="server" CssClass="InputText" Width="180px"></asp:TextBox>
                </td>
                <td style="width: 200px;">
                    <div class="field_title">
                        <asp:Label ID="Label1" runat="server" meta:resourcekey="StaffUser_Label_Email"></asp:Label></div>
                    <asp:TextBox ID="EmailTextBox" runat="server" CssClass="InputText" Width="180px"></asp:TextBox>
                </td>
                <td style="width: 200px;">
                    <div class="field_title">
                        <asp:Label ID="Label4" runat="server" meta:resourcekey="StaffUser_Label_Tel"></asp:Label></div>
                    <asp:TextBox ID="TelTextBox" runat="server" CssClass="InputText" Width="180px"></asp:TextBox>
                </td>
                <td style="width: 120px">
                    &nbsp;
                </td>
                <td style="width: 150px;" valign="bottom">
                    <asp:Button runat="server" ID="SearchButton" Text='<%$Resources:Common,Button_Search %>' CssClass="button_nor" OnClick="SearchButton_Click">
                    </asp:Button>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div class="title">
        <asp:Label ID="Label5" runat="server"  meta:resourcekey="StaffUser_Title_Label"></asp:Label>
    </div>
    <gc:GridView ID="StuffUserGridView" runat="server" AllowPaging="True" AllowSorting="True"
        AutoGenerateColumns="False" DataKeyNames="StuffUserId" DataSourceID="StuffUserObjectDataSource"
        PageSize="20" ShowFooter="false" CssClass="GridView" OnSelectedIndexChanged="StuffUserGridView_SelectedIndexChanged"
        OnRowDataBound="StuffUserGridView_RowDataBound" CellPadding="0">
        <Columns>
            <asp:TemplateField SortExpression="StuffName"  meta:resourcekey="StaffUserGridView_TemplateField_Username">
                <ItemStyle Width="167px" HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:Label ID="StuffNamelbl" runat="server" Text='<%# Bind("StuffName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Common,Form_Position %>">
                <ItemTemplate>
                    <asp:Label ID="PositionsLabel" runat="server"></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="300px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="UserName" meta:resourcekey="StaffUserGridView_TemplateField_LoginId">
                <ItemStyle Width="150px" HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:Label ID="lblUsername" runat="server" Text='<%# Bind("UserName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField SortExpression="StuffId" HeaderText="<%$Resources:Common,Form_StaffNo %>">
                <ItemStyle Width="115px" HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:Label ID="lblStuffId" runat="server" Text='<%# Bind("StuffId") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField SortExpression="Email" meta:resourcekey="StaffUserGridView_TemplateField_Email">
                <ItemStyle Width="250px" HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:Label ID="Label111" runat="server" Text='<%# Bind("Email") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField SortExpression="IsActive" HeaderText="<%$Resources:Common,Form_IsActive %>">
                <ItemStyle Width="100px" HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox2" runat="server" Checked='<%# Bind("IsActive") %>' Enabled="false" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField SortExpression="AttendDate" HeaderText="<%$Resources:Common,Form_AttendDate %>">
                <ItemStyle Width="100px" HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:Label ID="Label112" runat="server" Text='<%# Bind("AttendDate","{0:yyyy-MM-dd}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False">
                <ItemStyle Width="80px" HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:LinkButton Visible="<%# HasManageRight %>" ID="SetPositionLB" runat="server"
                        CausesValidation="False" CommandName="Select" meta:resourcekey="Position_Button_SetPosition"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="StuffUserId" HeaderText="StuffUserId" InsertVisible="False"
                ReadOnly="True" SortExpression="StuffUserId" Visible="False" />
            <asp:BoundField DataField="UserPassword" HeaderText="UserPassword" SortExpression="UserPassword"
                Visible="False" />
            <asp:BoundField DataField="EMail" HeaderText="EMail" SortExpression="EMail" Visible="False" />
            <asp:BoundField DataField="Telephone" HeaderText="Telephone" SortExpression="Telephone"
                Visible="False" />
        </Columns>
        <SelectedRowStyle CssClass="SelectedRow" />
        <HeaderStyle CssClass="Header" />
    </gc:GridView>
    <br />
    <div id="PositionSetPanel" runat="server">
        <div class="title">
            <asp:Label runat="server" meta:resourcekey="Position_Title_Label"></asp:Label>
        </div>
        <gc:GridView ID="StuffUserPositionGridView" runat="server" AutoGenerateColumns="False"
            CssClass="GridView" DataKeyNames="PositionId" DataSourceID="StuffUserPositionDS"
            OnRowDataBound="StuffUserPositionGridView_RowDataBound" OnSelectedIndexChanged="StuffUserPositionGridView_SelectedIndexChanged">
            <Columns>
                <asp:BoundField DataField="PositionId" HeaderText="PositionId" InsertVisible="False"
                    ReadOnly="True" SortExpression="PositionId" Visible="False" />
                <asp:TemplateField HeaderText="<%$Resources:Common,Form_Organization %>">
                    <ItemStyle Width="1100px" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="ParentOUNamesCtl" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField SortExpression="PositionName" HeaderText="<%$Resources:Common,Form_Position %>">
                    <ItemStyle Width="168px" HorizontalAlign="Center"/>
                    <ItemTemplate>
                        <asp:LinkButton CommandName="Select" ID="LinkButton3" Text='<%# Bind("PositionName") %>'
                            runat="server"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <table>
                    <tr class="Header">
                        <td align="center" style="width: 1100px;" class="Empty1">
                            <asp:Label ID="Label3" runat="server" Text="<%$Resources:Common,Form_Organization %>"/>
                        </td>
                        <td align="center" style="width: 168px;">
                            <asp:Label ID="Label6" runat="server" Text="<%$Resources:Common,Form_Position %>"/>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2" class="Empty2 noneLabel">
                            <asp:Label ID="Label_None" runat="server" Text="<%$Resources:Common,Label_None %>"/>
                        </td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <HeaderStyle CssClass="Header" />
        </gc:GridView>
        <asp:ObjectDataSource ID="StuffUserPositionDS" runat="server" SelectMethod="GetPositionByStuffUser"
            TypeName="BusinessObjects.AuthorizationBLL">
            <SelectParameters>
                <asp:Parameter Name="stuffUserId" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <br />
        <div class="title">
            <asp:Label ID="Label2" runat="server" meta:resourcekey="Position_Title_Label1"></asp:Label>
        </div>
        <div class="BorderedArea" style="width: 1240px; height: 400px; overflow: auto;">
            <asp:TreeView ID="OrganizationTreeView" runat="server" ExpandDepth="2" ShowLines="True">
                <SelectedNodeStyle BackColor="#000040" ForeColor="White" />
            </asp:TreeView>
        </div>
        <div style="text-align: center;">
            <asp:Button ID="SavePositionBtn" Enabled="<%# HasManageRight %>" Visible="<%# HasManageRight %>"
                runat="server" CssClass="button_nor" OnClick="SavePositionBtn_Click" meta:resourcekey="Button_SavePosition" /></div>
    </div>
    <asp:ObjectDataSource ID="StuffUserObjectDataSource" runat="server" OldValuesParameterFormatString="{0}"
        SelectMethod="GetStuffUserPaged" TypeName="BusinessObjects.StuffUserBLL" SelectCountMethod="TotalCount"
        SortParameterName="sortExpression" EnablePaging="true" OnSelecting="StuffUserObjectDataSource_Selecting">
        <SelectParameters>
            <asp:Parameter Name="queryExpression" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
