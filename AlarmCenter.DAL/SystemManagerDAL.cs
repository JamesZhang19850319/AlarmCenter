using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using AlarmCenter.DAL;
using System.Data;

namespace AlarmCenter.DAL
{
    public class SystemManagerDAL
    {


        public SystemManager[] ListAll()
        {
            DataTable dt = OleDbHelper.ExecuteDataTable("Main", "select * from [值班人员]");
            SystemManager[] systemmanagers = new SystemManager[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                systemmanagers[i] = ToSystemManager(dt.Rows[i]);
            }
            return systemmanagers;
        }

        private SystemManager ToSystemManager(DataRow row)
        {
            SystemManager sm = new SystemManager();
            sm.UserName = (string)OleDbHelper.FromDbValue(row["值班员代码"]);
            sm.Password = (string)OleDbHelper.FromDbValue(row["值班员口令"]);
            sm.RealName = (string)OleDbHelper.FromDbValue(row["值班员姓名"]);
            return sm;
        }

        public SystemManager GetByUserName(string userName)
        {
            DataTable table = OleDbHelper.ExecuteDataTable("Main", "select * from [值班人员] where [值班员代码]=@UserName",
                new OleDbParameter("@UserName", userName));
            if (table.Rows.Count <= 0)
            {
                return null;
            }
            else if (table.Rows.Count > 1)
            {
                throw new Exception("存在重名用户！");
            }
            else
            {
                return ToSystemManager(table.Rows[0]);
            }
        }
    }
}
