<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="ManageYourAccount.aspx.cs" Inherits="ITP213.ManageYourAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Breadcrumbs-->
    <ol class="breadcrumb">

        <li class="breadcrumb-item">

            <a href="#" style="color: #D6D6D6">Home</a>
        </li>
        <li class="breadcrumb-item active">Manage Your Account</li>
        <!--1. Change the name!-->
    </ol>
    <style>
        .breadcrumb {
            background-color: #FFFFFF !important;
        }

        .breadcrumb > .breadcrumb-item {
            color: #031A82 !important;
        }

        .breadcrumb .breadcrumb-item + .breadcrumb-item::before {
            color: #D6D6D6;
        }
    </style>
    <!-- //Breadcrumbs end-->

    <!-- Page Content -->
    <h1>Manage Your Account</h1>
    <!--2. Change the title!-->
    <hr />
    <p>
        <!--3. This is where you code all your features-->

        Phone Number:
        <asp:Label ID="lblPhoneNumber" runat="server" Text=""></asp:Label>
        (<asp:Label ID="lblVerifiedPhoneStatus" runat="server" Text=""></asp:Label>)
        &nbsp;[<asp:HyperLink ID="HyperLinkPhoneNum" runat="server">Change</asp:HyperLink>
        ]
    </p>
    <p>
        Email:
        <asp:Label ID="lblEmail" runat="server"></asp:Label>
        (<asp:Label ID="lblVerifiedEmailStatus" runat="server"></asp:Label>) [<asp:HyperLink ID="HyperLinkEmail" runat="server">Change</asp:HyperLink>
        ]
    </p>

    <asp:Panel ID="Panel1" runat="server" Visible="false">
        <p>
            One Time Password:
            <asp:Label ID="lblOTP" runat="server" Text=""></asp:Label>
            &nbsp;[<asp:Button ID="btnOTP" runat="server" Text="Disable" Style="padding: 0; border: none; background: none; color: #0000FF" OnClick="btnOTP_Click"/>
            ]
            
        </p>

    </asp:Panel>
    <asp:Panel ID="PanelCaptcha" runat="server" Visible="false">
        <div class="form-row">
            <div class="form-group">
                <div id="ReCaptchContainer"></div>
            </div>

        </div>
    </asp:Panel>
    Google Auth:
        <asp:Label ID="lblGoogleAuth" runat="server" Text=""></asp:Label>
    &nbsp;[<asp:Button ID="btnGoogleAuth" runat="server" Visible="true" Text="Enable" Style="padding: 0; border: none; background: none; color: #0000FF" OnClick="btnGoogleAuth_Click" />]<br />
    <asp:HyperLink ID="HyperLinkChangePassword" runat="server">Change Password</asp:HyperLink>
    <br />

    <p>
        <asp:Label ID="lblResult" runat="server" ForeColor="Green"></asp:Label>
    </p>
    <!--//Page Content-->
    <script src="https://www.google.com/recaptcha/api.js?onload=renderRecaptcha&render=explicit" async defer></script>
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
</asp:Content>
