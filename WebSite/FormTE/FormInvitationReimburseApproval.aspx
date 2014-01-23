<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="FormInvitationReimburseApproval.aspx.cs" Inherits="FormTE_FormInvitationApproval"
    Culture="Auto" UICulture="Auto" %>

<%@ Register Src="../UserControls/APFlowNodes.ascx" TagName="APFlowNodes" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/ucUpdateProgress.ascx" TagName="ucUpdateProgress"
    TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="title">
        <asp:Label ID="Label_BasicInfo" Text="<%$Resources:Common,Label_BasicInfo %>" runat="server" /></div>
    <div style="width: 1240px; background-color: #F6F6F6">
        <table>
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_FormNo" runat="server" Text="<%$Resources:Common,Form_FormNo %>" /></div>
                    <div>
                        <asp:TextBox ID="txtFormNo" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label Text='<%$Resources:Common,Form_ApplyUser %>' runat="server" /></div>
                    <div>
                        <asp:TextBox ID="StuffNameCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label runat="server" Text='<%$Resources:Common,Form_Position %>' /></div>
                    <div>
                        <asp:TextBox ID="PositionNameCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px" align="left">
                    <div class="field_title">
                        <asp:Label runat="server" Text='<%$Resources:Common,Form_Organization %>' /></div>
                    <div>
                        <asp:TextBox ID="DepartmentNameCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px" align="left">
                    <div class="field_title">
                        <asp:Label ID="Label_StuffNo" runat="server" Text="<%$Resources:Common,Form_StaffNo %>" /></div>
                    <div>
                        <asp:TextBox ID="txtStuffID" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px" align="left">
                    <div class="field_title">
                        <asp:Label runat="server" Text='<%$Resources:Common,Form_AttendDate %>' /></div>
                    <div>
                        <asp:TextBox ID="AttendDateCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="lblPeriod" runat="server" Text='<%$Resources:Common,Form_Period %>' /></div>
                    <div>
                        <asp:TextBox ID="txtPeriod" runat="server" ReadOnly="true" Width="170px"></asp:TextBox>
                    </div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_Customer" runat="server" Text="<%$Resources:Common,Form_Customer %>" /></div>
                    <div>
                        <asp:TextBox ID="txtCustomerName" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label11" runat="server" Text='<%$Resources:Common,Form_ExpenseItem %>' /></div>
                    <div>
                        <asp:TextBox ID="ExpenseItemCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px" valign="top">
                    <div class="field_title">
                        <asp:Label ID="Label12" runat="server" meta:resourcekey="InvitationInfo_Label_ApplyNo" /></div>
                    <div style="margin-top: 5px">
                        <asp:HyperLink ID="ApplyFormNoCtl" runat="server"></asp:HyperLink></div>
                </td>

                <td valign="top">
                    <div class="field_title">
                        <asp:Label ID="Form_RejectFormNo" runat="server" Text="<%$Resources:Common,Form_RejectFormNo %>" /></div>
                    <div style="margin-top: 5px">
                        <asp:HyperLink ID="lblRejectFormNo" runat="server"></asp:HyperLink></div>
                </td>
            </tr>
            <tr>
                <td align="left" colspan="6">
                    <div class="field_title">
                        <asp:Label ID="Label5" runat="server" Text='<%$Resources:Common,Form_Remark %>' /></div>
                    <div>
                        <asp:TextBox ID="RemarkCtl" runat="server" CssClass="InputText" TextMode="multiline"
                            Height="60px" Columns="140" ReadOnly="true"></asp:TextBox></div>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div runat="server" id="budgetTitleDIV" class="title">
        <asp:Label ID="Label13" runat="server" Text='<%$Resources:Common,Form_BudgetTitle %>' />
    </div>
    <div runat="server" id="budgetDIV"  class="searchDiv">
        <table class="searchTable">
            <tr>
                <td style="width: 200px" align="left">
                    <div class="field_title">
                        <asp:Label ID="Label7" runat="server" meta:resourcekey="InvitationInfo_Label_Label7" /></div>
                    <div>
                        <asp:TextBox ID="txtTotalBudget" runat="server" Width="180px" ReadOnly="true"></asp:TextBox></div>
                </td>
                <td style="width: 200px" align="left">
                    <div class="field_title">
                        <asp:Label ID="Label8" runat="server" meta:resourcekey="InvitationInfo_Label_Label8" /></div>
                    <div>
                        <asp:TextBox ID="txtApprovingAmount" runat="server" Width="180px" ReadOnly="true"></asp:TextBox></div>
                </td>
                <td style="width: 200px" align="left">
                    <div class="field_title">
                        <asp:Label ID="Label3" runat="server" meta:resourcekey="InvitationInfo_Label_Label9" /></div>
                    <div>
                        <asp:TextBox ID="txtApprovedAmount" runat="server" Width="180px" ReadOnly="true"></asp:TextBox></div>
                </td>
                <td style="width: 200px" align="left">
                    <div class="field_title">
                        <asp:Label ID="Label10" runat="server" meta:resourcekey="InvitationInfo_Label_Label10" /></div>
                    <div>
                        <asp:TextBox ID="txtRemainAmount" runat="server" Width="180px" ReadOnly="true"></asp:TextBox></div>
                </td>
            </tr>
        </table>
    </div>

    <br />
    <div class="title">
        <asp:Label runat="server" meta:resourcekey="Title_Label1" /></div>
    <div class="searchDiv">
        <table class="searchTable">
            <tr>
                <td colspan="2" style="width: 400px">
                    <div class="field_title">
                        <asp:Label runat="server" meta:resourcekey="InvitationInfo_Label_AttenderNames" /></div>
                    <div>
                        <asp:TextBox ID="txtAttenderNames" runat="server" Width="370px" ReadOnly="true"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label runat="server" meta:resourcekey="InvitationInfo_Label_BusinessRelation" /></div>
                    <div>
                        <asp:TextBox ID="txtBusinessRelation" runat="server" Width="170px" ReadOnly="true"></asp:TextBox>
                    </div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label runat="server" meta:resourcekey="InvitationInfo_Label_InvitationType" /></div>
                    <div>
                        <asp:TextBox ID="txtInvitationType" runat="server" Width="170px" ReadOnly="true"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label runat="server" meta:resourcekey="InvitationInfo_Label_AttenderCount" /></div>
                    <div>
                        <asp:TextBox ID="txtAttenderCount" runat="server" Width="170px" ReadOnly="true"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label runat="server" meta:resourcekey="InvitationInfo_Label_BusinessPurpose" /></div>
                    <div>
                        <asp:TextBox ID="txtBusinessPurpose" runat="server" Width="170px" ReadOnly="true"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label2" runat="server" meta:resourcekey="InvitationInfo_Label_OccuredDate" /></div>
                    <div>
                        <asp:TextBox ID="txtOccuredDate" runat="server" Width="170px" ReadOnly="true"></asp:TextBox>
                    </div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label runat="server" meta:resourcekey="InvitationInfo_Label_Place" /></div>
                    <div>
                        <asp:TextBox ID="txtPlace" runat="server" Width="170px" ReadOnly="true"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Form_Currency" runat="server" Text="<%$Resources:Common,Form_Currency %>" /></div>
                    <div>
                        <asp:TextBox ID="txtCurrency" runat="server" Width="170px" ReadOnly="true"></asp:TextBox>
                    </div>
                </td>
                <td style="width: 200px" align="left">
                    <div class="field_title">
                        <asp:Label ID="Form_ExchangeRate" runat="server" Text="<%$Resources:Common,Form_ExchangeRate %>" /></div>
                    <div>
                        <asp:TextBox ID="txtExchageRate" runat="server" Width="170px" ReadOnly="true"></asp:TextBox></div>
                </td>
                <td style="width: 200px" align="left">
                    <div class="field_title">
                        <asp:Label runat="server" meta:resourcekey="InvitationInfo_Label_Amount" /></div>
                    <div>
                        <asp:TextBox ID="txtAmount" runat="server" Width="170px" ReadOnly="true"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label runat="server" meta:resourcekey="InvitationInfo_Label_AmountRMB" /></div>
                    <div>
                        <asp:TextBox ID="txtAmountRMB" Text="0" runat="server" Width="170px" ReadOnly="true"></asp:TextBox>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <uc1:APFlowNodes ID="cwfAppCheck" runat="server"/>
    <asp:UpdatePanel ID="upButton" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div style="margin-top: 20px; width: 1200px; text-align: right">
                <asp:Button ID="SubmitBtn" runat="server" OnClick="SubmitBtn_Click" Text='<%$Resources:Common,Button_Approve %>'
                    CssClass="button_nor" />&nbsp;
                <asp:Button ID="CancelBtn" runat="server" OnClick="CancelBtn_Click" Text='<%$Resources:Common,Button_Back %>'
                    CssClass="button_nor" />&nbsp;
                <asp:Button ID="EditBtn" runat="server" OnClick="EditBtn_Click" Text='<%$Resources:Common,Button_Edit %>'
                    CssClass="button_nor" />&nbsp;
                <asp:Button ID="ScrapBtn" runat="server" OnClick="ScrapBtn_Click" Text='<%$Resources:Common,Button_Scrap %>'
                    CssClass="button_nor" />
            </div>
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
    <uc3:ucUpdateProgress ID="upUP" runat="server" vassociatedupdatepanelid="upCustomer" />
</asp:Content>
