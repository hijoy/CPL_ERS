<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Culture="auto" UICulture="auto" CodeFile="Brand.aspx.cs" Inherits="BaseData_Brand" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="title">
        <asp:Label ID="Label6" runat="server" meta:resourcekey="Label_Title"></asp:Label></div>
    <asp:UpdatePanel ID="BrandUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <gc:GridView ID="BrandView" CssClass="GridView" runat="server" DataSourceID="BrandObjectDataSource"
                AllowPaging="false" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="BrandID" CellPadding="0">
                <Columns>
                    <asp:TemplateField HeaderText="<%$Resources:Common,Form_Brand %>" SortExpression="BrandName">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtBrandName" runat="server" CssClass="InputText" MaxLength="20"
                                Text='<%# Bind("BrandName") %>' Width="400px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="BrandNameRequiredFieldValidator" runat="server" ControlToValidate="txtBrandName"
                                Display="None" meta:resourcekey="RequiredFieldValidator_Name" SetFocusOnError="True" ValidationGroup="EDIT"></asp:RequiredFieldValidator>
                        </EditItemTemplate>
                        <ItemStyle Width="738px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblBrandName" runat="server" Text='<%# Bind("BrandName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="BrandGridView_TemplateField_BrandNo" SortExpression="BrandNo">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtBrandNo" runat="server" Text='<%# Bind("BrandNo") %>' CssClass="InputText"
                                Width="330px" MaxLength="10"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="BrandNoRequiredFieldValidator" runat="server" ControlToValidate="txtBrandNo"
                                Display="None" meta:resourcekey="RequiredFieldValidator_Code" SetFocusOnError="True" ValidationGroup="EDIT"></asp:RequiredFieldValidator>
                        </EditItemTemplate>
                        <ItemStyle Width="350px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblBrandNo" runat="server" Text='<%# Bind("BrandNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <EditItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True"
                                ValidationGroup="skuEdit" CommandName="Update" Text='<%$Resources:Common,Button_Update %>'></asp:LinkButton>
                            <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="false"
                                CommandName="Cancel" Text='<%$Resources:Common,Button_Cancel %>'></asp:LinkButton>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="false"
                                CommandName="Edit" Text='<%$Resources:Common,Button_Edit %>'></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="150px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td scope="col" style="width: 738px" class="Empty1">
                                <asp:Label ID="Label1" runat="server" Text="<%$Resources:Common,Form_Brand %>"></asp:Label>
                            </td>
                            <td scope="col" style="width: 350px">
                                <asp:Label ID="Label2" runat="server" meta:resourcekey="Brand_Label_BrandNo"></asp:Label>
                            </td>
                            <td scope="col" style="width: 150px">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
            </gc:GridView>
            <asp:FormView ID="BrandFormView" runat="server" DataKeyNames="BrandID" CellPadding="0"
                CellSpacing="0" DataSourceID="BrandObjectDataSource" DefaultMode="Insert" >
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr>
                            <td align="center" style="width: 738px">
                                <asp:TextBox ID="txtBrandName" runat="server" Text='<%# Bind("BrandName") %>' CssClass="InputText"
                                    Width="400px" ValidationGroup="INS"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 350px">
                                <asp:TextBox ID="txtBrandNo" runat="server" Text='<%# Bind("BrandNo") %>' CssClass="InputText"
                                    Width="330px" ValidationGroup="INS" MaxLength="10"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 150px">
                                <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    Text='<%$Resources:Common,Button_Add %>' ValidationGroup="INS"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                    <asp:RequiredFieldValidator ID="BrandNameRequiredFieldValidator" runat="server" ControlToValidate="txtBrandName"
                        Display="None" meta:resourcekey="RequiredFieldValidator_Name" SetFocusOnError="True" ValidationGroup="INS"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="BrandNoRequiredFieldValidator" runat="server" ControlToValidate="txtBrandNo"
                        Display="None" meta:resourcekey="RequiredFieldValidator_Code" SetFocusOnError="True" ValidationGroup="INS"></asp:RequiredFieldValidator>
                    <asp:ValidationSummary ID="ValidationSummaryInsert" runat="server" ShowMessageBox="True"
                        ShowSummary="False" ValidationGroup="INS" />
                </InsertItemTemplate>
            </asp:FormView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="BrandObjectDataSource" runat="server" InsertMethod="InsertBrand"
        SelectMethod="GetBrand" TypeName="BusinessObjects.MasterDataBLL" UpdateMethod="UpdateBrand">
        <UpdateParameters>
            <asp:Parameter Name="BrandID" Type="Int32" />
            <asp:Parameter Name="BrandNo" Type="String" />
            <asp:Parameter Name="BrandName" Type="String" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="BrandNo" Type="String" />
            <asp:Parameter Name="BrandName" Type="String" />
        </InsertParameters>
    </asp:ObjectDataSource>
</asp:Content>
