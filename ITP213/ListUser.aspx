<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="ListUser.aspx.cs" Inherits="ITP213.ListUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical">
    
    <asp:GridView ID="gv_UserTable" runat="server" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="UUID" DataSourceID="SqlDataSource1" OnRowEditing="gv_UserTable_RowEditing" OnRowUpdating="gv_UserTable_RowUpdating" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowDataBound="gv_UserTable_RowDataBound" CellPadding="4" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:BoundField ControlStyle-CssClass="col" DataField="UUID" HeaderText="UUID" ReadOnly="True" SortExpression="UUID">
            <ControlStyle CssClass="col" />
            </asp:BoundField>
            <asp:BoundField DataField="name" HeaderText="name" SortExpression="name" />
            <asp:TemplateField HeaderText="accountType" SortExpression="accountType">
                <EditItemTemplate>
                    <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-check-label" DataSourceID="SqlDataSource2" DataTextField="accountType" DataValueField="accountType" SelectedValue='<%# Bind("accountType") %>'>
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT DISTINCT [accountType] FROM [account]"></asp:SqlDataSource>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lbl_accountType" runat="server" Text='<%# Bind("accountType") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="accountStatus" SortExpression="accountStatus">
                <EditItemTemplate>
                    <asp:DropDownList ID="DropDownList2" runat="server" DataSourceID="SqlDataSource3" DataTextField="accountStatus" DataValueField="accountStatus" SelectedValue='<%# Bind("accountStatus") %>'>
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT DISTINCT [accountStatus] FROM [account]"></asp:SqlDataSource>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("accountStatus") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="mobile" HeaderText="mobile" SortExpression="mobile" />
            <asp:CommandField ShowEditButton="True" />
        </Columns>
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#E9E7E2" />
        <SortedAscendingHeaderStyle BackColor="#506C8C" />
        <SortedDescendingCellStyle BackColor="#FFFDF8" />
        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
    </asp:GridView>
    </asp:Panel>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"  
        SelectCommand="SELECT [UUID], [name], [accountType], [accountStatus], [email], [mobile] FROM [account]" 
        UpdateCommand="UPDATE [account] SET name = @name, accountType = @accountType, accountStatus = @accountStatus, email = @email, mobile = @mobile WHERE UUID = @UUID">
        <UpdateParameters>
            <asp:Parameter Name="name" />
            <asp:Parameter Name="accountType" />
            <asp:Parameter Name="accountStatus" />
            <%--<asp:ControlParameter Name="accountStatus" ControlId="ddl_AccType" PropertyName="SelectedValue"/>--%>
            <asp:Parameter Name="email" />
            <asp:Parameter Name="mobile" />
            <asp:Parameter Name="UUID" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <br />
    
    
</asp:Content>
