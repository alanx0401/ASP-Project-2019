<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="AddRemarks.aspx.cs" Inherits="ITP213.AddRemarks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Breadcrumbs-->
    <ol class="breadcrumb">

        <li class="breadcrumb-item">

            <a href="/Default.aspx" style="color: #D6D6D6">Home</a>
        </li>
        <li class="breadcrumb-item">

            <a href="/ViewInjuryReport.aspx" style="color: #D6D6D6">View Injury Report</a>
        </li>
        <li class="breadcrumb-item active">
            <asp:Label ID="lblTitle" runat="server" Text="Add Remarks"></asp:Label></li>
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
    <!-- //Breadcrumbs end-->

    <!-- Page Content -->
    <h1>
        <asp:Label ID="lblTitle2" runat="server" Text="Add Remarks"></asp:Label></h1>
    <!--2. Change the title!-->
    <hr />
    <!--//Page Content-->
    <br />
    <link rel="stylesheet" href="Content/jquery-steps.css">
    <style>
        body {
            font-size: 14px;
            font-family: Arial, Helvetica, sans-serif;
        }

        button, input, select, textarea {
            font-family: inherit;
            font-size: inherit;
            line-height: inherit;
        }
        /* jquery validation */
        label.error {
            color: #e7505a;
            margin-left: 10px;
            padding: 7px;
        }

        input.error {
            border: 2px solid #e7505a;
        }
    </style>
    <asp:Panel ID="panelAlert" runat="server" Visible="False">
        <div class="alert alert-danger alert-dismissible fade show" role="alert">
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                <span aria-hidden="true">&times;</span>
            </button>
        </div>
    </asp:Panel>
    <asp:Panel ID="panelSuccess" runat="server" Visible="False">
        <div class="alert alert-success alert-dismissible fade show" role="alert">
            <asp:Label ID="lblSuccess" runat="server"></asp:Label>
            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                <span aria-hidden="true">&times;</span>
            </button>
        </div>
    </asp:Panel>
<div class="step-tab-panel" id="step1">
                    <div class="form-group">
                        Remarks:<asp:TextBox ID="tbRemarks" runat="server" Rows="8" Columns="10" TextMode="MultiLine" class="form-control" Style="resize: none;"></asp:TextBox>
                    </div>
                </div>
                <div class="step-footer">
                    <asp:Button ID="btnCreate" runat="server" class="step-btn btn btn-primary" Text="Create" OnClick="btnCreate_Click" />
                </div>
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
</asp:Content>
