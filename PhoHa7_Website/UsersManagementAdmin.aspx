<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage2.master" AutoEventWireup="true" CodeFile="UsersManagementAdmin.aspx.cs" Inherits="UsersManagementAdmin" %>

<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxFormLayout" TagPrefix="dx" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlace1" runat="Server">
    <dx:ASPxRoundPanel ID="ASPxRoundPanelUsersManager" Width="100%" Height="100%" runat="server" Theme="Metropolis" HeaderText="Users Management Administration">
        <PanelCollection>
            <dx:PanelContent>


                <dx:ASPxGridView ID="gridUsersManagementAdmin" ClientInstanceName="gridUsersManagementAdmin" runat="server"
                    KeyFieldName="EmployeeID" AutoGenerateColumns="False" Width="100%" Theme="Metropolis"
                    OnRowDeleting="gridUsersManagementAdmin_RowDeleting" OnRowInserting="gridUsersManagementAdmin_RowInserting"
                    OnRowUpdating="gridUsersManagementAdmin_RowUpdating" OnParseValue="gridUsersManagementAdmin_ParseValue">
                    <Columns>
                        <dx:GridViewCommandColumn VisibleIndex="0" ButtonType="Button">
                            <EditButton Visible="True"></EditButton>
                            <NewButton Visible="True"></NewButton>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataColumn FieldName="FullName" VisibleIndex="1" Caption="Name">
                            <CellStyle Font-Bold="True" Font-Size="15pt">
                            </CellStyle>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataTextColumn FieldName="SecurityCode" VisibleIndex="2" Caption="Passcode">
                            <PropertiesTextEdit Password="true">
                            </PropertiesTextEdit>
                            <CellStyle Font-Bold="True" Font-Size="15pt">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataColumn FieldName="Rate" VisibleIndex="2" Caption="Pay Rate">
                            <CellStyle Font-Bold="True" Font-Size="15pt">
                            </CellStyle>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="CheckPayRate" VisibleIndex="2" Caption="Pay Rate Check">
                            <CellStyle Font-Bold="True" Font-Size="15pt">
                            </CellStyle>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataCheckColumn FieldName="Active" VisibleIndex="3" Caption="Active">
                            <CellStyle Font-Bold="True" Font-Size="15pt">
                            </CellStyle>
                        </dx:GridViewDataCheckColumn>
                        <dx:GridViewDataComboBoxColumn FieldName="SecureLevel" VisibleIndex="3" Caption="Level">
                            <PropertiesComboBox>
                                <Items>
                                    <dx:ListEditItem Text="System" Value="1" />
                                    <dx:ListEditItem Text="Administration" Value="2" />
                                    <dx:ListEditItem Text="Manager" Value="3" />
                                    <dx:ListEditItem Text="Staff" Value="4" />
                                    <dx:ListEditItem Text="Guest" Value="5" />
                                </Items>
                            </PropertiesComboBox>
                            <EditItemTemplate>
                                <dx:ASPxComboBox ID="roleID" runat="server" Width="150px" Value='<%# Bind("SecureLevel") %>' ValueType="System.Int32" Theme="Metropolis">
                                    <Items>
                                        <dx:ListEditItem Text="Administration" Value="2" />
                                        <dx:ListEditItem Text="Manager" Value="3" />
                                        <dx:ListEditItem Text="Staff" Value="4" />
                                        <dx:ListEditItem Text="Guest" Value="5" />
                                    </Items>
                                </dx:ASPxComboBox>
                            </EditItemTemplate>
                            <CellStyle Font-Bold="True" Font-Size="15pt">
                            </CellStyle>
                        </dx:GridViewDataComboBoxColumn>
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
                        <EditForm Height="200px" HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="NotSet" Width="500px" />
                    </SettingsPopup>
                </dx:ASPxGridView>


            </dx:PanelContent>
        </PanelCollection>
        <BorderBottom BorderStyle="None" />
    </dx:ASPxRoundPanel>
</asp:Content>

