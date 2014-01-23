<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="FormVendorApply.aspx.cs" UICulture="Auto" Culture="Auto" Inherits="FormPurchase_FormVendorApply" %>

<%@ Register Src="../UserControls/VendorTypeControl.ascx" TagName="VendorTypeControl"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControls/ucUpdateProgress.ascx" TagName="ucUpdateProgress"
    TagPrefix="uc2" %>
<%@ Register Src="../UserControls/MultiAttachmentFile.ascx" TagName="UCFlie" TagPrefix="uc2" %>
<%@ Register Src="../UserControls/UCDateInput.ascx" TagName="UCDateInput" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <script src="../Script/DateInput.js" type="text/javascript"></script>
    <script type="text/javascript">
        function CopyValue(FromObj, ToObj) {
            var From = document.getElementById(FromObj);
            var To = document.getElementById(ToObj);
            if (!To.value) {
                To.value = From.value;
            }
        }
    </script>
    <div class="title">
        <asp:Label ID="Label1" Text="<%$Resources:Common,Label_BasicInfo %>" runat="server" /></div>
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
            </tr>
        </table>
    </div>
    <br />
    <div class="title">
        <asp:Label ID="Label2" runat="server" meta:resourcekey="Label_Title" />
    </div>
    <div class="searchDiv">
        <table class="searchTable">
            <tr>
                <td colspan="2" style="width: 400px">
                    <div class="field_title">
                        <span class="requiredLable">*</span>
                        <asp:Label ID="Form_VendorName" runat="server" Text="<%$Resources:Common,Form_VendorName %>" /></div>
                    <div>
                        <asp:TextBox ID="txtVendorName" runat="server" Width="370px"></asp:TextBox></div>
                </td>
                <td colspan="2" style="width: 400px">
                    <div class="field_title">
                        <span class="requiredLable">*</span>
                        <asp:Label ID="Label_VendorAddress" runat="server" meta:resourcekey="Label_VendorAddress" />
                    </div>
                    <div>
                        <asp:TextBox ID="txtVendorAddress" runat="server" Width="370px"></asp:TextBox></div>
                </td>
                <td colspan="2" style="width: 400px">
                    <div class="field_title">
                        <span class="requiredLable">*</span>
                        <asp:Label ID="Label_PurchasingAddress" runat="server" meta:resourcekey="Label_PurchasingAddress" /></div>
                    <div>
                        <asp:TextBox ID="txtPurchasingAddress" runat="server" Width="370px"></asp:TextBox></div>
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        <span class="requiredLable">*</span>
                        <asp:Label ID="Form_City" runat="server" Text="<%$Resources:Common,Form_City %>" />
                    </div>
                    <div>
                        <asp:TextBox ID="txtCity" runat="server" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <span class="requiredLable">*</span>
                        <asp:Label ID="Label_PurchasingCity" runat="server" meta:resourcekey="Label_PurchasingCity" /></div>
                    <div>
                        <asp:TextBox ID="txtPurchasingCity" runat="server" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <span class="requiredLable">*</span>
                        <asp:Label ID="Label_Postal" runat="server" meta:resourcekey="Label_Postal" />
                    </div>
                    <div>
                        <asp:TextBox ID="txtPostal" runat="server" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <span class="requiredLable">*</span>
                        <asp:Label ID="Lable_PurchaseingPostalCode" runat="server" meta:resourcekey="Lable_PurchaseingPostalCode" /></div>
                    <div>
                        <asp:TextBox ID="txtPurchaseingPostalCode" runat="server" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <span class="requiredLable">*</span>
                        <asp:Label ID="Label_ContactName" runat="server" meta:resourcekey="Label_ContactName" />
                    </div>
                    <div>
                        <asp:TextBox ID="txtContactName" runat="server" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <span class="requiredLable">*</span>
                        <asp:Label ID="Lable_PurchasingContact" runat="server" meta:resourcekey="Lable_PurchasingContact" /></div>
                    <div>
                        <asp:TextBox ID="txtPurchasingContact" runat="server" Width="170px"></asp:TextBox></div>
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        <span class="requiredLable">*</span>
                        <asp:Label ID="Label_VendorType" runat="server" meta:resourcekey="Label_VendorType" />
                    </div>
                    <div>
                        <uc1:VendorTypeControl ID="VendorTypeControl" runat="server" AutoPostBack="true"
                            OnVendorTypeNameTextChanged="txtVendorTypeName_TextChanged" IsNoClear="true" />
                    </div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label4" runat="server" Text="<%$Resources:Common,Label_CompanyCode %>" /></div>
                    <div>
                        <asp:TextBox ID="txtCompanyCode" runat="server" Width="170px" ReadOnly="true"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label_Company" runat="server" meta:resourcekey="Label_Company" /></div>
                    <div>
                        <asp:TextBox ID="txtCompany" runat="server" Width="170px" ReadOnly="true"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_Currency" runat="server" Text="<%$Resources:Common,Form_Currency %>" /></div>
                    <div>
                        <asp:TextBox ID="txtCurrency" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <span class="requiredLable">*</span>
                        <asp:Label ID="Label_OneTimeVendor" runat="server" meta:resourcekey="Label_OneTimeVendor" /></div>
                    <div>
                        <asp:DropDownList ID="ddlOneTimeVendor" Enabled="false" runat="server" Width="170px">
                            <asp:ListItem Text="Y" Value="1"></asp:ListItem>
                            <asp:ListItem Text="N" Value="0" Selected="True"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <span class="requiredLable">*</span>
                        <asp:Label ID="Label_HoldVendor" runat="server" meta:resourcekey="Label_HoldVendor" /></div>
                    <div>
                        <asp:DropDownList ID="ddlHoldVendor" runat="server" Width="170px">
                            <asp:ListItem Text="Y" Value="1"></asp:ListItem>
                            <asp:ListItem Text="N" Value="0" Selected="True"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        <span class="requiredLable">*</span>
                        <asp:Label ID="Label_PhoneNumber" runat="server" meta:resourcekey="Label_PhoneNumber" /></div>
                    <div>
                        <asp:TextBox ID="txtPhoneNumber" runat="server" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <span class="requiredLable">*</span>
                        <asp:Label ID="Label_PurchasePhoneNumber" runat="server" meta:resourcekey="Label_PurchasePhoneNumber" /></div>
                    <div>
                        <asp:TextBox ID="txtPurchasePhoneNumber" runat="server" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <span class="requiredLable">*</span>
                        <asp:Label ID="Lable_AlphaSearchKey" runat="server" meta:resourcekey="Lable_AlphaSearchKey" /></div>
                    <div>
                        <asp:TextBox ID="txtAlphaSearchKey" runat="server" Width="170px"></asp:TextBox></div>
                </td>
            </tr>
            <tr>
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
        <asp:Label ID="Label3" runat="server" meta:resourcekey="Label_Title1" /></div>
    <div class="searchDiv">
        <table class="searchTable">
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        <span class="requiredLable">*</span>
                        <asp:Label ID="Label_BankCode" runat="server" meta:resourcekey="Label_BankCode" />
                    </div>
                    <div>
                        <asp:DropDownList ID="ddlBankCode" runat="server" DataSourceID="odsBankCode" DataTextField="BankCode"
                            DataValueField="BankCodeID" Width="170px" AppendDataBoundItems="true">
                            <asp:ListItem Text="Please Select" Value=""></asp:ListItem>
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="odsBankCode" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>">
                        </asp:SqlDataSource>
                    </div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <span class="requiredLable">*</span>
                        <asp:Label ID="Form_PaymentType" runat="server" Text="<%$Resources:Common,Form_PaymentType %>" />
                    </div>
                    <div>
                        <asp:DropDownList ID="ddlMethodPayment" runat="server" Width="180px" AppendDataBoundItems="true"
                            Enabled="false">
                            <asp:ListItem Text="W" Value="1" Selected="True"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <span class="requiredLable">*</span>
                        <asp:Label ID="Form_PaymentTerms" runat="server" Text="<%$Resources:Common,Form_PaymentTerms %>" />
                    </div>
                    <asp:DropDownList ID="ddlPaymentTerm" runat="server" DataSourceID="odsPaymentTerm"
                        DataTextField="PaymentTermName" DataValueField="PaymentTermID" Width="170px"
                        AppendDataBoundItems="true">
                        <asp:ListItem Text="Please Select" Value=""></asp:ListItem>
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="odsPaymentTerm" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                        SelectCommand="select PaymentTermID,PaymentTermName+'-'+Description PaymentTermName from PaymentTerm where IsActive=1 order by PaymentTermName">
                    </asp:SqlDataSource>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <span class="requiredLable">*</span>
                        <asp:Label ID="Label_TransType" runat="server" meta:resourcekey="Label_TransType" />
                    </div>
                    <asp:DropDownList ID="ddlTransType" runat="server" DataSourceID="odsTransType" DataTextField="TransTypeName"
                        DataValueField="TransTypeID" Width="170px" AppendDataBoundItems="true">
                        <asp:ListItem Text="Please Select" Value=""></asp:ListItem>
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="odsTransType" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>">
                    </asp:SqlDataSource>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <span class="requiredLable">*</span>
                        <asp:Label ID="Label_ACType" runat="server" meta:resourcekey="Label_ACType" />
                    </div>
                    <asp:DropDownList ID="ddlACType" runat="server" DataSourceID="sdsACType" DataTextField="ACTypeName"
                        DataValueField="ACTypeID" Width="170px" AppendDataBoundItems="true">
                        <asp:ListItem Text="Please Select" Value=""></asp:ListItem>
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="sdsACType" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                        SelectCommand="select ACTypeID,ACTypeName+'-'+Description ACTypeName from ACType where IsActive=1 order by ACTypeName">
                    </asp:SqlDataSource>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <span class="requiredLable">*</span>
                        <asp:Label ID="Label_VATRate" runat="server" meta:resourcekey="Label_VATRate" />
                    </div>
                    <div>
                        <asp:DropDownList ID="ddlVatRate" runat="server" DataSourceID="sdsVATRate" DataTextField="VATTypeName"
                            DataValueField="VATTypeID" Width="170px" AppendDataBoundItems="true">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="sdsVATRate" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                            SelectCommand="select 0 VATTypeID,' Please select' VATTypeName Union  select VATTypeID,VATTypeName+'-'+[Description] VATTypeName from VATType where IsActive=1 order by VATTypeName">
                        </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        <span class="requiredLable">*</span>
                        <asp:Label ID="Label_BankName" runat="server" meta:resourcekey="Label_BankName" />
                    </div>
                    <div>
                        <asp:TextBox ID="txtBankName" runat="server" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <span class="requiredLable">*</span>
                        <asp:Label ID="Label_AccountNo" runat="server" meta:resourcekey="Label_AccountNo" />
                    </div>
                    <div>
                        <asp:TextBox ID="txtAccountNo" runat="server" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <span class="requiredLable">*</span>
                        <asp:Label ID="lblBankNo" runat="server" meta:resourcekey="Label_BankNo" />
                    </div>
                    <div>
                        <asp:TextBox ID="txtBankNo" runat="server" Width="170px"></asp:TextBox></div>
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <div class="field_title">
                        <asp:Label ID="lblRemark" runat="server" Text="<%$Resources:Common,Form_Remark %>" /></div>
                    <div>
                        <asp:TextBox ID="txtRemark" TextMode="MultiLine" Width="1000px" Height="60px" runat="server"></asp:TextBox></div>
                </td>
            </tr>
            <tr id="trModifyReason" runat="server" visible="false">
                <td colspan="5">
                    <div class="field_title">
                        <asp:Label ID="Label_ModifyReason" runat="server" meta:resourcekey="Label_ModifyReason" /></div>
                    <div>
                        <asp:TextBox ID="txtModifyReason" TextMode="MultiLine" Width="1000px" Height="60px"
                            runat="server"></asp:TextBox></div>
                </td>
            </tr>
        </table>
        <asp:ValidationSummary ID="ValidationSummaryINS" runat="server" ShowMessageBox="True"
            ShowSummary="False" ValidationGroup="NewDetail" />
    </div>
    <br />
    <asp:UpdatePanel ID="upButton" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div style="margin-top: 20px; width: 1200px; text-align: right">
                <asp:Button ID="SubmitBtn" runat="server" CssClass="button_nor" OnClick="SubmitBtn_Click"
                    Text="<%$Resources:Common,Button_Submit %>" CausesValidation="True" ValidationGroup="NewDetail" />
                <asp:Button ID="SaveBtn" runat="server" CssClass="button_nor" OnClick="SaveBtn_Click"
                    Text="<%$Resources:Common,Button_Save %>" CausesValidation="True" ValidationGroup="NewDetail" />
                <asp:Button ID="CancelBtn" runat="server" CssClass="button_nor" OnClick="CancelBtn_Click"
                    Text="<%$Resources:Common,Button_Back %>" />
                <asp:Button ID="DeleteBtn" runat="server" CssClass="button_nor" OnClick="DeleteBtn_Click"
                    Text="<%$Resources:Common,Button_Delete %>" Visible="false" />
            </div>
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
    <uc2:ucUpdateProgress ID="upUP" runat="server" vAssociatedUpdatePanelID="upButton" />
</asp:Content>
