<%@ Page Title="" Language="C#" MasterPageFile="~/DialogMasterPage.master" AutoEventWireup="true"
    CodeFile="VendorDetail.aspx.cs" Culture="auto" UICulture="auto" Inherits="Dialog_VendorDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphTop" runat="Server">
    <script language="javascript">
        var _oldColor;
        function SetNewColor(source) {
            _oldColor = source.style.backgroundColor;
            source.style.backgroundColor = '#C0C0C0';
        }

        function SetOldColor(source) {
            source.style.backgroundColor = _oldColor;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div class="title1">
        <asp:Label ID="Label2" runat="server" meta:resourcekey="Label_Title" /></div>
    <table style="background-color: #F6F6F6; vertical-align: top;">
        <tr>
            <td colspan="2" style="width: 400px">
                <div class="field_title">
                    <asp:Label ID="Form_VendorName" runat="server" Text="<%$Resources:Common,Form_VendorName %>" /></div>
                <div>
                    <asp:TextBox ID="txtVendorName" runat="server" Width="370px" CssClass="InputTextReadOnly"
                        ReadOnly="true"></asp:TextBox></div>
            </td>
            <td colspan="2" style="width: 400px">
                <div class="field_title">
                    <asp:Label ID="Label_VendorAddress" runat="server" meta:resourcekey="Label_VendorAddress" />
                </div>
                <div>
                    <asp:TextBox ID="txtVendorAddress" runat="server" Width="370px" CssClass="InputTextReadOnly"
                        ReadOnly="true"></asp:TextBox></div>
            </td>
            <td colspan="2" style="width: 400px">
                <div class="field_title">
                    <asp:Label ID="Label_PurchasingAddress" runat="server" meta:resourcekey="Label_PurchasingAddress" /></div>
                <div>
                    <asp:TextBox ID="txtPurchasingAddress" runat="server" CssClass="InputTextReadOnly"
                        Width="370px" ReadOnly="true"></asp:TextBox></div>
            </td>
        </tr>
        <tr>
            <td style="width: 200px">
                <div class="field_title">
                    <asp:Label ID="Form_City" runat="server" Text="<%$Resources:Common,Form_City %>" />
                </div>
                <div>
                    <asp:TextBox ID="txtCity" runat="server" Width="170px" CssClass="InputTextReadOnly"
                        ReadOnly="true"></asp:TextBox></div>
            </td>
            <td style="width: 200px">
                <div class="field_title">
                    <asp:Label ID="Label_PurchasingCity" runat="server" meta:resourcekey="Label_PurchasingCity" /></div>
                <div>
                    <asp:TextBox ID="txtPurchasingCity" runat="server" CssClass="InputTextReadOnly" Width="170px"
                        ReadOnly="true"></asp:TextBox></div>
            </td>
            <td style="width: 200px">
                <div class="field_title">
                    <asp:Label ID="Label_Postal" runat="server" meta:resourcekey="Label_Postal" />
                </div>
                <div>
                    <asp:TextBox ID="txtPostal" runat="server" Width="170px" CssClass="InputTextReadOnly"
                        ReadOnly="true"></asp:TextBox></div>
            </td>
            <td style="width: 200px">
                <div class="field_title">
                    <asp:Label ID="Lable_PurchaseingPostalCode" runat="server" meta:resourcekey="Lable_PurchaseingPostalCode" /></div>
                <div>
                    <asp:TextBox ID="txtPurchaseingPostalCode" runat="server" CssClass="InputTextReadOnly"
                        Width="170px" ReadOnly="true"></asp:TextBox></div>
            </td>
            <td style="width: 200px">
                <div class="field_title">
                    <asp:Label ID="Label_ContactName" runat="server" meta:resourcekey="Label_ContactName" />
                </div>
                <div>
                    <asp:TextBox ID="txtContactName" runat="server" CssClass="InputTextReadOnly" Width="170px"
                        ReadOnly="true"></asp:TextBox></div>
            </td>
            <td style="width: 200px">
                <div class="field_title">
                    <asp:Label ID="Lable_PurchasingContact" runat="server" meta:resourcekey="Lable_PurchasingContact" /></div>
                <div>
                    <asp:TextBox ID="txtPurchasingContact" runat="server" CssClass="InputTextReadOnly"
                        Width="170px" ReadOnly="true"></asp:TextBox></div>
            </td>
        </tr>
        <tr>
            <td style="width: 200px">
                <div class="field_title">
                    <asp:Label ID="Label_VendorType" runat="server" meta:resourcekey="Label_VendorType" />
                </div>
                <div>
                    <asp:TextBox ID="txtVendorType" runat="server" CssClass="InputTextReadOnly" Width="170px"
                        ReadOnly="true"></asp:TextBox>
                </div>
            </td>
            <td style="width: 200px">
                <div class="field_title">
                    <asp:Label ID="Label4" runat="server" Text="<%$Resources:Common,Label_CompanyCode %>" /></div>
                <div>
                    <asp:TextBox ID="txtCompanyCode" runat="server" Width="170px" ReadOnly="true" CssClass="InputTextReadOnly"></asp:TextBox></div>
            </td>
            <td style="width: 200px">
                <div class="field_title">
                    <asp:Label ID="Label_Company" runat="server" meta:resourcekey="Label_Company" /></div>
                <div>
                    <asp:TextBox ID="txtCompany" runat="server" Width="170px" CssClass="InputTextReadOnly"
                        ReadOnly="true"></asp:TextBox></div>
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
                    <asp:Label ID="Label_OneTimeVendor" runat="server" meta:resourcekey="Label_OneTimeVendor" /></div>
                <div>
                    <asp:TextBox ID="txtOneTimeVendor" runat="server" CssClass="InputTextReadOnly" Width="170px"
                        ReadOnly="true"></asp:TextBox>
            </td>
            <td style="width: 200px">
                <div class="field_title">
                    <asp:Label ID="Label_HoldVendor" runat="server" meta:resourcekey="Label_HoldVendor" /></div>
                <div>
                    <asp:TextBox ID="txtHoldVendor" runat="server" CssClass="InputTextReadOnly" Width="170px"
                        ReadOnly="true"></asp:TextBox></div>
            </td>
        </tr>
        <tr>
            <td style="width: 200px">
                <div class="field_title">
                    <asp:Label ID="Label_PhoneNumber" runat="server" meta:resourcekey="Label_PhoneNumber" /></div>
                <div>
                    <asp:TextBox ID="txtPhoneNumber" runat="server" CssClass="InputTextReadOnly" Width="170px"
                        ReadOnly="true"></asp:TextBox></div>
            </td>
            <td style="width: 200px">
                <div class="field_title">
                    <asp:Label ID="Label_PurchasePhoneNumber" runat="server" meta:resourcekey="Label_PurchasePhoneNumber" /></div>
                <div>
                    <asp:TextBox ID="txtPurchasePhoneNumber" runat="server" CssClass="InputTextReadOnly"
                        Width="170px" ReadOnly="true"></asp:TextBox></div>
            </td>
            <td style="width: 200px">
                <div class="field_title">
                    <asp:Label ID="Lable_AlphaSearchKey" runat="server" meta:resourcekey="Lable_AlphaSearchKey" /></div>
                <div>
                    <asp:TextBox ID="txtAlphaSearchKey" runat="server" CssClass="InputTextReadOnly" Width="170px"
                        ReadOnly="true"></asp:TextBox></div>
            </td>
        </tr>
    </table>
    <br />
    <div class="title1">
        <asp:Label ID="Label3" runat="server" meta:resourcekey="Label_Title1" /></div>
    <table style="background-color: #F6F6F6; vertical-align: top;">
        <tr>
            <td style="width: 200px">
                <div class="field_title">
                    <asp:Label ID="Label_BankCode" runat="server" meta:resourcekey="Label_BankCode" />
                </div>
                <div>
                    <asp:TextBox ID="txtBankCode" runat="server" Width="170px" CssClass="InputTextReadOnly"
                        ReadOnly="true"></asp:TextBox>
                </div>
            </td>
            <td style="width: 200px">
                <div class="field_title">
                    <asp:Label ID="Form_PaymentType" runat="server" Text="<%$Resources:Common,Form_PaymentType %>" />
                </div>
                <div>
                    <asp:TextBox ID="txtMethodPayment" runat="server" Width="170px" CssClass="InputTextReadOnly"
                        ReadOnly="true"></asp:TextBox>
                </div>
            </td>
            <td style="width: 200px">
                <div class="field_title">
                    <asp:Label ID="Form_PaymentTerms" runat="server" Text="<%$Resources:Common,Form_PaymentTerms %>" />
                </div>
                <asp:TextBox ID="txtPaymentTerm" runat="server" Width="170px" CssClass="InputTextReadOnly"
                    ReadOnly="true"></asp:TextBox>
            </td>
            <td style="width: 200px">
                <div class="field_title">
                    <asp:Label ID="Label_TransType" runat="server" meta:resourcekey="Label_TransType" />
                </div>
                <asp:TextBox ID="txtTransType" runat="server" Width="170px" CssClass="InputTextReadOnly"
                    ReadOnly="true"></asp:TextBox>
            </td>
            <td style="width: 200px">
                <div class="field_title">
                    <asp:Label ID="Label_ACType" runat="server" meta:resourcekey="Label_ACType" />
                </div>
                <asp:TextBox ID="txtACType" runat="server" Width="170px" CssClass="InputTextReadOnly"
                    ReadOnly="true"></asp:TextBox>
            </td>
            <td style="width: 200px">
                <div class="field_title">
                    <asp:Label ID="Label_VATRate" runat="server" meta:resourcekey="Label_VATRate" />
                </div>
                <div>
                    <asp:TextBox ID="txtVATRate" runat="server" Width="170px" CssClass="InputTextReadOnly"
                        ReadOnly="true"></asp:TextBox></div>
            </td>
        </tr>
        <tr>
            <td style="width: 200px">
                <div class="field_title">
                    <asp:Label ID="Label_BankName" runat="server" meta:resourcekey="Label_BankName" />
                </div>
                <div>
                    <asp:TextBox ID="txtBankName" runat="server" Width="170px" CssClass="InputTextReadOnly"
                        ReadOnly="true"></asp:TextBox></div>
            </td>
            <td style="width: 200px">
                <div class="field_title">
                    <asp:Label ID="Label_AccountNo" runat="server" meta:resourcekey="Label_AccountNo" />
                </div>
                <div>
                    <asp:TextBox ID="txtAccountNo" runat="server" Width="170px" CssClass="InputTextReadOnly"
                        ReadOnly="true"></asp:TextBox></div>
            </td>
            <td style="width: 200px">
                <div class="field_title">
                    <asp:Label ID="Label_BankNo" runat="server" meta:resourcekey="Label_BankNo" />
                </div>
                <div>
                    <asp:TextBox ID="txtBankNo" runat="server" Width="170px" CssClass="InputTextReadOnly"
                        ReadOnly="true"></asp:TextBox></div>
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <div class="field_title">
                    <asp:Label ID="lblRemark" runat="server" Text="<%$Resources:Common,Form_Remark %>" /></div>
                <div>
                    <asp:TextBox ID="txtRemark" TextMode="MultiLine" Width="1000px" CssClass="InputTextReadOnly"
                        ReadOnly="true" Height="60px" runat="server"></asp:TextBox></div>
            </td>
        </tr>
    </table>
</asp:Content>
