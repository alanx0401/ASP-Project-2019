<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangeEmail.aspx.cs" Inherits="ITP213.ChangeEmail" ValidateRequest="false"%>

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
    </style>
    <div class="container py-5">
        <div class="row">
            <div class="auto-style1">
                <h4 class="text-center text-white mb-4">Contact Us</h4>
                <div class="row">
                    <div class="col-md-4 mx-auto">
                        <!--Login form-->
                        <div class="card rounded-0">
                            <div class="card-header">
                                <h5 class="mb-0">
                                    <asp:Label ID="lblTitle" runat="server" Text="Incorrect Email Address"></asp:Label>
                                </h5>
                            </div>
                            <div class="card-body">
                                <form id="form1" runat="server">
                                    <!--NormalLogin-->
                                    <asp:Panel ID="PanelPart1" runat="server" Visible="true">
                                        <div class="form-group">
                                            <asp:TextBox ID="tbEmail" runat="server" placeholder="Your Email" TextMode="Email"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RFVEmail" runat="server" ControlToValidate="tbEmail" Display="Dynamic" ErrorMessage="Please enter your email." ForeColor="Red">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="REVEmail" runat="server" ControlToValidate="tbEmail" Display="Dynamic" ErrorMessage="Please enter a valid email format" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                                        </div>
                                        <div class="form-group">
                                            
                                            <asp:TextBox ID="tbMessage" runat="server" placeholder="Message" TextMode="Multiline" Rows="5"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RFVMessage" runat="server" ControlToValidate="tbMessage" Display="Dynamic" ErrorMessage="Please enter your message." ForeColor="Red">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group">
                                            <div id="ReCaptchContainer" runat="server" visible="false"></div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Button ID="btnLogin" class="btn btn-success" runat="server" Text="Submit" OnClick="btnPanel1_Click"/>
                                        </div>  
                                    </asp:Panel>
                                    <style>
                                        #tbEmail, #tbMessage{
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
                                        #tbEmail:focus, #tbMessage:focus {
                                            border-bottom: 2px solid #78788c;
                                        }
                                    </style>
                                    <div class="form-group">
                                        <p>
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" />
                                            <p>
                                            <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                                        </p>
                                    </div>
                                </form>
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
    var your_site_key = '<%= Environment.GetEnvironmentVariable("SiteKey")%>';  
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