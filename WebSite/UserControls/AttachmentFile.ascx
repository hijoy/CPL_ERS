<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AttachmentFile.ascx.cs" Inherits="UserControls_AttachmentFile" %>
<asp:FileUpload ID="fileUpload" runat="server"/><asp:HyperLink Runat="server" ID="lkDownload"></asp:HyperLink>
<asp:Button Runat="server" ID="btnDelete" Text="<%$Resources:Common,Button_Delete %>" OnClick="btnDelete_Click"></asp:Button>
<asp:Button Runat="server" ID="btnCancel" Text="<%$Resources:Common,Button_Cancel %>" OnClick="btnCancel_Click"></asp:Button>
<asp:HiddenField ID="FUNameCtl" runat="server" />
<asp:HiddenField ID="FUSizeCtl" runat="server" />
<asp:HiddenField ID="OldFUNameCtl" runat="server" />
<asp:HiddenField ID="OldFUSizeCtl" runat="server" />
