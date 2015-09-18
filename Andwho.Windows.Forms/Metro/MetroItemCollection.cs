using System;
using System.Text;
using System.Collections.Generic;

namespace Andwho.Windows.Forms.Metro
{
    /// <summary>
    /// 
    /// </summary>
    public class MetroItemCollection : List<MetroItem>
    {
        #region 变量
        /// <summary>
        /// 
        /// </summary>
        private MetroForm _owner = null;
        #endregion

        #region 构造函数
        /// <summary>
        /// 
        /// </summary>
        /// <param name="owner"></param>
        public MetroItemCollection(MetroForm owner)
        {
            this._owner = owner;
        }
        #endregion

        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public MetroForm Owner
        {
            get
            {
                return this._owner;
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        new public void Add(MetroItem item)
        {
            base.Add(item);
        }

        #endregion
    }
}
