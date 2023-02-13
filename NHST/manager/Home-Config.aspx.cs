using MB.Extensions;
using NHST.Bussiness;
using NHST.Controllers;
using NHST.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace NHST.manager
{
    public partial class Home_Config : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {                
                if (Session["userLoginSystem"] == null)
                {
                    Response.Redirect("/");
                }
                else
                {
                    string username_current = Session["userLoginSystem"].ToString();
                    tbl_Account ac = AccountController.GetByUsername(username_current);
                    if (ac.RoleID != 0)
                        Response.Redirect("/");
                }
                LoadData();
                LoadStep();
                LoadService();
                LoadCustomersBenefit();
                LoadView();
                //LoadFeedback();
                //LoadNews();
                //LoadNVSupport();
                //loaddataPresent();
            }
        }

        public string html = "";
        public string htmlList = "";
        public void LoadData()
        {
            var categories = MenuController.GetByLevel(0).OrderBy(c => c.Position).ToList();
            if (categories != null)
            {

                html += "<ul>";
                foreach (var c in categories)
                {
                    //html += "<li data-jstree='{ \"opened\" : false}'>" + c.CategoryName + "";
                    html += "<li id=\"" + c.ID + "\" data-jstree='{ \"opened\" : false}'>" + c.MenuName + " - <span class=\"register-link\" onclick=\"editMenu('" + c.ID + "')\">Edit</span> | <span class=\"register-link\" onclick=\"AddChildMenu('" + c.ID + "')\">Add Child</span> | <span class=\"register-link\" onclick=\"DeleteMenu('" + c.ID + "')\">Delete</span>";
                    Loadtree(c.ID);
                    html += "</li>";
                }
                html += "</ul>";
                ltrTree.Text = html;
            }
        }

        public void Loadtree(int UID)
        {
            var categories = MenuController.GetByLevel(UID);
            if (categories != null)
            {
                html += "<ul>";
                foreach (var c in categories)
                {
                    var ui = MenuController.GetByID(c.ID);
                    html += "<li id=\"" + c.ID + "\" data-jstree='{ \"opened\" : false}'>" + c.MenuName + " - <span class=\"register-link\" onclick=\"editMenu('" + c.ID + "')\">Edit</span>  | <span class=\"register-link\" onclick=\"DeleteMenu('" + c.ID + "')\">Delete</span>";
                    Loadtree(c.ID);
                    html += "</li>";
                }
                html += "</ul>";
            }
        }

        private void LoadView()
        {
            var listView = ViewProductController.GetAllAD();
            StringBuilder hcm = new StringBuilder();
            for (int i = 0; i < listView.Count; i++)
            {
                var item = listView[i];
                string site = "";
                if (item.Type == 1)
                {
                    site = "Mỹ";
                }
                else
                {
                    site = "Nhật";
                }
                hcm.Append("<tr>");
                hcm.Append("<td>" + item.ViewPosition + "</td>");
                hcm.Append("<td>" + item.ViewTitle + "</td>");
                hcm.Append("<td>" + PJUtils.ShowIMG40x40(item.ViewIMG) + "</td>");
                hcm.Append("<td>" + site + "</td>");
                hcm.Append("<td>" + PJUtils.BoolToStatusShow(item.IsHidden.ToString()) + "</td>");
                hcm.Append("<td>");
                hcm.Append("<div class=\"action-table\">");
                hcm.Append("<a href=\"#modalEditView\" id=\"EditView-" + item.ID + "\" onclick=\"EditView(" + item.ID + ")\" class=\" modal-trigger\" data-position=\"top\" ><i class=\"material-icons\">edit</i><span>Cập nhật</span></a>");
                hcm.Append("</div>");
                hcm.Append("</td>");
                hcm.Append("</tr>");
            }
            ltrListView.Text = hcm.ToString();
        }

        [WebMethod]
        public static string loadinfoNews(string ID)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var p = NewsController.GetByID(ID.ToInt(0));
            if (p != null)
            {

                tbl_News l = new tbl_News();
                l.ID = p.ID;
                l.NewsTitle = p.NewsTitle;
                l.NewsDay = p.NewsDay;
                l.NewsMonth = p.NewsMonth;
                l.NewsDesc = p.NewsDesc;
                l.IsHidden = p.IsHidden;
                l.NewsLink = p.NewsLink;
                l.CreatedDate = p.CreatedDate;
                l.NewsIMG = p.NewsIMG;
                l.ModifiedDate = p.ModifiedDate;
                l.NewsPosition = p.NewsPosition;
                return serializer.Serialize(l);
            }
            return serializer.Serialize(null);
        }

        private void LoadNews()
        {
            var listNews = NewsController.GetAllAD("");
            StringBuilder hcm = new StringBuilder();
            for (int i = 0; i < listNews.Count; i++)
            {
                var item = listNews[i];
                hcm.Append("<tr>");
                hcm.Append("<td>" + item.NewsPosition + "</td>");
                hcm.Append("<td>" + item.NewsTitle + "</td>");
                hcm.Append("<td>" + PJUtils.ShowIMG40x40(item.NewsIMG) + "</td>");
                hcm.Append("<td>" + PJUtils.BoolToStatusShow(item.IsHidden.ToString()) + "</td>");
                hcm.Append("<td>" + item.ModifiedDate + "</td>");
                hcm.Append("<td>");
                hcm.Append("<div class=\"action-table\">");
                hcm.Append("<a href=\"#modalEditNews\" id=\"EditNews-" + item.ID + "\" onclick=\"EditNews(" + item.ID + ")\" class=\" modal-trigger\" data-position=\"top\" ><i class=\"material-icons\">edit</i><span>Cập nhật</span></a>");
                hcm.Append("</div>");
                hcm.Append("</td>");
                hcm.Append("</tr>");
            }
            listltrNews.Text = hcm.ToString();
        }

        public void LoadStep()
        {
            var listStep = StepController.GetAll("");
            StringBuilder hcm = new StringBuilder();
            for (int i = 0; i < listStep.Count; i++)
            {

                var item = listStep[i];
                hcm.Append("<tr>");
                hcm.Append("<td>" + item.StepName + "</td>");
                hcm.Append("<td>" + item.StepLink + "</td>");
                hcm.Append("<td>" + item.StepIndex + "</td>");
                hcm.Append("<td>" + PJUtils.BoolToStatusShow(item.IsHidden.ToString()) + "</td>");
                hcm.Append("<td>" + item.CreatedDate + "</td>");
                hcm.Append("<td>");
                hcm.Append("<div class=\"action-table\">");
                hcm.Append("<a href=\"#modalEditStep\" id=\"EditStep-" + item.ID + "\" onclick=\"EditStep(" + item.ID + ")\" class=\" modal-trigger\" data-position=\"top\"><i class=\"material-icons\">edit</i><span>Cập nhật</span></a>");
                hcm.Append("</div>");
                hcm.Append("</td>");
                hcm.Append("</tr>");
            }
            ltrListStep.Text = hcm.ToString();
        }
        public void LoadNVSupport()
        {
            var list = NVsupportController.GetAll();
            StringBuilder hcm = new StringBuilder();
            for (int i = 0; i < list.Count; i++)
            {

                var item = list[i];
                hcm.Append("<tr>");
                hcm.Append("<td>" + item.Name + "</td>");
                hcm.Append("<td>" + item.Phone + "</td>");
                hcm.Append("<td>" + item.ModifiedBy + "</td>");
                hcm.Append("<td>" + item.ModifiedDate + "</td>");
                hcm.Append("<td>");
                hcm.Append("<div class=\"action-table\">");
                hcm.Append("<a href=\"#modalEditNVSP\" id=\"EditNVSP-" + item.ID + "\" onclick=\"EditNVSP(" + item.ID + ")\" class=\" modal-trigger\" data-position=\"top\"><i class=\"material-icons\">edit</i><span>Cập nhật</span></a>");
                hcm.Append("</div>");
                hcm.Append("</td>");
                hcm.Append("</tr>");
            }
            ltrNVSP.Text = hcm.ToString();
        }

        [WebMethod]
        public static string loadinfoServices(string ID)
        {

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var p = ServiceController.GetByID(ID.ToInt(0));
            if (p != null)
            {

                tbl_Service l = new tbl_Service();
                l.ID = p.ID;
                l.ServiceName = p.ServiceName;
                l.ServiceContent = p.ServiceContent;
                l.ServiceLink = p.ServiceLink;
                l.ServiceIMG = p.ServiceIMG;
                l.CreatedDate = p.CreatedDate;
                l.IsHidden = p.IsHidden;
                l.Position = p.Position;
                return serializer.Serialize(l);
            }
            return serializer.Serialize(null);
        }

        [WebMethod]
        public static string loadinfoNVSP(string ID)
        {

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var p = NVsupportController.GetByID(ID.ToInt(0));
            if (p != null)
            {

                tbl_NVsupport l = new tbl_NVsupport();
                l.ID = p.ID;
                l.Name = p.Name;
                l.Phone = p.Phone;
                return serializer.Serialize(l);
            }
            return serializer.Serialize(null);
        }

        [WebMethod]
        public static string loadinfoFeedback(string ID)
        {

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var p = FeedbackController.GetByID(ID.ToInt(0));
            if (p != null)
            {

                tbl_Feedback l = new tbl_Feedback();
                l.ID = p.ID;
                l.AuthorName = p.AuthorName;
                l.ShopName = p.ShopName;
                l.FeedbackContent = p.FeedbackContent;
                l.Image = p.Image;
                return serializer.Serialize(l);
            }
            return serializer.Serialize(null);
        }

        [WebMethod]
        public static string loadinfoView(string ID)
        {

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var p = ViewProductController.GetByID(ID.ToInt(0));
            if (p != null)
            {

                tbl_ViewProduct l = new tbl_ViewProduct();
                l.ID = p.ID;
                l.ViewTitle = p.ViewTitle;
                l.ViewIMG = p.ViewIMG;
                l.ViewLink = p.ViewLink;
                l.CreatedDate = p.CreatedDate;
                l.IsHidden = p.IsHidden;
                l.ViewPosition = p.ViewPosition;
                return serializer.Serialize(l);
            }
            return serializer.Serialize(null);
        }

        [WebMethod]
        public static string loadinfoStep(string ID)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var p = StepController.GetByID(ID.ToInt(0));
            if (p != null)
            {

                tbl_Step l = new tbl_Step();
                l.ID = p.ID;
                l.StepName = p.StepName;
                l.StepLink = p.StepLink;
                l.StepIndex = p.StepIndex;
                l.StepIMG = p.StepIMG;
                l.StepContent = p.StepContent;
                l.ClassIcon = p.ClassIcon;
                l.CreatedDate = p.CreatedDate;
                l.IsHidden = p.IsHidden;
                return serializer.Serialize(l);
            }
            return serializer.Serialize(null);
        }

        [WebMethod]
        public static string loadinfoBenefits(string ID)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var p = CustomerBenefitsController.GetByID(ID.ToInt(0));
            if (p != null)
            {

                tbl_CustomerBenefits l = new tbl_CustomerBenefits();
                l.ID = p.ID;
                l.CustomerBenefitName = p.CustomerBenefitName;
                l.CustomerBenefitLink = p.CustomerBenefitLink;
                l.CustomerBenefitDescription = p.CustomerBenefitDescription;
                l.Icon = p.Icon;
                l.ItemType = p.ItemType;
                l.CreatedDate = p.CreatedDate;
                l.IsHidden = p.IsHidden;
                l.Position = p.Position;
                return serializer.Serialize(l);
            }
            return serializer.Serialize(null);
        }
        private void LoadService()
        {
            var listService = ServiceController.GetAllAD();

            StringBuilder hcm = new StringBuilder();
            for (int i = 0; i < listService.Count; i++)
            {
                var item = listService[i];
                hcm.Append("<tr>");
                hcm.Append("<td>" + item.Position + "</td>");
                hcm.Append("<td>" + item.ServiceName + "</td>");
                hcm.Append("<td>" + PJUtils.ShowIMG40x40(item.ServiceIMG) + "</td>");
                hcm.Append("<td>" + PJUtils.BoolToStatusShow(item.IsHidden.ToString()) + "</td>");
                hcm.Append("<td>" + item.CreatedDate + "</td>");
                hcm.Append("<td>");
                hcm.Append("<div class=\"action-table\">");
                hcm.Append("<a href=\"#modalEditServices\" id=\"EditServices-" + item.ID + "\" onclick=\"EditServices(" + item.ID + ")\" class=\" modal-trigger\" data-position=\"top\" ><i class=\"material-icons\">edit</i><span>Cập nhật</span></a>");
                hcm.Append("</div>");
                hcm.Append("</td>");
                hcm.Append("</tr>");
            }
            ltrListService.Text = hcm.ToString();
        }
        private void LoadCustomersBenefit()
        {
            var customerBenefits = CustomerBenefitsController.GetAllAD("");
            StringBuilder hcm = new StringBuilder();
            for (int i = 0; i < customerBenefits.Count; i++)
            {

                var item = customerBenefits[i];
                string tempType = "";
                if (item.ItemType == 1)
                {
                    tempType = "Cam kết của chúng tôi";
                }
                else
                {
                    tempType = "Quyền lợi của khách hàng";
                }
                hcm.Append("<tr>");
                hcm.Append("<td>" + item.CustomerBenefitName + "</td>");
                hcm.Append("<td>" + PJUtils.BoolToStatusShow(item.IsHidden.ToString()) + "</td>");
                hcm.Append("<td>" + item.Position + "</td>");
                hcm.Append("<td>" + tempType + "</td>");
                hcm.Append("<td>" + item.CreatedDate + "</td>");
                hcm.Append("<td>");
                hcm.Append("<div class=\"action-table\">");
                hcm.Append("<a href=\"#modalEditBenefits\" id=\"EditBenefits-" + item.ID + "\" onclick=\"EditBenefits(" + item.ID + ")\" class=\" modal-trigger\" data-position=\"top\"><i class=\"material-icons\">edit</i><span>Cập nhật</span></a>");
                hcm.Append("</div>");
                hcm.Append("</td>");
                hcm.Append("</tr>");
            }
            ltrListCustomersBenefit.Text = hcm.ToString();
        }
        private void LoadFeedback()
        {
            var list = FeedbackController.GetAll();

            StringBuilder hcm = new StringBuilder();
            for (int i = 0; i < list.Count; i++)
            {
                var item = list[i];
                hcm.Append("<tr>");
                hcm.Append("<td>" + item.AuthorName + "</td>");
                hcm.Append("<td>" + item.ShopName + "</td>");
                hcm.Append("<td>" + PJUtils.ShowIMG40x40(item.Image) + "</td>");
                hcm.Append("<td>" + item.ModifiedBy + "</td>");
                hcm.Append("<td>" + item.ModifiedDate + "</td>");
                hcm.Append("<td>");
                hcm.Append("<div class=\"action-table\">");
                hcm.Append("<a href=\"#modalEditFeedback\" id=\"EditFeedback-" + item.ID + "\" onclick=\"EditFeedback(" + item.ID + ")\" class=\" modal-trigger\" data-position=\"top\" ><i class=\"material-icons\">edit</i><span>Cập nhật</span></a>");
                hcm.Append("</div>");
                hcm.Append("</td>");
                hcm.Append("</tr>");
            }
            ltrKH.Text = hcm.ToString();
        }
        protected void BtnUpStep_Click(object sender, EventArgs e)
        {
            string Username = Session["userLoginSystem"].ToString();
            int StepID = hdfStepID.Value.ToInt(0);
            string IMG = "";
            string KhieuNaiIMG = "/Uploads/Images/";
            string BackLink = "/manager/Home-Config.aspx";
            bool IsHidden = Convert.ToBoolean(hdfStepStatus.Value.ToInt(0));
            if (EditStepIMG.HasFiles)
            {
                foreach (HttpPostedFile f in EditStepIMG.PostedFiles)
                {
                    string fileContentType = f.ContentType; // getting ContentType

                    byte[] tempFileBytes = new byte[f.ContentLength];

                    var data = f.InputStream.Read(tempFileBytes, 0, Convert.ToInt32(f.ContentLength));

                    string fileName = f.FileName; // getting File Name
                    string fileExtension = Path.GetExtension(fileName).ToLower();

                    var result = FileUploadCheck.isValidFile(tempFileBytes, fileExtension, fileContentType); // Validate Header
                    if (result)
                    {
                        if (f.FileName.ToLower().Contains(".jpg") || f.FileName.ToLower().Contains(".png") || f.FileName.ToLower().Contains(".jpeg"))
                        {
                            if (f.ContentType == "image/png" || f.ContentType == "image/jpeg" || f.ContentType == "image/jpg")
                            {
                                //var o = KhieuNaiIMG + Guid.NewGuid() + Path.GetExtension(f.FileName);
                                try
                                {
                                    //f.SaveAs(Server.MapPath(o));
                                    IMG = FileUploadCheck.ConvertToBase64(tempFileBytes);
                                }
                                catch
                                {

                                }
                            }
                        }
                    }
                }
            }
            else
                IMG = EditIMGBefore.ImageUrl;

            string kq = StepController.Update(StepID, EditStepName.Text, IMG, Convert.ToInt32(EditStepIndex.Text), EditStepLink.Text, DateTime.Now, Username, EditStepSumary.Text, EditStepClassIcon.Text, IsHidden);
            if (Convert.ToInt32(kq) > 0)
            {
                PJUtils.ShowMessageBoxSwAlertBackToLink("Cập nhật thành công.", "s", true, BackLink, Page);
            }
            else
            {
                PJUtils.ShowMessageBoxSwAlert("Có lỗi trong quá trình Cập nhật. Vui lòng thử lại.", "e", true, Page);
            }
        }

        protected void BtnUpEditNVSP_Click(object sender, EventArgs e)
        {
            string Username = Session["userLoginSystem"].ToString();
            int StepID = hdfEditNVSPID.Value.ToInt(0);
            string BackLink = "/manager/Home-Config.aspx";

            string kq = NVsupportController.Update(StepID, EditNVSPName.Text, EditNVSPPhone.Text, DateTime.Now, Username);
            if (Convert.ToInt32(kq) > 0)
            {
                PJUtils.ShowMessageBoxSwAlertBackToLink("Cập nhật thành công.", "s", true, BackLink, Page);
            }
            else
            {
                PJUtils.ShowMessageBoxSwAlert("Có lỗi trong quá trình Cập nhật. Vui lòng thử lại.", "e", true, Page);
            }
        }

        protected void btnUpServices_Click(object sender, EventArgs e)
        {
            string Email = Session["userLoginSystem"].ToString();
            DateTime currentDate = DateTime.Now;
            int CBID = hdfEditServicesID.Value.ToInt(0);
            string BackLink = "/manager/Home-Config.aspx";

            string CustomerBenefitName = EditServicesTitle.Text;
            string CustomerBenefitDescription = EditServicesSummary.Text;
            string CustomerBenefitLink = EditServicesLink.Text;
            int Position = Convert.ToInt32(EditServicesPosition.Text.ToInt(0));

            bool IsHidden = Convert.ToBoolean(hdfEditServicesStatus.Value.ToInt(0));
            string IMG = "";
            string KhieuNaiIMG = "/Uploads/Images/";

            if (EditServicesIMG.HasFiles)
            {
                foreach (HttpPostedFile f in EditServicesIMG.PostedFiles)
                {
                    string fileContentType = f.ContentType; // getting ContentType

                    byte[] tempFileBytes = new byte[f.ContentLength];

                    var data = f.InputStream.Read(tempFileBytes, 0, Convert.ToInt32(f.ContentLength));

                    string fileName = f.FileName; // getting File Name
                    string fileExtension = Path.GetExtension(fileName).ToLower();

                    var result = FileUploadCheck.isValidFile(tempFileBytes, fileExtension, fileContentType); // Validate Header
                    if (result)
                    {
                        if (f.FileName.ToLower().Contains(".jpg") || f.FileName.ToLower().Contains(".png") || f.FileName.ToLower().Contains(".jpeg"))
                        {
                            if (f.ContentType == "image/png" || f.ContentType == "image/jpeg" || f.ContentType == "image/jpg")
                            {
                                //var o = KhieuNaiIMG + Guid.NewGuid() + Path.GetExtension(f.FileName);
                                try
                                {
                                    //f.SaveAs(Server.MapPath(o));
                                    IMG = FileUploadCheck.ConvertToBase64(tempFileBytes);
                                }
                                catch { }
                            }
                        }
                    }
                }
            }
            else
                IMG = EditBenefitIMGBefore.ImageUrl;

            var kq = ServiceController.Update(CBID, CustomerBenefitName, CustomerBenefitDescription, CustomerBenefitLink, IMG, IsHidden, Position, Email);

            if (kq != null)
            {
                PJUtils.ShowMessageBoxSwAlertBackToLink("Cập nhật trang thành công.", "s", true, BackLink, Page);
            }
            else
            {
                PJUtils.ShowMessageBoxSwAlert("Có lỗi trong quá trình cập nhật trang. Vui lòng thử lại.", "e", true, Page);
            }
        }

        protected void btnUpBenefit_Click(object sender, EventArgs e)
        {
            string Email = Session["userLoginSystem"].ToString();
            DateTime currentDate = DateTime.Now;
            int CBID = hdfEditBenefitID.Value.ToInt(0);
            string BackLink = "/manager/Home-Config.aspx";

            string CustomerBenefitName = EditBenefitName.Text;
            string CustomerBenefitDescription = EditBenefitDescription.Text;
            string CustomerBenefitLink = EditBenefitLink.Text;
            int Position = Convert.ToInt32(EditBenefitPosition.Text);

            bool IsHidden = Convert.ToBoolean(hdfEditBenefitStatus.Value.ToInt(0));
            string IMG = "";
            string KhieuNaiIMG = "/Uploads/Images/";

            if (EditBenefitIMG.HasFiles)
            {
                foreach (HttpPostedFile f in EditBenefitIMG.PostedFiles)
                {
                    string fileContentType = f.ContentType; // getting ContentType

                    byte[] tempFileBytes = new byte[f.ContentLength];

                    var data = f.InputStream.Read(tempFileBytes, 0, Convert.ToInt32(f.ContentLength));

                    string fileName = f.FileName; // getting File Name
                    string fileExtension = Path.GetExtension(fileName).ToLower();

                    var result = FileUploadCheck.isValidFile(tempFileBytes, fileExtension, fileContentType); // Validate Header
                    if (result)
                    {
                        if (f.FileName.ToLower().Contains(".jpg") || f.FileName.ToLower().Contains(".png") || f.FileName.ToLower().Contains(".jpeg"))
                        {
                            if (f.ContentType == "image/png" || f.ContentType == "image/jpeg" || f.ContentType == "image/jpg")
                            {
                                //var o = KhieuNaiIMG + Guid.NewGuid() + Path.GetExtension(f.FileName);
                                try
                                {
                                    //f.SaveAs(Server.MapPath(o));
                                    IMG = FileUploadCheck.ConvertToBase64(tempFileBytes);
                                }
                                catch { }
                            }
                        }
                    }
                }
            }
            else
                IMG = EditBenefitIMGBefore.ImageUrl;


            var kq = CustomerBenefitsController.Update(CBID, IMG, CustomerBenefitName,
                CustomerBenefitDescription, CustomerBenefitLink, IsHidden, Position, Email, ddlEditBenefitType.SelectedValue.ToInt());

            if (kq != null)
            {
                PJUtils.ShowMessageBoxSwAlertBackToLink("Cập nhật trang thành công.", "s", true, BackLink, Page);
            }
            else
            {
                PJUtils.ShowMessageBoxSwAlert("Có lỗi trong quá trình cập nhật trang. Vui lòng thử lại.", "e", true, Page);
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string t = hdfTest.Value;
            string[] list = t.Split('*');

            for (int i = 0; i < list.Length - 1; i++)
            {
                string[] value = list[i].Split('-');
                var root = MenuController.UpdateIndex(value[0].ToInt(), i, 0);
                if (root != null)
                {
                    if (!string.IsNullOrEmpty(value[1]))
                    {
                        string[] vl1 = value[1].Split('|');
                        for (int j = 0; j < vl1.Length - 1; j++)
                        {
                            var child = MenuController.UpdateIndex(vl1[j].ToInt(), j, root.ID);
                        }
                    }
                }

            }

            string Backlink = "/manager/Home-Config.aspx";
            PJUtils.ShowMessageBoxSwAlertBackToLink("Cập nhật thành công.", "s", true, Backlink, Page);
        }

        protected void btnDeleteMenu_Click(object sender, EventArgs e)
        {
            string Email = Session["userLoginSystem"].ToString();
            DateTime currentDate = DateTime.Now;
            int CBID = hdfMenuID.Value.ToInt(0);

            var menu = MenuController.GetByID(CBID);
            if(menu != null)
            {
                MenuController.Delete(menu.ID);
                PJUtils.ShowMessageBoxSwAlert("Xóa thành công.", "s", true, Page);
            }
        }

        protected void btnUpNews_Click(object sender, EventArgs e)
        {
            string Email = Session["userLoginSystem"].ToString();
            DateTime currentDate = DateTime.Now;
            int CBID = hdfEditNewsID.Value.ToInt(0);
            string BackLink = "/manager/Home-Config.aspx";

            string CustomerNewsName = EditNewsTitle.Text;
            string CustomerNewsDescription = EditNewsSummary.Text;
            int Position = Convert.ToInt32(EditNewsPosition.Text.ToInt(0));
            bool IsHidden = Convert.ToBoolean(hdfEditNewsStatus.Value.ToInt(0));
            string IMG = "";
            string KhieuNaiIMG = "/Uploads/Images/";
            if (EditNewsIMG.HasFiles)
            {
                foreach (HttpPostedFile f in EditNewsIMG.PostedFiles)
                {
                    string fileContentType = f.ContentType; // getting ContentType

                    byte[] tempFileBytes = new byte[f.ContentLength];

                    var data = f.InputStream.Read(tempFileBytes, 0, Convert.ToInt32(f.ContentLength));

                    string fileName = f.FileName; // getting File Name
                    string fileExtension = Path.GetExtension(fileName).ToLower();

                    var result = FileUploadCheck.isValidFile(tempFileBytes, fileExtension, fileContentType); // Validate Header
                    if (result)
                    {
                        if (f.FileName.ToLower().Contains(".jpg") || f.FileName.ToLower().Contains(".png") || f.FileName.ToLower().Contains(".jpeg"))
                        {
                            if (f.ContentType == "image/png" || f.ContentType == "image/jpeg" || f.ContentType == "image/jpg")
                            {
                                //var o = KhieuNaiIMG + Guid.NewGuid() + Path.GetExtension(f.FileName);
                                try
                                {
                                    //f.SaveAs(Server.MapPath(o));
                                    IMG = FileUploadCheck.ConvertToBase64(tempFileBytes);
                                }
                                catch { }
                            }
                        }
                    }
                }
            }
            else
                IMG = EditNewsIMGBefore.ImageUrl;

            var kq = NewsController.Update(CBID, CustomerNewsName, EditNewsLink.Text, IsHidden, CustomerNewsDescription, "", IMG, 1, Position, Email);

            if (kq != null)
            {
                PJUtils.ShowMessageBoxSwAlertBackToLink("Cập nhật trang thành công.", "s", true, BackLink, Page);
            }
            else
            {
                PJUtils.ShowMessageBoxSwAlert("Có lỗi trong quá trình cập nhật trang. Vui lòng thử lại.", "e", true, Page);
            }
        }
        protected void btnUpFeedbacks_Click(object sender, EventArgs e)
        {
            string Email = Session["userLoginSystem"].ToString();
            DateTime currentDate = DateTime.Now;
            int CBID = hdfEditFeedbackID.Value.ToInt(0);
            string BackLink = "/manager/Home-Config.aspx";

            string CustomerBenefitName = EditName.Text;
            string CustomerBenefitNameShop = EditNameShop.Text;
            string CustomerBenefitDescription = EditContent.Text;
            string IMG = "";
            string KhieuNaiIMG = "/Uploads/Images/";

            if (EditIMG.HasFiles)
            {
                foreach (HttpPostedFile f in EditIMG.PostedFiles)
                {
                    string fileContentType = f.ContentType; // getting ContentType

                    byte[] tempFileBytes = new byte[f.ContentLength];

                    var data = f.InputStream.Read(tempFileBytes, 0, Convert.ToInt32(f.ContentLength));

                    string fileName = f.FileName; // getting File Name
                    string fileExtension = Path.GetExtension(fileName).ToLower();

                    var result = FileUploadCheck.isValidFile(tempFileBytes, fileExtension, fileContentType); // Validate Header
                    if (result)
                    {
                        if (f.FileName.ToLower().Contains(".jpg") || f.FileName.ToLower().Contains(".png") || f.FileName.ToLower().Contains(".jpeg"))
                        {
                            if (f.ContentType == "image/png" || f.ContentType == "image/jpeg" || f.ContentType == "image/jpg")
                            {
                                //var o = KhieuNaiIMG + Guid.NewGuid() + Path.GetExtension(f.FileName);
                                try
                                {
                                    //f.SaveAs(Server.MapPath(o));
                                    IMG = FileUploadCheck.ConvertToBase64(tempFileBytes);
                                }
                                catch { }
                            }
                        }
                    }
                }
            }
            else
                IMG = IMGFeedback.ImageUrl;

            var kq = FeedbackController.Update(CBID, CustomerBenefitDescription, IMG, false, CustomerBenefitName, CBID, currentDate, Email, CustomerBenefitNameShop);

            if (kq != null)
            {
                PJUtils.ShowMessageBoxSwAlertBackToLink("Cập nhật trang thành công.", "s", true, BackLink, Page);
            }
            else
            {
                PJUtils.ShowMessageBoxSwAlert("Có lỗi trong quá trình cập nhật trang. Vui lòng thử lại.", "e", true, Page);
            }
        }

        protected void btnSaveNews_Click(object sender, EventArgs e)
        {
            string Email = Session["userLoginSystem"].ToString();
            DateTime currentDate = DateTime.Now;
            string BackLink = "/manager/Home-Config.aspx";

            string NewsName = AddNewsName.Text;
            string NewsDescription = AddNewsDesc.Text;
            string NewsLink = AddNewsLink.Text;
            int Position = Convert.ToInt32(AddNewsPosition.Text);

            bool IsHidden = Convert.ToBoolean(hdfAddNewsStatus.Value.ToInt(0));
            string IMG = "";
            string KhieuNaiIMG = "/Uploads/Images/";

            if (AddNewsIMG.HasFiles)
            {
                foreach (HttpPostedFile f in AddNewsIMG.PostedFiles)
                {
                    string fileContentType = f.ContentType; // getting ContentType

                    byte[] tempFileBytes = new byte[f.ContentLength];

                    var data = f.InputStream.Read(tempFileBytes, 0, Convert.ToInt32(f.ContentLength));

                    string fileName = f.FileName; // getting File Name
                    string fileExtension = Path.GetExtension(fileName).ToLower();

                    var result = FileUploadCheck.isValidFile(tempFileBytes, fileExtension, fileContentType); // Validate Header
                    if (result)
                    {
                        if (f.FileName.ToLower().Contains(".jpg") || f.FileName.ToLower().Contains(".png") || f.FileName.ToLower().Contains(".jpeg"))
                        {
                            if (f.ContentType == "image/png" || f.ContentType == "image/jpeg" || f.ContentType == "image/jpg")
                            {
                                //var o = KhieuNaiIMG + Guid.NewGuid() + Path.GetExtension(f.FileName);
                                try
                                {
                                    //f.SaveAs(Server.MapPath(o));
                                    IMG = FileUploadCheck.ConvertToBase64(tempFileBytes);
                                }
                                catch { }
                            }
                        }
                    }
                }
            }
            else
                IMG = AddNewsIMGBefore.ImageUrl;


            var kq = NewsController.Insert(NewsName, NewsLink, IsHidden, NewsDescription, "", IMG, 1, Position, Email);

            if (kq != null)
            {
                PJUtils.ShowMessageBoxSwAlertBackToLink("Thêm sản phẩm thành công.", "s", true, BackLink, Page);
            }
            else
            {
                PJUtils.ShowMessageBoxSwAlert("Có lỗi trong quá trình thêm sản phẩm. Vui lòng thử lại.", "e", true, Page);
            }
        }

        protected void btnUpView_Click(object sender, EventArgs e)
        {
            string Email = Session["userLoginSystem"].ToString();
            DateTime currentDate = DateTime.Now;
            int CBID = hdfEditViewID.Value.ToInt(0);
            string BackLink = "/manager/Home-Config.aspx";

            string CustomerBenefitName = EditViewTitle.Text;
            string CustomerBenefitLink = EditViewLink.Text;
            int Position = Convert.ToInt32(EditViewPosition.Text.ToInt(0));
            bool IsHidden = Convert.ToBoolean(hdfEditViewStatus.Value.ToInt(0));
            string IMG = "";            
            if (EditViewIMG.HasFiles)
            {
                foreach (HttpPostedFile f in EditViewIMG.PostedFiles)
                {
                    string fileContentType = f.ContentType; // getting ContentType

                    byte[] tempFileBytes = new byte[f.ContentLength];

                    var data = f.InputStream.Read(tempFileBytes, 0, Convert.ToInt32(f.ContentLength));

                    string fileName = f.FileName; // getting File Name
                    string fileExtension = Path.GetExtension(fileName).ToLower();

                    var result = FileUploadCheck.isValidFile(tempFileBytes, fileExtension, fileContentType); // Validate Header
                    if (result)
                    {
                        if (f.FileName.ToLower().Contains(".jpg") || f.FileName.ToLower().Contains(".png") || f.FileName.ToLower().Contains(".jpeg"))
                        {
                            if (f.ContentType == "image/png" || f.ContentType == "image/jpeg" || f.ContentType == "image/jpg")
                            {
                                //var o = KhieuNaiIMG + Guid.NewGuid() + Path.GetExtension(f.FileName);
                                try
                                {
                                    //f.SaveAs(Server.MapPath(o));
                                    IMG = FileUploadCheck.ConvertToBase64(tempFileBytes);
                                }
                                catch { }
                            }
                        }
                    }
                }
            }
            else
                IMG = EditViewIMGBefore.ImageUrl;

            var kq = ViewProductController.Update(CBID, CustomerBenefitName, CustomerBenefitLink, IMG, IsHidden, Position, Email);

            if (kq != null)
            {
                PJUtils.ShowMessageBoxSwAlertBackToLink("Cập nhật trang thành công.", "s", true, BackLink, Page);
            }
            else
            {
                PJUtils.ShowMessageBoxSwAlert("Có lỗi trong quá trình cập nhật trang. Vui lòng thử lại.", "e", true, Page);
            }
        }
    }
}