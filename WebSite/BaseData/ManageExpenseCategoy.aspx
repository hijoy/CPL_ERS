<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Culture="auto" UICulture="auto" CodeFile="ManageExpenseCategoy.aspx.cs" Inherits="BaseData_ManageExpenseCategoy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="title">
        <asp:Label ID="Label6" runat="server" meta:resourcekey="ExpenseType_Label_ManageExpenseCategoy"></asp:Label></div>
    <asp:UpdatePanel ID="upCategory" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <gc:GridView ID="gvManageExpenseCategoy" CssClass="GridView" runat="server" DataSourceID="odsManageExpenseCategoy"
                AutoGenerateColumns="False" DataKeyNames="ManageExpenseCategoyID" CellPadding="0"
                AllowSorting="True" OnSelectedIndexChanged="gvManageExpenseCategoy_SelectedIndexChanged"
                EnableModelValidation="True">
                <Columns>
                    <asp:TemplateField SortExpression="ManageExpenseCategoyName" meta:resourcekey="ExpenseTypeGridView_TemplateField_ManageExpenseCategoyName">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtManageExpenseCategoyName" runat="server" Text='<%# Bind("ManageExpenseCategoyName") %>'
                                Width="570px" CssClass="InputText" MaxLength="50"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnManageExpenseCategoyName" runat="server" CausesValidation="False"
                                CommandName="Select" Text='<%# Bind("ManageExpenseCategoyName") %>'></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="596px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateField_FormType" SortExpression="FormTypeID">
                        <EditItemTemplate>
                            <asp:DropDownList runat="server" ID="ddlFormType" Width="460px" SelectedValue='<%# Bind("FormTypeID") %>'>
                                <asp:ListItem meta:resourcekey="ddl_1" Value="1"></asp:ListItem>
                                <asp:ListItem meta:resourcekey="ddl_4" Value="4"></asp:ListItem>
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbFormType" runat="server" Text='<%# getPageType(Eval("FormTypeID"))%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="480px" HorizontalAlign="Center" />
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
                            <asp:RequiredFieldValidator ID="RFManageExpenseCategoy" runat="server" ControlToValidate="txtManageExpenseCategoyName"
                                Display="None" meta:resourcekey="RequiredFieldValidator_ExpenseCategoy" SetFocusOnError="True" ValidationGroup="ManageExpenseCategoyEdit"></asp:RequiredFieldValidator>
                            <asp:ValidationSummary ID="vsManageExpenseCategoyEdit" runat="server" ShowMessageBox="True"
                                ShowSummary="False" ValidationGroup="ManageExpenseCategoyEdit" />
                            <asp:LinkButton ID="LinkButton1" Visible="<%# HasManageRight %>" runat="server" CausesValidation="True"
                                ValidationGroup="ManageExpenseCategoyEdit" CommandName="Update" Text='<%$Resources:Common,Button_Update %>'></asp:LinkButton>
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
                        <tr class="Header">
                            <td align="center" style="width: 596px;" class="Empty1">
                                <asp:Label ID="Label1" runat="server" meta:resourcekey="ExpenseType_Label_ManageExpenseCategoyName"></asp:Label>
                            </td>
                            <td align="center" style="width: 460px;">
                                <asp:Label ID="Label2" runat="server" meta:resourcekey="Label_FormType"></asp:Label>
                            </td>
                            <td align="center" style="width: 70px;">
                                <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Common,Form_IsActive %>"></asp:Label>
                            </td>
                            <td align="center" style="width: 60px;">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <SelectedRowStyle CssClass="SelectedRow" />
            </gc:GridView>
            <asp:FormView ID="fvManageExpenseCategoy" runat="server" DataKeyNames="ManageExpenseCategoyID"
                DataSourceID="odsManageExpenseCategoy" DefaultMode="Insert" Visible="<%# HasManageRight %>"
                EnableModelValidation="True" CellPadding="0">
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr>
                            <td align="center" scope="col" style="height: 22px; width: 596px;">
                                <asp:TextBox ID="txtManageExpenseCategoyNameByAdd" runat="server" Text='<%# Bind("ManageExpenseCategoyName") %>'
                                    Width="570px" CssClass="InputText" ValidationGroup="CateIns" MaxLength="50"></asp:TextBox>
                            </td>
                            <td align="center" style="height: 22px; width: 480px;">
                                <asp:DropDownList runat="server" ID="ddlFormTypeByAdd" Width="460px" SelectedValue='<%# Bind("FormTypeID") %>'>
                                    <asp:ListItem meta:resourcekey="ddl_1" Value="1"></asp:ListItem>
                                    <asp:ListItem meta:resourcekey="ddl_3" Value="4"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td align="center" style="height: 22px; width: 70px;">
                                <asp:CheckBox runat="server" ID="chkActiveByAdd" Checked='<%# Bind("IsActive") %>' />
                            </td>
                            <td align="center" style="width: 90px;">
                                <asp:RequiredFieldValidator ID="RFManageExpenseCategoy" runat="server" ControlToValidate="txtManageExpenseCategoyNameByAdd"
                                    Display="None" meta:resourcekey="RequiredFieldValidator_ExpenseCategoy" SetFocusOnError="True" ValidationGroup="ManageExpenseCategoyAdd"></asp:RequiredFieldValidator>
                                <asp:ValidationSummary ID="vsManageExpenseCategoyAdd" runat="server" ShowMessageBox="True"
                                    ShowSummary="False" ValidationGroup="ManageExpenseCategoyAdd" />
                                <asp:LinkButton ID="InsertLinkButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    Text='<%$Resources:Common,Button_Add %>' ValidationGroup="ManageExpenseCategoyAdd"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </InsertItemTemplate>
            </asp:FormView>
            <asp:ObjectDataSource ID="odsManageExpenseCategoy" runat="server" TypeName="BusinessObjects.MasterDataBLL"
                InsertMethod="InsertManageExpenseCategoy" SelectMethod="GetManageExpenseCategoy"
                UpdateMethod="UpdateManageExpenseCategoy">
                <UpdateParameters>
                    <asp:Parameter Name="ManageExpenseCategoyID" Type="Int32" />
                    <asp:Parameter Name="FormTypeID" Type="Int32" />
                    <asp:Parameter Name="ManageExpenseCategoyName" Type="String" />
                    <asp:Parameter Name="IsActive" Type="Boolean" />
                </UpdateParameters>
                <InsertParameters>
                    <asp:Parameter Name="ManageExpenseCategoyName" Type="String" />
                    <asp:Parameter Name="FormTypeID" Type="Int32" />
                    <asp:Parameter Name="IsActive" Type="Boolean" />
                </InsertParameters>
            </asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <div class="title">
        <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Common,Form_ExpenseItem %>"></asp:Label></div>
    <asp:UpdatePanel ID="upManageExpenseItem" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <gc:GridView Visible="false" ID="gvManageExpenseItem" CssClass="GridView" runat="server"
                DataSourceID="odsManageExpenseItem" AutoGenerateColumns="False" CellPadding="0"
                AllowSorting="True" AllowPaging="True" PageSize="20" DataKeyNames="ManageExpenseItemID" OnSelectedIndexChanged="gvManageExpenseItem_SelectedIndexChanged">
                <HeaderStyle CssClass="Header" />
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td style="width: 338px;" class="Empty1">
                                <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Common,Form_ExpenseItem %>"></asp:Label>
                            </td>
                            <td style="width: 300px;">
                                <asp:Label ID="Label4" runat="server" meta:resourcekey="ExpenseItem_Label_AccountingCode"></asp:Label>
                            </td>
                            <td style="width: 300px;">
                                <asp:Label ID="Label8" runat="server" meta:resourcekey="Label_AccountingName"></asp:Label>
                            </td>
                            <td style="width: 100px;">
                                <asp:Label ID="Label7" runat="server" meta:resourcekey="ExpenseItem_Label_IsTicket"></asp:Label>
                            </td>
                            <td style="width: 100px;">
                                <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Common,Form_IsActive %>"></asp:Label>
                            </td>
                            <td style="width: 90px;">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <Columns>
                    <asp:TemplateField HeaderText="<%$ Resources:Common,Form_ExpenseItem %>" SortExpression="ManageExpenseItemName">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtManageExpenseItemName" runat="server" Text='<%# Bind("ManageExpenseItemName") %>'
                                CssClass="InputText" Width="310px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="lbManageExpenseItemName" runat="server" CausesValidation="False"
                                CommandName="Select" Text='<%# Bind("ManageExpenseItemName") %>'></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="338px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="ExpenseItemGridView_TemplateField_AccountingCode"
                        SortExpression="AccountingCode">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtAccountingCode" runat="server" Text='<%# Bind("AccountingCode") %>'
                                CssClass="InputText" Width="280px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblAccountingCode" runat="server" Text='<%#Eval("AccountingCode")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="300px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateField_AccountingName" SortExpression="AccountingName">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtAccountingName" runat="server" Text='<%# Bind("AccountingName") %>'
                                CssClass="InputText" Width="280px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblAccountingName" runat="server" Text='<%#Eval("AccountingName")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="300px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="ExpenseItemGridView_TemplateField_IsTicket">
                        <EditItemTemplate>
                            <asp:CheckBox runat="server" ID="chkIsTicketByEdit" Width="40px" Checked='<%# Bind("IsTicket") %>' />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="chkIsTicketByEdit1" Width="40px" Enabled="false"
                                Checked='<%# Bind("IsTicket") %>' />
                        </ItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:Common,Form_IsActive %>">
                        <EditItemTemplate>
                            <asp:CheckBox runat="server" ID="chkActiveByEdit" Width="40px" Checked='<%# Bind("IsActive") %>' />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="chkActiveByEdit1" Width="40px" Enabled="false" Checked='<%# Bind("IsActive") %>' />
                        </ItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <EditItemTemplate>
                            <asp:RequiredFieldValidator ID="RFSubCategoryEdit" runat="server" ControlToValidate="txtManageExpenseItemName"
                                Display="None" meta:resourcekey="RequiredFieldValidator_ExpenseItem" SetFocusOnError="True" ValidationGroup="subCateEdit"></asp:RequiredFieldValidator>
                            <asp:ValidationSummary ID="vsSubCateEdit" runat="server" ShowMessageBox="True" ShowSummary="False"
                                ValidationGroup="subCateEdit" />
                            <asp:LinkButton ID="LinkButton1" Visible="<%# HasManageRight %>" runat="server" ValidationGroup="subCateEdit"
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
            </gc:GridView>
            <asp:FormView ID="fvManageExpenseItem" Visible="false" runat="server" DataKeyNames="ManageExpenseItemID"
                DataSourceID="odsManageExpenseItem" DefaultMode="Insert" CellPadding="0">
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr>
                            <td align="center" style="height: 22px; width: 338px;">
                                <asp:TextBox ID="txtManageExpenseItemNameByAdd" runat="server" Text='<%# Bind("ManageExpenseItemName") %>'
                                    CssClass="InputText" ValidationGroup="ManageExpenseItemInsert" Width="310px"></asp:TextBox>
                            </td>
                            <td align="center" style="height: 22px; width: 300px;">
                                <asp:TextBox ID="txtAccountingCodeByAdd" runat="server" Text='<%# Bind("AccountingCode") %>'
                                    CssClass="InputText" ValidationGroup="ManageExpenseItemInsert" Width="280px"></asp:TextBox>
                            </td>
                            <td align="center" style="height: 22px; width: 300px;">
                                <asp:TextBox ID="txtAccountingNameByAdd" runat="server" Text='<%# Bind("AccountingName") %>'
                                    CssClass="InputText" ValidationGroup="ManageExpenseItemInsert" Width="280px"></asp:TextBox>
                            </td>
                            <td align="center" style="height: 22px; width: 100px;">
                                <asp:CheckBox runat="server" ID="chkTicketByAdd" Width="40px" Checked='<%# Bind("IsTicket") %>' />
                            </td>
                            <td align="center" style="height: 22px; width: 100px;">
                                <asp:CheckBox runat="server" ID="chkActiveByAdd" Width="40px" Checked='<%# Bind("IsActive") %>' />
                            </td>
                            <td align="center" style="width: 90px;">
                                <asp:RequiredFieldValidator ID="RFManageExpenseItemNameAdd" runat="server" ControlToValidate="txtManageExpenseItemNameByAdd"
                                    Display="None" meta:resourcekey="RequiredFieldValidator_ExpenseItem" SetFocusOnError="True" ValidationGroup="ManageExpenseItemInsert"></asp:RequiredFieldValidator>
                                <asp:ValidationSummary ID="vsSubCateAdd" runat="server" ShowMessageBox="True" ShowSummary="False"
                                    ValidationGroup="ManageExpenseItemInsert" />
                                <asp:LinkButton ID="InsertLinkButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    Text='<%$Resources:Common,Button_Add %>' ValidationGroup="ManageExpenseItemInsert"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                    <asp:ValidationSummary ID="SubCategoryInsertValidationSummary" ValidationGroup="ManageExpenseItemInsert"
                        runat="server" ShowMessageBox="true" ShowSummary="false" />
                </InsertItemTemplate>
            </asp:FormView>
            <asp:ObjectDataSource ID="odsManageExpenseItem" runat="server" TypeName="BusinessObjects.MasterDataBLL"
                InsertMethod="InsertManageExpenseItem" SelectMethod="GetManageExpenseItemPaged"
                SortParameterName="sortExpression" EnablePaging="true" SelectCountMethod="ManageExpenseItemTotalCount"
                UpdateMethod="UpdateManageExpenseItem" OnUpdating="odsManageExpenseItem_Updating"
                OnSelecting="odsManageExpenseItem_Selecting" OnInserting="odsManageExpenseItem_Inserting">
                <UpdateParameters>
                    <asp:Parameter Name="AccountingCode" Type="String" />
                    <asp:Parameter Name="ManageExpenseItemName" Type="String" />
                    <asp:Parameter Name="ManageExpenseItemID" Type="Int32" />
                    <asp:Parameter Name="ManageExpenseCategoyID" Type="Int32" />
                    <asp:Parameter Name="IsActive" Type="Boolean" />
                    <asp:Parameter Name="IsTicket" Type="Boolean" />
                </UpdateParameters>
                <InsertParameters>
                    <asp:Parameter Name="AccountingCode" Type="String" />
                    <asp:Parameter Name="ManageExpenseItemName" Type="String" />
                    <asp:Parameter Name="ManageExpenseCategoyID" Type="Int32" />
                    <asp:Parameter Name="IsActive" Type="Boolean" />
                    <asp:Parameter Name="IsTicket" Type="Boolean" />
                </InsertParameters>
                <SelectParameters>
                    <asp:Parameter Name="ManageExpenseCategoyID" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>

    <br />
    <div class="title" ><asp:Label ID="Label_Title" runat="server" meta:resourcekey="Label_Title"/></div>
    <asp:UpdatePanel ID="upAccounting" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <gc:GridView Visible="false" ID="gvAccounting" CssClass="GridView" runat="server" DataSourceID="odsAccounting"
                AutoGenerateColumns="False" CellPadding="0" DataKeyNames="ManageExpenseAccountingID">
                <Columns>
                    <asp:TemplateField HeaderText="<%$ Resources:Common,Form_CostCenter %>" >
                        <EditItemTemplate>
                            <asp:DropDownList runat="server" ID="ddlCostCenter" Width="350px" DataTextField="CostCenterName"
                                DataValueField="CostCenterID" DataSourceID="odsCostCenter" SelectedValue='<%# Bind("CostCenterID") %>'>
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblCostCenter" runat="server" Text='<%#GetCostCenterNameById(Eval("CostCenterID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="400px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateField_AccountingCode">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtAccountingCode" runat="server" Text='<%# Bind("AccountingCode") %>' CssClass="InputText" Width="250px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblAccountingCode" runat="server" Text='<%# Eval("AccountingCode") %>' ></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="300px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateField_AccountingName">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtAccountingName" runat="server" Text='<%# Bind("AccountingName") %>' CssClass="InputText" Width="350px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblAccountingName" runat="server" Text='<%# Eval("AccountingName") %>' ></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="400px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <EditItemTemplate>
                            <asp:RequiredFieldValidator ID="RF1" runat="server" ControlToValidate="txtAccountingCode"
                                Display="None" meta:resourcekey="RequiredFieldValidator_AccountingCode" SetFocusOnError="True" ValidationGroup="VGAccounting"></asp:RequiredFieldValidator>
                            <asp:ValidationSummary ID="vsAccount" runat="server" ShowMessageBox="True" ShowSummary="False"
                                ValidationGroup="VGAccounting" />
                            <asp:LinkButton ID="LinkButton1" Visible="<%# HasManageRight %>"  runat="server" ValidationGroup="VGAccounting"
                                CausesValidation="True" CommandName="Update" Text="<%$Resources:Common,Button_Update %>"></asp:LinkButton>
                            <asp:LinkButton ID="LinkButton3" Visible="<%# HasManageRight %>"  runat="server" CausesValidation="False"
                                CommandName="Cancel" Text="<%$Resources:Common,Button_Cancel %>"></asp:LinkButton>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" Visible="<%# HasManageRight %>"  runat="server" CausesValidation="False"
                                CommandName="Edit"  Text="<%$Resources:Common,Button_Edit %>"></asp:LinkButton>
                            <asp:LinkButton ID="LinkButton2" Visible="<%# HasManageRight %>"  runat="server" CommandName="Delete"
                                Text="<%$Resources:Common,Button_Delete %>" ></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="140px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td align="center" style="width: 400px;" class="Empty1">
                                <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Common,Form_CostCenter %>"/>
                            </td>
                            <td align="center" style="width: 300px;">
                                <asp:Label ID="Label9" runat="server" meta:resourcekey="Label_AccountingCode"/>
                            </td>
                            <td align="center" style="width: 400px;">
                                <asp:Label ID="Label10" runat="server" meta:resourcekey="Label_AccountingName"/>
                            </td>
                            <td align="center" style="width: 140px;">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <SelectedRowStyle CssClass="SelectedRow" />

            </gc:GridView>
            <asp:FormView ID="fvAccounting" Visible="false" runat="server" DataKeyNames="ManageExpenseAccountingID"
                DataSourceID="odsAccounting" DefaultMode="Insert" >
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr>
                            <td align="center" style="height: 22px; width: 400px;">
                                <asp:DropDownList runat="server" ID="ddlNewCostCenter" Width="350px" DataTextField="CostCenterName"
                                    DataValueField="CostCenterID" DataSourceID="odsCostCenter" SelectedValue='<%# Bind("CostCenterID") %>'>
                                </asp:DropDownList>
                            </td>
                            <td align="center" style="height: 22px; width: 300px;">
                                <asp:TextBox ID="txtNewAccountingCode" runat="server" Text='<%# Bind("AccountingCode") %>'
                                    CssClass="InputText" ValidationGroup="VGAccountInsert" Width="250px"></asp:TextBox>
                            </td>
                            <td align="center" style="height: 22px; width: 400px;">
                                <asp:TextBox ID="txtNewAccountingName" runat="server" Text='<%# Bind("AccountingName") %>'
                                    CssClass="InputText"  Width="280px"></asp:TextBox>
                            </td>

                            <td align="center" style="width: 140px;">
                                <asp:RequiredFieldValidator ID="rf2" runat="server" ControlToValidate="txtNewAccountingCode"
                                    Display="None" meta:resourcekey="RequiredFieldValidator_AccountingCode" SetFocusOnError="True" ValidationGroup="VGAccountInsert"></asp:RequiredFieldValidator>
                                <asp:ValidationSummary ID="vsAccountingAdd" runat="server" ShowMessageBox="True" ShowSummary="False"
                                    ValidationGroup="VGAccountInsert" />
                                <asp:LinkButton ID="InsertLinkButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    Text="<%$Resources:Common,Button_Add %>" ValidationGroup="VGAccountInsert"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </InsertItemTemplate>
            </asp:FormView>
            <asp:SqlDataSource ID="odsCostCenter" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                SelectCommand="select CostCenterID,CostCenterCode+'-'+CostCenterName CostCenterName from CostCenter where IsMAA = 0"></asp:SqlDataSource>
            <asp:ObjectDataSource ID="odsAccounting" runat="server" TypeName="BusinessObjects.MasterDataBLL"
                DeleteMethod="DeleteManageExpenseAccountingByID" InsertMethod="InsertManageExpenseAccounting" SelectMethod="GetManageExpenseAccountingByManageExpenseItemID"
                EnablePaging="false" UpdateMethod="UpdateManageExpenseAccounting" OnInserting="odsAccounting_Inserting" >
                <DeleteParameters>
                    <asp:Parameter Name="ManageExpenseAccountingID" Type="Int32" />
                </DeleteParameters>
                <UpdateParameters>
                    <asp:Parameter Name="ManageExpenseAccountingID" Type="Int32" />
                    <asp:Parameter Name="CostCenterID" Type="Int32" />
                    <asp:Parameter Name="AccountingCode" Type="String" />
                    <asp:Parameter Name="AccountingName" Type="String" />
                </UpdateParameters>
                <InsertParameters>
                    <asp:Parameter Name="ManageExpenseItemID" Type="Int32" />
                    <asp:Parameter Name="CostCenterID" Type="Int32" />
                    <asp:Parameter Name="AccountingCode" Type="String" />
                    <asp:Parameter Name="AccountingName" Type="String" />
                </InsertParameters>
                <SelectParameters>
                    <asp:Parameter Name="ManageExpenseItemID" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
