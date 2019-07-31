<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="EditEmail.aspx.cs" Inherits="ITP213.EditEmail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--************README: Hi, please change some of the following things below when you're coding your features. Thanks! -PS -->

    <!-- Breadcrumbs-->
    <ol class="breadcrumb">

        <li class="breadcrumb-item">

            <a href="#" style="color:#D6D6D6">Home</a>
        </li>
        <li class="breadcrumb-item">

            <a href="/ManageYourAccount.aspx" style="color:#D6D6D6">Manage Your Account</a>
        </li>
        <li class="breadcrumb-item active">Edit Email</li> <!--1. Change the name!-->
    </ol>
    <style>
        .breadcrumb
        {
        background-color: #FFFFFF !important;
            
        }
        .breadcrumb > .breadcrumb-item
        {
        color: #031A82 !important;
        }
        .breadcrumb .breadcrumb-item+.breadcrumb-item::before{
            color: #D6D6D6;
        }
    </style>
    <!-- //Breadcrumbs end-->

    <!-- Page Content -->
    <h1>Edit Email</h1> <!--2. Change the title!-->
    <hr/>
    <p> <!--3. This is where you code all your features-->
        <div class="form-row">
            <div class="form-group col-md-4">
                Email:
                <asp:TextBox ID="tbEmail" runat="server" class="form-control"></asp:TextBox>
                
                <asp:button runat="server" text="Resend Verification" ID="btnResendEmailVerification" class="btn btn-light" OnClick="btnResendEmailVerification_Click"/>
            </div>
            <div class="form-group col-md-4">
                <asp:Label ID="lblPasswordEmail" runat="server" Text="Enter your password:" Visible="false"></asp:Label>
                <asp:TextBox ID="tbPasswordEmail" runat="server" class="form-control" Visible="false" TextMode="Password"></asp:TextBox>
            </div>
        </div>
    </p>
    <asp:Button ID="btnConfirmUpdate" runat="server" Text="Update Information" OnClick="btnConfirmUpdate_Click" class="btn btn-success"/>
    </p>
    <p> 
        <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
    </p>
    <!--//Page Content-->
</asp:Content>
