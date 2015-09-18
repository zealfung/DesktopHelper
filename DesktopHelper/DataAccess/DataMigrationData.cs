#region << 版 本 注 释 >>
/*
 * ======================================================
 * Copyright(c) 2012-2015 Zeal Fung, All Rights Reserved.
 * ======================================================
 *  
 * 【当前类文件的功能】
 *  
 *  
 * 作者：[冯梓易]   时间：2014/2/1 星期六 下午 03:55:20
 * 文件名：DataMigrationData
 * 版本：V1.0.0
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ======================================================
*/
#endregion
using System;
using DesktopHelper.Entity;
using DesktopHelper.DB;
using System.Data.SQLite;
using Andwho.Logger;

namespace DesktopHelper.DataAccess
{
    public class DataMigrationData
    {
        Log log = new Log(true);

        public int ClearWebsiteData()
        {
            int result = 0;
            try
            {
                string sql = "delete from t_website";
                SqlAction action = new SqlAction();
                result = action.IntQuery(sql, null);
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
            return result;
        }

        public int InsertWebsiteData(Website website)
        {
            int result = 0;
            try
            {
                string sql = "INSERT INTO t_website(WebOrder,Type,Name,URL)values(@WebOrder,@Type,@Name,@URL)";

                SQLiteParameter[] parameters = new SQLiteParameter[]{ 
                                         new SQLiteParameter("@WebOrder",website.Index), 
                                         new SQLiteParameter("@Type",website.Type), 
                                         new SQLiteParameter("@Name",website.Name),
                                         new SQLiteParameter("@URL",website.Url)
                                         };
                SqlAction action = new SqlAction();
                result = action.IntQuery(sql, parameters);
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
            return result;
        }

        public int ClearCityData()
        {
            int result = 0;
            try
            {
                string sql = "delete from t_city";
                SqlAction action = new SqlAction();
                result = action.IntQuery(sql, null);
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
            return result;
        }

        public int InsertCityData(City city)
        {
            int result = 0;
            try
            {
                string sql = "INSERT INTO t_city(Name,ShengParent,ShiParent,Code)values(@Name,@ShengParent,@ShiParent,@Code)";

                SQLiteParameter[] parameters = new SQLiteParameter[]{ 
                                         new SQLiteParameter("@Name",city.Name), 
                                         new SQLiteParameter("@ShengParent",city.ShengParent), 
                                         new SQLiteParameter("@ShiParent",city.ShiParent),
                                         new SQLiteParameter("@Code",city.Code)
                                         };
                SqlAction action = new SqlAction();
                result = action.IntQuery(sql, parameters);
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
            return result;
        }
    }
}
