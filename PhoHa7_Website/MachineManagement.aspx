<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage2.master" AutoEventWireup="true" CodeFile="MachineManagement.aspx.cs" Inherits="MachineManagement" %>
<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxFormLayout" TagPrefix="dx" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlace1" Runat="Server">
     <dx:ASPxRoundPanel ID="ASPxRoundPanelMachineManager" Width="100%" Height="100%" runat="server" Theme="Metropolis" HeaderText="Machine Management">
        <PanelCollection>
            <dx:PanelContent>


                <dx:ASPxGridView ID="gridMachineManagement" ClientInstanceName="gridMachineManagement" runat="server" 
                    KeyFieldName="MachineID" AutoGenerateColumns="False" Width="100%" Theme="Metropolis" OnRowDeleting="gridMachineManagement_RowDeleting" OnRowInserting="gridMachineManagement_RowInserting" OnRowUpdating="gridMachineManagement_RowUpdating" >
                    <Columns>
                        <dx:GridViewCommandColumn VisibleIndex="0" ButtonType="Button">
                            <EditButton Visible="True"></EditButton>
                            <NewButton Visible="True"></NewButton>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataColumn FieldName="MachineName" VisibleIndex="1" Caption="Machine Name">
                            <CellStyle Font-Bold="True" Font-Size="15pt">
                            </CellStyle>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="MachineIpAdress" VisibleIndex="1" Caption="IP Address">
                            <CellStyle Font-Bold="True" Font-Size="15pt">
                            </CellStyle>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataCheckColumn FieldName="MachineActive" VisibleIndex="2" Caption="Active">
                            <CellStyle Font-Bold="True" Font-Size="15pt">
                            </CellStyle>
                        </dx:GridViewDataCheckColumn>
                       
                        <dx:GridViewCommandColumn ShowSelectCheckbox="false" VisibleIndex="3" ButtonType="Button">
                            <DeleteButton Visible="True"></DeleteButton>
                        </dx:GridViewCommandColumn>
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

