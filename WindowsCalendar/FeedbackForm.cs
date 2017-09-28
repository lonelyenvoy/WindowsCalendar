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
    public partial class FeedbackForm : Form
    {
        public FeedbackForm()
        {
            // 确保窗口在屏幕中央显示
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
        }

        private void FeedbackForm_Load(object sender, EventArgs e)
        {
            // 初始化窗口
            this.FormBorderStyle = FormBorderStyle.None;
            this.ShowInTaskbar = false;
            this.BackColor = LaunchForm.normalizedBlue;
            this.Opacity = 0;

            // 初始化contactsTextBox
            //feedbackContentTextBox.AcceptsReturn = true;
            //feedbackContentTextBox.Multiline = true;
            contactsTextBox.BackGroundText = "接收回复的联系方式(QQ/微信/邮箱等) (选填)";

            // 初始化feedbackContentTextBox
            feedbackContentTextBox.AcceptsReturn = true;
            feedbackContentTextBox.AcceptsTab = true;
            feedbackContentTextBox.Multiline = true;
            feedbackContentTextBox.BackGroundText = "在这里畅所欲言";

            // 初始化submitButtonPanel
            submitButtonPanel.ButtonText = "提交邮件(S)";
            submitButtonPanel.Style = ButtonPanelStyle.DarkBackground;

            // 初始化cancelButtonPanel
            cancelButtonPanel.ButtonText = "   取消(C)";
            cancelButtonPanel.Style = ButtonPanelStyle.DarkBackground;
        }

        private void FeedbackForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // 绘制标题
            Font titleFont = new Font("微软雅黑", 10, FontStyle.Bold);
            const string titleText = "意见反馈";

            g.DrawString(titleText, titleFont, Brushes.White, 12, 9);
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

        private void submitButtonPanel_MouseClick(object sender, MouseEventArgs e)
        {
            if (feedbackContentTextBox.Text.Trim() == "")
            {
                MessageBox.Show("请输入您要反馈的信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            string feedbackInfo = feedbackContentTextBox.Text;
            string contactsInfo = contactsTextBox.Text == "" ? "None" : contactsTextBox.Text;

            string mailBody = "Hard Disk Id: " + Hardware.GetHardDiskID()
                + "\n" + "Contacts: " + contactsInfo + "\n\n" + "Feedback: " + feedbackInfo;
            LaunchFormUtil.SendStatisticMail("User Feedback: " + LaunchFormUtil.GetClientIpInfo(), mailBody);

            MessageBox.Show("感谢您的反馈，祝您生活愉快 :-)", "提交成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            this.FadeOutToCloseForm();
        }

        private void cancelButtonPanel_MouseClick(object sender, MouseEventArgs e)
        {
            this.FadeOutToCloseForm();
        }

        private void closeButtonPanel_MouseClick(object sender, MouseEventArgs e)
        {
            this.FadeOutToCloseForm();
        }

        // MouseDown、MouseUp、MouseMove处理随意拖动逻辑
        private static bool isDrag = false;
        private static int mousePositionX = 0;
        private static int mousePositionY = 0;

        private void FeedbackForm_MouseDown(object sender, MouseEventArgs e)
        {
            isDrag = true;
            mousePositionX = e.Location.X;
            mousePositionY = e.Location.Y;
        }

        private void FeedbackForm_MouseUp(object sender, MouseEventArgs e)
        {
            isDrag = false;
            mousePositionX = 0;
            mousePositionY = 0;
        }

        private void FeedbackForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrag)
            {
                this.Left += e.Location.X - mousePositionX;
                this.Top += e.Location.Y - mousePositionY;
            }
        }

        private void FeedbackForm_Shown(object sender, EventArgs e)
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
