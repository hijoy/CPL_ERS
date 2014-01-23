<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="MarketingPaymentApply.aspx.cs" Culture="Auto" UICulture="Auto" Inherits="FormMarketing_MarketingPaymentApply" %>

<%@ Register Src="../UserControls/UCDateInput.ascx" TagName="UCDateInput" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/MultiAttachmentFile.ascx" TagName="UCFlie" TagPrefix="uc2" %>
<%@ Register Src="../UserControls/ucUpdateProgress.ascx" TagName="ucUpdateProgress" TagPrefix="uc3" %>
<%@ Register Src="../UserControls/POSearchControl.ascx" TagName="UCPO" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <script src="../Script/DateInput.js" type="text/javascript"></script>
    <script type="text/javascript">

        function MinusExpenseTotal(obj, index,name) {
            index = index + 2;
            var totalFee;
            if (obj.value != "" && isNaN(obj.value)) {
                alert("请录入数字");
                window.setTimeout(function () { document.getElementById(obj.id).focus(); }, 0);
                return false;
            }
            if (obj.value == "") {
                obj.value = 0;
            }
            for (j = 3; j <= 100; j++) {
                if (document.all("ctl00_ContentPlaceHolder1_gvPaymentDetails_ctl" + GetTBitNum(j) + "_lblAmountRMBTotal")) {
                    totalFee = uncommafy(document.all("ctl00_ContentPlaceHolder1_gvPaymentDetails_ctl" + GetTBitNum(j) + "_lblAmountRMBTotal").innerText);
                    break;
                }
            }
            //计算数据
            var lastTotal = 0;
            if (obj.value != "" && !isNaN(obj.value)) {
                lastTotal = parseFloat(totalFee) - parseFloat(obj.value);
                if (document.all("ctl00_ContentPlaceHolder1_gvPaymentDetails_ctl" + GetTBitNum(j) + "_lblAmountRMBTotal")) {
                    document.all("ctl00_ContentPlaceHolder1_gvPaymentDetails_ctl" + GetTBitNum(j) + "_lblAmountRMBTotal").innerText = commafy(lastTotal.toFixed(2));
                }
            }
            //总金额AmoutRMB
            var lastAmountRMB = document.all("ctl00_ContentPlaceHolder1_gvPaymentDetails_ctl" + GetTBitNum(index) + "_lblAmountRMB");
            if (lastAmountRMB) {
                lastAmountRMB.innerText = commafy((parseFloat(uncommafy(lastAmountRMB.innerText)) - parseFloat(obj.value)).toFixed(2));
            }
            var TotalAmountByName;
            for (j = 3; j <= 100; j++) {
                TotalAmountByName = document.all("ctl00_ContentPlaceHolder1_gvPaymentDetails_ctl" + GetTBitNum(j) + "_"+name);
                if (TotalAmountByName) {
                    TotalAmountByName.innerText = commafy((parseFloat(uncommafy(TotalAmountByName.innerText)) - parseFloat(obj.value)).toFixed(2));
                    break;
                }
            }
            if (totalFee == "") {
                totalFee = 0;
            }
        }

        function PlusExpenseTotal(obj, index,name) {
            index = index + 2;
            var totalFee;
            if (obj.value != "" && isNaN(obj.value)) {
                alert("请录入数字");
                window.setTimeout(function () { document.getElementById(obj.id).focus(); }, 0);
                return false;
            }
            if (obj.value == "") {
                obj.value = 0;
            }
            for (j = 3; j <= 100; j++) {
                if (document.all("ctl00_ContentPlaceHolder1_gvPaymentDetails_ctl" + GetTBitNum(j) + "_lblAmountRMBTotal")) {
                    totalFee = uncommafy(document.all("ctl00_ContentPlaceHolder1_gvPaymentDetails_ctl" + GetTBitNum(j) + "_lblAmountRMBTotal").innerText);
                    break;
                }
            }
            //计算数据
            var lastTotal = 0;
            if (obj.value != "" && !isNaN(obj.value)) {
                lastTotal = parseFloat(totalFee) + parseFloat(obj.value);
                document.all("ctl00_ContentPlaceHolder1_gvPaymentDetails_ctl" + GetTBitNum(j) + "_lblAmountRMBTotal").innerText = commafy(lastTotal.toFixed(2));
            } else {
                lastTotal = parseFloat(totalFee);
                document.all("ctl00_ContentPlaceHolder1_gvPaymentDetails_ctl" + GetTBitNum(j) + "_lblAmountRMBTotal").innerText = commafy(lastTotal.toFixed(2));
            }
            //总金额AmoutRMB
            var lastAmountRMB = document.all("ctl00_ContentPlaceHolder1_gvPaymentDetails_ctl" + GetTBitNum(index) + "_lblAmountRMB");
            if (lastAmountRMB) {
                lastAmountRMB.innerText = commafy((parseFloat(uncommafy(lastAmountRMB.innerText)) + parseFloat(obj.value)).toFixed(2));
            }
            var TotalAmountByName;
            for (j = 3; j <= 100; j++) {
                TotalAmountByName = document.all("ctl00_ContentPlaceHolder1_gvPaymentDetails_ctl" + GetTBitNum(j) + "_" + name);
                if (TotalAmountByName) {
                    TotalAmountByName.innerText = commafy((parseFloat(uncommafy(TotalAmountByName.innerText)) + parseFloat(obj.value)).toFixed(2));
                    break;
                }
            }
            if (totalFee == "") {
                totalFee = 0;
            }
            
        }

        function commafy(num) {
            num = num + "";
            var re = /(-?\d+)(\d{3})/;
            while (re.test(num)) {
                num = num.replace(re, "$1,$2");
            }
            return num;
        }
        function uncommafy(str) {
            var re = /\,/g;
            str = str.replace(re, '');
            return str
        }
        function GetTBitNum(num) {
            if (num < 10) {
                num = "0" + String(num);
            }
            return num;
        }


    </script>
    <div class="title">
        <asp:Label runat="server" meta:resourcekey="Label_Title" />
    </div>
    <div class="searchDiv">
        <table class="searchTable">
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_FormNo" runat="server" Text="<%$Resources:Common,Form_FormNo %>" /></div>
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
                        <asp:Label ID="Label_ExpenseCategory" runat="server" Text="<%$ Resources:Common,Form_ExpenseCategory %>" />
                    </div>
                    <div>
                        <asp:TextBox ID="ExpenseCategoryCtl" runat="server" CssClass="InputTextReadOnly"
                            ReadOnly="True" Width="170px"></asp:TextBox></div>
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_ProjectName" runat="server" Text="<%$Resources:Common,Form_ProjectName %>" /></div>
                    <div>
                        <asp:TextBox ID="ProjectNameCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td colspan="2" style="width: 400px">
                    <div class="field_title">
                        <asp:Label runat="server" meta:resourcekey="Label_ActivityBegin" />
                    </div>
                    <div>
                        <asp:TextBox ID="ActivityBeginCtl" runat="server" ReadOnly="true" Width="120px"></asp:TextBox>
                        <asp:Label ID="Label1" runat="server">~~</asp:Label>
                        <asp:TextBox ID="ActivityEndCtl" runat="server" ReadOnly="true" Width="120px"></asp:TextBox>
                    </div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_CostCenter" runat="server" Text="<%$Resources:Common,Form_CostCenter %>" /></div>
                    <div>
                        <asp:TextBox ID="CostCenterCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="True"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_PaymentType" runat="server" Text="<%$Resources:Common,Form_PaymentType %>" /></div>
                    <div>
                        <asp:DropDownList ID="PaymentTypeDDL" runat="server" AutoPostBack="true" Width="180px" DataSourceID="sdsPaymentType" DataTextField="PaymentTypeName" DataValueField="PaymentTypeID"
                            OnSelectedIndexChanged="PaymentTypeDDL_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="sdsPaymentType" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                            SelectCommand="SELECT PaymentTypeID, PaymentTypeName FROM PaymentType where PaymentTypeID in (1,6) ">
                        </asp:SqlDataSource>
                    </div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_InvoiceStatus" runat="server" Text="<%$Resources:Common,Form_InvoiceStatus %>" /></div>
                    <div>
                        <asp:DropDownList ID="InvoiceStatusDDL" runat="server" DataSourceID="odsInvoiceStatus"
                            DataTextField="Name" DataValueField="InvoiceStatusID" Width="170px">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="odsInvoiceStatus" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                            SelectCommand=" SELECT [InvoiceStatusID], [Name] FROM [InvoiceStatus] "></asp:SqlDataSource>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label3" runat="server" Text="<%$Resources:Common,Form_VATRate %>" /><span
                            class="requiredLable">*</span></div>
                    <div>
                        <asp:DropDownList ID="VATTypeDDL" runat="server" DataSourceID="odsVATType" DataTextField="VATTypeName"
                            DataValueField="VATTypeID" Width="180px">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="odsVATType" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                            SelectCommand="select 0 VATTypeID,' Please select' VATTypeName Union  SELECT VATTypeID, VATTypeName+'-'+[Description] VATTypeName FROM VATType where IsActive=1 ">
                        </asp:SqlDataSource>
                    </div>
                </td>
                <td style="width: 200px">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="3" valign="top">
                    <div class="field_title">
                        <asp:Label runat="server" meta:resourcekey="Label_ProjectDesc" />
                    </div>
                    <div>
                        <asp:TextBox ID="ProjectDescCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            TextMode="multiline" Rows="5" Columns="78"></asp:TextBox>
                    </div>
                </td>
                <td colspan="3" valign="top">
                    <div class="field_title">
                        <asp:Label runat="server" meta:resourcekey="Label_FileApply" />
                    </div>
                    <uc2:UCFlie ID="UCFileApply" runat="server" Width="400px" IsView="true" />
                </td>
            </tr>
            <tr>
                <td colspan="3" valign="top">
                    <div class="field_title">
                        <asp:Label ID="Form_Remark" runat="server" Text="<%$Resources:Common,Form_Remark %>" /></div>
                    <div>
                        <asp:TextBox ID="RemarkCtl" runat="server" CssClass="InputText" TextMode="multiline"
                            Rows="5" Columns="78"></asp:TextBox>
                    </div>
                </td>
                <td colspan="3" valign="top">
                    <div class="field_title">
                        <asp:Label runat="server" meta:resourcekey="Label_FliePayment" />
                    </div>
                    <uc2:UCFlie ID="UCFliePayment" runat="server" Width="400px" />
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div class="title">
        <asp:Label runat="server" meta:resourcekey="Label_Title1" />
    </div>
    <asp:UpdatePanel ID="upPaymentDetails" runat="server">
        <ContentTemplate>
            <gc:GridView ID="gvPaymentDetails" runat="server" ShowFooter="True" CssClass="GridView"
                AutoGenerateColumns="False" DataKeyNames="FormMarketingPaymentDetailID" DataSourceID="odsPaymentDetails"
                OnRowDataBound="gvPaymentDetails_RowDataBound" CellPadding="0">
                <Columns>
                <asp:TemplateField HeaderText="<%$Resources:Common,Form_VendorName %>">
                        <ItemTemplate>
                            <asp:Label ID="lblVendor" runat="server" Text='<%# GetVendorNameByID(Eval("VendorID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="250px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Common,Form_ExpenseItem %>">
                        <ItemTemplate>
                            <asp:Label ID="lblProductName" runat="server" Text='<%# GetExpenseItemNameByID(Eval("ExpenseItemID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="150px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Common,Form_ProductName %>">
                        <ItemTemplate>
                            <asp:Label ID="lblSKUName" runat="server" Text='<%# GetProductNameByID(Eval("SKUID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="300px" HorizontalAlign="Center" />
                        <FooterTemplate>
                            <asp:Label runat="server" ID="lblSum" Text="<%$Resources:Common,Form_Total %>"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle CssClass="RedTextAlignCenter" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="Label_ApplyAmount">
                        <ItemTemplate>
                            <asp:Label ID="lblApplyAmount" runat="server" Text='<%# Eval("ApplyAmount","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Right" />
                        <FooterTemplate>
                            <asp:Label runat="server" ID="lblApplyAmountTotal"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle CssClass="RedTextAlignCenter" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="Label_ApplyAmountRMB">
                        <ItemTemplate>
                            <asp:Label ID="lblApplyAmountRMB" runat="server" Text='<%# Eval("ApplyAmountRMB","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Right" />
                        <FooterTemplate>
                            <asp:Label runat="server" ID="lblApplyAmountRMBTotal"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle CssClass="RedTextAlignCenter" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="Label_PayedAmount">
                        <ItemTemplate>
                            <asp:Label ID="lblPayedAmount" runat="server" Text='<%# Eval("PaiedAmount","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Right" />
                        <FooterTemplate>
                            <asp:Label runat="server" ID="lblPayedAmountTotal"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle CssClass="RedTextAlignCenter" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="Label_AmountBeforeTax">
                        <ItemTemplate>
                            <asp:TextBox ID="txtAmountBeforeTax" runat="server" Text='<%# Eval("AmountBeforeTax") %>'
                                Width="90px"></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle Width="110px" HorizontalAlign="Right" />
                        <FooterTemplate>
                            <asp:Label runat="server" ID="lblAmountBeforeTaxTotal"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle CssClass="RedTextAlignCenter" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="Label_TaxAmount">
                        <ItemTemplate>
                            <asp:TextBox ID="txtTaxAmount" runat="server" Text='<%# Eval("TaxAmount") %>' Width="90px"></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle Width="110px" HorizontalAlign="Right" />
                        <FooterTemplate>
                            <asp:Label runat="server" ID="lblTaxAmountTotal"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle CssClass="RedTextAlignCenter" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="Label_AmountRMB">
                        <ItemTemplate>
                            <asp:Label ID="lblAmountRMB" runat="server" Text='<%# Eval("AmountRMB","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="130px" HorizontalAlign="Right" />
                        <FooterTemplate>
                            <asp:Label runat="server" ID="lblAmountRMBTotal"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle CssClass="RedTextAlignCenter" HorizontalAlign="Right" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="PO No">
                        <ItemTemplate>
                            <uc4:UCPO ID="UCPO" runat="server" Width="130px" FormID='<%# Eval("POFormID") %>'/>
                        </ItemTemplate>
                        <ItemStyle Width="270px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <FooterStyle Width="50px" Wrap="True" />
            </gc:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="odsPaymentDetails" runat="server" SelectMethod="GetFormMarketingPaymentDetail"
        TypeName="BusinessObjects.FormMarketingBLL" OnObjectCreated="odsPaymentDetails_OnObjectCreated">
    </asp:ObjectDataSource>
    <br />
    <div class="title">
        <asp:Label ID="Label2" runat="server" Text="<%$Resources:Common,Label_InvoiceTitle %>" /></div>
    <asp:UpdatePanel ID="upInvoice" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <gc:GridView ID="gvInvoice" runat="server" ShowFooter="True" CssClass="GridView"
                AutoGenerateColumns="False" DataKeyNames="FormInvoiceID" DataSourceID="odsInvoice"
                OnRowDataBound="gvInvoice_RowDataBound" CellPadding="0">
                <Columns>
                    <asp:TemplateField HeaderText="<%$Resources:Common,Form_InvoiceDate %>">
                        <ItemTemplate>
                            <asp:Label ID="lblInvoiceDate" runat="server" Text='<%# Eval("InvoiceDate","{0:yyyy-MM-dd}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="200px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Common,Form_InvoiceNo %>">
                        <ItemTemplate>
                            <asp:Label ID="lblInvoiceNo" runat="server" Text='<%# Eval("InvoiceNo") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="150px" HorizontalAlign="Center" />
                        <FooterTemplate>
                            <asp:Label runat="server" ID="lblsum" Text="<%$Resources:Common,Form_Total %>"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle CssClass="RedTextAlignCenter" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Common,Form_InvoiceAmount %>">
                        <ItemTemplate>
                            <asp:Label ID="lblInvoiceAmount" runat="server" Text='<%# Eval("InvoiceAmount","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="150px" HorizontalAlign="Right" />
                        <FooterTemplate>
                            <asp:Label runat="server" ID="lblInvoiceFeeTotal"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle CssClass="RedTextAlignCenter" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Common,Form_Remark %>">
                        <ItemTemplate>
                            <asp:Label ID="lblRemark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="340px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Common,Form_SystemInfo %>">
                        <ItemTemplate>
                            <asp:Label ID="lblSystemInfo" runat="server" Text='<%# Eval("SystemInfo") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="300px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Delete"
                                Text="<%$Resources:Common,Button_Delete %>"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="94px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td style="width: 200px;" class="Empty1">
                                <asp:Label runat="server" Text="<%$Resources:Common,Form_InvoiceDate %>" />
                            </td>
                            <td style="width: 150px;">
                                <asp:Label runat="server" Text="<%$Resources:Common,Form_InvoiceNo %>" />
                            </td>
                            <td style="width: 150px;">
                                <asp:Label runat="server" Text="<%$Resources:Common,Form_InvoiceAmount %>" />
                            </td>
                            <td style="width: 340px;">
                                <asp:Label runat="server" Text="<%$Resources:Common,Form_Remark %>" />
                            </td>
                            <td style="width: 300px;">
                                <asp:Label runat="server" Text="<%$Resources:Common,Form_SystemInfo %>" />
                            </td>
                            <td style="width: 94px">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <HeaderStyle CssClass="Header" />
            </gc:GridView>
            <asp:FormView ID="fvInvoice" runat="server" DataKeyNames="FormInvoiceID" DataSourceID="odsInvoice"
                DefaultMode="Insert" CellPadding="0">
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr>
                            <td style="width: 200px;" align="center">
                                <uc1:UCDateInput ID="UCInvoiceDate" runat="server" SelectedDate='<%# Bind("InvoiceDate") %>' />
                            </td>
                            <td style="width: 150px;" align="center">
                                <asp:TextBox ID="txtInvoiceNo" runat="server" MaxLength="20" Width="120px" Text='<%# Bind("InvoiceNo") %>'></asp:TextBox>
                            </td>
                            <td style="width: 150px;" align="center">
                                <asp:TextBox ID="txtAmount" runat="server" MaxLength="15" Width="120px" Text='<%# Bind("InvoiceAmount") %>'></asp:TextBox>
                            </td>
                            <td style="width: 340px;" align="center">
                                <asp:TextBox ID="txtRemark" runat="server" MaxLength="200" Width="310px" Text='<%# Bind("Remark") %>'></asp:TextBox>
                            </td>
                            <td style="width: 300px;" align="center">
                            </td>
                            <td style="width: 94px;" align="center">
                                <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    Text="<%$Resources:Common,Button_Add %>" ValidationGroup="NewInvoiceRow"></asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="UCInvoiceDate$txtDate"
                                meta:resourcekey="RequiredFieldValidator_InvoiceDate" Display="None" ValidationGroup="NewInvoiceRow"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RF1" runat="server" ControlToValidate="txtInvoiceNo"
                                meta:resourcekey="RequiredFieldValidator_InvoiceNo" Display="None" ValidationGroup="NewInvoiceRow"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RF2" runat="server" ControlToValidate="txtAmount"
                                meta:resourcekey="RequiredFieldValidator_Amount" Display="None" ValidationGroup="NewInvoiceRow"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtAmount"
                                ValidationExpression="<%$ Resources:RegularExpressions, Money %>" Display="None"
                                meta:resourcekey="RegularExpressionValidator_Amount" ValidationGroup="NewInvoiceRow"></asp:RegularExpressionValidator>
                            <asp:ValidationSummary ID="ValidationSummaryINS" runat="server" ShowMessageBox="True"
                                ShowSummary="False" ValidationGroup="NewInvoiceRow" />
                        </tr>
                    </table>
                    <br />
                </InsertItemTemplate>
            </asp:FormView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="odsInvoice" runat="server" DeleteMethod="DeleteFormInvoiceByID"
        SelectMethod="GetFormInvoice" TypeName="BusinessObjects.FormMarketingBLL" InsertMethod="AddFormInvoice"
        OnObjectCreated="odsInvoice_ObjectCreated">
        <DeleteParameters>
            <asp:Parameter Name="FormInvoiceID" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="FormID" Type="Int32" />
            <asp:Parameter Name="InvoiceNo" Type="String" />
            <asp:Parameter Name="InvoiceDate" Type="datetime" />
            <asp:Parameter Name="InvoiceAmount" Type="Decimal" />
            <asp:Parameter Name="Remark" Type="String" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
    <uc3:ucUpdateProgress ID="ucUP" runat="server" vassociatedupdatepanelid="upButton" />
    <br />
</asp:Content>
