<%@ Control Language="C#" AutoEventWireup="true" CodeFile="VendorTypeControl.ascx.cs"
    Inherits="UserControls_VendorTypeControl" %>
<nobr>
<asp:TextBox ID="txtVendorTypeName" runat="server" OnTextChanged="txtVendorTypeName_TextChanged"></asp:TextBox>
<asp:TextBox ID="txtDisplayVendorTypeName" Width="100px" runat="server" ReadOnly="true"></asp:TextBox>
<input type="button" style="height:18px;display:<%=IsVisible%>" value=":::" class="button_small" onclick="<%= GetShowDlgScript() %>"/>
<input type="button" style="height:18px;display:<%=IsNoClear%>;" value="Clear" class="button_small" onclick="<%= GetResetScript() %>" /></nobr>
<asp:HiddenField ID="VendorTypeNameCtl" runat="server" />
<asp:HiddenField ID="VendorTypeIDCtl" runat="server" />
