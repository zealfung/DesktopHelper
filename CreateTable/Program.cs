using System;
using System.Text;
using System.Data.SQLite;
using System.Security.Cryptography;

namespace CreateTable
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("启动...");
                
                //开始处理控制台命令
                while (true)
                {
                    //DropTable();
                    //CreateTable();
                    //DeleteData();
                    //InertData();
                    //UpdateData();
                    //AddColumns();
                    //ShowData();
                    //ChangePassword();
                    //ShowPassWord();
                    String cmd = Console.ReadLine();
                    if (cmd != null && cmd.Equals("quit"))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("错误的命令！");
                    }
                }
            }
            catch (Exception e)
            {
            }
        }

        static void DropTable()
        { 
            try
            {
                string dbPath = "E:\\Andwho.db";
                //如果不存在
                if (!System.IO.File.Exists(dbPath))
                {
                    return ;
                }

                SqlLiteDBHelper db = new SqlLiteDBHelper(dbPath);
                string sql = "DROP TABLE t_expenditure ";
                db.ExecuteNonQuery(sql, null);
                Console.WriteLine("删表成功");
            }
            catch (Exception e)
            {
                Console.WriteLine("删表失败：" + e.ToString());
            }
        }

        static void CreateTable()
        {
            try
            {
                string dbPath = "E:\\Andwho.db";
                //如果不存在改数据库文件，则创建该数据库文件 
                if (!System.IO.File.Exists(dbPath))
                {
                    SqlLiteDBHelper.CreateDB(dbPath);
                }

                SqlLiteDBHelper db = new SqlLiteDBHelper(dbPath);
                string sql = @"CREATE TABLE t_notepad(
                                                   Title   varchar(30)  NOT NULL,
                                                   CreateTime       datetime NOT NULL,
                                                   UpdateTime    datetime NOT NULL,
                                                   Content      TEXT)";

                db.ExecuteNonQuery(sql, null);
                Console.WriteLine("建表成功");
            }
            catch (Exception e)
            {
                Console.WriteLine("建表失败：" + e.ToString());
            }
        }

        static void InertData()
        {
            try
            {
                string dbPath = @"E:\Andwho.db";
                string sql = "INSERT INTO t_config(Name,Value)values(@Name,@Value)";
                SqlLiteDBHelper db = new SqlLiteDBHelper(dbPath);
                //string password = MD5EncryptDES("123");

                SQLiteParameter[] parameters = new SQLiteParameter[]{
                    new SQLiteParameter("@Name","CityCode"),
                    new SQLiteParameter("@Value","101010100")
                                         };
                db.ExecuteNonQuery(sql, parameters);


                Console.WriteLine("插入数据成功");
            }
            catch (Exception e)
            {
                Console.WriteLine("插入数据失败：" + e.ToString());
            }
        }
        static void DeleteData()
        {

            try
            {
                string dbPath = "E:\\data.db";


                string sql = "delete from t_income";

                //SQLiteParameter[] parameters = new SQLiteParameter[]{ 
                //                         new SQLiteParameter("@UserCode",usercode)
                //                         };
                SqlLiteDBHelper db = new SqlLiteDBHelper(dbPath);
                int ret = db.ExecuteNonQuery(sql, null);
                if (ret > 0)
                {
                    Console.WriteLine("删除数据成功");
                }
                else
                {
                    Console.WriteLine("删除数据失败");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("删除数据失败：" + ex.ToString());
            }
        }
        static void UpdateData()
        {
            try
            {
                string dbPath = "E:\\data.db";

                string sql = "update t_set set SetValue=@SetValue  where SetName=@SetName";

                SQLiteParameter[] parameters = new SQLiteParameter[]{ 
                  new SQLiteParameter("@SetName","UseDate"),
                                         new SQLiteParameter("@SetValue","")
                                         };
                SqlLiteDBHelper db = new SqlLiteDBHelper(dbPath);
                int ret = db.ExecuteNonQuery(sql, parameters);
                if (ret > 0)
                {
                    Console.WriteLine("更新数据成功");
                }
                else
                {
                    Console.WriteLine("更新数据失败");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("更新数据失败：" + ex.ToString());
            }
        }
        static void ChangePassword()
        {
            try
            {
                string dbPath = "E:\\data.db";

                SqlLiteDBHelper db = new SqlLiteDBHelper(dbPath);
                bool ret = db.ChangePassword("2014Andwho0107");
                if (ret)
                {
                    Console.WriteLine("修改数据库密码成功");
                }
                else
                {
                    Console.WriteLine("修改数据库密码失败");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("修改数据库密码失败：" + ex.ToString());
            }
        }
        static void AddColumns()
        {
            try
            {
                string dbPath = "E:\\data.db";

                string sql = "alter table t_income add column BarberId varchar(30)";
                SqlLiteDBHelper db = new SqlLiteDBHelper(dbPath);

                db.ExecuteNonQuery(sql, null);


                Console.WriteLine("添加字段成功");
            }
            catch (Exception e)
            {

                Console.WriteLine("添加字段失败：" + e.ToString());
            }
        }
        static void ShowData()
        {
            try
            {
                string dbPath = "E:\\data.db";
                //查询从50条起的20条记录 
                string sql = "select * from t_user";
                SqlLiteDBHelper db = new SqlLiteDBHelper(dbPath);
                using (SQLiteDataReader reader = db.ExecuteReader(sql, null))
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("ID:{0},TypeName{1}", reader.GetString(0), reader.GetString(1));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ShowData失败：" + ex.ToString());
            }
        }
        static void ShowPassWord()
        {
            string dbPath = "E:\\data.db";
            //查询从50条起的20条记录 
            string sql = "select PassWord  from t_user where UserCode = @UserCode";
            SQLiteParameter[] parameters = new SQLiteParameter[]{ 
                                         new SQLiteParameter("@UserCode","zealfung0")
                                         };
            SqlLiteDBHelper db = new SqlLiteDBHelper(dbPath);
            using (SQLiteDataReader reader = db.ExecuteReader(sql, parameters))
            {
                while (reader.Read())
                {
                    Console.WriteLine("PSWD:{0}", reader.GetString(0));
                }
            }
        }

        static string MD5EncryptDES(string encryptString)
        {
            try
            {
                string originalStr = encryptString;
                string resultStr = "";
                MD5 md5 = MD5.Create();
                byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(originalStr));
                for (int i = 0; i < s.Length; i++)
                {
                    resultStr += s[i].ToString("X");
                }
                return resultStr;
            }
            catch
            {
                return encryptString;
            }
        }

    }
}
