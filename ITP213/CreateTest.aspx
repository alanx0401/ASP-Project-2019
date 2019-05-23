<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="CreateTest.aspx.cs" Inherits="ITP213.CreateTest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--************README: Hi, please change some of the following things below when you're coding your features. Thanks! -PS -->

    <!-- Breadcrumbs-->
    <ol class="breadcrumb">
        <li class="breadcrumb-item">
            <a href="/Default.aspx" style="color: #D6D6D6">Home</a>
        </li>

        <li class="breadcrumb-item active">Test</li>
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
    <h1><asp:Label ID="lblTitle" runat="server" Text="Test"></asp:Label></h1> <!--2. Change the title!-->
    <hr/>

    Q1) What is the most popular sport in korea?<br />
&nbsp;&nbsp;&nbsp;<asp:RadioButtonList ID="radioButtonListQ1" runat="server" RepeatDirection="Horizontal">
        <asp:ListItem Value="1">Soccer</asp:ListItem>
        <asp:ListItem Value="2">Basketball</asp:ListItem>
        <asp:ListItem Value="3">Baseball</asp:ListItem>
        <asp:ListItem Value="4">Taekdondo</asp:ListItem>
    </asp:RadioButtonList>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="radioButtonListQ1" ErrorMessage="Please answer question" ForeColor="#CC3300"></asp:RequiredFieldValidator>
    <br />
    Q2) What is the name of the tradition Korean costume?<br />
&nbsp;&nbsp;&nbsp;<asp:RadioButtonList ID="radioButtonListQ2" runat="server" RepeatDirection="Horizontal">
        <asp:ListItem Value="1">Hanbok</asp:ListItem>
        <asp:ListItem Value="2">Sari</asp:ListItem>
        <asp:ListItem Value="3">Cheongsam</asp:ListItem>
        <asp:ListItem Value="4">Kimono</asp:ListItem>
    </asp:RadioButtonList>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="radioButtonListQ2" ErrorMessage="Please answer question" ForeColor="#CC3300"></asp:RequiredFieldValidator>
    <br />
    Q3) When&nbsp; do Koreans celebrate their birthdays?<br />
    Answer:
    <asp:TextBox ID="txtBoxQ3" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
    <br />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtBoxQ3" ErrorMessage="Please answer question" ForeColor="#CC3300"></asp:RequiredFieldValidator>
    <br />
    Q4) When is the most popular Korean surname?<br />
    Answer:
    <asp:TextBox ID="txtBoxQ4" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
    <br />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtBoxQ4" ErrorMessage="Please answer question" ForeColor="#CC3300"></asp:RequiredFieldValidator>
    <%--<br />--%>
    <br />
    Q5)What is the largest island in Korea<br />
    Answer:
    <asp:TextBox ID="txtBoxQ5" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
    <br />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtBoxQ5" ErrorMessage="Please answer question" ForeColor="#CC3300"></asp:RequiredFieldValidator>
    <br />
    <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" />
    <br />
    <asp:Label ID="lblTest" runat="server" Text="Label" Visible="False"></asp:Label>
    <br />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="#996600" />
    <br />

    <!--//Page Content-->
</asp:Content>

