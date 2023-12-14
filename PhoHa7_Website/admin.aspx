<%@ Page Language="C#" AutoEventWireup="true" CodeFile="admin.aspx.cs" Inherits="admin1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width" />
    <link rel="stylesheet" type="text/css" media="only screen and (max-device-width: 480px)" href="small-device.css" />
    <style type="text/css">
    @media only screen and (max-device-width: 480px) {
		div#wrapper {
			width: 400px;
		}

		div#header {
			background-image: url(media-queries-phone.jpg);
			height: 93px;
			position: relative;
		}

		div#header h1 {
			font-size: 140%;
		}

		#content {
			float: none;
			width: 100%;
		}

		#navigation {
			float:none;
			width: auto;
		}
	}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Calendar ID="Calendar1" runat="server" OnSelectionChanged="Calendar1_SelectionChanged" ></asp:Calendar>
        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
        <br />
        <table style="width:100%;">
            <tr>
                <td class="auto-style1">Cash</td>
                <td class="auto-style2">
                    <asp:Label ID="lblCash" runat="server" Text="Label"></asp:Label>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">Credit</td>
                <td class="auto-style2">
                    <asp:Label ID="lblCredit" runat="server" Text="Label"></asp:Label>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">Total</td>
                <td class="auto-style2">
                    <asp:Label ID="lblTotal" runat="server" Text="Label"></asp:Label>
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:Label ID="lbTotal" runat="server" Text="Amount"></asp:Label>
        <br />
        <div style ="width:480px; overflow:auto;">
        <asp:GridView  ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="TicketID,SaleItemID" DataSourceID="SqlDataSource1" CellPadding="4" ForeColor="#333333" GridLines="None" OnSelectedIndexChanging="GridView1_SelectedIndexChanging">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:CommandField ShowSelectButton="True" >
                <ItemStyle Width="5%" />
                </asp:CommandField>
                <asp:BoundField DataField="TableName" HeaderText="Table" SortExpression="TableName" >
                <ItemStyle Width="7%" />
                </asp:BoundField>
                <asp:BoundField DataField="DTicketNum" HeaderText="DTicket" SortExpression="DTicketNum" >
                <ItemStyle Width="2%" />
                </asp:BoundField>
                <asp:BoundField DataField="Price" HeaderText="Price" SortExpression="Price" >
                <ItemStyle Width="5%" />
                </asp:BoundField>
                <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" >
                <ItemStyle Width="61%" />
                </asp:BoundField>
                <asp:BoundField DataField="DateTimeIssue" HeaderText="DateTimeIssue" SortExpression="DateTimeIssue" >
                <ItemStyle Width="16%" />
                </asp:BoundField>
                <asp:BoundField DataField="TicketID" HeaderText="TicketID" SortExpression="TicketID" >
                <ItemStyle Width="2%" />
                </asp:BoundField>
                <asp:BoundField DataField="SaleItemID" HeaderText="SaleItemID" SortExpression="SaleItemID" >
                <ItemStyle Width="2%" />
                </asp:BoundField>
            </Columns>
            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
            <SortedAscendingCellStyle BackColor="#FDF5AC" />
            <SortedAscendingHeaderStyle BackColor="#4D0000" />
            <SortedDescendingCellStyle BackColor="#FCF6C0" />
            <SortedDescendingHeaderStyle BackColor="#820000" />
        </asp:GridView>
            <//div>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PhoHa7 %>" SelectCommand="getSaleItemCreditByDate" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:Parameter Name="dateBegin" Type="DateTime" />
                <asp:Parameter Name="dateEnd" Type="DateTime" />
            </SelectParameters>
        </asp:SqlDataSource>
    
        <br />
    
    </div>
    </form>
</body>
</html>
