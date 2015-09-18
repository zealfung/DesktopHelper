using System.Windows.Forms;

namespace Andwho.Windows.Win32
{
    /// <summary>
    /// 鼠标钩子扩展事件
    /// </summary>
    public class MouseEventExtArgs : MouseEventArgs
    {
        #region 变量
        /// <summary>
        /// 如果为 true 防止进一步的处理其他应用程序事件。
        /// </summary>
        private bool _handled;
        #endregion

        #region 构造函数

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buttons"></param>
        /// <param name="clicks"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="delta"></param>
        public MouseEventExtArgs(MouseButtons buttons, int clicks, int x, int y, int delta)
            : base(buttons, clicks, x, y, delta)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        internal MouseEventExtArgs(MouseEventArgs e)
            : base(e.Button, e.Clicks, e.X, e.Y, e.Delta)
        {
        }

        #endregion

        #region 属性
        /// <summary>
        /// 如果为 true 防止进一步的处理其他应用程序事件。
        /// </summary>
        public bool Handled
        {
            get { return this._handled; }
            set { this._handled = value; }
        }
        #endregion
    }
}
