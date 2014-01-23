<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="FormPersonalReimburseApply.aspx.cs" UICulture="Auto" Culture="Auto" Inherits="Form_FormPersonalReimburseApply" %>

<%@ Register Src="../UserControls/YearAndMonthUserControl.ascx" TagName="YearAndMonth" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/MultiAttachmentFile.ascx" TagName="UCFlie" TagPrefix="uc2" %>
<%@ Register Src="../UserControls/UCDateInput.ascx" TagName="DateInput" TagPrefix="uc3" %>
<%@ Register Src="../UserControls/ucUpdateProgress.ascx" TagName="ucUpdateProgress" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <script src="../Script/DateInput.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function ParameterChanged(ExchangeRateCtl, ApplyAmountCtl, RMBCtl) {
            var txtExchangeRate = document.getElementById(ExchangeRateCtl);
            var txtApplyAmount = document.getElementById(ApplyAmountCtl);
            var txtRMB = document.getElementById(RMBCtl);

            if (txtExchangeRate.value != "" && isNaN(txtExchangeRate.value)) {
                alert("请录入数字");
                window.setTimeout(function () { document.getElementById(ExchangeRateCtl).focus(); }, 0);
                return false;
            }
            if (txtApplyAmount.value != "" && isNaN(txtApplyAmount.value)) {
                alert("请录入数字");
                window.setTimeout(function () { document.getElementById(ApplyAmountCtl).focus(); }, 0);
                return false;
            }

            if (!isNaN(parseFloat(txtApplyAmount.value)) && !isNaN(parseFloat(txtExchangeRate.value))) {
                var ExchangeRate = parseFloat(txtExchangeRate.value);
                var ApplyAmount = parseFloat(txtApplyAmount.value);
                var amount = ApplyAmount * ExchangeRate;
                txtRMB.value = commafy(amount.toFixed(2));
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
                <td style="width: 200px" align="left">
                    <div class="field_title">
                        <asp:Label ID="Form_ApplyUser" Text='<%$Resources:Common,Form_ApplyUser %>' runat="server" /></div>
                    <div>
                        <asp:TextBox ID="StuffNameCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px" align="left">
                    <div class="field_title">
                        <asp:Label ID="Form_Position" runat="server" Text='<%$Resources:Common,Form_Position %>' /></div>
                    <div>
                        <asp:TextBox ID="PositionNameCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px" align="left">
                    <div class="field_title">
                        <asp:Label ID="Form_Organization" runat="server" Text='<%$Resources:Common,Form_Organization %>' /></div>
                    <div>
                        <asp:TextBox ID="DepartmentNameCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px" align="left">
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
                <td style="width: 200px" align="left">
                    <div class="field_title">
                        <asp:Label ID="Form_Period" runat="server" Text='<%$Resources:Common,Form_Period %>' /><span class="requiredLable">*</span></div>
                    <div>
                        <asp:DropDownList ID="PeriodDDL" runat="server" DataSourceID="odsPeriod" 
                            DataTextField="PeriodReimburse" DataValueField="PeriodReimburseID" Width="170px">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="odsPeriod" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                            SelectCommand=" select 0 PeriodReimburseID,'Please Select' PeriodReimburse Union SELECT [PeriodReimburseID], convert(varchar(50),year(PeriodReimburse))+'-'+Right(100+month(PeriodReimburse),2) PeriodReimburse FROM [PeriodReimburse] ">
                        </asp:SqlDataSource>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="3" valign="top">
                    <div class="field_title">
                        <asp:Label ID="Label5" runat="server" meta:resourcekey="FormPersonalReimburseApply_Label_ApplyReason" /></div>
                    <div>
                        <asp:TextBox ID="RemarkCtl" runat="server" CssClass="InputText" TextMode="multiline"
                            Height="60px" Columns="75"></asp:TextBox></div>
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
        <asp:Label ID="Label11" runat="server" meta:resourcekey="FormPersonalReimburseApply_Label_ReimburseDetails" /></div>
    <asp:UpdatePanel ID="upPersonalReimburseDetails" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <gc:GridView ID="gvPersonalReimburseDetails" runat="server" ShowFooter="True" CssClass="GridView"
                AutoGenerateColumns="False" DataKeyNames="FormPersonalReimburseDetailID" DataSourceID="odsPersonalReimburseDetails"
                OnRowDataBound="gvPersonalReimburseDetails_RowDataBound" CellPadding="0">
                <Columns>
                    <asp:TemplateField meta:resourcekey="PersonalReimburseDetailsGridView_TemplateField_OccurDate">
                        <ItemTemplate>
                            <asp:Label ID="lblOccurDate" runat="server" Text='<%# Eval("OccurDate", "{0:yyyy-MM-dd}") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <uc3:DateInput ID="UCOccurDate" runat="server" IsReadOnly="false" SelectedDate='<%# Bind("OccurDate") %>'
                                IsExpensePeriod="true" />
                        </EditItemTemplate>
                        <ItemStyle Width="200px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Common,Form_ExpenseItem %>" >
                        <ItemTemplate>
                            <asp:Label ID="lblManageExpenseItem" runat="server" Text='<%# GetManageExpenseItemNameByID(Eval("ManageExpenseItemID")) %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList runat="server" ID="ddlManageExpenseItem" DataTextField="ManageExpenseItemName"
                                AutoPostBack="true" DataValueField="ManageExpenseItemID" Width="180px" DataSourceID="odsManageExpenseItem"
                                SelectedValue='<%# Bind("ManageExpenseItemID") %>'>
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="odsManageExpenseItem" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                SelectCommand="select ManageExpenseItemID,ManageExpenseItemName from ManageExpenseItem join ManageExpenseCategoy on ManageExpenseItem.ManageExpenseCategoyID = ManageExpenseCategoy.ManageExpenseCategoyID  where ManageExpenseItem.IsActive=1 and ManageExpenseCategoy.FormTypeID=4 order by ManageExpenseItemID">
                            </asp:SqlDataSource>
                        </EditItemTemplate>
                        <ItemStyle Width="200px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Common,Form_Currency %>">
                        <ItemTemplate>
                            <asp:Label ID="lblCurrency" runat="server" Text='<%# GetCurrencyByID(Eval("CurrencyID")) %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList runat="server" ID="ddlCurrency" DataTextField="CurrencyFullName"
                                DataValueField="CurrencyID" Width="100px" DataSourceID="odsCurrency" SelectedValue='<%# Bind("CurrencyID") %>'>
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="odsCurrency" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                SelectCommand="select CurrencyID,CurrencyFullName from dbo.Currency where IsActive=1 order by CurrencyID">
                            </asp:SqlDataSource>
                        </EditItemTemplate>
                        <ItemStyle Width="150px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Common,Form_ExchangeRate %>">
                        <ItemTemplate>
                            <asp:Label ID="lblExchangeRate" runat="server" Text='<%# Eval("ExchangeRate") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtExchangeRate" runat="server" Text='<%# Bind("ExchangeRate") %>'
                                Width="100px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle Width="150px" HorizontalAlign="Center" />
                        <FooterTemplate>
                            <asp:Label runat="server" ID="lblSum" Text="<%$Resources:Common,Form_Total %>"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle CssClass="RedTextAlignCenter" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Common,Form_AmountRMB %>">
                        <ItemTemplate>
                            <asp:Label ID="lblApplyAmount" runat="server" Text='<%# Eval("ApplyAmount","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtApplyAmount" runat="server" Text='<%# Bind("ApplyAmount") %>'
                                Width="60px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle Width="75px" HorizontalAlign="Right" />
                        <FooterTemplate>
                            <asp:Label runat="server" ID="lblTotal"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle CssClass="RedTextAlignCenter" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="PersonalReimburseDetailsGridView_TemplateField_RMB">
                        <ItemTemplate>
                            <asp:Label ID="lblRMB" runat="server" Text='<%# Eval("RMB","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtRMB" runat="server" Text='<%# Eval("RMB") %>' ReadOnly="true"
                                Width="60px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle Width="75px" HorizontalAlign="Right" />
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
                            <asp:TextBox ID="txtRemark" runat="server" Text='<%# Bind("Remark") %>' Width="280px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle Width="300px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="Edit"
                                Text='<%$Resources:Common,Button_Edit%>'></asp:LinkButton>
                            <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Delete"
                                Text='<%$Resources:Common,Button_Delete%>'></asp:LinkButton>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:RequiredFieldValidator ID="RF1" runat="server" ControlToValidate="UCOccurDate$txtDate"
                                meta:resourcekey="RequiredFieldValidator_RF1" Display="None" ValidationGroup="NewDetailRow"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtApplyAmount"
                                meta:resourcekey="RequiredFieldValidator_RF2" Display="None" ValidationGroup="NewDetailRow"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtExchangeRate"
                                meta:resourcekey="RequiredFieldValidator_RF3" Display="None" ValidationGroup="NewDetailRow"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtApplyAmount"
                                ValidationExpression="<%$ Resources:RegularExpressions, Money %>" Display="None"
                                meta:resourcekey="RequiredFieldValidator_RF4" ValidationGroup="NewDetailRow"></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtExchangeRate"
                                ValidationExpression="<%$ Resources:RegularExpressions, Double %>" Display="None"
                                meta:resourcekey="RequiredFieldValidator_RF5" ValidationGroup="NewDetailRow"></asp:RegularExpressionValidator>
                            <asp:ValidationSummary ID="ValidationSummaryINS" runat="server" ShowMessageBox="True"
                                ShowSummary="False" ValidationGroup="NewDetailRow" />
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="true" CommandName="Update"
                                ValidationGroup="NewDetailRow" Text='<%$Resources:Common,Button_Update %>'></asp:LinkButton>
                            <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel"
                                Text='<%$Resources:Common,Button_Back %>'></asp:LinkButton>
                        </EditItemTemplate>
                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td style="width: 200px;" class="Empty1">
                                <asp:Label ID="Label12" runat="server" meta:resourcekey="FormPersonalReimburseApply_Label_OccurDate" />
                            </td>
                            <td style="width: 200px;">
                                <asp:Label ID="Label13" runat="server" Text="<%$Resources:Common,Form_ExpenseItem %>" />
                            </td>
                            <td style="width: 150px;">
                                <asp:Label ID="Label14" runat="server" Text="<%$Resources:Common,Form_Currency %>" />
                            </td>
                            <td style="width: 150px;">
                                <asp:Label ID="Label17" runat="server" Text="<%$Resources:Common,Form_ExchangeRate %>" />
                            </td>
                            <td style="width: 75px;">
                                <asp:Label ID="Label18" runat="server" Text="<%$Resources:Common,Form_AmountRMB %>" />
                            </td>
                            <td style="width: 75px;">
                                <asp:Label ID="Label21" runat="server" meta:resourcekey="FormPersonalReimburseApply_Label_RMB" />
                            </td>
                            <td style="width: 300px;">
                                <asp:Label ID="Label15" runat="server" Text="<%$Resources:Common,Form_Remark %>" />
                            </td>
                            <td style="width: 90px;">
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <HeaderStyle CssClass="Header" />
            </gc:GridView>
            <asp:FormView ID="fvPersonalReimburseDetails" runat="server" DataKeyNames="FormPersonalReimburseDetailID"
                DataSourceID="odsPersonalReimburseDetails" OnDataBound="fvPersonalReimburseDetails_DataBound"
                DefaultMode="Insert" CellPadding="0">
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr>
                            <td style="width: 200px;" align="center">
                                <uc3:DateInput ID="UCOccurDate" runat="server" IsReadOnly="false" IsExpensePeriod="true"
                                    SelectedDate='<%# Bind("OccurDate") %>' />
                            </td>
                            <td style="width: 200px;" align="center">
                                <asp:DropDownList runat="server" ID="ddlManageExpenseItem" DataTextField="ManageExpenseItemName"
                                    DataValueField="ManageExpenseItemID" Width="180px" DataSourceID="odsManageExpenseItem"
                                    SelectedValue='<%# Bind("ManageExpenseItemID") %>'>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="odsManageExpenseItem" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                    SelectCommand="select ManageExpenseItemID,ManageExpenseItemName from ManageExpenseItem join ManageExpenseCategoy on ManageExpenseItem.ManageExpenseCategoyID = ManageExpenseCategoy.ManageExpenseCategoyID  where ManageExpenseItem.IsActive=1 and ManageExpenseCategoy.FormTypeID=4 order by ManageExpenseItemID">
                                </asp:SqlDataSource>
                            </td>
                            <td style="width: 150px;" align="center">
                                <asp:DropDownList runat="server" ID="ddlCurrency" DataTextField="CurrencyFullName"
                                    SelectedValue='<%# Bind("CurrencyID") %>' DataValueField="CurrencyID" Width="100px"
                                    DataSourceID="odsCurrency">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="odsCurrency" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                    SelectCommand="select CurrencyID,CurrencyFullName from dbo.Currency where IsActive=1 order by CurrencyID">
                                </asp:SqlDataSource>
                            </td>
                            <td style="width: 150px;" align="center">
                                <asp:TextBox ID="txtExchangeRate" MaxLength="15" runat="server" Width="100px" Text='<%# Bind("ExchangeRate") %>'></asp:TextBox>
                            </td>
                            <td style="width: 75px;" align="center">
                                <asp:TextBox ID="txtApplyAmount" runat="server" MaxLength="8" Width="60px" Text='<%# Bind("ApplyAmount") %>'></asp:TextBox>
                            </td>
                            <td style="width: 75px;" align="center">
                                <asp:TextBox ID="txtRMB" MaxLength="8" runat="server" Width="60px" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td style="width: 300px;" align="center">
                                <asp:TextBox ID="txtRemark" runat="server" MaxLength="30" Width="280px" Text='<%# Bind("Remark") %>'></asp:TextBox>
                            </td>
                            <td style="width: 90px;" align="center">
                                <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    Text='<%$Resources:Common,Button_Add %>' ValidationGroup="DetailRow"></asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <asp:RequiredFieldValidator ID="RF1" runat="server" ControlToValidate="UCOccurDate$txtDate"
                                meta:resourcekey="RequiredFieldValidator_RF1" Display="None" ValidationGroup="DetailRow"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtApplyAmount"
                                meta:resourcekey="RequiredFieldValidator_RF2" Display="None" ValidationGroup="DetailRow"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtExchangeRate"
                                meta:resourcekey="RequiredFieldValidator_RF3" Display="None" ValidationGroup="DetailRow"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtApplyAmount"
                                ValidationExpression="<%$ Resources:RegularExpressions, Money %>" Display="None"
                                meta:resourcekey="RequiredFieldValidator_RF4" ValidationGroup="DetailRow"></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtExchangeRate"
                                ValidationExpression="<%$ Resources:RegularExpressions, Double %>" Display="None"
                                meta:resourcekey="RequiredFieldValidator_RF5" ValidationGroup="DetailRow"></asp:RegularExpressionValidator>
                            <asp:ValidationSummary ID="ValidationSummaryINS" runat="server" ShowMessageBox="True"
                                ShowSummary="False" ValidationGroup="DetailRow" />
                        </tr>
                    </table>
                    <br />
                </InsertItemTemplate>
            </asp:FormView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="odsPersonalReimburseDetails" runat="server" DeleteMethod="DeleteFormPersonalReimburseDetailByID"
        SelectMethod="GetFormPersonalReimburseDetail" UpdateMethod="UpdateFormPersonalReimburseDetail"
        TypeName="BusinessObjects.FormTEBLL" InsertMethod="AddFormPersonalReimburseDetail"
        OnObjectCreated="odsPersonalReimburseDetails_ObjectCreated" OnInserting="odsPersonalReimburseDetails_Inserting">
        <DeleteParameters>
            <asp:Parameter Name="FormPersonalReimburseDetailID" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="OccurDate" Type="Datetime" />
            <asp:Parameter Name="ManageExpenseItemID" Type="Int32" />
            <asp:Parameter Name="CurrencyID" Type="Int32" />
            <asp:Parameter Name="ApplyAmount" Type="Decimal" />
            <asp:Parameter Name="ExchangeRate" Type="Decimal" />
            <asp:Parameter Name="Remark" Type="String" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="OccurDate" Type="Datetime" />
            <asp:Parameter Name="ManageExpenseItemID" Type="Int32" />
            <asp:Parameter Name="CurrencyID" Type="Int32" />
            <asp:Parameter Name="ApplyAmount" Type="Decimal" />
            <asp:Parameter Name="ExchangeRate" Type="Decimal" />
            <asp:Parameter Name="Remark" Type="String" />
            <asp:Parameter Name="FormPersonalReimburseDetailID" Type="Int32" />
        </UpdateParameters>
    </asp:ObjectDataSource>
    <br />
    <asp:UpdatePanel ID="upCustomer" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div style="margin-top: 20px; width: 1200px; text-align: right">
                <asp:Button ID="SubmitBtn" runat="server" CssClass="button_nor" OnClick="SubmitBtn_Click"
                    Text="<%$Resources:Common,Button_Submit %>" />
                <asp:Button ID="SaveBtn" runat="server" CssClass="button_nor" OnClick="SaveBtn_Click"
                    Text="<%$Resources:Common,Button_Save %>" />
                <asp:Button ID="CancelBtn" runat="server" CssClass="button_nor" OnClick="CancelBtn_Click"
                    Text="<%$Resources:Common,Button_Back %>" />
                <asp:Button ID="DeleteBtn" runat="server" CssClass="button_nor" OnClick="DeleteBtn_Click"
                    Text="<%$Resources:Common,Button_Delete %>" />
            </div>
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
    <uc4:ucUpdateProgress ID="upUP" runat="server" vassociatedupdatepanelid="upCustomer" />
</asp:Content>
