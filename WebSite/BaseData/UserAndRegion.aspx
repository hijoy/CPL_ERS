<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Culture="auto" UICulture="auto" 
    CodeFile="UserAndRegion.aspx.cs" Inherits="Base_UserAndRegion" %>

<%@ Register Src="~/UserControls/StaffControl.ascx" TagName="UCStaff" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/UCDateInput.ascx" TagName="UCDateInput" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <script src="../Script/DateInput.js" type="text/javascript"></script>
    <div class="title" style="width: 1240px;">
        <asp:Label runat="server" Text='<%$Resources:Common,Label_SearchCondition %>' />
    </div>
    <div class="searchDiv">
        <table class="searchTable" cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 400px">
                    <span class="field_title">
                        <asp:Label ID="Label1" runat="server" meta:resourcekey="Label_UserName" />
                    </span>&nbsp;&nbsp;
                    <asp:TextBox ID="txtUserName" runat="server" Width="170px"></asp:TextBox>
                </td>
                <td style="width: 400px">
                    <span class="field_title">
                        <asp:Label ID="Label2" runat="server" meta:resourcekey="Label_CustomerRegion" />
                    </span>&nbsp;&nbsp;
                    <asp:DropDownList ID="ddlCustomerRegion" runat="server" DataTextField="CustomerRegionName"
                        DataValueField="CustomerRegionID" DataSourceID="odsCustomerRegion" Width="280px">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="odsCustomerRegion" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                        SelectCommand="select 0 CustomerRegionID, 'ÇëÑ¡Ôñ' CustomerRegionName union SELECT [CustomerRegionID], [CustomerRegionName] FROM [CustomerRegion]">
                    </asp:SqlDataSource>
                </td>
                <td style="width: 440px; vertical-align: middle;" align="center">
                    <asp:Button runat="server" ID="lbtnSearch" Text="<%$Resources:Common,Button_Search %>"
                        CssClass="button_nor" OnClick="lbtnSearch_Click"></asp:Button>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div class="title" style="width: 1240px;">
        <asp:Label ID="Title_Label" runat="server" meta:resourcekey="Label_Title" /></div>
    <asp:UpdatePanel ID="upUserAndRegion" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <gc:GridView ID="gvUserAndRegion" CssClass="GridView" runat="server" DataSourceID="odsUserAndRegion"
                AutoGenerateColumns="False" DataKeyNames="UserAndRegionID" AllowPaging="True" AllowSorting="True"
                PageSize="20" EnableModelValidation="True">
                <Columns>
                    <asp:TemplateField SortExpression="StuffUserID" meta:resourcekey="TemplateField_UserName">
                        <ItemTemplate>
                            <asp:Label ID="lblUserName" runat="server" Text='<%# GetUserNameByID(Eval("StuffUserID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="400px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="CustomerRegionID" meta:resourcekey="TemplateField_CustomerRegionName">
                        <ItemTemplate>
                            <asp:Label ID="lblCustomerRegionName" runat="server" Text='<%# GetCustomerRegionNameByID(Eval("CustomerRegionID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="300px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="Description" meta:resourcekey="TemplateField_Description">
                        <ItemTemplate>
                            <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="436px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="false" CommandName="Delete"
                                Text="<%$Resources:Common,Button_Delete %>"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td align="center" style="width: 400px;" class="Empty1">
                                <asp:Label ID="Label1" runat="server" meta:resourcekey="Label_UserName" />
                            </td>
                            <td align="center" style="width: 300px;">
                                <asp:Label ID="Label2" runat="server" meta:resourcekey="Label_CustomerRegion" />
                            </td>
                            <td align="center" style="width: 436px;">
                                <asp:Label ID="Label3" runat="server" meta:resourcekey="Label_Description" />
                            </td>
                            <td align="center" style="width: 100px;">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <SelectedRowStyle CssClass="SelectedRow" />
            </gc:GridView>
            <asp:FormView ID="fvUserAndRegion" runat="server" DataKeyNames="ID" DataSourceID="odsUserAndRegion"
                DefaultMode="Insert" CellPadding="0">
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr class="Header">
                            <td align="center" style="height: 22px; width: 400px;">
                                <uc1:UCStaff ID="NewUCUser" runat="server" Width="220px" StaffID='<%# Bind("StuffUserID") %>' />
                            </td>
                            <td align="center" style="height: 22px; width: 300px;">
                                <asp:DropDownList ID="ddlNewCustomerRegion" runat="server" DataTextField="CustomerRegionName"
                                    DataValueField="CustomerRegionID" DataSourceID="odsNewCustomerRegion" Width="280px"
                                    SelectedValue='<%#Bind("CustomerRegionID") %>'>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="odsNewCustomerRegion" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                    SelectCommand=" SELECT [CustomerRegionID], [CustomerRegionName] FROM [CustomerRegion]">
                                </asp:SqlDataSource>
                            </td>
                            <td align="center" style="height: 22px; width: 436px;">
                                <asp:TextBox ID="txtDescription" runat="server" Text='<%#Bind("Description") %>'
                                    Width="380px"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 100px;">
                                <asp:LinkButton ID="InsertLinkButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    Text="<%$Resources:Common,Button_Add %>" ValidationGroup="NewRow"></asp:LinkButton>
                            </td>
                        </tr>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="NewUCUser$txtStaffName"
                            meta:resourcekey="RequiredFieldValidator_UserName" Display="None" ValidationGroup="NewRow"></asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlNewCustomerRegion"
                            meta:resourcekey="RequiredFieldValidator_ProxyUserName" Display="None" ValidationGroup="NewRow"></asp:RequiredFieldValidator>
                        <asp:ValidationSummary ID="vsProxyReimburseAdd" runat="server" ShowMessageBox="True"
                            ShowSummary="False" ValidationGroup="NewRow" />
                        <tr>
                        </tr>
                    </table>
                </InsertItemTemplate>
            </asp:FormView>
            <asp:ObjectDataSource ID="odsUserAndRegion" runat="server" TypeName="BusinessObjects.MasterDataBLL"
                SortParameterName="sortExpression" EnablePaging="true" SelectCountMethod="UserAndRegionTotalCount"
                DeleteMethod="DeleteUserAndRegionByID" InsertMethod="InsertUserAndRegion" SelectMethod="GetPagedUserAndRegion"
                OnInserting="odsUserAndRegion_Inserting">
                <SelectParameters>
                    <asp:Parameter Name="queryExpression" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
</asp:Content>
