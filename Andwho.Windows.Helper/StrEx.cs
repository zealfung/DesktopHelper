using System;
using System.Collections.Generic;
using System.Text;

namespace Andwho.Windows.Helper
{
    /// <summary>
    /// 对 字符串 的扩展操作
    /// </summary>
    public static class StrEx
    {
        #region 方法
        
        /// <summary>
        /// 获取字符串的字符长度
        /// </summary>
        /// <param name="str">需要获取字符长度的字符串</param>
        /// <returns>返回字符串的字符长度</returns>
        public static int GetStrLength(string str)
        {
            return Encoding.GetEncoding("gb2312").GetBytes(str).Length;
        }

        #region JoinArray 将字符串数组按指定符号连接成字符串
        
        /// <summary>
        /// 将字符串数组以逗号（，）分隔
        /// </summary>
        /// <param name="array">string[]</param>
        /// <returns>返回分隔后的字符串</returns>
        public static string JoinArray(object[] array)
        {
            return StrEx.JoinArray(array, ",");
        }

        /// <summary>
        /// 将字符串数据以指定的符号分隔
        /// </summary>
        /// <param name="array">string[]</param>
        /// <param name="split">分隔数组的符号</param>
        /// <returns>返回分隔后的字符串</returns>
        public static string JoinArray(object[] array, string split)
        {
            StringBuilder sb = new StringBuilder();
            if (array != null && array.Length > 1)
            {
                sb.Append(array[0]);
                for (int i = 1; i < array.Length; i++)
                {
                    sb.Append(split);
                    sb.Append(array[i]);
                }
            }
            return sb.ToString();
        }

        #endregion

        #endregion
    }
}
