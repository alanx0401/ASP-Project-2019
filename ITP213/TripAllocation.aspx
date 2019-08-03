<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="TripAllocation.aspx.cs" Inherits="ITP213.TripAllocation" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="Content/jquery-steps.css">
    <script>
        /*$(document).ready(function () {
            var iSelectedTab = $(this).find("input[id*='tab_index']").val();
            $('.step-app').tabs({
                //collapsible: true,
                active: iSelectedTab
            });
            //$('[id$=tabs]').tabs({
                //selected: 1
            //});
            
        });*/
    </script>
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
    <style>
        .ui-tabs .ui-tabs-nav li {
            width: 50%;
            text-align: center;
        }

            .ui-tabs .ui-tabs-nav li a {
                display: inline-block;
                float: none;
                padding: 5px;
                text-decoration: none;
                width: 100%;
            }
    </style>
    <!-- Breadcrumbs-->
    <ol class="breadcrumb">

        <li class="breadcrumb-item">

            <a href="/Default.aspx" style="color: #D6D6D6">Home</a>
        </li>
        <li class="breadcrumb-item active">Create Trip</li>
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
    <h1>Create Trip</h1>
    <hr />
    <asp:Panel ID="panelAlert" runat="server" Visible="False">
        <div class="alert alert-danger alert-dismissible fade show" role="alert">
            <asp:Label ID="lblError" runat="server"></asp:Label>
            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                <span aria-hidden="true">&times;</span>
            </button>
        </div>
    </asp:Panel>
    <asp:Panel ID="panelSuccess" runat="server" Visible="False">
        <div class="alert alert-success alert-dismissible fade show" role="alert">
            <asp:Label ID="lblSuccess" runat="server" ForeColor="Black"></asp:Label>
            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                <span aria-hidden="true">&times;</span>
            </button>
        </div>
    </asp:Panel>
    <div id="demo">
        <div class="step-app" id="tabs">
            <asp:HiddenField ID="tab_index" Value="0" runat="server" />
            <ul class="step-steps"> 
                <li><a href="#tabs-1" runat="server" id="step1">Step 1</a></li>
                <li><a href="#tabs-2" runat="server" id="step2">Step 2</a></li>
                <li><a href="#tabs-3" runat="server" id="step3">Step 3</a></li>
            </ul>
            
            <div class="step-content">
                <div class="step-tab-panel" id="tabs-1">
                    <!--Trip Details-->
                    <div class="form-row">
                        <div class="form-group col-md-4">
                            <p>Trip Name:<asp:TextBox ID="tbTripName" runat="server" class="form-control" OnLoad="tbTripName_load" OnTextChanged="tbTripName_textChanged"></asp:TextBox></p>
                        </div>
                        <div class="form-group col-md-4">
                            <p>
                                Trip Type:<asp:DropDownList ID="ddlTripType" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlTripType_SelectedIndexChanged">
                                    <asp:ListItem>--Please Select--</asp:ListItem>
                                    <asp:ListItem>Study Trip</asp:ListItem>
                                    <asp:ListItem>Immersion Trip</asp:ListItem>
                                    <asp:ListItem>Internship</asp:ListItem>
                                </asp:DropDownList>
                            </p>
                        </div>
                        <div class="form-group col-md-4">
                            <p>
                                Country:
                                <asp:DropDownList ID="ddlCountry" runat="server" class="form-control">
                                    <asp:ListItem>--Select--</asp:ListItem>
                                    <asp:ListItem>Canada</asp:ListItem>
                                    <asp:ListItem>Hong Kong</asp:ListItem>
                                    <asp:ListItem>United States</asp:ListItem>
                                </asp:DropDownList>
                            </p>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="form-group col-md-4">
                            Cost:
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <div class="input-group-text">$</div>
                                </div>
                                <asp:TextBox ID="tbCost" runat="server" class="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group col-md-4">
                            <asp:Label ID="lblCompanyName" runat="server" Text="Company Name:" Visible="false"></asp:Label>
                            <div class="input-group">
                                <asp:TextBox ID="tbCompanyName" runat="server" class="form-control" Visible="false"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="form-group col-md-4">
                            <p>
                                Departure Date:<asp:TextBox ID="tbDepartureDate" runat="server" class="form-control" TextMode="DateTime" ClientIDMode="Static"></asp:TextBox>
                            </p>
                        </div>
                        <div class="form-group col-md-4">
                            <p>Arrival Date:<asp:TextBox ID="tbArrivalDate" runat="server" class="form-control" TextMode="DateTime" ClientIDMode="Static"></asp:TextBox></p>
                        </div>
                    </div>
                    <!--//Trip Details-->
                </div>
                <div class="step-tab-panel" id="tabs-2">
                    <!--Adding Students-->
                    <div class="form-row">
                        <div class="form-group col-md-6">
                            <p>
                                <asp:Panel ID="pnlname" runat="server" Enabled="false">
                                    Student&#39;s school:
                                <asp:DropDownList ID="ddlSchool" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlSchool_SelectedIndexChanged" readonly>
                                    <asp:ListItem Value="SIT">School of Information Technology</asp:ListItem>
                                </asp:DropDownList>
                                </asp:Panel>
                            </p>
                        </div>
                        <div class="form-group col-md-6">
                            <p>
                                <asp:Panel ID="Panel1" runat="server" Enabled="true">
                                    <asp:Label ID="lblCourses" runat="server" Text="Student's course:" Visible="True"></asp:Label>
                                    <asp:DropDownList ID="ddlCourses" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlCourses_SelectedIndexChanged" Visible="True">
                                    </asp:DropDownList>
                                </asp:Panel>
                            </p>
                        </div>
                    </div>
                    <p>Select Students:</p>
                    <div class="form-row">
                        <div class="form-group col-md-6">
                            <asp:Button ID="btnAddStudent" class="btn btn-success btn-block" runat="server" Text="Move All →" OnClick="btnAddStudent_Click" />
                            <asp:ListBox ID="lbStudents" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="lbStudents_SelectedIndexChanged"></asp:ListBox>


                        </div>
                        <div class="form-group col-md-6">
                            <asp:Button ID="btnRemoveStudent" class="btn btn-danger btn-block" runat="server" Text="← Remove All" OnClick="btnRemoveStudent_Click" />
                            <asp:ListBox ID="lbSelectedStudents" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="lbSelectedStudents_SelectedIndexChanged"></asp:ListBox>
                        </div>
                    </div>
                    <!--//Adding Students-->
                </div>
                <div class="step-tab-panel" id="tabs-3">
                    <!--Adding Lecturer-->
                    <div class="form-row">
                        <div class="form-group col-md-6">
                            <p>
                                Lecturer&#39;s department:
                                <asp:DropDownList ID="ddlLecturerDepartment" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlLecturerDepartment_SelectedIndexChanged">
                                    <asp:ListItem Value="--Please Select--">--Please Select--</asp:ListItem>
                                    <asp:ListItem Value="SIT">School of Information Technology</asp:ListItem>
                                </asp:DropDownList>
                            </p>
                        </div>
                    </div>
                    <p>Select Lecturers:</p>
                    <div class="form-row">
                        <div class="form-group col-md-6">
                            <asp:Button ID="btnAddLecturer" class="btn btn-success btn-block" runat="server" Text="Move All →" OnClick="btnAddLecturer_Click" />
                            <asp:ListBox ID="lbLecturers" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="lbLecturers_SelectedIndexChanged"></asp:ListBox>
                        </div>
                        <div class="form-group col-md-6">
                            <asp:Button ID="btnRemoveLecturer" class="btn btn-danger btn-block" runat="server" Text="← Remove All" OnClick="btnRemoveLecturer_Click" />
                            <asp:ListBox ID="lbSelectedLecturers" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="lbSelectedLecturers_SelectedIndexChanged"></asp:ListBox>
                        </div>
                    </div>
                    <!--//Adding Lecturer-->

                </div>
            </div>
            <div class="step-footer">
                <button data-direction="prev" class="step-btn" runat="server">Previous</button>
                <button data-direction="next" class="step-btn" runat="server" style="background-color:#4CAF50 !important; color:white !important;">Next</button>
                <button data-direction="finish" class="step-btn" runat="server"  OnServerClick="btnCreate_Click" style="background-color:#32C5D2 !important; color:white !important;">Create</button>
                <!--<asp:Button ID="btnCreate" data-direction="finish" runat="server" class="createBtn step-btn" Text="Create" OnClick="btnCreate_Click" />-->
            </div>
        </div>
    </div>



    <div class="form-row">
        <div class="form-group col-md-5">
        </div>
        <div class="form-group col-md-2">
        </div>
        <div class="form-group col-md-5">
            <asp:Label ID="lblMsg" runat="server" Forecolor="White"></asp:Label>
        </div>
    </div>
    <!--//Page Content-->
    <script type="text/javascript">
        $(document).ready(function () {
            $(function () {
                $("#tbDepartureDate").datepicker({
                    minDate: 0
                });
                $("#tbArrivalDate").datepicker({
                    minDate: 0
                });
            });
        });
        $(document).ready(function () {
            
            $(".createBtn").hide();
            $("#ContentPlaceHolder1_step1").click(function () {
                $(".createBtn").hide();
                var iSelectedTab = $(document).find("input[id*='tab_index']").val();
                console.log("hi :" + iSelectedTab);
            });
            $("#ContentPlaceHolder1_step2").click(function () {
                $(".createBtn").hide();
            });
            $("#ContentPlaceHolder1_step3").click(function () {
                $(".createBtn").show();
            });
        })
    </script>
    <!--<script src="http://code.jquery.com/jquery-latest.min.js"></script>-->
    <script src="Scripts/jquery-steps.js"></script>
    <script>
        var iSelectedTab2;
        var steps = new Promise(function (resolve, reject) {
            $(document).ready(function hi() { // PART A
                var iSelectedTab = $(this).find("input[id*='tab_index']").val();
                if (iSelectedTab == null)
                    iSelectedTab = 0;

                iSelectedTab2 = iSelectedTab;
                resolve(iSelectedTab2);
                /*$('.step-app').tabs({
                    //collapsible: true,
                    active: iSelectedTab
                });*/

                console.log("Selected tab isss: " + iSelectedTab)


            })
        });



        /*$('#demo').steps({ // PART B
            onInit: function () {
                iSelectedTab2 = $(this).find("input[id*='tab_index']").val();
                if (iSelectedTab2 == null)
                    iSelectedTab2 = 0;
                console.log("running??: " + iSelectedTab2);
                
            },
            startAt: iSelectedTab2
        });*/


        steps.then(function (value) {
            console.log("Promise val: " + value);
            $('#demo').steps({ // PART B
                startAt: value
            });
        });

    </script>


</asp:Content>
