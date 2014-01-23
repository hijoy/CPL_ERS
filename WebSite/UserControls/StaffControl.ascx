<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StaffControl.ascx.cs" Inherits="UserControls_StaffControl" %>
<nobr>
<asp:TextBox ID="txtStaffName" runat="server"  OnTextChanged="txtStaffName_TextChanged"></asp:TextBox>
<asp:TextBox ID="txtDisplayStaffName" Width="100px" runat="server"></asp:TextBox>
<input style="<%=IsVisible %>" type="button" style="height:18px;" value=":::" class="button_small" onclick="<%= GetShowDlgScript() %>"/>
<input type="button" style="height:18px;display:<%=IsNoClear %>;" value="Clear" class="button_small" onclick="<%= GetResetScript() %>" /></nobr>
<asp:HiddenField ID="StaffNameCtl" runat="server" />
<asp:HiddenField ID="StaffIDCtl" runat="server" />