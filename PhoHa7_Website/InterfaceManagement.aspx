<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage2.master" AutoEventWireup="true" CodeFile="InterfaceManagement.aspx.cs" Inherits="UsersManagement" %>

<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxFormLayout" TagPrefix="dx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlace1" runat="Server">
    <dx:ASPxRoundPanel ID="ASPxRoundPanelUsersManager" Width="100%" Height="100%" runat="server" Theme="Metropolis" HeaderText="Interface Management">
        <PanelCollection>
            <dx:PanelContent>


                <dx:ASPxGridView ID="gridInterface" ClientInstanceName="gridInterface" runat="server"
                    KeyFieldName="Syso_ID" AutoGenerateColumns="False" Width="100%" Theme="Metropolis" OnRowUpdating="gridCategoriesTab_RowUpdating" OnRowDeleting="gridCategoriesTab_RowDeleting" OnRowInserting="gridCategoriesTab_RowInserting">
                    <Columns>
                        <dx:GridViewCommandColumn VisibleIndex="0" ButtonType="Button">
                            <EditButton Visible="True"></EditButton>
                            <NewButton Visible="True"></NewButton>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataColumn FieldName="Syso_ID" VisibleIndex="1" Caption="ID">
                            <CellStyle Font-Bold="True" Font-Size="15pt">
                            </CellStyle>
                        </dx:GridViewDataColumn>
                         <dx:GridViewDataColumn FieldName="Syso_Name" VisibleIndex="2" Caption="Name">
                            <CellStyle Font-Bold="True" Font-Size="15pt">
                            </CellStyle>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="Syso_NameEn" VisibleIndex="3" Caption="English Name">
                            <CellStyle Font-Bold="True" Font-Size="15pt">
                            </CellStyle>
                        </dx:GridViewDataColumn>
                         <dx:GridViewDataColumn FieldName="Syso_Description" VisibleIndex="4" Caption="Description">
                            <CellStyle Font-Bold="True" Font-Size="15pt">
                            </CellStyle>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="Syso_Actived" VisibleIndex="5" Caption="Active">
                            <CellStyle Font-Bold="True" Font-Size="15pt">
                            </CellStyle>
                        </dx:GridViewDataColumn>

                    </Columns>
                    <SettingsBehavior AllowSelectSingleRowOnly="True" AllowSelectByRowClick="True" ConfirmDelete="True" />
                    <SettingsPager PageSize="20">
                    </SettingsPager>
                    <SettingsEditing Mode="PopupEditForm" />
                    <SettingsText ConfirmDelete="Confirm Delete? Are you sure?" />
                    <SettingsPopup>
                        <EditForm Height="200px" HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="NotSet" Width="500px" />
                    </SettingsPopup>
                </dx:ASPxGridView>


            </dx:PanelContent>
        </PanelCollection>
        <BorderBottom BorderStyle="None" />
    </dx:ASPxRoundPanel>
</asp:Content>