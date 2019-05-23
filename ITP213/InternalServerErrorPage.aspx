<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="InternalServerErrorPage.aspx.cs" Inherits="ITP213.InternalServerErrorPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Breadcrumbs-->
    <ol class="breadcrumb">

        <li class="breadcrumb-item">

            <a href="#" style="color:#D6D6D6">Home</a>
        </li>
        <li class="breadcrumb-item active">Internal Server Error</li> <!--1. Change the name!-->
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
    <p> <!--3. This is where you code all your features-->

        <!-- Page Content -->
          <h1 class="display-1">500</h1>
          <p class="lead">Internal Server Error. You can
            <a href="javascript:history.back()">go back</a>
            to the previous page, or
            <a href="/Default.aspx">return home</a>
          </p>
    </p>
    <!--//Page Content-->
</asp:Content>
