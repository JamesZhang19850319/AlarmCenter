using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlarmCenter.DAL;
using System.Data.OleDb;
using System.Data;

namespace AlarmCenter.DAL
{
    public class CIDDAL
    {
        public string CID { get; set; }
        public bool IsNewEvent { get; set; }

        /// <summary>
        /// 获取数据库表名得到对象数组
        /// </summary>
        /// <param name="formName"></param>
        /// <returns></returns>
        public CID[] GetReportFormatArray(string formName)
        {
            DataTable dt = OleDbHelper.ExecuteDataTable("Main", "select * from " + formName);
            CID[] cids = new CID[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = dt.Rows[i];

                cids[i] = ToCID(row);
            }
            return cids;
        }

        public CID GetReportFormatByID(string tableName, int id)
        {
            DataTable dt = OleDbHelper.ExecuteDataTable("Main", "select * from " + tableName + " where 序号=" + id.ToString());
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
                return ToCID(row);
            }
        }

        public CID GetEventInformation(string isNewEvent, string CID)
        {
            if (isNewEvent == "E")
                IsNewEvent = true;
            else
                IsNewEvent = false;
            DataTable dt = OleDbHelper.ExecuteDataTable("Main", "select * from [DRC_CONTACTID] where CID码=@ID AND 是否是新事件=@IsNewEvent",
            new OleDbParameter("@ID", CID), new OleDbParameter("@IsNewEvent", IsNewEvent));
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
                return ToCID(row);
            }
        }
        private CID ToCID(DataRow row)
        {
            CID cid = new CID();
            cid.ID = (int)row["序号"];
            cid.CIDCode = (string)row["CID码"];
            cid.IsNewEvent = (bool)OleDbHelper.FromDbValue(row["是否是新事件"]);
            cid.EventInformation = (string)OleDbHelper.FromDbValue(row["辅助信息"]);
            cid.EventTpye = (string)OleDbHelper.FromDbValue(row["事件类型"]);
            cid.StrategiesName = (string)OleDbHelper.FromDbValue(row["处理策略"]);
            cid.IsSendMessage = (bool)OleDbHelper.FromDbValue(row["是否发送短信"]);
            return cid;
        }
        public void Insert(string tableName, CID cid)
        {
            OleDbHelper.ExecuteNonQuery("Main", @"INSERT INTO " + tableName +
                        @" (CID码,是否是新事件,事件类型,辅助信息,处理策略,是否发送短信) 
                 VALUES (@CIDCode,@IsNewEvent,@EventTpye,@EventInformation,@StrategiesName,@IsSendMessage)",
                new OleDbParameter("@CIDCode", OleDbHelper.ToDbValue(cid.CIDCode)),
                new OleDbParameter("@IsNewEvent", OleDbHelper.ToDbValue(cid.IsNewEvent)),
                new OleDbParameter("@EventTpye", OleDbHelper.ToDbValue(cid.EventTpye)),
                new OleDbParameter("@EventInformation", OleDbHelper.ToDbValue(cid.EventInformation)),
                new OleDbParameter("@StrategiesName", OleDbHelper.ToDbValue(cid.StrategiesName)),
                new OleDbParameter("@IsSendMessage", OleDbHelper.ToDbValue(cid.IsSendMessage)));
        }
        public void Update(string tableName, CID cid)
        {
            OleDbHelper.ExecuteNonQuery("Main", @"UPDATE  " + tableName +
                @" SET  [CID码]=@CIDCode
                    ,[是否是新事件]=@IsNewEvent
                    ,[事件类型]=@EventTpye
                    ,[辅助信息]=@EventInformation
                    ,[处理策略]=@StrategiesName
                    ,[是否发送短信]=@IsSendMessage
                WHERE [序号]=@ID",
                new OleDbParameter("@CIDCode", OleDbHelper.ToDbValue(cid.CIDCode)),
                new OleDbParameter("@IsNewEvent", OleDbHelper.ToDbValue(cid.IsNewEvent)),
                new OleDbParameter("@EventTpye", OleDbHelper.ToDbValue(cid.EventTpye)),
                new OleDbParameter("@EventInformation", OleDbHelper.ToDbValue(cid.EventInformation)),
                new OleDbParameter("@StrategiesName", OleDbHelper.ToDbValue(cid.StrategiesName)),
                new OleDbParameter("@IsSendMessage", OleDbHelper.ToDbValue(cid.IsSendMessage)),
                new OleDbParameter("@ID", OleDbHelper.ToDbValue(cid.ID)));
        }
        public void DeleteByID(string tableName, int ID)
        {
            OleDbHelper.ExecuteNonQuery("Main", "delete from  " + tableName +" where [序号]=@ID",
                new OleDbParameter("@ID", ID));
        }
    }
}
