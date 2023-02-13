<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/BuyShipMaster.Master" CodeBehind="Default.aspx.cs" Inherits="NHST.Default7" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <main>
        <div class="main-banner">
            <div class="container">
                <div class="content-banner">
                    <div class="flag-icon">
                        <img src="/App_Themes/CSSBUYSHIP/images/flag.png" alt="">
                    </div>
                    <h3 class="title-banner wow fadeInRight" data-wow-delay=".3s" data-wow-duration="1s">Nhập hàng Nhật
                        <br />
                        Luôn đồng hành cùng bạn
                    </h3>
                </div>
            </div>
        </div>
        <section class="find-product-section">
            <div class="container wow zoomIn">
                <div class="find-prd-wrap tab-wrapper">
                    <div class="title">
                        <h3 class="hd tab-link left" data-tab="#search">
                            <img src="/App_Themes/CSSBUYSHIP/images/ic-search.png" alt="">
                            Tìm kiếm sản phẩm</h3>
                        <%--  <h3 class="hd tab-link right" data-tab="#tracking">
                            <img src="/App_Themes/CSSBUYSHIP/images/ic-tracking.png" alt="">
                            Tracking </h3>--%>
                    </div>
                    <div class="search-form-wrap">
                        <div id="search" class="search-form tab-content">
                            <div class="select-form">
                                <select class="fcontrol" id="brand-source" style="width: 260px;">
                                    <option value="auctions" data-image="/App_Themes/CSSBUYSHIP/images/icon-tag-1.png">https://auctions.yahoo.co.jp</option>
                                    <option value="mercari" data-image="/App_Themes/CSSBUYSHIP/images/icon-tag-2.png">https://jp.mercari.com</option>
                                    <option value="rakuten" data-image="/App_Themes/CSSBUYSHIP/images/icon-tag-3.png">https://www.rakuten.co.jp</option>
                                    <option value="amazon" data-image="/App_Themes/CSSBUYSHIP/images/icon-tag-4.png">https://www.amazon.co.jp</option>
                                    <%--<option value="mercari" data-image="/App_Themes/CSSBUYSHIP/images/flag-japan.png">Mercari </option>--%>
                                    <%--<option value="shopping" data-image="/App_Themes/CSSBUYSHIP/images/flag-japan.png">Shopping </option>--%>
                                    <%--<option value="ebay" data-image="/App_Themes/CSSBUYSHIP/images/flag-usa.png"> Ebay </option>
                                    <option value="amazon" data-image="/App_Themes/CSSBUYSHIP/images/flag-usa.png"> Amazon </option>
                                    <option value="walmart" data-image="/App_Themes/CSSBUYSHIP/images/flag-usa.png"> Walmart </option>
                                    <option value="bestbuy" data-image="/App_Themes/CSSBUYSHIP/images/flag-usa.png"> Bestbuy </option>
                                    <option value="ashford" data-image="/App_Themes/CSSBUYSHIP/images/flag-usa.png"> Ashford </option>
                                    <option value="nike" data-image="/App_Themes/CSSBUYSHIP/images/flag-usa.png"> Nike </option>--%>
                                </select>
                                <span class="icon">
                                    <i class="fa fa-sort" aria-hidden="true"></i>
                                </span>
                            </div>
                            <div class="input-form">
                                <asp:TextBox type="text" runat="server" ID="txtSearch" class="fcontrol f-input" placeholder="Nhập tên sản phẩm bạn tìm kiếm"></asp:TextBox>
                            </div>
                            <a href="javascript:" onclick="searchProduct()" class="main-btn">Tìm kiếm</a>
                            <asp:Button ID="btnSearch" runat="server"
                                OnClick="btnSearch_Click" Style="display: none"
                                OnClientClick="document.forms[0].target = '_blank';" UseSubmitBehavior="false" />
                        </div>
                        <%--<div id="tracking" class="search-form tab-content">
                            <div class="input-form">
                                <input type="text" class="fcontrol f-input" placeholder="Nhập link sản phẩm">
                            </div>
                            <a href="#" class="main-btn">Tracking</a>
                        </div>--%>
                    </div>
                </div>
            </div>
        </section>
        <section class="sec option-website pt-not">
            <div class="main-title">
                <h3 class=" title wow fadeInRight">Những trang mua sắm hàng đầu
                </h3>
            </div>
            <div class="main-website">
                <div class="title-web">
                    <div class="tab">
                        <%-- <button class="tablinks active" onclick="openCity(event, 'usa')"> Trang mua sắm tại Mỹ</button>--%>
                        <button class="tablinks" onclick="openCity(event, 'japan')">Trang mua sắm tại Nhật Bản</button>
                    </div>
                </div>
                <div class="website-form-wrap">
                    <div id="japan" class="tabcontent active">
                        <div class="website">
                            <div class="row">
                                <asp:Literal runat="server" ID="ltrView2"></asp:Literal>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <section class="sec-service-about ">
            <div class="container">
                <div class="main-title wow fadeInRight" data-wow-delay=".2s" data-wow-duration="1s">
                    <h3 class="title">Dịch vụ của nhập hàng Nhật
                    </h3>
                    <%--<p class="desc">
                        Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean in sagittis risus. Nam id ligula mi. Etiam mattis nulla non<br /> ipsum tristique, ac rutrum massa pharetra.
                    </p>--%>
                </div>
                <div class="table-services">
                    <div class="columns">
                        <asp:Literal runat="server" ID="ltrServices"></asp:Literal>
                    </div>
                </div>
            </div>
        </section>
        <section class="sec-hd-resgister sec">
            <div class="container">
                <div class="table-resgister">
                    <div class="main-title">
                        <h3 class="title color-fff wow fadeInRight" data-wow-delay=".3s" data-wow-duration="1s">hướng dẫn đăng ký
                        </h3>
                    </div>
                    <div class="c-tab">
                        <nav class="c-tab__nav wow fadeInUp" data-wow-delay=".3s" data-wow-duration="1s">
                            <div class="arrow-up">
                                <img src="/App_Themes/CSSBUYSHIP/images/tron.png" alt="">
                            </div>
                            <div class="arrow-down">
                                <img src="/App_Themes/CSSBUYSHIP/images/tron.png" alt="">
                            </div>
                            <ul>
                                <asp:Literal runat="server" ID="ltrStep1"></asp:Literal>
                            </ul>
                        </nav>
                        <asp:Literal runat="server" ID="ltrStep2"></asp:Literal>
                    </div>
                </div>
            </div>
        </section>
        <section class="sec sec-ql-customer">
            <div class="container">
                <div class="img-truck">
                    <img src="/App_Themes/CSSBUYSHIP/images/don-vi-chuyen-mua-ho-hang-my-uy-tin-danh-cho-ban2.jpg" alt="">
                </div>
                <div class="table-customer">
                    <div class="main-title text-center">
                        <h3 class="title wow fadeInRight" data-wow-delay=".3s" data-wow-duration="1s">quyền lợi khách hàng
                        </h3>
                    </div>
                    <div class="columns">
                        <asp:Literal runat="server" ID="ltrBenefits"></asp:Literal>
                    </div>
                </div>
                <div class="contact-bottom wow fadeInUp" data-wow-delay=".6s" data-wow-duration="1s">
                    <ul class="list-contact">
                        <li>
                            <a>
                                <span class="icon-ct">
                                    <img src="/App_Themes/CSSBUYSHIP/images/ct-1.png" alt="">
                                </span>
                                <asp:Literal runat="server" ID="ltrHotline"></asp:Literal>
                            </a>
                        </li>
                        <li>
                            <a>
                                <span class="icon-ct">
                                    <img src="/App_Themes/CSSBUYSHIP/images/ct-2.png" alt="">
                                </span>
                                <asp:Literal runat="server" ID="ltrEmail"></asp:Literal>
                            </a>
                        </li>
                        <li>
                            <a>
                                <span class="icon-ct">
                                    <img src="/App_Themes/CSSBUYSHIP/images/ct-3.png" alt="">
                                </span>
                                <asp:Literal runat="server" ID="ltrTime"></asp:Literal>
                            </a>
                        </li>
                    </ul>
                </div>
            </div>
        </section>
    </main>
    <asp:HiddenField ID="hdfWebsearch" runat="server" />
    <script type="text/javascript">

        $(document).ready(function () {
            $('.txtSearch').on("keypress", function (e) {
                if (e.keyCode == 13) {
                    searchProduct();
                }
            });
        });

        function keyclose_ms(e) {
            if (e.keyCode == 27) {
                close_popup_ms();
            }
        }

        function searchProduct() {
            var web = $("#brand-source").val();
            $("#<%=hdfWebsearch.ClientID%>").val(web);
            $("#<%=btnSearch.ClientID%>").click();
        }

        $(document).ready(function () {
            $.ajax({
                type: "POST",
                url: "/Default.aspx/getPopup",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d != "null") {
                        var data = JSON.parse(msg.d);
                        var title = data.NotiTitle;
                        var content = data.NotiContent;
                        var email = data.NotiEmail;
                        var obj = $('form');
                        $(obj).css('overflow', 'hidden');
                        $(obj).attr('onkeydown', 'keyclose_ms(event)');
                        var bg = "<div id='bg_popup_home'></div>";
                        var fr = "<div id='pupip_home' class=\"columns-container1\">" +
                            "  <div class=\"center_column col-xs-12 col-sm-5\" id=\"popup_content_home\">";
                        fr += "<div class=\"popup_header\">";
                        fr += title;
                        fr += "<a style='cursor:pointer;right:5px;' onclick='close_popup_ms()' class='close_message'></a>";
                        fr += "</div>";
                        fr += "     <div class=\"changeavatar\">";
                        fr += "         <div class=\"content1\">";
                        fr += content;
                        fr += "         </div>";
                        fr += "         <div class=\"content2\">";
                        fr += "<a href=\"javascript:;\" class=\"btn btn-close-full\" onclick='closeandnotshow()'>Đóng & không hiện</a>";
                        fr += "<a href=\"javascript:;\" class=\"btn btn-close\" onclick='close_popup_ms()'>Đóng</a>";
                        fr += "         </div>";
                        fr += "     </div>";
                        fr += "<div class=\"popup_footer\">";
                        fr += "<span class=\"float-right\">" + email + "</span>";
                        fr += "</div>";
                        fr += "   </div>";
                        fr += "</div>";
                        $(bg).appendTo($(obj)).show().animate({ "opacity": 0.7 }, 800);
                        $(fr).appendTo($(obj));
                        setTimeout(function () {
                            $('#pupip').show().animate({ "opacity": 1, "top": 20 + "%" }, 200);
                            $("#bg_popup").attr("onclick", "close_popup_ms()");
                        }, 1000);
                    }
                },
                error: function (xmlhttprequest, textstatus, errorthrow) {
                    alert('lỗi');
                }
            });
        });

        function close_popup_ms() {
            $("#pupip_home").animate({ "opacity": 0 }, 400);
            $("#bg_popup_home").animate({ "opacity": 0 }, 400);
            setTimeout(function () {
                $("#pupip_home").remove();
                $(".zoomContainer").remove();
                $("#bg_popup_home").remove();
                $('body').css('overflow', 'auto').attr('onkeydown', '');
            }, 500);
        }

        function closeandnotshow() {
            $.ajax({
                type: "POST",
                url: "/Default.aspx/setNotshow",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    close_popup_ms();
                },
                error: function (xmlhttprequest, textstatus, errorthrow) {
                    alert('lỗi');
                }
            });
        }
    </script>
    <style>
        #bg_popup_home {
            position: fixed;
            width: 100%;
            height: 100%;
            background-color: #333;
            opacity: 0.7;
            filter: alpha(opacity=70);
            left: 0px;
            top: 0px;
            z-index: 999999999;
            opacity: 0;
            filter: alpha(opacity=0);
        }

        #popup_ms_home {
            background: #fff;
            border-radius: 0px;
            box-shadow: 0px 2px 10px #fff;
            float: left;
            position: fixed;
            width: 735px;
            z-index: 10000;
            left: 50%;
            margin-left: -370px;
            top: 200px;
            opacity: 0;
            filter: alpha(opacity=0);
            height: 360px;
        }

            #popup_ms_home .popup_body {
                border-radius: 0px;
                float: left;
                position: relative;
                width: 735px;
            }

            #popup_ms_home .content {
                /*background-color: #487175;     border-radius: 10px;*/
                margin: 12px;
                padding: 15px;
                float: left;
            }

            #popup_ms_home .title_popup {
                /*background: url("../images/img_giaoduc1.png") no-repeat scroll -200px 0 rgba(0, 0, 0, 0);*/
                color: #ffffff;
                font-family: Arial;
                font-size: 24px;
                font-weight: bold;
                height: 35px;
                margin-left: 0;
                margin-top: -5px;
                padding-left: 40px;
                padding-top: 0;
                text-align: center;
            }

            #popup_ms_home .text_popup {
                color: #fff;
                font-size: 14px;
                margin-top: 20px;
                margin-bottom: 20px;
                line-height: 20px;
            }

                #popup_ms_home .text_popup a.quen_mk, #popup_ms_home .text_popup a.dangky {
                    color: #FFFFFF;
                    display: block;
                    float: left;
                    font-style: italic;
                    list-style: -moz-hangul outside none;
                    margin-bottom: 5px;
                    margin-left: 110px;
                    -webkit-transition-duration: 0.3s;
                    -moz-transition-duration: 0.3s;
                    transition-duration: 0.3s;
                }

                    #popup_ms_home .text_popup a.quen_mk:hover, #popup_ms_home .text_popup a.dangky:hover {
                        color: #8cd8fd;
                    }

            #popup_ms_home .close_popup {
                background: url("/App_Themes/Camthach/images/close_button.png") no-repeat scroll 0 0 rgba(0, 0, 0, 0);
                display: block;
                height: 28px;
                position: absolute;
                right: 0px;
                top: 5px;
                width: 26px;
                cursor: pointer;
                z-index: 10;
            }

        #popup_content_home {
            height: auto;
            position: fixed;
            background-color: #fff;
            z-index: 999999999;
            border-radius: 10px;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
            width: 75vw;
        }

        .popup_header, .popup_footer {
            float: left;
            width: 100%;
            background: #ff6600;
            padding: 10px;
            position: relative;
            color: #fff;
        }

        .popup_header {
            font-weight: bold;
            font-size: 16px;
            text-transform: uppercase;
        }

        .close_message {
            top: 10px;
        }

        .changeavatar {
            padding: 10px;
            margin: 5px 0;
            float: left;
            width: 100%;
            height: fit-content;
            max-height: 90vh;
            overflow-y: auto;
        }

        .float-right {
            float: right;
        }

        .content1 {
            float: left;
            width: 100%;
        }

        .content2 {
            float: left;
            width: 100%;
            border-top: 1px solid #eee;
            clear: both;
            margin-top: 10px;
        }

        .btn.btn-close {
            float: right;
            background: #2f78d3;
            color: #fff;
            margin: 10px 5px;
            text-transform: none;
            padding: 10px 20px;
            width: unset;
        }

            .btn.btn-close:hover {
                background: #365393;
            }

        .btn.btn-close-full {
            float: right;
            background: #7bb1c7;
            color: #fff;
            margin: 10px 5px;
            text-transform: none;
            padding: 10px 20px;
            width: unset;
        }

            .btn.btn-close-full:hover {
                background: #435156;
            }

        @media screen and (max-width: 480px) {
            .changeavatar {
                height: 50vh;
            }
        }
    </style>
</asp:Content>
