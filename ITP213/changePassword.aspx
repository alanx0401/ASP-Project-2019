<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="changePassword.aspx.cs" Inherits="ITP213.changePassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <!--favicon-->
    <link rel="shortcut icon" type="image/png" href="Images/favicon.png" />

    <!--Bootstrap core CSS-->
    <link rel="stylesheet" href="Content/bootstrap.min.css" />

    <!--Font awesome(glyphicon)-->
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.5.0/css/all.css" integrity="sha384-B4dIYHKNBt8Bc12p+WXckhzcICo0wtJAoU8YZTY5qE0Id1GSseTk6S+L3BlXeVIU" crossorigin="anonymous" />

    <!--Custom styles for this template-->
    <link rel="stylesheet" href="Content/style.css" />

    <!--Date time picker-->
    <script src="https://code.jquery.com/jquery-2.2.4.min.js" integrity="sha256-BbhdlvQf/xTY9gja0Dq3HiwQF8LaCRTXxZKRutelT44=" crossorigin="anonymous"></script>
    <script src="Scripts/jquery-ui.js"></script>
    <script src='Scripts/jquery-ui-timepicker-addon.js'></script>
    <script src='Scripts/jquery-ui-timepicker-addon-i18n.js'></script>

    <link rel='stylesheet' href='Content/jquery-ui-timepicker-addon.min.css' />
    <!--Tab-->
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
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
                                    <asp:Label ID="Label1" runat="server" Text="Change Password"></asp:Label></h5>
                            </div>

                            <div class="card-body">
                                <form id="form1" runat="server">
                                    <asp:Panel ID="PanelPart1" runat="server">
                                        <div class="form-group">
                                            <!--TextMode="Password"-->
                                            <asp:TextBox ID="tbPassword" runat="server" TextMode="Password" placeholder="Password" oninput="checkPassword();"></asp:TextBox>
                                            <asp:Label ID="passstrength" runat="server" TextMode="Password"></asp:Label>
                                        </div>
                                        <div class="form-group">
                                            <asp:TextBox ID="tbConfirmPassword" runat="server" placeholder="Confirm Password"></asp:TextBox>
                                            <asp:Label ID="matchpassword" runat="server"></asp:Label>
                                        </div>
                                    </asp:Panel>

                                    <p>
                                        <asp:Button ID="btnChangePassword" class="btn btn-success float-right" runat="server" Text="Change Password" Visible="true" OnClick="btnChangePassword_Click"/>
                                    </p>
                                    <p>
                                        <asp:Label ID="lblError" runat="server"></asp:Label>
                                    </p>
                                </form>
                                <style>
                                    #tbPassword, #tbConfirmPassword {
                                        width: 100%;
                                        padding: 10px;
                                        box-sizing: border-box;
                                        background: none;
                                        outline: none;
                                        resize: none;
                                        border: 0;
                                        font-family: 'Montserrat',sans-serif;
                                        transition: all .3s;
                                        border-bottom: 2px solid #bebed2;
                                    }

                                    #tbPassword:focus, tbConfirmPassword:focus{
                                        border-bottom: 2px solid #78788c;
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
    <!--<script src="Scripts/jquery.min.js"></script>-->
    <!--Comment this bc it doesn't work well with datetime picker-->
    <script src="Scripts/bootstrap.bundle.min.js"></script>
    <!--//Bootstrap core Javascript-->
    <!--Core plugin Javascript-->
    <script src="Scripts/jquery.easing.min.js"></script>
    <!--//Core plugin Javascript-->
    <!--Custom scripts for all pages-->
    <script src="Scripts/script.min.js"></script>
    <!--//Custom scripts for all pages-->

    <script>
        $(document).ready(function () {
            $(function () {
                $("#tbDateOfBirth").datepicker({
                    maxDate: 0
                });
            });
        });
        function checkPassword() {
            var password = document.getElementById("tbPassword")
            var email = document.getElementById("tbEmail")
            var confirmPassword = document.getElementById("tbConfirmPassword")

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

            /*if (confirmPassword.value == "") {
                 confirmPassword.setCustomValidity("Field cannot be empty!");
             } else {
                 confirmPassword.setCustomValidity('');
             }*/
            /*var con = (document.getElementById("tbPassword").value != document.getElementById("tbConfirmPassword").value)
            if (con == true) {
                confirmPassword.setCustomValidity("Passwords do not match!");
            }
            if (con == false)
            {
                confirmPassword.setCustomValidity('');
            }*/
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
    <script>
        //temporary
        $(document).ready(function () {

            $('#tbPassword').keyup(function (e) {
                var strongRegex = new RegExp("^(?=.{8,})(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*\\W).*$", "g");
                var mediumRegex = new RegExp("^(?=.{7,})(((?=.*[A-Z])(?=.*[a-z]))|((?=.*[A-Z])(?=.*[0-9]))|((?=.*[a-z])(?=.*[0-9]))).*$", "g");
                var enoughRegex = new RegExp("(?=.{6,}).*", "g");
                if (false === enoughRegex.test($(this).val())) {
                    $('#passstrength').html('More Characters');
                } else if (strongRegex.test($(this).val())) {
                    $('#passstrength').className = 'ok';
                    $('#passstrength').html('Strong!');
                } else if (mediumRegex.test($(this).val())) {
                    $('#passstrength').className = 'alert';
                    $('#passstrength').html('Medium!');
                } else {
                    $('#passstrength').className = 'error';
                    $('#passstrength').html('Weak!');
                }
                return true;
            });
            /*$('#tbConfirmPassword').keyup(function (e) {
                var password = $('#tbPassword');
                var confirmPassword = $('#tbConfirmPassword');
                if (password.val() == confirmPassword.val()) {
                    $('#matchpassword').html('password match');
                }
                if (password.val() != confirmPassword.val()) {
                    $('#matchpassword').html('Password do not match');
                }
                
            });*/
        });
    </script>
</body>
</html>
