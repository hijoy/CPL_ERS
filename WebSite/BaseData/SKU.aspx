<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Culture="auto" UICulture="auto" CodeFile="SKU.aspx.cs" Inherits="BaseData_SKU" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="title" style="width: 1240px;">
        <asp:Label ID="Label_SearchCondition" runat="server" Text="<%$Resources:Common,Label_SearchCondition %>" /></div>
    <div class="searchDiv" style="width: 1240px;">
        <table class="searchTable" cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 240px;" class="field_title">
                    <asp:Label ID="Label5" runat="server" meta:resourcekey="Label_SKUNo" />
                </td>
                <td style="width: 240px;" class="field_title">
                    <asp:Label ID="Label1" runat="server" Text="<%$Resources:Common,Form_ProductName %>" />
                </td>
                <td style="width: 240px;" class="field_title">
                    <asp:Label ID="Label7" runat="server" Text="<%$Resources:Common,Form_Brand %>" />
                </td>
                <td style="width: 240px;" class="field_title">
                    <asp:Label ID="Label9" runat="server" meta:resourcekey="Label_SKUType" />
                </td>
                <td style="width: 240px;" class="field_title">
                    <asp:Label ID="Label10" runat="server" meta:resourcekey="Label_SKUCategory" />
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top">
                    <asp:TextBox runat="server" ID="txtSKUNoBySearch" Width="160px"></asp:TextBox>
                </td>
                <td style="vertical-align: top">
                    <asp:TextBox runat="server" ID="txtSKUNameBySearch" Width="160px"></asp:TextBox>
                </td>
                <td style="vertical-align: top">
                    <asp:DropDownList runat="server" ID="ddlBrandBySearch" DataSourceID="sdsBrand" DataTextField="BrandName"
                        DataValueField="BrandID" Width="160px" AppendDataBoundItems="true">
                        <asp:ListItem Selected="True" Text="All" Value=""></asp:ListItem>
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="sdsBrand" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                        SelectCommand="select BrandID,BrandName from Brand order by BrandName"></asp:SqlDataSource>
                </td>
                <td style="vertical-align: top">
                    <asp:DropDownList runat="server" ID="ddlSKUTypeBySearch" DataTextField="SKUTypeName"
                        DataValueField="SKUTypeID" Width="160px" DataSourceID="odsSKUType" AppendDataBoundItems="true">
                        <asp:ListItem Selected="True" Text="All" Value=""></asp:ListItem>
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="odsSKUType" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                        SelectCommand="select SKUTypeID,SKUTypeName from dbo.SKUType  order by SKUTypeName">
                    </asp:SqlDataSource>
                </td>
                <td style="vertical-align: top">
                    <asp:DropDownList runat="server" ID="ddlSKUCategoryBySearch" DataTextField="SKUCategoryName"
                        AppendDataBoundItems="true" DataValueField="SKUCategoryID" Width="200px" DataSourceID="odsSKUCategory">
                        <asp:ListItem Selected="True" Text="All" Value=""></asp:ListItem>
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="odsSKUCategory" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                        SelectCommand="select SKUCategoryID,SKUCategoryName from dbo.SKUCategory   order by SKUCategoryName">
                    </asp:SqlDataSource>
                </td>
            </tr>
        </table>
    </div>
    <div style="margin-top: 5px; width: 1200px; text-align: right;">
        <asp:Button runat="server" ID="SearchButton" Text="<%$Resources:Common,Button_Search %>"
            CssClass="button_nor" OnClick="SearchButton_Click"></asp:Button></div>
    <br />
    <div class="title" style="width: 1260px;">
        <asp:Label ID="Label8" runat="server" meta:resourcekey="Label_Title" />
    </div>
    <asp:UpdatePanel ID="SKUUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <gc:GridView ID="SKUGridView" runat="server" AllowPaging="True" AllowSorting="True"
                AutoGenerateColumns="False" DataKeyNames="SKUID" DataSourceID="SKUObjectDataSource"
                PageSize="20" ShowFooter="True" CssClass="GridView" OnSelectedIndexChanged="SKUGridView_SelectedIndexChanged"
                OnRowDataBound="SKUGridView_RowDataBound">
                <Columns>
                    <asp:TemplateField meta:resourcekey="TemplateField_SKUNo" SortExpression="SKUNo">
                        <ItemTemplate>
                            <asp:Label ID="lblSKUNo" runat="server" Text='<%# Bind("SKUNo") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="162px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Common,Form_ProductName %>" SortExpression="SKUName">
                        <ItemTemplate>
                            <asp:LinkButton ID="SKULinkButton" runat="server" CommandName="Select" OnClick="SKULinkButton_Click"
                                Text='<%# Bind("SKUName") %>'></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="200px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Common,Form_Brand %>" SortExpression="BrandID">
                        <ItemTemplate>
                            <asp:Label ID="lblBrand" runat="server" Text='<%# GetBrandByID(Eval("BrandID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="220px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateField_SKUType" SortExpression="SKUTypeID">
                        <ItemTemplate>
                            <asp:Label ID="lblSKUType" runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="150px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateField_SKUCategory" SortExpression="SKUCategoryID">
                        <ItemTemplate>
                            <asp:Label ID="lblSKUCategory" runat="server" Text='<%# GetSKUCategoryByID(Eval("SKUCategoryID"))%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="250px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Common,Form_IsActive %>">
                        <ItemTemplate>
                            <asp:CheckBox ID="ckbIsActive" runat="server" Checked='<%# Bind("IsActive") %>' Enabled="false" />
                        </ItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <EditItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update"
                                Text="Update"></asp:LinkButton>
                            <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel"
                                Text="Cancel"></asp:LinkButton>
                        </EditItemTemplate>
                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:LinkButton Visible="<%# HasManageRight %>" ID="EditLinkButton" runat="server"
                                CausesValidation="False" CommandName="Select" Text="<%$Resources:Common,Button_Edit %>"
                                OnClick="EditLinkButton_Click"></asp:LinkButton>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:LinkButton Visible="false" ID="AddLinkButton1" runat="server" CausesValidation="True"
                                CommandName="Select" Text="<%$Resources:Common,Button_Add %>" OnClick="AddLinkButton_Click"></asp:LinkButton>
                        </FooterTemplate>
                        <FooterStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td style="width: 162px;" class="Empty1">
                                <asp:Label ID="Label5" runat="server" meta:resourcekey="Label_SKUNo" />
                            </td>
                            <td style="width: 200px;">
                                <asp:Label ID="Label1" runat="server" Text="<%$Resources:Common,Form_ProductName %>" />
                            </td>
                            <td style="width: 220px;">
                                <asp:Label ID="Label7" runat="server" Text="<%$Resources:Common,Form_Brand %>" />
                            </td>
                            <td style="width: 150px;">
                                <asp:Label ID="Label9" runat="server" meta:resourcekey="Label_SKUType" />
                            </td>
                            <td style="width: 250px;">
                                <asp:Label ID="Label10" runat="server" meta:resourcekey="Label_SKUCategory" />
                            </td>
                            <td style="width: 100px;">
                                <asp:Label ID="Label11" runat="server" Text="<%$Resources:Common,Form_IsActive %>" />
                            </td>
                            <td style="width: 80px;">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="7" class="Empty2 noneLabel">
                                <asp:Label ID="Label_None" runat="server" Text="<%$Resources:Common,Label_None %>" />
                            </td>
                            <td class="Empty2 noneLabel">
                                <asp:LinkButton Visible="false" ID="LinkButton4" runat="server" CausesValidation="True"
                                    CommandName="Select" Text="<%$Resources:Common,Button_Add %>" OnClick="AddLinkButton_Click"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <SelectedRowStyle CssClass="SelectedRow" />
                <HeaderStyle CssClass="Header" />
                <FooterStyle CssClass="Footer" />
            </gc:GridView>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="SearchButton" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="SKUFormView" EventName="ItemInserted" />
            <asp:AsyncPostBackTrigger ControlID="SKUFormView" EventName="ItemUpdated" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="SKUAddUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:FormView ID="SKUFormView" runat="server" DataKeyNames="SKUID" DataSourceID="SKUAddObjectDataSource"
                Width="1240px" OnDataBound="SKUFormView_DataBound">
                <EditItemTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr style="height: 30px">
                            <td align="left" style="text-align: right" width="22%">
                                &nbsp;<asp:Label ID="Label5" runat="server" meta:resourcekey="Label_SKUNo" />
                            </td>
                            <td align="left" width="28%">
                                &nbsp;<asp:TextBox ID="txtSKUNo" runat="server" Text='<%# Bind("SKUNo") %>' ValidationGroup="EDIT"
                                    Width="200px" MaxLength="50" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td align="left" style="text-align: right" width="22%">
                                &nbsp;<asp:Label ID="Label1" runat="server" Text="<%$Resources:Common,Form_ProductName %>" />
                            </td>
                            <td align="left" width="28%">
                                &nbsp;<asp:TextBox ID="txtSKUName" runat="server" Text='<%# Bind("SKUName") %>' ValidationGroup="EDIT"
                                    Width="200px" MaxLength="50" ReadOnly="true"></asp:TextBox>&nbsp;
                            </td>
                        </tr>
                        <tr style="height: 30px">
                            <td align="left" style="text-align: right">
                                &nbsp;<asp:Label ID="Label7" runat="server" Text="<%$Resources:Common,Form_Brand %>" />
                            </td>
                            <td align="left">
                                &nbsp;<asp:DropDownList runat="server" ID="ddlBrand" DataTextField="BrandName" DataValueField="BrandID"
                                    Width="200px" DataSourceID="odsBrand" SelectedValue='<%# Bind("BrandID") %>'
                                    Enabled="false">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="odsBrand" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                    SelectCommand="select BrandID,BrandName from dbo.Brand   order by BrandName">
                                </asp:SqlDataSource>
                            </td>
                            <td align="left" style="text-align: right">
                                &nbsp;<asp:Label ID="Label9" runat="server" meta:resourcekey="Label_SKUType" />
                            </td>
                            <td align="left">
                                &nbsp;<asp:DropDownList runat="server" ID="ddlSKUType" DataTextField="SKUTypeName"
                                    Enabled="false" DataValueField="SKUTypeID" Width="200px" DataSourceID="odsSKUType">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="odsSKUType" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                    SelectCommand="select 0 SKUTypeID,'' SKUTypeName union select SKUTypeID,SKUTypeName from dbo.SKUType  order by SKUTypeName">
                                </asp:SqlDataSource>
                            </td>
                        </tr>
                        <tr style="height: 30px">
                            <td align="left" style="text-align: right">
                                &nbsp;<asp:Label ID="Label10" runat="server" meta:resourcekey="Label_SKUCategory" />
                            </td>
                            <td align="left">
                                &nbsp;<asp:DropDownList runat="server" ID="ddlSKUCategory" DataTextField="SKUCategoryName"
                                    Enabled="false" DataValueField="SKUCategoryID" Width="200px" DataSourceID="odsSKUCategory"
                                    SelectedValue='<%# Bind("SKUCategoryID") %>'>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="odsSKUCategory" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                    SelectCommand="select SKUCategoryID,SKUCategoryName from dbo.SKUCategory  order by SKUCategoryName">
                                </asp:SqlDataSource>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="text-align: right">
                                &nbsp;<asp:Label ID="Label12" runat="server" meta:resourcekey="Label_Pallet" />
                            </td>
                            <td align="left">
                                &nbsp;<asp:TextBox ID="txtPallet" runat="server" Text='<%# Bind("Pallet") %>' Width="200px"
                                    MaxLength="50"></asp:TextBox>
                            </td>
                            <td align="left" style="text-align: right">
                                &nbsp;<asp:Label ID="Label13" runat="server" meta:resourcekey="Label_Weight" />
                            </td>
                            <td align="left">
                                &nbsp;<asp:TextBox ID="txtWeight" runat="server" Text='<%# Bind("Weight") %>' Width="200px"
                                    MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="text-align: right">
                                &nbsp;<asp:Label ID="Label14" runat="server" meta:resourcekey="Label_Volume" />
                            </td>
                            <td align="left">
                                &nbsp;<asp:TextBox ID="txtVolume" runat="server" Text='<%# Bind("Volume") %>' Width="200px"
                                    MaxLength="50"></asp:TextBox>
                            </td>
                            <td align="left" style="text-align: right">
                                &nbsp;<asp:Label ID="Label15" runat="server" meta:resourcekey="Label_Barcode" />
                            </td>
                            <td align="left">
                                &nbsp;<asp:TextBox ID="txtBarcode" runat="server" Text='<%# Bind("Barcode") %>' Width="200px"
                                    MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="text-align: right">
                                &nbsp;<asp:Label ID="Label16" runat="server" meta:resourcekey="Label_StandardCost" />
                            </td>
                            <td align="left">
                                &nbsp;<asp:TextBox ID="txtStandardCost" runat="server" Text='<%# Bind("StandardCost") %>'
                                    Width="200px" MaxLength="50"></asp:TextBox>
                            </td>
                            <td align="left" style="text-align: right">
                                <asp:Label ID="Label17" runat="server" meta:resourcekey="Label_ShelfLife" />
                            </td>
                            <td align="left">
                                &nbsp;<asp:TextBox ID="txtShelfLife" runat="server" Text='<%# Bind("ShelfLife") %>'
                                    Width="200px" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="text-align: right">
                                &nbsp;<asp:Label ID="Label11" runat="server" Text="<%$Resources:Common,Form_IsActive %>" />
                            </td>
                            <td align="left">
                                &nbsp;<asp:CheckBox ID="ckbIsActive" runat="server" Checked='<%# Bind("IsActive") %>'
                                    Enabled="false" />
                            </td>
                            <td align="left" style="text-align: right">
                                &nbsp;<asp:Label ID="Label18" runat="server" meta:resourcekey="Label_PackPerCase" />
                            </td>
                            <td align="left">
                                &nbsp;<asp:TextBox ID="txtPackPerCase" runat="server" Text='<%# Bind("PackPerCase") %>'
                                    Width="200px" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="height: 30px">
                            <td align="left" style="text-align: right">
                            </td>
                            <td align="left">
                            </td>
                            <td align="left" style="text-align: right">
                            </td>
                            <td align="left">
                                &nbsp;<asp:Button ID="UpdateButton" runat="server" CssClass="button_nor" Text="<%$Resources:Common,Button_Update %>"
                                    CommandName="Update" OnClick="InsertButton_Click" ValidationGroup="EDIT" />
                                <asp:Button ID="Button2" runat="server" CssClass="button_nor" Text="<%$Resources:Common,Button_Back %>"
                                    OnClick="BackButton_Click" CommandName="Cancel" />
                            </td>
                        </tr>
                    </table>
                    <asp:RequiredFieldValidator ID="SKUNoRequiredFieldValidator" runat="server" ControlToValidate="txtSKUNo"
                        Display="Dynamic" meta:resourcekey="RequiredFieldValidator_Code" SetFocusOnError="True"
                        ValidationGroup="EDIT"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="SKUNameRequiredFieldValidator" runat="server" ControlToValidate="txtSKUName"
                        Display="Dynamic" meta:resourcekey="RequiredFieldValidator_Name" SetFocusOnError="True"
                        ValidationGroup="EDIT"></asp:RequiredFieldValidator>
                    <asp:ValidationSummary ID="ValidationSummaryEdit" runat="server" ShowMessageBox="True"
                        ShowSummary="true" ValidationGroup="EDIT" />
                </EditItemTemplate>
                <InsertItemTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr style="height: 30px">
                            <td align="left" style="text-align: right" width="22%">
                                &nbsp;<asp:Label ID="Label5" runat="server" meta:resourcekey="Label_SKUNo" />
                            </td>
                            <td align="left" width="28%">
                                &nbsp;<asp:TextBox ID="txtSKUNo" runat="server" Text='<%# Bind("SKUNo") %>' ValidationGroup="INS"
                                    Width="200px" MaxLength="50"></asp:TextBox>
                            </td>
                            <td align="left" style="text-align: right" width="22%">
                                &nbsp;<asp:Label ID="Label1" runat="server" Text="<%$Resources:Common,Form_ProductName %>" />
                            </td>
                            <td align="left" width="28%">
                                &nbsp;<asp:TextBox ID="txtSKUName" runat="server" Text='<%# Bind("SKUName") %>' ValidationGroup="INS"
                                    Width="200px" MaxLength="50"></asp:TextBox>&nbsp;
                            </td>
                        </tr>
                        <tr style="height: 30px">
                            <td align="left" style="text-align: right">
                                &nbsp;<asp:Label ID="Label7" runat="server" Text="<%$Resources:Common,Form_Brand %>" />
                            </td>
                            <td align="left">
                                &nbsp;<asp:DropDownList runat="server" ID="ddlBrand" DataTextField="BrandName" Width="200px"
                                    DataValueField="BrandID" DataSourceID="odsBrand" SelectedValue='<%# Bind("BrandID") %>'>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="odsBrand" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                    SelectCommand="select BrandID,BrandName from dbo.Brand   order by BrandName">
                                </asp:SqlDataSource>
                            </td>
                            <td align="left" style="text-align: right">
                                &nbsp;<asp:Label ID="Label9" runat="server" meta:resourcekey="Label_SKUType" />
                            </td>
                            <td align="left">
                                &nbsp;<asp:DropDownList runat="server" ID="ddlSKUType" DataTextField="SKUTypeName"
                                    Width="200px" DataValueField="SKUTypeID" DataSourceID="odsSKUType" SelectedValue='<%# Bind("SKUTypeID") %>'>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="odsSKUType" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                    SelectCommand="select SKUTypeID,SKUTypeName from dbo.SKUType   order by SKUTypeName">
                                </asp:SqlDataSource>
                            </td>
                        </tr>
                        <tr style="height: 30px">
                            <td align="left" style="text-align: right">
                                &nbsp;<asp:Label ID="Label10" runat="server" meta:resourcekey="Label_SKUCategory" />
                            </td>
                            <td align="left">
                                &nbsp;<asp:DropDownList runat="server" ID="ddlSKUCategory" DataTextField="SKUCategoryName"
                                    Width="200px" DataValueField="SKUCategoryID" DataSourceID="odsSKUCategory" SelectedValue='<%# Bind("SKUCategoryID") %>'>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="odsSKUCategory" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                    SelectCommand="select SKUCategoryID,SKUCategoryName from dbo.SKUCategory  order by SKUCategoryName">
                                </asp:SqlDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="text-align: right">
                                &nbsp;<asp:Label ID="Label12" runat="server" meta:resourcekey="Label_Pallet" />
                            </td>
                            <td align="left">
                                &nbsp;<asp:TextBox ID="txtPallet" runat="server" Text='<%# Bind("Pallet") %>' Width="200px"
                                    MaxLength="50"></asp:TextBox>
                            </td>
                            <td align="left" style="text-align: right">
                                &nbsp;<asp:Label ID="Label13" runat="server" meta:resourcekey="Label_Weight" />
                            </td>
                            <td align="left">
                                &nbsp;<asp:TextBox ID="txtWeight" runat="server" Text='<%# Bind("Weight") %>' Width="200px"
                                    MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="text-align: right">
                                &nbsp;<asp:Label ID="Label14" runat="server" meta:resourcekey="Label_Volume" />
                            </td>
                            <td align="left">
                                &nbsp;<asp:TextBox ID="txtVolume" runat="server" Text='<%# Bind("Volume") %>' Width="200px"
                                    MaxLength="50"></asp:TextBox>
                            </td>
                            <td align="left" style="text-align: right">
                                <asp:Label ID="Label15" runat="server" meta:resourcekey="Label_Barcode" />
                            </td>
                            <td align="left">
                                &nbsp;<asp:TextBox ID="txtBarcode" runat="server" Text='<%# Bind("Barcode") %>' Width="200px"
                                    MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="text-align: right">
                                &nbsp;<asp:Label ID="Label16" runat="server" meta:resourcekey="Label_StandardCost" />
                            </td>
                            <td align="left">
                                &nbsp;<asp:TextBox ID="txtStandardCost" runat="server" Text='<%# Bind("StandardCost") %>'
                                    Width="200px" MaxLength="50"></asp:TextBox>
                            </td>
                            <td align="left" style="text-align: right">
                                <asp:Label ID="Label17" runat="server" meta:resourcekey="Label_ShelfLife" />
                            </td>
                            <td align="left">
                                &nbsp;<asp:TextBox ID="txtShelfLife" runat="server" Text='<%# Bind("ShelfLife") %>'
                                    Width="200px" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="text-align: right">
                                &nbsp;<asp:Label ID="Label11" runat="server" Text="<%$Resources:Common,Form_IsActive %>" />
                            </td>
                            <td align="left">
                                &nbsp;<asp:CheckBox ID="ckbIsActive" runat="server" Checked='<%# Bind("IsActive") %>' />
                            </td>
                            <td align="left" style="text-align: right">
                                &nbsp;<asp:Label ID="Label18" runat="server" meta:resourcekey="Label_PackPerCase" />
                            </td>
                            <td align="left">
                                &nbsp;<asp:TextBox ID="txtPackPerCase" runat="server" Text='<%# Bind("PackPerCase") %>'
                                    Width="200px" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="height: 30px">
                            <td align="left" style="text-align: right">
                            </td>
                            <td align="left">
                            </td>
                            <td align="left" style="text-align: right">
                            </td>
                            <td align="left">
                                &nbsp;<asp:Button ID="InsertButton" runat="server" CssClass="button_nor" Text="<%$Resources:Common,Button_Save %>"
                                    CommandName="Insert" OnClick="InsertButton_Click" ValidationGroup="INS" />
                                <asp:Button ID="BackButton" runat="server" CssClass="button_nor" Text="<%$Resources:Common,Button_Back %>"
                                    OnClick="BackButton_Click" CommandName="Cancel" />
                            </td>
                        </tr>
                    </table>
                    <asp:RequiredFieldValidator ID="SKUNoRequiredFieldValidator" runat="server" ControlToValidate="txtSKUNo"
                        Display="Dynamic" meta:resourcekey="RequiredFieldValidator_Code" SetFocusOnError="True"
                        ValidationGroup="INS"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="SKUNameRequiredFieldValidator" runat="server" ControlToValidate="txtSKUName"
                        Display="Dynamic" meta:resourcekey="RequiredFieldValidator_Name" SetFocusOnError="True"
                        ValidationGroup="INS"></asp:RequiredFieldValidator>
                    <asp:ValidationSummary ID="ValidationSummaryInsert" runat="server" ShowMessageBox="True"
                        ShowSummary="False" ValidationGroup="INS" />
                </InsertItemTemplate>
                <ItemTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr style="height: 30px">
                            <td align="left" style="text-align: right" width="22%">
                                &nbsp;<asp:Label ID="Label5" runat="server" meta:resourcekey="Label_SKUNo" />
                            </td>
                            <td align="left" width="28%">
                                &nbsp;<asp:TextBox ID="txtSKUNo" runat="server" ReadOnly="true" Text='<%# Bind("SKUNo") %>'
                                    Width="200px" MaxLength="50"></asp:TextBox>
                            </td>
                            <td align="left" style="text-align: right" width="22%">
                                &nbsp;<asp:Label ID="Label1" runat="server" Text="<%$Resources:Common,Form_ProductName %>" />
                            </td>
                            <td align="left" width="28%">
                                &nbsp;<asp:TextBox ID="txtSKUName" runat="server" ReadOnly="true" Text='<%# Bind("SKUName") %>'
                                    Width="200px" MaxLength="50"></asp:TextBox>&nbsp;
                            </td>
                        </tr>
                        <tr style="height: 30px">
                            <td align="left" style="text-align: right">
                                &nbsp;<asp:Label ID="Label7" runat="server" Text="<%$Resources:Common,Form_Brand %>" />
                            </td>
                            <td align="left">
                                &nbsp;<asp:DropDownList runat="server" ID="ddlBrand" DataTextField="BrandName" Enabled="false"
                                    Width="200px" DataValueField="BrandID" DataSourceID="odsBrand" SelectedValue='<%# Bind("BrandID") %>'>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="odsBrand" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                    SelectCommand="select BrandID,BrandName from dbo.Brand  order by BrandName">
                                </asp:SqlDataSource>
                            </td>
                            <td align="left" style="text-align: right">
                                &nbsp;<asp:Label ID="Label9" runat="server" meta:resourcekey="Label_SKUType" />
                            </td>
                            <td align="left">
                                &nbsp;<asp:DropDownList runat="server" ID="ddlSKUType" DataTextField="SKUTypeName"
                                    Enabled="false" Width="200px" DataValueField="SKUTypeID" DataSourceID="odsSKUType">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="odsSKUType" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                    SelectCommand="select 0 SKUTypeID,'' SKUTypeName union Select SKUTypeID,SKUTypeName from dbo.SKUType   order by SKUTypeName">
                                </asp:SqlDataSource>
                            </td>
                        </tr>
                        <tr style="height: 30px">
                            <td align="left" style="text-align: right">
                                &nbsp;<asp:Label ID="Label10" runat="server" meta:resourcekey="Label_SKUCategory" />
                            </td>
                            <td align="left">
                                &nbsp;<asp:DropDownList runat="server" ID="ddlSKUCategory" DataTextField="SKUCategoryName"
                                    Enabled="false" Width="200px" DataValueField="SKUCategoryID" DataSourceID="odsSKUCategory"
                                    SelectedValue='<%# Bind("SKUCategoryID") %>'>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="odsSKUCategory" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                    SelectCommand="select SKUCategoryID,SKUCategoryName from dbo.SKUCategory  order by SKUCategoryName">
                                </asp:SqlDataSource>
                            </td>
                            <td align="left" style="text-align: right">
                            </td>
                            <td align="left">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="text-align: right">
                                &nbsp;<asp:Label ID="Label12" runat="server" meta:resourcekey="Label_Pallet" />
                            </td>
                            <td align="left">
                                &nbsp;<asp:TextBox ID="txtPallet" ReadOnly="true" runat="server" Text='<%# Bind("Pallet") %>'
                                    Width="200px" MaxLength="50"></asp:TextBox>
                            </td>
                            <td align="left" style="text-align: right">
                                &nbsp;<asp:Label ID="Label13" runat="server" meta:resourcekey="Label_Weight" />
                            </td>
                            <td align="left">
                                &nbsp;<asp:TextBox ID="txtWeight" runat="server" ReadOnly="true" Text='<%# Bind("Weight") %>'
                                    Width="200px" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="text-align: right">
                                &nbsp;<asp:Label ID="Label14" runat="server" meta:resourcekey="Label_Volume" />
                            </td>
                            <td align="left">
                                &nbsp;<asp:TextBox ID="txtVolume" runat="server" ReadOnly="true" Text='<%# Bind("Volume") %>'
                                    Width="200px" MaxLength="50"></asp:TextBox>
                            </td>
                            <td align="left" style="text-align: right">
                                <asp:Label ID="Label15" runat="server" meta:resourcekey="Label_Barcode" />
                            </td>
                            <td align="left">
                                &nbsp;<asp:TextBox ID="txtBarcode" runat="server" ReadOnly="true" Text='<%# Bind("Barcode") %>'
                                    Width="200px" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="text-align: right">
                                &nbsp;<asp:Label ID="Label16" runat="server" meta:resourcekey="Label_StandardCost" />
                            </td>
                            <td align="left">
                                &nbsp;<asp:TextBox ID="txtStandardCost" runat="server" ReadOnly="true" Text='<%# Bind("StandardCost") %>'
                                    Width="200px" MaxLength="50"></asp:TextBox>
                            </td>
                            <td align="left" style="text-align: right">
                                <asp:Label ID="Label17" runat="server" meta:resourcekey="Label_ShelfLife" />
                            </td>
                            <td align="left">
                                &nbsp;<asp:TextBox ID="txtShelfLife" runat="server" ReadOnly="true" Text='<%# Bind("ShelfLife") %>'
                                    Width="200px" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="text-align: right">
                                &nbsp;<asp:Label ID="Label11" runat="server" Text="<%$Resources:Common,Form_IsActive %>" />
                            </td>
                            <td align="left">
                                &nbsp;<asp:CheckBox ID="ckbIsActive" runat="server" Checked='<%# Bind("IsActive") %>' />
                            </td>
                            <td align="left" style="text-align: right">
                                &nbsp;<asp:Label ID="Label18" runat="server" meta:resourcekey="Label_PackPerCase" />
                            </td>
                            <td align="left">
                                &nbsp;<asp:TextBox ID="txtPackPerCase" ReadOnly="true" runat="server" Text='<%# Bind("PackPerCase") %>'
                                    Width="200px" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="height: 30px">
                            <td align="left" style="text-align: right">
                            </td>
                            <td align="left">
                            </td>
                            <td align="left" style="text-align: right">
                            </td>
                            <td align="left">
                                <asp:Button ID="BackButton" runat="server" CssClass="button_nor" Text="<%$Resources:Common,Button_Back %>"
                                    OnClick="BackButton_Click" CommandName="Cancel" />
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:FormView>
        </ContentTemplate>
        <%--<Triggers>
            <asp:AsyncPostBackTrigger ControlID="StuffUserGridView" EventName="SelectedIndexChanged" />
        </Triggers>--%>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="SKUObjectDataSource" runat="server" TypeName="BusinessObjects.MasterDataBLL"
        SelectMethod="GetSKUPaged" SelectCountMethod="QuerySKUCount" SortParameterName="sortExpression"
        EnablePaging="true">
        <SelectParameters>
            <asp:Parameter Name="queryExpression" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="SKUAddObjectDataSource" runat="server" InsertMethod="InsertSKU"
        SelectMethod="GetSKUById" TypeName="BusinessObjects.MasterDataBLL" UpdateMethod="UpdateSKU"
        OnInserted="SKUAddObjectDataSource_Inserted" OnUpdated="SKUAddObjectDataSource_Updated">
        <UpdateParameters>
            <asp:Parameter Name="SKUID" Type="Int32" />
            <asp:Parameter Name="SKUNo" Type="String" />
            <asp:Parameter Name="SKUName" Type="String" />
            <asp:Parameter Name="StandardCost" Type="String" />
            <asp:Parameter Name="PackPerCase" Type="String" />
            <asp:Parameter Name="Pallet" Type="String" />
            <asp:Parameter Name="SKUTypeID" Type="Int32" />
            <asp:Parameter Name="BrandID" Type="Int32" />
            <asp:Parameter Name="SKUCategoryID" Type="Int32" />
            <asp:Parameter Name="Weight" Type="String" />
            <asp:Parameter Name="Volume" Type="String" />
            <asp:Parameter Name="Barcode" Type="String" />
            <asp:Parameter Name="ShelfLife" Type="String" />
            <asp:Parameter Name="IsActive" Type="Boolean" />
        </UpdateParameters>
        <SelectParameters>
            <asp:Parameter Name="SKUID" Type="Int32" />
        </SelectParameters>
        <InsertParameters>
            <asp:Parameter Name="SKUNo" Type="String" />
            <asp:Parameter Name="SKUName" Type="String" />
            <asp:Parameter Name="StandardCost" Type="String" />
            <asp:Parameter Name="PackPerCase" Type="String" />
            <asp:Parameter Name="Pallet" Type="String" />
            <asp:Parameter Name="SKUTypeID" Type="Int32" />
            <asp:Parameter Name="BrandID" Type="Int32" />
            <asp:Parameter Name="SKUCategoryID" Type="Int32" />
            <asp:Parameter Name="Weight" Type="String" />
            <asp:Parameter Name="Volume" Type="String" />
            <asp:Parameter Name="Barcode" Type="String" />
            <asp:Parameter Name="ShelfLife" Type="String" />
            <asp:Parameter Name="IsActive" Type="Boolean" />
        </InsertParameters>
    </asp:ObjectDataSource>
    <br />
    <asp:UpdatePanel ID="SKUPriceUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div runat="server" id="divSKUPrice" visible="false">
                <div class="title">
                    <asp:Label ID="Label6" runat="server" meta:resourcekey="SKUPrice_Label_SKUPrice"></asp:Label></div>
                <gc:GridView ID="SKUPriceView" CssClass="GridView" runat="server" DataSourceID="odsSKUPrice"
                    AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="SKUPriceID" CellPadding="0">
                    <Columns>
                        <asp:TemplateField SortExpression="CustomerTypeID" meta:resourcekey="SKUPriceGridView_TemplateField_CustomerType">
                            <ItemTemplate>
                                <asp:Label ID="lblCustomerType" runat="server" Text='<%# GetCustomerTypeByID(Eval("CustomerTypeID")) %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList runat="server" ID="ddlCustomerTypeBySearch" DataTextField="CustomerTypeName"
                                    DataValueField="CustomerTypeID" Width="250px" DataSourceID="odsCustomerType"
                                    SelectedValue='<%# Bind("CustomerTypeID") %>'>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="odsCustomerType" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                    SelectCommand="select CustomerTypeID,CustomerTypeName from dbo.CustomerType where IsActive=1  order by CustomerTypeName">
                                </asp:SqlDataSource>
                            </EditItemTemplate>
                            <ItemStyle Width="400px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="CustomerChannelID" meta:resourcekey="SKUPriceGridView_TemplateField_CustomerChannel">
                            <ItemTemplate>
                                <asp:Label ID="lblCustomerChannel" runat="server" Text='<%# GetCustomerChannelByID(Eval("CustomerChannelID")) %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList runat="server" ID="ddlCustomerChannel" DataTextField="CustomerChannelName"
                                    DataValueField="CustomerChannelID" Width="250px" DataSourceID="odsCustomerChannel"
                                    SelectedValue='<%# Bind("CustomerChannelID") %>'>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="odsCustomerChannel" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                    SelectCommand="select CustomerChannelID,CustomerChannelName from dbo.CustomerChannel where IsActive=1 order by CustomerChannelName">
                                </asp:SqlDataSource>
                            </EditItemTemplate>
                            <ItemStyle Width="400px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="DeliveryPrice" meta:resourcekey="SKUPriceGridView_TemplateField_DeliveryPrice">
                            <ItemTemplate>
                                <asp:Label ID="lblDeliveryPrice" runat="server" Text='<%# Bind("DeliveryPrice") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtDeliveryPrice" runat="server" CssClass="InputText" MaxLength="15"
                                    Text='<%# Bind("DeliveryPrice") %>' Width="200px"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemStyle Width="300px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <EditItemTemplate>
                                <asp:LinkButton ID="LinkButton1" Visible="<%# HasManageRight %>" runat="server" CausesValidation="True"
                                    ValidationGroup="skuPriceEdit" CommandName="Update" Text='<%$Resources:Common,Button_Update %>'></asp:LinkButton>
                                <asp:LinkButton ID="LinkButton3" Visible="<%# HasManageRight %>" runat="server" CausesValidation="false"
                                    CommandName="Cancel" Text='<%$Resources:Common,Button_Cancel %>'></asp:LinkButton>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton2" Visible="<%# HasManageRight %>" runat="server" CausesValidation="false"
                                    CommandName="Edit" Text='<%$Resources:Common,Button_Edit %>'></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="140px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="Header" />
                    <EmptyDataTemplate>
                        <table>
                            <tr class="Header">
                                <td scope="col" style="width: 400px">
                                    <asp:Label ID="Label2" runat="server" meta:resourcekey="SKUPrice_Label_CustomerType"></asp:Label>
                                </td>
                                <td scope="col" style="width: 400px">
                                    <asp:Label ID="Label3" runat="server" meta:resourcekey="SKUPrice_Label_CustomerChannel"></asp:Label>
                                </td>
                                <td scope="col" style="width: 300px">
                                    <asp:Label ID="Label4" runat="server" meta:resourcekey="SKUPrice_Label_DeliveryPrice"></asp:Label>
                                </td>
                                <td scope="col" style="width: 140px">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                </gc:GridView>
                <asp:FormView ID="SKUPriceFormView" runat="server" DataKeyNames="SKUPriceID" CellPadding="0"
                    CellSpacing="0" DataSourceID="odsSKUPrice" DefaultMode="Insert" Visible="<%# HasManageRight %>">
                    <InsertItemTemplate>
                        <table class="FormView">
                            <tr>
                                <td align="center" style="width: 400px">
                                    <asp:DropDownList runat="server" ID="ddlCustomerTypeBySearch" DataTextField="CustomerTypeName"
                                        DataValueField="CustomerTypeID" Width="250px" DataSourceID="sdsCustomerType"
                                        SelectedValue='<%# Bind("CustomerTypeID") %>'>
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="sdsCustomerType" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                        SelectCommand="select CustomerTypeID,CustomerTypeName from dbo.CustomerType where IsActive=1  order by CustomerTypeName">
                                    </asp:SqlDataSource>
                                </td>
                                <td align="center" style="width: 400px">
                                    <asp:DropDownList runat="server" ID="ddlCustomerChannel" DataTextField="CustomerChannelName"
                                        DataValueField="CustomerChannelID" Width="250px" DataSourceID="odsCustomerChannel"
                                        SelectedValue='<%# Bind("CustomerChannelID") %>'>
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="odsCustomerChannel" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                        SelectCommand="select CustomerChannelID,CustomerChannelName from dbo.CustomerChannel where IsActive=1 order by CustomerChannelName">
                                    </asp:SqlDataSource>
                                </td>
                                <td align="center" style="height: 22px; width: 300px;">
                                    <asp:TextBox ID="txtDeliveryPrice" runat="server" Text='<%# Bind("DeliveryPrice") %>'
                                        ValidationGroup="INS" Width="200px" MaxLength="15"></asp:TextBox>
                                </td>
                                <td align="center" style="width: 140px">
                                    <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                                        Text='<%$Resources:Common,Button_Add %>' ValidationGroup="INS"></asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                        <asp:RequiredFieldValidator ID="DeliveryPriceRequiredFieldValidator" runat="server"
                            ControlToValidate="txtDeliveryPrice" Display="None" meta:resourcekey="RequiredFieldValidator_DeliveryPrice"
                            SetFocusOnError="True" ValidationGroup="INS"></asp:RequiredFieldValidator>
                        <asp:ValidationSummary ID="ValidationSummaryInsert" runat="server" ShowMessageBox="True"
                            ShowSummary="False" ValidationGroup="INS" />
                    </InsertItemTemplate>
                </asp:FormView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="odsSKUPrice" runat="server" TypeName="BusinessObjects.MasterDataBLL"
        SelectMethod="GetSKUPriceBySKUID" EnablePaging="false" InsertMethod="InsertSKUPrice"
        OnUpdating="odsSKUPrice_Updating" OnInserting="odsSKUPrice_Inserting" UpdateMethod="UpdateSKUPrice">
        <InsertParameters>
            <asp:Parameter Name="CustomerTypeID" Type="Int32" />
            <asp:Parameter Name="SKUID" Type="Int32" />
            <asp:Parameter Name="CustomerChannelID" Type="Int32" />
            <asp:Parameter Name="DeliveryPrice" Type="String" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="CustomerTypeID" Type="Int32" />
            <asp:Parameter Name="SKUID" Type="Int32" />
            <asp:Parameter Name="SKUPriceID" Type="Int32" />
            <asp:Parameter Name="CustomerChannelID" Type="Int32" />
            <asp:Parameter Name="DeliveryPrice" Type="String" />
        </UpdateParameters>
        <SelectParameters>
            <asp:Parameter Name="SKUID" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
