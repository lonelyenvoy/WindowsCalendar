using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsCalendar
{
    /// <summary>
    /// 加水印的TextBox组件
    /// </summary>
    public class WatermarkedTextBox : TextBox
    {
        // 以下代码出处（有删改）：http://blog.163.com/chs1987@126/blog/static/68709178201110148542833/
        // 版权归原作者所有

        private const int WM_PAINT = 0x000F;

        private string backGroundText = "";

        [Description("文本框无文字时的提示文本")]
        public string BackGroundText
        {
            get { return backGroundText; }
            set { backGroundText = value; }
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_PAINT)
            {
                using (Graphics g = CreateGraphics())
                {
                    if (string.IsNullOrEmpty(Text) /*&& !Focused*/)
                    {
                        SizeF size = g.MeasureString(backGroundText, Font);
                        //draw background text  
                        g.DrawString(backGroundText, Font, Brushes.DarkGray, new PointF(0, 1));
                        //g.DrawString(backGroundText, Font, Brushes.DarkGray, new PointF(0, (Height - size.Height) / 2));
                    }
                    // 绘制当前日期文本
                    //g.DrawString(MainForm.CurrentTextEditingDate.Month.ToString().PadLeft(2, '0')
                    //+ MainForm.CurrentTextEditingDate.Day.ToString().PadLeft(2, '0'),
                    //new Font("微软雅黑", 36), Brushes.LightGray, new PointF(0, this.ClientRectangle.Height - 70));
                }
            }
        }
    }

}
