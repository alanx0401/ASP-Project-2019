<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConfirmEmail.aspx.cs" Inherits="ITP213.ConfirmEmail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <!--favicon-->
    <link rel="shortcut icon" type="image/png" href="Images/favicon.png" />

    <!--Bootstrap core CSS-->
    <link rel="stylesheet" href="Content/bootstrap.min.css" />

    <!--Font awesome(glyphicon)-->
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.5.0/css/all.css" integrity="sha384-B4dIYHKNBt8Bc12p+WXckhzcICo0wtJAoU8YZTY5qE0Id1GSseTk6S+L3BlXeVIU" crossorigin="anonymous" />

    <!--Custom styles for this template-->
    <link rel="stylesheet" href="Content/style.css" />

    <script src="https://code.jquery.com/jquery-2.2.4.min.js" integrity="sha256-BbhdlvQf/xTY9gja0Dq3HiwQF8LaCRTXxZKRutelT44=" crossorigin="anonymous"></script>

</head>
<body>
    <style>
        body {
            background-image: url("Images/trip_background.jpg");
            background-color: #2C65A8;
            background-size: 100%;
        }
        /*.jumbotron{
            background: url("Images/trip_background.jpg") no-repeat center center; 
            -webkit-background-size: 100% 100%;
            -moz-background-size: 100% 100%;
            -o-background-size: 100% 100%;
            background-size: 100% 100%;
        }*/
    </style>
    <div class="jumbotron text-center">
      <h1>Verification Link</h1>
      <p>
        <form id="form1" runat="server">
            <div class="text-center">
                <asp:Label ID="lblResult" runat="server" CssClass="text-center mb-4"></asp:Label>
            </div>
        </form>
      </p> 
    </div>
    
    <!--Bootstrap core Javascript-->
    <script src="Scripts/jquery.min.js"></script>
    <script src="Scripts/bootstrap.bundle.min.js"></script>
    <!--//Bootstrap core Javascript-->
    <!--Core plugin Javascript-->
    <script src="Scripts/jquery.easing.min.js"></script>
    <!--//Core plugin Javascript-->
    <!--Custom scripts for all pages-->
    <script src="Scripts/script.min.js"></script>
    <!--//Custom scripts for all pages-->
</body>
</html>
