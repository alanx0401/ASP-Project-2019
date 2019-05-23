<%@ Page Title="Manage Image Gallery" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="ManageImageGallery.aspx.cs" Inherits="ITP213.ManageImageGallery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Content/imageGallery.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ol class="breadcrumb">
        <li class="breadcrumb-item">
            <a href="#" style="color: #D6D6D6">Home</a>
        </li>
        <li class="breadcrumb-item active">Manage Image Gallery</li>
        <!--1. Change the name!-->
    </ol>
    <style>
        .breadcrumb {
            background-color: #FFFFFF !important;
        }

            .breadcrumb > .breadcrumb-item {
                color: #031A82 !important;
            }

            .breadcrumb .breadcrumb-item + .breadcrumb-item::before {
                color: #D6D6D6;
            }
    </style>
    <!-- Page Content -->
    <h1>Manage Image Gallery</h1>
    <!--2. Change the title!-->
    <hr />
    <div style="overflow-x: auto;">
        <asp:GridView ID="GridViewImages" CssClass="table" runat="server" AutoGenerateColumns="False" OnRowDeleting="GridViewImages_RowDeleting" CellPadding="4" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="title" HeaderText="Title" />
                <asp:ImageField DataImageUrlField="image" HeaderText="Image">
                </asp:ImageField>
                <asp:BoundField DataField="user" HeaderText="Uploaded By" />
                <asp:BoundField DataField="location" HeaderText="Location" />
                <asp:BoundField DataField="tags" HeaderText="Tags" />
                <asp:CommandField HeaderText="Delete" ShowCancelButton="False" ShowDeleteButton="True" />
                <asp:TemplateField Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="LabelImageID" runat="server" Text='<%# Eval("imageID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
    </div>
    <!--//Page Content-->
</asp:Content>
