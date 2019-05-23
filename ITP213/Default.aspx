<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ITP213.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ol class="breadcrumb">
        <li class="breadcrumb-item active" style="color: #031A82; cursor: pointer;">Home
        </li>
    </ol>
    <style>
        .breadcrumb {
            background-color: #FFFFFF !important;
        }

        h2 {
            font-size: 24px;
            text-transform: uppercase;
            color: #303030;
            font-weight: 600;
            margin-bottom: 30px;
        }

        h4 {
            font-size: 19px;
            line-height: 1.375em;
            color: #303030;
            font-weight: 400;
            margin-bottom: 30px;
        }

        .item h4 {
            font-size: 19px;
            line-height: 1.375em;
            font-weight: 400;
            font-style: italic;
            margin: 70px 0;
        }

        .item span {
            font-style: normal;
        }

        .serviceIcons {
            font-size: 50px;
            margin-bottom: 20px;
            color: #f4511e;
        }
    </style>
    <!-- Container (About Section) -->
    <div id="about" class="container-fluid">
        <div class="row">
            <div class="col-sm-12">
                <h2 style="text-align: center">About The System</h2>
                <br />
                <h4 style="text-align: center">This system is called the Student Overseas Trip Management System (SOTMS). This system will manage different types of overseas programmes, which are Study Trips, Immersion Programs and Internships.</h4>
                <br />
            </div>
        </div>
    </div>
    <!-- Container (Services Section) -->
    <div id="services" class="container-fluid text-center">
        <h2>SERVICES</h2>
        <h4>What we offer</h4>
        <br>
        <div class="row">
            <div class="col-sm-3">
                <span class="fas fa-pencil-alt serviceIcons"></span>
                <h4>Blog</h4>
                <p>Students can write about their trip</p>
            </div>
            <div class="col-sm-3">
                <span class="fas fa-sign-out-alt serviceIcons"></span>
                <h4>Withdrawal Request</h4>
                <p>Students can make withdrawal requests</p>
            </div>
            <div class="col-sm-3">
                <span class="fas fa-images serviceIcons"></span>
                <h4>Image Gallery</h4>
                <p>Students can upload trip images</p>
            </div>
            <div class="col-sm-3">
                <span class="far fa-angry serviceIcons"></span>
                <h4>Complaints</h4>
                <p>Students can make complaints</p>
            </div>
        </div>
        <br>
        <br>
        <div class="row">
            <div class="col-sm-3">
                <span class="fas fa-users serviceIcons"></span>
                <h4>Trip Assignment</h4>
                <p>Lecturers can assign students to trips</p>
            </div>
            <div class="col-sm-3">
                <span class="fas fa-chart-pie serviceIcons"></span>
                <h4>Statistics</h4>
                <p>Lecturers can view trip statistics</p>
            </div>
            <div class="col-sm-3">
                <span class="fas fa-user-injured serviceIcons"></span>
                <h4 style="color: #303030;">Injury Report</h4>
                <p>Lecturers can make injury reports</p>
            </div>
            <div class="col-sm-3">
                <span class="fas fa-info serviceIcons"></span>
                <h4 style="color: #303030;">Student Particulars</h4>
                <p>Lecturers can view student particulars</p>
            </div>
        </div>
        <br>
        <br>
        <div class="row">
            <div class="col-sm-3">
            </div>
            <div class="col-sm-3">
                <span class="fas fa-bell serviceIcons"></span>
                <h4 style="color: #303030;">Emergency Alert</h4>
                <p>Parents can receive alerts</p>
            </div>
            <div class="col-sm-3">
                <span class="fas fa-book-open serviceIcons"></span>
                <h4>Tests</h4>
                <p>Students can take tests and lecturers give a grade</p>
            </div>
            <div class="col-sm-3">
            </div>
        </div>
    </div>
    <br />
    <!--//Page Content-->
</asp:Content>
