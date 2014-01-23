<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StuffUserManage.aspx.cs"
    Inherits="StuffUserManagePage" MasterPageFile="~/MasterPage.master" Culture="Auto"
    UICulture="Auto" %>

<%@ Register Src="../UserControls/UCDateInput.ascx" TagName="UCDateInput" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <script src="../Script/DateInput.js" type="text/javascript"></script>
    <div class="title">
        <asp:Label runat="server" Text='<%$Resources:Common,Label_SearchCondition %>'></asp:Label></div>
    <div class="searchDiv">
        <table class="searchTable" cellpadding="0" cellspacing="0">
            <tr style="vertical-align: top; height: 40px">
                <td style="width: 200px;">
                    <div class="field_title">
                        <asp:Label runat="server" meta:resourcekey="StaffUser_Label_LoginId"></asp:Label></div>
                    <asp:TextBox ID="UserAccountTextBox" runat="server" Width="160px"></asp:TextBox>
                </td>
                <td style="width: 200px;">
                    <div class="field_title">
                        <asp:Label ID="Label1" runat="server" meta:resourcekey="StaffUser_Label_Username"></asp:Label></div>
                    <asp:TextBox ID="StuffNameTextBox" runat="server" Width="160px"></asp:TextBox>
                </td>
                <td style="width: 200px;">
                    <div class="field_title">
                        <asp:Label ID="Label4" runat="server" Text="<%$Resources:Common,Form_StaffNo %>"></asp:Label></div>
                    <asp:TextBox ID="EmployeeNoTextBox" runat="server" Width="160px"></asp:TextBox>
                </td>
                <td style="width: 200px;">
                    <div class="field_title">
                        <asp:Label ID="Label5" runat="server" meta:resourcekey="StaffUser_Label_Email"></asp:Label></div>
                    <asp:TextBox ID="EmailTextBox" runat="server" Width="160px"></asp:TextBox>
                </td>
                <td style="width: 200px;">
                    <div class="field_title">
                        <asp:Label ID="Label7" runat="server" meta:resourcekey="StaffUser_Label_Tel"></asp:Label></div>
                    <asp:TextBox ID="TelTextBox" runat="server" Width="160px"></asp:TextBox>
                </td>
                <td colspan="2" align="left" valign="bottom">
                    <input type="hidden" id="btnclicked" name="btnclicked" value="0" />
                    <asp:Button ID="SearchButton" runat="server" CssClass="button_nor" Text='<%$Resources:Common,Button_Search %>'
                        OnClick="SearchButton_Click" />&nbsp;&nbsp;
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div class="title">
        <asp:Label runat="server" meta:resourcekey="Title_Label"></asp:Label>
    </div>
    <asp:UpdatePanel ID="StuffUserUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <gc:GridView ID="StuffUserGridView" runat="server" AllowPaging="True" AllowSorting="True"
                AutoGenerateColumns="False" DataKeyNames="StuffUserId" DataSourceID="StuffUserObjectDataSource"
                PageSize="20" ShowFooter="True" CssClass="GridView" OnSelectedIndexChanged="StuffUserGridView_SelectedIndexChanged"
                OnRowDataBound="StuffUserGridView_RowDataBound">
                <Columns>
                    <asp:TemplateField SortExpression="UserName" meta:resourcekey="StaffUserGridView_TemplateField_LoginID">
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("UserName") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="140px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="EnglishName" meta:resourcekey="StaffUserGridView_TemplateField_EnglishName">
                        <ItemTemplate>
                            <asp:LinkButton ID="EnglishNameLinkButton" runat="server" CommandName="Select" OnClick="StuffNameLinkButton_Click"
                                Text='<%# Bind("EnglishName") %>'></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="120px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="StuffName" meta:resourcekey="StaffUserGridView_TemplateField_Username">
                        <ItemTemplate>
                            <asp:Label ID="Label123" runat="server" Text='<%# Bind("StuffName") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Common,Form_Position %>">
                        <ItemTemplate>
                            <asp:Label ID="PositionsLabel" runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="320px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="StuffId" HeaderText="<%$Resources:Common,Form_StaffNo %>">
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("StuffId") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="Email" meta:resourcekey="StaffUserGridView_TemplateField_Email">
                        <ItemTemplate>
                            <asp:Label ID="Label111" runat="server" Text='<%# Bind("Email") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="250px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="AttendDate" HeaderText="<%$Resources:Common,Form_AttendDate %>">
                        <ItemTemplate>
                            <asp:Label ID="Label6" runat="server" Text='<%# Eval("AttendDate","{0:yyyy-MM-dd}" ) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="IsActive" HeaderText="<%$Resources:Common,Form_IsActive %>">
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox2" runat="server" Checked='<%# Bind("IsActive") %>' Enabled="false" />
                        </ItemTemplate>
                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <EditItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update"
                                Text='<%$Resources:Common,Button_Update %>'></asp:LinkButton>
                            <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel"
                                Text="'<%$Resources:Common,Button_Back %>'"></asp:LinkButton>
                        </EditItemTemplate>
                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:LinkButton Visible="<%# HasManageRight %>" ID="EditLinkButton" runat="server"
                                CausesValidation="False" CommandName="Select" Text='<%$Resources:Common,Button_Edit %>'
                                OnClick="EditLinkButton_Click"></asp:LinkButton>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:LinkButton Visible="<%# HasManageRight %>" ID="AddLinkButton1" runat="server"
                                CausesValidation="True" CommandName="Select" Text='<%$Resources:Common,Button_Add %>'
                                OnClick="AddLinkButton_Click"></asp:LinkButton>
                        </FooterTemplate>
                        <FooterStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="StuffUserId" HeaderText="StuffUserId" InsertVisible="False"
                        ReadOnly="True" SortExpression="StuffUserId" Visible="False" />
                    <asp:BoundField DataField="UserPassword" HeaderText="UserPassword" SortExpression="UserPassword"
                        Visible="False" />
                    <asp:BoundField DataField="EMail" HeaderText="EMail" SortExpression="EMail" Visible="False" />
                    <asp:BoundField DataField="Telephone" HeaderText="Telephone" SortExpression="Telephone"
                        Visible="False" />
                </Columns>
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td style="width: 14px;" class="Empty1">
                                <asp:Label runat="server" meta:resourcekey="StaffUserGridView_TemplateField_LoginId"></asp:Label>
                            </td>
                            <td style="width: 120px;">
                                <asp:Label ID="Label16" runat="server" meta:resourcekey="StaffUserGridView_TemplateField_EnglishName"></asp:Label>
                            </td>
                            <td style="width: 100px;">
                                <asp:Label runat="server" meta:resourcekey="StaffUserGridView_TemplateField_Username"></asp:Label>
                            </td>
                            <td style="width: 320px;">
                                <asp:Label runat="server" Text="<%$Resources:Common,Form_Position %>"></asp:Label>
                            </td>
                            <td style="width: 100px;">
                                <asp:Label runat="server" Text="<%$Resources:Common,Form_StaffNo %>"></asp:Label>
                            </td>
                            <td style="width: 250px;">
                                <asp:Label runat="server" meta:resourcekey="StaffUserGridView_TemplateField_Email"></asp:Label>
                            </td>
                            <td style="width: 100px;">
                                <asp:Label runat="server" Text="<%$Resources:Common,Form_AttendDate %>"></asp:Label>
                            </td>
                            <td style="width: 100px;">
                                <asp:Label runat="server" Text="<%$Resources:Common,Form_IsActive %>"></asp:Label>
                            </td>
                            <td style="width: 80px;">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="8" class="Empty2 noneLabel">
                                <asp:Label runat="server" Text='<%$Resources:Common,Label_None %>'></asp:Label>
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <SelectedRowStyle CssClass="SelectedRow" />
                <HeaderStyle CssClass="Header" />
                <FooterStyle CssClass="Footer" />
            </gc:GridView>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="SearchButton" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="StuffUserFormView" EventName="ItemInserted" />
            <asp:AsyncPostBackTrigger ControlID="StuffUserFormView" EventName="ItemUpdated" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="StuffUserAddUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:FormView ID="StuffUserFormView" runat="server" DataKeyNames="StuffUserId" DataSourceID="StuffUserAddObjectDataSource"
                Width="1240px" OnItemUpdating="StuffUserFormView_ItemUpdating" OnDataBound="StuffUserFormView_DataBound"
                OnItemInserting="StuffUserFormView_ItemInserting">
                <EditItemTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr style="height: 30px">
                            <td align="left" style="text-align: right; width: 22%;">
                                &nbsp;<asp:Label runat="server" meta:resourcekey="StaffUser_Label_LoginId"></asp:Label>
                            </td>
                            <td align="left" style="width: 28%;">
                                &nbsp;<asp:TextBox ID="UserNameTextBox" runat="server" Text='<%# Bind("UserName") %>'
                                    ValidationGroup="EDIT" Width="200px" MaxLength="50" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td align="left" style="text-align: right; width: 22%;">
                                &nbsp;<asp:Label ID="Label18" runat="server" meta:resourcekey="StaffUser_Label_EnglishName"></asp:Label>
                            </td>
                            <td align="left" style="width: 28%;">
                                &nbsp;<asp:TextBox ID="EnglishNameTextBox" runat="server" Text='<%# Bind("EnglishName") %>'
                                    ValidationGroup="EDIT" Width="200px" MaxLength="50"></asp:TextBox>&nbsp;
                            </td>
                        </tr>
                        <tr style="height: 30px">
                            <td align="left" style="text-align: right">
                                &nbsp;<asp:Label ID="Label11" runat="server" meta:resourcekey="Label_UserPassword"/>
                            </td>
                            <td align="left">
                                &nbsp;<asp:TextBox ID="UserPasswordTextBox" runat="server" ValidationGroup="EDIT"
                                    Width="200px" Text="!!!!!!" TextMode="Password" MaxLength="50"></asp:TextBox>
                            </td>
                            <td align="left" style="text-align: right; width: 22%;">
                                &nbsp;<asp:Label ID="Label17" runat="server" meta:resourcekey="StaffUser_Label_Username"></asp:Label>
                            </td>
                            <td align="left" style="width: 28%;">
                                &nbsp;<asp:TextBox ID="StuffNameTextBox" runat="server" Text='<%# Bind("StuffName") %>'
                                    ValidationGroup="EDIT" Width="200px" MaxLength="50"></asp:TextBox>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="text-align: right">
                                &nbsp;<asp:Label ID="Label10" runat="server" meta:resourcekey="Label_UserPasswords"/>
                            </td>
                            <td align="left">
                                &nbsp;<asp:TextBox ID="UserPasswordsTextBox" runat="server" ValidationGroup="EDIT"
                                    Text="!!!!!!" Width="200px" TextMode="Password" MaxLength="50"></asp:TextBox>
                            </td>
                            <td align="left" style="text-align: right">
                                &nbsp;<asp:Label ID="Label19" runat="server" Text="<%$Resources:Common,Form_StaffNo %>"></asp:Label>
                            </td>
                            <td align="left">
                                &nbsp;<asp:TextBox ID="StuffIdTextBox" runat="server" Text='<%# Bind("StuffId") %>'
                                    ValidationGroup="EDIT" Width="200px" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="height: 30px">
                            <td align="left" style="text-align: right">
                                &nbsp;<asp:Label runat="server" meta:resourcekey="StaffUser_Label_Tel"></asp:Label>
                            </td>
                            <td align="left">
                                &nbsp;<asp:TextBox ID="TelephoneTextBox" runat="server" Text='<%# Bind("Telephone") %>'
                                    Width="200px" MaxLength="50" ValidationGroup="EDIT"></asp:TextBox>
                            </td>
                            <td align="left" style="text-align: right">
                                &nbsp;<asp:Label runat="server" Text="<%$Resources:Common,Form_AttendDate %>"></asp:Label>
                            </td>
                            <td align="left" style="padding-left: 6px;">
                                <uc1:UCDateInput ID="UCAttendDate" runat="server" SelectedDate='<%#Bind("AttendDate","{0:yyyy-MM-dd}")%>' />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="text-align: right">
                                职级
                            </td>
                            <td align="left">
                                &nbsp;<asp:DropDownList runat="server" ID="ddlStaffLevel" DataSourceID="odsStaffLevel"
                                    DataTextField="StaffLevelName" DataValueField="StaffLevelID" SelectedValue='<%# Bind("StaffLevelID") %>'
                                    Width="210px">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="odsStaffLevel" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                    SelectCommand="SELECT [StaffLevelID], [StaffLevelName] FROM [StaffLevel] ">
                                </asp:SqlDataSource>
                            </td>
                            <td align="left" style="text-align: right">
                                <asp:Label ID="Label15" runat="server" Text="<%$Resources:Common,Form_IsActive %>"></asp:Label>
                            </td>
                            <td align="left">
                                &nbsp;<asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("IsActive") %>' />
                            </td>
                        </tr>
                        <tr style="height: 30px">
                            <td align="left" style="text-align: right">
                                &nbsp;<asp:Label ID="Label20" runat="server" meta:resourcekey="StaffUser_Label_Email"></asp:Label>
                            </td>
                            <td align="left">
                                &nbsp;<asp:TextBox ID="EMailTextBox" runat="server" Text='<%# Bind("EMail") %>' Width="200px"
                                    MaxLength="100" ValidationGroup="EDIT"></asp:TextBox>
                            </td>
                            <td align="left" style="text-align: right">
                                &nbsp;<asp:Label ID="Label28" runat="server" meta:resourcekey="StaffUser_Label_VendorCode"></asp:Label>
                            </td>
                            <td align="left">
                                &nbsp;<asp:TextBox ID="VendorCodeTextBox" runat="server" Text='<%# Bind("VendorCode") %>' Width="200px"
                                    MaxLength="100" ValidationGroup="EDIT"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="text-align: right">
                            </td>
                            <td align="left" style="text-align: right">
                            </td>
                            <td align="left" style="text-align: right">
                            </td>
                            <td align="left">
                                &nbsp;<asp:Button ID="UpdateButton" runat="server" CssClass="button_nor" Text='<%$Resources:Common,Button_Update %>'
                                    CommandName="Update" OnClick="InsertButton_Click" ValidationGroup="EDIT" />
                                <asp:Button ID="Button2" runat="server" CssClass="button_nor" Text='<%$Resources:Common,Button_Back %>'
                                    OnClick="BackButton_Click" CommandName="Cancel" />
                            </td>

                        </tr>
                    </table>
                    <asp:RequiredFieldValidator ID="UserNameRequiredFieldValidator" runat="server" ControlToValidate="UserNameTextBox"
                        Display="Dynamic" meta:resourcekey="RequiredFieldValidator_UserName" SetFocusOnError="True" ValidationGroup="EDIT"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="StuffNameRequiredFieldValidator" runat="server" ControlToValidate="StuffNameTextBox"
                        Display="Dynamic" meta:resourcekey="RequiredFieldValidator_StuffName" SetFocusOnError="True" ValidationGroup="EDIT"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="EnglishNameTextBox"
                        Display="Dynamic" meta:resourcekey="RequiredFieldValidator_EnglishName" SetFocusOnError="True" ValidationGroup="EDIT"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="StuffIDRequiredFieldValidator" runat="server" ControlToValidate="StuffIdTextBox"
                        Display="Dynamic" meta:resourcekey="RequiredFieldValidator_StuffID" SetFocusOnError="True" ValidationGroup="EDIT"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="VendorCodeTextBox"
                        Display="Dynamic" ErrorMessage="please key in Vendor Code" SetFocusOnError="True" ValidationGroup="EDIT"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="EMailRegularExpressionValidator" runat="server"
                        ControlToValidate="EMailTextBox" Display="Dynamic" meta:resourcekey="RegularExpressionValidator_Email"
                        SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                        ValidationGroup="EDIT"></asp:RegularExpressionValidator>
                    <asp:ValidationSummary ID="ValidationSummaryEdit" runat="server" ShowMessageBox="True"
                        ShowSummary="true" ValidationGroup="EDIT" />
                </EditItemTemplate>
                <InsertItemTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr style="height: 30px">
                            <td align="left" style="text-align: right; width: 22%;">
                                &nbsp;<asp:Label runat="server" meta:resourcekey="StaffUser_Label_LoginId"></asp:Label>
                            </td>
                            <td align="left" style="width: 28%;">
                                &nbsp;<asp:TextBox ID="UserNameTextBox" runat="server" Text='<%# Bind("UserName") %>'
                                    ValidationGroup="INS" Width="200px" MaxLength="50"></asp:TextBox>
                            </td>
                            <td align="left" style="text-align: right; width: 22%;">
                                &nbsp;<asp:Label ID="Label22" runat="server" meta:resourcekey="StaffUser_Label_EnglishName"></asp:Label>
                            </td>
                            <td align="left" width="28%">
                                &nbsp;<asp:TextBox ID="EnglishNameTextBox" runat="server" Text='<%# Bind("EnglishName") %>'
                                    ValidationGroup="INS" Width="200px" MaxLength="50"></asp:TextBox>&nbsp;
                            </td>
                        </tr>
                        <tr style="height: 30px">
                            <td align="left" style="text-align: right">
                                &nbsp;<asp:Label ID="Label12" runat="server" meta:resourcekey="Label_UserPassword"/>
                            </td>
                            <td align="left">
                                &nbsp;<asp:TextBox ID="UserPasswordTextBox" runat="server" Text='<%# Bind("UserPassword") %>'
                                    ValidationGroup="INS" Width="200px" MaxLength="50" TextMode="Password"></asp:TextBox>
                            </td>
                            <td align="left" style="text-align: right; width: 22%;">
                                &nbsp;<asp:Label ID="Label21" runat="server" meta:resourcekey="StaffUser_Label_Username"></asp:Label>
                            </td>
                            <td align="left" width="28%">
                                &nbsp;<asp:TextBox ID="StuffNameTextBox" runat="server" Text='<%# Bind("StuffName") %>'
                                    ValidationGroup="INS" Width="200px" MaxLength="50"></asp:TextBox>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="text-align: right">
                                &nbsp;<asp:Label ID="Label10" runat="server" meta:resourcekey="Label_UserPasswords"/>
                            </td>
                            <td align="left">
                                &nbsp;<asp:TextBox ID="UserPasswordsTextBox" runat="server" ValidationGroup="INS"
                                    Width="200px" MaxLength="50" TextMode="Password"></asp:TextBox>
                            </td>
                            <td align="left" style="text-align: right">
                                &nbsp;<asp:Label ID="Label23" runat="server" Text="<%$Resources:Common,Form_StaffNo %>"></asp:Label>
                            </td>
                            <td align="left">
                                &nbsp;<asp:TextBox ID="StuffIdTextBox" runat="server" Text='<%# Bind("StuffId") %>'
                                    ValidationGroup="INS" Width="200px" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="height: 30px">
                            <td align="left" style="text-align: right">
                                &nbsp;<asp:Label runat="server" meta:resourcekey="StaffUser_Label_Tel"></asp:Label>
                            </td>
                            <td align="left">
                                &nbsp;<asp:TextBox ID="TelephoneTextBox" runat="server" Text='<%# Bind("Telephone") %>'
                                    Width="200px" MaxLength="50"></asp:TextBox>
                            </td>
                            <td align="left" style="text-align: right">
                                &nbsp;<asp:Label runat="server" Text="<%$Resources:Common,Form_AttendDate %>"></asp:Label>
                            </td>
                            <td align="left" style="padding-left: 6px;">
                                <uc1:UCDateInput ID="UCNewAttendDate" runat="server" IsReadOnly="false" SelectedDate='<%#Bind("AttendDate","{0:yyyy-MM-dd}")%>' />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="text-align: right">
                                职级
                            </td>
                            <td align="left">
                                &nbsp;<asp:DropDownList runat="server" ID="ddlNewStaffLevel" DataSourceID="odsStaffLevel"
                                    DataTextField="StaffLevelName" DataValueField="StaffLevelID" SelectedValue='<%# Bind("StaffLevelID") %>'
                                    Width="210px">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="odsStaffLevel" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                    SelectCommand="SELECT [StaffLevelID], [StaffLevelName] FROM [StaffLevel] ">
                                </asp:SqlDataSource>
                            </td>
                            <td align="left" style="text-align: right">
                                &nbsp;<asp:Label ID="Label24" runat="server" meta:resourcekey="StaffUser_Label_Email"></asp:Label>
                            </td>
                            <td align="left">
                                &nbsp;<asp:TextBox ID="EMailTextBox" runat="server" Text='<%# Bind("EMail") %>' Width="200px"
                                    MaxLength="100"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="text-align: right">
                                &nbsp;<asp:Label ID="Label29" runat="server" meta:resourcekey="StaffUser_Label_VendorCode"></asp:Label>
                            </td>
                            <td align="left">
                                &nbsp;<asp:TextBox ID="VendorCodeTextBox" runat="server" Text='<%# Bind("VendorCode") %>' Width="200px"
                                    MaxLength="100"></asp:TextBox>
                            </td>
                            <td align="left" style="text-align: right">
                                &nbsp;
                            </td>
                            <td align="left">
                                &nbsp;<asp:Button ID="InsertButton" runat="server" CssClass="button_nor" Text='<%$Resources:Common,Button_Add %>'
                                    CommandName="Insert" OnClick="InsertButton_Click" ValidationGroup="INS" />
                                <asp:Button ID="BackButton" runat="server" CssClass="button_nor" Text='<%$Resources:Common,Button_Back %>'
                                    OnClick="BackButton_Click" CommandName="Cancel" />
                            </td>
                        </tr>
                    </table>
                    <asp:RequiredFieldValidator ID="UserNameRequiredFieldValidator" runat="server" ControlToValidate="UserNameTextBox"
                        Display="Dynamic" meta:resourcekey="RequiredFieldValidator_UserName" SetFocusOnError="True" ValidationGroup="INS"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="EnglishNameTextBox"
                        Display="Dynamic" meta:resourcekey="RequiredFieldValidator_EnglishName" SetFocusOnError="True" ValidationGroup="INS"></asp:RequiredFieldValidator>&nbsp;
                    <asp:RequiredFieldValidator ID="StuffNameRequiredFieldValidator" runat="server" ControlToValidate="StuffNameTextBox"
                        Display="Dynamic" meta:resourcekey="RequiredFieldValidator_StuffName" SetFocusOnError="True" ValidationGroup="INS"></asp:RequiredFieldValidator>&nbsp;
                    <asp:RequiredFieldValidator ID="StuffIdRequiredFieldValidator" runat="server" ControlToValidate="StuffIdTextBox"
                        Display="Dynamic" meta:resourcekey="RequiredFieldValidator_StuffID" SetFocusOnError="True" ValidationGroup="INS"></asp:RequiredFieldValidator>&nbsp;
                    <asp:RequiredFieldValidator ID="EmailRequiredFieldValidator" runat="server" ControlToValidate="EMailTextBox"
                        Display="Dynamic" meta:resourcekey="RequiredFieldValidator_Email" SetFocusOnError="True" ValidationGroup="INS"></asp:RequiredFieldValidator>&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="VendorCodeTextBox"
                        Display="Dynamic" ErrorMessage="please key in Vendor Code" SetFocusOnError="True" ValidationGroup="INS"></asp:RequiredFieldValidator>&nbsp;
                    <asp:RegularExpressionValidator ID="EMailRegularExpressionValidator" runat="server"
                        ControlToValidate="EMailTextBox" Display="Dynamic" meta:resourcekey="RegularExpressionValidator_Email"
                        SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                        ValidationGroup="INS"></asp:RegularExpressionValidator>
                    <asp:ValidationSummary ID="ValidationSummaryInsert" runat="server" ShowMessageBox="True"
                        ShowSummary="False" ValidationGroup="INS" />
                </InsertItemTemplate>
                <ItemTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr style="height: 30px">
                            <td style="text-align: right; width: 22%;">
                                <asp:Label runat="server" meta:resourcekey="StaffUser_Label_LoginId"></asp:Label>
                            </td>
                            <td style="text-align: left; width: 28%;">
                                &nbsp;<asp:TextBox ID="UserNameTextBox" runat="server" ReadOnly="True" Text='<%# Bind("UserName") %>'
                                    Width="200px"></asp:TextBox>
                            </td>
                            <td style="text-align: right; width: 22%;">
                                <asp:Label ID="Label25" runat="server" meta:resourcekey="StaffUser_Label_Username"></asp:Label>
                            </td>
                            <td style="text-align: left; width: 28%;">
                                &nbsp;<asp:TextBox ID="EnglishNameTextBox" runat="server" ReadOnly="True" Text='<%# Bind("EnglishName") %>'
                                    Width="200px"></asp:TextBox>&nbsp;
                            </td>

                        </tr>
                        <tr style="height: 30px">
                            <td style="text-align: right; width: 22%;">
                                <asp:Label ID="Label8" runat="server" meta:resourcekey="StaffUser_Label_Username"></asp:Label>
                            </td>
                            <td style="text-align: left; width: 28%;">
                                &nbsp;<asp:TextBox ID="StuffNameTextBox" runat="server" ReadOnly="True" Text='<%# Bind("StuffName") %>'
                                    Width="200px"></asp:TextBox>&nbsp;
                            </td>

                            <td align="left" style="text-align: right">
                                &nbsp;<asp:Label runat="server" Text="<%$Resources:Common,Form_StaffNo %>"></asp:Label>
                            </td>
                            <td align="left">
                                &nbsp;<asp:TextBox ID="StuffIdTextBox" runat="server" ReadOnly="True" Text='<%# Bind("StuffId") %>'
                                    Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="height: 30px">
                            <td align="left" style="text-align: right">
                                <asp:Label ID="Label26" runat="server" meta:resourcekey="StaffUser_Label_Tel"></asp:Label>
                            </td>
                            <td align="left">
                                &nbsp;<asp:TextBox ID="TelephoneTextBox" runat="server" ReadOnly="True" Text='<%# Bind("Telephone") %>'
                                    Width="200px"></asp:TextBox>
                            </td>
                            <td align="left" style="text-align: right;">
                                &nbsp;<asp:Label runat="server" Text="<%$Resources:Common,Form_AttendDate %>"></asp:Label>
                            </td>
                            <td align="left">
                                &nbsp;<asp:TextBox ID="AttendDateTextBox" runat="server" ReadOnly="True" Text='<%# Bind("AttendDate","{0:yyyy-MM-dd}") %>'
                                    Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="text-align: right">
                                职级
                            </td>
                            <td align="left">
                                &nbsp;<asp:DropDownList runat="server" ID="ddlViewStaffLevel" DataSourceID="odsStaffLevel"
                                    DataTextField="StaffLevelName" DataValueField="StaffLevelID" SelectedValue='<%# Bind("StaffLevelID") %>'
                                    Width="210px" Enabled="false">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="odsStaffLevel" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                    SelectCommand="SELECT [StaffLevelID], [StaffLevelName] FROM [StaffLevel] ">
                                </asp:SqlDataSource>
                            </td>
                            <td align="left" style="text-align: right;">
                                <asp:Label ID="Label9" runat="server" Text="<%$Resources:Common,Form_IsActive %>" ></asp:Label>
                            </td>
                            <td align="left">
                                <asp:CheckBox ID="IsActiveCheckBox" runat="server" Checked='<%# Bind("IsActive") %>'
                                    Enabled="False" />
                            </td>
                        </tr>
                        <tr style="height: 30px">
                            <td align="left" style="text-align: right;">
                                &nbsp;<asp:Label ID="Label27" runat="server" meta:resourcekey="StaffUser_Label_Email"></asp:Label>
                            </td>
                            <td align="left">
                                &nbsp;<asp:TextBox ID="EMailTextBox" runat="server" ReadOnly="True" Text='<%# Bind("EMail") %>'
                                    Width="200px"></asp:TextBox>
                            </td>
                            <td align="left" style="text-align: right;">
                                &nbsp;<asp:Label ID="Label30" runat="server" meta:resourcekey="StaffUser_Label_VendorCode"></asp:Label>
                            </td>
                            <td align="left">
                                &nbsp;<asp:TextBox ID="VendorCodeTextBox" runat="server" ReadOnly="True" Text='<%# Bind("VendorCode") %>'
                                    Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="text-align: right">
                                &nbsp;
                            </td>
                            <td align="left" style="text-align: right">
                                &nbsp;
                            </td>
                            <td align="left" style="text-align: right">
                                &nbsp;
                            </td>
                            <td align="left">
                                &nbsp;<asp:Button ID="BackButton" runat="server" CssClass="button_nor" Text='<%$Resources:Common,Button_Back %>'
                                    OnClick="BackButton_Click" CommandName="Cancel" />
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:FormView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="OrganizationUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="PositionSetPanel" runat="server">
                <div>
                    <asp:Label ID="Label_Title" runat="server" meta:resourcekey="Label_Title"/></div>
                <div>
                    <gc:GridView ID="StuffUserPositionGridView" runat="server" AutoGenerateColumns="False"
                        CssClass="GridView" DataKeyNames="PositionId" DataSourceID="StuffUserPositionDS"
                        Width="815px" OnRowDataBound="StuffUserPositionGridView_RowDataBound" OnSelectedIndexChanged="StuffUserPositionGridView_SelectedIndexChanged">
                        <Columns>
                            <asp:BoundField DataField="PositionId" HeaderText="PositionId" InsertVisible="False"
                                ReadOnly="True" SortExpression="PositionId" Visible="False" />
                            <asp:TemplateField meta:resourcekey="TemplateField_ParentOU">
                                <ItemStyle Width="700px" />
                                <ItemTemplate>
                                    <asp:Label ID="ParentOUNamesCtl" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField meta:resourcekey="TemplateField_PositionName" SortExpression="PositionName">
                                <ItemStyle Width="115px" />
                                <ItemTemplate>
                                    <asp:LinkButton CommandName="Select" ID="LinkButton3" Text='<%# Bind("PositionName") %>'
                                        runat="server"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <table class="GridView">
                                <tr class="Header">
                                    <td style="width: 700px;">
                                        <asp:Label ID="Label_Title" runat="server" meta:resourcekey="Label_ParentOU"/>
                                    </td>
                                    <td style="width: 115px;">
                                        <asp:Label ID="Label13" runat="server" meta:resourcekey="Label_PositionName"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center;" colspan="2">
                                        <asp:Label ID="Label_None" runat="server" meta:resourcekey="Label_None"/>
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
                </div>
                <div class="TitleBar">
                    <asp:Label ID="Label14" runat="server" meta:resourcekey="Label_Title1"/></div>
                <asp:Label ID="PositionSetLabel" meta:resourcekey="Label_PositionSet" runat="server"></asp:Label>
                <div class="BorderedArea" style="width: 815px; height: 400px; overflow: auto;">
                    <asp:TreeView ID="OrganizationTreeView" runat="server" ExpandDepth="2" ShowLines="True">
                        <SelectedNodeStyle BackColor="#000040" ForeColor="White" />
                    </asp:TreeView>
                </div>
                <div style="text-align: center;">
                    <asp:Button ID="SavePositionBtn" Enabled="<%# HasManageRight %>" Visible="<%# HasManageRight %>"
                        runat="server" CssClass="button_nor" OnClick="SavePositionBtn_Click" meta:resourcekey="Button_SavePosition" /></div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="StuffUserObjectDataSource" runat="server" TypeName="BusinessObjects.StuffUserBLL"
        OldValuesParameterFormatString="{0}" SelectMethod="GetStuffUserPaged" DeleteMethod="DeleteAlterFeeSubType"
        SelectCountMethod="TotalCount" SortParameterName="sortExpression" EnablePaging="true"
        OnSelecting="StuffUserObjectDataSource_Selecting" OnDeleted="StuffUserObjectDataSource_Deleted">
        <DeleteParameters>
            <asp:Parameter Name="StuffUserId" Type="Int32" />
        </DeleteParameters>
        <SelectParameters>
            <asp:Parameter Name="queryExpression" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="StuffUserAddObjectDataSource" runat="server" DeleteMethod="DeleteAlterFeeSubType"
        InsertMethod="InsertStuffUser" OldValuesParameterFormatString="{0}" SelectMethod="GetStuffUser"
        TypeName="BusinessObjects.StuffUserBLL" UpdateMethod="UpdateStuffUser" OnInserted="StuffUserAddObjectDataSource_Inserted"
        OnUpdated="StuffUserAddObjectDataSource_Updated">
        <DeleteParameters>
            <asp:Parameter Name="StuffUserId" Type="Int32" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="StuffUserId" Type="Int32" />
            <asp:Parameter Name="StuffName" Type="String" />
            <asp:Parameter Name="StuffId" Type="String" />
            <asp:Parameter Name="UserName" Type="String" />
            <asp:Parameter Name="UserPassword" Type="String" />
            <asp:Parameter Name="IsActive" Type="Boolean" />
            <asp:Parameter Name="Telephone" Type="String" />
            <asp:Parameter Name="EMail" Type="String" />
            <asp:Parameter Name="EnglishName" Type="String" />
            <asp:Parameter Name="AttendDate" Type="DateTime" />
            <asp:Parameter Name="StaffLevelID" Type="Int32" />
            <asp:Parameter Name="VendorCode" Type="String" />
        </UpdateParameters>
        <SelectParameters>
            <asp:Parameter Name="StuffUserId" Type="Int32" />
        </SelectParameters>
        <InsertParameters>
            <asp:Parameter Name="StuffName" Type="String" />
            <asp:Parameter Name="StuffId" Type="String" />
            <asp:Parameter Name="UserName" Type="String" />
            <asp:Parameter Name="UserPassword" Type="String" />
            <asp:Parameter Name="Telephone" Type="String" />
            <asp:Parameter Name="EMail" Type="String" />
            <asp:Parameter Name="EnglishName" Type="String" />
            <asp:Parameter Name="AttendDate" Type="DateTime" />
            <asp:Parameter Name="StaffLevelID" Type="Int32" />
            <asp:Parameter Name="VendorCode" Type="String" />
        </InsertParameters>
    </asp:ObjectDataSource>
</asp:Content>
