using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace WindowsCalendar
{
    /// <summary>
    /// 优化过的Panel组件
    /// </summary>
    public class OptimizedPanel : Panel
    {
        public OptimizedPanel()
        {
            // 启动双缓冲绘图模式，防止卡顿
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
        }

        private bool allowDragWindow = true;

        [Description("是否允许用户通过拖动此控件来拖动窗口")]
        public bool AllowDragWindow
        {
            get { return allowDragWindow; }
            set { allowDragWindow = value; }
        }

        // MouseDown、MouseUp、MouseMove处理随意拖动逻辑
        private static bool isDrag = false;
        private static int mousePositionX = 0;
        private static int mousePositionY = 0;

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (allowDragWindow)
            {
                isDrag = true;
                mousePositionX = e.Location.X;
                mousePositionY = e.Location.Y;
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (allowDragWindow)
            {
                isDrag = false;
                mousePositionX = 0;
                mousePositionY = 0;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (allowDragWindow && isDrag)
            {
                this.FindForm().Left += e.Location.X - mousePositionX;
                this.FindForm().Top += e.Location.Y - mousePositionY;
            }
        }

    }
}