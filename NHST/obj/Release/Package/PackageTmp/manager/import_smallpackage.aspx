<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/manager/adminMasterNew.Master" CodeBehind="import_smallpackage.aspx.cs" Inherits="NHST.manager.import_smallpackage" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <link rel="stylesheet" type="text/css" href="/App_Themes/AdminNew45/assets/js/lightgallery/css/lightgallery.min.css">
    <script src="/App_Themes/AdminNew45/assets/js/lightgallery/js/lightgallery-all.min.js" type="text/javascript"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="main" class="">
        <div class="row">
            <div class="content-wrapper-before bg-dark-gradient"></div>
            <div class="page-title">
                <div class="card-panel">
                    <h4 class="title no-margin" style="display: inline-block;">Import excel</h4>
                </div>
            </div>
            <div class="list-staff col s12 section">
                <div class="list-table card-panel">
                    <div class="filter">
                        <div class="select-bao">
                            <div class="input-field inline">
                                <asp:DropDownList ID="ddlBigpack" runat="server" CssClass="select2"
                                    DataValueField="ID" DataTextField="">
                                </asp:DropDownList>
                                <%--<select id="ddlBigpack">
                                </select>--%>
                            </div>
                            <a href="#addBadge" class="btn modal-trigger waves-effect">Tạo mới bao lớn</a>
                        </div>
                        <div class="search-name input-field no-margin">
                            <asp:FileUpload ID="FileUpload1" runat="server" />
                        </div>
                        <asp:Button ID="btnImport" runat="server" CssClass="btn waves-effect mt-1" ValidationGroup="Group"  Text="Import" OnClick="btnImport_Click" Style="margin-top: 24px; color: #fff !important"></asp:Button>
                        <asp:Button ID="btnExport" runat="server" CssClass="btn waves-effect mt-1" ValidationGroup="Group" Text="Xuất file mẫu" OnClick="btnExport_Click" Style="margin-top: 24px; color: #fff !important"></asp:Button>
                    </div>

                </div>
            </div>
        </div>
    </div>
    <div id="addBadge" class="modal modal-big add-package">
        <div class="modal-hd">
            <span class="right"><i class="material-icons modal-close right-align">clear</i></span>
            <h4 class="no-margin center-align">Thêm bao lớn mới</h4>
        </div>
        <div class="modal-bd">
            <div class="row">

                <div class="input-field col s12 m4">
                    <asp:TextBox runat="server" ID="txtPackageCode" CssClass="validate" placeholder="Mã bao hàng"></asp:TextBox>
                    <span class="error-info-show">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPackageCode" Display="Dynamic"
                            ErrorMessage="Không để trống" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </span>
                    <label for="kg_weight">Mã bao hàng</label>
                </div>

                <div class="input-field col s12 m4">
                    <telerik:RadNumericTextBox runat="server" CssClass="validate" Skin="MetroTouch"
                        ID="pWeight" MinValue="0" NumberFormat-DecimalDigits="2"
                        NumberFormat-GroupSizes="3" Width="100%" placeholder="Cân (kg)" Value="0">
                    </telerik:RadNumericTextBox>
                    <label for="kg_weight" class="active">Cân nặng (kg)</label>
                </div>
                <div class="input-field col s12 m4">
                    <telerik:RadNumericTextBox runat="server" CssClass="validate" Skin="MetroTouch"
                        ID="pVolume" MinValue="0" NumberFormat-DecimalDigits="2"
                        NumberFormat-GroupSizes="3" Width="100%" placeholder="Khối (m3)" Value="0">
                    </telerik:RadNumericTextBox>
                    <label for="m2_weigth" class="active">Khối (m<sup>3</sup>)</label>

                </div>
            </div>
        </div>
        <div class="modal-ft">
            <div class="ft-wrap center-align">
                <a href="javascript:;" onclick="AddBigPackage()" class="modal-action btn modal-close waves-effect waves-green mr-2 submit-button">Thêm</a>
                <a href="javascript:;" class="modal-action btn orange darken-2 modal-close waves-effect waves-green ml-2">Hủy</a>
            </div>
        </div>
    </div>
    <asp:HiddenField runat="server" ID="hdfID" />
    <script type="text/javascript">
        function isEmpty(str) {
            return !str.replace(/^\s+/g, '').length; // boolean (`true` if field is empty)
        }
        function AddBigPackage() {
            var packageCode = $("#<%=txtPackageCode.ClientID%>").val();
            var weight = $("#<%=pWeight.ClientID%>").val();
            var Volume = $("#<%=pVolume.ClientID%>").val();
            if (!isEmpty(packageCode)) {
                $.ajax({
                    type: "POST",
                    url: "/manager/import_smallpackage.aspx/AddBigPackage",
                    data: "{PackageCode:'" + packageCode + "',Weight:'" + weight + "',Volume:'" + Volume + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        var data = msg.d;
                        if (data != null) {
                            if (data != "existCode") {
                                //loadBigPackage(parseInt(data));
                                location.reload();
                            }
                            else {
                                alert('Mã bao hàng đã tồn tài.');
                            }
                        }
                        else {
                            alert('Có lỗi trong quá trình xử lý.');
                        }
                    }
                })
            }
            else {
                alert('Không được để trống mã bao hàng!');
            }
        }
    </script>
</asp:Content>
