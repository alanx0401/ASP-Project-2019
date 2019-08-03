<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgetPasswordWindow.aspx.cs" Inherits="ITP213.ForgetPasswordWindow" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <center>
                Enter your Email : <asp:TextBox ID="tbEmail" runat="server"></asp:TextBox>
                <asp:Button ID="btnResetpw" runat="server" Text="Button"></asp:Button>
                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>

            </center>
        </div>
    </form>
</body>
</html>
