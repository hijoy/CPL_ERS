<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ItemControl.ascx.cs" Inherits="UserControls_ItemControl" %>

<nobr>
<asp:TextBox ID="txtItemName" runat="server"  OnTextChanged="txtItemName_TextChanged"></asp:TextBox>
<asp:TextBox ID="txtDisplayItemName" Width="100px" runat="server"></asp:TextBox>
<input style="<%=IsVisible %>" type="button" style="height:18px;" value=":::" class="button_small" onclick="<%= GetShowDlgScript() %>"/>
<input type="button" style="height:18px;display:<%=IsNoClear %>;" value="Clear" class="button_small" onclick="<%= GetResetScript() %>" /></nobr>
<asp:HiddenField ID="ItemNameCtl" runat="server" />
<asp:HiddenField ID="ItemIDCtl" runat="server" />