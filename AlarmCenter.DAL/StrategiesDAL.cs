using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace AlarmCenter.DAL
{
   public class StrategiesDAL
    {
        public string strategiesNum { get; set; }

        /// <summary>
        /// 获取处理策略资料
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public DataTable GetStrategiesDataTable(string account)
        {
            DataTable dt = OleDbHelper.ExecuteDataTable("Main", "select * from 处理策略");
            return dt;
        }

        /// <summary>
        /// 获取策略对象数组
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public Strategies[] GetStrategiesArray()
        {
            DataTable dt = OleDbHelper.ExecuteDataTable("Main", "select * from 处理策略 ");
            Strategies[] strategiess = new Strategies[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = dt.Rows[i];

                strategiess[i] = ToStrategies(row);
            }
            return strategiess;
        }

        public void DeleteByID(int ID)
        {
            OleDbHelper.ExecuteNonQuery("Main", "delete from 处理策略 where 序号=@ID",
                new OleDbParameter("@ID", ID));
        }

        public Strategies GetStrategiesByStrategiesName(string strategiesID)
        {
            DataTable dt = OleDbHelper.ExecuteDataTable("Main", "select * from 处理策略 where 策略名称=@strategiesID",
                new OleDbParameter("@strategiesID", strategiesID));
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
                return ToStrategies(row);
            }
        }
        private Strategies ToStrategies(DataRow row)
        {
            Strategies strategies = new Strategies();
            strategies.ID = (int)row["序号"];
            strategies.StrategiesID = (string)OleDbHelper.FromDbValue(row["策略编号"]);
            strategies.StrategiesName = (string)OleDbHelper.FromDbValue(row["策略名称"]);
            strategies.EventFontColor = (string)OleDbHelper.FromDbValue(row["事件字体颜色"]);
            strategies.EventBackgroundColor = (string)OleDbHelper.FromDbValue(row["事件背景颜色"]);
            strategies.NoticeType = (string)OleDbHelper.FromDbValue(row["提示类型"]);
            strategies.IsPrint = (bool)OleDbHelper.FromDbValue(row["自动打印"]);
            strategies.SoundFile = (string)OleDbHelper.FromDbValue(row["报警声文件"]);
            return strategies;
        }

        public void Insert(Strategies strategies)
        {
            OleDbHelper.ExecuteNonQuery("Main", @"INSERT INTO 处理策略 
                        (策略编号,策略名称,事件字体颜色,事件背景颜色,提示类型,自动打印,报警声文件) 
                 VALUES (@StrategiesID,@StrategiesName,@EventFontColor,@EventBackgroundColor,@NoticeType,@IsPrint,@SoundFile)",
                new OleDbParameter("@StrategiesID", OleDbHelper.ToDbValue(strategies.StrategiesID)),
                new OleDbParameter("@StrategiesName", OleDbHelper.ToDbValue(strategies.StrategiesName)),
                new OleDbParameter("@EventFontColor", OleDbHelper.ToDbValue(strategies.EventFontColor)),
                new OleDbParameter("@EventBackgroundColor", OleDbHelper.ToDbValue(strategies.EventBackgroundColor)),
                new OleDbParameter("@NoticeType", OleDbHelper.ToDbValue(strategies.NoticeType)),
                new OleDbParameter("@IsPrint", OleDbHelper.ToDbValue(strategies.IsPrint)),
                new OleDbParameter("@SoundFile", OleDbHelper.ToDbValue(strategies.SoundFile)));
        }
        public void Update(Strategies strategies)
        {
            OleDbHelper.ExecuteNonQuery("Main", @"UPDATE 处理策略
                SET  [策略编号]=@StrategiesID
                    ,[策略名称]=@StrategiesName
                    ,[事件字体颜色]=@EventFontColor
                    ,[事件背景颜色]=@EventBackgroundColor
                    ,[提示类型]=@NoticeType
                    ,[自动打印]=@IsPrint
                    ,[报警声文件]=@SoundFile
                WHERE [序号]=@ID",
                new OleDbParameter("@StrategiesID", OleDbHelper.ToDbValue(strategies.StrategiesID)),
                new OleDbParameter("@StrategiesName", OleDbHelper.ToDbValue(strategies.StrategiesName)),
                new OleDbParameter("@EventFontColor", OleDbHelper.ToDbValue(strategies.EventFontColor)),
                new OleDbParameter("@EventBackgroundColor", OleDbHelper.ToDbValue(strategies.EventBackgroundColor)),
                new OleDbParameter("@NoticeType", OleDbHelper.ToDbValue(strategies.NoticeType)),
                new OleDbParameter("@IsPrint", OleDbHelper.ToDbValue(strategies.IsPrint)),
                new OleDbParameter("@SoundFile", OleDbHelper.ToDbValue(strategies.SoundFile)),
                new OleDbParameter("@ID", OleDbHelper.ToDbValue(strategies.ID)));
        }

        /// <summary>
        /// 根据策略序号字段获取该该处理策略
        /// </summary>
        /// <param name="strategiesID"></param>
        /// <returns></returns>
        public Strategies GetStrategiesByID(int strategiesID)
        {
            DataTable dt = OleDbHelper.ExecuteDataTable("Main", "select * from 处理策略 where 序号=@strategiesID",
                new OleDbParameter("@strategiesID", strategiesID));
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
                return ToStrategies(row);
            }
        }
    }
}
