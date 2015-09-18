using System;
using System.Collections.Generic;
using System.Text;

namespace Andwho.Windows.Forms
{
    /// <summary>
    /// Item 的类型
    /// </summary>
    public enum EItemType
    {
        /// <summary>
        /// 当前应用程序
        /// </summary>
        Application,
        /// <summary>
        /// 文件夹
        /// </summary>
        Directory,
        /// <summary>
        /// 可执行文件
        /// </summary>
        Exe,
        /// <summary>
        /// 菜单
        /// </summary>
        Menu,
        /// <summary>
        /// 未知
        /// </summary>
        None,
        /// <summary>
        /// 系统程序
        /// </summary>
        System,
    }
}
