<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="StudentWithdrawalRequest.aspx.cs" Inherits="ITP213.StudentWithdrawalRequest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--************README: Hi, please change some of the following things below when you're coding your features. Thanks! -PS -->

    <!-- Breadcrumbs-->
    <ol class="breadcrumb">

        <li class="breadcrumb-item">

            <a href="/Default.aspx" style="color:#D6D6D6">Home</a>
        </li>
        <li class="breadcrumb-item active">Withdrawal Request</li> <!--1. Change the name!-->
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
    <h1>Withdrawal Request</h1> <!--2. Change the title!-->
    <hr/>
    <p> <!--3. This is where you code all your features-->
        <asp:Panel ID="panelError" runat="server" Visible="false">
            <div class="alert alert-danger alert-dismissible fade show" role="alert">
                <asp:Label ID="lblError" runat="server" Text="Label"></asp:Label>
                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
        </asp:Panel>
        <div class="form-row">
            <div class="form-group col-12 col-sm-12">
               <asp:Label ID="lblAdminNo" runat="server" Visible="false"></asp:Label>
            </div>
        </div>
        <div class="form-row">
            <div class="form-group col-12 col-sm-12">
                Trip Name:
                <asp:DropDownList ID="ddlTripName" runat="server" class="form-control">
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-row">
            <div class="form-group col-12 col-sm-12">
                Reasons: 
                <asp:TextBox ID="tbReasons" runat="server" Columns="30" Rows="8" TextMode="MultiLine" class="form-control" Style="resize: none"></asp:TextBox>   
            </div>
        </div>
        <div class="form-row">
            <div class="form-group col-6">
                <asp:Button ID="btnSubmit" class="btn btn-success" runat="server" Text="Submit Request" OnClick="btnSubmit_Click" />
            </div>
        </div>
    </p>
    <!--//Page Content-->
</asp:Content>
