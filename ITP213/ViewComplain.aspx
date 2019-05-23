<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="ViewComplain.aspx.cs" Inherits="ITP213.ViewComplain" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel='stylesheet' href='Content/mdb1.min.css' />
    <!-- Breadcrumbs-->
    <ol class="breadcrumb">
        <li class="breadcrumb-item">
            <a href="/Default.aspx" style="color: #D6D6D6">Home</a>
        </li>

        <li class="breadcrumb-item active">View Feedbacks</li>
    </ol>
    <style>
        .breadcrumb {
            background-color: #FFFFFF !important;
        }
    </style>
    <!-- //Breadcrumbs end-->


    <!-- Page Content -->

    <h1>
        <asp:Label ID="lblTitle" runat="server" Text="View Feedbacks"></asp:Label></h1>
    <hr />

    <asp:DataList ID="DataList1" runat="server" DataKeyField="complainID" DataSourceID="SqlDataSource1" RepeatColumns="3" BorderStyle="None" Height="181px" Width="426px" CellPadding="5" CellSpacing="5" RepeatDirection="Horizontal">
        <ItemTemplate>
            <asp:Panel ID="Panel1" runat="server" Height="200px" Width="300px" Visible='<%#Convert.ToBoolean(Eval("complainStatus")) %>'>
                <div class="card-header card bg-primary text-white" style="background-color: #5A95F5 !important">Trip : <%# Eval("trip") %>
                    <br />
                    Category : <%# Eval("subject") %></div>
                <div class="card-body card bg-light text-dark"><%# Eval("comments") %></div>
                <div class="card-footer">
                    <button type="button" class="btn btn-primary float-right" data-toggle="modal" data-target='#myModal<%# Eval("complainID") %>'>View Image</button>
                    <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-primary" OnClick="Button1_Click" Text="Delete" OnCommand="deleteCommand" CommandName="delete_Click" CommandArgument='<%# Eval("complainID") %>' />
                </div>


            </asp:Panel>
            <!-- The Modal -->
            <div class='modal fade' id='myModal<%# Eval("complainID") %>'>
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <!--Modal Header -->
                        <div class="modal-header">
                            <h4 class="modal-title">Photo</h4>
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                        </div>
                        <!-- Modal body -->
                        <div class="modal-body">
                            <table style="margin-left: auto; margin-right: auto;" class="auto-style2">
                                <tr>
                                    <td>
                                        <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("image") %>' Height="200px" Width="300px" />
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
        </ItemTemplate>
    </asp:DataList>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [complain]"></asp:SqlDataSource>
    <!--//Page Content-->

</asp:Content>

