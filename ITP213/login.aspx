<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="ITP213.login" %>

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
</head>
<body>
    <style>
        body {
            background-image: url("Images/trip_background.jpg");
            background-color: #2C65A8;
            background-size: 100%;
        }
    </style>
    <div class="container py-5">
        <div class="row">
            <div class="col-md-12">
                <h4 class="text-center text-white mb-4">NYP Login</h4>
                <div class="row">
                    <div class="col-md-4 mx-auto">
                        <!--Login form-->
                        <div class="card rounded-0">
                            <div class="card-header">
                                <h5 class="mb-0">
                                    <asp:Label ID="lblTitle" runat="server" Text="Login"></asp:Label>
                                </h5>
                            </div>

                            <div class="card-body">
                                <form id="form1" runat="server">
                                    <!--NormalLogin-->
                                    <asp:Panel ID="PanelPart1" runat="server" Visible="true">
                                        <div class="form-group">
                                            <asp:TextBox ID="tbEmail" runat="server" placeholder="Email" TextMode="Email" oninput="checkPassword();" AutoCompleteType="Disabled"></asp:TextBox>
                                        </div>
                                        <!--TextMode="Password"-->
                                        <div class="form-group">
                                            <asp:TextBox ID="tbPassword" runat="server" placeholder="Password" oninput="checkPassword();" AutoCompleteType="Disabled"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <!--<asp:CheckBox ID="cbRememberMe" lass="form-check-input" runat="server" text="Remember Me" Checked="True" />-->
                                            <asp:Label ID="lblForgetAccount" runat="server"><a href="#">Forget Account?</a></asp:Label>
                                        </div>
                                        <p>
                                            <asp:Label ID="lblCreateAccount" runat="server"><a href="register.aspx">Create Account</a></asp:Label>
                                            <asp:Button ID="btnLogin" class="btn btn-success float-right" runat="server" Text="Login" OnClick="btnCheckFor2FA_Click"/>
                                        </p>
                                        
                                    </asp:Panel>
                                    <!--//NormalLogin-->
                                    <!--2FA Choice-->
                                    <asp:Panel ID="PanelPart2" runat="server" Visible="false">
                                        <div class="form-group">
                                            <asp:RadioButtonList ID="RadioButtonList1" runat="server">
                                                <asp:ListItem>Google Authenticator</asp:ListItem>
                                                <asp:ListItem>One Time Password</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                        <div class="form-group">
                                            <asp:Button ID="btnBack1" class="btn btn-default float-left" runat="server" Text="Back" OnClick="btnBack1_Click"/>
                                            <asp:Button ID="btnSubmitChoice" class="btn btn-primary float-right" runat="server" Text="Next" OnClick="btnSubmitChoice_Click"/>
                                        </div>
                                        <br />
                                    </asp:Panel>
                                    <!--2FA Choice-->
                                    <!--2FA Password-->
                                    <asp:Panel ID="PanelPart3" runat="server" Visible="false">
                                        <div class="form-group">
                                            <asp:TextBox ID="tb2FAPin" runat="server" placeholder="Enter the password"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <asp:Button ID="btnBack2" class="btn btn-default float-left" runat="server" Text="Back" OnClick="btnBack2_Click" CausesValidation="false"/>
                                            <asp:Button ID="btnSubmitPassword" class="btn btn-success float-right" runat="server" Text="Submit" OnClick="btnLogin_Click" />
                                        </div>
                                    </asp:Panel>
                                    <!--2FA Password-->
                                    <p>
                                        <asp:Label ID="lblError" runat="server"></asp:Label>
                                    </p>
                                </form>
                                <style>
                                    #tbEmail, #tbPassword, #tb2FAPin {
                                        width: 100%;
                                        padding: 10px;
                                        box-sizing: border-box;
                                        background: none;
                                        outline: none;
                                        resize: none;
                                        border: 0;
                                        font-family: 'Montserrat',sans-serif;
                                        transition: all .3s;
                                        border-bottom: 2px solid #bebed2
                                    }

                                        #tbEmail:focus, #tbPassword:focus, #tb2FAPin:focus {
                                            border-bottom: 2px solid #78788c
                                        }
                                </style>
                            </div>
                        </div>
                        <!--//Login form-->
                    </div>
                </div>
            </div>
        </div>
        <style>
            .form-control {
                background: #f7f7f7 none repeat scroll 0 0;
                border: 1px solid #d4d4d4;
                border-radius: 4px;
                font-size: 14px;
                height: 50px;
                line-height: 50px;
            }
        </style>
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

    <script>
        function checkPassword() {
            var password = document.getElementById("tbPassword")
            var email = document.getElementById("tbEmail")
            if (password.value == "") {
                password.setCustomValidity("Field cannot be empty!");
            } else {
                password.setCustomValidity('');
            }

            if (email.value == "") {
                email.setCustomValidity("Field cannot be empty!");
            } else {
                email.setCustomValidity('');
            }
        }
    </script>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@7.32.4/dist/sweetalert2.all.min.js" type="text/javascript"></script>
    <script>
        function alertme() {
            Swal(
                'Good job!',
                'You clicked the button!',
                'success'
            )
        }
    </script>
</body>
</html>
