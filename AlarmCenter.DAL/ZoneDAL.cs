using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlarmCenter.DAL;
using System.Data;
using System.Data.OleDb;

namespace AlarmCenter.DAL
{
    public class ZoneDAL
    {
        public string zoneNum { get; set; }
        public string account { get; set; }

        public Zone[] ListAll()
        {
            DataTable dt = GetZonesDataTable(account);
            Zone[] zones = new Zone[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                zones[i] = ToZone(dt.Rows[i]);
            }
            return zones;
        }
        public DataTable GetZonesDataTable(string account)
        {
            DataTable dt = OleDbHelper.ExecuteDataTable("Main", "select * from 防区资料 where 主机编号=@account",
                new OleDbParameter("@account", account));
            return dt;
        }
        /// <summary>
        /// 获取一个用户的所有防区资料，返回一个防区对象数组
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public Zone[] GetZonesArray(string account)
        {
            DataTable dt = OleDbHelper.ExecuteDataTable("Main", "select * from 防区资料 where 主机编号=@account",
                new OleDbParameter("@account", account));
            Zone[] zones = new Zone[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = dt.Rows[i];

                zones[i] = ToZone(row);
            }
            return zones;
        }
        public void DeleteByID(int ID)
        {
            OleDbHelper.ExecuteNonQuery("Main", "delete from 防区资料 where 序号=@ID",
                new OleDbParameter("@ID", ID));
        }
        private Zone ToZone(DataRow row)
        {
            Zone zone = new Zone();
            zone.ID = (int)row["序号"];
            zone.Account = (string)OleDbHelper.FromDbValue(row["主机编号"]);
            zone.ZoneNum = (int)OleDbHelper.FromDbValue(row["防区编号"]);
            zone.PartitionNum = (string)OleDbHelper.FromDbValue(row["分区编号"]);
            zone.ZoneType = (string)OleDbHelper.FromDbValue(row["防区类型"]);
            zone.DetectorType = (string)OleDbHelper.FromDbValue(row["探测器型号"]);
            zone.InstallSide = (string)OleDbHelper.FromDbValue(row["安装位置"]);
            zone.ZoneStatus = (string)OleDbHelper.FromDbValue(row["防区状态"]);
            zone.Abscissa = (int)OleDbHelper.FromDbValue(row["横坐标"]);
            zone.Ordinate = (int)OleDbHelper.FromDbValue(row["纵坐标"]);
            zone.StatusRefreshTime = (DateTime)OleDbHelper.FromDbValue(row["防区状态更新时间"]);
            zone.EedEventTime = (DateTime)OleDbHelper.FromDbValue(row["最近一次上报时间"]);
            zone.IsAlarm = (bool)OleDbHelper.FromDbValue(row["是否报警"]);
            return zone;
        }
        private Zone NullZone()
        {
            Zone zone = new Zone();
            zone.Account = "";
            zone.ZoneNum = 0;
            zone.PartitionNum = "";
            zone.ZoneType = "";
            zone.DetectorType = "";
            zone.InstallSide = "";
            zone.ZoneStatus = "";
            zone.Abscissa = 1;
            zone.Ordinate = 1;
            zone.StatusRefreshTime = DateTime.Now;
            zone.EedEventTime = DateTime.Now;
            zone.IsAlarm = false;
            return zone;
        }
        public void Insert(Zone zone)
        {
            OleDbHelper.ExecuteNonQuery("Main", @"INSERT INTO 防区资料 
                        ([主机编号],[防区编号],[分区编号],[防区类型],[探测器型号],[安装位置],[防区状态],[横坐标],[纵坐标],[防区状态更新时间],[最近一次上报时间],[是否报警]) 
                 VALUES (@Account,@ZoneNum,@PartitionNum,@ZoneType,@DetectorType,@InstallSide,@ZoneStatus,@Abscissa,@Ordinate,@StatusRefreshTime,@EedEventTime,@IsAlarm)",
                new OleDbParameter("@Account", OleDbHelper.ToDbValue(zone.Account)),
                new OleDbParameter("@ZoneNum", OleDbHelper.ToDbValue(zone.ZoneNum)),
                new OleDbParameter("@PartitionNum", OleDbHelper.ToDbValue(zone.PartitionNum)),
                new OleDbParameter("@ZoneType", OleDbHelper.ToDbValue(zone.ZoneType)),
                new OleDbParameter("@DetectorType", OleDbHelper.ToDbValue(zone.DetectorType)),
                new OleDbParameter("@InstallSide", OleDbHelper.ToDbValue(zone.InstallSide)),
                new OleDbParameter("@ZoneStatus", OleDbHelper.ToDbValue(zone.ZoneStatus)),
                new OleDbParameter("@Abscissa", OleDbHelper.ToDbValue(zone.Abscissa)),
                new OleDbParameter("@Ordinate", OleDbHelper.ToDbValue(zone.Ordinate)),
                new OleDbParameter("@StatusRefreshTime", OleDbHelper.ToDbValue(zone.StatusRefreshTime)),
                new OleDbParameter("@EedEventTime", OleDbHelper.ToDbValue(zone.EedEventTime)),
                new OleDbParameter("@IsAlarm", OleDbHelper.ToDbValue(zone.IsAlarm)));
        }
        public void Update(Zone zone)
        {
            OleDbHelper.ExecuteNonQuery("Main", @"UPDATE 防区资料
                SET  [主机编号]=@Account
                    ,[防区编号]=@ZoneNum
                    ,[分区编号]=@PartitionNum
                    ,[防区类型]=@ZoneType
                    ,[探测器型号]=@DetectorType
                    ,[安装位置]=@InstallSide
                    ,[防区状态]=@ZoneStatus
                    ,[横坐标]=@Abscissa
                    ,[纵坐标]=@Ordinate
                    ,[防区状态更新时间]=@StatusRefreshTime
                    ,[最近一次上报时间]=@EedEventTime
                    ,[是否报警]=@IsAlarm 
                WHERE [序号]=@ID",
                new OleDbParameter("@Account", OleDbHelper.ToDbValue(zone.Account)),
                new OleDbParameter("@ZoneNum", OleDbHelper.ToDbValue(zone.ZoneNum)),
                new OleDbParameter("@PartitionNum", OleDbHelper.ToDbValue(zone.PartitionNum)),
                new OleDbParameter("@ZoneType", OleDbHelper.ToDbValue(zone.ZoneType)),
                new OleDbParameter("@DetectorType", OleDbHelper.ToDbValue(zone.DetectorType)),
                new OleDbParameter("@InstallSide", OleDbHelper.ToDbValue(zone.InstallSide)),
                new OleDbParameter("@ZoneStatus", OleDbHelper.ToDbValue(zone.ZoneStatus)),
                new OleDbParameter("@Abscissa", OleDbHelper.ToDbValue(zone.Abscissa)),
                new OleDbParameter("@Ordinate", OleDbHelper.ToDbValue(zone.Ordinate)),
                new OleDbParameter("@StatusRefreshTime", OleDbHelper.ToDbValue(zone.StatusRefreshTime)),
                new OleDbParameter("@EedEventTime", OleDbHelper.ToDbValue(zone.EedEventTime)),
                new OleDbParameter("@IsAlarm", OleDbHelper.ToDbValue(zone.IsAlarm)),
                new OleDbParameter("@ID", OleDbHelper.ToDbValue(zone.ID)));
        }
        /// <summary>
        /// 根据主机编号和防区编号获取该用户的防区信息
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public Zone GetZoneByAccountAddZoneNum(string account, int zoneNum)
        {
            DataTable dt = OleDbHelper.ExecuteDataTable("Main", "select * from 防区资料 where 主机编号=@account AND 防区编号=@ZoneNum",
                new OleDbParameter("@account", account), new OleDbParameter("@ZoneNum", zoneNum));
            if (dt.Rows.Count <= 0)
            {
                return NullZone();
            }
            else if (dt.Rows.Count > 1)
            {
                throw new Exception("严重错误，查出多条数据！");
            }
            else
            {
                DataRow row = dt.Rows[0];
                return ToZone(row);
            }
        }
        /// <summary>
        /// 根据防区表序号字段获取该防区资料
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public Zone GetZoneByID(int zoneID)
        {
            DataTable dt = OleDbHelper.ExecuteDataTable("Main", "select * from 防区资料 where 序号=@zoneID",
                new OleDbParameter("@zoneID", zoneID));
            if (dt.Rows.Count <= 0)
            {
                return NullZone();
            }
            else if (dt.Rows.Count > 1)
            {
                throw new Exception("严重错误，查出多条数据！");
            }
            else
            {
                DataRow row = dt.Rows[0];
                return ToZone(row);
            }
        }
    }
}
