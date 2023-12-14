<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage2.master" AutoEventWireup="true" CodeFile="SQLQuery.aspx.cs" Inherits="UsersManagement" %>

<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxFormLayout" TagPrefix="dx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlace1" runat="Server">
    <dx:ASPxRoundPanel ID="ASPxRoundPanelUsersManager" Width="100%" Height="100%" runat="server" Theme="Metropolis" HeaderText="SQL Query">
        <PanelCollection>
            <dx:PanelContent>
                <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server">
                    <Items>
                        <dx:LayoutItem Caption="Layout Item" ShowCaption="False">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                    <dx:ASPxMemo ID="ASPxMemo1" Text="delete from DaySales" runat="server" Height="150px" Width="300px"></dx:ASPxMemo>
                                    <dx:ASPxButton ID="ASPxButton1" runat="server" Text="Submit" OnClick="ASPxButton1_Click"></dx:ASPxButton>
                                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="ASPxLabel"></dx:ASPxLabel>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Layout Item" ShowCaption="False">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">

                                    <dx:ASPxGridView ID="gridDictionary" ClientInstanceName="gridDictionary" runat="server"
                                        AutoGenerateColumns="true" Width="100%" Theme="Metropolis"
                                        OnRowUpdating="gridUsersManagement_RowUpdating" OnRowDeleting="gridUsersManagement_RowDeleting" OnRowInserting="gridUsersManagement_RowInserting">

                                        <SettingsBehavior AllowSelectSingleRowOnly="True" AllowSelectByRowClick="True" />
                                        <SettingsPager PageSize="100">
                                        </SettingsPager>
                                    </dx:ASPxGridView>

                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                    </Items>
                </dx:ASPxFormLayout>




            </dx:PanelContent>
        </PanelCollection>
        <BorderBottom BorderStyle="None" />
    </dx:ASPxRoundPanel>
</asp:Content>




<%--<dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="gridUsersManagement" runat="server" 
                    KeyFieldName="EmployeeID" AutoGenerateColumns="False" Width="100%" Theme="Metropolis" OnRowUpdating="gridUsersManagement_RowUpdating" OnRowDeleting="gridUsersManagement_RowDeleting" OnRowInserting="gridUsersManagement_RowInserting" >
                    <Columns>
                        <dx:GridViewCommandColumn VisibleIndex="0" ButtonType="Button">
                            <EditButton Visible="True"></EditButton>
                            <NewButton Visible="True"></NewButton>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataColumn FieldName="FullName" VisibleIndex="1" Caption="Name">
                            <CellStyle Font-Bold="True" Font-Size="15pt">
                            </CellStyle>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="SecurityCode" VisibleIndex="2" Visible="false" Caption="Passcode">
                            <CellStyle Font-Bold="True" Font-Size="15pt">
                            </CellStyle>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="Active" VisibleIndex="3" Caption="Active">
                            <CellStyle Font-Bold="True" Font-Size="15pt">
                            </CellStyle>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="Administrator" Visible="false" Caption="Administrator">
                            <CellStyle Font-Bold="True" Font-Size="15pt">
                            </CellStyle>
                        </dx:GridViewDataColumn>
                        <dx:GridViewCommandColumn ShowSelectCheckbox="false" VisibleIndex="3" ButtonType="Button">
                            <DeleteButton Visible="True"></DeleteButton>
                        </dx:GridViewCommandColumn>
                    </Columns>
                    <SettingsBehavior AllowSelectSingleRowOnly="True" AllowSelectByRowClick="True" ConfirmDelete="True" />
                    <SettingsPager PageSize="20">
                    </SettingsPager>
                    <SettingsEditing Mode="PopupEditForm" />
                    <SettingsText ConfirmDelete="Confirm Delete? This process will also delete payroll. Are you sure?" />
                    <SettingsPopup>
                        <EditForm Height="200px" HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter" Width="500px" />
                    </SettingsPopup>
                </dx:ASPxGridView>--%>