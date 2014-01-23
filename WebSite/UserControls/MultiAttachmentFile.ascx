<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MultiAttachmentFile.ascx.cs"
    Inherits="UserControls_MultiAttachmentFile" %>
<table cellpadding="0" cellspacing="0">
    <tr>
        <td style="display: inline;" valign="top">
            <asp:FileUpload ID="fileUpload" runat="server" BackColor="#e5effb" />
            <asp:Button ID="btAddFile" runat="server" meta:resourcekey="btAddFile" CssClass="button_upload" OnClick="btAddFile_Click" />
        </td>
    </tr>
    <tr>
        <td>
            <gc:GridView ID="gvMultiAttachmentFile" runat="server" AutoGenerateColumns="False"
                CssClass="GridView" AllowPaging="false" ShowFooter="false" Width="100%" DataKeyNames="AttachmentFileName"
                OnRowDataBound="gvMultiAttachmentFile_RowDataBound" BorderWidth="0px" OnRowDeleting="gvMultiAttachmentFile_RowDeleting"
                ShowHeader="false">
                <Columns>
                    <asp:TemplateField HeaderText="RealName" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblRealFileName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "RealAttachmentFileName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemStyle Width="90%" BorderWidth="0" />
                        <ItemTemplate>
                            <a href='<%# DataBinder.Eval(Container.DataItem,"DownloadUrl")%>'>
                                <%# DataBinder.Eval(Container.DataItem, "RealAttachmentFileName")%>
                            </a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemStyle HorizontalAlign="Center" BorderWidth="0" />
                        <ItemTemplate>
                            <asp:ImageButton ID="imgBtnDelete" runat="server" CausesValidation="false" CommandName="Delete"
                               Height="20px"  ImageUrl="~/Images/pic2.png" OnClientClick="return confirm('确定删除此行文件吗？');" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
            </gc:GridView>
        </td>
    </tr>
    <asp:HiddenField ID="FUNameCtl" runat="server" />
    <asp:HiddenField ID="NewFUNameCtl" runat="server" />
    <asp:HiddenField ID="DeleteFUNameCtl" runat="server" />
    <asp:HiddenField ID="OldFUNameCtl" runat="server" />
    <asp:HiddenField ID="NewUploadFUNameCtl" runat="server" />
    <asp:HiddenField ID="RealFUNameCtl" runat="server" />
    <asp:HiddenField ID="NewRealFUNameCtl" runat="server" />
    <asp:HiddenField ID="DeleteRealFUNameCtl" runat="server" />
    <asp:HiddenField ID="OldRealFUNameCtl" runat="server" />
    <asp:HiddenField ID="NewUploadRealFUNameCtl" runat="server" />
</table>
