<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="register.aspx.cs" Inherits="ITP213.register" %>

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
                <h4 class="text-center text-white mb-4">NYP Register</h4>
                <div class="row">
                    <div class="col-md-4 mx-auto">
                        <!--Login form-->
                        <div class="card rounded-0">
                            <div class="card-header">
                                <h5 class="mb-0">
                                    <asp:Label ID="Label1" runat="server" Text="Register"></asp:Label></h5>
                            </div>

                            <div class="card-body">
                                <form id="form1" runat="server">
                                    <asp:Panel ID="PanelPart1" runat="server">
                                        <div class="form-group">
                                            <asp:TextBox ID="tbName" runat="server" placeholder="Name"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <asp:TextBox ID="tbAdminNo" runat="server" placeholder="Admin No"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <asp:TextBox ID="tbEmail" runat="server" placeholder="Email" TextMode="Email" oninput="checkPassword();"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <asp:TextBox ID="tbPassword" runat="server" placeholder="Password" TextMode="Password" oninput="checkPassword();"></asp:TextBox>
                                            <asp:Label ID="passstrength" runat="server"></asp:Label>
                                            <div id="pswd_info">
                                                <strong>Your password must:</strong>
                                                <ul style="list-style-type:none;padding:0;margin:0;">
                                                    <li id="length" class="invalid">Be at least <strong>8 characters</strong></li>
                                                    <li id="letter" class="invalid">At least <strong>one letter</strong></li>
                                                    <li id="capital" class="invalid">At least <strong>one capital letter</strong></li>
                                                    <li id="number" class="invalid">At least <strong>one number</strong></li>
                                                    <li id="symbol" class="invalid">At least <strong>1 <a href="#" data-toggle="tooltip" data-placement="top" title="For example: @%+\\\/'!#$^?:.(){}\[\]~\-_.">symbol</a></strong></li>
                                                </ul>
                                            </div>
                                            <style>
                                                #pswd_info {
                                                    position: absolute;
                                                    bottom: -50px;
                                                    bottom: -115px\9; /* IE Specific */
                                                    right: 55px;
                                                    width: 250px;
                                                    padding: 15px;
                                                    background: #fefefe;
                                                    font-size: .875em;
                                                    border-radius: 5px;
                                                    box-shadow: 0 1px 3px #ccc;
                                                    border: 1px solid #ddd;
                                                    margin-left: auto;
                                                    margin-right: auto;
                                                }

                                                #pswd_info strong {
                                                    margin: 0 0 10px 0;
                                                    padding: 0;
                                                    font-weight: normal;
                                                }

                                                #pswd_info::before {
                                                    content: "\25B2";
                                                    position: absolute;
                                                    top: -12px;
                                                    left: 45%;
                                                    font-size: 14px;
                                                    line-height: 14px;
                                                    color: #ddd;
                                                    text-shadow: none;
                                                    display: block;
                                                }

                                                .invalid {
                                                    background: url(images/unchecked.png) no-repeat 0 50%;
                                                    padding-left: 22px;
                                                    line-height: 24px;
                                                    color: #ec3f41;
                                                }

                                                .valid {
                                                    background: url(images/check.png) no-repeat 0 50%;
                                                    padding-left: 22px;
                                                    line-height: 24px;
                                                    color: #B1DD3A;
                                                }

                                                #pswd_info {
                                                    display:none;
                                                }
                                            </style>
                                        </div>
                                        <div class="form-group">
                                            <asp:TextBox ID="tbConfirmPassword" runat="server" TextMode="Password" placeholder="Confirm Password"></asp:TextBox>
                                            <asp:Label ID="matchpassword" runat="server"></asp:Label>
                                        </div>
                                    </asp:Panel>
                                    <asp:Panel ID="PanelPart2" runat="server" Visible="false">
                                        <div class="form-group">
                                            <asp:TextBox ID="tbDateOfBirth" runat="server" class="form-control" TextMode="DateTime" ClientIDMode="Static" placeholder="Date of birth"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <asp:TextBox ID="tbContactNumber" runat="server" placeholder="Contact Number"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <asp:CheckBox ID="cbReadAgreement" lass="form-check-input" runat="server" Text="I have read the agreement." />
                                        </div>
                                    </asp:Panel>
                                    <asp:Panel ID="PanelPart3" runat="server" Visible="false">
                                        <div class="form-group">
                                            <asp:TextBox ID="tbVerifyPassword" runat="server" placeholder="Enter code"></asp:TextBox>
                                        </div>
                                    </asp:Panel>

                                    <p>
                                        <asp:Label ID="lblLogin" runat="server"><a href="/login.aspx">Sign in instead.</a></asp:Label>
                                        <asp:Button ID="btnNext" class="btn btn-primary float-right" runat="server" Text="Next" OnClick="btnNext_Click" CausesValidation="False" />
                                        <asp:Button ID="btnNext1" class="btn btn-primary float-right" runat="server" Text="Next" OnClick="btnNext1_Click" Visible="false" CausesValidation="False" />
                                        <asp:Button ID="btnBack1" class="btn btn-default float-left" runat="server" Text="Back" Visible="false" OnClick="btnBack1_Click" />
                                        <asp:Button ID="btnRegister" class="btn btn-success float-right" runat="server" Text="Register" Visible="false" OnClick="btnRegister_Click" />
                                    </p>
                                    <p>
                                        <asp:Label ID="lblError" runat="server"></asp:Label>
                                        <asp:Label ID="lblError0" runat="server"></asp:Label>
                                    </p>
                                </form>
                                <style>
                                    #tbEmail, #tbPassword, #tbName, #tbContactNumber, #tbConfirmPassword, #tbDateOfBirth, #tbAdminNo {
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

                                        #tbEmail:focus, #tbPassword:focus, #tbName:focus, #tbContactNumber:focus, tbConfirmPassword:focus, tbDateOfBirth:focus, tbAdminNo:focus {
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
        /*$(document).ready(function () {

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
            });*/

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

        //});
        $(document).ready(function () {

            //code here
            $('#tbPassword').keyup(function () {
                // keyup code here
                // set password variable
                var pswd = $(this).val();

                //validate the length
                if (pswd.length < 8) {
                    $('#length').removeClass('valid').addClass('invalid');
                } else {
                    $('#length').removeClass('invalid').addClass('valid');
                }

                //validate letter
                if (pswd.match(/[A-z]/)) {
                    $('#letter').removeClass('invalid').addClass('valid');
                } else {
                    $('#letter').removeClass('valid').addClass('invalid');
                }

                //validate capital letter
                if (pswd.match(/[A-Z]/)) {
                    $('#capital').removeClass('invalid').addClass('valid');
                } else {
                    $('#capital').removeClass('valid').addClass('invalid');
                }

                //validate number
                if (pswd.match(/\d/)) {
                    $('#number').removeClass('invalid').addClass('valid');
                } else {
                    $('#number').removeClass('valid').addClass('invalid');
                }

                //validate symbols
                if (pswd.match(/[@%+\\\/'!#$^?:.(){}\[\]~\-_.]/)) {
                    $('#symbol').removeClass('invalid').addClass('valid');
                } else {
                    $('#symbol').removeClass('valid').addClass('invalid');
                }
            }).focus(function () {
                $('#pswd_info').show();
            }).blur(function () {
                $('#pswd_info').hide();
            });


        });

    </script>
    <script>
    $(document).ready(function(){
      $('[data-toggle="tooltip"]').tooltip();   
    });
    </script>
</body>
</html>
