<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="TestDetails.aspx.cs" Inherits="ITP213.TestDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--************README: Hi, please change some of the following things below when you're coding your features. Thanks! -PS -->

    <!-- Breadcrumbs-->
    <ol class="breadcrumb">
        <li class="breadcrumb-item">
            <a href="/Default.aspx" style="color:#D6D6D6">Home</a>
        </li>

        <li class="breadcrumb-item active">Answers</li>
    </ol>
    <style>
        .breadcrumb
        {
        background-color: #FFFFFF !important;
            
        }
        </style>
    <!-- //Breadcrumbs end-->
    <h1>Answers</h1> <!--2. Change the title!-->
    <hr/>
    Q1) What is the most popular sport in korea?<br />
    Answer:<asp:Label ID="lblQuestionOne" runat="server" Text="Label"></asp:Label>
    <br />
    Q2) What is the name of the tradition Korean costume?<br />
    Answer:<asp:Label ID="lblQuestionTwo" runat="server" Text="Label"></asp:Label>
    <br />
    Q3) When&nbsp; do Koreans celebrate their birthdays?<br />
    Answer:
    <asp:Label ID="lblQuestionThree" runat="server" Text="Label"></asp:Label>
    <br />
    Q4) When is the most popular Korean surname?<br />
    Answer:
    <asp:Label ID="lblQuestionFour" runat="server" Text="Label"></asp:Label>
    <br />
    Q5)What is the largest island in Korea<br />
    Answer:
    <asp:Label ID="lblQuestionFive" runat="server" Text="Label"></asp:Label>
    <br />
    <br />
</asp:Content>