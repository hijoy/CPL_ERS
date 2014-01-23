<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="BudgetSaleFee.aspx.cs" Inherits="BudgetManage_BudgetSale" Culture="auto"
    UICulture="auto" %>

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
                    &nbsp;
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
                DataKeyNames="BudgetSaleID" CellPadding="0" DataSourceID="odsBudget" AllowPaging="True"
                AllowSorting="True" PageSize="20" OnSelectedIndexChanged="GVBudget_SelectedIndexChanged">
                <Columns>
                    <asp:TemplateField HeaderText="<%$Resources:Common,Form_OrganizationBudget %>" SortExpression="OrganizationUnitID">
                        <EditItemTemplate>
                            <asp:Label ID="lblOrganizationUnit1" runat="server" Text='<%# GetOUNameByOuID(Eval("OrganizationUnitID")) %>'></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="lbOrganizationUnit" runat="server" CommandName="Select" Text='<%# GetOUNameByOuID(Eval("OrganizationUnitID")) %>'></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="220px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Common,Form_CustomerChannel %>" SortExpression="CustomerChannelID">
                        <ItemTemplate>
                            <asp:Label ID="lblCustomerChannel" runat="server" Text='<%#GetCustomerChannelNameByID(Eval("CustomerChannelID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="120px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Common,Form_Brand %>" SortExpression="BrandID">
                        <ItemTemplate>
                            <asp:Label ID="lblBrand" runat="server" Text='<%#GetBrandNameByID(Eval("BrandID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="120px" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="ExpenseCategoryID" meta:resourcekey="TemplateField_ExpenseCategory">
                        <ItemTemplate>
                            <asp:Label ID="lblExpenseCategoryID" runat="server" Text='<%# GetExpenseCategoryNameByID(Eval("ExpenseCategoryID")) %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="120px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Common,Form_YearMonth %>" SortExpression="FPeriod">
                        <ItemTemplate>
                            <asp:Label ID="lblFPeriod" runat="server" Text='<%#Eval("FPeriod","{0:yyyyMM}") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="120px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Common,Form_AOPBudget %>" SortExpression="AOPBudget">
                        <ItemTemplate>
                            <asp:Label ID="lblAOP" runat="server" Text='<%# Eval("AOPBudget", "{0:N}") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Common,Form_AOPRBudget %>" SortExpression="AOPRBudget">
                        <ItemTemplate>
                            <asp:Label ID="lblAOPR" runat="server" Text='<%# Eval("AOPRBudget", "{0:N}") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateField_Projection" SortExpression="ProjectionBudget">
                        <ItemTemplate>
                            <asp:Label ID="lblProjection" runat="server" Text='<%# Eval("ProjectionBudget", "{0:N}") %>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtProjectionByEdit" runat="server" Width="60" Text='<%# Bind("ProjectionBudget") %>'></asp:TextBox>
                            <asp:RegularExpressionValidator ID="REProjection" runat="server" ControlToValidate="txtProjectionByEdit"
                                ValidationExpression="<%$ Resources:RegularExpressions, Money %>" Display="None"
                                meta:resourcekey="RegularExpressionValidator_Money" ValidationGroup="EDIT"></asp:RegularExpressionValidator>
                        </EditItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Common,Form_AdjustBudget %>" SortExpression="AdjustBudget">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtAdjustByEdit" runat="server" Width="60" Text='<%# Bind("AdjustBudget") %>'></asp:TextBox>
                            <asp:RegularExpressionValidator ID="REAdjust" runat="server" ControlToValidate="txtAdjustByEdit"
                                ValidationExpression="<%$ Resources:RegularExpressions, Money %>" Display="None"
                                meta:resourcekey="RegularExpressionValidator_Money" ValidationGroup="EDIT"></asp:RegularExpressionValidator>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblAdjustBudget" runat="server" Text='<%# Eval("AdjustBudget", "{0:N}") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateField_ModifyReason">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtModifyReason" runat="server" Text='<%# Bind("ModifyReason") %>'
                                Width="110px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RF4" runat="server" ControlToValidate="txtModifyReason"
                                Display="None" meta:resourcekey="RequiredFieldValidator_ModifyReason" ValidationGroup="EDIT"></asp:RequiredFieldValidator>
                            <asp:ValidationSummary ID="ValidationSummaryEDIT" runat="server" ShowMessageBox="True"
                                ShowSummary="False" ValidationGroup="EDIT" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblModifyReason" runat="server" Text='<%# Bind("ModifyReason") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="129px" />
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <EditItemTemplate>
                            <asp:LinkButton ID="UpdateLinkButton" runat="server" CommandName="Update" Text="<%$Resources:Common,Button_Update %>"
                                ValidationGroup="EDIT"></asp:LinkButton>
                            <asp:LinkButton ID="CancelLinkButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                Text="<%$Resources:Common,Button_Cancel %>"></asp:LinkButton>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="EditLinkButton" runat="server" CausesValidation="False" CommandName="Edit"
                                Text="<%$Resources:Common,Button_Edit %>"></asp:LinkButton>
                            <asp:LinkButton ID="DeleteButton" runat="server" CausesValidation="False" CommandName="Delete"
                                Text="<%$Resources:Common,Button_Delete %>" OnClientClick="return confirm('确定删除此行数据吗？');"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td style="width: 220px" class="Empty1">
                                <asp:Label ID="Label1" runat="server" Text="<%$Resources:Common,Form_OrganizationBudget %>" />
                            </td>
                            <td style="width: 120px">
                                <asp:Label ID="Label2" runat="server" Text="<%$Resources:Common,Form_CustomerChannel %>" />
                            </td>
                            <td style="width: 120px">
                                <asp:Label ID="Label3" runat="server" Text="<%$Resources:Common,Form_Brand %>" />
                            </td>
                            <td style="width: 120px">
                                <asp:Label ID="Label4" runat="server" meta:resourcekey="Label_ExpenseCategory" />
                            </td>
                            <td style="width: 120px">
                                <asp:Label ID="Label5" runat="server" Text="<%$Resources:Common,Form_YearMonth %>" />
                            </td>
                            <td style="width: 80px">
                                <asp:Label ID="Label6" runat="server" Text="<%$Resources:Common,Form_AOPBudget %>" />
                            </td>
                            <td style="width: 80px">
                                <asp:Label ID="Label7" runat="server" Text="<%$Resources:Common,Form_AOPRBudget %>" />
                            </td>
                            <td style="width: 80px">
                                <asp:Label ID="Label8" runat="server" meta:resourcekey="Label_Projection" />
                            </td>
                            <td style="width: 80px">
                                <asp:Label ID="Label9" runat="server" Text="<%$Resources:Common,Form_AdjustBudget %>" />
                            </td>
                            <td style="width: 129px">
                                <asp:Label ID="Label10" runat="server" meta:resourcekey="Label_ModifyReason" />
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
            <asp:FormView DefaultMode="Insert" ID="BudgetAddFormView" runat="server" DataKeyNames="BudgetSaleID"
                DataSourceID="odsBudget" Visible="<%# HasManageRight %>" CellPadding="0" meta:resourcekey="BudgetAddFormViewResource1">
                <InsertItemTemplate>
                    <table style="height: 30px;" class="FormView">
                        <tr>
                            <td align="center" style="width: 220px;">
                                <uc1:ucOUSelect runat="server" ID="ucNewOuSelect" IsNoClear="true" OUId='<%# Bind("OrganizationUnitID") %>'
                                    Width="160px" />
                                <asp:RequiredFieldValidator ID="RFOU" runat="server" ControlToValidate="ucNewOuSelect$OUNameCtl"
                                    Display="None" meta:resourcekey="RequiredFieldValidator_OUName" SetFocusOnError="True"
                                    ValidationGroup="INS"></asp:RequiredFieldValidator>
                            </td>
                            <td align="center" style="width: 120px;">
                                <asp:DropDownList ID="ddlCustomerChannel" runat="server" SelectedValue='<%# Bind("CustomerChannelID") %>'
                                    DataSourceID="sdsCustomerChannel" DataTextField="CustomerChannelName" DataValueField="CustomerChannelID"
                                    Width="100px">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="sdsCustomerChannel" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                    SelectCommand="Select CustomerChannelID,CustomerChannelName FROM CustomerChannel where IsActive = 1">
                                </asp:SqlDataSource>
                            </td>
                            <td align="center" style="width: 120px;">
                                <asp:DropDownList ID="ddlBrand" runat="server" SelectedValue='<%# Bind("BrandID") %>'
                                    DataSourceID="sdsBrand" DataTextField="BrandName" DataValueField="BrandID" Width="100px">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="sdsBrand" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                    SelectCommand="Select BrandID,BrandName FROM Brand"></asp:SqlDataSource>
                            </td>
                            <td align="center" style="width: 120px;">
                                <asp:DropDownList ID="ddlExpenseCategory" runat="server" SelectedValue='<%# Bind("ExpenseCategoryID") %>'
                                    DataSourceID="sdsExpenseCategory" DataTextField="ExpenseCategoryName" DataValueField="ExpenseCategoryID"
                                    Width="100px">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="sdsExpenseCategory" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                    SelectCommand="Select ExpenseCategoryID,ExpenseCategoryName FROM ExpenseCategory where BusinessType=1">
                                </asp:SqlDataSource>
                            </td>
                            <td align="center" style="width: 120px;">
                                <uc2:YearAndMonthUserControl ID="UCFPeriod" runat="server" SelectedDate='<%# Bind("FPeriod") %>'
                                    IsReadOnly="false" IsExpensePeriod="true" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="UCFPeriod$txtDate"
                                    Display="None" meta:resourcekey="RequiredFieldValidator_FPeriod" SetFocusOnError="True"
                                    ValidationGroup="INS"></asp:RequiredFieldValidator>
                            </td>
                            <td align="center" style="width: 80px;">
                                <asp:TextBox ID="txtNewAOPBudget" runat="server" Text='<%# Bind("AOPBudget") %>'
                                    Width="60px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RFAOP" runat="server" ControlToValidate="txtNewAOPBudget"
                                    Display="None" meta:resourcekey="RequiredFieldValidator_AOPBudget" SetFocusOnError="True"
                                    ValidationGroup="INS"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="REAOP" runat="server" ControlToValidate="txtNewAOPBudget"
                                    ValidationExpression="<%$ Resources:RegularExpressions, Money %>" Display="None"
                                    meta:resourcekey="RegularExpressionValidator_Money" ValidationGroup="INS"></asp:RegularExpressionValidator>
                            </td>
                            <td align="center" style="width: 80px;">
                                <asp:TextBox ID="txtNewAOPRBudget" runat="server" Text='<%# Bind("AOPRBudget") %>'
                                    Width="60px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RFAOPR" runat="server" ControlToValidate="txtNewAOPRBudget"
                                    Display="None" meta:resourcekey="RequiredFieldValidator_AOPRBudget" SetFocusOnError="True"
                                    ValidationGroup="INS"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="REAOPR" runat="server" ControlToValidate="txtNewAOPRBudget"
                                    ValidationExpression="<%$ Resources:RegularExpressions, Money %>" Display="None"
                                    meta:resourcekey="RegularExpressionValidator_Money" ValidationGroup="INS"></asp:RegularExpressionValidator>
                            </td>
                            <td align="center" style="width: 80px;">
                                <asp:TextBox ID="txtProjectionBudget" runat="server" Text='<%# Bind("ProjectionBudget") %>'
                                    Width="60px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RFProjection" runat="server" ControlToValidate="txtProjectionBudget"
                                    Display="None" meta:resourcekey="RequiredFieldValidator_Projection" SetFocusOnError="True"
                                    ValidationGroup="INS"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtProjectionBudget"
                                    ValidationExpression="<%$ Resources:RegularExpressions, Money %>" Display="None"
                                    meta:resourcekey="RegularExpressionValidator_Money" ValidationGroup="INS"></asp:RegularExpressionValidator>
                            </td>
                            <td align="center" style="width: 80px;">
                                <asp:TextBox ID="txtAdjustBudget" runat="server" Text='<%# Bind("AdjustBudgetStr") %>'
                                    Width="60px"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="REAdjust" runat="server" ControlToValidate="txtAdjustBudget"
                                    ValidationExpression="<%$ Resources:RegularExpressions, Money %>" Display="None"
                                    meta:resourcekey="RegularExpressionValidator_Money" ValidationGroup="INS"></asp:RegularExpressionValidator>
                            </td>
                            <td align="center" style="width: 129px;">
                                <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("ModifyReason") %>' Width="110px"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 80px;">
                                <asp:LinkButton ID="InsertButton" runat="server" CommandName="Insert" Text="<%$Resources:Common,Button_Add %>"
                                    ValidationGroup="INS"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                    <asp:ValidationSummary ID="ValidationSummaryINS" runat="server" ShowMessageBox="True"
                        ShowSummary="False" ValidationGroup="INS" meta:resourcekey="ValidationSummaryINSResource1" />
                </InsertItemTemplate>
            </asp:FormView>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="GVBudget" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="odsBudget" runat="server" InsertMethod="InsertBudgetSale"
        SelectMethod="GetPagedBudgetSale" DeleteMethod="DeleteBudgetSaleByID" TypeName="BusinessObjects.BudgetBLL"
        UpdateMethod="UpdateBudgetSale" EnablePaging="True" SortParameterName="sortExpression"
        SelectCountMethod="QueryBudgetSaleTotalCount" OnInserting="odsBudget_Inserting"
        OnUpdating="odsBudget_Updating" OnDeleted="odsBudget_Deleted"
        OnInserted="odsBudget_Inserted">
        <UpdateParameters>
            <asp:Parameter Name="UserID" Type="Int32" />
            <asp:Parameter Name="PositionID" Type="Int32" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="UserID" Type="Int32" />
            <asp:Parameter Name="PositionID" Type="Int32" />
        </InsertParameters>
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
    <div class="title">
        <asp:Label ID="Label_HistoryTitle" runat="server" meta:resourcekey="Label_HistoryTitle" /></div>
    <asp:UpdatePanel ID="UPHistory" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <gc:GridView CssClass="GridView" ID="GVHistory" runat="server" AutoGenerateColumns="False"
                DataKeyNames="BudgetSaleHistoryID" DataSourceID="odsHistory" meta:resourcekey="GVHistoryResource1">
                <Columns>
                    <asp:TemplateField meta:resourcekey="TemplateField_Projection">
                        <ItemTemplate>
                            <asp:Label ID="lblProjectionBudget" runat="server" Text='<%# Eval("ProjectionBudget", "{0:N}") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Common,Form_AdjustBudget %>">
                        <ItemTemplate>
                            <asp:Label ID="lblAdjustBudget" runat="server" Text='<%# Eval("AdjustBudget", "{0:N}") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateField_ModifyUser">
                        <ItemTemplate>
                            <asp:Label ID="lblUser" runat="server" Text='<%# GetUserNameByID(Eval("UserID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateField_ModifyPosition">
                        <ItemTemplate>
                            <asp:Label ID="lblPosition" runat="server" Text='<%# GetPositionNameByID(Eval("PositionID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="120px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateField_Action">
                        <ItemTemplate>
                            <asp:Label ID="lblAction" runat="server" Text='<%# Eval("Action") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateField_ModifyDate">
                        <ItemTemplate>
                            <asp:Label ID="lblModifyDate" runat="server" Text='<%# Eval("ModifyDate", "{0:yyyy-MM-dd HH:mm}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="120px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateField_ModifyReason">
                        <ItemTemplate>
                            <asp:Label ID="lblModifyReason" runat="server" Text='<%# Eval("ModifyReason") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="661px" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
            </gc:GridView>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="GVBudget" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="odsHistory" runat="server" SelectMethod="GetBudgetSaleHistory"
        TypeName="BusinessObjects.BudgetBLL">
        <SelectParameters>
            <asp:Parameter Name="BudgetSaleID" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
