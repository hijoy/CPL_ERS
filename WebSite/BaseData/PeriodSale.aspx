<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="PeriodSale.aspx.cs" Culture="auto" UICulture="auto" Inherits="BaseData_PeriodSale" %>

<%@ Register Src="../UserControls/YearAndMonthUserControl.ascx" TagName="UCDateInput"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <script src="../Script/DateInput.js" type="text/javascript"></script>
    <div class="title">
        <asp:Label ID="Label5" runat="server" meta:resourcekey="Label_Title"></asp:Label></div>
    <asp:UpdatePanel ID="PeriodSaleUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <gc:GridView ID="PeriodSaleGridView" CssClass="GridView" runat="server" DataSourceID="odsPeriodSale"
                AutoGenerateColumns="False" DataKeyNames="PeriodSaleID" CellPadding="0">
                <Columns>
                    <asp:TemplateField HeaderText="<%$Resources:Common,Form_Period %>" SortExpression="PeriodSale">
                        <EditItemTemplate>
                            <uc1:UCDateInput ID="UCPeriodSale" runat="server" IsExpensePeriod="true" SelectedDate='<%#Bind("PeriodSale")%>' />
                            <asp:RequiredFieldValidator ID="RFPeriodSale" runat="server" ControlToValidate="UCPeriodSale$txtDate"
                                Display="None" meta:resourcekey="RequiredFieldValidator_Period" SetFocusOnError="True" ValidationGroup="PeriodSaleEdit"></asp:RequiredFieldValidator>
                        </EditItemTemplate>
                        <ItemStyle Width="1177px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblPeriodSale" runat="server" Text='<%#Eval("PeriodSale","{0:yyyy-MM}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton4" runat="server" CausesValidation="false" CommandName="Delete"
                                Text='<%$Resources:Common,Button_Delete %>'></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td scope="col" style="width: 1177px" class="Empty1">
                                <asp:Label runat="server" Text="<%$Resources:Common,Form_Period %>"></asp:Label>
                            </td>
                            <td scope="col" style="width: 100px">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
            </gc:GridView>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="PeriodSaleFormView" EventName="ItemInserted" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="PeriodSaleUpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:FormView ID="PeriodSaleFormView" runat="server" DataKeyNames="PeriodSaleID"
                CellPadding="0" CellSpacing="0" DataSourceID="odsPeriodSale" DefaultMode="Insert">
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr>
                            <td align="center" style="width: 1177px">
                                <uc1:UCDateInput ID="UCPeriodSale" runat="server" IsExpensePeriod="true" SelectedDate='<%#Bind("PeriodSale")%>' />
                                <asp:RequiredFieldValidator ID="RFPeriodSale" runat="server" ControlToValidate="UCPeriodSale$txtDate"
                                    Display="None" meta:resourcekey="RequiredFieldValidator_Period" SetFocusOnError="True" ValidationGroup="PeriodSaleIns"></asp:RequiredFieldValidator>
                            </td>
                            <td align="center" style="width: 100px">
                                <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    Text='<%$Resources:Common,Button_Add %>' ValidationGroup="PeriodSaleIns"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                    <asp:ValidationSummary ID="ValidationSummaryInsert" runat="server" ShowMessageBox="True"
                        ShowSummary="False" ValidationGroup="PeriodSaleIns" />
                </InsertItemTemplate>
            </asp:FormView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="odsPeriodSale" runat="server" InsertMethod="InsertPeriodSale"
        SelectMethod="GetPeriodSale" DeleteMethod="DeletePeriodSaleByID" TypeName="BusinessObjects.MasterDataBLL"
        UpdateMethod="UpdatePeriodSale" OnInserted="odsPeriodSale_Inserted"></asp:ObjectDataSource>
</asp:Content>
