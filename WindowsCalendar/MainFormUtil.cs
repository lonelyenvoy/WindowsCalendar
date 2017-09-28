using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsCalendar
{
    /// <summary>
    /// MainForm相关的工具类
    /// </summary>
    public static class MainFormUtil
    {
        /// <summary>
        /// 绘制文本并描边，仅限于在Paint方法中调用
        /// </summary>
        /// <param name="e">组件Paint方法提供的事件参数</param>
        /// <param name="text">需要画出的文本</param>
        /// <param name="textRect">文本框矩形</param>
        /// <param name="font">字体</param>
        /// <param name="textBrush">文本画刷</param>
        /// <param name="outlinePen">轮廓画笔</param>
        public static void DrawStrokedText(PaintEventArgs e, string text, RectangleF textRect,
                                    Font font, Brush textBrush, Pen outlinePen)
        {
            Graphics g = e.Graphics;
            StringFormat titleFormat = StringFormat.GenericTypographic;
            float dpi = g.DpiY;
            using (GraphicsPath path = GetStringPath(text, dpi, textRect, font, titleFormat))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias; // 设置字体质量
                g.DrawPath(outlinePen, path); // 绘制轮廓（描边）
                g.FillPath(textBrush, path); // 填充轮廓（填充）
            }
        }

        /// <summary>
        /// 获得围绕文本的Path
        /// </summary>
        /// <param name="s">文本</param>
        /// <param name="dpi">屏幕dpi</param>
        /// <param name="rect">文本矩形</param>
        /// <param name="font">文本字体</param>
        /// <param name="format">StringFormat</param>
        /// <returns></returns>
        public static GraphicsPath GetStringPath(string s, float dpi, RectangleF rect, Font font, StringFormat format)
        {
            GraphicsPath path = new GraphicsPath();
            // 将字体大小转换为合适的坐标
            float emSize = dpi * font.SizeInPoints / 72;
            path.AddString(s, font.FontFamily, (int)font.Style, emSize, rect, format);
            return path;
        }
    }
}
