<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Task.aspx.cs" Inherits="ITP213.Task" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:Panel ID="Panel1" runat="server">
        Force check if account is expire<br />
        <asp:Button ID="btn_expire" runat="server" Text="Execute" OnClick="btn_expire_Click" />
    </asp:Panel>
    <asp:Panel ID="Panel2" runat="server">
        Force upload today&#39;s block<br />
        <asp:Button ID="btn_forceUpload" runat="server" Text="Execute" OnClick="btn_forceUpload_Click" />

    </asp:Panel>

    <asp:Button ID="btn_displayBC" runat="server" Text=" Display Block Chain" OnClick="btn_displayBC_Click" />
    <asp:Panel ID="BC_Panel" runat="server" Visible="false" ScrollBars="Vertical">
        <pre>
            <asp:Label ID="lb_BC" runat="server" Text=""></asp:Label>
        </pre>
        
        <br />
        &nbsp;</asp:Panel>

</asp:Content>
