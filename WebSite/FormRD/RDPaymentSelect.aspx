<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="RDPaymentSelect.aspx.cs" Culture="Auto" UICulture="Auto" Inherits="FormRD_RDPaymentSelect" %>

<%@ Register Src="~/UserControls/UCDateInput.ascx" TagName="UCDateInput" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/OUSelect.ascx" TagName="OUSelect" TagPrefix="uc2" %>
<%@ Register Src="~/UserControls/YearAndMonthUserControl.ascx" TagName="YearAndMonthUserControl"
    TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <script src="../Script/DateInput.js" type="text/javascript"></script>
    <div class="title">
        <asp:Label ID="Label_SearchCondition" runat="server" Text="<%$Resources:Common,Label_SearchCondition %>" /></div>
    <div class="searchDiv">
        <table class="searchTable" cellpadding="0" cellspacing="0" width="1240px">
            <tr style="vertical-align: top; height: 40px">
                <td>
                    <div class="field_title">
                        <asp:Label ID="Form_FormNo" runat="server" Text="<%$Resources:Common,Form_FormNo %>" /></div>
                    <asp:TextBox ID="txtFormNo" MaxLength="20" runat="server" Width="170px"></asp:TextBox>
                </td>
                <td>
                    <div class="field_title">
                        <asp:Label ID="Label_Brand" runat="server" Text="<%$Resources:Common,Form_Brand %>" /></div>
                    <asp:DropDownList ID="BrandDDL" runat="server" DataSourceID="odsBrand" DataTextField="BrandName"
                        DataValueField="BrandID" Width="200px">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="odsBrand" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                        SelectCommand=" select 0 BrandID, ' All' BrandName Union SELECT [BrandID], BrandName FROM [Brand] order by BrandName">
                    </asp:SqlDataSource>
                </td>
                <td colspan="2">
                    <div class="field_title">
                        <asp:Label ID="Form_Period" runat="server" Text="<%$Resources:Common,Form_Period %>" /></div>
                    <nobr>
                        <uc4:YearAndMonthUserControl ID="UCPeriodBegin" runat="server" IsReadOnly="false" IsExpensePeriod="true" />
                        <asp:Label ID="lblSignPeriod" runat="server">~~</asp:Label>
                        <uc4:YearAndMonthUserControl ID="UCPeriodEnd" runat="server" IsReadOnly="false" IsExpensePeriod="true" />
                    </nobr>
                </td>
                <td colspan="2">
                    <div class="field_title">
                        <asp:Label ID="Form_SubmitDate" runat="server" Text="<%$Resources:Common,Form_SubmitDate %>" /></div>
                    <nobr>
                    <uc1:UCDateInput ID="UCDateInputBeginDate" runat="server" IsReadOnly="false" />
                    <asp:Label ID="lbSign" runat="server">~~</asp:Label>
                    <uc1:UCDateInput ID="UCDateInputEndDate" runat="server" IsReadOnly="false" />
                    </nobr>
                </td>
            </tr>
        </table>
    </div>
    <table width="1200px">
        <tr>
            <td align="right" valign="middle" colspan="6">
                <asp:Button ID="btnSearch" runat="server" CssClass="button_nor" Text="<%$Resources:Common,Button_Search %>" OnClick="btnSearch_Click" />&nbsp;
            </td>
        </tr>
    </table>
    <br />
    <div class="title">
        <asp:Label ID="Label_Title" runat="server" meta:resourcekey="Label_Title" />
    </div>
    <gc:GridView CssClass="GridView" ID="gvApplyList" runat="server" DataSourceID="odsApplyList"
        AutoGenerateColumns="False" DataKeyNames="FormID" AllowPaging="True" AllowSorting="True"
        PageSize="20" OnRowDataBound="gvApplyList_RowDataBound" 
        onrowcommand="gvApplyList_RowCommand">
        <columns>
            <asp:TemplateField Visible="False">
                <ItemTemplate>
                    <asp:Label ID="lblFormApplyID" runat="server" Text='<%# Bind("FormID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField SortExpression="FormNo" HeaderText="<%$Resources:Common,Form_FormNo %>">
                <ItemTemplate>
                    <asp:LinkButton ID="lblFormNo" runat="server" CausesValidation="False" CommandName="Select"
                        Text='<%# Bind("FormNo") %>'></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle Width="110px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="CustomerChannelName" HeaderText="<%$Resources:Common,Form_CustomerChannel %>">
                <ItemTemplate>
                    <asp:Label ID="lblCustomerChannelName" runat="server" Text='<%# Eval("CustomerChannelName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="100px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="BrandName" HeaderText="<%$Resources:Common,Form_Brand %>">
                <ItemTemplate>
                    <asp:Label ID="lblBrandName" runat="server" Text='<%# Eval("BrandName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="80px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="RDProjectName" HeaderText="<%$Resources:Common,Form_ProjectName %>">
                <ItemTemplate>
                    <asp:Label ID="lblProjectName" runat="server"></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="220px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField  HeaderText="��������">
                <ItemTemplate>
                    <asp:Label ID="lblProjectDesc" runat="server"></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="260px" HorizontalAlign="Center" />
            </asp:TemplateField>

            <asp:TemplateField SortExpression="AmountRMB" HeaderText="<%$Resources:Common,Form_AmountRMB %>">
                <ItemTemplate>
                    <asp:Label ID="lblAmountRMB" runat="server" Text='<%# Bind("AmountRMB","{0:N}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="100px" HorizontalAlign="right" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="FPeriod" HeaderText="<%$Resources:Common,Form_Period %>">
                <ItemTemplate>
                    <asp:Label ID="lblPeriod" Text='<%# Bind("FPeriod", "{0:yyyyMM}") %>' runat="server"></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="80px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="CurrencyID" HeaderText="<%$Resources:Common,Form_Currency %>">
                <ItemTemplate>
                    <asp:Label ID="lblCurrency" runat="server" Text='<%# Bind("CurrencyShortName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="60px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Common,Form_ApplyUser %>">
                <ItemTemplate>
                    <asp:Label ID="lblStuffName" runat="server" Text='<%# GetStaffNameByID(Eval("UserID")) %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="60px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="SubmitDate" HeaderText="<%$Resources:Common,Form_SubmitDate %>">
                <ItemTemplate>
                    <asp:Label ID="lblSubmitDate" runat="server" Text='<%# Bind("SubmitDate", "{0:yyyy-MM-dd}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="80px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Common,Label_Operate %>">
                <ItemTemplate>
                    <asp:LinkButton ID="lbPaymentApply" runat="server" CausesValidation="False" CommandArgument='<%#Eval("FormID")+","+Eval("AmountRMB")%>' CommandName="Apply"
                        meta:resourcekey="Label_PaymentApply"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle Width="80px" HorizontalAlign="Center" />
            </asp:TemplateField>
        </columns>
        <headerstyle cssclass="Header" />
        <emptydatatemplate>
            <table>
                <tr class="Header">
                    <td style="width: 130px;" class="Empty1">
                        <asp:Label  runat="server" Text="<%$Resources:Common,Form_FormNo %>" />
                    </td>
                    <td style="width: 180px;">
                        <asp:Label ID="Label1"  runat="server" Text="<%$Resources:Common,Form_Customer %>" />
                    </td>
                    <td style="width: 100px;">
                        <asp:Label ID="Label2"  runat="server" Text="<%$Resources:Common,Form_CustomerChannel %>" />
                    </td>
                    <td style="width: 280px;">
                        <asp:Label ID="Label3"  runat="server" Text="<%$Resources:Common,Form_ProjectName %>" />
                    </td>
                    <td style="width: 100px;">
                        <asp:Label ID="Label4"  runat="server" Text="<%$Resources:Common,Form_AmountRMB %>" />
                    </td>
                    <td style="width: 80px;">
                        <asp:Label ID="Label5"  runat="server" Text="<%$Resources:Common,Form_Period %>" />
                    </td>
                    <td style="width: 60px;">
                        <asp:Label ID="Label7"  runat="server" Text="<%$Resources:Common,Form_Currency %>" />
                    </td>
                    <td style="width: 60px;">
                        <asp:Label ID="Label8"  runat="server" Text="<%$Resources:Common,Form_ApplyUser %>" />
                    </td>
                    <td style="width: 100px;">
                        <asp:Label ID="Label9"  runat="server" Text="<%$Resources:Common,Form_SubmitDate %>" />
                    </td>
                    <td style="width: 80px;">
                    </td>
                </tr>
                <tr>
                    <td colspan="10" class="Empty2 noneLabel">
                        <asp:Label ID="Label6"  runat="server" Text="<%$Resources:Common,Label_None %>" />
                    </td>
                </tr>
            </table>
        </emptydatatemplate>
        <selectedrowstyle cssclass="SelectedRow" />
    </gc:GridView>
    <asp:ObjectDataSource ID="odsApplyList" runat="server" TypeName="BusinessObjects.FormQueryBLL"
        SelectMethod="GetPagedFormRDApplyView" EnablePaging="True" SelectCountMethod="QueryFormRDApplyViewCount"
        SortParameterName="sortExpression">
        <SelectParameters>
            <asp:Parameter Name="queryExpression" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
