<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="LecturerWithdrawalRequest.aspx.cs" Inherits="ITP213.LecturerWithdrawalRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <link rel='stylesheet' href='Content/mdb1.min.css' />
    <!-- Breadcrumbs-->
    <ol class="breadcrumb">

        <li class="breadcrumb-item">

            <a href="/Default.aspx" style="color: #D6D6D6">Home</a>
        </li>
        <li class="breadcrumb-item active">Withdrawal Request</li>
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

        .tablink {
            background-color: #555;
            color: white;
            float: left;
            border: none;
            outline: none;
            cursor: pointer;
            padding: 14px 16px;
            font-size: 17px;
            width: 50%;
        }

            .tablink:hover {
                background-color: #777;
            }

        @media only screen and (max-width: 600px) {
            .tablink {
                width: 100%;
            }
        }
    </style>
    <!-- //Breadcrumbs end-->

    <!-- Page Content -->
    <h1>Withdrawal Request</h1>
    <!--2. Change the title!-->
    <hr />
    <p>
        <script>
            $(function () {
                $("#tabs").tabs();
            });
        </script>
        <% String currentAccountType = Session["accountType"].ToString();%>
        <div id="tabs">
            <ul>
                <button class="tablink" onclick="location.href='/ViewAnnouncement.aspx';return false;">
                    <li><a href="#tabs-1" style="text-align: center; color: white" >Announcements</a></li>
                </button>
                <% if (currentAccountType == "lecturer")
                    {%>
                <button class="tablink" style="background-color: orange;">
                    <li><a href="#tabs-2">Withdrawal Request</a></li>
                </button>
                <%} %>
            </ul>
            <div id="tabs-1">
            </div>
            <div id="tabs-2">
                <p><asp:Repeater ID="RepeaterWithdrawalRequest" runat="server" OnItemCommand="RepeaterWithdrawalRequest_ItemCommand" OnItemDataBound="RepeaterWithdrawalRequest_ItemDataBound">
                        <ItemTemplate>
                            <div class="list-group">
                                <div href="#" class="list-group-item list-group-item-action flex-column align-items-start">
                                    <div class="d-flex w-100 justify-content-between">
                                        <h5 class="mb-1">
                                            <asp:LinkButton ID="LinkButton1" runat="server" CommandName="trips_Click" CommandArgument='<%# Eval("withdrawTripRequestID") %>'
                                                Text='<%#Eval("tripNameAndTripType") %>' data-toggle="tooltip" data-html="true" data-placement="right"
                                                title='<%#"From "+Convert.ToDateTime(Eval("departureDate")).ToShortDateString() + " to "+ Convert.ToDateTime(Eval("arrivalDate")).ToShortDateString() %>'>
                                            </asp:LinkButton>
                                        </h5>
                                        <small>
                                            <button data-toggle="modal" data-target="#centralModalLGInfoDemo<%# Eval("withdrawTripRequestID") %>" type="button" class="btn btn-warning btn-sm text-white">
                                                View Withdrawal Request
                                            </button>

                                        </small>
                                    </div>
                                    <div class="d-flex w-100 justify-content-between">
                                        <p class="mb-1">
                                            <!--<asp:Label ID="Label3" runat="server" Text='<%# Eval("withdrawalReason") %>'></asp:Label>-->
                                            Requested by:
                                            <asp:Label ID="Label4" runat="server" Text='<%# Eval("adminNo") %>'></asp:Label>
                                        </p>
                                        <small>
                                            <!--(Convert.ToDateTime(Eval("soDateTo"))).ToShortDateString()-->
                                            Posted on:
                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("createdOn") %>'></asp:Label>
                                        </small>
                                    </div>
                                </div>
                            </div>
                            <!-- Central Modal Large Info-->
                            <div class="modal fade" id="centralModalLGInfoDemo<%# Eval("withdrawTripRequestID") %>" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                                <div class="modal-dialog modal-notify modal-warning modal-top" role="document">
                                    <!--Content-->
                                    <div class="modal-content">
                                        <!--Header-->
                                        <div class="modal-header">
                                            <p class="heading lead text-white">
                                                <asp:Label ID="Label6" runat="server" Text='Withdrawal Request'></asp:Label>
                                            </p>

                                            <button type="button" class="close text-white" data-dismiss="modal" aria-label="Close">
                                                <span aria-hidden="true" class="white-text">&times;</span>
                                            </button>
                                        </div>

                                        <!--Body-->
                                        <div class="modal-body">
                                            <div class="text-center">
                                                <i class="fas fa-file-alt fa-4x mb-3 animated rotateIn" style="color: #FF941A"></i>
                                                <p>
                                                    <fieldset visible="true" style="width: auto !important; margin-bottom: 0px !important; font-size: 12px; font-weight: bold;" class="text-left">
                                                        <table style="border: 0;" class="text-left table borderless" style="border: none; background-color: rgba(210,130,240, 0.3) !important;">
                                                            <tr>
                                                                <td>
                                                                    <div style="font-size: 15px; font-weight: bold;">
                                                                        Reasons:
                                                                    </div>
                                                                    <asp:Label ID="Label5" runat="server" Text='<%# Eval("withdrawalReason") %>'></asp:Label>
                                                                    <br />
                                                                    <br />
                                                                    <asp:Label ID="lblTripStatus2" runat="server" Text='Status:'></asp:Label>
                                                                    <asp:Label ID="lblTripStatus" runat="server" Text='<%# Eval("withdrawalTripRequestStatus") %>'></asp:Label>
                                                                    <asp:Button ID="btnApproved" runat="server" Text="Approve" class="btn btn-success btn-sm" OnClick="btnApproved_Click" CommandName="trips_Click" CommandArgument='<%# Eval("withdrawTripRequestID") %>' OnCommand="btnApproved_Command" />
                                                                    <asp:Button ID="btnRejected" runat="server" Text="Reject" class="btn btn-danger btn-sm" OnClick="btnRejected_Click" CommandName="trips_Click" CommandArgument='<%# Eval("withdrawTripRequestID") %>' OnCommand="btnRejected_Command" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </fieldset>
                                                </p>
                                            </div>
                                        </div>

                                        <!--Footer-->
                                        <div class="modal-footer">
                                            
                                            <asp:Button ID="Button1" runat="server" class="btn btn-outline-secondary-modal waves-effect btn-sm" data-dismiss="modal" Text="Close" />
                                        </div>
                                    </div>
                                    <!--/.Content-->
                                </div>
                            </div>
                            <!-- Central Modal Large Info-->
                        </ItemTemplate>
                    </asp:Repeater>
                    
                    <!--If repeater is empty-->
                    <asp:Panel ID="PanelEmptyWithdrawalRequest" runat="server">
                        <div class="jumbotron jumbotron-fluid">
                            <div class="container">
                                <h1 class="display-4">No withdrawal request</h1>
                                <p class="lead">Currently, no student has requested for any withdrawal request.</p>
                            </div>
                        </div>
                    </asp:Panel>
                    <!--//If repeater is empty-->
                </p>
            </div>
        </div>  
        <asp:Label ID="lblTesting" runat="server" Text="Label" Visible="false"></asp:Label>

        
    </p>
    <script>
        $(function () {
            $('[data-toggle="tooltip"]').tooltip()
        })

        $('tabs').tabs({ active: 1 });
    </script>
    <!--//Page Content-->
</asp:Content>
