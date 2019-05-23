<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="ViewStatistics.aspx.cs" Inherits="ITP213.ViewStatistics" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Breadcrumbs-->
    <ol class="breadcrumb">
        <li class="breadcrumb-item">
            <a href="#" style="color:#D6D6D6">Home</a>
        </li>
        <li class="breadcrumb-item active">View Statistics</li>
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
    </style>
    <!-- //Breadcrumbs end-->

    <!-- Page Content -->
    <h1>View Statistics</h1>
    <hr/>
    <div class="container mt-5 mb-4">
        <div class="card pl-3 pr-3">
            <div class="pb-5 text-left m-md-3">
            <table class="auto-style5">
            <tr>
                <td class="auto-style6">
            <table class="auto-style5">
            <tr>
                <td class="auto-style3">Country :</td>
                <td class="auto-style2">
                    <asp:DropDownList ID="CountryDropDown" runat="server" AutoPostBack="True" Width="231px">
                        <asp:ListItem>---Select---</asp:ListItem>
                        <asp:ListItem>Thailand</asp:ListItem>
                        <asp:ListItem>Korea</asp:ListItem>
                        <asp:ListItem>China</asp:ListItem>
                        <asp:ListItem>Vietnam</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">&nbsp;</td>
                <td class="auto-style1">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style8">Type Of Trip :</td>
                <td class="auto-style9">
                    <asp:DropDownList ID="TypeDropDown" runat="server" AutoPostBack="True">
                        <asp:ListItem>---Select---</asp:ListItem>
                        <asp:ListItem>Immersion Trip</asp:ListItem>
                        <asp:ListItem>Study Trip</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="auto-style4">&nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style4"></td>
                <td>
      
                </td>
            </tr>
            </table>
                </td>
                <td class="auto-style7">
                    <table class="auto-style5">
                        <tr>
                            <td class="auto-style4">&nbsp;</td>
                            <td>
                    
                                
                    
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style4">&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="auto-style4">&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="auto-style4">&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="auto-style6">
                    <asp:Label ID="Label3" runat="server" Text="Total Cost Of The Programme :"></asp:Label>
            <asp:Chart ID="Chart2" runat="server" Width="600px" BackGradientStyle="VerticalCenter">
            <series>
                <asp:Series Name="Series1" XValueMember="tripNameAndTripType" YValueMembers="tripCost">
                </asp:Series>
            </series>
            <chartareas>
                <asp:ChartArea Name="ChartArea1">
                </asp:ChartArea>
            </chartareas>
            </asp:Chart>
       
                </td>
                <td class="auto-style7">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style6">
                    <table class="auto-style5">
                        <tr>
                            <td class="auto-style4">&nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="auto-style4">&nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
                </td>
                <td class="auto-style7">
                    <table class="auto-style11">
                        <tr>
                            <td>
                                <table class="auto-style5">
                                    <tr>
                                        <td class="auto-style10">&nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style10">&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="auto-style6">
                    &nbsp;</td>
                <td class="auto-style7">
                    &nbsp;</td>
            </tr>
            </table>
            </div>
        </div>
    </div>
    <!--//Page Content-->
</asp:Content>
