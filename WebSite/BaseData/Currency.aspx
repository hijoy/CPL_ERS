<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Currency.aspx.cs" Culture="auto" UICulture="auto" Inherits="BaseData_Currency" %>

<%@ Register Src="../UserControls/YearAndMonthUserControl.ascx" TagName="UCDateInput"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <script src="../Script/DateInput.js" type="text/javascript"></script>
    <div class="title">
        <asp:Label ID="Label5" runat="server" meta:resourcekey="Label_Title"></asp:Label></div>
    <asp:UpdatePanel ID="CurrencyUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <gc:GridView ID="gvCurrency" CssClass="GridView" runat="server" DataSourceID="odsCurrency"
                AllowPaging="False" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="CurrencyID"
                CellPadding="0" OnSelectedIndexChanged="gvCurrency_SelectedIndexChanged">
                <Columns>
                    <asp:TemplateField meta:resourcekey="CurrencyGridView_TemplateField_CurrencyFullName"
                        SortExpression="CurrencyFullName">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCurrencyFullName" runat="server" CssClass="InputText" MaxLength="20"
                                Text='<%# Bind("CurrencyFullName") %>' Width="440px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="CurrencyFullNameRequiredFieldValidator" runat="server"
                                ControlToValidate="txtCurrencyFullName" Display="None" meta:resourcekey="RequiredFieldValidator_FullName"
                                SetFocusOnError="True" ValidationGroup="EDIT"></asp:RequiredFieldValidator>
                        </EditItemTemplate>
                        <ItemStyle Width="460px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnCurrencyName" runat="server" CausesValidation="false" CommandName="Select"
                                Text='<%# Bind("CurrencyFullName") %>'></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Common,Form_Currency %>" SortExpression="CurrencyShortName">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCurrencyShortName" runat="server" Text='<%# Bind("CurrencyShortName") %>'
                                CssClass="InputText" Width="440px" MaxLength="20"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="CurrencyShortNameRequiredFieldValidator" runat="server"
                                ControlToValidate="txtCurrencyShortName" Display="None" meta:resourcekey="RequiredFieldValidator_ShortName"
                                SetFocusOnError="True" ValidationGroup="EDIT"></asp:RequiredFieldValidator>
                        </EditItemTemplate>
                        <ItemStyle Width="465px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblCurrencyShortName" runat="server" Text='<%# Bind("CurrencyShortName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="CurrencyGridView_TemplateField_CurrencySymbol"
                        SortExpression="CurrencySymbol">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCurrencySymbol" runat="server" Text='<%# Bind("CurrencySymbol") %>'
                                CssClass="InputText" Width="100px" MaxLength="10"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="CurrencySymbolRequiredFieldValidator" runat="server"
                                ControlToValidate="txtCurrencySymbol" Display="None" meta:resourcekey="RequiredFieldValidator_Symbol"
                                SetFocusOnError="True" ValidationGroup="EDIT"></asp:RequiredFieldValidator>
                        </EditItemTemplate>
                        <ItemStyle Width="140px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblCurrencySymbol" runat="server" Text='<%# Bind("CurrencySymbol") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="IsActive" HeaderText="<%$Resources:Common,Form_IsActive %>">
                        <EditItemTemplate>
                            <asp:CheckBox runat="server" ID="chkActiveByEdit" Checked='<%# Bind("IsActive") %>' />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="chkActiveByEdit1" Enabled="false" Checked='<%# Bind("IsActive") %>' />
                        </ItemTemplate>
                        <ItemStyle Width="70px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <EditItemTemplate>
                            <asp:LinkButton ID="LinkButton1" Visible="<%# HasManageRight %>" runat="server" CausesValidation="True"
                                ValidationGroup="skuEdit" CommandName="Update" Text='<%$Resources:Common,Button_Update %>'></asp:LinkButton>
                            <asp:LinkButton ID="LinkButton3" Visible="<%# HasManageRight %>" runat="server" CausesValidation="false"
                                CommandName="Cancel" Text='<%$Resources:Common,Button_Cancel %>'></asp:LinkButton>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton2" Visible="<%# HasManageRight %>" runat="server" CausesValidation="false"
                                CommandName="Edit" Text='<%$Resources:Common,Button_Edit %>'></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td scope="col" style="width: 440px" class="Empty1">
                                <asp:Label ID="Label1" runat="server" meta:resourcekey="Currency_Label_CurrencyFullName"></asp:Label>
                            </td>
                            <td scope="col" style="width: 465px">
                                <asp:Label ID="Label2" runat="server" Text="<%$Resources:Common,Form_Currency %>"></asp:Label>
                            </td>
                            <td scope="col" style="width: 140px">
                                <asp:Label ID="Label3" runat="server" meta:resourcekey="Currency_Label_CurrencySymbol"></asp:Label>
                            </td>
                            <td scope="col" style="width: 70px">
                                <asp:Label ID="Label4" runat="server" Text="<%$Resources:Common,Form_IsActive %>"></asp:Label>
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
            <asp:AsyncPostBackTrigger ControlID="CurrencyFormView" EventName="ItemInserted" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="CurrencyUpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:FormView ID="CurrencyFormView" runat="server" DataKeyNames="CurrencyID" CellPadding="0"
                CellSpacing="0" DataSourceID="odsCurrency" DefaultMode="Insert" Visible="<%# HasManageRight %>">
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr>
                            <td align="center" style="width: 460px">
                                <asp:TextBox ID="txtCurrencyFullName" runat="server" Text='<%# Bind("CurrencyFullName") %>'
                                    CssClass="InputText" Width="440px" ValidationGroup="INS"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 465px">
                                <asp:TextBox ID="txtCurrencyShortName" runat="server" Text='<%# Bind("CurrencyShortName") %>'
                                    CssClass="InputText" Width="440px" ValidationGroup="INS" MaxLength="100"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 140px">
                                <asp:TextBox ID="txtCurrencySymbol" runat="server" Text='<%# Bind("CurrencySymbol") %>'
                                    CssClass="InputText" Width="100px" ValidationGroup="INS" MaxLength="500"></asp:TextBox>
                            </td>
                            <td align="center" style="height: 22px; width: 70px;">
                                <asp:CheckBox runat="server" ID="chkActiveByAdd" Checked='<%# Bind("IsActive") %>' />
                            </td>
                            <td align="center" style="width: 100px">
                                <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    Text='<%$Resources:Common,Button_Add %>' ValidationGroup="INS"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                    <asp:RequiredFieldValidator ID="CurrencyShortNameRequiredFieldValidator" runat="server"
                        ControlToValidate="txtCurrencyShortName" Display="None" meta:resourcekey="RequiredFieldValidator_FullName"
                        SetFocusOnError="True" ValidationGroup="INS"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="CurrencyFullNameRequiredFieldValidator" runat="server"
                        ControlToValidate="txtCurrencyFullName" Display="None" meta:resourcekey="RequiredFieldValidator_ShortName"
                        SetFocusOnError="True" ValidationGroup="INS"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="CurrencySymbolRequiredFieldValidator" runat="server"
                        ControlToValidate="txtCurrencySymbol" Display="None" meta:resourcekey="RequiredFieldValidator_Symbol"
                        SetFocusOnError="True" ValidationGroup="INS"></asp:RequiredFieldValidator>
                    <asp:ValidationSummary ID="ValidationSummaryInsert" runat="server" ShowMessageBox="True"
                        ShowSummary="False" ValidationGroup="INS" />
                </InsertItemTemplate>
            </asp:FormView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="odsCurrency" runat="server" InsertMethod="InsertCurrency"
        SelectMethod="GetCurrency" TypeName="BusinessObjects.MasterDataBLL" UpdateMethod="UpdateCurrency">
    </asp:ObjectDataSource>
    <br />
    <div class="title">
        <asp:Label runat="server" Text="<%$Resources:Common,Form_ExchangeRate %>"></asp:Label></div>
    <asp:UpdatePanel ID="upExchangeRate" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <gc:GridView ID="gvExchangeRate" CssClass="GridView" runat="server" DataSourceID="odsExchangeRate"
                AllowPaging="true" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="ExchangeRateID"
                PageSize="20" CellPadding="0" Visible="false">
                <Columns>
                    <asp:TemplateField HeaderText="<%$Resources:Common,Form_Period %>" SortExpression="Period">
                        <EditItemTemplate>
                            <uc1:UCDateInput ID="UCPeriod" runat="server" IsExpensePeriod="true" SelectedDate='<%#Bind("Period")%>' />
                            <asp:RequiredFieldValidator ID="RFPeriod" runat="server" ControlToValidate="UCPeriod$txtDate"
                                Display="None" meta:resourcekey="RequiredFieldValidator_Period" SetFocusOnError="True" ValidationGroup="ExchangeRateEdit"></asp:RequiredFieldValidator>
                        </EditItemTemplate>
                        <ItemStyle Width="500px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblBeginDate" runat="server" Text='<%#Eval("Period","{0:yyyy-MM}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Common,Form_ExchangeRate %>" SortExpression="Value">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtExchangeRate" runat="server" CssClass="InputText" MaxLength="20"
                                Text='<%# Bind("Value") %>' Width="200px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RFExchangeRate" runat="server" ControlToValidate="txtExchangeRate"
                                Display="None" meta:resourcekey="RequiredFieldValidator_ExchangeRate" SetFocusOnError="True" ValidationGroup="ExchangeRateEdit"></asp:RequiredFieldValidator>
                        </EditItemTemplate>
                        <ItemStyle Width="637px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblExchangeRate" runat="server" Text='<%# Bind("Value") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <EditItemTemplate>
                            <asp:ValidationSummary ID="ValidationSummaryEdit" runat="server" ShowMessageBox="True"
                                ShowSummary="False" ValidationGroup="ExchangeRateEdit" />
                            <asp:LinkButton ID="LinkButton1" Visible="<%# HasManageRight %>" runat="server" CausesValidation="True"
                                ValidationGroup="ExchangeRateEdit" CommandName="Update" Text='<%$Resources:Common,Button_Update %>'></asp:LinkButton>
                            <asp:LinkButton ID="LinkButton3" Visible="<%# HasManageRight %>" runat="server" CausesValidation="false"
                                CommandName="Cancel" Text='<%$Resources:Common,Button_Cancel %>'></asp:LinkButton>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton2" Visible="<%# HasManageRight %>" runat="server" CausesValidation="false"
                                CommandName="Edit" Text='<%$Resources:Common,Button_Edit %>'></asp:LinkButton>
                            <asp:LinkButton ID="LinkButton4" Visible="<%# HasManageRight %>" runat="server" CausesValidation="false"
                                CommandName="Delete" Text='<%$Resources:Common,Button_Delete %>' OnClientClick="return confirm('确定要删除么？')"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td scope="col" style="width: 500px" class="Empty1">
                                <asp:Label ID="Label7" runat="server" Text="<%$Resources:Common,Form_Period %>"></asp:Label>
                            </td>
                            <td scope="col" style="width: 637px">
                                <asp:Label ID="Label8" runat="server" Text="<%$Resources:Common,Form_ExchangeRate %>"></asp:Label>
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
            <asp:AsyncPostBackTrigger ControlID="fvExchangeRate" EventName="ItemInserted" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="upExchangeRate1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:FormView ID="fvExchangeRate" runat="server" DataKeyNames="ExchangeRateID" CellPadding="0"
                CellSpacing="0" DataSourceID="odsExchangeRate" DefaultMode="Insert" Visible="false">
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr>
                            <td align="center" style="width: 500px">
                                <uc1:UCDateInput ID="UCPeriod" runat="server" IsExpensePeriod="true" SelectedDate='<%#Bind("Period")%>' />
                                <asp:RequiredFieldValidator ID="RFPeriod" runat="server" ControlToValidate="UCPeriod$txtDate"
                                    Display="None" meta:resourcekey="RequiredFieldValidator_Period" SetFocusOnError="True" ValidationGroup="ExchangeRateIns"></asp:RequiredFieldValidator>
                            </td>
                            <td align="center" style="width: 637px">
                                <asp:TextBox ID="txtExchangeRate" runat="server" CssClass="InputText" MaxLength="20"
                                    Text='<%# Bind("Value") %>' Width="200px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RFExchangeRate" runat="server" ControlToValidate="txtExchangeRate"
                                    Display="None" meta:resourcekey="RequiredFieldValidator_ExchangeRate" SetFocusOnError="True" ValidationGroup="ExchangeRateIns"></asp:RequiredFieldValidator>
                            </td>
                            <td align="center" style="width: 100px">
                                <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    Text='<%$Resources:Common,Button_Add %>' ValidationGroup="ExchangeRateIns"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                    <asp:ValidationSummary ID="ValidationSummaryInsert" runat="server" ShowMessageBox="True"
                        ShowSummary="False" ValidationGroup="ExchangeRateIns" />
                </InsertItemTemplate>
            </asp:FormView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="odsExchangeRate" EnablePaging="true" SortParameterName="sortExpression"
        runat="server" InsertMethod="InsertExchangeRate" SelectMethod="GetPagedExchangeRate"
        DeleteMethod="DeleteByID" SelectCountMethod="ExchangeRateTotalCount" TypeName="BusinessObjects.MasterDataBLL"
        UpdateMethod="UpdateExchangeRate" OnSelecting="odsExchangeRate_Selecting" OnInserting="odsExchangeRate_Inserting"
        OnUpdating="odsExchangeRate_Updating">
        <SelectParameters>
            <asp:Parameter Name="CurrencyID" />
        </SelectParameters>
        <InsertParameters>
            <asp:Parameter Name="CurrencyID" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="CurrencyID" />
        </UpdateParameters>
    </asp:ObjectDataSource>
</asp:Content>
