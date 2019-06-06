<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="EventLogs.aspx.cs" Inherits="ITP213.EventLogs" %>
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
    <h1>Event Logs</h1> <!--2. Change the title!-->
    <hr />
    <div>
  
    </div>
    <div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="eventID" DataSourceID="SqlDataSourceEventLog" Height="217px" Width="747px">
            <Columns>
                <asp:BoundField DataField="eventID" HeaderText="eventID" ReadOnly="True" SortExpression="eventID" />
                <asp:BoundField DataField="eventDesc" HeaderText="eventDesc" SortExpression="eventDesc" />
                <asp:BoundField DataField="dateTimeDetails" HeaderText="dateTimeDetails" SortExpression="dateTimeDetails" />
                <asp:BoundField DataField="UUID" HeaderText="UUID" SortExpression="UUID" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSourceEventLog" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [Eventlogs] ORDER BY [eventID] DESC"></asp:SqlDataSource>
    </div>
</asp:Content>

