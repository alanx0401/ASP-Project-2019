<<<<<<< HEAD
<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="UserSecurityEventLogs.aspx.cs" Inherits="ITP213.UserSecurityEventLogs" %>
=======
﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="UserSecurityEventLogs.aspx.cs" Inherits="ITP213.IndividualUserEventLog" %>
>>>>>>> TechnicalReview2
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

<<<<<<< HEAD
<<<<<<< HEAD
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
    FROM [Eventlogs] INNER JOIN [account] ON [account].UUID = [Eventlogs].UUID WHERE ([Eventlogs].[UUID] = @UUID) ORDER BY [eventID] DESC" ProviderName="System.Data.SqlClient">
        <SelectParameters>
            <asp:ControlParameter ControlID="DDLUUID" Name="UUID" PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
     </asp:SqlDataSource>
    <div>
         <fieldset>
            <legend>Security Events of <asp:Label ID="lbUser" runat="server" Text=""></asp:Label></legend>
        </fieldset>
       <asp:GridView ID="GVUserSecurityEventLogs" runat="server" AutoGenerateColumns="False" Height="217px" Width="747px" >
=======
=======
>>>>>>> TechnicalReview2
    <!-- Page Content -->
    <h1>Event Logs</h1>
     <!--2. Change the title!--> <asp:Label ID="lblUser"  runat="server" Text="Label"></asp:Label>
    <hr />
      <div>
        <asp:Panel ID="PanelEvents" runat="server">
         <asp:Panel runat="server" ScrollBars="Vertical" Height="200px">
          <asp:GridView ID="GVEventLogs" runat="server" AutoGenerateColumns="False" height="199px" Width="737px" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1" GridLines="None" >
            <Columns>
                <asp:BoundField DataField="eventDesc" HeaderText="Event Description" />
                <asp:BoundField DataField="dateTimeDetails" HeaderText="Date Time Occured" />
            </Columns>
              <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
              <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
              <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
              <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
              <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
              <SortedAscendingCellStyle BackColor="#F1F1F1" />
              <SortedAscendingHeaderStyle BackColor="#594B9C" />
              <SortedDescendingCellStyle BackColor="#CAC9C9" />
              <SortedDescendingHeaderStyle BackColor="#33276A" />
          </asp:GridView>
          </asp:Panel>
        </asp:Panel>
    </div>
    <div>
        <asp:Panel ID="PanelSearchFilter" runat="server">
          <p>Search Security Event based on security event description:<asp:DropDownList ID="DDLEventDesc" runat="server" AutoPostBack="True" DataSourceID="SqlDataSourceDDL" DataTextField="eventDesc" DataValueField="eventDesc">
              </asp:DropDownList>
            </p> 
            <asp:Panel runat="server" ScrollBars="Vertical" Height="200px">
                <asp:GridView ID="GVParticularEvent" runat="server" DataSourceID="SqlDataSourceGVUserParticularEvent" AutoGenerateColumns="False" Height="155px" Width="749px" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1" GridLines="None">
                    <Columns>
                        <asp:BoundField DataField="eventDesc" HeaderText="eventDesc" SortExpression="eventDesc" />
                        <asp:BoundField DataField="dateTimeDetails" HeaderText="dateTimeDetails" SortExpression="dateTimeDetails" />
                    </Columns>
                    <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                    <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
                    <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                    <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                    <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#594B9C" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#33276A" />
                </asp:GridView>
            </asp:Panel>
        </asp:Panel>            
    </div>
</asp:Content>
>>>>>>> TechnicalReview2
