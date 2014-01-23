<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Culture="auto" UICulture="auto" CodeFile="Item.aspx.cs" Inherits="BaseData_Item" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="title" >
        <asp:Label ID="Label_SearchCondition" runat="server" Text="<%$Resources:Common,Label_SearchCondition %>" /></div>
    <div class="searchDiv" style="width: 1240px;">
        <table class="searchTable" cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 200px;" class="field_title">
                    <asp:Label ID="Label2" runat="server" meta:resourcekey="Item_Label_ItemCode"/>
                </td>
                <td style="width: 200px;" class="field_title">
                    <asp:Label ID="Label5" runat="server" meta:resourcekey="Item_Label_ItemName"/>
                </td>
                <td style="width: 220px;" class="field_title">
                    <asp:Label ID="Label7" runat="server" meta:resourcekey="Item_Label_ItemCategory"/>
                </td>
                <td style="width: 200px;" class="field_title">
                    <asp:Label ID="Label8" runat="server" meta:resourcekey="Item_Label_UOM"/>
                </td>
                <td style="width: 200px;" class="field_title">
                    <asp:Label ID="Label9" runat="server" meta:resourcekey="Item_Label_Package"/>
                </td>
                <td style="width: 200px;" class="field_title">
                    <asp:Label ID="Label11" runat="server" Text="<%$ Resources:Common,Form_IsActive %>"/>
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top">
                    <asp:TextBox runat="server" ID="txtItemCodeBySearch" Width="160px"></asp:TextBox>
                </td>
                <td style="vertical-align: top">
                    <asp:TextBox runat="server" ID="txtItemNameBySearch" Width="160px"></asp:TextBox>
                </td>
                <td style="vertical-align: top">
                    <asp:DropDownList runat="server" ID="dplItemCategoryBySearch" DataSourceID="odsItemCategory"
                        DataTextField="ItemCategoryName" DataValueField="ItemCategoryID" Width="200px"
                        AppendDataBoundItems="true">
                        <asp:ListItem Selected="True" Text="All" Value=""></asp:ListItem>
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="odsItemCategory" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                        SelectCommand="select ItemCategoryID,ItemCategoryName from dbo.ItemCategory order by ItemCategoryName">
                    </asp:SqlDataSource>
                </td>
                <td style="vertical-align: top">
                    <asp:TextBox runat="server" ID="txtUOMBySearch" Width="160px"></asp:TextBox>
                </td>
                <td style="vertical-align: top">
                    <asp:TextBox runat="server" ID="txtPackageBySearch" Width="160px"></asp:TextBox>
                </td>
                <td style="vertical-align: top">
                    <asp:DropDownList runat="server" ID="dplActiveBySearch" Width="160px">
                        <asp:ListItem Selected="True" Text="All" Value="3"></asp:ListItem>
                        <asp:ListItem Text="<%$ Resources:Common,Form_IsActive %>" Value="1"></asp:ListItem>
                        <asp:ListItem meta:resourcekey="Label_NoActive" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </div>
    <div style="margin-top: 5px; width: 1200px; text-align: right;">
        <asp:Button runat="server" ID="lbtnSearch" Text="<%$Resources:Common,Button_Search %>" CssClass="button_nor" OnClick="lbtnSearch_Click">
        </asp:Button></div>
    <br />
    <div class="title">
        <asp:Label ID="Label6" runat="server" meta:resourcekey="Label_Title"></asp:Label></div>
    <asp:UpdatePanel ID="upItem" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <gc:GridView ID="ItemView" CssClass="GridView" runat="server" DataSourceID="ItemObjectDataSource"
                AllowPaging="true" PageSize="20" AllowSorting="True" AutoGenerateColumns="False"
                DataKeyNames="ItemID" CellPadding="0">
                <Columns>
                    <asp:TemplateField meta:resourcekey="ItemGridView_TemplateField_ItemName" SortExpression="ItemName">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtItemName" runat="server" CssClass="InputText" MaxLength="20"
                                Text='<%# Bind("ItemName") %>' Width="150px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="ItemNameRequiredFieldValidator" runat="server" ControlToValidate="txtItemName"
                                Display="None" meta:resourcekey="RequiredFieldValidator_ItemName" SetFocusOnError="True" ValidationGroup="EDIT"></asp:RequiredFieldValidator>
                        </EditItemTemplate>
                        <ItemStyle Width="200px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblItemName" runat="server" Text='<%# Bind("ItemName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="ItemGridView_TemplateField_ItemCode" SortExpression="ItemCode">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtItemCode" runat="server" Text='<%# Bind("ItemCode") %>' CssClass="InputText"
                                Width="110px" MaxLength="10"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="ItemCodeRequiredFieldValidator" runat="server" ControlToValidate="txtItemCode"
                                Display="None" meta:resourcekey="RequiredFieldValidator_ItemCode" SetFocusOnError="True" ValidationGroup="EDIT"></asp:RequiredFieldValidator>
                        </EditItemTemplate>
                        <ItemStyle Width="140px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblItemCode" runat="server" Text='<%# Bind("ItemCode") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="ItemGridView_TemplateField_ItemCategory" SortExpression="ItemCode">
                        <EditItemTemplate>
                            <asp:DropDownList runat="server" ID="ddlItemCategory" DataTextField="ItemCategoryName"
                                DataValueField="ItemCategoryID" Width="170px" DataSourceID="odsItemCategory"
                                SelectedValue='<%# Bind("ItemCategoryID") %>'>
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="odsItemCategory" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                SelectCommand="select ItemCategoryID,ItemCategoryName from dbo.ItemCategory  order by ItemCategoryName">
                            </asp:SqlDataSource>
                        </EditItemTemplate>
                        <ItemStyle Width="200px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblItemCategory" runat="server" Text='<%# GetItemCategoryByID(Eval("ItemCategoryID")) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="ItemGridView_TemplateField_UOM" SortExpression="UOM">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtUOM" runat="server" CssClass="InputText" MaxLength="20" Text='<%# Bind("UOM") %>'
                                Width="80px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblUOM" runat="server" Text='<%# Bind("UOM") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="ItemGridView_TemplateField_Package" SortExpression="Package">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtPackage" runat="server" CssClass="InputText" MaxLength="20" Text='<%# Bind("Package") %>'
                                Width="80px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblPackage" runat="server" Text='<%# Bind("Package") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="ItemGridView_TemplateField_UnitPrice" SortExpression="UnitPrice">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtUnitPrice" runat="server" CssClass="InputText" MaxLength="20"
                                Text='<%# Bind("UnitPrice") %>' Width="80px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblUnitPrice" runat="server" Text='<%# Bind("UnitPrice") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="ItemCategoryGridView_TemplateField_Description"
                        SortExpression="Description">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtDescription" runat="server" CssClass="InputText" MaxLength="20"
                                Text='<%# Bind("Description") %>' Width="170px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle Width="200px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                        </ItemTemplate>
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
                    <asp:TemplateField>
                        <EditItemTemplate>
                            <asp:LinkButton ID="LinkButton1" Visible="<%# HasManageRight %>" runat="server" CausesValidation="True"
                                ValidationGroup="skuEdit" CommandName="Update" Text='<%$Resources:Common,Button_Update %>'></asp:LinkButton>
                            <asp:LinkButton ID="LinkButton3" Visible="<%# HasManageRight %>" runat="server" CausesValidation="false"
                                CommandName="Cancel" Text='<%$Resources:Common,Button_Cancel %>'></asp:LinkButton>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton2" Visible="false" runat="server" CausesValidation="false"
                                CommandName="Edit" Text='<%$Resources:Common,Button_Edit %>'></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td scope="col" style="width: 200px" class="Empty1">
                                <asp:Label ID="Label1" runat="server" meta:resourcekey="Item_Label_ItemName"></asp:Label>
                            </td>
                            <td scope="col" style="width: 140px">
                                <asp:Label ID="Label2" runat="server" meta:resourcekey="Item_Label_ItemCode"></asp:Label>
                            </td>
                            <td scope="col" style="width: 200px">
                                <asp:Label ID="Label7" runat="server" meta:resourcekey="Item_Label_ItemCategory"></asp:Label>
                            </td>
                            <td scope="col" style="width: 100px">
                                <asp:Label ID="Label8" runat="server" meta:resourcekey="Item_Label_UOM"></asp:Label>
                            </td>
                            <td scope="col" style="width: 100px">
                                <asp:Label ID="Label9" runat="server" meta:resourcekey="Item_Label_Package"></asp:Label>
                            </td>
                            <td scope="col" style="width: 100px">
                                <asp:Label ID="Label10" runat="server" meta:resourcekey="Item_Label_UnitPrice"></asp:Label>
                            </td>
                            <td scope="col" style="width: 200px">
                                <asp:Label ID="Label3" runat="server" meta:resourcekey="Item_Label_Description"></asp:Label>
                            </td>
                            <td scope="col" style="width: 70px">
                                <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Common,Form_IsActive %>"></asp:Label>
                            </td>
                            <td scope="col" style="width: 100px">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
            </gc:GridView>
            <asp:FormView ID="ItemFormView" runat="server" DataKeyNames="ItemID" CellPadding="0"
                CellSpacing="0" DataSourceID="ItemObjectDataSource" DefaultMode="Insert" Visible="false">
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr>
                            <td align="center" style="width: 200px">
                                <asp:TextBox ID="txtItemName" runat="server" Text='<%# Bind("ItemName") %>' CssClass="InputText"
                                    Width="150px" ValidationGroup="INS"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 140px">
                                <asp:TextBox ID="txtItemCode" runat="server" Text='<%# Bind("ItemCode") %>' CssClass="InputText"
                                    Width="100px" ValidationGroup="INS" MaxLength="10"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 200px">
                                <asp:DropDownList runat="server" ID="ddlItemCategory" DataTextField="ItemCategoryName"
                                    DataValueField="ItemCategoryID" Width="170px" DataSourceID="odsItemCategory"
                                    SelectedValue='<%# Bind("ItemCategoryID") %>'>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="odsItemCategory" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                    SelectCommand="select ItemCategoryID,ItemCategoryName from dbo.ItemCategory  order by ItemCategoryName">
                                </asp:SqlDataSource>
                            </td>
                            <td align="center" style="width: 100px">
                                <asp:TextBox ID="txtUOM" runat="server" Text='<%# Bind("UOM") %>' CssClass="InputText"
                                    Width="80px" ValidationGroup="INS" MaxLength="10"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 100px">
                                <asp:TextBox ID="txtPackage" runat="server" Text='<%# Bind("Package") %>' CssClass="InputText"
                                    Width="80px" ValidationGroup="INS" MaxLength="10"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 100px">
                                <asp:TextBox ID="txtUnitPrice" runat="server" Text='<%# Bind("UnitPrice") %>' CssClass="InputText"
                                    Width="80px" ValidationGroup="INS" MaxLength="10"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 200px">
                                <asp:TextBox ID="txtDescription" runat="server" Text='<%# Bind("Description") %>'
                                    CssClass="InputText" Width="170px" ValidationGroup="INS" MaxLength="100"></asp:TextBox>
                            </td>
                            <td align="center" style="height: 22px; width: 70px;">
                                <asp:CheckBox runat="server" ID="chkActiveByAdd" Checked='<%# Bind("IsActive") %>' />
                            </td>
                            <td align="center" style="width: 100px">
                                <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    Text='<%$Resources:Common,Button_Add %>' ValidationGroup="INS"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                    <asp:RequiredFieldValidator ID="ItemNameRequiredFieldValidator" runat="server" ControlToValidate="txtItemName"
                        Display="None" meta:resourcekey="RequiredFieldValidator_ItemName" SetFocusOnError="True" ValidationGroup="INS"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="ItemCodeRequiredFieldValidator" runat="server" ControlToValidate="txtItemCode"
                        Display="None" meta:resourcekey="RequiredFieldValidator_ItemCode" SetFocusOnError="True" ValidationGroup="INS"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUOM"
                        Display="None" meta:resourcekey="RequiredFieldValidator_UOM" SetFocusOnError="True" ValidationGroup="INS"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPackage"
                        Display="None" meta:resourcekey="RequiredFieldValidator_Package" SetFocusOnError="True" ValidationGroup="INS"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtUnitPrice"
                        Display="None" meta:resourcekey="RequiredFieldValidator_UnitPrice" SetFocusOnError="True" ValidationGroup="INS"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtUnitPrice"
                        ValidationExpression="<%$ Resources:RegularExpressions, Money %>" Display="None"
                        meta:resourcekey="RegularExpressionValidator_Money" ValidationGroup="NewDetailRow"></asp:RegularExpressionValidator>
                    <asp:ValidationSummary ID="ValidationSummaryInsert" runat="server" ShowMessageBox="True"
                        ShowSummary="False" ValidationGroup="INS" />
                </InsertItemTemplate>
            </asp:FormView>
            <asp:ObjectDataSource ID="ItemObjectDataSource" runat="server" InsertMethod="InsertItem"
                EnablePaging="true" SelectMethod="GetItemPaged" SelectCountMethod="ItemCount"
                SortParameterName="sortExpression" TypeName="BusinessObjects.MasterDataBLL"
                UpdateMethod="UpdateItem">
                <UpdateParameters>
                    <asp:Parameter Name="ItemID" Type="Int32" />
                    <asp:Parameter Name="ItemCode" Type="String" />
                    <asp:Parameter Name="ItemCategoryID" Type="Int32" />
                    <asp:Parameter Name="UOM" Type="String" />
                    <asp:Parameter Name="Package" Type="String" />
                    <asp:Parameter Name="UnitPrice" Type="Decimal" />
                    <asp:Parameter Name="Description" Type="String" />
                    <asp:Parameter Name="IsActive" Type="Boolean" />
                </UpdateParameters>
                <InsertParameters>
                    <asp:Parameter Name="ItemCode" Type="String" />
                    <asp:Parameter Name="ItemCategoryID" Type="Int32" />
                    <asp:Parameter Name="UOM" Type="String" />
                    <asp:Parameter Name="Package" Type="String" />
                    <asp:Parameter Name="UnitPrice" Type="Decimal" />
                    <asp:Parameter Name="Description" Type="String" />
                    <asp:Parameter Name="IsActive" Type="Boolean" />
                </InsertParameters>
                <SelectParameters>
                    <asp:Parameter Name="queryExpression" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
