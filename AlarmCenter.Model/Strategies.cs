using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlarmCenter.DAL
{
    public class Strategies//处理策略
    {
        public int ID { get; set; }//序号
        public string StrategiesID { get; set; }//策略编号
        public string StrategiesName { get; set; }//报警事件颜色
        public string EventFontColor { get; set; }//事件字体颜色
        public string EventBackgroundColor { get; set; }//事件背景颜色
        public bool IsPrint { get; set; }//自动打印
        public string NoticeType { get; set; }//提示类型
        public string SoundFile { get; set; }//报警声文件
    }
}
