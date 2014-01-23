<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="PaymentFreeGoodsApproval.aspx.cs" Inherits="FormSale_PaymentFreeGoodsApproval"
    Culture="auto" UICulture="auto" %>

<%@ Register Src="../UserControls/APFlowNodes.ascx" TagName="APFlowNodes" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/MultiAttachmentFile.ascx" TagName="UCFlie" TagPrefix="uc2" %>
<%@ Register Src="../UserControls/ucUpdateProgress.ascx" TagName="ucUpdateProgress"
    TagPrefix="uc3" %>
<%@ Register Src="~/UserControls/PrintReport.ascx" TagName="ucPrint" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <script src="../Script/DateInput.js" type="text/javascript"></script>
    <div class="title">
        <asp:Label runat="server" meta:resourcekey="LabelResource1" /></div>
    <div class="searchDiv">
        <table class="searchTable">
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Common,Form_FormNo %>" /></div>
                    <div>
                        <asp:TextBox ID="FormNoCtl" runat="server" ReadOnly="True" Width="170px"></asp:TextBox></div>
                </td>
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
                        <asp:Label ID="Form_AttendDate" runat="server" Text="<%$ Resources:Common,Form_AttendDate %>" /></div>
                    <div>
                        <asp:TextBox ID="AttendDateCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="True"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label_Brand" runat="server" Text="<%$ Resources:Common,Form_Brand %>" /></div>
                    <div>
                        <asp:TextBox ID="BrandCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="True"
                            Width="170px"></asp:TextBox></div>
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label_Customer" runat="server" Text="<%$ Resources:Common,Form_Customer %>" /></div>
                    <div>
                        <asp:TextBox ID="CustomerNameCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="True"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label_CustomerChannel" runat="server" Text="<%$ Resources:Common,Form_CustomerChannel %>" /></div>
                    <div>
                        <asp:TextBox ID="CustomerChannelCtl" runat="server" CssClass="InputTextReadOnly"
                            ReadOnly="True" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_CustomerRegion" runat="server" Text="<%$ Resources:Common,Form_CustomerRegion %>" /></div>
                    <div>
                        <asp:TextBox ID="CustomerRegionCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="True"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_City" runat="server" Text="<%$ Resources:Common,Form_City %>" /></div>
                    <div>
                        <asp:TextBox ID="CityCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="True"
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
                        <asp:Label ID="Label_ExpenseSubCategory" runat="server" Text="<%$ Resources:Common,Form_ExpenseSubCategory %>" /></div>
                    <div>
                        <asp:TextBox ID="ExpenseSubCategoryCtl" runat="server" CssClass="InputTextReadOnly"
                            ReadOnly="True" Width="170px"></asp:TextBox></div>
                </td>
            </tr>
            <tr id="FeeSumTR" runat="server">
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label2" runat="server" meta:resourcekey="Label2Resource1" /></div>
                    <div>
                        <asp:TextBox ID="ApplyAmountRMBCtl" runat="server" ReadOnly="True" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label3" runat="server" meta:resourcekey="Label3Resource1" /></div>
                    <div>
                        <asp:TextBox ID="ForecastOrderAmountCtl" runat="server" ReadOnly="True" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label4" runat="server" meta:resourcekey="Label4Resource1" /></div>
                    <div>
                        <asp:TextBox ID="CostBenefitRateCtl" runat="server" ReadOnly="True" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label5" runat="server" meta:resourcekey="Label5Resource1" /></div>
                    <div>
                        <asp:TextBox ID="AmountRMBCtl" runat="server" ReadOnly="True" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label6" runat="server" meta:resourcekey="Label6Resource1" /></div>
                    <div>
                        <asp:TextBox ID="ActualOrderAmountCtl" runat="server" ReadOnly="True" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label7" runat="server" meta:resourcekey="Label7Resource1" /></div>
                    <div>
                        <asp:TextBox ID="ActualCostBenefitRateCtl" runat="server" ReadOnly="True" Width="170px"></asp:TextBox></div>
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label_Currency" runat="server" Text="<%$ Resources:Common,Form_Currency %>" /></div>
                    <div>
                        <asp:TextBox ID="CurrencyCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="True"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_CostCenter" runat="server" Text="<%$ Resources:Common,Form_CostCenter %>" /></div>
                    <div>
                        <asp:TextBox ID="CostCenterCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="True"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_PaymentType" runat="server" Text="<%$ Resources:Common,Form_PaymentType %>" /></div>
                    <div>
                        <asp:TextBox ID="PaymentTypeCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="True"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px" valign="top">
                    <div class="field_title">
                        <asp:Label ID="Form_VendorName" Text="<%$Resources:Common,Form_VendorName %>" runat="server" /></div>
                    <div>
                        <asp:TextBox ID="VendorCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox>
                    </div>
                </td>
                <td style="width: 200px" valign="top">
                    <div class="field_title">
                        <asp:Label ID="Label8" runat="server" meta:resourcekey="Label8Resource1" /></div>
                    <div style="margin-top: 5px">
                        <asp:HyperLink ID="SettlementFormNoCtl" runat="server">[SettlementFormNoCtl]</asp:HyperLink></div>
                </td>
                <td valign="top">
                    <div class="field_title">
                        <asp:Label ID="Label9" runat="server" Text="<%$ Resources:Common,Form_RejectFormNo %>" /></div>
                    <div style="margin-top: 5px">
                        <asp:HyperLink ID="lblRejectFormNo" runat="server"></asp:HyperLink></div>
                </td>
            </tr>
            <tr>
                <td colspan="3" valign="top">
                    <div class="field_title">
                        <asp:Label ID="Label10" runat="server" meta:resourcekey="Label10Resource1" /></div>
                    <div>
                        <asp:TextBox ID="SettlementRemarkCtl" runat="server" CssClass="InputTextReadOnly"
                            ReadOnly="True" TextMode="MultiLine" Height="60px" Columns="75"></asp:TextBox>
                    </div>
                </td>
                <td colspan="3" valign="top">
                    <div class="field_title">
                        <asp:Label ID="Label11" runat="server" meta:resourcekey="Label11Resource1" /></div>
                    <uc2:UCFlie ID="UCSettlementFile" runat="server" Width="400px" IsView="true" />
                </td>
            </tr>
            <tr>
                <td colspan="3" valign="top">
                    <div class="field_title">
                        <asp:Label ID="Label12" runat="server" Text="<%$ Resources:Common,Form_Remark %>" /></div>
                    <div>
                        <asp:TextBox ID="RemarkCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="True"
                            TextMode="MultiLine" Height="60px" Columns="75"></asp:TextBox>
                    </div>
                </td>
                <td colspan="3" valign="top">
                    <div class="field_title">
                        <asp:Label ID="Label13" runat="server" meta:resourcekey="Label13Resource1" /></div>
                    <uc2:UCFlie ID="UCPaymentFile" runat="server" Width="400px" IsView="true" />
                </td>
            </tr>
        </table>
    </div>
    <div id="SKUDiv" runat="server" class="title">
        <asp:Label ID="Label14" runat="server" Text="<%$ Resources:Common,Label_ProductTitle %>" />
    </div>
    <asp:UpdatePanel ID="upSKUDetails" runat="server">
        <ContentTemplate>
            <gc:GridView ID="gvSKUDetails" runat="server" ShowFooter="True" CssClass="GridView"
                AutoGenerateColumns="False" DataKeyNames="FormSettlementSKUDetailID" DataSourceID="odsSKUDetails"
                OnRowDataBound="gvSKUDetails_RowDataBound" CellPadding="0">
                <Columns>
                    <asp:TemplateField meta:resourcekey="TemplateFieldResource1">
                        <ItemTemplate>
                            <asp:HyperLink ID="lblApplyFormNo" runat="server" Text='<%# Eval("ApplyFormNo") %>'></asp:HyperLink>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="120px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:Common,Form_Period %>">
                        <ItemTemplate>
                            <asp:Label ID="lblPeriod" runat="server" Text='<%# Eval("ApplyPeriod","{0:yyyy-MM}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Common,Form_ProductName %>">
                        <ItemTemplate>
                            <asp:Label ID="lblProductName" runat="server" Text='<%# GetProductNameByID(Eval("SKUID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="200px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateFieldResource4">
                        <ItemTemplate>
                            <asp:Label ID="lblDiscount" runat="server" Text='<%# Eval("Discount","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="60px" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateFieldResource5">
                        <ItemTemplate>
                            <asp:Label ID="lblDiscountCampbell" runat="server" Text='<%# Eval("DiscountCampbell","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="80px" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateFieldResource6">
                        <ItemTemplate>
                            <asp:Label ID="lblForecastSaleQuantity" runat="server" Text='<%# Eval("ForecastSaleQuantity","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="90px" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateFieldResource7">
                        <ItemTemplate>
                            <asp:Label ID="lblForecastOrderQuantity" runat="server" Text='<%# Eval("ForecastOrderQuantity","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="90px" HorizontalAlign="Right" />
                        <FooterTemplate>
                            <asp:Label runat="server" ID="lblSum" Text="<%$ Resources:Common,Form_Total %>"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle CssClass="RedTextAlignCenter" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateFieldResource8">
                        <ItemTemplate>
                            <asp:Label ID="lblForecastOrderAmount" runat="server" Text='<%# Eval("ForecastOrderAmount","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Right" />
                        <FooterTemplate>
                            <asp:Label runat="server" ID="lblOrderAmountTotal" meta:resourcekey="lblOrderAmountTotalResource1"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle CssClass="RedTextAlignCenter" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateFieldResource9">
                        <ItemTemplate>
                            <asp:Label ID="lblActualOrderQuantity" runat="server" Text='<%# Eval("ActualOrderQuantity","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateFieldResource10">
                        <ItemTemplate>
                            <asp:Label ID="lblActualOrderAmount" runat="server" Text='<%# Eval("ActualOrderAmount","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Right" />
                        <FooterTemplate>
                            <asp:Label runat="server" ID="lblActualOrderAmountTotal" meta:resourcekey="lblActualOrderAmountTotalResource1"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle CssClass="RedTextAlignCenter" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateFieldResource11">
                        <ItemTemplate>
                            <asp:Label ID="lblActualRate" runat="server" Text='<%# Eval("ActualRate","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="80px" HorizontalAlign="Right" />
                        <FooterTemplate>
                            <asp:Label runat="server" ID="lblActualRateTotal" meta:resourcekey="lblActualRateTotalResource1"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle CssClass="RedTextAlignCenter" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:Common,Form_Remark %>">
                        <ItemTemplate>
                            <asp:Label ID="lblRemark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="160px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <FooterStyle Width="50px" Wrap="True" />
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td style="width: 120px;" class="Empty1">
                                <asp:Label ID="Label14" runat="server" meta:resourcekey="Label14Resource2" />
                            </td>
                            <td style="width: 100px;">
                                <asp:Label ID="Label15" runat="server" Text="<%$ Resources:Common,Form_Period %>" />
                            </td>
                            <td style="width: 200px;">
                                <asp:Label ID="Label16" runat="server" Text="<%$Resources:Common,Form_ProductName %>" />
                            </td>
                            <td style="width: 100px;">
                                <asp:Label ID="Label17" runat="server" meta:resourcekey="Label17Resource1" />
                            </td>
                            <td style="width: 100px;">
                                <asp:Label ID="Label18" runat="server" meta:resourcekey="Label18Resource1" />
                            </td>
                            <td style="width: 100px;">
                                <asp:Label ID="Label19" runat="server" meta:resourcekey="Label19Resource1" />
                            </td>
                            <td style="width: 100px;">
                                <asp:Label ID="Label20" runat="server" meta:resourcekey="Label20Resource1" />
                            </td>
                            <td style="width: 100px;">
                                <asp:Label ID="Label21" runat="server" meta:resourcekey="Label21Resource1" />
                            </td>
                            <td style="width: 100px;">
                                <asp:Label ID="Label22" runat="server" meta:resourcekey="Label22Resource1" />
                            </td>
                            <td style="width: 100px;">
                                <asp:Label ID="Label23" runat="server" meta:resourcekey="Label23Resource1" />
                            </td>
                            <td style="width: 160px;">
                                <asp:Label ID="Label24" runat="server" Text="<%$ Resources:Common,Form_Remark %>" />
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
            </gc:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="odsSKUDetails" runat="server" SelectMethod="GetFormSettlementSKUDetailBySettlementID"
        TypeName="BusinessObjects.FormSaleBLL">
        <SelectParameters>
            <asp:Parameter Name="FormSaleSettlementID" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <br />
    <div class="title">
        <asp:Label ID="Label23" runat="server" meta:resourcekey="Label23Resource2" /></div>
    <asp:UpdatePanel ID="upPaymentDetails" runat="server">
        <ContentTemplate>
            <gc:GridView ID="gvPaymentDetails" runat="server" ShowFooter="True" CssClass="GridView"
                AutoGenerateColumns="False" DataKeyNames="FormSalePaymentDetailID" DataSourceID="odsPaymentDetails"
                OnRowDataBound="gvPaymentDetails_RowDataBound" CellPadding="0">
                <Columns>
                    <asp:TemplateField meta:resourcekey="TemplateFieldResource13">
                        <ItemTemplate>
                            <asp:HyperLink ID="lblApplyFormNo" runat="server" Text='<%# Eval("ApplyFormNo") %>'></asp:HyperLink>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="120px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:Common,Form_Period %>">
                        <ItemTemplate>
                            <asp:Label ID="lblPeriod" runat="server" Text='<%# Eval("ApplyPeriod","{0:yyyy-MM}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:Common,Form_ExpenseItem %>">
                        <ItemTemplate>
                            <asp:Label ID="lblProductName" runat="server" Text='<%# GetExpenseItemNameByID(Eval("ExpenseItemID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="120px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateFieldResource16">
                        <ItemTemplate>
                            <asp:Label ID="lblShopName" runat="server" Text='<%# GetDescriptionByID(Eval("FormSaleExpenseDetailID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="200px" HorizontalAlign="Center" />
                        <FooterTemplate>
                            <asp:Label runat="server" ID="lblSum" Text="<%$ Resources:Common,Form_Total %>"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle CssClass="RedTextAlignCenter" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateFieldResource17">
                        <ItemTemplate>
                            <asp:Label ID="lblApplyAmount" runat="server" Text='<%# Eval("ApplyAmount","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Right" />
                        <FooterTemplate>
                            <asp:Label runat="server" ID="lblApplyAmountTotal" meta:resourcekey="lblApplyAmountTotalResource1"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle CssClass="RedTextAlignCenter" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateFieldResource18">
                        <ItemTemplate>
                            <asp:Label ID="lblApplyAmountRMB" runat="server" Text='<%# Eval("ApplyAmountRMB","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Right" />
                        <FooterTemplate>
                            <asp:Label runat="server" ID="lblApplyAmountRMBTotal" meta:resourcekey="lblApplyAmountRMBTotalResource1"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle CssClass="RedTextAlignCenter" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateFieldResource19">
                        <ItemTemplate>
                            <asp:Label ID="lblSettlementAmount" runat="server" Text='<%# Eval("SettlementAmount","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Right" />
                        <FooterTemplate>
                            <asp:Label runat="server" ID="lblSettlementAmountTotal" meta:resourcekey="lblSettlementAmountTotalResource1"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle CssClass="RedTextAlignCenter" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateFieldResource20">
                        <ItemTemplate>
                            <asp:Label ID="lblPayedAmount" runat="server" Text='<%# Eval("PayedAmount","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Right" />
                        <FooterTemplate>
                            <asp:Label runat="server" ID="lblPayedAmountTotal" meta:resourcekey="lblPayedAmountTotalResource1"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle CssClass="RedTextAlignCenter" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateFieldResource21">
                        <ItemTemplate>
                            <asp:Label ID="lblAmountRMB" runat="server" Text='<%# Eval("AmountRMB","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="150px" HorizontalAlign="Right" />
                        <FooterTemplate>
                            <asp:Label runat="server" ID="lblAmountRMBTotal" meta:resourcekey="lblAmountRMBTotalResource1"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle CssClass="RedTextAlignCenter" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:Common,Form_Remark %>">
                        <ItemTemplate>
                            <asp:Label ID="lblApplyRemark" runat="server" Text='<%# GetApplyRemark(Eval("FormSaleExpenseDetailID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="190px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <FooterStyle Width="50px" Wrap="True" />
            </gc:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="odsPaymentDetails" runat="server" SelectMethod="GetFormSalePaymentDetailByPaymentID"
        TypeName="BusinessObjects.FormSaleBLL">
        <SelectParameters>
            <asp:Parameter Name="FormSalePaymentID" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <br />
    <div class="title">
        <asp:Label ID="Label25" runat="server" meta:resourcekey="Label25Resource1" /></div>
    <asp:UpdatePanel ID="upGreeGoodsDetails" runat="server">
        <ContentTemplate>
            <gc:GridView ID="gvFreeGoods" runat="server" ShowFooter="True" CssClass="GridView"
                AutoGenerateColumns="False" DataKeyNames="FormSalePaymentFreeGoodsID" DataSourceID="odsFreeGoods"
                OnRowDataBound="gvFreeGoods_RowDataBound" CellPadding="0">
                <Columns>
                    <asp:TemplateField HeaderText="<%$Resources:Common,Form_ProductName %>">
                        <ItemTemplate>
                            <asp:Label ID="lblProductName" runat="server" Text='<%# GetProductNameByID(Eval("SKUID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="400px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateFieldResource24">
                        <ItemTemplate>
                            <asp:Label ID="lblPackPerCase" runat="server" Text='<%# Eval("PackPerCase") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateFieldResource25">
                        <ItemTemplate>
                            <asp:Label ID="lblDeliveryPrice" runat="server" Text='<%# Eval("DeliveryPrice","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateFieldResource26">
                        <ItemTemplate>
                            <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("Quantity","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Right" />
                        <FooterTemplate>
                            <asp:Label runat="server" ID="lblsum" Text="<%$Resources:Common,Form_Total %>"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle CssClass="RedTextAlignCenter" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateFieldResource27">
                        <ItemTemplate>
                            <asp:Label ID="lblAmountRMB" runat="server" Text='<%# Eval("AmountRMB","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Right" />
                        <FooterTemplate>
                            <asp:Label runat="server" ID="lblAmountRMBTotal" meta:resourcekey="lblAmountRMBTotalResource2"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle CssClass="RedTextAlignCenter" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:Common,Form_Remark %>">
                        <ItemTemplate>
                            <asp:Label ID="lblRemark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="600px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <FooterStyle Width="50px" Wrap="True" />
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td style="width: 300px;" class="Empty1">
                                <asp:Label ID="Label25" runat="server" Text="<%$Resources:Common,Form_ProductName %>" />
                            </td>
                            <td style="width: 100px;">
                                <asp:Label ID="Label26" runat="server" meta:resourcekey="Label26Resource1" />
                            </td>
                            <td style="width: 100px;">
                                <asp:Label ID="Label27" runat="server" meta:resourcekey="Label27Resource1" />
                            </td>
                            <td style="width: 100px;">
                                <asp:Label ID="Label28" runat="server" meta:resourcekey="Label28Resource1" />
                            </td>
                            <td style="width: 100px;">
                                <asp:Label ID="Label29" runat="server" meta:resourcekey="Label29Resource1" />
                            </td>
                            <td style="width: 440px;">
                                <asp:Label ID="Label30" runat="server" Text="<%$ Resources:Common,Form_Remark %>" />
                            </td>
                            <td style="width: 100px">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
            </gc:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="odsFreeGoods" runat="server" SelectMethod="GetFormSalePaymentFreeGoodsByPaymentID"
        TypeName="BusinessObjects.FormSaleBLL">
        <SelectParameters>
            <asp:Parameter Name="FormSalePaymentID" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <br />
    <asp:UpdatePanel ID="upDeliveryInfo" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div id="divDeliveryInfo" runat="server" class="title">
                <asp:Label ID="Label31" runat="server" Text="发货信息" />
            </div>
            <gc:GridView ID="gvDeliveryInfo" runat="server" ShowFooter="True" CssClass="GridView"
                AutoGenerateColumns="False" DataKeyNames="FormDeliveryGoodsID" CellPadding="0"
                OnRowDataBound="gvDeliveryInfo_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="发货单号">
                        <ItemTemplate>
                            <asp:Label ID="lblDeliveryNo" runat="server" Text='<%# Eval("DeliveryNo") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="300px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Common,Form_ProductName %>">
                        <ItemTemplate>
                            <asp:Label ID="lblProduct" runat="server" Text='<%# GetProductNameByID(Eval("SKUID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="430px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="发货价">
                        <ItemTemplate>
                            <asp:Label ID="lblMAAUnitPrice" runat="server" Text='<%# Eval("MAAUnitPrice","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="120px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="发货数量">
                        <ItemTemplate>
                            <asp:Label ID="lblDeliveryQuantity" runat="server" Text='<%# Eval("Quantity","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="120px" />
                        <FooterStyle CssClass="RedTextAlignCenter" HorizontalAlign="Center" />
                        <FooterTemplate>
                            <asp:Label ID="sumskulbl" runat="server" Text="合计"></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="发货金额">
                        <ItemTemplate>
                            <asp:Label ID="lblAmountRMB" runat="server" Text='<%# Eval("AmountRMB","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="120px" />
                        <FooterStyle HorizontalAlign="Right" CssClass="RedTextAlignCenter" />
                        <FooterTemplate>
                            <asp:Label ID="lblAmountRMBTotal" runat="server"></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="发货日期">
                        <ItemTemplate>
                            <asp:Label ID="lblDeliveryDate" runat="server" Text='<%# Eval("DeliveryDate","{0:yyyy-MM-dd}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="150px" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <FooterStyle Width="50px" Wrap="True" />
            </gc:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <uc1:APFlowNodes ID="cwfAppCheck" runat="server" />
    <asp:UpdatePanel ID="upButton" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div style="margin-top: 20px; width: 1200px; text-align: right">
                <uc3:ucPrint ID="ucPrint" runat="server" />
                <asp:Button ID="DeliveryCompleteBtn" runat="server" OnClick="DeliveryCompleteBtn_Click"
                    Text="<%$ Resources:Common,Button_DeliveryComplete %>" CssClass="button_nor" />&nbsp;
                <asp:Button ID="SubmitBtn" runat="server" OnClick="SubmitBtn_Click" Text="<%$ Resources:Common,Button_Approve %>"
                    CssClass="button_nor" />&nbsp;
                <asp:Button ID="CancelBtn" runat="server" OnClick="CancelBtn_Click" Text="<%$ Resources:Common,Button_Back %>"
                    CssClass="button_nor" />&nbsp;
                <asp:Button ID="EditBtn" runat="server" OnClick="EditBtn_Click" Text="<%$ Resources:Common,Button_Edit %>"
                    CssClass="button_nor" />&nbsp;
                <asp:Button ID="ScrapBtn" runat="server" OnClick="ScrapBtn_Click" Text="<%$ Resources:Common,Button_Scrap %>"
                    CssClass="button_nor" />&nbsp;
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <uc3:ucUpdateProgress ID="upUP" runat="server" vAssociatedUpdatePanelID="upButton" />
    <br />
</asp:Content>
