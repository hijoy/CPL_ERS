<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ExpenseItem.aspx.cs" Inherits="BaseData_ExpenseSubCategory" Culture="Auto"
    UICulture="Auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <script src="../Script/DateInput.js" type="text/javascript"></script>
    <div class="title">
        <asp:Label ID="Label7" runat="server" Text='<%$Resources:Common,Label_SearchCondition %>'></asp:Label></div>
    <div class="searchDiv">
        <table class="searchTable" cellpadding="0" cellspacing="0">
            <tr style="vertical-align: top; height: 40px">
                <td style="width: 200px;">
                    <div class="field_title">
                        <asp:Label runat="server" meta:resourcekey="Label_AccountingCode"></asp:Label></div>
                    <asp:TextBox ID="txtAccountingCode" runat="server" Width="160px"></asp:TextBox>
                </td>
                <td style="width: 200px;">
                    <div class="field_title">
                        <asp:Label runat="server" Text="<%$Resources:Common,Form_ExpenseItem %>"></asp:Label></div>
                    <asp:TextBox ID="txtExpenseItemName" runat="server" Width="160px"></asp:TextBox>
                </td>
                <td style="width: 200px;">
                    <div class="field_title">
                        <asp:Label ID="Label8" runat="server" Text="<%$Resources:Common,Form_ExpenseSubCategory %>"></asp:Label></div>
                    <asp:DropDownList runat="server" ID="ddlExpenseSubCategory" Width="170px" 
                        DataTextField="ExpenseSubCategoryName" DataValueField="ExpenseSubCategoryID"
                        DataSourceID="odsExpenseSubCategory">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="odsExpenseSubCategory" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                        SelectCommand="select 0 ExpenseSubCategoryID,' ALL' ExpenseSubCategoryName Union SELECT [ExpenseSubCategoryID], [ExpenseSubCategoryName] FROM [ExpenseSubCategory] where IsActive = 1">
                    </asp:SqlDataSource>
                    
                </td>
                <td style="width: 200px;">
                    <div class="field_title">
                        <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Common,Form_IsActive %>"></asp:Label></div>
                    <asp:DropDownList ID="ddlActive" runat="server" Width="160">
                        <asp:ListItem Selected="True" Text="ALL" Value=""></asp:ListItem>
                        <asp:ListItem meta:resourcekey="ListItem_Active" Value="1"></asp:ListItem>
                        <asp:ListItem meta:resourcekey="ListItem_NoActive" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </td>

                <td style="width: 200px;">
                    &nbsp;
                </td>
                <td colspan="2" align="left" valign="bottom">
                    <input type="hidden" id="btnclicked" name="btnclicked" value="0" />
                    <asp:Button ID="SearchButton" runat="server" CssClass="button_nor" Text='<%$Resources:Common,Button_Search %>'
                        OnClick="SearchButton_Click" />&nbsp;&nbsp;
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div class="title">
        <asp:Label runat="server" meta:resourcekey="Label_Title"></asp:Label>
    </div>
    <asp:UpdatePanel ID="upExpenseItem" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <gc:GridView ID="gvExpenseItem" CssClass="GridView" runat="server" DataSourceID="odsExpenseItem"
                CellPadding="0" AutoGenerateColumns="False" DataKeyNames="ExpenseItemID" AllowPaging="True"
                AllowSorting="True" PageSize="20" EnableModelValidation="True">
                <Columns>
                    <asp:TemplateField SortExpression="AccountingCode" HeaderText="P&L会计科目">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtAccountingCode" runat="server" Text='<%# Bind("AccountingCode") %>'
                                Width="130px" CssClass="InputText" MaxLength="50"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblAccountingCode" Text='<%# Eval("AccountingCode")%>' />
                        </ItemTemplate>
                        <ItemStyle Width="140px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="AccrualAccountingCode" HeaderText="Accrual会计科目">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtAccrualAccountingCode" runat="server" Text='<%# Bind("AccrualAccountingCode") %>'
                                Width="130px" CssClass="InputText" MaxLength="50"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblAccrualAccountingCode" Text='<%# Eval("AccrualAccountingCode")%>' />
                        </ItemTemplate>
                        <ItemStyle Width="140px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="LastAccountingCode" HeaderText="Accrual去年科目" >
                        <EditItemTemplate>
                            <asp:TextBox ID="txtLastAccountingCode" runat="server" Text='<%# Bind("LastAccountingCode") %>'
                                Width="130px" CssClass="InputText" MaxLength="50"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblLastAccountingCode" Text='<%# Eval("LastAccountingCode")%>' />
                        </ItemTemplate>
                        <ItemStyle Width="140px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="ExpenseItemName" HeaderText="<%$Resources:Common,Form_ExpenseItem %>">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtExpenseItemName" runat="server" Text='<%# Bind("ExpenseItemName") %>'
                                Width="230px" CssClass="InputText" MaxLength="50"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblExpenseItemName" Text='<%# Eval("ExpenseItemName")%>' />
                        </ItemTemplate>
                        <ItemStyle Width="250px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="ExpenseSubCategoryID" HeaderText="<%$Resources:Common,Form_ExpenseSubCategory %>">
                        <EditItemTemplate>
                            <asp:DropDownList runat="server" ID="ddlExpenseSubCategory" Width="180px" SelectedValue='<%# Bind("ExpenseSubCategoryID")%>'
                                DataTextField="ExpenseSubCategoryName" DataValueField="ExpenseSubCategoryID"
                                DataSourceID="sdsExpenseSubCategory">
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="sdsExpenseSubCategory" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                SelectCommand="SELECT [ExpenseSubCategoryID], [ExpenseSubCategoryName] FROM [ExpenseSubCategory] where IsActive = 1">
                            </asp:SqlDataSource>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblExpenseSubCategoryName" Text='<%# getExpenseSubCategoryName(Eval("ExpenseSubCategoryID"))%>' />
                        </ItemTemplate>
                        <ItemStyle Width="200px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="NeedShopInfo" meta:resourcekey="TemplateField_NeedShopInfo">
                        <EditItemTemplate>
                            <asp:CheckBox runat="server" ID="chkNeedShopInfoByEdit" Checked='<%# Bind("NeedShopInfo") %>' />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="chkNeedShopInfoByEdit1" Enabled="false" Checked='<%# Bind("NeedShopInfo") %>' />
                        </ItemTemplate>
                        <ItemStyle Width="120px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="IsPriceDiscount" meta:resourcekey="TemplateField_IsPriceDiscount">
                        <EditItemTemplate>
                            <asp:CheckBox runat="server" ID="chkIsPriceDiscountByEdit" Checked='<%# Bind("IsPriceDiscount") %>' />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="chkIsPriceDiscountByEdit1" Enabled="false" Checked='<%# Bind("IsPriceDiscount") %>' />
                        </ItemTemplate>
                        <ItemStyle Width="110px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="IsActive" HeaderText="<%$ Resources:Common,Form_IsActive %>">
                        <EditItemTemplate>
                            <asp:CheckBox runat="server" ID="chkActiveByEdit" Checked='<%# Bind("IsActive") %>' />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="chkActiveByEdit1" Enabled="false" Checked='<%# Bind("IsActive") %>' />
                        </ItemTemplate>
                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <EditItemTemplate>
                            <asp:RequiredFieldValidator ID="RFAccountingCode" runat="server" ControlToValidate="txtAccountingCode"
                                    Display="None" meta:resourcekey="RequiredFieldValidator_AccountingCode" SetFocusOnError="True" ValidationGroup="ItemEdit"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtLastAccountingCode"
                                    Display="None" ErrorMessage="请录入Accrual去年会计科目编码" SetFocusOnError="True" ValidationGroup="ItemEdit"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtAccrualAccountingCode"
                                    Display="None" ErrorMessage="请录入Accrual会计科目编码" SetFocusOnError="True" ValidationGroup="ItemEdit"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RFItemName" runat="server" ControlToValidate="txtExpenseItemName"
                                Display="None" meta:resourcekey="RequiredFieldValidator_ItemName" SetFocusOnError="True" ValidationGroup="ItemEdit"></asp:RequiredFieldValidator>
                            <asp:ValidationSummary ID="vsItemEdit" runat="server" ShowMessageBox="True" ShowSummary="False"
                                ValidationGroup="ItemEdit" />
                            <asp:LinkButton ID="LinkButton1" Visible="<%# HasManageRight %>" runat="server" CausesValidation="True"
                                ValidationGroup="ItemEdit" CommandName="Update" Text='<%$Resources:Common,Button_Update %>'></asp:LinkButton>
                            <asp:LinkButton ID="LinkButton3" Visible="<%# HasManageRight %>" runat="server" CausesValidation="false"
                                CommandName="Cancel" Text='<%$Resources:Common,Button_Cancel %>'></asp:LinkButton>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" Visible="<%# HasManageRight %>" runat="server" CausesValidation="false"
                                CommandName="Edit" Text='<%$Resources:Common,Button_Edit %>'></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="70px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header" style="height: 22px;">
                            <td align="center" style="width: 140px;" class="Empty1">
                                <asp:Label ID="Label1" runat="server" Text="P&L会计科目" ></asp:Label>
                            </td>
                            <td align="center" style="width: 140px;">
                                <asp:Label ID="Label10" runat="server" Text="Accrual会计科目"></asp:Label>
                            </td>
                            <td align="center" style="width: 140px;">
                                <asp:Label ID="Label9" runat="server" Text="Accrual去年科目"></asp:Label>
                            </td>
                            <td align="center" style="width: 250px;">
                                <asp:Label ID="Label5" runat="server" Text="<%$Resources:Common,Form_ExpenseItem %>"></asp:Label>
                            </td>
                            <td align="center" style="width: 200px;">
                                <asp:Label ID="Label2" runat="server" Text="<%$Resources:Common,Form_ExpenseSubCategory %>"></asp:Label>
                            </td>
                            <td align="center" style="width: 120px;">
                                <asp:Label ID="Label3" runat="server" meta:resourcekey="Label_IsPriceDiscount"></asp:Label>
                            </td>
                            <td align="center" style="width: 110px;">
                                <asp:Label ID="Label6" runat="server" meta:resourcekey="Label_IsPriceDiscount"></asp:Label>
                            </td>
                            <td align="center" style="width: 50px;">
                                <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Common,Form_IsActive %>"></asp:Label>
                            </td>
                            <td style="width: 70px;">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <SelectedRowStyle CssClass="SelectedRow" />
            </gc:GridView>
            <asp:FormView ID="fvExpenseItem" runat="server" DataKeyNames="ExpenseItemID" DataSourceID="odsExpenseItem"
                DefaultMode="Insert" Visible="<%# HasManageRight %>" EnableModelValidation="True"
                CellPadding="0">
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr>
                            <td align="center" style="height: 22px; width: 140px;">
                                <asp:TextBox ID="txtAccountingCodeByAdd" runat="server" Text='<%# Bind("AccountingCode") %>'
                                    Width="120px" CssClass="InputText" ValidationGroup="ItemINS" MaxLength="50"></asp:TextBox>
                            </td>
                            <td align="center" style="height: 22px; width: 140px;">
                                <asp:TextBox ID="txtAccrualAccountingCodeByAdd" runat="server" Text='<%# Bind("AccrualAccountingCode") %>'
                                    Width="120px" CssClass="InputText" ValidationGroup="ItemINS" MaxLength="50"></asp:TextBox>
                            </td>
                            <td align="center" style="height: 22px; width: 140px;">
                                <asp:TextBox ID="txtLastAccountingCodeByAdd" runat="server" Text='<%# Bind("LastAccountingCode") %>'
                                    Width="120px" CssClass="InputText" ValidationGroup="ItemINS" MaxLength="50"></asp:TextBox>
                            </td>

                            <td align="center" style="height: 22px; width: 250px;">
                                <asp:TextBox ID="txtExpenseItemNameByAdd" runat="server" Text='<%# Bind("ExpenseItemName") %>'
                                    Width="230px" CssClass="InputText" ValidationGroup="ItemINS" MaxLength="50"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 200px;">
                                <asp:DropDownList runat="server" ID="ddlExpenseSubCategory" Width="180px" SelectedValue='<%# Bind("ExpenseSubCategoryID")%>'
                                    DataTextField="ExpenseSubCategoryName" DataValueField="ExpenseSubCategoryID" DataSourceID="sdsExpenseSubCategory">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="sdsExpenseSubCategory" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                    SelectCommand="SELECT [ExpenseSubCategoryID], [ExpenseSubCategoryName] FROM [ExpenseSubCategory] where IsActive = 1 order by ExpenseSubCategoryName">
                                </asp:SqlDataSource>
                            </td>
                            <td align="center" style="width: 120px;">
                                <asp:CheckBox runat="server" ID="chkNeedShopInfoByAdd" Checked='<%# Bind("NeedShopInfo") %>' />
                            </td>
                            <td align="center" style="width: 110px;">
                                <asp:CheckBox runat="server" ID="chkIsPriceDiscount" Checked='<%# Bind("IsPriceDiscount") %>' />
                            </td>
                            <td align="center" style="width: 50px;">
                                <asp:CheckBox runat="server" ID="chkActiveByAdd" Checked='<%# Bind("IsActive") %>' />
                            </td>
                            <td align="center" style="width: 70px;">
                                <asp:RequiredFieldValidator ID="RFAccountingCode" runat="server" ControlToValidate="txtAccountingCodeByAdd"
                                    Display="None" meta:resourcekey="RequiredFieldValidator_AccountingCode" SetFocusOnError="True" ValidationGroup="ItemAdd"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RFItemName" runat="server" ControlToValidate="txtExpenseItemNameByAdd"
                                    Display="None" meta:resourcekey="RequiredFieldValidator_ItemName" SetFocusOnError="True" ValidationGroup="ItemAdd"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtLastAccountingCodeByAdd"
                                    Display="None" ErrorMessage="请录入Accrual去年科目编码" SetFocusOnError="True" ValidationGroup="ItemAdd"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtAccrualAccountingCodeByAdd"
                                    Display="None" ErrorMessage="请录入Accrual会计科目编码" SetFocusOnError="True" ValidationGroup="ItemAdd"></asp:RequiredFieldValidator>
                                <asp:ValidationSummary ID="vsItemAdd" runat="server" ShowMessageBox="True" ShowSummary="False"
                                    ValidationGroup="ItemAdd" />
                                <asp:LinkButton ID="InsertLinkButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    Text='<%$Resources:Common,Button_Add %>' ValidationGroup="ItemAdd"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                    <asp:ValidationSummary ID="ItemINSertValidationSummary" ValidationGroup="ItemINS"
                        ShowMessageBox="true" ShowSummary="false" EnableClientScript="true" runat="server" />
                </InsertItemTemplate>
            </asp:FormView>
            <asp:ObjectDataSource ID="odsExpenseItem" runat="server" EnablePaging="true" TypeName="BusinessObjects.MasterDataBLL"
                InsertMethod="InsertExpenseItem" SelectMethod="GetPagedExpenseItem" SortParameterName="sortExpression"
                SelectCountMethod="ExpenseItemTotalCount" UpdateMethod="UpdateExpenseItem" OnInserted="odsExpenseItem_Inserted"
                OnUpdated="odsExpenseItem_Updated">
                <SelectParameters>
                    <asp:Parameter Name="queryExpression" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
