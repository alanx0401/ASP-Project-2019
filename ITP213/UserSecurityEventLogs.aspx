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
        })
    </script>
    <!-- //Breadcrumbs end-->
    <!-- Page Content -->
     <!--2. Change the title!--> 
    <hr />
     <asp:SqlDataSource ID="SqlDataSourceDDL" runat="server" ConnectionString="<%$ ConnectionStrings: ConnStr %>" SelectCommand="SELECT DISTINCT [eventDesc] FROM [Eventlogs]"></asp:SqlDataSource>
     <asp:SqlDataSource ID="SqlDataSourceGVUserParticularEvent" runat="server" ConnectionString="<%$ ConnectionStrings:ConnStr %>" SelectCommand="SELECT [eventDesc], [dateTimeDetails] FROM [Eventlogs] WHERE (([eventDesc] = @eventDesc) AND ([UUID] = @UUID))" ProviderName="System.Data.SqlClient">
         <SelectParameters>
            <asp:ControlParameter ControlID="DDLEventDesc" Name="eventDesc" PropertyName="SelectedValue" Type="String" />
             <asp:ControlParameter ControlID="lbUUID" Name="UUID" PropertyName="Text" Type="String" />
         </SelectParameters>
     </asp:SqlDataSource>
     <div>     
        <fieldset>
            <legend>Security Events for: <asp:Label ID="lbUser"  runat="server" Text="Label"></asp:Label></legend>
        </fieldset>
         <asp:Label ID="lbUUID" runat="server" Visible="False"></asp:Label>
        <p>Search by dropdown list: &nbsp<asp:DropDownList ID="DDLSearch" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDLSearch_SelectedIndexChanged">
                <asp:ListItem Value="0">Please Select</asp:ListItem>
                <asp:ListItem Value="1">Search by Event Description</asp:ListItem>
            </asp:DropDownList>&nbsp <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" /></p>
    </div>
      <div>
        <asp:Panel ID="PanelEvents" runat="server">
          <asp:GridView ID="GVEventLogs" runat="server" AutoGenerateColumns="False" Height="217px" Width="747px" >
            <Columns>
                <asp:BoundField DataField="eventDesc" HeaderText="Event Description" />
                <asp:BoundField DataField="dateTimeDetails" HeaderText="Date Time Occured" />
            </Columns>
          </asp:GridView>
        </asp:Panel>
    </div>
    <div>
        <asp:Panel ID="PanelSearchFilter" runat="server">
          <p>Search Security Event based on security event description:<asp:DropDownList ID="DDLEventDesc" runat="server" AutoPostBack="True" DataSourceID="SqlDataSourceDDL" DataTextField="eventDesc" DataValueField="eventDesc">
              </asp:DropDownList>
            </p> <asp:GridView ID="GVParticularEvent" runat="server" DataSourceID="SqlDataSourceGVUserParticularEvent" AutoGenerateColumns="False" Height="155px" Width="749px">
              <Columns>
                  <asp:BoundField DataField="eventDesc" HeaderText="eventDesc" SortExpression="eventDesc" />
                  <asp:BoundField DataField="dateTimeDetails" HeaderText="dateTimeDetails" SortExpression="dateTimeDetails" />
              </Columns>
            </asp:GridView>
          <br />
        </asp:Panel>
    </div>
</asp:Content>