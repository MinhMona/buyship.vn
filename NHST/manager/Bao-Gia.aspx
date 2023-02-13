<%@ Page Title="" Language="C#" MasterPageFile="~/manager/adminMasterNew.Master" AutoEventWireup="true" CodeBehind="Bao-Gia.aspx.cs" Inherits="NHST.manager.Bao_Gia" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="NHST.Controllers" %>
<%@ Import Namespace="NHST.Models" %>
<%@ Import Namespace="NHST.Bussiness" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Button Style="display: none" UseSubmitBehavior="false" ID="btnSearch" runat="server" OnClick="btnSearch_Click" />
    <div id="main" class="main-full">
        <div class="row">
            <div class="content-wrapper-before bg-dark-gradient"></div>
            <div class="page-title">
                <div class="card-panel">
                    <h4 class="title no-margin" style="display: inline-block;">DANH SÁCH YÊU CẦU BÁO GIÁ</h4>
                    <div class="clearfix"></div>
                </div>
            </div>
            <div class="list-staff col s12 section">
                <div class="list-table card-panel">
                    <div class="filter">
                        <div class="row">
                            <div class="input-field col s6 m4 l2">
                                <asp:TextBox runat="server" type="text" ID="search_name" placeholder="" class=""></asp:TextBox>
                                <label>Họ Tên</label>
                            </div>
                            <div class="input-field col s6 m4 l2">
                                <asp:TextBox runat="server" type="text" ID="search_phone" placeholder="" class=""></asp:TextBox>
                                <label>Số điện thoại</label>
                            </div>
                            <div class="input-field col s6 m4 l2">
                                <asp:DropDownList runat="server" ID="ddlStatus">
                                    <asp:ListItem Value="0" Text="Tất cả"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Chờ báo giá"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Đã báo giá"></asp:ListItem>
                                </asp:DropDownList>
                                <label>Trạng thái</label>
                            </div>
                            <div class="input-field col s12 m4 l2">
                                <a href="javascript:;" onclick="myFunction()" class="btn btnsearch">Lọc</a>
                            </div>
                        </div>
                        <div class="clearfix"></div>
                    </div>
                    <table class="table responsive-table bordered highlight">
                        <thead>
                            <tr>
                                <th>ID</th>
                                <th>Tên khách hàng</th>
                                <th>Số điện thoại</th>                                
                                <th>Email</th> 
                                <th>Ý kiến khách hàng</th>
                                <th>Ngày gửi</th>
                                <th>Trạng thái</th>
                                <th>Thao tác</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Literal ID="ltr" runat="server" EnableViewState="false"></asp:Literal>
                        </tbody>
                    </table>
                    <div class="pagi-table float-right mt-2">
                        <%this.DisplayHtmlStringPaging1();%>
                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hdfID" runat="server" />
    <asp:HiddenField ID="hdfFullName" runat="server" />
    <asp:Button ID="btnUpStatus" runat="server" Style="display: none" OnClick="btnUpStatus_Click" />
    <script>
        function UpdateStatus(ID) {
            $("#<%=hdfID.ClientID%>").val(ID);
            $('#<%=btnUpStatus.ClientID%>').click();
        }

        function myFunction() {
            $('#<%=btnSearch.ClientID%>').click();
        }

        $('.search-action').click(function () {
            console.log('dkm ngon');
            console.log($('#<%=search_name.ClientID%>').val());
            $('#<%=btnSearch.ClientID%>').click();
        })
        $(document).ready(function () {
            $('#search_name').autocomplete({
                data: {
                    "Apple": null,
                    "Microsoft": null,
                    "Google": 'https://placehold.it/250x250',
                    "Asgard": null
                },
            });
        });
    </script>
</asp:Content>

