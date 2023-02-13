using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;

namespace NHST.Controllers
{
    public class ViewProductController
    {
        #region 
        public static tbl_ViewProduct Insert(string ViewTitle, string ViewLink, string ViewIMG, bool IsHidden, int ViewPosition, string Created)
        {
            using (var db = new NHSTEntities())
            {
                tbl_ViewProduct sv = new tbl_ViewProduct();
                sv.ViewTitle = ViewTitle;
                sv.ViewLink = ViewLink;
                sv.ViewIMG = ViewIMG;
                sv.IsHidden = IsHidden;
                sv.ViewPosition = ViewPosition;
                sv.CreatedBy = Created;
                sv.CreatedDate = DateTime.Now;
                db.tbl_ViewProduct.Add(sv);
                db.SaveChanges();
                return sv;
            }
        }

        public static tbl_ViewProduct Update(int ID, string ViewTitle, string ViewLink, string ViewIMG, bool IsHidden, int ViewPosition, string Created)
        {
            using (var db = new NHSTEntities())
            {
                var sv = db.tbl_ViewProduct.Where(x => x.ID == ID).FirstOrDefault();
                if (sv != null)
                {
                    sv.ViewTitle = ViewTitle;
                    sv.ViewLink = ViewLink;
                    if (!string.IsNullOrEmpty(ViewIMG))
                        sv.ViewIMG = ViewIMG;
                    sv.IsHidden = IsHidden;
                    sv.ViewPosition = ViewPosition;
                    sv.ModifiedBy = Created;
                    sv.ModifiedDate = DateTime.Now;
                    db.SaveChanges();
                    return sv;
                }
                return null;
            }
        }
        #endregion


        #region Select
        public static List<tbl_ViewProduct> GetAllAD()
        {
            using (var db = new NHSTEntities())
            {
                var sv = db.tbl_ViewProduct.ToList();
                if (sv.Count > 0)
                    return sv;
                return null;
            }
        }

        public static List<tbl_ViewProduct> GetAll()
        {
            using (var db = new NHSTEntities())
            {
                var sv = db.tbl_ViewProduct.Where(x => x.IsHidden != true).ToList();
                if (sv.Count > 0)
                    return sv;
                return null;
            }
        }

        public static tbl_ViewProduct GetByID(int ID)
        {
            using (var db = new NHSTEntities())
            {
                var sv = db.tbl_ViewProduct.Where(x => x.ID == ID).FirstOrDefault();
                if (sv != null)
                    return sv;
                return null;
            }
        }
        #endregion
    }
}