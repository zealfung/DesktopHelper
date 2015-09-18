using System;
using System.Diagnostics;

namespace Andwho.Windows.Helper
{
    /// <summary>
    /// 将一个基本数据类型转换为另一个基本数据类型。
    /// </summary>
    public static class Convert
    {
        #region 常量
        /// <summary>
        /// 每毫米等于的英寸数值
        /// </summary>
        private const float MM_OF_INCH = 0.039370078740157f;

        #endregion

        #region 方法

        #region 对 英寸 的转换

        /// <summary>
        /// 将毫米转换为英寸
        /// </summary>
        /// <param name="mm">毫米</param>
        /// <returns></returns>
        public static float MmToInch(float mm)
        {
            float d = (float)(mm * Convert.MM_OF_INCH);
            return d * 100;
        }

        #endregion

        #region 对 Float 的转换

        /// <summary>
        /// 将 object 类型转换为 float
        /// </summary>
        /// <param name="obj">需要转换的数值</param>
        /// <remarks>如果转换失败，则返回 0</remarks>
        /// <returns></returns>
        public static float ToFloat(object obj)
        {
            float result = 0.0f;
            float.TryParse(obj.ToString(), out result);
            return result;
        }

        #endregion

        #region 对 Int 的转换

        /// <summary>
        /// 将 object 类型转换为 int
        /// </summary>
        /// <param name="obj">需要转换的数值</param>
        /// <remarks>如果转换失败，则返回 0</remarks>
        /// <returns></returns>
        public static int ToInt32(object obj)
        {
            int result = 0;
            Int32.TryParse(obj.ToString(), out result);
            return result;
        }

        /// <summary>
        /// 将 string 类型转换为 int
        /// </summary>
        /// <param name="str">需要转换的数值</param>
        /// <returns>如果转换失败，则返回 0</returns>
        public static int ToInt32(string str)
        {
            int result = -1;
            int.TryParse(str, out result);
            return result;
        }

        #endregion

        #region 将 数值 转换为中文大写字符串
        
        /// <summary>
        /// 转换数字金额主函数（包括小数）
        /// 数字字符串
        /// 转换成中文大写后的字符串或者出错信息提示字符串
        /// </summary>
        public static string ToChineseStr(string str)
        {
            string result = string.Empty;
            try
            {
                if (!Convert.IsDecimal(str))                // 判断是否为正整数
                    return result;
                if (Double.Parse(str) > double.MaxValue)    // 判断数值是否太大
                    return result;

                char sign = '.';                            //小数点
                string[] splitstr = str.Split(sign);        //按小数点分割字符串
                if (splitstr.Length == 1)                   //只有整数部分
                {
                    result = Convert.ToData(str) + "圆整";
                }
                else                                        //有小数部分
                {
                    result = Convert.ToData(splitstr[0]) + "圆";//转换整数部分
                    result += Convert.ToDecimalStr(splitstr[1]);//转换小数部分
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Convert.ToChineseStr(string) :: " + ex.Message);
                throw ex;
            }
            return result;
        }

        /// <summary>
        /// 判断是否是正数字字符串
        /// <remarks>如果是数字，返回true，否则返回false</remarks>
        /// </summary>
        private static bool IsDecimal(string str)
        {
            Decimal d = -1;
            Decimal.TryParse(str, out d);
            if (d < 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 转换数字（整数）
        /// </summary>
        private static string ToData(string str)
        {
            string rstr = "";
            try
            {
                string tmpstr = "";
                int strlen = str.Length;
                if (strlen <= 4)
                {
                    rstr = ToDigit(str);
                }
                else
                {

                    if (strlen <= 8)//数字长度大于四位，小于八位
                    {
                        tmpstr = str.Substring(strlen - 4, 4);//先截取最后四位数字
                        rstr = ToDigit(tmpstr);//转换最后四位数字
                        tmpstr = str.Substring(0, strlen - 4);//截取其余数字
                        //将两次转换的数字加上萬后相连接
                        rstr = String.Concat(ToDigit(tmpstr) + "萬", rstr);
                        rstr = rstr.Replace("零萬", "萬");
                        rstr = rstr.Replace("零零", "零");

                    }
                    else
                        if (strlen <= 12)//数字长度大于八位，小于十二位
                        {
                            tmpstr = str.Substring(strlen - 4, 4);//先截取最后四位数字
                            rstr = ToDigit(tmpstr);//转换最后四位数字
                            tmpstr = str.Substring(strlen - 8, 4);//再截取四位数字
                            rstr = String.Concat(ToDigit(tmpstr) + "萬", rstr);
                            tmpstr = str.Substring(0, strlen - 8);
                            rstr = String.Concat(ToDigit(tmpstr) + "億", rstr);
                            rstr = rstr.Replace("零億", "億");
                            rstr = rstr.Replace("零萬", "零");
                            rstr = rstr.Replace("零零", "零");
                            rstr = rstr.Replace("零零", "零");
                        }
                }
                strlen = rstr.Length;
                if (strlen >= 2)
                {
                    switch (rstr.Substring(strlen - 2, 2))
                    {
                        case "佰零": rstr = rstr.Substring(0, strlen - 2) + "佰"; break;
                        case "仟零": rstr = rstr.Substring(0, strlen - 2) + "仟"; break;
                        case "萬零": rstr = rstr.Substring(0, strlen - 2) + "萬"; break;
                        case "億零": rstr = rstr.Substring(0, strlen - 2) + "億"; break;

                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Convert.ToData(string) :: " + ex.Message);
                throw ex;
            }

            return rstr;
        }

        /// <summary>
        /// 转换数字（小数部分）
        /// </summary>
        private static string ToDecimalStr(string str)
        {
            string result = string.Empty;
            try
            {
                int strlen = str.Length;
                if (strlen == 1)
                {
                    result = Convert.To1Digit(str) + "角";
                }
                else
                {
                    string tmpstr = str.Substring(0, 1);
                    result = Convert.To1Digit(tmpstr) + "角";
                    tmpstr = str.Substring(1, 1);
                    result += Convert.To1Digit(tmpstr) + "分";
                    result = result.Replace("零分", "");
                    result = result.Replace("零角", "");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Convert.ToDecimalStr(string) :: " + ex.Message);
                throw ex;
            }
            return result;
        }

        /// <summary>
        /// 转换的字符串（四位以内）
        /// </summary>
        private static string ToDigit(string str)
        {   
            string result = string.Empty;
            try
            {
                switch (str.Length)
                {
                    case 1: result = To1Digit(str); break;
                    case 2: result = To2Digit(str); break;
                    case 3: result = To3Digit(str); break;
                    case 4: result = To4Digit(str); break;
                }
                result = result.Replace("拾零", "拾");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Convert.ToDigit(string) :: " + ex.Message);
                throw ex;
            }

            return result;
        }

        /// <summary>
        /// 转换四位数字
        /// </summary>
        private static string To4Digit(string str)
        {
            string result = string.Empty;
            try
            {
                string str1 = str.Substring(0, 1);
                string str2 = str.Substring(1, 1);
                string str3 = str.Substring(2, 1);
                string str4 = str.Substring(3, 1);

                result += Convert.To1Digit(str1) + "仟";
                result += Convert.To1Digit(str2) + "佰";
                result += Convert.To1Digit(str3) + "拾";
                result += Convert.To1Digit(str4);
                result = result.Replace("零仟", "零");
                result = result.Replace("零佰", "零");
                result = result.Replace("零拾", "零");
                result = result.Replace("零零", "零");
                result = result.Replace("零零", "零");
                result = result.Replace("零零", "零");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Convert.To4Digit(string) :: " + ex.Message);
                throw ex;
            }
            return result;
        }

        /// <summary>
        /// 转换三位数字
        /// </summary>
        private static string To3Digit(string str)
        {
            string result = string.Empty;
            try
            {
                string str1 = str.Substring(0, 1);
                string str2 = str.Substring(1, 1);
                string str3 = str.Substring(2, 1);
                result += Convert.To1Digit(str1) + "佰";
                result += Convert.To1Digit(str2) + "拾";
                result += Convert.To1Digit(str3);
                result = result.Replace("零佰", "零");
                result = result.Replace("零拾", "零");
                result = result.Replace("零零", "零");
                result = result.Replace("零零", "零");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Convert.To3Digit(string) :: " + ex.Message);
                throw ex;
            }
            return result;
        }

        /// <summary>
        /// 转换二位数字
        /// </summary>
        private static string To2Digit(string str)
        {
            string result = string.Empty;
            try
            {
                string str1 = str.Substring(0, 1);
                string str2 = str.Substring(1, 1);

                result += Convert.To1Digit(str1) + "拾";
                result += Convert.To1Digit(str2);
                result = result.Replace("零拾", "零");
                result = result.Replace("零零", "零");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Convert.To2Digit(string) :: " + ex.Message);
                throw ex;
            }
            return result;
        }

        /// <summary>
        /// 将一位数字转换成中文大写数字
        /// </summary>
        private static string To1Digit(string str)
        {
            try
            {
                //"零壹贰叁肆伍陆柒捌玖拾佰仟萬億圆整角分"
                switch (str)
                {
                    case "1": return "壹";
                    case "2": return "贰";
                    case "3": return "叁";
                    case "4": return "肆";
                    case "5": return "伍";
                    case "6": return "陆";
                    case "7": return "柒";
                    case "8": return "捌";
                    case "9": return "玖";
                    default: return "零";
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Convert.To1Digit(string) :: " + ex.Message);
                throw ex;
            }
        }

        #endregion

        #endregion
    }
}
