<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="GoogleAuth.aspx.cs" Inherits="ITP213.GoogleAuth" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--************README: Hi, please change some of the following things below when you're coding your features. Thanks! -PS -->

    <!-- Breadcrumbs-->
    <ol class="breadcrumb">

        <li class="breadcrumb-item">

            <a href="/Default.aspx" style="color: #D6D6D6">Home</a>
        </li>
        <li class="breadcrumb-item">

            <a href="/ManageYourAccount.aspx" style="color: #D6D6D6">Manage Your Account</a>
        </li>
        <li class="breadcrumb-item active">Google Auth</li>
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
    <h1>Google Auth</h1>
    <!--2. Change the title!-->
    <hr />
    <p>
        <!--3. This is where you code all your features-->

        <asp:Image ID="Image1" runat="server" />
    </p>
    <p>
        <asp:Label ID="Label1" runat="server"></asp:Label>
    </p>
    <hr />
    <div class="form-row">
        <div class="form-group col-md-3">
            Enter code:
            <asp:TextBox ID="TextBox1" runat="server" class="form-control"></asp:TextBox>
        </div>
    </div>
    <asp:Panel ID="PanelCaptcha" runat="server" Visible="false">
        <div class="form-row">
            <div class="form-group" runat="server">
                <div id="ReCaptchContainer"></div>
            </div>
        </div>
    </asp:Panel>
    <div class="form-row">
        <div class="form-group">
            <asp:Button ID="Button1" runat="server" Text="Confirm" OnClick="Button1_Click" class="btn btn-success" />
            
        </div>
    </div>
    <asp:Label ID="Label2" runat="server" ForeColor="Red"></asp:Label>
    <br />
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
