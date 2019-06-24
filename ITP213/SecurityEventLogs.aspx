<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="SecurityEventLogs.aspx.cs" Inherits="ITP213.SecurityEventLogs" %>
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
    <h1>Event Logs</h1>
     <!--2. Change the title!--> 
    <hr />
     <asp:SqlDataSource ID="SqlDataSourceDDL" runat="server" ConnectionString="<%$ ConnectionStrings: ConnStr %>" SelectCommand="SELECT DISTINCT [eventDesc] FROM [Eventlogs]"></asp:SqlDataSource>  
    <asp:SqlDataSource ID="SqlDataSourceGVParticularEvent" runat="server" ConnectionString="<%$ ConnectionStrings: ConnStr %>" SelectCommand="SELECT * FROM [Eventlogs] WHERE ([eventDesc] = @eventDesc) ORDER BY [eventDesc] DESC">
        <SelectParameters>
            <asp:ControlParameter ControlID="DDLEventDesc" Name="eventDesc" PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
     </asp:SqlDataSource>
    <div>
          <asp:Label ID="lb_title" runat="server" Text="Search Filter: "></asp:Label>
          <asp:DropDownList ID="DDLEventDesc" runat="server" DataSourceID="SqlDataSourceDDL" DataTextField="eventDesc" DataValueField="eventDesc" AutoPostBack="True" OnSelectedIndexChanged="DDLEventDesc_SelectedIndexChanged">
          </asp:DropDownList>
          &nbsp&nbsp <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" />
          <asp:GridView ID="GVEventLogs" runat="server" AutoGenerateColumns="False" Height="217px" Width="747px">
            <Columns>
                <asp:BoundField DataField="eventID" HeaderText="Event ID" />
                <asp:BoundField DataField="eventDesc" HeaderText="Event Description" />
                <asp:BoundField DataField="dateTimeDetails" HeaderText="Date Time Occured" />
                <asp:BoundField DataField="UUID" HeaderText="UUID" />
            </Columns>
          </asp:GridView>
    </div>
    <div>
     <asp:Label ID="lb_SearchFilter" runat="server" Text="Search Based on Filter: "></asp:Label>
     <asp:GridView ID="GVParticularEvent" runat="server" DataSourceID="SqlDataSourceGVParticularEvent"></asp:GridView>
    </div>
</asp:Content>
