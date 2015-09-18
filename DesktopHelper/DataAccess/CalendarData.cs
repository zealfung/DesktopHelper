#region << 版 本 注 释 >>
/*
 * ======================================================
 * Copyright(c) 2012-2015 Zeal Fung, All Rights Reserved.
 * ======================================================
 *  
 * 【当前类文件的功能】
 *  
 *  
 * 作者：[冯梓易]   时间：2014/1/22 星期三 下午 04:04:53
 * 文件名：MainData
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
using DesktopHelper.DB;
using Andwho.Entity;
using DesktopHelper.Entity;

namespace DesktopHelper.DataAccess
{
    class CalendarData
    {
        public DataTable GetRiliDataByMonth(int year,int month)
        {
            DataTable data = new DataTable();
            try
            {
                string sqlString = @"select *  from t_rili where Yangli like '" + string.Format("{0}年{1}月",year,month) + "%' order by Yangli";
                SqlAction action = new SqlAction();
                data = action.DataTableQuery(sqlString, null);
            }
            catch
            {
            }
            return data;
        }

        public DataTable GetConfig()
        {
            DataTable data = new DataTable();
            try
            {
                string sqlString = @"select *  from t_config";
                SqlAction action = new SqlAction();
                data = action.DataTableQuery(sqlString, null);
            }
            catch
            {
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

        public DataTable GetReminders(DateTime now)
        {
            DataTable data = new DataTable();
            try
            {
                string sqlString = @"select *  from t_tixing where Time >= @Time order by Id";
                SQLiteParameter[] parameters = new SQLiteParameter[]{ 
                                         new SQLiteParameter("@Time",now.ToString("yyyy-MM-dd HH:mm:ss"))
                                         };
                SqlAction action = new SqlAction();
                data = action.DataTableQuery(sqlString, parameters);
            }
            catch
            {
            }
            return data;
        }

        public int AddReminder(Reminder reminder)
        {
            int result = 0;
            try
            {
                string sql = "INSERT INTO t_tixing(Id,Time,Info)values(@Id,@Time,@Info)";

                SQLiteParameter[] parameters = new SQLiteParameter[]{ 
                                         new SQLiteParameter("@Id",reminder.ReminderId), 
                                         new SQLiteParameter("@Time",reminder.ReminderTime.ToString("yyyy-MM-dd HH:mm:ss")), 
                                         new SQLiteParameter("@Info",reminder.ReminderInfo)
                                         };
                SqlAction action = new SqlAction();
                result = action.IntQuery(sql, parameters);
            }
            catch
            {
            }
            return result;
        }

        public int DeleteReminderExpired(DateTime now)
        {
            int result = 0;
            try
            {
                string sql = "delete from t_tixing where Time < @Time";

                SQLiteParameter[] parameters = new SQLiteParameter[]{ 
                                         new SQLiteParameter("@Time",now.ToString("yyyy-MM-dd HH:mm:ss"))
                                         };
                SqlAction action = new SqlAction();
                result = action.IntQuery(sql, parameters);
            }
            catch
            {
            }
            return result;
        }

        public int DeleteReminderById(int id)
        {
            int result = 0;
            try
            {
                string sql = "delete from t_tixing where Id = @Id";

                SQLiteParameter[] parameters = new SQLiteParameter[]{ 
                                         new SQLiteParameter("@Id",id)
                                         };
                SqlAction action = new SqlAction();
                result = action.IntQuery(sql, parameters);
            }
            catch
            {
            }
            return result;
        }

        public DataTable GetTimedEvents()
        {
            DataTable data = new DataTable();
            try
            {
                string sqlString = @"select *  from t_dingshi";

                SqlAction action = new SqlAction();
                data = action.DataTableQuery(sqlString, null);
            }
            catch
            {
            }
            return data;
        }

        public int AddTimedEvent(TimedEvent timerEvent)
        {
            int result = 0;
            try
            {
                string sql = "INSERT INTO t_dingshi(Id,Frequency,Time,ExecEvents,FilePath)values(@Id,@Frequency,@Time,@ExecEvents,@FilePath)";

                SQLiteParameter[] parameters = new SQLiteParameter[]{ 
                                         new SQLiteParameter("@Id",timerEvent.Id), 
                                         new SQLiteParameter("@Frequency",timerEvent.Frequency), 
                                         new SQLiteParameter("@Time",timerEvent.Time.ToString("yyyy-MM-dd HH:mm:ss")),
                                         new SQLiteParameter("@ExecEvents",timerEvent.ExecEvents),
                                         new SQLiteParameter("@FilePath",timerEvent.FilePath)
                                         };
                SqlAction action = new SqlAction();
                result = action.IntQuery(sql, parameters);
            }
            catch
            {
            }
            return result;
        }

        public int DeleteTimedEvent(int id)
        {
            int result = 0;
            try
            {
                string sql = string.Empty;
                SQLiteParameter[] parameters = null;

                if (-1 == id)
                {
                    sql = "delete from t_dingshi where Frequency='仅一次' and Time < @Time";
                    parameters = new SQLiteParameter[]{ 
                                         new SQLiteParameter("@Time",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                                         };
                }
                else
                {
                    sql = "delete from t_dingshi where Id = @Id";
                    parameters = new SQLiteParameter[]{ 
                                         new SQLiteParameter("@Id",id)
                                         };
                }
                SqlAction action = new SqlAction();
                result = action.IntQuery(sql, parameters);
            }
            catch
            {
            }
            return result;
        }

        public DataTable GetNotepad()
        {
            DataTable data = new DataTable();
            try
            {
                string sqlString = @"select CreateTime,Title  from t_notepad order by UpdateTime";

                SqlAction action = new SqlAction();
                data = action.DataTableQuery(sqlString, null);
            }
            catch (Exception)
            {                
                throw;
            }
            return data;
        }

        public DataTable GetNotepadByCreateTime(string createTime)
        {
            DataTable data = new DataTable();
            try
            {
                string sqlString = @"select CreateTime,Title,Content from t_notepad  where CreateTime=@CreateTime";
                SQLiteParameter[] parameters = new SQLiteParameter[]{ 
                                         new SQLiteParameter("@CreateTime",createTime)
                                         };
                SqlAction action = new SqlAction();
                data = action.DataTableQuery(sqlString, parameters);
            }
            catch (Exception)
            {
                throw;
            }
            return data;
        }

        public int AddNotepad(string title, string content)
        {
            int result = 0;
            try
            {
                string sql = "INSERT INTO t_notepad(Title,CreateTime,UpdateTime,Content)values(@Title,@CreateTime,@UpdateTime,@Content)";

                SQLiteParameter[] parameters = new SQLiteParameter[]{ 
                                         new SQLiteParameter("@Title",title), 
                                         new SQLiteParameter("@CreateTime",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")), 
                                         new SQLiteParameter("@UpdateTime",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                                         new SQLiteParameter("@Content",content)
                                         };
                SqlAction action = new SqlAction();
                result = action.IntQuery(sql, parameters);
            }
            catch (Exception)
            {                
                throw;
            }
            return result;
        }

        public int UpdateNotepad(string createTime, string title, string content)
        {
            int result = 0;
            try
            {
                string sql = "update t_notepad set Title=@Title,UpdateTime=@UpdateTime,Content=@Content where CreateTime=@CreateTime";

                SQLiteParameter[] parameters = new SQLiteParameter[]{ 
                                         new SQLiteParameter("@Title",title), 
                                         new SQLiteParameter("@CreateTime",createTime), 
                                         new SQLiteParameter("@UpdateTime",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                                         new SQLiteParameter("@Content",content)
                                         };
                SqlAction action = new SqlAction();
                result = action.IntQuery(sql, parameters);
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public int DeleteNotepad(string createTime)
        {
            int result = 0;
            try
            {
                string sql = "delete from t_notepad where CreateTime=@CreateTime";
                SQLiteParameter[] parameters = new SQLiteParameter[]{ 
                                         new SQLiteParameter("@CreateTime",createTime)
                                         };
                SqlAction action = new SqlAction();
                result = action.IntQuery(sql, parameters);
            }
            catch (Exception)
            {
                
                throw;
            }
            return result;
        }
    }
}
