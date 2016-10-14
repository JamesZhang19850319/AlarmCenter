using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace AlarmCenter.DAL
{
    public  class ContactsDAL
    {
        public string contactsNum { get; set; }
        public string account { get; set; }

        /// <summary>
        /// 获取一个用户的所有联系人资料
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public DataTable GetContactsDataTable(string account)
        {
            DataTable dt = OleDbHelper.ExecuteDataTable("Main", "select * from [联系人资料] where 主机编号=@account",
                new OleDbParameter("@account", account));
            return dt;
        }

        /// <summary>
        /// 获取一个用户的所有防区资料，返回一个防区对象数组
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public Contacts[] GetContactsArray(string account)
        {
            DataTable dt = OleDbHelper.ExecuteDataTable("Main", "select * from [联系人资料] where 主机编号=@account",
                new OleDbParameter("@account", account));
            Contacts[] contactss = new Contacts[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = dt.Rows[i];

                contactss[i] = ToContacts(row);
            }
            return contactss;
        }

        public void DeleteByID(int ID)
        {
            OleDbHelper.ExecuteNonQuery("Main", "delete from [联系人资料] where [序号]=@ID",
                new OleDbParameter("@ID", ID));
        }

        private Contacts ToContacts(DataRow row)
        {
            Contacts contacts = new Contacts();
            contacts.ID = (int)row["序号"];
            contacts.Account = (string)OleDbHelper.FromDbValue(row["主机编号"]);
            contacts.ContactsID = (string)OleDbHelper.FromDbValue(row["联系人序号"]);
            contacts.ContactsName = (string)OleDbHelper.FromDbValue(row["姓名"]);
            contacts.ContactsJob = (string)OleDbHelper.FromDbValue(row["职务"]);
            contacts.ContactsPhoneNumber = (string)OleDbHelper.FromDbValue(row["电话"]);
            return contacts;
        }

        public void Insert(Contacts contacts)
        {
            OleDbHelper.ExecuteNonQuery("Main", @"INSERT INTO [联系人资料] 
                        (主机编号,联系人序号,姓名,职务,电话) 
                 VALUES (@Account,@ContactsID,@ContactsName,@ContactsJob,@ContactsPhoneNumber)",
                new OleDbParameter("@Account", OleDbHelper.ToDbValue(contacts.Account)),
                new OleDbParameter("@ContactsID", OleDbHelper.ToDbValue(contacts.ContactsID)),
                new OleDbParameter("@ContactsName", OleDbHelper.ToDbValue(contacts.ContactsName)),
                new OleDbParameter("@ContactsJob", OleDbHelper.ToDbValue(contacts.ContactsJob)),
                new OleDbParameter("@ContactsPhoneNumber", OleDbHelper.ToDbValue(contacts.ContactsPhoneNumber)));
        }
        public void Update(Contacts contacts)
        {
            OleDbHelper.ExecuteNonQuery("Main", @"UPDATE [联系人资料]
                SET  [主机编号]=@Account
                    ,[联系人序号]=@ContactsID
                    ,[姓名]=@ContactsName
                    ,[职务]=@ContactsJob
                    ,[电话]=@PartitionNum
                WHERE [序号]=@ID",
                new OleDbParameter("@Account", OleDbHelper.ToDbValue(contacts.Account)),
                new OleDbParameter("@ContactsID", OleDbHelper.ToDbValue(contacts.ContactsID)),
                new OleDbParameter("@ContactsName", OleDbHelper.ToDbValue(contacts.ContactsName)),
                new OleDbParameter("@ContactsJob", OleDbHelper.ToDbValue(contacts.ContactsJob)),
                new OleDbParameter("@ContactsPhoneNumber", OleDbHelper.ToDbValue(contacts.ContactsPhoneNumber)),
                new OleDbParameter("@ID", OleDbHelper.ToDbValue(contacts.ID)));
        }

        /// <summary>
        /// 根据防区表序号字段获取该防区资料
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public Contacts GetContactsByID(int contactsID)
        {
            DataTable dt = OleDbHelper.ExecuteDataTable("Main", "select * from [联系人资料] where 序号=@contactsID",
                new OleDbParameter("@contactsID", contactsID));
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
                return ToContacts(row);
            }
        }
    }
}
