<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="SaleSampleRequestApply.aspx.cs" Inherits="SampleRequest_SaleSampleRequestApply"
    Culture="auto" UICulture="auto" %>

<%@ Register Src="../UserControls/UCDateInput.ascx" TagName="UCDateInput" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/MultiAttachmentFile.ascx" TagName="UCFlie" TagPrefix="uc2" %>
<%@ Register Src="../UserControls/ucUpdateProgress.ascx" TagName="ucUpdateProgress"
    TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <script src="../Script/DateInput.js" type="text/javascript"></script>
    <div class="title">
        <asp:Label ID="Label_Title" runat="server" meta:resourcekey="Label_TitleResource1" />
    </div>
    <div class="searchDiv">
        <table class="searchTable">
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_ApplyUser" runat="server" Text="<%$ Resources:Common,Form_ApplyUser %>" /></div>
                    <div>
                        <asp:TextBox ID="StuffNameCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="True"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_Position" runat="server" Text="<%$ Resources:Common,Form_Position %>" /></div>
                    <div>
                        <asp:TextBox ID="PositionNameCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="True"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_Organization" runat="server" Text="<%$ Resources:Common,Form_Organization %>" /></div>
                    <div>
                        <asp:TextBox ID="DepartmentNameCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="True"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label_StuffNo" runat="server" Text="<%$ Resources:Common,Form_StaffNo %>" /></div>
                    <div>
                        <asp:TextBox ID="StuffNoCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="True"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_AttendDate" runat="server" Text="<%$ Resources:Common,Form_AttendDate %>" /></div>
                    <div>
                        <asp:TextBox ID="AttendDateCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="True"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_Period" runat="server" Text="<%$ Resources:Common,Form_Period %>" /></div>
                    <div>
                        <asp:TextBox ID="PeriodCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="True"
                            Width="170px"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Common,Label_CustomerNo %>" /></div>
                    <div>
                        <asp:TextBox ID="txtCustomerCode" runat="server" CssClass="InputTextReadOnly" ReadOnly="True"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label_Customer" runat="server" Text="<%$ Resources:Common,Form_Customer %>" /></div>
                    <div>
                        <asp:TextBox ID="CustomerNameCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="True"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label_CustomerChannel" runat="server" Text="<%$ Resources:Common,Form_CustomerChannel %>" />
                    </div>
                    <div>
                        <asp:TextBox ID="CustomerChannelCtl" runat="server" CssClass="InputTextReadOnly"
                            ReadOnly="True" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label_KATypeCtl" runat="server" Text="<%$Resources:Common,Form_KAType %>" />
                    </div>
                    <div>
                        <asp:TextBox ID="KATypeCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_CustomerRegion" runat="server" Text="<%$Resources:Common,Form_CustomerRegion %>" />
                    </div>
                    <div>
                        <asp:TextBox ID="CustomerRegionCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="True"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_City" runat="server" Text="<%$Resources:Common,Form_City %>" />
                    </div>
                    <div>
                        <asp:TextBox ID="CityCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="True"
                            Width="170px"></asp:TextBox></div>
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label_ExpenseCategory" runat="server" Text="<%$Resources:Common,Form_ExpenseCategory %>" />
                    </div>
                    <div>
                        <asp:TextBox ID="ExpenseCategoryCtl" runat="server" CssClass="InputTextReadOnly"
                            ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label_ExpenseSubCategory" runat="server" Text="<%$ Resources:Common,Form_ExpenseSubCategory %>" /></div>
                    <div>
                        <asp:TextBox ID="ExpenseSubCategoryCtl" runat="server" CssClass="InputTextReadOnly"
                            ReadOnly="True" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label_Brand" runat="server" Text="<%$ Resources:Common,Form_Brand %>" /></div>
                    <div>
                        <asp:TextBox ID="BrandCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="True"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label_Currency" runat="server" Text="<%$ Resources:Common,Form_Currency %>" /></div>
                    <div>
                        <asp:TextBox ID="CurrencyCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="True"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label_ExchangeRate" runat="server" Text="<%$ Resources:Common,Form_ExchangeRate %>" /></div>
                    <div>
                        <asp:TextBox ID="ExchangeRateCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="True"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_CostCenter" runat="server" Text="<%$Resources:Common,Form_CostCenter %>" />
                        <span class="requiredLable">*</span>
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
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label6" runat="server" meta:resourcekey="LabelResource5" />
                        <span class="requiredLable">*</span>
                    </div>
                    <div>
                        <asp:TextBox ID="ShopNameCtl" runat="server" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label7" runat="server" meta:resourcekey="LabelResource6" />
                        <span class="requiredLable">*</span>
                    </div>
                    <div>
                        <asp:TextBox ID="ShopCountCtl" runat="server" Width="170px"></asp:TextBox></div>
                </td>
                <td colspan="2" style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label_ProjectName" runat="server" Text="<%$ Resources:Common,Form_ProjectName %>" />
                        <span class="requiredLable">*</span>
                    </div>
                    <div>
                        <asp:TextBox ID="ProjectNameCtl" runat="server" Width="370px"></asp:TextBox></div>
                </td>
                <td colspan="2" style="width: 400px">
                    <div class="field_title">
                        <asp:Label runat="server" meta:resourcekey="LabelResource8" />
                        <span class="requiredLable">*</span>
                    </div>
                    <div>
                        <uc1:UCDateInput ID="UCActivityBegin" runat="server" IsReadOnly="false" />
                        <asp:Label ID="Label2" runat="server">~~</asp:Label>
                        <uc1:UCDateInput ID="UCActivityEnd" runat="server" IsReadOnly="false" />
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="field_title">
                        <asp:Label ID="Label3" runat="server" meta:resourcekey="LabelResource23" />
                        <span class="requiredLable">*</span>
                    </div>
                    <div>
                        <uc1:UCDateInput ID="ucDeliveryDate" runat="server" IsReadOnly="false" />
                    </div>
                </td>
                <td colspan="2">
                    <div class="field_title">
                        <asp:Label ID="Label4" runat="server" meta:resourcekey="LabelResource24" />
                        <span class="requiredLable">*</span>
                    </div>
                    <div>
                        <asp:TextBox ID="txtDeliveryAddress" runat="server" Width="370px"></asp:TextBox></div>
                </td>
            </tr>
            <tr>
                <td colspan="3" valign="top">
                    <div class="field_title">
                        <asp:Label runat="server" meta:resourcekey="LabelResource9" />
                    </div>
                    <div>
                        <asp:TextBox ID="ProjectDescCtl" runat="server" CssClass="InputText" TextMode="MultiLine"
                            Height="60px" Columns="75"></asp:TextBox>
                    </div>
                </td>
                <td colspan="3" valign="top">
                    <div class="field_title">
                        <asp:Label ID="Form_Adjunct" runat="server" Text="<%$Resources:Common,Form_Adjunct %>" />
                    </div>
                    <uc2:UCFlie ID="UCFileUpload" runat="server" Width="400px" />
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div class="title" id="divBudgetInfoTitle" runat="server">
        <asp:Label ID="Label_Title1" runat="server" Text="<%$Resources:Common,Form_BudgetTitle %>" />
    </div>
    <div class="searchDiv" id="divBudgetInfo" runat="server">
        <table class="searchTable">
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label runat="server" meta:resourcekey="LabelResource11" />
                    </div>
                    <div>
                        <asp:TextBox ID="TotalBudgetCtl" runat="server" ReadOnly="True" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px;">
                    <div class="field_title">
                        <asp:Label runat="server" meta:resourcekey="LabelResource12" />
                    </div>
                    <div>
                        <asp:TextBox ID="ApprovedAmountCtl" runat="server" ReadOnly="True" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label runat="server" meta:resourcekey="LabelResource13" />
                    </div>
                    <div>
                        <asp:TextBox ID="ApprovingAmountCtl" runat="server" ReadOnly="True" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px; display: none;">
                    <div class="field_title">
                        <asp:Label runat="server" meta:resourcekey="LabelResource14" />
                    </div>
                    <div>
                        <asp:TextBox ID="CompletedAmountCtl" runat="server" ReadOnly="True" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px; display: none;">
                    <div class="field_title">
                        <asp:Label runat="server" meta:resourcekey="LabelResource15" />
                    </div>
                    <div>
                        <asp:TextBox ID="ReimbursedAmountCtl" runat="server" ReadOnly="True" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label runat="server" meta:resourcekey="LabelResource16" />
                    </div>
                    <div>
                        <asp:TextBox ID="RemainBudgetCtl" runat="server" ReadOnly="True" Width="170px"></asp:TextBox></div>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div class="title">
        <asp:Label ID="Label_Title2" runat="server" meta:resourcekey="Label_Title2Resource1" />
    </div>
    <asp:HiddenField ID="BrandID" runat="server" Visible="False" />
    <asp:HiddenField ID="ExpenseSubCategoryID" runat="server" Visible="False" />
    <asp:UpdatePanel ID="upDetails" runat="server">
        <ContentTemplate>
            <gc:GridView ID="gvDetails" runat="server" ShowFooter="True" CssClass="GridView"
                AutoGenerateColumns="False" DataKeyNames="FormSaleExpenseDetailID" DataSourceID="odsDetails"
                OnRowDataBound="gvDetails_RowDataBound" CellPadding="0" meta:resourcekey="gvDetailsResource1">
                <Columns>
                    <asp:TemplateField HeaderText="<%$Resources:Common,Form_ExpenseItem %>">
                        <ItemTemplate>
                            <asp:Label ID="lblExpenseItem" runat="server" Text='<%# GetExpenseItemNameByID(Eval("ExpenseItemID")) %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList runat="server" ID="ExpenseItemDDL" DataTextField="ExpenseItemName"
                                DataValueField="ExpenseItemID" DataSourceID="odsExpenseItem" Width="160px" SelectedValue='<%# Bind("ExpenseItemID") %>'>
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="odsExpenseItem" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                SelectCommand="select ExpenseItemID,ExpenseItemName from ExpenseItem where IsActive=1 and IsPriceDiscount = 0 and ExpenseSubCategoryID = @ExpenseSubCategoryID order by ExpenseItemName">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="ExpenseSubCategoryID" Name="ExpenseSubCategoryID"
                                        PropertyName="Value" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </EditItemTemplate>
                        <ItemStyle Width="180px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Common,Form_ProductName %>">
                        <ItemTemplate>
                            <asp:Label ID="lblProduct" runat="server" Text='<%# GetProductNameByID(Eval("SKUID")) %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList runat="server" ID="SKUDDL" DataTextField="SKUName" DataValueField="SKUID"
                                OnSelectedIndexChanged="SKUDDLByEdit_SelectedIndexChanged" AutoPostBack="true"
                                DataSourceID="odsSKU" Width="280px" SelectedValue='<%# Bind("SKUID") %>'>
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="odsSKU" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                SelectCommand="SELECT [SKUID], [SKUName]+'-'+SKUNo SKUName FROM [SKU] where IsActive = 1 and BrandID = @BrandID order by SKUName">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="BrandID" Name="BrandID" PropertyName="Value" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </EditItemTemplate>
                        <ItemStyle Width="300px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateFieldResource5">
                        <ItemTemplate>
                            <asp:Label ID="lblDeliveryPrice" runat="server" Text='<%# Eval("DeliveryPrice","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Right" />
                        <EditItemTemplate>
                            <asp:TextBox ID="txtDeliveryPrice" runat="server" Text='<%# Bind("DeliveryPrice") %>'
                                Width="80px" ReadOnly="true"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateFieldResource6">
                        <ItemTemplate>
                            <asp:Label ID="lblDeliveryQuantity" runat="server" Text='<%# Eval("DeliveryQuantity") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtDeliveryQuantity" runat="server" Text='<%# Bind("DeliveryQuantity") %>'
                                Width="80px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Right" />
                        <FooterTemplate>
                            <asp:Label runat="server" ID="lblSum" Text="<%$Resources:Common,Form_Total %>"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle CssClass="RedTextAlignCenter" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateFieldResource4">
                        <ItemTemplate>
                            <asp:Label ID="lblAmountRMB" runat="server" Text='<%# Eval("AmountRMB","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtAmountRMB" runat="server" ReadOnly="True" Width="80px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Right" />
                        <FooterTemplate>
                            <asp:Label runat="server" ID="lblTotalRMB" meta:resourcekey="lblTotalRMBResource1"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle CssClass="RedTextAlignCenter" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:Common,Form_Remark %>">
                        <ItemTemplate>
                            <asp:Label ID="lblRemark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtRemark" runat="server" Text='<%# Bind("Remark") %>' Width="360px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle Width="380px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit"
                                Text="<%$ Resources:Common,Button_Edit %>"></asp:LinkButton>
                            <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Delete"
                                Text="<%$ Resources:Common,Button_Delete %>"></asp:LinkButton>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="true" ValidationGroup="EDIT"
                                CommandName="Update" Text="<%$ Resources:Common,Button_Update %>"></asp:LinkButton>
                            <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel"
                                Text="<%$ Resources:Common,Button_Cancel %>"></asp:LinkButton>
                            <asp:RequiredFieldValidator ID="RF2" runat="server" ControlToValidate="txtDeliveryQuantity"
                                Display="None" ValidationGroup="EDIT" SetFocusOnError="True" meta:resourcekey="RF2Resource1"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RF4" runat="server" ControlToValidate="txtDeliveryQuantity"
                                ValidationExpression="<%$ Resources:RegularExpressions, Money %>" Display="None"
                                ValidationGroup="EDIT" meta:resourcekey="RF4Resource1"></asp:RegularExpressionValidator>
                            <asp:ValidationSummary ID="ValidationSummaryEdit" runat="server" ShowMessageBox="True"
                                ShowSummary="False" ValidationGroup="EDIT" />
                        </EditItemTemplate>
                        <ItemStyle Width="74px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <FooterStyle Width="50px" Wrap="True" />
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td style="width: 180px;" class="Empty1">
                                <asp:Label runat="server" Text="<%$Resources:Common,Form_ExpenseItem %>" />
                            </td>
                            <td style="width: 300px;">
                                <asp:Label runat="server" Text="<%$Resources:Common,Form_ProductName %>" />
                            </td>
                            <td style="width: 100px;">
                                <asp:Label runat="server" meta:resourcekey="LabelResource21" />
                            </td>
                            <td style="width: 100px;">
                                <asp:Label ID="Label1" runat="server" meta:resourcekey="LabelResource22" />
                            </td>
                            <td style="width: 100px;">
                                <asp:Label runat="server" meta:resourcekey="LabelResource20" />
                            </td>
                            <td style="width: 380px;">
                                <asp:Label runat="server" Text="<%$ Resources:Common,Form_Remark %>" />
                            </td>
                            <td style="width: 74px">
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
                            <td style="width: 180px;" align="center">
                                <asp:DropDownList ID="newExpenseItemDDL" runat="server" DataSourceID="odsNewExpenseItem"
                                    DataTextField="ExpenseItemName" DataValueField="ExpenseItemID" SelectedValue='<%# Bind("ExpenseItemID") %>'
                                    Width="160px">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="odsNewExpenseItem" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                    SelectCommand="SELECT [ExpenseItemID], [ExpenseItemName] FROM [ExpenseItem] where IsActive = 1 and IsPriceDiscount = 0 and ExpenseSubCategoryID = @ExpenseSubCategoryID order by ExpenseItemName">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="ExpenseSubCategoryID" Name="ExpenseSubCategoryID"
                                            PropertyName="Value" Type="Int32" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </td>
                            <td style="width: 300px;" align="center">
                                <asp:DropDownList ID="newSKUDDL" runat="server" DataSourceID="odsNewSKU" DataTextField="SKUName"
                                    OnSelectedIndexChanged="SKUDDLByAdd_SelectedIndexChanged" AutoPostBack="true"
                                    DataValueField="SKUID" SelectedValue='<%# Bind("SKUID") %>' Width="280px">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="odsNewSKU" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                    SelectCommand="SELECT 0 SKUID,' please select' SKUName union SELECT [SKUID], [SKUName]+'-'+SKUNo SKUName FROM [SKU] where IsActive = 1 and BrandID = @BrandID order by SKUName">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="BrandID" Name="BrandID" PropertyName="Value" Type="Int32" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </td>
                            <td style="width: 100px;" align="center" valign="top">
                                <asp:TextBox ID="newDeliveryPrice" runat="server" Text='<%# Bind("DeliveryPrice") %>'
                                    ReadOnly="true" Width="80px"></asp:TextBox>
                            </td>
                            <td style="width: 100px;" align="center" valign="top">
                                <asp:TextBox ID="newDeliveryQuantity" runat="server" Text='<%# Bind("DeliveryQuantity") %>'
                                    Width="80px"></asp:TextBox>
                            </td>
                            <td style="width: 100px;" align="center" valign="top">
                                <asp:TextBox ID="newAmountRMBCtl" runat="server" ReadOnly="True" Width="80px"></asp:TextBox>
                            </td>
                            <td style="width: 380px;" align="center">
                                <asp:TextBox ID="newRemarkCtl" MaxLength="20" runat="server" Text='<%# Bind("Remark") %>'
                                    Width="360px"></asp:TextBox>
                            </td>
                            <td style="width: 74px;" align="center">
                                <asp:LinkButton ID="InsertButton" runat="server" CommandName="insert" Text="<%$Resources:Common,Button_Add %>"
                                    ValidationGroup="INS"></asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <asp:RequiredFieldValidator ID="RF1" runat="server" ControlToValidate="newDeliveryQuantity"
                                Display="None" ValidationGroup="INS" SetFocusOnError="True" meta:resourcekey="RF1Resource1"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RF3" runat="server" ControlToValidate="newDeliveryQuantity"
                                ValidationExpression="<%$ Resources:RegularExpressions, Money %>" Display="None"
                                ValidationGroup="INS" meta:resourcekey="RF3Resource1"></asp:RegularExpressionValidator>
                            <asp:ValidationSummary ID="ValidationSummaryINS" runat="server" ShowMessageBox="True"
                                ShowSummary="False" ValidationGroup="INS" />
                        </tr>
                    </table>
                </InsertItemTemplate>
            </asp:FormView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="odsDetails" runat="server" DeleteMethod="DeleteFormSaleExpenseDetailByID"
        SelectMethod="GetFormSaleExpenseDetail" TypeName="BusinessObjects.FormSampleRequestBLL"
        OnInserting="odsDetails_Inserting" OnObjectCreated="odsDetails_ObjectCreated"
        OnUpdating="odsDetails_Updating" InsertMethod="AddFormSaleExpenseDetail" UpdateMethod="UpdateFormSaleExpenseDetail">
        <DeleteParameters>
            <asp:Parameter Name="FormSaleExpenseDetailID" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="FormSaleApplyID" Type="Int32" />
            <asp:Parameter Name="ExpenseItemID" Type="Int32" />
            <asp:Parameter Name="SKUID" Type="Int32" />
            <asp:Parameter Name="ShopName" Type="String" />
            <asp:Parameter Name="Amount" Type="Decimal" />
            <asp:Parameter Name="AmountRMB" Type="Decimal" />
            <asp:Parameter Name="Remark" Type="String" />
            <asp:Parameter Name="DeliveryPrice" Type="Decimal" />
            <asp:Parameter Name="DeliveryQuantity" Type="Int32" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="FormSaleExpenseDetailID" Type="Int32" />
            <asp:Parameter Name="ExpenseItemID" Type="Int32" />
            <asp:Parameter Name="SKUID" Type="Int32" />
            <asp:Parameter Name="ShopName" Type="String" />
            <asp:Parameter Name="Amount" Type="Decimal" />
            <asp:Parameter Name="AmountRMB" Type="Decimal" />
            <asp:Parameter Name="Remark" Type="String" />
            <asp:Parameter Name="DeliveryPrice" Type="Decimal" />
            <asp:Parameter Name="DeliveryQuantity" Type="Int32" />
        </UpdateParameters>
    </asp:ObjectDataSource>
    <br />
    <asp:UpdatePanel ID="upButton" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div style="margin-top: 20px; width: 1200px; text-align: right">
                <asp:Button ID="SubmitBtn" runat="server" CssClass="button_nor" OnClick="SubmitBtn_Click"
                    Text="<%$ Resources:Common,Button_Submit %>" />
                <asp:Button ID="SaveBtn" runat="server" CssClass="button_nor" OnClick="SaveBtn_Click"
                    Text="<%$ Resources:Common,Button_Save %>" />
                <asp:Button ID="CancelBtn" runat="server" CssClass="button_nor" OnClick="CancelBtn_Click"
                    Text="<%$ Resources:Common,Button_Back %>" />
                <asp:Button ID="DeleteBtn" runat="server" CssClass="button_nor" OnClick="DeleteBtn_Click"
                    Text="<%$ Resources:Common,Button_Delete %>" />
            </div>
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
    <uc3:ucUpdateProgress ID="upUP" runat="server" vAssociatedUpdatePanelID="upButton" />
</asp:Content>
