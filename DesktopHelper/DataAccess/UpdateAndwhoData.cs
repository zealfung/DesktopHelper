#region << 版 本 注 释 >>
/*
 * ======================================================
 * Copyright(c) 2012-2015 Zeal Fung, All Rights Reserved.
 * ======================================================
 *  
 * 【当前类文件的功能】
 *  
 *  
 * 作者：[冯梓易]   时间：2014/2/15 星期六 下午 07:52:55
 * 文件名：UpdateAndwhoData
 * 版本：V1.0.0
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ======================================================
*/
#endregion
using System.Data;
using DesktopHelper.DB;
using System.Data.SQLite;
using System.Windows.Forms;
using Andwho.Logger;
using System;

namespace DesktopHelper.DataAccess
{
    public class UpdateAndwhoData
    {
        string dbPath = Application.StartupPath + @"\Andwho.db";
        Log log = new Log(true);

        /// <summary>
        /// 获取当前"网址设置表"数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetWebsiteConfig()
        {
            DataTable data = new DataTable();
            try
            {
                string sqlString = @"select *  from t_website_config order by WebOrder";
                SqlAction action = new SqlAction();
                data = action.DataTableQuery(sqlString, null);
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
            return data;
        }

        /// <summary>
        /// 将当前"网址设置表"数据插入新表
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int InsertWebsiteConfig2NewAndwho(DataTable data)
        {
            int result = 0;
            try
            {
                string sql = "delete from t_website_config";
                UpdateDBHelper db = new UpdateDBHelper(dbPath);
                db.ExecuteNonQuery(sql, null);

                sql = "INSERT INTO t_website_config(WebOrder,Name,URL)values(@WebOrder,@Name,@URL)";
                SQLiteParameter[] parameters = null;
                foreach (DataRow dr in data.Rows)
                {
                    parameters = new SQLiteParameter[]{
                    new SQLiteParameter("@WebOrder",dr["WebOrder"]),
                    new SQLiteParameter("@Name",dr["Name"]),
                    new SQLiteParameter("@URL",dr["URL"])
                                         };
                    result += db.ExecuteNonQuery(sql, parameters);
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
            return result;
        }

        /// <summary>
        /// 获取当前"设置表"数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetConfig()
        {
            DataTable data = new DataTable();
            try
            {
                string sqlString = @"select *  from t_config";
                SqlAction action = new SqlAction();
                data = action.DataTableQuery(sqlString, null);
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
            return data;
        }

        /// <summary>
        /// 将当前"设置表"数据插入新表
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int InsertConfig2NewConfig(DataTable data)
        {
            int result = 0;
            try
            {
                string sql = "delete from t_config";
                UpdateDBHelper db = new UpdateDBHelper(dbPath);
                db.ExecuteNonQuery(sql, null);

                sql = "INSERT INTO t_config(Name,Value)values(@Name,@Value)";
                SQLiteParameter[] parameters = null;
                foreach (DataRow dr in data.Rows)
                {
                    parameters = new SQLiteParameter[]{
                    new SQLiteParameter("@Name",dr["Name"]),
                    new SQLiteParameter("@Value",dr["Value"])
                                         };
                    result += db.ExecuteNonQuery(sql, parameters);
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
            return result;
        }

        /// <summary>
        /// 获取当前"提醒表"数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetTixing()
        {
            DataTable data = new DataTable();
            try
            {
                string sqlString = @"select *  from t_tixing";
                SqlAction action = new SqlAction();
                data = action.DataTableQuery(sqlString, null);
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
            return data;
        }

        /// <summary>
        /// 将当前"提醒表"数据插入新表
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int InsertTixing2NewTixing(DataTable data)
        {
            int result = 0;
            try
            {
                string sql = "delete from t_tixing";
                UpdateDBHelper db = new UpdateDBHelper(dbPath);
                db.ExecuteNonQuery(sql, null);

                sql = "INSERT INTO t_tixing(Id,Time,Info)values(@Id,@Time,@Info)";
                SQLiteParameter[] parameters = null;
                foreach (DataRow dr in data.Rows)
                {
                    parameters = new SQLiteParameter[]{
                    new SQLiteParameter("@Id",dr["Id"]),
                    new SQLiteParameter("@Time",dr["Time"]),
                    new SQLiteParameter("@Info",dr["Info"])
                                         };
                    result += db.ExecuteNonQuery(sql, parameters);
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
            return result;
        }

        /// <summary>
        /// 获取当前“定时表”数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetDingshi()
        {
            DataTable data = new DataTable();
            try
            {
                string sqlString = @"select *  from t_dingshi";
                SqlAction action = new SqlAction();
                data = action.DataTableQuery(sqlString, null);
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
            return data;
        }

        /// <summary>
        /// 将当前"定时表"数据插入新表
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int InsertDingshi2NewDingshi(DataTable data)
        {
            int result = 0;
            try
            {
                string sql = "delete from t_dingshi";
                UpdateDBHelper db = new UpdateDBHelper(dbPath);
                db.ExecuteNonQuery(sql, null);

                sql = "INSERT INTO t_dingshi(Id,Frequency,Time,ExecEvents,FilePath)values(@Id,@Frequency,@Time,@ExecEvents,@FilePath)";
                SQLiteParameter[] parameters = null;
                foreach (DataRow dr in data.Rows)
                {
                    parameters = new SQLiteParameter[]{
                    new SQLiteParameter("@Id",dr["Id"]),
                    new SQLiteParameter("@Frequency",dr["Frequency"]),
                    new SQLiteParameter("@Time",dr["Time"]),
                    new SQLiteParameter("@ExecEvents",dr["ExecEvents"]),
                    new SQLiteParameter("@FilePath",dr["FilePath"])
                                         };
                    result += db.ExecuteNonQuery(sql, parameters);
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
            return result;
        }
    }
}
