<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="ViewTestAnswers.aspx.cs" Inherits="ITP213.ViewTestAnswers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--************README: Hi, please change some of the following things below when you're coding your features. Thanks! -PS -->

    <!-- Breadcrumbs-->
    <ol class="breadcrumb">
        <li class="breadcrumb-item">
            <a href="/Default.aspx" style="color:#D6D6D6">Home</a>
        </li>

        <li class="breadcrumb-item active">Results</li>
    </ol>
    <style>
        .breadcrumb
        {
        background-color: #FFFFFF !important;
            
        }
        </style>
    <!-- //Breadcrumbs end-->

    <!-- Page Content -->
    <h1>Test List</h1> <!--2. Change the title!-->
    <hr/>
    <br />
    <div class="container" style="overflow-x:auto;">
       <div class="row">
            <asp:GridView ID="GridView2" runat="server" AllowCustomPaging="True" AutoGenerateColumns="False" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2" ForeColor="Black" HorizontalAlign="Justify" Width="940px" OnRowCommand="GridView2_RowCommand">
                <Columns>
                    <asp:BoundField DataField="testID" HeaderText="Number" />
                    <asp:BoundField DataField="studentName" HeaderText="Student Name" />
                    <asp:BoundField DataField="adminNo" HeaderText="Admin Number" />
                    <asp:ButtonField CommandName="View" Text="View" />
                </Columns>
                <FooterStyle BackColor="#CCCCCC" />
                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                <RowStyle BackColor="White" />
                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#808080" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#383838" />
            </asp:GridView>
        </div>
    </div>
    <br/>
    </asp:Content>