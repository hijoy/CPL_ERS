<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="MyInfoManage.aspx.cs" Inherits="AuthorizationManage_MyInfoManage" Culture="Auto"
    UICulture="Auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table>
        <tr>
            <td>
                <b>
                    <asp:Label ID="Label_StuffName" runat="server" meta:resourcekey="Label_StuffName" /></b>
            </td>
            <td>
                &nbsp;&nbsp;<asp:Label ID="StuffNameCtl" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <b>
                    <asp:Label ID="Label_UserName" runat="server" meta:resourcekey="Label_UserName" /></b>
            </td>
            <td>
                &nbsp;&nbsp;<asp:Label ID="UserNameCtl" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <b>
                    <asp:Label ID="Label_StuffId" runat="server" meta:resourcekey="Label_StuffId" /></b>
            </td>
            <td>
                &nbsp;&nbsp;<asp:Label ID="StuffIdCtl" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <b>
                    <asp:Label ID="Label_LastSetPasswordTime" runat="server" meta:resourcekey="Label_LastSetPasswordTime" /></b>
            </td>
            <td>
                &nbsp;&nbsp;<asp:Label ID="LastSetPasswordTimeCtl" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <b>
                    <asp:Label ID="Label_Password" runat="server" meta:resourcekey="Label_Password" /></b>
            </td>
            <td>
                &nbsp;&nbsp;<asp:TextBox ID="PasswordCtl" TextMode="password" Width="250px" ValidationGroup="EDIT"
                    runat="server" Text="xxxxxx"></asp:TextBox>
                <%--            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="PasswordCtl"
                 Display="None" ValidationExpression="^[A-Za-z]+[A-Za-z0-9]+$"
                ErrorMessage="密码必须包含字母和数字的组合" ValidationGroup="EDIT"></asp:RegularExpressionValidator>
                --%>
            </td>
        </tr>
        <tr>
            <td>
                <b>
                    <asp:Label ID="Label_RepeatPassword" runat="server" meta:resourcekey="Label_RepeatPassword" /></b>
            </td>
            <td>
                &nbsp;&nbsp;<asp:TextBox ID="RepeatPasswordCtl" TextMode="password" Width="250px"
                    runat="server" Text="xxxxxx"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <b>
                    <asp:Label ID="Label_Email" runat="server" meta:resourcekey="Label_Email" /></b>
            </td>
            <td>
                &nbsp;&nbsp;<asp:TextBox ID="EmailCtl" runat="server" Width="250px"></asp:TextBox>
                <asp:RegularExpressionValidator ID="EMailRegularExpressionValidator" runat="server"
                    ControlToValidate="EmailCtl" Display="Dynamic" meta:resourcekey="RegularExpressionValidator_Email"
                    SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                    ValidationGroup="EDIT"></asp:RegularExpressionValidator>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                    ShowSummary="False" ValidationGroup="EDIT" />
            </td>
        </tr>
        <tr>
            <td>
                <b>
                    <asp:Label ID="Label_TelphoneNo" runat="server" meta:resourcekey="Label_TelphoneNo" /></b>
            </td>
            <td>
                &nbsp;&nbsp;<asp:TextBox ID="TelphoneNoCtl" runat="server" Width="250px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center;">
                <asp:Button CssClass="button_nor" Text="<%$Resources:Common,Button_Update %>" ID="SaveBtn"
                    runat="server" CausesValidation="true" ValidationGroup="EDIT" OnClick="SaveBtn_Click" />
                <asp:Button CssClass="button_nor" Text="<%$Resources:Common,Button_Cancel %>" ID="CancelBtn"
                    runat="server" OnClick="CancelBtn_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
