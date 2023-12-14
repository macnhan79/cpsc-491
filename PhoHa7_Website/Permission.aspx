<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage2.master" AutoEventWireup="true" CodeFile="Permission.aspx.cs" Inherits="Permission" %>

<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxFormLayout" TagPrefix="dx" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlace1" runat="Server">
    <script type="text/javascript">
        function gridUserPermissionSelectionChange() {
            gridPermission.GetSelectedFieldValues('EmployeeID', OnGridSelectionComplete);
            
        }

        function OnGridSelectionComplete(values) {
            gridPermission.PerformCallback(values);
        }
    </script>


    <dx:ASPxRoundPanel ID="ASPxRoundPanelAttendance" Width="100%" Height="100%" runat="server" Theme="Metropolis" HeaderText="Permission Management">
        <PanelCollection>
            <dx:PanelContent>
                <dx:ASPxFormLayout runat="server" ID="ASPxFormLayout1" Theme="Metropolis" ColCount="2">
                    <Items>
                        <dx:LayoutItem Caption="Users List" HorizontalAlign="Left" VerticalAlign="Top">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                    <dx:ASPxGridView ID="gridUsersPermission" ClientInstanceName="gridUsersPermission" runat="server"
                                        KeyFieldName="EmployeeID" AutoGenerateColumns="False" Width="100%" Theme="Metropolis">
                                        <ClientSideEvents RowClick="function(s, e) {
	gridUserPermissionSelectionChange();
}" />
                                        <Columns>
                                            <dx:GridViewDataColumn FieldName="FullName" VisibleIndex="1" Caption="Name">
                                                <CellStyle Font-Bold="True" Font-Size="15pt">
                                                </CellStyle>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="Active" VisibleIndex="3" Caption="Active">
                                                <CellStyle Font-Bold="True" Font-Size="15pt">
                                                </CellStyle>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="EmployeeID" Visible="false"></dx:GridViewDataColumn>
                                        </Columns>
                                        <SettingsBehavior AllowSelectSingleRowOnly="True" ProcessSelectionChangedOnServer="true" AllowSelectByRowClick="True" />
                                        <SettingsPager PageSize="50">
                                        </SettingsPager>
                                        <SettingsEditing Mode="PopupEditForm" />
                                        <SettingsPopup>
                                            <EditForm Height="200px" HorizontalAlign="WindowCenter" VerticalAlign="NotSet" Width="500px" />
                                        </SettingsPopup>
                                    </dx:ASPxGridView>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                            <CaptionSettings Location="Top" />
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Permission" HorizontalAlign="Left" VerticalAlign="Top">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                    <dx:ASPxGridView ID="gridPermission" ClientInstanceName="gridPermission" runat="server"
                                        KeyFieldName="UP_Object_ID;UP_User_ID" AutoGenerateColumns="False" Width="100%" Theme="Metropolis" OnCustomCallback="gridPermission_CustomCallback" OnRowUpdating="gridPermission_RowUpdating"
                                        >
                                        <Columns>
                                            <dx:GridViewCommandColumn VisibleIndex="0" ButtonType="Button">
                                                <EditButton Visible="True"></EditButton>
                                                <NewButton Visible="false"></NewButton>
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewDataColumn FieldName="UP_Object_Name" VisibleIndex="1" Caption="Function">
                                                <CellStyle Font-Bold="True" Font-Size="15pt">
                                                </CellStyle>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="View" VisibleIndex="3" Caption="View">
                                                <CellStyle Font-Bold="True" Font-Size="15pt">
                                                </CellStyle>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="Add" VisibleIndex="3" Caption="Add">
                                                <CellStyle Font-Bold="True" Font-Size="15pt">
                                                </CellStyle>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="Update" VisibleIndex="3" Caption="Update">
                                                <CellStyle Font-Bold="True" Font-Size="15pt">
                                                </CellStyle>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="Delete" VisibleIndex="3" Caption="Delete">
                                                <CellStyle Font-Bold="True" Font-Size="15pt">
                                                </CellStyle>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="Report" VisibleIndex="3" Caption="Report">
                                                <CellStyle Font-Bold="True" Font-Size="15pt">
                                                </CellStyle>
                                            </dx:GridViewDataColumn>
                                        </Columns>
                                        <SettingsBehavior AllowSelectSingleRowOnly="True" AllowSelectByRowClick="True" />
                                        <SettingsPager Mode="ShowAllRecords">
                                        </SettingsPager>
                                        <SettingsEditing Mode="PopupEditForm" />
                                        <SettingsPopup>
                                            <EditForm Height="200px" HorizontalAlign="WindowCenter" VerticalAlign="NotSet" Width="500px" />
                                        </SettingsPopup>
                                    </dx:ASPxGridView>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                            <CaptionSettings Location="Top" />
                        </dx:LayoutItem>
                    </Items>
                </dx:ASPxFormLayout>



            </dx:PanelContent>
        </PanelCollection>
        <BorderBottom BorderStyle="None" />
    </dx:ASPxRoundPanel>
</asp:Content>



