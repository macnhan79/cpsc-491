<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Orders" EnableEventValidation="true" %>

<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridLookup" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxFormLayout" TagPrefix="dx" %>

<%@ Register Src="~/UserControl/ItemList.ascx" TagPrefix="dx" TagName="ItemList" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="css/softkeys-0.0.1.css">
    <script src="js/talking_keyboard.js"></script>
    <style type="text/css">
        .auto-style1 {
            height: 19px;
        }

        .btnMoreButton {
            width: 100px;
            height: 100px;
            margin: 20px;
        }

        .btnCashButton {
            width: 70px;
            height: 70px;
            margin: 5px;
        }

        .btnCashButton1 {
            width: 70px;
            height: 40px;
            margin: 5px;
        }

        .dxtcActiveTab_Metropolis {
            padding: 4px 10px 4px !important;
        }

        .dxtcTab_Metropolis {
            padding: 4px 10px !important;
        }

        .dxgvCommandColumnItem_Metropolis {
            background-color: white;
            border: 1px solid #c0c0c0;
        }
    </style>
    <script type="text/javascript" src="translate/languages.js"></script>
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceMenu" runat="Server">
    <%--    <dx:ASPxCallback ID="callBackDictionary" ClientInstanceName="callBackDictionary" runat="server" OnCallback="callBackDictionary_Callback" OnInit="callBackDictionary_Init" ClientSideEvents-EndCallback="OnCallBackDictionaryComplete">
        <ClientSideEvents EndCallback="function(s, e) { OnCallBackDictionaryComplete(s,e); }" />
    </dx:ASPxCallback>--%>

    <script type="text/javascript">
        // <![CDATA[

        function printBill() {
            grid.PerformCallback("PrintBill");
        }

        function separateItem() {
            grid.PerformCallback("SeparateItem");
        }

        function openDrawer() {
            grid.PerformCallback("OpenDrawer");
        }

        function combineAllTable()
        {

        }

        function customAmountOK() {
            grid.PerformCallback("CustomAmount");
            if (grid.cpCustomAmountOK == "OK") {

            }
            pcCustomAmount.Hide();
            pcMore.Hide();
        }

        function discount10PercentOK() {
            grid.PerformCallback("discount10PercentOK");
            if (grid.cpDiscount10PercentOK == "OK") {

            }
            pcMore.Hide();
        }

        function callBackCustomAmountComplete(s, e) {
            if (callBackCustomAmount.cpOK == "OK") {

            }

        }

        function ShowPayWindow() {
            LoadingPanel.Show();
            ASPxCallbackPayment.PerformCallback("");
            gridReviewOrder.PerformCallback();
            pcOrderReview.Show();
            LoadingPanel.Hide();
        }

        function showPCMore() {
            pcMore.Show();
        }

        function ShowCreateAccountWindow() {
            pcCreateAccount.Show();
            tbUsername.Focus();
        }
        function ASPxCallbackPaymentComplete(s, e) {
            if (ASPxCallbackPayment.cpTakeOutAlert === "Yes") {
                pcTakeOutAlert.Show();
            }
            //LoadingPanel.Hide();
            lblDiscount.SetText(ASPxCallbackPayment.cplblDiscount);
            lblSubTotal.SetText(ASPxCallbackPayment.cplblSubTotal);
            lblTax.SetText(ASPxCallbackPayment.cplblTax);
            lblTotal.SetText(ASPxCallbackPayment.cplblTotal);
        }

        function payfunction() {
            callBackPayButton.PerformCallback();
            console.log("payfunction");
        }

        function callBackPayButtonComplete(s, e) {
            //LoadingPanel.Hide();
            if (callBackPayButton.cpOK == "OK") {
                var amount = callBackPayButton.cplblSubTotal;
                console.log(amount);
                //var amount = lblSubTotal.GetText();
                var number = parseInt((Number(amount.replace(/[^0-9\.]+/g, "")) * 100).toString());
                //alert(number);
                var dataParameter = {
                    amount_money: {
                        amount: number,
                        currency_code: "USD"
                    },

                    // Replace this value with your application's callback URL
                    callback_url: "https://phominh/SquarePaymentCallBack.aspx",

                    // Replace this value with your application's ID
                    client_id: "sq0idp-98Z69SwPeJygZ-iVuhCTkQ",

                    version: "1.3",
                    notes: callBackPayButton.cpTicketID.toString(),
                    options: {
                        supported_tender_types: ["CREDIT_CARD", "CASH"]
                        //supported_tender_types: ["CREDIT_CARD", "CASH", "OTHER", "SQUARE_GIFT_CARD", "CARD_ON_FILE"]
                    }
                };
                console.log(dataParameter);
                window.location =
                  "square-commerce-v1://payment/create?data=" +
                  encodeURIComponent(JSON.stringify(dataParameter));
            } else {
                alert("Please send order to kitchen before Check Out.")
            }
        }


        function OKCash(cashMount) {
            var cash = Number(cashMount.replace(/[^0-9\.]+/g, ""))
            callBackCash.PerformCallback(cash);
        }

        function HidePcCash() {
            pcCash.Hide();
        }
        function ShowPcCash() {
            callBackCash.PerformCallback("init");

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
            var amount = parseInt(Number(text.replace(/[^0-9]+/g, ""))).toString();
            amount = parseInt(amount + "" + 1) / 100;
            txtCashCheckOut.SetText(amount);
        }
        function btnExpressCash2OnClick() {
            var text = txtCashCheckOut.GetText();
            var amount = parseInt(Number(text.replace(/[^0-9]+/g, ""))).toString();
            amount = parseInt(amount + "" + 2) / 100;
            txtCashCheckOut.SetText(amount);
        }
        function btnExpressCash3OnClick() {
            var text = txtCashCheckOut.GetText();
            var amount = parseInt(Number(text.replace(/[^0-9]+/g, ""))).toString();
            amount = parseInt(amount + "" + 3) / 100;
            txtCashCheckOut.SetText(amount);
        }
        function btnExpressCash4OnClick() {
            var text = txtCashCheckOut.GetText();
            var amount = parseInt(Number(text.replace(/[^0-9]+/g, ""))).toString();
            amount = parseInt(amount + "" + 4) / 100;
            txtCashCheckOut.SetText(amount);
        }
        function btnExpressCash5OnClick() {
            var text = txtCashCheckOut.GetText();
            var amount = parseInt(Number(text.replace(/[^0-9]+/g, ""))).toString();
            amount = parseInt(amount + "" + 5) / 100;
            txtCashCheckOut.SetText(amount);
        }
        function btnExpressCash6OnClick() {
            var text = txtCashCheckOut.GetText();
            var amount = parseInt(Number(text.replace(/[^0-9]+/g, ""))).toString();
            amount = parseInt(amount + "" + 6) / 100;
            txtCashCheckOut.SetText(amount);
        }
        function btnExpressCash7OnClick() {
            var text = txtCashCheckOut.GetText();
            var amount = parseInt(Number(text.replace(/[^0-9]+/g, ""))).toString();
            amount = parseInt(amount + "" + 7) / 100;
            txtCashCheckOut.SetText(amount);
        }
        function btnExpressCash8OnClick() {
            var text = txtCashCheckOut.GetText();
            var amount = parseInt(Number(text.replace(/[^0-9]+/g, ""))).toString();
            amount = parseInt(amount + "" + 8) / 100;
            txtCashCheckOut.SetText(amount);
        }
        function btnExpressCash9OnClick() {
            var text = txtCashCheckOut.GetText();
            var amount = parseInt(Number(text.replace(/[^0-9]+/g, ""))).toString();
            amount = parseInt(amount + "" + 9) / 100;
            txtCashCheckOut.SetText(amount);
        }
        function btnExpressCash0OnClick() {
            var text = txtCashCheckOut.GetText();
            console.log(text);
            text = text.replace(".", "") + "0";
            console.log(text);
            var amount = text.substring(0, text.length - 2) + "." + text.substring(text.length - 2);
            console.log(amount);

            //amount = parseInt(amount + "" + 0) / 100;
            txtCashCheckOut.SetText(amount);
        }
        function delCashInput() {
            var text = txtCashCheckOut.GetText();
            var amount = parseInt(Number(text.replace(/[^0-9]+/g, ""))).toString();
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
        // ]]>
    </script>
    <dx:ASPxPopupControl ID="pcOrderReview" runat="server" CloseAction="CloseButton" Modal="True" ShowFooter="True" Theme="Metropolis"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="pcOrderReview"
        HeaderText="Review" AllowDragging="True" PopupAnimationType="None" EnableViewState="False" Width="500px">
        <%--<ClientSideEvents PopUp="function(s, e) { ASPxClientEdit.ClearGroup('entryGroup'); tbLogin.Focus(); }" />--%>
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                <dx:ASPxPanel ID="ASPxPanel1" runat="server" DefaultButton="btOK">
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent2" runat="server">
                            <table>
                                <tbody>
                                    <tr>
                                        <td colspan="2">
                                            <dx:ASPxGridView ID="gridReviewOrder" ClientInstanceName="gridReviewOrder" runat="server"
                                                KeyFieldName="SaleItemID" AutoGenerateColumns="False" Width="100%" Theme="Metropolis" CssClass="girdItems" OnInit="gridReviewOrder_Init"
                                                OnCustomCallback="gridReviewOrder_CustomCallback">
                                                <%-- OnPreRender="grid_PreRender"--%>
                                                <Settings VerticalScrollBarMode="Auto" VerticalScrollableHeight="260" />
                                                <%--RowClick="function(s,e){grid.PerformCallback('SelectionChanged');}" --%>
                                                <ClientSideEvents EndCallback="function(s, e) { OnEndCallback(s,e); }" />
                                                <Columns>
                                                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" Width="40px"></dx:GridViewCommandColumn>
                                                    <dx:GridViewDataColumn FieldName="Qty" VisibleIndex="1" Caption="SL" Width="40px">
                                                        <CellStyle Font-Size="16px">
                                                        </CellStyle>
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="CustomKitchenName" VisibleIndex="2" Caption="Name">
                                                        <CellStyle Font-Bold="True" Font-Size="16px">
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
                                                        <CellStyle Font-Bold="False" Font-Size="16px">
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
                                                    <dx:GridViewDataColumn FieldName="CategoryID" VisibleIndex="12" Visible="False">
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataTextColumn FieldName="TotalPrice" Caption="Price" Width="60px" VisibleIndex="13" Visible="True">
                                                        <PropertiesTextEdit DisplayFormatString="c2" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataColumn FieldName="LPrice" Caption="Price" Width="60px" VisibleIndex="13" Visible="false">
                                                    </dx:GridViewDataColumn>
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
                                        <td>
                                            <dx:ASPxLabel ID="lblSubTotal" ClientInstanceName="lblSubTotal" runat="server" Text="ASPxLabel"></dx:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right; font-weight: bold;">Tax</td>
                                        <td>
                                            <dx:ASPxLabel ID="lblTax" ClientInstanceName="lblTax" runat="server" Text="ASPxLabel"></dx:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right; font-weight: bold;">Discount</td>
                                        <td>
                                            <dx:ASPxLabel ID="lblDiscount" ClientInstanceName="lblDiscount" runat="server" Text="0"></dx:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right; font-weight: bold;">Total</td>
                                        <td>
                                            <dx:ASPxLabel ID="lblTotal" Font-Size="Large" Font-Bold="true" ClientInstanceName="lblTotal" runat="server" Text="ASPxLabel"></dx:ASPxLabel>
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
            <div style="margin: 3px;">
                <%--<dx:ASPxButton ID="btOK" runat="server" Text="OK" Width="80px" AutoPostBack="False" OnClick="btOK_Click"></dx:ASPxButton>--%>
                <dx:ASPxButton ID="btPayment" runat="server" Text="Pay Now" AutoPostBack="False"
                    ClientSideEvents-Click="function(s, e) { payfunction(); }" EnableTheming="False" Native="True" CssClass="softkeys__btnPayment" />
                <dx:ASPxButton ID="btCancelOrderReview" runat="server" Text="Cancel" AutoPostBack="False"
                    ClientSideEvents-Click="function(s, e) { HidePopOrderReview(); }" Native="True" EnableTheming="False" CssClass="softkeys__btnExtraCancel" />
                <dx:ASPxButton ID="btnPayCash" runat="server" Text="Pay Cash" AutoPostBack="False"
                    ClientSideEvents-Click="function(s, e) { ShowPcCash(); }" Native="True" EnableTheming="False" CssClass="softkeys__btnPayment" />
            </div>
        </FooterTemplate>
    </dx:ASPxPopupControl>

    <dx:ASPxPopupControl ID="pcCash" runat="server" CloseAction="OuterMouseClick" Modal="True" ShowFooter="True" Theme="Metropolis"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="pcCash"
        HeaderText="Cash Input" AllowDragging="True" PopupAnimationType="None" EnableViewState="False" Width="400px">
        <%--<ClientSideEvents PopUp="function(s, e) { ASPxClientEdit.ClearGroup('entryGroup'); tbLogin.Focus(); }" />--%>
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
                <dx:ASPxButton ID="btnCashOK" runat="server" Text="OK" AutoPostBack="False"
                    ClientSideEvents-Click="function(s, e) { OKCash(txtCashCheckOut.GetText()); }" EnableTheming="False" Native="True" CssClass="softkeys__btnPayment" />
                <dx:ASPxButton ID="btnCancelCash" runat="server" Text="Cancel" AutoPostBack="False"
                    ClientSideEvents-Click="function(s, e) { HidePcCash(); }" Native="True" EnableTheming="False" CssClass="softkeys__btnExtraCancel" />
            </div>
        </FooterTemplate>
    </dx:ASPxPopupControl>
    <dx:ASPxCallback ID="callBackCash" ClientInstanceName="callBackCash" runat="server" OnInit="callBackCash_Init" OnCallback="callBackCash_Callback">
        <ClientSideEvents EndCallback="function(s, e) { callBackCashPerformCallBackEnd(s,e); }" />
    </dx:ASPxCallback>

    <dx:ASPxPopupControl ID="pcCashChange" runat="server" CloseAction="CloseButton" Modal="True" ShowFooter="True" Theme="Metropolis" ShowCloseButton="false"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="pcCashChange"
        HeaderText="Cash Change" AllowDragging="True" PopupAnimationType="None" EnableViewState="False" Width="500px">
        <%--<ClientSideEvents PopUp="function(s, e) { ASPxClientEdit.ClearGroup('entryGroup'); tbLogin.Focus(); }" />--%>
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl6" runat="server">
                <dx:ASPxPanel ID="ASPxPanel5" runat="server" DefaultButton="btOK">
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent6" runat="server">
                            <dx:ASPxLabel ID="lblCashChange" ClientInstanceName="lblCashChange" Font-Bold="true" Font-Size="48" runat="server" Text="ASPxLabel" Theme="Metropolis"></dx:ASPxLabel>
                            <dx:ASPxHiddenField ID="ticketIDCompletedTicket" ClientInstanceName="ticketIDCompletedTicket"  runat="server"></dx:ASPxHiddenField>
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
                <dx:ASPxButton ID="btnCancalChange" runat="server" Text="OK" AutoPostBack="False"
                    ClientSideEvents-Click="function(s, e) { location.reload(true); }" Native="True" EnableTheming="False" CssClass="softkeys__btnPayment" />
                <dx:ASPxButton ID="btnPrintBill" runat="server" Text="Print" AutoPostBack="False"
                    ClientSideEvents-Click="function(s, e) { printCompletedTicket();location.reload(true); }" Native="True" EnableTheming="False" CssClass="softkeys__btnPayment" />
            </div>

        </FooterTemplate>
    </dx:ASPxPopupControl>
    <script type="text/javascript">
        function printCompletedTicket() {
            callBackCash.PerformCallback("PrintCompletedTicket");
        }
    </script>

    <dx:ASPxPopupControl ID="pcMore" runat="server" CloseAction="CloseButton" Modal="True" ShowFooter="True" Theme="Metropolis"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="pcMore"
        HeaderText="More" AllowDragging="True" PopupAnimationType="None" EnableViewState="False" Width="400px">
        <%--<ClientSideEvents PopUp="function(s, e) { ASPxClientEdit.ClearGroup('entryGroup'); tbLogin.Focus(); }" />--%>
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl3" runat="server">
                <dx:ASPxPanel ID="ASPxPanel2" runat="server" DefaultButton="btOK">
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent3" runat="server">
                            <table>
                                <tr>
                                    <td>
                                        <dx:ASPxButton CssClass="btnMoreButton" ID="btnOpenDrawer" Font-Bold="true" Font-Size="10" runat="server" Text="OPEN DRAWER" AutoPostBack="False" Theme="Metropolis">
                                            <ClientSideEvents Click="function(s, e) { openDrawer(); }" />
                                            <FocusRectPaddings Padding="0px" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td>
                                        <dx:ASPxButton CssClass="btnMoreButton" ID="btnCustomAmount" Font-Bold="true" Font-Size="10" Text="Custom Amount" runat="server" AutoPostBack="False" Theme="Metropolis">
                                            <ClientSideEvents Click="function(s, e) { pcCustomAmount.Show(); }" />
                                            <FocusRectPaddings Padding="0px" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td>
                                        <dx:ASPxButton CssClass="btnMoreButton" ID="btnPrintReceipt" Font-Bold="true" Font-Size="10" runat="server" Text="Change Size" AutoPostBack="False" Theme="Metropolis">
                                            <ClientSideEvents Click="function(s, e) { ChangeSizeGrid(); }" />
                                            <FocusRectPaddings Padding="0px" />
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dx:ASPxButton CssClass="btnMoreButton" ID="btnVoidTicket" Font-Bold="true" Font-Size="10" runat="server" Text="Void Ticket" AutoPostBack="False" Theme="Metropolis">
                                            <ClientSideEvents Click="function(s, e) { voidTicket(); }" />
                                            <FocusRectPaddings Padding="0px" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td>
                                        <dx:ASPxButton CssClass="btnMoreButton" ID="btnUndo" runat="server" Font-Bold="true" Font-Size="10" Text="Undo" AutoPostBack="False" Theme="Metropolis">
                                            <ClientSideEvents Click="function(s, e) { UndoGrid(); }" />
                                            <FocusRectPaddings Padding="0px" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td>
                                        <dx:ASPxButton CssClass="btnMoreButton" ID="btnDiscount10Percent" runat="server" Font-Bold="true" Font-Size="10" Text="Discount 10%" AutoPostBack="False" Theme="Metropolis">
                                            <ClientSideEvents Click="function(s, e) { discount10PercentOK(); }" />
                                            <FocusRectPaddings Padding="0px" />
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dx:ASPxButton CssClass="btnMoreButton" ID="btnForceComplete" Font-Bold="true" Font-Size="10" runat="server" Text="Force Complete" AutoPostBack="False" Theme="Metropolis">
                                            <ClientSideEvents Click="function(s, e) { <%--payFunction();--%> }" />
                                            <FocusRectPaddings Padding="0px" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td>
                                        <dx:ASPxButton CssClass="btnMoreButton" ID="ASPxButton8" Font-Bold="true" Font-Size="10" runat="server" Text="Separate All Item" AutoPostBack="False" Theme="Metropolis">
                                            <ClientSideEvents Click="function(s, e) { <%--separateItem();--%> }" />
                                            <FocusRectPaddings Padding="0px" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td>
                                        <dx:ASPxButton CssClass="btnMoreButton" ID="ASPxButton9" runat="server" Font-Bold="true" Font-Size="10" Text="Combine All Table" AutoPostBack="False" Theme="Metropolis">
                                            <ClientSideEvents Click="function(s, e) { combineAllTable(); }" />
                                            <FocusRectPaddings Padding="0px" />
                                        </dx:ASPxButton>
                                    </td>
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
    </dx:ASPxPopupControl>

    <dx:ASPxCallback ID="ASPxCallbackPayment" ClientInstanceName="ASPxCallbackPayment" runat="server" OnCallback="ASPxCallbackPayment_Callback" OnInit="ASPxCallbackPayment_Init" ClientSideEvents-EndCallback="ASPxCallbackPaymentComplete">
        <ClientSideEvents EndCallback="function(s, e) { ASPxCallbackPaymentComplete(s,e); }" />
    </dx:ASPxCallback>

    <dx:ASPxCallback ID="callBackPayButton" ClientInstanceName="callBackPayButton" runat="server" OnCallback="callBackPayButton_Callback" OnInit="callBackPayButton_Init" ClientSideEvents-EndCallback="callBackPayButtonComplete">
        <ClientSideEvents EndCallback="function(s, e) { callBackPayButtonComplete(s,e); }" />
    </dx:ASPxCallback>

    <dx:ASPxPopupControl ID="pcTakeOutAlert" runat="server" CloseAction="CloseButton" Modal="True" ShowFooter="True" Theme="Metropolis" ShowCloseButton="false"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="pcTakeOutAlert"
        HeaderText="Warning" AllowDragging="false" PopupAnimationType="Fade" EnableViewState="False" Width="100%">
        <%--<ClientSideEvents PopUp="function(s, e) { ASPxClientEdit.ClearGroup('entryGroup'); tbLogin.Focus(); }" />--%>
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl7" runat="server">
                <dx:ASPxPanel ID="ASPxPanel6" runat="server" DefaultButton="btOK">
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent7" runat="server">
                            <dx:ASPxLabel ID="ASPxLabel3" ClientInstanceName="ASPxLabel3" Font-Bold="true" Font-Size="48" runat="server" Text="Please make sure that you have delivered the takeout item to the customer" Theme="Metropolis"></dx:ASPxLabel>
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
                <dx:ASPxButton ID="btnCancalChange" runat="server" Text="OK" AutoPostBack="False"
                    ClientSideEvents-Click="function(s, e) { pcTakeOutAlert.Hide(); }" Native="True" EnableTheming="False" CssClass="softkeys__btnPayment" />
            </div>

        </FooterTemplate>
    </dx:ASPxPopupControl>


    <div id="wb_Shape2" style="position: absolute; width: 53%; height: 600px; z-index: 1;">
        <div style="position: absolute; width: 100%; height: 100%">



            <script type="text/javascript">
                // <![CDATA[
                function CloseGridLookup() {
                    gridLookup.ConfirmCurrentSelection();
                    gridLookup.HideDropDown();
                }
                function OnGetSelectionButtonClick(s, e) {
                    var grid = gridLookup.GetGridView();
                    grid.GetRowValues(grid.GetFocusedRowIndex(), 'ProductID;BarCode', OnGetRowValues);
                }

                function gridKeyDown(s, e) {
                    gridLookup.ShowDropDown();
                }

                function OnGetRowValues(values) {
                    if (values[0] == null) return;
                    grid.PerformCallback(values[1]);
                    //alert('Product: ' + values[0]);
                }
            </script>
            <style type="text/css">
            .gridlookup{
                margin-top:6px;
                margin-bottom:5px;
            }
            </style>
            <dx:ASPxGridLookup CssClass="gridlookup" ID="gridLookup" runat="server" SelectionMode="Single" ClientInstanceName="gridLookup" ClientSideEvents-RowClick="OnGetSelectionButtonClick"
                KeyFieldName="ProductID" Width="100%" IncrementalFilteringMode="Contains">
                <Columns>
                    <dx:GridViewDataColumn FieldName="ProductID" Caption="ID" Visible="false" />
                    <dx:GridViewDataColumn FieldName="ProductName" Caption="Name" />
                    <dx:GridViewDataColumn FieldName="BarCode" Caption="Code" Settings-AllowAutoFilter="true" />
                </Columns>
                <DropDownButton Width="40" Image-Height="21" />
                <ClientSideEvents Init="function(s, e) { s.GetGridView().SetWidth(s.GetWidth()); }"  
                DropDown="function(s, e) { s.GetGridView().SetWidth(s.GetWidth()); }" />
                <GridViewStyles>

                    <Cell>
                        <Paddings PaddingTop="15px" PaddingBottom="15px" />
                    </Cell>
                </GridViewStyles>
                <GridViewProperties>

                    <Templates>

                        <StatusBar>
                            <table width="100%">
                                <tr>
                                    <td style="width: 100%" />
                                    <td onclick="return _aspxCancelBubble(event)">
                                        <dx:ASPxButton ID="Close" runat="server" AutoPostBack="false" Text="Close" ClientSideEvents-Click="CloseGridLookup" />
                                    </td>
                                </tr>
                            </table>
                        </StatusBar>
                    </Templates>
                    <Settings ShowFilterRow="false" />

                </GridViewProperties>
            </dx:ASPxGridLookup>



            <%--  <div style="height: 100%">--%>
            <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0" EnableHierarchyRecreation="True"
                Height="100%" Theme="Metropolis" Width="100%" EnableTabScrolling="true">
                <%--  <TabPages>
                   <dx:TabPage Text="PHO" TabStyle-Height="69px">
                        <TabStyle Height="69px"></TabStyle>
                        <ContentCollection>
                            <dx:ContentControl ID="ContentControl3" runat="server" Height="69px">
                                <uc:ItemList runat="server" ID="itemList1" CategoryId="4" MType="13" Flag="add" />
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Text="APERT">
                        <ContentCollection>
                            <dx:ContentControl ID="ContentControl4" runat="server">
                                <uc:ItemList runat="server" ID="itemList3" CategoryId="8" MType="0|13" Flag="add" />
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Text="RICE">
                        <ContentCollection>
                            <dx:ContentControl ID="ContentControl5" runat="server">
                                <uc:ItemList runat="server" ID="itemList4" CategoryId="1" MType="13" Flag="add" />
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Text="VERMICELLI">
                        <ContentCollection>
                            <dx:ContentControl ID="ContentControl10" runat="server">
                                <uc:ItemList runat="server" ID="itemList2" CategoryId="9" MType="13" Flag="add" />
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Text="DRINKS">
                        <ContentCollection>
                            <dx:ContentControl ID="ContentControl7" runat="server">
                                <uc:ItemList runat="server" ID="itemList6" CategoryId="13" MType="0|13" Flag="add" />
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Text="SODA">
                        <ContentCollection>
                            <dx:ContentControl ID="ContentControl8" runat="server">
                                <uc:ItemList runat="server" ID="itemList7" CategoryId="31" MType="0" Flag="add" />
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Text="SMOOTHIES">
                        <ContentCollection>
                            <dx:ContentControl ID="ContentControl9" runat="server">
                                <uc:ItemList runat="server" ID="itemList8" CategoryId="20" MType="0|13" Flag="add" />
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                </TabPages>--%>
                <TabStyle Height="50px">
                </TabStyle>
                <%--<ScrollButtonStyle>
                <Paddings Padding="20px" />
            </ScrollButtonStyle>--%>
            </dx:ASPxPageControl>
            <%--      </div>--%>
        </div>
    </div>

    <div id="wb_Shape3" style="position: absolute; left: 53%; width: 47%; height: 600px; z-index: 2;">
        <div id="Shape3">


            <script type="text/javascript">
                // <![CDATA[
                function ShowLoginWindow() {
                    pcLogin.PerformCallback();
                    pcLogin.Show();
                }

                function onEndCallBackPopUpUpdate(s, e) {
                    if (pcLogin.cpShowExtraWith == "1") {
                        lblcbListWith.SetVisible(true);
                        cbListWith.SetVisible(true);
                    } else {
                        lblcbListWith.SetVisible(false);
                        cbListWith.SetVisible(false);
                    }
                    if (pcLogin.cpShowExtraWithout == "1") {
                        lblcbListWithout.SetVisible(true);
                        cbListWithout.SetVisible(true);
                    } else {
                        lblcbListWithout.SetVisible(false);
                        cbListWithout.SetVisible(false);
                    }
                    if (pcLogin.cpShowCustomSelect == "1") {
                        lblcbListCustomName.SetVisible(true);
                        cbListCustomName.SetVisible(true);
                    } else {
                        lblcbListCustomName.SetVisible(false);
                        cbListCustomName.SetVisible(false);
                    }
                }

                function HidePopAddExtra() {
                    grid.PerformCallback("phomacextracancel");
                    pcLogin.Hide();
                }

                function HidePopOrderReview() {

                    pcOrderReview.Hide();
                }

                function addItem() {
                    var text = document.getElementById("txtItemAdd").value;
                    grid.PerformCallback(text);

                }

                function addExtraItem() {
                    grid.PerformCallback("phomacextra");
                    pcLogin.Hide();
                }




                //for gridview reload
                function OnEndCallback(s, e) {
                    if (grid.cpmsg != "") {
                        alert(grid.cpmsg);
                        //grid.cpmsg="";
                    } else if (grid.cpShowPopup == "1") {
                        pcLogin.Show();
                        document.getElementById("hiddencbListWith").value = "";
                        document.getElementById("hiddencbListWithout").value = "";
                        document.getElementById("hiddencbListCustomName").value = "";
                        pcLogin.PerformCallback();
                        //grid.cpShowPopup = "";
                    } else if (grid.cpVoidTicket == "OK") {
                        window.location.assign("/table.aspx");
                    }
                    else {
                        document.getElementById("txtItemAdd").value = "";
                        document.getElementById("txtOptionAdd").value = "";
                        checkedItemTakeOut.SetChecked(false);
                    }

                }

                function OnInit(s, e) {
                    $(".radioButtonStyle").parent().css("vertical-align", "top");
                }

                function clearAll() {
                    if (confirm('Are you sure?')) {
                        grid.PerformCallback("clearAll");
                    }
                }

                function voidTicket() {
                    if (confirm('Are you sure?')) {
                        grid.PerformCallback("VoidTicket");
                    }
                }

                function cbListExtraWith(text) {

                    pcLogin.PerformCallback(text);
                }


                function cbListWith_Init(s, e) {
                    var formats = [];
                    formats = document.getElementById("hiddencbListWith").value.split('|');
                    for (var i = 0; i < s.GetItemCount() ; i++) {
                        for (var j = 0; j < formats.length; j++) {
                            if (s.GetItem(i).value == formats[j]) {
                                s.SelectIndices([i]);
                            }
                        }
                    }
                }
                function cbListWith_SelectedIndexChanged(s, e) {
                    if (e.index == -1 && document.getElementById("hiddencbListWithInit").value != "") {
                        alert("ok");

                    }
                    var selectedItemsCount = s.GetSelectedItems();
                    document.getElementById("hiddencbListWith").value = "";
                    for (var i = 0; i < s.GetSelectedItems().length; i++) {
                        if (i == 0) {
                            document.getElementById("hiddencbListWith").value = document.getElementById("hiddencbListWith").value + s.GetSelectedItems()[i].value;
                        }
                        else {
                            document.getElementById("hiddencbListWith").value = document.getElementById("hiddencbListWith").value + "|" + s.GetSelectedItems()[i].value;
                        }
                    }
                }

                function cbListWithout_Init(s, e) {
                    var formats = [];
                    formats = document.getElementById("hiddencbListWithout").value.split('|');
                    for (var i = 0; i < s.GetItemCount() ; i++) {
                        for (var j = 0; j < formats.length; j++) {
                            if (s.GetItem(i).value == formats[j]) {
                                s.SelectIndices([i]);
                            }
                        }
                    }
                }
                function cbListWithout_SelectedIndexChanged(s, e) {
                    var selectedItemsCount = s.GetSelectedItems();
                    document.getElementById("hiddencbListWithout").value = "";
                    for (var i = 0; i < s.GetSelectedItems().length; i++) {
                        if (i == 0) {
                            document.getElementById("hiddencbListWithout").value = document.getElementById("hiddencbListWithout").value + s.GetSelectedItems()[i].value;
                        }
                        else {
                            document.getElementById("hiddencbListWithout").value = document.getElementById("hiddencbListWithout").value + "|" + s.GetSelectedItems()[i].value;
                        }
                    }
                }

                function cbListCustomName_Init(s, e) {
                    var formats = [];
                    formats = document.getElementById("hiddencbListCustomName").value.split('|');
                    for (var i = 0; i < s.GetItemCount() ; i++) {
                        for (var j = 0; j < formats.length; j++) {
                            if (s.GetItem(i).value == formats[j]) {
                                s.SelectIndices([i]);
                            }
                        }
                    }
                }
                function cbListCustomName_SelectedIndexChanged(s, e) {
                    var selectedItemsCount = s.GetSelectedItems();
                    document.getElementById("hiddencbListCustomName").value = "";
                    for (var i = 0; i < s.GetSelectedItems().length; i++) {
                        if (i == 0) {
                            document.getElementById("hiddencbListCustomName").value = document.getElementById("hiddencbListCustomName").value + s.GetSelectedItems()[i].value;
                        }
                        else {
                            document.getElementById("hiddencbListCustomName").value = document.getElementById("hiddencbListCustomName").value + "|" + s.GetSelectedItems()[i].value;
                        }
                    }
                }

                function confirmChangeTableIsCombine() {
                    var text = cbTableChange.GetText();
                    var formats = [];
                    formats = text.split(';');

                    if (formats.length == 2) {
                        if (formats[1].trim() == "Update") {
                            if (confirm("Are you sure you want to combine table " + formats[0] + " ?"))
                                toKitchen();
                        } else {
                            toKitchen();
                        }
                    }
                    else {
                        toKitchen();
                    }
                }

                function toKitchen() {
                    LoadingPanel.Show();
                    callBackToKitchen.PerformCallback('');
                }

                function OnCallbackToKitchenComplete(s, e) {
                    //LoadingPanel.Hide();
                    if (callBackToKitchen.cpCompleteMsg == "Done") {

                        window.location.assign("/table.aspx");
                        //LoadingPanel.Hide();
                    }
                    else if (callBackToKitchen.cpCompleteMsg != "") {
                        LoadingPanel.Hide();
                        alert(callBackToKitchen.cpCompleteMsg);
                    }
                }

                function ClearSelectionGrid() {
                    grid.PerformCallback("ClearSelection");
                    //grid.UnselectRows();
                }

                function ChangeSizeGrid() {
                    grid.PerformCallback("ChangeSizeGrid");
                }

                function TakeOutGrid() {
                    grid.PerformCallback("TakeOutGrid");
                }

                function DoubleItemsGrid() {
                    grid.PerformCallback("DoubleItemsGrid");
                }



                function EmergencyToKitchen() {
                    var value = document.getElementById("hiddenEmergency").value;
                    if (value == "false") {
                        value = "true";
                        btnSchedule.SetText("Emergency (Yes)");
                    } else {
                        value = "false";
                        btnSchedule.SetText("Emergency (No)");
                    }
                    document.getElementById("hiddenEmergency").value = value;
                    //grid.PerformCallback("Emergency");
                }

                function UndoGrid() {
                    grid.PerformCallback("UndoGrid");
                    pcMore.Hide();
                }

                function selectionChangedGrid(s, e) {
                    grid.PerformCallback("SelectionChanged");
                }

                // ]]>
            </script>

            <div style="float: left; width: 100%; height: 10%;">
                <style type="text/css">
                    /*.dxeListBoxItemRow_DevEx {
                height: 50px !important;
            }*/
                    .dxeListBoxItemRow_Metropolis {
                        height: 50px !important;
                    }
                </style>
                <div style="float: left; width: 25%; margin: 6px;">
                    <%--DataSourceID="SqlDataSourcecbTableChange"--%>
                    <dx:ASPxComboBox ID="cbTableChange" runat="server" ClientInstanceName="cbTableChange" Theme="Metropolis"
                        TextField="TableName" ValueField="TableID" OnCallback="cbTableChange_Callback" OnInit="cbTableChange_Init" Width="100%"
                        EnableCallbackMode="True" EnableViewState="False" ValueType="System.Int32" Height="100%">
                        <ClientSideEvents DropDown="function(s, e) {cbTableChange.PerformCallback();}" />
                        <Columns>
                            <dx:ListBoxColumn FieldName="TableName" Name="Table" />
                            <dx:ListBoxColumn FieldName="Status" Name="Status" />
                            <dx:ListBoxColumn FieldName="orderby" Name="Sort" Visible="false" />
                        </Columns>
                    </dx:ASPxComboBox>
                </div>

                <div style="float: left; margin: 3px;">
                    <asp:TextBox ID="txtCustomerName" runat="server" placeholder="Customer Name" Height="24px"></asp:TextBox>
                </div>
                <div style="float: left; margin: 5px;">
                    <input type="button" id="btnClearAll" value="Clear All" onclick="clearAll()" />
                </div>
                <div style="float: left; margin-top: 6px;">
                    <%--<dx:ASPxCheckBox ID="ckIsNewTable" runat="server" Text="New Table"></dx:ASPxCheckBox>--%>
                    <%--<asp:CheckBox ID="ckIsNewTable" Text="New Table" runat="server" />--%>
                    <asp:HiddenField ID="checkTakeOutTable" ClientIDMode="Static" runat="server" />
                </div>
                <%--<asp:Button ID="" runat="server" Text="Clear All" OnClientClick="clearAll()" UseSubmitBehavior="false" />--%>
            </div>

            <style type="text/css">
                .divfunctionButton {
                    float: left;
                    width: 85%;
                    height: 14%;
                    margin: 5%;
                }

                .functionButton {
                    width: 100%;
                    height: 100%;
                }
            </style>

            <div style="float: left; width: 100%; height: 90%">
                <div style="float: left; width: 15%; height: 100%">
                    <div class="divfunctionButton">
                        <dx:ASPxButton CssClass="functionButton" ID="btnClearSelection" UseSubmitBehavior="False" runat="server" Text="Clear Selection" AutoPostBack="False" Theme="Metropolis">
                            <ClientSideEvents Click="function(s, e) { ClearSelectionGrid(); }" />
                            <FocusRectPaddings Padding="0px" />
                        </dx:ASPxButton>
                    </div>
                    <div class="divfunctionButton">
                        <dx:ASPxButton CssClass="functionButton" ID="btnChangeSize" UseSubmitBehavior="false" runat="server" Text="Print" AutoPostBack="False" Theme="Metropolis">
                            <ClientSideEvents Click="function(s, e) { printBill(); }" />
                            <FocusRectPaddings Padding="0px" />
                        </dx:ASPxButton>
                    </div>
                    <div class="divfunctionButton">
                        <dx:ASPxButton CssClass="functionButton" ID="btnTakeOut" UseSubmitBehavior="false" runat="server" Text="Take Out" AutoPostBack="False" Theme="Metropolis">
                            <ClientSideEvents Click="function(s, e) { TakeOutGrid(); }" />
                            <FocusRectPaddings Padding="0px" />
                        </dx:ASPxButton>
                    </div>
                    <div class="divfunctionButton">
                        <dx:ASPxButton CssClass="functionButton" ID="btnDouble" UseSubmitBehavior="false" runat="server" Text="Double" AutoPostBack="False" Theme="Metropolis">
                            <ClientSideEvents Click="function(s, e) { DoubleItemsGrid(); }" />
                            <FocusRectPaddings Padding="0px" />
                        </dx:ASPxButton>
                    </div>

                    <div class="divfunctionButton">
                        <dx:ASPxButton CssClass="functionButton" ID="btnMore" runat="server" Text="More" AutoPostBack="False" Theme="Metropolis">
                            <ClientSideEvents Click="function(s, e) { showPCMore(); }" />
                            <FocusRectPaddings Padding="0px" />
                        </dx:ASPxButton>
                        <%--<dx:ASPxButton ID="ShowButton" runat="server" Text="Show Popup Window" AutoPostBack="False" />--%>
                    </div>
                    <asp:HiddenField ID="hiddenEmergency" Value="false" ClientIDMode="Static" runat="server" />
                    <%--<div class="divfunctionButton">
                       
                        <dx:ASPxButton CssClass="functionButton" ID="btnSchedule" ClientIDMode="Static" ClientInstanceName="btnSchedule" UseSubmitBehavior="false" runat="server" Text="Pay" AutoPostBack="False">
                            <ClientSideEvents Click="function(s, e) { ShowPayWindow(); }" />
                            <FocusRectPaddings Padding="0px" />

                        </dx:ASPxButton>
                    </div>--%>
                    <div class="divfunctionButton">
                        <dx:ASPxButton CssClass="functionButton" ID="btnPayment" ClientIDMode="Static" UseSubmitBehavior="false" ClientInstanceName="btnPayment" runat="server" Text="Pay" AutoPostBack="False" Theme="Metropolis">
                            <ClientSideEvents Click="function(s, e) { ShowPayWindow(); }" />
                            <FocusRectPaddings Padding="0px" />

                        </dx:ASPxButton>
                    </div>
                    <%-- <div class="divfunctionButton">
                        <dx:ASPxButton CssClass="functionButton" ID="btnUndoGrid" runat="server" Text="Undo" AutoPostBack="False">
                            <ClientSideEvents Click="function(s, e) { UndoGrid(); }" />
                            <FocusRectPaddings Padding="0px" />
                        </dx:ASPxButton>
                        <%--<dx:ASPxButton ID="ShowButton" runat="server" Text="Show Popup Window" AutoPostBack="False" />--%>
                    <%--</div>--%>
                    <!-- Popup Schedule -->
                    <dx:ASPxPopupControl ID="PopupControl" runat="server" CloseAction="OuterMouseClick" LoadContentViaCallback="OnFirstShow" Theme="Metropolis"
                        PopupVerticalAlign="WindowCenter" PopupHorizontalAlign="WindowCenter" AllowDragging="True"
                        ShowFooter="True" Width="250px" Height="130px" HeaderText="Schedule To Kitchen" ClientInstanceName="ClientPopupControl" PopupAnimationType="None">
                        <ContentCollection>
                            <dx:PopupControlContentControl ID="PopupControlContentControl" runat="server">
                                <dx:ASPxDateEdit ID="dateEditSchedule" EditFormatString="dd/MM/yyyy hh:mm tt" runat="server" EditFormat="Custom" Date="<%#DateTime.Today %>" Width="190" Caption="ASPxDateEdit" TimeSectionProperties-Visible="True" Theme="Metropolis">
                                    <TimeSectionProperties>
                                        <TimeEditProperties EditFormatString="hh:mm tt" />
                                    </TimeSectionProperties>
                                    <CalendarProperties>
                                        <FastNavProperties Enabled="true" />
                                    </CalendarProperties>
                                </dx:ASPxDateEdit>
                            </dx:PopupControlContentControl>
                        </ContentCollection>
                        <FooterStyle>
                            <Paddings Padding="10px" />
                        </FooterStyle>
                        <FooterTemplate>
                            <div class="divfunctionButtonSchedule">
                                <dx:ASPxButton CssClass="functionButtonSchedule" ID="UpdateButton" runat="server" Text="Submit" AutoPostBack="False"
                                    ClientSideEvents-Click="function(s, e) { ClientPopupControl.PerformCallback(); }">
                                    <FocusRectPaddings Padding="0px" />
                                </dx:ASPxButton>
                            </div>
                            <div class="divfunctionButtonSchedule">
                                <dx:ASPxButton CssClass="functionButtonSchedule" ID="ASPxButton1" runat="server" Text="Clear Schedule" AutoPostBack="False"
                                    ClientSideEvents-Click="function(s, e) { ClientPopupControl.PerformCallback(); }">
                                    <FocusRectPaddings Padding="0px" />
                                </dx:ASPxButton>
                            </div>
                        </FooterTemplate>
                    </dx:ASPxPopupControl>
                </div>
                <style type="text/css">
                    .divfunctionButtonSchedule {
                        float: right;
                        width: 25%;
                        height: 100%;
                        margin: 5px;
                    }

                    .functionButtonSchedule {
                        width: 100%;
                        height: 100%;
                    }
                </style>
                <div style="float: left; width: 85%">
                    <dx:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server"
                        KeyFieldName="SaleItemID" AutoGenerateColumns="False" Width="100%" Theme="Metropolis" CssClass="girdItems" OnCustomCallback="grid_CustomCallback"
                        OnCustomButtonCallback="grid_CustomButtonCallback" OnInit="grid_Init" OnCustomUnboundColumnData="grid_CustomUnboundColumnData">
                        <%-- OnPreRender="grid_PreRender"--%>
                        <Settings VerticalScrollBarMode="Auto" VerticalScrollableHeight="260" />
                        <%--RowClick="function(s,e){grid.PerformCallback('SelectionChanged');}" --%>
                        <ClientSideEvents EndCallback="function(s, e) { OnEndCallback(s,e); }" />
                        <Columns>
                            <dx:GridViewCommandColumn ButtonType="Button" VisibleIndex="0" Width="65px">
                                <CustomButtons>
                                    <dx:GridViewCommandColumnCustomButton ID="btnDetails" Text="Update" />
                                </CustomButtons>
                            </dx:GridViewCommandColumn>
                            <dx:GridViewDataColumn FieldName="Qty" VisibleIndex="1" Caption="SL" Width="40px">
                                <CellStyle Font-Size="12pt">
                                </CellStyle>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="CustomKitchenName" VisibleIndex="2" Caption="Name">
                                <CellStyle Font-Bold="True" Font-Size="12pt">
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
                            <dx:GridViewDataColumn FieldName="BarCode" VisibleIndex="5" Caption="Code" Width="60px" Visible="false">
                                <CellStyle Font-Bold="False" Font-Size="12pt">
                                </CellStyle>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="SaleItemID" VisibleIndex="8" Visible="False">
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="BarCode" VisibleIndex="10" Visible="true" Width="40px">
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
                            <dx:GridViewCommandColumn ButtonType="Button" VisibleIndex="6" Width="65px">
                                <CustomButtons>
                                    <dx:GridViewCommandColumnCustomButton ID="btnDel" Text="Delete" />
                                </CustomButtons>
                            </dx:GridViewCommandColumn>
                        </Columns>

                        <SettingsBehavior AllowSelectSingleRowOnly="True" ProcessSelectionChangedOnServer="True" AllowSelectByRowClick="True" ConfirmDelete="True" />
                        <SettingsPager Mode="ShowAllRecords" />
                        <Settings ShowTitlePanel="true" VerticalScrollBarMode="Visible" />
                        <SettingsText Title="Edit Form Editing" />

                        <Styles>
                            <Row Font-Bold="False">
                            </Row>
                            <TitlePanel Font-Bold="True" ForeColor="Black">
                            </TitlePanel>
                        </Styles>
                    </dx:ASPxGridView>
                </div>
                <% 
                    PhoMac.Model.Employee emp = (PhoMac.Model.Employee)Session["LoginEmp"];
                %>
                <%--MaxHeight="500px" MaxWidth="800px" MinHeight="500px" MinWidth="800px"--%>
                <dx:ASPxPopupControl ID="pcLogin" runat="server" CloseAction="CloseButton" Modal="True" Theme="Metropolis"
                    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="TopSides" ClientInstanceName="pcLogin"
                    HeaderText="Update Item" AllowDragging="True" PopupAnimationType="None" EnableViewState="False"
                    Width="80%" Height="50%"
                    ShowCloseButton="False"
                    OnWindowCallback="pcLogin_WindowCallback" OnInit="pcLogin_Init" LoadContentViaCallback="OnPageLoad" ShowFooter="True" DisappearAfter="10">
                    <ContentCollection>
                        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                            <dx:ASPxPanel ID="Panel1" runat="server">
                                <PanelCollection>
                                    <dx:PanelContent ID="PanelContent1" runat="server">

                                        <dx:ASPxFormLayout runat="server" ID="OptionsFormLayout" Theme="Metropolis">
                                            <Items>
                                                <dx:LayoutGroup ColCount="3" ShowCaption="False">
                                                    <Items>
                                                        <dx:LayoutItem Caption="Kitchen Name">
                                                            <LayoutItemNestedControlCollection>
                                                                <dx:LayoutItemNestedControlContainer>
                                                                    <dx:ASPxTextBox ID="txtKitchenName" Theme="Metropolis" runat="server" ReadOnly="true" Width="150px" AutoPostBack="false"></dx:ASPxTextBox>
                                                                </dx:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dx:LayoutItem>
                                                        <dx:LayoutItem Caption="Option">
                                                            <LayoutItemNestedControlCollection>
                                                                <dx:LayoutItemNestedControlContainer>
                                                                    <dx:ASPxTextBox ID="txtOption" Theme="Metropolis" runat="server" Width="150px" ClientInstanceName="txtOption" AutoPostBack="false"></dx:ASPxTextBox>
                                                                    <asp:HiddenField ID="txtOptionVNese" ClientIDMode="Static" runat="server" />
                                                                </dx:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dx:LayoutItem>
                                                        <dx:LayoutItem RowSpan="6" Caption="Extra">
                                                            <LayoutItemNestedControlCollection>
                                                                <dx:LayoutItemNestedControlContainer>
                                                                    <dx:ASPxListBox ID="listExtraName" Theme="DevEx" runat="server" ClientInstanceName="listExtraName" ValueType="System.String" OnCallback="listExtraName_Callback" EnableCallbackMode="True"></dx:ASPxListBox>
                                                                </dx:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dx:LayoutItem>
                                                        <dx:LayoutItem Caption="Quality">
                                                            <LayoutItemNestedControlCollection>
                                                                <dx:LayoutItemNestedControlContainer>
                                                                    <dx:ASPxComboBox ID="dropListQty" runat="server" Width="150px" ValueType="System.Int32" Theme="Metropolis">
                                                                        <Items>
                                                                            <dx:ListEditItem Text="1" Value="1" />
                                                                            <dx:ListEditItem Text="2" Value="2" />
                                                                            <dx:ListEditItem Text="3" Value="3" />
                                                                            <dx:ListEditItem Text="4" Value="4" />
                                                                            <dx:ListEditItem Text="5" Value="5" />
                                                                            <dx:ListEditItem Text="6" Value="6" />
                                                                            <dx:ListEditItem Text="7" Value="7" />
                                                                            <dx:ListEditItem Text="8" Value="8" />
                                                                            <dx:ListEditItem Text="9" Value="9" />
                                                                            <dx:ListEditItem Text="10" Value="10" />
                                                                            <dx:ListEditItem Text="11" Value="11" />
                                                                            <dx:ListEditItem Text="12" Value="12" />
                                                                            <dx:ListEditItem Text="13" Value="13" />
                                                                            <dx:ListEditItem Text="14" Value="14" />
                                                                            <dx:ListEditItem Text="15" Value="15" />
                                                                            <dx:ListEditItem Text="16" Value="16" />
                                                                            <dx:ListEditItem Text="17" Value="17" />
                                                                            <dx:ListEditItem Text="18" Value="18" />
                                                                            <dx:ListEditItem Text="19" Value="19" />
                                                                            <dx:ListEditItem Text="20" Value="20" />
                                                                        </Items>
                                                                    </dx:ASPxComboBox>
                                                                </dx:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dx:LayoutItem>
                                                        <dx:LayoutItem Caption=" ">
                                                            <LayoutItemNestedControlCollection>
                                                                <dx:LayoutItemNestedControlContainer>
                                                                </dx:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dx:LayoutItem>
                                                        <dx:LayoutItem Caption="Take Out">
                                                            <LayoutItemNestedControlCollection>
                                                                <dx:LayoutItemNestedControlContainer>
                                                                    <dx:ASPxCheckBox ID="checkedTakeOut" Theme="Metropolis" runat="server" Text="Take Out" AutoPostBack="false" />
                                                                </dx:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dx:LayoutItem>
                                                        <dx:LayoutItem Caption="Small Size">
                                                            <LayoutItemNestedControlCollection>
                                                                <dx:LayoutItemNestedControlContainer>
                                                                    <dx:ASPxCheckBox ID="checkedSmallSize" Theme="Metropolis" runat="server" Text="Small Size" AutoPostBack="false" />
                                                                </dx:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dx:LayoutItem>
                                                        <dx:LayoutItem ColSpan="3" Caption="With">
                                                            <LayoutItemNestedControlCollection>
                                                                <dx:LayoutItemNestedControlContainer>
                                                                    <dx:ASPxCheckBoxList ID="cbListWith" Theme="Metropolis" ClientInstanceName="cbListWith" runat="server" ValueType="System.String"
                                                                        ValueField="ProductID" TextField="ProductName" RepeatColumns="6" RepeatLayout="Table">
                                                                        <ClientSideEvents SelectedIndexChanged="cbListWith_SelectedIndexChanged" Init="cbListWith_Init" />
                                                                    </dx:ASPxCheckBoxList>
                                                                    <asp:HiddenField ID="hiddencbListWith" ClientIDMode="Static" runat="server" />
                                                                </dx:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dx:LayoutItem>
                                                        <dx:LayoutItem ColSpan="3" Caption="Without">
                                                            <LayoutItemNestedControlCollection>
                                                                <dx:LayoutItemNestedControlContainer>
                                                                    <dx:ASPxCheckBoxList ID="cbListWithout" Theme="Metropolis" ClientInstanceName="cbListWithout" runat="server"
                                                                        ValueField="ProductID" TextField="ProductName" RepeatColumns="6" RepeatLayout="Table">
                                                                        <ClientSideEvents SelectedIndexChanged="cbListWithout_SelectedIndexChanged" Init="cbListWithout_Init" />
                                                                    </dx:ASPxCheckBoxList>
                                                                    <asp:HiddenField ID="hiddencbListWithout" ClientIDMode="Static" runat="server" />
                                                                </dx:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dx:LayoutItem>
                                                        <dx:LayoutItem ColSpan="3" Caption="Custom Select">
                                                            <LayoutItemNestedControlCollection>
                                                                <dx:LayoutItemNestedControlContainer>
                                                                    <dx:ASPxCheckBoxList ID="cbListCustomName" Theme="Metropolis" ClientInstanceName="cbListCustomName" runat="server"
                                                                        ValueField="ProductID" TextField="ProductName" RepeatColumns="6" RepeatLayout="Table">
                                                                        <ClientSideEvents SelectedIndexChanged="cbListCustomName_SelectedIndexChanged" Init="cbListCustomName_Init" />
                                                                    </dx:ASPxCheckBoxList>
                                                                    <asp:HiddenField ID="hiddencbListCustomName" ClientIDMode="Static" runat="server" />
                                                                </dx:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dx:LayoutItem>
                                                    </Items>
                                                </dx:LayoutGroup>
                                            </Items>
                                        </dx:ASPxFormLayout>
                                    </dx:PanelContent>
                                </PanelCollection>
                            </dx:ASPxPanel>
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <FooterTemplate>
                        <div style="margin: 3px;">
                            <%--<dx:ASPxButton ID="btOK" runat="server" Text="OK" Width="80px" AutoPostBack="False" OnClick="btOK_Click"></dx:ASPxButton>--%>
                            <dx:ASPxButton ID="btOK" runat="server" Text="Update" AutoPostBack="False"
                                ClientSideEvents-Click="function(s, e) { addExtraItem(); }" EnableTheming="False" Native="True" CssClass="softkeys__btnExtraOK" />
                            <dx:ASPxButton ID="btCancel" runat="server" Text="Cancel" AutoPostBack="False"
                                ClientSideEvents-Click="function(s, e) { HidePopAddExtra(); }" Native="True" EnableTheming="False" CssClass="softkeys__btnExtraCancel" />
                        </div>
                    </FooterTemplate>
                    <ClientSideEvents EndCallback="function(s, e) { onEndCallBackPopUpUpdate(s,e); }" />
                    <ContentStyle>
                        <Paddings PaddingBottom="5px" />
                    </ContentStyle>
                </dx:ASPxPopupControl>

            </div>


        </div>

        <div id="Div1" style="height: 40%; background-color: #ffc50026">
            <div style="float: left;">

                <%--<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceFunctionGrid" runat="Server">--%>

                <div style="float: left; display: table; width: 20%;">
                    <div>
                        <div class="dBtnAddItem">
                            <%--Theme="BlackGlass"--%>
                            <dx:ASPxRadioButtonList ID="radioSize" runat="server" EnableTheming="True" Theme="Metropolis" SelectedIndex="0" RepeatColumns="2" Visible="false">
                                <RadioButtonStyle CssClass="radioButtonStyle"></RadioButtonStyle>
                                <ClientSideEvents Init="OnInit" />
                                <Items>
                                    <dx:ListEditItem Selected="True" Text="Small" Value="true" />
                                    <dx:ListEditItem Text="Large" Value="false" />
                                </Items>
                                <Border BorderStyle="None" />
                            </dx:ASPxRadioButtonList>
                        </div>
                        <div id="dTxtItem">
                            <%--Theme="BlackGlass"--%>
                            <dx:ASPxCheckBox ID="checkedItemTakeOut" Layout="Flow" ClientIDMode="Static" ClientInstanceName="checkedItemTakeOut" runat="server" Text="ToGo" Theme="Metropolis">
                            </dx:ASPxCheckBox>
                            <asp:TextBox ID="txtItemAdd" ClientIDMode="Static" runat="server" ReadOnly="true"></asp:TextBox>

                            <%--<asp:CheckBox ID="checkedItemTakeOut" ClientIDMode="Static" runat="server" Text="ToGo" />--%>
                        </div>

                        <div class="dBtnAddItem" style="padding-left: 8px;">
                            <div class="softkeys">
                                <li class="softkeys__btnAdd" data-type="symbol"><span><strong>Add Item</strong></span></li>
                            </div>
                        </div>

                        <div id="Div2" style="margin-left: 3%;">
                            <asp:TextBox ID="txtOptionAdd" ClientIDMode="Static" runat="server" TextMode="MultiLine" Text="" Columns="16" placeholder="Option"></asp:TextBox>
                            <asp:HiddenField ID="txtOptionAddVNese" ClientIDMode="Static" runat="server" />
                        </div>
                        <div id="Div3">
                            <%--OnClick="btnToKitchen_Click"--%>
                            <%--OnClientClick="if ( ! confirmChangeTableIsCombine()) return false;"--%><%-- --%>
                            <dx:ASPxButton ID="btnToKitchen" CssClass="cssBtnAddItem" Native="True" runat="server" Text="To Kitchen" AutoPostBack="False" UseSubmitBehavior="False" EnableTheming="true" Theme="Metropolis">
                                <ClientSideEvents Click="function(s, e) {
	confirmChangeTableIsCombine()
}" />
                            </dx:ASPxButton>
                            <dx:ASPxLoadingPanel ID="LoadingPanel" Modal="true" runat="server" ClientInstanceName="LoadingPanel"></dx:ASPxLoadingPanel>
                            <dx:ASPxCallback ID="callBackToKitchen" ClientInstanceName="callBackToKitchen" runat="server" OnCallback="callBackToKitchen_Callback" OnInit="callBackToKitchen_Init" ClientSideEvents-EndCallback="OnCallbackToKitchenComplete">
                                <ClientSideEvents EndCallback="function(s, e) {
	OnCallbackToKitchenComplete(s,e);
}" />
                            </dx:ASPxCallback>

                        </div>

                    </div>
                </div>
                <div style="float: left; width: 68%; position: absolute; left: 31%;">
                    <div class="softkeys">
                        <li class="softkeys__btn  " data-type="symbol"><span><strong>1</strong></span></li>
                        <li class="softkeys__btn  " data-type="symbol"><span><strong>2</strong></span></li>
                        <li class="softkeys__btn  " data-type="symbol"><span><strong>3</strong></span></li>
                        <br>
                        <li class="softkeys__btn  " data-type="symbol"><span><strong>4</strong></span></li>
                        <li class="softkeys__btn  " data-type="symbol"><span><strong>5</strong></span></li>
                        <li class="softkeys__btn  " data-type="symbol"><span><strong>6</strong></span></li>
                        <br>
                        <li class="softkeys__btn  " data-type="symbol"><span><strong>7</strong></span></li>
                        <li class="softkeys__btn  " data-type="symbol"><span><strong>8</strong></span></li>
                        <li class="softkeys__btn  " data-type="symbol"><span><strong>9</strong></span></li>
                        <br>
                        <li class="softkeys__btn  " data-type="symbol"><span><strong>0</strong></span></li>
                        <li class="softkeys__btn  " data-type="symbol"><span><strong>E</strong></span></li>
                        <li class="softkeys__btn  " data-type="symbol"><span><strong>A</strong></span></li>
                        <br />
                        <li class="softkeys__btn  " data-type="symbol"><span><strong>B</strong></span></li>
                        <li class="softkeys__btn" data-type="symbol"><span><strong>C</strong></span></li>
                        <li class="softkeys__btndel" data-type="delete"><span><strong>Del</strong></span></li>
                        <br>
                    </div>

                </div>
                <%--</asp:Content>--%>
            </div>
        </div>


    </div>


    <dx:ASPxPopupControl ID="pcCustomAmount" runat="server" CloseAction="CloseButton" Modal="True" ShowFooter="True" Theme="Metropolis"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="pcCustomAmount"
        HeaderText="Custom Amount" AllowDragging="True" PopupAnimationType="None" EnableViewState="False" Width="400px">
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl4" runat="server">
                <dx:ASPxPanel ID="ASPxPanel3" runat="server" DefaultButton="btOK">
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent4" runat="server">
                            <table>
                                <tr>
                                    <td>
                                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Name"></dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxTextBox ID="txtCustomAmountName" Text="Custom Amount" runat="server" Width="170px"></dx:ASPxTextBox>
                                        <td>
                                </tr>
                                <tr>
                                    <td>
                                        <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="Amount"></dx:ASPxLabel>
                                        <td>
                                            <dx:ASPxTextBox ID="txtCustomAmount" ClientInstanceName="txtCustomAmount" runat="server" Width="170px"></dx:ASPxTextBox>
                                            <td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <dx:ASPxButton ID="btnOKCustomAmount" runat="server" Text="OK" AutoPostBack="False"
                                            ClientSideEvents-Click="function(s, e) { customAmountOK(); }" EnableTheming="False" Native="True" CssClass="softkeys__btnPayment" />
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
    </dx:ASPxPopupControl>
    <dx:ASPxCallback ID="callBackCustomAmount" ClientInstanceName="callBackCustomAmount" runat="server" OnCallback="callBackCustomAmount_Callback" OnInit="callBackCustomAmount_Init" ClientSideEvents-EndCallback="callBackCustomAmountComplete">
        <ClientSideEvents EndCallback="function(s, e) { callBackCustomAmountComplete(s,e); }" />
    </dx:ASPxCallback>

</asp:Content>
