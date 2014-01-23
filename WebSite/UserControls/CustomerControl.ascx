<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CustomerControl.ascx.cs" Inherits="UserControls_CustomerControl" %>

<nobr>
<asp:TextBox ID="txtCustomerName" runat="server"  OnTextChanged="txtCustomerName_TextChanged"></asp:TextBox>
<asp:TextBox ID="txtDisplayCustomerName" Width="100px" runat="server"></asp:TextBox>
<input style="<%=IsVisible %>" type="button" style="height:18px;" value=":::" class="button_small" onclick="<%= GetShowDlgScript() %>"/>
<input type="button" style="height:18px;display:<%=IsNoClear %>;" value="Clear" class="button_small" onclick="<%= GetResetScript() %>" /></nobr>
<asp:HiddenField ID="CustomerNameCtl" runat="server" />
<asp:HiddenField ID="CustomerIDCtl" runat="server" />