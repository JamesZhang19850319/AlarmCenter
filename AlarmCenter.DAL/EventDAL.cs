using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using AlarmCenter.DAL;
using System.Data.OleDb;

namespace AlarmCenter.DAL
{
    public class EventDAL
    {
        public int pageCount { get; set; }
        DataPageDAL dataPage = new DataPageDAL();
        //OleDbParameter[][] parameters;
        string queryFieldName= " 序号,报警时间,用户名称,用户地址,主机编号,用户类型,主机类型,分区编号,防区编号,防区类型,探测器型号,安装位置,事件类型,辅助信息,归类处理,通讯代码,处理内容,值班员姓名,事件字体颜色,事件背景颜色,来电号码,站点编号,二级站点 ";

        //public DataTable GetEventsDataTable(string DBName, string tableName, int pageSize, int pageIndex)
        //{
        //    dataPage.PageSize = pageSize;
        //    dataPage.TableName = tableName;
        //    dataPage.PageIndex = pageIndex;
        //    dataPage.QueryFieldName = " 序号,报警时间,用户名称,用户地址,主机编号,用户类型,主机类型,分区编号,防区编号,防区类型,探测器型号,安装位置,事件类型,辅助信息,归类处理,通讯代码,处理内容,值班员姓名,事件字体颜色,事件背景颜色,来电号码,站点编号,二级站点 ";
        //    dataPage.OrderStr = "[序号]";
        //    dataPage.PrimaryKey = "[序号]";
        //    DataTable table = dataPage.QueryDataTable(DBName);
        //    pageCount = dataPage.PageCount;
        //    return table;
        //}
        public DataTable GetEventsDataTable(string dbName, string tableName, string queryFieldName)
        {
            DataTable dt = OleDbHelper.ExecuteDataTable(dbName, "select " + queryFieldName + " from " + tableName+" order by [报警时间] desc");
            return dt;
        }
        public Event[] GetEventsArray(string dbName, string tableName)
        {
            DataTable table = GetEventsDataTable(dbName, tableName, queryFieldName);

            Event[] events = new Event[table.Rows.Count];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataRow row = table.Rows[i];

                events[i] = ToEvent(row);
            }
            return events;

        }
        public Event GetCurrentEventOnlyOne()
        {
            DataTable dt = OleDbHelper.ExecuteDataTable("Event", "select " + queryFieldName + " from [报警事件] where 序号= (select max(序号) from  报警事件) ");
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
                return ToEvent(row);
            }
        }
        private Event ToEvent(DataRow row)
        {
            Event _event = new Event();
            _event.ID = (string)row["序号"];
            _event.AlarmTime = (DateTime)OleDbHelper.FromDbValue(row["报警时间"]);
            _event.UserName = (string)row["用户名称"];
            _event.Address = (string)OleDbHelper.FromDbValue(row["用户地址"]);
            _event.Account = (string)OleDbHelper.FromDbValue(row["主机编号"]);
            _event.UserType = (string)OleDbHelper.FromDbValue(row["用户类型"]);
            _event.PanelType = (string)OleDbHelper.FromDbValue(row["主机类型"]);
            _event.PartitionNumber = (string)OleDbHelper.FromDbValue(row["分区编号"]);
            _event.ZoneNumber = (string)OleDbHelper.FromDbValue(row["防区编号"]);
            _event.ZoneType = (string)OleDbHelper.FromDbValue(row["防区类型"]);
            _event.DetectorType = (string)OleDbHelper.FromDbValue(row["探测器型号"]);
            _event.InstallSide = (string)OleDbHelper.FromDbValue(row["安装位置"]);
            _event.EventTpye = (string)OleDbHelper.FromDbValue(row["事件类型"]);
            _event.EventInfomation = (string)OleDbHelper.FromDbValue(row["辅助信息"]);
            _event.Classify = (string)OleDbHelper.FromDbValue(row["归类处理"]);
            _event.DataCode = (string)OleDbHelper.FromDbValue(row["通讯代码"]);
            _event.MarkEvent = (string)OleDbHelper.FromDbValue(row["处理内容"]);
            _event.Operator = (string)OleDbHelper.FromDbValue(row["值班员姓名"]);
            _event.EventFontColor = (string)OleDbHelper.FromDbValue(row["事件字体颜色"]);
            _event.EventBackgroundColor = (string)OleDbHelper.FromDbValue(row["事件背景颜色"]);
            _event.TellNum = (string)OleDbHelper.FromDbValue(row["来电号码"]);
            _event.Side = (string)OleDbHelper.FromDbValue(row["站点编号"]);
            _event.TowLeverSide = (string)OleDbHelper.FromDbValue(row["二级站点"]);
            return _event;
        }
        public void Update(Event e)
        {
            OleDbParameter[] parameters = new OleDbParameter[22];
            parameters[0] = new OleDbParameter("@UserName", OleDbHelper.ToDbValue(e.UserName));
            parameters[1] = new OleDbParameter("@Address", OleDbHelper.ToDbValue(e.Address));
            parameters[2] = new OleDbParameter("@Account", OleDbHelper.ToDbValue(e.Account));
            parameters[3] = new OleDbParameter("@UserType", OleDbHelper.ToDbValue(e.UserType));
            parameters[4] = new OleDbParameter("@PanelType", OleDbHelper.ToDbValue(e.PanelType));
            parameters[5] = new OleDbParameter("@PartitionNumber", OleDbHelper.ToDbValue(e.PartitionNumber));
            parameters[6] = new OleDbParameter("@ZoneNumber", OleDbHelper.ToDbValue(e.ZoneNumber));
            parameters[7] = new OleDbParameter("@ZoneType", OleDbHelper.ToDbValue(e.ZoneType));
            parameters[8] = new OleDbParameter("@DetectorType", OleDbHelper.ToDbValue(e.DetectorType));
            parameters[9] = new OleDbParameter("@InstallSide", OleDbHelper.ToDbValue(e.InstallSide));
            parameters[10] = new OleDbParameter("@EventTpye", OleDbHelper.ToDbValue(e.EventTpye));
            parameters[11] = new OleDbParameter("@EventInfomation", OleDbHelper.ToDbValue(e.EventInfomation));
            parameters[12] = new OleDbParameter("@Classify", OleDbHelper.ToDbValue(e.Classify));
            parameters[13] = new OleDbParameter("@DataCode", OleDbHelper.ToDbValue(e.DataCode));
            parameters[14] = new OleDbParameter("@MarkEvent", OleDbHelper.ToDbValue(e.MarkEvent));
            parameters[15] = new OleDbParameter("@Operator", OleDbHelper.ToDbValue(e.Operator));
            parameters[16] = new OleDbParameter("@EventFontColor", OleDbHelper.ToDbValue(e.EventFontColor));
            parameters[17] = new OleDbParameter("@EventBackgroundColor", OleDbHelper.ToDbValue(e.EventBackgroundColor));
            parameters[18] = new OleDbParameter("@TellNum", OleDbHelper.ToDbValue(e.TellNum));
            parameters[19] = new OleDbParameter("@Side", OleDbHelper.ToDbValue(e.Side));
            parameters[20] = new OleDbParameter("@TowLeverSide", OleDbHelper.ToDbValue(e.TowLeverSide));
            parameters[21] = new OleDbParameter("@ID", OleDbHelper.ToDbValue(e.ID));

            OleDbHelper.ExecuteNonQuery1("Event", @"UPDATE [报警事件]
                SET   [用户名称]=@UserName,[用户地址]=@Address,[主机编号]=@Account,[用户类型]=@UserType,[主机类型]=@PanelType,[分区编号]=@PartitionNumber,
                      [防区编号]=@ZoneNumber,[防区类型]=@ZoneType,[探测器型号]=@DetectorType,[安装位置]=@InstallSide,[事件类型]=@EventTpye,
                      [辅助信息]=@EventInfomation,[归类处理]=@Classify,[通讯代码]=@DataCode,[处理内容]=@MarkEvent,[值班员姓名]=@Operator,[事件字体颜色]=@EventFontColor,
                      [事件背景颜色]=@EventBackgroundColor,[来电号码]=@TellNum,[站点编号]=@Side,[二级站点]=@TowLeverSide
                WHERE [序号]=@ID"
                , parameters);
        }
        public void Insert(Event e)
        {
            OleDbParameter[] parameters = new OleDbParameter[22];
            parameters[0] = new OleDbParameter("@ID", OleDbHelper.ToDbValue(e.ID));
            parameters[1] = new OleDbParameter("@UserName", OleDbHelper.ToDbValue(e.UserName));
            parameters[2] = new OleDbParameter("@Address", OleDbHelper.ToDbValue(e.Address));
            parameters[3] = new OleDbParameter("@Account", OleDbHelper.ToDbValue(e.Account));
            parameters[4] = new OleDbParameter("@UserType", OleDbHelper.ToDbValue(e.UserType));
            parameters[5] = new OleDbParameter("@PanelType", OleDbHelper.ToDbValue(e.PanelType));
            parameters[6] = new OleDbParameter("@PartitionNumber", OleDbHelper.ToDbValue(e.PartitionNumber));
            parameters[7] = new OleDbParameter("@ZoneNumber", OleDbHelper.ToDbValue(e.ZoneNumber));
            parameters[8] = new OleDbParameter("@ZoneType", OleDbHelper.ToDbValue(e.ZoneType));
            parameters[9] = new OleDbParameter("@DetectorType", OleDbHelper.ToDbValue(e.DetectorType));
            parameters[10] = new OleDbParameter("@InstallSide", OleDbHelper.ToDbValue(e.InstallSide));
            parameters[11] = new OleDbParameter("@EventTpye", OleDbHelper.ToDbValue(e.EventTpye));
            parameters[12] = new OleDbParameter("@EventInfomation", OleDbHelper.ToDbValue(e.EventInfomation));
            parameters[13] = new OleDbParameter("@Classify", OleDbHelper.ToDbValue(e.Classify));
            parameters[14] = new OleDbParameter("@DataCode", OleDbHelper.ToDbValue(e.DataCode));
            parameters[15] = new OleDbParameter("@MarkEvent", OleDbHelper.ToDbValue(e.MarkEvent));
            parameters[16] = new OleDbParameter("@Operator", OleDbHelper.ToDbValue(e.Operator));
            parameters[17] = new OleDbParameter("@EventFontColor", OleDbHelper.ToDbValue(e.EventFontColor));
            parameters[18] = new OleDbParameter("@EventBackgroundColor", OleDbHelper.ToDbValue(e.EventBackgroundColor));
            parameters[19] = new OleDbParameter("@TellNum", OleDbHelper.ToDbValue(e.TellNum));
            parameters[20] = new OleDbParameter("@Side", OleDbHelper.ToDbValue(e.Side));
            parameters[21] = new OleDbParameter("@TowLeverSide", OleDbHelper.ToDbValue(e.TowLeverSide));

            OleDbHelper.ExecuteNonQuery1("Event", @"INSERT INTO [报警事件]
                       ([序号],[用户名称],[用户地址],[主机编号],[用户类型],[主机类型],[分区编号],[防区编号],[防区类型],[探测器型号],[安装位置],[事件类型]
                       ,[辅助信息],[归类处理],[通讯代码],[处理内容],[值班员姓名],[事件字体颜色],[事件背景颜色],[来电号码],[站点编号],[二级站点])
                 VALUES
                       (@ID,@UserName,@Address,@Account,@UserType,@PanelType,@PartitionNumber,@ZoneNumber,@ZoneType,@DetectorType,@InstallSide,@EventTpye,
                        @EventInfomation,@Classify,@DataCode,@MarkEvent,@Operator,@EventFontColor,@EventBackgroundColor,@TellNum,@Side,@TowLeverSide)",
            parameters);
        }
//        public void Insert(Event[] e)
//        {
//            parameters = new OleDbParameter[e.Length][];
//            for (int i = 0; i < e.Length; i++)
//            {
//                parameters[i]=new OleDbParameter[21];
//                parameters[i][0] = new OleDbParameter("@UserName", OleDbHelper.ToDbValue(e[i].UserName));
//                parameters[i][1] = new OleDbParameter("@Address", OleDbHelper.ToDbValue(e[i].Address));
//                parameters[i][2] = new OleDbParameter("@Account", OleDbHelper.ToDbValue(e[i].Account));
//                parameters[i][3] = new OleDbParameter("@UserType", OleDbHelper.ToDbValue(e[i].UserType));
//                parameters[i][4] = new OleDbParameter("@PanelType", OleDbHelper.ToDbValue(e[i].PanelType));
//                parameters[i][5] = new OleDbParameter("@PartitionNumber", OleDbHelper.ToDbValue(e[i].PartitionNumber));
//                parameters[i][6] = new OleDbParameter("@ZoneNumber", OleDbHelper.ToDbValue(e[i].ZoneNumber));
//                parameters[i][7] = new OleDbParameter("@ZoneType", OleDbHelper.ToDbValue(e[i].ZoneType));
//                parameters[i][8] = new OleDbParameter("@DetectorType", OleDbHelper.ToDbValue(e[i].DetectorType));
//                parameters[i][9] = new OleDbParameter("@InstallSide", OleDbHelper.ToDbValue(e[i].InstallSide));
//                parameters[i][10] = new OleDbParameter("@EventTpye", OleDbHelper.ToDbValue(e[i].EventTpye));
//                parameters[i][11] = new OleDbParameter("@EventInfomation", OleDbHelper.ToDbValue(e[i].EventInfomation));
//                parameters[i][12] = new OleDbParameter("@Classify", OleDbHelper.ToDbValue(e[i].Classify));
//                parameters[i][13] = new OleDbParameter("@DataCode", OleDbHelper.ToDbValue(e[i].DataCode));
//                parameters[i][14] = new OleDbParameter("@Mark", OleDbHelper.ToDbValue(e[i].Mark));
//                parameters[i][15] = new OleDbParameter("@Operator", OleDbHelper.ToDbValue(e[i].Operator));
//                parameters[i][16] = new OleDbParameter("@EventFontColor", OleDbHelper.ToDbValue(e[i].EventFontColor));
//                parameters[i][17] = new OleDbParameter("@EventBackgroundColor", OleDbHelper.ToDbValue(e[i].EventBackgroundColor));
//                parameters[i][18] = new OleDbParameter("@TellNum", OleDbHelper.ToDbValue(e[i].TellNum));
//                parameters[i][19] = new OleDbParameter("@Side", OleDbHelper.ToDbValue(e[i].Side));
//                parameters[i][20] = new OleDbParameter("@TowLeverSide", OleDbHelper.ToDbValue(e[i].TowLeverSide));
//            }
//            OleDbHelper.ExecuteNonQuery1("Event", @"INSERT INTO [报警事件]
//                       ([用户名称],[用户地址],[主机编号],[用户类型],[主机类型],[分区编号],[防区编号],[防区类型],[探测器型号],[安装位置],[事件类型]
//                       ,[辅助信息],[归类处理],[通讯代码],[处理内容],[值班员姓名],[事件字体颜色],[事件背景颜色],[来电号码],[站点编号],[二级站点])
//                 VALUES
//                       (@UserName,@Address,@Account,@UserType,@PanelType,@PartitionNumber,@ZoneNumber,@ZoneType,@DetectorType,@InstallSide,@EventTpye,
//                        @EventInfomation,@Classify,@DataCode,@Mark,@Operator,@EventFontColor,@EventBackgroundColor,@TellNum,@Side,@TowLeverSide)",
//            parameters);
//        }
    }

}
