using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlarmCenter.DAL 
{
    public class User
    {
        public int ID { get; set; }
        public string Account { get; set; }
        public string UserName { get; set; }
        public string Address { get; set; }   
        public string UserType { get; set; }
        public string Mark { get; set; }
        public string Protocol { get; set; }
        public string PanelName { get; set; }
        public DateTime InstallDate { get; set; }
        public string InstallCompany { get; set; }
        public string Installer { get; set; }
        public DateTime Deadline { get; set; }
        public string Charge { get; set; }
        public string PanelStatus { get; set; }
        public string Trouble { get; set; }
        public DateTime LastTime { get; set; }
    }
}
