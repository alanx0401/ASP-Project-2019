<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="ListUser.aspx.cs" Inherits="ITP213.ListUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:GridView ID="gv_UserTable" runat="server" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="UUID" DataSourceID="SqlDataSource1" OnRowEditing="gv_UserTable_RowEditing" OnRowUpdating="gv_UserTable_RowUpdating" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
        <Columns>
            <asp:BoundField DataField="UUID" HeaderText="UUID" ReadOnly="True" SortExpression="UUID" />
            <asp:BoundField DataField="name" HeaderText="name" SortExpression="name" />
            <asp:TemplateField HeaderText="accountType" SortExpression="accountType">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("accountType") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("accountType") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="accountStatus" HeaderText="accountStatus" SortExpression="accountStatus" />
            <asp:BoundField DataField="email" HeaderText="email" SortExpression="email" />
            <asp:BoundField DataField="mobile" HeaderText="mobile" SortExpression="mobile" />
            <asp:CommandField ShowEditButton="True" />
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [account]"></asp:SqlDataSource>
    <br />
    ll
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\ITP213.mdf;Integrated Security=True" ProviderName="System.Data.SqlClient" SelectCommand="SELECT [UUID], [name], [accountType], [email], [accountStatus], [mobile], [dateOfBirth] FROM [account]"></asp:SqlDataSource>
    <br />
    <asp:DropDownList ID="DropDownList1" runat="server">
        <asp:ListItem>Ban</asp:ListItem>
        <asp:ListItem>Unban</asp:ListItem>
    </asp:DropDownList>
</asp:Content>
