<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Culture="Auto" UICulture="Auto" CodeFile="Home.aspx.cs" Inherits="Home" %>

<%@ OutputCache Duration="1" NoStore="true" Location="none" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <table width="1240px" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 10px;">
                &nbsp;
            </td>
            <td style="width: 604px;" valign="top">
                <div class="title2" style="width: 590px;">
                    <asp:Label runat="server" meta:resourcekey="TitleLable_Announcement"></asp:Label></div>
                <gc:GridView ID="BulletinGridView" CssClass="GridView" runat="server" AllowPaging="True"
                    AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="BulletinId" DataSourceID="BulletinDS"
                    OnRowDataBound="BulletinGridView_RowDataBound" PageSize="10">
                    <Columns>
                        <asp:TemplateField SortExpression="BulletinTitle" meta:resourcekey="BulletinGridView_TemplateField_Title">
                            <ItemTemplate>
                                <asp:HyperLink ID="TitleLink" runat="server" Text='<%# Bind("BulletinTitle") %>' />
                            </ItemTemplate>
                            <ItemStyle Width="400px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="CreateTime" meta:resourcekey="BulletinGridView_TemplateField_CreateOn">
                            <ItemTemplate>
                                <asp:Label ID="CreateTimeLabel" runat="server" Text='<%# Eval("CreateTime","{0:yyyy-MM-dd HH:mm}") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="150px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:CheckBoxField DataField="IsHot" SortExpression="IsHot" meta:resourcekey="BulletinGridView_TemplateField_IsHot">
                            <ItemStyle Width="47px" HorizontalAlign="Center" />
                        </asp:CheckBoxField>
                    </Columns>
                    <HeaderStyle CssClass="Header" />
                    <EmptyDataTemplate>
                        <table>
                            <tr>
                                <td style="width: 400px;" class="Empty1">
                                    <asp:Label runat="server" meta:resourcekey="Bulletin_Label_Title"></asp:Label>
                                </td>
                                <td style="width: 150px;">
                                    <asp:Label runat="server" meta:resourcekey="Bulletin_Label_CreateOn"></asp:Label>
                                </td>
                                <td style="width: 47px;">
                                    <asp:Label runat="server" meta:resourcekey="Bulletin_Label_IsHot"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center;" class="Empty2 noneLabel">
                                    <asp:Label ID="Label3" runat="server" Text='<%$Resources:Common,Label_None %>'></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                </gc:GridView>
                <asp:ObjectDataSource ID="BulletinDS" runat="server" SelectMethod="GetPageInActive"
                    TypeName="BusinessObjects.MasterDataBLL" EnablePaging="True" SelectCountMethod="TotalCount"
                    SortParameterName="sortExpression">
                    <SelectParameters>
                        <asp:Parameter Name="queryExpression" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:ObjectDataSource ID="BulletinDetailDS" runat="server" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="GetBulletinById" TypeName="BusinessObjects.MasterDataBLL">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="BulletinGridView" Name="BulletinId" PropertyName="SelectedValue"
                            Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <br />
                <br />
                <div class="title2" style="width: 590px;">
                    <asp:Label runat="server" meta:resourcekey="TitleLable_Draft"></asp:Label></div>
                <gc:GridView ID="gvMyDraft" runat="server" CssClass="GridView" DataSourceID="odsMyDraft"
                    AutoGenerateColumns="False" DataKeyNames="FormID" AllowPaging="True" AllowSorting="True"
                    PageSize="10" OnRowDataBound="gvMyDraft_RowDataBound" CellPadding="0" CellSpacing="0">
                    <Columns>
                        <asp:TemplateField SortExpression="FormTypeName" meta:resourcekey="Form_TemplateField_FormType">
                            <ItemTemplate>
                                &nbsp;<asp:LinkButton ID="lblFormType" runat="server" CausesValidation="False" CommandName="Select"
                                    Text='<%# Bind("FormTypeName") %>'></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="250px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="LastModified" meta:resourcekey="Form_TemplateField_LastModified">
                            <ItemTemplate>
                                <asp:Label ID="lblCreateDate" runat="server" Text='<%# Bind("LastModified", "{0:yyyy-MM-dd HH:mm}") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="348px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="Header" />
                    <EmptyDataTemplate>
                        <table>
                            <tr class="Header">
                                <td style="width: 250px;" class="Empty1">
                                    <asp:Label runat="server" meta:resourcekey="Form_Label_FormType"></asp:Label>
                                </td>
                                <td style="width: 348px;">
                                    <asp:Label runat="server" meta:resourcekey="Form_Label_LastModified"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" class="Empty2 noneLabel">
                                    <asp:Label ID="Label2" runat="server" Text='<%$Resources:Common,Label_None %>'></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <SelectedRowStyle CssClass="SelectedRow" />
                </gc:GridView>
                <asp:ObjectDataSource ID="odsMyDraft" runat="server" TypeName="BusinessObjects.FormQueryBLL"
                    SelectMethod="GetPagedFormView" EnablePaging="True" SelectCountMethod="QueryFormViewCount"
                    SortParameterName="sortExpression">
                    <SelectParameters>
                        <asp:Parameter Name="queryExpression" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
            <td style="width: 603px;" valign="top">
                <div class="title2" style="width: 590px;">
                    <asp:Label runat="server" meta:resourcekey="TitleLable_MyAwating"></asp:Label></div>
                <gc:GridView CssClass="GridView" ID="gvMyAwaiting" runat="server" DataSourceID="odsMyAwaiting"
                    AutoGenerateColumns="False" DataKeyNames="FormId" AllowPaging="True" AllowSorting="True"
                    PageSize="10" OnRowDataBound="gvMyAwaiting_RowDataBound" CellPadding="0" CellSpacing="0"
                    Width="600px">
                    <Columns>
                        <asp:TemplateField Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblFormId" runat="server" Text='<%# Bind("FormId") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="FormTypeName" meta:resourcekey="Form_TemplateField_FormType">
                            <ItemTemplate>
                                <asp:Label ID="lblFormType" runat="server" Text='<%# Bind("FormTypeName") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="150px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="FormNo" meta:resourcekey="Form_TemplateField_FormNo">
                            <ItemTemplate>
                                <asp:LinkButton ID="lblFormNo" runat="server" CausesValidation="False" CommandName="Select"
                                    Text='<%# Bind("FormNo") %>'></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="150px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="StuffName" meta:resourcekey="Form_TemplateField_ApplyUser">
                            <ItemTemplate>
                                <asp:Label ID="lblStuffuserId" runat="server" Text='<%# Bind("StuffName") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="150px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="SubmitDate" meta:resourcekey="Form_TemplateField_ApplyDate">
                            <ItemTemplate>
                                <asp:Label ID="lblSubmitDate" runat="server" Text='<%# Bind("SubmitDate", "{0:yyyy-MM-dd HH:mm}") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="146px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="Header" />
                    <EmptyDataTemplate>
                        <table>
                            <tr class="Header">
                                <td style="width: 150px;" class="Empty1">
                                    <asp:Label runat="server" meta:resourcekey="Form_Label_FormType"></asp:Label>
                                </td>
                                <td style="width: 150px;">
                                    <asp:Label runat="server" meta:resourcekey="Form_Label_FormNo"></asp:Label>
                                </td>
                                <td style="width: 150px;">
                                    <asp:Label runat="server" meta:resourcekey="Form_Label_ApplyUser"></asp:Label>
                                </td>
                                <td style="width: 146px;">
                                    <asp:Label runat="server" meta:resourcekey="Form_Label_ApplyDate"></asp:Label>
                                </td>
                            </tr>
                            <tr class="Empty2">
                                <td colspan="4" class="Empty2 noneLabel">
                                    <asp:Label runat="server" Text='<%$Resources:Common,Label_None %>'></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <SelectedRowStyle CssClass="SelectedRow" />
                </gc:GridView>
                <asp:ObjectDataSource ID="odsMyAwaiting" runat="server" TypeName="BusinessObjects.FormQueryBLL"
                    SelectMethod="GetPagedFormView" EnablePaging="True" SelectCountMethod="QueryFormViewCount"
                    SortParameterName="sortExpression">
                    <SelectParameters>
                        <asp:Parameter Name="queryExpression" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <br />
                <br />
                <div class="title2" style="width: 590px;">
                    <asp:Label runat="server" meta:resourcekey="TitleLable_MySubmission"></asp:Label></div>
                <gc:GridView ID="gvMySubmitted" CssClass="GridView" runat="server" DataSourceID="odsMySubmitted"
                    AutoGenerateColumns="False" DataKeyNames="FormID" AllowPaging="True" AllowSorting="True"
                    PageSize="10" OnRowDataBound="gvMySubmitted_RowDataBound" CellPadding="0" CellSpacing="0">
                    <Columns>
                        <asp:TemplateField SortExpression="FormTypeName" meta:resourcekey="Form_TemplateField_FormType">
                            <ItemTemplate>
                                <asp:Label ID="lblFormType" runat="server" Text='<%# Bind("FormTypeName") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="100px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="FormNo" meta:resourcekey="Form_TemplateField_FormNo">
                            <ItemTemplate>
                                <asp:LinkButton ID="lblFormNo" runat="server" CausesValidation="False" CommandName="Select"
                                    Text='<%# Bind("FormNo") %>'></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="120px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="StatusID" meta:resourcekey="Form_TemplateField_FormStatus">
                            <ItemTemplate>
                                <asp:Label ID="lblStatus" runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="80px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="SubmitDate" meta:resourcekey="Form_TemplateField_LastModified">
                            <ItemTemplate>
                                <asp:Label ID="lblSubmitDate" runat="server" Text='<%# Bind("SubmitDate", "{0:yyyy-MM-dd HH:mm}") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="146px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField  meta:resourcekey="Form_TemplateField_CurrentApprover">
                            <ItemTemplate>
                                <asp:Label ID="lblCurrentApprover" runat="server" Text='<%#GetCurrentUserByInturnID(Eval("InTurnUserIds")) %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="150px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="Header" />
                    <EmptyDataTemplate>
                        <table>
                            <tr class="Header">
                                <td style="width: 100px;" class="Empty1">
                                    <asp:Label runat="server" meta:resourcekey="Form_Label_FormType"></asp:Label>
                                </td>
                                <td style="width: 120px;">
                                    <asp:Label runat="server" meta:resourcekey="Form_Label_FormNo"></asp:Label>
                                </td>
                                <td style="width: 80px;">
                                    <asp:Label runat="server" meta:resourcekey="Form_Label_FormStatus"></asp:Label>
                                </td>
                                <td style="width: 146px;">
                                    <asp:Label runat="server" meta:resourcekey="Form_Label_ApplyDate"></asp:Label>
                                </td>
                                <td style="width: 146px;">
                                    <asp:Label runat="server" meta:resourcekey="Form_Label_CurrentApprover"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="5" class="Empty2 noneLabel">
                                    <asp:Label ID="Label1" runat="server" Text='<%$Resources:Common,Label_None %>'></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <SelectedRowStyle CssClass="SelectedRow" />
                </gc:GridView>
                <asp:ObjectDataSource ID="odsMySubmitted" runat="server" TypeName="BusinessObjects.FormQueryBLL"
                    SelectMethod="GetPagedFormView" EnablePaging="True" SelectCountMethod="QueryFormViewCount"
                    SortParameterName="sortExpression">
                    <SelectParameters>
                        <asp:Parameter Name="queryExpression" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
        </tr>
    </table>
</asp:Content>
