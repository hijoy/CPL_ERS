<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LogIn.aspx.cs" Inherits="LogIn" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/tr/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Campbell Swire 协同办公系统</title>
    <link href="~/css/css.css" rel="stylesheet" type="text/css" runat="server" />
    <style type="text/css">
        .input
        {
            border-right: #4f4f4f 1px solid;
            padding-right: 1px;
            border-top: #4f4f4f 1px solid;
            padding-left: 1px;
            font-size: 9pt;
            padding-bottom: 1px;
            border-left: #4f4f4f 1px solid;
            padding-top: 1px;
            border-bottom: #4f4f4f 1px solid;
            height: 18px;
            background-color: #e7e7e7;
        }
        .12
        {
            font-size: 12px;
            color: #000000;
            font-family: "Verdana" , "Arial" , "Helvetica" , "sans-serif";
        }
        .LogInBtn
        {
            border-top-width: 0px;
            border-left-width: 0px;
            border-bottom-width: 0px;
            border-right-width: 0px;
            background-image: url(Images/LogIn/b_login.gif);
            width: 40px;
            height: 20px;
        }
    </style>
</head>
<body>
    <form id="Form1" runat="server">
    <div style="height: 130px">
        &nbsp;
    </div>
    <table style="width: 100%; height: 100%;">
        <tr>
            <td align="center">
                <table cellspacing="6px" cellpadding="0px" width="533px" style="height: 319px;" background="Images/LogIn/login_bg.png"
                    border="0px">
                    <tr>
                        <td colspan="2" style="height: 100px; padding-left:20px; padding-top:6px;" align="left" valign="top">
                            <asp:Label ID="lblTitle" ForeColor="White" Font-Bold="true" runat="server">Campbell Swire 协同办公系统</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 38%;">
                            <strong><span style="font-size: 10pt">系统登录帐号/User ID：</span></strong>
                        </td>
                        <td style="width: 235px">
                            <asp:TextBox ID="UserIdCtl" runat="server" MaxLength="50" Width="180px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 28px" align="right">
                            <strong><span style="font-size: 10pt">密码/Password：</span></strong>
                        </td>
                        <td style="width: 235px; height: 28px">
                            <asp:TextBox ID="PasswordCtl" runat="server" MaxLength="50" TextMode="Password" Width="180px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="12" align="right">
                        </td>
                        <td>
                            <b>
                                <asp:Label ID="MessageCtl" ForeColor="red" meta:resourcekey="Message" runat="server"></asp:Label></b>
                        </td>
                    </tr>
                    <tr>
                        <td class="12" align="right">
                        </td>
                        <td>
                            <b>
                                <asp:Label ID="ErrorCtl" ForeColor="red" runat="server"></asp:Label></b>
                        </td>
                    </tr>
                    <tr>
                        <td class="12" align="right">
                            <strong><font color="#ffffff">&nbsp;</font></strong>
                        </td>
                        <td style="width: 235px">
                            &nbsp;
                            <asp:ImageButton ID="LogInBtn" ImageUrl="~/Images/LogIn/b_login.gif" OnClick="LogInBtn_Click"
                                runat="server" />
                            <asp:CustomValidator ID="LogInValidator" runat="server" Display="None" OnServerValidate="LogInValidator_ServerValidate"></asp:CustomValidator>
                        </td>
                        <td style="width: 172px">
                        </td>
                    </tr>
                    <tr>
                        <td class="12" align="right">
                            <strong><font color="#ffffff">&nbsp;</font></strong>
                        </td>
                        <td style="width: 235px">
                            &nbsp;
                        </td>
                        <td style="width: 172px">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
