using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;


namespace WindowsCalendar
{
    public partial class TransitionForm : Form
    {
        public TransitionForm()
        {
            // 设置双缓冲绘图方式
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);

            // 确保窗口在屏幕中央显示
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
        }

        private void TransitionForm_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.TopMost = true;
            this.ShowInTaskbar = false;
            this.Opacity = 0;
        }

        private void TransitionForm_Shown(object sender, EventArgs e)
        {
            // 启动后台线程，防止UI线程卡死

            Task fadeInTask = Task.Factory.StartNew(() =>
                {
                    Thread.Sleep(10); // 轻微延迟后启动，确保TransitionForm优先显示，防止UI线程卡死
                    this.BeginInvoke(new MethodInvoker(delegate { FadeIn(); }));
                });

            Task showFormTask = Task.Factory.StartNew(() => 
                {
                    Thread.Sleep(50); // 确保fadeInTask先执行
                    this.BeginInvoke(new MethodInvoker(delegate { ShowForm(); }));
                });
        }

        private void ShowForm()
        {
            MainForm mainForm = new MainForm();
            mainForm.MainFormLoadFinished += new MyEventHandlerArgs(HideFormEventHandler); // 增加事件处理机制
            mainForm.Show();
        }

        private void FadeIn()
        {
            while ((this.Opacity += 0.04) < 1)
                Thread.Sleep(15);
        }

        // 隐藏窗口事件处理程序，调用FadeOut方法
        private void HideFormEventHandler(object o)
        {
            Task hideFormTask = Task.Factory.StartNew(() =>
                {
                    Thread.Sleep(1500); // 等待MainForm完全加载完毕后再退出前台，最小值：500ms
                    this.BeginInvoke(new MethodInvoker(delegate { FadeOut(o); }));
                }); // 启动后台线程，防止MainForm线程卡死
        }

        private void FadeOut(object o)
        {
            (o as MainForm)?.Activate(); // 激活主窗口
            while ((this.Opacity -= 0.04) > 0)
                Thread.Sleep(10);
            this.Visible = false;
            this.Dispose(true);
            this.Close();
        }

        private void TransitionForm_Paint(object sender, PaintEventArgs e)
        {
            string title = "中华万年历";
            string copyright = "版权所有 © lonelyenvoy\nAll Rights Reserved.";
            string donateTitle = "特别感谢以下人士对此项目的赞助";
            RectangleF titleRect = new RectangleF(400, 220, 400, 100);
            RectangleF copyrightRect = new RectangleF(6, this.Height - 38, 400, 40);
            RectangleF donateTitleRect = new RectangleF(402, 310, 400, 50);
            RectangleF donateInfoRect = new RectangleF(402, 345, 400, 100);
            RectangleF noticeRect = new RectangleF(402, 455, 570, 100);
            Font titleFont = new Font("微软雅黑", 34);
            Font copyrightFont = new Font("微软雅黑", 9);
            Font donateTitleFont = new Font("微软雅黑", 16);
            Font donateInfoFont = new Font("微软雅黑", 10);
            Font noticeFont = new Font("微软雅黑", 14);
            Pen titlePen = new Pen(Color.FromArgb(206, 119, 255), 4);
            //Pen copyrightPen = new Pen(Color.FromArgb(206, 119, 255), 1);
            Pen donateTitlePen = new Pen(Color.FromArgb(206, 119, 255), 2);
            Pen donateInfoPen = new Pen(Color.FromArgb(206, 119, 255), 1);
            Pen noticePen = new Pen(Color.FromArgb(206, 119, 255), 2);

            MainFormUtil.DrawStrokedText(e, title, titleRect, titleFont, Brushes.White, titlePen);
            //MainForm.DrawStrokedText(e, copyright, copyrightRect, copyrightFont, Brushes.White, copyrightPen);

            Graphics g = e.Graphics;
            g.DrawString(copyright, copyrightFont, Brushes.White, copyrightRect);

            if (ApplicationStatus.isInternetConnected && ApplicationStatus.donateInfo != "")
            {
                MainFormUtil.DrawStrokedText(e, donateTitle,
                    donateTitleRect, donateTitleFont, Brushes.White, donateTitlePen);
                MainFormUtil.DrawStrokedText(e, ApplicationStatus.donateInfo,
                    donateInfoRect, donateInfoFont, Brushes.White, donateInfoPen);
            }
            if (ApplicationStatus.notice != "") 
            {
                MainFormUtil.DrawStrokedText(e, ApplicationStatus.notice, noticeRect, noticeFont, Brushes.White, noticePen);
            }
        }

    }
}
