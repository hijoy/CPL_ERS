<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="FormInvitationReimburseApply.aspx.cs" Inherits="FormTE_FormInvitationApply"
    Culture="Auto" UICulture="Auto" %>

<%@ Register Src="../UserControls/YearAndMonthUserControl.ascx" TagName="YearAndMonth"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControls/UCDateInput.ascx" TagName="DateInput" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/MultiAttachmentFile.ascx" TagName="UCFlie" TagPrefix="uc2" %>
<%@ Register Src="../UserControls/ucUpdateProgress.ascx" TagName="ucUpdateProgress"
    TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <script src="../Script/DateInput.js" type="text/javascript"></script>
    <script type="text/javascript">
        function updateTotalRMB() {
            var txtAmount = document.getElementById('<%=Amount_ClientID %>');
            var txtAmountRMB = document.getElementById('<%=AmountRMB_ClientID %>');
            var txtExchangeRate = document.getElementById('<%=ExchangeRate_ClientID %>');
            var txtAttenderCount = document.getElementById('<%=AttenderCount_ClientID %>');
            var CostLimit = parseFloat('<%= CostLimitInfo[0]%>');
            var ApprovingAmount = parseFloat('<%= CostLimitInfo[1]%>');
            var ApprovedAmount = parseFloat('<%= CostLimitInfo[2]%>');
            var RemainAmount = parseFloat('<%= CostLimitInfo[3]%>');
            if (txtAmount.value && (!isNaN(parseFloat(txtAmount.value)))) {
                var amount = parseFloat(txtAmount.value) * parseFloat(txtExchangeRate.value);
                txtAmountRMB.value = commafy(amount.toFixed(2));
                if (amount > RemainAmount) {
                    txtAmount.style.color = "Red";
                } else {
                    txtAmount.style.color = "";
                }
            } else {
                txtAmountRMB.value = 0;
            }
        }
        function commafy(num) {
            num = num + "";
            var re = /(-?\d+)(\d{3})/
            while (re.test(num)) {
                num = num.replace(re, "$1,$2")
            }
            return num;
        }
    </script>
    <div class="title">
        <asp:Label ID="Label_BasicInfo" Text="<%$Resources:Common,Label_BasicInfo %>" runat="server" /></div>
    <div class="searchDiv">
        <table class="searchTable">
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label Text='<%$Resources:Common,Form_ApplyUser %>' runat="server" /></div>
                    <div>
                        <asp:TextBox ID="StuffNameCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label runat="server" Text='<%$Resources:Common,Form_Position %>' /></div>
                    <div>
                        <asp:TextBox ID="PositionNameCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px" align="left">
                    <div class="field_title">
                        <asp:Label runat="server" Text='<%$Resources:Common,Form_Organization %>' /></div>
                    <div>
                        <asp:TextBox ID="DepartmentNameCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px" align="left">
                    <div class="field_title">
                        <asp:Label runat="server" Text='<%$Resources:Common,Form_AttendDate %>' /></div>
                    <div>
                        <asp:TextBox ID="AttendDateCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label3" runat="server" Text='<%$Resources:Common,Form_StaffNo %>' /><span
                            class="requiredLable">*</span></div>
                    <div>
                        <asp:TextBox ID="StuffNoCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox></div>
                </td>

                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="lblPeriod" runat="server" Text='<%$Resources:Common,Form_Period %>' /><span
                            class="requiredLable">*</span></div>
                    <div>
                        <asp:DropDownList ID="dplPeriod" runat="server" DataSourceID="odsPeriod"
                            DataTextField="PeriodReimburse" DataValueField="PeriodReimburseID" Width="170px">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="odsPeriod" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                            SelectCommand=" select 0 PeriodReimburseID,'Please Select' PeriodReimburse Union SELECT [PeriodReimburseID], convert(varchar(50),year(PeriodReimburse))+'-'+Right(100+month(PeriodReimburse),2) PeriodReimburse FROM [PeriodReimburse] ">
                        </asp:SqlDataSource>
                        <asp:RequiredFieldValidator ID="RFPeriod" runat="server" ControlToValidate="dplPeriod"
                            ValidationGroup="add" meta:resourcekey="RequiredFieldValidator_RFPeriod" Display="None" InitialValue="0"></asp:RequiredFieldValidator>
                    </div>
                </td>
            </tr>
            <tr>
                <td align="left" colspan="6">
                    <div class="field_title">
                        <asp:Label ID="Label5" runat="server" Text='<%$Resources:Common,Form_Remark %>' /></div>
                    <div>
                        <asp:TextBox ID="RemarkCtl" runat="server" CssClass="InputText" TextMode="multiline"
                            Height="60px" Columns="140"></asp:TextBox></div>
                </td>
            </tr>
        </table>
    </div>

    <br />
    <div class="title">
        <asp:Label runat="server" meta:resourcekey="Title_Label1" /></div>
    <div class="searchDiv">
        <table class="searchTable">
            <tr>
                <td style="width: 200px" align="left">
                    <div class="field_title">
                        <asp:Label ID="Label2" runat="server" Text="<%$Resources:Common,Form_Customer %>" /></div>
                    <div>
                        <asp:TextBox ID="txtCustomerName" runat="server" Width="170px"></asp:TextBox></div>
                    <asp:RequiredFieldValidator ID="RFCustomerName" runat="server" ControlToValidate="txtCustomerName"
                        ValidationGroup="add" meta:resourcekey="RequiredFieldValidator_RFCustomerName" Display="None" InitialValue=""></asp:RequiredFieldValidator>
                </td>
                <td colspan="2" style="width: 400px">
                    <div class="field_title">
                        <asp:Label runat="server" meta:resourcekey="InvitationInfo_Label_AttenderNames" /></div>
                    <div>
                        <asp:TextBox ID="txtAttenderNames" runat="server" Width="370px"></asp:TextBox></div>
                    <asp:RequiredFieldValidator ID="RFAttenderNames" runat="server" ControlToValidate="txtAttenderNames"
                        ValidationGroup="add" meta:resourcekey="RequiredFieldValidator_RFAttenderNames" Display="None" InitialValue=""></asp:RequiredFieldValidator>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label runat="server" meta:resourcekey="InvitationInfo_Label_BusinessRelation" /></div>
                    <div>
                        <asp:TextBox ID="txtBusinessRelation" runat="server" Width="170px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFBussinessRelation" runat="server" ControlToValidate="txtBusinessRelation"
                            ValidationGroup="add" meta:resourcekey="RequiredFieldValidator_RFBussinessRelation" Display="None" InitialValue=""></asp:RequiredFieldValidator>
                    </div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label runat="server" meta:resourcekey="InvitationInfo_Label_InvitationType" /></div>
                    <div>
                        <asp:TextBox ID="txtInvitationType" runat="server" Width="170px"></asp:TextBox></div>
                    <asp:RequiredFieldValidator ID="RFInvitationType" runat="server" ControlToValidate="txtInvitationType"
                        ValidationGroup="add" meta:resourcekey="RequiredFieldValidator_RFInvitationType" Display="None" InitialValue=""></asp:RequiredFieldValidator>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label4" runat="server" meta:resourcekey="InvitationInfo_Label_AttenderCount" /></div>
                    <div>
                        <asp:TextBox ID="txtAttenderCount" runat="server" Width="170px"></asp:TextBox></div>
                    <asp:RequiredFieldValidator ID="RFAttenderCount" runat="server" ControlToValidate="txtAttenderCount"
                        ValidationGroup="add" meta:resourcekey="RequiredFieldValidator_RFAttenderCount" Display="None" InitialValue=""></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label1" runat="server" meta:resourcekey="InvitationInfo_Label_OccuredDate" /></div>
                    <div>
                        <uc1:YearAndMonth ID="UCOccuredDate" runat="server" />
                        <asp:RequiredFieldValidator ID="RFOccuredDate" runat="server" ControlToValidate="UCOccuredDate$txtDate"
                            ValidationGroup="add" meta:resourcekey="RequiredFieldValidator_RFOccuredDate" Display="None" InitialValue=""></asp:RequiredFieldValidator>
                    </div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label runat="server" meta:resourcekey="InvitationInfo_Label_Place" /></div>
                    <div>
                        <asp:TextBox ID="txtPlace" runat="server" Width="170px"></asp:TextBox></div>
                    <asp:RequiredFieldValidator ID="RFPlace" runat="server" ControlToValidate="txtPlace"
                        ValidationGroup="add" meta:resourcekey="RequiredFieldValidator_RFPlace" Display="None" InitialValue=""></asp:RequiredFieldValidator>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_Currency" runat="server" Text="<%$Resources:Common,Form_Currency %>" /></div>
                    <div>
                        <asp:DropDownList ID="dplCurrency" runat="server" DataSourceID="sdsCurrency" DataTextField="CurrencyShortName"
                            DataValueField="CurrencyID" Width="170px">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="sdsCurrency" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                            SelectCommand=" SELECT [CurrencyID],[CurrencyShortName] FROM [Currency] where IsActive=1 order by CurrencyID ">
                        </asp:SqlDataSource>
                    </div>
                </td>
                <td style="width: 200px" align="left">
                    <div class="field_title">
                        <asp:Label ID="Form_ExchangeRate" runat="server" Text="<%$Resources:Common,Form_ExchangeRate %>" />
                    </div>
                    <div>
                        <asp:TextBox ID="txtExchageRate" Text="1.0000" runat="server" Width="170px"></asp:TextBox></div>
                    <asp:RequiredFieldValidator ID="RFExchangeRate" runat="server" ControlToValidate="txtExchageRate"
                        ValidationGroup="add" meta:resourcekey="RequiredFieldValidator_RFExchangeRate" Display="None" InitialValue=""></asp:RequiredFieldValidator>
                </td>
                <td style="width: 200px" align="left">
                    <div class="field_title">
                        <asp:Label runat="server" meta:resourcekey="InvitationInfo_Label_Amount" /></div>
                    <div>
                        <asp:TextBox ID="txtAmount" runat="server" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label runat="server" meta:resourcekey="InvitationInfo_Label_AmountRMB" /></div>
                    <div>
                        <asp:TextBox ID="txtAmountRMB" Text="0" runat="server" Width="170px" ReadOnly="true"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label6" runat="server" meta:resourcekey="InvitationInfo_Label_BusinessPurpose" /></div>
                    <div>
                        <asp:TextBox ID="txtBusinessPurpose" runat="server" Width="170px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFBusinessPurpose" runat="server" ControlToValidate="txtBusinessPurpose"
                            ValidationGroup="add" meta:resourcekey="RequiredFieldValidator_RFBusinessPurpose" Display="None" InitialValue=""></asp:RequiredFieldValidator>
                    </div>
                </td>
                <td>
                    <div class="field_title">
                        <asp:Label ID="Label11" runat="server" Text='<%$Resources:Common,Form_ExpenseItem %>' /></div>
                    <div>
                        <asp:DropDownList runat="server" ID="ddlManageExpenseItem" DataTextField="ManageExpenseItemName"
                            DataValueField="ManageExpenseItemID" Width="180px" DataSourceID="odsManageExpenseItem">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="odsManageExpenseItem" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                            SelectCommand="select ManageExpenseItemID,ManageExpenseItemName from ManageExpenseItem join ManageExpenseCategoy on ManageExpenseItem.ManageExpenseCategoyID = ManageExpenseCategoy.ManageExpenseCategoyID  where ManageExpenseItem.IsActive=1 and ManageExpenseCategoy.FormTypeID=3 order by ManageExpenseItemName">
                        </asp:SqlDataSource>
                    </div>
                </td>
                <td style="width: 200px" valign="top">
                    <div class="field_title">
                        <asp:Label ID="Label12" runat="server" meta:resourcekey="InvitationInfo_Label_ApplyNo" /></div>
                    <div style="margin-top: 5px">
                        <asp:HyperLink ID="ApplyFormNoCtl" runat="server"></asp:HyperLink></div>
                </td>

            </tr>
        </table>
    </div>
    <asp:UpdatePanel ID="upCustomer" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div style="margin-top: 20px; width: 1200px; text-align: right">
                <asp:ValidationSummary ID="vsAdd" runat="server" ShowMessageBox="true" ShowSummary="false"
                    ValidationGroup="add" />
                <asp:Button ID="SubmitBtn" runat="server" CssClass="button_nor" CausesValidation="true"
                    ValidationGroup="add" OnClick="SubmitBtn_Click" Text='<%$Resources:Common,Button_Submit %>' />
                <asp:Button ID="SaveBtn" runat="server" CssClass="button_nor" OnClick="SaveBtn_Click" 
                    Text='<%$Resources:Common,Button_Save %>' />
                <asp:Button ID="CancelBtn" runat="server" CssClass="button_nor" OnClick="CancelBtn_Click"
                    Text='<%$Resources:Common,Button_Back %>' />
                <asp:Button ID="DeleteBtn" runat="server" CssClass="button_nor" OnClick="DeleteBtn_Click"
                    Text='<%$Resources:Common,Button_Delete %>' />
            </div>
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
    <uc3:ucUpdateProgress ID="upUP" runat="server" vassociatedupdatepanelid="upCustomer" />
</asp:Content>
