using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using AlarmCenter.DAL;
using System.Data.OleDb;
using AlarmCenter.DAL;

namespace AlarmCenter.DAL
{
    public class SystemLogDAL
    {
        public int pageCount{get;set;}

        public DataTable GetSystemLogsDataTable(string DBName, string tableName, int pageSize, int pageIndex)
        {
            DataPageDAL dataPage = new DataPageDAL();
            dataPage.PageSize = pageSize;
            dataPage.TableName = tableName;
            dataPage.PageIndex = pageIndex;
            dataPage.QueryFieldName = " * ";
            dataPage.OrderStr = "[序号]";
            dataPage.PrimaryKey = "[序号]";
            DataTable table = dataPage.QueryDataTable(DBName);
            pageCount = dataPage.PageCount;
            return table;

        }
    }

}
