using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataCollection.Util;
using DataCollection.Logger;
using System.Threading;
using DataCollection.Entity;

namespace DataCollection
{
    class Program
    {
        static Thread collectionThread = new Thread(RefreshRealTimePrice);

        static void Main(string[] args)
        {
            //DateTime dt = new DateTime(2014, 10, 27);
            //ChineseDate cd = new ChineseDate(dt);
            //Console.WriteLine("阳历：" + cd.DateString);
            //Console.WriteLine("农历：" + cd.ChineseDateString);
            //Console.WriteLine("支干：" + cd.GanZhiDateString);
            //Console.WriteLine("星座：" + cd.Constellation);
            //Console.WriteLine("星期：" + cd.WeekDayStr);

            //collectionThread.IsBackground = true;
            //collectionThread.Start();

            //开始处理控制台命令
            while (true)
            {
                String cmd = Console.ReadLine();
                if (cmd != null && cmd.Equals("quit"))
                {
                    break;
                }
                //停止线程
                else if (cmd != null && cmd.StartsWith("stop"))
                {
                    collectionThread.Suspend();
                    Console.WriteLine("数据收集线程暂停工作");
                }
                //启动线程
                else if (cmd != null && cmd.StartsWith("start"))
                {
                    collectionThread.Resume();
                    Console.WriteLine("数据收集线程继续工作");
                }
                else
                {
                    Console.WriteLine("错误的命令！");
                }
            }
        }

        //获取网页内容
        static GetWebContent gwc = new GetWebContent();
        static DateTime beginDate = new DateTime(2020, 1, 1);
        static DateTime endDate = new DateTime(2030, 12, 31);
        static int days = (endDate - beginDate).Days;
        private static void RefreshRealTimePrice()
        {
            string url = string.Empty;
            Encoding encoding = Encoding.UTF8;
            int count = 0;
            Rili rili = new Rili();
            DataManipulation dm = new DataManipulation();
            while (count <= days)
            {
                try
                {
                    url = string.Format("http://laohuangli.51240.com/{0}__laohuangli/", beginDate.ToString("yyyy-M-d")); ;
                    string ori = gwc.GetWebContentByUrl(url, encoding);
                    string dropHTML = string.Empty;
                    if (ori.Contains("链接超时") || ori.Contains("链接异常"))
                    {
                        Log.Error("打开数据网站超时，日期：" + beginDate.ToString("yyyy-M-d"));
                    }
                    else
                    {
                        dropHTML = gwc.DropHTMLTag(ori);
                        int start = 0;
                        start = dropHTML.IndexOf("推荐工具");
                        dropHTML = dropHTML.Replace(dropHTML.Substring(start), "");
                        start = dropHTML.IndexOf("日期");
                        dropHTML = dropHTML.Substring(start);
                        start = dropHTML.IndexOf("document");
                        string content = dropHTML.Replace(dropHTML.Substring(start), "");
                        
                        //Log.Info(content);

                        start = content.IndexOf("节气");
                        string jieqi = content.Substring(start);
                        content = content.Replace(jieqi, "");
                        start = content.IndexOf("冲\n生肖冲");
                        content = content.Replace(content.Substring(start), "");
                        start = content.IndexOf("忌");
                        string ji = content.Substring(start);
                        content = content.Replace(ji, "");
                        ji = ji.Contains("\n") ? ji.Replace("忌\n", "") : "";
                        start = content.IndexOf("宜");
                        string yi = content.Substring(start);
                        content = content.Replace(yi, "");
                        yi = yi.Contains("\n") ? yi.Replace("宜\n", "") : "";
                        string riqi = content;


                        ChineseDate cd = new ChineseDate(beginDate);
                        rili = new Rili();
                        rili.Yangli = cd.DateString;
                        rili.Nongli = cd.ChineseDateString;
                        rili.Zhigan = cd.GanZhiDateString;
                        rili.Xingzuo = cd.Constellation;
                        rili.Xingqi = cd.WeekDayStr;
                        rili.Yi = string.IsNullOrEmpty(yi) ? "诸事不宜." : yi;
                        rili.Ji = string.IsNullOrEmpty(ji) ? "诸事不宜." : ji;

                        int i = dm.AddRili(rili);
                        if (i == 1)
                        {
                            Log.Info("日期数据入库成功：" + rili.ToString());
                        }
                        else
                        {
                            Log.Error("日期数据入库失败：" + rili.ToString());
                        }
                    }
                    beginDate = beginDate.AddDays(1);
                    count++;
                }
                catch (Exception ex)
                {                    
                    Log.Error("数据收集线程出错：" + ex.ToString());
                    collectionThread.Abort();
                }
            }

        }
    }
}
