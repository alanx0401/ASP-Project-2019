<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="ReportInjury.aspx.cs" Inherits="ITP213.ReportInjury" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Breadcrumbs-->
    <ol class="breadcrumb">

        <li class="breadcrumb-item">

            <a href="#" style="color: #D6D6D6">Home</a>
        </li>
        <li class="breadcrumb-item active">Report Injury</li>
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
    <h1>Report Injury<asp:Label ID="lblUpdate" runat="server" Text="Finish" Visible="false"></asp:Label></h1>
    <!--2. Change the title!-->
    <hr />
    <p>
        <!--3. This is where you code all your features-->

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
                <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
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
            <div class="step-app">
                <asp:HiddenField ID="tab_index" Value="0" runat="server" />
                <ul class="step-steps">
                    <li><a href="#tabs-1">Step 1</a></li>
                    <li><a href="#tabs-2">Step 2</a></li>
                    <li><a href="#tabs-3">Step 3</a></li>
                    <li><a href="#tabs-4">Step 4</a></li>
                    <li><a href="#tabs-5">Step 5</a></li>
                </ul>
                <div class="step-content">
                    <div class="step-tab-panel" id="tabs-1">
                        <fieldset>
                            <legend>Details of injured person:</legend>
                            <div class="row">
                                <div class="col-4">
                                    Injury Severity:
                                    <asp:DropDownList ID="ddlInjurySeverity" runat="server" class="form-control">
                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                        <asp:ListItem Value="#FFC9C7">High</asp:ListItem>
                                        <asp:ListItem Value="#d6ffda">Normal</asp:ListItem>
                                        <asp:ListItem Value="#c8d7fa">Low</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-4">
                                    Trip:
                                    <asp:DropDownList ID="ddlTrip" class="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTrip_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="col-4">
                                    <asp:Label ID="lblName" runat="server" Text="Name:" Visible="false"></asp:Label>
                                    <asp:DropDownList ID="ddlName" class="form-control" runat="server" AutoPostBack="True" Visible="false"></asp:DropDownList>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <div class="step-tab-panel" id="tabs-2">
                        <fieldset>
                            <legend>Details of incident:</legend>
                            <div class="row">
                                <div class="col-4">
                                    Date and time of injury:
                                    <asp:TextBox ID="tbDateTimeOfInjury" class="form-control" runat="server" ClientIDMode="Static"></asp:TextBox>
                                </div>
                                <div class="col-4">
                                    Location:
                                    <asp:TextBox ID="tbLocation" class="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-4">
                                    Description:
                                    <asp:TextBox ID="tbDescription" class="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <div class="step-tab-panel" id="tabs-3">
                        <fieldset>
                            <legend>Details of witness:</legend>
                            <div class="row">
                                <div class="col-6">
                                    Witness name:
                                    <asp:TextBox ID="tbWitnessName" class="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-6">
                                    Witness Phone:
                                    <asp:TextBox ID="tbWitnessPhone" class="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <div class="step-tab-panel" id="tabs-4">
                        <fieldset>
                            <legend>Details of injury:</legend>
                            <div class="row">
                                <div class="col-4">
                                    Nature of injury(eg burn, cut, sprain):
                                    <asp:TextBox ID="tbNatureOfInjury" class="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-4">
                                    Cause of injury(eg fall, grabbed by person):
                                    <asp:TextBox ID="tbCauseOfInjury" class="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-4">
                                    Select Image:
                                    <asp:Label ID="lblImageValidation" runat="server" Text="*" ForeColor="Red" Visible="false"></asp:Label>
                                    <asp:FileUpload ID="FileUploadImage" runat="server" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-6">
                                    Location on body(eg back, left forearm):
                                    <asp:TextBox ID="tbLocationOfBody" class="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-6">
                                    Agency(eg lounge chair, another person, hot water)
                                    <asp:TextBox ID="tbAgency" class="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <div class="step-tab-panel" id="tabs-5">
                        <fieldset>
                            <legend>Treatment Administered:</legend>
                            <div class="row">
                                <div class="col">
                                    First Aid given
                                    <asp:Label ID="lblRbValidation" runat="server" Text="*" ForeColor="Red" Visible="false"></asp:Label>
                                    <asp:RadioButtonList ID="rbFirstAidGiven" runat="server">
                                        <asp:ListItem>Yes</asp:ListItem>
                                        <asp:ListItem>No</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-6">
                                    First Aider name:
                                    <asp:TextBox ID="tbFirstAiderName" class="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-6">
                                    Treatment:
                                    <asp:TextBox ID="tbTreatment" class="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                </div>
                <div class="step-footer">
                    <button data-direction="prev" class="step-btn">Previous</button>
                    <button data-direction="next" class="step-btn" style="background-color:#4CAF50 !important; color:white !important;">Next</button>
                    <button data-direction="finish" class="step-btn" runat="server"  OnServerClick="btnSubmit_Click" style="background-color:#32C5D2 !important; color:white !important;">Create</button>
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
                    maxDate: 0
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
            $('#tbDateTimeOfInjury').datetimepicker();
        </script>
        <script src="http://code.jquery.com/jquery-latest.min.js"></script>
        <script src="Scripts/jquery-steps.js"></script>
        <script>
            //*************** how do I ensure PART A runs first before PART B?
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

        <asp:Label ID="Label1" runat="server" Text="" ForeColor="White"></asp:Label>

    

    </p>
    <!--//Page Content-->
</asp:Content>
