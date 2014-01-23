<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ItemCategoryControl.ascx.cs" Inherits="UserControls_ItemCategoryControl" %>

<nobr>
<asp:TextBox ID="txtItemCategoryName" runat="server"  OnTextChanged="txtItemCategoryName_TextChanged"></asp:TextBox>
<asp:TextBox ID="txtDisplayItemCategoryName" Width="100px" runat="server"></asp:TextBox>
<input style="<%=IsVisible %>" type="button" style="height:18px;" value=":::" class="button_small" onclick="<%= GetShowDlgScript() %>"/>
<input type="button" style="height:18px;display:<%=IsNoClear %>;" value="Clear" class="button_small" onclick="<%= GetResetScript() %>" /></nobr>
<asp:HiddenField ID="ItemCategoryNameCtl" runat="server" />
<asp:HiddenField ID="ItemCategoryIDCtl" runat="server" />