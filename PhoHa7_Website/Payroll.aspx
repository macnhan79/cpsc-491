<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage2.master" AutoEventWireup="true" CodeFile="Payroll.aspx.cs" Inherits="Payroll" EnableEventValidation="false" %>

<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxFormLayout" TagPrefix="dx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlace1" runat="Server">


    <dx:ASPxRoundPanel ID="ASPxRoundPanelPayroll" Width="100%" Height="100%" runat="server" Theme="Metropolis" HeaderText="Payroll">
        <PanelCollection>
            <dx:PanelContent>
                <dx:ASPxFormLayout runat="server" ID="OptionsFormLayout" Theme="Metropolis">
                    <SettingsItemCaptions Location="Top"></SettingsItemCaptions>
                    <Items>
                        <dx:LayoutGroup ColCount="5" Caption="Select Date">
                            <Items>
                                <dx:LayoutItem Caption="Month">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxComboBox ID="cbMonth" runat="server" Theme="Metropolis">
                                                <Items>
                                                    <dx:ListEditItem Text="1" Value="1"></dx:ListEditItem>
                                                    <dx:ListEditItem Text="2" Value="2"></dx:ListEditItem>
                                                    <dx:ListEditItem Text="3" Value="3"></dx:ListEditItem>
                                                    <dx:ListEditItem Text="4" Value="4"></dx:ListEditItem>
                                                    <dx:ListEditItem Text="5" Value="5"></dx:ListEditItem>
                                                    <dx:ListEditItem Text="6" Value="6"></dx:ListEditItem>
                                                    <dx:ListEditItem Text="7" Value="7"></dx:ListEditItem>
                                                    <dx:ListEditItem Text="8" Value="8"></dx:ListEditItem>
                                                    <dx:ListEditItem Text="9" Value="9"></dx:ListEditItem>
                                                    <dx:ListEditItem Text="10" Value="10"></dx:ListEditItem>
                                                    <dx:ListEditItem Text="11" Value="11"></dx:ListEditItem>
                                                    <dx:ListEditItem Text="12" Value="12"></dx:ListEditItem>
                                                </Items>
                                            </dx:ASPxComboBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Year">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxComboBox ID="cbYear" runat="server" Theme="Metropolis">
                                            </dx:ASPxComboBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="*">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxButton ID="btnSubmit" runat="server" Text="Submit" Theme="Metropolis" OnClick="btnSubmit_Click"></dx:ASPxButton>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                
                                <%--         <dx:LayoutItem Caption="*">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxButton ID="ASPxButton2" runat="server" Text="Submit" Theme="Metropolis"></dx:ASPxButton>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>--%>
                            </Items>
                        </dx:LayoutGroup>
                    </Items>
                </dx:ASPxFormLayout>
                <dx:ASPxFormLayout runat="server" ID="ASPxFormLayout2" Theme="Metropolis">
                    <SettingsItemCaptions Location="Top"></SettingsItemCaptions>
                    <Items>
                        <dx:LayoutGroup ColCount="1" Caption="1-15">
                            <Items>
                                <dx:LayoutItem ShowCaption="False">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxButton ID="btnExport115" runat="server" Text="Export" Theme="Metropolis" OnClick="btnExport115_Click"></dx:ASPxButton>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                            </Items>
                            <Items>
                                <dx:LayoutItem ShowCaption="False">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxGridView ID="gridPayRoll115" KeyFieldName="Att_AttendanceID" runat="server" AutoGenerateColumns="False">

                                                <Columns>
                                                    <dx:GridViewDataTextColumn FieldName="Att_AttendanceID" Visible="false"></dx:GridViewDataTextColumn>

                                                    <dx:GridViewDataCheckColumn Caption="1" FieldName="Day1" VisibleIndex="1"></dx:GridViewDataCheckColumn>
                                                    <dx:GridViewDataCheckColumn Caption="2" FieldName="Day2" VisibleIndex="2"></dx:GridViewDataCheckColumn>
                                                    <dx:GridViewDataCheckColumn Caption="3" FieldName="Day3" VisibleIndex="3"></dx:GridViewDataCheckColumn>
                                                    <dx:GridViewDataCheckColumn Caption="4" FieldName="Day4" VisibleIndex="4"></dx:GridViewDataCheckColumn>
                                                    <dx:GridViewDataCheckColumn Caption="5" FieldName="Day5" VisibleIndex="5"></dx:GridViewDataCheckColumn>
                                                    <dx:GridViewDataCheckColumn Caption="6" FieldName="Day6" VisibleIndex="6"></dx:GridViewDataCheckColumn>
                                                    <dx:GridViewDataCheckColumn Caption="7" FieldName="Day7" VisibleIndex="7"></dx:GridViewDataCheckColumn>
                                                    <dx:GridViewDataCheckColumn Caption="8" FieldName="Day8" VisibleIndex="8"></dx:GridViewDataCheckColumn>
                                                    <dx:GridViewDataCheckColumn Caption="9" FieldName="Day9" VisibleIndex="9"></dx:GridViewDataCheckColumn>
                                                    <dx:GridViewDataCheckColumn Caption="10" FieldName="Day10" VisibleIndex="10"></dx:GridViewDataCheckColumn>
                                                    <dx:GridViewDataCheckColumn Caption="11" FieldName="Day11" VisibleIndex="11"></dx:GridViewDataCheckColumn>
                                                    <dx:GridViewDataCheckColumn Caption="12" FieldName="Day12" VisibleIndex="12"></dx:GridViewDataCheckColumn>
                                                    <dx:GridViewDataCheckColumn Caption="13" FieldName="Day13" VisibleIndex="13"></dx:GridViewDataCheckColumn>
                                                    <dx:GridViewDataCheckColumn Caption="14" FieldName="Day14" VisibleIndex="14"></dx:GridViewDataCheckColumn>
                                                    <dx:GridViewDataCheckColumn Caption="15" FieldName="Day15" VisibleIndex="15"></dx:GridViewDataCheckColumn>

                                                    <dx:GridViewDataTextColumn Caption="Days" FieldName="pDays" VisibleIndex="17"></dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="Rate" FieldName="Att_Rate" VisibleIndex="18">
                                                        <PropertiesTextEdit DisplayFormatString="c2" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="Check" FieldName="Att_AmountCheck" VisibleIndex="19">
                                                        <PropertiesTextEdit DisplayFormatString="c2" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="Cash" FieldName="Cash" VisibleIndex="20">
                                                        <PropertiesTextEdit DisplayFormatString="c2" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="Amount" FieldName="AmountTotal" VisibleIndex="21">
                                                        <PropertiesTextEdit DisplayFormatString="c2" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="Name" FieldName="Att_EmployeeName" VisibleIndex="22"></dx:GridViewDataTextColumn>
                                                </Columns>
                                                <SettingsPager Mode="ShowAllRecords" />
                                            </dx:ASPxGridView>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                            </Items>
                        </dx:LayoutGroup>
                    </Items>
                </dx:ASPxFormLayout>

                <dx:ASPxFormLayout runat="server" ID="ASPxFormLayout3" Theme="Metropolis">
                    <SettingsItemCaptions Location="Top"></SettingsItemCaptions>
                    <Items>
                        <dx:LayoutGroup ColCount="1" Caption="16-31">
                            <Items>
                                <dx:LayoutItem ShowCaption="False">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxButton ID="btnExport1631" runat="server" Text="Export" Theme="Metropolis" OnClick="btnExport1631_Click"></dx:ASPxButton>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                            </Items>
                            <Items>
                                <dx:LayoutItem ShowCaption="False">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxGridView ID="gridPayRoll1631" KeyFieldName="Att_AttendanceID" runat="server" AutoGenerateColumns="False">
                                                <Columns>
                                                    <dx:GridViewDataTextColumn FieldName="Att_AttendanceID" Visible="false"></dx:GridViewDataTextColumn>

                                                    <dx:GridViewDataCheckColumn Caption="16" FieldName="Day16" VisibleIndex="1"></dx:GridViewDataCheckColumn>
                                                    <dx:GridViewDataCheckColumn Caption="17" FieldName="Day17" VisibleIndex="2"></dx:GridViewDataCheckColumn>
                                                    <dx:GridViewDataCheckColumn Caption="18" FieldName="Day18" VisibleIndex="3"></dx:GridViewDataCheckColumn>
                                                    <dx:GridViewDataCheckColumn Caption="19" FieldName="Day19" VisibleIndex="4"></dx:GridViewDataCheckColumn>
                                                    <dx:GridViewDataCheckColumn Caption="20" FieldName="Day20" VisibleIndex="5"></dx:GridViewDataCheckColumn>
                                                    <dx:GridViewDataCheckColumn Caption="21" FieldName="Day21" VisibleIndex="6"></dx:GridViewDataCheckColumn>
                                                    <dx:GridViewDataCheckColumn Caption="22" FieldName="Day22" VisibleIndex="7"></dx:GridViewDataCheckColumn>
                                                    <dx:GridViewDataCheckColumn Caption="23" FieldName="Day23" VisibleIndex="8"></dx:GridViewDataCheckColumn>
                                                    <dx:GridViewDataCheckColumn Caption="24" FieldName="Day24" VisibleIndex="9"></dx:GridViewDataCheckColumn>
                                                    <dx:GridViewDataCheckColumn Caption="25" FieldName="Day25" VisibleIndex="10"></dx:GridViewDataCheckColumn>
                                                    <dx:GridViewDataCheckColumn Caption="26" FieldName="Day26" VisibleIndex="11"></dx:GridViewDataCheckColumn>
                                                    <dx:GridViewDataCheckColumn Caption="27" FieldName="Day27" VisibleIndex="12"></dx:GridViewDataCheckColumn>
                                                    <dx:GridViewDataCheckColumn Caption="28" FieldName="Day28" VisibleIndex="13"></dx:GridViewDataCheckColumn>
                                                    <dx:GridViewDataCheckColumn Caption="29" FieldName="Day29" VisibleIndex="14"></dx:GridViewDataCheckColumn>
                                                    <dx:GridViewDataCheckColumn Caption="30" FieldName="Day30" VisibleIndex="15"></dx:GridViewDataCheckColumn>
                                                    <dx:GridViewDataCheckColumn Caption="31" FieldName="Day31" VisibleIndex="16"></dx:GridViewDataCheckColumn>

                                                    <dx:GridViewDataTextColumn Caption="Days" FieldName="pDays" VisibleIndex="18"></dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="Rate" FieldName="Att_Rate" VisibleIndex="19">
                                                        <PropertiesTextEdit DisplayFormatString="c2" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="Check" FieldName="Att_AmountCheck" VisibleIndex="20">
                                                        <PropertiesTextEdit DisplayFormatString="c2" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="Cash" FieldName="Cash" VisibleIndex="21">
                                                        <PropertiesTextEdit DisplayFormatString="c2" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="Amount" FieldName="AmountTotal" VisibleIndex="22">
                                                        <PropertiesTextEdit DisplayFormatString="c2" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="Name" FieldName="Att_EmployeeName" VisibleIndex="23"></dx:GridViewDataTextColumn>
                                                </Columns>
                                                <SettingsPager Mode="ShowAllRecords" />
                                            </dx:ASPxGridView>
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

