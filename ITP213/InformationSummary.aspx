<%@ Page Title="Information Summary" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="InformationSummary.aspx.cs" Inherits="ITP213.InformationSummary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ol class="breadcrumb">
        <li class="breadcrumb-item">
            <a href="#" style="color: #D6D6D6">Home</a>
        </li>
        <li class="breadcrumb-item active">Information Summary</li>
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
    <!-- Page Content -->
    <h1>Information Summary</h1>
    <!--2. Change the title!-->
    <hr />
    <div class="row">
        <div class="col-lg-6">
            <h5>Please choose a summary
                <asp:DropDownList ID="DropDownListSummary" runat="server" InitialValue="0" CssClass="btn btn-primary dropdown-toggle" OnSelectedIndexChanged="DropDownListSummary_SelectedIndexChanged" AutoPostBack="True">
                    <asp:ListItem>-- Select --</asp:ListItem>
                    <asp:ListItem Value="Acad">Academic</asp:ListItem>
                    <asp:ListItem Value="Diet">Dietary Needs</asp:ListItem>
                </asp:DropDownList>
            </h5>
        </div>
        <div class="col-lg-2"></div>
        <div class="col-lg-4">
            <input style="border-color: #007BFF;" class="form-control" id="myInput" type="text" placeholder="Search.." />
        </div>
    </div>
    <br />
    <asp:Panel ID="PanelAcademicSummary" Visible="false" runat="server">
        <h4 style="text-align: center">Academic Results Summary</h4>
        <br />
        <div class="row">
            <div class="container col-lg-8">
                <asp:GridView ID="GridView1" runat="server" CssClass="table table-striped" AutoGenerateColumns="False" AllowPaging="True" PageSize="8" OnPageIndexChanging="GridView1_PageIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField ItemStyle-CssClass="tableData" HeaderText="Admin Number" DataField="adminNo" >
                        <ItemStyle CssClass="tableData" />
                        </asp:BoundField>
                        <asp:BoundField ItemStyle-CssClass="tableData" HeaderText="Cumulative GPA" DataField="cumulativeGPA" >
                        <ItemStyle CssClass="tableData" />
                        </asp:BoundField>
                    </Columns>
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerSettings NextPageText="Next" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="PanelDietSummary" Visible="false" runat="server">
        <h4 style="text-align: center">Dietary Needs Summary</h4>
        <br />
        <div class="row">   
            <div class="container col-lg-8">
                <asp:GridView ID="GridView2" runat="server" CssClass="table table-striped" AutoGenerateColumns="False" AllowPaging="True" PageSize="8" OnPageIndexChanging="GridView2_PageIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="adminNo" HeaderText="Admin Number" ItemStyle-CssClass="tableData">
                        <ItemStyle CssClass="tableData" />
                        </asp:BoundField>
                        <asp:BoundField DataField="studentName" HeaderText="Student Name" ItemStyle-CssClass="tableData">
                        <ItemStyle CssClass="tableData" />
                        </asp:BoundField>
                        <asp:BoundField DataField="dietaryNeeds" HeaderText="Dietary Needs" ItemStyle-CssClass="tableData">
                        <ItemStyle CssClass="tableData" />
                        </asp:BoundField>
                    </Columns>
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>
            </div>
        </div>
    </asp:Panel>
    <br />
    <script type="text/javascript">
        $(document).ready(function () {
            $("#myInput").on("keyup", function () {
                var value = $(this).val().toLowerCase();
                $("tr:not(tr:first)").filter(function () {
                    $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1);
                });
            });
        });
    </script>
    <!--//Page Content-->
</asp:Content>
