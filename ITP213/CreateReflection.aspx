<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="CreateReflection.aspx.cs" Inherits="ITP213.CreateReflection" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Breadcrumbs-->
    <ol class="breadcrumb">
        <li class="breadcrumb-item">
            <a href="#" style="color: #D6D6D6">Home</a>
        </li>
        <li class="breadcrumb-item active">Create Reflection</li>
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

        .table-borderless td,
        .table-borderless th {
            border: 0;
        }

        @media (max-width:40em) {
            table, thead, tbody, tfoot, th, td, tr {
                display: block;
            }

                tr + tr {
                    margin-top: 1em;
                }
        }
    </style>
    <!-- //Breadcrumbs end-->

    <!-- Page Content -->
    <h1>Create Reflection</h1>
    <hr />
            <table class="table table-borderless">
                <tr>
                    <td>Please enter your reflection for your 12 / 24 weeks internship below</td>
                </tr>
                <tr>
                    <td>Trip Name:
                        <!--Displaying trips tht are internship, and are ongoing-->
                        <asp:DropDownList ID="ddlTripName" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="tbReflection" runat="server" Rows="9" Columns="12" TextMode="MultiLine" class="form-control" Style="resize: none"></asp:TextBox></td>
                </tr>
                <tr>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnSubmitReflecction" runat="server" Text="Submit Reflection" OnClick="btnSubmitReflecction_Click" class="btn btn-success" />
                    </td>
                </tr>
            </table>
    <!--//Page Content-->
</asp:Content>
