#region << 版 本 注 释 >>
/*
 * ======================================================
 * Copyright(c) 2012-2015 Zeal Fung, All Rights Reserved.
 * ======================================================
 *  
 * 【当前类文件的功能】
 *  
 *  
 * 作者：[冯梓易]   时间：2014/1/20 星期一 上午 10:23:09
 * 文件名：ChineseDate
 * 版本：V1.0.0
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ======================================================
*/
#endregion
using System;
using System.Collections;
using System.Globalization;

namespace DesktopHelper.Util
{
    /// <summary>
    /// 中国农历
    /// </summary>
    public static class ChineseDate
    {
        private static ChineseLunisolarCalendar china = new ChineseLunisolarCalendar();
        private static Hashtable gHoliday = new Hashtable();
        private static Hashtable nHoliday = new Hashtable();

        private static string zhiStr = "子丑寅卯辰巳午未申酉戌亥";

        private static string[] JQ = {
                                         "小寒", "大寒", "立春", "雨水", "惊蛰", "春分", "清明", "谷雨", "立夏", "小满", "芒种", "夏至", "小暑", "大暑"
                                         , "立秋", "处暑", "白露", "秋分", "寒露", "霜降", "立冬", "小雪", "大雪", "冬至"
                                     };

        private static int[] JQData = {
                                          0, 21208, 42467, 63836, 85337, 107014, 128867, 150921, 173149, 195551, 218072,
                                          240693, 263343, 285989, 308563, 331033, 353350, 375494, 397447, 419210, 440795,
                                          462224, 483532, 504758
                                      };

        static ChineseDate()
        {
            //公历节日
            gHoliday.Add("0101", "元旦");
            gHoliday.Add("0202", "世界湿地日");
            gHoliday.Add("0210", "国际气象节");
            gHoliday.Add("0214", "情人节");
            gHoliday.Add("0301", "国际海豹日");
            gHoliday.Add("0305", "学雷锋纪念日");
            gHoliday.Add("0308", "妇女节");
            gHoliday.Add("0312", "植树节 孙中山逝世纪念日");
            gHoliday.Add("0314", "国际警察日");
            gHoliday.Add("0315", "消费者权益日");
            gHoliday.Add("0317", "中国国医节 国际航海日");
            gHoliday.Add("0321", "世界森林日 消除种族歧视国际日 世界儿歌日");
            gHoliday.Add("0322", "世界水日");
            gHoliday.Add("0324", "世界防治结核病日");
            gHoliday.Add("0401", "愚人节");
            gHoliday.Add("0407", "世界卫生日");
            gHoliday.Add("0422", "世界地球日");
            gHoliday.Add("0501", "劳动节");
            gHoliday.Add("0503", "世界新闻自由日");
            gHoliday.Add("0504", "青年节");
            gHoliday.Add("0508", "世界红十字日");
            gHoliday.Add("0512", "国际护士节");
            gHoliday.Add("0531", "世界无烟日");
            gHoliday.Add("0601", "国际儿童节");
            gHoliday.Add("0605", "世界环境保护日");
            gHoliday.Add("0626", "国际禁毒日");
            gHoliday.Add("0701", "建党节 香港回归纪念 世界建筑日");
            gHoliday.Add("0711", "世界人口日");
            gHoliday.Add("0801", "建军节");
            gHoliday.Add("0808", "中国男子节 父亲节");
            gHoliday.Add("0815", "抗日战争胜利纪念");
            gHoliday.Add("0909", "毛泽东逝世纪念");
            gHoliday.Add("0910", "教师节");
            gHoliday.Add("0918", "九•一八事变纪念日");
            gHoliday.Add("0920", "国际爱牙日");
            gHoliday.Add("0927", "世界旅游日");
            gHoliday.Add("0928", "孔子诞辰");
            gHoliday.Add("1001", "国庆节 国际音乐日国际老年人日");
            gHoliday.Add("1004", "世界动物日");
			gHoliday.Add("1009", "世界邮政日 万国邮联日");
			gHoliday.Add("1010", "世界视力日 辛亥革命纪念日 世界精神卫生日");
			gHoliday.Add("1013", "世界保健日");
			gHoliday.Add("1014", "世界标准日");
			gHoliday.Add("1015", "国际盲人节");
			gHoliday.Add("1016", "世界粮食日");
			gHoliday.Add("1017", "世界消除贫困日");
			gHoliday.Add("1022", "世界传统医药日");
            gHoliday.Add("1024", "联合国日");
            gHoliday.Add("1110", "世界青年节");
            gHoliday.Add("1112", "孙中山诞辰纪念");
            gHoliday.Add("1201", "世界艾滋病日");
            gHoliday.Add("1203", "世界残疾人日"); 
            gHoliday.Add("1220", "澳门回归纪念");
            gHoliday.Add("1224", "平安夜");
            gHoliday.Add("1225", "圣诞节");
            gHoliday.Add("1226", "毛泽东诞辰纪念");

            //农历节日
            nHoliday.Add("0101", "春节");
            nHoliday.Add("0115", "元宵节");
            nHoliday.Add("0505", "端午节");
            nHoliday.Add("0707", "七夕情人节");
            nHoliday.Add("0715", "中元节 盂兰盆节");
            nHoliday.Add("0815", "中秋节");
            nHoliday.Add("0909", "重阳节");
            nHoliday.Add("1208", "腊八节");
            nHoliday.Add("1223", "北方小年(扫房)");
            nHoliday.Add("1224", "南方小年(掸尘)");
        }

        /// <summary>
        /// 获取农历
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string GetChinaDate(DateTime dt)
        {
            if (dt > china.MaxSupportedDateTime || dt < china.MinSupportedDateTime)
            {
                //日期范围：1901 年 2 月 19 日 - 2101 年 1 月 28 日
                throw new Exception(string.Format("日期超出范围！必须在{0}到{1}之间！",
                                                  china.MinSupportedDateTime.ToString("yyyy-MM-dd"),
                                                  china.MaxSupportedDateTime.ToString("yyyy-MM-dd")));
            }
            string str = string.Format("{0} {1}{2}", GetYear(dt), GetMonth(dt), GetDay(dt));
            string strJQ = GetSolarTerm(dt);
            if (strJQ != "")
            {
                str += " (" + strJQ + ")";
            }
            string strHoliday = GetHoliday(dt);
            if (strHoliday != "")
            {
                str += " " + strHoliday;
            }
            string strChinaHoliday = GetChinaHoliday(dt);
            if (strChinaHoliday != "")
            {
                str += " " + strChinaHoliday;
            }

            return str;
        }

        /// <summary>
        /// 获取农历年份
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string GetYear(DateTime dt)
        {
            int yearIndex = china.GetSexagenaryYear(dt);
            string yearTG = " 甲乙丙丁戊己庚辛壬癸";
            string yearDZ = " 子丑寅卯辰巳午未申酉戌亥";
            string yearSX = " 鼠牛虎兔龙蛇马羊猴鸡狗猪";
            int year = china.GetYear(dt);
            int yTG = china.GetCelestialStem(yearIndex);
            int yDZ = china.GetTerrestrialBranch(yearIndex);

            string str = string.Format("[{1}]{2}{3}{0}", year, yearSX[yDZ], yearTG[yTG], yearDZ[yDZ]);
            return str;
        }

        /// <summary>
        /// 获取农历月份
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string GetMonth(DateTime dt)
        {
            int year = china.GetYear(dt);
            int iMonth = china.GetMonth(dt);
            int leapMonth = china.GetLeapMonth(year);
            bool isLeapMonth = iMonth == leapMonth;
            if (leapMonth != 0 && iMonth >= leapMonth)
            {
                iMonth--;
            }

            string szText = "正二三四五六七八九十";
            string strMonth = isLeapMonth ? "闰" : "";
            if (iMonth <= 10)
            {
                strMonth += szText.Substring(iMonth - 1, 1);
            }
            else if (iMonth == 11)
            {
                strMonth += "冬";
            }
            else
            {
                strMonth += "腊";
            }
            return strMonth + "月";
        }

        /// <summary>
        /// 获取农历日期
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string GetDay(DateTime dt)
        {
            int iDay = china.GetDayOfMonth(dt);
            string szText1 = "初十廿三";
            string szText2 = "一二三四五六七八九十";
            string strDay;
            if (iDay == 20)
            {
                strDay = "二十";
            }
            else if (iDay == 30)
            {
                strDay = "三十";
            }
            else
            {
                strDay = szText1.Substring((iDay - 1) / 10, 1);
                strDay = strDay + szText2.Substring((iDay - 1) % 10, 1);
            }
            return strDay;
        }

        /// <summary>
        /// 获取时辰
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string GetHour(DateTime dt)
        {
            int _hour, _minute, offset;
            _hour = dt.Hour;    //获得当前时间小时
            _minute = dt.Minute;  //获得当前时间分钟
            if (_minute != 0) _hour += 1;
            offset = _hour / 2;
            if (offset >= 12) offset = 0;
            return zhiStr[offset].ToString() + "时";
        }

        /// <summary>
        /// 获取节气
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string GetSolarTerm(DateTime dt)
        {
            DateTime dtBase = new DateTime(1900, 1, 6, 2, 5, 0);
            DateTime dtNew;
            double num;
            int y;
            string strReturn = "";

            y = dt.Year;
            for (int i = 1; i <= 24; i++)
            {
                num = 525948.76 * (y - 1900) + JQData[i - 1];
                dtNew = dtBase.AddMinutes(num);
                if (dtNew.DayOfYear == dt.DayOfYear)
                {
                    strReturn = JQ[i - 1];
                }
            }

            return strReturn;
        }

        /// <summary>
        /// 获取公历节日
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string GetHoliday(DateTime dt)
        {
            string strReturn = "";
            object g = gHoliday[dt.Month.ToString("00") + dt.Day.ToString("00")];
            if (g != null)
            {
                strReturn = g.ToString();
            }

            return strReturn;
        }

        /// <summary>
        /// 获取农历节日
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string GetChinaHoliday(DateTime dt)
        {
            string strReturn = "";
            int year = china.GetYear(dt);
            int iMonth = china.GetMonth(dt);
            int leapMonth = china.GetLeapMonth(year);
            int iDay = china.GetDayOfMonth(dt);
            if (china.GetDayOfYear(dt) == china.GetDaysInYear(year))
            {
                strReturn = "除夕";
            }
            else if (leapMonth != iMonth)
            {
                if (leapMonth != 0 && iMonth >= leapMonth)
                {
                    iMonth--;
                }
                object n = nHoliday[iMonth.ToString("00") + iDay.ToString("00")];
                if (n != null)
                {
                    if (strReturn == "")
                    {
                        strReturn = n.ToString();
                    }
                    else
                    {
                        strReturn += " " + n.ToString();
                    }
                }
            }

            return strReturn;
        }
    }
}
