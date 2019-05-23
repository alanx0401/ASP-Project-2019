<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="BlogDetails.aspx.cs" Inherits="ITP213.BlogDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--************README: Hi, please change some of the following things below when you're coding your features. Thanks! -PS -->

    <!-- Breadcrumbs-->
    <ol class="breadcrumb">
        <li class="breadcrumb-item">
            <a href="/Default.aspx" style="color:#D6D6D6">BlogDetails</a>
        </li>
        <li class="breadcrumb-item active">BlogDetails</li>
    </ol>
    <style>
        .breadcrumb
        {
        background-color: #FFFFFF !important;
            
        }
    </style>
    <hr />
    <h1>&nbsp;<asp:Label ID="LabelTitle" runat="server" Text="Name" style="text-align:center" ></asp:Label></h1>
    <h6>&nbsp;By : <asp:Label ID="LabelName" runat="server" Text="Title" style="text-align:center"></asp:Label></h6>
    <h3>&nbsp;<asp:Label ID="LabelContent" runat="server" Text="Content" style="text-align:center"></asp:Label></h3>
    


</asp:Content>
