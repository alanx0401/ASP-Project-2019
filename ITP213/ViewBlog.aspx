<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="ViewBlog.aspx.cs" Inherits="ITP213.ViewBlog" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--************README: Hi, please change some of the following things below when you're coding your features. Thanks! -PS -->

    <!-- Breadcrumbs-->
    <ol class="breadcrumb">
        <li class="breadcrumb-item">
            <a href="/Default.aspx" style="color:#D6D6D6">Home</a>
        </li>

        <li class="breadcrumb-item active">View Blogs</li>
    </ol>
    <style>
        .breadcrumb
        {
        background-color: #FFFFFF !important;
            
        }
        </style>
    <!-- //Breadcrumbs end-->

    <!-- Page Content -->
    <h1>Blog List</h1> <!--2. Change the title!-->
    <hr/>
    <br />
        <asp:GridView ID="GridView2" runat="server" AllowCustomPaging="True" AutoGenerateColumns="False" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2" ForeColor="Black" HorizontalAlign="Justify" Width="940px" OnRowCommand="GridView2_RowCommand">
            <Columns>
                <asp:BoundField DataField="blogID" Visible="True" />
                <asp:BoundField DataField="studentName" HeaderText="Student Name" />
                <asp:BoundField DataField="title" HeaderText="Title" />
                <asp:BoundField DataField="country" HeaderText="Trip" />
                <asp:BoundField DataField="blogTime" DataFormatString="{0:d}" HeaderText="Date Uploaded" />
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
    <br/>
    </asp:Content>