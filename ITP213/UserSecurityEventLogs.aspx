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
        <li class="breadcrumb-item active">Security Event Logs</li> <!--1. Change the name!-->
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
    <asp:SqlDataSource ID="SqlDataSourceChart" runat="server" ConnectionString="Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\ITP213.mdf;Integrated Security=True" ProviderName="System.Data.SqlClient" SelectCommand="SELECT [eventDesc], COUNT (*) AS countEvent FROM [Eventlogs] WHERE (([dateTimeDetails] &gt;= @dateTimeDetails) AND ([dateTimeDetails] &lt;= @dateTimeDetails2) AND ([UUID] = @UUID))GROUP BY eventDesc">
            <SelectParameters>
                <asp:ControlParameter ControlID="tbStartDate" Name="dateTimeDetails" PropertyName="Text" Type="DateTime" />
                <asp:ControlParameter ControlID="tbEndDate" Name="dateTimeDetails2" PropertyName="Text" Type="DateTime" />
                <asp:ControlParameter ControlID="lbUUID" Name="UUID" PropertyName="Text" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
     <div>     
        <fieldset>
            <legend>Security Events for: <asp:Label ID="lbUser"  runat="server" Text=""></asp:Label></legend>
        </fieldset>
         <asp:Label ID="lbUUID" runat="server" Visible="False"></asp:Label>
        <p>Search by dropdown list: &nbsp<asp:DropDownList ID="DDLSearch" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDLSearch_SelectedIndexChanged">
                <asp:ListItem Value="0">Please Select</asp:ListItem>
                <asp:ListItem Value="1">Search by Security Event Description</asp:ListItem>
                <asp:ListItem Value="2">Search Security Event based on date range</asp:ListItem>
            </asp:DropDownList>&nbsp <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" /></p>
    </div>
      <div>
        <asp:Panel ID="PanelEvents" runat="server">
         <asp:Panel runat="server" ScrollBars="Vertical" Height="200px" Width="1100px">
          <asp:GridView ID="GVEventLogs" runat="server" AutoGenerateColumns="False" height="200px" Width="1100px" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1" GridLines="None" >
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
    <div>
        <asp:Panel ID="PanelEventDateRange" runat="server">
          <p>Search Security Event based on range of dates:&nbsp</p>
        <table>  
            <tr>
                <td>Start: <asp:TextBox ID="tbStartDate" runat="server" class="form-control" TextMode="DateTime" style="width:120px" ClientIDMode="Static"></asp:TextBox></td>
                <td>End: <asp:TextBox ID="tbEndDate" runat="server" class="form-control" TextMode="DateTime" style="width:120px" ClientIDMode="Static"></asp:TextBox></td>
                <td><asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" /></td>
            </tr>
        </table>
          <asp:Panel runat="server" ScrollBars="Vertical" Height="200px" Width="1100px">
              <asp:GridView ID="GVEventDateRange" runat="server" AutoGenerateColumns="False" Height="200px" Width="1100px" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1" GridLines="None">
             <Columns>
                 <asp:BoundField DataField="eventDesc" HeaderText="eventDesc"/>
                 <asp:BoundField DataField="dateTimeDetails" HeaderText="dateTimeDetails"/>
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
        <asp:Chart ID="ChartEvent" runat="server" DataSourceID="SqlDataSourceChart" Height="199px" Width="737px">
                <Series>
                    <asp:Series Name="Series1" XValueMember="eventDesc" YValueMembers="countEvent">
                    </asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1">
                    </asp:ChartArea>
                </ChartAreas>
            </asp:Chart> 
    </asp:Panel>
    </div>
</asp:Content>
