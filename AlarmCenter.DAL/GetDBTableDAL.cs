using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace AlarmCenter.DAL
{
    public class GetDBTableDAL
    {
        public DataTable GetMainTable(string tableName)
        {
            DataTable table = OleDbHelper.ExecuteDataTable("Main","select * from "+ tableName);
            return table;
        }
        public DataTable GetEventTable(string tableName)
        {
            DataTable table = OleDbHelper.ExecuteDataTable("Event", "select * from " + tableName);
            return table;
        }
    }
}
