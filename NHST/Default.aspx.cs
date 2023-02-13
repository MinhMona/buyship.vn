using NHST.Bussiness;
using NHST.Controllers;
using Supremes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace NHST
{
    public partial class Default7 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();                
            }
        }
        public void LoadData()
        {
            var confi = ConfigurationController.GetByTop1();
            if (confi != null)
            {
                ltrHotline.Text += "<span class=\"text-ct\">HOTLINE <br/><i href=\"tel:" + confi.Hotline + "\">" + confi.Hotline + "</i></span>";
                ltrEmail.Text += "<span class=\"text-ct\">EMAIL <br/><i href=\"mailto:" + confi.EmailContact + "\">" + confi.EmailContact + "</i></span>";
                ltrTime.Text += "<span class=\"text-ct\">GIỜ HOẠT ĐỘNG <br/><i>" + confi.TimeWork + "</i></span>";

                try
                {
                    string weblink = "https://BUYSHIP.VN";
                    string url = HttpContext.Current.Request.Url.AbsoluteUri;

                    HtmlHead objHeader = (HtmlHead)Page.Header;                   
                    HtmlMeta objMetaFacebook = new HtmlMeta();

                    objMetaFacebook = new HtmlMeta();
                    objMetaFacebook.Attributes.Add("property", "fb:app_id");
                    objMetaFacebook.Content = "676758839172144";
                    objHeader.Controls.Add(objMetaFacebook);

                    objMetaFacebook = new HtmlMeta();
                    objMetaFacebook.Attributes.Add("property", "og:url");
                    objMetaFacebook.Content = url;
                    objHeader.Controls.Add(objMetaFacebook);

                    objMetaFacebook = new HtmlMeta();
                    objMetaFacebook.Attributes.Add("property", "og:type");
                    objMetaFacebook.Content = "website";
                    objHeader.Controls.Add(objMetaFacebook);

                    objMetaFacebook = new HtmlMeta();
                    objMetaFacebook.Attributes.Add("property", "og:title");
                    objMetaFacebook.Content = confi.OGTitle;
                    objHeader.Controls.Add(objMetaFacebook);

                    objMetaFacebook = new HtmlMeta();
                    objMetaFacebook.Attributes.Add("property", "og:description");
                    objMetaFacebook.Content = confi.OGDescription;
                    objHeader.Controls.Add(objMetaFacebook);

                    objMetaFacebook = new HtmlMeta();
                    objMetaFacebook.Attributes.Add("property", "og:image");
                    objMetaFacebook.Content = weblink + confi.OGImage;
                    objHeader.Controls.Add(objMetaFacebook);

                    objMetaFacebook = new HtmlMeta();
                    objMetaFacebook.Attributes.Add("property", "og:image:width");
                    objMetaFacebook.Content = "200";
                    objHeader.Controls.Add(objMetaFacebook);

                    objMetaFacebook = new HtmlMeta();
                    objMetaFacebook.Attributes.Add("property", "og:image:height");
                    objMetaFacebook.Content = "500";
                    objHeader.Controls.Add(objMetaFacebook);

                    HtmlMeta meta = new HtmlMeta();
                    meta = new HtmlMeta();
                    meta.Attributes.Add("name", "description");
                    meta.Content = confi.MetaDescription;
                    objHeader.Controls.Add(meta);

                    this.Title = confi.MetaTitle;

                    objMetaFacebook = new HtmlMeta();
                    objMetaFacebook.Attributes.Add("property", "og:title");
                    objMetaFacebook.Content = confi.OGTitle;
                    objHeader.Controls.Add(objMetaFacebook);

                    objMetaFacebook = new HtmlMeta();
                    objMetaFacebook.Attributes.Add("property", "twitter:title");
                    objMetaFacebook.Content = confi.OGTwitterTitle;
                    objHeader.Controls.Add(objMetaFacebook);

                    objMetaFacebook = new HtmlMeta();
                    objMetaFacebook.Attributes.Add("property", "twitter:description");
                    objMetaFacebook.Content = confi.OGTwitterDescription;
                    objHeader.Controls.Add(objMetaFacebook);

                    objMetaFacebook = new HtmlMeta();
                    objMetaFacebook.Attributes.Add("property", "twitter:image");
                    objMetaFacebook.Content = weblink + confi.OGTwitterImage;
                    objHeader.Controls.Add(objMetaFacebook);

                    objMetaFacebook = new HtmlMeta();
                    objMetaFacebook.Attributes.Add("property", "twitter:image:width");
                    objMetaFacebook.Content = "200";
                    objHeader.Controls.Add(objMetaFacebook);

                    objMetaFacebook = new HtmlMeta();
                    objMetaFacebook.Attributes.Add("property", "twitter:image:height");
                    objMetaFacebook.Content = "500";
                    objHeader.Controls.Add(objMetaFacebook);

                    HtmlLink canonicalTag = new HtmlLink();
                    canonicalTag.Attributes["rel"] = "canonical";
                    canonicalTag.Href = weblink;
                    Page.Header.Controls.Add(canonicalTag);
                }
                catch
                {

                }
            }

            var services = ServiceController.GetAll().OrderBy(x => x.Position).ToList();
            if (services.Count > 0)
            {
                foreach (var item in services)
                {
                    ltrServices.Text += "<div class=\"colum\">";
                    ltrServices.Text += "<div class=\"content wow fadeInUp\" data-wow-delay=\".3s\" data-wow-duration=\"1s\">";
                    ltrServices.Text += "<div class=\"icon\">";
                    ltrServices.Text += "<img src=\"" + item.ServiceIMG + "\" alt=\"\">";
                    ltrServices.Text += "</div>";
                    ltrServices.Text += "<h3 class=\"title\"><a href=\"#\">" + item.ServiceName + "</a></h3>";
                    ltrServices.Text += "<p class=\"desc\">" + item.ServiceContent + "</p>";
                    if (!string.IsNullOrEmpty(item.ServiceLink))
                        ltrServices.Text += "<a href=\"" + item.ServiceLink + "\" class=\"btn btn-xt\">Xem thêm <i class=\"fa fa-long-arrow-right\"></i></a>";
                    ltrServices.Text += "</div>";
                    ltrServices.Text += "</div>";
                }
            }

            var ql = CustomerBenefitsController.GetAllByItemType(2);
            if (ql.Count > 0)
            {
                foreach (var q in ql)
                {
                    ltrBenefits.Text += "<div class=\"colum\">";
                    ltrBenefits.Text += "<div class=\"content wow fadeInUp\" data-wow-delay=\".3s\" data-wow-duration=\"1s\" >";
                    ltrBenefits.Text += "<div class=\"icon\">";
                    ltrBenefits.Text += "<img src=\"" + q.Icon + "\" alt=\"\">";
                    ltrBenefits.Text += "</div>";
                    ltrBenefits.Text += "<h4 class=\"title\">" + q.CustomerBenefitName + "</h4>";
                    ltrBenefits.Text += "<p class=\"desc\">" + q.CustomerBenefitDescription + "</p>";
                    ltrBenefits.Text += "</div>";
                    ltrBenefits.Text += "</div>";
                }
            }

            var ViewProduct2 = ViewProductController.GetAll().Where(w => w.Type == 2).OrderBy(x => x.ViewPosition).ToList();
            if (ViewProduct2.Count > 0)
            {
                foreach (var item in ViewProduct2)
                {
                    ltrView2.Text += "<div class=\"col-4\">";
                    ltrView2.Text += "<div class=\"web-on\">";
                    ltrView2.Text += "<div class=\"demo\">";
                    ltrView2.Text += " <img src=\"" + item.ViewIMG + "\" alt=\"\">";
                    ltrView2.Text += "</div>";
                    ltrView2.Text += "<div class=\"name\">";
                    ltrView2.Text += "<a href=\"" + item.ViewLink + "\"> " + item.ViewTitle + "</a>";
                    ltrView2.Text += "</div>";
                    ltrView2.Text += "</div>";
                    ltrView2.Text += "</div>";
                }
            }

            var steps = StepController.GetAll("");
            if (steps.Count > 0)
            {
                int count = 1;
                foreach (var item in steps)
                {
                    string name = item.StepName;
                    string namenotdau = LeoUtils.ConvertToUnSign(name);

                    if (count == 1)
                    {
                        ltrStep1.Text += "<li data-tab=\"js-" + count + "\" class=\"active\"><i class=\"fa fa-long-arrow-left\"></i>" + item.StepName + "</li>";
                    }
                    else
                    {
                        ltrStep1.Text += "<li data-tab=\"js-" + count + "\"><i class=\"fa fa-long-arrow-left\"></i>" + item.StepName + "</li>";
                    }
                    if (count == 1)
                    {
                        ltrStep2.Text += "<div class=\"c-tab__content js-" + count + " active wow fadeInUp\" data-wow-delay=\".3s\" data-wow-duration=\"1s\">";
                    }
                    else
                    {
                        ltrStep2.Text += "<div class=\"c-tab__content js-" + count + "\">";
                    }
                    ltrStep2.Text += "<div class=\"box-img\">";
                    ltrStep2.Text += "<img src=\"/App_Themes/ShipBuy/images/img-hd-1.jpg\" alt=\"\">";
                    ltrStep2.Text += "</div>";
                    ltrStep2.Text += "<div class=\"content\">";
                    ltrStep2.Text += "<div class=\"title-intro\">" + item.StepName + "</div>";
                    ltrStep2.Text += "<p class=\"desc-intro\">" + item.StepContent + "</p>";
                    if (!string.IsNullOrEmpty(item.StepLink))
                        ltrStep2.Text += "<a href=\"" + item.StepLink + "\" class=\"btn btn-detail\">Xem thêm <i class=\"fa fa-long-arrow-right\"></i></a>";
                    ltrStep2.Text += "</div>";
                    ltrStep2.Text += "</div>";
                    count++;
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string text = txtSearch.Text.Trim();
                if (!string.IsNullOrEmpty(text))
                {
                    string page = hdfWebsearch.Value;
                    //if (page == "ebay" || page == "amazon" || page == "walmart" || page == "bestbuy" || page == "ashford" || page == "nike")
                    //{
                    //    string a = PJUtils.TranslateTextNew(text, "vi", "en");
                    //    a = a.Replace("[", "").Replace("]", "").Replace("\"", "");
                    //    string[] ass = a.Split(',');
                    //    SearchPage_USA(page, PJUtils.RemoveHTMLTags(ass[0]));
                    //}
                    string a = PJUtils.TranslateTextNew(text, "vi", "ja");
                    a = a.Replace("[", "").Replace("]", "").Replace("\"", "");
                    string[] ass = a.Split(',');
                    SearchPage_Japan(page, PJUtils.RemoveHTMLTags(ass[0]));

                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        public static byte[] GetHash(string inputString)
        {
            HashAlgorithm algorithm = MD5.Create();  //or use SHA1.Create();
            byte[] bytes = Encoding.GetEncoding("gb2312").GetBytes(inputString);
            return bytes;
        }

        public static string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append("%" + b.ToString("X2"));

            return sb.ToString();
        }
        public void SearchPage_Japan(string page, string text)
        {
            string linkgo = "";
            if (page == "auctions")
            {
                string a = text;
                string textsearch_auctions = GetHashString(a);
                linkgo = "https://auctions.yahoo.co.jp/search/search?p=" + textsearch_auctions + "&navigator=all&_input_charset=&spm=a21bp.7806943.20151106.1";
            }
            else if (page == "amazonjapan")
            {
                string a = text;
                string textsearch_amazonjapan = GetHashString(a);
                linkgo = "https://www.amazon.co.jp/s?k=" + textsearch_amazonjapan + "";
            }
            else if (page == "rakuten")
            {
                string a = text;
                string textsearch_rakuten = GetHashString(a);
                linkgo = "https://search.rakuten.co.jp/search/mall/" + textsearch_rakuten + "&button_click=top&earseDirect=false&n=y";
            }
            else if (page == "mercari")
            {
                string a = text;
                string textsearch_mercari = GetHashString(a);
                linkgo = "https://jp.mercari.com/search?keyword=" + textsearch_mercari + "&type=p&vmarket=&spm=875.7931836%2FB.a2227oh.d100&from=mallfp..pc_1_searchbutton";
            }
            //else if (page == "shopping")
            //{
            //    string a = text;
            //    string textsearch_shopping = GetHashString(a);
            //    linkgo = "https://shopping.yahoo.co.jp/search?p=" + textsearch_shopping + "&cid=&pf=&pt=&area=01&astk=&sc_i=shp_pc_top_searchBox_2&sretry=1";
            //}
            Response.Redirect(linkgo);            
        }
        public void SearchPage_USA(string page, string text)
        {
            string linkgo = "";
            if (page == "ebay")
            {
                string a = text;
                string textsearch_ebay = GetHashString(a);
                linkgo = "https://www.ebay.com/sch/i.html?_from=R40&_trksid=p2380057.m570.l1313&_nkw=" + textsearch_ebay + "&_sacat=0";
            }
            else if (page == "amazon")
            {
                string a = text;
                string textsearch_amazon = GetHashString(a);
                linkgo = "https://www.amazon.com/s?k=" + textsearch_amazon + "&ref=nb_sb_noss_2";
            }
            else if (page == "walmart")
            {
                string a = text;
                string textsearch_walmart = GetHashString(a);
                linkgo = "https://www.walmart.com/search?q=" + textsearch_walmart + "";
            }
            else if (page == "bestbuy")
            {
                string a = text;
                string textsearch_bestbuy = GetHashString(a);
                linkgo = "https://www.bestbuy.com/site/searchpage.jsp?st=" + textsearch_bestbuy + "&_dyncharset=UTF-8&_dynSessConf=&id=pcat17071&type=page&sc=Global&cp=1&nrp=&sp=&qp=&list=n&af=true&iht=y&usc=All+Categories&ks=960&keys=keys";
            }
            else if (page == "ashford")
            {
                string a = text;
                string textsearch_ashford = GetHashString(a);
                linkgo = "https://www.ashford.com/catalogsearch/result/?q=" + textsearch_ashford + "";
            }
            else if (page == "nike")
            {
                string a = text;
                string textsearch_nike = GetHashString(a);
                linkgo = "https://www.nike.com/vn/w?q=jordan&vst=" + textsearch_nike + "";
            }
            Response.Redirect(linkgo);
        }

        [WebMethod]
        public static string getPopup()
        {
            if (HttpContext.Current.Session["notshowpopup"] == null)
            {
                var conf = ConfigurationController.GetByTop1();
                string popup = conf.NotiPopupTitle;
                if (!string.IsNullOrEmpty(popup))
                {
                    NotiInfo n = new NotiInfo();
                    n.NotiTitle = conf.NotiPopupTitle;
                    n.NotiEmail = conf.NotiPopupEmail;
                    n.NotiContent = conf.NotiPopup;
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    return serializer.Serialize(n);
                }
                else
                    return "null";
            }
            else
                return null;
        }

        [WebMethod]
        public static void setNotshow()
        {
            HttpContext.Current.Session["notshowpopup"] = "1";
        }

        [WebMethod]
        public static string closewebinfo()
        {
            HttpContext.Current.Session["infoclose"] = "ok";
            return "ok";
        }
        public class NotiInfo
        {
            public string NotiTitle { get; set; }
            public string NotiContent { get; set; }
            public string NotiEmail { get; set; }
        }
    }
}