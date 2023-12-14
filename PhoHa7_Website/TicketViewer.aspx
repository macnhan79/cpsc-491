<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="TicketViewer.aspx.cs" Inherits="TicketViewer" %>



<%--<%@ Master Language="C#" AutoEventWireup="true" CodeFile="MasterPage2.master.cs" Inherits="MasterPage2" %>--%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" class="gr__restaurant_sansoftwareindia_com">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Pho Ha 7</title>
    <link href="favicon.ico" rel="shortcut icon" type="image/x-icon" />

    <link href="css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="css/components-md.min.css" rel="stylesheet" id="style_components" type="text/css" />
    <link href="css/layout.min.css" rel="stylesheet" type="text/css" />
    <link href="css/default.min.css" rel="stylesheet" type="text/css" id="style_color" />
    <script src="js/hide-address-bar.js"></script>

    <link rel="stylesheet" href="table_responsive/css/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="css/style_table.css" />
    <link href="css/style_002.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
        .btnLogout {
            border: 1px solid #FFF;
            border-radius: 14px;
            top: 0px;
            right: 8px;
            background: #3598db;
            height: 39px;
            color: #FFF;
            padding: 9px 7px 8px 9px;
        }
    </style>
</head>
<body class="page-container-bg-solid page-md" data-gr-c-s-loaded="true">
    <form id="form1" runat="server" enctype="multipart/form-data">
        <%--    <asp:ScriptManager ID="ScriptManager1" runat="server" />--%>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:Timer ID="Timer1" OnTick="Timer1_Tick" runat="server" Interval="4000"></asp:Timer>
        <dx:ASPxLoadingPanel ID="LoadingPanel1" Modal="true" runat="server" ClientInstanceName="LoadingPanel1"></dx:ASPxLoadingPanel>
        <div id="overlay">
            <div id="modalcontainer"></div>
        </div>
        <div class="clearfix ">
            <div id="main_panel_container" class="left">
                <div id="dashboard" style="padding: 0px!important;">
                    <div class="btn-group btn-group-justified" style="height: 60px;">
                        <a class="btn btn-primary" style="background: #3598db; border: #00c0ef; font-size: 17px" href="/table.aspx">Tables</a>
                        <a class="btn btn-primary" style="background: #3598db; border: #00c0ef; font-size: 17px" href="/MyOrders.aspx">My Order</a>
                        <a class="btn btn-primary" style="background: #3598db; border: #00c0ef; font-size: 17px" href="/MyAccount.aspx">My Account</a>
                        <a class="btn btn-primary" style="background: #3598db!important; border: none;">
                            <div class="input-group-btn">
                                <asp:Button ID="btnLogout" runat="server" CssClass="btn btn-default btnLogout"
                                    Text='HI' OnClick="btnLogout_Click" UseSubmitBehavior="False" />
                            </div>
                        </a>
                    </div>
                    <div style="clear: both;"></div>

                    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:DataList runat="server" ID="listTicket" DataKeyField="TicketID" CssClass="confirm" OnItemDataBound="listTicket_ItemDataBound" RepeatColumns="4" RepeatDirection="Horizontal">
                                <ItemTemplate>
                                  <div id="gridviewDetails" style="padding: 10px">  <h3 class="confirm new" style="font-weight:bolder">Table
                <asp:Label ID="lblaOrder" runat="server" Text='<%#Eval("TableName") %>'></asp:Label>
                                    </h3>
                                    <dx:ASPxHiddenField ID="hiddenTicketID" runat="server"></dx:ASPxHiddenField>
                                    <!-- end confirm_question -->
                                    
                                        <dx:ASPxGridView ID="gridResponses" ClientInstanceName="gridResponses" runat="server"
                                            KeyFieldName="SaleItemID" AutoGenerateColumns="False" Width="250px" Theme="Metropolis" CssClass="girdItems" OnHtmlRowPrepared="gridResponses_HtmlRowPrepared">
                                            <%-- OnPreRender="grid_PreRender"--%>
                                            <Settings VerticalScrollBarMode="Auto" VerticalScrollableHeight="260" />
                                            <%--RowClick="function(s,e){grid.PerformCallback('SelectionChanged');}" --%>
                                            <%--<ClientSideEvents EndCallback="function(s, e) { OnEndCallback(s,e); }" />--%>
                                            <Columns>
                                                <%--                    <dx:GridViewCommandColumn ButtonType="Button" VisibleIndex="0" Width="65px">
                        <CustomButtons>
                            <dx:GridViewCommandColumnCustomButton ID="btnDetails" Text="Update" />
                        </CustomButtons>
                    </dx:GridViewCommandColumn>--%>
                                                <dx:GridViewDataColumn FieldName="Qty" VisibleIndex="1" CellStyle-Font-Size="18" CellStyle-Font-Bold="true" Caption="SL" Width="40px">
                                                    <%--<CellStyle Font-Size="14pt">
                                                    </CellStyle>--%>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="CustomKitchenName" VisibleIndex="2" Caption="Name" CellStyle-Font-Size="18" CellStyle-Font-Bold="true">
<%--                                                    <CellStyle Font-Bold="True" Font-Size="14pt">--%>
                                                    <%--</CellStyle>--%>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataCheckColumn FieldName="TakeOut" VisibleIndex="3" Caption="ToGo" Width="40px">
                                                    <PropertiesCheckEdit ValueGrayed="False">
                                                    </PropertiesCheckEdit>
                                                </dx:GridViewDataCheckColumn>
                                                <dx:GridViewDataCheckColumn FieldName="SmallSize" VisibleIndex="4" Caption="Small" Width="40px" Visible="False">
                                                    <PropertiesCheckEdit ValueGrayed="False">
                                                    </PropertiesCheckEdit>
                                                </dx:GridViewDataCheckColumn>
                                                <dx:GridViewDataColumn FieldName="BarCode" VisibleIndex="5" Caption="Code" Width="60px" Visible="false">
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="SaleItemID" VisibleIndex="8" Visible="False">
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="BarCode" VisibleIndex="10" Visible="False">
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="ProductID" VisibleIndex="14" Visible="False">
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="KitchenName" VisibleIndex="11" Visible="False">
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="CategoryID" VisibleIndex="12" Visible="False">
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="LPrice" VisibleIndex="13" Visible="False">
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="SPrice" VisibleIndex="14" Visible="false">
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="Extra" VisibleIndex="7" Visible="False">
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="EmployeeName" VisibleIndex="9" Visible="False">
                                                </dx:GridViewDataColumn>
                                                <%--<dx:GridViewCommandColumn ButtonType="Button" VisibleIndex="6" Width="65px">
                        <CustomButtons>
                            <dx:GridViewCommandColumnCustomButton ID="btnDel" Text="Delete" />
                        </CustomButtons>
                    </dx:GridViewCommandColumn>--%>
                                            </Columns>

                                            <SettingsBehavior AllowSelectSingleRowOnly="True" ProcessSelectionChangedOnServer="True" AllowSelectByRowClick="True" ConfirmDelete="True" />
                                            <SettingsPager Mode="ShowAllRecords" />
                                            <Settings ShowTitlePanel="true" VerticalScrollBarMode="Visible" />
                                            <Styles>
                                                <Row Font-Bold="False">
                                                </Row>
                                            </Styles>
                                        </dx:ASPxGridView>

                                    </div>
                                </ItemTemplate>
                            </asp:DataList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <div style="clear: left;"></div>
    </form>
</body>

</html>
