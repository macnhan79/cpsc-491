<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<!DOCTYPE html>

<html lang="en" class="gr__restaurant_sansoftwareindia_com">
<head>
    <title>Pho Ha 7</title>
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
    <meta name="keywords" content="aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa">
    <script type="application/x-javascript"> 
        addEventListener("load", function() { setTimeout(hideURLbar, 0); }, false);function hideURLbar(){ window.scrollTo(0,1); } 
    </script>

    <link rel="stylesheet" href="css/style_login.css" type="text/css" media="all">
    <link rel="stylesheet" href="css/font-awesome.css">

    <script type="text/javascript" src="js/jquery.js"></script>
    <script type="text/javascript" src="js/jquery-2.1.4.min.js"></script>

    <script>
        window.onload = function () {
            document.forms[0][0].focus();
        }

        $(document).ready(function () {
            document.getElementById('uname').focus();
        });
    </script>
    <script>
        function log() {
            var uname = document.form1.uname.value;
            var upass = document.form1.upass.value;
            if (uname == "") {
                alert("Enter the user name");
                document.form1.uname.focus();
            }
        }
    </script>
    <style>
        @media only screen and (min-device-width : 568px) and (max-device-width : 1024px) {
            /*.main-content-agile {
                margin-top: 30%;
            }*/
        }

        @media only screen and (min-device-width : 320px) and (max-device-width : 480px) {
            .main-content-agile {
                margin-top: 20%;
            }
        }
    </style>
    <link rel="stylesheet" href="css/softkeys-0.0.1.css">
    <script src="js/softkeys-0.0.1.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.softkeys').softkeys({
                target: $('.softkeys').data('target'),
                layout: [
                    [
                        ['1', '!'],
                        ['2', '@'],
                        ['3', '#'],
                    ],
                    [
                        ['4', '$'],
                        ['5', '%'],
                        ['6', '^'],
                    ],
                    [
                        ['7', '&amp;'],
                        ['8', '*'],
                        ['9', '('],
                    ],
                    [
                        ['0', ')'],
                        'delete'
                    ]
                ]
            });
        });

    </script>
</head>
<body data-gr-c-s-loaded="true">
    <dx:ASPxLabel ID="lblInfo" runat="server" Text=""></dx:ASPxLabel>
    <div class="w3-agile-banner">
        <div class="center-container">
            <!--header-->
            <!--//header-->
            <!--main-->

            <div class="main-content-agile" style="background: none;">
                <div class="wthree-pro">
                </div>
                <br>
                <!---728x90--->
                <div class="sub-main-w3" style="background: rgba(0, 0, 0, 0.6);">
                    <div class="header-w3l">
                        <h1 style="color: #FFF; font-size: 20px;">LOG <span>IN</span></h1>
                    </div>
                    <br>
                    <form id="form1" runat="server">
                        <dx:ASPxTextBox ID="uname" name="uname" ClientInstanceName="uname" ReadOnly="true" runat="server" placeholder="Passcode" EnableTheming="False" Native="True" Password="True"></dx:ASPxTextBox>
                        <%--<input type="text" name="uname" placeholder="User Name" id="uname" required="">--%>
                        <div class="clear"></div>
                        <asp:HiddenField ID="ipAddress" runat="server" />
                        <asp:HiddenField ID="machineName" runat="server" />
                        <asp:Button ID="Button1" runat="server" Text="Login" OnClick="Button1_Click" />
                        <br>
                    </form>
                </div>
                <div class="softkeys" data-target="input[name='uname']"></div>
                <br>
            </div>
        </div>
    </div>
</body>
</html>
