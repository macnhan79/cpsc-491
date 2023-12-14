<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage2.master" AutoEventWireup="true" CodeFile="ErrorsView.aspx.cs" Inherits="UsersManagement" %>

<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxFormLayout" TagPrefix="dx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlace1" runat="Server">
    <dx:ASPxRoundPanel ID="ASPxRoundPanelUsersManager" Width="100%" Height="100%" runat="server" Theme="Metropolis" HeaderText="Errrors Management">
        <PanelCollection>
            <dx:PanelContent>


                <dx:ASPxGridView ID="gridDictionary" ClientInstanceName="gridDictionary" runat="server"
                    KeyFieldName="ErrorID" AutoGenerateColumns="False" Width="100%" Theme="Metropolis">
                    <Columns>
                        <dx:GridViewDataColumn FieldName="ErrorDate" VisibleIndex="1" Caption="Error Date">
                            <CellStyle Font-Bold="True" Font-Size="15pt">
                            </CellStyle>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="ErrorText" VisibleIndex="1" Caption="Error Text">
                            <CellStyle Font-Bold="True" Font-Size="15pt">
                            </CellStyle>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="ErrorDetails" VisibleIndex="1" Caption="Error Details">
                            <CellStyle Font-Bold="True" Font-Size="15pt">
                            </CellStyle>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="ErrorType" VisibleIndex="1" Caption="Error Type">
                            <CellStyle Font-Bold="True" Font-Size="15pt">
                            </CellStyle>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="InnerException" VisibleIndex="1" Caption="Inner Exception">
                            <CellStyle Font-Bold="True" Font-Size="15pt">
                            </CellStyle>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="InnerSource" VisibleIndex="1" Caption="Inner Source">
                            <CellStyle Font-Bold="True" Font-Size="15pt">
                            </CellStyle>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="InnerExceptionStackTrace" VisibleIndex="1" Caption="Inner Exception StackTrace">
                            <CellStyle Font-Bold="True" Font-Size="15pt">
                            </CellStyle>
                        </dx:GridViewDataColumn>
                    </Columns>
                    <SettingsBehavior AllowSelectSingleRowOnly="True" AllowSelectByRowClick="True" ConfirmDelete="True" />
                    <SettingsPager PageSize="20">
                    </SettingsPager>
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