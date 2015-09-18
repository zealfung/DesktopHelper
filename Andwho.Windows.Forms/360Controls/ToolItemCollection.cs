using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Andwho.Windows.Forms
{
    /// <summary>
    /// 代表 ToolBar 中项的集合。
    /// </summary>
    [ListBindable(false)]
    public class ToolItemCollection : List<ToolItem>
    {
        #region 变量
        /// <summary>
        /// ToolBar
        /// </summary>
        private ToolBar _owner = null;

        #endregion

        #region 构造函数
        /// <summary>
        /// 初始化 Andwho.Windows.Forms.ToolItemCollection 新的实例。
        /// </summary>
        /// <param name="owner">ToolBar</param>
        public ToolItemCollection(ToolBar owner)
        {
            this._owner = owner;
        }

        #endregion

        #region 方法
        /// <summary>
        /// 返回该项在集合中的索引值
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int GetIndexOfRange(ToolItem item)
        {
            int result = -1;
            for (int i = 0; i < base.Count; i++)
            {
                if (item == base[i])
                {
                    result = i;
                    break;
                }
            }
            return result;
        }
        #endregion
    }
}
