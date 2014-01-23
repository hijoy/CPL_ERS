<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeFile="POSpecialApply.aspx.cs" Culture="Auto" UICulture="Auto" Inherits="FormPurchase_POSpecialApply" %>


<%@ Register Src="../UserControls/UCDateInput.ascx" TagName="UCDateInput" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/MultiAttachmentFile.ascx" TagName="UCFlie" TagPrefix="uc2" %>
<%@ Register Src="../UserControls/ucUpdateProgress.ascx" TagName="ucUpdateProgress" TagPrefix="uc3" %>
<%@ Register Src="../UserControls/ItemControl.ascx" TagName="UCItem" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <script src="../Script/DateInput.js" type="text/javascript"></script>
    <script type="text/javascript">
        function ParameterChanged(priceCtl, quantityCtl, amountCtl, amountRMBCtl,exchangeRate) {
            var txtPrice = document.getElementById(priceCtl);
            var txtQuantity = document.getElementById(quantityCtl);
            
            if (txtPrice.value != "" && isNaN(txtPrice.value)) {
                alert("请录入数字");
                window.setTimeout(function () { document.getElementById(priceCtl).focus(); }, 0);
                return false;
            }

            if (txtQuantity != "" && isNaN(txtQuantity.value)) {
                alert("请录入数字");
                window.setTimeout(function () { document.getElementById(quantityCtl).focus(); }, 0);
                return false;
            }

            if (!isNaN(parseFloat(txtPrice.value)) && !isNaN(parseFloat(txtQuantity.value))) {
                var price = parseFloat(txtPrice.value);
                var quantity = parseFloat(txtQuantity.value);
                var amount = quantity * price;
                var amountRMB = amount * parseFloat(exchangeRate);
                document.getElementById(amountCtl).value = commafy(amount.toFixed(2));
                document.getElementById(amountRMBCtl).value = commafy(amountRMB.toFixed(2));
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
    <div class="title"><asp:Label Text="<%$Resources:Common,Label_BasicInfo %>" runat="server" /></div>
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
                        <asp:Label ID="Label_VendorCode" runat="server" meta:resourcekey="Label_VendorCode" />
                     </div>
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
                        <asp:Label ID="Label_VendorAddress" runat="server" meta:resourcekey="Label_VendorAddress" />
                    </div>
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
                        <asp:TextBox ID="PurchaseBudgetTypeCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_PurchaseType" runat="server" Text="<%$Resources:Common,Form_PurchaseType %>" /></div>
                    <div>
                        <asp:DropDownList ID="PurchaseTypeDDL" runat="server" DataSourceID="odsPurchaseType" DataTextField="PurchaseTypeName" DataValueField="PurchaseTypeID" Width="180px" >
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="odsPurchaseType" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                            SelectCommand="  SELECT [PurchaseTypeID], PurchaseTypeName FROM [PurchaseType] order by PurchaseTypeName">
                        </asp:SqlDataSource>
                    </div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_ShippingTerm" runat="server" Text="<%$Resources:Common,Form_ShippingTerm %>" /></div>
                    <div>
                        <asp:DropDownList ID="ShippingTermDDL" runat="server" DataSourceID="odsShippingTerm" DataTextField="ShippingTermName" DataValueField="ShippingTermID" Width="180px" >
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="odsShippingTerm" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                            SelectCommand="  SELECT [ShippingTermID], ShippingTermName FROM [ShippingTerm] where IsActive = 1 order by ShippingTermName ">
                        </asp:SqlDataSource>
                    </div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_PaymentTerms" runat="server" Text="<%$Resources:Common,Form_PaymentTerms %>" /></div>
                    <div>
                        <asp:TextBox ID="PaymentTermCtl" runat="server" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label_ApplyAmount" runat="server" meta:resourcekey="Label_ApplyAmount" />
                    </div>
                    <div>
                        <asp:TextBox ID="ApplyAmountCtl" runat="server" Width="170px"  CssClass="InputTextReadOnly" ReadOnly="true"></asp:TextBox></div>
                </td>
                <td style="width: 200px" valign="top">
                    <div class="field_title">
                        <asp:Label ID="Form_FormNo" runat="server" Text="<%$Resources:Common,Form_FormNo %>" /></div>
                    <div style="margin-top: 5px">
                        <asp:HyperLink ID="ApplyFormNoCtl" runat="server"></asp:HyperLink></div>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="width: 400px">
                    <div class="field_title">
                        <asp:Label ID="Form_DeliveryAddress" runat="server" Text="<%$Resources:Common,Form_DeliveryCompany %>" /></div>
                    <div>
                        <asp:DropDownList ID="CompanyDDL" runat="server" DataSourceID="odsCompany" DataTextField="CompanyAddress" DataValueField="CompanyID" 
                                Width="370px" AutoPostBack="true" OnSelectedIndexChanged="CompanyDDL_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="odsCompany" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                            SelectCommand="select 0 CompanyID,' Please Select' CompanyAddress Union SELECT CompanyID, CompanyCode+'-'+CompanyAddress FROM Company ">
                        </asp:SqlDataSource>
                    </div>
                </td>
                <td colspan="2" style="width: 400px">
                    <div class="field_title">
                        <asp:Label ID="Label10" runat="server" Text="<%$Resources:Common,Form_DeliveryAddress %>" /></div>
                    <div>
                        <asp:TextBox ID="RealDeliveryAddressCtl" runat="server" Width="370px"></asp:TextBox></div>
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
                <td colspan="3" valign="top">
                    <div class="field_title">
                        <asp:Label ID="Form_Adjunct" runat="server" Text="<%$Resources:Common,Form_Adjunct %>" /></div>
                        <uc2:UCFlie ID="UCFileUpload" runat="server" Width="400px" />
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div class="title"><asp:Label runat="server" meta:resourcekey="Label_Title" /></div>
    <asp:UpdatePanel ID="upDetails" runat="server">
        <ContentTemplate>
            <gc:GridView ID="gvDetails" runat="server" ShowFooter="True" CssClass="GridView" AutoGenerateColumns="False" DataKeyNames="FormPODetailID" DataSourceID="odsDetails"
                OnRowDataBound="gvDetails_RowDataBound" CellPadding="0" OnDataBound="gvDetails_OnDataBound">
                <Columns>
                    <asp:TemplateField meta:resourcekey="TemplateField_Item">
                        <ItemTemplate>
                            <asp:Label ID="lblItem" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <uc4:UCItem ID="UCItem" runat="server" ItemID='<%# Bind("ItemID") %>' OnItemNameTextChanged="UCItem_SelectedIndexChanged" 
                                IsNoClear="true" Width="240px" ItemCategoryID="<%# CategoryID %>" AutoPostBack="true"/>
                        </EditItemTemplate>
                        <ItemStyle Width="300px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateField_Package">
                        <ItemTemplate>
                            <asp:Label ID="lblPackage" runat="server" Text='<%# Eval("Package") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="PackageCtl" runat="server" Width="60px" Text='<%# Eval("Package") %>' ReadOnly="true"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateField_UnitPrice">
                        <ItemTemplate>
                            <asp:Label ID="lblUnitPrice" runat="server" Text='<%# Eval("UnitPrice","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="UnitPriceCtl" runat="server" Width="60px" Text='<%# Eval("UnitPrice","{0:N}") %>' ReadOnly="true"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle Width="80px" HorizontalAlign="Right" />
                        <FooterTemplate>
                            <asp:Label runat="server" ID="lblTotal" ></asp:Label>
                        </FooterTemplate>
                        <FooterStyle CssClass="RedTextAlignCenter" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateField_FinalPrice">
                        <ItemTemplate>
                            <asp:Label ID="lblFinalPrice" runat="server" Text='<%# Eval("FinalPrice","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="FinalPriceCtl" runat="server" Text='<%# Bind("FinalPrice") %>' Width="70px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle Width="80px" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateField_Quantity">
                        <ItemTemplate>
                            <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("Quantity","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="QuantityCtl" runat="server" Text='<%# Bind("Quantity") %>' Width="70px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle Width="80px" HorizontalAlign="Right" />
                        <FooterTemplate>
                            <asp:Label runat="server" ID="lblsum" Text="<%$Resources:Common,Form_Total %>" ></asp:Label>
                        </FooterTemplate>
                        <FooterStyle CssClass="RedTextAlignCenter" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateField_Amount">
                        <ItemTemplate>
                            <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="AmountCtl" runat="server" Width="90px" Text='<%# Eval("Amount","{0:N}") %>' ReadOnly="true"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Right" />
                        <FooterTemplate>
                            <asp:Label runat="server" ID="lblAmountTotal" ></asp:Label>
                        </FooterTemplate>
                        <FooterStyle CssClass="RedTextAlignCenter" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateField_AmountRMB">
                        <ItemTemplate>
                            <asp:Label ID="lblAmountRMB" runat="server" Text='<%# Eval("AmountRMB","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="AmountRMBCtl" runat="server" Width="90px" Text='<%# Eval("AmountRMB","{0:N}") %>' ReadOnly="true"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Right" />
                        <FooterTemplate>
                            <asp:Label runat="server" ID="lblAmountRMBTotal" ></asp:Label>
                        </FooterTemplate>
                        <FooterStyle CssClass="RedTextAlignCenter" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateField_DeliveryDate">
                        <ItemTemplate>
                            <asp:Label ID="lblDeliveryDate" runat="server" Text='<%# Eval("DeliveryDate","{0:yyyy-MM-dd}") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <uc1:UCDateInput ID="UCDeliveryDate" runat="server" SelectedDate='<%# Bind("DeliveryDate") %>' />
                        </EditItemTemplate>
                        <ItemStyle Width="150px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Common,Form_Remark %>">
                        <ItemTemplate>
                            <asp:Label ID="lblDeliveryAddress" runat="server" Text='<%# Eval("DeliveryAddress") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="DeliveryAddressCtl" runat="server" Text='<%# Bind("DeliveryAddress") %>' Width="180px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle Width="200px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="Edit"
                                Text="<%$Resources:Common,Button_Edit %>"></asp:LinkButton>
                            <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Delete"
                                Text="<%$Resources:Common,Button_Delete %>"></asp:LinkButton>
                        </ItemTemplate>
                        <EditItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="Update"
                                Text="<%$Resources:Common,Button_Update %>"></asp:LinkButton>
                            <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel"
                                Text="<%$Resources:Common,Button_Cancel %>"></asp:LinkButton>
                        </EditItemTemplate>
                        <ItemStyle Width="70px" HorizontalAlign="Center" />
                    </asp:TemplateField>

                </Columns>
                <HeaderStyle CssClass="Header" />
                <FooterStyle Width="50px" Wrap="True" />
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td style="width: 300px;" class="Empty1">
                                <asp:Label ID="Label1" runat="server" meta:resourcekey="Label_Item" />
                            </td>
                            <td style="width: 80px;">
                                <asp:Label ID="Label2" runat="server" meta:resourcekey="Label_Package" />
                            </td>
                            <td style="width: 80px;">
                                <asp:Label ID="Label3" runat="server" meta:resourcekey="Label_UnitPrice" />
                            </td>
                            <td style="width: 80px;">
                                <asp:Label ID="Label4" runat="server" meta:resourcekey="Label_FinalPrice" />
                            </td>
                            <td style="width: 80px;">
                                <asp:Label ID="Label5" runat="server" meta:resourcekey="Label_Quantity" />
                            </td>
                            <td style="width: 100px;">
                                <asp:Label ID="Label6" runat="server" meta:resourcekey="Label_Amount" />
                            </td>
                            <td style="width: 100px;">
                                <asp:Label ID="Label7" runat="server" meta:resourcekey="Label_AmountRMB" />
                            </td>
                            <td style="width: 150px;">
                                <asp:Label ID="Label8" runat="server" meta:resourcekey="Label_DeliveryDate" />
                            </td>
                            <td style="width: 200px;">
                                <asp:Label ID="Label9" runat="server" Text="<%$Resources:Common,Form_Remark %>" />
                            </td>
                            <td style="width: 70px">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
            </gc:GridView>
            <asp:FormView ID="fvDetails" runat="server" DataKeyNames="FormPODetailID" DataSourceID="odsDetails" DefaultMode="Insert" CellPadding="0" OnDataBound="fvDetails_OnDataBound">
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr>
                            <td style="width: 300px;" align="center">
                                <uc4:UCItem ID="NewUCItem" runat="server" ItemID='<%# Bind("ItemID") %>' OnItemNameTextChanged="NewUCItem_SelectedIndexChanged" 
                                    IsNoClear="true" Width="240px" ItemCategoryID="<%# CategoryID %>" AutoPostBack="true"/>
                            </td>
                            <td style="width: 80px;" align="center" valign="top">
                                <asp:TextBox ID="NewPackageCtl" runat="server" Width="60px" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td style="width: 80px;" align="center" valign="top">
                                <asp:TextBox ID="NewUnitPriceCtl" runat="server" Width="60px" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td style="width: 80px;" align="center">
                                <asp:TextBox ID="NewFinalPriceCtl" runat="server" Text='<%# Bind("FinalPrice") %>' Width="70px"></asp:TextBox>
                            </td>                            
                            <td style="width: 80px;" align="center">
                                <asp:TextBox ID="NewQuantityCtl" runat="server" Text='<%# Bind("Quantity") %>' Width="70px"></asp:TextBox>
                            </td>
                            <td style="width: 100px;" align="center">
                                <asp:TextBox ID="NewAmountCtl" runat="server" Width="90px" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td style="width: 100px;" align="center">
                                <asp:TextBox ID="NewAmountRMBCtl" runat="server" Width="90px" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td style="width: 150px;" align="center">
                                <uc1:UCDateInput ID="NewUCDeliveryDate" runat="server" SelectedDate='<%# Bind("DeliveryDate") %>' />
                            </td>
                            <td style="width: 200px;" align="center">
                                <asp:TextBox ID="NewDeliveryAddressCtl" runat="server" Text='<%# Bind("DeliveryAddress") %>' Width="130px"></asp:TextBox>
                            </td>
                            <td style="width: 70px;" align="center">
                                <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    Text="<%$Resources:Common,Button_Add %>" ValidationGroup="NewDetailRow"></asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <asp:RequiredFieldValidator ID="RF1" runat="server" ControlToValidate="NewFinalPriceCtl"
                                Display="None" meta:resourcekey="RequiredFieldValidator_RF1" ValidationGroup="NewDetailRow" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="NewQuantityCtl"
                                Display="None" meta:resourcekey="RequiredFieldValidator_RF2" ValidationGroup="NewDetailRow" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="NewUCDeliveryDate$txtDate"
                                Display="None" meta:resourcekey="RequiredFieldValidator_RF3" ValidationGroup="NewDetailRow" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="NewUCItem$txtDisplayItemName"
                                Display="None" meta:resourcekey="RequiredFieldValidator_RF4" ValidationGroup="NewDetailRow" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <asp:ValidationSummary ID="ValidationSummaryINS" runat="server" ShowMessageBox="True"
                                ShowSummary="False" ValidationGroup="NewDetailRow" />
                        </tr>
                    </table>
                </InsertItemTemplate>
            </asp:FormView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="odsDetails" runat="server" DeleteMethod="DeleteFormPODetailByID"
        SelectMethod="GetPODetail" TypeName="BusinessObjects.FormPurchaseBLL"
        OnInserting="odsDetails_Inserting" OnObjectCreated="odsDetails_ObjectCreated" OnUpdating="odsDetails_Updating"
        InsertMethod="AddFormPODetail" UpdateMethod="UpdateFormPODetail" >
        <DeleteParameters>
            <asp:Parameter Name="FormPODetailID" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="FormPOID" Type="Int32" />
            <asp:Parameter Name="ItemID" Type="Int32" />
            <asp:Parameter Name="FinalPrice" Type="Decimal" />
            <asp:Parameter Name="Quantity" Type="Decimal" />
            <asp:Parameter Name="ExchangeRate" Type="Decimal" />
            <asp:Parameter Name="DeliveryDate" Type="Datetime" />
            <asp:Parameter Name="DeliveryAddress" Type="String" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="FormPODetailID" Type="Int32" />
            <asp:Parameter Name="ItemID" Type="Int32" />
            <asp:Parameter Name="FinalPrice" Type="Decimal" />
            <asp:Parameter Name="Quantity" Type="Decimal" />
            <asp:Parameter Name="ExchangeRate" Type="Decimal" />
            <asp:Parameter Name="DeliveryDate" Type="Datetime" />
            <asp:Parameter Name="DeliveryAddress" Type="String" />
        </UpdateParameters>
    </asp:ObjectDataSource>

    <br />
    <asp:UpdatePanel ID="upButton" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div style="margin-top: 20px; width: 1200px; text-align: right">
                <asp:Button ID="SubmitBtn" runat="server" CssClass="button_nor" OnClick="SubmitBtn_Click" Text="<%$Resources:Common,Button_Submit %>" />
                <asp:Button ID="SaveBtn" runat="server" CssClass="button_nor"  OnClick="SaveBtn_Click" Text="<%$Resources:Common,Button_Save %>" />
                <asp:Button ID="CancelBtn" runat="server" CssClass="button_nor" OnClick="CancelBtn_Click" Text="<%$Resources:Common,Button_Back %>" />
                <asp:Button ID="DeleteBtn" runat="server" CssClass="button_nor" OnClick="DeleteBtn_Click" Text="<%$Resources:Common,Button_Delete %>" />
            </div>
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
    <uc3:ucUpdateProgress ID="upUP" runat="server" vAssociatedUpdatePanelID="upButton" />


</asp:Content>
