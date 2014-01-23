<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="MarketingSampleRequestApproval.aspx.cs" Culture="Auto" UICulture="Auto"
    Inherits="FormMarketing_MarketingApproval" %>

<%@ Register Src="../UserControls/APFlowNodes.ascx" TagName="APFlowNodes" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/MultiAttachmentFile.ascx" TagName="UCFlie" TagPrefix="uc2" %>
<%@ Register Src="../UserControls/ucUpdateProgress.ascx" TagName="ucUpdateProgress"
    TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <script src="../Script/DateInput.js" type="text/javascript"></script>
    <div class="title">
        <asp:Label ID="Label_Title" runat="server" meta:resourcekey="Label_Title" /></div>
    <div class="searchDiv">
        <table class="searchTable">
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label2" runat="server" Text="<%$Resources:Common,Form_FormNo %>" /></div>
                    <div>
                        <asp:TextBox ID="FormNoCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
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
            </tr>
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_Period" runat="server" Text="<%$Resources:Common,Form_Period %>" /></div>
                    <div>
                        <asp:TextBox ID="PeriodCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox>
                    </div>
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
            </tr>
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label_ExpenseSubCategory" runat="server" Text="<%$ Resources:Common,Form_ExpenseSubCategory %>" />
                    </div>
                    <div>
                        <asp:TextBox ID="ExpenseSubCategoryCtl" runat="server" CssClass="InputTextReadOnly"
                            ReadOnly="True" Width="170px"></asp:TextBox></div>
                </td>
                <td colspan="2" style="width: 400px">
                    <div class="field_title">
                        <asp:Label ID="Form_ProjectName" runat="server" Text="<%$Resources:Common,Form_ProjectName %>" /></div>
                    <div>
                        <asp:TextBox ID="ProjectNameCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="370px"></asp:TextBox></div>
                </td>
                <td colspan="2" style="width: 400px">
                    <div class="field_title">
                        <asp:Label ID="Label_ActivityBegin" runat="server" meta:resourcekey="Label_ActivityBegin" /></div>
                    <div>
                        <asp:TextBox ID="ActivityBeginCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox>
                        <asp:Label ID="Label1" runat="server">~~</asp:Label>
                        <asp:TextBox ID="ActivityEndCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox>
                    </div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_CostCenter" runat="server" Text="<%$Resources:Common,Form_CostCenter %>" />
                    </div>
                    <div>
                        <asp:TextBox ID="CostCenterCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="True"
                            Width="170px"></asp:TextBox></div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="field_title">
                        <asp:Label ID="Label14" runat="server" meta:resourcekey="LabelResource10" />
                    </div>
                    <div>
                        <asp:TextBox ID="txtExpectDeliveryDate" runat="server" Width="170px" ReadOnly="true"></asp:TextBox>
                    </div>
                </td>
                <td colspan="2">
                    <div class="field_title">
                        <asp:Label ID="Label15" runat="server" meta:resourcekey="LabelResource12" />
                    </div>
                    <div>
                        <asp:TextBox ID="txtDeliveryAddress" runat="server" Width="370px" ReadOnly="true"></asp:TextBox></div>
                </td>
                <td valign="top" style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_RejectFormNo" runat="server" Text="<%$Resources:Common,Form_RejectFormNo %>" /></div>
                    <div style="margin-top: 5px">
                        <asp:HyperLink ID="lblRejectFormNo" runat="server"></asp:HyperLink></div>
                </td>
            </tr>
            <tr>
                <td colspan="3" valign="top">
                    <div class="field_title">
                        <asp:Label ID="Label_ProjectDesc" runat="server" meta:resourcekey="Label_ProjectDesc" /></div>
                    <div>
                        <asp:TextBox ID="ProjectDescCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            TextMode="multiline" Height="60px" Columns="75"></asp:TextBox>
                    </div>
                </td>
                <td colspan="3" valign="top">
                    <div class="field_title">
                        <asp:Label ID="Form_Adjunct" runat="server" Text="<%$Resources:Common,Form_Adjunct %>" /></div>
                    <uc2:UCFlie ID="UCFileUpload" runat="server" Width="400px" IsView="true" />
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div class="title">
        <asp:Label ID="Form_BudgetTitle" runat="server" Text="<%$Resources:Common,Form_BudgetTitle %>" /></div>
    <div class="searchDiv">
        <table class="searchTable">
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label_TotalBudget" runat="server" meta:resourcekey="Label_TotalBudget" /></div>
                    <div>
                        <asp:TextBox ID="TotalBudgetCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px;">
                    <div class="field_title">
                        <asp:Label ID="Label_ApprovedAmount" runat="server" meta:resourcekey="Label_ApprovedAmount" /></div>
                    <div>
                        <asp:TextBox ID="ApprovedAmountCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label_ApprovingAmount" runat="server" meta:resourcekey="Label_ApprovingAmount" /></div>
                    <div>
                        <asp:TextBox ID="ApprovingAmountCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px; display: none;">
                    <div class="field_title">
                        <asp:Label ID="Label_ReimbursedAmount" runat="server" meta:resourcekey="Label_ReimbursedAmount" /></div>
                    <div>
                        <asp:TextBox ID="ReimbursedAmountCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label_RemainBudget" runat="server" meta:resourcekey="Label_RemainBudget" /></div>
                    <div>
                        <asp:TextBox ID="RemainBudgetCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div class="title">
        <asp:Label ID="Label3" runat="server" meta:resourcekey="Label_Title1" /></div>
    <asp:UpdatePanel ID="upDetails" runat="server">
        <ContentTemplate>
            <gc:GridView ID="gvDetails" runat="server" ShowFooter="True" CssClass="GridView"
                AutoGenerateColumns="False" DataKeyNames="FormMarketingApplyDetailID" DataSourceID="odsDetails"
                OnRowDataBound="gvDetails_RowDataBound" CellPadding="0" meta:resourcekey="gvDetailsResource1">
                <Columns>
                    <asp:TemplateField HeaderText="<%$Resources:Common,Form_ExpenseItem %>">
                        <ItemTemplate>
                            <asp:Label ID="lblExpenseItem" runat="server" Text='<%# GetExpenseItemNameByID(Eval("ExpenseItemID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="180px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Common,Form_ProductName %>">
                        <ItemTemplate>
                            <asp:Label ID="lblProduct" runat="server" Text='<%# GetProductNameByID(Eval("SKUID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="300px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateFieldResource5">
                        <ItemTemplate>
                            <asp:Label ID="lblDeliveryPrice" runat="server" Text='<%# Eval("DeliveryPrice","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateFieldResource6">
                        <ItemTemplate>
                            <asp:Label ID="lblDeliveryQuantity" runat="server" Text='<%# Eval("DeliveryQuantity") %>'></asp:Label>
                        </ItemTemplate>
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
                        <ItemStyle Width="454px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <FooterStyle Width="50px" Wrap="True" />
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td style="width: 180px;" class="Empty1">
                                <asp:Label ID="Label9" runat="server" Text="<%$Resources:Common,Form_ExpenseItem %>" />
                            </td>
                            <td style="width: 300px;">
                                <asp:Label ID="Label10" runat="server" Text="<%$Resources:Common,Form_ProductName %>" />
                            </td>
                            <td style="width: 100px;">
                                <asp:Label ID="Label11" runat="server" meta:resourcekey="LabelResource21" />
                            </td>
                            <td style="width: 100px;">
                                <asp:Label ID="Label1" runat="server" meta:resourcekey="LabelResource22" />
                            </td>
                            <td style="width: 100px;">
                                <asp:Label ID="Label12" runat="server" meta:resourcekey="LabelResource20" />
                            </td>
                            <td style="width: 454px;">
                                <asp:Label ID="Label13" runat="server" Text="<%$ Resources:Common,Form_Remark %>" />
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
            </gc:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="odsDetails" runat="server" SelectMethod="GetFormMarketingApplyDetailByMarketingApplyID"
        TypeName="BusinessObjects.FormMarketingBLL">
        <SelectParameters>
            <asp:Parameter Name="FormMarketingApplyID" Type="Int32" />
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
                <asp:Button ID="DeliveryCompleteBtn" runat="server" OnClick="DeliveryCompleteBtn_Click"
                    Text="<%$ Resources:Common,Button_DeliveryComplete %>" CssClass="button_nor" />&nbsp;
                <asp:Button ID="SubmitBtn" runat="server" OnClick="SubmitBtn_Click" Text="<%$Resources:Common,Button_Approve %>"
                    CssClass="button_nor" />&nbsp;
                <asp:Button ID="CancelBtn" runat="server" OnClick="CancelBtn_Click" Text="<%$Resources:Common,Button_Back %>"
                    CssClass="button_nor" />&nbsp;
                <asp:Button ID="EditBtn" runat="server" OnClick="EditBtn_Click" Text="<%$Resources:Common,Button_Edit %>"
                    CssClass="button_nor" />&nbsp;
                <asp:Button ID="ScrapBtn" runat="server" OnClick="ScrapBtn_Click" Text="<%$Resources:Common,Button_Scrap %>"
                    CssClass="button_nor" />&nbsp;
                <asp:Button ID="CloseBtn" runat="server" OnClick="CloseBtn_Click" Text="<%$Resources:Common,Button_CompleteReimburse %>"
                    OnClientClick="return window.confirm('确定要关闭么，关闭之后将不能进行报销？');" CssClass="button_nor" />&nbsp;
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <uc3:ucUpdateProgress ID="ucUP" runat="server" vassociatedupdatepanelid="upButton" />
    <br />
</asp:Content>
