<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="FormPaymentList.aspx.cs" Culture="Auto" UICulture="Auto" Inherits="FormQuery_FormPaymentList" %>

<%@ Register Src="~/UserControls/UCDateInput.ascx" TagName="UCDateInput" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/OUSelect.ascx" TagName="OUSelect" TagPrefix="uc2" %>
<%@ Register Src="~/UserControls/YearAndMonthUserControl.ascx" TagName="YearAndMonthUserControl"
    TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <script src="../Script/DateInput.js" type="text/javascript"></script>
    <div class="title">
        <asp:Label ID="Label_SearchCondition" runat="server" Text="<%$Resources:Common,Label_SearchCondition %>" /></div>
    <div class="searchDiv">
        <table class="searchTable" cellpadding="0" cellspacing="0" width="1240px">
            <tr style="vertical-align: top; height: 40px">
                <td width="160px">
                    <div class="field_title">
                        <asp:Label ID="Form_FormNo" runat="server" Text="<%$Resources:Common,Form_FormNo %>" /></div>
                    <asp:TextBox ID="txtFormNo" MaxLength="20" runat="server" Width="120px"></asp:TextBox>
                </td>
                <td width="160px">
                    <div class="field_title">
                        <asp:Label ID="Form_ApplyUser" runat="server" Text="<%$Resources:Common,Form_ApplyUser %>" /></div>
                    <asp:TextBox ID="txtStuffUser" MaxLength="50" runat="server" Width="120px"></asp:TextBox>
                </td>
                <td width="320px">
                    <div class="field_title">
                        <asp:Label ID="Form_SubmitDate" runat="server" Text="<%$Resources:Common,Form_SubmitDate %>" /></div>
                    <nobr>   
                    <uc1:UCDateInput ID="UCDateInputBeginDate" runat="server" IsReadOnly="false" />
                    <asp:Label ID="lbSign" runat="server">~~</asp:Label>
                    <uc1:UCDateInput ID="UCDateInputEndDate" runat="server" IsReadOnly="false" />
                    </nobr>
                </td>
                <td width="320px">
                    <div class="field_title">
                        <asp:Label ID="Form_CreateVoucherDate" runat="server" Text="<%$Resources:Common,Form_CreateVoucherDate %>" /></div>
                    <nobr>   
                    <uc1:UCDateInput ID="UCDateInputBeginCreateVoucherDate" runat="server" IsReadOnly="false" />
                    <asp:Label ID="Label12" runat="server">~~</asp:Label>
                    <uc1:UCDateInput ID="UCDateInputEndCreateVoucherDate" runat="server" IsReadOnly="false" />
                    </nobr>
                </td>
                <td align="center">
                    <div class="field_title">
                        <asp:Label ID="Form_FormType" runat="server" Text="<%$Resources:Common,Form_FormType %>" /></div>
                    <asp:DropDownList ID="ddlFormType" runat="server" Width="120px">
                        <asp:ListItem Value="0">全部</asp:ListItem>
                        <asp:ListItem Value="1">差旅费报销</asp:ListItem>
                        <asp:ListItem Value="4">个人费用报销</asp:ListItem>
                        <asp:ListItem Value="14">销售部付款申请</asp:ListItem>
                        <asp:ListItem Value="13">销售部预付款</asp:ListItem>
                        <asp:ListItem Value="24">PV申请</asp:ListItem>
                        <asp:ListItem Value="32">R&D方案报销</asp:ListItem>
                        <asp:ListItem Value="42">市场部方案报销</asp:ListItem>
                        <asp:ListItem Value="23">PO</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </div>
    <table width="1200px">
        <tr>
            <td align="right" valign="middle" colspan="6">
                <asp:Button ID="btnSearch" runat="server" CssClass="button_nor" Text="<%$Resources:Common,Button_Search %>"
                    OnClick="btnSearch_Click" />
            </td>
        </tr>
    </table>
    <br />
    <div class="title">
        <asp:Label ID="Label_Title" runat="server" meta:resourcekey="Label_Title" /></div>
    <asp:UpdatePanel ID="uplist" runat="server">
        <ContentTemplate>
            <table width="1200px">
                <tr>
                    <td align="right">
                        <asp:Button ID="btnUnLock" runat="server" CssClass="button_nor" Text="解锁" OnClick="btnUnLock_Click" />
                    </td>
                </tr>
            </table>
            <gc:GridView CssClass="GridView" ID="gvApplyList" runat="server" DataSourceID="odsApplyList"
                AutoGenerateColumns="False" DataKeyNames="FormID" AllowPaging="True" AllowSorting="True"
                PageSize="20" OnRowDataBound="gvApplyList_RowDataBound">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="cbIsUnLock" runat="server" />
                        </ItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lblFormApplyID" runat="server" Text='<%# Bind("FormID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="FormNo" HeaderText="<%$Resources:Common,Form_FormNo %>">
                        <ItemTemplate>
                            <asp:Label ID="lbtnFormNo" runat="server" Text='<%# Bind("FormNo") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="150px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Common,Form_FormType %>" SortExpression="FormTypeName">
                        <ItemTemplate>
                            <asp:Label ID="lblFormTypeName" runat="server" Text='<%# Eval("FormTypeName") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="240px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Common,Form_ApplyUser %>" SortExpression="UserID">
                        <ItemTemplate>
                            <asp:Label ID="lblStuffName" runat="server" Text='<%# GetStaffNameByID(Eval("UserID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="SubmitDate" HeaderText="<%$Resources:Common,Form_SubmitDate %>">
                        <ItemTemplate>
                            <asp:Label ID="lblSubmitDate" runat="server" Text='<%# Bind("SubmitDate", "{0:yyyy-MM-dd}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="150px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="IsCreateVoucher" HeaderText="<%$Resources:Common,Form_IsCreateVoucher %>">
                        <ItemTemplate>
                            <asp:Label ID="lblIsCreateVoucher" runat="server" Text='<%# Bind("IsCreateVoucher") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="150px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="CreateVoucherDate" HeaderText="<%$Resources:Common,Form_CreateVoucherDate %>">
                        <ItemTemplate>
                            <asp:Label ID="lblCreateVoucherDate" runat="server" Text='<%# Bind("CreateVoucherDate", "{0:yyyy-MM-dd}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="150px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="IsExportLock" HeaderText="<%$Resources:Common,Form_IsExportLock %>">
                        <ItemTemplate>
                            <asp:Label ID="lblIsExportLock" runat="server" Text='<%# Bind("IsExportLock")  %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="150px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td style="width: 150px;">
                                <asp:Label ID="Label1" runat="server" Text="<%$Resources:Common,Form_FormNo %>" />
                            </td>
                            <td style="width: 240px;">
                                <asp:Label ID="Label2" runat="server" Text="<%$Resources:Common,Form_FormType %>" />
                            </td>
                            <td style="width: 100px;">
                                <asp:Label ID="Label3" runat="server" Text="<%$Resources:Common,Form_ApplyUser %>" />
                            </td>
                            <td style="width: 150px;">
                                <asp:Label ID="Label4" runat="server" Text="<%$Resources:Common,Form_SubmitDate %>" />
                            </td>
                            <td style="width: 150px;">
                                <asp:Label ID="Label5" runat="server" Text="<%$Resources:Common,Form_IsCreateVoucher %>" />
                            </td>
                            <td style="width: 150px;">
                                <asp:Label ID="Label6" runat="server" Text="<%$Resources:Common,Form_CreateVoucherDate %>" />
                            </td>
                            <td style="width: 150px;">
                                <asp:Label ID="Label7" runat="server" Text="<%$Resources:Common,Form_IsExportLock %>" />
                            </td>
                            <td style="width: 150px;">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="9" class="Empty2 noneLabel">
                                <asp:Label ID="Label_None" runat="server" Text="<%$Resources:Common,Label_None %>" />
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <SelectedRowStyle CssClass="SelectedRow" />
            </gc:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="odsApplyList" runat="server" TypeName="BusinessObjects.FormQueryBLL"
        SelectMethod="GetPagedFormView" EnablePaging="True" SelectCountMethod="QueryFormViewCount"
        SortParameterName="sortExpression" UpdateMethod="UpdateFormbyID">
        <SelectParameters>
            <asp:Parameter Name="queryExpression" Type="String" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="FormID" Type="Int32" />
            <asp:Parameter Name="IsExportLock" Type="Boolean" />
        </UpdateParameters>
    </asp:ObjectDataSource>
    <br />
    <br />
</asp:Content>
