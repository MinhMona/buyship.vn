using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;

namespace NHST.Controllers
{
    public class FeedbackController
    {
        public static string Insert(string FeedbackContent, string Image, string AuthorName, bool IsHidden, int Position, DateTime CreatedDate, string CreatedBy, string ShopName)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_Feedback p = new tbl_Feedback();
                p.FeedbackContent = FeedbackContent;
                p.Image = Image;
                p.AuthorName = AuthorName;
                p.Position = Position;
                p.ShopName = ShopName;
                p.IsHidden = IsHidden;
                p.CreatedDate = CreatedDate;
                p.CreatedBy = CreatedBy;
                dbe.tbl_Feedback.Add(p);
                dbe.Configuration.ValidateOnSaveEnabled = false;
                int kq = dbe.SaveChanges();
                //string k = kq + "|" + user.ID;
                string k = p.ID.ToString();
                return k;
            }
        }

        public static string Update(int ID, string FeedbackContent, string Image, bool IsHidden, string AuthorName, int Position, DateTime ModifiedDate, string ModifiedBy, string ShopName)
        {
            using (var dbe = new NHSTEntities())
            {
                var p = dbe.tbl_Feedback.Where(pa => pa.ID == ID).FirstOrDefault();
                if (p != null)
                {
                    p.IsHidden = IsHidden;
                    p.FeedbackContent = FeedbackContent;
                    if (!string.IsNullOrEmpty(Image))
                        p.Image = Image;
                    p.AuthorName = AuthorName;
                    p.Position = Position;
                    p.ShopName = ShopName;
                    p.ModifiedBy = ModifiedBy;
                    p.ModifiedDate = ModifiedDate;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }

        public static List<tbl_Feedback> GetAll()
        {
            using (var db = new NHSTEntities())
            {
                List<tbl_Feedback> ctb = new List<tbl_Feedback>();
                ctb = db.tbl_Feedback.OrderBy(x => x.Position).ToList();
                return ctb;
            }
        }
        public static tbl_Feedback GetByID(int ID)
        {
            using (var db = new NHSTEntities())
            {
                var sv = db.tbl_Feedback.Where(x => x.ID == ID).FirstOrDefault();
                if (sv != null)
                    return sv;
                return null;
            }
        }
        //public static List<tbl_Feedback> GetAllAD()
        //{
        //    using (var db = new NHSTEntities())
        //    {
        //        var sv = db.tbl_Feedback.Where(x=> x.IsHidden == true).ToList();
        //        if (sv.Count > 0)
        //            return sv;
        //        return null;
        //    }
        //}
        public static List<tbl_Feedback> GetAllAD(string CustomerFeedbackName)
        {
            using (var db = new NHSTEntities())
            {
                var ctb = db.tbl_Feedback.Where(x => x.AuthorName.Contains(CustomerFeedbackName)).OrderBy(x => x.Position).ToList();
                if (ctb.Count > 0)
                    return ctb;
                return null;
            }
        }
    }
}