<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="StudentParticulars.aspx.cs" Inherits="ITP213.StudentParticulars" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Breadcrumbs-->
    <ol class="breadcrumb">
        <li class="breadcrumb-item">
            <a href="#" style="color:#D6D6D6">Home</a>
        </li>
        <li class="breadcrumb-item active">View Student Particulars</li> <!--1. Change the name!-->
    </ol>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <style>
        .breadcrumb
        {
        background-color: #FFFFFF !important;
            
        }
        .breadcrumb > .breadcrumb-item
        {
        color: #031A82 !important;
        }
        .breadcrumb .breadcrumb-item+.breadcrumb-item::before{
            color: #D6D6D6;
        }
    </style>
    <!-- //Breadcrumbs end-->
    <!-- Page Content -->
    <h1>View Student Particulars</h1> <!--2. Change the title!-->
    <hr/>
    <div class="row">
        <div class="container col-lg-6">
            <asp:Panel ID="PanelErrorResult" Visible="false" runat="server" CssClass="alert alert-dismissable alert-danger">
                <button type="button" class="close" data-dismiss="alert">
                    <span aria-hidden="true">&times;</span>
                </button>
                <asp:Label ID="Lbl_err" runat="server"></asp:Label>
            </asp:Panel>
        </div>
    </div>
    <div class="row">
        <div class="container col-lg-6">
            <div class="card">
                <div class="card-header">Search</div>
                <div class="card-body">
                    <div class="form-group">
                        <asp:Label ID="Label1" runat="server" Text="Student Admin Number:"></asp:Label>
                        <asp:TextBox ID="tbAdminNo" runat="server" CssClass="styledl form-control" Width="250px"></asp:TextBox>
                        <br />
                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-basic" Text="Get" Width="65px" OnClick="btnGetStudent_Click"/>
                    </div>
                </div> 
            </div>
        </div>
    </div>
    <br />
    <asp:Panel ID="PanelInfo" Visible="false" runat="server">
        <div class="row">
            <div class="container col-lg-7">
                <ul class="nav nav-tabs">
                    <li class="nav-item">
                        <a class="nav-link active" data-toggle="tab" href="#home">General Information</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" data-toggle="tab" href="#menu1">Academic Results</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" data-toggle="tab" href="#menu2">CCA Records</a>
                    </li>
                </ul>
                <!-- Tab panes -->
                <div class="tab-content">
                    <div id="home" class="container tab-pane active"><br/>
                        <h4>General Information</h4>
                        <asp:Panel ID="PanelGeneralRecord" Visible="false" runat="server">
                            <div class="row">
                                <label for="Lbl_StudName" class="col-lg-2 col-form-label">Name:</label>
                                <div class="col-lg-4">
                                    <asp:Label ID="Lbl_StudName" runat="server"></asp:Label>
                                </div>
                                <label for="Lbl_AdminNo" class="col-lg-2 col-form-label">Admin No:</label>
                                <div class="col-lg-4">
                                    <asp:Label ID="Lbl_AdminNo" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <label for="Lbl_Sch" class="col-lg-2 col-form-label">School:</label>
                                <div class="col-lg-4">
                                    <asp:Label ID="Lbl_Sch" runat="server"></asp:Label>
                                </div>
                                <label for="Lbl_Course" class="col-lg-2 col-form-label">Course:</label>
                                <div class="col-lg-4">
                                    <asp:Label ID="Lbl_Course" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <label for="Lbl_Email" class="col-lg-2 col-form-label">Email:</label>
                                <div class="col-lg-4">
                                    <asp:Label ID="Lbl_Email" runat="server"></asp:Label>
                                </div>
                                <label for="Lbl_DOB" class="col-lg-2 col-form-label">DOB:</label>
                                <div class="col-lg-4">
                                    <asp:Label ID="Lbl_DOB" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <label for="Lbl_DietNeeds" class="col-lg-2 col-form-label">Diet Needs:</label>
                                <div class="col-lg-4">
                                    <asp:Label ID="Lbl_DietNeeds" runat="server"></asp:Label>
                                </div>
                                <label for="Lbl_Lang" class="col-lg-2 col-form-label">Spoken Language:</label>
                                <div class="col-lg-4">
                                    <asp:Label ID="Lbl_Lang" runat="server"></asp:Label>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                    <div id="menu1" class="container tab-pane fade"><br/>
                        <h4>Academic Results</h4>
                        <asp:Panel ID="PanelAcademicResult" Visible="false" runat="server">
                            <div class="row">
                                <label for="Lbl_StudNameAcad" class="col-lg-2 col-form-label">Name:</label>
                                <div class="col-lg-4">
                                    <asp:Label ID="Lbl_StudNameAcad" runat="server"></asp:Label>
                                </div>
                                <label for="Lbl_AdminNoAcad" class="col-lg-2 col-form-label">Admin No:</label>
                                <div class="col-lg-4">
                                    <asp:Label ID="Lbl_AdminNoAcad" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <label for="Lbl_CumGPA" class="col-lg-2 col-form-label">Cumulative GPA:</label>
                                <div class="col-lg-4">
                                    <asp:Label ID="Lbl_CumGPA" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-4">
                                    <label style="text-decoration: underline">Year 1</label>
                                </div>
                                <div class="col-lg-4">
                                </div>
                            </div>
                            <div class="row">
                                <label for="Lbl_Sem1Year1GPA" class="col-lg-2 col-form-label">Semester 1:</label>
                                <div class="col-lg-4">
                                    <asp:Label ID="Lbl_Sem1Year1GPA" runat="server"></asp:Label>
                                </div>
                                <label for="Lbl_Sem2Year1GPA" class="col-lg-2 col-form-label">Semester 2:</label>
                                <div class="col-lg-4">
                                    <asp:Label ID="Lbl_Sem2Year1GPA" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-4">
                                    <label style="text-decoration: underline">Year 2</label>
                                </div>
                            </div>
                            <div class="row">
                                <label for="Lbl_Sem1Year2GPA" class="col-lg-2 col-form-label">Semester 1:</label>
                                <div class="col-lg-4">
                                    <asp:Label ID="Lbl_Sem1Year2GPA" runat="server"></asp:Label>
                                </div>
                                <label for="Lbl_Sem2Year2GPA" class="col-lg-2 col-form-label">Semester 2:</label>
                                <div class="col-lg-4">
                                    <asp:Label ID="Lbl_Sem2Year2GPA" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-4">
                                    <label style="text-decoration: underline">Year 3</label>
                                </div>
                                <div class="col-lg-4">
                                </div>
                            </div>
                            <div class="row">
                                <label for="Lbl_Sem1Year3GPA" class="col-lg-2 col-form-label">Semester 1:</label>
                                <div class="col-lg-4">
                                    <asp:Label ID="Lbl_Sem1Year3GPA" runat="server"></asp:Label>
                                </div>
                                <label for="Lbl_Sem2Year3GPA" class="col-lg-2 col-form-label">Semester 2:</label>
                                <div class="col-lg-4">
                                    <asp:Label ID="Lbl_Sem2Year3GPA" runat="server"></asp:Label>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                    <div id="menu2" class="container tab-pane fade"><br/>
                        <h4>CCA Records</h4>
                        <asp:Panel ID="PanelCCARecord" Visible="false" runat="server">
                            <div class="row">
                                <label for="Lbl_StudNameCCA" class="col-lg-2 col-form-label">Name:</label>
                                <div class="col-lg-4">
                                    <asp:Label ID="Lbl_StudNameCCA" runat="server"></asp:Label>
                                </div>
                                <label for="Lbl_AdminNoCCA" class="col-lg-2 col-form-label">Admin No:</label>
                                <div class="col-lg-4">
                                    <asp:Label ID="Lbl_AdminNoCCA" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <label for="Lbl_Classification" class="col-lg-2 col-form-label">Classification:</label>
                                <div class="col-lg-4">
                                    <asp:Label ID="Lbl_Classification" runat="server"></asp:Label>
                                </div>
                                <label for="Lbl_AchievementTitle" class="col-lg-2 col-form-label">Activity:</label>
                                <div class="col-lg-4">
                                    <asp:Label ID="Lbl_AchievementTitle" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <label for="Lbl_Date" class="col-lg-2 col-form-label">Date:</label>
                                <div class="col-lg-4">
                                    <asp:Label ID="Lbl_Date" runat="server"></asp:Label>
                                </div>
                                <label for="Lbl_Points" class="col-lg-2 col-form-label">Points:</label>
                                <div class="col-lg-4">
                                    <asp:Label ID="Lbl_Points" runat="server"></asp:Label>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    <br />
    <!--//Page Content-->
</asp:Content>
