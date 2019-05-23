<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="CreateComplaints.aspx.cs" Inherits="ITP213.CreateComplaints" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--************README: Hi, please change some of the following things below when you're coding your features. Thanks! -PS -->

    <!-- Breadcrumbs-->
    <ol class="breadcrumb">
        <li class="breadcrumb-item">
            <a href="/Default.aspx" style="color: #D6D6D6">Home</a>
        </li>

        <li class="breadcrumb-item active">New Complaints</li>
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

        .auto-style2 {
            width: 37%;
        }

        @media (max-width:40em) {
            table, thead, tbody, tfoot, th, td, tr {
                display: block;
            }

                tr + tr {
                    margin-top: 1em;
                }
        }
        .auto-style3 {
            width: 134px;
        }
    </style>
    <!-- //Breadcrumbs end-->

    <!-- Page Content -->
    <h1>New Complaints</h1>
    <!--2. Change the title!-->
    <hr />
    <table>
        <tr>
            <td class="auto-style3">Subject </td>
            <td>
                <asp:TextBox ID="txtBoxSubject" runat="server" class="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtBoxSubject" ErrorMessage="Please fill in information" ForeColor="#996600"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="auto-style3">Trip Name:</td>
            <td>
                <asp:DropDownList ID="ddlTripName" runat="server" class="form-control">
                    <asp:ListItem>--Select--</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlTripName" ErrorMessage="Select a trip" ForeColor="#CC3300" InitialValue="--Select--"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="auto-style3">Complain 
               
                <br />
                Type:</td>
            <td>
                <asp:TextBox ID="txtBoxComplainType" runat="server" class="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtBoxComplainType" ErrorMessage="Please fill in information" ForeColor="#996600"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="auto-style3"></td>
            <td>
                <asp:TextBox ID="txtBoxComments" runat="server" Rows="8" Columns="10" TextMode="MultiLine" class="form-control" Style="resize: none"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtBoxComments" ErrorMessage="Please fill in information" ForeColor="#996600"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="auto-style3">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnPublish_Click" CssClass="btn btn-primary" /></td>
            <td>
                <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#myModal">Upload Pictures</button></td>
        </tr>
    </table>
    <!-- The Modal -->
    <div class="modal fade" id="myModal">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <!--Modal Header -->
                <div class="modal-header">
                    <h4 class="modal-title">Upload Picture</h4>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <!-- Modal body -->
                <div class="modal-body">
                    <table style="margin-left: auto; margin-right: auto;" class="auto-style2">
                        <tr>
                            <td>
                                <asp:FileUpload ID="FileUpload1" runat="server" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="FileUpload1" runat="server" ForeColor="#996600" ErrorMessage="Please upload a file"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </div>
                <!-- Modal Footer -->
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
    <asp:Label ID="Label2" runat="server" Text="Label" Visible="False"></asp:Label>
    <br />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="#CC3300" />
    <br />
</asp:Content>
