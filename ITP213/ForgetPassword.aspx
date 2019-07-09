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
            width: 78%;
            height: 413px;
            margin-right: 252px;
        }
        .auto-style4 {
            width: 662px;
            height: 66px;
            text-align: center;
        }
        .auto-style5 {
            height: 66px;
            width: 1301px;
        }
        .auto-style6 {
            width: 662px;
            height: 86px;
            text-align: center;
        }
        .auto-style7 {
            height: 86px;
            width: 1301px;
        }
        .auto-style8 {
            width: 1301px;
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
        .auto-style13 {
            width: 662px;
            height: 72px;
            text-align: center;
        }
        .auto-style14 {
            height: 72px;
            width: 1301px;
        }
        .auto-style15 {
            width: 1301px;
            height: 66px;
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
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorEmail" runat="server" ControlToValidate="tbEmail" ErrorMessage="Email is Required" ForeColor="#CC3300"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    
                    <tr>
                        <td class="auto-style13">
                            <asp:Label ID="lbNewpassword" runat="server" Font-Size="X-Large" ForeColor="White" Text="Enter your new password"></asp:Label>
                        </td>
                        <td class="auto-style14">
                            <asp:TextBox ID="tbNewpassword" runat="server" Height="34px" Width="410px" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorNewpw" runat="server" ControlToValidate="tbNewpassword" ErrorMessage="Password is required" ForeColor="#CC3300"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style6">
                            <asp:Label ID="lbConfirmpassword" runat="server" Font-Size="X-Large" ForeColor="White" Text="Enter confirmed password"></asp:Label>
                        </td>
                        <td class="auto-style7">
                            <asp:TextBox ID="tbConfirmpassword" runat="server" Height="34px" Width="410px" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorConfirmpw" runat="server" ControlToValidate="tbConfirmpassword" ErrorMessage="Enter password again" ForeColor="#CC3300"></asp:RequiredFieldValidator>
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
                        <td class="auto-style15">
                            <asp:Button ID="btnReset" runat="server" Text="Reset Password" Font-Size="X-Large" Height="37px" Width="225px" />
                        </td>
                    </tr>
                </table>

            </div>
    </form>
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
            var newPassword = document.getElementById("tbNewPassword")

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
