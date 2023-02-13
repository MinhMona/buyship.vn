<%@ Page Title="" Language="C#" MasterPageFile="~/App.Master" AutoEventWireup="true" CodeBehind="nap-tien-app.aspx.cs" Inherits="NHST.nap_tien_app" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="NHST.Controllers" %>
<%@ Import Namespace="NHST.Models" %>
<%@ Import Namespace="NHST.Bussiness" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <link rel="stylesheet" href="/App_Themes/App/css/style-NA.css" media="all">
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <main id="main-wrap">

        <div class="page-wrap">
            <asp:Panel ID="pnMobile" runat="server" Visible="false">
                <div class="page-body">
                    <div class="all donhang">
                        <h2 class="title_page">NẠP TIỀN</h2>
                        <div class="content_page">
                            <p class="title_onpage">NẠP TIỀN</p>
                            <p class="user">
                                <a class="item-user"><i class="fa fa-user"></i></a>
                                <asp:Literal ID="ltrIfn" runat="server"></asp:Literal>
                            </p>
                            <div class="content_create_order">
                                <div class="bottom_order">
                                    <ul>
                                        <li>Ngân hàng: </li>
                                        <li>
                                            <asp:DropDownList runat="server" ID="ddlBank" DataTextField="BankInfo" DataValueField="ID" CssClass="input_control"></asp:DropDownList>
                                        </li>
                                    </ul>
                                    <ul>
                                        <li>Số tiền:</li>
                                        <li>
                                            <telerik:RadNumericTextBox runat="server" CssClass="input_control" Skin="MetroTouch"
                                                ID="pAmount" NumberFormat-DecimalDigits="0" Value="0"
                                                NumberFormat-GroupSizes="3" Width="100%">
                                            </telerik:RadNumericTextBox>
                                        </li>
                                    </ul>
                                    <label class="b-label" for="files">
                                        Upload Images
                                    </label>
                                    <div class="field" align="left">

                                        <input class="form-upload" type="file" id="files" name="files[]" multiple />

                                    </div>
                                    <div class="overlay-background"></div>

                                    <%-- <ul>
                                        <li>Hình ảnh: </li>
                                        <li class="right-content">
                                            <div class="right-content">
                                                <input class="upload-img" type="file" onchange="previewFiles(this);" multiple title="">
                                                <button type="button" class="btn-upload">Upload</button>
                                            </div>
                                            <div class="preview-img">
                                            </div>
                                        </li>
                                    </ul>--%>
                                    <ul>
                                        <li>Nội dung: </li>
                                        <li>
                                            <asp:TextBox ID="txtNote" runat="server" CssClass="input_control message" TextMode="MultiLine"></asp:TextBox>
                                        </li>
                                        <li>
                                            <p>- Nội dung chuyển khoản theo cú pháp:</p>

                                            <p style="color: red; padding-left: 10px">+ NAP tentaikhoan sodienthoai</p>
                                        </li>
                                    </ul>


                                    <ul>
                                        <li>Thông tin tài khoản: </li>
                                        <asp:Literal runat="server" ID="ltrBank"></asp:Literal>
                                    </ul>

                                </div>

                                <p class="btn_order">
                                    <a href="javascript:;" onclick="AddWallet($(this))" class="btn_ordersp">GỬI YÊU CẦU</a>
                                    <asp:Button ID="btnSend" runat="server" Style="display: none" Text="GỬI YÊU CẦU" CssClass="btn_ordersp" OnClick="btnSend_Click" />
                                </p>
                            </div>
                        </div>
                    </div>
                    <div class="page-bottom-toolbar">
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnShowNoti" runat="server" Visible="false">
                <div class="page-body">
                    <div class="heading-search">
                        <h4 class="page-title">Bạn vui lòng đăng xuất và đăng nhập lại!</h4>
                    </div>
                </div>
            </asp:Panel>
        </div>

    </main>
    <asp:HiddenField runat="server" ID="hdfListIMG" />
    <script>
        function AddWallet(obj) {
            var r = confirm("Xác nhận nạp tiền!");
            var base64 = "";
            if (r == true) {
                obj.removeAttr("onclick");
                $(".field img").each(function () {
                    base64 += $(this).attr('src') + "|";
                })
                $("#<%=hdfListIMG.ClientID%>").val(base64);
                $("#<%=btnSend.ClientID%>").click();
            }
        }
    </script>
    <style>
        .field {
            position: relative
        }

        .form-upload {
            position: absolute;
            width: 0;
            height: 0;
        }

        input[type="file"] {
            display: block;
        }

        .imageThumb {
            max-height: 75px;
            border: 2px solid;
            padding: 1px;
            cursor: pointer;
        }

        .pip {
            display: inline-block;
            margin: 10px 10px 0 0;
        }

        .remove {
            display: block;
            background: #444;
            border: 1px solid black;
            color: white;
            text-align: center;
            cursor: pointer;
        }

            .remove:hover {
                background: white;
                color: black;
            }

        .hide {
            display: none !important;
        }

        .show-image {
            position: absolute;
            top: 50%;
            left: 50%;
            transform: translate(-50%,-50%);
            width: 700px;
            max-height: 100%;
            transition: .3s all;
            z-index: 11;
        }

        .overlay-background {
            position: absolute;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background-color: rgba(0,0,0,.5);
            z-index: 10;
            opacity: 0;
            visibility: hidden;
        }

            .overlay-background.open {
                opacity: 1;
                visibility: visible;
            }

        .b-label {
            padding: 5px 30px;
            color: white;
            margin-top: 10px;
            background: #f37421;
        }
    </style>
    <script>
        $(document).ready(function () {
            if (window.File && window.FileList && window.FileReader) {
                $("#files").on("change", function (e) {
                    var files = e.target.files,
                        filesLength = files.length;
                    for (var i = 0; i < filesLength; i++) {
                        var f = files[i]
                        var fileReader = new FileReader();
                        fileReader.onload = (function (e) {
                            var file = e.target;
                            $(
                                "<span class=\"pip\"><img class=\"imageThumb\" src=\"" + e.target.result + "\" " +
                                "title=\"" + file.name + "\"/><br/><span class=\"remove\">Xóa hình ảnh</span></" +
                                "span>"
                            ).insertAfter("#files");
                            $(".remove").click(function () {
                                $(this)
                                    .parent(".pip")
                                    .remove();
                            });
                            $(".imageThumb").click(function () {
                                $(this).addClass("show-image");
                                $(".overlay-background").addClass("open");
                            });
                            $(".overlay-background").click(function () {
                                $(".imageThumb").removeClass("show-image");
                                $(".overlay-background").removeClass("open");
                            });
                        });
                        fileReader.readAsDataURL(f);
                    }
                });
            } else {
                alert("Your browser doesn't support to File API")
            }
        });
    </script>
</asp:Content>

