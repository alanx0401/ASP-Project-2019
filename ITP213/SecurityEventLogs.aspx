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
        <li class="breadcrumb-item active">Security Event Logs</ol>
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

            $(function () {
                var clicked = false;
                var firstbtn = $('#btnUUID');
                var secondbtn = $('#btnUsername');
                firstbtn.on('click', function () {
                    clicked = !clicked;
                    if (clicked)
                        secondbtn.attr('disabled', 'disabled');
                    else
                        secondbtn.removeAttr('disabled')
                });
                secondbtn.on('click', function () {
                    clicked = !clicked;
                    if (clicked)
                        firstbtn.attr('disabled', 'disabled');
                    else
                        firstbtn.removeAttr('disabled')
                });
            });
        })
    </script>
    <!-- Page Content -->
     <!--2. Change the title!--> 
    <hr />
    <asp:SqlDataSource ID="SqlDataSourceDDL" runat="server" ConnectionString="<%$ ConnectionStrings: ConnStr %>" SelectCommand="SELECT DISTINCT [eventDesc] FROM [Eventlogs]"></asp:SqlDataSource>  
    <asp:SqlDataSource ID="SqlDataSourceGVEventsByUsers" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT eventID, eventDesc, dateTimeDetails, account.name As username FROM EventLogs INNER JOIN account ON account.UUID = Eventlogs.UUID  ORDER BY eventID DESC
"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSourceGVParticularEvent" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [eventDesc], [dateTimeDetails], [UUID] FROM [Eventlogs]  WHERE  ([eventDesc] = @eventDesc) ">
        <SelectParameters>
            <asp:ControlParameter ControlID="DDLEventDesc" Name="eventDesc" PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
     </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSourceChart" runat="server" ConnectionString="Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\ITP213.mdf;Integrated Security=True" ProviderName="System.Data.SqlClient" SelectCommand="SELECT eventDesc, COUNT (*) AS countEvent FROM [Eventlogs] WHERE (([dateTimeDetails] &gt;= @dateTimeDetails) AND ([dateTimeDetails] &lt;= @dateTimeDetails2)) GROUP BY eventDesc">
            <SelectParameters>
                <asp:ControlParameter ControlID="tbStartDate" Name="dateTimeDetails" PropertyName="Text" Type="DateTime" />
                <asp:ControlParameter ControlID="tbEndDate" Name="dateTimeDetails2" PropertyName="Text" Type="DateTime" />
            </SelectParameters>
        </asp:SqlDataSource>
    <div>     
        <fieldset>
            <legend>Security Event Logs of all users</legend>
        </fieldset>
        <p>Search by dropdown list: &nbsp<asp:DropDownList ID="DDLSearch" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDLSearch_SelectedIndexChanged">
                <asp:ListItem Value="0">Please Select</asp:ListItem>
                <asp:ListItem Value="1">Search Security Event by Description</asp:ListItem>
                <asp:ListItem Value="2">Search Security Event by Range of dates</asp:ListItem>
                <asp:ListItem Value="3">Get Security Events based on Account Type</asp:ListItem>
            </asp:DropDownList>&nbsp <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" /></p>
    </div>
    <div>
        <asp:Panel ID="PanelEvents" runat="server">
          <p>All Security Event Logs 
              <asp:Button ID="btnUUID" runat="server" Text="Switch to UUID" OnClick="btnUUID_Click" />
              <asp:Button ID="btnUsername" runat="server" Text="Switch to username" OnClick="btnUsername_Click" />
            </p>
          <asp:Panel runat="server" ScrollBars="Vertical" Height="400px" Width="1000px">
             <asp:GridView ID="GVEventLogs" runat="server" AutoGenerateColumns="False" Height="187px" Width="992px" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1" GridLines="None">
            <Columns>
                <asp:BoundField DataField="eventID" HeaderText="Event ID" />
                <asp:BoundField DataField="eventDesc" HeaderText="Event Description" />
                <asp:BoundField DataField="dateTimeDetails" HeaderText="Date Time Occured" />
                <asp:BoundField DataField="UUID" HeaderText="UUID" />
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
          <br />
          <asp:GridView ID="GVEventLogsInUsername" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1" DataKeyNames="eventID" DataSourceID="SqlDataSourceGVEventsByUsers" GridLines="None" Height="200px" Width="1000px" Visible="False">
                  <Columns>
                      <asp:BoundField DataField="eventID" HeaderText="eventID" InsertVisible="False" ReadOnly="True" SortExpression="eventID" />
                      <asp:BoundField DataField="eventDesc" HeaderText="eventDesc" SortExpression="eventDesc" />
                      <asp:BoundField DataField="dateTimeDetails" HeaderText="dateTimeDetails" SortExpression="dateTimeDetails" />
                      <asp:BoundField DataField="username" HeaderText="username" SortExpression="username" />
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
    <br />
    <div>
        <asp:Panel ID="PanelSearchFilter" runat="server">
          <p>Search Security Event based on security event description:<asp:DropDownList ID="DDLEventDesc" runat="server" AutoPostBack="True" DataSourceID="SqlDataSourceDDL" DataTextField="eventDesc" DataValueField="eventDesc">
             </asp:DropDownList></p>
          <asp:Panel runat="server" ScrollBars="vertical" Height="200px" Width="1000px">
           <asp:GridView ID="GVParticularEvent" runat="server" DataSourceID="SqlDataSourceGVParticularEvent" AutoGenerateColumns="False" Height="200px" Width="1000px" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1" GridLines="None">
              <Columns>
                  <asp:BoundField DataField="eventDesc" HeaderText="eventDesc" SortExpression="eventDesc" />
                  <asp:BoundField DataField="dateTimeDetails" HeaderText="dateTimeDetails" SortExpression="dateTimeDetails" />
                  <asp:BoundField DataField="UUID" HeaderText="UUID" SortExpression="UUID" />
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
        <asp:Panel runat="server" ScrollBars="Vertical" Height="200px" Width="1000px">
              <asp:GridView ID="GVEventDateRange" runat="server" AutoGenerateColumns="False" Height="200px" Width="1000px" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1" GridLines="None">
             <Columns>
                 <asp:BoundField DataField="dateTimeDetails" HeaderText="dateTimeDetails" />
                 <asp:BoundField DataField="eventDesc" HeaderText="eventDesc" />
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
            <asp:Chart ID="ChartEvent" runat="server" DataSourceID="SqlDataSourceChart" Height="200px" Width="1000px">
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
     <div>
        <asp:Panel ID="PanelAuditLogs" runat="server">
          <p>Search Security Events sorted by Account Type:
          <asp:Panel runat="server" ScrollBars="vertical" Height="200px" Width="1000px">
           <asp:GridView ID="GVAuditLogs" runat="server" AutoGenerateColumns="False" Height="200px" Width="1000px" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1" GridLines="None">
              <Columns>
                  <asp:BoundField DataField="eventID" HeaderText="eventID" />
                  <asp:BoundField DataField="eventDesc" HeaderText="Event Description" />
                  <asp:BoundField DataField="dateTimeDetails" HeaderText="DateTime Details" />
                  <asp:BoundField DataField="UUID" HeaderText="UUID" />
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
