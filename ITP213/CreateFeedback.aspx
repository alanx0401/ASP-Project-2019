<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="OverseasInternMain.aspx.cs" Inherits="ITP213.Format" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Breadcrumbs-->
    <ol class="breadcrumb">
        <li class="breadcrumb-item">
            <a href="#" style="color:#D6D6D6">Home</a>
        </li>
        <li class="breadcrumb-item active">Overseas Internships</li>
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
    </style>
    <!-- //Breadcrumbs end-->

    <!-- Page Content -->
    <h1>Create New Feedback</h1>
    <hr/>
    <div class="container mt-5 mb-4">
        <div class="card pl-3 pr-3">
            <div class="pb-5 text-left m-md-3">
                <h1 class="font-weight-light mt-3 mb-3">
                    <strong>Lorem ipsum</strong>
                </h1>
                <div class="form" runat="server">
                <h2>Test Create Feedback</h2>
                <table>
                    <tr>
                        <td>Question No :</td>
                        <td>
                            <asp:TextBox ID="tbAdminNo"   runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Question :</td>
                        <td>
                            <asp:TextBox ID="tbDepDate"   runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Type of Question :</td>
                        <td>
                            <asp:TextBox ID="tbArrDate"   runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    
                </table>
                </div>
            </div>
        </div>
    </div>
    <!--//Page Content-->
</asp:Content>

