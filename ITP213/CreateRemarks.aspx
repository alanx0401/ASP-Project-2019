<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="CreateRemarks.aspx.cs" Inherits="ITP213.CreateRemarks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="Content/jquery-steps.css">
    <!-- Demo stylesheet -->
    <link rel="stylesheet" href="Content/style.css">
    <!--************README: Hi, please change some of the following things below when you're coding your features. Thanks! -PS -->

    <!-- Breadcrumbs-->
    <ol class="breadcrumb">

        <li class="breadcrumb-item">

            <a href="/Default.aspx" style="color: #D6D6D6">Home</a>
        </li>
        <li class="breadcrumb-item">

            <a href="/ViewInjuryReport.aspx" style="color: #D6D6D6">View Injury Report</a>
        </li>
        <li class="breadcrumb-item active">Add Remarks</li>
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
    <h1>Add Remarks</h1>
    <!--2. Change the title!-->
    <hr />
    <p>
        <!--3. This is where you code all your features-->

        <!--My content-->
        <div id="demo">
            <div class="step-app">
                <ul class="step-steps">
                    <li><a href="#step1">Add Remarks</a></li>
                </ul>
                <div class="step-content">
                    <div class="step-tab-panel" id="step1">
                        <div class="form-group">
                            <div class="row">
                                <div class="col-sm-12">
                                    Remarks:<asp:TextBox ID="tbRemarks" runat="server" Rows="8" Columns="10" TextMode="MultiLine" class="form-control" Style="resize: none;"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="step-footer">
                    <div class="step-footer">
                        <asp:Button ID="btnCreate" runat="server" class="step-btn btn btn-primary" Text="Create" OnClick="btnCreate_Click" />
                    </div>
                    <!--<button data-direction="finish" class="step-btn">Finish</button>-->
                </div>
            </div>
        </div>
        <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
    </p>
    <!--//Page Content-->

    <script src="http://code.jquery.com/jquery-latest.min.js"></script>
    <script src="Scripts/jquery-steps.js"></script>
    <script>
        $('#demo').steps({
            onFinish: function () {
                alert('Wizard Completed');
            }
        });
    </script>
    <!--//My content-->
</asp:Content>
