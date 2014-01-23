<%@ Control Language="C#" AutoEventWireup="true" CodeFile="POSearchControl.ascx.cs"
    Inherits="UserControls_POSearchControl" %>
<nobr>
<asp:TextBox ID="txtFormNo" runat="server" OnTextChanged="txtFormNo_TextChanged"></asp:TextBox>
<asp:TextBox ID="txtDisplayFormNo" Width="100px" runat="server" ReadOnly="true"></asp:TextBox>
<input style="height:18px;<%=IsVisible %>"  type="button" value=":::" class="button_small" onclick="<%= GetShowDlgScript() %>"/>
<input type="button" style="height:18px;display:<%=IsNoClear %>;" value="Clear" class="button_small" onclick="<%= GetResetScript() %>" /></nobr>
<asp:HiddenField ID="FormNoCtl" runat="server" />
<asp:HiddenField ID="FormIDCtl" runat="server" />
