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
    public class UserDAL
    {

        //根据ID获取GetByID、Update、DeleteByID、GetAll、GetPagedData（分页数据）
        //Insert（插入新数据）
        public int pageCount{get;set;}
        private User ToUser(DataRow row)
        {
            User user = new User();
            user.ID=(int)row["序号"];
            user.Account = (string)OleDbHelper.FromDbValue(row["主机编号"]);
            user.UserName = (string)OleDbHelper.FromDbValue(row["用户名称"]);
            user.Address = (string)OleDbHelper.FromDbValue(row["用户地址"]);
            user.InstallCompany = (string)OleDbHelper.FromDbValue(row["安装单位"]);
            user.InstallDate = (DateTime)OleDbHelper.FromDbValue(row["安装日期"]);
            user.Installer = (string)OleDbHelper.FromDbValue(row["安装人员"]);
            user.LastTime = (DateTime)OleDbHelper.FromDbValue(row["最后一条事件时间"]);
            user.PanelName = (string)OleDbHelper.FromDbValue(row["主机类型"]);
            user.PanelStatus = (string)OleDbHelper.FromDbValue(row["主机状态"]);
            user.Protocol = (string)OleDbHelper.FromDbValue(row["通讯格式"]);
            user.Mark = (string)OleDbHelper.FromDbValue(row["备注说明"]);
            user.Trouble = (string)OleDbHelper.FromDbValue(row["故障状态"]);
            user.UserType = (string)OleDbHelper.FromDbValue(row["用户类型"]);
            user.Deadline = (DateTime)OleDbHelper.FromDbValue(row["合同期限"]);
            user.Charge = (string)OleDbHelper.FromDbValue(row["收费标准"]);
            return user;
        }

        private User NullUser()
        {
            User user = new User();
            user.UserName = "未知";
            user.Account = "未知";
            user.Address = "未知";
            user.InstallCompany = "未知";
            user.InstallDate = DateTime.Today;
            user.Installer = "未知";
            user.LastTime = DateTime.Today;
            user.PanelName = "未知";
            user.PanelStatus = "未知";
            user.Protocol = "未知";
            user.Mark = "未知";
            user.Trouble = "未知";
            user.UserType = "未知";
            user.Deadline = DateTime.Today;
            user.Charge = "未知";
            return user;
        }

        public User GetByID(int id)
        {
            DataTable dt = OleDbHelper.ExecuteDataTable("Main", "select * from [用户资料] where 序号=@ID",
                new OleDbParameter("@ID", id));
            if (dt.Rows.Count <= 0)
            {
                return null;
            }
            else if (dt.Rows.Count > 1)
            {
                throw new Exception("严重错误，查出多条数据！");
            }
            else
            {
                DataRow row = dt.Rows[0];
                return ToUser(row);
            }
        }
        /// <summary>
        /// 根据主机编号获取该用户信息
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public User GetUserInfomation(string account, string queryFieldName)
        {
            DataTable dt = OleDbHelper.ExecuteDataTable("Main", "select "+queryFieldName+" from [用户资料] where 主机编号=@account",
               new OleDbParameter("@account", account) );
            if (dt.Rows.Count <= 0)
            {

                return NullUser();
            }
            else if (dt.Rows.Count > 1)
            {
                throw new Exception("严重错误，查出多条数据！");
            }
            else
            {
                DataRow row = dt.Rows[0];
                return ToUserInfomation(row);
            }
        }
        private User ToUserInfomation(DataRow row)
        {
            User user = new User();
            user.Account = (string)OleDbHelper.FromDbValue(row["主机编号"]);
            user.UserName = (string)row["用户名称"];
            user.Address = (string)OleDbHelper.FromDbValue(row["用户地址"]);
            user.PanelName = (string)OleDbHelper.FromDbValue(row["主机类型"]);
            user.UserType = (string)OleDbHelper.FromDbValue(row["用户类型"]);
            return user;
        }

        public User GetUserInfomationByAccount(string account, string queryFieldName)
        {
            DataTable dt = OleDbHelper.ExecuteDataTable("Main", "select "+queryFieldName+" from [用户资料] where 主机编号=@account",
               new OleDbParameter("@account", account) );
            if (dt.Rows.Count <= 0)
            {

                return NullUser();
            }
            else if (dt.Rows.Count > 1)
            {
                throw new Exception("严重错误，查出多条数据！");
            }
            else
            {
                DataRow row = dt.Rows[0];
                return ToUserInfomationByAccount(row);
            }
        }

        private User ToUserInfomationByAccount(DataRow row)
        {
            User user = new User();
            user.Account = (string)OleDbHelper.FromDbValue(row["主机编号"]);
            user.UserName = (string)row["用户名称"];
            user.Address = (string)OleDbHelper.FromDbValue(row["用户地址"]);
            user.PanelName = (string)OleDbHelper.FromDbValue(row["主机类型"]);
            user.UserType = (string)OleDbHelper.FromDbValue(row["用户类型"]);
            user.Protocol = (string)OleDbHelper.FromDbValue(row["通讯格式"]);
            user.InstallDate = (DateTime)OleDbHelper.FromDbValue(row["安装日期"]);
            user.InstallCompany = (string)OleDbHelper.FromDbValue(row["安装单位"]);
            user.Installer = (string)OleDbHelper.FromDbValue(row["安装人员"]);
            user.Deadline = (DateTime)OleDbHelper.FromDbValue(row["合同期限"]);
            user.Charge = (string)OleDbHelper.FromDbValue(row["收费标准"]);
            return user;
        }

        public void DeleteByID(int id)
        {
            OleDbHelper.ExecuteNonQuery("Main", "delete from [用户资料] where [序号]=@ID",
                new OleDbParameter("@ID", id));
        }

        public void Insert(User user)
        {
            OleDbHelper.ExecuteNonQuery("Main", @"INSERT INTO [用户资料]
                       (主机编号,用户名称,用户地址,用户类型,通讯格式,主机类型,安装人员,安装日期,安装单位,收费标准,合同期限,备注说明)
                 VALUES
                       (@Account,@UserName, @Address,@UserType,@Protocol,@PanelName,@Installer,@InstallDate,@InstallCompany,@Charge,@Deadline,@Mark)",
                        new OleDbParameter("@Account", OleDbHelper.ToDbValue(user.Account)),
                        new OleDbParameter("@UserName", OleDbHelper.ToDbValue(user.UserName)),
                        new OleDbParameter("@Address", OleDbHelper.ToDbValue(user.Address)),
                        new OleDbParameter("@UserType", OleDbHelper.ToDbValue(user.UserType)),
                        new OleDbParameter("@Protocol", OleDbHelper.ToDbValue(user.Protocol)),
                        new OleDbParameter("@PanelName", OleDbHelper.ToDbValue(user.PanelName)),
                        new OleDbParameter("@Installer", OleDbHelper.ToDbValue(user.Installer)),
                        new OleDbParameter("@InstallDate", OleDbHelper.ToDbValue(user.InstallDate)),
                        new OleDbParameter("@InstallCompany", OleDbHelper.ToDbValue(user.InstallCompany)),
                        new OleDbParameter("@Charge", OleDbHelper.ToDbValue(user.Charge)),
                        new OleDbParameter("@Deadline", OleDbHelper.ToDbValue(user.Deadline)),
                        new OleDbParameter("@Mark", OleDbHelper.ToDbValue(user.Mark)));
        }

        public void Update(User user)
        {
            OleDbHelper.ExecuteNonQuery("Main", @"UPDATE [用户资料]
                SET 用户名称 = @UserName
                    ,用户地址 = @Address
                    ,主机编号 = @Account
                    ,用户类型 = @UserType
                    ,通讯格式 = @Protocol
                    ,主机类型 = @PanelName
                    ,安装人员 = @Installer
                    ,安装日期 = @InstallDate
                    ,安装单位 = @InstallCompany
                    ,收费标准 = @Charge
                    ,合同期限 = @Deadline
                    ,备注说明 = @Mark
                WHERE 序号=@ID",
                        new OleDbParameter("@Name", OleDbHelper.ToDbValue(user.UserName)),
                        new OleDbParameter("@Address", OleDbHelper.ToDbValue(user.Address)),
                        new OleDbParameter("@Account", OleDbHelper.ToDbValue(user.Account)),
                        new OleDbParameter("@UserType", OleDbHelper.ToDbValue(user.UserType)),
                        new OleDbParameter("@Protocol", OleDbHelper.ToDbValue(user.Protocol)),
                        new OleDbParameter("@PanelName", OleDbHelper.ToDbValue(user.PanelName)),
                        new OleDbParameter("@Installer", OleDbHelper.ToDbValue(user.Installer)),
                        new OleDbParameter("@InstallDate", OleDbHelper.ToDbValue(user.InstallDate)),
                        new OleDbParameter("@InstallCompany", OleDbHelper.ToDbValue(user.InstallCompany)),
                        new OleDbParameter("@Charge", OleDbHelper.ToDbValue(user.Charge)),
                        new OleDbParameter("@Deadline", OleDbHelper.ToDbValue(user.Deadline)),
                        new OleDbParameter("@Mark", OleDbHelper.ToDbValue(user.Mark)),
                        new OleDbParameter("@ID", OleDbHelper.ToDbValue(user.ID)));
        }
        public User[] GetUsersDataTable(string DBName, string tableName, int pageSize, int pageIndex)
        {
            //DataPageDAL dataPage = new DataPageDAL();
            //dataPage.PageSize = pageSize;
            //dataPage.TableName = "用户资料";
            //dataPage.PageIndex = pageIndex;
            //dataPage.QueryFieldName = " 用户名称,用户地址,主机编号,用户类型,用户状态 ";
            //dataPage.OrderStr = "[主机编号]";
            //dataPage.PrimaryKey = "[主机编号]";
            //DataTable table = dataPage.QueryDataTable(DBName);
            //pageCount = dataPage.PageCount;
            //return table;

            DataPageDAL dataPage = new DataPageDAL();
            dataPage.PageSize = pageSize;
            dataPage.TableName = "用户资料";
            dataPage.PageIndex = pageIndex;
            dataPage.QueryFieldName = " 序号,主机编号,用户名称,用户地址,安装单位,安装日期,安装人员,主机类型,主机状态,通讯格式,备注说明,故障状态,用户类型,合同期限,收费标准,最后一条事件时间 ";
            dataPage.OrderStr = "[序号]";
            dataPage.PrimaryKey = "[序号]";
            DataTable table = dataPage.QueryDataTable(DBName);
            pageCount = dataPage.PageCount;
            User[] users = new User[table.Rows.Count];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataRow row = table.Rows[i];

                users[i] = ToUser(row);
            }
            return users;
        }

        /// <summary>
        /// 获取所有用户资料的主机编号
        /// </summary>
        /// <returns></returns>
        public String[] GetaccountArray()
        {
            DataTable table = OleDbHelper.ExecuteDataTable("Main", "select " + " 主机编号 " + " from [用户资料]");
            String[] accountArray = new String[table.Rows.Count];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataRow row = table.Rows[i];

                accountArray[i]= (string)OleDbHelper.FromDbValue(row["主机编号"]);
            }
            return accountArray;
        }

        public User[] GetUsers(int pageSize,int pageIndex,string tableName)
        {
            //DataTable table = OleDbHelper.ExecuteDataTable("Main", "select * from [用户资料]");

            //User[] users = new User[table.Rows.Count];
            //for (int i = 0; i < table.Rows.Count; i++)
            //{
            //    DataRow row = table.Rows[i];

            //    users[i] = ToUser(row);
            //}
            //return users;

            DataPageDAL dataPage = new DataPageDAL();
            dataPage.PageSize = pageSize;
            dataPage.TableName = "用户资料";
            dataPage.PageIndex = pageIndex;
            dataPage.QueryFieldName = " * ";
            dataPage.OrderStr = "[主机编号]";
            dataPage.PrimaryKey = "[主机编号]";
            DataTable table = dataPage.QueryDataTable("Main");
            pageCount=dataPage.PageCount;

            User[] users = new User[table.Rows.Count];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataRow row = table.Rows[i];

                users[i] = ToUser(row);
            }
            return users;
            
        }
    }

}
