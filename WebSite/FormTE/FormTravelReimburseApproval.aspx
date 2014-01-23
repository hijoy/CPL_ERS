<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="FormTravelReimburseApproval.aspx.cs" UICulture="Auto" Culture="Auto" Inherits="Form_FormTravelReimburseApproval" %>

<%@ Register Src="../UserControls/APFlowNodes.ascx" TagName="APFlowNodes" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/MultiAttachmentFile.ascx" TagName="UCFlie" TagPrefix="uc2" %>
<%@ Register Src="~/UserControls/PrintReport.ascx" TagName="ucPrint" TagPrefix="uc3" %>
<%@ Register Src="../UserControls/YearAndMonthUserControl.ascx" TagName="YearAndMonthUserControl" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" src="../Script/js.js" type="text/javascript"></script>
    <script language="javascript" src="../Script/DateInput.js" type="text/javascript"></script>
    <div class="title" style="width: 1240px">
        <asp:Label ID="Label_BasicInfo" Text="<%$Resources:Common,Label_BasicInfo %>" runat="server" />
    </div>
    <div class="searchDiv">
        <table class="searchTable">
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_FormNo" runat="server" Text="<%$Resources:Common,Form_FormNo %>" /></div>
                    <div>
                        <asp:TextBox ID="txtFormNo" runat="server" ReadOnly="true" Width="170px" />
                    </div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label3" runat="server" meta:resourcekey="FormTravelReimburseApprove_Label_ApplyDate" /></div>
                    <div>
                        <asp:TextBox ID="ApplyDateCtl" runat="server" ReadOnly="true" Width="170px" />
                    </div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_Period" runat="server" Text='<%$Resources:Common,Form_Period %>' /></div>
                    <div>
                        <asp:TextBox ID="txtPeriod" runat="server" ReadOnly="true" Width="170px" />
                    </div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_ApplyUser" Text='<%$Resources:Common,Form_ApplyUser %>' runat="server" /></div>
                    <div>
                        <asp:TextBox ID="StuffNameCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_Position" runat="server" Text='<%$Resources:Common,Form_Position %>' /></div>
                    <div>
                        <asp:TextBox ID="PositionNameCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_Organization" runat="server" Text='<%$Resources:Common,Form_Organization %>' /></div>
                    <div>
                        <asp:TextBox ID="DepartmentNameCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox></div>
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_AttendDate" runat="server" Text='<%$Resources:Common,Form_AttendDate %>' /></div>
                    <div>
                        <asp:TextBox ID="AttendDateCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_StaffNo" runat="server" Text='<%$Resources:Common,Form_StaffNo %>' /></div>
                    <div>
                        <asp:TextBox ID="StuffNoCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td valign="top">
                    <div class="field_title">
                        <asp:Label ID="Form_RejectFormNo" runat="server" Text="<%$Resources:Common,Form_RejectFormNo %>" /></div>
                    <div style="margin-top: 5px">
                        <asp:HyperLink ID="lblRejectFormNo" runat="server"></asp:HyperLink></div>
                </td>
            </tr>
            <tr>
                <td colspan="3" valign="top">
                    <div class="field_title">
                        <asp:Label ID="Label10" runat="server" meta:resourcekey="FormTravelReimburseApprove_Label_ApplyReason" /></div>
                    <div>
                        <asp:TextBox ID="RemarkCtl" runat="server" CssClass="InputText"  TextMode="multiline"
                            Height="60px" Columns="75" ReadOnly="true"></asp:TextBox></div>
                </td>
                <td colspan="3" valign="top">
                    <div class="field_title">
                        <asp:Label ID="Form_Adjunct" runat="server" Text="<%$Resources:Common,Form_Adjunct %>" /></div>
                        <uc2:UCFlie ID="UCFileUpload" runat="server" Width="400px" IsView="true"/>
                </td>

            </tr>
        </table>
    </div>
    <br />
    <div runat="server" id="budgetTitleDIV" class="title" style="width: 1240px">
        <asp:Label ID="Form_BudgetTitle" runat="server" Text='<%$Resources:Common,Form_BudgetTitle %>' /></div>
    <div runat="server" id="budgetDIV" class="searchDiv">
        <table class="searchTable">
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label11" runat="server" meta:resourcekey="FormTravelReimburseApprove_Label_TotalBudget" />
                    </div>
                    <div>
                        <asp:TextBox ID="txtTotalBudget" runat="server" Width="180px" ReadOnly="true"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label12" runat="server" meta:resourcekey="FormTravelReimburseApprove_Label_ApprovingAmount" />
                    </div>
                    <div>
                        <asp:TextBox ID="txtApprovingAmount" runat="server" Width="180px" ReadOnly="true"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label13" runat="server" meta:resourcekey="FormTravelReimburseApprove_Label_ApprovedAmount" />
                    </div>
                    <div>
                        <asp:TextBox ID="txtApprovedAmount" runat="server" Width="180px" ReadOnly="true"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label14" runat="server" meta:resourcekey="FormTravelReimburseApprove_Label_RemainAmount" /></div>
                    <div>
                        <asp:TextBox ID="txtRemainAmount" runat="server" Width="180px" ReadOnly="true"></asp:TextBox></div>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div class="title" style="width: 1240px">
        <asp:Label ID="Label15" runat="server" meta:resourcekey="FormTravelReimburseApprove_Label_ReimburseDetails" /></div>
    <gc:GridView ID="gvTravelReimburseDetails" runat="server" ShowFooter="True" CssClass="GridView"
        AutoGenerateColumns="False" DataKeyNames="FormTravelReimburseDetailID" DataSourceID="odsTravelReimburseDetails"
        OnRowDataBound="gvTravelReimburseDetails_RowDataBound" CellPadding="0">
        <Columns>
            <asp:TemplateField meta:resourcekey="TravelReimburseDetailsGridView_TemplateField_OccurDate">
                <ItemTemplate>
                    <asp:Label ID="lblOccurDate" runat="server" Text='<%# Eval("OccurDate", "{0:yyyy-MM-dd}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="150px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField meta:resourcekey="TravelReimburseDetailsGridView_TemplateField_Destination">
                <ItemTemplate>
                    <asp:Label ID="lblDestination" runat="server" Text='<%# Bind("Destination") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="150px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Common,Form_ExpenseItem %>">
                <ItemTemplate>
                    <asp:Label ID="lblManageExpenseItem" runat="server" Text='<%# GetManageExpenseItemNameByID(Eval("ManageExpenseItemID")) %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="150px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Common,Form_Currency %>">
                <ItemTemplate>
                    <asp:Label ID="lblCurrency" runat="server" Text='<%# GetCurrencyByID(Eval("CurrencyID")) %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="100px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Common,Form_ExchangeRate %>">
                <ItemTemplate>
                    <asp:Label ID="lblExchangeRate" runat="server" Text='<%# Eval("ExchangeRate") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="100px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField meta:resourcekey="TravelReimburseDetailsGridView_TemplateField_UnitPrice">
                <ItemTemplate>
                    <asp:Label ID="lblUnitPrice" runat="server" Text='<%# Eval("UnitPrice") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="100px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField meta:resourcekey="TravelReimburseDetailsGridView_TemplateField_Frequency">
                <ItemTemplate>
                    <asp:Label ID="lblFrequency" runat="server" Text='<%# Eval("Frequency") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="100px" HorizontalAlign="Center" />
                <FooterTemplate>
                    <asp:Label runat="server" ID="lblSum" Text="<%$Resources:Common,Form_Total %>"></asp:Label>
                </FooterTemplate>
                <FooterStyle CssClass="RedTextAlignCenter" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField meta:resourcekey="TravelReimburseDetailsGridView_TemplateField_Cost">
                <ItemTemplate>
                    <asp:Label ID="lblCost" runat="server" Text='<%# Eval("Cost","{0:N}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="100px" HorizontalAlign="Right" />
                 <FooterTemplate>
                            <asp:Label runat="server" ID="lblTotalRMB"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle CssClass="RedTextAlignCenter" HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField meta:resourcekey="TravelReimburseDetailsGridView_TemplateField_PayMan">
                <ItemTemplate>
                    <asp:Label ID="lblPayMan" runat="server" Text='<%# GetPayMan(Eval("PayMan"))%>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="100px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Common,Form_Remark %>">
                <ItemTemplate>
                    <asp:Label ID="lblRemark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="210px" HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
        <HeaderStyle CssClass="Header" />
    </gc:GridView>
    <asp:ObjectDataSource ID="odsTravelReimburseDetails" runat="server" SelectMethod="GetFormTravelReimburseDetailByFormTravelReimburseID"
        TypeName="BusinessObjects.FormTEBLL">
        <SelectParameters>
            <asp:Parameter Name="FormTravelReimburselID" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <br />
    <div runat="server" id="FinanceRemarkTitleDIV" class="title" style="width: 1240px">
        <asp:Label ID="Label1" runat="server" Text="Remark from Finance"/></div>
    <div runat="server" id="FinanceRemarkDIV" class="searchDiv">
        <table class="searchTable">
            <tr>
                <td colspan="6">
                    <asp:TextBox ID="FinanceRemarkCtl" runat="server" CssClass="InputText" Width="800px" TextMode="multiline"
                        Height="60px" ></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <uc1:APFlowNodes ID="cwfAppCheck" runat="server" />
<%--    <asp:UpdatePanel ID="upCustomer" UpdateMode="Conditional" runat="server">
        <ContentTemplate>--%>
            <div style="margin-top: 20px; width: 1200px; text-align: right">
                <uc4:YearAndMonthUserControl ID="UCNewPeriod" runat="server" IsReadOnly="false" IsExpensePeriod="true" />&nbsp;
                <asp:Button ID="ModifyPeriodBtn" runat="server" OnClick="ModifyPeriodBtn_Click" Text="修改费用期间" CssClass="button_nor" />&nbsp;&nbsp;&nbsp;&nbsp;

                <uc3:ucPrint ID="ucPrint" runat="server" />&nbsp;
                <asp:Button ID="SubmitBtn" runat="server" OnClick="SubmitBtn_Click" Text='<%$Resources:Common,Button_Approve %>'
                    CssClass="button_nor" />&nbsp;
                <asp:Button ID="CancelBtn" runat="server" OnClick="CancelBtn_Click" Text='<%$Resources:Common,Button_Back %>'
                    CssClass="button_nor" />&nbsp;
                <asp:Button ID="EditBtn" runat="server" OnClick="EditBtn_Click" Text='<%$Resources:Common,Button_Edit %>'
                    CssClass="button_nor" />&nbsp;
                <asp:Button ID="ScrapBtn" runat="server" OnClick="ScrapBtn_Click" Text='<%$Resources:Common,Button_Scrap %>'
                    CssClass="button_nor" />
                <asp:Button ID="SaveBtn" runat="server" CssClass="button_nor" OnClick="SaveBtn_Click"
                    Text="<%$Resources:Common,Button_Save %>" />
            </div>
            <br />
<%--        </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
