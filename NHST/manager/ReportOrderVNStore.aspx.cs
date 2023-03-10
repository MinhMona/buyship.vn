using MB.Extensions;
using NHST.Controllers;
using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using ZLADIPJ.Business;
using static NHST.Controllers.MainOrderController;
using static NHST.Controllers.StaffIncomeController;

namespace NHST.manager
{
    public partial class ReportOrderVNStore : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userLoginSystem"] == null)
                {
                    Response.Redirect("/manager/Login.aspx");
                }
                else
                {
                    string Username = Session["userLoginSystem"].ToString();
                    var obj_user = AccountController.GetByUsername(Username);
                    if (obj_user != null)
                    {
                        if (obj_user.RoleID != 0 && obj_user.RoleID != 2)
                        {
                            Response.Redirect("/trang-chu");
                        }
                    }
                }
                loadFilter();
                LoadData();
            }
        }

        public void loadFilter()
        {
            ddlStatus.SelectedValue = "-1";
        }
        public void LoadData()
        {
            string st = Request.QueryString["st"];
            if (!string.IsNullOrEmpty(st))
            {
                var list = st.Split(',').ToList();

                for (int j = 0; j < list.Count; j++)
                {
                    for (int i = 0; i < ddlStatus.Items.Count; i++)
                    {
                        var item = ddlStatus.Items[i];
                        if (item.Value == list[j])
                        {
                            ddlStatus.Items[i].Selected = true;
                        }
                    }
                }
            }
            string fd = Request.QueryString["fd"];
            string td = Request.QueryString["td"];
            if (!string.IsNullOrEmpty(fd))
            {
                rFD.Text = fd;

                fd = DateTime.ParseExact(fd, "dd/MM/yyyy HH:mm", null).ToString();
            }
            else
            {
                fd = null;
            }
            if (!string.IsNullOrEmpty(td))
            {

                rTD.Text = td;

                td = DateTime.ParseExact(td, "dd/MM/yyyy HH:mm", null).ToString();
            }
            else
            {
                td = null;
            }
            string search = "";
            if (!string.IsNullOrEmpty(Request.QueryString["s"]))
            {
                search = Request.QueryString["s"].ToString().Trim();
                search_name.Text = search;
            }

            int page = 0;
            Int32 Page = GetIntFromQueryString("Page");
            if (Page > 0)
            {
                page = Page - 1;
            }
            string username_current = Session["userLoginSystem"].ToString();
            tbl_Account ac = AccountController.GetByUsername(username_current);
            var la = MainOrderController.GetOrderINVNStoreList(0, search, fd, td, null, 20, page*20, 0);
            var tt = MainOrderController.GetOrderINVNStoreListExcel(0, search, fd, td, null, 0); ;
            var totalData = MainOrderController.GetOrderINVNStoreListTotal(0, search, fd, td, null);

            double tongtienconno = totalData.totalPriceVND.Value - totalData.totalDeposit.Value;
            ltrTonggiatridonhang.Text = string.Format("{0:N0}", totalData.totalPriceVND) + " VNĐ";
            ltrTongphimuahang.Text = string.Format("{0:N0}", totalData.totalFeeBuyPro) + " VNĐ";
            ltrTongvanchuyenqt.Text = string.Format("{0:N0}", totalData.totalFeeWeight) + " VNĐ";
            ltrTongvanchuyennoidia.Text = string.Format("{0:N0}", totalData.totalFeeShipCN) + " VNĐ";
            ltrTongcannang.Text = string.Format("{0:N1}",  totalData.totalOrderWeight) + " KG";
            ltrTongsodonhang.Text = string.Format("{0:N0}", totalData.totalMainOrder);
            ltrTongtienconno.Text = string.Format("{0:N0}", tongtienconno) + " VNĐ";
            ltrTongtiendatra.Text = string.Format("{0:N0}", totalData.totalDeposit) + " VNĐ";

            int total = 0;
            if (la.Count > 0)
            {
                total = la[0].totalRow.Value;
            }
            pagingall(la, total);
        }
        protected void search_Click(object sender, EventArgs e)
        {
            string st = "";
            if (!string.IsNullOrEmpty(ddlStatus.SelectedValue))
            {
                List<string> myValues = new List<string>();
                for (int i = 0; i < ddlStatus.Items.Count; i++)
                {
                    var item = ddlStatus.Items[i];
                    if (item.Selected)
                    {
                        myValues.Add(item.Value);
                    }
                }
                st = String.Join(",", myValues.ToArray());
            }
            string searchname = search_name.Text.Trim();
            string fd = "";
            string td = "";
            if (!string.IsNullOrEmpty(rFD.Text))
            {
                fd = rFD.Text.ToString();
            }
            if (!string.IsNullOrEmpty(rTD.Text))
            {
                td = rTD.Text.ToString();
            }
            if (string.IsNullOrEmpty(searchname) == true && string.IsNullOrEmpty(st) == true && fd == "" && td == "")
            {
                Response.Redirect("ReportOrderVNStore.aspx");
            }
            else
            {
                Response.Redirect("ReportOrderVNStore.aspx?s=" + searchname + "&st=7&fd=" + fd + "&td=" + td + "");
            }

        }
        #region Pagging
        public void pagingall(List<ReportOrderINVNStore_Result> acs, int total)
        {
            string username_current = Session["userLoginSystem"].ToString();
            tbl_Account ac = AccountController.GetByUsername(username_current);
            int PageSize = 20;
            if (total > 0)
            {
                int TotalItems = total;
                if (TotalItems % PageSize == 0)
                    PageCount = TotalItems / PageSize;
                else
                    PageCount = TotalItems / PageSize + 1;

                Int32 Page = GetIntFromQueryString("Page");

                if (Page == -1) Page = 1;
                int FromRow = (Page - 1) * PageSize;
                int ToRow = Page * PageSize - 1;
                if (ToRow >= TotalItems)
                    ToRow = TotalItems - 1;
                StringBuilder hcm = new StringBuilder();
                for (int i = 0; i < acs.Count; i++)
                {
                    var item = acs[i];
                    hcm.Append("<tr>");
                    hcm.Append("<td>" + item.SaleUserName + "</td>");
                    hcm.Append("<td>" + item.Username + "</td>");
                    hcm.Append("<td>" + string.Format("{0:N0}", Convert.ToDouble(item.totalPriceVND)) + "</td>");
                    hcm.Append("<td>" + string.Format("{0:N0}", Convert.ToDouble(item.totalFeeBuyPro)) + "</td>");
                    hcm.Append("<td>" + string.Format("{0:N0}", Convert.ToDouble(item.totalFeeShipCN)) + "</td>");
                    hcm.Append("<td>" + string.Format("{0:N0}", Convert.ToDouble(item.totalFeeWeight)) + "</td>");
                    hcm.Append("<td>" + string.Format("{0:N0}", Convert.ToDouble(item.totalDeposit)) + "</td>");
                    hcm.Append("<td>" + string.Format("{0:N0}", (Convert.ToDouble(item.totalPriceVND)- Convert.ToDouble(item.totalDeposit))) + "</td>");
                    hcm.Append("<td>" + item.totalOrderWeight + "</td>");
                    hcm.Append("<td>" + string.Format("{0:N0}", Convert.ToDouble(item.totalMainOrder)) + "</td>");
                    /*var a = AccountController.GetAllSaleID(item.SalerID);
                    if (a != null)
                    {
                        if (a.Count > 0)
                        {
                            hcm.Append("<td>" + a.Count + "</td>");
                        }
                    }
                    else
                    {
                        hcm.Append("<td>0</td>");
                    }*/
                    //hcm.Append("<td>");
                    //hcm.Append(" <div class=\"action-table\">");
                    //hcm.Append("<a href =\"userlist-sale.aspx?id=" + item.SalerID + "\" target=\"_blank\" data-position=\"top\" ><i class=\"material-icons\">edit</i><span>Xem</span></a>");
                    //hcm.Append("</div>");
                    //hcm.Append("</td>");
                    hcm.Append("</tr>");

                }
                ltr.Text = hcm.ToString();

            }
        }

        public static Int32 GetIntFromQueryString(String key)
        {
            Int32 returnValue = -1;
            String queryStringValue = HttpContext.Current.Request.QueryString[key];
            try
            {
                if (queryStringValue == null)
                    return returnValue;
                if (queryStringValue.IndexOf("#") > 0)
                    queryStringValue = queryStringValue.Substring(0, queryStringValue.IndexOf("#"));
                returnValue = Convert.ToInt32(queryStringValue);
            }
            catch
            { }
            return returnValue;
        }
        private int PageCount;
        protected void DisplayHtmlStringPaging1()
        {
            Int32 CurrentPage = Convert.ToInt32(Request.QueryString["Page"]);
            if (CurrentPage == -1) CurrentPage = 1;
            string[] strText = new string[4] { "Trang đầu", "Trang cuối", "Trang sau", "Trang trước" };
            if (PageCount > 1)
                Response.Write(GetHtmlPagingAdvanced(6, CurrentPage, PageCount, Context.Request.RawUrl, strText));
        }
        private static string GetPageUrl(int currentPage, string pageUrl)
        {
            pageUrl = Regex.Replace(pageUrl, "(\\?|\\&)*" + "Page=" + currentPage, "");
            if (pageUrl.IndexOf("?") > 0)
            {
                if (pageUrl.IndexOf("Page=") > 0)
                {
                    int a = pageUrl.IndexOf("Page=");
                    int b = pageUrl.Length;
                    pageUrl.Remove(a, b - a);
                }
                else
                {
                    pageUrl += "&Page={0}";
                }

            }
            else
            {
                pageUrl += "?Page={0}";
            }
            return pageUrl;
        }
        public static string GetHtmlPagingAdvanced(int pagesToOutput, int currentPage, int pageCount, string currentPageUrl, string[] strText)
        {
            //Nếu Số trang hiển thị là số lẻ thì tăng thêm 1 thành chẵn
            if (pagesToOutput % 2 != 0)
            {
                pagesToOutput++;
            }

            //Một nửa số trang để đầu ra, đây là số lượng hai bên.
            int pagesToOutputHalfed = pagesToOutput / 2;

            //Url của trang
            string pageUrl = GetPageUrl(currentPage, currentPageUrl);


            //Trang đầu tiên
            int startPageNumbersFrom = currentPage - pagesToOutputHalfed; ;

            //Trang cuối cùng
            int stopPageNumbersAt = currentPage + pagesToOutputHalfed; ;

            StringBuilder output = new StringBuilder();

            //Nối chuỗi phân trang
            //output.Append("<div class=\"paging\">");
            //output.Append("<ul class=\"paging_hand\">");

            //Link First(Trang đầu) và Previous(Trang trước)
            if (currentPage > 1)
            {
                //output.Append("<li class=\"UnselectedPrev \" ><a title=\"" + strText[0] + "\" href=\"" + string.Format(pageUrl, 1) + "\">|<</a></li>");
                //output.Append("<li class=\"UnselectedPrev\" ><a title=\"" + strText[1] + "\" href=\"" + string.Format(pageUrl, currentPage - 1) + "\"><i class=\"fa fa-angle-left\"></i></a></li>");
                output.Append("<a class=\"prev-page pagi-button\" title=\"" + strText[1] + "\" href=\"" + string.Format(pageUrl, currentPage - 1) + "\">Prev</a>");
                //output.Append("<span class=\"Unselect_prev\"><a href=\"" + string.Format(pageUrl, currentPage - 1) + "\"></a></span>");
            }

            /******************Xác định startPageNumbersFrom & stopPageNumbersAt**********************/
            if (startPageNumbersFrom < 1)
            {
                startPageNumbersFrom = 1;

                //As page numbers are starting at one, output an even number of pages.  
                stopPageNumbersAt = pagesToOutput;
            }

            if (stopPageNumbersAt > pageCount)
            {
                stopPageNumbersAt = pageCount;
            }

            if ((stopPageNumbersAt - startPageNumbersFrom) < pagesToOutput)
            {
                startPageNumbersFrom = stopPageNumbersAt - pagesToOutput;
                if (startPageNumbersFrom < 1)
                {
                    startPageNumbersFrom = 1;
                }
            }
            /******************End: Xác định startPageNumbersFrom & stopPageNumbersAt**********************/

            //Các dấu ... chỉ những trang phía trước  
            if (startPageNumbersFrom > 1)
            {
                output.Append("<a href=\"" + string.Format(GetPageUrl(currentPage - 1, pageUrl), startPageNumbersFrom - 1) + "\">&hellip;</a>");
            }

            //Duyệt vòng for hiển thị các trang
            for (int i = startPageNumbersFrom; i <= stopPageNumbersAt; i++)
            {
                if (currentPage == i)
                {
                    output.Append("<a class=\"pagi-button current-active\">" + i.ToString() + "</a>");
                }
                else
                {
                    output.Append("<a class=\"pagi-button\" href=\"" + string.Format(pageUrl, i) + "\">" + i.ToString() + "</a>");
                }
            }

            //Các dấu ... chỉ những trang tiếp theo  
            if (stopPageNumbersAt < pageCount)
            {
                output.Append("<a href=\"" + string.Format(pageUrl, stopPageNumbersAt + 1) + "\">&hellip;</a>");
            }

            //Link Next(Trang tiếp) và Last(Trang cuối)
            if (currentPage != pageCount)
            {
                //output.Append("<span class=\"Unselect_next\"><a href=\"" + string.Format(pageUrl, currentPage + 1) + "\"></a></span>");
                //output.Append("<li class=\"UnselectedNext\" ><a title=\"" + strText[2] + "\" href=\"" + string.Format(pageUrl, currentPage + 1) + "\"><i class=\"fa fa-angle-right\"></i></a></li>");
                output.Append("<a class=\"next-page pagi-button\" title=\"" + strText[2] + "\" href=\"" + string.Format(pageUrl, currentPage + 1) + "\">Next</a>");
                //output.Append("<li class=\"UnselectedNext\" ><a title=\"" + strText[3] + "\" href=\"" + string.Format(pageUrl, pageCount) + "\">>|</a></li>");
            }
            //output.Append("</ul>");
            //output.Append("</div>");
            return output.ToString();
        }
        #endregion

        public class OrderReport
        {
            public int OrderCode { get; set; }
            public string Username { get; set; }
            public string TotalPrice { get; set; }
            public string TotalBuyProReal { get; set; }
            public string TotalIncome { get; set; }
            public string CreatedDate { get; set; }
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            string st = Request.QueryString["st"];
            if (!string.IsNullOrEmpty(st))
            {
                var list = st.Split(',').ToList();

                for (int j = 0; j < list.Count; j++)
                {
                    for (int i = 0; i < ddlStatus.Items.Count; i++)
                    {
                        var item = ddlStatus.Items[i];
                        if (item.Value == list[j])
                        {
                            ddlStatus.Items[i].Selected = true;
                        }
                    }
                }
            }

            string search = "";
            if (!string.IsNullOrEmpty(Request.QueryString["s"]))
            {
                search = Request.QueryString["s"].ToString().Trim();
                search_name.Text = search;
            }
            string username_current = Session["userLoginSystem"].ToString();
            tbl_Account ac = AccountController.GetByUsername(username_current);
            string fd = Request.QueryString["fd"];
            string td = Request.QueryString["td"];
            if (!string.IsNullOrEmpty(rFD.Text))
            {
                fd = rFD.Text.ToString();
            }
            if (!string.IsNullOrEmpty(rTD.Text))
            {
                td = rTD.Text.ToString();
            }
            var tt = MainOrderController.GetFromDateToDate_IncomSaler_Thang_total(fd, td, search, ac.Username, st);
            StringBuilder StrExport = new StringBuilder();
            StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
            StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
            StrExport.Append("<DIV  style='font-size:12px;'>");
            StrExport.Append("<table border=\"1\">");
            StrExport.Append("  <tr>");
            StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Saler</strong></th>");
            StrExport.Append("      <th><strong>Giá trị đơn hàng</strong></th>");
            StrExport.Append("      <th><strong>Tiền hàng</strong></th>");
            StrExport.Append("      <th><strong>Phí đơn hàng</strong></th>");
            StrExport.Append("      <th><strong>Phí mua hàng</strong></th>");
            StrExport.Append("      <th><strong>Vận chuyển TQ-VN</strong></th>");
            StrExport.Append("      <th><strong>Vận chuyển nội địa</strong></th>");
            StrExport.Append("      <th><strong>Mặc cả</strong></th>");
            StrExport.Append("      <th><strong>Cân nặng</strong></th>");
            StrExport.Append("      <th><strong>Số đơn hàng</strong></th>");
            StrExport.Append("      <th><strong>Số khách hàng</strong></th>");
            StrExport.Append("  </tr>");
            foreach (var item in tt)
            {
                StrExport.Append("  <tr>");
                StrExport.Append("      <td  >" + item.Dealer + "</td>");
                StrExport.Append("      <td >" + string.Format("{0:N0}", Convert.ToDouble(item.TotalPriceVND)) + "</td>");
                StrExport.Append("      <td>" + string.Format("{0:N0}", Convert.ToDouble(item.PriceVND)) + "</td>");
                StrExport.Append("      <td >" + string.Format("{0:N0}", Convert.ToDouble(item.phidonhang)) + "</td>");
                StrExport.Append("      <td >" + string.Format("{0:N0}", Convert.ToDouble(item.FeeBuyPro)) + "</td>");
                StrExport.Append("      <td >" + string.Format("{0:N0}", Convert.ToDouble(item.FeeWeight)) + "</td>");
                StrExport.Append("      <td >" + string.Format("{0:N0}", Convert.ToDouble(item.FeeShipCN)) + "</td>");
                StrExport.Append("      <td >" + string.Format("{0:N0}", Convert.ToDouble(item.tienmacca)) + "</td>");
                StrExport.Append("      <td >" + item.TQVNWeight + "</td>");
                StrExport.Append("      <td >" + string.Format("{0:N0}", Convert.ToDouble(item.TotalOrder)) + "</td>");

                var a = AccountController.GetAllSaleID(item.SalerID);
                if (a != null)
                {
                    if (a.Count > 0)
                    {
                        StrExport.Append("<td>" + a.Count + "</td>");
                    }
                }

                else
                {
                    StrExport.Append("<td>0</td>");
                }
                StrExport.Append("  </tr>");
            }
            StrExport.Append("</table>");
            StrExport.Append("</div></body></html>");
            string strFile = "Thong-ke-doanh-thu-cho-saler.xls";
            string strcontentType = "application/vnd.ms-excel";
            Response.ClearContent();
            Response.ClearHeaders();
            Response.BufferOutput = true;
            Response.ContentType = strcontentType;
            Response.AddHeader("Content-Disposition", "attachment; filename=" + strFile);
            Response.Write(StrExport.ToString());
            Response.Flush();
            //Response.Close();
            Response.End();
        }

    }
}