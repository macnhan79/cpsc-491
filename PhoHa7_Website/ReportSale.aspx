<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage2.master" AutoEventWireup="true" CodeFile="ReportSale.aspx.cs" Inherits="ReportSale" %>

<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxFormLayout" TagPrefix="dx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlace1" runat="Server">
    <dx:ASPxRoundPanel ID="ASPxRoundPanelDaySale" Width="100%" Height="100%" runat="server" Theme="Metropolis" HeaderText="Report Sale">
        <PanelCollection>
            <dx:PanelContent>

                
                
                
                
                
               

                <dx:ASPxFormLayout runat="server" ID="OptionsFormLayout" Theme="Metropolis">
                    <Items>
                        <dx:LayoutGroup GroupBoxDecoration="None" ColCount="2" Caption="Date">
                            <Items>
                                <dx:LayoutItem Caption="Layout Item" RowSpan="10" ShowCaption="False">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxCalendar ID="ASPxCalendar1" AutoPostBack="true" runat="server" Theme="Metropolis" OnSelectionChanged="ASPxCalendar1_SelectionChanged"></dx:ASPxCalendar>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Cash">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxLabel ID="lblCash" runat="server" Text="ASPxLabel" Theme="Metropolis" ></dx:ASPxLabel>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Credit">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxLabel ID="lblCredit" runat="server" Text="ASPxLabel" Theme="Metropolis"></dx:ASPxLabel>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Withdraw">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxLabel ID="lblWithdraw" runat="server" Text="ASPxLabel" Theme="Metropolis"></dx:ASPxLabel>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Cash-Withdraw">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxLabel runat="server" Text="ASPxLabel" Theme="Metropolis" ID="lblCash1"></dx:ASPxLabel>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Sale Tax">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxLabel runat="server" Text="ASPxLabel" Theme="Metropolis" ID="lblSaleTax"></dx:ASPxLabel>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Credit Tips">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxLabel runat="server" Text="ASPxLabel" Theme="Metropolis" ID="lblTips"></dx:ASPxLabel>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Gross Total">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                             <dx:ASPxLabel ID="lblTotal" runat="server" Text="ASPxLabel" Theme="Metropolis"></dx:ASPxLabel>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Net Total" CaptionStyle-Font-Bold="true">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server" SupportsDisabledAttribute="True">
                                             <dx:ASPxLabel ID="lblNetTotal" Font-Bold="true" runat="server" Text="ASPxLabel" Theme="Metropolis"></dx:ASPxLabel>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Layout Item" ShowCaption="False">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True"></dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Layout Item" ShowCaption="False">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True"></dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="grid" ShowCaption="False" ColSpan="2">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxGridView ID="gridWithdraw" runat="server" AutoGenerateColumns="False" Theme="Metropolis">
                                                <Columns>
                                                    <dx:GridViewDataTextColumn FieldName="Description" Caption="Name" VisibleIndex="0"></dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="Amount" Caption="Amount" VisibleIndex="1">
                                                        <PropertiesTextEdit DisplayFormatString="c2" />
                                                    </dx:GridViewDataTextColumn>
                                                </Columns>
                                            </dx:ASPxGridView>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                            </Items>
                            <Items>
                                

                            </Items>
                        </dx:LayoutGroup>
                    </Items>
                </dx:ASPxFormLayout>

            </dx:PanelContent>
        </PanelCollection>
        <BorderBottom BorderStyle="None" />
    </dx:ASPxRoundPanel>
</asp:Content>

