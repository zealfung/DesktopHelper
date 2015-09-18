using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Diagnostics;
using System.Drawing.Imaging;

namespace Andwho.Windows.Helper
{
    /// <summary>
    /// 图像处理类
    /// </summary>
    public class ImageProcess
    {
        #region 方法
        
        /// <summary>
        /// 图片无损缩放
        /// </summary>
        /// <param name="source">源图片</param>
        /// <param name="destHeight">缩放后图片高度</param>
        /// <param name="destWidth">缩放后图片宽度</param>
        /// <returns></returns>
        public static Bitmap GetThumbnail(Image source, int destWidth, int destHeight)
        {
            Bitmap bitmap = null;
            try
            {
                System.Drawing.Imaging.ImageFormat sourceFormat = source.RawFormat;
                int sW = 0, sH = 0;
                // 按比例缩放
                int sWidth = source.Width;
                int sHeight = source.Height;

                if (sHeight > destHeight || sWidth > destWidth)
                {
                    if ((sWidth * destHeight) > (sHeight * destWidth))
                    {
                        sW = destWidth;
                        sH = (destWidth * sHeight) / sWidth;
                    }
                    else
                    {
                        sH = destHeight;
                        sW = (sWidth * destHeight) / sHeight;
                    }
                }
                else
                {
                    sW = sWidth;
                    sH = sHeight;
                }

                bitmap = new Bitmap(destWidth, destHeight);
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.Clear(Color.WhiteSmoke);

                    // 设置画布的描绘质量
                    g.CompositingQuality = CompositingQuality.HighQuality;
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                    Rectangle rect = new Rectangle((destWidth - sW) / 2, (destHeight - sH) / 2, sW, sH);
                    g.DrawImage(source, rect, 0, 0, source.Width, source.Height, GraphicsUnit.Pixel);

                    source.Dispose();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ImageProcess.GEtThumbnail(Image, int, int) :: " + ex.Message);
                throw ex;
            }
            return bitmap;
        }

        /// <summary>
        /// 图片无损缩放
        /// </summary>
        /// <param name="filePath">图片源路径</param>
        /// <param name="destHeight">缩放后图片高度</param>
        /// <param name="destWidth">缩放后图片宽度</param>
        /// <returns></returns>
        public static Bitmap GetThumbnail(string filePath, int destWidth, int destHeight)
        {
            Bitmap bitmap = null;
            try
            {
                using (Image source = Image.FromFile(filePath))
                {
                    ImageProcess.GetThumbnail(source, destWidth, destHeight);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ImageProcess.GetThumbnail(string, int, int) :: " + ex.Message);
                throw ex;
            }
            return bitmap;
        }

        /// <summary>
        /// 截取屏幕图像
        /// </summary>
        /// <returns></returns>
        public static Bitmap GetScreenPic()
        {
            Bitmap bitmap = null;
            try
            {
                Size size = Screen.PrimaryScreen.WorkingArea.Size;
                if (bitmap == null)
                    bitmap = new Bitmap(size.Width, size.Height);
                Graphics g = Graphics.FromImage(bitmap);
                g.CopyFromScreen(Point.Empty, Point.Empty, size);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ImageProcess.GetScreenPic() :: " + ex.Message);
                throw ex;
            }
            return bitmap;
        }

        /// <summary>
        /// 灰度
        /// </summary>
        /// <param name="image">需要处理成灰度的图片对象</param>
        /// <returns></returns>
        public static Bitmap ToGray(Image image)
        {
            Bitmap bitmap = new Bitmap(image);
            try
            {
                image.Dispose();
                image = null;

                int width = bitmap.Width;
                int height = bitmap.Height;
                Rectangle rect = new Rectangle(0, 0, width, height);
                //用可读写的方式锁定全部位图像素
                BitmapData bmpData = bitmap.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
                byte gray = 0;
                unsafe//启用不安全模式
                {
                    byte* p = (byte*)bmpData.Scan0;//获取首地址
                    int offset = bmpData.Stride - width * 3;
                    //二维图像循环
                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            //gray = (byte)((float)p[0] * 0.114f + (float)p[1] * 0.587f + (float)p[2] * 0.299f);
                            gray = (byte)((float)(p[0] + p[1] + p[2]) / 3.0f);
                            p[2] = p[1] = p[0] = (byte)gray;
                            p += 3;
                        }
                        p += offset;
                    }
                }
                bitmap.UnlockBits(bmpData);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ImageProcess.ToGray(Image) :: " + ex.Message);
                throw ex;
            }
            #region 效果不如前者

            ////得到首地址
            //IntPtr ptr = bmpData.Scan0;
            ////24位bmp位图字节数
            //int bytes = width * height * 3;
            //byte[] rgbValues = new byte[bytes];
            //Marshal.Copy(ptr, rgbValues, 0, bytes);
            ////灰度化
            //double colorTemp = 0;
            //for (int i = 0; i < bytes; i += 3)
            //{
            //    //colorTemp = (byte)(rgbValues[i] * 0.114f + rgbValues[i + 1] * 0.587f + rgbValues[i + 2] * 0.299f);
            //    colorTemp = (rgbValues[i] + rgbValues[i + 1] + rgbValues[i + 2]) / 3;
            //    rgbValues[i] = rgbValues[i + 1] = rgbValues[i + 2] = (byte)colorTemp;
            //}
            ////还原位图
            //Marshal.Copy(rgbValues, 0, ptr, bytes);
            //bitmap.UnlockBits(bmpData);
            #endregion

            return bitmap;
        }

        #endregion
    }
}
