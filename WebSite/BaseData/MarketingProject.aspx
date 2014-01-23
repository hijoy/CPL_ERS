<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="MarketingProject.aspx.cs" Culture="auto" UICulture="auto" Inherits="BaseData_MarketingProject" %>

<%@ Register Src="../UserControls/UCDateInput.ascx" TagName="UCDateInput" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <script src="../Script/DateInput.js" type="text/javascript"></script>
    <div class="title">
        <asp:Label ID="Label_SearchCondition" runat="server" Text="<%$Resources:Common,Label_SearchCondition %>" /></div>
    <div class="searchDiv">
        <table class="searchTable" cellpadding="0" cellspacing="0">
            <tr style="vertical-align: top; height: 40px">
                <td style="width: 200px;">
                    <div class="field_title">
                        <asp:Label runat="server" Text="<%$Resources:Common,Form_ProjectName %>"></asp:Label></div>
                    <asp:TextBox ID="txtProjectName" MaxLength="20" runat="server" Width="170px"></asp:TextBox>
                </td>
                <td valign="bottom" colspan="4">
                    <asp:Button ID="btnSearch" runat="server" CssClass="button_nor" Text="<%$Resources:Common,Button_Search %>"
                        OnClick="btnSearch_Click" />&nbsp;
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div class="title">
        <asp:Label ID="Label5" runat="server" meta:resourcekey="Label_Title"></asp:Label></div>
    <asp:UpdatePanel ID="MarketingProjectUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <gc:GridView ID="MarketingProjectGridView" CssClass="GridView" runat="server" DataSourceID="MarketingProjectObjectDataSource"
                AllowPaging="true" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="MarketingProjectID"
                PageSize="15" CellPadding="0">
                <Columns>
                    <asp:TemplateField HeaderText="<%$Resources:Common,Form_ProjectName %>" SortExpression="MarketingProjectName">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtMarketingProjectName" runat="server" CssClass="InputText" MaxLength="20"
                                Text='<%# Bind("MarketingProjectName") %>' Width="340px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RFMarketingProjectName" runat="server" ControlToValidate="txtMarketingProjectName"
                                Display="None" meta:resourcekey="RequiredFieldValidator_ProjectName" SetFocusOnError="True"
                                ValidationGroup="marketingProjectEdit"></asp:RequiredFieldValidator>
                        </EditItemTemplate>
                        <ItemStyle Width="665px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblMarketingProjectName" runat="server" Text='<%# Bind("MarketingProjectName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateField_BeginDate" SortExpression="BeginDate">
                        <EditItemTemplate>
                            <uc1:UCDateInput ID="UCBeginDate" runat="server" SelectedDate='<%#Bind("BeginDate","{0:yyyy-MM-dd}")%>' />
                            <asp:RequiredFieldValidator ID="RFBeginDate" runat="server" ControlToValidate="UCBeginDate$txtDate"
                                Display="None" meta:resourcekey="RequiredFieldValidator_BeginDate" SetFocusOnError="True"
                                ValidationGroup="marketingProjectEdit"></asp:RequiredFieldValidator>
                        </EditItemTemplate>
                        <ItemStyle Width="200px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblBeginDate" runat="server" Text='<%#Eval("BeginDate","{0:yyyy-MM-dd}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateField_EndDate" SortExpression="EndDate">
                        <EditItemTemplate>
                            <uc1:UCDateInput ID="UCEndDate" runat="server" SelectedDate='<%#Bind("EndDate","{0:yyyy-MM-dd}")%>' />
                            <asp:RequiredFieldValidator ID="RFEndDate" runat="server" ControlToValidate="UCEndDate$txtDate"
                                Display="None" meta:resourcekey="RequiredFieldValidator_EndDate" SetFocusOnError="True"
                                ValidationGroup="marketingProjectEdit"></asp:RequiredFieldValidator>
                        </EditItemTemplate>
                        <ItemStyle Width="200px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblEndDate" runat="server" Text='<%# Bind("EndDate","{0:yyyy-MM-dd}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="IsActive" HeaderText="<%$Resources:Common,Form_IsActive %>">
                        <EditItemTemplate>
                            <asp:CheckBox runat="server" ID="chkActiveByEdit" Checked='<%# Bind("IsActive") %>' />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="chkActiveByEdit1" Enabled="false" Checked='<%# Bind("IsActive") %>' />
                        </ItemTemplate>
                        <ItemStyle Width="70px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <EditItemTemplate>
                            <asp:ValidationSummary ID="ValidationSummaryEdit" runat="server" ShowMessageBox="True"
                                ShowSummary="False" ValidationGroup="marketingProjectEdit" />
                            <asp:LinkButton ID="LinkButton1" Visible="<%# HasManageRight %>" runat="server" CausesValidation="True"
                                ValidationGroup="marketingProjectEdit" CommandName="Update" Text='<%$Resources:Common,Button_Update %>'></asp:LinkButton>
                            <asp:LinkButton ID="LinkButton3" Visible="<%# HasManageRight %>" runat="server" CausesValidation="false"
                                CommandName="Cancel" Text='<%$Resources:Common,Button_Cancel %>'></asp:LinkButton>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton2" Visible="<%# HasManageRight %>" runat="server" CausesValidation="false"
                                CommandName="Edit" Text='<%$Resources:Common,Button_Edit %>'></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td scope="col" style="width: 665px" class="Empty1">
                                <asp:Label runat="server" Text="<%$Resources:Common,Form_ProjectName %>"></asp:Label>
                            </td>
                            <td scope="col" style="width: 200px">
                                <asp:Label runat="server" meta:resourcekey="Label_BeginDate"></asp:Label>
                            </td>
                            <td scope="col" style="width: 200px">
                                <asp:Label runat="server" meta:resourcekey="Label_EndDate"></asp:Label>
                            </td>
                            <td scope="col" style="width: 70px">
                                <asp:Label runat="server" Text="<%$Resources:Common,Form_IsActive %>"></asp:Label>
                            </td>
                            <td scope="col" style="width: 100px">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="8" class="Empty2 noneLabel">
                                <asp:Label ID="Label_None" runat="server" Text="<%$Resources:Common,Label_None %>" />
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
            </gc:GridView>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="MarketingProjectFormView" EventName="ItemInserted" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="MarketingProjectUpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:FormView ID="MarketingProjectFormView" runat="server" DataKeyNames="MarketingProjectID"
                CellPadding="0" CellSpacing="0" DataSourceID="MarketingProjectObjectDataSource"
                DefaultMode="Insert" Visible="<%# HasManageRight %>">
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr>
                            <td align="center" style="width: 665px">
                                <asp:TextBox ID="txtMarketingProjectNameByAdd" runat="server" Text='<%# Bind("MarketingProjectName") %>'
                                    CssClass="InputText" Width="340px" ValidationGroup="INS"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RFMarketingProjectName" runat="server" ControlToValidate="txtMarketingProjectNameByAdd"
                                    Display="None" meta:resourcekey="RequiredFieldValidator_ProjectName" SetFocusOnError="True"
                                    ValidationGroup="marketingProjectIns"></asp:RequiredFieldValidator>
                            </td>
                            <td align="center" style="width: 200px">
                                <uc1:UCDateInput ID="UCBeginDateByAdd" runat="server" SelectedDate='<%#Bind("BeginDate","{0:yyyy-MM-dd}")%>' />
                                <asp:RequiredFieldValidator ID="RFBeginDate" runat="server" ControlToValidate="UCBeginDateByAdd$txtDate"
                                    Display="None" meta:resourcekey="RequiredFieldValidator_BeginDate" SetFocusOnError="True"
                                    ValidationGroup="marketingProjectIns"></asp:RequiredFieldValidator>
                            </td>
                            <td align="center" style="width: 200px">
                                <uc1:UCDateInput ID="UCEndDateByAdd" runat="server" SelectedDate='<%#Bind("EndDate","{0:yyyy-MM-dd}")%>' />
                                <asp:RequiredFieldValidator ID="RFEndDate" runat="server" ControlToValidate="UCEndDateByAdd$txtDate"
                                    Display="None" meta:resourcekey="RequiredFieldValidator_EndDate" SetFocusOnError="True"
                                    ValidationGroup="marketingProjectIns"></asp:RequiredFieldValidator>
                            </td>
                            <td align="center" style="height: 22px; width: 70px;">
                                <asp:CheckBox runat="server" ID="chkActiveByAdd" Checked='<%# Bind("IsActive") %>' />
                            </td>
                            <td align="center" style="width: 100px">
                                <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    Text='<%$Resources:Common,Button_Add %>' ValidationGroup="marketingProjectIns"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                    <asp:ValidationSummary ID="ValidationSummaryInsert" runat="server" ShowMessageBox="True"
                        ShowSummary="False" ValidationGroup="marketingProjectIns" />
                </InsertItemTemplate>
            </asp:FormView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="MarketingProjectObjectDataSource" EnablePaging="true" SortParameterName="sortExpression"
        runat="server" InsertMethod="InsertMarketingProject" SelectMethod="GetPagedMarketingProject"
        SelectCountMethod="MarketingProjectTotalCount" TypeName="BusinessObjects.MasterDataBLL"
        UpdateMethod="UpdateMarketingProject">
        <SelectParameters>
            <asp:Parameter Name="queryExpression" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
