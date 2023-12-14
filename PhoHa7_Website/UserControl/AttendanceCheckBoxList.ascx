<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AttendanceCheckBoxList.ascx.cs" Inherits="UserControl_AttendanceCheckBoxList" %>
<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxFormLayout" TagPrefix="dx" %>

<script type="text/javascript">
    function attendanceEmpChange(s, e) {
        var value = s.GetValue();
        var id = s.GetMainElement().className.split(' ')[1];
        ASPxCallback1.PerformCallback(value + "|" + id);
    }
</script>

<dx:ASPxFormLayout runat="server" ID="OptionsFormLayout123" Theme="Metropolis" ShowItemCaptionColon="False">
    <Items>
        <dx:LayoutItem Caption="Layout Item" >
            <LayoutItemNestedControlCollection>
                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                    <asp:ListView ID="ListView1" runat="server">
                        <ItemTemplate>
                            <div style="float: left; width: 20%; padding: 15px;">
                                <dx:ASPxCheckBox EnableTheming="True" ID="ASPxCheckBox1" CssClass='<%# Eval("Att_AttendanceID") %>' Text='<%# Eval("Att_EmployeeName") %>' runat="server" Theme="BlackGlass"
                                Value='<%# Eval("AttendanceValue").ToString() %>' AllowGrayed="True"
                                ValueChecked='1' ValueGrayed='0.5' ValueUnchecked='0' ValueType="System.String" EnableDefaultAppearance="False">
                                <ClientSideEvents ValueChanged='function(s, e) { attendanceEmpChange(s,e); }' />
                            </dx:ASPxCheckBox>
                            </div>
                            
                        </ItemTemplate>
                    </asp:ListView>

                  <%--  <dx:ASPxDataView ID="attendanceDataView" ClientInstanceName="attendanceDataView" EnableTheming="False"
                        Width="100%" runat="server" AllowPaging="False" ColumnCount="5">
                        <ItemTemplate>
                            
                        </ItemTemplate>
                        <ItemStyle BackColor="Transparent" Height="0px">
                            <Paddings Padding="2px" />
                            <Border BorderStyle="None" />
                        </ItemStyle>
                    </dx:ASPxDataView>--%>
                </dx:LayoutItemNestedControlContainer>
            </LayoutItemNestedControlCollection>
        </dx:LayoutItem>
        <dx:LayoutItem Caption="Layout Item">
            <LayoutItemNestedControlCollection>
                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                    <dx:ASPxLabel ID="lblTotal" runat="server" Text="Total: "></dx:ASPxLabel>
                </dx:LayoutItemNestedControlContainer>
            </LayoutItemNestedControlCollection>
        </dx:LayoutItem>
    </Items>
    <Items></Items>
</dx:ASPxFormLayout>

<dx:ASPxCallback ID="ASPxCallback1" ClientInstanceName="ASPxCallback1" runat="server" OnCallback="ASPxCallback1_Callback">
    <ClientSideEvents BeginCallback="function(s, e) {
	LoadingPanel1.Show();
}"
        EndCallback="function(s, e) {
	LoadingPanel1.Hide();
}" />
</dx:ASPxCallback>



