<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="CreateAnnouncement.aspx.cs" Inherits="ITP213.CreateAnnouncement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Breadcrumbs-->
    <ol class="breadcrumb">

        <li class="breadcrumb-item">

            <a href="/Default.aspx" style="color: #D6D6D6">Home</a>
        </li>
        <li class="breadcrumb-item active">
            <asp:Label ID="lblTitle" runat="server" Text="Create Announcement"></asp:Label></li>
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
        <asp:Label ID="lblTitle2" runat="server" Text="Create Announcement"></asp:Label></h1>
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
    <div id="demo">
        <div class="step-app">
            <ul class="step-steps">
                <li><a href="#step1">Step 1</a></li>
                <li><a href="#step2">Step 2</a></li>
            </ul>
            <div class="step-content">
                <div class="step-tab-panel" id="step1">
                    <div class="form-group">
                        <div class="row">
                            <div class="col-6">
                                Trip Name:
                                <asp:DropDownList ID="ddlTripName" runat="server" class="form-control">
                                </asp:DropDownList>
                            </div>
                            <div class="col-6">
                                Title:<asp:TextBox ID="tbTitle" runat="server" class="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        Message:<asp:TextBox ID="tbMessage" runat="server" Rows="8" Columns="10" TextMode="MultiLine" class="form-control" style="resize:none;"></asp:TextBox>
                    </div>
                </div>
                <div class="step-tab-panel" id="step2">
                    <div class="form-group">
                        <div class="row">
                            <div class="col-6">
                                Time for the message to stop showing:<br />
                                <asp:TextBox ID="tbTimeDue" runat="server" class="form-control" ClientIDMode="Static"></asp:TextBox>
                            </div>
                            <div class="col-6">
                                Who can view this announcement in this group: <asp:Label ID="lblCbValidation" runat="server" Text="*" ForeColor="Red" Visible="false"></asp:Label><br />
                                <asp:CheckBox ID="cbStudents" runat="server" Text="Students" class="form-check-label" />
                                <br />
                                <asp:CheckBox ID="cbLecturers" runat="server" Text="Travelling lecturers" class="form-check-label" checked="true" Visible="false"/>
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="step-footer">
                <button data-direction="prev" class="step-btn">Previous</button>
                <button data-direction="next" class="step-btn" style="background-color:#4CAF50 !important; color:white !important;">Next</button>
                <button data-direction="finish" class="step-btn" runat="server"  OnServerClick="btnCreate_Click" style="background-color:#32C5D2 !important; color:white !important;">Create</button>
                <!--<asp:Button ID="btnCreate" data-direction="finish" runat="server" class="step-btn" Text="Create" OnClick="btnCreate_Click" />-->
            </div>
        </div>
    </div>  
    

    <script>
        /* Brazilian initialisation for the jQuery UI date picker plugin. */
        /* Written by Leonildo Costa Silva (leocsilva@gmail.com). */
        (function (factory) {
            if (typeof define === "function" && define.amd) {

                // AMD. Register as an anonymous module.
                define(["../datepicker"], factory);
            } else {

                // Browser globals
                factory(jQuery.datepicker);
            }
        }(function (datepicker) {

            datepicker.regional['SG'] = {
                closeText: 'Confirm',
                prevText: '&#x3C;Previous',
                nextText: 'Next&#x3E;',
                currentText: 'Hoje',
                monthNames: ['January', 'February', 'March', 'April', 'March', 'June',
                'July', 'August', 'September', 'October', 'November', 'December'],
                monthNamesShort: ['Jan', 'Feb', 'Mar', 'Apr', 'Mar', 'Jun',
                'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
                dayNames: ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'],
                dayNamesShort: ['Sun', 'Mon', 'Tues', 'Wed', 'Thurs', 'Fri', 'Sat'],
                dayNamesMin: ['Sun', 'Mon', 'Tues', 'Wed', 'Thurs', 'Fri', 'Sat'],
                weekHeader: 'Sm',
                dateFormat: 'dd/mm/yy',
                firstDay: 0,
                isRTL: false,
                showMonthAfterYear: false,
                yearSuffix: '',
                minDate: 1
            };
            datepicker.setDefaults(datepicker.regional['SG']);

            return datepicker.regional['SG'];

        }));

        (function ($) {
            $.timepicker.regional['SG'] = {
                timeOnlyTitle: 'Escolha o horÃ¡rio',
                timeText: 'Time',
                hourText: 'Hour',
                minuteText: 'Minutes',
                secondText: 'Seconds',
                millisecText: 'Miliseconds',
                microsecText: 'Microsecends',
                timezoneText: 'Fuso horÃ¡rio',
                currentText: 'Current Timing',
                closeText: 'Confirm',
                timeFormat: 'HH:mm',
                amNames: ['a.m.', 'AM', 'A'],
                pmNames: ['p.m.', 'PM', 'P'],
                isRTL: false
            };
            $.timepicker.setDefaults($.timepicker.regional['SG']);
        })(jQuery);
        $('#tbTimeDue').datetimepicker();
    </script>
    <script src="http://code.jquery.com/jquery-latest.min.js"></script>
    <script src="Scripts/jquery-steps.js"></script>
    <script>
        $('#demo').steps({
            onFinish: function () {
                //alert('Wizard Completed');
            }
        });
    </script>

</asp:Content>
