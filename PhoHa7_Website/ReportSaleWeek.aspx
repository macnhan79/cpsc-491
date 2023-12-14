<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage2.master" AutoEventWireup="true" CodeFile="ReportSaleWeek.aspx.cs" Inherits="ReportSaleWeek" %>

<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxFormLayout" TagPrefix="dx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlace1" runat="Server">

    <dx:ASPxRoundPanel ID="ASPxRoundPanelAttendance" Width="100%" Height="100%" runat="server" Theme="Metropolis" HeaderText="Week Sale">
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
                                            <dx:ASPxDateEdit ID="dateEdit" EditFormatString="dd/MM/yyyy" runat="server" EditFormat="Custom" Date="<%#DateTime.Today %>" Width="190" Caption="ASPxDateEdit" Theme="Metropolis" OnDateChanged="dateEditAttendance_DateChanged" AutoPostBack="True">
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
                            </Items>
                        </dx:LayoutGroup>
                    </Items>
                </dx:ASPxFormLayout>

                <dx:ASPxFormLayout runat="server" ID="ASPxFormLayout2" Theme="Metropolis">
                    <Items>
                        <dx:LayoutGroup ColCount="1" ShowCaption="False">
                            <Items>
                                <dx:LayoutItem ShowCaption="False">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:WebChartControl ID="WebChartControl1" runat="server" Height="350px" Width="600px">
                                                <seriesserializable>
<dx:Series Name="Week" LabelsVisibility="True" LegendText="Week Sale"></dx:Series>
</seriesserializable>

                                                <diagramserializable>
<dx:XYDiagram>
<AxisX VisibleInPanesSerializable="-1">
<Range SideMarginsEnabled="True"></Range>

<NumericOptions Format="General"></NumericOptions>
</AxisX>

<AxisY VisibleInPanesSerializable="-1">
<Range SideMarginsEnabled="True"></Range>

<NumericOptions Format="General"></NumericOptions>
</AxisY>
</dx:XYDiagram>
</diagramserializable>

                                                <fillstyle><OptionsSerializable>
<dx:SolidFillOptions></dx:SolidFillOptions>
</OptionsSerializable>
</fillstyle>

                                                <legend alignmenthorizontal="Center" alignmentvertical="TopOutside"></legend>
                                                <seriesserializable>
<dx:Series Name="Week" LegendText="Week Sale"><ViewSerializable>
<dx:SideBySideBarSeriesView></dx:SideBySideBarSeriesView>
</ViewSerializable>
<LabelSerializable>
<dx:SideBySideBarSeriesLabel LineVisible="True">
<FillStyle><OptionsSerializable>
<dx:SolidFillOptions></dx:SolidFillOptions>
</OptionsSerializable>
</FillStyle>
<PointOptionsSerializable>
<dx:PointOptions>
<ArgumentNumericOptions Format="General"></ArgumentNumericOptions>

<ValueNumericOptions Format="General"></ValueNumericOptions>
</dx:PointOptions>
</PointOptionsSerializable>
</dx:SideBySideBarSeriesLabel>
</LabelSerializable>
<LegendPointOptionsSerializable>
<dx:PointOptions>
<ArgumentNumericOptions Format="General"></ArgumentNumericOptions>

<ValueNumericOptions Format="General"></ValueNumericOptions>
</dx:PointOptions>
</LegendPointOptionsSerializable>
</dx:Series>
</seriesserializable>

                                                <seriestemplate><ViewSerializable>
<dx:SideBySideBarSeriesView></dx:SideBySideBarSeriesView>
</ViewSerializable>
<LabelSerializable>
<dx:SideBySideBarSeriesLabel LineVisible="True">
<FillStyle><OptionsSerializable>
<dx:SolidFillOptions></dx:SolidFillOptions>
</OptionsSerializable>
</FillStyle>
<PointOptionsSerializable>
<dx:PointOptions>
<ArgumentNumericOptions Format="General"></ArgumentNumericOptions>

<ValueNumericOptions Format="General"></ValueNumericOptions>
</dx:PointOptions>
</PointOptionsSerializable>
</dx:SideBySideBarSeriesLabel>
</LabelSerializable>
<LegendPointOptionsSerializable>
<dx:PointOptions>
<ArgumentNumericOptions Format="General"></ArgumentNumericOptions>

<ValueNumericOptions Format="General"></ValueNumericOptions>
</dx:PointOptions>
</LegendPointOptionsSerializable>
</seriestemplate>

                                                <crosshairoptions argumentlinecolor="222, 57, 205" valuelinecolor="222, 57, 205"><CommonLabelPositionSerializable>
<dx:CrosshairMousePosition></dx:CrosshairMousePosition>
</CommonLabelPositionSerializable>
</crosshairoptions>

                                                <tooltipoptions><ToolTipPositionSerializable>
<dx:ToolTipMousePosition></dx:ToolTipMousePosition>
</ToolTipPositionSerializable>
</tooltipoptions>

                                            </dx:WebChartControl>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                            </Items>
                        </dx:LayoutGroup>
                    </Items>
                </dx:ASPxFormLayout>


            </dx:PanelContent>
        </PanelCollection>
        <BorderBottom BorderStyle="None" />
    </dx:ASPxRoundPanel>



</asp:Content>

