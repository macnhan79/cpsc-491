<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MyOrders.aspx.cs" Inherits="Table" %>

<%--<%@ Page Title="Table" Language="C#" MasterPageFile="~/MasterPage2.master" AutoEventWireup="true" CodeFile="Table.aspx.cs" Inherits="Table" %>--%>

<%@ Register Src="~/UserControl/TableList.ascx" TagPrefix="uc" TagName="TableList" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlace1" runat="Server">
    
</asp:Content>--%>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" class="gr__restaurant_sansoftwareindia_com">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
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
    <link href="https://fonts.googleapis.com/css?family=Open+Sans" rel="stylesheet" />
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
<body class="page-container-bg-solid page-md" style="background: #f6f6f6;" data-gr-c-s-loaded="true">
    <form id="form1" runat="server">
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
                                    UseSubmitBehavior="False" Text='HI <%=((System.Data.DataTable)Session["LoginEmp"]).Rows[0]["FullName"] %>' OnClick="btnLogout_Click" />
                            </div>
                        </a>
                    </div>
                    <div style="clear: both;"></div>
                    <div id="tabledata" class="section ui-resizable" style="border: none; margin-top: 0px;">
                        <div id="venue_table" style="background: #f6f6f6; margin-bottom: 5%;" class="col-md-12 col-sm-12 col-centered">

                            <dx:ASPxDataView CssClass="tablelist" runat="server" ID="DataView" ClientIDMode="AutoID" EnableTheming="false"
                                EnableDefaultAppearance="false" AllowPaging="false" Width="100%" ItemSpacing="0" ColumnCount="5" >
                                <ItemTemplate>
                                    <div id="Div1" class="col-sm-3 col-xs-6 col-centered" style="border-right: 1px solid #ddd; border-bottom: 1px solid #dddd;">
                                        <div class="row" style="margin-top: 5%; background: #FFF; margin-right: 10px; margin-left: 10px;">
                                            <div class="  col-sm-4 col-xs-5 col-lg-4 col-lg-offset-3  col-xs-offset-3">
                                                <div class="col-xs-12">
                                                    <%--2700ff--%>
                                                    <p style='background: <%#Convert.ToBoolean(Eval("TakeOut"))==true?"#2700ff":"#f39c11" %>; padding: 3px 58px 14px 16px; margin: 0px 0px 0px 0px; color: #FFF; text-align: center;' id="table_name">
                                                        <span style="line-height: 35px; font-size: 17px; text-align: center;"><%#Convert.ToBoolean(Eval("TakeOut"))==true?"":"Table" %> <%# (Eval("CustomerName")+string.Empty)==""?Eval("TableName"):Eval("CustomerName") %></span><br>
                                                    </p>
                                                </div>
                                            </div>
                                        </div>
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


                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div style="clear: left;"></div>
    </form>
</body>

</html>

