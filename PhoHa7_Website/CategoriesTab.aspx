<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage2.master" AutoEventWireup="true" CodeFile="CategoriesTab.aspx.cs" Inherits="UsersManagement" %>

<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxFormLayout" TagPrefix="dx" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlace1" runat="Server">
    <script type="text/javascript">
        function click() {

        }

        var textSeparator = ",";
        var index = -1;

        function OnListBoxSelectionChanged(UserList, args) {
            debugger;


            //if (args.index == 0)
            //    args.isSelected ? UserList.SelectIndices([0]) : UserList.UnselectIndices([0]);

            //UpdateSelectAllItemState();

            UpdateText();

        }

        function OnInit(s, e) {
            //UpdateText();
        }

        function UpdateText() {
            debugger;
            var selectedItems = listBox.GetSelectedItems();
            checkbox.SetText(GetSelectedItemsText(selectedItems));
        }

        function SynchronizeListBoxValues(dropDown, args) {
            debugger;
            listBox.UnselectAll();
            var texts = dropDown.GetText().split(textSeparator);
            var values = GetValuesByTexts(texts);

            listBox.SelectValues(values);
            document.getElementById("cbHidden").value = values;
            //    UpdateSelectAllItemState();
            UpdateText(); // for remove non-existing texts
        }

        function GetSelectedItemsText(items) {
            debugger;
            var texts = [];
            for (var i = 0; i < items.length; i++)
                if (items[i].index >= 0)
                    texts.push(items[i].text);
            return texts.join(textSeparator);
        }


        function GetValuesByTexts(texts) {
            debugger;
            var actualValues = [];
            var item;
            for (var i = 0; i < texts.length; i++) {
                item = listBox.FindItemByText(texts[i]);;
                if (item != null)
                    actualValues.push(item.value);
            }
            return actualValues;

        }
    </script>
    <asp:HiddenField ID="cbHidden" ClientIDMode="Static" runat="server" />
    <dx:ASPxRoundPanel ID="ASPxRoundPanelUsersManager" Width="100%" Height="100%" runat="server" Theme="Metropolis" HeaderText="Categories Tab Management">
        <PanelCollection>
            <dx:PanelContent>


                <dx:ASPxGridView ID="gridTabItems" ClientInstanceName="gridTabItems" runat="server"
                    KeyFieldName="CategoryID" AutoGenerateColumns="False" Width="100%" Theme="Metropolis"
                    OnRowUpdating="gridTabItems_RowUpdating" OnRowDeleting="gridTabItems_RowDeleting" OnRowInserting="gridTabItems_RowInserting" OnCellEditorInitialize="gridTabItems_CellEditorInitialize">
                    <Columns>
                        <dx:GridViewCommandColumn VisibleIndex="0" ButtonType="Button">
                            <EditButton Visible="True"></EditButton>
                            <NewButton Visible="True"></NewButton>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataColumn FieldName="CategoryName" VisibleIndex="1" Caption="Name">
                                                        <CellStyle Font-Bold="True" Font-Size="15pt">
                            </CellStyle>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="WebName" VisibleIndex="2" Caption="Web Name">
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="WebShowType" Visible="false" Caption="Web Name">
                        </dx:GridViewDataColumn>

                        <dx:GridViewDataDropDownEditColumn FieldName="WebShowTypeName" Caption="Show Type" 
                            PropertiesDropDownEdit-ClientInstanceName="checkbox" PropertiesDropDownEdit-ValidationSettings-RequiredField-IsRequired="true">
                            <PropertiesDropDownEdit>
                                <DropDownWindowTemplate>
                                    <dx:ASPxListBox ID="listBox" runat="server" SelectedValues='<%# Bind("WebShowTypeName") %>' TextField="ProductTypeName" ValueField="ProductTypeCode" SelectionMode="CheckColumn"
                                        ClientInstanceName="listBox" ValueType="System.Int32" OnInit="UserList_Init" OnDataBound="listBox_DataBound" ValidationSettings-Display="None" Width="100%">
                                        <ClientSideEvents SelectedIndexChanged="OnListBoxSelectionChanged" Init="OnInit" />
                                    </dx:ASPxListBox>
                                </DropDownWindowTemplate>
                                <ClientSideEvents CloseUp="SynchronizeListBoxValues" TextChanged="SynchronizeListBoxValues" />

                                <ValidationSettings>
                                    <RequiredField IsRequired="True"></RequiredField>
                                </ValidationSettings>
                            </PropertiesDropDownEdit>
                        </dx:GridViewDataDropDownEditColumn>


                        <dx:GridViewDataTextColumn FieldName="WebOrderBy" VisibleIndex="4" Caption="ZOrder">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataColumn FieldName="WebFlag" VisibleIndex="5" Caption="Web Flag">
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="WebActive" VisibleIndex="5" Caption="Web Active">

                        </dx:GridViewDataColumn>
                    </Columns>
                    <SettingsBehavior AllowSelectSingleRowOnly="True" AllowSelectByRowClick="True" ConfirmDelete="True" />
                    <SettingsPager PageSize="20">
                    </SettingsPager>
                    <SettingsEditing Mode="PopupEditForm" />
                    <SettingsText ConfirmDelete="Confirm Delete? Are you sure?" />
                    <SettingsPopup>
                        <EditForm Height="300px" HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="NotSet" Width="500px" />
                    </SettingsPopup>
                </dx:ASPxGridView>


            </dx:PanelContent>
        </PanelCollection>
        <BorderBottom BorderStyle="None" />
    </dx:ASPxRoundPanel>
</asp:Content>



<%--<dx:GridViewDataComboBoxColumn FieldName="WebShowType" VisibleIndex="2" Caption="Show Type">
                            <PropertiesComboBox>
                                <Items>
                                    <dx:ListEditItem Text="Menu Items" Value="0" />
                                    <dx:ListEditItem Text="Extra Items" Value="13" />
                                </Items>
                            </PropertiesComboBox>
                            <EditItemTemplate>
                                <dx:ASPxComboBox ID="roleID" runat="server" Width="150px" Value='<%# Bind("WebShowType") %>' ValueType="System.String" Theme="Metropolis">
                                    <Items>
                                        <dx:ListEditItem Text="Menu Items" Value="0" />
                                        <dx:ListEditItem Text="Extra Items" Value="13" />
                                    </Items>
                                </dx:ASPxComboBox>
                            </EditItemTemplate>
                            <CellStyle Font-Bold="True" Font-Size="15pt">
                            </CellStyle>
                        </dx:GridViewDataComboBoxColumn>--%>