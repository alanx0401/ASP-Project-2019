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

      <script type="text/javascript">
        $(document).ready(function () {
            $(function () {
                $("#tbStartDate").datepicker({
                    //minDate: 0
                    dateFormat: 'yy/mm/dd'
                });
                $("#tbEndDate").datepicker({
                    //minDate: 0
                    dateFormat: 'yy/mm/dd'
                });
            });
        });
        $(document).ready(function () {
            
            $(".createBtn").hide();
            $("#ContentPlaceHolder1_step1").click(function () {
                $(".createBtn").hide();
                var iSelectedTab = $(document).find("input[id*='tab_index']").val();
                console.log("hi :" + iSelectedTab);
            });
            $("#ContentPlaceHolder1_step2").click(function () {
                $(".createBtn").hide();
            });
            $("#ContentPlaceHolder1_step3").click(function () {
                $(".createBtn").show();
            });
        })
    </script>
    <!-- Page Content -->
    <h1>Event Logs</h1>
     <!--2. Change the title!--> 
    <hr />
    <asp:SqlDataSource ID="SqlDataSourceDDL" runat="server" ConnectionString="<%$ ConnectionStrings: ConnStr %>" SelectCommand="SELECT DISTINCT [eventDesc] FROM [Eventlogs]"></asp:SqlDataSource>  
     <asp:SqlDataSource ID="SqlDataSourceDDLUUID" runat="server" ConnectionString="<%$ ConnectionStrings:ConnStr %>" SelectCommand="SELECT DISTINCT [UUID] FROM [Eventlogs]">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSourceGVParticularEvent" runat="server" ConnectionString="<%$ ConnectionStrings:ConnStr %>" SelectCommand="SELECT [eventDesc], [dateTimeDetails], [UUID] FROM [Eventlogs] WHERE ([eventDesc] = @eventDesc)" ProviderName="System.Data.SqlClient">
        <SelectParameters>
            <asp:ControlParameter ControlID="DDLEventDesc" Name="eventDesc" PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
     </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSourceGVUUID" runat="server" ConnectionString="<%$ ConnectionStrings:ConnStr %>" SelectCommand="SELECT [Eventlogs].[UUID], [eventDesc], [dateTimeDetails], [name]
FROM [Eventlogs] 
INNER JOIN [account]
ON [account].UUID = [Eventlogs].UUID
WHERE ([Eventlogs].[UUID] = @UUID) 
ORDER BY [eventID] DESC" ProviderName="System.Data.SqlClient">
        <SelectParameters>
            <asp:ControlParameter ControlID="DDLUUID" Name="UUID" PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
     </asp:SqlDataSource>
    <div>
        <fieldset>
            <legend>Search by dropdown list</legend>
            <asp:DropDownList ID="DDLSearch" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDLSearch_SelectedIndexChanged">
                <asp:ListItem Value="0">Please Select</asp:ListItem>
                <asp:ListItem Value="1">Search by Event Description</asp:ListItem>
                <asp:ListItem Value="2">Search by Date</asp:ListItem>
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
        </asp:Panel>
    </div>
    <br />
    <div>
        <asp:Panel ID="PanelSearchFilter" runat="server">
          <p>Search Filter based on particular event:<asp:DropDownList ID="DDLEventDesc" runat="server" AutoPostBack="True" DataSourceID="SqlDataSourceDDL" DataTextField="eventDesc" DataValueField="eventDesc">
              </asp:DropDownList>
            </p> &nbsp<asp:GridView ID="GVParticularEvent" runat="server" DataSourceID="SqlDataSourceGVParticularEvent" AutoGenerateColumns="False">
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
          <p>Search filter based on when the event occured:&nbsp</p>
        <table>  
            <tr>
                <td>Start Date<asp:TextBox ID="tbStartDate" runat="server" class="form-control" TextMode="DateTime" ClientIDMode="Static"></asp:TextBox></td>
                <td>End Date<asp:TextBox ID="tbEndDate" runat="server" class="form-control" TextMode="DateTime" ClientIDMode="Static"></asp:TextBox></td>
                <td><asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" /> <asp:Button ID="btnResetDate" runat="server" Text="Reset Dates" OnClick="btnResetDate_Click" /> </td>
            </tr>
            
        </table>
          <asp:GridView ID="GVeventDuration" runat="server" AutoGenerateColumns="False">
              <Columns>
                  <asp:BoundField DataField="dateTimeDetails" HeaderText="dateTimeDetails"/>
                  <asp:BoundField DataField="eventID" HeaderText="eventID" />
                  <asp:BoundField DataField="eventDesc" HeaderText="eventDesc"/>
                  <asp:BoundField DataField="UUID" HeaderText="UUID"/>
              </Columns>
            </asp:GridView>
            <asp:Chart ID="chartEvent" runat="server" Height="318px" Palette="Fire" Width="607px">
                <Series>
                    <asp:Series ChartType="Bar" Name="Series1">
                    </asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1">
                        <AxisX Title ="Number of Events"></AxisX>
                        <AxisY Title="Event Description"></AxisY>
                    </asp:ChartArea>
                </ChartAreas>
            </asp:Chart>
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
                  <asp:BoundField DataField="name" HeaderText="name" SortExpression="name" />
              </Columns>
            </asp:GridView>
          <br />
        </asp:Panel>
    </div>
</asp:Content>
