#region << 版 本 注 释 >>
/*
 * ======================================================
 * Copyright(c) 2012-2015 Zeal Fung, All Rights Reserved.
 * ======================================================
 *  
 * 【当前类文件的功能】
 *  
 *  
 * 作者：[冯梓易]   时间：2014/3/11 星期二 下午 02:35:50
 * 文件名：Class1
 * 版本：V1.0.0
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ======================================================
*/
#endregion
using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace DesktopHelper.Util
{
    namespace WinShellApi
    {
        [Flags]
        public enum SHERB : uint
        {
            /*   #define SHERB_NOCONFIRMATION    0x00000001
                 #define SHERB_NOPROGRESSUI      0x00000002
                 #define SHERB_NOSOUND           0x00000004*/

            /// <summary>
            /// 不选择其他的三个项,不可与其他选项同时使用
            /// </summary>
            SHERB_GENNERAL = 0x00000000,

            /// <summary>
            /// 不显示确认删除的对话框
            /// </summary>
            SHERB_NOCONFIRMATION = 0x00000001,
            /// <summary>
            /// 不显示删除过程的进度条
            /// </summary>
            SHERB_NOPROGRESSUI = 0x00000002,
            /// <summary>
            /// 当删除完成时，不播放声音
            /// </summary>
            SHERB_NOSOUND = 0x00000004,
            /// <summary>
            /// 静默删除
            /// </summary>
            SHERB_SILENT = 0x00000007

        };

        [StructLayout(LayoutKind.Explicit, Pack = 2)]
        public struct SHQUERYRBINFO
        {
            //这个结构必须是用户显示编写偏移量才能准确获取数值
            [FieldOffset(0)]
            public int cbsize;
            [FieldOffset(4)]
            public long i64Size;
            [FieldOffset(12)]
            public long i64NumItems;
        };

        public static class LibWarp
        {

            /// <summary>
            /// 清空指定磁盘或目录的回收站的内容
            /// </summary>
            /// <param name="hwnd">对话框的句柄，通常应当设为NULL</param>
            /// <param name="RootPath">磁盘路径，如果要清空所有磁盘，设置为null值或空值</param>
            /// <param name="flags">SHERB枚举的值，一个或多个的组合</param>
            /// <returns>成功返回0，S_OK值，失败为其他的OLE定义值</returns>

            [DllImport("shell32.dll", CharSet = CharSet.Unicode, CallingConvention =
               CallingConvention.StdCall)]
            //   [PreserveSig()]
            public static extern uint
           SHEmptyRecycleBinW(
                IntPtr hwnd,
             [In()]
                [MarshalAs(UnmanagedType.LPWStr, SizeConst = 260)]
                string rootpath,
                SHERB flags);


            /// <summary>
            ///API的SHQueryRecycleBinW
            /// </summary>
            [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
            public static extern uint SHQueryRecycleBinW(
                [In()]
                [MarshalAs(UnmanagedType.LPWStr, SizeConst = 260)]
                string RootPath
                , ref SHQUERYRBINFO queyRBInfo);

        };

        internal class _rbState
        {
            private IntPtr _hwnd;
            private string _rootpath;
            private SHERB _sherb;
            private uint _rv;
            internal _rbState(IntPtr hwnd, string rootpath, SHERB sherb)
            {
                this._hwnd = hwnd;
                this._rootpath = rootpath;
                this._sherb = sherb;
                _rv = 0;
            }
            internal _rbState()
            {
                this._hwnd = IntPtr.Zero;
                this._rootpath = null;
                this._sherb = SHERB.SHERB_GENNERAL;
            }
            internal IntPtr Hwnd
            {
                get
                {
                    return this._hwnd;
                }
                set
                {
                    this._hwnd = value;
                }
            }
            internal string Rootpath
            {
                get
                {
                    return this._rootpath;
                }
                set
                {
                    this._rootpath = value;
                }
            }
            internal SHERB Sherb
            {
                get
                {
                    return this._sherb;
                }
                set
                {
                    this._sherb = value;
                }
            }
            internal uint Revalue
            {
                set
                {
                    this._rv = value;
                }
                get
                {
                    return this._rv;
                }
            }
            internal void
                ReSetState(IntPtr hwnd, string rootpath, SHERB sherb)
            {
                this._hwnd = hwnd;
                this._rootpath = rootpath;
                this._sherb = sherb;
            }
        };

        /// <summary>
        /// 解决不能删除的原因：调用线程必须是STA线程
        /// 查询回收站的大小和个数，以及从回收站清空文件
        /// 这个类是对SHQueryRecycleBinW函数和SHEmptyRecycleBinW函数的封装
        /// 调用EmptyRecycleBin方法，删除回收站的内容；调用QuerySizeRecycleBin，查询
        /// 回收站的大小和文件的个数
        /// </summary>
        public class RecycleBinInfo
        {
            /*
             * 注意：多个对象实例（不同引用的），如果调用EmptyRecycleBin方法的任何重载，
             * 它们的调用是顺序的，（就好象在单个线程里的调用是一样的）*/
            //--------------

            /*这个类的实现是，在内部创建一个线程调用wrokThread，这样做的原因是将对象完全封装起来，
             * 因为并不能确定用户调用SHEmptyRecycleBin函数的线程模式是否为STA模式，
             * SHEmptyRecycleBin函数必须在STA模式下才能正确调用，这个类采用一个折衷的方法来实现。
             * 当调用EmptyRecycleBin方法之一的重载，都在内部创建一个线程，将线程模式设置为STAThread
             * 
             */
            private void wrokThread(object state)
            {
                //state对象转换_rbState
                lock (state)
                { /*同步*/
                    _rbState temp = (_rbState)state;

                    temp.Revalue = LibWarp.SHEmptyRecycleBinW(
                          temp.Hwnd,
                          temp.Rootpath,
                          temp.Sherb);
                    ((_rbState)state).Revalue = temp.Revalue;

                }
                this.ewh.Set();
            }
            /// <summary>
            /// 实例化一个RecycleBinInfo对象
            /// </summary>
            public RecycleBinInfo()
            {
                /*初始化对象*/
                this.ewh = new EventWaitHandle(
                false, EventResetMode.AutoReset,
                Guid.NewGuid().ToString());
            }
            /// <summary>
            /// 清空回收站，这个方法同SHEmptyRecycleBin函数一样调用
            /// </summary>
            /// <param name="hwnd">在调用SHEmptyRecycleBin期间，指向用来显示的对话框的父窗体的句柄
            /// 可为NULL值
            /// </param>
            /// <param name="rootpath">最大长度为260个字符的字符串，指定磁盘根目录或文件夹目录，可为null，则清空整个回收站的内容</param>
            /// <param name="dwFlags">SHERB枚举，可组合使用</param>
            /// <returns>成功返回0,否则为OLE定义的错误值</returns>
            public uint EmptyRecycleBin(IntPtr hwnd, string rootpath, SHERB dwFlags)
            {

                _rbState rvs = new _rbState(hwnd, rootpath, dwFlags);
                rvs.Revalue = 0x8000FFFF;


                long size, items;

                this.QuerySizeRecycleBin(out size, out items);
                if (size == 0)
                    return rvs.Revalue;

                lock (rvs)
                {
                    Thread t = new Thread(
                        new ParameterizedThreadStart(this.wrokThread));
                    t.SetApartmentState(ApartmentState.STA);
                    t.Start(rvs);
                }
                /* Console.WriteLine("aaa");测试了，同一个对象实例，依次调用EmptyRecycleBin
                的任何一个重载，其顺序总是：先被调用的，总是先运行，后面的--排队等待
                 * 这样的原因是ewh是AUTO的，set之后，同样命名的对象就不会wait了
                 */
                this.ewh.WaitOne();
                this.ewh.Reset();
                return rvs.Revalue;
            }

            /// <summary>
            /// 清空整个回收站的内容
            /// </summary>
            /// <returns>成功返回0,否则为OLE定义的错误值</returns>
            public uint EmptyRecycleBin()
            {
                _rbState rvs = new _rbState();
                rvs.Revalue = 0x8000FFFF;

                long size, items;

                this.QuerySizeRecycleBin(out size, out items);
                if (size == 0)
                    return rvs.Revalue;

                lock (rvs)
                {
                    Thread t = new Thread(
                       new ParameterizedThreadStart(this.wrokThread));
                    t.SetApartmentState(ApartmentState.STA);//将线程设置为STA
                    t.Start(rvs);

                }
                this.ewh.WaitOne();
                this.ewh.Reset();
                return rvs.Revalue;
            }
            /// <summary>
            /// 查询整个回收站的大小和被删除的文件的个数
            /// </summary>
            /// <param name="RBsize">大小,按字节计算</param>
            /// <param name="RBNumItems">个数</param>
            /// <returns>如果函数成功返回0，错误为其他OLE定义的错误值</returns>
            public uint QuerySizeRecycleBin(out long RBsize, out long RBNumItems)
            {
                //整个 ,字节,两个出参都为-1表示异常
                RBsize = -1;
                RBNumItems = -1;
                SHQUERYRBINFO rbinfo = new SHQUERYRBINFO();
                rbinfo.cbsize = Marshal.SizeOf(rbinfo);

                uint rv = LibWarp.SHQueryRecycleBinW(null, ref rbinfo);
                RBsize = rbinfo.i64Size;
                RBNumItems = rbinfo.i64NumItems;

                return rv;
            }
            /// <summary>
            /// 查询指定目录或磁盘下被删除到回收站的文件的总大小和个数。
            /// 如果设置为null，效果跟 QuerySizeRecycleBin(out long RBsize,out long RBNumItems )一样
            /// </summary>
            /// <param name="rootpath">指定的磁盘和目录</param>
            /// <param name="RBsize">总大小，按字节计算</param>
            /// <param name="RBNumItems">文件的个数</param>
            /// <returns>如果函数成功返回0，错误为其他OLE定义的错误值</returns>
            public uint QuerySizeRecycleBin(string rootpath, out long RBsize, out long RBNumItems)
            {

                RBsize = -1;
                RBNumItems = -1;
                SHQUERYRBINFO rbinfo = new SHQUERYRBINFO();
                rbinfo.cbsize = Marshal.SizeOf(rbinfo);

                uint rv = LibWarp.SHQueryRecycleBinW(rootpath, ref rbinfo);
                RBsize = rbinfo.i64Size;
                RBNumItems = rbinfo.i64NumItems;
                return rv;
            }
            #region 字段
            private EventWaitHandle ewh;
            /// <summary>
            /// HRSULT的E_UNEXCEPTED值
            /// </summary>
            public static readonly uint E_UNEXCEPTED = 0x8000FFFF;
            #endregion 字段
        };
    }
}
