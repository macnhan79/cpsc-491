<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage2.master" AutoEventWireup="true" CodeFile="ChildProducts.aspx.cs" Inherits="UsersManagement" %>

<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxFormLayout" TagPrefix="dx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlace1" runat="Server">
    
    <dx:ASPxRoundPanel ID="ASPxRoundPanelUsersManager" Width="100%" Height="100%" runat="server" Theme="Metropolis" HeaderText="Product With">
        <PanelCollection>
            <dx:PanelContent>
                <dx:ASPxGridView ID="gridWith" ClientInstanceName="gridWith" runat="server"
                    KeyFieldName="ProductID" AutoGenerateColumns="False" Width="100%" Theme="Metropolis" 
                    OnRowUpdating="gridWith_RowUpdating" OnRowDeleting="gridWith_RowDeleting" OnRowInserting="gridWith_RowInserting">
                    <Columns>
                         <dx:GridViewCommandColumn VisibleIndex="0" ButtonType="Button">
                            <EditButton Visible="True"></EditButton>
                            <NewButton Visible="True"></NewButton>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataColumn FieldName="ProductName" VisibleIndex="2" Caption="Name">
                            <CellStyle Font-Bold="True" Font-Size="15pt">
                            </CellStyle>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataTextColumn FieldName="KitchenName" VisibleIndex="3" Visible="true" Caption="Kitchen Name">
                            <CellStyle Font-Size="15pt">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <%--<dx:GridViewDataColumn FieldName="BarCode" Visible="false" VisibleIndex="5" Caption="BarCode">
                            <EditFormSettings Visible="True"></EditFormSettings>
                            <CellStyle Font-Size="15pt">
                            </CellStyle>
                        </dx:GridViewDataColumn>--%>
                        <dx:GridViewDataTextColumn PropertiesTextEdit-MaskSettings-Mask="<0..999999>" FieldName="OrderBy" Caption="ZOrder" VisibleIndex="5" Visible="true"  >
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataColumn FieldName="Active" VisibleIndex="5" Caption="Active">
                            <CellStyle Font-Size="15pt">
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

    <dx:ASPxRoundPanel ID="ASPxRoundPanel1" Width="100%" Height="100%" runat="server" Theme="Metropolis" HeaderText="Product Without">
        <PanelCollection>
            <dx:PanelContent>
                <dx:ASPxGridView ID="gridWithout" ClientInstanceName="gridWithout" runat="server"
                    KeyFieldName="ProductID" AutoGenerateColumns="False" Width="100%" Theme="Metropolis" 
                    OnRowUpdating="gridWithout_RowUpdating" OnRowDeleting="gridWithout_RowDeleting" OnRowInserting="gridWithout_RowInserting">
                    <Columns>
                         <dx:GridViewCommandColumn VisibleIndex="0" ButtonType="Button">
                            <EditButton Visible="True"></EditButton>
                            <NewButton Visible="True"></NewButton>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataColumn FieldName="ProductName" VisibleIndex="2" Caption="Name">
                            <CellStyle Font-Bold="True" Font-Size="15pt">
                            </CellStyle>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataTextColumn FieldName="KitchenName" VisibleIndex="3" Visible="true" Caption="Kitchen Name">
                            <CellStyle Font-Size="15pt">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <%--<dx:GridViewDataColumn FieldName="BarCode" Visible="false" VisibleIndex="5" Caption="BarCode">
                            <EditFormSettings Visible="True"></EditFormSettings>
                            <CellStyle Font-Size="15pt">
                            </CellStyle>
                        </dx:GridViewDataColumn>--%>
                        <dx:GridViewDataTextColumn PropertiesTextEdit-MaskSettings-Mask="<0..999999>" FieldName="OrderBy" Caption="ZOrder" VisibleIndex="5" Visible="true"  >
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataColumn FieldName="Active" VisibleIndex="5" Caption="Active">
                            <CellStyle Font-Size="15pt">
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

    <dx:ASPxRoundPanel ID="ASPxRoundPanel2" Width="100%" Height="100%" runat="server" Theme="Metropolis" HeaderText="Product Custom">
        <PanelCollection>
            <dx:PanelContent>
                <dx:ASPxGridView ID="gridCustom" ClientInstanceName="gridCustom" runat="server"
                    KeyFieldName="ProductID" AutoGenerateColumns="False" Width="100%" Theme="Metropolis" 
                    OnRowUpdating="gridCustom_RowUpdating" OnRowDeleting="gridCustom_RowDeleting" OnRowInserting="gridCustom_RowInserting">
                    <Columns>
                         <dx:GridViewCommandColumn VisibleIndex="0" ButtonType="Button">
                            <EditButton Visible="True"></EditButton>
                            <NewButton Visible="True"></NewButton>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataColumn FieldName="ProductName" VisibleIndex="2" Caption="Name">
                            <CellStyle Font-Bold="True" Font-Size="15pt">
                            </CellStyle>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataTextColumn FieldName="KitchenName" VisibleIndex="3" Visible="true" Caption="Kitchen Name">
                            <CellStyle Font-Size="15pt">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                       <%-- <dx:GridViewDataColumn FieldName="BarCode" Visible="false" VisibleIndex="5" Caption="BarCode">
                            <EditFormSettings Visible="True"></EditFormSettings>
                            <CellStyle Font-Size="15pt">
                            </CellStyle>
                        </dx:GridViewDataColumn>--%>
                        <dx:GridViewDataTextColumn PropertiesTextEdit-MaskSettings-Mask="<0..999999>" FieldName="OrderBy" Caption="ZOrder" VisibleIndex="5" Visible="true"  >
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataColumn FieldName="Active" VisibleIndex="5" Caption="Active">
                            <CellStyle Font-Size="15pt">
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