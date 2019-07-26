<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="GoogleAuth.aspx.cs" Inherits="ITP213.GoogleAuth" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--************README: Hi, please change some of the following things below when you're coding your features. Thanks! -PS -->

    <!-- Breadcrumbs-->
    <ol class="breadcrumb">

        <li class="breadcrumb-item">

            <a href="/Default.aspx" style="color:#D6D6D6">Home</a>
        </li>
        <li class="breadcrumb-item active">Google Auth</li> <!--1. Change the name!-->
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
    <h1>Google Auth</h1> <!--2. Change the title!-->
    <hr/>
    <p> <!--3. This is where you code all your features-->

        <asp:Image ID="Image1" runat="server" />
    </p>
    <p> 
        <asp:Label ID="Label1" runat="server"></asp:Label>
    </p>
    <hr />
    Enter code:
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    <br />
    <asp:Button ID="Button1" runat="server" Text="Confirm" OnClick="Button1_Click" />
    <br />
    <asp:Label ID="Label2" runat="server"></asp:Label>
    <!--//Page Content-->
</asp:Content>
