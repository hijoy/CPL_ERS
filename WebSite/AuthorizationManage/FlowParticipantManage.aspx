<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="AuthorizationManage_FlowParticipantManage"
    UICulture="Auto" Culture="Auto" CodeFile="FlowParticipantManage.aspx.cs" %>

<%@ Register Src="../UserControls/StaffControl.ascx" TagName="StaffControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="title">
        <asp:Label ID="Label6" runat="server" meta:resourcekey="Flow_Title1"></asp:Label></div>
    <asp:UpdatePanel ID="upCustomerType" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <gc:GridView ID="gvFlowConfigure" CssClass="GridView" runat="server" DataSourceID="odsFlowConfigure"
                CellPadding="0" AutoGenerateColumns="False" DataKeyNames="FlowID" AllowPaging="false"
                 OnRowDataBound="gvFlowConfigure_RowDataBound" EnableModelValidation="True">
                <Columns>
                    <asp:TemplateField SortExpression="FlowName" meta:resourcekey="FlowConfigureGridView_TemplateField_FlowName">
                        <ItemTemplate>
                            <asp:LinkButton ID="lblFlowName" runat="server" Text='<%# Bind("FlowName") %>' OnClick="lknSelect"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="400px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="FlowID" meta:resourcekey="FlowConfigureGridView_TemplateField_FlowParticipant">
                        <ItemTemplate>
                            <asp:Label ID="lblFlowParticipant" runat="server" Text='<%# Bind("FlowID") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="320px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="FlowTemplateName" meta:resourcekey="FlowConfigureGridView_TemplateField_FlowTemplateName">
                        <ItemTemplate>
                            <asp:Label ID="lblFlowTemplateName" runat="server" Text='<%# Bind("FlowTemplateName") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="240px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="FormTypeId" HeaderText="业务类型">
                        <ItemTemplate>
                            <asp:Label ID="lblFormTypeName" runat="server" Text='<%# Bind("FormTypeId") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="220px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Delete"
                                Text='<%$Resources:Common,Button_Delete %>'></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="79px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td style="width: 400px" class="Empty1">
                                <asp:Label ID="lblFlowName" runat="server" meta:resourcekey="Flow_Label_FlowName"></asp:Label>
                            </td>
                            <td style="width: 320px;">
                                <asp:Label ID="lblFlowParticipant" runat="server" meta:resourcekey="Flow_Label_FlowParticipant"></asp:Label>
                            </td>
                            <td style="width: 240px;">
                                <asp:Label ID="lblFlowTemplateName" runat="server" meta:resourcekey="Flow_Label_FlowTemplateName"></asp:Label>
                            </td>
                            <td style="width: 220px;">
                                <asp:Label ID="lblFormTypeName" runat="server" Text="业务类型"></asp:Label>
                            </td>
                            <td style="width: 79px;">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <SelectedRowStyle CssClass="SelectedRow" />
            </gc:GridView>
            <asp:FormView ID="fvFlowConfigure" runat="server" DataKeyNames="FlowID" DataSourceID="odsFlowConfigure"
                DefaultMode="Insert" EnableModelValidation="True" CellPadding="0" OnDataBound="fvFlowConfigure_DataBound">
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr>
                            <td align="center" style="height: 22px; width: 400px;">
                                <asp:TextBox ID="txtFlowName" runat="server" Text='<%# Bind("FlowName") %>' Width="360px"
                                    CssClass="InputText" MaxLength="30"></asp:TextBox>
                            </td>
                            <td align="center" style="height: 22px; width: 320px;">
                                <asp:TextBox ID="txtFlowParticipant" runat="server" Width="200px" CssClass="InputText"
                                    MaxLength="20" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td align="center" style="height: 22px; width: 240px;">
                                <asp:DropDownList runat="server" ID="ddlDefName" Width="220px">
                                </asp:DropDownList>
                            </td>
                            <td align="center" style="height: 22px; width: 220px;">
                                <asp:DropDownList runat="server" ID="ddlFormType" DataSourceID="odsFormType" DataTextField="FormTypeName"
                                    DataValueField="FormTypeId" Width="200px">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="odsFormType" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                    SelectCommand="SELECT [FormTypeId], [FormTypeName] FROM [FormType]"></asp:SqlDataSource>
                            </td>
                            <td align="center" style="width: 79px;">
                                <asp:LinkButton ID="InsertLinkButton" runat="server" CommandName="Insert" Text='<%$Resources:Common,Button_Add %>'></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </InsertItemTemplate>
            </asp:FormView>
            <asp:ObjectDataSource ID="odsFlowConfigure" runat="server" TypeName="BusinessObjects.AuthorizationBLL"
                InsertMethod="InsertFlowParticipantConfigure" SelectMethod="GetFlowParticipantConfigure"
                OnInserting="odsFlowConfigure_Inserting" DeleteMethod="DeleteFlowParticipantConfigure"
                EnablePaging="false">
                <DeleteParameters>
                    <asp:Parameter Name="FlowID" Type="Int32" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="FlowTemplateName" Type="String" />
                    <asp:Parameter Name="FlowName" Type="String" />
                    <asp:Parameter Name="FormTypeId" Type="Int32" />
                </InsertParameters>
            </asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <asp:UpdatePanel ID="upFlowParticipantDetail" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="StuffPanel" runat="server" style="display: none">
                <div class="title" style="width: 100%">
                    <asp:Label ID="lblFlowTitle" runat="server" meta:resourcekey="Flow_Title2"></asp:Label>
                </div>
                <gc:GridView ID="StuffGridView" CssClass="GridView" runat="server" AutoGenerateColumns="False"
                    DataKeyNames="FlowParticipantConfigureDetailID" DataSourceID="FlowParticipantDetail"
                    CellPadding="0" Width="100%">
                    <HeaderStyle CssClass="Header" />
                    <Columns>
                        <asp:TemplateField SortExpression="StuffName" meta:resourcekey="StuffGridView_TemplateField_StaffName">
                            <ItemTemplate>
                                <asp:Label ID="lblStuffName" runat="server" Text='<%# Bind("StaffName") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="45%" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="UserName" meta:resourcekey="StuffGridView_TemplateField_UserName">
                            <ItemTemplate>
                                <asp:Label ID="lblUserName" runat="server" Text='<%# Bind("UserName") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="45%" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Delete"
                                    Text='<%$Resources:Common,Button_Delete %>'></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <table width="100%">
                            <tr class="Header">
                                <td style="width: 45%" class="Empty1">
                                    <asp:Label ID="lbltitle1" runat="server" Text="员工姓名"></asp:Label>
                                </td>
                                <td style="width: 45%">
                                    <asp:Label ID="lbltitle2" runat="server" Text="用户名"></asp:Label>
                                </td>
                                <td style="width: 10%">
                                    &nbsp
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                </gc:GridView>
                <asp:FormView ID="fvStuff" runat="server" DataKeyNames="FlowParticipantConfigureDetailID"
                    DataSourceID="FlowParticipantDetail" DefaultMode="Insert" EnableModelValidation="True"
                    CellPadding="0" Width="100%">
                    <InsertItemTemplate>
                        <table class="FormView" style="width: 100%">
                            <tr>
                                <td align="center" style="height: 22px; width: 45%;">
                                    <uc1:StaffControl ID="StaffControl1" runat="server" Width="200px" AutoPostBack="true"
                                        OnStaffNameTextChanged="txtStaffName_TextChanged" />
                                </td>
                                <td align="center" style="height: 22px; width: 45%;">
                                    <asp:TextBox ID="txtFlowParticipant" runat="server" Text='' Width="200px" CssClass="InputText"
                                        MaxLength="20" ReadOnly="true"></asp:TextBox>
                                </td>
                                <td align="center" style="width: 10%;">
                                    <asp:LinkButton ID="InsertLinkButton1" runat="server" CommandName="Insert" Text='<%$Resources:Common,Button_Add %>'></asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </InsertItemTemplate>
                </asp:FormView>
                <asp:ObjectDataSource ID="FlowParticipantDetail" runat="server" DeleteMethod="DeleteFlowParticipantConfigureDetailByID"
                    SelectMethod="GetFlowParticipantConfigureDetail" TypeName="BusinessObjects.AuthorizationBLL"
                    OnInserting="FlowParticipantDetail_Inserting" OnObjectCreated="FlowParticipantDetail_ObjectCreated"
                    InsertMethod="AddFlowParticipantConfigureDetail">
                    <DeleteParameters>
                        <asp:Parameter Name="FlowParticipantConfigureDetailID" Type="Int32" />
                    </DeleteParameters>
                    <InsertParameters>
                        <asp:Parameter Name="FlowID" Type="Int32" />
                        <asp:Parameter Name="UserName" Type="Int32" />
                        <asp:Parameter Name="UserID" Type="Int32" />
                        <asp:Parameter Name="StaffName" Type="String" />
                    </InsertParameters>
                </asp:ObjectDataSource>
                <br />
                <div style="margin-top: 20px; width: 100%; text-align: right">
                    <asp:Button ID="SubmitBtn" runat="server" CssClass="button_nor" OnClick="SubmitBtn_Click"
                        Text='<%$Resources:Common,Button_Save %>' />
                    <asp:Button ID="CancelBtn" runat="server" CssClass="button_nor" OnClick="CancelBtn_Click"
                        Text='<%$Resources:Common,Button_Cancel %>' />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
