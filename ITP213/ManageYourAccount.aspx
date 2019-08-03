<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="ManageYourAccount.aspx.cs" Inherits="ITP213.ManageYourAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Breadcrumbs-->
    <ol class="breadcrumb">

        <li class="breadcrumb-item">

            <a href="#" style="color: #D6D6D6">Home</a>
        </li>
        <li class="breadcrumb-item active">Manage Your Account</li>
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
    <!-- //Breadcrumbs end-->

    <!-- Page Content -->
    <h1>Manage Your Account</h1>
    <!--2. Change the title!-->
    <hr />
    <p>
        <!--3. This is where you code all your features-->

        Phone Number:
        <asp:Label ID="lblPhoneNumber" runat="server" Text=""></asp:Label>
        (<asp:Label ID="lblVerifiedPhoneStatus" runat="server" Text=""></asp:Label>)
        &nbsp;[<asp:HyperLink ID="HyperLinkPhoneNum" runat="server">Change</asp:HyperLink>
        ]
    </p>
    <p>
        Email:
        <asp:Label ID="lblEmail" runat="server"></asp:Label>
        (<asp:Label ID="lblVerifiedEmailStatus" runat="server"></asp:Label>) [<asp:HyperLink ID="HyperLinkEmail" runat="server">Change</asp:HyperLink>
        ]
    </p>

    <asp:Panel ID="Panel1" runat="server" Visible="false">
        <p>
            One Time Password:
            <asp:Label ID="lblOTP" runat="server" Text=""></asp:Label>
            &nbsp;[<asp:Button ID="btnOTP" runat="server" Text="Disable" Style="padding: 0; border: none; background: none; color: #0000FF" OnClick="btnOTP_Click"/>
            ]
            
        </p>

    </asp:Panel>
    <asp:Panel ID="PanelCaptcha" runat="server" Visible="false">
        <div class="form-row">
            <div class="form-group">
                <div id="ReCaptchContainer"></div>
            </div>

        </div>
    </asp:Panel>
    Google Auth:
        <asp:Label ID="lblGoogleAuth" runat="server" Text=""></asp:Label>
    &nbsp;[<asp:Button ID="btnGoogleAuth" runat="server" Visible="true" Text="Enable" Style="padding: 0; border: none; background: none; color: #0000FF" OnClick="btnGoogleAuth_Click" />]<br />
    <asp:HyperLink ID="HyperLinkChangePassword" runat="server">Change Password</asp:HyperLink>
    <br />
    <p>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="PublicIPAddress,UUID,macAddress" DataSourceID="SqlDataSource1" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="PublicIPAddress" HeaderText="Public IP Address" ReadOnly="True" SortExpression="PublicIPAddress" />
                <asp:BoundField DataField="macAddress" HeaderText="Mac Address" ReadOnly="True" SortExpression="macAddress" />
                <asp:BoundField DataField="Location" HeaderText="Location" SortExpression="Location" />
                <asp:BoundField DataField="LastLogin" HeaderText="Last Login" SortExpression="LastLogin" />
            </Columns>
            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
            <SortedAscendingCellStyle BackColor="#FDF5AC" />
            <SortedAscendingHeaderStyle BackColor="#4D0000" />
            <SortedDescendingCellStyle BackColor="#FCF6C0" />
            <SortedDescendingHeaderStyle BackColor="#820000" />
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\ITP213.mdf;Integrated Security=True" ProviderName="System.Data.SqlClient" SelectCommand="SELECT [PublicIPAddress], [UUID], [LastLogin], [Location], [macAddress] FROM [NewDeviceLogin] WHERE ([UUID] = @UUID)">
            <SelectParameters>
                <asp:SessionParameter Name="UUID" SessionField="UUID" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
    </p>

    <p>
        <asp:Label ID="lblResult" runat="server" ForeColor="Green"></asp:Label>
    </p>
    <!--//Page Content-->
    <script src="https://www.google.com/recaptcha/api.js?onload=renderRecaptcha&render=explicit" async defer></script>
    <script type="text/javascript">
        var your_site_key = '<%= Environment.GetEnvironmentVariable("SiteKey")%>';
        var renderRecaptcha = function () {
            grecaptcha.render('ReCaptchContainer', {
                'sitekey': your_site_key,
                theme: 'light', //light or dark    
                type: 'image',// image or audio    
                size: 'normal'//normal or compact    
            });
        };

    </script>
</asp:Content>
