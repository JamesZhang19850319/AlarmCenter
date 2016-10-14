using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlarmCenter.DAL
{
    public class Event
    {
        public string ID { get; set; }//序号
        public DateTime AlarmTime { get; set; }//报警时间
        public string UserName { get; set; }//用户名称
        public string Address { get; set; }//用户地址
        public string Account { get; set; }//主机编号
        public string UserType { get; set; }//用户类型
        public string PanelType { get; set; }//主机类型
        public string PartitionNumber { get; set; }//分区编号
        public string ZoneNumber { get; set; }//防区编号
        public string ZoneType { get; set; }//防区类型
        public string DetectorType { get; set; }//探测器型号
        public string InstallSide { get; set; }//安装位置
        public string EventTpye { get; set; }//事件类型
        public string EventInfomation { get; set; }//辅助信息
        public string Classify { get; set; }//归类处理
        public string DataCode { get; set; }//通讯代码
        public string MarkEvent { get; set; }//处理内容
        public string Operator { get; set; }//操作员姓名
        public string EventFontColor { get; set; }//事件字体颜色
        public string EventBackgroundColor { get; set; }//事件背景颜色
        public string TellNum { get; set; }//来电号码
        public string Side { get; set; }//站点编号
        public string TowLeverSide { get; set; }//二级站点
    }
}
