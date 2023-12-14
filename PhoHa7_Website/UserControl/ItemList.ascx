<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ItemList.ascx.cs" Inherits="UserControl_ItemList" %>
<script type="text/javascript">
    // <![CDATA[
    function updateAddExtra(productID) {
        //grid.GetSelectedFieldValues('SaleItemID', OnGetSelectedFieldValues);
        dataView.PerformCallback(productID);
    }

    function OnGetSelectedFieldValues(selectedValues) {
        return selectedValues;
    }

    function OnEndCallbackDataView(s, e) {
        if (dataView.cpRefeshGrid != "") {
            //confirm("Press a button!");
            grid.PerformCallback(dataView.cpRefeshGrid);
            //dataView.cpmsg="";
        }
        //if (dataView.cpRefeshListBox == "cpRefeshListBox") {
        //    listExtraName.PerformCallback('1');
        //    //dataView.cpRefeshListBox = "";
        //}


        //var formats = [];
        //formats = dataView.cpSubmitAddExtra.split('|');
        //if (formats.length == 2) {
        //    if (formats[0].trim()=="cpSubmitAddExtra") {
        //        grid.PerformCallback(dataView.cpSubmitAddExtra);
        //    }
        //}//DataSourceID="SqlDataSource2"
    }
</script>
<% 
    PhoMac.Model.Employee emp = (PhoMac.Model.Employee)Session["LoginEmp"];
    if (emp == null)
    {
        Response.Redirect("Login.aspx");
    }
%>

<div class="itemlist" style="overflow-y: auto; height: 90%; position: absolute; width: 99.6%;">
    <dx:ASPxDataView runat="server" ID="dataView" EnableTheming="False" AllowPaging="False"
        Width="100%" ItemSpacing="2px" ColumnCount="4" OnCustomCallback="DataView_CustomCallback" OnInit="DataView_Init" ClientInstanceName="dataView">
        <ClientSideEvents EndCallback="function(s, e) {OnEndCallbackDataView(s,e);}" />
        <ItemTemplate>
            <%--   --%>
            <div class="food os-animation animated fadeInDown" style="list-style: none; cursor: pointer;max-width:130px">
                <img style="width: 100%;" src='<%#Eval("ProductImage").ToString()==""?"image_products\\none.jpg":Eval("ProductImage") %>' onclick='updateAddExtra("<%# Eval("BarCode") %>")' />

                <input style="width: 100%" id="Button1" type="button" class="order" maxlength="10" value="<%# ((PhoMac.Model.Employee)Session["LoginEmp"]) != null ? (((PhoMac.Model.Employee)Session["LoginEmp"]).Language == 0 ? Eval("KitchenName") : Eval("ProductName")): Eval("KitchenName") %> " onclick='updateAddExtra("<%# Eval("BarCode") %>    ")' />
            </div>
        </ItemTemplate>
        <ContentStyle>
            <Border BorderColor="Red" />
        </ContentStyle>
        <%--BackColor="Red"--%>
        <ItemStyle Height="0px" Width="25%" BackColor="White">
            <Paddings Padding="2px" />
            <Border BorderStyle="None" />
        </ItemStyle>
    </dx:ASPxDataView>
</div>


<%--<dx:ASPxPopupControl ID="pcOption" ClientInstanceName="pcOption" runat="server" CloseAction="CloseButton" Modal="True"
    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" 
    HeaderText="Update Item" AllowDragging="True" PopupAnimationType="None" EnableViewState="False"
    MaxHeight="500px" MaxWidth="800px" MinHeight="500px" MinWidth="800px" ShowCloseButton="False"
     LoadContentViaCallback="OnPageLoad" ShowFooter="True">
    <%--OnWindowCallback="pcLogin_WindowCallback"--%>
<%--  <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
            <dx:ASPxPanel ID="Panel1" runat="server">
                <PanelCollection>
                    <dx:PanelContent ID="PanelContent1" runat="server">

                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxPanel>
        </dx:PopupControlContentControl>
    </ContentCollection>
    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
    <FooterTemplate>
        <div style="margin: 3px;">
            <dx:ASPxButton ID="btOK" runat="server" Text="Update" AutoPostBack="False"
                ClientSideEvents-Click="function(s, e) {  }" EnableTheming="False" Native="True" CssClass="softkeys__btnExtraOK" />
            <dx:ASPxButton ID="btCancel" runat="server" Text="Cancel" AutoPostBack="False"
                ClientSideEvents-Click="function(s, e) {  }" Native="True" EnableTheming="False" CssClass="softkeys__btnExtraCancel" />
        </div>
    </FooterTemplate>
    <ContentStyle>
        <Paddings PaddingBottom="5px" />
    </ContentStyle>
</dx:ASPxPopupControl>--%>


<asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:PhoHa7 %>"
    SelectCommand="SELECT [ProductID], [ProductName], [CategoryID], [Active], [Price], [MType], [CPrice],[KitchenName],[BarCode],[ProductImage] FROM [Products] where CategoryID = @CategoryID and MType = @MType order by orderby" OnSelecting="SqlDataSource2_Selecting">
    <SelectParameters>
        <asp:Parameter Name="CategoryID" Type="Int32" />
        <asp:Parameter Name="MType" Type="Int32" />
    </SelectParameters>
</asp:SqlDataSource>
