<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="ManageYourAccount.aspx.cs" Inherits="ITP213.ManageYourAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--************README: Hi, please change some of the following things below when you're coding your features. Thanks! -PS -->

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

    <asp:Panel ID="Panel1" runat="server" Visible="false">
        <p>
            One Time Password:
            <asp:Label ID="lblOTP" runat="server" Text=""></asp:Label>
            &nbsp;[<asp:LinkButton ID="LinkButtonOTP" runat="server" OnClick="LinkButtonOTP_Click">Enable</asp:LinkButton>
            ]</p>
        <p>
    </asp:Panel>
        Google Auth:
        <asp:Label ID="lblGoogleAuth" runat="server" Text=""></asp:Label>
        &nbsp;[<asp:HyperLink ID="HyperLinkGoogleAuth" runat="server">Enable</asp:HyperLink>
        ]<br />
    <asp:HyperLink ID="HyperLinkChangePassword" runat="server">Change Password</asp:HyperLink>
    </p>
    <p>
        <asp:Label ID="lblResult" runat="server"></asp:Label>
    </p>
    <!--//Page Content-->
</asp:Content>
