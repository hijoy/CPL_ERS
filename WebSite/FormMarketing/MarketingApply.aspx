<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="MarketingApply.aspx.cs" Culture="Auto" UICulture="Auto" Inherits="FormMarketing_MarketingApply" %>

<%@ Register Src="../UserControls/UCDateInput.ascx" TagName="UCDateInput" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/MultiAttachmentFile.ascx" TagName="UCFlie" TagPrefix="uc2" %>
<%@ Register Src="../UserControls/ucUpdateProgress.ascx" TagName="ucUpdateProgress"
    TagPrefix="uc3" %>
<%@ Register Src="../UserControls/VendorControl.ascx" TagName="UCVendor" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <script src="../Script/DateInput.js" type="text/javascript"></script>
    <div class="title">
        <asp:Label ID="Label_Title" runat="server" meta:resourcekey="Label_Title" />
    </div>
    <div class="searchDiv">
        <table class="searchTable">
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_ApplyUser" runat="server" Text="<%$Resources:Common,Form_ApplyUser %>" /></div>
                    <div>
                        <asp:TextBox ID="StuffNameCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_Position" runat="server" Text="<%$Resources:Common,Form_Position %>" /></div>
                    <div>
                        <asp:TextBox ID="PositionNameCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_Organization" runat="server" Text="<%$Resources:Common,Form_Organization %>" /></div>
                    <div>
                        <asp:TextBox ID="DepartmentNameCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label_StuffNo" runat="server" Text="<%$Resources:Common,Form_StaffNo %>" /></div>
                    <div>
                        <asp:TextBox ID="StuffNoCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_AttendDate" runat="server" Text="<%$Resources:Common,Form_AttendDate %>" /></div>
                    <div>
                        <asp:TextBox ID="AttendDateCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_Period" runat="server" Text="<%$Resources:Common,Form_Period %>" /></div>
                    <div>
                        <asp:TextBox ID="PeriodCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_CustomerChannel" runat="server" Text="<%$Resources:Common,Form_CustomerChannel %>" /></div>
                    <div>
                        <asp:TextBox ID="CustomerChannelCtl" runat="server" CssClass="InputTextReadOnly"
                            ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label_Brand" runat="server" Text="<%$Resources:Common,Form_Brand %>" /></div>
                    <div>
                        <asp:TextBox ID="BrandCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label_Currency" runat="server" Text="<%$Resources:Common,Form_Currency %>" /></div>
                    <div>
                        <asp:TextBox ID="CurrencyCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label_ExchangeRate" runat="server" Text="<%$Resources:Common,Form_ExchangeRate %>" /></div>
                    <div>
                        <asp:TextBox ID="ExchangeRateCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label_ExpenseCategory" runat="server" Text="<%$Resources:Common,Form_ExpenseCategory %>" />
                    </div>
                    <div>
                        <asp:TextBox ID="ExpenseCategoryCtl" runat="server" CssClass="InputTextReadOnly"
                            ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="width: 400px">
                    <div class="field_title">
                        <asp:Label ID="Form_ProjectName" runat="server" Text="<%$Resources:Common,Form_ProjectName %>" /></div>
                    <div>
                        <asp:DropDownList ID="ddlMarketingProject" runat="server" DataTextField="MarketingProjectName"
                            DataValueField="MarketingProjectID" DataSourceID="odsMarketingProject" Width="370px">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="odsMarketingProject" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                            SelectCommand="SELECT 0 [MarketingProjectID],'please select' MarketingProjectName union select [MarketingProjectID], MarketingProjectName FROM [MarketingProject] where IsActive=1 order by MarketingProjectID">
                        </asp:SqlDataSource>
                    </div>
                </td>
                <td colspan="2" style="width: 400px">
                    <div class="field_title">
                        <asp:Label ID="Label_ActivityBegin" runat="server" meta:resourcekey="Label_ActivityBegin" />
                    </div>
                    <div>
                        <uc1:UCDateInput ID="UCActivityBegin" runat="server" IsReadOnly="false" />
                        <asp:Label ID="Label2" runat="server">~~</asp:Label>
                        <uc1:UCDateInput ID="UCActivityEnd" runat="server" IsReadOnly="false" />
                    </div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_CostCenter" runat="server" Text="<%$Resources:Common,Form_CostCenter %>" />
                    </div>
                    <div>
                        <asp:DropDownList ID="CostCenterDDL" runat="server" DataSourceID="odsCostCenter"
                            DataTextField="CostCenterCode" DataValueField="CostCenterID" Width="180px">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="odsCostCenter" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                            SelectCommand="select 0 [CostCenterID],' please select' CostCenterCode,' ' CompanyCode union SELECT [CostCenterID], Company.CompanyCode+'-'+isNull(Region,'')+'-'+ CostCenterCode as CostCenterCode, CompanyCode FROM [CostCenter] join Company on CostCenter.CompanyID = Company.CompanyID where IsActive = 1 and IsMAA=1 order by CompanyCode,CostCenterCode">
                        </asp:SqlDataSource>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="3" valign="top">
                    <div class="field_title">
                        <asp:Label ID="Label_ProjectDesc" runat="server" meta:resourcekey="Label_ProjectDesc" />
                    </div>
                    <asp:TextBox ID="ProjectDescCtl" runat="server" CssClass="InputText" TextMode="multiline"
                        Rows="5" Columns="78"></asp:TextBox>
                </td>
                <td colspan="3" valign="top">
                    <div class="field_title">
                        <asp:Label ID="Form_Adjunct" runat="server" Text="<%$Resources:Common,Form_Adjunct %>" /></div>
                    <uc2:UCFlie ID="UCFileUpload" runat="server" Width="400px" />
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div class="title">
        <asp:Label ID="Form_BudgetTitle" runat="server" Text="<%$Resources:Common,Form_BudgetTitle %>" />
    </div>
    <div class="searchDiv">
        <table class="searchTable">
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label_TotalBudget" runat="server" meta:resourcekey="Label_TotalBudget" />
                    </div>
                    <div>
                        <asp:TextBox ID="TotalBudgetCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px; ">
                    <div class="field_title">
                        <asp:Label ID="Label_ApprovedAmount" runat="server" meta:resourcekey="Label_ApprovedAmount" />
                    </div>
                    <div>
                        <asp:TextBox ID="ApprovedAmountCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label_ApprovingAmount" runat="server" meta:resourcekey="Label_ApprovingAmount" />
                    </div>
                    <div>
                        <asp:TextBox ID="ApprovingAmountCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px;display: none;">
                    <div class="field_title">
                        <asp:Label ID="Label_ReimbursedAmount" runat="server" meta:resourcekey="Label_ReimbursedAmount" />
                    </div>
                    <div>
                        <asp:TextBox ID="ReimbursedAmountCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label_RemainBudget" runat="server" meta:resourcekey="Label_RemainBudget" />
                    </div>
                    <div>
                        <asp:TextBox ID="RemainBudgetCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div class="title">
        <asp:Label ID="Label1" runat="server" meta:resourcekey="Label_Title1" />
    </div>
    <asp:HiddenField ID="hdnBrandID" runat="server" Visible="False" />
    <asp:HiddenField ID="ExpenseCategoryID" runat="server" Visible="False" />
    <asp:UpdatePanel ID="upDetails" runat="server">
        <ContentTemplate>
            <gc:GridView ID="gvDetails" runat="server" ShowFooter="True" CssClass="GridView"
                AutoGenerateColumns="False" DataKeyNames="FormMarketingApplyDetailID" DataSourceID="odsDetails"
                OnRowDataBound="gvDetails_RowDataBound" CellPadding="0">
                <Columns>
                    <asp:TemplateField HeaderText="<%$Resources:Common,Form_VendorName %>">
                        <ItemTemplate>
                            <asp:Label ID="lblVendor" runat="server" Text='<%# GetVendorNameByID(Eval("VendorID")) %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <uc4:UCVendor ID="UCVendor" runat="server" Width="170px" IsNoClear="true" VendorID='<%# Bind("VendorID") %>' IsLimited="true" />
                        </EditItemTemplate>
                        <ItemStyle Width="250px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Common,Form_ExpenseItem %>">
                        <ItemTemplate>
                            <asp:Label ID="lblExpenseItem" runat="server" Text='<%# GetExpenseItemNameByID(Eval("ExpenseItemID")) %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlExpenseItemByEdit" runat="server" DataSourceID="sdsExpenseItem"
                                DataTextField="ExpenseItemName" DataValueField="ExpenseItemID" SelectedValue='<%# Bind("ExpenseItemID") %>'
                                Width="180px">
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="sdsExpenseItem" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                SelectCommand="select ExpenseItemID,ExpenseItemName from ExpenseItem join ExpenseSubCategory on ExpenseItem.ExpenseSubCategoryID=ExpenseSubCategory.ExpenseSubCategoryID where ExpenseSubCategory.ExpenseCategoryID = @ExpenseCategoryID order by ExpenseItemName">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="ExpenseCategoryID" Name="ExpenseCategoryID" PropertyName="Value"
                                        Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </EditItemTemplate>
                        <ItemStyle Width="200px" HorizontalAlign="Center" />
                        <FooterStyle CssClass="RedTextAlignCenter" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Common,Form_ProductName %>">
                        <ItemTemplate>
                            <asp:Label ID="lblSKU" runat="server" Text='<%# GetSKUNameByID(Eval("SKUID")) %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlSKUByAdd" runat="server" DataSourceID="sdsSKUByAdd" DataTextField="SKUName"
                                DataValueField="SKUID" SelectedValue='<%# Bind("SKUID") %>' Width="180px">
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="sdsSKUByAdd" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                SelectCommand="SELECT [SKUID], [SKUName]+'-'+[SKUNo] SKUName FROM [SKU] where IsActive = 1 and BrandID = @BrandID order by SKUName">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="hdnBrandID" Name="BrandID" PropertyName="Value"
                                        Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </EditItemTemplate>
                        <ItemStyle Width="200px" HorizontalAlign="Center" />
                        <FooterTemplate>
                            <asp:Label runat="server" ID="lblSum" Text="<%$Resources:Common,Form_Total %>"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle CssClass="RedTextAlignCenter" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateField_Amount">
                        <ItemTemplate>
                            <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtAmount" runat="server" Width="80px" CssClass="InputNumber" Text='<%# Bind("Amount") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:Label runat="server" ID="lblTotal"></asp:Label>
                        </FooterTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Right" />
                        <FooterStyle CssClass="RedTextAlignCenter" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateField_AmountRMB">
                        <ItemTemplate>
                            <asp:Label ID="lblAmountRMB" runat="server" Text='<%# Eval("AmountRMB","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtAmountRMB" runat="server" ReadOnly="true" Width="80px" Text='<%# Bind("AmountRMB","{0:N}") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Right" />
                        <FooterTemplate>
                            <asp:Label runat="server" ID="lblTotalRMB"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle CssClass="RedTextAlignCenter" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Common,Form_Remark %>">
                        <ItemTemplate>
                            <asp:Label ID="lblRemark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtRemark" runat="server" Text='<%# Bind("Remark") %>' Width="260px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle Width="284px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="Edit"
                                Text="<%$Resources:Common,Button_Edit %>"></asp:LinkButton>
                            <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Delete"
                                Text="<%$Resources:Common,Button_Delete %>"></asp:LinkButton>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="Update"
                                Text="<%$Resources:Common,Button_Update %>"></asp:LinkButton>
                            <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel"
                                Text="<%$Resources:Common,Button_Cancel %>"></asp:LinkButton>
                        </EditItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <FooterStyle Width="50px" Wrap="True" />
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td style="width: 250px;" class="Empty1">
                                <asp:Label ID="Label1" runat="server" Text="<%$Resources:Common,Form_VendorName %>" />
                            </td>
                            <td style="width: 200px;">
                                <asp:Label ID="Label3" runat="server" Text="<%$Resources:Common,Form_ExpenseItem %>" />
                            </td>
                            <td style="width: 200px;">
                                <asp:Label ID="Label4" runat="server" Text="<%$Resources:Common,Form_ProductName %>" />
                            </td>
                            <td style="width: 100px;">
                                <asp:Label ID="Label5" runat="server" meta:resourcekey="Label_Amount" />
                            </td>
                            <td style="width: 100px;">
                                <asp:Label ID="Label6" runat="server" meta:resourcekey="Label_AmountRMB" />
                            </td>
                            <td style="width: 284px;">
                                <asp:Label ID="Label7" runat="server" Text="<%$Resources:Common,Form_Remark %>" />
                            </td>
                            <td style="width: 100px">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
            </gc:GridView>
            <asp:FormView ID="fvDetails" runat="server" DataKeyNames="FormActivityExpenseDetailID"
                DataSourceID="odsDetails" DefaultMode="Insert" CellPadding="0">
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr>
                            <td style="width: 250px;" align="center">
                                <uc4:UCVendor ID="UCVendor" runat="server" Width="170px" IsNoClear="true" VendorID='<%# Bind("VendorID") %>' IsLimited="true" />
                            </td>
                            <td style="width: 200px;" align="center">
                                <asp:DropDownList ID="ddlExpenseItem" runat="server" DataSourceID="sdsExpenseItem"
                                    DataTextField="ExpenseItemName" DataValueField="ExpenseItemID" SelectedValue='<%# Bind("ExpenseItemID") %>'
                                    Width="180px">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="sdsExpenseItem" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                    SelectCommand="select ExpenseItemID,ExpenseItemName from ExpenseItem join ExpenseSubCategory on ExpenseItem.ExpenseSubCategoryID=ExpenseSubCategory.ExpenseSubCategoryID where ExpenseSubCategory.ExpenseCategoryID = @ExpenseCategoryID and ExpenseSubCategory.PageType<>43 order by ExpenseItemName">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="ExpenseCategoryID" Name="ExpenseCategoryID" PropertyName="Value"
                                            Type="Int32" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </td>
                            <td style="width: 200px;" align="center">
                                <asp:DropDownList ID="ddlSKUByAdd" runat="server" DataSourceID="sdsSKUByAdd" DataTextField="SKUName"
                                    DataValueField="SKUID" SelectedValue='<%# Bind("SKUID") %>' Width="180px">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="sdsSKUByAdd" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                    SelectCommand="SELECT [SKUID], [SKUName]+'-'+[SKUNo] SKUName FROM [SKU] where IsActive = 1 and BrandID = @BrandID order by SKUName">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="hdnBrandID" Name="BrandID" PropertyName="Value"
                                            Type="Int32" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </td>
                            <td style="width: 100px;" align="center" valign="top">
                                <asp:TextBox ID="newAmountCtl" runat="server" CssClass="InputNumber" Text='<%# Bind("Amount") %>'
                                    Width="80px"></asp:TextBox>
                            </td>
                            <td style="width: 100px;" align="center" valign="top">
                                <asp:TextBox ID="newAmountRMBCtl" runat="server" ReadOnly="true" Width="80px"></asp:TextBox>
                            </td>
                            <td style="width: 284px;" align="center">
                                <asp:TextBox ID="newRemarkCtl" MaxLength="20" runat="server" Text='<%# Bind("Remark") %>'
                                    Width="260px"></asp:TextBox>
                            </td>
                            <td style="width: 100px;" align="center">
                                <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    Text="<%$Resources:Common,Button_Add %>" ValidationGroup="NewExpenseDetailRow"></asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <asp:RequiredFieldValidator ID="RF1" runat="server" ControlToValidate="newAmountCtl"
                                Display="None" meta:resourcekey="RequiredFieldValidator_RF1" ValidationGroup="NewExpenseDetailRow"
                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RF3" runat="server" ControlToValidate="newAmountCtl"
                                ValidationExpression="<%$ Resources:RegularExpressions, Money %>" Display="None"
                                meta:resourcekey="RequiredFieldValidator_RF3" ValidationGroup="NewExpenseDetailRow"></asp:RegularExpressionValidator>
                            <asp:ValidationSummary ID="ValidationSummaryINS" runat="server" ShowMessageBox="True"
                                ShowSummary="False" ValidationGroup="NewExpenseDetailRow" />
                        </tr>
                    </table>
                </InsertItemTemplate>
            </asp:FormView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="odsDetails" runat="server" DeleteMethod="DeleteFormMarketingApplyDetailByID"
        SelectMethod="GetFormMarketingApplyDetail" TypeName="BusinessObjects.FormMarketingBLL"
        OnInserting="odsDetails_Inserting" OnObjectCreated="odsDetails_ObjectCreated"
        OnUpdating="odsDetails_Updating" InsertMethod="AddFormMarketingApplyDetail" UpdateMethod="UpdateFormMarketingApplyDetail">
        <InsertParameters>
            <asp:Parameter Name="FormMarketingApplyID" Type="Int32" />
        </InsertParameters>
    </asp:ObjectDataSource>
    <br />
    <asp:UpdatePanel ID="upButton" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div style="margin-top: 20px; width: 1200px; text-align: right">
                <asp:Button ID="SubmitBtn" runat="server" CssClass="button_nor" OnClick="SubmitBtn_Click"
                    Text="<%$Resources:Common,Button_Submit %>" />
                <asp:Button ID="SaveBtn" runat="server" CssClass="button_nor" OnClick="SaveBtn_Click"
                    Text="<%$Resources:Common,Button_Save %>" />
                <asp:Button ID="CancelBtn" runat="server" CssClass="button_nor" OnClick="CancelBtn_Click"
                    Text="<%$Resources:Common,Button_Back %>" />
                <asp:Button ID="DeleteBtn" runat="server" CssClass="button_nor" Visible="false" OnClick="DeleteBtn_Click"
                    Text="<%$Resources:Common,Button_Delete %>" />
            </div>
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
    <uc3:ucUpdateProgress ID="upUP" runat="server" vAssociatedUpdatePanelID="upButton" />
</asp:Content>
