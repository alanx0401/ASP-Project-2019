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

    <script src="https://code.jquery.com/jquery-2.2.4.min.js" integrity="sha256-BbhdlvQf/xTY9gja0Dq3HiwQF8LaCRTXxZKRutelT44=" crossorigin="anonymous"></script>
    <script>
        $(document).ready(function () {
            $('#show_password').hover(function show() {
                //Change the attribute to text  
                $('#tbPassword').attr('type', 'text');
                $('.icon_password').removeClass('fa fa-eye-slash').addClass('fa fa-eye');
            },
            function () {
                //Change the attribute back to password  
                $('#tbPassword').attr('type', 'password');
                $('.icon_password').removeClass('fa fa-eye').addClass('fa fa-eye-slash');
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
            <div class="auto-style1">
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
                                            <asp:TextBox ID="tbEmail" runat="server" placeholder="Email" TextMode="Email" AutoCompleteType="Disabled"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RFVEmail" runat="server" ErrorMessage="Please enter your email." ControlToValidate="tbEmail" ForeColor="Red" Display="Dynamic">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="REVEmail" runat="server" ErrorMessage="Please enter a valid email format" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="tbEmail" Display="Dynamic">*</asp:RegularExpressionValidator>
                                        </div>
                                        <div class="form-group input-group">
                                            <asp:TextBox ID="tbPassword" runat="server" placeholder="Password" AutoCompleteType="Disabled" TextMode="Password"></asp:TextBox>
                                            <div class="input-group-append">  
                                                <button id="show_password" type="button" style="padding: 0; border: none; background: none;">  
                                                    <span class="fa fa-eye-slash icon_password"></span>  
                                                </button> 
                                            </div>
                                            <asp:RequiredFieldValidator ID="RFVPassword" runat="server" ErrorMessage="Please enter your password." ControlToValidate="tbPassword" ForeColor="Red" Display="Dynamic">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group">
                                            <div id="ReCaptchContainer" runat="server" visible="false"></div>
                                        </div>
                                        <div class="form-group">
                                            <!--<asp:CheckBox ID="cbRememberMe" lass="form-check-input" runat="server" text="Remember Me" Checked="True" />-->
                                            <asp:Label ID="lblForgetAccount" runat="server"><a href="#">Forget Account?</a></asp:Label>
                                        </div>
                                        
                                        <p>
                                            <asp:Label ID="lblCreateAccount" runat="server"><a href="register.aspx">Create Account</a></asp:Label>
                                            <asp:Button ID="btnLogin" class="btn btn-success float-right" runat="server" Text="Login" OnClick="btnPanel1_Click"/>
                                        </p>
                                        
                                    </asp:Panel>
                                    <!--//NormalLogin-->
                                    <!--2FA Choice-->
                                    <asp:Panel ID="PanelPart2" runat="server" Visible="false">
                                        <div class="form-group">
                                            <asp:RequiredFieldValidator ID="RRVrb2FATypes" runat="server" ErrorMessage="Please pick one." ControlToValidate="rb2FATypes" ForeColor="Red" Display="Dynamic">*</asp:RequiredFieldValidator>
                                            <asp:RadioButtonList ID="rb2FATypes" runat="server">
                                            </asp:RadioButtonList>
                                            
                                        </div>
                                        <p>
                                            <div class="form-group">
                                                <asp:Button ID="btnSubmitChoice" class="btn btn-primary float-right" runat="server" Text="Next" OnClick="btnPanel2_Click"/>
                                            </div>
                                        </p>
                                        <br />
                                    </asp:Panel>
                                    <!--2FA Choice-->
                                    <!--2FA Password-->
                                    <asp:Panel ID="PanelPart3" runat="server" Visible="false">
                                        <div class="form-group">
                                            <asp:TextBox ID="tb2FAPin" runat="server" placeholder="Enter the password"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RFVVerifyPassword" runat="server" ErrorMessage="Please enter your password" ControlToValidate="tb2FAPin" ForeColor="Red" Display="Dynamic">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="REVVerifyPassword" runat="server" ErrorMessage="Please enter the password in a correct format" ControlToValidate="tb2FAPin" Display="Dynamic" ForeColor="Red" ValidationExpression="^\d{6}$">*</asp:RegularExpressionValidator>
                                        </div>
                                        <p>
                                            <div class="form-group">
                                                <asp:Button ID="btnBack2" class="btn btn-default float-left" runat="server" Text="Back" OnClick="btnBack2_Click" CausesValidation="false"/>
                                                <asp:Button ID="btnSubmitPassword" class="btn btn-success float-right" runat="server" Text="Submit" OnClick="btnPanel3_Click" />
                                            </div>
                                        </p>
                                    </asp:Panel>
                                    <!--2FA Password-->
                                    <p>
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" />
                                        <p>
                                        <asp:Label ID="lblError" runat="server"></asp:Label>
                                    </p>
                                </form>
                                <style>
                                    #tbEmail, #tbPassword, #tb2FAPin {
                                        width: 87%;
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
            .auto-style1 {
                position: relative;
                width: 100%;
                min-height: 1px;
                -ms-flex: 0 0 100%;
                flex: 0 0 100%;
                max-width: 100%;
                left: 0px;
                top: 0px;
                padding-left: 15px;
                padding-right: 15px;
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
    <!--Refere reCaptcha API-->    
    <script src="https://www.google.com/recaptcha/api.js?onload=renderRecaptcha&render=explicit&fallback=true?" async defer></script>  
    <script type="text/javascript">  
    var your_site_key = '<%= ConfigurationManager.AppSettings["SiteKey"]%>';  
    var renderRecaptcha = function () {  
        grecaptcha.render('ReCaptchContainer', {
            'sitekey': your_site_key,
            theme: 'light', //light or dark    
            type: 'image',// image or audio    
            size: 'normal'//normal or compact    
        });  
    };
  
    </script>
</body>
</html>
