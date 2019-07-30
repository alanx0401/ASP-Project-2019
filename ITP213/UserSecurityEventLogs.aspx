<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="UserSecurityEventLogs.aspx.cs" Inherits="ITP213.UserSecurityEventLogs" %>
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
    <!-- Page Content -->
    <h1>Event Logs</h1>
     <!--2. Change the title!--> <asp:Label ID="lblUser"  runat="server" Text="Label"></asp:Label>
    <hr />
      <div>
        <asp:Panel ID="PanelEvents" runat="server">
          <p>Security Web events for <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
          <asp:GridView ID="GVEventLogs" runat="server" AutoGenerateColumns="False" Height="217px" Width="747px" >
>>>>>>> Kaiming
            <Columns>
                <asp:BoundField DataField="eventDesc" HeaderText="Event Description" />
                <asp:BoundField DataField="dateTimeDetails" HeaderText="Date Time Occured" />
            </Columns>
          </asp:GridView>
    </div>
</asp:Content>