using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlarmCenter.DAL
{
    public class Contacts
    {
        public int ID { get; set; }//序号
        public string ContactsID { get; set; }//联系人序号
        public string Account { get; set; }//主机编号
        public string ContactsName { get; set; }//姓名
        public string ContactsJob { get; set; }//职务
        public string ContactsPhoneNumber { get; set; }//电话
    }
}
