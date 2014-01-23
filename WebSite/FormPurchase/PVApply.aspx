<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="PVApply.aspx.cs" Culture="Auto" UICulture="Auto" Inherits="FormPurchase_PVApply" %>

<%@ Register Src="../UserControls/UCDateInput.ascx" TagName="UCDateInput" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/MultiAttachmentFile.ascx" TagName="UCFlie" TagPrefix="uc2" %>
<%@ Register Src="../UserControls/ucUpdateProgress.ascx" TagName="ucUpdateProgress"
    TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <script src="../Script/DateInput.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">

        function ParameterChanged(amtBeforeTaxCtl, amtTaxCtl, amountCtl, amountRMBCtl, exchangeRate) {
            var txtAmtBeforeTax = document.getElementById(amtBeforeTaxCtl);
            var txtAmtTax = document.getElementById(amtTaxCtl);
            var txtAmount = document.getElementById(amountCtl);

            if (txtAmtBeforeTax.value != "" && isNaN(txtAmtBeforeTax.value)) {
                alert("请录入数字");
                window.setTimeout(function () { document.getElementById(amtBeforeTaxCtl).focus(); }, 0);
                return false;
            }

            if (txtAmtTax.value != "" && isNaN(txtAmtTax.value)) {
                alert("请录入数字");
                window.setTimeout(function () { document.getElementById(amtTaxCtl).focus(); }, 0);
                return false;
            }
            var AmtBeforeTax;
            var AmtTax;
            if (txtAmtBeforeTax.value == "") {
                AmtBeforeTax = 0;
            } else {
                AmtBeforeTax = parseFloat(txtAmtBeforeTax.value);
            }
            if (txtAmtTax.value == "") {
                AmtTax = 0;
            } else {
                AmtTax = parseFloat(txtAmtTax.value);
            }

            var amount = AmtBeforeTax + AmtTax;
            var amountRMB = amount * parseFloat(exchangeRate);
            document.getElementById(amountCtl).value = commafy(amount.toFixed(2));
            document.getElementById(amountRMBCtl).value = commafy(amountRMB.toFixed(2));

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
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_Period" runat="server" Text="<%$Resources:Common,Form_Period %>" /></div>
                    <div>
                        <asp:TextBox ID="PeriodCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label_VendorCode" runat="server" meta:resourcekey="Label_VendorCode" /></div>
                    <div>
                        <asp:TextBox ID="VendorCodeCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_VendorName" runat="server" Text="<%$Resources:Common,Form_VendorName %>" /></div>
                    <div>
                        <asp:TextBox ID="VendorNameCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label_VendorAddress" runat="server" meta:resourcekey="Label_VendorAddress" /></div>
                    <div>
                        <asp:TextBox ID="VendorAddressCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_ItemCategory" runat="server" Text="<%$Resources:Common,Form_ItemCategory %>" /></div>
                    <div>
                        <asp:TextBox ID="ItemCategoryCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_Currency" runat="server" Text="<%$Resources:Common,Form_Currency %>" /></div>
                    <div>
                        <asp:TextBox ID="CurrencyCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_ExchangeRate" runat="server" Text="<%$Resources:Common,Form_ExchangeRate %>" /></div>
                    <div>
                        <asp:TextBox ID="ExchangeRateCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox></div>
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_BudgetType" runat="server" Text="<%$Resources:Common,Form_BudgetType %>" /></div>
                    <div>
                        <asp:TextBox ID="PurchaseBudgetTypeCtl" runat="server" CssClass="InputTextReadOnly"
                            ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_PurchaseType" runat="server" Text="<%$Resources:Common,Form_PurchaseType %>" /></div>
                    <div>
                        <asp:TextBox ID="PurchaseTypeCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox>
                    </div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_ShippingTerm" runat="server" Text="<%$Resources:Common,Form_ShippingTerm %>" /></div>
                    <div>
                        <asp:TextBox ID="ShippingTermCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox>
                    </div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_PaymentTerms" runat="server" Text="<%$Resources:Common,Form_PaymentTerms %>" /></div>
                    <div>
                        <asp:TextBox ID="PaymentTermCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td colspan="2" style="width: 400px">
                    <div class="field_title">
                        <asp:Label ID="Form_DeliveryAddress" runat="server" Text="<%$Resources:Common,Form_DeliveryAddress %>" /></div>
                    <div>
                        <asp:TextBox ID="RealDeliveryAddressCtl" runat="server" CssClass="InputTextReadOnly"
                            ReadOnly="true" Width="370px"></asp:TextBox></div>
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_PaymentType" runat="server" Text="<%$Resources:Common,Form_PaymentType %>" /></div>
                    <div>
                        <asp:DropDownList ID="MethodPaymentDDL" runat="server" DataSourceID="odsMethodPayment"
                            DataTextField="MethodPaymentName" DataValueField="MethodPaymentID" Width="180px">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="odsMethodPayment" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                            SelectCommand="  SELECT MethodPaymentID, MethodPaymentName+'-'+Description MethodPaymentName FROM [MethodPayment] where IsActive=1 ">
                        </asp:SqlDataSource>
                    </div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label_ExpectPaymentDate" runat="server" meta:resourcekey="Label_ExpectPaymentDate" />
                        <span class="requiredLable">*</span>
                    </div>
                    <div>
                        <uc1:UCDateInput ID="UCExpectPaymentDateCtl" runat="server" />
                    </div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label_Urgent" runat="server" meta:resourcekey="Label_Urgent" /></div>
                    <div>
                        <asp:DropDownList ID="IsUrgentDDL" runat="server" Width="180px">
                            <asp:ListItem Selected="true" Text="No" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label_Public" runat="server" meta:resourcekey="Label_Public" /></div>
                    <div>
                        <asp:DropDownList ID="IsPublicDDL" runat="server" Width="180px">
                            <asp:ListItem Selected="true" Text="No" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                        </asp:DropDownList>
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
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label2" runat="server" Text="<%$Resources:Common,Form_VATRate %>" /><span
                            class="requiredLable">*</span></div>
                    <div>
                        <asp:DropDownList ID="VATRateDDL" runat="server" DataSourceID="odsVATRate" DataTextField="VATTypeName"
                            DataValueField="VATTypeID" Width="180px" AutoPostBack="true" OnSelectedIndexChanged="VATRateDDL_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="odsVATRate" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                            SelectCommand="select 0 VATTypeID,' Please select' VATTypeName Union  SELECT VATTypeID, VATTypeName+'-'+[Description] VATTypeName FROM VATType where IsActive=1 ">
                        </asp:SqlDataSource>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_AmountRMB" runat="server" Text="<%$Resources:Common,Form_AmountRMB %>" /></div>
                    <div>
                        <asp:TextBox ID="ApplyAmountCtl" runat="server" Width="170px" CssClass="InputTextReadOnly"
                            ReadOnly="true"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label_PayedAmount" runat="server" meta:resourcekey="Label_PayedAmount" />
                    </div>
                    <div>
                        <asp:TextBox ID="PayedAmountCtl" runat="server" Width="170px" CssClass="InputTextReadOnly"
                            ReadOnly="true"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label3" runat="server" Text="<%$Resources:Common,Form_AMTBeforeTax %>" />
                    </div>
                    <div>
                        <asp:TextBox ID="AMTBeforeTaxCtl" runat="server" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label4" runat="server" Text="<%$Resources:Common,Form_AMTTax %>" />
                        <span class="requiredLable">*</span>
                    </div>
                    <div>
                        <asp:TextBox ID="AMTTaxCtl" runat="server" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label_Amount1" runat="server" meta:resourcekey="Label_Amount1" />
                    </div>
                    <div>
                        <asp:TextBox ID="AmountCtl" runat="server" Width="170px" ReadOnly="true"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label_AmountRMB1" runat="server" meta:resourcekey="Label_AmountRMB1" />
                    </div>
                    <div>
                        <asp:TextBox ID="AmountRMBCtl" runat="server" Width="170px" CssClass="InputTextReadOnly"
                            ReadOnly="true"></asp:TextBox></div>
                </td>
            </tr>
            <tr>
                <td colspan="3" valign="top">
                    <div class="field_title">
                        <asp:Label ID="Form_Remark" runat="server" Text="<%$Resources:Common,Form_Remark %>" /></div>
                    <div>
                        <asp:TextBox ID="RemarkCtl" runat="server" CssClass="InputText" TextMode="multiline"
                            Height="60px" Columns="75"></asp:TextBox>
                    </div>
                </td>
                <td colspan="2" valign="top">
                    <div class="field_title">
                        <asp:Label ID="Form_Adjunct" runat="server" Text="<%$Resources:Common,Form_Adjunct %>" /></div>
                    <uc2:UCFlie ID="UCFileUpload" runat="server" Width="350px" />
                </td>
                <td style="width: 200px" valign="top">
                    <div class="field_title">
                        <asp:Label ID="Label_ParentForm" runat="server" meta:resourcekey="Label_ParentForm" />
                    </div>
                    <div style="margin-top: 5px">
                        <asp:HyperLink ID="ParentFormNoCtl" runat="server"></asp:HyperLink></div>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div class="title">
        <asp:Label ID="Label_InvoiceTitle" Text="<%$Resources:Common,Label_InvoiceTitle %>"
            runat="server" /></div>
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
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td style="width: 200px;" class="Empty1">
                                <asp:Label ID="Label1" Text="<%$Resources:Common,Form_InvoiceDate %>" runat="server" />
                            </td>
                            <td style="width: 150px;">
                                <asp:Label ID="Label9" Text="<%$Resources:Common,Form_InvoiceNo %>" runat="server" />
                            </td>
                            <td style="width: 150px;">
                                <asp:Label ID="Label10" Text="<%$Resources:Common,Form_InvoiceAmount %>" runat="server" />
                            </td>
                            <td style="width: 340px;">
                                <asp:Label ID="Label11" Text="<%$Resources:Common,Form_Remark %>" runat="server" />
                            </td>
                            <td style="width: 300px;">
                                <asp:Label ID="Label12" Text="<%$Resources:Common,Form_SystemInfo %>" runat="server" />
                            </td>
                            <td style="width: 100px">
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
                            <td style="width: 100px;" align="center">
                                <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    Text="<%$Resources:Common,Button_Add %>" ValidationGroup="NewDetailRow"></asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="UCInvoiceDate$txtDate"
                                meta:resourcekey="RequiredFieldValidator_RF5" Display="None" ValidationGroup="NewDetailRow"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RF1" runat="server" ControlToValidate="txtInvoiceNo"
                                meta:resourcekey="RequiredFieldValidator_RF6" Display="None" ValidationGroup="NewDetailRow"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RF2" runat="server" ControlToValidate="txtAmount"
                                meta:resourcekey="RequiredFieldValidator_RF7" Display="None" ValidationGroup="NewDetailRow"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtAmount"
                                ValidationExpression="<%$ Resources:RegularExpressions, Money %>" Display="None"
                                meta:resourcekey="RegularExpressionValidator_REV1" ValidationGroup="NewDetailRow"></asp:RegularExpressionValidator>
                            <asp:ValidationSummary ID="ValidationSummaryINS" runat="server" ShowMessageBox="True"
                                ShowSummary="False" ValidationGroup="NewDetailRow" />
                        </tr>
                    </table>
                    <br />
                </InsertItemTemplate>
            </asp:FormView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="odsInvoice" runat="server" DeleteMethod="DeleteFormInvoiceByID"
        SelectMethod="GetFormInvoice" TypeName="BusinessObjects.FormPurchaseBLL" InsertMethod="AddFormInvoice"
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
    <div class="title">
        <asp:Label ID="Label1" runat="server" meta:resourcekey="Label_Title" /></div>
    <asp:UpdatePanel ID="upDetails" runat="server">
        <ContentTemplate>
            <gc:GridView ID="gvDetails" runat="server" ShowFooter="True" CssClass="GridView"
                AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="odsDetails" CellPadding="0">
                <Columns>
                    <asp:TemplateField meta:resourcekey="TemplateField_Item">
                        <ItemTemplate>
                            <asp:Label ID="lblItem" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="300px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateField_Package">
                        <ItemTemplate>
                            <asp:Label ID="lblPackage" runat="server" Text='<%# Eval("Package") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateField_UnitPrice">
                        <ItemTemplate>
                            <asp:Label ID="lblUnitPrice" runat="server" Text='<%# Eval("UnitPrice","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="80px" HorizontalAlign="Right" />
                        <FooterTemplate>
                            <asp:Label runat="server" ID="lblTotal"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle CssClass="RedTextAlignCenter" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateField_FinalPrice">
                        <ItemTemplate>
                            <asp:Label ID="lblFinalPrice" runat="server" Text='<%# Eval("FinalPrice","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="80px" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateField_Quantity">
                        <ItemTemplate>
                            <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("Quantity","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="80px" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateField_Amount">
                        <ItemTemplate>
                            <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateField_AmountRMB">
                        <ItemTemplate>
                            <asp:Label ID="lblAmountRMB" runat="server" Text='<%# Eval("AmountRMB","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateField_DeliveryDate">
                        <ItemTemplate>
                            <asp:Label ID="lblDeliveryDate" runat="server" Text='<%# Eval("DeliveryDate","{0:yyyy-MM-dd}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="150px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Common,Form_Remark %>">
                        <ItemTemplate>
                            <asp:Label ID="lblDeliveryAddress" runat="server" Text='<%# Eval("DeliveryAddress") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="270px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <FooterStyle Width="50px" Wrap="True" />
            </gc:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="odsDetails" runat="server" SelectMethod="GetFormPRPODetailView"
        TypeName="BusinessObjects.FormPurchaseBLL" OnObjectCreated="odsDetails_ObjectCreated">
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
                <asp:Button ID="DeleteBtn" runat="server" CssClass="button_nor" OnClick="DeleteBtn_Click"
                    Text="<%$Resources:Common,Button_Delete %>" />
            </div>
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
    <uc3:ucUpdateProgress ID="upUP" runat="server" vAssociatedUpdatePanelID="upButton" />
</asp:Content>
