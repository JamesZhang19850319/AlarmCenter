using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using AlarmCenter.DAL;
using System.Data.OleDb;
using AlarmCenter.DAL;

namespace AlarmCenter.DAL
{
    public class ReceiverDAL
    {
        public int pageIndex = 1;
        public const int pageSize = 10;

        //根据ID获取GetByID、Update、DeleteByID、GetAll、GetPagedData（分页数据）
        //Insert（插入新数据）
        public int pageCount{get;set;}
        public Receiver[] ListAll()
        {
            DataTable dt = GetReceiversDataTable("Main", "串口设置", pageSize, pageIndex);
            Receiver[] receivers = new Receiver[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                receivers[i] = ToReceiver(dt.Rows[i]);
            }
            return receivers;
        }

        public bool SerialPortNumIsUsed(string serialPortNum, int ID)
        {
            bool serialPortNumIsUsed = false;
            DataTable dt = GetReceiversDataTable("Main", "串口设置", pageSize, pageIndex);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if ((dt.Rows[i]["串口序号"].ToString() == serialPortNum) && (dt.Rows[i]["序号"].ToString() != Convert.ToString(ID)))
                {
                    serialPortNumIsUsed = true;
                }
            }
            return serialPortNumIsUsed;
        }

        private Receiver ToReceiver(DataRow row)
        {
            Receiver receiver = new Receiver();
            receiver.ID = (int)OleDbHelper.FromDbValue(row["序号"]);
            receiver.ReceiverName = (string)OleDbHelper.FromDbValue(row["接警机名"]);
            receiver.SerialPortNum = (string)OleDbHelper.FromDbValue(row["串口序号"]);
            receiver.ReceiverType = (string)OleDbHelper.FromDbValue(row["接警机类型"]);
            receiver.Version = (string)OleDbHelper.FromDbValue(row["版本号"]);
            receiver.BaudRate = (int)OleDbHelper.FromDbValue(row["波特率"]);
            receiver.DataBits = (int)OleDbHelper.FromDbValue(row["数据位"]);
            receiver.StopBits = (int)OleDbHelper.FromDbValue(row["停止位"]);
            receiver.FlowControl = (string)OleDbHelper.FromDbValue(row["流控制"]);
            receiver.Parity = (string)OleDbHelper.FromDbValue(row["校验"]);
            receiver.EndCode = (int)OleDbHelper.FromDbValue(row["结束代码"]);
            receiver.ACK = (int)OleDbHelper.FromDbValue(row["握手代码"]);
            receiver.CheckTimer = (int)OleDbHelper.FromDbValue(row["连接检查间隔"]);
            receiver.IsCheck = (bool)OleDbHelper.FromDbValue(row["是否检查连接"]);
            receiver.Mark = (string)OleDbHelper.FromDbValue(row["备注"]);
            return receiver;
        }

        public Receiver GetByID(int id)
        {
            DataTable dt = OleDbHelper.ExecuteDataTable("Main", "select * from [串口设置] where [序号]=@ID",
                new OleDbParameter("@ID", id));
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
                return ToReceiver(row);
            }
        }

        public void DeleteByID(object id)
        {
            OleDbHelper.ExecuteNonQuery("Main", "delete from [串口设置] where [序号]=@ID",
                new OleDbParameter("@ID", id));
        }

        public void Insert(Receiver receiver)
        {
            OleDbHelper.ExecuteNonQuery("Main", @"INSERT INTO [串口设置] 
                        ([串口序号],[接警机名],[接警机类型],[版本号],[波特率],[数据位],[停止位],[流控制],[校验],[结束代码],[握手代码],[连接检查间隔],[是否检查连接],[备注]) 
                 VALUES (@SerialPortNum,@ReceiverName,@ReceiverType,@Version,@BaudRate,@DataBits,@StopBits,@FlowControl,@Parity,@EndCode,@ACK,@CheckTimer,@IsCheck,@Mark)",
                new OleDbParameter("@SerialPortNum", OleDbHelper.ToDbValue(receiver.SerialPortNum)),
                new OleDbParameter("@ReceiverName", OleDbHelper.ToDbValue(receiver.ReceiverName)),
                new OleDbParameter("@ReceiverType", OleDbHelper.ToDbValue(receiver.ReceiverType)),
                new OleDbParameter("@Version", OleDbHelper.ToDbValue(receiver.Version)),   
                new OleDbParameter("@BaudRate", OleDbHelper.ToDbValue(receiver.BaudRate)),
                new OleDbParameter("@DataBits", OleDbHelper.ToDbValue(receiver.DataBits)),
                new OleDbParameter("@StopBits", OleDbHelper.ToDbValue(receiver.StopBits)),
                new OleDbParameter("@FlowControl", OleDbHelper.ToDbValue(receiver.FlowControl)),
                new OleDbParameter("@Parity", OleDbHelper.ToDbValue(receiver.Parity)),
                new OleDbParameter("@EndCode", OleDbHelper.ToDbValue(receiver.EndCode)),
                new OleDbParameter("@ACK", OleDbHelper.ToDbValue(receiver.ACK)),
                new OleDbParameter("@CheckTimer", OleDbHelper.ToDbValue(receiver.CheckTimer)),
                new OleDbParameter("@IsCheck", OleDbHelper.ToDbValue(receiver.IsCheck)),
                new OleDbParameter("@Mark", OleDbHelper.ToDbValue(receiver.Mark)));
        }

        public void Update(Receiver receiver)
        {
            OleDbHelper.ExecuteNonQuery("Main", @"UPDATE [串口设置]
                SET  [接警机名]=@ReceiverName
                    ,[接警机类型]=@ReceiverType
                    ,[版本号]=@Version
                    ,[波特率]=@BaudRate
                    ,[数据位]=@DataBits
                    ,[停止位]=@StopBits
                    ,[流控制]=@FlowControl
                    ,[校验]=@Parity
                    ,[结束代码]=@EndCode
                    ,[握手代码]=@ACK
                    ,[连接检查间隔]=@CheckTimer
                    ,[是否检查连接]=@IsCheck 
                    ,[串口序号]=@SerialPortNum
                    ,[备注]=@Mark
                WHERE [序号]=@ID",
                new OleDbParameter("@ReceiverName", OleDbHelper.ToDbValue(receiver.ReceiverName)),
                new OleDbParameter("@ReceiverType", OleDbHelper.ToDbValue(receiver.ReceiverType)),
                new OleDbParameter("@Version", OleDbHelper.ToDbValue(receiver.Version)),
                new OleDbParameter("@BaudRate", OleDbHelper.ToDbValue(receiver.BaudRate)),
                new OleDbParameter("@DataBits", OleDbHelper.ToDbValue(receiver.DataBits)),
                new OleDbParameter("@StopBits", OleDbHelper.ToDbValue(receiver.StopBits)),
                new OleDbParameter("@FlowControl", OleDbHelper.ToDbValue(receiver.FlowControl)),
                new OleDbParameter("@Parity", OleDbHelper.ToDbValue(receiver.Parity)),
                new OleDbParameter("@EndCode", OleDbHelper.ToDbValue(receiver.EndCode)),
                new OleDbParameter("@ACK", OleDbHelper.ToDbValue(receiver.ACK)),
                new OleDbParameter("@CheckTimer", OleDbHelper.ToDbValue(receiver.CheckTimer)),
                new OleDbParameter("@IsCheck", OleDbHelper.ToDbValue(receiver.IsCheck)),
                new OleDbParameter("@SerialPortNum", OleDbHelper.ToDbValue(receiver.SerialPortNum)),
                new OleDbParameter("@Mark", OleDbHelper.ToDbValue(receiver.Mark)),
                new OleDbParameter("@ID", OleDbHelper.ToDbValue(receiver.ID)));
        }
        public DataTable GetReceiversDataTable(string DBName, string tableName, int pageSize, int pageIndex)
        {
            DataPageDAL dataPage = new DataPageDAL();
            dataPage.PageSize = pageSize;
            dataPage.TableName = "串口设置";
            dataPage.PageIndex = pageIndex;
            dataPage.QueryFieldName = " 序号,串口序号,接警机名,接警机类型,版本号,波特率,数据位,停止位,流控制,校验,结束代码,握手代码,连接检查间隔,是否检查连接,备注 ";
            dataPage.OrderStr = "[序号]";
            dataPage.PrimaryKey = "[序号]";
            DataTable table = dataPage.QueryDataTable(DBName);
            pageCount = dataPage.PageCount;
            return table;

        }


        public Receiver[] GetReceivers(int pageSize,int pageIndex,string tableName)
        {
            DataPageDAL dataPage = new DataPageDAL();
            dataPage.PageSize = pageSize;
            dataPage.TableName = "串口设置";
            dataPage.PageIndex = pageIndex;
            dataPage.QueryFieldName = " * ";
            dataPage.OrderStr = "[序号]";
            dataPage.PrimaryKey = "[序号]";
            DataTable table = dataPage.QueryDataTable("Main");
            pageCount=dataPage.PageCount;

            Receiver[] receivers = new Receiver[table.Rows.Count];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataRow row = table.Rows[i];

                receivers[i] = ToReceiver(row);
            }
            return receivers;
            
        }
    }

}
