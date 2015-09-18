#region << 版 本 注 释 >>
/*
 * ======================================================
 * Copyright(c) 2012-2015 Zeal Fung, All Rights Reserved.
 * ======================================================
 *  
 * 【当前类文件的功能】
 *  
 *  
 * 作者：[冯梓易]   时间：2014/1/22 星期三 下午 01:01:10
 * 文件名：SQLiteDBHelper
 * 版本：V1.0.0
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ======================================================
*/
#endregion
using System;
using System.Data.SQLite;
using System.Data;
using System.Data.Common;

namespace DataCollection.DB
{
    public class SQLiteDBHelper
    {
        private string connectionString = @"Data Source=E:\Andwho.db;Password=2014Andwho0107";
        private SQLiteConnection connection = null;

        /// <summary> 
        /// 构造函数 
        /// </summary> 
        public SQLiteDBHelper()
        {
            Init();
        }
        /// <summary> 
        /// 构造函数 
        /// </summary> 
        /// <param name="dbPath">SQLite数据库文件路径</param> 
        public SQLiteDBHelper(string dbPath)
        {
            this.connectionString = dbPath;
            Init();
        }

        private void Init()
        {
            connection = new SQLiteConnection(connectionString);
        }

        /// <summary>
        /// 打开连接
        /// </summary>
        private void OpenConnection()
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
        }
        /// <summary>
        /// 关闭连接
        /// </summary>
        private void CloseConnection()
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }

        }
        /// <summary> 
        /// 创建SQLite数据库文件 
        /// </summary> 
        public void CreateDB()
        {
            try
            {
                OpenConnection();
                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText = "CREATE TABLE Demo(id integer NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE)";
                    command.ExecuteNonQuery();
                }
            }
            finally
            {
                CloseConnection();
            }

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
                OpenConnection();
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
            finally
            {
                CloseConnection();
            }
            return affectedRows;
        }
        /// <summary> 
        /// 执行一个查询语句，返回一个关联的SQLiteDataReader实例 
        /// </summary> 
        /// <param name="sql">要执行的查询语句</param> 
        /// <param name="parameters">执行SQL查询语句所需要的参数，参数必须以它们在SQL语句中的顺序为准</param> 
        /// <returns></returns> 
        public SQLiteDataReader ExecuteReader(string sql, SQLiteParameter[] parameters)
        {
            SQLiteDataReader reader = null;
            try
            {
                OpenConnection();
                SQLiteCommand command = new SQLiteCommand(sql, connection);
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            }
            finally
            {
                CloseConnection();
            }
            return reader;
        }
        /// <summary> 
        /// 执行一个查询语句，返回一个包含查询结果的DataTable 
        /// </summary> 
        /// <param name="sql">要执行的查询语句</param> 
        /// <param name="parameters">执行SQL查询语句所需要的参数，参数必须以它们在SQL语句中的顺序为准</param> 
        /// <returns></returns> 
        public DataTable ExecuteDataTable(string sql, SQLiteParameter[] parameters)
        {
            try
            {
                OpenConnection();
                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
                    DataTable data = new DataTable();
                    data.TableName = "datatable";
                    adapter.Fill(data);
                    return data;
                }
            }
            finally
            {
                CloseConnection();
            }

        }
        /// <summary> 
        /// 执行一个查询语句，返回查询结果的第一行第一列 
        /// </summary> 
        /// <param name="sql">要执行的查询语句</param> 
        /// <param name="parameters">执行SQL查询语句所需要的参数，参数必须以它们在SQL语句中的顺序为准</param> 
        /// <returns></returns> 
        public Object ExecuteScalar(string sql, SQLiteParameter[] parameters)
        {
            try
            {
                OpenConnection();
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
            finally
            {
                CloseConnection();
            }
        }
        /// <summary> 
        /// 查询数据库中的所有数据类型信息 
        /// </summary> 
        /// <returns></returns> 
        public DataTable GetSchema()
        {
            try
            {
                OpenConnection();
                DataTable data = connection.GetSchema("TABLES");
                return data;
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}
