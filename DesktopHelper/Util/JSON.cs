#region << 版 本 注 释 >>
/*
 * ======================================================
 * Copyright(c) 2012-2015 Zeal Fung, All Rights Reserved.
 * ======================================================
 *  
 * 【当前类文件的功能】
 *  
 *  
 * 作者：[冯梓易]   时间：2014/2/21 星期五 下午 04:21:24
 * 文件名：JSON
 * 版本：V1.0.0
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ======================================================
*/
#endregion
using System.Text;
using System.IO;
using System.Runtime.Serialization.Json;

namespace DesktopHelper.Util
{
    public static class JSON
    {
        /// <summary>
        /// 将Json字符串反序列化成对象
        /// </summary>
        /// <param name="jsonString">反序列化Json字符串</param>
        /// <returns></returns>
        public static T Json2Object<T>(string jsonString)
        {
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
            {
                return (T)new DataContractJsonSerializer(typeof(T)).ReadObject(ms);
            }
        }
    }
}
