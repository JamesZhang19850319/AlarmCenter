using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace AlarmCenter.DAL
{
   public  class GroupDAL
    {
        private Group[] ToGroup(DataRow[] rows)
        {
            Group[] groups = new Group[rows.Length];
            for(int i=0;i<rows.Length;i++)
            {
                groups[i].ID = (int)OleDbHelper.FromDbValue(rows[i]["ID"]);
                groups[i].MainGroup = (string)OleDbHelper.FromDbValue(rows[i]["组"]);
                groups[i].SubGroup = (string)OleDbHelper.FromDbValue(rows[i]["子组"]);
            }
            return groups;
        }
        /// <summary>
        /// 根据主机编号获取该用户信息
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public DataTable GetGroupItems(string group)
        {
            DataTable dt = OleDbHelper.ExecuteDataTable("Main", "select * from [可选项及内容] where 组=@group ",
                new OleDbParameter("@group", group));
           if (dt.Rows.Count <= 0)
            {
                return null;
            }
            else 
            {
                return dt;  
            }    
        }
    }
}
