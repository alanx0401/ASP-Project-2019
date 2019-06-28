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
    <asp:SqlDataSource ID="SqlDataSourceDDLEventDuration" runat="server" ConnectionString="<%$ ConnectionStrings:ConnStr %>" SelectCommand="SELECT DISTINCT [dateTimeDetails] FROM [Eventlogs]"></asp:SqlDataSource>  
    <asp:SqlDataSource ID="SqlDataSourceEventCount" runat="server" ConnectionString="<%$ ConnectionStrings:ConnStr %>" SelectCommand="SELECT eventDesc, COUNT(*) AS CountEvent FROM Eventlogs GROUP BY eventDesc">
    </asp:SqlDataSource>
     <asp:SqlDataSource ID="SqlDataSourceDDLUUID" runat="server" ConnectionString="<%$ ConnectionStrings:ConnStr %>" SelectCommand="SELECT DISTINCT [UUID] FROM [Eventlogs]">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSourceGVParticularEvent" runat="server" ConnectionString="<%$ ConnectionStrings:ConnStr %>" SelectCommand="SELECT [eventDesc], [dateTimeDetails], [UUID] FROM [Eventlogs] WHERE ([eventDesc] = @eventDesc)" ProviderName="System.Data.SqlClient">
        <SelectParameters>
            <asp:ControlParameter ControlID="DDLEventDesc" Name="eventDesc" PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
     </asp:SqlDataSource>
     <asp:SqlDataSource ID="SqlDataSourceGVEventDuration" runat="server" ConnectionString="<%$ ConnectionStrings:ConnStr %>" SelectCommand="SELECT [dateTimeDetails], [eventDesc], [UUID] FROM [Eventlogs] WHERE ([dateTimeDetails] = @dateTimeDetails)">
        <SelectParameters>
            <asp:ControlParameter ControlID="DDLEventPeriod" Name="dateTimeDetails" PropertyName="SelectedValue" Type="DateTime" />
        </SelectParameters>
     </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSourceGVUUID" runat="server" ConnectionString="<%$ ConnectionStrings:ConnStr %>" SelectCommand="SELECT [UUID], [eventDesc], [dateTimeDetails] FROM [Eventlogs] WHERE ([UUID] = @UUID) ORDER BY [eventID] DESC">
        <SelectParameters>
            <asp:ControlParameter ControlID="DDLUUID" Name="UUID" PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
     </asp:SqlDataSource>
    <div>
        <fieldset>
            <legend>Search by dropdown list</legend>
            <asp:DropDownList ID="DDLSearch" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDLSearch_SelectedIndexChanged">
                <asp:ListItem Value="0">Please Select</asp:ListItem>
                <asp:ListItem Value="1">Search By Event Description</asp:ListItem>
                <asp:ListItem Value="2">Search by Date Time</asp:ListItem>
                <asp:ListItem Value="3">Search by UUID</asp:ListItem>
            </asp:DropDownList>&nbsp <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" />
        </fieldset>
    </div>
    <div>
        <asp:Panel ID="PanelEvents" runat="server">
          <p>All Security Events</p>
          <asp:GridView ID="GVEventLogs" runat="server" AutoGenerateColumns="False" Height="217px" Width="747px" >
            <Columns>
                <asp:BoundField DataField="eventID" HeaderText="Event ID" />
                <asp:BoundField DataField="eventDesc" HeaderText="Event Description" />
                <asp:BoundField DataField="dateTimeDetails" HeaderText="Date Time Occured" />
                <asp:BoundField DataField="UUID" HeaderText="UUID" />
            </Columns>
          </asp:GridView>
            <asp:Chart ID="chartEvent" runat="server" DataSourceID="SqlDataSourceEventCount" Height="318px" Width="607px">
                <Series>
                    <asp:Series Name="Series1" ChartType="Bar" XValueMember="eventDesc" YValueMembers="CountEvent"></asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                </ChartAreas>
            </asp:Chart>
        </asp:Panel>
    </div>
    <br />
    <div>
        <asp:Panel ID="PanelSearchFilter" runat="server">
          <p>Search Filter based on particular event:</p> &nbsp <asp:DropDownList ID="DDLEventDesc" runat="server" DataSourceID="SqlDataSourceDDL" DataTextField="eventDesc" DataValueField="eventDesc" AutoPostBack="True" OnSelectedIndexChanged="DDLEventDesc_SelectedIndexChanged" >
          </asp:DropDownList>
          <asp:GridView ID="GVParticularEvent" runat="server" DataSourceID="SqlDataSourceGVParticularEvent" AutoGenerateColumns="False">
              <Columns>
                  <asp:BoundField DataField="eventDesc" HeaderText="eventDesc" SortExpression="eventDesc" />
                  <asp:BoundField DataField="dateTimeDetails" HeaderText="dateTimeDetails" SortExpression="dateTimeDetails" />
                  <asp:BoundField DataField="UUID" HeaderText="UUID" SortExpression="UUID" />
              </Columns>
            </asp:GridView>
          <br />
        </asp:Panel>
    </div>
    <div>
        <asp:Panel ID="PanelEventDuration" runat="server">
          <p>Search filter based on when the event occured:&nbsp<asp:DropDownList ID="DDLEventPeriod" runat="server" DataSourceID="SqlDataSourceDDLEventDuration" DataTextField="dateTimeDetails" DataValueField="dateTimeDetails" AutoPostBack="True" OnSelectedIndexChanged="DDLEventDesc_SelectedIndexChanged" >
          </asp:DropDownList></p>
          <asp:GridView ID="GVeventDuration" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSourceGVEventDuration" OnSelectedIndexChanged="GVeventDuration_SelectedIndexChanged">
              <Columns>
                  <asp:BoundField DataField="dateTimeDetails" HeaderText="dateTimeDetails" SortExpression="dateTimeDetails" />
                  <asp:BoundField DataField="eventDesc" HeaderText="eventDesc" SortExpression="eventDesc" />
                  <asp:BoundField DataField="UUID" HeaderText="UUID" SortExpression="UUID" />
              </Columns>
            </asp:GridView>
          <br />
        </asp:Panel>
    </div>
    <div>
        <asp:Panel ID="PanelUUID" runat="server">
          <p>Search filter of event based on UUID:&nbsp<asp:DropDownList ID="DDLUUID" runat="server" DataSourceID="SqlDataSourceDDLUUID" DataTextField="UUID" DataValueField="UUID" AutoPostBack="True" OnSelectedIndexChanged="DDLEventDesc_SelectedIndexChanged" >
          </asp:DropDownList></p>
          <asp:GridView ID="GVUUID" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSourceGVUUID">
              <Columns>
                  <asp:BoundField DataField="UUID" HeaderText="UUID" SortExpression="UUID" />
                  <asp:BoundField DataField="eventDesc" HeaderText="eventDesc" SortExpression="eventDesc" />
                  <asp:BoundField DataField="dateTimeDetails" HeaderText="dateTimeDetails" SortExpression="dateTimeDetails" />
              </Columns>
            </asp:GridView>
          <br />
        </asp:Panel>
    </div>
</asp:Content>
