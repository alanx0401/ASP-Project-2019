<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="ViewAllocatedTrip.aspx.cs" Inherits="ITP213.ViewAllocatedTrip" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel='stylesheet' href='Content/mdb1.min.css' />
    <style>
        .tabs {
            position: relative;
            top: 1px;
            left: 10px;
        }

        .tab {
            border: solid 1px black;
            background-color: #F5F6FA;
            padding: 5px 10px;
        }

        .selectedTab {
            background-color: white;
            border-bottom: solid 1px white;
        }

        .tabContents {
            border: solid 1px black;
            padding: 10px;
            background-color: white;
        }
    </style>
    <!-- Breadcrumbs-->
    <ol class="breadcrumb">

        <li class="breadcrumb-item">

            <a href="/Default.aspx" style="color: #D6D6D6">Home</a>
        </li>
        <li class="breadcrumb-item active">View Trips</li>
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
    <h1>View Trips</h1>
    <!--2. Change the title!-->
    <hr />
    <p>
        <!--3. This is where you code all your features-->
        <script>
            $(function () {
                $("#tabs").tabs();
            });
        </script>
        <asp:Panel ID="panelAlert" runat="server" Visible="False">
            <div class="alert alert-success alert-dismissible fade show" role="alert">
                Yay! You have successfully added a group.
                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
        </asp:Panel>
        <div id="tabs">
            <ul>
                <li><a href="#tabs-1">Study Trips</a></li>
                <li><a href="#tabs-2">Immersion Trips</a></li>
                <li><a href="#tabs-3">Internships</a></li>
            </ul>
            <div id="tabs-1">
                <asp:Repeater ID="RepeaterStudyTrips" runat="server" OnItemCommand="Repeater1_ItemCommand" OnItemDataBound="RepeaterStudyTrip_ItemDataBound">
                    <ItemTemplate>
                        <div class="row">
                            <div class="col-12 col-sm-12">
                                <div class="list-group">
                                    <div href="#" class="list-group-item list-group-item-action flex-column align-items-start">
                                        <div class="d-flex w-100 justify-content-between">
                                            <h5 class="mb-1">
                                                <asp:LinkButton ID="LinkButton1" runat="server" CommandName="trips_Click" CommandArgument='<%# Eval("tripID") %>' Text='<%#Eval("tripName") %>'>
                                                </asp:LinkButton>
                                            </h5>
                                            <small>
                                                <asp:Button ID="btnStudyTripsWithdraw" runat="server" Text="Withdraw" Class="btn btn-warning btn-sm text-white" CommandName="trips_Click" CommandArgument='<%# Eval("tripID") %>' OnClick="btnStudyTripsWithdraw_Click" OnCommand="btnStudyTrips_Command" ForeColor="White"/>
                                                <asp:Button ID="btnStudyTripsEdit" runat="server" Class="btn btn-warning btn-sm text-white" Text="Edit Trip" OnCommand="EditTrip_Command" CommandName="trips_Click" CommandArgument='<%# Eval("tripID") %>' />
                                                <asp:Button ID="btnStudyTripsDelete" runat="server" Class="btn btn-danger btn-sm" Text="Delete" OnCommand="delete_Command" CommandName="trips_Click" CommandArgument='<%# Eval("tripID") %>' />
                                            </small>
                                        </div>
                                        <!--<p class="mb-1">
                                            
                                        </p>-->
                                        <small>From
                                            <asp:Label ID="Label1" runat="server" Text='<%# Convert.ToDateTime(Eval("departureDate")).ToShortDateString() %>'></asp:Label>
                                            to
                                            <asp:Label ID="Label2" runat="server" Text='<%# Convert.ToDateTime(Eval("arrivalDate")).ToShortDateString() %>'></asp:Label>
                                            (<asp:Label ID="lblTripStatus" runat="server" Text='<%# Eval("overseasTripStatus") %>'></asp:Label>)
                                        </small>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <br />
                <!--If repeater is empty-->
                <asp:Panel ID="PanelStudyTrips" runat="server" Visible="False">
                    <div class="jumbotron jumbotron-fluid">
                        <div class="container">
                            <h1 class="display-5">You are currently not in any Study Trip programme.</h1>
                            <p class="lead">Keep a look out for future overseas programme!</p>
                        </div>
                    </div>
                </asp:Panel>
                <!--//If repeater is empty-->
                <center>
                    <button type="button" class="btn btn-warning btn-sm text-white" data-toggle="modal" data-target="#PastStudyTrip">
                        View Past Trips</button>
                </center>
                <!--Central Modal Large Warning-->
                <div class="modal fade" id="PastStudyTrip" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                    <div class="modal-dialog modal-lg modal-notify modal-warning" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="heading lead">Past Study Trips</h5>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true" class="white-text">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <asp:Label ID="lblPastStudyTripMsg" runat="server" Text="Currently, there are no past Study Trips yet!"></asp:Label>
                                <!-- Display a list of announcements-->
                                <asp:Repeater ID="RepeaterPastStudyTrips" runat="server" OnItemCommand="RepeaterPastStudyTrips_Command">
                                    <ItemTemplate>
                                        <div class="row">
                                            <div class="col-12 col-sm-12">
                                                <div class="list-group">
                                                    <div href="#" class="list-group-item list-group-item-action flex-column align-items-start">
                                                        <div class="d-flex w-100 justify-content-between">
                                                            <h5 class="mb-1">
                                                                <asp:LinkButton ID="LinkButton1" runat="server" CommandName="trips_Click" CommandArgument='<%# Eval("tripID") %>' Text='<%#Eval("tripName") %>'>
                                                                </asp:LinkButton>
                                                            </h5>

                                                        </div>
                                                        <small>From
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Convert.ToDateTime(Eval("departureDate")).ToShortDateString() %>'></asp:Label>
                                                            to
                                                            <asp:Label ID="Label2" runat="server" Text='<%# Convert.ToDateTime(Eval("arrivalDate")).ToShortDateString() %>'></asp:Label>
                                                        </small>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <!--//Display a list of announcements-->
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-outline-secondary-modal waves-effect btn-sm" data-dismiss="modal">
                                    Close</button>
                            </div>
                        </div>
                    </div>
                </div>
                <!--//Central Modal Large Warning-->

            </div>
            <div id="tabs-2">
                <asp:Repeater ID="RepeaterImmersionTrips" runat="server" OnItemCommand="Repeater1_ItemCommand" OnItemDataBound="RepeaterImmersionTrip_ItemDataBound">
                    <ItemTemplate>
                        <div class="row">
                            <div class="col-12 col-sm-12">
                                <div class="list-group">
                                    <div class="list-group-item list-group-item-action flex-column align-items-start">
                                        <div class="d-flex w-100 justify-content-between">
                                            <h5 class="mb-1">
                                                <asp:LinkButton ID="LinkButton2" runat="server" CommandName="trips_Click" CommandArgument='<%# Eval("tripID") %>' Text='<%#Eval("tripName") %>'>
                                                </asp:LinkButton>
                                            </h5>
                                            <small>
                                                <asp:Button ID="btnImmersionTripsWithdraw" runat="server" Text="Withdraw" Class="btn btn-warning btn-sm" CommandName="trips_Click" CommandArgument='<%# Eval("tripID") %>' OnClick="btnStudyTripsWithdraw_Click" OnCommand="btnStudyTrips_Command"  ForeColor="White"/>
                                                <asp:Button ID="btnImmersionTripsEdit" runat="server" Class="btn btn-warning btn-sm text-white" Text="Edit Trip" OnCommand="EditTrip_Command" CommandName="trips_Click" CommandArgument='<%# Eval("tripID") %>' />
                                                <asp:Button ID="btnImmersionTripsDelete" runat="server" Class="btn btn-danger btn-sm" Text="Delete" OnCommand="delete_Command" CommandName="trips_Click" CommandArgument='<%# Eval("tripID") %>' />
                                            </small>
                                        </div>
                                        <div class="d-flex w-100 justify-content-between">
                                            <small>From
                                            <asp:Label ID="Label3" runat="server" Text='<%# Convert.ToDateTime(Eval("departureDate")).ToShortDateString() %>'></asp:Label>
                                                to
                                            <asp:Label ID="Label4" runat="server" Text='<%# Convert.ToDateTime(Eval("arrivalDate")).ToShortDateString() %>'></asp:Label>
                                                (<asp:Label ID="lblTripStatus" runat="server" Text='<%# Eval("overseasTripStatus") %>'></asp:Label>)
                                            </small>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <br />
                <!--If repeater is empty-->
                <asp:Panel ID="PanelImmersionTrips" runat="server" Visible="False">
                    <div class="jumbotron jumbotron-fluid">
                        <div class="container">
                            <h1 class="display-5">You are currently not in any Immersion Trip programme.</h1>
                            <p class="lead">Keep a look out for future overseas programme!</p>
                        </div>
                    </div>
                </asp:Panel>
                <!--//If repeater is empty-->
                <center>
                    <button type="button" class="btn btn-warning btn-sm text-white" data-toggle="modal" data-target="#PastImmersionTrip">
                        View Past Trips</button>
                </center>
                <!--Central Modal Large Warning-->
                <div class="modal fade" id="PastImmersionTrip" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                    <div class="modal-dialog modal-lg modal-notify modal-warning" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="heading lead">Past Immersion Trips</h5>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true" class="white-text">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <asp:Label ID="lblPastImmersionTripMsg" runat="server" Text="Currently, there are no past Immersion Trips yet!"></asp:Label>
                                <!-- Display a list of announcements-->
                                <asp:Repeater ID="RepeaterPastImmersionTrips" runat="server" OnItemCommand="RepeaterPastStudyTrips_Command">
                                    <ItemTemplate>
                                        <div class="row">
                                            <div class="col-12 col-sm-12">
                                                <div class="list-group">
                                                    <div href="#" class="list-group-item list-group-item-action flex-column align-items-start">
                                                        <div class="d-flex w-100 justify-content-between">
                                                            <h5 class="mb-1">
                                                                <asp:LinkButton ID="LinkButton1" runat="server" CommandName="trips_Click" CommandArgument='<%# Eval("tripID") %>' Text='<%#Eval("tripName") %>'>
                                                                </asp:LinkButton>
                                                            </h5>

                                                        </div>
                                                        <small>From
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Convert.ToDateTime(Eval("departureDate")).ToShortDateString() %>'></asp:Label>
                                                            to
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Convert.ToDateTime(Eval("arrivalDate")).ToShortDateString() %>'></asp:Label>
                                                        </small>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <!--//Display a list of announcements-->
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-outline-secondary-modal waves-effect btn-sm" data-dismiss="modal">
                                    Close
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
                <!--//Central Modal Large Warning-->

            </div>
            <div id="tabs-3">
                <asp:Repeater ID="RepeaterInternship" runat="server" OnItemCommand="Repeater1_ItemCommand" OnItemDataBound="RepeaterImmersionTrip_ItemDataBound">
                    <ItemTemplate>
                        <div class="row">
                            <div class="col-12 col-sm-12">
                                <div class="list-group">
                                    <div class="list-group-item list-group-item-action flex-column align-items-start">
                                        <div class="d-flex w-100 justify-content-between">
                                            <h5 class="mb-1">
                                                <asp:LinkButton ID="LinkButton2" runat="server" CommandName="trips_Click" CommandArgument='<%# Eval("tripID") %>' Text='<%#Eval("tripName") %>'>
                                                </asp:LinkButton>
                                            </h5>
                                            <small>
                                                <asp:Button ID="btnImmersionTripsWithdraw" runat="server" Text="Withdraw" Class="btn btn-warning btn-sm" CommandName="trips_Click" CommandArgument='<%# Eval("tripID") %>' OnClick="btnStudyTripsWithdraw_Click" OnCommand="btnStudyTrips_Command"  ForeColor="White"/>
                                                <asp:Button ID="btnImmersionTripsEdit" runat="server" Class="btn btn-warning btn-sm text-white" Text="Edit Trip" OnCommand="EditTrip_Command" CommandName="trips_Click" CommandArgument='<%# Eval("tripID") %>' />
                                                <asp:Button ID="btnImmersionTripsDelete" runat="server" Class="btn btn-danger btn-sm" Text="Delete" OnCommand="delete_Command" CommandName="trips_Click" CommandArgument='<%# Eval("tripID") %>' />
                                            </small>
                                        </div>
                                        <div class="d-flex w-100 justify-content-between">
                                            <small>From
                                            <asp:Label ID="Label3" runat="server" Text='<%# Convert.ToDateTime(Eval("departureDate")).ToShortDateString() %>'></asp:Label>
                                                to
                                            <asp:Label ID="Label4" runat="server" Text='<%# Convert.ToDateTime(Eval("arrivalDate")).ToShortDateString() %>'></asp:Label>
                                                (<asp:Label ID="lblTripStatus" runat="server" Text='<%# Eval("overseasTripStatus") %>'></asp:Label>)
                                            </small>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <br />
                <!--If repeater is empty-->
                <asp:Panel ID="PanelInternship" runat="server" Visible="False">
                    <div class="jumbotron jumbotron-fluid">
                        <div class="container">
                            <h1 class="display-5">You are currently not in any Internship programme.</h1>
                            <p class="lead">Keep a look out for future overseas programme!</p>
                        </div>
                    </div>
                </asp:Panel>
                <!--//If repeater is empty-->
                <center>
                    <button type="button" class="btn btn-warning btn-sm text-white" data-toggle="modal" data-target="#PastInternshipTrip">
                        View Past Trips</button>
                </center>
                <!--Central Modal Large Warning-->
                <div class="modal fade" id="PastInternshipTrip" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                    <div class="modal-dialog modal-lg modal-notify modal-warning" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="heading lead">Past Internship Trips</h5>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true" class="white-text">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <asp:Label ID="lblPastInternship" runat="server" Text="Currently, there are no past Internship Trips yet!"></asp:Label>
                                <!-- Display a list of announcements-->
                                <asp:Repeater ID="RepeaterPastInternship" runat="server" OnItemCommand="RepeaterPastStudyTrips_Command">
                                    <ItemTemplate>
                                        <div class="row">
                                            <div class="col-12 col-sm-12">
                                                <div class="list-group">
                                                    <div href="#" class="list-group-item list-group-item-action flex-column align-items-start">
                                                        <div class="d-flex w-100 justify-content-between">
                                                            <h5 class="mb-1">
                                                                <asp:LinkButton ID="LinkButton1" runat="server" CommandName="trips_Click" CommandArgument='<%# Eval("tripID") %>' Text='<%#Eval("tripName") %>'>
                                                                </asp:LinkButton>
                                                            </h5>

                                                        </div>
                                                        <small>From
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Convert.ToDateTime(Eval("departureDate")).ToShortDateString() %>'></asp:Label>
                                                            to
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Convert.ToDateTime(Eval("arrivalDate")).ToShortDateString() %>'></asp:Label>
                                                        </small>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <!--//Display a list of announcements-->
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-outline-secondary-modal waves-effect btn-sm" data-dismiss="modal">
                                    Close
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
                <!--//Central Modal Large Warning-->
            </div>
        </div>

        <asp:Label ID="lblTesting" runat="server" Text="Label" Visible="false"></asp:Label>


    </p>
    <!--//Page Content-->
</asp:Content>
