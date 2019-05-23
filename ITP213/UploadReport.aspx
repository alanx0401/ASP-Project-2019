<%@ Page Title="Upload Report" MasterPageFile="~/Site.master" Language="C#" AutoEventWireup="true" CodeBehind="UploadReport.aspx.cs" Inherits="ITP213.UploadReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ol class="breadcrumb">
        <li class="breadcrumb-item">
            <a href="#" style="color: #D6D6D6">Home</a>
        </li>
        <li class="breadcrumb-item active">Upload Report</li>
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

        .table-borderless td,
        .table-borderless th {
            border: 0;
        }
    </style>
    <!-- //Breadcrumbs end-->

    <!-- Page Content -->
    <h1>Upload Report</h1>
    <!--2. Change the title!-->
    <hr />
    <div class="row">
        <div class="col-lg-4"></div>
        <div class="col-lg-4">
            <div class="container" style="overflow-x:auto;">
                <div class="row">
                    <table class="table w-100 table-borderless">
                        <tr>
                            <td>Please upload your report for your 12 / 24 weeks internship below</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:FileUpload ID="FileUpload1" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button CssClass="btn btn-primary" ID="Button1" runat="server" Text="Upload" OnClick="Button1_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="col-lg-4"></div>
    </div>
    <!--//Page Content-->
    <br />
    <br />
    <br />
</asp:Content>
