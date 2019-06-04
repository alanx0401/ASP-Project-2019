<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgetPassword.aspx.cs" Inherits="ITP213.ForgetPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            text-align: center;
        }
        .auto-style2 {
            width: 73%;
            height: 413px;
        }
        .auto-style3 {
            width: 662px;
        }
        .auto-style4 {
            width: 662px;
            height: 66px;
        }
        .auto-style5 {
            height: 66px;
            width: 576px;
        }
        .auto-style6 {
            width: 662px;
            height: 86px;
        }
        .auto-style7 {
            height: 86px;
            width: 576px;
        }
        .auto-style8 {
            width: 576px;
        }
    </style>
</head>
<body>
    <style>
        body {
         background-image: url("Images/trip_background.jpg");
         background-color: #2C65A8;
         background-size: 100%;
        }
    </style>
    <form id="form1" runat="server">
        <div class="auto-style1">
            <h1><strong>Forget Password</strong></h1>
        </div>
            <div>

                <table class="auto-style2" align="center">
                    <tr>
                        <td class="auto-style3">Enter your Email</td>
                        <td class="auto-style8">
                            <asp:TextBox ID="TextBox1" runat="server" Height="34px" Width="410px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style3">Enter your Admin No.</td>
                        <td class="auto-style8">
                            <asp:TextBox ID="TextBox2" runat="server" Height="34px" Width="410px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style3">Enter your new password</td>
                        <td class="auto-style8">
                            <asp:TextBox ID="TextBox3" runat="server" Height="34px" Width="410px" TextMode="Password"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style6">Confirm password</td>
                        <td class="auto-style7">
                            <asp:TextBox ID="TextBox4" runat="server" Height="34px" Width="410px" TextMode="Password"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style4">Enter any information you know about your account(optional)</td>
                        <td class="auto-style5">
                            <asp:TextBox ID="TextBox5" runat="server" Height="113px" Width="410px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>

                        </td>
                        <td class="auto-style5">
                            <asp:Button ID="Button1" runat="server" Text="Reset Password" />
                        </td>
                    </tr>
                </table>

            </div>
    </form>

</body>
</html>
