<%@ Page Title="Uploaded Images" MasterPageFile="~/Site.master" Language="C#" AutoEventWireup="true" CodeBehind="UploadedImages.aspx.cs" Inherits="ITP213.UploadedImages" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="Content/imageGallery.css" rel="stylesheet" type="text/css" />
    <ol class="breadcrumb">
        <li class="breadcrumb-item">
            <a href="#" style="color: #D6D6D6">Home</a>
        </li>
        <li class="breadcrumb-item active">Uploaded Images</li>
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
        .table-borderless td,
        .table-borderless th {
            border: 0;
        }
    </style>
    <!-- Page Content -->
    <h1>Uploaded Images</h1>
    <!--2. Change the title!-->
    <hr />
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [image] where [user]= @name">
        <SelectParameters>
            <asp:SessionParameter Name="Name" SessionField="name" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    <div class="container">
        <div style="overflow-x: auto;" class="row">
            <asp:Repeater ID="myRepeater" runat="server" DataSourceID="SqlDataSource1">
                <ItemTemplate>
                    <div class="table col-lg-4">
                        <asp:Label ID="LabelUsername" runat="server"><%# Eval("user") %></asp:Label>
                        <asp:Label ID="AT" runat="server">✈</asp:Label>
                        <asp:Label ID="LabelLocation" runat="server"><%# Eval("location") %></asp:Label>
                        <asp:Button ID="ButtonDelete" CssClass="btn btn-light float-right"  runat="server" Text="X" OnCommand="delete_Click" CommandArgument='<%#Eval("imageID") %>' CommandName="delete_Click2"/>
                        <br />
                        <a alt='<%# Eval("location") %>' runat="server">
                            <asp:Image class="myImg" alt='<%# String.Format("{0} <br /> {1}", Eval("title"), Eval("tags")) %>' ID="Image1" ImageUrl='<%# Bind("image") %>' runat="server" Width="350px" Height="200px" />
                        </a>
                        <br />
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
    <br />
    <!-- The Modal -->
    <div style="overflow: auto;" id="myModal2" class="modal">
        <span class="close" onclick="document.getElementById('myModal2').style.display='none'">&times;</span>
        <img class="modal-content" id="img01">
        <div id="caption"></div>
    </div>
    <script type="text/javascript">;
        // Get the modal
        var modal = document.getElementById('myModal2');
        // Get the image and insert it inside the modal - use its "alt" text as a caption
        var img = $('.myImg');
        var modalImg = $("#img01");
        var captionText = document.getElementById("caption");
        $('.myImg').click(function () {
            modal.style.display = "block";
            var newSrc = this.src;
            modalImg.attr('src', newSrc);
            captionText.innerHTML = this.alt;
        });
        // Get the <span> element that closes the modal
        var span = document.getElementsByClassName("close")[0];

        // When the user clicks on <span> (x), close the modal
        span.onclick = function () {
            modal.style.display = "none";
        }
        //Esc Keyboard Function
        document.onkeydown = function (evt) {
            evt = evt || window.event;
            var isEscape = false;
            if ("key" in evt) {
                isEscape = (evt.key == "Escape" || evt.key == "Esc");
            } else {
                isEscape = (evt.keyCode == 27);
            }
            if (isEscape) {
                document.getElementById('myModal2').style.display = 'none';
            }
        };
        //End of Esc Keyboard Function
    </script>
    </asp:Content>
