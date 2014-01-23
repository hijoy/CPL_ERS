<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SKUControl.ascx.cs" Inherits="UserControls_SKUControl" %>

<nobr>
<asp:TextBox ID="txtSKUName" runat="server"  OnTextChanged="txtSKUName_TextChanged"></asp:TextBox>
<asp:TextBox ID="txtDisplaySKUName" Width="100px" runat="server"></asp:TextBox>
<input style="<%=IsVisible %>" type="button" style="height:18px;" value=":::" class="button_small" onclick="<%= GetShowDlgScript() %>"/>
<input type="button" style="height:18px;display:<%=IsNoClear %>;" value="Clear" class="button_small" onclick="<%= GetResetScript() %>" /></nobr>
<asp:HiddenField ID="SKUNameCtl" runat="server" />
<asp:HiddenField ID="SKUIDCtl" runat="server" />