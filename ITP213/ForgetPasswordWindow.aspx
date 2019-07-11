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
                Enter your Email:<asp:TextBox ID="tbEmail" runat="server"></asp:TextBox><br />  
                <asp:Button ID="btnReset" runat="server" Text="Get your password"></asp:Button>
                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
            </center>
        </div>
    </form>
</body>
</html>
