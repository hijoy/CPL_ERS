<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="SpecialSystemPage.aspx.cs" Inherits="BaseData_SpecialSystemPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="title">
        <asp:Label ID="lblTitle" runat="server" Text="手动生成结案单"></asp:Label>
    </div>
    <div class="searchDiv">
        <table class="searchTable">
            <tr>
                <td style="width: 200px;">
                    <div class="field_title">
                        <asp:Label ID="lblFormNo" runat="server" Text="申请单号" />
                    </div>
                    <asp:TextBox runat="server" ID="txtFormNo" Width="160px"></asp:TextBox>
                </td>
                <td valign="bottom" style="width: 200px;">
                    <asp:Button ID="btnGenearate" runat="server" CssClass="button_nor" Text="生成" OnClick="btnGenearate_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblError" ForeColor="Red" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
