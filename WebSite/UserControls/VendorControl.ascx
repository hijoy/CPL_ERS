<%@ Control Language="C#" AutoEventWireup="true" CodeFile="VendorControl.ascx.cs" Inherits="UserControls_VendorControl" %>

<nobr>
<asp:TextBox ID="txtVendorName" runat="server"  OnTextChanged="txtVendorName_TextChanged"></asp:TextBox>
<asp:TextBox ID="txtDisplayVendorName" Width="100px" runat="server"></asp:TextBox>
<input style="<%=IsVisible %>" type="button" style="height:18px;" value=":::" class="button_small" onclick="<%= GetShowDlgScript() %>"/>
<input type="button" style="height:18px;display:<%=IsNoClear %>;" value="Clear" class="button_small" onclick="<%= GetResetScript() %>" /></nobr>
<asp:HiddenField ID="VendorNameCtl" runat="server" />
<asp:HiddenField ID="VendorIDCtl" runat="server" />