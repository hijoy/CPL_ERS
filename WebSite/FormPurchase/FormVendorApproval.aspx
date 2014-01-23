<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="FormVendorApproval.aspx.cs" UICulture="Auto" Culture="Auto" Inherits="FormPurchase_FormVendorApproval" %>

<%@ Register Src="../UserControls/APFlowNodes.ascx" TagName="APFlowNodes" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/VendorTypeControl.ascx" TagName="VendorTypeControl"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControls/ucUpdateProgress.ascx" TagName="ucUpdateProgress"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControls/MultiAttachmentFile.ascx" TagName="UCFlie" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="title">
        <asp:Label ID="Label_BasicInfo" Text="<%$Resources:Common,Label_BasicInfo %>" runat="server" /></div>
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
                        <asp:TextBox ID="StuffNameCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_Position" runat="server" Text="<%$Resources:Common,Form_Position %>" /></div>
                    <div>
                        <asp:TextBox ID="PositionNameCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_Organization" runat="server" Text="<%$Resources:Common,Form_Organization %>" /></div>
                    <div>
                        <asp:TextBox ID="DepartmentNameCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label_StuffNo" runat="server" Text="<%$Resources:Common,Form_StaffNo %>" /></div>
                    <div>
                        <asp:TextBox ID="StuffNoCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_AttendDate" runat="server" Text="<%$Resources:Common,Form_AttendDate %>" /></div>
                    <div>
                        <asp:TextBox ID="AttendDateCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label_ActionType" runat="server" meta:resourcekey="Label_ActionType" /></div>
                    <div>
                        <asp:TextBox ID="txtActionType" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px;">
                    <div class="field_title">
                        <asp:Label ID="Form_RejectFormNo" runat="server" Text="<%$Resources:Common,Form_RejectFormNo %>" /></div>
                    <div>
                        <asp:HyperLink ID="lblRejectFormNo" runat="server"></asp:HyperLink></div>
                    <br />
                </td>
                <td colspan="4">
                    <div class="field_title">
                        <asp:Label ID="Label_Vendor" runat="server" Text="<%$Resources:Common,Label_Vendor %>" /></div>
                    <div>
                        <asp:HyperLink ID="hlVendor" runat="server"></asp:HyperLink></div>
                    <br />
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div class="title">
        <asp:Label ID="Label2" runat="server" meta:resourcekey="Label_Title" /></div>
    <div class="searchDiv">
        <table class="searchTable">
            <tr>
                <td colspan="2" style="width: 400px">
                    <div class="field_title">
                        <asp:Label ID="Form_VendorName" runat="server" Text="<%$Resources:Common,Form_VendorName %>" /></div>
                    <div>
                        <asp:TextBox ID="txtVendorName" runat="server" Width="370px" ReadOnly="true"></asp:TextBox></div>
                </td>
                <td colspan="2" style="width: 400px">
                    <div class="field_title">
                        <asp:Label ID="Label_VendorAddress" runat="server" meta:resourcekey="Label_VendorAddress" />
                    </div>
                    <div>
                        <asp:TextBox ID="txtVendorAddress" runat="server" Width="370px" ReadOnly="true"></asp:TextBox></div>
                </td>
                <td colspan="2" style="width: 400px">
                    <div class="field_title">
                        <asp:Label ID="Label_PurchasingAddress" runat="server" meta:resourcekey="Label_PurchasingAddress" /></div>
                    <div>
                        <asp:TextBox ID="txtPurchasingAddress" runat="server" Width="370px" ReadOnly="true"></asp:TextBox></div>
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_City" runat="server" Text="<%$Resources:Common,Form_City %>" />
                    </div>
                    <div>
                        <asp:TextBox ID="txtCity" runat="server" Width="170px" ReadOnly="true"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label_PurchasingCity" runat="server" meta:resourcekey="Label_PurchasingCity" /></div>
                    <div>
                        <asp:TextBox ID="txtPurchasingCity" runat="server" Width="170px" ReadOnly="true"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label_Postal" runat="server" meta:resourcekey="Label_Postal" />
                    </div>
                    <div>
                        <asp:TextBox ID="txtPostal" runat="server" Width="170px" ReadOnly="true"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Lable_PurchaseingPostalCode" runat="server" meta:resourcekey="Lable_PurchaseingPostalCode" /></div>
                    <div>
                        <asp:TextBox ID="txtPurchaseingPostalCode" runat="server" Width="170px" ReadOnly="true"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label_ContactName" runat="server" meta:resourcekey="Label_ContactName" />
                    </div>
                    <div>
                        <asp:TextBox ID="txtContactName" runat="server" Width="170px" ReadOnly="true"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Lable_PurchasingContact" runat="server" meta:resourcekey="Lable_PurchasingContact" /></div>
                    <div>
                        <asp:TextBox ID="txtPurchasingContact" runat="server" Width="170px" ReadOnly="true"></asp:TextBox></div>
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label_VendorType" runat="server" meta:resourcekey="Label_VendorType" />
                    </div>
                    <div>
                        <uc1:VendorTypeControl ID="VendorTypeControl" runat="server" Width="120px" AutoPostBack="true"
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
                        <asp:Label ID="Label_OneTimeVendor" runat="server" meta:resourcekey="Label_OneTimeVendor" /></div>
                    <div>
                        <asp:TextBox ID="txtOneTimeVendor" runat="server" Width="170px" ReadOnly="true"></asp:TextBox>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label_HoldVendor" runat="server" meta:resourcekey="Label_HoldVendor" /></div>
                    <div>
                        <asp:TextBox ID="txtHoldVendor" runat="server" Width="170px" ReadOnly="true"></asp:TextBox></div>
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label_PhoneNumber" runat="server" meta:resourcekey="Label_PhoneNumber" /></div>
                    <div>
                        <asp:TextBox ID="txtPhoneNumber" runat="server" Width="170px" ReadOnly="true"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label_PurchasePhoneNumber" runat="server" meta:resourcekey="Label_PurchasePhoneNumber" /></div>
                    <div>
                        <asp:TextBox ID="txtPurchasePhoneNumber" runat="server" Width="170px" ReadOnly="true"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Lable_AlphaSearchKey" runat="server" meta:resourcekey="Lable_AlphaSearchKey" /></div>
                    <div>
                        <asp:TextBox ID="txtAlphaSearchKey" runat="server" Width="170px" ReadOnly="true"></asp:TextBox></div>
                </td>
            </tr>
            <tr>
                <td colspan="3" valign="top">
                    <div class="field_title">
                        <asp:Label ID="Form_Adjunct" runat="server" Text="<%$Resources:Common,Form_Adjunct %>" /></div>
                    <uc2:UCFlie ID="UCFileUpload" runat="server" IsView="true" Width="400px" />
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
                        <asp:Label ID="Label_BankCode" runat="server" meta:resourcekey="Label_BankCode" />
                    </div>
                    <div>
                        <asp:TextBox ID="txtBankCode" runat="server" Width="170px" ReadOnly="true"></asp:TextBox>
                    </div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_PaymentType" runat="server" Text="<%$Resources:Common,Form_PaymentType %>" />
                    </div>
                    <div>
                        <asp:TextBox ID="txtMethodPayment" runat="server" Width="170px" ReadOnly="true"></asp:TextBox>
                    </div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_PaymentTerms" runat="server" Text="<%$Resources:Common,Form_PaymentTerms %>" />
                    </div>
                    <asp:TextBox ID="txtPaymentTerm" runat="server" Width="170px" ReadOnly="true"></asp:TextBox>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label_TransType" runat="server" meta:resourcekey="Label_TransType" />
                    </div>
                    <asp:TextBox ID="txtTransType" runat="server" Width="170px" ReadOnly="true"></asp:TextBox>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label_ACType" runat="server" meta:resourcekey="Label_ACType" />
                    </div>
                    <asp:TextBox ID="txtACType" runat="server" Width="170px" ReadOnly="true"></asp:TextBox>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label_VATRate" runat="server" meta:resourcekey="Label_VATRate" />
                    </div>
                    <div>
                        <asp:TextBox ID="txtVATRate" runat="server" Width="170px" ReadOnly="true"></asp:TextBox></div>
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label_BankName" runat="server" meta:resourcekey="Label_BankName" />
                    </div>
                    <div>
                        <asp:TextBox ID="txtBankName" runat="server" Width="170px" ReadOnly="true"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label_AccountNo" runat="server" meta:resourcekey="Label_AccountNo" />
                    </div>
                    <div>
                        <asp:TextBox ID="txtAccountNo" runat="server" Width="170px" ReadOnly="true"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label_BankNo" runat="server" meta:resourcekey="Label_BankNo" />
                    </div>
                    <div>
                        <asp:TextBox ID="txtBankNo" runat="server" Width="170px" ReadOnly="true"></asp:TextBox></div>
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <div class="field_title">
                        <asp:Label ID="lblRemark" runat="server" Text="<%$Resources:Common,Form_Remark %>" /></div>
                    <div>
                        <asp:TextBox ID="txtRemark" TextMode="MultiLine" Width="1000px" ReadOnly="true" Height="60px"
                            runat="server"></asp:TextBox></div>
                </td>
            </tr>
            <tr id="trModifyReason" runat="server" visible="false">
                <td colspan="5">
                    <div class="field_title">
                        <asp:Label ID="Label_ModifyReason" runat="server" meta:resourcekey="Label_ModifyReason" /></div>
                    <div>
                        <asp:TextBox ID="txtModifyReason" TextMode="MultiLine" Width="1000px" Height="60px"
                            runat="server" ReadOnly="true"></asp:TextBox></div>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <uc1:APFlowNodes ID="cwfAppCheck" runat="server" />
    <asp:UpdatePanel ID="upButton" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div style="margin-top: 20px; width: 1200px; text-align: right">
                <asp:Button ID="SubmitBtn" runat="server" OnClick="SubmitBtn_Click" Text="<%$Resources:Common,Button_Approve %>"
                    CssClass="button_nor" />&nbsp;
                <asp:Button ID="btnSave" runat="server" meta:resourcekey="Button_SaveVendorType"
                    CssClass=" button_big" OnClick="btnSave_Click" />&nbsp;
                <asp:Button ID="CancelBtn" runat="server" OnClick="CancelBtn_Click" Text="<%$Resources:Common,Button_Back %>"
                    CssClass="button_nor" />&nbsp;
                <asp:Button ID="EditBtn" runat="server" OnClick="EditBtn_Click" Text="<%$Resources:Common,Button_Edit %>"
                    CssClass="button_nor" />&nbsp;
                <asp:Button ID="ScrapBtn" runat="server" OnClick="ScrapBtn_Click" Text="<%$Resources:Common,Button_Scrap %>"
                    CssClass="button_nor" />&nbsp;
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <uc1:ucUpdateProgress ID="ucUP" runat="server" vassociatedupdatepanelid="upButton" />
</asp:Content>
