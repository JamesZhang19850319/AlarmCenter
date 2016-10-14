using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlarmCenter.DAL
{
    public class CID
    {
        public int ID { set; get; }
        public string CIDCode { set; get; }
        public bool IsNewEvent { set; get; }
        public string EventTpye { set; get; }
        public string EventInformation { set; get; }
        public string StrategiesName { set; get; }
        public bool IsSendMessage { set; get; }
    }
}
