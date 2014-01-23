<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="CostLimit.aspx.cs" Inherits="BaseData_CostLimit" Culture="auto" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="title" >
        <asp:Label ID="Label_SearchCondition" runat="server" Text="<%$Resources:Common,Label_SearchCondition %>" /></div>
    <div class="searchDiv">
        <table class="searchTable">
            <tr>
                <td style="width: 240px;">
                    <div class="field_title">
                        <asp:Label ID="Label8" runat="server" meta:resourcekey="CostLimit_Label_CityTypeName"></asp:Label>
                     </div>
                    <asp:DropDownList runat="server" ID="ddlSearchCityType" DataSourceID="odsSearchCityType" DataTextField="CityTypeName"
                        DataValueField="CityTypeID" Width="180px" >
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="odsSearchCityType" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                        SelectCommand="select -1 CityTypeID,'全部' CityTypeName union select 0 CityTypeID,' 不适用' CityTypeName Union SELECT [CityTypeID], [CityTypeName] FROM [CityType] where IsActive= 1">
                    </asp:SqlDataSource>
                </td>
                <td style="width: 240px;">
                    <div class="field_title">
                        <asp:Label ID="Label1" runat="server" meta:resourcekey="CostLimit_Label_StaffLevel"></asp:Label>
                    </div>
                    <asp:DropDownList runat="server" ID="ddlSearchStaffLevel" DataSourceID="odsSearchStaffLevel"
                        DataTextField="StaffLevelName" DataValueField="StaffLevelID" Width="180px">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="odsSearchStaffLevel" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                        SelectCommand="select 0 StaffLevelID,'全部' StaffLevelName union  SELECT [StaffLevelID], [StaffLevelName] FROM [StaffLevel] where IsActive= '1'">
                    </asp:SqlDataSource>
                </td>
                <td style="width: 240px;">
                    <div class="field_title">
                        <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Common,Form_ExpenseItem %>"></asp:Label>
                    </div>
                    <asp:DropDownList runat="server" ID="ddlSearchManageExpenseItem" DataSourceID="odsSearchManageExpenseItem"
                        DataTextField="ManageExpenseItemName" DataValueField="ManageExpenseItemID" Width="180px">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="odsSearchManageExpenseItem" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                        SelectCommand="select 0 ManageExpenseItemID,'全部' ManageExpenseItemName union SELECT [ManageExpenseItemID], [ManageExpenseItemName] FROM [ManageExpenseItem] where IsActive= '1'">
                    </asp:SqlDataSource>
                </td>
                <td style="width: 500px;text-align: right;">
                    <asp:Button runat="server" ID="SearchButton" Text="<%$Resources:Common,Button_Search %>" CssClass="button_nor" OnClick="SearchButton_Click"></asp:Button>
                </td>
            </tr>
        </table>
    </div>

    <br />

    <div class="title">
        <asp:Label ID="Label2" runat="server" meta:resourcekey="CostLimit_Label_Title"></asp:Label></div>
    <asp:UpdatePanel ID="upCostLimit" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <gc:GridView ID="gvCostLimit" CssClass="GridView" runat="server" DataSourceID="odsCostLimit"
                CellPadding="0" AutoGenerateColumns="False" DataKeyNames="CostLimitID" AllowPaging="true"
                PageSize="20" EnableModelValidation="True">
                <Columns>
                    <asp:TemplateField SortExpression="CityTypeID" meta:resourcekey="CostLimitGridView_TemplateField_CityTypeName">
                        <EditItemTemplate>
                            <asp:DropDownList runat="server" ID="ddlCityType" DataSourceID="odsCityType" DataTextField="CityTypeName"
                                DataValueField="CityTypeID" Width="240px" SelectedValue='<%# Bind("CityTypeID") %>'>
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="odsCityType" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                SelectCommand="select 0 CityTypeID,' 不适用' CityTypeName Union SELECT [CityTypeID], [CityTypeName] FROM [CityType] where IsActive= 1">
                            </asp:SqlDataSource>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblCityTypeName" runat="server" Text='<%# GetCityTypeNameByID(Eval("CityTypeID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="262px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="StaffLevelID" meta:resourcekey="CostLimitGridView_TemplateField_StaffLevel">
                        <EditItemTemplate>
                            <asp:DropDownList runat="server" ID="ddlStaffLevel" DataSourceID="odsStaffLevel"
                                DataTextField="StaffLevelName" DataValueField="StaffLevelID" Width="260px" SelectedValue='<%# Bind("StaffLevelID") %>'>
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="odsStaffLevel" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                SelectCommand="SELECT [StaffLevelID], [StaffLevelName] FROM [StaffLevel] where IsActive= '1'">
                            </asp:SqlDataSource>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblStaffLevelName" runat="server" Text='<%# GetStaffLevelNameByID(Eval("StaffLevelID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="282px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="ManageExpenseItemID" HeaderText="<%$ Resources:Common,Form_ExpenseItem %>">
                        <EditItemTemplate>
                            <asp:DropDownList runat="server" ID="ddlManageExpenseItem" DataSourceID="odsManageExpenseItem"
                                DataTextField="ManageExpenseItemName" DataValueField="ManageExpenseItemID" Width="240px"
                                SelectedValue='<%# Bind("ManageExpenseItemID") %>'>
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="odsManageExpenseItem" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                SelectCommand="SELECT [ManageExpenseItemID], [ManageExpenseItemName] FROM [ManageExpenseItem] where IsActive= '1'">
                            </asp:SqlDataSource>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblManageExpenseItem" runat="server" Text='<%# GetManageExpenseItemNameByID(Eval("ManageExpenseItemID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="268px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="LimitCost" meta:resourcekey="CostLimitGridView_TemplateField_LimitCost">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtLimitCost" runat="server" Text='<%# Bind("LimitCost") %>' Width="240px"
                                CssClass="InputText" MaxLength="50"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RF3" runat="server" ControlToValidate="txtLimitCost"
                                ValidationExpression="<%$ Resources:RegularExpressions, Money %>" Display="None"
                                meta:resourcekey="RegularExpressionValidator_Money" ValidationGroup="EDIT" ></asp:RegularExpressionValidator>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblLimitCost" runat="server" Text='<%# Bind("LimitCost") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="262px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <EditItemTemplate>
                            <asp:RequiredFieldValidator ID="RFAccommodationCost" runat="server" ControlToValidate="txtLimitCost"
                                Display="None" meta:resourcekey="RequiredFieldValidator_AccommodationCost" SetFocusOnError="True" ValidationGroup="EDIT"></asp:RequiredFieldValidator>
                            <asp:ValidationSummary ID="vsAccommodationCostEditEdit" runat="server" ShowMessageBox="True"
                                ShowSummary="False" ValidationGroup="EDIT" />
                            <asp:LinkButton ID="LinkButton1" Visible="<%# HasManageRight %>" runat="server" CausesValidation="True"
                                meta:resourcekey="UpdateLinkButtonResource1" ValidationGroup="EDIT" CommandName="Update"
                                Text='<%$Resources:Common,Button_Update %>'></asp:LinkButton>
                            <asp:LinkButton ID="LinkButton3" Visible="<%# HasManageRight %>" runat="server" CausesValidation="false"
                                CommandName="Cancel" Text='<%$Resources:Common,Button_Cancel %>'></asp:LinkButton>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" Visible="<%# HasManageRight %>" runat="server" CausesValidation="false"
                                CommandName="Edit" Text='<%$Resources:Common,Button_Edit %>'></asp:LinkButton>&nbsp;
                            <asp:LinkButton ID="DeleteButton" runat="server" CausesValidation="False" CommandName="Delete" Visible="<%# HasManageRight %>"
                                Text='<%$Resources:Common,Button_Delete %>' OnClientClick="return confirm('确定删除此行数据吗？');"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="160px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td style="width: 262px;" class="Empty1">
                                <asp:Label ID="Label2" runat="server" meta:resourcekey="CostLimit_Label_CityTypeName"></asp:Label>
                            </td>
                            <td style="width: 282px;">
                                <asp:Label ID="Label1" runat="server" meta:resourcekey="CostLimit_Label_StaffLevel"></asp:Label>
                            </td>
                            <td style="width: 282px;">
                                <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Common,Form_ExpenseItem %>"></asp:Label>
                            </td>
                            <td style="width: 262px;">
                                <asp:Label ID="Label4" runat="server" meta:resourcekey="CostLimit_Label_LimitCost"></asp:Label>
                            </td>
                            <td style="width: 160px;">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <SelectedRowStyle CssClass="SelectedRow" />
            </gc:GridView>
            <asp:FormView ID="fvCostLimit" runat="server" DataKeyNames="CostLimitID" DataSourceID="odsCostLimit"
                DefaultMode="Insert" EnableModelValidation="True" CellPadding="0" Visible="<%# HasManageRight %>" >
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr>
                            <td align="center" style="height: 22px; width: 262px;">
                                <asp:DropDownList runat="server" ID="ddlCityType" DataSourceID="odsCityType" DataTextField="CityTypeName"
                                    DataValueField="CityTypeID" Width="240px" SelectedValue='<%# Bind("CityTypeID") %>'>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="odsCityType" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                    SelectCommand="select 0 CityTypeID,' 不适用' CityTypeName Union SELECT [CityTypeID], [CityTypeName] FROM [CityType] where IsActive= 1">
                                </asp:SqlDataSource>
                            </td>
                            <td align="center" style="height: 22px; width: 282px;">
                                <asp:DropDownList runat="server" ID="ddlStaffLevel" DataSourceID="odsStaffLevel"
                                    DataTextField="StaffLevelName" DataValueField="StaffLevelID" SelectedValue='<%# Bind("StaffLevelID") %>'
                                    Width="260px">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="odsStaffLevel" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                    SelectCommand="SELECT [StaffLevelID], [StaffLevelName] FROM [StaffLevel] where IsActive= '1'">
                                </asp:SqlDataSource>
                            </td>
                            <td align="center" style="height: 22px; width: 268px;">
                                <asp:DropDownList runat="server" ID="ddlManageExpenseItem" DataSourceID="odsManageExpenseItem"
                                    DataTextField="ManageExpenseItemName" DataValueField="ManageExpenseItemID" SelectedValue='<%# Bind("ManageExpenseItemID") %>'
                                    Width="240px">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="odsManageExpenseItem" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                    SelectCommand="SELECT [ManageExpenseItemID], [ManageExpenseItemName] FROM [ManageExpenseItem] where IsActive= '1'">
                                </asp:SqlDataSource>
                            </td>
                            <td align="center" style="height: 22px; width: 262px;">
                                <asp:TextBox ID="txtLimitCost" runat="server" Text='<%# Bind("LimitCost") %>' Width="240px"
                                    CssClass="InputText" MaxLength="10"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 160px;">
                                <asp:LinkButton ID="InsertLinkButton" runat="server" CommandName="Insert" Text='<%$Resources:Common,Button_Add %>'
                                    ValidationGroup="INS" meta:resourcekey="InsertButtonResource1"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                    <asp:RequiredFieldValidator ID="NewRF1" runat="server" ControlToValidate="txtLimitCost"
                        Display="None" meta:resourcekey="RequiredFieldValidator_AccommodationCost" SetFocusOnError="True" 
                        ValidationGroup="INS"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="NewRF2" runat="server" ControlToValidate="txtLimitCost"
                        ValidationExpression="<%$ Resources:RegularExpressions, Money %>" Display="None"
                        meta:resourcekey="RegularExpressionValidator_Money" ValidationGroup="INS"></asp:RegularExpressionValidator>
                    <asp:ValidationSummary ID="ValidationSummaryINS" runat="server" ShowMessageBox="True"
                        ShowSummary="False" ValidationGroup="INS" />
                </InsertItemTemplate>
            </asp:FormView>
            <asp:ObjectDataSource ID="odsCostLimit" runat="server" TypeName="BusinessObjects.MasterDataBLL"
                SortParameterName="sortExpression" SelectMethod="GetPagedCostLimit" InsertMethod="InsertCostLimit"
                UpdateMethod="UpdateCostLimit" EnablePaging="true" SelectCountMethod="QueryCostLimitCount" DeleteMethod="DeleteCostLimitByID">
                <UpdateParameters>
                    <asp:Parameter Name="CostLimitID" Type="Int32" />
                    <asp:Parameter Name="CityTypeID" Type="Int32" />
                    <asp:Parameter Name="StaffLevelID" Type="Int32" />
                    <asp:Parameter Name="ManageExpenseItemID" Type="Int32" />
                    <asp:Parameter Name="LimitCost" Type="Decimal" />
                </UpdateParameters>
                <InsertParameters>
                    <asp:Parameter Name="CityTypeID" Type="Int32" />
                    <asp:Parameter Name="StaffLevelID" Type="Int32" />
                    <asp:Parameter Name="ManageExpenseItemID" Type="Int32" />
                    <asp:Parameter Name="LimitCost" Type="Decimal" />
                </InsertParameters>
                <SelectParameters>
                    <asp:Parameter Name="queryExpression" Type="String" />
                </SelectParameters>
                <DeleteParameters>
                    <asp:Parameter Name="CostLimitID" Type="Int32" />
                </DeleteParameters>
            </asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
