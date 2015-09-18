#region << 版 本 注 释 >>
/*
 * ======================================================
 * Copyright(c) 2012-2015 Zeal Fung, All Rights Reserved.
 * ======================================================
 *  
 * 【当前类文件的功能】
 *  
 *  
 * 作者：[冯梓易]   时间：2014/1/21 星期二 下午 03:07:24
 * 文件名：NavigationData
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

namespace DesktopHelper.DataAccess
{
    public class NavigationData
    {
        /// <summary>
        /// 获取固定网址数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetFixedWebsite()
        {
            DataTable data = new DataTable();
            try
            {
                string sqlString = @"select *  from t_website order by WebOrder";
                SqlAction action = new SqlAction();
                data = action.DataTableQuery(sqlString, null);
            }
            catch
            {
            }
            return data;
        }

        /// <summary>
        /// 获取用户配置网址数据
        /// </summary>
        /// <returns></returns>
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

        public string GetDefaultSearch()
        {
            string result = string.Empty;
            try
            {
                string sqlString = @"select Value  from t_config where Name = 'DefaultSearch'";
                SqlAction action = new SqlAction();
                result = action.StringQuery(sqlString, null);
            }
            catch
            {
            }
            return result;
        }
    }
}
