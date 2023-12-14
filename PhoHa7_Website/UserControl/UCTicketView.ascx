<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UCTicketView.ascx.cs" Inherits="UCTicketView" %>


<div id="nhanmac">
    <asp:Timer ID="Timer1" OnTick="Timer1_Tick" runat="server" Interval="10000"></asp:Timer>
    <%--<asp:Timer ID="Timer1" OnTick="Timer1_Tick" runat="server" Interval="10000"></asp:Timer>--%>
</div>
<asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
    </Triggers>
    <ContentTemplate>
        <%--<asp:Label ID="Label1" runat="server" Text="UpdatePanel1 not refreshed yet."></asp:Label>--%>
        <dx:ASPxDataView CssClass="tablelist" runat="server" ID="DataView" ClientIDMode="AutoID" EnableTheming="false"
            EnableDefaultAppearance="false" AllowPaging="false" Width="100%" ItemSpacing="0" ColumnCount="5" OnCustomCallback="DataView_CustomCallback">
            <ItemTemplate>
                <div id="Div1" class="col-sm-3 col-xs-6 col-centered" style="border-right: 1px solid #ddd; border-bottom: 1px solid #dddd;">
                    <div class="row" style="margin-top: 5%; background: #FFF; margin-right: 10px; margin-left: 10px;">
                        <div class="  col-sm-12 col-xs-5 col-lg-4 col-lg-offset-3  col-xs-offset-3">
                            <div class="col-xs-12">
                                <%--2700ff--%>
                                <p style='background: <%#Convert.ToBoolean(Eval("TakeOut"))==true?"#2700ff":"#f39c11" %>; padding: 5px 0px 15px 0px; <%--margin: 0px 0px 0px 0px;--%> color: #FFF; text-align: center;' id="table_name">
                                    <span style="line-height: 35px; font-size: 17px; text-align: center;font-weight:bold">
                                        <%--<%#Convert.ToBoolean(Eval("TakeOut"))==true?"":"Table" %>--%> 
                                        <%# Eval("TableName") +" "+Eval("CustomerName")%> 
                                        <br />
                                        <%#((Eval("Description")+string.Empty)==(Eval("TableID")+string.Empty))?"("+ (int)(DateTime.Now-Convert.ToDateTime(Eval("OnDate"))).TotalMinutes   +" Mins)":"(New)"%>
                                    </span><br>
                                </p>
                            </div>
                        </div>
                    </div>


                 <%--   <div class="row" style=" display: flex; justify-content: center; align-items: center; background: #FFF; margin-right: 10px; margin-left: 10px;">
                        <div class="col-sm-12 col-lg-12 col-xs-12" style="display: flex; justify-content: center; align-items: center; background: <%#((Eval("Description")+string.Empty)==(Eval("TableID")+string.Empty))?"#cc2d2d":"#2dcc70" %>;">
                            <dx:ASPxButton ID="ASPxButton2" CssClass="go-butInfor" EnableTheming="False" Native="True" runat="server" Text="ASPxButton"></dx:ASPxButton>
                        </div>
                    </div>--%>


                    <div class="row" style="margin-bottom: 6%; display: flex; justify-content: center; align-items: center; background: #FFF; margin-right: 10px; margin-left: 10px;">
                        <div class="col-sm-12 col-lg-12 col-xs-12" style="display: flex; justify-content: center; align-items: center; background: <%#((Eval("Description")+string.Empty)==(Eval("TableID")+string.Empty))?"#cc2d2d":"#2dcc70" %>;">
                            <dx:ASPxButton ID="ASPxButton1" runat="server" OnClick="ASPxButton1_Click" EnableTheming="False" Native="True"
                                CssClass='<%#((Eval("Description")+string.Empty)==(Eval("TableID")+string.Empty))?"go-butUpdate":"go-butNew" %>'
                                CommandArgument='<%#((Eval("Description")+string.Empty)==(Eval("TableID")+string.Empty))?"modify":"new" %>' CommandName='<%# Eval("TableID") %>'
                                Text='<%#((Eval("Description")+string.Empty)==(Eval("TableID")+string.Empty))?"UPDATE":"ORDER NOW" %>'>
                            </dx:ASPxButton>

                        </div>
                    </div>
                    
                </div>
            </ItemTemplate>
        </dx:ASPxDataView>
    </ContentTemplate>
</asp:UpdatePanel>


<asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:PhoHa7 %>"
    SelectCommand="SELECT t.[TableID], t.[CategoryID], t.[Active], t.[TableName], t.[TakeOut],p.tableid as pTable, p.DateTimeIssue FROM [Tables] as t 
left join ProcTickets as p on t.tableid=p.tableid 
WHERE [CategoryID] = @CategoryID and t.Active = 1 order by t.orderby asc"
    OnSelecting="SqlDataSource2_Selecting">
    <SelectParameters>
        <asp:Parameter Name="CategoryID" Type="Int32" />
    </SelectParameters>
</asp:SqlDataSource>
<script runat="server">
    
</script>
