using AlarmCenter.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace AlarmCenter.DAL
{
    class DataPageDAL
    {
        private int _PageSize = 10;
        private int _PageIndex = 1;
        private int _PageCount = 0;
        private int _TotalCount = 0;
        private string _TableName;//表名
        private string _QueryFieldName = "*";//表字段FieldStr
        private string _OrderStr = string.Empty; //排序_SortStr
        private string _QueryCondition = string.Empty;//查询的条件 RowFilter
        private string _PrimaryKey = string.Empty;//主键
        private bool _isQueryTotalCounts = true;//是否查询总的记录条数
        /// <summary>
        /// 是否查询总的记录条数
        /// </summary>
        public bool IsQueryTotalCounts
        {
            get { return _isQueryTotalCounts; }
            set { _isQueryTotalCounts = value; }
        }
        /// <summary>
        /// 显示页数
        /// </summary>
        public int PageSize
        {
            get
            {
                return _PageSize;

            }
            set
            {
                _PageSize = value;
            }
        }
        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex
        {
            get
            {
                return _PageIndex;
            }
            set
            {
                _PageIndex = value;
            }
        }
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount
        {
            get
            {
                return _PageCount;
            }
        }
        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalCount
        {
            get
            {
                return _TotalCount;
            }
        }
        /// <summary>
        /// 表名，包括视图
        /// </summary>
        public string TableName
        {
            get
            {
                return _TableName;
            }
            set
            {
                _TableName = value;
            }
        }
        /// <summary>
        /// 表字段FieldStr
        /// </summary>
        public string QueryFieldName
        {
            get
            {
                return _QueryFieldName;
            }
            set
            {
                _QueryFieldName = value;
            }
        }
        /// <summary>
        /// 排序字段
        /// </summary>
        public string OrderStr
        {
            get
            {
                return _OrderStr;
            }
            set
            {
                _OrderStr = value;
            }
        }
        /// <summary>
        /// 查询条件
        /// </summary>
        public string QueryCondition
        {
            get
            {
                return _QueryCondition;
            }
            set
            {
                _QueryCondition = value;
            }
        }
        /// <summary>
        /// 主键
        /// </summary>
        public string PrimaryKey
        {
            get
            {
                return _PrimaryKey;
            }
            set
            {
                _PrimaryKey = value;
            }
        }
        public DataTable QueryDataTable(string DBName)
        {
            string strsql;
            if (_isQueryTotalCounts)
            {
                _TotalCount = GetTotalCount(DBName);
            }
            if (_TotalCount == 0)
            {
                _PageIndex = 1;
                _PageCount = 1;
                strsql = "select * from " + TableName;
                return OleDbHelper.ExecuteDataTable(DBName, strsql);
            }
            else
            {
                _PageCount = _TotalCount % _PageSize == 0 ? _TotalCount / _PageSize : _TotalCount / _PageSize + 1;
                if (_PageIndex > _PageCount)
                {
                    _PageIndex = _PageCount;

                }
            }
            //利用MAX函数这种方法在大数据量测试中表现出较高的效率，稳定性最优。
            //在查询第1页时，SQL语句的构成公式不适用，因为 "SELECT TOP 0 ..." 会报错，需要单独处理： SELECT TOP [PageNum] * FROM [TableName]可以使用。
                strsql = "select top " + PageSize + QueryFieldName + " from " + TableName + " where " + PrimaryKey + " < ((select max(" + PrimaryKey + ") from " + TableName + ") - ("
                    + (PageIndex-1 ) * PageSize + "-1)) order by " + PrimaryKey + " desc ";

            return OleDbHelper.ExecuteDataTable(DBName, strsql);
        }

        public int GetTotalCount(string DBName)
        {
            string strSql = " select count(" + PrimaryKey + ") from " + _TableName;
            if (_QueryCondition != string.Empty)
            {
                strSql += " where 1=1" + _QueryCondition;
            }
            return (int)OleDbHelper.ExecuteScalar(DBName, strSql);
        }
    }
}

