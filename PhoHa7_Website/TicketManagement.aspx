<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage2.master" AutoEventWireup="true" CodeFile="TicketManagement.aspx.cs" Inherits="TicketManagement" %>

<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxFormLayout" TagPrefix="dx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlace1" runat="Server">
    <link rel="stylesheet" href="css/softkeys-0.0.1.css">
    <dx:ASPxRoundPanel ID="ASPxRoundPanelUsersManager" Width="100%" Height="100%" runat="server" Theme="Metropolis" HeaderText="Tickets Management">
        <PanelCollection>
            <dx:PanelContent>
                <dx:ASPxFormLayout runat="server" ID="OptionsFormLayout" Theme="Metropolis">
                    <Items>
                        <dx:LayoutGroup ColCount="8" Caption="Date">
                            <Items>
                                <dx:LayoutItem ShowCaption="False">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxButton ID="btnPrev" runat="server" Text="Prev" Theme="Metropolis" OnClick="btnPrev_Click"></dx:ASPxButton>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem ShowCaption="False">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxDateEdit ID="dateTicket" EditFormatString="dd/MM/yyyy" runat="server" EditFormat="Custom" Date="<%#DateTime.Today %>" Width="190" Caption="ASPxDateEdit" Theme="Metropolis" OnDateChanged="dateTicket_DateChanged" AutoPostBack="True">
                                                <CalendarProperties>
                                                    <FastNavProperties Enabled="true" />
                                                </CalendarProperties>
                                            </dx:ASPxDateEdit>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem ShowCaption="False">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxButton ID="btnNext" runat="server" Text="Next" Theme="Metropolis" OnClick="btnNext_Click"></dx:ASPxButton>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem ShowCaption="False">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxButton ID="btnToday" runat="server" Text="Today" Theme="Metropolis" OnClick="btnToday_Click"></dx:ASPxButton>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Layout Item" ShowCaption="False">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server" SupportsDisabledAttribute="True"></dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <%--<dx:LayoutItem Caption="Layout Item" ShowCaption="False">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxButton runat="server" Text="Submit" ID="btnSubmit" Theme="Metropolis" OnClick="btnSubmit_Click"></dx:ASPxButton>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>--%>
                            </Items>
                        </dx:LayoutGroup>
                    </Items>
                </dx:ASPxFormLayout>
                <script>
                  
                    function detailsTicket(ticketID)
                    {
                        
                        pcOrderReview.Show();
                       
                        //alert("A"+ticketID);
                        //alert(request_data);
                        //pcDetails.Show();
                        hiddenTicketID.Set('ticketID', ticketID); 
                        gridReviewOrder.PerformCallback(ticketID);

                        lblSubTotal.SetText(gridReviewOrder.cplblSubTotal);
                    }


                    function HidePopOrderReview() {
                        pcOrderReview.Hide();
                    }

                    function deleteTicket()
                    {
                        if (confirm("Are you sure want to void this ticket?") == true) {

                            gridReviewOrder.PerformCallback("DeleteTicket");
                            pcOrderReview.Hide();
                        } else {
                            
                        }
                    }


                    function OnEndCallback(s, e) {
                        lblSubTotal.SetText(gridReviewOrder.cplblSubTotal);
                        lblTax.SetText(gridReviewOrder.cplblSaleTax);
                        lblTip.SetText(gridReviewOrder.cplblTip);
                        lblTotal.SetText(gridReviewOrder.cplblTotal);
                        lblCash.SetText(gridReviewOrder.cplblCash);
                        lblCredit.SetText(gridReviewOrder.cplblCredit);
                        pcOrderReview.SetHeaderText(gridReviewOrder.cpTableName);
                        if (gridReviewOrder.cplblCreditCode!="")
                        {
                            lblCreditCode.SetText("Card "+ gridReviewOrder.cplblCreditCode);
                        }
                    }

                    function printTicket(){
                        gridReviewOrder.PerformCallback("PrintBill");
                    }

                </script>
                <dx:ASPxGridView ID="gridTicket" ClientInstanceName="gridTicket" runat="server" Font-Size="12" SettingsBehavior-AllowSort="false"
                    KeyFieldName="TicketID" AutoGenerateColumns="False" Width="100%" Theme="Metropolis">
                    <Settings ShowFilterRow="true" />
                    <Columns>
                        <dx:GridViewDataColumn FieldName="TicketID" VisibleIndex="1" Visible="true">
                            <CellStyle Font-Bold="True">
                            </CellStyle>
                            <DataItemTemplate>
                                <%--<dx:ASPxButton ID="btnTicketID" runat="server" Text="ABC" OnClick="btnTicketID_Click" ClientSideEvents-Click="function(s, e) { detailsTicket(); }">

                                </dx:ASPxButton>--%>
                                <input type="button" value="Details" accesskey='' onclick='detailsTicket(<%# Eval("TicketID") %>)' />
                            </DataItemTemplate>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="TableName" VisibleIndex="2" Caption="Table Name">
                            <CellStyle Font-Bold="True" HorizontalAlign="Center"></CellStyle>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataTextColumn FieldName="DateTimeIssue" VisibleIndex="3" Visible="true" Caption="Time">
                            <CellStyle Font-Bold="True" HorizontalAlign="Center"></CellStyle>
                            <PropertiesTextEdit DisplayFormatString="hh:mm tt" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="3"
                            FieldName="MyTotal" Caption="Total"
                            UnboundType="Decimal"
                            UnboundExpression="PaidCash+PaidCredit+Tips">
                            <PropertiesTextEdit DisplayFormatString="c2" />
                            <CellStyle Font-Bold="True" HorizontalAlign="Center"></CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="PaidCash" VisibleIndex="4" Visible="false" Caption="Cash">
                            <CellStyle HorizontalAlign="Center"></CellStyle>
                            <PropertiesTextEdit DisplayFormatString="c2" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="PaidCredit" VisibleIndex="5" Visible="false" Caption="Credit">
                            <CellStyle HorizontalAlign="Center"></CellStyle>
                            <PropertiesTextEdit DisplayFormatString="c2" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataCheckColumn FieldName="Voided" VisibleIndex="6" Caption="Void">
                            <CellStyle Font-Bold="True" HorizontalAlign="Center"></CellStyle>
                        </dx:GridViewDataCheckColumn>
                        <dx:GridViewDataColumn FieldName="DTicketNum" VisibleIndex="7" Caption="Date Ticket Number">
                            <CellStyle Font-Bold="True" HorizontalAlign="Center"></CellStyle>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="TicketNum" VisibleIndex="8" Caption="Ticket Number">
                            <CellStyle Font-Bold="True" HorizontalAlign="Center"></CellStyle>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="Tips" VisibleIndex="9" Visible="false" Caption="Tips">
                            <CellStyle Font-Bold="True" HorizontalAlign="Center"></CellStyle>
                        </dx:GridViewDataColumn>
                        <%--<dx:GridViewCommandColumn ShowSelectCheckbox="false" ButtonType="Button">
                            <DeleteButton Visible="True"></DeleteButton>
                        </dx:GridViewCommandColumn>--%>
                    </Columns>
                    <SettingsBehavior AllowSelectSingleRowOnly="True" AllowSelectByRowClick="True" ConfirmDelete="True" />
                    <SettingsPager PageSize="25">
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

    <dx:ASPxHiddenField ID="ticketIDCompletedTicket" ClientInstanceName="ticketIDCompletedTicket" runat="server"></dx:ASPxHiddenField>
    <dx:ASPxPopupControl ID="pcOrderReview" runat="server" CloseAction="OuterMouseClick" Modal="True" ShowFooter="True" Theme="Metropolis"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="pcOrderReview" 
        HeaderText="Review" AllowDragging="True" PopupAnimationType="None" EnableViewState="False" Width="500px">

        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                <dx:ASPxPanel ID="ASPxPanel1" runat="server" DefaultButton="btOK">
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent2" runat="server">
                            <table>
                                <tbody>
                                    <tr>
                                        <td colspan="2">
                                            <dx:ASPxHiddenField ID="hiddenTicketID" ClientInstanceName="hiddenTicketID" runat="server"></dx:ASPxHiddenField>
                                            <dx:ASPxGridView ID="gridReviewOrder" ClientInstanceName="gridReviewOrder" runat="server"
                                                KeyFieldName="SaleItemID" AutoGenerateColumns="False" Width="100%" Theme="Metropolis" CssClass="girdItems"
                                                OnCustomCallback="gridReviewOrder_CustomCallback">
                                                <Settings VerticalScrollBarMode="Auto" VerticalScrollableHeight="260" />

                                                <ClientSideEvents EndCallback="function(s, e) { OnEndCallback(s,e); }" />
                                                <Columns>
                                                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" Width="40px"></dx:GridViewCommandColumn>
                                                    <dx:GridViewDataColumn FieldName="Qty" VisibleIndex="1" Caption="SL" Width="40px">
                                                        <CellStyle Font-Size="12px">
                                                        </CellStyle>
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="Description" VisibleIndex="2" Caption="Name">
                                                        <CellStyle Font-Bold="True" Font-Size="12px">
                                                        </CellStyle>
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataCheckColumn FieldName="TakeOut" VisibleIndex="3" Caption="ToGo" Width="40px">
                                                        <PropertiesCheckEdit ValueGrayed="False">
                                                        </PropertiesCheckEdit>
                                                    </dx:GridViewDataCheckColumn>
                                                    <dx:GridViewDataCheckColumn FieldName="SmallSize" VisibleIndex="4" Caption="Small" Width="40px" Visible="False">
                                                        <PropertiesCheckEdit ValueGrayed="False">
                                                        </PropertiesCheckEdit>
                                                    </dx:GridViewDataCheckColumn>
                                                    <dx:GridViewDataColumn FieldName="BarCode" VisibleIndex="5" Caption="Code" Width="60px" Visible="False">
                                                        <CellStyle Font-Bold="False" Font-Size="12px">
                                                        </CellStyle>
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="SaleItemID" VisibleIndex="8" Visible="False">
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="BarCode" VisibleIndex="10" Visible="False">
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="ProductID" VisibleIndex="14" Visible="False">
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="KitchenName" VisibleIndex="11" Visible="False">
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataTextColumn FieldName="TPrice" Caption="Price" Width="60px" VisibleIndex="13" Visible="True">
                                                        <PropertiesTextEdit DisplayFormatString="c2" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataColumn FieldName="SPrice" VisibleIndex="14" Visible="false">
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="Extra" VisibleIndex="7" Visible="False">
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="EmployeeName" VisibleIndex="9" Visible="False">
                                                    </dx:GridViewDataColumn>
                                                </Columns>
                                                <SettingsBehavior ProcessSelectionChangedOnServer="True" />
                                                <SettingsPager Mode="ShowAllRecords" />
                                                <Settings ShowTitlePanel="true" VerticalScrollBarMode="Visible" />
                                                <SettingsText Title="Order Review" />

                                                <Styles>
                                                    <Row Font-Bold="False">
                                                    </Row>
                                                    <TitlePanel Font-Bold="True" ForeColor="Black">
                                                    </TitlePanel>
                                                </Styles>
                                            </dx:ASPxGridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right; font-weight: bold;">Sub Total</td>
                                        <td style="text-align: center; font-weight: bold;">
                                            <dx:ASPxLabel ID="lblSubTotal" ClientInstanceName="lblSubTotal" runat="server" Text="ASPxLabel"></dx:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right; font-weight: bold;">Tax</td>
                                        <td style="text-align: center; font-weight: bold;">
                                            <dx:ASPxLabel ID="lblTax" ClientInstanceName="lblTax" runat="server" Text="ASPxLabel"></dx:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right; font-weight: bold;">Tip</td>
                                        <td style="text-align: center; font-weight: bold;">
                                            <dx:ASPxLabel ID="lblTip" ClientInstanceName="lblTip" runat="server" Text="Tip"></dx:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right; font-weight: bold;">Total</td>
                                        <td style="text-align: center; font-weight: bold;">
                                            <dx:ASPxLabel ID="lblTotal" Font-Size="Large" Font-Bold="true" ClientInstanceName="lblTotal" runat="server" Text="ASPxLabel"></dx:ASPxLabel>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style="text-align: right; font-weight: bold;">Cash</td>
                                        <td style="text-align: center; font-weight: bold;">
                                            <dx:ASPxLabel ID="lblCash" ClientInstanceName="lblCash" runat="server" Text="ASPxLabel"></dx:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right; font-weight: bold;">
                                            <dx:ASPxLabel ID="lblCreditCode" ClientInstanceName="lblCreditCode" runat="server" Text="Card" Font-Bold="true"></dx:ASPxLabel>
                                        </td>
                                        <td style="text-align: center; font-weight: bold;">
                                            <dx:ASPxLabel ID="lblCredit" ClientInstanceName="lblCredit" runat="server" Text="ASPxLabel"></dx:ASPxLabel>
                                        </td>
                                    </tr>

                                </tbody>
                            </table>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxPanel>

            </dx:PopupControlContentControl>
        </ContentCollection>
        <ContentStyle>
            <Paddings PaddingBottom="10px" />
        </ContentStyle>
        <FooterTemplate>
            <div style="margin: 3px; text-align: center;">
                <%--<dx:ASPxButton ID="btOK" runat="server" Text="OK" Width="80px" AutoPostBack="False" OnClick="btOK_Click"></dx:ASPxButton>--%>
                <dx:ASPxButton ID="btnPrint" runat="server" Text="Print Ticket" AutoPostBack="False"
                    ClientSideEvents-Click="function(s, e) { printTicket(); }" EnableTheming="False" Native="True" CssClass="softkeys__btnPayment" />
                <dx:ASPxButton ID="btCancelOrderReview" runat="server" Text="Cancel" AutoPostBack="False"
                    ClientSideEvents-Click="function(s, e) { HidePopOrderReview(); }" Native="True" EnableTheming="False" CssClass="softkeys__btnExtraCancel" />
                <% PhoMac.Model.Employee dtEmp = (PhoMac.Model.Employee)Session["LoginEmp"];
                   if (dtEmp.SecureLevel < 4)
                   {
                   
                %>
                <dx:ASPxButton ID="btnDelete" runat="server" Text="Void" AutoPostBack="False"
                    ClientSideEvents-Click="function(s, e) { deleteTicket(); }" Native="True" EnableTheming="False" CssClass="softkeys__btnExtraCancel" />
                <dx:ASPxButton ID="ASPxButton1" runat="server" Text="Refund" AutoPostBack="False"
                    ClientSideEvents-Click="function(s, e) { deleteTicket(); }" Native="True" EnableTheming="False" CssClass="softkeys__btnExtraCancel" />
                <% }%>
            </div>
        </FooterTemplate>

    </dx:ASPxPopupControl>

    <script type="text/javascript">
        function OKCash(cashMount) {
            var cash = Number(cashMount.replace(/[^0-9\.]+/g, ""))
            callBackCash.PerformCallback(cash);
        }
        function callBackCashPerformCallBackEnd(s, e) {
            if (callBackCash.cpOK === "OK") {
                console.log(callBackCash.cpOK);
                pcCash.Hide();
                pcCashChange.Show();
                lblCashChange.SetText(callBackCash.cpCashChange);
                ticketIDCompletedTicket.Set('ticketID', callBackCash.cpCashChangeTicketID);
            } else if (callBackCash.cpOK === "SendKitchen") {
                alert("Please send order to kitchen before Check Out.")
            }
            else {
                btnCheckOutCashLabel.SetText(callBackCash.cpbtnCheckOutCashLabel);
                btnExpressCash1.SetText(callBackCash.cpbtnExpressCash1);
                btnExpressCash2.SetText(callBackCash.cpbtnExpressCash2);
                btnExpressCash3.SetText(callBackCash.cpbtnExpressCash3);
                btnExpressCash4.SetText(callBackCash.cpbtnExpressCash4);
                btnExpressCash5.SetText(callBackCash.cpbtnExpressCash5);
                pcCash.Show();
            }

        }

        function btnExpressCash1OnClick() {
            var text = txtCashCheckOut.GetText();
            var amount = parseInt(Number(text.replace(/[^0-9\.]+/g, ""))* 100).toString();
            amount = parseInt(amount + "" + 1) / 100;
            txtCashCheckOut.SetText(amount);
        }
        function btnExpressCash2OnClick() {
            var text = txtCashCheckOut.GetText();
            var amount = parseInt(Number(text.replace(/[^0-9\.]+/g, "") * 100)).toString();
            amount = parseInt(amount + "" + 2) / 100;
            txtCashCheckOut.SetText(amount);
        }
        function btnExpressCash3OnClick() {
            var text = txtCashCheckOut.GetText();
            var amount = parseInt(Number(text.replace(/[^0-9\.]+/g, "") * 100)).toString();
            amount = parseInt(amount + "" + 3) / 100;
            txtCashCheckOut.SetText(amount);
        }
        function btnExpressCash4OnClick() {
            var text = txtCashCheckOut.GetText();
            var amount = parseInt(Number(text.replace(/[^0-9\.]+/g, "") * 100)).toString();
            amount = parseInt(amount + "" + 4) / 100;
            txtCashCheckOut.SetText(amount);
        }
        function btnExpressCash5OnClick() {
            var text = txtCashCheckOut.GetText();
            var amount = parseInt(Number(text.replace(/[^0-9\.]+/g, "") * 100)).toString();
            amount = parseInt(amount + "" + 5) / 100;
            txtCashCheckOut.SetText(amount);
        }
        function btnExpressCash6OnClick() {
            var text = txtCashCheckOut.GetText();
            var amount = parseInt(Number(text.replace(/[^0-9\.]+/g, "") * 100)).toString();
            amount = parseInt(amount + "" + 6) / 100;
            txtCashCheckOut.SetText(amount);
        }
        function btnExpressCash7OnClick() {
            var text = txtCashCheckOut.GetText();
            console.log(text);
            var amount = parseInt(Number(text.replace(/[^0-9\.]+/g, "") * 100)).toString();
            console.log(amount);
            amount = parseInt(amount + "" + 7) / 100;
            console.log(amount);
            txtCashCheckOut.SetText(amount);
        }
        function btnExpressCash8OnClick() {
            var text = txtCashCheckOut.GetText();
            var amount = parseInt(Number(text.replace(/[^0-9\.]+/g, "") * 100)).toString();
            amount = parseInt(amount + "" + 8) / 100;
            txtCashCheckOut.SetText(amount);
        }
        function btnExpressCash9OnClick() {
            var text = txtCashCheckOut.GetText();
            var amount = parseInt(Number(text.replace(/[^0-9\.]+/g, "") * 100)).toString();
            amount = parseInt(amount + "" + 9) / 100;
            txtCashCheckOut.SetText(amount);
        }
        function btnExpressCash0OnClick() {
            var text = txtCashCheckOut.GetText();
            var amount = parseInt(Number(text.replace(/[^0-9\.]+/g, "") * 100)).toString();
            amount = parseInt(amount + "" + 0) / 100;
            txtCashCheckOut.SetText(amount);
        }
        function delCashInput() {
            var text = txtCashCheckOut.GetText();
            var amount = parseInt(Number(text.replace(/[^0-9\.]+/g, "") * 100)).toString();
            text = amount + "";
            text = text.substring(0, text.length - 1);
            if (text === "") {
                text = "0";
            }
            //amount = parseFloat(text) / 100;
            //alert(parseFloat(text) / 100);
            //amount = parseInt(amount + "" + 0) / 100;
            txtCashCheckOut.SetText(parseInt(text) / 100);
        }
        function ClearCashInput() {
            txtCashCheckOut.SetText("0.00");
        }


        function callBackCashPerformCallBackEnd(s, e) {
            if (callBackCash.cpOK === "OK") {
                console.log(callBackCash.cpOK);
                pcCash.Hide();
                pcCashChange.Show();
                lblCashChange.SetText(callBackCash.cpCashChange);
                ticketIDCompletedTicket.Set('ticketID', callBackCash.cpCashChangeTicketID);
            } else if (callBackCash.cpOK === "SendKitchen") {
                alert("Please send order to kitchen before Check Out.")
            }
            else {
                btnCheckOutCashLabel.SetText(callBackCash.cpbtnCheckOutCashLabel);
                btnExpressCash1.SetText(callBackCash.cpbtnExpressCash1);
                btnExpressCash2.SetText(callBackCash.cpbtnExpressCash2);
                btnExpressCash3.SetText(callBackCash.cpbtnExpressCash3);
                btnExpressCash4.SetText(callBackCash.cpbtnExpressCash4);
                btnExpressCash5.SetText(callBackCash.cpbtnExpressCash5);
                pcCash.Show();
            }

        }
    </script>
     <dx:ASPxCallback ID="callBackPayButton" ClientInstanceName="callBackPayButton" runat="server" OnCallback="callBackCash_Callback">
        <ClientSideEvents EndCallback="function(s, e) { callBackPayButtonComplete(s,e); }" />
    </dx:ASPxCallback>
    <dx:ASPxCallback ID="callBackCash" ClientInstanceName="callBackCash" runat="server" >
        <ClientSideEvents EndCallback="function(s, e) { callBackCashPerformCallBackEnd(s,e); }" />
    </dx:ASPxCallback>
    <dx:ASPxPopupControl ID="pcCash" runat="server" CloseAction="OuterMouseClick" Modal="True" ShowFooter="True" Theme="Metropolis"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="pcCash"
        HeaderText="Cash Input" AllowDragging="True" PopupAnimationType="None" EnableViewState="False" Width="400px">
        
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl5" runat="server">
                <dx:ASPxPanel ID="ASPxPanel4" runat="server" DefaultButton="btOK">
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent5" runat="server">
                            <table>
                                <tr>
                                    <th colspan="5">
                                        <div style="border: solid 1px black; text-align: center;">
                                            <dx:ASPxLabel ID="btnCheckOutCashLabel" ClientInstanceName="btnCheckOutCashLabel" Font-Bold="true" Font-Size="18" runat="server" Text="ASPxLabel" Theme="Metropolis"></dx:ASPxLabel>
                                        </div>
                                    </th>
                                </tr>
                                <tr>
                                    <td>
                                        <dx:ASPxButton CssClass="btnCashButton1" ID="btnExpressCash1" ClientInstanceName="btnExpressCash1" Font-Size="15" runat="server" Text="$1" AutoPostBack="False" Theme="Metropolis">
                                            <ClientSideEvents Click="function(s, e) { OKCash(btnExpressCash1.GetText()); }" />
                                            <FocusRectPaddings Padding="0px" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td>
                                        <dx:ASPxButton CssClass="btnCashButton1" ID="btnExpressCash2" ClientInstanceName="btnExpressCash2" Font-Size="15" runat="server" Text="$2" AutoPostBack="False" Theme="Metropolis">
                                            <ClientSideEvents Click="function(s, e) { OKCash(btnExpressCash2.GetText()); }" />
                                            <FocusRectPaddings Padding="0px" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td>
                                        <dx:ASPxButton CssClass="btnCashButton1" ID="btnExpressCash3" ClientInstanceName="btnExpressCash3" Font-Size="15" runat="server" Text="$3" AutoPostBack="False" Theme="Metropolis">
                                            <ClientSideEvents Click="function(s, e) { OKCash(btnExpressCash3.GetText()); }" />
                                            <FocusRectPaddings Padding="0px" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td>
                                        <dx:ASPxButton CssClass="btnCashButton1" ID="btnExpressCash4" ClientInstanceName="btnExpressCash4" Font-Size="15" runat="server" Text="$4" AutoPostBack="False" Theme="Metropolis">
                                            <ClientSideEvents Click="function(s, e) { OKCash(btnExpressCash4.GetText()); }" />
                                            <FocusRectPaddings Padding="0px" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td>
                                        <dx:ASPxButton CssClass="btnCashButton1" ID="btnExpressCash5" ClientInstanceName="btnExpressCash5" Font-Size="15" runat="server" Text="$5" AutoPostBack="False" Theme="Metropolis">
                                            <ClientSideEvents Click="function(s, e) { OKCash(btnExpressCash5.GetText()); }" />
                                            <FocusRectPaddings Padding="0px" />
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <th colspan="3">
                                        <dx:ASPxTextBox ID="txtCashCheckOut" ClientInstanceName="txtCashCheckOut" runat="server" Height="100%" Width="100%" Text="0" Font-Size="15" DisplayFormatString="c2" HorizontalAlign="Center" Theme="Metropolis"></dx:ASPxTextBox>
                                    </th>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <dx:ASPxButton CssClass="btnCashButton" ID="ASPxButton10" runat="server" Text="1" Font-Bold="true" Font-Size="19" AutoPostBack="False" Theme="Metropolis">
                                            <ClientSideEvents Click="function(s, e) { btnExpressCash1OnClick(); }" />
                                            <FocusRectPaddings Padding="0px" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td>
                                        <dx:ASPxButton CssClass="btnCashButton" ID="ASPxButton11" runat="server" Text="2" Font-Bold="true" Font-Size="19" AutoPostBack="False" Theme="Metropolis">
                                            <ClientSideEvents Click="function(s, e) { btnExpressCash2OnClick(); }" />
                                            <FocusRectPaddings Padding="0px" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td>
                                        <dx:ASPxButton CssClass="btnCashButton" ID="ASPxButton12" runat="server" Text="3" Font-Bold="true" Font-Size="19" AutoPostBack="False" Theme="Metropolis">
                                            <ClientSideEvents Click="function(s, e) { btnExpressCash3OnClick(); }" />
                                            <FocusRectPaddings Padding="0px" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <dx:ASPxButton CssClass="btnCashButton" ID="ASPxButton4" runat="server" Text="4" Font-Bold="true" Font-Size="19" AutoPostBack="False" Theme="Metropolis">
                                            <ClientSideEvents Click="function(s, e) { btnExpressCash4OnClick(); }" />
                                            <FocusRectPaddings Padding="0px" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td>
                                        <dx:ASPxButton CssClass="btnCashButton" ID="ASPxButton13" runat="server" Text="5" Font-Bold="true" Font-Size="19" AutoPostBack="False" Theme="Metropolis">
                                            <ClientSideEvents Click="function(s, e) { btnExpressCash5OnClick(); }" />
                                            <FocusRectPaddings Padding="0px" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td>
                                        <dx:ASPxButton CssClass="btnCashButton" ID="ASPxButton14" runat="server" Text="6" Font-Bold="true" Font-Size="19" AutoPostBack="False" Theme="Metropolis">
                                            <ClientSideEvents Click="function(s, e) { btnExpressCash6OnClick(); }" />
                                            <FocusRectPaddings Padding="0px" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <dx:ASPxButton CssClass="btnCashButton" ID="ASPxButton15" runat="server" Text="7" Font-Bold="true" Font-Size="19" AutoPostBack="False" Theme="Metropolis">
                                            <ClientSideEvents Click="function(s, e) { btnExpressCash7OnClick(); }" />
                                            <FocusRectPaddings Padding="0px" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td>
                                        <dx:ASPxButton CssClass="btnCashButton" ID="ASPxButton16" runat="server" Text="8" Font-Bold="true" Font-Size="19" AutoPostBack="False" Theme="Metropolis">
                                            <ClientSideEvents Click="function(s, e) { btnExpressCash8OnClick(); }" />
                                            <FocusRectPaddings Padding="0px" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td>
                                        <dx:ASPxButton CssClass="btnCashButton" ID="ASPxButton17" runat="server" Text="9" Font-Bold="true" Font-Size="19" AutoPostBack="False" Theme="Metropolis">
                                            <ClientSideEvents Click="function(s, e) { btnExpressCash9OnClick(); }" />
                                            <FocusRectPaddings Padding="0px" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <dx:ASPxButton CssClass="btnCashButton" ID="ASPxButton3" runat="server" Text="Del" Font-Size="19" AutoPostBack="False" Theme="Metropolis">
                                            <ClientSideEvents Click="function(s, e) { delCashInput(); }" />
                                            <FocusRectPaddings Padding="0px" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td>
                                        <dx:ASPxButton CssClass="btnCashButton" ID="ASPxButton18" runat="server" Text="0" Font-Bold="true" Font-Size="19" AutoPostBack="False" Theme="Metropolis">
                                            <ClientSideEvents Click="function(s, e) { btnExpressCash0OnClick(); }" />
                                            <FocusRectPaddings Padding="0px" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td>
                                        <dx:ASPxButton CssClass="btnCashButton" ID="ASPxButton2" runat="server" Text="Clear" Font-Size="19" AutoPostBack="False" Theme="Metropolis">
                                            <ClientSideEvents Click="function(s, e) { ClearCashInput(); }" />
                                            <FocusRectPaddings Padding="0px" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td></td>
                                </tr>
                            </table>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxPanel>

            </dx:PopupControlContentControl>
        </ContentCollection>
        <ContentStyle>
            <Paddings PaddingBottom="10px" />
        </ContentStyle>
        <FooterTemplate>
            <div style="margin: 3px;">
                <%--<dx:ASPxButton ID="btOK" runat="server" Text="OK" Width="80px" AutoPostBack="False" OnClick="btOK_Click"></dx:ASPxButton>--%>
                <dx:ASPxButton ID="btnCashOK" runat="server" Text="Refund" AutoPostBack="False"
                    ClientSideEvents-Click="function(s, e) { OKCash(txtCashCheckOut.GetText()); }" EnableTheming="False" Native="True" CssClass="softkeys__btnPayment" />
                <dx:ASPxButton ID="btnCancelCash" runat="server" Text="Cancel" AutoPostBack="False"
                    ClientSideEvents-Click="function(s, e) { HidePcCash(); }" Native="True" EnableTheming="False" CssClass="softkeys__btnExtraCancel" />
            </div>
        </FooterTemplate>
    </dx:ASPxPopupControl>











</asp:Content>



