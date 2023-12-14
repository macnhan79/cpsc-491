<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login1.aspx.cs" Inherits="login2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        User:
        <asp:TextBox ID="txtUsername" runat="server" Width="304px"></asp:TextBox>
        <br />
    
        <asp:Label ID="Label1" runat="server" Text="Password: "></asp:Label>
        <asp:TextBox ID="TextBox1" runat="server" TextMode="Password" Width="275px"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Login" />
    
    </div>
    </form>
</body>
</html>
