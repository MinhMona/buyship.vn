using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHST.Models;

namespace NHST.Bussiness
{
    public class TableSql
    {
        protected readonly NHSTEntities _context;

        public TableSql(NHSTEntities context)
        {
            _context = context;
        }

        //public List<LoadDataBaoCaoNoiBoCongTy_Result> LoadDataBaoCaoNoiBoCongTy(string fd, string td)
        //{
        //    var model = _context.LoadDataBaoCaoNoiBoCongTy(fd,td).ToList();
        //    return model;
        //}

        //public List<int?> UpdateStatusMainOrder(int day, int user)
        //{
        //    var model = _context.UpdateStatusMainOrder(day, user).ToList();
        //    return model;
        //}
        //public List<GetRePortIncome_Result> getRePortInCome(string startdate, string enddate, string username, int? status,int? type,int pagesize, int pageindex)
        //{
        //    var model = _context.GetRePortIncome(startdate, enddate, username, status, type, pagesize, pageindex).ToList();
        //    return model;
        //}
        //public GetRePortIncomeTotal_Result getRePortInComeTotal(string startdate, string enddate, string username, int? status,int? type)
        //{
        //    var model = _context.GetRePortIncomeTotal(startdate, enddate, username, status,type).FirstOrDefault();
        //    return model;
        //}

        public List<int?> getListOrderImagebase64(string search)
        {
            var model = _context.getListOrderImagebase64(search).ToList();
               return model;
        }
    }
}