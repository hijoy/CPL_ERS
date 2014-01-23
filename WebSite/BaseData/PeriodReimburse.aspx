<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Culture="auto" UICulture="auto" CodeFile="PeriodReimburse.aspx.cs" Inherits="BaseData_PeriodReimburse" %>

<%@ Register Src="~/UserControls/YearAndMonthUserControl.ascx" TagName="YearAndMonthUserControl"
    TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <script src="../Script/DateInput.js" type="text/javascript"></script>
    <div class="title">
        <asp:Label ID="Label_Title" runat="server" meta:resourcekey="Label_Title" /></div>
    <asp:UpdatePanel ID="upPeriodReimburse" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <gc:GridView ID="gvPeriodReimburse" CssClass="GridView" runat="server" DataSourceID="odsPeriodReimburse"
                CellPadding="0" AutoGenerateColumns="False" DataKeyNames="PeriodReimburseID"
                AllowPaging="false" AllowSorting="True" EnableModelValidation="True">
                <Columns>
                    <asp:TemplateField SortExpression="PeriodReimburse" HeaderText="<%$ Resources:Common,Form_Period %>">
                        <ItemTemplate>
                            <asp:Label ID="lblPeriodReimburse" runat="server" Text='<%# Eval("PeriodReimburse","{0:yyyy-MM}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="1178px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="DeleteButton" runat="server" CausesValidation="False" CommandName="Delete"
                                Text='<%$Resources:Common,Button_Delete %>' OnClientClick="return confirm('确定删除此行数据吗？');"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td style="width: 1178px;" class="Empty1">
                                <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Common,Form_Period %>"></asp:Label>
                            </td>
                            <td style="width: 60px;">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <SelectedRowStyle CssClass="SelectedRow" />
            </gc:GridView>
            <asp:FormView ID="fvPeriodReimburse" runat="server" DataKeyNames="PeriodReimburseD"
                DataSourceID="odsPeriodReimburse" DefaultMode="Insert" EnableModelValidation="True"
                CellPadding="0">
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr>
                            <td align="center" style="height: 22px; width: 1178px;">
                                <uc4:YearAndMonthUserControl ID="ucNewPeriod" runat="server" SelectedDate='<%# Bind("PeriodReimburse") %>'
                                    IsReadOnly="false" IsExpensePeriod="true" />
                            </td>
                            <td align="center" style="width: 60px;">
                                <asp:LinkButton ID="InsertLinkButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    Text='<%$Resources:Common,Button_Add %>'></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </InsertItemTemplate>
            </asp:FormView>
            <asp:ObjectDataSource ID="odsPeriodReimburse" runat="server" TypeName="BusinessObjects.MasterDataBLL"
                DeleteMethod="DeletePeriodReimburseById" InsertMethod="InsertPeriodReimburse"
                SelectMethod="GetPeriodReimburse" OnInserting="odsPeriodReimburse_Inserting" OnInserted="odsPeriodReimburse_Inserted"
                EnablePaging="false">
                <InsertParameters>
                    <asp:Parameter Name="PeriodReimburse" Type="String" />
                </InsertParameters>
            </asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
