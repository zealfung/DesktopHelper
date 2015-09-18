#region << 版 本 注 释 >>
/*
 * ======================================================
 * Copyright(c) 2012-2015 Zeal Fung, All Rights Reserved.
 * ======================================================
 *  
 * 【当前类文件的功能】
 *  
 *  
 * 作者：[冯梓易]   时间：2014/1/22 星期三 下午 01:00:39
 * 文件名：SqlAction
 * 版本：V1.0.0
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ======================================================
*/
#endregion
using System;
using System.Data;
using System.Data.SQLite;

namespace DataCollection.DB
{
    public class SqlAction
    {
        /// <summary>
        /// 执行查询SQL,返回查询结果表
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>查询结果表</returns>
        public DataTable DataTableQuery(string sql, SQLiteParameter[] parameters)
        {
            DataTable data = new DataTable();
            try
            {
                SQLiteDBHelper dbHelper = new SQLiteDBHelper();
                data = dbHelper.ExecuteDataTable(sql, parameters);
            }
            catch (Exception ex)
            {
                //Log.Error("DataTableQuery出错:" + ex.ToString());
            }
            return data;
        }
        /// <summary>
        /// 执行查询SQL,返回受影响行数
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public int IntQuery(string sql, SQLiteParameter[] parameters)
        {
            int result = 0;
            try
            {
                SQLiteDBHelper dbHelper = new SQLiteDBHelper();
                result = dbHelper.ExecuteNonQuery(sql, parameters);
            }
            catch (Exception exMsg)
            {
                //Log.Error("ExecuteNonQuery出错:" + exMsg.ToString());
            }
            return result;
        }
        /// <summary>
        /// 执行查询SQL,返回查询结果的首行首列
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public string StringQuery(string sql, SQLiteParameter[] parameters)
        {
            string result = string.Empty;
            try
            {
                SQLiteDBHelper dbHelper = new SQLiteDBHelper();
                DataTable data = dbHelper.ExecuteDataTable(sql, parameters);
                if (data != null && data.Rows.Count > 0)
                {
                    result = data.Rows[0][0].ToString();
                }

            }
            catch (Exception ex)
            {
                //Log.Error("DataTableQuery出错:" + ex.ToString());
            }
            return result;
        }
    }
}
