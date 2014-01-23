<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="FormTravelReimburseApply.aspx.cs" UICulture="Auto" Culture="Auto" Inherits="Form_FormTravelReimburseApply" %>

<%@ Register Src="../UserControls/YearAndMonthUserControl.ascx" TagName="YearAndMonth" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/MultiAttachmentFile.ascx" TagName="UCFlie" TagPrefix="uc2" %>
<%@ Register Src="../UserControls/UCDateInput.ascx" TagName="DateInput" TagPrefix="uc3" %>
<%@ Register Src="../UserControls/ucUpdateProgress.ascx" TagName="ucUpdateProgress" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <script src="../Script/DateInput.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function ParameterChanged(ExchangeRateCtl, UnitPriceCtl, FrequencyCtl, CostCtl) {

            var txtExchangeRate = document.getElementById(ExchangeRateCtl);
            var txtUnitPrice = document.getElementById(UnitPriceCtl);
            var txtFrequency = document.getElementById(FrequencyCtl);
            var txtCost = document.getElementById(CostCtl);

            if (txtExchangeRate.value != "" && isNaN(txtExchangeRate.value)) {
                alert("请录入数字");
                window.setTimeout(function () { document.getElementById(ExchangeRateCtl).focus(); }, 0);
                return false;
            }
            if (txtUnitPrice.value != "" && isNaN(txtUnitPrice.value)) {
                alert("请录入数字");
                window.setTimeout(function () { document.getElementById(UnitPriceCtl).focus(); }, 0);
                return false;
            }
            if (txtFrequency.value != "" && isNaN(txtFrequency.value)) {
                alert("请录入数字");
                window.setTimeout(function () { document.getElementById(FrequencyCtl).focus(); }, 0);
                return false;
            }
            if (!isNaN(parseFloat(txtUnitPrice.value)) && !isNaN(parseFloat(txtExchangeRate.value)) && !isNaN(parseFloat(txtFrequency.value))) {
                var UnitPrice = parseFloat(txtUnitPrice.value);
                var ExchangeRate = parseFloat(txtExchangeRate.value);
                var Frequency = parseFloat(txtFrequency.value);
                var amount = UnitPrice * ExchangeRate * Frequency;
                txtCost.value = commafy(amount.toFixed(2));
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
                        <asp:Label ID="Form_Period" runat="server" Text='<%$Resources:Common,Form_Period %>' /><span
                            class="requiredLable">*</span></div>
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
                        <asp:Label ID="Label5" runat="server" meta:resourcekey="FormTravelReimburseApply_Label_ApplyReason" /></div>
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
    <div class="title"><asp:Label ID="Label11" runat="server" meta:resourcekey="FormTravelReimburseApply_Label_ReimburseDetails" /></div>
    <asp:UpdatePanel ID="upTravelReimburseDetails" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <gc:GridView ID="gvTravelReimburseDetails" runat="server" ShowFooter="True" CssClass="GridView"
                AutoGenerateColumns="False" DataKeyNames="FormTravelReimburseDetailID" DataSourceID="odsTravelReimburseDetails"
                OnRowDataBound="gvTravelReimburseDetails_RowDataBound" CellPadding="0">
                <Columns>
                    <asp:TemplateField meta:resourcekey="TravelReimburseDetailsGridView_TemplateField_OccurDate">
                        <ItemTemplate>
                            <asp:Label ID="lblOccurDate" runat="server" Text='<%# Eval("OccurDate", "{0:yyyy-MM-dd}") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <uc3:DateInput ID="UCOccurDate" runat="server" IsReadOnly="false" SelectedDate='<%# Bind("OccurDate") %>'
                                IsExpensePeriod="true" />
                        </EditItemTemplate>
                        <ItemStyle Width="150px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TravelReimburseDetailsGridView_TemplateField_City">
                        <ItemTemplate>
                            <asp:Label ID="lblCity" runat="server" Text='<%# GetCityByID(Eval("CityID")) %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList runat="server" ID="ddlCity" DataTextField="CityName" DataValueField="CityID"
                                CausesValidation="True" ValidationGroup="CountDetailRow" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlCity_SelectedIndexChanged" Width="80px" DataSourceID="odsCity"
                                SelectedValue='<%# Bind("CityID") %>'>
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="odsCity" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                SelectCommand="select CityID,CityName from dbo.City where IsActive=1 order by CityName">
                            </asp:SqlDataSource>
                        </EditItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TravelReimburseDetailsGridView_TemplateField_Destination">
                        <ItemTemplate>
                            <asp:Label ID="lblDestination" runat="server" Text='<%# Bind("Destination") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtDestination" runat="server" Text='<%# Bind("Destination") %>'
                                ReadOnly="true" Width="50px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle Width="70px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Common,Form_ExpenseItem %>">
                        <ItemTemplate>
                            <asp:Label ID="lblManageExpenseItem" runat="server" Text='<%# GetManageExpenseItemNameByID(Eval("ManageExpenseItemID")) %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList runat="server" ID="ddlManageExpenseItem" DataTextField="ManageExpenseItemName"
                                CausesValidation="True" ValidationGroup="CountDetailRow" AutoPostBack="true"
                                DataValueField="ManageExpenseItemID" Width="150px" DataSourceID="odsManageExpenseItem"
                                OnSelectedIndexChanged="ddlManageExpenseItem_SelectedIndexChanged" SelectedValue='<%# Bind("ManageExpenseItemID") %>'>
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="odsManageExpenseItem" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                SelectCommand="select ManageExpenseItemID,ManageExpenseItemName from ManageExpenseItem join ManageExpenseCategoy on ManageExpenseItem.ManageExpenseCategoyID = ManageExpenseCategoy.ManageExpenseCategoyID  where ManageExpenseItem.IsActive=1 and ManageExpenseCategoy.FormTypeID=1 order by ManageExpenseItemName">
                            </asp:SqlDataSource>
                        </EditItemTemplate>
                        <ItemStyle Width="170px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Common,Form_Currency %>">
                        <ItemTemplate>
                            <asp:Label ID="lblCurrency" runat="server" Text='<%# GetCurrencyByID(Eval("CurrencyID")) %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList runat="server" ID="ddlCurrency" DataTextField="CurrencyFullName"
                                DataValueField="CurrencyID" Width="70px" DataSourceID="odsCurrency" SelectedValue='<%# Bind("CurrencyID") %>'>
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="odsCurrency" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                SelectCommand="select CurrencyID,CurrencyFullName from dbo.Currency where IsActive=1 order by CurrencyFullName">
                            </asp:SqlDataSource>
                        </EditItemTemplate>
                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Common,Form_ExchangeRate %>">
                        <ItemTemplate>
                            <asp:Label ID="lblExchangeRate" runat="server" Text='<%# Eval("ExchangeRate") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtExchangeRate" runat="server" Text='<%# Bind("ExchangeRate") %>'
                                Width="50px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle Width="70px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TravelReimburseDetailsGridView_TemplateField_UnitPrice">
                        <ItemTemplate>
                            <asp:Label ID="lblUnitPrice" runat="server" Text='<%# Eval("UnitPrice") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtUnitPrice" runat="server" Text='<%# Bind("UnitPrice") %>' Width="60px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TravelReimburseDetailsGridView_TemplateField_Frequency">
                        <ItemTemplate>
                            <asp:Label ID="lblFrequency" runat="server" Text='<%# Eval("Frequency") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtFrequency" runat="server" Text='<%# Bind("Frequency") %>' Width="50px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle Width="70px" HorizontalAlign="Center" />
                        <FooterTemplate>
                            <asp:Label runat="server" ID="lblSum" Text="<%$Resources:Common,Form_Total %>"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle CssClass="RedTextAlignCenter" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TravelReimburseDetailsGridView_TemplateField_Cost">
                        <ItemTemplate>
                            <asp:Label ID="lblCost" runat="server" Text='<%# Eval("Cost","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCost" runat="server" Text='<%# Eval("Cost") %>' Width="70px"
                                ReadOnly="true"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Right" />
                        <FooterTemplate>
                            <asp:Label runat="server" ID="lblTotalRMB"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle CssClass="RedTextAlignCenter" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TravelReimburseDetailsGridView_TemplateField_PayMan">
                        <ItemTemplate>
                            <asp:Label ID="lblPayMan" runat="server" Text='<%# GetPayMan(Eval("PayMan")) %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList runat="server" ID="ddlPayMan" Width="80px" Enabled="false" SelectedValue='<%# Bind("PayMan") %>'>
                                <asp:ListItem meta:resourcekey="TravelReimburseDetailsGridView_TemplateField_ddlPayMan0" Value="0"></asp:ListItem>
                                <asp:ListItem meta:resourcekey="TravelReimburseDetailsGridView_TemplateField_ddlPayMan1" Value="1"></asp:ListItem>
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Common,Form_Remark %>">
                        <ItemTemplate>
                            <asp:Label ID="lblRemark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtRemark" runat="server" Text='<%# Bind("Remark") %>' Width="150px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle Width="178px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="Edit"
                                Text='<%$Resources:Common,Button_Edit%>'></asp:LinkButton>
                            <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Delete"
                                Text='<%$Resources:Common,Button_Delete%>'></asp:LinkButton>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:RequiredFieldValidator ID="RF1" runat="server" ControlToValidate="UCOccurDate$txtDate" meta:resourcekey="ValidatorTravelReimburseApply_Message_RF1"
                                Display="None" ValidationGroup="DetailRow"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RF3" runat="server" ControlToValidate="txtDestination" meta:resourcekey="ValidatorTravelReimburseApply_Message_RF3"
                                Display="None" ValidationGroup="DetailRow"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFrequency"
                                meta:resourcekey="ValidatorTravelReimburseApply_Message_RequiredFieldValidator1" Display="None" ValidationGroup="DetailRow"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtUnitPrice"
                                meta:resourcekey="ValidatorTravelReimburseApply_Message_RequiredFieldValidator2" Display="None" ValidationGroup="DetailRow"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtExchangeRate"
                                meta:resourcekey="ValidatorTravelReimburseApply_Message_RequiredFieldValidator3" Display="None" ValidationGroup="DetailRow"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtFrequency"
                                ValidationExpression="<%$ Resources:RegularExpressions, Number %>" Display="None"
                                meta:resourcekey="ValidatorTravelReimburseApply_Message_RequiredFieldValidator7" ValidationGroup="DetailRow"></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtUnitPrice"
                                ValidationExpression="<%$ Resources:RegularExpressions, Money %>" Display="None"
                                meta:resourcekey="ValidatorTravelReimburseApply_Message_RequiredFieldValidator8" ValidationGroup="DetailRow"></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="txtExchangeRate"
                                ValidationExpression="<%$ Resources:RegularExpressions, Double %>" Display="None"
                                meta:resourcekey="ValidatorTravelReimburseApply_Message_RequiredFieldValidator9" ValidationGroup="DetailRow"></asp:RegularExpressionValidator>
                            <asp:ValidationSummary ID="ValidationSummaryINS" runat="server" ShowMessageBox="True"
                                ShowSummary="False" ValidationGroup="DetailRow" />
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="true" CommandName="Update"
                                ValidationGroup="DetailRow" Text='<%$Resources:Common,Button_Update %>'></asp:LinkButton>
                            <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel"
                                Text='<%$Resources:Common,Button_Back %>'></asp:LinkButton>
                        </EditItemTemplate>
                        <ItemStyle Width="70px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td style="width: 150px;" class="Empty1">
                                <asp:Label runat="server" meta:resourcekey="FormTravelReimburseApply_Label_OccurDate" />
                            </td>
                            <td style="width: 100px;">
                                <asp:Label runat="server" meta:resourcekey="FormTravelReimburseApply_Label_City" />
                            </td>
                            <td style="width: 70px;">
                                <asp:Label runat="server" meta:resourcekey="FormTravelReimburseApply_Label_Destination" />
                            </td>
                            <td style="width: 170px;">
                                <asp:Label runat="server" Text="<%$Resources:Common,Form_ExpenseItem %>" />
                            </td>
                            <td style="width: 80px;">
                                <asp:Label runat="server" Text="<%$Resources:Common,Form_Currency %>" />
                            </td>
                            <td style="width: 70px;">
                                <asp:Label runat="server" Text="<%$Resources:Common,Form_ExchangeRate %>" />
                            </td>
                            <td style="width: 80px;">
                                <asp:Label runat="server" meta:resourcekey="FormTravelReimburseApply_Label_UnitPrice" />
                            </td>
                            <td style="width: 70px;">
                                <asp:Label runat="server" meta:resourcekey="FormTravelReimburseApply_Label_Frequency" />
                            </td>
                            <td style="width: 100px;">
                                <asp:Label runat="server" meta:resourcekey="FormTravelReimburseApply_Label_Cost" />
                            </td>
                            <td style="width: 90px;">
                                <asp:Label runat="server" meta:resourcekey="FormTravelReimburseApply_Label_PayMan" />
                            </td>
                            <td style="width: 178px;">
                                <asp:Label runat="server" Text="<%$Resources:Common,Form_Remark %>" />
                            </td>
                            <td style="width: 70px">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <HeaderStyle CssClass="Header" />
            </gc:GridView>
            <asp:FormView ID="fvTravelReimburseDetails" runat="server" DataKeyNames="FormTravelReimburseDetailID"
                DataSourceID="odsTravelReimburseDetails" OnDataBound="fvTravelReimburseDetails_DataBound"
                DefaultMode="Insert" CellPadding="0">
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr>
                            <td style="width: 150px;" align="center">
                                <uc3:DateInput ID="UCOccurDate" runat="server" IsReadOnly="false" SelectedDate='<%# Bind("OccurDate") %>'
                                    IsExpensePeriod="true" />
                            </td>
                            <td style="width: 100px;" align="center">
                                <asp:DropDownList runat="server" ID="ddlCity" DataTextField="CityName" DataValueField="CityID"
                                    SelectedValue='<%# Bind("CityID") %>' AutoPostBack="true" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged"
                                    Width="90px" DataSourceID="odsCity" CausesValidation="True" ValidationGroup="CountNewDetailRow">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="odsCity" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                    SelectCommand="select 0 as  CityID,'Please Select' as  CityName Union  select CityID,CityName from dbo.City where IsActive=1 ">
                                </asp:SqlDataSource>
                            </td>
                            <td style="width: 70px;" align="center">
                                <asp:TextBox ID="txtDestination" MaxLength="15" runat="server" ReadOnly="true" Width="50px"
                                    Text='<%# Bind("Destination") %>'></asp:TextBox>
                            </td>
                            <td style="width: 170px;" align="center">
                                <asp:DropDownList runat="server" ID="ddlManageExpenseItem" DataTextField="ManageExpenseItemName"
                                    SelectedValue='<%# Bind("ManageExpenseItemID") %>' OnSelectedIndexChanged="ddlManageExpenseItem_SelectedIndexChanged"
                                    DataValueField="ManageExpenseItemID" Width="160px" DataSourceID="odsManageExpenseItem"
                                    CausesValidation="True" ValidationGroup="CountNewDetailRow" AutoPostBack="true">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="odsManageExpenseItem" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                    SelectCommand="select 0 as  ManageExpenseItemID,'Please Select' as  ManageExpenseItemName Union select ManageExpenseItemID,ManageExpenseItemName from ManageExpenseItem join ManageExpenseCategoy on ManageExpenseItem.ManageExpenseCategoyID = ManageExpenseCategoy.ManageExpenseCategoyID  where ManageExpenseItem.IsActive=1 and ManageExpenseCategoy.FormTypeID=1 order by ManageExpenseItemID">
                                </asp:SqlDataSource>
                            </td>
                            <td style="width: 80px;" align="center">
                                <asp:DropDownList runat="server" ID="ddlCurrency" DataTextField="CurrencyFullName"
                                    SelectedValue='<%# Bind("CurrencyID") %>' DataValueField="CurrencyID" Width="70px"
                                    DataSourceID="odsCurrency">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="odsCurrency" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                    SelectCommand="select CurrencyID,CurrencyFullName from dbo.Currency where IsActive=1 order by CurrencyID">
                                </asp:SqlDataSource>
                            </td>
                            <td style="width: 70px;" align="center">
                                <asp:TextBox ID="txtExchangeRate" MaxLength="15" runat="server" Width="50px" Text='<%# Bind("ExchangeRate") %>'></asp:TextBox>
                            </td>
                            <td style="width: 80px;" align="center">
                                <asp:TextBox ID="txtUnitPrice" runat="server" MaxLength="40" Width="60px" Text='<%# Bind("UnitPrice") %>'></asp:TextBox>
                            </td>
                            <td style="width: 70px;" align="center">
                                <asp:TextBox ID="txtFrequency" MaxLength="15" runat="server" Width="50px" Text='<%# Bind("Frequency") %>'></asp:TextBox>
                            </td>
                            <td style="width: 100px;" align="center">
                                <asp:TextBox ID="txtCost" MaxLength="15" runat="server" Width="80px" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td style="width: 90px;" align="center">
                                <asp:DropDownList runat="server" ID="ddlPayMan" Width="80px" SelectedValue='<%# Bind("PayMan") %>'
                                    Enabled="false">
                                    <asp:ListItem meta:resourcekey="TravelReimburseDetailsGridView_TemplateField_ddlPayMan0" Value="0"></asp:ListItem>
                                    <asp:ListItem meta:resourcekey="TravelReimburseDetailsGridView_TemplateField_ddlPayMan1" Value="1"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="width: 178px;" align="center">
                                <asp:TextBox ID="txtRemark" runat="server" MaxLength="30" Width="150px" Text='<%# Bind("Remark") %>'></asp:TextBox>
                            </td>
                            <td style="width: 70px;" align="center">
                                <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    Text='<%$Resources:Common,Button_Add %>' ValidationGroup="NewDetailRow"></asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <asp:RequiredFieldValidator ID="RF1" runat="server" ControlToValidate="UCOccurDate$txtDate"
                                meta:resourcekey="ValidatorTravelReimburseApply_Message_RF1" Display="None" ValidationGroup="NewDetailRow"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RF3" runat="server" ControlToValidate="txtDestination"
                                meta:resourcekey="ValidatorTravelReimburseApply_Message_RF3" Display="None" ValidationGroup="NewDetailRow"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFrequency"
                                meta:resourcekey="ValidatorTravelReimburseApply_Message_RequiredFieldValidator1" Display="None" ValidationGroup="NewDetailRow"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtUnitPrice"
                                meta:resourcekey="ValidatorTravelReimburseApply_Message_RequiredFieldValidator2" Display="None" ValidationGroup="NewDetailRow"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtExchangeRate"
                                meta:resourcekey="ValidatorTravelReimburseApply_Message_RequiredFieldValidator3" Display="None" ValidationGroup="NewDetailRow"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtFrequency"
                                ValidationExpression="<%$ Resources:RegularExpressions, Number %>" Display="None"
                                meta:resourcekey="ValidatorTravelReimburseApply_Message_RequiredFieldValidator7" ValidationGroup="NewDetailRow"></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtUnitPrice"
                                ValidationExpression="<%$ Resources:RegularExpressions, Money %>" Display="None"
                                meta:resourcekey="ValidatorTravelReimburseApply_Message_RequiredFieldValidator8" ValidationGroup="NewDetailRow"></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtExchangeRate"
                                ValidationExpression="<%$ Resources:RegularExpressions, Double %>" Display="None"
                                meta:resourcekey="ValidatorTravelReimburseApply_Message_RequiredFieldValidator9" ValidationGroup="NewDetailRow"></asp:RegularExpressionValidator>
                            <asp:ValidationSummary ID="ValidationSummaryINS" runat="server" ShowMessageBox="True"
                                ShowSummary="False" ValidationGroup="NewDetailRow" />
                        </tr>
                    </table>
                    <br />
                </InsertItemTemplate>
            </asp:FormView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="odsTravelReimburseDetails" runat="server" DeleteMethod="DeleteFormTravelReimburseDetailByID"
        SelectMethod="GetFormTravelReimburseDetail" UpdateMethod="UpdateFormTravelReimburseDetail"
        TypeName="BusinessObjects.FormTEBLL" InsertMethod="AddFormTravelReimburseDetail"
        OnInserting="odsTravelReimburseDetails_Inserting" OnObjectCreated="odsTravelReimburseDetails_ObjectCreated">
        <DeleteParameters>
            <asp:Parameter Name="FormTravelReimburseDetailID" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="OccurDate" Type="Datetime" />
            <asp:Parameter Name="CityID" Type="Int32" />
            <asp:Parameter Name="Destination" Type="String" />
            <asp:Parameter Name="ManageExpenseItemID" Type="Int32" />
            <asp:Parameter Name="CurrencyID" Type="Int32" />
            <asp:Parameter Name="ExchangeRate" Type="Decimal" />
            <asp:Parameter Name="UnitPrice" Type="Decimal" />
            <asp:Parameter Name="Frequency" Type="Int32" />
            <asp:Parameter Name="PayMan" Type="String" />
            <asp:Parameter Name="Remark" Type="String" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="OccurDate" Type="Datetime" />
            <asp:Parameter Name="CityID" Type="Int32" />
            <asp:Parameter Name="Destination" Type="String" />
            <asp:Parameter Name="ManageExpenseItemID" Type="Int32" />
            <asp:Parameter Name="CurrencyID" Type="Int32" />
            <asp:Parameter Name="ExchangeRate" Type="Decimal" />
            <asp:Parameter Name="UnitPrice" Type="Decimal" />
            <asp:Parameter Name="Frequency" Type="Int32" />
            <asp:Parameter Name="PayMan" Type="String" />
            <asp:Parameter Name="Remark" Type="String" />
            <asp:Parameter Name="FormTravelReimburseDetailID" Type="Int32" />
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
    <uc4:ucUpdateProgress ID="upUP" runat="server" vAssociatedUpdatePanelID="upCustomer" />
</asp:Content>
