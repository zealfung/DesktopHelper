#region << 版 本 注 释 >>
/*
 * ======================================================
 * Copyright(c) 2012-2015 Zeal Fung, All Rights Reserved.
 * ======================================================
 *  
 * 【当前类文件的功能】
 *  
 *  
 * 作者：[冯梓易]   时间：2014/2/15 星期六 下午 10:30:59
 * 文件名：UnZipClass
 * 版本：V1.0.0
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ======================================================
*/
#endregion
using System;
using System.IO;

namespace DesktopHelper.Util
{
    public class UnZipClass
    {
        #region 解压zip格式的文件
        /// <summary>
        /// 功能：解压zip格式的文件。
        /// </summary>
        /// <param name="zipFilePath">压缩文件路径</param>
        /// <param name="unZipDir">解压文件存放路径,为空时默认与压缩文件同一级目录下，跟压缩文件同名的文件夹</param>
        /// <param name="err">出错信息</param>
        /// <returns>解压是否成功</returns>
        public bool UnZipFile(string zipFilePath, string unZipDir, out string err)
        {
            err = "";
            if (zipFilePath.Length == 0)
            {
                err = "压缩文件不能为空！";
                return false;
            }
            else if (!zipFilePath.EndsWith(".zip"))
            {
                err = "文件格式不正确！";
                return false;
            }
            else if (!File.Exists(zipFilePath))
            {
                err = "压缩文件不存在！";
                return false;
            }
            //解压文件夹为空时默认与压缩文件同一级目录下，跟压缩文件同名的文件夹
            if (unZipDir.Length == 0)
                unZipDir = zipFilePath.Replace(Path.GetFileName(zipFilePath), Path.GetFileNameWithoutExtension(zipFilePath));
            if (!unZipDir.EndsWith("\\"))
                unZipDir += "\\";
            if (!Directory.Exists(unZipDir))
                Directory.CreateDirectory(unZipDir);
            try
            {
                Shell32.ShellClass sc = new Shell32.ShellClass();
                Shell32.Folder SrcFolder = sc.NameSpace(zipFilePath);
                Shell32.Folder DestFolder = sc.NameSpace(unZipDir);
                Shell32.FolderItems items = SrcFolder.Items();
                DestFolder.CopyHere(items, 20);
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return false;
            }
            return true;
        }//解压结束
        #endregion
    }
}
