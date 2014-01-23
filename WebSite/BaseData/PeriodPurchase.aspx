<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Culture="auto" UICulture="auto" CodeFile="PeriodPurchase.aspx.cs" Inherits="BaseData_PeriodPurchase" %>

<%@ Register Src="~/UserControls/YearAndMonthUserControl.ascx" TagName="YearAndMonthUserControl"
    TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <script src="../Script/DateInput.js" type="text/javascript"></script>
    <div class="title">
        <asp:Label ID="Label1" runat="server" meta:resourcekey="Label_Title" /></div>
    <asp:UpdatePanel ID="upPeriodPurchase" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <gc:GridView ID="gvPeriodPurchase" CssClass="GridView" runat="server" DataSourceID="odsPeriodPurchase"
                CellPadding="0" AutoGenerateColumns="False" DataKeyNames="PeriodPurchaseID" AllowPaging="false"
                AllowSorting="True" EnableModelValidation="True">
                <Columns>
                    <asp:TemplateField SortExpression="PeriodPurchase" HeaderText="<%$ Resources:Common,Form_Period %>">
                        <ItemTemplate>
                            <asp:Label ID="lblPeriodPurchase" runat="server" Text='<%# Eval("PeriodPurchase","{0:yyyy-MM}") %>'></asp:Label>
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
            <asp:FormView ID="fvPeriodPurchase" runat="server" DataKeyNames="PeriodPurchaseD"
                DataSourceID="odsPeriodPurchase" DefaultMode="Insert" EnableModelValidation="True" CellPadding="0">
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr>
                            <td align="center" style="height: 22px; width: 1178px;">
                                <uc4:YearAndMonthUserControl ID="ucNewPeriod" runat="server" SelectedDate='<%# Bind("PeriodPurchase") %>'
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
            <asp:ObjectDataSource ID="odsPeriodPurchase" runat="server" TypeName="BusinessObjects.MasterDataBLL"
                DeleteMethod="DeletePeriodPurchaseById" InsertMethod="InsertPeriodPurchase" OnInserting="odsPeriodPurchase_Inserting"
                SelectMethod="GetPeriodPurchase" EnablePaging="false" OnInserted="odsPeriodPurchase_Inserted">
                <InsertParameters>
                    <asp:Parameter Name="PeriodPurchase" Type="String" />
                </InsertParameters>
            </asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
