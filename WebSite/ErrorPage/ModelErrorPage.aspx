<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ModelErrorPage.aspx.cs" Inherits="ErrorPage_NoRightErrorPage"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="text-align: center; margin-top: 40px; color: Red; font-weight: bold;">
        <asp:Label runat="server" meta:resourcekey="Label_Info1"/></div>
    <div style="height: 500px;">
        <b><asp:Label runat="server" meta:resourcekey="Label_Info2"/></b><br />
        <asp:Label ID="lblError" runat="server" Width="800px" Height="500px"></asp:Label>
    </div>
</asp:Content>
