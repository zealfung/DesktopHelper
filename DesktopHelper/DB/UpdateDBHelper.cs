#region << 版 本 注 释 >>
/*
 * ======================================================
 * Copyright(c) 2012-2015 Zeal Fung, All Rights Reserved.
 * ======================================================
 *  
 * 【当前类文件的功能】
 *  
 *  
 * 作者：[冯梓易]   时间：2014/2/15 星期六 下午 07:45:59
 * 文件名：UpdateDBHelper
 * 版本：V1.0.0
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ======================================================
*/
#endregion
using System;
using System.Data.SQLite;
using System.Data.Common;
using System.Data;

namespace DesktopHelper.DB
{
    public class UpdateDBHelper
    {
        private string connectionString = string.Empty;
        /// <summary> 
        /// 构造函数 
        /// </summary> 
        /// <param name="dbPath">SQLite数据库文件路径</param> 
        public UpdateDBHelper(string dbPath)
        {
            this.connectionString = "Data Source=" + dbPath + ";Password=2014Andwho0107";
        }

        /// <summary> 
        /// 对SQLite数据库执行增删改操作，返回受影响的行数。 
        /// </summary> 
        /// <param name="sql">要执行的增删改的SQL语句</param> 
        /// <param name="parameters">执行增删改语句所需要的参数，参数必须以它们在SQL语句中的顺序为准</param> 
        /// <returns></returns> 
        public int ExecuteNonQuery(string sql, SQLiteParameter[] parameters)
        {
            int affectedRows = 0;
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    using (DbTransaction transaction = connection.BeginTransaction())
                    {
                        using (SQLiteCommand command = new SQLiteCommand(connection))
                        {
                            command.CommandText = sql;
                            if (parameters != null)
                            {
                                command.Parameters.AddRange(parameters);
                            }
                            affectedRows = command.ExecuteNonQuery();
                        }
                        transaction.Commit();
                    }
                }
            }
            catch
            { }
            return affectedRows;
        }
        /// <summary> 
        /// 执行一个查询语句，返回一个包含查询结果的DataTable 
        /// </summary> 
        /// <param name="sql">要执行的查询语句</param> 
        /// <param name="parameters">执行SQL查询语句所需要的参数，参数必须以它们在SQL语句中的顺序为准</param> 
        /// <returns></returns> 
        public DataTable ExecuteDataTable(string sql, SQLiteParameter[] parameters)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
                    DataTable data = new DataTable();
                    adapter.Fill(data);
                    return data;
                }
            }

        }
    }
}
