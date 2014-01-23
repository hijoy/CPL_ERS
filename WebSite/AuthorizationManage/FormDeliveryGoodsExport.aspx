<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="FormDeliveryGoodsExport.aspx.cs" Inherits="AuthorizationManage_FormDeliveryGoodsExport" Culture="Auto"
    UICulture="Auto" %>

<%@ Register Src="~/UserControls/UCDateInput.ascx" TagName="UCDateInput" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/OUSelect.ascx" TagName="OUSelect" TagPrefix="uc2" %>
<%@ Register Src="~/UserControls/VendorControl.ascx" TagName="UCVendor" TagPrefix="uc3" %>
<%@ Register Src="~/UserControls/YearAndMonthUserControl.ascx" TagName="YearAndMonthUserControl"
    TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <script src="../Script/DateInput.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function checkbtnenable() {
            var fileUpLoad = document.getElementById("<%= fileUpLoad.ClientID %>");
            if (fileUpLoad.value == "" || fileUpLoad.value.indexOf('xls') =='-1') {
                return false;
            }
            if (document.all("btnclicked").value == "0") {
                document.all("btnclicked").value = "1";
                alert("此操作将持续一段时候,请不要关闭页面,请耐心等候!");
            }
            else {
                document.all("ctl00_ContentPlaceHolder1_btnImport").disabled = true;
                alert("系统正在处理上传文件，请稍候。");
            }
        }

    </script>
    <div class="title">
        <asp:Label ID="Label7" runat="server" Text="导入"></asp:Label>
    </div>
    <table width="1240px">
        <tr>
            <td align="left" valign="middle">
                <input type="hidden" id="btnclicked" name="btnclicked" value="0" />
                <input id="fileUpLoad" runat="server" type="file" style="width: 500px; background-color: rgb(229, 239, 251);" />&nbsp;&nbsp;<asp:Button
                    ID="btnImport" CssClass="button_nor" runat="server" Text="导入" OnClientClick="checkbtnenable()"
                    OnClick="btnImport_Click" />
            </td>
        </tr>
    </table>
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
                    <nobr> 
                    <uc1:UCDateInput ID="UCStartDate" runat="server" IsReadOnly="false" />
                    </nobr>
                </td>
                <td>
                    <asp:Label ID="lblEndate" runat="server" Text="结束时间"></asp:Label>
                </td>
                <td>
                    <uc1:UCDateInput ID="ucEnddate" runat="server" IsReadOnly="false" />
                </td>
                <td>
                   操作人
                </td>
                <td>
                   <asp:TextBox ID="txtUser" runat="server"></asp:TextBox>
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
            <gc:GridView CssClass="GridView" ID="gvlogList" runat="server" DataSourceID="odsApplyList"
                AutoGenerateColumns="False" DataKeyNames="LogID" AllowPaging="True" AllowSorting="True"
                PageSize="20" Width="100%">
                <Columns>
                    <asp:TemplateField Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblLogID" runat="server" Text='<%# Bind("LogID") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="导入文件名称" SortExpression="FileName">
                        <ItemTemplate>
                            <asp:Label ID="lblFileName" runat="server" Text='<%# Bind("FileName") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="导入时间" SortExpression="ExportDate">
                        <ItemTemplate>
                            <asp:Label ID="lblImportDate" runat="server" Text='<%# Bind("ImportDate") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="操作人" SortExpression="ImportUserID">
                        <ItemTemplate>
                            <asp:Label ID="lblStuffUser" runat="server" Text='<%# GetStaffNameByID( Eval("ImportUserID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="导入总数" SortExpression="TotalCount">
                        <ItemTemplate>
                            <asp:Label ID="lblTotalCount" runat="server" Text='<%# Bind("TotalCount") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="导入成功数" SortExpression="SuccessCount">
                        <ItemTemplate>
                            <asp:Label ID="lblSuccessCount" runat="server" Text='<%# Bind("SuccessCount") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="导入失败数" SortExpression="FailCount">
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
                                <asp:Label ID="Label2" runat="server" Text="导入文件名称"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="导入时间"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="操作人"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label4" runat="server" Text="导入总数"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label5" runat="server" Text="导入成功数"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label6" runat="server" Text="导入失败数"></asp:Label>
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
                SelectMethod="GetPagedFromImport" EnablePaging="True" SelectCountMethod="GetImportDataCount"
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
                    <asp:TemplateField HeaderText="错误行数">
                        <ItemTemplate>
                            <asp:Label ID="lblline" runat="server" Text='<%# Bind("line") %>'></asp:Label>
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
                SelectMethod="GetLogDetailByLogId">
                <SelectParameters>
                    <asp:Parameter Name="logId" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
