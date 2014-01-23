<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ActivityApply.aspx.cs" UICulture="Auto" Culture="Auto" Inherits="FormSale_ActivityApply" %>

<%@ Register Src="../UserControls/UCDateInput.ascx" TagName="UCDateInput" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/MultiAttachmentFile.ascx" TagName="UCFlie" TagPrefix="uc2" %>
<%@ Register Src="../UserControls/ucUpdateProgress.ascx" TagName="ucUpdateProgress"
    TagPrefix="uc3" %>
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
                        <asp:Label ID="Form_ApplyUser" runat="server" Text="<%$Resources:Common,Form_ApplyUser %>" />
                    </div>
                    <div>
                        <asp:TextBox ID="StuffNameCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_Position" runat="server" Text="<%$Resources:Common,Form_Position %>" />
                    </div>
                    <div>
                        <asp:TextBox ID="PositionNameCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_Organization" runat="server" Text="<%$Resources:Common,Form_Organization %>" />
                    </div>
                    <div>
                        <asp:TextBox ID="DepartmentNameCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label_StuffNo" runat="server" Text="<%$Resources:Common,Form_StaffNo %>" />
                    </div>
                    <div>
                        <asp:TextBox ID="StuffNoCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_AttendDate" runat="server" Text="<%$Resources:Common,Form_AttendDate %>" />
                    </div>
                    <div>
                        <asp:TextBox ID="AttendDateCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_Period" runat="server" Text="<%$Resources:Common,Form_Period %>" />
                    </div>
                    <div>
                        <asp:TextBox ID="PeriodCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label6" runat="server" Text="<%$Resources:Common,Label_CustomerNo %>" />
                    </div>
                    <div>
                        <asp:TextBox ID="txtCustomerCode" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label_Customer" runat="server" Text="<%$Resources:Common,Form_Customer %>" />
                    </div>
                    <div>
                        <asp:TextBox ID="CustomerNameCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_CustomerChannel" runat="server" Text="<%$Resources:Common,Form_CustomerChannel %>" />
                    </div>
                    <div>
                        <asp:TextBox ID="CustomerChannelCtl" runat="server" CssClass="InputTextReadOnly"
                            ReadOnly="true" Width="170px"></asp:TextBox></div>
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
                        <asp:TextBox ID="CustomerRegionCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_City" runat="server" Text="<%$Resources:Common,Form_City %>" />
                    </div>
                    <div>
                        <asp:TextBox ID="CityCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox></div>
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label_Brand" runat="server" Text="<%$Resources:Common,Form_Brand %>" />
                    </div>
                    <div>
                        <asp:TextBox ID="BrandCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
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
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label_ExpenseSubCategory" runat="server" Text="<%$Resources:Common,Form_ExpenseSubCategory %>" />
                    </div>
                    <div>
                        <asp:TextBox ID="ExpenseSubCategoryCtl" runat="server" CssClass="InputTextReadOnly"
                            ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label_Currency" runat="server" Text="<%$Resources:Common,Form_Currency %>" />
                    </div>
                    <div>
                        <asp:TextBox ID="CurrencyCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label_ExchangeRate" runat="server" Text="<%$Resources:Common,Form_ExchangeRate %>" />
                    </div>
                    <div>
                        <asp:TextBox ID="ExchangeRateCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
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
                        <asp:Label ID="Label_ShopNameCtl" runat="server" meta:resourcekey="Label_ShopNameCtl" />
                        <span class="requiredLable">*</span>
                    </div>
                    <div>
                        <asp:TextBox ID="ShopNameCtl" runat="server" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label_ShopCountCtl" runat="server" meta:resourcekey="Label_ShopCountCtl" />
                        <span class="requiredLable">*</span>
                    </div>
                    <div>
                        <asp:TextBox ID="ShopCountCtl" runat="server" Width="170px"></asp:TextBox></div>
                </td>
                <td valign="top" colspan="2" style="width: 400px">
                    <div class="field_title">
                        <asp:Label ID="Form_ProjectName" runat="server" Text="<%$Resources:Common,Form_ProjectName %>" />
                        <span class="requiredLable">*</span>
                    </div>
                    <div>
                        <asp:TextBox ID="ProjectNameCtl" runat="server" Width="370px"></asp:TextBox></div>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td colspan="3" valign="top">
                    <div class="field_title">
                        <asp:Label ID="Label_ProjectDescCtl" runat="server" meta:resourcekey="Label_ProjectDescCtl" />
                    </div>
                    <div>
                        <asp:TextBox ID="ProjectDescCtl" runat="server" CssClass="InputText" TextMode="multiline"
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
    <div class="title">
        <asp:Label ID="Label8" runat="server" meta:resourcekey="Label_Title1" />
    </div>
    <div class="searchDiv">
        <table class="searchTable">
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label9" runat="server" meta:resourcekey="Label_DisplayTypeDDL" />
                    </div>
                    <div>
                        <asp:DropDownList ID="DisplayTypeDDL" runat="server" DataSourceID="odsDisplayType"
                            DataTextField="DisplayTypeName" DataValueField="DisplayTypeID" Width="170px">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="odsDisplayType" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                            SelectCommand=" SELECT [DisplayTypeID], [DisplayTypeName] FROM [DisplayType] where IsActive = 1 ">
                        </asp:SqlDataSource>
                    </div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label10" runat="server" meta:resourcekey="Label_DisplayAreaCtl" />
                    </div>
                    <div>
                        <asp:TextBox ID="DisplayAreaCtl" runat="server" MaxLength="400" CssClass="InputText"
                            Width="170px"></asp:TextBox>
                    </div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label11" runat="server" meta:resourcekey="Label_DMDDL" />
                    </div>
                    <div>
                        <asp:DropDownList ID="DMDDL" runat="server" Width="170px">
                            <asp:ListItem Text="Yes" Selected="True" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label12" runat="server" meta:resourcekey="Label_DiscountTypeDDL" />
                    </div>
                    <div>
                        <asp:DropDownList ID="DiscountTypeDDL" runat="server" DataSourceID="odsDiscountType"
                            DataTextField="DiscountTypeName" DataValueField="DiscountTypeID" Width="170px">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="odsDiscountType" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                            SelectCommand=" SELECT [DiscountTypeID], [DiscountTypeName] FROM [DiscountType] where IsActive = 1 ">
                        </asp:SqlDataSource>
                    </div>
                </td>
                <td colspan="2" style="width: 400px">
                    <div class="field_title">
                        <asp:Label ID="Label13" runat="server" meta:resourcekey="Label_UCActivityBegin" />
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
                <td colspan="2" style="width: 400px">
                    <div class="field_title">
                        <asp:Label ID="Label14" runat="server" meta:resourcekey="Label_UCDeliveryBegin" />
                        <span class="requiredLable">*</span>
                    </div>
                    <div>
                        <uc1:UCDateInput ID="UCDeliveryBegin" runat="server" IsReadOnly="false" />
                        <asp:Label ID="Label3" runat="server">~~</asp:Label>
                        <uc1:UCDateInput ID="UCDeliveryEnd" runat="server" IsReadOnly="false" />
                    </div>
                </td>
                <td style="width: 200px">
                </td>
                <td style="width: 200px">
                </td>
                <td style="width: 200px">
                </td>
                <td style="width: 200px">
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div class="title" id="divBudgetInfoTitle" runat="server">
        <asp:Label ID="Form_BudgetTitle" runat="server" Text="<%$Resources:Common,Form_BudgetTitle %>" />
    </div>
    <div class="searchDiv" id="divBudgetInfo" runat="server">
        <table class="searchTable">
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label16" runat="server" meta:resourcekey="Label_TotalBudgetCtl" />
                    </div>
                    <div>
                        <asp:TextBox ID="TotalBudgetCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px;">
                    <div class="field_title">
                        <asp:Label ID="Label17" runat="server" meta:resourcekey="Label_ApprovedAmountCtl" />
                    </div>
                    <div>
                        <asp:TextBox ID="ApprovedAmountCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label18" runat="server" meta:resourcekey="Label_ApprovingAmountCtl" />
                    </div>
                    <div>
                        <asp:TextBox ID="ApprovingAmountCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px; display: none;">
                    <div class="field_title">
                        <asp:Label ID="Label19" runat="server" meta:resourcekey="Label_CompletedAmountCtl" />
                    </div>
                    <div>
                        <asp:TextBox ID="CompletedAmountCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px; display: none;">
                    <div class="field_title">
                        <asp:Label ID="Label20" runat="server" meta:resourcekey="Label_ReimbursedAmountCtl" />
                    </div>
                    <div>
                        <asp:TextBox ID="ReimbursedAmountCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label21" runat="server" meta:resourcekey="Label_RemainBudgetCtl" />
                    </div>
                    <div>
                        <asp:TextBox ID="RemainBudgetCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div class="title">
        <asp:Label ID="Label22" runat="server" Text="<%$ Resources:Common,Label_ProductTitle %>" />
    </div>
    <asp:HiddenField ID="BrandID" runat="server" Visible="False" />
    <asp:UpdatePanel ID="upDetails" runat="server">
        <ContentTemplate>
            <gc:GridView ID="gvSKUDetails" runat="server" ShowFooter="True" CssClass="GridView"
                AutoGenerateColumns="False" DataKeyNames="FormSaleSKUDetailID" DataSourceID="odsSKUDetails"
                OnRowDataBound="gvSKUDetails_RowDataBound" CellPadding="0" OnDataBound="gvSKUDetails_OnDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="<%$Resources:Common,Form_ProductName %>">
                        <ItemTemplate>
                            <asp:Label ID="lblProductName" runat="server" Text='<%# GetProductNameByID(Eval("SKUID")) %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList runat="server" ID="SKUDDL" DataTextField="SKUName" DataValueField="SKUID"
                                DataSourceID="odsSKU" Width="270px" SelectedValue='<%# Bind("SKUID") %>'>
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
                    <asp:TemplateField meta:resourcekey="TemplateField_lblDiscount">
                        <ItemTemplate>
                            <asp:Label ID="lblDiscount" runat="server" Text='<%# Eval("Discount","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="DiscountCtl" runat="server" Text='<%# Bind("Discount") %>' Width="50px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle Width="70px" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateField_lblDiscountCampbell">
                        <ItemTemplate>
                            <asp:Label ID="lblDiscountCampbell" runat="server" Text='<%# Eval("DiscountCampbell","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="DiscountCampbellCtl" runat="server" Text='<%# Bind("DiscountCampbell") %>'
                                Width="50px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle Width="70px" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateField_lblForecastSaleQuantity">
                        <ItemTemplate>
                            <asp:Label ID="lblForecastSaleQuantity" runat="server" Text='<%# Eval("ForecastSaleQuantity","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="ForecastSaleQuantityCtl" runat="server" Text='<%# Bind("ForecastSaleQuantity") %>'
                                Width="80px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateField_lblForecastOrderQuantity">
                        <ItemTemplate>
                            <asp:Label ID="lblForecastOrderQuantity" runat="server" Text='<%# Eval("ForecastOrderQuantity","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Right" />
                        <EditItemTemplate>
                            <asp:TextBox ID="ForecastOrderQuantityCtl" runat="server" Text='<%# Bind("ForecastOrderQuantity") %>'
                                Width="80px"></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:Label runat="server" ID="lblSum" Text="<%$Resources:Common,Form_Total %>"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle CssClass="RedTextAlignCenter" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateField_lblForecastOrderAmount">
                        <ItemTemplate>
                            <asp:Label ID="lblForecastOrderAmount" runat="server" Text='<%# Eval("ForecastOrderAmount","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="ForecastOrderAmountCtl" runat="server" ReadOnly="true" Width="80px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Right" />
                        <FooterTemplate>
                            <asp:Label runat="server" ID="lblOrderAmountTotal"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle CssClass="RedTextAlignCenter" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateField_lblPriceDiscountAmount">
                        <ItemTemplate>
                            <asp:Label ID="lblPriceDiscountAmount" runat="server" Text='<%# Eval("PriceDiscountAmount","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="PriceDiscountAmountCtl" runat="server" Text='<%# Bind("PriceDiscountAmount") %>'
                                Width="90px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Right" />
                        <FooterTemplate>
                            <asp:Label runat="server" ID="lblPriceDiscountTotal"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle CssClass="RedTextAlignCenter" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateField_lblPriceDiscountAmountRMB">
                        <ItemTemplate>
                            <asp:Label ID="lblPriceDiscountAmountRMB" runat="server" Text='<%# Eval("PriceDiscountAmountRMB","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="PriceDiscountAmountRMBCtl" runat="server" ReadOnly="true" Width="90px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Right" />
                        <FooterTemplate>
                            <asp:Label runat="server" ID="lblPriceDiscountRMBTotal" Width="100px"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle CssClass="RedTextAlignCenter" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Common,Form_Remark %>">
                        <ItemTemplate>
                            <asp:Label ID="lblRemark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="RemarkCtl" runat="server" Text='<%# Bind("Remark") %>' Width="210px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle Width="240px" HorizontalAlign="Center" />
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
                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <FooterStyle Width="50px" Wrap="True" />
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td style="width: 300px;" class="Empty1">
                                <asp:Label ID="Label23" runat="server" Text="<%$Resources:Common,Form_ProductName %>"></asp:Label>
                            </td>
                            <td style="width: 70px;">
                                <asp:Label ID="Label1" runat="server" meta:resourcekey="Label_lblDiscount"></asp:Label>
                            </td>
                            <td style="width: 70px;">
                                <asp:Label ID="Label4" runat="server" meta:resourcekey="Label_lblDiscountCampbell"></asp:Label>
                            </td>
                            <td style="width: 100px;">
                                <asp:Label ID="Label24" runat="server" meta:resourcekey="Label_lblForecastSaleQuantity"></asp:Label>
                            </td>
                            <td style="width: 100px;">
                                <asp:Label ID="Label25" runat="server" meta:resourcekey="Label_lblForecastOrderQuantity"></asp:Label>
                            </td>
                            <td style="width: 100px;">
                                <asp:Label ID="Label26" runat="server" meta:resourcekey="Label_lblForecastOrderAmount"></asp:Label>
                            </td>
                            <td style="width: 100px;">
                                <asp:Label ID="Label27" runat="server" meta:resourcekey="Label_lblPriceDiscountAmount"></asp:Label>
                            </td>
                            <td style="width: 100px;">
                                <asp:Label ID="Label5" runat="server" meta:resourcekey="Label_lblPriceDiscountAmountRMB"></asp:Label>
                            </td>
                            <td style="width: 240px;">
                                <asp:Label ID="Label28" runat="server" Text="<%$Resources:Common,Form_Remark %>"></asp:Label>
                            </td>
                            <td style="width: 60px">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
            </gc:GridView>
            <asp:FormView ID="fvSKUDetails" runat="server" DataKeyNames="FormSaleSKUDetailID"
                DataSourceID="odsSKUDetails" DefaultMode="Insert" CellPadding="0">
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr>
                            <td style="width: 300px;" align="center">
                                <asp:DropDownList ID="newSKUDDL" runat="server" DataSourceID="odsNewSKU" DataTextField="SKUName"
                                    DataValueField="SKUID" SelectedValue='<%# Bind("SKUID") %>' Width="280px">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="odsNewSKU" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                    SelectCommand="SELECT [SKUID], [SKUName]+'-'+SKUNo SKUName FROM [SKU] where IsActive = 1 and BrandID = @BrandID order by SKUName">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="BrandID" Name="BrandID" PropertyName="Value" Type="Int32" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </td>
                            <td style="width: 70px;" align="center" valign="top">
                                <asp:TextBox ID="newDiscountCtl" MaxLength="15" runat="server" Text='<%# Bind("Discount") %>'
                                    Width="50px"></asp:TextBox>
                            </td>
                            <td style="width: 70px;" align="center" valign="top">
                                <asp:TextBox ID="newDiscountCampbellCtl" MaxLength="15" runat="server" Text='<%# Bind("DiscountCampbell") %>'
                                    Width="50px"></asp:TextBox>
                            </td>
                            <td style="width: 100px;" align="center" valign="top">
                                <asp:TextBox ID="newForecastSaleQuantityCtl" MaxLength="15" runat="server" Text='<%# Bind("ForecastSaleQuantity") %>'
                                    Width="80px"></asp:TextBox>
                            </td>
                            <td style="width: 100px;" align="center" valign="top">
                                <asp:TextBox ID="newForecastOrderQuantityCtl" MaxLength="15" runat="server" Text='<%# Bind("ForecastOrderQuantity") %>'
                                    Width="80px"></asp:TextBox>
                            </td>
                            <td style="width: 100px;" align="center" valign="top">
                                <asp:TextBox ID="newForecastOrderAmountCtl" MaxLength="15" runat="server" ReadOnly="true"
                                    Width="80px"></asp:TextBox>
                            </td>
                            <td style="width: 100px;" align="center" valign="top">
                                <asp:TextBox ID="newPriceDiscountAmountCtl" MaxLength="15" runat="server" Text='<%# Bind("PriceDiscountAmount") %>'
                                    Width="90px"></asp:TextBox>
                            </td>
                            <td style="width: 100px;" align="center" valign="top">
                                <asp:TextBox ID="newPriceDiscountAmountRMBCtl" MaxLength="15" runat="server" ReadOnly="true"
                                    Width="90px"></asp:TextBox>
                            </td>
                            <td style="width: 240px;" align="center">
                                <asp:TextBox ID="newRemarkCtl" MaxLength="20" runat="server" Text='<%# Bind("Remark") %>'
                                    Width="220px"></asp:TextBox>
                            </td>
                            <td style="width: 60px;" align="center">
                                <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    Text="<%$Resources:Common,Button_Add %>" ValidationGroup="NewDetailRow"></asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <asp:RequiredFieldValidator ID="RF1" runat="server" ControlToValidate="newForecastSaleQuantityCtl"
                                Display="None" meta:resourcekey="RequiredFieldValidator_RF1" ValidationGroup="NewDetailRow"
                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RF2" runat="server" ControlToValidate="newForecastOrderQuantityCtl"
                                Display="None" meta:resourcekey="RequiredFieldValidator_RF2" ValidationGroup="NewDetailRow"
                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RF3" runat="server" ControlToValidate="newForecastSaleQuantityCtl"
                                ValidationExpression="<%$ Resources:RegularExpressions, Money %>" Display="None"
                                meta:resourcekey="RequiredFieldValidator_RF3" ValidationGroup="NewDetailRow"></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="newForecastOrderQuantityCtl"
                                ValidationExpression="<%$ Resources:RegularExpressions, Money %>" Display="None"
                                meta:resourcekey="RequiredFieldValidator_RF4" ValidationGroup="NewDetailRow"></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="newDiscountCtl"
                                ValidationExpression="<%$ Resources:RegularExpressions, Money %>" Display="None"
                                meta:resourcekey="RequiredFieldValidator_RF5" ValidationGroup="NewDetailRow"></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="newDiscountCampbellCtl"
                                ValidationExpression="<%$ Resources:RegularExpressions, Money %>" Display="None"
                                meta:resourcekey="RequiredFieldValidator_RF6" ValidationGroup="NewDetailRow"></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="newPriceDiscountAmountCtl"
                                ValidationExpression="<%$ Resources:RegularExpressions, Money %>" Display="None"
                                meta:resourcekey="RequiredFieldValidator_RF7" ValidationGroup="NewDetailRow"></asp:RegularExpressionValidator>
                            <asp:ValidationSummary ID="ValidationSummaryINS" runat="server" ShowMessageBox="True"
                                ShowSummary="False" ValidationGroup="NewDetailRow" />
                        </tr>
                    </table>
                    <br />
                </InsertItemTemplate>
            </asp:FormView>
            <br />
            <div class="title">
                <asp:Label ID="Label29" runat="server" meta:resourcekey="Label_Title4" />
            </div>
            <asp:HiddenField ID="ExpenseSubCategoryID" runat="server" Visible="False" />
            <gc:GridView ID="gvExpenseDetails" runat="server" ShowFooter="True" CssClass="GridView"
                AutoGenerateColumns="False" DataKeyNames="FormSaleExpenseDetailID" DataSourceID="odsExpenseDetails"
                OnRowDataBound="gvExpenseDetails_RowDataBound" CellPadding="0" OnDataBound="gvExpenseDetails_OnDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="<%$Resources:Common,Form_ExpenseItem %>">
                        <ItemTemplate>
                            <asp:Label ID="lblProductName" runat="server" Text='<%# GetExpenseItemNameByID(Eval("ExpenseItemID")) %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList runat="server" ID="ExpenseItemDDL" DataTextField="ExpenseItemName"
                                DataValueField="ExpenseItemID" DataSourceID="odsExpenseItem" Width="220px" SelectedValue='<%# Bind("ExpenseItemID") %>'>
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="odsExpenseItem" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                SelectCommand="select ExpenseItemID,ExpenseItemName from ExpenseItem where IsActive=1 and IsPriceDiscount = 0 and ExpenseSubCategoryID = @ExpenseSubCategoryID order by ExpenseItemName">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="ExpenseSubCategoryID" Name="ExpenseSubCategoryID"
                                        PropertyName="Value" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </EditItemTemplate>
                        <ItemStyle Width="250px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateField_lblShopName">
                        <ItemTemplate>
                            <asp:Label ID="lblShopName" runat="server" Text='<%# Eval("ShopName") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtShopName" runat="server" Text='<%# Bind("ShopName") %>' Width="270px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle Width="300px" HorizontalAlign="Center" />
                        <FooterTemplate>
                            <asp:Label runat="server" ID="lblSum" Text="<%$Resources:Common,Form_Total %>"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle CssClass="RedTextAlignCenter" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateField_lblAmount">
                        <ItemTemplate>
                            <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtAmount" runat="server" Text='<%# Bind("Amount") %>' Width="80px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Right" />
                        <FooterTemplate>
                            <asp:Label runat="server" ID="lblOtherTotal"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle CssClass="RedTextAlignCenter" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateField_lblAmountRMB">
                        <ItemTemplate>
                            <asp:Label ID="lblAmountRMB" runat="server" Text='<%# Eval("AmountRMB","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtAmountRMB" runat="server" ReadOnly="true" Width="80px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Right" />
                        <FooterTemplate>
                            <asp:Label runat="server" ID="lblOtherTotalRMB"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle CssClass="RedTextAlignCenter" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Common,Form_Remark %>">
                        <ItemTemplate>
                            <asp:Label ID="lblRemark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtRemark" runat="server" Text='<%# Bind("Remark") %>' Width="350px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle Width="380px" HorizontalAlign="Center" />
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
                                <asp:Label ID="Label5" runat="server" Text="<%$Resources:Common,Form_ExpenseItem %>"></asp:Label>
                            </td>
                            <td style="width: 300px;">
                                <asp:Label ID="Label30" runat="server" meta:resourcekey="Label_ShopNameCtl"></asp:Label>
                            </td>
                            <td style="width: 100px;">
                                <asp:Label ID="Label31" runat="server" meta:resourcekey="Label_lblAmount"></asp:Label>
                            </td>
                            <td style="width: 100px;">
                                <asp:Label ID="Label32" runat="server" meta:resourcekey="Label_lblAmountRMB"></asp:Label>
                            </td>
                            <td style="width: 380px;">
                                <asp:Label ID="Label33" runat="server" Text="<%$Resources:Common,Form_Remark %>"></asp:Label>
                            </td>
                            <td style="width: 100px">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
            </gc:GridView>
            <asp:FormView ID="fvExpenseDetails" runat="server" DataKeyNames="FormSaleExpenseDetailID"
                DataSourceID="odsExpenseDetails" DefaultMode="Insert" CellPadding="0">
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr>
                            <td style="width: 250px;" align="center">
                                <asp:DropDownList ID="newExpenseItemDDL" runat="server" DataSourceID="odsNewExpenseItem"
                                    DataTextField="ExpenseItemName" DataValueField="ExpenseItemID" SelectedValue='<%# Bind("ExpenseItemID") %>'
                                    Width="220px">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="odsNewExpenseItem" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                    SelectCommand="SELECT [ExpenseItemID], [ExpenseItemName] FROM [ExpenseItem] where IsActive = 1 and IsPriceDiscount = 0 and ExpenseSubCategoryID = @ExpenseSubCategoryID order by ExpenseItemName">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="ExpenseSubCategoryID" Name="ExpenseSubCategoryID"
                                            PropertyName="Value" Type="Int32" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </td>
                            <td style="width: 300px;" align="center" valign="top">
                                <asp:TextBox ID="newShopNameCtl" runat="server" Text='<%# Bind("ShopName") %>' Width="270px"></asp:TextBox>
                            </td>
                            <td style="width: 100px;" align="center" valign="top">
                                <asp:TextBox ID="newAmountCtl" runat="server" Text='<%# Bind("Amount") %>' Width="80px"></asp:TextBox>
                            </td>
                            <td style="width: 100px;" align="center" valign="top">
                                <asp:TextBox ID="newAmountRMBCtl" runat="server" ReadOnly="true" Width="80px"></asp:TextBox>
                            </td>
                            <td style="width: 380px;" align="center">
                                <asp:TextBox ID="newRemarkCtl" MaxLength="20" runat="server" Text='<%# Bind("Remark") %>'
                                    Width="350px"></asp:TextBox>
                            </td>
                            <td style="width: 100px;" align="center">
                                <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    Text="<%$Resources:Common,Button_Add %>" ValidationGroup="NewExpenseDetailRow"></asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <asp:RequiredFieldValidator ID="RF1" runat="server" ControlToValidate="newAmountCtl"
                                Display="None" meta:resourcekey="RequiredFieldValidator_RF8" ValidationGroup="NewExpenseDetailRow"
                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RF3" runat="server" ControlToValidate="newAmountCtl"
                                ValidationExpression="<%$ Resources:RegularExpressions, Money %>" Display="None"
                                meta:resourcekey="RequiredFieldValidator_RF9" ValidationGroup="NewDetailRow"></asp:RegularExpressionValidator>
                            <asp:ValidationSummary ID="ValidationSummaryINS" runat="server" ShowMessageBox="True"
                                ShowSummary="False" ValidationGroup="NewExpenseDetailRow" />
                        </tr>
                    </table>
                </InsertItemTemplate>
            </asp:FormView>
            <br />
            <div class="title">
                <asp:Label ID="Label34" runat="server" meta:resourcekey="Label_Title5" />
            </div>
            <div class="searchDiv">
                <table class="searchTable">
                    <tr>
                        <td style="width: 240px">
                            <div class="field_title">
                                <asp:Label ID="Label35" runat="server" meta:resourcekey="Label_PriceDiscountAmountRMBCtl" />
                            </div>
                            <div>
                                <asp:TextBox ID="PriceDiscountAmountRMBCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                        </td>
                        <td style="width: 240px">
                            <div class="field_title">
                                <asp:Label ID="Label36" runat="server" meta:resourcekey="Label_OtherAmountRMBCtl" />
                            </div>
                            <div>
                                <asp:TextBox ID="OtherAmountRMBCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                        </td>
                        <td style="width: 240px">
                            <div class="field_title">
                                <asp:Label ID="Label37" runat="server" meta:resourcekey="Label_AmountRMBCtl" />
                            </div>
                            <div>
                                <asp:TextBox ID="AmountRMBCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                        </td>
                        <td style="width: 240px">
                            <div class="field_title">
                                <asp:Label ID="Label38" runat="server" meta:resourcekey="Label_ForecastOrderAmountCtl" />
                            </div>
                            <div>
                                <asp:TextBox ID="ForecastOrderAmountCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                        </td>
                        <td style="width: 240px">
                            <div class="field_title">
                                <asp:Label ID="Label39" runat="server" meta:resourcekey="Label_CostBenefitRateCtl" />
                            </div>
                            <div>
                                <asp:TextBox ID="CostBenefitRateCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="odsSKUDetails" runat="server" DeleteMethod="DeleteFormSaleSKUDetailByID"
        SelectMethod="GetFormSaleSKUDetail" TypeName="BusinessObjects.FormSaleBLL" OnInserting="odsSKUDetails_Inserting"
        OnUpdating="odsSKUDetails_Updating" OnObjectCreated="odsSKUDetails_ObjectCreated"
        InsertMethod="AddFormSaleSKUDetail" UpdateMethod="UpdateFormSaleSKUDetail">
        <DeleteParameters>
            <asp:Parameter Name="FormSaleSKUDetailID" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="FormSaleApplyID" Type="Int32" />
            <asp:Parameter Name="SKUID" Type="Int32" />
            <asp:Parameter Name="Discount" Type="Decimal" />
            <asp:Parameter Name="DiscountCampbell" Type="Decimal" />
            <asp:Parameter Name="ForecastSaleQuantity" Type="Decimal" />
            <asp:Parameter Name="ForecastOrderQuantity" Type="Decimal" />
            <asp:Parameter Name="DeliveryPrice" Type="Decimal" />
            <asp:Parameter Name="ForecastOrderAmount" Type="Decimal" />
            <asp:Parameter Name="PriceDiscountAmount" Type="Decimal" />
            <asp:Parameter Name="PriceDiscountAmountRMB" Type="Decimal" />
            <asp:Parameter Name="Remark" Type="String" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="FormSaleSKUDetailID" Type="Int32" />
            <asp:Parameter Name="SKUID" Type="Int32" />
            <asp:Parameter Name="Discount" Type="Decimal" />
            <asp:Parameter Name="DiscountCampbell" Type="Decimal" />
            <asp:Parameter Name="ForecastSaleQuantity" Type="Decimal" />
            <asp:Parameter Name="ForecastOrderQuantity" Type="Decimal" />
            <asp:Parameter Name="DeliveryPrice" Type="Decimal" />
            <asp:Parameter Name="ForecastOrderAmount" Type="Decimal" />
            <asp:Parameter Name="PriceDiscountAmount" Type="Decimal" />
            <asp:Parameter Name="PriceDiscountAmountRMB" Type="Decimal" />
            <asp:Parameter Name="Remark" Type="String" />
        </UpdateParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsExpenseDetails" runat="server" DeleteMethod="DeleteFormSaleExpenseDetailByID"
        SelectMethod="GetFormSaleExpenseDetail" TypeName="BusinessObjects.FormSaleBLL"
        OnInserting="odsExpenseDetails_Inserting" OnObjectCreated="odsExpenseDetails_ObjectCreated"
        OnUpdating="odsExpenseDetails_Updating" InsertMethod="AddFormSaleExpenseDetail"
        UpdateMethod="UpdateFormSaleExpenseDetail">
        <DeleteParameters>
            <asp:Parameter Name="FormSaleExpenseDetailID" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="FormSaleApplyID" Type="Int32" />
            <asp:Parameter Name="ExpenseItemID" Type="Int32" />
            <asp:Parameter Name="ShopName" Type="String" />
            <asp:Parameter Name="Amount" Type="Decimal" />
            <asp:Parameter Name="AmountRMB" Type="Decimal" />
            <asp:Parameter Name="Remark" Type="String" />
            <asp:Parameter Name="SKUID" Type="Int32" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="FormSaleExpenseDetailID" Type="Int32" />
            <asp:Parameter Name="ExpenseItemID" Type="Int32" />
            <asp:Parameter Name="ShopName" Type="String" />
            <asp:Parameter Name="Amount" Type="Decimal" />
            <asp:Parameter Name="AmountRMB" Type="Decimal" />
            <asp:Parameter Name="Remark" Type="String" />
            <asp:Parameter Name="SKUID" Type="Int32" />
        </UpdateParameters>
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
                    Text="<%$Resources:Common,Button_Cancel %>" />
                <asp:Button ID="DeleteBtn" runat="server" CssClass="button_nor" OnClick="DeleteBtn_Click"
                    Text="<%$Resources:Common,Button_Delete %>" />
            </div>
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
    <uc3:ucUpdateProgress ID="upUP" runat="server" vAssociatedUpdatePanelID="upButton" />
</asp:Content>
