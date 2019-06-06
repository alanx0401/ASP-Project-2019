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
        .auto-style4 {
            width: 662px;
            height: 66px;
            text-align: center;
        }
        .auto-style5 {
            height: 66px;
            width: 576px;
        }
        .auto-style6 {
            width: 662px;
            height: 86px;
            text-align: center;
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
        .auto-style10 {
            font-weight: normal;
        }
        .auto-style11 {
            width: 662px;
            text-align: center;
        }
        .auto-style12 {
            height: 66px;
            width: 576px;
            text-align: center;
        }
    </style>
    <form id="form1" runat="server">
        <div class="auto-style1">
            <h1 class="auto-style10">
                <asp:Label ID="Label1" runat="server" ForeColor="White" Text="Forget Password"></asp:Label>
            </h1>
        </div>
            <div>

                <table class="auto-style2" align="center">
                    <tr>
                        <td class="auto-style11">
                            <asp:Label ID="lbEmail" runat="server" Font-Size="X-Large" ForeColor="White" Text="Enter your Email"></asp:Label>
                        </td>
                        <td class="auto-style8">
                            <asp:TextBox ID="tbEmail" runat="server" Height="34px" Width="410px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style11">
                            <asp:Label ID="lbAdminno" runat="server" Font-Size="X-Large" ForeColor="White" Text="Enter your Admin No."></asp:Label>
                        </td>
                        <td class="auto-style8">
                            <asp:TextBox ID="tbAdminno" runat="server" Height="34px" Width="410px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style11">
                            <asp:Label ID="lbNewpassword" runat="server" Font-Size="X-Large" ForeColor="White" Text="Enter your new password"></asp:Label>
                        </td>
                        <td class="auto-style8">
                            <asp:TextBox ID="tbNewpassword" runat="server" Height="34px" Width="410px" TextMode="Password"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="lbConfirmpassword" runat="server" Font-Size="X-Large" ForeColor="White" Text="Enter confirmed password"></asp:Label>
                        </td>
                        <td class="auto-style7">
                            <asp:TextBox ID="tbConfirmpassword" runat="server" Height="34px" Width="410px" TextMode="Password"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style4">
                            <asp:Label ID="lbAdditional" runat="server" Font-Size="X-Large" ForeColor="White" Text="Enter any information you know about your account(optional)"></asp:Label>
                        </td>
                        <td class="auto-style5">
                            <asp:TextBox ID="tbAdditional" runat="server" Height="113px" Width="410px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>

                        </td>
                        <td class="auto-style12">
                            <asp:Button ID="btnReset" runat="server" Text="Reset Password" Font-Size="X-Large" Height="37px" Width="225px" />
                        </td>
                    </tr>
                </table>

            </div>
    </form>

</body>
</html>
