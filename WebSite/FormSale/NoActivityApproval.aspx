﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="NoActivityApproval.aspx.cs" Inherits="FormSale_NoActivityApproval"
    Culture="auto" UICulture="auto" %>

<%@ Register Src="../UserControls/APFlowNodes.ascx" TagName="APFlowNodes" TagPrefix="uc1" %>
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
                        <asp:Label runat="server" Text="<%$ Resources:Common,Form_FormNo %>" /></div>
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
            </tr>
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_Period" runat="server" Text="<%$ Resources:Common,Form_Period %>" /></div>
                    <div>
                        <asp:TextBox ID="PeriodCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="True"
                            Width="170px"></asp:TextBox>
                    </div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label16" runat="server" Text="<%$ Resources:Common,Label_CustomerNo %>" /></div>
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
            </tr>
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_City" runat="server" Text="<%$Resources:Common,Form_City %>" />
                    </div>
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
            </tr>
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label17" runat="server" meta:resourcekey="LabelResource6" />
                    </div>
                    <div>
                        <asp:TextBox ID="ShopNameCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="True"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label15" runat="server" meta:resourcekey="LabelResource7" />
                    </div>
                    <div>
                        <asp:TextBox ID="ShopCountCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="True"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td colspan="2" style="width: 400px">
                    <div class="field_title">
                        <asp:Label ID="Label_ProjectName" runat="server" Text="<%$ Resources:Common,Form_ProjectName %>" />
                    </div>
                    <div>
                        <asp:TextBox ID="ProjectNameCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="True"
                            Width="370px"></asp:TextBox></div>
                </td>
                <td colspan="2" style="width: 400px">
                    <div class="field_title">
                        <asp:Label runat="server" meta:resourcekey="LabelResource9" />
                    </div>
                    <div>
                        <asp:TextBox ID="ActivityBeginCtl" runat="server" ReadOnly="True" Width="170px"></asp:TextBox>
                        <asp:Label ID="Label1" runat="server">~~</asp:Label>
                        <asp:TextBox ID="ActivityEndCtl" runat="server" ReadOnly="True" Width="170px"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_CostCenter" runat="server" Text="<%$Resources:Common,Form_CostCenter %>" />
                    </div>
                    <div>
                        <asp:TextBox ID="CostCenterCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="True"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label_DiscountTypeCtl" runat="server" meta:resourcekey="Label_DiscountTypeCtlResource1" />
                    </div>
                    <div>
                        <asp:TextBox ID="DiscountTypeCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="True"
                            Width="170px"></asp:TextBox>
                    </div>
                </td>
                <td valign="top">
                    <div class="field_title">
                        <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Common,Form_RejectFormNo %>" /></div>
                    <div style="margin-top: 5px">
                        <asp:HyperLink ID="lblRejectFormNo" runat="server"></asp:HyperLink></div>
                </td>
                <td valign="top">
                    <div class="field_title">
                        <asp:Label ID="Label14" runat="server" Text="<%$ Resources:Common,Form_SettlementFormNo %>" />
                    </div>
                    <div style="margin-top: 5px">
                        <asp:HyperLink ID="hlSettlementFormNo" runat="server"></asp:HyperLink></div>
                </td>
            </tr>
            <tr>
                <td colspan="3" valign="top">
                    <div class="field_title">
                        <asp:Label runat="server" meta:resourcekey="LabelResource11" />
                    </div>
                    <div>
                        <asp:TextBox ID="ProjectDescCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="True"
                            TextMode="MultiLine" Height="60px" Columns="75"></asp:TextBox>
                    </div>
                </td>
                <td colspan="2" valign="top">
                    <div class="field_title">
                        <asp:Label ID="Form_Adjunct" runat="server" Text="<%$Resources:Common,Form_Adjunct %>" />
                    </div>
                    <uc2:UCFlie ID="UCFileUpload" runat="server" Width="370px" IsView="true" />
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
                        <asp:Label ID="Label3" runat="server" meta:resourcekey="Label3Resource1" />
                    </div>
                    <div>
                        <asp:TextBox ID="TotalBudgetCtl" runat="server" ReadOnly="True" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px;">
                    <div class="field_title">
                        <asp:Label ID="Label4" runat="server" meta:resourcekey="Label4Resource1" />
                    </div>
                    <div>
                        <asp:TextBox ID="ApprovedAmountCtl" runat="server" ReadOnly="True" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label5" runat="server" meta:resourcekey="Label5Resource1" />
                    </div>
                    <div>
                        <asp:TextBox ID="ApprovingAmountCtl" runat="server" ReadOnly="True" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px; display: none;">
                    <div class="field_title">
                        <asp:Label ID="Label6" runat="server" meta:resourcekey="Label6Resource1" />
                    </div>
                    <div>
                        <asp:TextBox ID="CompletedAmountCtl" runat="server" ReadOnly="True" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px; display: none;">
                    <div class="field_title">
                        <asp:Label ID="Label7" runat="server" meta:resourcekey="Label7Resource1" />
                    </div>
                    <div>
                        <asp:TextBox ID="ReimbursedAmountCtl" runat="server" ReadOnly="True" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label8" runat="server" meta:resourcekey="Label8Resource1" />
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
                        <ItemStyle Width="250px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Common,Form_ProductName %>">
                        <ItemTemplate>
                            <asp:Label ID="lblProduct" runat="server" Text='<%# GetProductNameByID(Eval("SKUID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="300px" HorizontalAlign="Center" />
                        <FooterTemplate>
                            <asp:Label runat="server" ID="lblSum" Text="<%$Resources:Common,Form_Total %>"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle CssClass="RedTextAlignCenter" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateFieldResource3">
                        <ItemTemplate>
                            <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Right" />
                        <FooterTemplate>
                            <asp:Label runat="server" ID="lblTotal" Text="<%$Resources:Common,Form_Total %>"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle CssClass="RedTextAlignCenter" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateFieldResource4">
                        <ItemTemplate>
                            <asp:Label ID="lblAmountRMB" runat="server" Text='<%# Eval("AmountRMB","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
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
                        <ItemStyle Width="480px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <FooterStyle Width="50px" Wrap="True" />
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td style="width: 250px;" class="Empty1">
                                <asp:Label ID="Label13" runat="server" Text="<%$Resources:Common,Form_ExpenseItem %>" />
                            </td>
                            <td style="width: 300px;">
                                <asp:Label ID="Label9" runat="server" Text="<%$Resources:Common,Form_ProductName %>" />
                            </td>
                            <td style="width: 100px;">
                                <asp:Label ID="Label10" runat="server" meta:resourcekey="Label10Resource1" />
                            </td>
                            <td style="width: 100px;">
                                <asp:Label ID="Label11" runat="server" meta:resourcekey="Label11Resource1" />
                            </td>
                            <td style="width: 480px;">
                                <asp:Label ID="Label12" runat="server" Text="<%$ Resources:Common,Form_Remark %>" />
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
            </gc:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="odsDetails" runat="server" SelectMethod="GetFormSaleExpenseDetailByFormSaleApplyID"
        TypeName="BusinessObjects.FormSaleBLL">
        <SelectParameters>
            <asp:Parameter Name="FormSaleApplyID" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <br />
    <uc1:APFlowNodes ID="cwfAppCheck" runat="server" />
    <asp:UpdatePanel ID="upButton" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div style="margin-top: 20px; width: 1200px; text-align: right">
                <asp:Button ID="SubmitBtn" runat="server" OnClick="SubmitBtn_Click" Text="<%$ Resources:Common,Button_Approve %>"
                    CssClass="button_nor" />&nbsp;
                <asp:Button ID="CancelBtn" runat="server" OnClick="CancelBtn_Click" Text="<%$ Resources:Common,Button_Back %>"
                    CssClass="button_nor" />&nbsp;
                <asp:Button ID="EditBtn" runat="server" OnClick="EditBtn_Click" Text="<%$ Resources:Common,Button_Edit %>"
                    CssClass="button_nor" />&nbsp;
                <asp:Button ID="ScrapBtn" runat="server" OnClick="ScrapBtn_Click" Text="<%$ Resources:Common,Button_Scrap %>"
                    CssClass="button_nor" />&nbsp;
                <input type="button" style="display: <%=IsVisible %>" value="查看报销单" class="button_nor"
                    onclick="<%= GetShowWindowScript() %>" />&nbsp;
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <uc3:ucUpdateProgress ID="ucUP" runat="server" vassociatedupdatepanelid="upButton" />
    <br />
</asp:Content>
