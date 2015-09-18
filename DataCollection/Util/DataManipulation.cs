#region << 版 本 注 释 >>
/*
 * ======================================================
 * Copyright(c) 2012-2015 Zeal Fung, All Rights Reserved.
 * ======================================================
 *  
 * 【当前类文件的功能】
 *  
 *  
 * 作者：[冯梓易]   时间：2014/1/22 星期三 下午 01:06:34
 * 文件名：DataManipulation
 * 版本：V1.0.0
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ======================================================
*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataCollection.Entity;
using System.Data.SQLite;
using DataCollection.DB;
using DataCollection.Logger;

namespace DataCollection.Util
{
    class DataManipulation
    {
        public int AddRili(Rili rili)
        {
            int result = 0;
            try
            {
                string sql = "INSERT INTO t_rili(Yangli,Nongli,Zhigan,Xingzuo,Xingqi,Yi,Ji)values(@Yangli,@Nongli,@Zhigan,@Xingzuo,@Xingqi,@Yi,@Ji)";

                SQLiteParameter[] parameters = new SQLiteParameter[]{ 
                                         new SQLiteParameter("@Yangli",rili.Yangli), 
                                         new SQLiteParameter("@Nongli",rili.Nongli), 
                                         new SQLiteParameter("@Zhigan",rili.Zhigan),
                                         new SQLiteParameter("@Xingzuo",rili.Xingzuo),
                                         new SQLiteParameter("@Xingqi",rili.Xingqi),
                                         new SQLiteParameter("@Yi",rili.Yi),
                                         new SQLiteParameter("@Ji",rili.Ji)
                                         };
                SqlAction action = new SqlAction();
                result = action.IntQuery(sql, parameters);
            }
            catch (Exception ex)
            {
                Log.Error("插入日历数据出错：" + ex.ToString());
            }
            return result;
        }
    }
}
