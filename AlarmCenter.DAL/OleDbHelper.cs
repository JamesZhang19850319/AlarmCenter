using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.OleDb;

namespace AlarmCenter.DAL
{
    public static class OleDbHelper
    {
        public static string dbName;
        public static string connstr;
        private static  void DBNameSelect(string dbName)
        {
            if (dbName == "Main")
            {
                connstr = ConfigurationManager.ConnectionStrings["connstrMain"].ConnectionString;
            }
            if (dbName == "Event")
            {
                connstr = ConfigurationManager.ConnectionStrings["connstrEvent"].ConnectionString;
            }
        }

        public static int ExecuteNonQuery(string dbName,string sql,params OleDbParameter[] parameters)
        {
            DBNameSelect(dbName);
            using (OleDbConnection conn = new OleDbConnection(connstr))
            {
                conn.Open();
                using (OleDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public static void ExecuteNonQuery1(string dbName, string sql, params OleDbParameter[][] parameters)
        {
            DBNameSelect(dbName);
            using (OleDbConnection conn = new OleDbConnection(connstr))
            {
                conn.Open();
                using (OleDbCommand cmd = conn.CreateCommand())
                {
                    OleDbTransaction trans = null;
                    trans = cmd.Connection.BeginTransaction();
                    cmd.Transaction = trans;
                    cmd.CommandText = sql;

                    //插入n=parameters.Length个数据
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        cmd.Parameters.AddRange(parameters[i]);
                        cmd.ExecuteNonQuery();
                    }
                    trans.Commit();
                }
            }
        }
        public static object ExecuteScalar(string dbName, string sql, params OleDbParameter[] parameters)
        {
            DBNameSelect(dbName);
            using (OleDbConnection conn = new OleDbConnection(connstr))
            {
                conn.Open();
                using (OleDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteScalar();
                }
            }
        }
        public static DataTable ExecuteDataTable(string dbName, string sql, params OleDbParameter[] parameters)
        {
            DBNameSelect(dbName);
            using (OleDbConnection conn = new OleDbConnection(connstr))
            {
                conn.Open();
                using (OleDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(parameters);

                    DataSet dataset = new DataSet();
                    OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                    adapter.Fill(dataset);
                    return dataset.Tables[0];
                }
            }
        }

        public static DataSet ExecuteDataSet(string dbName, string sql, params OleDbParameter[] parameters)
        {
            DBNameSelect(dbName);
            using (OleDbConnection conn = new OleDbConnection(connstr))
            {
                conn.Open();
                using (OleDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(parameters);

                    DataSet dataset = new DataSet();
                    OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                    adapter.Fill(dataset);
                    return dataset;
                }
            }
        }

        public static object FromDbValue(object value)
        {
            if (value == DBNull.Value)
            {
                return null;
            }
            else
            {
                return value;
            }
        }

        public static object ToDbValue(object value)
        {
            if (value == null)
            {
                return DBNull.Value;
            }
            else
            {
                return value;
            }
        }
    }
}
                //OleDbConnection conn=....;
                //OleDbTransaction trans=null;
                //try
                //            {
                //                trans = conn.BeginTransaction();
                //                OleDbCommand cmd = conn.CreateCommand();
                //                cmd.Transaction = trans;

                //                //执行插入数据的SQL操作

                //                trans.Commit();
                //                cmd.Dispose();
                //                trans.Dispose();
                //            }
                //            catch(Exception e)
                //            {
                //            }
                //using (OleDbCommand cmd = new OleDbCommand("DoInsert", new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=d:\\MyTest.mdb;Persist Security Info=False;")))
                //{
                //    cmd.CommandType = CommandType.StoredProcedure;
                //    cmd.Connection.Open();
                //    OleDbTransaction trans = null;

                //    trans = cmd.Connection.BeginTransaction();
                //    cmd.Transaction = trans;
                //    cmd.Parameters.Add("@tname", OleDbType.VarWChar);
                //    //插入1000个数据
                //    for (int i = 1; i < 1001; i++)
                //    {
                //        cmd.Parameters["@tname"].Value = i;
                //        cmd.ExecuteNonQuery();
                //    }
                //    trans.Commit();
                //    cmd.Connection.Close();
                //    Console.WriteLine("OK");
                //}