using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsCalendar
{
    public partial class AgreementForm : Form
    {
        public AgreementForm()
        {
            // 确保窗口在屏幕中央显示
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
        }

        private void AgreementForm_Load(object sender, EventArgs e)
        {
            // 初始化窗口
            this.ShowInTaskbar = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.TopMost = true;

            // 初始化backgroundPanel
            backgroundPanel.BackColor = LaunchForm.normalizedBlue;

            // 初始化agreeButtonPanel
            agreeButtonPanel.ButtonText = "  我同意(Y)";
            agreeButtonPanel.Style = ButtonPanelStyle.DarkBackground;

            // 初始化exitButtonPanel
            exitButtonPanel.ButtonText = "   退出(N)";
            exitButtonPanel.Style = ButtonPanelStyle.DarkBackground;

            // 初始化viewHomepageButtonPanel
            viewHomepageButtonPanel.ButtonText = "访问主页(H)";
            viewHomepageButtonPanel.Style = ButtonPanelStyle.DarkBackground;
        }

        private void backgroundPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            Font titleFont = new Font("微软雅黑", 10, FontStyle.Bold);
            Font normalFont = new Font("微软雅黑", 10);
            Font importantFont = new Font("微软雅黑", 10, FontStyle.Bold);

            const string titleText = "中华万年历 - 最终用户许可协议   Ver: " + ApplicationStatus.version;
            //const string contentText =
            //    "                                欢迎！\n" +
            //    "\n" +
            //    "本软件在运行时需要以下权限：\n" +
            //    "[1] 连接网络通过访问api获取黄历信息，页面地址：\n" +
            //    "　  http://japi.juhe.cn/calendar/day \n" +
            //    "[2] 发送一封电子邮件，将使用信息反馈给开发者。\n" +
            //    "     邮件仅包含一串特征码，不会发送任何个人信息。\n" +
            //    "     收件地址： prepcal@163.com";

            string contentText = "";
            if (ApplicationStatus.isVersionUpdated)
                contentText += "                   欢迎！感谢您更新万年历。\n\n";
            else
                contentText += "                                欢迎！\n\n";
            contentText += 
                "本软件在运行时需要以下权限：\n" +
                "[1] 连接网络通过访问api获取黄历信息，页面地址：\n" +
                "　  http://v.juhe.cn/calendar/day \n" +
                "[2] 发送一封电子邮件，将使用信息反馈给开发者。\n" +
                "     邮件仅包含一串特征码，不会发送任何个人信息。\n" +
                "     收件地址： prepcal@163.com";

            const string importantText =
                "由于使用了上述权限，在使用过程中安全软件可能会\n" +
                "弹出警告。本人以一名X大学学生的名义担保，\n" +
                "只要您从官方渠道下载，本软件绝不含任何木马病毒。";

            g.DrawString(titleText, titleFont, Brushes.White, 12, 9);
            g.DrawString(contentText, normalFont, Brushes.White, 20, 45);
            g.DrawString(importantText, importantFont, new SolidBrush(Color.FromArgb(255, 242, 157)), 16, 200);
        }

        private void agreeButtonPanel_MouseClick(object sender, MouseEventArgs e)
        {
            // 返回信号
            LaunchForm.OnAgreementFormClosed(true);
            this.Dispose(true);
            this.Close();
        }

        private void exitButtonPanel_MouseClick(object sender, MouseEventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void viewHomepageButtonPanel_MouseClick(object sender, MouseEventArgs e)
        {
            System.Diagnostics.Process.Start(ApplicationStatus.homepage);
        }

        private void AgreementForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'y')
                agreeButtonPanel_MouseClick(sender, new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
            else if (e.KeyChar == 'n')
                exitButtonPanel_MouseClick(sender, new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
            else if (e.KeyChar == 'h')
                viewHomepageButtonPanel_MouseClick(sender, new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
        }
    }
}
