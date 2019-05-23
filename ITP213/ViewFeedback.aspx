<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="ViewFeedback.aspx.cs" Inherits="ITP213.ViewFeedback" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Breadcrumbs-->
    <ol class="breadcrumb">
        <li class="breadcrumb-item">
            <a href="#" style="color: #D6D6D6">Home</a>
        </li>
        <li class="breadcrumb-item active">View Feedback (Trip Survey)</li>
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
    <h1>View Feedback (Trip Survey)</h1>
    <hr />

    <div class="container mt-5 mb-4">
        <div class="card pl-3 pr-3">
            <div class="pb-5 text-center m-md-3">
                <div class="container" style="overflow-x:auto;">
                  <div class="row">
                    <asp:GridView ID="GridViewFeedback" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Height="271px" Width="1103px">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField DataField="adminNo" HeaderText="Submitted By" ReadOnly="True"></asp:BoundField>
                            <asp:BoundField DataField="enjoy" HeaderText="Enjoyment" ReadOnly="True" />
                            <asp:BoundField DataField="lodging" HeaderText="Lodging" ReadOnly="True" />
                            <asp:BoundField DataField="affordable" HeaderText="Affordability" ReadOnly="True" />
                            <asp:BoundField DataField="interaction" HeaderText="Interaction" ReadOnly="True" />
                            <asp:BoundField DataField="companyVisit" HeaderText="Company Visit" ReadOnly="True" />
                            <asp:BoundField DataField="transport" HeaderText="Transport" ReadOnly="True" />
                            <asp:BoundField DataField="improvement" HeaderText="Improvements" ReadOnly="True" />
                        </Columns>
                        <EditRowStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#E3EAEB" />
                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#F8FAFA" />
                        <SortedAscendingHeaderStyle BackColor="#246B61" />
                        <SortedDescendingCellStyle BackColor="#D4DFE1" />
                        <SortedDescendingHeaderStyle BackColor="#15524A" />
                    </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--//Page Content-->
</asp:Content>
