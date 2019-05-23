<%@ Page Title="Image Gallery" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeBehind="ImageGallery.aspx.cs" Inherits="ITP213.ImageGallery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Content/imageGallery.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel='stylesheet' href='Content/mdb1.min.css' />
    <ol class="breadcrumb">
        <li class="breadcrumb-item">
            <a href="#" style="color: #D6D6D6">Home</a>
        </li>
        <li class="breadcrumb-item active">Image Gallery</li>
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

        .auto-style1 {
            width: 300px;
        }

        .auto-style2 {
            width: 37%;
        }

        .table-borderless td,
        .table-borderless th {
            border: 0;
        }
        #inner {
            width: 50%;
            margin: 0 auto;
        }
    </style>
    <!-- Page Content -->
    <h1>Image Gallery</h1>
    <!--2. Change the title!-->
    <hr />
    <!--//Page Content-->
    <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#myModal" style="color: white !important;">
        Upload Picture
   
    </button>
    <!-- The Modal -->
    <div class="modal fade" id="myModal">
        <div class="modal-dialog modal-frame modal-top modal-notify modal-info" role="document">
            <div class="modal-content">
                <!-- Modal Header -->
                <div class="modal-header">
                    <h4 class="modal-title text-white">Upload Picture</h4>
                    <button type="button" class="close" data-dismiss="modal" style="color: white;">&times;</button>
                </div>
                <!-- Modal body -->
                <div class="modal-body">
                    <table style="margin-left: auto; margin-right: auto;" class="auto-style2 table table-borderless">
                        <tr>
                            <td class="auto-style1">Title</td>
                            <td>
                                <asp:TextBox ID="TextBoxTitle" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="TextBoxTitle" Font-Size="Smaller" ForeColor="Red" runat="server" ErrorMessage="Please enter title">*required</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1">Tags</td>
                            <td>
                                <asp:CheckBoxList ID="CheckBoxListTags" runat="server" RepeatColumns="2" RepeatDirection="Horizontal">
                                    <asp:ListItem>#Scenery</asp:ListItem>
                                    <asp:ListItem>#Attractions</asp:ListItem>
                                    <asp:ListItem>#People</asp:ListItem>
                                    <asp:ListItem>#Food</asp:ListItem>
                                </asp:CheckBoxList>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1">Location</td>
                            <td>
                                <asp:DropDownList CssClass="btn btn-outline-secondary-modal waves-effect" ID="DropDownListLocation" runat="server">
                                    <asp:ListItem>Thailand</asp:ListItem>
                                    <asp:ListItem>Korea</asp:ListItem>
                                    <asp:ListItem>Vietnam</asp:ListItem>
                                    <asp:ListItem>Japan</asp:ListItem>
                                    <asp:ListItem>China</asp:ListItem>
                                    <asp:ListItem>Indonesia</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1">Select Image</td>
                            <td>
                                <asp:FileUpload ID="FileUploadImage" runat="server" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="FileUploadImage" Font-Size="Smaller" ForeColor="Red" runat="server" ErrorMessage="Please upload an image">*required</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </div>
                <!-- Modal footer -->
                <div class="modal-footer">
                    <asp:Button ID="ButtonUpload" class="btn btn-primary-modal text-white" runat="server" OnClick="ButtonUpload_Click" Text="Upload" />
                    <button type="button" class="btn btn-outline-secondary-modal waves-effect" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
    &nbsp;&nbsp;&nbsp;<br />
    &nbsp;
   
    <div class="row">
        <div class="col-lg-7">
            <button id="btnAll" type="button" class="btn btn-primary">All</button>
            <button id="btnScenery" type="button" class="btn btn-primary">Scenery</button>
            <button id="btnAttractions" type="button" class="btn btn-primary">Attractions</button>
            <button id="btnPeople" type="button" class="btn btn-primary">People</button>
            <button id="btnFood" type="button" class="btn btn-primary">Food</button>
        </div>
        <div class="col-lg-1">
        </div>
        <div class="col-lg-4">
            <input style="border-color: #007BFF;" class="form-control" id="myInput" type="text" placeholder="Search.." />
        </div>
    </div>
    <br />
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [image]"></asp:SqlDataSource>
    <div class="container">
        <div style="overflow-x: auto;" class="row">
            <asp:Repeater ID="myRepeater" runat="server" DataSourceID="SqlDataSource1">
                <ItemTemplate>
                    <div class="table col-lg-4 picture">
                        <asp:Label ID="LabelUsername" runat="server"><%# Eval("user") %></asp:Label>
                        <asp:Label ID="AT" runat="server">✈</asp:Label>
                        <asp:Label ID="LabelLocation" runat="server"><%# Eval("location") %></asp:Label>
                        <br />
                        <a alt='<%# Eval("location") %>' runat="server">
                            <asp:Image class="myImg" alt='<%# String.Format("{0} <br /> {1}", Eval("title"), Eval("tags")) %>' ID="Image1" ImageUrl='<%# Bind("image") %>' runat="server" Width="350px" Height="200px" />
                        </a>
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
        //Filter Function
        $(document).ready(function () {
            $("#myInput").on("keyup", function () {
                var value = $(this).val().toLowerCase();
                $(".picture").filter(function () {
                    $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1);
                });
            });
        });
        $("#btnAll").click(function () {
            $(".picture").filter(function () {
                $(this).toggle($(this).text().toLowerCase().indexOf(" ") > -1);
            });
        })
        $("#btnScenery").click(function () {
            $(".picture").filter(function () {
                $(this).toggle($(this).find('img').attr('alt').toLowerCase().indexOf("scenery") > -1);
            });
        })
        $("#btnAttractions").click(function () {
            $(".picture").filter(function () {
                $(this).toggle($(this).find('img').attr('alt').toLowerCase().indexOf("attractions") > -1);
            });
        })
        $("#btnFood").click(function () {
            $(".picture").filter(function () {
                $(this).toggle($(this).find('img').attr('alt').toLowerCase().indexOf("food") > -1);
            });
        })
        $("#btnPeople").click(function () {
            $(".picture").filter(function () {
                $(this).toggle($(this).find('img').attr('alt').toLowerCase().indexOf("people") > -1);
            });
        })
        //End of Filter Function
    </script>
</asp:Content>
