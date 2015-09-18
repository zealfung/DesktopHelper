#region << 版 本 注 释 >>
/*
 * ======================================================
 * Copyright(c) 2012-2015 Zeal Fung, All Rights Reserved.
 * ======================================================
 *  
 * 【当前类文件的功能】
 *  
 *  
 * 作者：[冯梓易]   时间：2014/2/19 星期三 上午 11:46:38
 * 文件名：WeatherinfoModel
 * 版本：V1.0.0
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ======================================================
*/
#endregion
using System;

namespace DesktopHelper.Entity
{
    /// <summary>
    /// 天气查询结果信息实体
    /// </summary>
    public class WeatherInfo
    {
        /// <summary>
        /// 城市名称
        /// </summary>
        public String city { get; set; }

        /// <summary>
        /// 城市英文名称==拼音
        /// </summary>
        public String city_en { get; set; }

        /// <summary>
        /// 今日时间【年-月-日】
        /// </summary>
        public String date_y { get; set; }

        /// <summary>
        /// 为空，无用
        /// </summary>
        public String date { get; set; }

        /// <summary>
        /// 星期几
        /// </summary>
        public String week { get; set; }

        /// <summary>
        /// 系统更新时间
        /// </summary>
        public String fchh { get; set; }

        /// <summary>
        /// 城市代码
        /// </summary>
        public String cityid { get; set; }

        /// <summary>
        /// 今天及之后五天的摄氏温度
        /// </summary>
        public String temp1 { get; set; }
        public String temp2 { get; set; }
        public String temp3 { get; set; }
        public String temp4 { get; set; }
        public String temp5 { get; set; }
        public String temp6 { get; set; }

        /// <summary>
        /// 今天及之后五天的华氏温度
        /// </summary>
        public String tempF1 { get; set; }
        public String tempF2 { get; set; }
        public String tempF3 { get; set; }
        public String tempF4 { get; set; }
        public String tempF5 { get; set; }
        public String tempF6 { get; set; }

        /// <summary>
        /// 今天及之后五天的天气描述
        /// </summary>
        public String weather1 { get; set; }
        public String weather2 { get; set; }
        public String weather3 { get; set; }
        public String weather4 { get; set; }
        public String weather5 { get; set; }
        public String weather6 { get; set; }

        /// <summary>
        /// 天气描述图片序号
        /// </summary>
        public String img1 { get; set; }
        public String img2 { get; set; }
        public String img3 { get; set; }
        public String img4 { get; set; }
        public String img5 { get; set; }
        public String img6 { get; set; }
        public String img7 { get; set; }
        public String img8 { get; set; }
        public String img9 { get; set; }
        public String img10 { get; set; }
        public String img11 { get; set; }
        public String img12 { get; set; }
        public String img_single { get; set; }

        /// <summary>
        /// 图片名称
        /// </summary>
        public String img_title1 { get; set; }
        public String img_title2 { get; set; }
        public String img_title3 { get; set; }
        public String img_title4 { get; set; }
        public String img_title5 { get; set; }
        public String img_title6 { get; set; }
        public String img_title7 { get; set; }
        public String img_title8 { get; set; }
        public String img_title9 { get; set; }
        public String img_title10 { get; set; }
        public String img_title11 { get; set; }
        public String img_title12 { get; set; }
        public String img_title_single { get; set; }

        /// <summary>
        /// 今天及之后五天的风速描述
        /// </summary>
        public String wind1 { get; set; }
        public String wind2 { get; set; }
        public String wind3 { get; set; }
        public String wind4 { get; set; }
        public String wind5 { get; set; }
        public String wind6 { get; set; }

        /// <summary>
        /// 风速级别描述
        /// </summary>
        public String fx1 { get; set; }
        public String fx2 { get; set; }
        public String fl1 { get; set; }
        public String fl2 { get; set; }
        public String fl3 { get; set; }
        public String fl5 { get; set; }
        public String fl6 { get; set; }

        /// <summary>
        /// 今天穿衣指数
        /// </summary>
        public String index { get; set; }
        public String index_d { get; set; }

        /// <summary>
        /// 48小时穿衣指数
        /// </summary>
        public String index48 { get; set; }
        public String index48_d { get; set; }

        /// <summary>
        /// 紫外线及48小时紫外线
        /// </summary>
        public String index_uv { get; set; }
        public String index48_uv { get; set; }

        /// <summary>
        /// 洗车
        /// </summary>
        public String index_xc { get; set; }

        /// <summary>
        /// 旅游
        /// </summary>
        public String index_tr { get; set; }

        /// <summary>
        /// 舒适指数
        /// </summary>
        public String index_co { get; set; }

        /// <summary>
        /// ？？
        /// </summary>
        public String st1 { get; set; }
        public String st2 { get; set; }
        public String st3 { get; set; }
        public String st4 { get; set; }
        public String st5 { get; set; }
        public String st6 { get; set; }

        /// <summary>
        /// 晨练
        /// </summary>
        public String index_cl { get; set; }

        /// <summary>
        /// 晾晒
        /// </summary>
        public String index_ls { get; set; }

        /// <summary>
        /// 过敏
        /// </summary>
        public String index_ag { get; set; }
    }
}
