#region << 版 本 注 释 >>
/*
 * ======================================================
 * Copyright(c) 2012-2015 Zeal Fung, All Rights Reserved.
 * ======================================================
 *  
 * 【当前类文件的功能】
 *  
 *  
 * 作者：[冯梓易]   时间：2014/2/1 星期六 下午 10:20:05
 * 文件名：ConfigData
 * 版本：V1.0.0
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ======================================================
*/
#endregion
using System;
using System.Data;
using DesktopHelper.DB;
using System.Data.SQLite;
using Andwho.Logger;
using DesktopHelper.Entity;

namespace DesktopHelper.DataAccess
{
    public class ConfigData
    {
        Log log = new Log(true);

        public DataTable GetConfig()
        {
            DataTable data = new DataTable();
            try
            {
                string sqlString = @"select *  from t_config";
                SqlAction action = new SqlAction();
                data = action.DataTableQuery(sqlString, null);
            }
            catch(Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
            return data;
        }

        public int UpdateConfig(string name, string value)
        {
            int result = 0;
            try
            {
                string sql = "update t_config set Value=@Value where  Name=@Name";

                SQLiteParameter[] parameters = new SQLiteParameter[]{ 
                                         new SQLiteParameter("@Name",name), 
                                         new SQLiteParameter("@Value",value)
                                         };
                SqlAction action = new SqlAction();
                result = action.IntQuery(sql, parameters);
            }
            catch
            {
            }
            return result;
        }

        public DataTable GetConfigWebsite()
        {
            DataTable data = new DataTable();
            try
            {
                string sqlString = @"select *  from t_website_config order by WebOrder";
                SqlAction action = new SqlAction();
                data = action.DataTableQuery(sqlString, null);
            }
            catch
            {
            }
            return data;
        }

        public int ClearConfigWebsite()
        {
            int result = 0;
            try
            {
                string sqlString = @"delete from t_website_config";
                SqlAction action = new SqlAction();
                result = action.IntQuery(sqlString, null);
            }
            catch
            {
            }
            return result;
        }

        public int AddConfigWebsite(string webOrder, string name, string url)
        {
            int result = 0;
            try
            {
                string sql = "INSERT INTO t_website_config(WebOrder,Name,URL)values(@WebOrder,@Name,@URL)";

                SQLiteParameter[] parameters = new SQLiteParameter[]{ 
                                         new SQLiteParameter("@WebOrder",webOrder), 
                                         new SQLiteParameter("@Name",name), 
                                         new SQLiteParameter("@URL",url)
                                         };
                SqlAction action = new SqlAction();
                result = action.IntQuery(sql, parameters);
            }
            catch
            {
            }
            return result;
        }

        public enum CityType
        {
            Sheng=0,
            Shi=1,
            Xian=2
        }

        public DataTable GetCityData(CityType type, string parent)
        {
            DataTable data = new DataTable();
            try
            {
                string sqlString = string.Empty;
                SQLiteParameter[] parameters = null;
                City city = new City();
                switch (type)
                {
                    case CityType.Sheng:
                        sqlString = "select Name  from t_city where ShengParent IS NULL and ShiParent IS NULL and Code IS NULL";
                        break;
                    case CityType.Shi:
                        sqlString = "select Name  from t_city where ShengParent=@ShengParent and ShiParent IS NULL and Code IS NULL";
                        parameters = new SQLiteParameter[]{ 
                                         new SQLiteParameter("@ShengParent",parent)
                                         };
                        break;
                    case CityType.Xian:
                        sqlString = "select Name,Code  from t_city where ShiParent=@ShiParent";
                        parameters = new SQLiteParameter[]{ 
                                         new SQLiteParameter("@ShiParent",parent)
                                         };
                        break;
                }
                SqlAction action = new SqlAction();
                data = action.DataTableQuery(sqlString, parameters);
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
            return data;
        }

        public DataTable GetCityInfoByCode(string code)
        {
            DataTable data = new DataTable();
            try
            {
                string sqlString = string.Format("select *  from t_city where Code='{0}'", code);

                SqlAction action = new SqlAction();
                data = action.DataTableQuery(sqlString, null);
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
            return data;
        }
    }
}
