using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;

namespace NHST.Controllers
{
    public class NVsupportController
    {
        public static string Insert(string Name, string Phone, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_NVsupport p = new tbl_NVsupport();
                p.Name = Name;
                p.Phone = Phone;
                p.CreatedDate = CreatedDate;
                p.CreatedBy = CreatedBy;
                dbe.tbl_NVsupport.Add(p);
                dbe.Configuration.ValidateOnSaveEnabled = false;
                int kq = dbe.SaveChanges();
                //string k = kq + "|" + user.ID;
                string k = p.ID.ToString();
                return k;
            }
        }

        public static string Update(int ID, string Name, string Phone, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var p = dbe.tbl_NVsupport.Where(pa => pa.ID == ID).FirstOrDefault();
                if (p != null)
                {
                    p.Name = Name;
                    p.Phone = Phone;
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

        public static List<tbl_NVsupport> GetAll()
        {
            using (var db = new NHSTEntities())
            {
                List<tbl_NVsupport> ctb = new List<tbl_NVsupport>();
                ctb = db.tbl_NVsupport.OrderBy(x => x.ID).ToList();
                return ctb;
            }
        }
        public static tbl_NVsupport GetByID(int ID)
        {
            using (var db = new NHSTEntities())
            {
                var sv = db.tbl_NVsupport.Where(x => x.ID == ID).FirstOrDefault();
                if (sv != null)
                    return sv;
                return null;
            }
        }
        //public static List<tbl_NVsupport> GetAllAD()
        //{
        //    using (var db = new NHSTEntities())
        //    {
        //        var sv = db.tbl_NVsupport.Where(x=> x.IsHidden == true).ToList();
        //        if (sv.Count > 0)
        //            return sv;
        //        return null;
        //    }
        //}
        //public static List<tbl_NVsupport> GetAllAD(string CustomerFeedbackName)
        //{
        //    using (var db = new NHSTEntities())
        //    {
        //        var ctb = db.tbl_NVsupport.Where(x => x.AuthorName.Contains(CustomerFeedbackName)).OrderBy(x => x.Position).ToList();
        //        if (ctb.Count > 0)
        //            return ctb;
        //        return null;
        //    }
        //}
    }
}