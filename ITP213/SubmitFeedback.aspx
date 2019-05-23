<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="SubmitFeedback.aspx.cs" Inherits="ITP213.SubmitFeedback" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Breadcrumbs-->
    <ol class="breadcrumb">
        <li class="breadcrumb-item">
            <a href="#" style="color:#D6D6D6">Home</a>
        </li>
        <li class="breadcrumb-item active">Submit Feedback</li>
    </ol>
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
        .auto-style1 {
            height: 32px;
        }
    .auto-style2 {
        height: 25px;
    }
    </style>
    <!-- //Breadcrumbs end-->

    <!-- Page Content -->
    <h1>Submit Feedback</h1>
    <hr/>
    <div class="container mt-5 mb-4">
        <div class="card pl-3 pr-3">
            <div class="pb-5 text-left m-md-3">
                <div class="container" style="overflow-x:auto;">
                    <div class="row">
                    <table class="table">
                    <tr>
                        <td class="auto-style1">Please select your trip :</td>
                        <td>
                             <asp:DropDownList ID="ddlTripName" runat="server"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style1">
                            &nbsp;</td>
                        <td>
                            &nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style1">Did you enjoy your overseas trip ? :</td>
                        <td>
                             <asp:DropDownList ID="EnjoyDropDown" runat="server" Width="186px">
                                <asp:ListItem>---Select---</asp:ListItem>
                                <asp:ListItem>Not at all</asp:ListItem>
                                <asp:ListItem>A little bit</asp:ListItem>
                                <asp:ListItem>It was fine</asp:ListItem>
                                <asp:ListItem>Enjoyed it</asp:ListItem>
                                <asp:ListItem>It was awesomez</asp:ListItem>
                             </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style1">The lodging in the program is adequate and comfortable :</td>
                        <td>
                             <asp:DropDownList ID="LodgingDropDown" runat="server">
                                <asp:ListItem>---Select---</asp:ListItem>
                                <asp:ListItem>Strongly Agree</asp:ListItem>
                                <asp:ListItem>Agree</asp:ListItem>
                                <asp:ListItem>Disagree</asp:ListItem>
                                <asp:ListItem>Strongly Disagree</asp:ListItem>
                             </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style1">Do you think the trip was affordable ? :</td>
                        <td>
                            <asp:DropDownList ID="AffordableDropDown" runat="server" Width="186px">
                                <asp:ListItem>---Select---</asp:ListItem>
                                <asp:ListItem>No</asp:ListItem>
                                <asp:ListItem>Yes</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style1">The interaction with local students was a refreshing experience. It gave me an understanding of living and studying in the city : </td>
                        <td>
                        <asp:DropDownList ID="InteractDropDown" runat="server" Width="186px">
                            <asp:ListItem>---Select---</asp:ListItem>
                            <asp:ListItem>Strongly Agree</asp:ListItem>
                            <asp:ListItem>Agree</asp:ListItem>
                            <asp:ListItem>Disagree</asp:ListItem>
                            <asp:ListItem>Strongly Disagree</asp:ListItem>
                        </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style1">The company visits were useful. It gave me a good understanding of the local working and industrial landscape :</td>
                        <td>
                        <asp:DropDownList ID="CompanyVisitDropDown" runat="server" Width="186px">
                            <asp:ListItem>---Select---</asp:ListItem>
                            <asp:ListItem>Strongly Agree</asp:ListItem>
                            <asp:ListItem>Agree</asp:ListItem>
                            <asp:ListItem>Disagree</asp:ListItem>
                            <asp:ListItem>Strongly Disagree</asp:ListItem>
                        </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style1">The transport service provided is timely arranged and comfortable :</td>
                        <td>
                        <asp:DropDownList ID="TransportDropDown" runat="server" Width="186px">
                            <asp:ListItem>---Select---</asp:ListItem>
                            <asp:ListItem>Strongly Agree</asp:ListItem>
                            <asp:ListItem>Agree</asp:ListItem>
                            <asp:ListItem>Disagree</asp:ListItem>
                            <asp:ListItem>Strongly Disagree</asp:ListItem>
                        </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style1">How can this trip be improved for future participants ? :</td>
                        <td>
                            <asp:TextBox ID="tbImprove" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style1">
                            &nbsp;</td>
                        <td>
                            &nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style1">
                    
                        </td>
                        <td>
                            <asp:Button ID="btnSubmit" class="btn btn-success" runat="server" Text="Submit Your Feedback" OnClick="btnSubmit_Click"/></td>
                    </tr>
                    <tr>
                        <td class="auto-style1">&nbsp;</td>
                        <td>
                            <asp:Label ID="lbResult" Text="" runat="server"></asp:Label>
                        </td>
                    </tr>
                    </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--//Page Content-->
</asp:Content>
