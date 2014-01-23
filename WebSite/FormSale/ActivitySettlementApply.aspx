<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ActivitySettlementApply.aspx.cs" Inherits="FormSale_ActivitySettlementApply"
    Culture="auto" UICulture="auto" %>

<%@ Register Src="../UserControls/MultiAttachmentFile.ascx" TagName="UCFlie" TagPrefix="uc2" %>
<%@ Register Src="../UserControls/ucUpdateProgress.ascx" TagName="ucUpdateProgress"
    TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <script src="../Script/DateInput.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">

        function MinusSKUTotal(index) {
            var thisIndex = index + 2;
            var totalFee = 0;
            for (j = 3; j <= 100; j++) {
                if (document.all("ctl00_ContentPlaceHolder1_gvSKUDetails_ctl" + GetTBitNum(j) + "_lblActualOrderAmountTotal")) {
                    totalFee = uncommafy(document.all("ctl00_ContentPlaceHolder1_gvSKUDetails_ctl" + GetTBitNum(j) + "_lblActualOrderAmountTotal").innerText);
                    break;
                }
            }
            if (totalFee == "") {
                totalFee = 0;
            }
            //取得本条实际订货金额
            var actualOrderAmount = uncommafy(document.all("ctl00_ContentPlaceHolder1_gvSKUDetails_ctl" + GetTBitNum(thisIndex) + "_txtActualOrderAmount").value);
            if (actualOrderAmount == "") {
                actualOrderAmount = 0;
            }
            //计算实际订货金额合计
            var lastTotal = 0;
            lastTotal = parseFloat(totalFee) - parseFloat(actualOrderAmount);
            if (document.all("ctl00_ContentPlaceHolder1_gvSKUDetails_ctl" + GetTBitNum(j) + "_lblActualOrderAmountTotal")) {
                document.all("ctl00_ContentPlaceHolder1_gvSKUDetails_ctl" + GetTBitNum(j) + "_lblActualOrderAmountTotal").innerText = commafy(lastTotal.toFixed(2));
            }
            //计算完成度合计 
            var orderAmountTotal = 0;
            if (document.all("ctl00_ContentPlaceHolder1_gvSKUDetails_ctl" + GetTBitNum(j) + "_lblOrderAmountTotal")) {
                orderAmountTotal = uncommafy(document.all("ctl00_ContentPlaceHolder1_gvSKUDetails_ctl" + GetTBitNum(j) + "_lblOrderAmountTotal").innerText);
            }

            var lastRate = parseFloat(lastTotal) / parseFloat(orderAmountTotal) * 100;
            if (document.all("ctl00_ContentPlaceHolder1_gvSKUDetails_ctl" + GetTBitNum(j) + "_lblActualRateTotal")) {
                document.all("ctl00_ContentPlaceHolder1_gvSKUDetails_ctl" + GetTBitNum(j) + "_lblActualRateTotal").innerText = commafy(lastRate.toFixed(2));
            }

        }

        function PlusSKUTotal(index, deliveryPrice) {
            //debugger
            var thisIndex = index + 2;
            var totalFee;
            for (j = 3; j <= 100; j++) {
                if (document.all("ctl00_ContentPlaceHolder1_gvSKUDetails_ctl" + GetTBitNum(j) + "_lblActualOrderAmountTotal")) {
                    totalFee = uncommafy(document.all("ctl00_ContentPlaceHolder1_gvSKUDetails_ctl" + GetTBitNum(j) + "_lblActualOrderAmountTotal").innerText);
                    break;
                }
            }

            var price = deliveryPrice;
            var quantity = document.all("ctl00_ContentPlaceHolder1_gvSKUDetails_ctl" + GetTBitNum(thisIndex) + "_txtActualOrderQuantity").value;

            if (quantity != "" && isNaN(quantity)) {
                alert("请录入数字");
                window.setTimeout(function () { document.all("ctl00_ContentPlaceHolder1_gvSKUDetails_ctl" + GetTBitNum(thisIndex) + "_txtActualOrderQuantity").focus(); }, 0);
                return false;
            }

            //计算本条实际订货金额
            var actualOrderAmount = 0;
            actualOrderAmount = parseFloat(quantity) * parseFloat(price);
            document.all("ctl00_ContentPlaceHolder1_gvSKUDetails_ctl" + GetTBitNum(thisIndex) + "_txtActualOrderAmount").value = commafy(actualOrderAmount.toFixed(2));

            //计算本条完成度
            var forecastOrderAmount = uncommafy(document.all("ctl00_ContentPlaceHolder1_gvSKUDetails_ctl" + GetTBitNum(thisIndex) + "_lblForecastOrderAmount").innerText);
            var actualRate = 0;
            actualRate = parseFloat(actualOrderAmount) / parseFloat(forecastOrderAmount) * 100;
            document.all("ctl00_ContentPlaceHolder1_gvSKUDetails_ctl" + GetTBitNum(thisIndex) + "_txtActualRate").value = commafy(actualRate.toFixed(2));

            //计算实际订货金额合计
            var lastTotal = 0;
            lastTotal = parseFloat(totalFee) + parseFloat(actualOrderAmount);
            if (document.all("ctl00_ContentPlaceHolder1_gvSKUDetails_ctl" + GetTBitNum(j) + "_lblActualOrderAmountTotal")) {
                document.all("ctl00_ContentPlaceHolder1_gvSKUDetails_ctl" + GetTBitNum(j) + "_lblActualOrderAmountTotal").innerText = commafy(lastTotal.toFixed(2));
            }

            //计算完成度合计 
            var orderAmountTotal = 0;
            if (document.all("ctl00_ContentPlaceHolder1_gvSKUDetails_ctl" + GetTBitNum(j) + "_lblOrderAmountTotal")) {
                orderAmountTotal = uncommafy(document.all("ctl00_ContentPlaceHolder1_gvSKUDetails_ctl" + GetTBitNum(j) + "_lblOrderAmountTotal").innerText);
            }

            var lastRate = parseFloat(lastTotal) / parseFloat(orderAmountTotal) * 100;
            if (document.all("ctl00_ContentPlaceHolder1_gvSKUDetails_ctl" + GetTBitNum(j) + "_lblActualRateTotal")) {
                document.all("ctl00_ContentPlaceHolder1_gvSKUDetails_ctl" + GetTBitNum(j) + "_lblActualRateTotal").innerText = commafy(lastRate.toFixed(2));
            }

        }

        function MinusExpenseTotal(obj) {
            var totalFee;
            for (j = 3; j <= 100; j++) {
                if (document.all("ctl00_ContentPlaceHolder1_gvExpenseDetails_ctl" + GetTBitNum(j) + "_lblSettlementAmountTotal")) {
                    totalFee = uncommafy(document.all("ctl00_ContentPlaceHolder1_gvExpenseDetails_ctl" + GetTBitNum(j) + "_lblSettlementAmountTotal").innerText);
                    break;
                }
            }
            if (totalFee == "") {
                totalFee = 0;
            }
            if (obj.value != "" && isNaN(obj.value)) {
                alert("请录入数字");
                window.setTimeout(function () { document.getElementById(obj.id).focus(); }, 0);
                return false;
            }
            //计算数据
            var lastTotal = 0;
            if (obj.value != "" && !isNaN(obj.value)) {
                lastTotal = parseFloat(totalFee) - parseFloat(obj.value);
                if (document.all("ctl00_ContentPlaceHolder1_gvExpenseDetails_ctl" + GetTBitNum(j) + "_lblSettlementAmountTotal")) {
                    document.all("ctl00_ContentPlaceHolder1_gvExpenseDetails_ctl" + GetTBitNum(j) + "_lblSettlementAmountTotal").innerText = commafy(lastTotal.toFixed(2));
                }
            }
        }

        function PlusExpenseTotal(obj) {
            var totalFee;
            for (j = 3; j <= 100; j++) {
                if (document.all("ctl00_ContentPlaceHolder1_gvExpenseDetails_ctl" + GetTBitNum(j) + "_lblSettlementAmountTotal")) {
                    totalFee = uncommafy(document.all("ctl00_ContentPlaceHolder1_gvExpenseDetails_ctl" + GetTBitNum(j) + "_lblSettlementAmountTotal").innerText);
                    break;
                }
            }
            if (totalFee == "") {
                totalFee = 0;
            }
            if (obj.value != "" && isNaN(obj.value)) {
                alert("请录入数字");
                window.setTimeout(function () { document.getElementById(obj.id).focus(); }, 0);
                return false;
            }
            //计算数据
            var lastTotal = 0;
            if (obj.value != "" && !isNaN(obj.value)) {
                lastTotal = parseFloat(totalFee) + parseFloat(obj.value);
                document.all("ctl00_ContentPlaceHolder1_gvExpenseDetails_ctl" + GetTBitNum(j) + "_lblSettlementAmountTotal").innerText = commafy(lastTotal.toFixed(2));
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
        <asp:Label runat="server" meta:resourcekey="LabelResource1" /></div>
    <div class="searchDiv">
        <table class="searchTable">
            <tr>
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
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label_Currency" runat="server" Text="<%$ Resources:Common,Form_Currency %>"
                            meta:resourcekey="Label_CurrencyResource1" /></div>
                    <div>
                        <asp:TextBox ID="CurrencyCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="True"
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
                        <asp:Label ID="Label_KATypeCtl" runat="server" Text="<%$Resources:Common,Form_KAType %>" />
                    </div>
                    <div>
                        <asp:TextBox ID="KATypeCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label_CustomerRegion" runat="server" Text="<%$ Resources:Common,Form_CustomerRegion %>" /></div>
                    <div>
                        <asp:TextBox ID="CustomerRegionCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="True"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label_City" runat="server" Text="<%$ Resources:Common,Form_City %>" /></div>
                    <div>
                        <asp:TextBox ID="CityCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="True"
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
                        <asp:Label ID="Label_ExpenseCategory" runat="server" Text="<%$Resources:Common,Form_ExpenseCategory %>" />
                    </div>
                    <div>
                        <asp:TextBox ID="ExpenseCategoryCtl" runat="server" CssClass="InputTextReadOnly"
                            ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Common,Form_ExpenseSubCategory %>" /></div>
                    <div>
                        <asp:TextBox ID="ExpenseSubCategoryCtl" runat="server" CssClass="InputTextReadOnly"
                            ReadOnly="True" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_PaymentType" runat="server" Text="<%$Resources:Common,Form_PaymentType %>" /></div>
                    <div>
                        <asp:DropDownList ID="PaymentTypeDDL" runat="server" DataTextField="PaymentTypeName"
                            DataValueField="PaymentTypeID" Width="180px" DataSourceID="odsPaymentType">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="odsPaymentType" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                            SelectCommand="select 0 PaymentTypeID,' Please Select' PaymentTypeName Union SELECT PaymentTypeID, PaymentTypeName FROM [PaymentType] order by PaymentTypeID ">
                        </asp:SqlDataSource>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="3" valign="top">
                    <div class="field_title">
                        <asp:Label ID="Label2" runat="server" meta:resourcekey="Label2Resource1" /></div>
                    <div>
                        <asp:TextBox ID="RemarkCtl" runat="server" CssClass="InputText" TextMode="MultiLine"
                            Height="60px" Columns="75"></asp:TextBox>
                    </div>
                </td>
                <td colspan="3" valign="top">
                    <div class="field_title">
                        <asp:Label ID="Form_Adjunct" runat="server" Text="<%$Resources:Common,Form_Adjunct %>" /></div>
                    <uc2:UCFlie ID="UCSettlementFile" runat="server" Width="400px" />
                </td>
            </tr>
        </table>
    </div>
    <div class="title">
        <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Common,Label_ProductTitle %>" /></div>
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
                    <asp:TemplateField HeaderText="<%$ Resources:Common,Form_ProductName %>">
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
                            <asp:TextBox ID="txtActualOrderQuantity" runat="server" Text='<%# Eval("ActualOrderQuantity") %>'
                                Width="80px"></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateFieldResource10">
                        <ItemTemplate>
                            <asp:TextBox ID="txtActualOrderAmount" runat="server" Text='<%# Eval("ActualOrderAmount","{0:N}") %>'
                                Width="80px" ReadOnly="True"></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Right" />
                        <FooterTemplate>
                            <asp:Label runat="server" ID="lblActualOrderAmountTotal" meta:resourcekey="lblActualOrderAmountTotalResource1"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle CssClass="RedTextAlignCenter" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateFieldResource11">
                        <ItemTemplate>
                            <asp:TextBox ID="txtActualRate" runat="server" Text='<%# Eval("ActualRate") %>' Width="60px"
                                ReadOnly="True"></asp:TextBox>
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
            </gc:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="odsSKUDetails" runat="server" SelectMethod="GetFormSettlementSKUDetail"
        TypeName="BusinessObjects.FormSaleBLL" OnObjectCreated="odsSKUDetails_OnObjectCreated">
    </asp:ObjectDataSource>
    <br />
    <div class="title">
        <asp:Label ID="Label5" runat="server" meta:resourcekey="Label5Resource1" /></div>
    <asp:UpdatePanel ID="upExpenseDetails" runat="server">
        <ContentTemplate>
            <gc:GridView ID="gvExpenseDetails" runat="server" ShowFooter="True" CssClass="GridView"
                AutoGenerateColumns="False" DataKeyNames="FormSettlementExpenseDetailID" DataSourceID="odsExpenseDetails"
                OnRowDataBound="gvExpenseDetails_RowDataBound" CellPadding="0" meta:resourcekey="gvExpenseDetailsResource1">
                <Columns>
                    <asp:TemplateField meta:resourcekey="TemplateFieldResource13">
                        <ItemTemplate>
                            <asp:HyperLink ID="lblApplyFormNo" runat="server" Text='<%# Eval("ApplyFormNo") %>'
                                meta:resourcekey="lblApplyFormNoResource2"></asp:HyperLink>
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
                            <asp:Label ID="lblShopName" runat="server" Text='<%# GetDescriptionByID(Eval("FormSettlementExpenseDetailID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="200px" HorizontalAlign="Center" />
                        <FooterTemplate>
                            <asp:Label runat="server" ID="lblSum" Text="<%$ Resources:Common,Form_Total %>"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle CssClass="RedTextAlignCenter" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateFieldResource17">
                        <ItemTemplate>
                            <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("ApplyAmount","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Right" />
                        <FooterTemplate>
                            <asp:Label runat="server" ID="lblApplyAmountTotal" meta:resourcekey="lblApplyAmountTotalResource1"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle CssClass="RedTextAlignCenter" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateFieldResource18">
                        <ItemTemplate>
                            <asp:Label ID="lblAmountRMB" runat="server" Text='<%# Eval("ApplyAmountRMB","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Right" />
                        <FooterTemplate>
                            <asp:Label runat="server" ID="lblApplyAmountRMBTotal" meta:resourcekey="lblApplyAmountRMBTotalResource1"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle CssClass="RedTextAlignCenter" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateFieldResource19">
                        <ItemTemplate>
                            <asp:Label ID="lblAdvancedAmount" runat="server" Text='<%# Eval("AdvancedAmount","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Right" />
                        <FooterTemplate>
                            <asp:Label runat="server" ID="lblAdvancedAmountTotal" meta:resourcekey="lblAdvancedAmountTotalResource1"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle CssClass="RedTextAlignCenter" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateFieldResource20">
                        <ItemTemplate>
                            <asp:TextBox ID="txtSettlementAmount" runat="server" Text='<%# Eval("AmountRMB") %>'
                                Width="80px"></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Right" />
                        <FooterTemplate>
                            <asp:Label runat="server" ID="lblSettlementAmountTotal" meta:resourcekey="lblSettlementAmountTotalResource1"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle CssClass="RedTextAlignCenter" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateFieldResource21">
                        <ItemTemplate>
                            <asp:Label ID="lblRemark" runat="server" Text='<%# Eval("ApplyRemark") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="150px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateFieldResource22">
                        <ItemTemplate>
                            <asp:TextBox ID="txtSettlementRemark" runat="server" Text='<%# Eval("Remark") %>'
                                Width="160px"></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle Width="190px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <FooterStyle Width="50px" Wrap="True" />
            </gc:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="odsExpenseDetails" runat="server" SelectMethod="GetFormSettlementExpenseDetail"
        TypeName="BusinessObjects.FormSaleBLL" OnObjectCreated="odsExpenseDetails_OnObjectCreated">
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
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
    <uc3:ucUpdateProgress ID="upUP" runat="server" vAssociatedUpdatePanelID="upButton" />
</asp:Content>
