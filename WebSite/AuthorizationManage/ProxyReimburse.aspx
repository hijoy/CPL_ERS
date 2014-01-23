<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ProxyReimburse.aspx.cs" Inherits="AuthorizationManage_ProxyReimburse" Culture="Auto" UICulture="Auto"%>

<%@ Register Src="~/UserControls/StaffControl.ascx" TagName="UCStaff" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/UCDateInput.ascx" TagName="UCDateInput" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <script src="../Script/DateInput.js" type="text/javascript"></script>
    <div class="title" style="width: 1240px;">
        <asp:Label  runat="server" Text='<%$Resources:Common,Label_SearchCondition %>'/>
    </div>
    <div class="searchDiv" style="width: 1270px;">
        <table class="searchTable" cellpadding="0" cellspacing="0">
            <tr>
                <td style=" width:400px">
                    <span class="field_title">
                        <asp:Label ID="Label1" runat="server" meta:resourcekey="Label_UserName"/>
                    </span>&nbsp;&nbsp;
                    <uc1:UCStaff ID="UCUser" runat="server" Width="150px" />                    
                </td>
                <td style=" width:400px">
                    <span class="field_title">
                        <asp:Label ID="Label2" runat="server" meta:resourcekey="Label_ProxyUserName"/>
                    </span>&nbsp;&nbsp;
                    <uc1:UCStaff ID="UCProxyUser" runat="server" Width="150px" />                    
                </td>
                <td style="width: 440px; vertical-align: middle;" align="center" >
                    <asp:Button runat="server" ID="lbtnSearch" Text="<%$Resources:Common,Button_Search %>" CssClass="button_nor" OnClick="lbtnSearch_Click">
                    </asp:Button>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div class="title" style="width: 1240px;">
        <asp:Label ID="Title_Label" runat="server" meta:resourcekey="Label_Title"/></div>
    <asp:UpdatePanel ID="upProxyReimburse" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <gc:GridView ID="gvProxyReimburse" CssClass="GridView" runat="server" DataSourceID="odsProxyReimburse"
                AutoGenerateColumns="False" DataKeyNames="ID" AllowPaging="True" AllowSorting="True" PageSize="20" EnableModelValidation="True" >
                <Columns>
                    <asp:TemplateField SortExpression="UserID" meta:resourcekey="TemplateField_UserName">
                        <ItemTemplate>
                            <asp:Label ID="lblUserName" runat="server" Text='<%# GetUserNameByID(Eval("UserID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="400px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="ProxyUserID" meta:resourcekey="TemplateField_ProxyUserName">
                        <ItemTemplate>
                            <asp:Label ID="lblProxyUserName" runat="server" Text='<%# GetUserNameByID(Eval("ProxyUserID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="400px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="EndDate" meta:resourcekey="TemplateField_EndDate" >
                        <ItemTemplate>
                            <asp:Label ID="lblEndDate" runat="server" Text='<%# Eval("EndDate","{0:yyyy-MM-dd}")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="300px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="false"
                                CommandName="Delete" Text="<%$Resources:Common,Button_Delete %>"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="140px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td align="center" style="width: 400px;" class="Empty1">
                                <asp:Label ID="Label1" runat="server" meta:resourcekey="Label_UserName"/>
                            </td>
                            <td align="center" style="width: 400px;">
                                <asp:Label ID="Label2" runat="server" meta:resourcekey="Label_ProxyUserName"/>
                            </td>
                            <td align="center" style="width: 300px;">
                                <asp:Label ID="Label3" runat="server" meta:resourcekey="Label_EndDate"/>
                            </td>
                            <td align="center" style="width: 140px;">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <SelectedRowStyle CssClass="SelectedRow" />
            </gc:GridView>
            <asp:FormView ID="fvProxyReimburse" runat="server" DataKeyNames="ID" DataSourceID="odsProxyReimburse"
                DefaultMode="Insert"  CellPadding="0">
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr class="Header">
                            <td align="center" style="height: 22px; width: 400px;">
                                <uc1:UCStaff ID="NewUCUser" runat="server" Width="150px" StaffID='<%# Bind("UserID") %>' />                    
                            </td>
                            <td align="center" style="height: 22px; width: 400px;">
                                <uc1:UCStaff ID="NewUCProxyUser" runat="server" Width="150px" StaffID='<%# Bind("ProxyUserID") %>' />  
                            </td>
                            <td align="center" style="height: 22px; width: 300px;">
                                <uc2:UCDateInput ID="NewUCEndDate" runat="server" />
                            </td>
                            <td align="center" style="width: 140px;">
                                <asp:LinkButton ID="InsertLinkButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    Text="<%$Resources:Common,Button_Add %>" ValidationGroup="NewRow"></asp:LinkButton>
                            </td>
                        </tr>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="NewUCEndDate$txtDate"
                                    meta:resourcekey="RequiredFieldValidator_EndDate" Display="None" ValidationGroup="NewRow"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="NewUCUser$txtStaffName"
                                    meta:resourcekey="RequiredFieldValidator_UserName" Display="None" ValidationGroup="NewRow"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="newUCProxyUser$txtStaffName"
                                    meta:resourcekey="RequiredFieldValidator_ProxyUserName" Display="None" ValidationGroup="NewRow"></asp:RequiredFieldValidator>
                                <asp:ValidationSummary ID="vsProxyReimburseAdd" runat="server" ShowMessageBox="True" ShowSummary="False"
                                    ValidationGroup="NewRow" />
                        <tr>
                        </tr>
                    </table>
                </InsertItemTemplate>
            </asp:FormView>
            <asp:ObjectDataSource ID="odsProxyReimburse" runat="server" TypeName="BusinessObjects.AuthorizationBLL"
                SortParameterName="sortExpression" EnablePaging="true" SelectCountMethod="ProxyReimburseTotalCount"
                DeleteMethod="DeleteProxyReimburseByID" InsertMethod="InsertProxyReimburse" SelectMethod="GetProxyReimbursePaged" OnInserting="odsProxyReimburse_Inserting">
                <DeleteParameters>
                    <asp:Parameter Name="ID" Type="Int32" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="UserID" Type="Int32" />
                    <asp:Parameter Name="ProxyUserID" Type="Int32" />
                    <asp:Parameter Name="EndDate" Type="DateTime" />
                </InsertParameters>
                <SelectParameters>
                    <asp:Parameter Name="queryExpression" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />

</asp:Content>
