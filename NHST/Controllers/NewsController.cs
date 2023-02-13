using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;

namespace NHST.Controllers
{
    public class NewsController
    {
        public static tbl_News Insert(string NewsTitle, string NewsLink, bool IsHidden, string NewsDesc, string NewsMonth, string NewsIMG, int NewsDay, int NewsPosition, string CreatedBy)
        {
            using (var db = new NHSTEntities())
            {
                tbl_News ctb = new tbl_News();
                ctb.NewsTitle = NewsTitle;
                ctb.NewsDay = NewsDay;
                ctb.NewsMonth = NewsMonth;
                ctb.NewsLink = NewsLink;
                ctb.IsHidden = IsHidden;
                if (!string.IsNullOrEmpty(NewsIMG))
                    ctb.NewsIMG = NewsIMG;
                ctb.NewsDesc = NewsDesc;
                ctb.NewsPosition = NewsPosition;
                ctb.CreatedBy = CreatedBy;
                ctb.CreatedDate = DateTime.Now;
                db.tbl_News.Add(ctb);
                db.SaveChanges();
                return ctb;
            }
        }
        public static tbl_News Update(int ID, string NewsTitle, string NewsLink, bool IsHidden, string NewsDesc, string NewsMonth, string NewsIMG, int NewsDay, int NewsPosition, string Created)
        {
            using (var db = new NHSTEntities())
            {
                var sv = db.tbl_News.Where(x => x.ID == ID).FirstOrDefault();
                if (sv != null)
                {
                    sv.NewsTitle = NewsTitle;
                    sv.NewsDay = NewsDay;
                    sv.NewsMonth = NewsMonth;
                    sv.NewsLink = NewsLink;
                    sv.IsHidden = IsHidden;
                    if (!string.IsNullOrEmpty(NewsIMG))
                        sv.NewsIMG = NewsIMG;
                    sv.NewsDesc = NewsDesc;
                    sv.NewsPosition = NewsPosition;
                    sv.ModifiedBy = Created;
                    sv.ModifiedDate = DateTime.Now;
                    db.SaveChanges();
                    return sv;
                }
                return null;
            }
        }

        public static List<tbl_News> GetAll()
        {
            using (var db = new NHSTEntities())
            {
                List<tbl_News> ctb = new List<tbl_News>();
                ctb = db.tbl_News.OrderBy(x => x.NewsPosition).ToList();
                return ctb;
            }
        }

        public static tbl_News GetByID(int ID)
        {
            using (var db = new NHSTEntities())
            {
                var sv = db.tbl_News.Where(x => x.ID == ID).FirstOrDefault();
                if (sv != null)
                    return sv;
                return null;
            }
        }

        public static List<tbl_News> GetAllAD(string CustomerNewsName)
        {
            using (var db = new NHSTEntities())
            {
                var ctb = db.tbl_News.Where(x => x.NewsTitle.Contains(CustomerNewsName)).OrderBy(x => x.NewsPosition).ToList();
                if (ctb.Count > 0)
                    return ctb;
                return null;
            }
        }
    }
}