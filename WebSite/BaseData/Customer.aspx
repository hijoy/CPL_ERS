<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Culture="auto" UICulture="auto" CodeFile="Customer.aspx.cs" Inherits="BaseData_Customer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="title" >
        <asp:Label ID="Label_SearchCondition" runat="server" Text="<%$Resources:Common,Label_SearchCondition %>" /></div>
    <div class="searchDiv">
        <table class="searchTable">
            <tr>
                <td style="width: 200px;">
                    <div class="field_title">
                        <asp:Label ID="Label1" runat="server" meta:resourcekey="Label_CustomerNo" />
                     </div>
                    <asp:TextBox runat="server" ID="txtCustNoBySearch" Width="160px"></asp:TextBox>
                </td>
                <td style="width: 200px;">
                    <div class="field_title">
                        <asp:Label ID="Label2" runat="server" Text="<%$Resources:Common,Form_Customer %>" />
                    </div>
                    <asp:TextBox runat="server" ID="txtCustNameBySearch" Width="160px"></asp:TextBox>
                </td>
                <td style="width: 200px;">
                    <div class="field_title">
                        <asp:Label ID="Label3" runat="server" Text="<%$Resources:Common,Form_CustomerRegion %>" />
                    </div>
                    <asp:DropDownList runat="server" ID="ddlCustomerRegionBySearch" DataSourceID="sdsCustomerRegion"
                        DataTextField="CustomerRegionName" DataValueField="CustomerRegionID" Width="160px"
                        AppendDataBoundItems="true">
                        <asp:ListItem Selected="True" Text="All" Value=""></asp:ListItem>
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="sdsCustomerRegion" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                        SelectCommand="select CustomerRegionID,CustomerRegionName from CustomerRegion order by CustomerRegionName">
                    </asp:SqlDataSource>
                </td>
                <td style="width: 200px;">
                    <div class="field_title">
                        <asp:Label ID="Label4" runat="server" meta:resourcekey="Label_CustomerType" />
                    </div>
                    <asp:DropDownList runat="server" ID="ddlCustomerTypeBySearch" DataTextField="CustomerTypeName"
                        DataValueField="CustomerTypeID" Width="160px" DataSourceID="odsCustomerType"
                        AppendDataBoundItems="true">
                        <asp:ListItem Selected="True" Text="All" Value=""></asp:ListItem>
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="odsCustomerType" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                        SelectCommand="select CustomerTypeID,CustomerTypeName from dbo.CustomerType where IsActive=1  order by CustomerTypeName">
                    </asp:SqlDataSource>
                </td>
                <td style="width: 200px;">
                    <div class="field_title">
                        <asp:Label ID="Label5" runat="server" Text="<%$Resources:Common,Form_CustomerChannel %>" />
                    </div>
                    <asp:DropDownList runat="server" ID="ddlCustomerChannelBySearch" DataTextField="CustomerChannelName"
                        AppendDataBoundItems="true" DataValueField="CustomerChannelID" DataSourceID="odsCustomerChannel" Width="170">
                        <asp:ListItem Selected="True" Text="All" Value=""></asp:ListItem>
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="odsCustomerChannel" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                        SelectCommand="select CustomerChannelID,CustomerChannelName from dbo.CustomerChannel where IsActive=1  order by CustomerChannelName">
                    </asp:SqlDataSource>
                </td>
                <td style="width: 200px;">
                    <div class="field_title">
                        <asp:Label ID="Label6" runat="server" Text="<%$Resources:Common,Form_IsActive %>" />
                    </div>
                    <asp:DropDownList runat="server" ID="ddlActiveBySearch" Width="170">
                    <asp:ListItem Selected="True" Text="All" Value=""></asp:ListItem>
                    <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Inactive" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </div>
    <div style="margin-top: 5px; width: 1200px; text-align: right;">
        <asp:Button runat="server" ID="SearchButton" Text="<%$Resources:Common,Button_Search %>" CssClass="button_nor" OnClick="SearchButton_Click">
        </asp:Button></div>
    <br />
    <div class="title" style="width: 1260px;">
        <asp:Label ID="Label7" runat="server" meta:resourcekey="Label_Title"/>
    </div>
    <asp:UpdatePanel ID="CustomerUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <gc:GridView ID="CustomerGridView" runat="server" AllowPaging="True" AllowSorting="True"
                AutoGenerateColumns="False" DataKeyNames="CustomerID" DataSourceID="CustomerObjectDataSource"
                PageSize="20" ShowFooter="True" CssClass="GridView" OnSelectedIndexChanged="CustomerGridView_SelectedIndexChanged">
                <Columns>
                    <asp:TemplateField meta:resourcekey="TemplateField_CustomerNo" SortExpression="CustomerNo">
                        <ItemTemplate>
                            <asp:Label ID="lblCustomerNo" runat="server" Text='<%# Bind("CustomerNo") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Common,Form_Customer %>" SortExpression="CustomerName">
                        <ItemTemplate>
                            <asp:LinkButton ID="CustomerLinkButton" runat="server" CommandName="Select" OnClick="CustomerLinkButton_Click"
                                Text='<%# Bind("CustomerName") %>'></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="150px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Common,Form_CustomerChannel %>" SortExpression="CustomerChannelID">
                        <ItemTemplate>
                            <asp:Label ID="lblCustomerChannel" runat="server" Text='<%# GetCustomerChannelByID(Eval("CustomerChannelID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateField_CustomerType" SortExpression="CustomerTypeID">
                        <ItemTemplate>
                            <asp:Label ID="lblCustomerType" runat="server" Text='<%# GetCustomerTypeByID(Eval("CustomerTypeID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Common,Form_CustomerRegion %>" SortExpression="CustomerRegionID">
                        <ItemTemplate>
                            <asp:Label ID="lblCustomerRegion" runat="server" Text='<%# GetCustomerRegionByID(Eval("CustomerRegionID"))%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateField_Province" SortExpression="ProvinceID">
                        <ItemTemplate>
                            <asp:Label ID="lblProvince" runat="server" Text='<%# GetProvinceNameByID(Eval("ProvinceID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Common,Form_City %>" SortExpression="City">
                        <ItemTemplate>
                            <asp:Label ID="lblCity" runat="server" Text='<%# Bind("City") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateField_Contactor" SortExpression="Contactor">
                        <ItemTemplate>
                            <asp:Label ID="lblContactor" runat="server" Text='<%# Bind("Contactor") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateField_Tel" SortExpression="Tel">
                        <ItemTemplate>
                            <asp:Label ID="lblTel" runat="server" Text='<%# Bind("Tel") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateField_Address" SortExpression="Address">
                        <ItemTemplate>
                            <asp:Label ID="lblAddress" runat="server" Text='<%# Bind("Address") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="170px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="KaType" meta:resourcekey="TemplateField_KaType">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblKaType"  Text='<%# Bind("KaType") %>' />
                        </ItemTemplate>
                        <ItemStyle Width="70px" HorizontalAlign="Center" />
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
                    <asp:TemplateField ShowHeader="False">
                        <EditItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update"
                                Text="Update"></asp:LinkButton>
                            <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel"
                                Text="Cancel"></asp:LinkButton>
                        </EditItemTemplate>
                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:LinkButton Visible="false" ID="EditLinkButton" runat="server"
                                CausesValidation="False" CommandName="Select" Text="<%$Resources:Common,Button_Edit %>" OnClick="EditLinkButton_Click"></asp:LinkButton>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:LinkButton Visible="false" ID="AddLinkButton1" runat="server"
                                CausesValidation="True" CommandName="Select" Text="<%$Resources:Common,Button_Add %>" OnClick="AddLinkButton_Click"></asp:LinkButton>
                        </FooterTemplate>
                        <FooterStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td style="width: 150px;" class="Empty1">
                                <asp:Label ID="Label1" runat="server" meta:resourcekey="Label_CustomerNo" />
                            </td>
                            <td style="width: 200px;">
                                <asp:Label ID="Label2" runat="server" Text="<%$Resources:Common,Form_Customer %>" />
                            </td>
                            <td style="width: 100px;">
                                <asp:Label ID="Label3" runat="server" Text="<%$Resources:Common,Form_CustomerChannel %>" />
                            </td>
                            <td style="width: 150px;">
                                <asp:Label ID="Label4" runat="server" meta:resourcekey="Label_CustomerType" />
                            </td>
                            <td style="width: 150px;">
                                <asp:Label ID="Label5" runat="server" Text="<%$Resources:Common,Form_CustomerRegion %>" />
                            </td>
                            <td style="width: 100px;">
                                <asp:Label ID="Label6" runat="server" Text="<%$Resources:Common,Form_City %>" />
                            </td>
                            <td style="width: 100px;">
                                <asp:Label ID="Label8" runat="server" meta:resourcekey="Label_Province" />
                            </td>
                            <td style="width: 100px;">
                                <asp:Label ID="Label9" runat="server" meta:resourcekey="Label_Contactor" />
                            </td>
                            <td style="width: 100px;">
                                <asp:Label ID="Label10" runat="server" meta:resourcekey="Label_Tel" />
                            </td>
                            <td style="width: 100px;">
                                <asp:Label ID="Label11" runat="server" meta:resourcekey="Label_Address" />
                            </td>
                            <td style="width: 100px;">
                                <asp:Label ID="Label12" runat="server" Text="<%$Resources:Common,Form_IsActive %>" />
                            </td>
                            <td style="width: 80px;">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="11" class="Empty2 noneLabel">
                                <asp:Label ID="Label_None"  runat="server" Text="<%$Resources:Common,Label_None %>" />
                            </td>
                            <td class="Empty2 noneLabel">
                                <asp:LinkButton Visible="<%# HasManageRight %>" ID="LinkButton4" runat="server" CausesValidation="True"
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
            <asp:AsyncPostBackTrigger ControlID="CustomerFormView" EventName="ItemInserted" />
            <asp:AsyncPostBackTrigger ControlID="CustomerFormView" EventName="ItemUpdated" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="CustomerAddUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:FormView ID="CustomerFormView" runat="server" DataKeyNames="CustomerID" DataSourceID="CustomerAddObjectDataSource"
                Width="1240px">
                <EditItemTemplate>
                    <table>
                        <tr>
                            <td align="left" valign="middle" style="text-align: right; width: 300px;">
                                <asp:Label ID="Label1" runat="server" meta:resourcekey="Label_CustomerNo" />
                            </td>
                            <td align="left" valign="middle" style="padding-left: 10px; width: 170px;">
                                <asp:TextBox ID="txtCustomerNo" runat="server" Text='<%# Bind("CustomerNo") %>' ValidationGroup="EDIT"
                                    Width="200px" MaxLength="50"></asp:TextBox>
                            </td>
                            <td align="left" valign="middle" style="text-align: right; width: 200px;">
                                <asp:Label ID="Label2" runat="server" Text="<%$Resources:Common,Form_Customer %>" />
                            </td>
                            <td align="left" valign="middle" style="padding-left: 10px; width: 170px;">
                                <asp:TextBox ID="txtCustomerName" runat="server" Text='<%# Bind("CustomerName") %>'
                                    ValidationGroup="EDIT" Width="200px" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="text-align: right; width: 300px;">
                                <asp:Label ID="Label3" runat="server" Text="<%$Resources:Common,Form_CustomerChannel %>" />
                            </td>
                            <td align="left" valign="middle" style="padding-left: 10px; width: 170px;">
                                <asp:DropDownList runat="server" ID="ddlCustomerChannel" DataTextField="CustomerChannelName"
                                    DataValueField="CustomerChannelID" Width="200px" DataSourceID="odsCustomerChannel"
                                    SelectedValue='<%# Bind("CustomerChannelID") %>'>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="odsCustomerChannel" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                    SelectCommand="select CustomerChannelID,CustomerChannelName from dbo.CustomerChannel where IsActive=1  order by CustomerChannelName">
                                </asp:SqlDataSource>
                            </td>
                            <td align="left" valign="middle" style="text-align: right; width: 200px;">
                                <asp:Label ID="Label4" runat="server" meta:resourcekey="Label_CustomerType" />
                            </td>
                            <td align="left" valign="middle" style="padding-left: 10px; width: 170px;">
                                <asp:DropDownList runat="server" ID="ddlCustomerType" DataTextField="CustomerTypeName"
                                    DataValueField="CustomerTypeID" Width="200px" DataSourceID="odsCustomerType"
                                    SelectedValue='<%# Bind("CustomerTypeID") %>'>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="odsCustomerType" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                    SelectCommand="select CustomerTypeID,CustomerTypeName from dbo.CustomerType where IsActive=1  order by CustomerTypeName">
                                </asp:SqlDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="text-align: right; width: 300px;">
                                <asp:Label ID="Label5" runat="server" Text="<%$Resources:Common,Form_CustomerRegion %>" />
                            </td>
                            <td align="left" valign="middle" style="padding-left: 10px; width: 170px;">
                                <asp:DropDownList runat="server" ID="ddlCustomerRegion" DataTextField="CustomerRegionName"
                                    DataValueField="CustomerRegionID" Width="200px" DataSourceID="odsCustomerRegion"
                                    SelectedValue='<%# Bind("CustomerRegionID") %>'>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="odsCustomerRegion" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                    SelectCommand="select CustomerRegionID,CustomerRegionName from dbo.CustomerRegion  order by CustomerRegionName">
                                </asp:SqlDataSource>
                            </td>
                            <td align="left" valign="middle" style="text-align: right; width: 200px;">
                                <asp:Label ID="Label6" runat="server" Text="<%$Resources:Common,Form_City %>" />
                            </td>
                            <td align="left" valign="middle" style="padding-left: 10px; width: 170px;">
                                <asp:TextBox ID="txtCity" runat="server" Text='<%# Bind("City") %>' Width="200px"
                                    MaxLength="100"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="text-align: right; width: 300px;">
                                <asp:Label ID="Label10" runat="server" meta:resourcekey="Label_Tel" />
                            </td>
                            <td align="left" valign="middle" style="padding-left: 10px; width: 170px;">
                                <asp:TextBox ID="txtTel" runat="server" Text='<%# Bind("Tel") %>' Width="200px" MaxLength="50"></asp:TextBox>
                            </td>
                            <td align="left" valign="middle" style="text-align: right; width: 200px;">
                                <asp:Label ID="Label13" runat="server" meta:resourcekey="Label_ResponsiblePerson" />
                            </td>
                            <td align="left" valign="middle" style="padding-left: 10px; width: 170px;">
                                <asp:TextBox ID="txtResponsiblePerson" runat="server" Text='<%# Bind("ResponsiblePerson") %>'
                                    Width="200px" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="text-align: right; width: 300px;">
                                <asp:Label ID="Label9" runat="server" meta:resourcekey="Label_Contactor" />
                            </td>
                            <td align="left" valign="middle" style="padding-left: 10px; width: 170px;">
                                <asp:TextBox ID="txtContactor" runat="server" Text='<%# Bind("Contactor") %>' Width="200px"
                                    MaxLength="50"></asp:TextBox>
                            </td>
                            <td align="left" valign="middle" style="text-align: right; width: 200px;">
                                <asp:Label ID="Label14" runat="server" meta:resourcekey="Label_PostCode" />
                            </td>
                            <td align="left" valign="middle" style="padding-left: 10px; width: 170px;">
                                <asp:TextBox ID="txtPostCode" runat="server" Text='<%# Bind("PostCode") %>' Width="200px"
                                    MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="text-align: right; width: 300px;">
                                <asp:Label ID="Label15" runat="server" meta:resourcekey="Label_AccountCode" />
                            </td>
                            <td align="left" valign="middle" style="padding-left: 10px; width: 170px;">
                                <asp:TextBox ID="txtAccountCode" runat="server" Text='<%# Bind("AccountCode") %>'
                                    Width="200px" MaxLength="50"></asp:TextBox>
                            </td>
                            <td align="left" valign="middle" style="text-align: right; width: 200px;">
                                <asp:Label ID="Label16" runat="server" meta:resourcekey="Label_DealerName" />
                            </td>
                            <td align="left" valign="middle" style="padding-left: 10px; width: 170px;">
                                <asp:TextBox ID="txtDealerName" runat="server" Text='<%# Bind("DealerName") %>' Width="200px"
                                    MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="text-align: right; width: 300px;">
                                <asp:Label ID="Label11" runat="server" meta:resourcekey="Label_Address" />
                            </td>
                            <td align="left" valign="middle" style="padding-left: 10px; width: 170px;">
                                <asp:TextBox ID="txtAddress" runat="server" Text='<%# Bind("Address") %>' Width="200px"
                                    MaxLength="50"></asp:TextBox>
                            </td>
                            <td align="left" valign="middle" style="text-align: right; width: 200px;">
                                <asp:Label ID="Label17" runat="server" meta:resourcekey="Label_Remark1" />
                            </td>
                            <td align="left" valign="middle" style="padding-left: 10px; width: 170px;">
                                <asp:TextBox ID="txtRemark1" runat="server" Text='<%# Bind("Remark1") %>' Width="200px"
                                    MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="text-align: right; width: 300px;">
                                <asp:Label ID="Label18" runat="server" meta:resourcekey="Label_Remark2" />
                            </td>
                            <td align="left" valign="middle" style="padding-left: 10px; width: 170px;">
                                <asp:TextBox ID="txtRemark2" runat="server" Text='<%# Bind("Remark2") %>' Width="200px"
                                    MaxLength="50"></asp:TextBox>
                            </td>
                            <td align="left" valign="middle" style="text-align: right; width: 200px;">
                                <asp:Label ID="Label19" runat="server" meta:resourcekey="Label_Remark3" />
                            </td>
                            <td align="left" valign="middle" style="padding-left: 10px; width: 170px;">
                                <asp:TextBox ID="txtRemark3" runat="server" Text='<%# Bind("Remark3") %>' Width="200px"
                                    MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                        <td align="left" valign="middle" style="text-align: right; width: 200px;">
                                <asp:Label ID="Label29" runat="server" meta:resourcekey="Lable_katype" />
                            </td>
                            <td align="left" valign="middle" style="padding-left: 10px; width: 170px;">
                                <asp:TextBox ID="txtKaType" runat="server" Text='<%# Bind("KaType") %>' Width="200px"
                                    MaxLength="50"></asp:TextBox>
                            </td>
                        <td colspan="2">
                        </td>
                        </tr>
                        <tr style="height: 30px">
                            <td align="left" valign="middle" style="text-align: right; width: 300px;">
                                <asp:Label ID="Label8" runat="server" meta:resourcekey="Label_Province" />
                            </td>
                            <td align="left" valign="middle" style="padding-left: 10px; width: 170px;">
                                <asp:DropDownList runat="server" ID="ProvinceDDL" DataTextField="ProvinceName" DataValueField="ProvinceID"
                                    Width="200px" DataSourceID="odsProvince" SelectedValue='<%# Bind("ProvinceID") %>'>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="odsProvince" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                    SelectCommand="select ProvinceID,ProvinceName from Province"></asp:SqlDataSource>
                            </td>
                            <td align="left" valign="middle" style="text-align: right; width: 200px;">
                                <asp:Label ID="Label12" runat="server" Text="<%$Resources:Common,Form_IsActive %>" />
                            </td>
                            <td align="left" valign="middle" style="padding-left: 10px; width: 170px;">
                                <asp:CheckBox ID="chkActive" runat="server" Checked='<%# Bind("IsActive") %>' />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="left" valign="middle" style="text-align: right; width: 200px;">
                            </td>
                            <td align="left" valign="middle" style="padding-left: 10px; width: 170px;">
                                <asp:Button ID="UpdateButton" runat="server" CssClass="button_nor" Text="<%$Resources:Common,Button_Update %>" CommandName="Update"
                                    OnClick="InsertButton_Click" ValidationGroup="EDIT" />
                                <asp:Button ID="Button2" runat="server" CssClass="button_nor" Text="<%$Resources:Common,Button_Back %>" OnClick="BackButton_Click"
                                    CommandName="Cancel" />
                            </td>
                        </tr>
                    </table>
                    <asp:RequiredFieldValidator ID="CustomerNoRequiredFieldValidator" runat="server"
                        ControlToValidate="txtCustomerNo" Display="Dynamic" meta:resourcekey="RequiredFieldValidator_Code"
                        SetFocusOnError="True" ValidationGroup="EDIT"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="CustomerNameRequiredFieldValidator" runat="server"
                        ControlToValidate="txtCustomerName" Display="Dynamic" meta:resourcekey="RequiredFieldValidator_Name"
                        SetFocusOnError="True" ValidationGroup="EDIT"></asp:RequiredFieldValidator>
                    <asp:ValidationSummary ID="ValidationSummaryEdit" runat="server" ShowMessageBox="True"
                        ShowSummary="true" ValidationGroup="EDIT" />
                </EditItemTemplate>
                <InsertItemTemplate>
                    <table>
                        <tr>
                            <td align="left" valign="middle" style="text-align: right; width: 300px;">
                                <asp:Label ID="Label1" runat="server" meta:resourcekey="Label_CustomerNo" />
                            </td>
                            <td align="left" valign="middle" style="padding-left: 10px; width: 170px;">
                                <asp:TextBox ID="txtCustomerNo" runat="server" Text='<%# Bind("CustomerNo") %>' ValidationGroup="INS"
                                    Width="200px" MaxLength="50"></asp:TextBox>
                            </td>
                            <td align="left" valign="middle" style="text-align: right; width: 200px;">
                                <asp:Label ID="Label2" runat="server" Text="<%$Resources:Common,Form_Customer %>" />
                            </td>
                            <td align="left" valign="middle" style="padding-left: 10px; width: 170px;">
                                <asp:TextBox ID="txtCustomerName" runat="server" Text='<%# Bind("CustomerName") %>'
                                    ValidationGroup="INS" Width="200px" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="text-align: right; width: 300px;">
                                <asp:Label ID="Label3" runat="server" Text="<%$Resources:Common,Form_CustomerChannel %>" />
                            </td>
                            <td align="left" valign="middle" style="padding-left: 10px; width: 170px;">
                                <asp:DropDownList runat="server" ID="ddlCustomerChannel" DataTextField="CustomerChannelName"
                                    Width="200px" DataValueField="CustomerChannelID" DataSourceID="odsCustomerChannel"
                                    SelectedValue='<%# Bind("CustomerChannelID") %>'>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="odsCustomerChannel" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                    SelectCommand="select CustomerChannelID,CustomerChannelName from dbo.CustomerChannel where IsActive=1  order by CustomerChannelName">
                                </asp:SqlDataSource>
                            </td>
                            <td align="left" valign="middle" style="text-align: right; width: 200px;">
                                <asp:Label ID="Label4" runat="server" meta:resourcekey="Label_CustomerType" />
                            </td>
                            <td align="left" valign="middle" style="padding-left: 10px; width: 170px;">
                                <asp:DropDownList runat="server" ID="ddlCustomerType" DataTextField="CustomerTypeName"
                                    Width="200px" DataValueField="CustomerTypeID" DataSourceID="odsCustomerType"
                                    SelectedValue='<%# Bind("CustomerTypeID") %>'>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="odsCustomerType" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                    SelectCommand="select CustomerTypeID,CustomerTypeName from dbo.CustomerType where IsActive=1  order by CustomerTypeName">
                                </asp:SqlDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="text-align: right; width: 300px;">
                                <asp:Label ID="Label5" runat="server" Text="<%$Resources:Common,Form_CustomerRegion %>" />
                            </td>
                            <td align="left" valign="middle" style="padding-left: 10px; width: 170px;">
                                <asp:DropDownList runat="server" ID="ddlCustomerRegion" DataTextField="CustomerRegionName"
                                    Width="200px" DataValueField="CustomerRegionID" DataSourceID="odsCustomerRegion"
                                    SelectedValue='<%# Bind("CustomerRegionID") %>'>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="odsCustomerRegion" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                    SelectCommand="select CustomerRegionID,CustomerRegionName from dbo.CustomerRegion  order by CustomerRegionName">
                                </asp:SqlDataSource>
                            </td>
                            <td align="left" valign="middle" style="text-align: right; width: 200px;">
                                <asp:Label ID="Label6" runat="server" Text="<%$Resources:Common,Form_City %>" />
                            </td>
                            <td align="left" valign="middle" style="padding-left: 10px; width: 170px;">
                                <asp:TextBox ID="txtCity" runat="server" Text='<%# Bind("City") %>' Width="200px"
                                    MaxLength="100"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="text-align: right; width: 300px;">
                                <asp:Label ID="Label10" runat="server" meta:resourcekey="Label_Tel" />
                            </td>
                            <td align="left" valign="middle" style="padding-left: 10px; width: 170px;">
                                <asp:TextBox ID="txtTel" runat="server" Text='<%# Bind("Tel") %>' Width="200px" MaxLength="50"></asp:TextBox>
                            </td>
                            <td align="left" valign="middle" style="text-align: right; width: 200px;">
                                <asp:Label ID="Label13" runat="server" meta:resourcekey="Label_ResponsiblePerson" />
                            </td>
                            <td align="left" valign="middle" style="padding-left: 10px; width: 170px;">
                                <asp:TextBox ID="txtResponsiblePerson" runat="server" Text='<%# Bind("ResponsiblePerson") %>'
                                    Width="200px" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="text-align: right; width: 300px;">
                                <asp:Label ID="Label9" runat="server" meta:resourcekey="Label_Contactor" />
                            </td>
                            <td align="left" valign="middle" style="padding-left: 10px; width: 170px;">
                                <asp:TextBox ID="txtContactor" runat="server" Text='<%# Bind("Contactor") %>' Width="200px"
                                    MaxLength="50"></asp:TextBox>
                            </td>
                            <td align="left" valign="middle" style="text-align: right; width: 200px;">
                                <asp:Label ID="Label14" runat="server" meta:resourcekey="Label_PostCode" />
                            </td>
                            <td align="left" valign="middle" style="padding-left: 10px; width: 170px;">
                                <asp:TextBox ID="txtPostCode" runat="server" Text='<%# Bind("PostCode") %>' Width="200px"
                                    MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="text-align: right; width: 300px;">
                                <asp:Label ID="Label15" runat="server" meta:resourcekey="Label_AccountCode" />
                            </td>
                            <td align="left" valign="middle" style="padding-left: 10px; width: 170px;">
                                <asp:TextBox ID="txtAccountCode" runat="server" Text='<%# Bind("AccountCode") %>'
                                    Width="200px" MaxLength="50"></asp:TextBox>
                            </td>
                            <td align="left" valign="middle" style="text-align: right; width: 200px;">
                                <asp:Label ID="Label16" runat="server" meta:resourcekey="Label_DealerName" />
                            </td>
                            <td align="left" valign="middle" style="padding-left: 10px; width: 170px;">
                                <asp:TextBox ID="txtDealerName" runat="server" Text='<%# Bind("DealerName") %>' Width="200px"
                                    MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="text-align: right; width: 300px;">
                                <asp:Label ID="Label20" runat="server" meta:resourcekey="Label_Address" />
                            </td>
                            <td align="left" valign="middle" style="padding-left: 10px; width: 170px;">
                                <asp:TextBox ID="txtAddress" runat="server" Text='<%# Bind("Address") %>' Width="200px"
                                    MaxLength="50"></asp:TextBox>
                            </td>
                            <td align="left" valign="middle" style="text-align: right; width: 200px;">
                                <asp:Label ID="Label25" runat="server" meta:resourcekey="Label_Remark1" />
                            </td>
                            <td align="left" valign="middle" style="padding-left: 10px; width: 170px;">
                                <asp:TextBox ID="txtRemark1" runat="server" Text='<%# Bind("Remark1") %>' Width="200px"
                                    MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="text-align: right; width: 300px;">
                                <asp:Label ID="Label26" runat="server" meta:resourcekey="Label_Remark2" />
                            </td>
                            <td align="left" valign="middle" style="padding-left: 10px; width: 170px;">
                                <asp:TextBox ID="txtRemark2" runat="server" Text='<%# Bind("Remark2") %>' Width="200px"
                                    MaxLength="50"></asp:TextBox>
                            </td>
                            <td align="left" valign="middle" style="text-align: right; width: 200px;">
                                <asp:Label ID="Label27" runat="server" meta:resourcekey="Label_Remark3" />
                            </td>
                            <td align="left" valign="middle" style="padding-left: 10px; width: 170px;">
                                <asp:TextBox ID="txtRemark3" runat="server" Text='<%# Bind("Remark3") %>' Width="200px"
                                    MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="height: 30px">
                            <td align="left" valign="middle" style="text-align: right; width: 300px;">
                                <asp:Label ID="Label28" runat="server" meta:resourcekey="Label_Province" />
                            </td>
                            <td align="left" valign="middle" style="padding-left: 10px; width: 170px;">
                                <asp:DropDownList runat="server" ID="ProvinceDDL" DataTextField="ProvinceName" DataValueField="ProvinceID"
                                    Width="200px" DataSourceID="odsProvince" SelectedValue='<%# Bind("ProvinceID") %>'>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="odsProvince" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                    SelectCommand="select ProvinceID,ProvinceName from Province"></asp:SqlDataSource>
                            </td>
                            <td align="left" valign="middle" style="text-align: right; width: 200px;">
                                <asp:Label ID="Label12" runat="server" Text="<%$Resources:Common,Form_IsActive %>" />
                            </td>
                            <td align="left" valign="middle" style="padding-left: 10px; width: 170px;">
                                <asp:CheckBox ID="chkActive" runat="server" Checked='<%# Bind("IsActive") %>' />
                            </td>
                        </tr>
                        <tr>
                        <td align="left" valign="middle" style="text-align: right; width: 200px;">
                                <asp:Label ID="Label29" runat="server" meta:resourcekey="Lable_katype" />
                            </td>
                            <td align="left" valign="middle" style="padding-left: 10px; width: 170px;">
                                <asp:TextBox ID="txtKaType" runat="server" Text='<%# Bind("KaType") %>' Width="200px"
                                    MaxLength="50"></asp:TextBox>
                            </td>
                        <td colspan="2">
                        </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                            </td>
                            <td align="left" valign="middle" style="padding-left: 10px; width: 170px;">
                                <asp:Button ID="InsertButton" runat="server" CssClass="button_nor" Text="<%$Resources:Common,Button_Save %>" CommandName="Insert"
                                    OnClick="InsertButton_Click" ValidationGroup="INS" />
                                <asp:Button ID="BackButton" runat="server" CssClass="button_nor" Text="<%$Resources:Common,Button_Back %>" OnClick="BackButton_Click"
                                    CommandName="Cancel" />
                            </td>
                        </tr>
                    </table>
                    <asp:RequiredFieldValidator ID="CustomerNoRequiredFieldValidator" runat="server"
                        ControlToValidate="txtCustomerNo" Display="Dynamic" meta:resourcekey="RequiredFieldValidator_Code"
                        SetFocusOnError="True" ValidationGroup="INS"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="CustomerNameRequiredFieldValidator" runat="server"
                        ControlToValidate="txtCustomerName" Display="Dynamic" meta:resourcekey="RequiredFieldValidator_Name"
                        SetFocusOnError="True" ValidationGroup="INS"></asp:RequiredFieldValidator>
                    <asp:ValidationSummary ID="ValidationSummaryInsert" runat="server" ShowMessageBox="True"
                        ShowSummary="False" ValidationGroup="INS" />
                </InsertItemTemplate>
                <ItemTemplate>
                    <table>
                        <tr>
                            <td align="left" valign="middle" style="text-align: right; width: 300px;">
                                <asp:Label ID="Label1" runat="server" meta:resourcekey="Label_CustomerNo" />
                            </td>
                            <td align="left" valign="middle" style="padding-left: 10px; width: 170px;">
                                <asp:TextBox ID="txtCustomerNo" runat="server" ReadOnly="true" Text='<%# Bind("CustomerNo") %>'
                                    Width="200px" MaxLength="50"></asp:TextBox>
                            </td>
                            <td align="left" valign="middle" style="text-align: right; width: 200px;">
                                <asp:Label ID="Label2" runat="server" Text="<%$Resources:Common,Form_Customer %>" />
                            </td>
                            <td align="left" valign="middle" style="padding-left: 10px; width: 170px;">
                                <asp:TextBox ID="txtCustomerName" runat="server" ReadOnly="true" Text='<%# Bind("CustomerName") %>'
                                    Width="200px" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="text-align: right; width: 300px;">
                                <asp:Label ID="Label3" runat="server" Text="<%$Resources:Common,Form_CustomerChannel %>" />
                            </td>
                            <td align="left" valign="middle" style="padding-left: 10px; width: 170px;">
                                <asp:DropDownList runat="server" ID="ddlCustomerChannel" DataTextField="CustomerChannelName"
                                    Enabled="false" Width="200px" DataValueField="CustomerChannelID" DataSourceID="odsCustomerChannel"
                                    SelectedValue='<%# Bind("CustomerChannelID") %>'>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="odsCustomerChannel" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                    SelectCommand="select CustomerChannelID,CustomerChannelName from dbo.CustomerChannel where IsActive=1  order by CustomerChannelName">
                                </asp:SqlDataSource>
                            </td>
                            <td align="left" valign="middle" style="text-align: right; width: 200px;">
                                <asp:Label ID="Label4" runat="server" meta:resourcekey="Label_CustomerType" />
                            </td>
                            <td align="left" valign="middle" style="padding-left: 10px; width: 170px;">
                                <asp:DropDownList runat="server" ID="ddlCustomerType" DataTextField="CustomerTypeName"
                                    Enabled="false" Width="200px" DataValueField="CustomerTypeID" DataSourceID="odsCustomerType"
                                    SelectedValue='<%# Bind("CustomerTypeID") %>'>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="odsCustomerType" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                    SelectCommand="select CustomerTypeID,CustomerTypeName from dbo.CustomerType where IsActive=1  order by CustomerTypeName">
                                </asp:SqlDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="text-align: right; width: 300px;">
                                <asp:Label ID="Label5" runat="server" Text="<%$Resources:Common,Form_CustomerRegion %>" />
                            </td>
                            <td align="left" valign="middle" style="padding-left: 10px; width: 170px;">
                                <asp:DropDownList runat="server" ID="ddlCustomerRegion" DataTextField="CustomerRegionName"
                                    Enabled="false" Width="200px" DataValueField="CustomerRegionID" DataSourceID="odsCustomerRegion"
                                    SelectedValue='<%# Bind("CustomerRegionID") %>'>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="odsCustomerRegion" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                    SelectCommand="select CustomerRegionID,CustomerRegionName from dbo.CustomerRegion  order by CustomerRegionName">
                                </asp:SqlDataSource>
                            </td>
                            <td align="left" valign="middle" style="text-align: right; width: 200px;">
                                <asp:Label ID="Label6" runat="server" Text="<%$Resources:Common,Form_City %>" />
                            </td>
                            <td align="left" valign="middle" style="padding-left: 10px; width: 170px;">
                                <asp:TextBox ID="txtCity" runat="server" ReadOnly="true" Text='<%# Bind("City") %>'
                                    Width="200px" MaxLength="100"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="text-align: right; width: 300px;">
                                <asp:Label ID="Label10" runat="server" meta:resourcekey="Label_Tel" />
                            </td>
                            <td align="left" valign="middle" style="padding-left: 10px; width: 170px;">
                                <asp:TextBox ID="txtTel" runat="server" ReadOnly="true" Text='<%# Bind("Tel") %>'
                                    Width="200px" MaxLength="50"></asp:TextBox>
                            </td>
                            <td align="left" valign="middle" style="text-align: right; width: 200px;">
                                <asp:Label ID="Label13" runat="server" meta:resourcekey="Label_ResponsiblePerson" />
                            </td>
                            <td align="left" valign="middle" style="padding-left: 10px; width: 170px;">
                                <asp:TextBox ID="txtResponsiblePerson" ReadOnly="true" runat="server" Text='<%# Bind("ResponsiblePerson") %>'
                                    Width="200px" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="text-align: right; width: 300px;">
                                <asp:Label ID="Label9" runat="server" meta:resourcekey="Label_Contactor" />
                            </td>
                            <td align="left" valign="middle" style="padding-left: 10px; width: 170px;">
                                <asp:TextBox ID="txtContactor" ReadOnly="true" runat="server" Text='<%# Bind("Contactor") %>'
                                    Width="200px" MaxLength="50"></asp:TextBox>
                            </td>
                            <td align="left" valign="middle" style="text-align: right; width: 200px;">
                                <asp:Label ID="Label14" runat="server" meta:resourcekey="Label_PostCode" />
                            </td>
                            <td align="left" valign="middle" style="padding-left: 10px; width: 170px;">
                                <asp:TextBox ID="txtPostCode" runat="server" ReadOnly="true" Text='<%# Bind("PostCode") %>'
                                    Width="200px" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="text-align: right; width: 300px;">
                                <asp:Label ID="Label15" runat="server" meta:resourcekey="Label_AccountCode" />
                            </td>
                            <td align="left" valign="middle" style="padding-left: 10px; width: 170px;">
                                <asp:TextBox ID="txtAccountCode" runat="server" ReadOnly="true" Text='<%# Bind("AccountCode") %>'
                                    Width="200px" MaxLength="50"></asp:TextBox>
                            </td>
                            <td align="left" valign="middle" style="text-align: right; width: 200px;">
                                <asp:Label ID="Label16" runat="server" meta:resourcekey="Label_DealerName" />
                            </td>
                            <td align="left" valign="middle" style="padding-left: 10px; width: 170px;">
                                <asp:TextBox ID="txtDealerName" runat="server" ReadOnly="true" Text='<%# Bind("DealerName") %>'
                                    Width="200px" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="text-align: right; width: 300px;">
                                <asp:Label ID="Label20" runat="server" meta:resourcekey="Label_Address" />
                            </td>
                            <td align="left" valign="middle" style="padding-left: 10px; width: 170px;">
                                <asp:TextBox ID="txtAddress" runat="server" ReadOnly="true" Text='<%# Bind("Address") %>'
                                    Width="200px" MaxLength="50"></asp:TextBox>
                            </td>
                            <td align="left" valign="middle" style="text-align: right; width: 200px;">
                                <asp:Label ID="Label21" runat="server" meta:resourcekey="Label_Remark1" />
                            </td>
                            <td align="left" valign="middle" style="padding-left: 10px; width: 170px;">
                                <asp:TextBox ID="txtRemark1" runat="server" ReadOnly="true" Text='<%# Bind("Remark1") %>'
                                    Width="200px" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="text-align: right; width: 300px;">
                                <asp:Label ID="Label22" runat="server" meta:resourcekey="Label_Remark2" />
                            </td>
                            <td align="left" valign="middle" style="padding-left: 10px; width: 170px;">
                                <asp:TextBox ID="txtRemark2" runat="server" ReadOnly="true" Text='<%# Bind("Remark2") %>'
                                    Width="200px" MaxLength="50"></asp:TextBox>
                            </td>
                            <td align="left" valign="middle" style="text-align: right; width: 200px;">
                                <asp:Label ID="Label23" runat="server" meta:resourcekey="Label_Remark3" />
                            </td>
                            <td align="left" valign="middle" style="padding-left: 10px; width: 170px;">
                                <asp:TextBox ID="txtRemark3" runat="server" ReadOnly="true" Text='<%# Bind("Remark3") %>'
                                    Width="200px" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="middle" style="text-align: right; width: 300px;">
                                <asp:Label ID="Label24" runat="server" meta:resourcekey="Label_Province" />
                            </td>
                            <td align="left" valign="middle" style="padding-left: 10px; width: 170px;">
                                <asp:DropDownList runat="server" ID="ProvinceDDL" DataTextField="ProvinceName" DataValueField="ProvinceID"
                                    Enabled="false" Width="200px" DataSourceID="odsProvince" SelectedValue='<%# Bind("ProvinceID") %>'>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="odsProvince" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                    SelectCommand="select ProvinceID,ProvinceName from Province"></asp:SqlDataSource>
                            </td>
                            <td align="left" valign="middle" style="text-align: right; width: 200px;">
                                <asp:Label ID="Label12" runat="server" Text="<%$Resources:Common,Form_IsActive %>" />
                            </td>
                            <td align="left" valign="middle" style="padding-left: 10px; width: 170px;">
                                <asp:CheckBox ID="chkActive" runat="server" Enabled="false" Checked='<%# Bind("IsActive") %>' />
                            </td>
                        </tr>
                        <tr>
                        <td align="left" valign="middle" style="text-align: right; width: 200px;">
                                <asp:Label ID="Label29" runat="server" meta:resourcekey="Lable_katype" />
                            </td>
                            <td align="left" valign="middle" style="padding-left: 10px; width: 170px;">
                                <asp:TextBox ID="txtKaType" runat="server" Enabled="false"  Text='<%# Bind("KaType") %>' Width="200px"
                                    MaxLength="50"></asp:TextBox>
                            </td>
                        <td colspan="2">
                        </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                            </td>
                            <td align="left" valign="middle" style="padding-left: 10px; width: 170px;">
                                <asp:Button ID="BackButton" runat="server" CssClass="button_nor" Text="<%$Resources:Common,Button_Back %>" OnClick="BackButton_Click"
                                    CommandName="Cancel" />
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:FormView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="CustomerObjectDataSource" runat="server" TypeName="BusinessObjects.MasterDataBLL"
        SelectMethod="GetCustomerPaged" SelectCountMethod="CustomerCount" SortParameterName="sortExpression"
        EnablePaging="true">
        <SelectParameters>
            <asp:Parameter Name="queryExpression" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="CustomerAddObjectDataSource" runat="server" InsertMethod="InsertCustomer"
        SelectMethod="GetCustomerById" TypeName="BusinessObjects.MasterDataBLL" UpdateMethod="UpdateCustomer"
        OnInserted="CustomerAddObjectDataSource_Inserted" OnUpdated="CustomerAddObjectDataSource_Updated">
        <SelectParameters>
            <asp:Parameter Name="CustomerID" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
