<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Culture="auto" UICulture="auto" CodeFile="VendorType.aspx.cs" Inherits="BaseData_VendorType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="title" style="width: 1240px;">
        <asp:Label ID="Label_SearchCondition" runat="server" Text="<%$Resources:Common,Label_SearchCondition %>" /></div>
    <div class="searchDiv" style="width: 1240px;">
        <table class="searchTable" cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 400px;" class="field_title">
                    <asp:Label ID="Label1" runat="server" meta:resourcekey="VendorType_Label_VendorTypeName" />
                </td>
                <td style="width: 400px;" class="field_title">
                    <asp:Label ID="Label2" runat="server" meta:resourcekey="VendorType_Label_Company" />
                </td>
                <td style="width: 400px;" class="field_title">
                    <asp:Label ID="Label3" runat="server" Text="<%$Resources:Common,Form_Currency %>" />
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top">
                    <asp:TextBox runat="server" ID="txtVendorTypeNameBySearch" Width="250px"></asp:TextBox>
                </td>
                <td style="vertical-align: top">
                    <asp:DropDownList runat="server" ID="ddlCompany" DataSourceID="odsCompany" DataTextField="CompanyName"
                        DataValueField="CompanyID" Width="250px" AppendDataBoundItems="true">
                        <asp:ListItem Selected="True" Text="All" Value=""></asp:ListItem>
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="odsCompany" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                        SelectCommand="SELECT [CompanyID], [CompanyName] FROM [Company] "></asp:SqlDataSource>
                </td>
                <td style="vertical-align: top">
                    <asp:DropDownList runat="server" ID="ddlCurrency" DataTextField="CurrencyFullName"
                        DataValueField="CurrencyID" Width="250px" DataSourceID="odsCurrency" AppendDataBoundItems="true">
                        <asp:ListItem Selected="True" Text="All" Value=""></asp:ListItem>
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="odsCurrency" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                        SelectCommand="select CurrencyID,CurrencyFullName from dbo.Currency where IsActive=1 order by CurrencyFullName">
                    </asp:SqlDataSource>
                </td>
            </tr>
        </table>
    </div>
    <div style="margin-top: 5px; width: 1200px; text-align: right;">
        <asp:Button runat="server" ID="SearchButton" Text="<%$Resources:Common,Button_Search %>"
            CssClass="button_nor" OnClick="SearchButton_Click"></asp:Button></div>
    <br />
    <div class="title">
        <asp:Label ID="Label6" runat="server" meta:resourcekey="Label_Title"></asp:Label></div>
    <asp:UpdatePanel ID="VendorTypeUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <gc:GridView ID="VendorTypeView" CssClass="GridView" runat="server" DataSourceID="VendorTypeObjectDataSource"
                AllowPaging="true" PageSize="20" AllowSorting="True" AutoGenerateColumns="False"
                DataKeyNames="VendorTypeID" CellPadding="0">
                <Columns>
                    <asp:TemplateField meta:resourcekey="VendorTypeGridView_TemplateField_VendorTypeName"
                        SortExpression="VendorTypeName">
                        <ItemTemplate>
                            <asp:Label ID="lblVendorTypeName" runat="server" Text='<%# Bind("VendorTypeName") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="150px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="VendorTypeGridView_TemplateField_Company" SortExpression="CompanyID">
                        <ItemTemplate>
                            <asp:Label ID="lblCompany" runat="server" Text='<%# GetCompanyNameByID(Eval("CompanyID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="250px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Common,Form_Currency %>" SortExpression="CurrencyID">
                        <ItemTemplate>
                            <asp:Label ID="lblCurrency" runat="server" Text='<%# GetCurrencyByID(Eval("CurrencyID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="200px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Common,Label_Description %>" SortExpression="Description">
                        <ItemTemplate>
                            <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="464px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="IsActive" HeaderText="<%$ Resources:Common,Form_IsActive %>">
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="chkActiveByEdit1" Enabled="false" Checked='<%# Bind("IsActive") %>' />
                        </ItemTemplate>
                        <ItemStyle Width="70px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Label ID="lblEmpty" runat="server" Text=""></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td scope="col" style="width: 150px" class="Empty1">
                                <asp:Label ID="Label1" runat="server" meta:resourcekey="VendorType_Label_VendorTypeName"></asp:Label>
                            </td>
                            <td scope="col" style="width: 250px">
                                <asp:Label ID="Label2" runat="server" meta:resourcekey="VendorType_Label_Company"></asp:Label>
                            </td>
                            <td scope="col" style="width: 200px">
                                <asp:Label ID="Label3" runat="server" Text="<%$Resources:Common,Form_Currency %>"></asp:Label>
                            </td>
                            <td scope="col" style="width: 464px">
                                <asp:Label ID="Label5" runat="server" Text="<%$Resources:Common,Label_Description %>"></asp:Label>
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
            <asp:FormView ID="VendorTypeFormView" runat="server" DataKeyNames="VendorTypeID"
                CellPadding="0" CellSpacing="0" DataSourceID="VendorTypeObjectDataSource" DefaultMode="Insert">
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr>
                            <td align="center" style="width: 150px">
                                <asp:TextBox ID="txtVendorTypeName" runat="server" Text='<%# Bind("VendorTypeName") %>'
                                    CssClass="InputText" Width="130px" ValidationGroup="INS"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 250px">
                                <asp:DropDownList runat="server" ID="ddlCompany" DataSourceID="odsCompany" DataTextField="CompanyName"
                                    DataValueField="CompanyID" Width="230px" SelectedValue='<%# Bind("CompanyID") %>'>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="odsCompany" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                    SelectCommand="SELECT [CompanyID], [CompanyName] FROM [Company] "></asp:SqlDataSource>
                            </td>
                            <td align="center" style="width: 200px">
                                <asp:DropDownList runat="server" ID="ddlCurrency" DataTextField="CurrencyFullName"
                                    DataValueField="CurrencyID" Width="180px" DataSourceID="odsCurrency" SelectedValue='<%# Bind("CurrencyID") %>'>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="odsCurrency" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                    SelectCommand="select CurrencyID,CurrencyFullName from dbo.Currency where IsActive=1 order by CurrencyFullName">
                                </asp:SqlDataSource>
                            </td>
                            <td align="center" style="width: 464px">
                                <asp:TextBox ID="txtDescription" runat="server" Text='<%# Bind("Description") %>'
                                    CssClass="InputText" Width="430px" ValidationGroup="INS"></asp:TextBox>
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
                    <asp:RequiredFieldValidator ID="VendorTypeNameRequiredFieldValidator" runat="server"
                        ControlToValidate="txtVendorTypeName" Display="None" meta:resourcekey="RequiredFieldValidator_VendorTypeName"
                        SetFocusOnError="True" ValidationGroup="INS"></asp:RequiredFieldValidator>
                    <asp:ValidationSummary ID="ValidationSummaryInsert" runat="server" ShowMessageBox="True"
                        ShowSummary="False" ValidationGroup="INS" />
                </InsertItemTemplate>
            </asp:FormView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="VendorTypeObjectDataSource" runat="server" InsertMethod="InsertVendorType"
        SelectMethod="GetVendorTypePaged" TypeName="BusinessObjects.MasterDataBLL" SelectCountMethod="VendorTypeCount"
        SortParameterName="sortExpression" EnablePaging="true">
        <SelectParameters>
            <asp:Parameter Name="queryExpression" Type="String" />
        </SelectParameters>
        <InsertParameters>
            <asp:Parameter Name="CompanyID" Type="Int32" />
            <asp:Parameter Name="VendorTypeName" Type="String" />
            <asp:Parameter Name="CurrencyID" Type="Int32" />
            <asp:Parameter Name="Description" Type="String" />
            <asp:Parameter Name="IsActive" Type="Boolean" />
        </InsertParameters>
    </asp:ObjectDataSource>
</asp:Content>
