<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Culture="auto" UICulture="auto"
    CodeFile="Company.aspx.cs" Inherits="BaseData_Company" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="title">
        <asp:Label ID="Label5" runat="server" meta:resourcekey="Label_Title"></asp:Label></div>
    <asp:UpdatePanel ID="CompanyUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <gc:GridView ID="CompanyGridView" CssClass="GridView" runat="server" DataSourceID="CompanyObjectDataSource"
                AllowPaging="false" AutoGenerateColumns="False" DataKeyNames="CompanyID"
                CellPadding="0">
                <Columns>
                    <asp:TemplateField meta:resourcekey="CompanyGridView_TemplateField_CompanyName">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCompanyName" runat="server" CssClass="InputText" MaxLength="20"
                                Text='<%# Bind("CompanyName") %>' Width="270px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="CompanyNameRequiredFieldValidator" runat="server"
                                ControlToValidate="txtCompanyName" Display="None" meta:resourcekey="RequiredFieldValidator_CompanyName" SetFocusOnError="True"
                                ValidationGroup="EDIT"></asp:RequiredFieldValidator>
                        </EditItemTemplate>
                        <ItemStyle Width="300px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblCompanyName" runat="server" Text='<%# Bind("CompanyName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="CompanyGridView_TemplateField_CompanyCode" >
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCompanyCode" runat="server" Text='<%# Bind("CompanyCode") %>'
                                CssClass="InputText" Width="80px" MaxLength="10"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="CompanyCodeRequiredFieldValidator" runat="server"
                                ControlToValidate="txtCompanyCode" Display="None" meta:resourcekey="RequiredFieldValidator_CompanyCode" SetFocusOnError="True"
                                ValidationGroup="EDIT"></asp:RequiredFieldValidator>
                        </EditItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblCompanyCode" runat="server" Text='<%# Bind("CompanyCode") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="CompanyGridView_TemplateField_CompanyShortName">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCompanyShortName" runat="server" Text='<%# Bind("CompanyShortName") %>'
                                CssClass="InputText" Width="170px" MaxLength="20"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="CompanyShortNameRequiredFieldValidator" runat="server"
                                ControlToValidate="txtCompanyShortName" Display="None" meta:resourcekey="RequiredFieldValidator_CompanyShortName"
                                SetFocusOnError="True" ValidationGroup="EDIT"></asp:RequiredFieldValidator>
                        </EditItemTemplate>
                        <ItemStyle Width="200px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblCompanyShortName" runat="server" Text='<%# Bind("CompanyShortName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateField_BeginSequenceNo">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtBeginSequenceNo" runat="server" Text='<%# Bind("BeginSequenceNo") %>'
                                CssClass="InputText" Width="80px" MaxLength="10"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="BeginSequenceNoRequiredFieldValidator" runat="server"
                                ControlToValidate="txtBeginSequenceNo" Display="None" meta:resourcekey="RequiredFieldValidator_BeginSequenceNo" SetFocusOnError="True"
                                ValidationGroup="EDIT"></asp:RequiredFieldValidator>
                        </EditItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblBeginSequenceNo" runat="server" Text='<%# Bind("BeginSequenceNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateField_EndSequenceNo">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEndSequenceNo" runat="server" Text='<%# Bind("EndSequenceNo") %>'
                                CssClass="InputText" Width="80px" MaxLength="10"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="EndSequenceNoRequiredFieldValidator" runat="server"
                                ControlToValidate="txtEndSequenceNo" Display="None" meta:resourcekey="RequiredFieldValidator_EndSequenceNo" SetFocusOnError="True"
                                ValidationGroup="EDIT"></asp:RequiredFieldValidator>
                        </EditItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblEndSequenceNo" runat="server" Text='<%# Bind("EndSequenceNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField meta:resourcekey="TemplateField_CompanyAddress">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCompanyAddress" runat="server" Text='<%# Bind("CompanyAddress") %>'
                                CssClass="InputText" Width="300px" MaxLength="20"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="CompanyAddressRequiredFieldValidator" runat="server"
                                ControlToValidate="txtCompanyAddress" Display="None" meta:resourcekey="RequiredFieldValidator_CompanyAddress"
                                SetFocusOnError="True" ValidationGroup="EDIT"></asp:RequiredFieldValidator>
                        </EditItemTemplate>
                        <ItemStyle Width="340px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblCompanyAddress" runat="server" Text='<%# Bind("CompanyAddress") %>'></asp:Label>
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
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td scope="col" style="width: 300px" class="Empty1">
                                <asp:Label ID="Label1" runat="server" meta:resourcekey="Company_Label_CompanyName"></asp:Label>
                            </td>
                            <td scope="col" style="width: 100px">
                                <asp:Label ID="Label2" runat="server" meta:resourcekey="Company_Label_CompanyCode"></asp:Label>
                            </td>
                            <td scope="col" style="width: 200px">
                                <asp:Label ID="Label3" runat="server" meta:resourcekey="Company_Label_CompanyShortName"></asp:Label>
                            </td>
                            <td scope="col" style="width: 100px">
                                <asp:Label ID="Label4" runat="server" meta:resourcekey="Label_BeginSequenceNo"/>
                            </td>
                            <td scope="col" style="width: 100px">
                                <asp:Label ID="Label6" runat="server" meta:resourcekey="Label_EndSequenceNo"/>
                            </td>
                            <td scope="col" style="width: 340px">
                                <asp:Label ID="Label7" runat="server" meta:resourcekey="Label_CompanyAddress"/>
                            </td>
                          
                            <td scope="col" style="width: 100px">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
            </gc:GridView>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="CompanyFormView" EventName="ItemInserted" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="CompanyUpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:FormView ID="CompanyFormView" runat="server" DataKeyNames="CompanyID" CellPadding="0"
                CellSpacing="0" DataSourceID="CompanyObjectDataSource" DefaultMode="Insert" >
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr>
                            <td align="center" style="width: 300px">
                                <asp:TextBox ID="txtCompanyName" runat="server" Text='<%# Bind("CompanyName") %>'
                                    CssClass="InputText" Width="270px" ValidationGroup="INS"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 100px">
                                <asp:TextBox ID="txtCompanyCode" runat="server" Text='<%# Bind("CompanyCode") %>'
                                    CssClass="InputText" Width="80px" ValidationGroup="INS" MaxLength="10"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 200px">
                                <asp:TextBox ID="txtCompanyShortName" runat="server" Text='<%# Bind("CompanyShortName") %>'
                                    CssClass="InputText" Width="170px" ValidationGroup="INS" MaxLength="100"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 100px">
                                <asp:TextBox ID="txtBeginSequenceNo" runat="server" Text='<%# Bind("BeginSequenceNo") %>'
                                    CssClass="InputText" Width="80px" ValidationGroup="INS" MaxLength="10"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 100px">
                                <asp:TextBox ID="txtEndSequenceNo" runat="server" Text='<%# Bind("EndSequenceNo") %>'
                                    CssClass="InputText" Width="80px" ValidationGroup="INS" MaxLength="10"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 340px">
                                <asp:TextBox ID="txtCompanyAddress" runat="server" Text='<%# Bind("CompanyAddress") %>'
                                    CssClass="InputText" Width="300px" ValidationGroup="INS" MaxLength="100"></asp:TextBox>
                            </td>
                           
                            <td align="center" style="width: 100px">
                                <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    Text='<%$Resources:Common,Button_Add %>' ValidationGroup="INS"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                    <asp:RequiredFieldValidator ID="CompanyShortNameRequiredFieldValidator" runat="server"
                        ControlToValidate="txtCompanyShortName" Display="None" meta:resourcekey="RequiredFieldValidator_CompanyShortName"
                        SetFocusOnError="True" ValidationGroup="INS"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="CompanyNameRequiredFieldValidator" runat="server"
                        ControlToValidate="txtCompanyName" Display="None" meta:resourcekey="RequiredFieldValidator_CompanyName" SetFocusOnError="True"
                        ValidationGroup="INS"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="CompanyCodeRequiredFieldValidator" runat="server"
                        ControlToValidate="txtCompanyCode" Display="None" meta:resourcekey="RequiredFieldValidator_CompanyCode" SetFocusOnError="True"
                        ValidationGroup="INS"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                        ControlToValidate="txtBeginSequenceNo" Display="None" meta:resourcekey="RequiredFieldValidator_BeginSequenceNo" SetFocusOnError="True"
                        ValidationGroup="INS"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                        ControlToValidate="txtEndSequenceNo" Display="None" meta:resourcekey="RequiredFieldValidator_EndSequenceNo" SetFocusOnError="True"
                        ValidationGroup="INS"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                        ControlToValidate="txtCompanyAddress" Display="None" meta:resourcekey="RequiredFieldValidator_CompanyAddress" SetFocusOnError="True"
                        ValidationGroup="INS"></asp:RequiredFieldValidator>
                    <asp:ValidationSummary ID="ValidationSummaryInsert" runat="server" ShowMessageBox="True"
                        ShowSummary="False" ValidationGroup="INS" />
                </InsertItemTemplate>
            </asp:FormView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="CompanyObjectDataSource" runat="server" InsertMethod="InsertCompany"
        SelectMethod="GetCompany" TypeName="BusinessObjects.MasterDataBLL" UpdateMethod="UpdateCompany">
        <UpdateParameters>
            <asp:Parameter Name="CompanyID" Type="Int32" />
            <asp:Parameter Name="CompanyCode" Type="String" />
            <asp:Parameter Name="CompanyShortName" Type="String" />
            <asp:Parameter Name="CompanyName" Type="String" />
            <asp:Parameter Name="BeginSequenceNo" Type="Int32" />
            <asp:Parameter Name="EndSequenceNo" Type="Int32" />
            <asp:Parameter Name="CompanyAddress" Type="String" />
          
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="CompanyCode" Type="String" />
            <asp:Parameter Name="CompanyShortName" Type="String" />
            <asp:Parameter Name="CompanyName" Type="String" />
            <asp:Parameter Name="BeginSequenceNo" Type="Int32" />
            <asp:Parameter Name="EndSequenceNo" Type="Int32" />
            <asp:Parameter Name="CompanyAddress" Type="String" />
            
        </InsertParameters>
    </asp:ObjectDataSource>
</asp:Content>
