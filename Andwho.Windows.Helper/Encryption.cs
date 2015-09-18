using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Reflection;
using System.Diagnostics;

namespace Andwho.Windows.Helper
{
    /// <summary>
    /// 加密
    /// </summary>
    public class Encryption
    {
        #region 方法
        
        /// <summary>
        /// Base64(MD5)
        /// </summary>
        /// <param name="pEncData"></param>
        /// <returns></returns>
        public static string GetSign(string pEncData)
        {
            string result = null;
            try
            {
                MD5 md = new MD5CryptoServiceProvider();
                byte[] bytes = Encoding.GetEncoding("utf-8").GetBytes(pEncData);
                byte[] inArray = md.ComputeHash(bytes);
                result = System.Convert.ToBase64String(inArray, 0, inArray.Length);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Encryption.GetSign(string) :: " + ex.Message);
                throw ex;
            }
            return result;
        }

        /// <summary>
        /// 加密 Base64(3DES(加密内容))
        /// </summary>
        /// <param name="content"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Encrypt3DES(string content, string key)
        {
            string result = string.Empty;
            try
            {
                TripleDESCryptoServiceProvider provider = new TripleDESCryptoServiceProvider();
                byte[] buffer3 = Encoding.GetEncoding("utf-8").GetBytes(content);
                provider.Key = Encoding.GetEncoding("utf-8").GetBytes(key);
                provider.Mode = CipherMode.ECB;
                MemoryStream stream = new MemoryStream();
                ICryptoTransform transform = provider.CreateEncryptor();
                CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Write);
                stream2.Write(buffer3, 0, buffer3.Length);
                stream2.FlushFinalBlock();
                result = System.Convert.ToBase64String(stream.ToArray());
                stream.Flush();
                stream.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Encryption.Encrypt3DES(string, string) :: " + ex.Message);
                throw ex;
            }

            return result;
        }

        /// <summary>
        /// 解密 Base64(3DES(加密内容))
        /// </summary>
        /// <param name="sourceData"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Decrypt3DES(string sourceData, string key)
        {
            string result = string.Empty;
            try
            {
                TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
                byte[] content = System.Convert.FromBase64String(sourceData);
                des.Key = Encoding.GetEncoding("utf-8").GetBytes(key);
                des.Mode = CipherMode.ECB;
                MemoryStream ms = new MemoryStream();
                ICryptoTransform transform = des.CreateDecryptor();
                CryptoStream cs = new CryptoStream(ms, transform, CryptoStreamMode.Write);
                cs.Write(content, 0, content.Length);
                cs.FlushFinalBlock();
                byte[] b = ms.ToArray();
                result = Encoding.GetEncoding("utf-8").GetString(b, 0, b.Length);

                ms.Flush();
                ms.Close();
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Encryption.Decrypt3DES(string, string) :: " + ex.Message);
                throw ex;
            }
            return result;
        }

        #endregion
    }
}
