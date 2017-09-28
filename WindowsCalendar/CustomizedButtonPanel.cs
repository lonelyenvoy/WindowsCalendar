using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsCalendar
{
    /// <summary>
    /// 自定义的Panel的按钮风格
    /// </summary>
    public enum ButtonPanelStyle
    {
        LightBackground, DarkBackground, Highlighted
    }

    /// <summary>
    /// 按钮风格化的Panel
    /// </summary>
    public class CustomizedButtonPanel : OptimizedPanel
    {
        private string buttonText = "";
        private ButtonPanelStyle style = ButtonPanelStyle.LightBackground;
        private bool isMouseInPanel = false;
        private bool isMouseDown = false;

        private readonly Point buttonStringPoint = new Point(4, 3);
        private readonly Pen buttonWhitePen = new Pen(Color.White, 1);
        private readonly Pen buttonBluePen = new Pen(LaunchForm.normalizedBlue, 1);
        private readonly Pen buttonMediumPurplePen = new Pen(Color.MediumPurple, 1);
        private readonly Font buttonFont = new Font("微软雅黑", 10);
        private readonly Font buttonSmallFont = new Font("微软雅黑", 8);
        private readonly SolidBrush buttonWhiteBrush = new SolidBrush(Color.White);
        private readonly SolidBrush buttonLightGrayBrush = new SolidBrush(Color.FromArgb(224, 224, 224));
        private readonly SolidBrush buttonBlueBrush = new SolidBrush(LaunchForm.normalizedBlue);
        private readonly SolidBrush buttonDarkBlueBrush = new SolidBrush(LaunchForm.normalizedDarkBlue);
        private readonly SolidBrush buttonMediumPurpleBrush = new SolidBrush(Color.MediumPurple);
        private readonly SolidBrush buttonPurpleBrush = new SolidBrush(Color.Purple);

        [Description("按钮中的文本")]
        public string ButtonText
        {
            get { return buttonText; }
            set { buttonText = value; }
        }

        [Description("按钮风格")]
        public ButtonPanelStyle Style
        {
            get { return style; }
            set { style = value; }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);

            Graphics g = e.Graphics;
            if (!isMouseInPanel)
            {
                if (style == ButtonPanelStyle.LightBackground)
                {
                    g.DrawRectangle(buttonBluePen, 0, 0, this.Width - 1, this.Height - 1);
                    g.FillRectangle(buttonWhiteBrush, 1, 1, this.Width - 2, this.Height - 2);
                    g.DrawString(buttonText, buttonFont, buttonBlueBrush, buttonStringPoint);
                }
                else if (style == ButtonPanelStyle.DarkBackground)
                {
                    g.DrawRectangle(buttonWhitePen, 0, 0, this.Width - 1, this.Height - 1);
                    g.DrawString(buttonText, buttonFont, buttonWhiteBrush, buttonStringPoint);
                }
                else if (style == ButtonPanelStyle.Highlighted)
                {
                    g.DrawRectangle(buttonMediumPurplePen, 0, 0, this.Width - 1, this.Height - 1);
                    g.FillRectangle(buttonWhiteBrush, 1, 1, this.Width - 2, this.Height - 2);
                    g.DrawString(ButtonText, buttonFont, buttonMediumPurpleBrush, buttonStringPoint);
                }
            }
            else if (!isMouseDown)
            {
                if (style == ButtonPanelStyle.LightBackground)
                {
                    g.DrawRectangle(buttonWhitePen, 0, 0, this.Width - 1, this.Height - 1);
                    g.FillRectangle(buttonBlueBrush, 1, 1, this.Width - 2, this.Height - 2);
                    g.DrawString(buttonText, buttonFont, buttonWhiteBrush, buttonStringPoint);
                }
                else if (style == ButtonPanelStyle.DarkBackground)
                {
                    g.DrawRectangle(buttonWhitePen, 0, 0, this.Width - 1, this.Height - 1);
                    g.FillRectangle(buttonWhiteBrush, 1, 1, this.Width - 2, this.Height - 2);
                    g.DrawString(buttonText, buttonFont, buttonBlueBrush, buttonStringPoint);
                }
                else if (style == ButtonPanelStyle.Highlighted)
                {
                    g.DrawRectangle(buttonWhitePen, 0, 0, this.Width - 1, this.Height - 1);
                    g.FillRectangle(buttonMediumPurpleBrush, 1, 1, this.Width - 2, this.Height - 2);
                    g.DrawString(buttonText, buttonFont, buttonWhiteBrush, buttonStringPoint);
                }
            }
            else
            {
                if (style == ButtonPanelStyle.LightBackground)
                {
                    //g.DrawRectangle(buttonWhitePen, 0, 0, this.Width - 1, this.Height - 1);
                    //g.FillRectangle(buttonDarkBlueBrush, 1, 1, this.Width - 2, this.Height - 2);
                    g.DrawRectangle(buttonWhitePen, 1, 1, this.Width - 3, this.Height - 3);
                    g.FillRectangle(buttonDarkBlueBrush, 2, 2, this.Width - 4, this.Height - 4);
                    g.DrawString(buttonText, buttonFont, buttonWhiteBrush, buttonStringPoint);
                }
                else if (style == ButtonPanelStyle.DarkBackground)
                {
                    g.DrawRectangle(buttonBluePen, 0, 0, this.Width - 1, this.Height - 1);
                    g.FillRectangle(buttonLightGrayBrush, 1, 1, this.Width - 2, this.Height - 2);
                    g.DrawString(buttonText, buttonFont, buttonBlueBrush, buttonStringPoint);
                }
                else if (style == ButtonPanelStyle.Highlighted)
                {
                    g.DrawRectangle(buttonWhitePen, 1, 1, this.Width - 3, this.Height - 3);
                    g.FillRectangle(buttonPurpleBrush, 2, 2, this.Width - 4, this.Height - 4);
                    g.DrawString(buttonText, buttonFont, buttonWhiteBrush, buttonStringPoint);
                }
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            //base.OnMouseEnter(e);

            isMouseInPanel = true;
            this.Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            //base.OnMouseLeave(e);

            isMouseInPanel = false;
            this.Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            //base.OnMouseDown(e);

            isMouseDown = true;
            this.Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            //base.OnMouseUp(e);

            isMouseDown = false;
            this.Invalidate();
        }
    }
}