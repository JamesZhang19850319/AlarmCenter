using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlarmCenter.DAL
{
    public class Zone
    {
        public int ID { get; set; }//序号
        public string Account { get; set; }//主机编号
        public int ZoneNum { get; set; }//防区编号
        public string PartitionNum { get; set; }//分区编号
        public string ZoneType { get; set; }//防区类型
        public string DetectorType { get; set; }//探测器型号
        public string InstallSide { get; set; }//安装位置
        public string ZoneStatus { get; set; }//防区状态
        public int Abscissa { get; set; }//横坐标
        public int Ordinate { get; set; }//纵坐标
        public DateTime StatusRefreshTime { get; set; }//防区状态更新时间
        public DateTime EedEventTime { get; set; }//最近一次上报时间
        public bool IsAlarm { get; set; }//是否报警
    }
}
