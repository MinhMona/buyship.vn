using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;
using NHST.Bussiness;
using System.Data;
using WebUI.Business;
using MB.Extensions;

namespace NHST.Controllers
{
    public class BGController
    {
        public static string Insert(string Name, string Phone, string DescOne, string DescTwo, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                tbl_BG p = new tbl_BG();
                p.Name = Name;
                p.Phone = Phone;
                p.DescOne = DescOne;
                p.DescTwo = DescTwo;
                p.Status = 1;
                p.CreatedDate = CreatedDate;
                p.CreatedBy = CreatedBy;
                dbe.tbl_BG.Add(p);
                dbe.Configuration.ValidateOnSaveEnabled = false;
                int kq = dbe.SaveChanges();
                //string k = kq + "|" + user.ID;
                string k = p.ID.ToString();
                return k;
            }
        }

        public static string Update(int ID, string Name, string Phone, string DescOne, string DescTwo, int status, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var p = dbe.tbl_BG.Where(pa => pa.ID == ID).FirstOrDefault();
                if (p != null)
                {
                    p.Name = Name;
                    p.Phone = Phone;
                    p.DescOne = DescOne;
                    p.DescTwo = DescTwo;
                    p.Status = status;
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

        public static string UpdateStatus(int ID, int status, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new NHSTEntities())
            {
                var p = dbe.tbl_BG.Where(pa => pa.ID == ID).FirstOrDefault();
                if (p != null)
                {
                    p.Status = status;
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
        public static List<tbl_BG> GetAll()
        {
            using (var db = new NHSTEntities())
            {
                List<tbl_BG> ctb = new List<tbl_BG>();
                ctb = db.tbl_BG.OrderBy(x => x.ID).ToList();
                return ctb;
            }
        }
        public static tbl_BG GetByID(int ID)
        {
            using (var db = new NHSTEntities())
            {
                var sv = db.tbl_BG.Where(x => x.ID == ID).FirstOrDefault();
                if (sv != null)
                    return sv;
                return null;
            }
        }
        //public static List<tbl_BG> GetAllAD()
        //{
        //    using (var db = new NHSTEntities())
        //    {
        //        var sv = db.tbl_BG.Where(x=> x.IsHidden == true).ToList();
        //        if (sv.Count > 0)
        //            return sv;
        //        return null;
        //    }
        //}
        //public static List<tbl_BG> GetAllAD(string CustomerFeedbackName)
        //{
        //    using (var db = new NHSTEntities())
        //    {
        //        var ctb = db.tbl_BG.Where(x => x.AuthorName.Contains(CustomerFeedbackName)).OrderBy(x => x.Position).ToList();
        //        if (ctb.Count > 0)
        //            return ctb;
        //        return null;
        //    }
        //}

        public static int GetTotal(string s, string phone, int Status)
        {
            var sql = @"select Total=Count(*) ";
            sql += "from tbl_BG ";
            sql += "Where Name like N'%" + s + "%' and Phone like N'%" + phone + "%'";
            if (Status > 0)
            {
                sql += " And Status=" + Status + " ";
            }
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            int a = 0;
            while (reader.Read())
            {
                if (reader["Total"] != DBNull.Value)
                    a = reader["Total"].ToString().ToInt(0);

            }
            reader.Close();
            return a;
        }


        public static List<tbl_BG> GetAllBySQL(string s, string phone, int pageIndex, int pageSize, int Status)
        {
            var sql = @"select * ";
            sql += "from tbl_BG ";
            sql += "Where Name Like N'%" + s + "%' and Phone Like N'%" + phone + "%'";
            if (Status > 0)
            {
                sql += " And Status=" + Status + " ";
            }
            sql += "order by id DESC OFFSET " + pageIndex + "*" + pageSize + " ROWS FETCH NEXT " + pageSize + " ROWS ONLY";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            List<tbl_BG> a = new List<tbl_BG>();
            while (reader.Read())
            {
                var entity = new tbl_BG();
                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);

                if (reader["Name"] != DBNull.Value)
                    entity.Name = reader["Name"].ToString();

                if (reader["Phone"] != DBNull.Value)
                    entity.Phone = reader["Phone"].ToString();

                if (reader["DescOne"] != DBNull.Value)
                    entity.DescOne = reader["DescOne"].ToString();

                if (reader["DescTwo"] != DBNull.Value)
                    entity.DescTwo = reader["DescTwo"].ToString();

                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());

                if (reader["Status"] != DBNull.Value)
                    entity.Status = Convert.ToInt32(reader["Status"].ToString());

                a.Add(entity);
            }
            reader.Close();
            return a;
        }

    }
}