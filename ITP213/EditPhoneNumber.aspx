<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="EditPhoneNumber.aspx.cs" Inherits="ITP213.EditPhoneNumber" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--************README: Hi, please change some of the following things below when you're coding your features. Thanks! -PS -->

    <!-- Breadcrumbs-->
    <ol class="breadcrumb">

        <li class="breadcrumb-item">

            <a href="#" style="color:#D6D6D6">Home</a>
        </li>
        <li class="breadcrumb-item">

            <a href="/ManageYourAccount.aspx" style="color:#D6D6D6">Manage Your Account</a>
        </li>
        <li class="breadcrumb-item active">Edit Phone Number</li> <!--1. Change the name!-->
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
    <h1>Edit Phone Number</h1> <!--2. Change the title!-->
    <hr/>
    <p> <!--3. This is where you code all your features--> 
        <div class="form-row">
            <div class="form-group col-md-4">
                 Phone Number:
                <asp:TextBox ID="tbPhoneNumber" runat="server" class="form-control"></asp:TextBox>
                <asp:RegularExpressionValidator ID="REVContactNumber" runat="server" ErrorMessage="Contact Number is in a wrong format. It must be a Singapore number." ControlToValidate="tbPhoneNumber" Display="Dynamic" ForeColor="Red" ValidationExpression="^[89]\d{7}$">*</asp:RegularExpressionValidator>
                
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:button runat="server" text="Resend Verification" ID="btnResendPhoneVerification" class="btn btn-light" OnClick="btnResendPhoneVerification_Click"/>
                    <asp:Label ID="Label1" runat="server" Visible="false">0</asp:Label>
                    <asp:Timer ID="Timer1" runat="server" Interval="1000" 
                        ontick="Timer1_Tick">
                    </asp:Timer>
                </ContentTemplate>
                </asp:UpdatePanel>
                
            </div>
            <div class="form-group col-md-4">
                <asp:Panel ID="PanelEnterPasswordToChangePhoneNo" runat="server" Visible="false">
                    <asp:Label ID="lblPasswordPhoneNumber" runat="server" Text="Enter your password:" Visible="true"></asp:Label>
                    <asp:TextBox ID="tbPasswordPhoneNumber" runat="server" class="form-control" Visible="true" TextMode="Password"></asp:TextBox>
                </asp:Panel>
            </div>
        </div>
        <asp:panel runat="server" ID="PanelOTP" visible="false">
            <div class="form-row">
                <div class="form-group col-md-4">
                    One Time Password:
                    <asp:TextBox ID="tbOneTimePassword" runat="server" class="form-control"></asp:TextBox>  
                    <asp:Button ID="BtnConfirmOTP" runat="server" Text="Confirm" OnClick="BtnConfirmOTP_Click" class="btn btn-primary float-right"/>
                
                </div>
            </div>
        </asp:panel>
       
    </p>
        <asp:Button ID="btnConfirmUpdate" runat="server" Text="Update Information" OnClick="btnConfirmUpdate_Click" class="btn btn-success"/>
    </p>
    <p> 
        <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
    </p>
    <!--//Page Content-->
</asp:Content>
