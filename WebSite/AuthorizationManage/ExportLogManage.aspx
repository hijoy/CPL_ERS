<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="ExportLogManage.aspx.cs" Inherits="AuthorizationManage_ExportLogManage" Culture="Auto"
    UICulture="Auto" %>

<%@ Register Src="~/UserControls/UCDateInput.ascx" TagName="UCDateInput" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/OUSelect.ascx" TagName="OUSelect" TagPrefix="uc2" %>
<%@ Register Src="~/UserControls/VendorControl.ascx" TagName="UCVendor" TagPrefix="uc3" %>
<%@ Register Src="~/UserControls/YearAndMonthUserControl.ascx" TagName="YearAndMonthUserControl"
    TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" src="../Script/js.js" type="text/javascript"></script>
    <script language="javascript" src="../Script/DateInput.js" type="text/javascript"></script>
    <div class="title">
        <asp:Label ID="titleLabel" runat="server" Text="搜索条件"></asp:Label>
    </div>
    <div class="searchDiv">
        <table class="searchTable" cellpadding="0" cellspacing="0" width="1240px">
            <tr>
                <td>
                    <asp:Label ID="lblStartDate" runat="server" Text="开始时间"></asp:Label>
                </td>
                <td>
                    <uc1:UCDateInput ID="UCStartDate" runat="server" IsReadOnly="false" />
                </td>
                <td>
                    <asp:Label ID="lblEndate" runat="server" Text="结束时间"></asp:Label>
                </td>
                <td>
                    <uc1:UCDateInput ID="ucEnddate" runat="server" IsReadOnly="false" />
                </td>
                <td>
                    导出类型
                </td>
                <td>
                    <asp:DropDownList ID="ddltype" runat="server">
                        <asp:ListItem Value="">All</asp:ListItem>
                        <asp:ListItem Value="1">个人费用报销</asp:ListItem>
                        <asp:ListItem Value="2">差旅费报销</asp:ListItem>
                        <asp:ListItem Value="3">PV</asp:ListItem>
                        <asp:ListItem Value="4">SALE</asp:ListItem>
                        <asp:ListItem Value="5">Marketing</asp:ListItem>
                        <asp:ListItem Value="6">R&D</asp:ListItem>
                        <asp:ListItem Value="8">Vendor</asp:ListItem>
                        <asp:ListItem Value="9">PO</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td valign="middle">
                    <asp:Button ID="btnSearch" runat="server" CssClass="button_nor" Text="<%$Resources:Common,Button_Search %>"
                        OnClick="btnSearch_Click" />&nbsp;
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div class="title">
        <asp:Label ID="Label_Title" runat="server" meta:resourcekey="titleLabel" />
    </div>
    <asp:UpdatePanel runat="server" ID="upsList">
        <ContentTemplate>
    <gc:GridView CssClass="GridView" ID="gvApplyList" runat="server" DataSourceID="odsApplyList"
        AutoGenerateColumns="False" DataKeyNames="LogID" AllowPaging="True" AllowSorting="True"
        PageSize="20" Width="100%" OnRowDataBound="gvApplyList_RowDataBound">
        <Columns>
            <asp:TemplateField Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblLogID" runat="server" Text='<%# Bind("LogID") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="导出数据类型" SortExpression="ExportType">
                <ItemTemplate>
                    <asp:Label ID="lblExportType" runat="server" Text='<%# Bind("ExportType") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="导出文件名称" SortExpression="FileName">
                <ItemTemplate>
                    <asp:Label ID="lblFileName" runat="server" Text='<%# Bind("FileName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="导出时间" SortExpression="ExportDate">
                <ItemTemplate>
                    <asp:Label ID="lblImportDate" runat="server" Text='<%# Bind("ExportDate") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="导出总数" SortExpression="TotalCount">
                <ItemTemplate>
                    <asp:Label ID="lblTotalCount" runat="server" Text='<%# Bind("TotalCount") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="导出成功数" SortExpression="SuccessCount">
                <ItemTemplate>
                    <asp:Label ID="lblSuccessCount" runat="server" Text='<%# Bind("SuccessCount") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="导出失败数" SortExpression="FailCount">
                <ItemTemplate>
                    <asp:Label ID="lblFailCount" runat="server" Text='<%# Bind("FailCount") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="lblErrorDetail" runat="server" Text="错误明细" OnClick="lblErrorDetail_Click"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
        <HeaderStyle CssClass="Header" />
        <EmptyDataTemplate>
            <table width="100%">
                <tr class="Header">
                    <td>
                        <asp:Label runat="server" Text="导出数据类型"></asp:Label>
                    </td>
                    <td>
                        <asp:Label runat="server" Text="导出文件名称"></asp:Label>
                    </td>
                    <td>
                        <asp:Label runat="server" Text="导出时间"></asp:Label>
                    </td>
                    <td>
                        <asp:Label runat="server" Text="导出总数"></asp:Label>
                    </td>
                    <td>
                        <asp:Label runat="server" Text="导出成功数"></asp:Label>
                    </td>
                    <td>
                        <asp:Label runat="server" Text="导出失败数"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="13" class="Empty2 noneLabel">
                        <asp:Label ID="Label_None" runat="server" Text="<%$Resources:Common,Label_None %>" />
                    </td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <SelectedRowStyle CssClass="SelectedRow" />
    </gc:GridView>
    <asp:ObjectDataSource ID="odsApplyList" runat="server" TypeName="BusinessObjects.LogBLL"
        SelectMethod="GetPagedFromExport" EnablePaging="True" SelectCountMethod="GetExportDataCount"
        SortParameterName="sortExpression">
        <SelectParameters>
            <asp:Parameter Name="queryExpression" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <br />
    <div class="title">
        <asp:Label ID="lblFailTitle" runat="server" Text="错误明细" />
    </div>
    <gc:GridView CssClass="GridView" ID="gvFaillist" runat="server" DataSourceID="odsFailList"
        AutoGenerateColumns="False" DataKeyNames="LogDetailID" AllowPaging="True" AllowSorting="True"
        PageSize="20" Width="100%" Visible="false">
        <Columns>
            <asp:TemplateField Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblLogID" runat="server" Text='<%# Bind("LogDetailID") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="错误信息">
                <ItemTemplate>
                    <asp:Label ID="lblError" runat="server" Text='<%# Bind("Error") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
        <HeaderStyle CssClass="Header" />
        <SelectedRowStyle CssClass="SelectedRow" />
    </gc:GridView>
    <asp:ObjectDataSource ID="odsFailList" runat="server" TypeName="BusinessObjects.LogBLL"
        SelectMethod="GetExportLogDetailByLogId">
        <SelectParameters>
            <asp:Parameter Name="logId" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <br />
     </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
