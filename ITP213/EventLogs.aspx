<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="EventLogs.aspx.cs" Inherits="ITP213.EventLogs1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <!--************README: Hi, please change some of the following things below when you're coding your features. Thanks! -PS -->

    <!-- Breadcrumbs-->
    <ol class="breadcrumb">

        <li class="breadcrumb-item">

            <a href="#" style="color:#D6D6D6">Home</a>
        </li>
        <li class="breadcrumb-item active">Your current page</li> <!--1. Change the name!-->
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
    <h1>Security Event Logs</h1> <!--2. Change the title!-->
    <hr />
    <div>
  
    </div>
    <div>
        <asp:GridView ID="GVEventLogs" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="eventID" HeaderText="Event ID:" />
                <asp:BoundField DataField="eventDesc" HeaderText="Event Description:" />
                <asp:BoundField DataField="dateTimeDetails" HeaderText="Date and Time of Event:" />
                <asp:BoundField DataField="UUID" HeaderText="Unique User Identification:" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
