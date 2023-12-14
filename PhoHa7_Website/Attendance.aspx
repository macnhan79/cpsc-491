<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage2.master" AutoEventWireup="true" CodeFile="Attendance.aspx.cs" Inherits="Attendance" %>

<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxFormLayout" TagPrefix="dx" %>
<%@ Register Src="~/UserControl/AttendanceCheckBoxList.ascx" TagPrefix="uc" TagName="AttendanceCheckBoxList" %>




<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlace1" runat="Server">
    <dx:ASPxRoundPanel ID="ASPxRoundPanelAttendance" Width="100%" Height="100%" runat="server" Theme="Metropolis" HeaderText="Attendance">
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
                                            <dx:ASPxDateEdit ID="dateEditAttendance" EditFormatString="dd/MM/yyyy" runat="server" EditFormat="Custom" Date="<%#DateTime.Today %>" Width="190" Caption="ASPxDateEdit" Theme="Metropolis" OnDateChanged="dateEditAttendance_DateChanged" AutoPostBack="True">
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
                                        <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True"></dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Layout Item" ShowCaption="False">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxButton runat="server" Text="Submit" ID="btnSubmit" Theme="Metropolis" OnClick="btnSubmit_Click"></dx:ASPxButton>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                
                            </Items>
                        </dx:LayoutGroup>
                    </Items>
                </dx:ASPxFormLayout>



                <dx:ASPxFormLayout runat="server" ID="ASPxFormLayout1" Theme="Metropolis" ColCount="2">
                    <Items>
                        <dx:LayoutGroup Caption="Choose Employee" Height="100%" ColSpan="2">
                            <Items>
                                <dx:LayoutItem ShowCaption="false" Caption=" ">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <uc:AttendanceCheckBoxList runat="server" id="AttendanceCheckBoxList" />
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

