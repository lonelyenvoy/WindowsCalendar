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
using System.Text.RegularExpressions;

namespace WindowsCalendar
{
    public partial class AboutForm : Form
    {
        private static bool isShown = false;

        public static bool IsShown
        {
            get { return isShown; }
        }

        public AboutForm()
        {
            // 使窗口居中显示
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            // 初始化窗口
            this.FormBorderStyle = FormBorderStyle.None;
            this.TopMost = true;
            this.ShowInTaskbar = false;
            this.BackColor = LaunchForm.normalizedBlue;
            this.Opacity = 0;

            // 初始化supportButtonPanel
            supportButtonPanel.Style = ButtonPanelStyle.DarkBackground;
            supportButtonPanel.ButtonText = " 参与一元项目\n赞助开发者￥1";

            // 初始化qrDonatePanel
            qrDonatePanel.Visible = false;
            qrDonatePanel.BackgroundImageLayout = ImageLayout.Zoom;
            qrDonatePanel.BackgroundImage = WindowsCalendar.Properties.Resources.qrDonate;

            isShown = true;
        }

        private bool isMouseInCloseButton = false;

        private void closeButtonPanel_Paint(object sender, PaintEventArgs e)
        {
            // 画一个叉
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.White);
            pen.Width = isMouseInCloseButton ? 2 : 1;

            g.DrawLine(pen, 0, 0, 12, 12);
            g.DrawLine(pen, 12, 0, 0, 12);
        }

        private void closeButtonPanel_MouseEnter(object sender, EventArgs e)
        {
            isMouseInCloseButton = true;
            closeButtonPanel.Invalidate();
        }

        private void closeButtonPanel_MouseLeave(object sender, EventArgs e)
        {
            isMouseInCloseButton = false;
            closeButtonPanel.Invalidate();
        }

        private void closeButtonPanel_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.FadeOutToCloseForm();
            }
        }

        // MouseDown、MouseUp、MouseMove处理随意拖动逻辑
        private static bool isDrag = false;
        private static int mousePositionX = 0;
        private static int mousePositionY = 0;

        private void AboutForm_MouseDown(object sender, MouseEventArgs e)
        {
            isDrag = true;
            mousePositionX = e.Location.X;
            mousePositionY = e.Location.Y;
        }

        private void AboutForm_MouseUp(object sender, MouseEventArgs e)
        {
            isDrag = false;
            mousePositionX = 0;
            mousePositionY = 0;
        }

        private void AboutForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrag)
            {
                this.Left += e.Location.X - mousePositionX;
                this.Top += e.Location.Y - mousePositionY;
            }
        }

        private void AboutForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Font titleFont = new Font("微软雅黑", 10, FontStyle.Bold);
            Font normalFont = new Font("微软雅黑", 10);
            Font smallFont = new Font("微软雅黑", 8);

            const string titleText = "关于中华万年历   版本: Ver " + ApplicationStatus.version;
            const string bodyText =
                "中华万年历 ™ 是 Easier Coding Win32 ™\n" +
                "项目下基于C#.NET开发的一款小型软件，\n" +
                "具有农历显示、查看黄历信息、周历、记事\n" +
                "本等实用功能。\n" +
                "\n" +
                "作者在开发过程中付出了许多心血，投入了\n" +
                "大量时间和资金，并将始终坚持无广告原则。\n" +
                "您的支持是我的不竭动力。多谢支持！";
            const string copyrightText =
                "© 版权所有  lonelyenvoy";
            const string descriptionText =
                "前十位赞助者的名字将\n出现在我开发的软件中";
            string donateManTimeText = "";

            if (ApplicationStatus.isInternetConnected && PrepcalWebInfoService.IsInitialized)
                donateManTimeText = "已赞助人数：" + ApplicationStatus.donateManTimeCount;

            // 绘制标题
            g.DrawString(titleText, titleFont, Brushes.White, 12, 9);

            // 绘制正文
            g.DrawString(bodyText, normalFont, Brushes.White, 25, 45);

            // 绘制版权信息
            g.DrawString(copyrightText, normalFont, Brushes.White, 40, 195);

            // 绘制说明文本
            g.DrawString(descriptionText, smallFont, Brushes.Yellow, 70, 239);

            // 网络连接正常且有人赞助时，绘制“已赞助人数”
            if (ApplicationStatus.isInternetConnected 
                && ApplicationStatus.donateManTimeCount != "0" && ApplicationStatus.donateManTimeCount != "")
                g.DrawString(donateManTimeText, smallFont, Brushes.White, 70, 225);
        }

        private void supportButtonPanel_MouseClick(object sender, MouseEventArgs e)
        {
            if (qrDonatePanel.Visible)
                qrDonatePanel.Visible = false;
            else
                qrDonatePanel.Visible = true;
        }

        private void AboutForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            isShown = false;
        }

        private void AboutForm_Shown(object sender, EventArgs e)
        {
            // 淡入效果
            Task fadeInTask = Task.Factory.StartNew(() =>
            {
                // 等待窗口载入完毕
                Thread.Sleep(50);
                this.BeginInvoke(new MethodInvoker(delegate {
                    while ((this.Opacity += 0.06) < 1)
                        Thread.Sleep(10);
                }));
            });
        }

        private void FadeOutToCloseForm()
        {
            // 淡出效果
            Task fadeOutTask = Task.Factory.StartNew(() =>
            {
                Thread.Sleep(10);
                this.BeginInvoke(new MethodInvoker(delegate {
                    while ((this.Opacity -= 0.06) > 0)
                        Thread.Sleep(10);
                    this.Visible = false;
                    this.Close();
                }));
            });
        }
    }
}
