<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="BudgetManageFee.aspx.cs" Inherits="BudgetManage_BudgetManageFee" Culture="auto"
    UICulture="auto" meta:resourcekey="PageResource1" %>

<%@ Register Src="~/UserControls/OUSelect.ascx" TagName="ucOUSelect" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/YearAndMonthUserControl.ascx" TagName="YearAndMonthUserControl"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <script src="../Script/DateInput.js" type="text/javascript"></script>
    <div class="title">
        <asp:Label ID="Label_SearchCondition" runat="server" Text="<%$Resources:Common,Label_SearchCondition %>" /></div>
    <div class="searchDiv">
        <table class="searchTable" cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 400px;">
                    <div class="field_title">
                        <asp:Label ID="Form_OrganizationBudget" runat="server" Text="<%$Resources:Common,Form_OrganizationBudget %>" /></div>
                    <uc1:ucOUSelect runat="server" ID="ucSearchOU" Width="220px" />
                </td>
                <td style="width: 300px;">
                    <div class="field_title">
                        <asp:Label ID="Form_YearMonth" runat="server" Text="<%$Resources:Common,Form_YearMonth %>" /></div>
                    <nobr>
                            <uc2:YearAndMonthUserControl ID="UCPeriodBegin" runat="server" IsReadOnly="false"
                                IsExpensePeriod="true" />
                            <asp:Label ID="lblSignPeriod" runat="server" >~~</asp:Label>
                            <uc2:YearAndMonthUserControl ID="UCPeriodEnd" runat="server" IsReadOnly="false" IsExpensePeriod="true" />
                </td>
                <td style="width: 250px;">
                    <div class="field_title">
                        <asp:Label ID="Form_ExpenseItem" runat="server" Text="<%$Resources:Common,Form_ExpenseItem %>" /></div>
                    <asp:DropDownList ID="SearchExpenseTypeDDL" runat="server" DataSourceID="odsSearchExpenseType"
                        DataTextField="ManageExpenseItemName" DataValueField="ManageExpenseItemID" Width="150px">
                    </asp:DropDownList>
                </td>
                <td style="width: 100px;" align="center" valign="bottom">
                    <asp:Button ID="SearchBtn" CssClass="button_nor" runat="server" Text="<%$Resources:Common,Button_Search %>" 
                    OnClick="SearchBtn_Click" />
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div class="title" style="width: 1250px;">
        <asp:Label ID="Form_BudgetTitle" runat="server" Text="<%$Resources:Common,Form_BudgetTitle %>" /></div>
    <asp:UpdatePanel ID="UPBudget" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <gc:GridView CssClass="GridView" ID="GVBudget" runat="server" AutoGenerateColumns="False"
                DataKeyNames="BudgetManageFeeID" CellPadding="0" DataSourceID="odsBudget" AllowPaging="True"
                AllowSorting="True" PageSize="20" OnSelectedIndexChanged="GVBudget_SelectedIndexChanged">
                <Columns>
                    <asp:TemplateField HeaderText="<%$Resources:Common,Form_OrganizationBudget %>" SortExpression="OrganizationUnitID" >
                        <EditItemTemplate>
                            <asp:Label ID="lblOrganizationUnit1" runat="server" Text='<%# GetOUNameByOuID(Eval("OrganizationUnitID")) %>'></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="lbOrganizationUnit" runat="server" CommandName="Select" Text='<%# GetOUNameByOuID(Eval("OrganizationUnitID")) %>'></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="250px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateField_Period" SortExpression="Year">
                        <EditItemTemplate>
                            <asp:Label ID="lblPeriod1" runat="server" Text='<%# Eval("Year") %>' ></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblPeriod2" runat="server" Text='<%# Eval("Year") %>' ></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="120px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Common,Form_AOPBudget %>" SortExpression="AOPBudget" >
                        <ItemTemplate>
                            <asp:Label ID="lblAOPBudget2" runat="server" Text='<%# Eval("AOPBudget", "{0:N}") %>'/>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Common,Form_AOPRBudget %>" SortExpression="AOPRBudget" >
                        <EditItemTemplate>
                            <asp:TextBox ID="txtAOPRBudget" runat="server" Text='<%# Bind("AOPRBudget") %>' Width="80px"
                                meta:resourcekey="txtAdjustBudgetResource1"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RF3" runat="server" ControlToValidate="txtAOPRBudget"
                                ValidationExpression="<%$ Resources:RegularExpressions, MinusMoney %>" Display="None"
                                meta:resourcekey="RegularExpressionValidator_Money" ValidationGroup="EDIT" ></asp:RegularExpressionValidator>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblAOPRBudget2" runat="server" Text='<%# Eval("AOPRBudget", "{0:N}") %>'/>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Common,Form_AdjustBudget %>" SortExpression="AdjustBudget" >
                        <ItemTemplate>
                            <asp:Label ID="lblAdjustBudget" runat="server" Text='<%# Eval("AdjustBudget", "{0:N}") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateField_ModifyReason">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtModifyReason" runat="server" Text='<%# Bind("ModifyReason") %>'
                                Width="460px" meta:resourcekey="txtModifyReasonResource1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RF4" runat="server" ControlToValidate="txtModifyReason"
                                Display="None"  ValidationGroup="EDIT" meta:resourcekey="RequiredFieldValidator_ModifyReason"></asp:RequiredFieldValidator>
                            <asp:ValidationSummary ID="ValidationSummaryEDIT" runat="server" ShowMessageBox="True"
                                ShowSummary="False" ValidationGroup="EDIT"  />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblModifyReason" runat="server" Text='<%# Bind("ModifyReason") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="482px" />
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <EditItemTemplate>
                            <asp:LinkButton ID="UpdateLinkButton" runat="server" CommandName="Update" Text="<%$Resources:Common,Button_Update %>"
                                ValidationGroup="EDIT" ></asp:LinkButton>
                            <asp:LinkButton ID="CancelLinkButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                Text="<%$Resources:Common,Button_Cancel %>" ></asp:LinkButton>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="EditLinkButton" runat="server" CausesValidation="False" CommandName="Edit"
                                Text="<%$Resources:Common,Button_Edit %>" ></asp:LinkButton>
                            <asp:LinkButton ID="DeleteButton" runat="server" CausesValidation="False" CommandName="Delete"
                                Text="<%$Resources:Common,Button_Delete %>" OnClientClick="return confirm('确定删除此行数据吗？');" ></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td style="width: 250px" class="Empty1">
                                <asp:Label ID="Label1" runat="server" Text="<%$Resources:Common,Form_OrganizationBudget %>" />
                            </td>
                            <td style="width: 120px">
                                <asp:Label ID="Label2" runat="server" meta:resourcekey="Label_Period" />
                            </td>
                            <td style="width: 100px">
                                <asp:Label ID="Label4" runat="server" Text="<%$Resources:Common,Form_AOPBudget %>" />
                            </td>
                            <td style="width: 100px">
                                <asp:Label ID="Label5" runat="server" Text="<%$Resources:Common,Form_AOPRBudget %>" />
                            </td>
                            <td style="width: 100px">
                                <asp:Label ID="Label6" runat="server" Text="<%$Resources:Common,Form_AdjustBudget %>" />
                            </td>
                            <td style="width: 482px">
                                <asp:Label ID="Label7" runat="server" meta:resourcekey="Label_ModifyReason" />
                            </td>
                            <td style="width: 80px">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
            </gc:GridView>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="BudgetAddFormView" EventName="ItemInserted" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="AddUpdatePanel" runat="server">
        <ContentTemplate>
            <asp:FormView DefaultMode="Insert" ID="BudgetAddFormView" runat="server" DataKeyNames="BudgetManageFeeID"
                DataSourceID="odsBudget" Visible="<%# HasManageRight %>" CellPadding="0" >
                <InsertItemTemplate>
                    <table style="height: 30px;" class="FormView">
                        <tr>
                            <td align="center" style="width: 250px;">
                                <uc1:ucOUSelect runat="server" ID="ucNewOuSelect" IsNoClear="true" OUId='<%# Bind("OrganizationUnitID") %>'
                                    Width="190px" />
                            </td>
                            <td align="center" style="width: 120px;">
                                <asp:TextBox ID="txtYearByAdd" runat="server" Text='<%# Bind("Year") %>' Width="100px" MaxLength="4"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 100px;">
                                <asp:TextBox ID="txtAOPBudgetByAdd" runat="server" Text='<%# Bind("AOPBudget") %>'
                                    Width="80px"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 100px;">
                                <asp:TextBox ID="txtAOPRBudgetByAdd" runat="server" Text='<%# Bind("AOPRBudget") %>'
                                    Width="80px" ></asp:TextBox>
                            </td>
                            <td align="center" style="width: 100px;">
                            </td>
                            <td align="center" style="width: 482px;">
                                <asp:TextBox ID="txtModifyReasonByAdd" runat="server" Text='<%# Bind("ModifyReason") %>'
                                    Width="460px" ></asp:TextBox>
                            </td>
                            <td align="center" style="width: 80px;">
                                <asp:LinkButton ID="InsertButton" runat="server" CommandName="Insert" Text="<%$Resources:Common,Button_Add %>" 
                                    ValidationGroup="INS" ></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                    <asp:RequiredFieldValidator ID="RFAOPByAdd" runat="server" ControlToValidate="txtAOPBudgetByAdd"
                        Display="None"  SetFocusOnError="True" ValidationGroup="INS"
                        meta:resourcekey="RequiredFieldValidator_AOPBudget"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RFAOPRByAdd" runat="server" ControlToValidate="txtAOPRBudgetByAdd"
                        Display="None" meta:resourcekey="RequiredFieldValidator_AOPRBudget" SetFocusOnError="True" ValidationGroup="INS"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RF3" runat="server" ControlToValidate="txtAOPBudgetByAdd"
                        ValidationExpression="<%$ Resources:RegularExpressions, Money %>" Display="None"
                        ValidationGroup="INS" meta:resourcekey="RegularExpressionValidator_Money"></asp:RegularExpressionValidator>
                    <asp:RegularExpressionValidator ID="RF5" runat="server" ControlToValidate="txtAOPRBudgetByAdd"
                        ValidationExpression="<%$ Resources:RegularExpressions, MinusMoney %>" Display="None"
                        ValidationGroup="INS" meta:resourcekey="RegularExpressionValidator_Money"></asp:RegularExpressionValidator>
                    <asp:ValidationSummary ID="ValidationSummaryINS" runat="server" ShowMessageBox="True"
                        ShowSummary="False" ValidationGroup="INS"  />
                </InsertItemTemplate>
            </asp:FormView>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="GVBudget" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="odsBudget" runat="server" InsertMethod="InsertBudgetManageFee"
        SelectMethod="GetPagedBudgetManageFee" 
        TypeName="BusinessObjects.BudgetBLL" UpdateMethod="UpdateBudgetManageFee"
        EnablePaging="True" SortParameterName="sortExpression" 
        SelectCountMethod="QueryBudgetManageFeeTotalCount" DeleteMethod="DeleteBudgetManageFeeByID"
        OnInserting="odsBudget_Inserting" OnUpdating="odsBudget_Updating" 
        OnUpdated="odsBudget_Updating" ondeleted="odsBudget_Deleted" 
        oninserted="odsBudget_Inserted">
        <SelectParameters>
            <asp:Parameter Name="queryExpression" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:SqlDataSource ID="odsSearchExpenseType" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
        SelectCommand="select 0 ManageExpenseItemID,'ALL' ManageExpenseItemName union SELECT [ManageExpenseItemID], [ManageExpenseItemName] FROM [ManageExpenseItem] ">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="odsExpenseType" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
        SelectCommand=" SELECT [ManageExpenseItemID], [ManageExpenseItemName] FROM [ManageExpenseItem] where IsActive = 1">
    </asp:SqlDataSource>
    <br />
    <div class="title" style="width: 1250px;">
        <asp:Label ID="Label_HistoryTitle" runat="server" meta:resourcekey="Label_HistoryTitle" /></div>
    <asp:UpdatePanel ID="UPHistory" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <gc:GridView CssClass="GridView" ID="GVHistory" runat="server" AutoGenerateColumns="False"
                DataKeyNames="BudgetManageFeeHistoryID" DataSourceID="odsHistory" meta:resourcekey="GVHistoryResource1">
                <Columns>
                    <asp:TemplateField HeaderText="<%$Resources:Common,Form_AOPBudget %>">
                        <ItemTemplate>
                            <asp:Label ID="lblAOPBudget" runat="server" Text='<%# Eval("AOPBudget", "{0:N}") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Common,Form_AOPRBudget %>">
                        <ItemTemplate>
                            <asp:Label ID="lblAOPRBudget" runat="server" Text='<%# Eval("AOPRBudget", "{0:N}") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Common,Form_AdjustBudget %>">
                        <ItemTemplate>
                            <asp:Label ID="lblAdjustBudget" runat="server" Text='<%# Eval("AdjustBudget", "{0:N}") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField  meta:resourcekey="TemplateField_ModifyUser">
                        <ItemTemplate>
                            <asp:Label ID="lblUser" runat="server" Text='<%# GetUserNameByID(Eval("UserID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField  meta:resourcekey="TemplateField_ModifyPosition">
                        <ItemTemplate>
                            <asp:Label ID="lblPosition" runat="server" Text='<%# GetPositionNameByID(Eval("PositionID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="120px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField  meta:resourcekey="TemplateField_Action">
                        <ItemTemplate>
                            <asp:Label ID="lblAction" runat="server" Text='<%# Eval("Action") %>' ></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                    </asp:TemplateField>
                    <asp:TemplateField  meta:resourcekey="TemplateField_ModifyDate">
                        <ItemTemplate>
                            <asp:Label ID="lblModifyDate" runat="server" Text='<%# Eval("ModifyDate", "{0:yyyy-MM-dd HH:mm}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="120px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField  meta:resourcekey="TemplateField_ModifyReason">
                        <ItemTemplate>
                            <asp:Label ID="lblModifyReason" runat="server" Text='<%# Eval("ModifyReason") %>'/>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="532px" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
            </gc:GridView>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="GVBudget" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="odsHistory" runat="server" SelectMethod="GetBudgetManageFeeHistoryByParentID"
        TypeName="BusinessObjects.BudgetBLL">
        <SelectParameters>
            <asp:Parameter Name="BudgetManageFeeID" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
