<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ExpenseCategory.aspx.cs" Inherits="BaseData_ExpenseSubCategory" Culture="Auto"
    UICulture="Auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="title">
        <asp:Label runat="server" meta:resourcekey="Label_ExpenseCategory"></asp:Label>
    </div>
    <asp:UpdatePanel ID="upExpenseCategory" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <gc:GridView ID="gvExpenseCategory" CssClass="GridView" runat="server" DataSourceID="odsExpenseCategory"
                CellPadding="0" AutoGenerateColumns="False" DataKeyNames="ExpenseCategoryID"
                AllowPaging="True" AllowSorting="True" PageSize="20" OnSelectedIndexChanged="gvExpenseCategory_SelectedIndexChanged"
                EnableModelValidation="True">
                <Columns>
                    <asp:TemplateField SortExpression="ExpenseCategoryName" meta:resourcekey="TemplateField_Name">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtExpenseCategoryName" runat="server" Text='<%# Bind("ExpenseCategoryName") %>'
                                Width="290px" CssClass="InputText" MaxLength="50"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnExpenseCategoryName" runat="server" CausesValidation="False"
                                CommandName="Select" Text='<%# Bind("ExpenseCategoryName") %>'></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="946px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="BusinessType" meta:resourcekey="TemplateField_BusinessType">
                        <EditItemTemplate>
                            <asp:DropDownList runat="server" ID="ddlBusinessType" Width="110px" SelectedValue='<%# Bind("BusinessType") %>'>
                                <asp:ListItem meta:resourcekey="ListItem1" Value="1"></asp:ListItem>
                                <asp:ListItem meta:resourcekey="ListItem2" Value="2"></asp:ListItem>
                                <asp:ListItem meta:resourcekey="ListItem3" Value="3"></asp:ListItem>
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblPageType" Text='<%# getBusinessType(Eval("BusinessType"))%>' />
                        </ItemTemplate>
                        <ItemStyle Width="130px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="NeedPO" meta:resourcekey="TemplateField_NeedPO">
                        <EditItemTemplate>
                            <asp:CheckBox runat="server" ID="chkNeedPOByEdit" Checked='<%# Bind("NeedPO") %>' />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="chkNeedPOByEdit1" Enabled="false" Checked='<%# Bind("NeedPO") %>' />
                        </ItemTemplate>
                        <ItemStyle Width="70px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <EditItemTemplate>
                            <asp:RequiredFieldValidator ID="RFCateName" runat="server" ControlToValidate="txtExpenseCategoryName"
                                Display="None" meta:resourcekey="RequiredFieldValidator_ExpenseCategoryName"
                                SetFocusOnError="True" ValidationGroup="CateEdit"></asp:RequiredFieldValidator>
                            <asp:ValidationSummary ID="vsCateEdit" runat="server" ShowMessageBox="True" ShowSummary="False"
                                ValidationGroup="CateEdit" />
                            <asp:LinkButton ID="LinkButton1" Visible="<%# HasManageRight %>" runat="server" CausesValidation="True"
                                ValidationGroup="CateEdit" CommandName="Update" Text='<%$Resources:Common,Button_Update %>'></asp:LinkButton>
                            <asp:LinkButton ID="LinkButton3" Visible="<%# HasManageRight %>" runat="server" CausesValidation="false"
                                CommandName="Cancel" Text='<%$Resources:Common,Button_Cancel %>'></asp:LinkButton>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" Visible="<%# HasManageRight %>" runat="server" CausesValidation="false"
                                CommandName="Edit" Text='<%$Resources:Common,Button_Edit %>'></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header" style="height: 22px;">
                            <td align="center" style="width: 947px;" class="Empty1">
                                <asp:Label ID="Label1" runat="server" meta:resourcekey="Label_Name"></asp:Label>
                            </td>
                            <td align="center" style="width: 130px;">
                                <asp:Label ID="Label5" runat="server" meta:resourcekey="Label_BusinessType"></asp:Label>
                            </td>
                            <td align="center" style="width: 70px;">
                                <asp:Label ID="Label2" runat="server" meta:resourcekey="Label_NeedPO"></asp:Label>
                            </td>
                            <td style="width: 90px;">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <SelectedRowStyle CssClass="SelectedRow" />
            </gc:GridView>
            <asp:FormView ID="fvExpenseCategory" runat="server" DataKeyNames="ExpenseCategoryID"
                DataSourceID="odsExpenseCategory" DefaultMode="Insert" Visible="<%# HasManageRight %>"
                EnableModelValidation="True" CellPadding="0">
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr>
                            <td align="center" style="height: 22px; width: 947px;">
                                <asp:TextBox ID="txtExpenseCategoryNameByAdd" runat="server" Text='<%# Bind("ExpenseCategoryName") %>'
                                    Width="290px" CssClass="InputText" ValidationGroup="CateINS" MaxLength="50"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 130px;">
                                <asp:DropDownList runat="server" ID="ddlBusinessType" Width="110px" SelectedValue='<%# Bind("BusinessType") %>'>
                                    <asp:ListItem meta:resourcekey="ListItem1" Value="1"></asp:ListItem>
                                    <asp:ListItem meta:resourcekey="ListItem2" Value="2"></asp:ListItem>
                                    <asp:ListItem meta:resourcekey="ListItem3" Value="3"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td align="center" style="width: 70px;">
                                <asp:CheckBox runat="server" ID="chkNeedPOByAdd" Checked='<%# Bind("NeedPO") %>' />
                            </td>
                            <td align="center" style="width: 90px;">
                                <asp:RequiredFieldValidator ID="RFCateName" runat="server" ControlToValidate="txtExpenseCategoryNameByAdd"
                                    Display="None" meta:resourcekey="RequiredFieldValidator_ExpenseCategoryName"
                                    SetFocusOnError="True" ValidationGroup="CateAdd"></asp:RequiredFieldValidator>
                                <asp:ValidationSummary ID="vsCateAdd" runat="server" ShowMessageBox="True" ShowSummary="False"
                                    ValidationGroup="CateAdd" />
                                <asp:LinkButton ID="InsertLinkButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    Text='<%$Resources:Common,Button_Add %>' ValidationGroup="CateAdd"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                    <asp:ValidationSummary ID="CateInsertValidationSummary" ValidationGroup="CateINS"
                        ShowMessageBox="true" ShowSummary="false" EnableClientScript="true" runat="server" />
                </InsertItemTemplate>
            </asp:FormView>
            <asp:ObjectDataSource ID="odsExpenseCategory" runat="server" TypeName="BusinessObjects.MasterDataBLL"
                InsertMethod="InsertExpenseCategory" SelectMethod="GetExpenseCategory" UpdateMethod="UpdateExpenseCategory"
                OnInserted="odsExpenseCategory_Inserted" OnUpdated="odsExpenseCategory_Updated">
            </asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <div class="title">
        <asp:Label runat="server" Text="<%$Resources:Common,Form_ExpenseSubCategory %>"></asp:Label>
    </div>
    <asp:UpdatePanel ID="upExpenseSubCategory" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <gc:GridView Visible="false" ID="gvExpenseSubCategory" CssClass="GridView" runat="server"
                DataSourceID="odsExpenseSubCategory" AutoGenerateColumns="False" CellPadding="0"
                AllowSorting="True" AllowPaging="True" PageSize="20" DataKeyNames="ExpenseSubCategoryId">
                <Columns>
                    <asp:TemplateField meta:resourcekey="TemplateField_Name" SortExpression="ExpenseSubCategoryName">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtExpenseSubCategoryNameByEdit" runat="server" Text='<%# Bind("ExpenseSubCategoryName") %>'
                                CssClass="InputText" Width="290px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblExpenseSubCategoryNameByEdit" runat="server" Text='<%# Bind("ExpenseSubCategoryName") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="946px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="PageType" meta:resourcekey="TemplateField_PageType">
                        <EditItemTemplate>
                            <asp:DropDownList runat="server" ID="ddlFormType" Width="110px" SelectedValue='<%# Bind("PageType") %>'>
                                <asp:ListItem meta:resourcekey="ListItem_PageType1" Value="11"></asp:ListItem>
                                <asp:ListItem meta:resourcekey="ListItem_PageType2" Value="12"></asp:ListItem>
                                <asp:ListItem meta:resourcekey="ListItem_PageType3" Value="13"></asp:ListItem>
                                <asp:ListItem meta:resourcekey="ListItem_PageType4" Value="41"></asp:ListItem>
                                <asp:ListItem meta:resourcekey="ListItem_PageType5" Value="27"></asp:ListItem>
                                <asp:ListItem meta:resourcekey="ListItem_PageType6" Value="43"></asp:ListItem>
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblPageType" Text='<%# getPageType(Eval("PageType"))%>' />
                        </ItemTemplate>
                        <ItemStyle Width="130px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="IsActive" HeaderText="<%$ Resources:Common,Form_IsActive %>">
                        <EditItemTemplate>
                            <asp:CheckBox runat="server" ID="chkActiveByEdit" Checked='<%# Bind("IsActive") %>' />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="chkActiveByEdit1" Enabled="false" Checked='<%# Bind("IsActive") %>' />
                        </ItemTemplate>
                        <ItemStyle Width="70px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <EditItemTemplate>
                            <asp:RequiredFieldValidator ID="RFExpenseSubCategoryName" runat="server" ControlToValidate="txtExpenseSubCategoryNameByEdit"
                                Display="None" meta:resourcekey="RequiredFieldValidator_ExpenseSubCategoryName"
                                SetFocusOnError="True" ValidationGroup="ExpenseSubCategoryEdit"></asp:RequiredFieldValidator>
                            <asp:ValidationSummary ID="vsExpenseSubCategoryEdit" runat="server" ShowMessageBox="True"
                                ShowSummary="False" ValidationGroup="ExpenseSubCategoryEdit" />
                            <asp:LinkButton ID="LinkButton1" Visible="<%# HasManageRight %>" runat="server" ValidationGroup="ExpenseSubCategoryEdit"
                                CausesValidation="True" CommandName="Update" Text='<%$Resources:Common,Button_Update %>'></asp:LinkButton>
                            <asp:LinkButton ID="LinkButton3" Visible="<%# HasManageRight %>" runat="server" CausesValidation="False"
                                CommandName="Cancel" Text='<%$Resources:Common,Button_Cancel %>'></asp:LinkButton>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" Visible="<%# HasManageRight %>" runat="server" CausesValidation="False"
                                CommandName="Edit" Text='<%$Resources:Common,Button_Edit %>'></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td align="center" style="width: 946px;" class="Empty1">
                                <asp:Label ID="Label1" runat="server" meta:resourcekey="Label_Name"></asp:Label>
                            </td>
                            <td align="center" style="width: 130px;">
                                <asp:Label ID="Label3" runat="server" meta:resourcekey="Label_PageType"></asp:Label>
                            </td>
                            <td align="center" style="width: 70px;">
                                <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Common,Form_IsActive %>"></asp:Label>
                            </td>
                            <td style="width: 90px;">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
            </gc:GridView>
            <asp:FormView ID="fvExpenseSubCategory" Visible="false" runat="server" DataKeyNames="ExpenseSubCategoryId"
                DataSourceID="odsExpenseSubCategory" DefaultMode="Insert" OnItemInserting="fvExpenseSubCategory_ItemInserting"
                CellPadding="0" CellSpacing="0">
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr>
                            <td align="center" style="width: 946px;">
                                <asp:TextBox ID="txtExpenseSubCategoryNameByAdd" runat="server" Text='<%# Bind("ExpenseSubCategoryName") %>'
                                    CssClass="InputText" ValidationGroup="ExpenseSubCategoryInsert" Width="290px"></asp:TextBox>
                            </td>
                            <td align="center" style="height: 22px; width: 130px;">
                                <asp:DropDownList runat="server" ID="ddlFormType" Width="110px" SelectedValue='<%# Bind("PageType") %>'>
                                    <asp:ListItem meta:resourcekey="ListItem_PageType1" Value="11"></asp:ListItem>
                                    <asp:ListItem meta:resourcekey="ListItem_PageType2" Value="12"></asp:ListItem>
                                    <asp:ListItem meta:resourcekey="ListItem_PageType3" Value="41"></asp:ListItem>
                                    <asp:ListItem meta:resourcekey="ListItem_PageType4" Value="31"></asp:ListItem>
                                    <asp:ListItem meta:resourcekey="ListItem_PageType5" Value="27"></asp:ListItem>
                                    <asp:ListItem meta:resourcekey="ListItem_PageType6" Value="43"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td align="center" style="height: 22px; width: 70px;">
                                <asp:CheckBox runat="server" ID="chkActiveByAdd" Checked='<%# Bind("IsActive") %>' />
                            </td>
                            <td align="center" style="width: 90px;">
                                <asp:RequiredFieldValidator ID="RFExpenseSubCategoryName" runat="server" ControlToValidate="txtExpenseSubCategoryNameByAdd"
                                    Display="None" meta:resourcekey="RequiredFieldValidator_ExpenseSubCategoryName"
                                    SetFocusOnError="True" ValidationGroup="ExpenseSubCategoryAdd"></asp:RequiredFieldValidator>
                                <asp:ValidationSummary ID="vsExpenseSubCategoryAdd" runat="server" ShowMessageBox="True"
                                    ShowSummary="False" ValidationGroup="ExpenseSubCategoryAdd" />
                                <asp:LinkButton ID="InsertLinkButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    Text='<%$Resources:Common,Button_Add %>' ValidationGroup="ExpenseSubCategoryAdd"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                    <asp:ValidationSummary ID="ExpenseSubCategoryInsertValidationSummary" ValidationGroup="ExpenseSubCategoryInsert"
                        runat="server" ShowMessageBox="true" ShowSummary="false" />
                </InsertItemTemplate>
            </asp:FormView>
            <asp:ObjectDataSource ID="odsExpenseSubCategory" runat="server" TypeName="BusinessObjects.MasterDataBLL"
                InsertMethod="InsertExpenseSubCategory" SelectMethod="GetPagedExpenseSubCategory"
                OnInserted="odsExpenseSubCategory_Inserted" OnUpdated="odsExpenseSubCategory_Updated"
                EnablePaging="true" SelectCountMethod="ExpenseSubCategoryTotalCount" UpdateMethod="UpdateExpenseSubCategory"
                SortParameterName="sortExpression" OnSelecting="odsExpenseSubCategory_Selecting"
                OnInserting="odsExpenseSubCategory_Inserting">
                <InsertParameters>
                    <asp:Parameter Name="ExpenseCategoryID" Type="Int32" />
                </InsertParameters>
                <SelectParameters>
                    <asp:Parameter Name="ExpenseCategoryID" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
