<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="changePassword1.aspx.cs" Inherits="ITP213.changePassword1" %>

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
    <script>
        $(document).ready(function () {
            $('#show_password').hover(function show() {
                //Change the attribute to text  
                $('#tbPassword1').attr('type', 'text');
                $('.icon_password').removeClass('fa fa-eye-slash').addClass('fa fa-eye');
            },
            function () {
                //Change the attribute back to password  
                $('#tbPassword1').attr('type', 'password');
                $('.icon_password').removeClass('fa fa-eye').addClass('fa fa-eye-slash');
            });
            $('#show_confirmPassword').hover(function show() {
                //Change the attribute to text  
                $('#tbConfirmPassword').attr('type', 'text');
                $('.icon_confirmPassword').removeClass('fa fa-eye-slash').addClass('fa fa-eye');
            },
            function () {
                //Change the attribute back to password  
                $('#tbConfirmPassword').attr('type', 'password');
                $('.icon_confirmPassword').removeClass('fa fa-eye').addClass('fa fa-eye-slash');
            });
            //show_confirmPassword
            $('#show_currentPassword').hover(function show() {
                //Change the attribute to text  
                $('#tbCurrentPassword').attr('type', 'text');
                $('.icon_currentPassword').removeClass('fa fa-eye-slash').addClass('fa fa-eye');
            },
            function () {
                //Change the attribute back to password  
                $('#tbCurrentPassword').attr('type', 'password');
                $('.icon_currentPassword').removeClass('fa fa-eye').addClass('fa fa-eye-slash');
            });
        });
    </script>
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
                                        <div class="form-group input-group" style="left: 0px; top: 0px">
                                            <!--<asp:TextBox ID="tbCurrentPassword" runat="server" TextMode="Password" placeholder="Current Password"></asp:TextBox>--->
                                            <div class="input-group-append">  
                                                
                                            </div>
                                        </div>
                                        <div class="form-group input-group">
                                            <asp:TextBox ID="tbPassword1" runat="server" placeholder="Password" TextMode="Password"></asp:TextBox>
                                            <div class="input-group-append">  
                                                <button id="show_password" type="button" style="padding: 0; border: none; background: none;">  
                                                    <span class="fa fa-eye-slash icon_password"></span>  
                                                </button> 
                                            </div>
                                            <div id="pswd_info">
                                                <strong>Your password must:</strong>
                                                <ul style="list-style-type:none;padding:0;margin:0;">
                                                    <li id="length" class="invalid">Be at least <strong>8 characters</strong></li>
                                                    <li id="letter" class="invalid">At least <strong>one small-case letter</strong></li>
                                                    <li id="capital" class="invalid">At least <strong>one capital letter</strong></li>
                                                    <li id="number" class="invalid">At least <strong>one number</strong></li>
                                                    <li id="symbol" class="invalid">At least <strong>1 <a href="#" data-toggle="tooltip" data-placement="top" title="For example: @%+\\\/'!#$^?:.(){}\[\]~\-_.">symbol</a></strong></li>
                                                </ul>
                                            </div>
                                            <asp:RequiredFieldValidator ID="RFVPassword" runat="server" ControlToValidate="tbPassword1" Display="Dynamic" ErrorMessage="Please enter your password." ForeColor="Red">*</asp:RequiredFieldValidator>
                                            <asp:CustomValidator ID="CVPassword1" runat="server" ErrorMessage="Password cannot contain your name or admin number." ControlToValidate="tbPassword1" ForeColor="Red" Display="Dynamic" OnServerValidate="CVPassword1_ServerValidate">*</asp:CustomValidator>
                                            <asp:CustomValidator ID="CVPassword2" runat="server" ErrorMessage="Password is too common" ForeColor="Red" ControlToValidate="tbPassword1" Display="Dynamic" OnServerValidate="CVPassword1_ServerValidate" >*</asp:CustomValidator>
                                            <asp:RegularExpressionValidator ID="REVPassword" runat="server" ErrorMessage="Password does not meet the requirements." ControlToValidate="tbPassword1" Display="Dynamic" ForeColor="Red" ValidationExpression="(?=^.{8,100}$)(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@%+\\\/'!#$^?:.(){}\[\]~\-_.])(?!.*\s).*$">*</asp:RegularExpressionValidator>
                                            <style>
                                                #pswd_info {
                                                    position: absolute;
                                                    bottom: -200px;
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
                                                    z-index: 100;
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

                                            <asp:CompareValidator ID="CompareValidatorCurrentPassword0" runat="server" ControlToCompare="tbCurrentPassword" ControlToValidate="tbPassword1" Display="Dynamic" ErrorMessage="Cannot use the same password as current password!" ForeColor="Red" Operator="NotEqual">*</asp:CompareValidator>

                                        </div>
                                        <div class="form-group input-group" style="left: 0px; top: 0px">
                                            <asp:TextBox ID="tbConfirmPassword" runat="server" TextMode="Password" placeholder="Confirm Password"></asp:TextBox>
                                            <div class="input-group-append">  
                                                <button id="show_confirmPassword" type="button" style="padding: 0; border: none; background: none;">  
                                                    <span class="fa fa-eye-slash icon_confirmPassword"></span>  
                                                </button> 
                                                <asp:RequiredFieldValidator ID="RFVConfirmPassword" runat="server" ControlToValidate="tbConfirmPassword" Display="Dynamic" ErrorMessage="Please enter the same password." ForeColor="Red">*</asp:RequiredFieldValidator>
                                                <asp:CompareValidator ID="CVConfirmPassword" runat="server" ErrorMessage="Password does not match!" ControlToCompare="tbPassword1" ControlToValidate="tbConfirmPassword" ForeColor="Red" Display="Dynamic">*</asp:CompareValidator>
                                            </div>
                                        </div>
                                    </asp:Panel>

                                    <p>
                                        <asp:Button ID="btnChangePassword" class="btn btn-success float-right" runat="server" Text="Change Password" Visible="true" OnClick="btnChangePassword_Click"/>
                                    </p>
                                    <br />
                                    <p>
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" />
                                    <p>
                                        <asp:Label ID="lblError" runat="server"></asp:Label>
                                    </p>

                                </form>
                                <style>
                                    #tbPassword1, #tbConfirmPassword {
                                        width: 87%;
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

                                    #tbPassword1:focus, tbConfirmPassword:focus, #tbConfirmPassword:focus{
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

            //code here
            $('#tbPassword1').keyup(function () {
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
                if (pswd.match(/[a-z]/)) {
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
