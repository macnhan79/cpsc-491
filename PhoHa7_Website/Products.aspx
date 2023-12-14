<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage2.master" AutoEventWireup="true" CodeFile="Products.aspx.cs" Inherits="UsersManagement" %>

<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxFormLayout" TagPrefix="dx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlace1" runat="Server">
    <script type="text/javascript">
        function OnFileUploadComplete(s, e) {
            if (e.callbackData !== "") {
                lblFileName.SetText(e.callbackData);
                btnDeleteFile.SetVisible(true);
                callback.PerformCallback(lblFileName.GetText());
            }
        }
        function OnClick(s, e) {
            callback.PerformCallback(lblFileName.GetText());
        }
        function OnCallbackComplete(s, e) {
            if (e.result === "ok") {
                lblFileName.SetText(null);
                btnDeleteFile.SetVisible(false);
            }
        }
    </script>
    <asp:HiddenField ID="hiddenImageName" runat="server" />
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
    </asp:ScriptManager>
    <dx:ASPxRoundPanel ID="ASPxRoundPanelUsersManager" Width="100%" Height="100%" runat="server" Theme="Metropolis" HeaderText="Products Management">
        <PanelCollection>
            <dx:PanelContent>
                <dx:ASPxHiddenField ID="hiddenField" runat="server" ClientInstanceName="hf"></dx:ASPxHiddenField>
                <dx:ASPxGridView ID="gridProductsManagement" ClientInstanceName="gridProductsManagement" runat="server"
                    KeyFieldName="ProductID" AutoGenerateColumns="False" Width="100%" Theme="Metropolis"
                    OnRowUpdating="gridProductsManagement_RowUpdating" OnRowDeleting="gridProductsManagement_RowDeleting" OnRowInserting="gridProductsManagement_RowInserting">
                    <Settings ShowFilterRow="true" />
                    <Columns>
                        <dx:GridViewCommandColumn VisibleIndex="0" ButtonType="Button">
                            <EditButton Visible="True"></EditButton>
                            <NewButton Visible="True"></NewButton>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataColumn FieldName="ProductID" Caption="Product ID" ReadOnly="true" VisibleIndex="1">
                            <DataItemTemplate>
                                <a href='ChildProducts.aspx?id=<%# Eval("ProductID") %>'>More Info...</a>
                            </DataItemTemplate>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="ProductName" VisibleIndex="2" Caption="Name">
                            <Settings AllowAutoFilter="True" FilterMode="DisplayText" ShowFilterRowMenu="True" SortMode="DisplayText" ShowFilterRowMenuLikeItem="True" AllowHeaderFilter="True" />
                            <CellStyle Font-Bold="True" Font-Size="15pt">
                            </CellStyle>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataTextColumn FieldName="KitchenName" VisibleIndex="3" Visible="true" Caption="Kitchen Name">
                            <CellStyle Font-Size="15pt">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataColumn FieldName="Active" VisibleIndex="8" Caption="Active">
                            <CellStyle Font-Size="15pt">
                            </CellStyle>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="PrintBoth" VisibleIndex="9" Visible="false" Caption="Print Both">
                            <EditFormSettings Visible="True"></EditFormSettings>
                            <CellStyle Font-Size="15pt">
                            </CellStyle>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="BarCode" Visible="true" VisibleIndex="5" Caption="BarCode">
                            <EditFormSettings Visible="True"></EditFormSettings>
                            <CellStyle Font-Size="15pt">
                            </CellStyle>
                        </dx:GridViewDataColumn>
                        <%--category--%>
                        <dx:GridViewDataComboBoxColumn FieldName="CategoryID" VisibleIndex="6" Visible="false" Caption="Category">
                            <EditFormSettings Visible="True"></EditFormSettings>
                            <EditItemTemplate>
                                <dx:ASPxComboBox ID="cbCategory" runat="server" AutoPostBack="false" Value='<%# Bind("CategoryID") %>' ValueType="System.Int32"
                                    Theme="Metropolis" DataSourceID="EntityDataSource1" ValueField="CategoryID" TextField="CategoryName">
                                </dx:ASPxComboBox>
                                <asp:EntityDataSource runat="server" ID="EntityDataSource1" DefaultContainerName="Entities" ConnectionString="name=DbProviderEntities"
                                    EnableFlattening="False" EntitySetName="Categories" EntityTypeFilter="Category">
                                </asp:EntityDataSource>
                            </EditItemTemplate>
                            <CellStyle Font-Bold="True" Font-Size="15pt">
                            </CellStyle>
                        </dx:GridViewDataComboBoxColumn>

                        <dx:GridViewDataTextColumn PropertiesTextEdit-MaskSettings-Mask="<0..999999>" FieldName="OrderBy" Caption="ZOrder" VisibleIndex="7" Visible="false">
                            <EditFormSettings Visible="True"></EditFormSettings>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn PropertiesTextEdit-MaskSettings-Mask="<0..999999>" FieldName="Page" Caption="Page" Visible="false" VisibleIndex="7">
                            <EditFormSettings Visible="True"></EditFormSettings>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn PropertiesTextEdit-MaskSettings-Mask="<0..999999>" FieldName="Col" Caption="Col" Visible="false" VisibleIndex="7">
                            <EditFormSettings Visible="True"></EditFormSettings>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn PropertiesTextEdit-MaskSettings-Mask="<0..999999>" FieldName="Row" Caption="Row" Visible="false" VisibleIndex="7">
                            <EditFormSettings Visible="True"></EditFormSettings>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn PropertiesTextEdit-MaskSettings-Mask="<0..999999999g>.<00..99>" FieldName="Price" Caption="Large Price" Visible="false" VisibleIndex="7">
                            <EditFormSettings Visible="True"></EditFormSettings>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn PropertiesTextEdit-MaskSettings-Mask="<0..999999999g>.<00..99>" FieldName="CPrice" Caption="Small Price" Visible="false" VisibleIndex="7">
                            <EditFormSettings Visible="True"></EditFormSettings>
                        </dx:GridViewDataTextColumn>
                        <%--<dx:GridViewDataColumn FieldName="HasSmallSize" Caption="Is Small" Visible="false" VisibleIndex="5" > <EditFormSettings Visible="True"></EditFormSettings></dx:GridViewDataColumn>--%>
                        <dx:GridViewDataColumn FieldName="ProductImage" Caption="Image" Visible="false" VisibleIndex="7">
                            <EditFormSettings Visible="True"></EditFormSettings>
                            <EditItemTemplate>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="conditional">
                                    <Triggers>
                                        <%--<asp:PostBackTrigger ControlID="UploadBtn" />--%>
                                    </Triggers>
                                    <ContentTemplate>
                                        <br />
                                        <table>
                                            <tr>
                                                <td colspan="2">
                                                    <dx:ASPxUploadControl ID="FileUpload1" runat="server" UploadMode="Standard"
                                                        ShowUploadButton="true" Width="100%"
                                                        OnFileUploadComplete="ASPxUploadControl1_FileUploadComplete">
                                                        <ClientSideEvents FileUploadComplete="OnFileUploadComplete" />
                                                    </dx:ASPxUploadControl>
                                                    <%-- <asp:FileUpload ID="FileUpload1" runat="server" />--%>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" ClientInstanceName="lblFileName"></dx:ASPxLabel>
                                                </td>
                                                <td align="right">
                                                    <dx:ASPxButton ID="ASPxButton1" RenderMode="Link" runat="server" ClientVisible="false" ClientInstanceName="btnDeleteFile" AutoPostBack="false" Text="Submit Image">
                                                        <ClientSideEvents Click="OnClick" />
                                                    </dx:ASPxButton>

                                                    <%-- <asp:Button ID="Button1" runat="server"
                        Text="Upload1" OnClick="UploadBtn_Click" />
                                                    <dx:ASPxButton ID="UploadBtn" runat="server" Text="Upload" AutoPostBack="false"
                                                        ClientSideEvents-Click="function(s, e) { ASPxCallback1.PerformCallback(); }" />--%>
                                                    <dx:ASPxCallback ID="ASPxCallback1" ClientIDMode="Static" ClientInstanceName="ASPxCallback1" runat="server"
                                                        OnCallback="ASPxCallback1_Callback">
                                                    </dx:ASPxCallback>
                                                </td>

                                            </tr>
                                        </table>
                                        <%--   
                                <dx:ASPxLabel ID="ASPxLabel1" runat="server" ClientInstanceName="lblFileName"></dx:ASPxLabel>
                                <dx:ASPxButton ID="ASPxButton1" RenderMode="Link" runat="server" ClientVisible="false" ClientInstanceName="btnDeleteFile" AutoPostBack="false" Text="Remove">
                                    <ClientSideEvents Click="OnClick" />
                                </dx:ASPxButton>--%>
                                    </ContentTemplate>
                                </asp:UpdatePanel>



                            </EditItemTemplate>
                        </dx:GridViewDataColumn>

                        <dx:GridViewDataImageColumn FieldName="ProductImage" PropertiesImage-ImageHeight="100px" PropertiesImage-ImageWidth="100px" Caption="Picture" Visible="false" VisibleIndex="7">
                            <EditFormSettings Visible="True"></EditFormSettings>
                            <PropertiesImage ImageUrlFormatString="{0}">
                            </PropertiesImage>
                        </dx:GridViewDataImageColumn>

                        <dx:GridViewCommandColumn ShowSelectCheckbox="false" VisibleIndex="5" ButtonType="Button">
                            <DeleteButton Visible="True"></DeleteButton>
                        </dx:GridViewCommandColumn>

                    </Columns>

                    <SettingsBehavior AllowSelectSingleRowOnly="True" AllowSelectByRowClick="True" ConfirmDelete="True" />
                    <SettingsPager PageSize="20">
                    </SettingsPager>
                    <SettingsEditing Mode="PopupEditForm" />
                    <SettingsText ConfirmDelete="Confirm Delete? Are you sure?" />
                    <SettingsPopup>
                        <EditForm Height="450px" HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="Below" Width="600px" />
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