using System;
using System.Drawing;
using System.Text;

namespace Andwho.Windows.Forms.Metro
{
    /// <summary>
    /// 使用 MetroRenderer 类将特定样式或主题应用于 MetroForm
    /// </summary>
    public class MetroRenderer
    {
        #region 变量
        
        /// <summary>
        /// 默认背景颜色
        /// </summary>
        private Color _backColor = Color.FromArgb(30, 30, 30);
        /// <summary>
        /// 鼠标划过时的背景颜色
        /// </summary>
        private Color _enterColor = Color.FromArgb(63, 63, 63);
        /// <summary>
        /// 鼠标按下时的背景颜色
        /// </summary>
        private Color _downColor = Color.FromArgb(0, 122, 204);
        /// <summary>
        /// 文字的前景色
        /// </summary>
        private Color _foreColor = Color.FromArgb(241, 241, 241);
        /// <summary>
        /// 开始菜单前景色
        /// </summary>
        private Color _startColor = Color.FromArgb(200, 200, 200);//Color.FromArgb(115, 39, 148);

        private Font _font = new Font("宋体", 9f);

        #endregion

        #region 构造函数
        /// <summary>
        /// 
        /// </summary>
        public MetroRenderer() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="font"></param>
        public MetroRenderer(Font font)
            : this()
        {
            this._font = font;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="backColor"></param>
        public MetroRenderer(Color backColor)
            : this()
        {
            this._backColor = backColor;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="backColor"></param>
        /// <param name="foreColor"></param>
        public MetroRenderer(Color backColor, Color foreColor)
            : this(backColor)
        {
            this._foreColor = foreColor;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="backColor"></param>
        /// <param name="foreColor"></param>
        /// <param name="enterColor"></param>
        /// <param name="downColor"></param>
        public MetroRenderer(Color backColor, Color foreColor, Color enterColor, Color downColor)
            : this(backColor, foreColor)
        {
            this._enterColor = enterColor;
            this._downColor = downColor;
        }
        #endregion

        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public virtual Color BackColor
        {
            get { return this._backColor; }
            set { this._backColor = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual Color EnterColor
        {
            get { return this._enterColor; }
            set { this._enterColor = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual Color DownColor
        {
            get { return this._downColor; }
            set { this._downColor = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual Color ForeColor
        {
            get { return this._foreColor; }
            set { this._foreColor = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual Color StartColor
        {
            get { return this._startColor; }
            set { this._startColor = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual Font Font
        {
            get { return this._font; }
            set { this._font = value; }
        }

        #endregion
    }
}
