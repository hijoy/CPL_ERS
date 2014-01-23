<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ActivityAdvancedPaymentApply.aspx.cs" Inherits="FormSale_ActivityAdvancedPaymentApply"
    Culture="auto" UICulture="auto" %>

<%@ Register Src="../UserControls/UCDateInput.ascx" TagName="UCDateInput" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/MultiAttachmentFile.ascx" TagName="UCFlie" TagPrefix="uc2" %>
<%@ Register Src="../UserControls/ucUpdateProgress.ascx" TagName="ucUpdateProgress"
    TagPrefix="uc3" %>
<%@ Register Src="../UserControls/VendorControl.ascx" TagName="UCVendor" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <script src="../Script/DateInput.js" type="text/javascript"></script>
    <script type="text/javascript">

        function MinusExpenseTotal(obj, index, name) {
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
                TotalAmountByName = document.all("ctl00_ContentPlaceHolder1_gvPaymentDetails_ctl" + GetTBitNum(j) + "_" + name);
                if (TotalAmountByName) {
                    TotalAmountByName.innerText = commafy((parseFloat(uncommafy(TotalAmountByName.innerText)) - parseFloat(obj.value)).toFixed(2));
                    break;
                }
            }
            if (totalFee == "") {
                totalFee = 0;
            }
        }

        function PlusExpenseTotal(obj, index, name) {
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
        <asp:Label runat="server" meta:resourcekey="LabelResource1" />
    </div>
    <div class="searchDiv">
        <table class="searchTable">
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label_FormNo" runat="server" Text="<%$ Resources:Common,Form_FormNo %>" /></div>
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
                        <asp:Label ID="Form_Period" runat="server" Text="<%$ Resources:Common,Form_Period %>" /></div>
                    <div>
                        <asp:TextBox ID="PeriodCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="True"
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
                        <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Common,Form_CustomerChannel %>" /></div>
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
                        <asp:Label ID="Label7" runat="server" meta:resourcekey="Label7Resource1" />
                    </div>
                    <div>
                        <asp:TextBox ID="ShopNameCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="True"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label8" runat="server" meta:resourcekey="Label8Resource1" />
                    </div>
                    <div>
                        <asp:TextBox ID="ShopCountCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="True"
                            Width="170px"></asp:TextBox></div>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="width: 400px">
                    <div class="field_title">
                        <asp:Label ID="Label9" runat="server" Text="<%$ Resources:Common,Form_ProjectName %>" /></div>
                    <div>
                        <asp:TextBox ID="ProjectNameCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="True"
                            Width="370px"></asp:TextBox></div>
                </td>
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
                        <asp:Label ID="Form_PaymentType" runat="server" Text="<%$Resources:Common,Form_PaymentType %>" />
                    </div>
                    <div>
                        <asp:TextBox ID="PaymentTypeCtl" runat="server" CssClass="InputTextReadOnly" Text="电汇"
                            ReadOnly="True" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_VendorName" Text="<%$Resources:Common,Form_VendorName %>" runat="server" /></div>
                    <div>
                        <uc4:UCVendor ID="UCVendor" runat="server" Width="150px" IsNoClear="true" IsLimited="true" />
                    </div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_InvoiceStatus" runat="server" Text="<%$Resources:Common,Form_InvoiceStatus %>" />
                    </div>
                    <div>
                        <asp:DropDownList ID="InvoiceStatusDDL" runat="server" DataSourceID="odsInvoiceStatus"
                            DataTextField="Name" DataValueField="InvoiceStatusID" Width="170px">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="odsInvoiceStatus" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                            SelectCommand=" SELECT [InvoiceStatusID], [Name] FROM [InvoiceStatus] where InvoiceStatusID = 1">
                        </asp:SqlDataSource>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label4" runat="server" Text="<%$Resources:Common,Form_VATRate %>" /><span
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
                        <asp:Label ID="Label13" runat="server" meta:resourcekey="Label13Resource1" />
                    </div>
                    <div>
                        <asp:TextBox ID="ProjectDescCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="True"
                            TextMode="MultiLine" Height="60px" Columns="75"></asp:TextBox>
                    </div>
                </td>
                <td colspan="3" valign="top">
                    <div class="field_title">
                        <asp:Label ID="Label14" runat="server" meta:resourcekey="Label14Resource1" />
                    </div>
                    <uc2:UCFlie ID="UCFileUpload" runat="server" Width="400px" IsView="true" />
                </td>
            </tr>
            <tr>
                <td colspan="3" valign="top">
                    <div class="field_title">
                        <asp:Label ID="Label15" runat="server" Text="<%$ Resources:Common,Form_Remark %>" />
                    </div>
                    <div>
                        <asp:TextBox ID="RemarkCtl" runat="server" CssClass="InputText" TextMode="MultiLine"
                            Height="60px" Columns="75"></asp:TextBox>
                    </div>
                </td>
                <td colspan="3" valign="top">
                    <div class="field_title">
                        <asp:Label ID="Label16" runat="server" meta:resourcekey="Label16Resource1" />
                    </div>
                    <uc2:UCFlie ID="UCPaymentFile" runat="server" Width="400px" />
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div class="title">
        <asp:Label ID="Label17" runat="server" meta:resourcekey="Label17Resource1" />
    </div>
    <div class="searchDiv">
        <table class="searchTable">
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label18" runat="server" meta:resourcekey="Label18Resource1" />
                    </div>
                    <div>
                        <asp:TextBox ID="DisplayTypeCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="True"
                            Width="170px"></asp:TextBox>
                    </div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label19" runat="server" meta:resourcekey="Label19Resource1" />
                    </div>
                    <div>
                        <asp:TextBox ID="DisplayAreaCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="True"
                            Width="170px"></asp:TextBox>
                    </div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label20" runat="server" meta:resourcekey="Label20Resource1" />
                    </div>
                    <div>
                        <asp:TextBox ID="IsDMCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="True"
                            Width="170px"></asp:TextBox>
                    </div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label21" runat="server" meta:resourcekey="Label21Resource1" />
                    </div>
                    <div>
                        <asp:TextBox ID="DiscountTypeCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="True"
                            Width="170px"></asp:TextBox>
                    </div>
                </td>
                <td colspan="2" style="width: 400px">
                    <div class="field_title">
                        <asp:Label ID="Label22" runat="server" meta:resourcekey="Label22Resource1" />
                    </div>
                    <div>
                        <asp:TextBox ID="ActivityBeginCtl" runat="server" ReadOnly="True" Width="170px"></asp:TextBox>
                        <asp:Label ID="Label1" runat="server">~~</asp:Label>
                        <asp:TextBox ID="ActivityEndCtl" runat="server" ReadOnly="True" Width="170px"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="width: 400px">
                    <div class="field_title">
                        <asp:Label ID="Label23" runat="server" meta:resourcekey="Label23Resource1" />
                    </div>
                    <div>
                        <asp:TextBox ID="DeliveryBeginCtl" runat="server" ReadOnly="True" Width="170px"></asp:TextBox>
                        <asp:Label ID="Label2" runat="server">~~</asp:Label>
                        <asp:TextBox ID="DeliveryEndCtl" runat="server" ReadOnly="True" Width="170px"></asp:TextBox>
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
    <div class="title">
        <asp:Label ID="Label24" runat="server" Text="<%$ Resources:Common,Label_ProductTitle %>" />
    </div>
    <asp:UpdatePanel ID="upSKUDetails" runat="server">
        <ContentTemplate>
            <gc:GridView ID="gvSKUDetails" runat="server" ShowFooter="True" CssClass="GridView"
                AutoGenerateColumns="False" DataKeyNames="FormSaleSKUDetailID" DataSourceID="odsSKUDetails"
                OnRowDataBound="gvSKUDetails_RowDataBound" CellPadding="0">
                <Columns>
                    <asp:TemplateField HeaderText="<%$Resources:Common,Form_ProductName %>">
                        <ItemTemplate>
                            <asp:Label ID="lblProductName" runat="server" Text='<%# GetProductNameByID(Eval("SKUID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="280px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateFieldResource2">
                        <ItemTemplate>
                            <asp:Label ID="lblDiscount" runat="server" Text='<%# Eval("Discount","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="60px" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateFieldResource3">
                        <ItemTemplate>
                            <asp:Label ID="lblDiscountCampbell" runat="server" Text='<%# Eval("DiscountCampbell","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="80px" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateFieldResource4">
                        <ItemTemplate>
                            <asp:Label ID="lblForecastSaleQuantity" runat="server" Text='<%# Eval("ForecastSaleQuantity","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateFieldResource5">
                        <ItemTemplate>
                            <asp:Label ID="lblForecastOrderQuantity" runat="server" Text='<%# Eval("ForecastOrderQuantity","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Right" />
                        <FooterTemplate>
                            <asp:Label runat="server" ID="lblSum" Text="<%$Resources:Common,Form_Total %>"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle CssClass="RedTextAlignCenter" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateFieldResource6">
                        <ItemTemplate>
                            <asp:Label ID="lblForecastOrderAmount" runat="server" Text='<%# Eval("ForecastOrderAmount","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Right" />
                        <FooterTemplate>
                            <asp:Label runat="server" ID="lblOrderAmountTotal" meta:resourcekey="lblOrderAmountTotalResource1"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle CssClass="RedTextAlignCenter" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateFieldResource7">
                        <ItemTemplate>
                            <asp:Label ID="lblPriceDiscountAmount" runat="server" Text='<%# Eval("PriceDiscountAmount","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="120px" HorizontalAlign="Right" />
                        <FooterTemplate>
                            <asp:Label runat="server" ID="lblPriceDiscountTotal" meta:resourcekey="lblPriceDiscountTotalResource1"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle CssClass="RedTextAlignCenter" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateFieldResource8">
                        <ItemTemplate>
                            <asp:Label ID="lblPriceDiscountAmountRMB" runat="server" Text='<%# Eval("PriceDiscountAmount","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="120px" HorizontalAlign="Right" />
                        <FooterTemplate>
                            <asp:Label runat="server" ID="lblPriceDiscountRMBTotal" meta:resourcekey="lblPriceDiscountRMBTotalResource1"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle CssClass="RedTextAlignCenter" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:Common,Form_Remark %>">
                        <ItemTemplate>
                            <asp:Label ID="lblRemark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="280px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <FooterStyle Width="50px" Wrap="True" />
            </gc:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="odsSKUDetails" runat="server" SelectMethod="GetFormSaleSKUDetailByFormSaleApplyID"
        TypeName="BusinessObjects.FormSaleBLL">
        <SelectParameters>
            <asp:Parameter Name="FormSaleApplyID" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <br />
    <div class="title">
        <asp:Label ID="Label25" runat="server" meta:resourcekey="Label25Resource1" />
    </div>
    <asp:UpdatePanel ID="upPaymentDetails" runat="server">
        <ContentTemplate>
            <gc:GridView ID="gvPaymentDetails" runat="server" ShowFooter="True" CssClass="GridView"
                AutoGenerateColumns="False" DataKeyNames="FormSalePaymentDetailID" DataSourceID="odsPaymentDetails"
                OnRowDataBound="gvPaymentDetails_RowDataBound" CellPadding="0">
                <Columns>
                    <asp:TemplateField HeaderText="<%$Resources:Common,Form_ExpenseItem %>">
                        <ItemTemplate>
                            <asp:Label ID="lblProductName" runat="server" Text='<%# GetExpenseItemNameByID(Eval("ExpenseItemID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="150px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateFieldResource11">
                        <ItemTemplate>
                            <asp:Label ID="lblShopName" runat="server" Text='<%# GetDescriptionByID(Eval("FormSaleExpenseDetailID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="300px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateFieldResource12">
                        <ItemTemplate>
                            <asp:Label ID="lblApplyAmount" runat="server" Text='<%# Eval("ApplyAmount","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="70px" HorizontalAlign="Right" />
                        <FooterTemplate>
                            <asp:Label runat="server" ID="lblApplyAmountTotal" meta:resourcekey="lblApplyAmountTotalResource1"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle CssClass="RedTextAlignCenter" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateFieldResource13">
                        <ItemTemplate>
                            <asp:Label ID="lblApplyAmountRMB" runat="server" Text='<%# Eval("ApplyAmountRMB","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="70px" HorizontalAlign="Right" />
                        <FooterTemplate>
                            <asp:Label runat="server" ID="lblApplyAmountRMBTotal" meta:resourcekey="lblApplyAmountRMBTotalResource1"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle CssClass="RedTextAlignCenter" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateFieldResource14">
                        <ItemTemplate>
                            <asp:Label ID="lblPayedAmount" runat="server" Text='<%# Eval("PayedAmount","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="70px" HorizontalAlign="Right" />
                        <FooterTemplate>
                            <asp:Label runat="server" ID="lblPayedAmountTotal" meta:resourcekey="lblPayedAmountTotalResource1"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle CssClass="RedTextAlignCenter" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="Label_AmountBeforeTax">
                        <ItemTemplate>
                            <asp:TextBox ID="txtAmountBeforeTax" runat="server" Text='<%# Eval("AmountBeforeTax") %>'
                                Width="60px"></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle Width="70px" HorizontalAlign="Right" />
                        <FooterTemplate>
                            <asp:Label runat="server" ID="lblAmountBeforeTaxTotal"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle CssClass="RedTextAlignCenter" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="Label_TaxAmount">
                        <ItemTemplate>
                            <asp:TextBox ID="txtTaxAmount" runat="server" Text='<%# Eval("TaxAmount") %>' Width="60px"></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle Width="70px" HorizontalAlign="Right" />
                        <FooterTemplate>
                            <asp:Label runat="server" ID="lblTaxAmountTotal"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle CssClass="RedTextAlignCenter" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateFieldResource15">
                        <ItemTemplate>
                            <asp:Label ID="lblAmountRMB" runat="server" Text='<%# Eval("AmountRMB","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="80px" HorizontalAlign="Right" />
                        <FooterTemplate>
                            <asp:Label runat="server" ID="lblAmountRMBTotal"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle CssClass="RedTextAlignCenter" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:Common,Form_Remark %>">
                        <ItemTemplate>
                            <asp:Label ID="lblRemark" runat="server" Text='<%# GetApplyRemark(Eval("FormSaleExpenseDetailID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="347px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <FooterStyle Width="50px" Wrap="True" />
            </gc:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="odsPaymentDetails" runat="server" SelectMethod="GetFormSalePaymentDetail"
        TypeName="BusinessObjects.FormSaleBLL" OnObjectCreated="odsPaymentDetails_OnObjectCreated">
    </asp:ObjectDataSource>
    <br />
    <div class="title">
        <asp:Label ID="Label26" runat="server" Text="<%$ Resources:Common,Label_InvoiceTitle %>" />
    </div>
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
                            <asp:Label runat="server" ID="lblInvoiceFeeTotal" meta:resourcekey="lblInvoiceFeeTotalResource1"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle CssClass="RedTextAlignCenter" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:Common,Form_Remark %>">
                        <ItemTemplate>
                            <asp:Label ID="lblRemark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="340px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:Common,Form_SystemInfo %>">
                        <ItemTemplate>
                            <asp:Label ID="lblSystemInfo" runat="server" Text='<%# Eval("SystemInfo") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="300px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False" meta:resourcekey="TemplateFieldResource22">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Delete"
                                Text="<%$ Resources:Common,Button_Delete %>"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="94px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td style="width: 200px;" class="Empty1">
                                <asp:Label ID="Label26" runat="server" Text="<%$Resources:Common,Form_InvoiceDate %>" />
                            </td>
                            <td style="width: 150px;">
                                <asp:Label ID="Label27" runat="server" Text="<%$Resources:Common,Form_InvoiceNo %>" />
                            </td>
                            <td style="width: 150px;">
                                <asp:Label ID="Label28" runat="server" Text="<%$Resources:Common,Form_InvoiceAmount %>" />
                            </td>
                            <td style="width: 340px;">
                                <asp:Label ID="Label29" runat="server" Text="<%$ Resources:Common,Form_Remark %>" />
                            </td>
                            <td style="width: 300px;">
                                <asp:Label ID="Label30" runat="server" Text="<%$ Resources:Common,Form_SystemInfo %>" />
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
                                <asp:LinkButton ID="InsertButton" runat="server" CommandName="Insert" Text="<%$ Resources:Common,Button_Add %>"
                                    ValidationGroup="NewInvoiceRow"></asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="UCInvoiceDate$txtDate"
                                Display="None" ValidationGroup="NewInvoiceRow" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RF1" runat="server" ControlToValidate="txtInvoiceNo"
                                Display="None" ValidationGroup="NewInvoiceRow" meta:resourcekey="RF1Resource1"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RF2" runat="server" ControlToValidate="txtAmount"
                                Display="None" ValidationGroup="NewInvoiceRow" meta:resourcekey="RF2Resource1"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtAmount"
                                ValidationExpression="<%$ Resources:RegularExpressions, Money %>" Display="None"
                                ValidationGroup="NewInvoiceRow" meta:resourcekey="RegularExpressionValidator1Resource1"></asp:RegularExpressionValidator>
                            <asp:ValidationSummary ID="ValidationSummaryINS" runat="server" ShowMessageBox="True"
                                ShowSummary="False" ValidationGroup="NewInvoiceRow" meta:resourcekey="ValidationSummaryINSResource1" />
                        </tr>
                    </table>
                    <br />
                </InsertItemTemplate>
            </asp:FormView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="odsInvoice" runat="server" DeleteMethod="DeleteFormInvoiceByID"
        SelectMethod="GetFormInvoice" TypeName="BusinessObjects.FormSaleBLL" InsertMethod="AddFormInvoice"
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
                    Text="<%$ Resources:Common,Button_Submit %>" />
                <asp:Button ID="SaveBtn" runat="server" CssClass="button_nor" OnClick="SaveBtn_Click"
                    Text="<%$ Resources:Common,Button_Save %>" />
                <asp:Button ID="CancelBtn" runat="server" CssClass="button_nor" OnClick="CancelBtn_Click"
                    Text="<%$ Resources:Common,Button_Back %>" />
                <asp:Button ID="DeleteBtn" runat="server" CssClass="button_nor" OnClick="DeleteBtn_Click"
                    Text="<%$ Resources:Common,Button_Delete %>" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <uc3:ucUpdateProgress ID="ucUP" runat="server" vassociatedupdatepanelid="upButton" />
    <br />
</asp:Content>
