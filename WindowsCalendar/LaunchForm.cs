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
using System.Net.NetworkInformation;
using System.Diagnostics;
using System.IO;

namespace WindowsCalendar
{
    public partial class LaunchForm : System.Windows.Forms.Form
    {
        private const int NETWORKTIMEOUT = 5000;
        private const int SLOWPING = 80;

        private bool isNetworkTestingFinished = false;
        private bool isLoadFinished = false;
        private bool isPrepcalWebInfoServiceInitializating = false;
        private bool isPrepcalWebInfoServiceInitialized = false;
        private int netWorkDelay = 0;
        private int loadProgress = 0;

        private bool isTryingToEnterWithoutNetwork = false;

        public static readonly Color normalizedBlue = Color.FromArgb(41, 130, 204);
        public static readonly Color normalizedLightBlue = Color.FromArgb(90, 163, 222);
        public static readonly Color normalizedDarkBlue = Color.FromArgb(31, 99, 154);
        public static readonly Color normalizedGray = Color.FromArgb(75, 75, 75);

        public LaunchForm()
        {
            // 确保窗口在屏幕中央显示
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
        }

        private static bool isAgreementFormClosed = false;

        // 记录协议窗口已关闭（用户已点击“我同意”）
        public static void OnAgreementFormClosed(bool isAgreementSigned)
        {
            if (isAgreementSigned)
            {
                ApplicationStatus.isAgreementSigned = true;
                StoreAgreementSignedFlag();
            }
            isAgreementFormClosed = true;
        }

        private void LaunchForm_Load(object sender, EventArgs e)
        {
            // 初始化窗口样式，隐藏窗口标题以及任务栏显示
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.ShowInTaskbar = false;
            this.Visible = false;

            // 初始化 backgroundPanel
            backgroundPanel.BackColor = normalizedBlue;

            // 初始化 okButtonPanel
            okButtonPanel.BackColor = normalizedBlue;
            okButtonPanel.Visible = false;

            // 初始化 retryButtonPanel
            retryButtonPanel.BackColor = normalizedBlue;
            retryButtonPanel.Visible = false;

            // 初始化 exitButtonPanel
            exitButtonPanel.BackColor = normalizedBlue;
            exitButtonPanel.Visible = false;

            // 初始化 closeButtonPanel
            closeButtonPanel.BackColor = normalizedBlue;

            // 初始化 hintLabel
            hintLabel.Font = new Font("微软雅黑", 10, FontStyle.Bold);
            hintLabel.ForeColor = Color.White;
            hintLabel.BackColor = Color.Transparent;

            // 初始化 loadLabel
            loadLabel.Font = new Font("微软雅黑", 10);
            loadLabel.ForeColor = Color.White;

            // 初始化 progressLabel
            progressLabel.Font = new Font("微软雅黑", 10);
            progressLabel.ForeColor = Color.White;

            // 初始化 switchFormTimer
            switchFormTimer.Interval = 100;
            switchFormTimer.Enabled = true;

            // 初始化 progressTimer
            progressTimer.Interval = 50;
            progressTimer.Enabled = true;

            // 初始化 infoTimer
            infoTimer.Interval = 100;
            infoTimer.Enabled = true;

            // 初始化 agreementTimer
            agreementTimer.Interval = 10;
            agreementTimer.Enabled = true;
        }

        // 保存元数据
        private void StoreMetaData(bool isOverride = false)
        {
            // 保存version
            if (!isOverride)
            {
                if (File.Exists(Path.Combine(ApplicationStatus.applicationDataFolderPath, ApplicationStatus.metaFileName)))
                    throw new FileLoadException(); // 文件已存在，且不允许覆盖写入元数据
            }

            FileStream fs = new FileStream(
                Path.Combine(ApplicationStatus.applicationDataFolderPath, ApplicationStatus.metaFileName), FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(ApplicationStatus.version);
            bw.Close();
        }

        // 读取元数据
        private string ReadMetaData()
        {
            // 读取version
            FileStream fs = new FileStream(
                Path.Combine(ApplicationStatus.applicationDataFolderPath, ApplicationStatus.metaFileName), FileMode.Open);
            BinaryReader br = new BinaryReader(fs);
            string result = br.ReadString();
            br.Close();
            return result;
        }

        // 保存协议已同意信息
        private static void StoreAgreementSignedFlag()
        {
            File.Create(Path.Combine(ApplicationStatus.applicationDataFolderPath, ApplicationStatus.agreementSignedFlagFileName));
        }

        private void LaunchForm_Shown(object sender, EventArgs e)
        {
            // 如果是第一次启动
            if (!Directory.Exists(ApplicationStatus.applicationDataFolderPath))
            {
                // 记录初次启动信息
                ApplicationStatus.isFirstRun = true;

                // 创建程序数据目录
                Directory.CreateDirectory(ApplicationStatus.applicationDataFolderPath);
            }
            else
            {
                // 判断是否有元数据并读取
                if (File.Exists(Path.Combine(ApplicationStatus.applicationDataFolderPath, ApplicationStatus.metaFileName)))
                {
                    string metaData = ReadMetaData();
                    // 元数据中版本号比当前版本低，说明用户更新了版本
                    if (Convert.ToSingle(metaData) < Convert.ToSingle(ApplicationStatus.version))
                    {
                        ApplicationStatus.isVersionUpdated = true;
                        ApplicationStatus.previousVersion = metaData;
                        
                        // 如果存在用户已同意协议的记录，删除该记录
                        if (File.Exists(Path.Combine(ApplicationStatus.applicationDataFolderPath, ApplicationStatus.agreementSignedFlagFileName))) 
                            File.Delete(Path.Combine(ApplicationStatus.applicationDataFolderPath, ApplicationStatus.agreementSignedFlagFileName));
                    }
                    // 正常启动
                    else
                    {

                    }
                }
                else
                {
                    // 没有元数据，说明用户更新了版本，且以前使用的是1.29以下版本
                    ApplicationStatus.isVersionUpdated = true;
                    ApplicationStatus.previousVersion = "below 1.29";

                    // 如果存在用户已同意协议的记录，删除该记录
                    if (File.Exists(Path.Combine(ApplicationStatus.applicationDataFolderPath, ApplicationStatus.agreementSignedFlagFileName)))
                        File.Delete(Path.Combine(ApplicationStatus.applicationDataFolderPath, ApplicationStatus.agreementSignedFlagFileName));
                }
            }

            // 判断是否已同意协议
            if (File.Exists(Path.Combine(ApplicationStatus.applicationDataFolderPath, ApplicationStatus.agreementSignedFlagFileName)))
                ApplicationStatus.isAgreementSigned = true;

            // 判断第一次启动时网络连接是否正常
            if (File.Exists(Path.Combine(ApplicationStatus.applicationDataFolderPath, ApplicationStatus.firstRunNoInternetFlagFileName)))
                ApplicationStatus.isFirstRunNoInternet = true;

            // 若初次启动或已更新，将元数据覆盖写入硬盘
            if (ApplicationStatus.isFirstRun || ApplicationStatus.isVersionUpdated)
                StoreMetaData(true);


            // 初次运行或已更新时弹出协议窗口
            if (ApplicationStatus.isFirstRun || ApplicationStatus.isVersionUpdated || !ApplicationStatus.isAgreementSigned)
            {
                // 载入协议窗口，隐藏启动窗口
                Task awaitingUserAgreementTask = Task.Factory.StartNew(() =>
                {
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        AgreementForm agreementForm = new AgreementForm();
                        agreementForm.Show();
                        this.Visible = false;
                    }));
                });
            }
            else
            {
                // 不需要弹出协议窗口
                OnAgreementFormClosed(true);
            }
        }

        // 切换窗口时钟 周期事件
        private void switchFormTimer_Tick(object sender, EventArgs e)
        {
            if (isLoadFinished || isTryingToEnterWithoutNetwork)
            {
                switchFormTimer.Enabled = false;
                //Form mainForm = new MainForm();
                //mainForm.Show();
                Form transitionForm = new TransitionForm();
                transitionForm.Show();
                this.Visible = false;
            }
        }

        private void backgroundLoadWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // 测试网络连接
            netWorkDelay = TestNetwork();
            isNetworkTestingFinished = true;
            if (netWorkDelay == -1)
            {
                ApplicationStatus.isInternetConnected = false;
                while (isNetworkTestingFinished == false || netWorkDelay == -1)
                {
                    Thread.Sleep(500); // 等待其他线程尝试恢复网络
                    if (this.Visible == false) // LaunchForm已隐藏，说明通过无网络方式进入主界面
                        return;
                }
            }

            // 成功连接网络
            progressTimer.Enabled = true;

            // 如果是初次运行，发送统计信息邮件
            if (ApplicationStatus.isFirstRun)
            {
                LaunchFormUtil.SendStatisticMail("New User: " + LaunchFormUtil.GetClientIpInfo(),
                    "Hard Disk Id: " + Hardware.GetHardDiskID() + "\n  Version: " + ApplicationStatus.version);
            }
            // 更新后发送统计信息邮件
            else if (ApplicationStatus.isVersionUpdated)
            {
                LaunchFormUtil.SendStatisticMail("User App Version Updated: " + LaunchFormUtil.GetClientIpInfo(),
                    "Hard Disk Id: " + Hardware.GetHardDiskID()
                    + "\n  Current Version: " + ApplicationStatus.version 
                    + "\n  Previous Version: " + ApplicationStatus.previousVersion);
            }
            // 第一次启动时网络故障，现在重新发送统计信息邮件
            else if (ApplicationStatus.isFirstRunNoInternet)
            {
                LaunchFormUtil.SendStatisticMail("New User: " + LaunchFormUtil.GetClientIpInfo(),
                    "Hard Disk Id: " + Hardware.GetHardDiskID()
                    + "\n  Version: " + ApplicationStatus.version + "\n  Note: FirstRunNoInternet" );
                // 删除记录初次运行网络故障的flag文件
                File.Delete(Path.Combine(ApplicationStatus.applicationDataFolderPath, 
                    ApplicationStatus.firstRunNoInternetFlagFileName));
            }

            // 加载主窗口资源
            // 刷新DateInfoDatabase
            OnlineDateInfoDatabase.Refresh(DateTime.Today.Year, DateTime.Today.Month);

            // 初始化PrepcalWebInfoService
            isPrepcalWebInfoServiceInitializating = true;
            Thread.Sleep(10); // 给infoTimer提供反应时间
            PrepcalWebInfoService.Initializate();
            isPrepcalWebInfoServiceInitializating = false;
            isPrepcalWebInfoServiceInitialized = true;

            isLoadFinished = true;
        }

        private int sameProgressRemainCount = 0; // 加载时，保持相同进度不动维持的次数（用于backgroundProgressWorker)
        private bool isBackgroundProgressWorkerUIAlreadyInvalidated = false;

        private void backgroundProgressWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (isNetworkTestingFinished == false || netWorkDelay == -1)
            {
                Thread.Sleep(100); // 等待网络测试结束，且网络必须正常
                if (this.Visible == false) // LaunchForm已隐藏，说明通过无网络方式进入主界面
                    return;
            }

            // 长时间进度保持不动，且不在初始化prepcalWebInfoService，不是初次运行（初次运行时发送邮件较慢）
            while (!isLoadFinished && !isPrepcalWebInfoServiceInitializating)
            {
                this.backgroundProgressWorker.ReportProgress(OnlineDateInfoDatabase.InitProgess);
                if (!ApplicationStatus.isFirstRun && ++sameProgressRemainCount >= NETWORKTIMEOUT / 100)
                {
                    if (isBackgroundProgressWorkerUIAlreadyInvalidated == false) // 刷新backgroundPanel
                    {
                        netWorkDelay = -1; // 网络故障，只修改一次
                        backgroundPanel.Invalidate(); // 只刷新一次
                        isBackgroundProgressWorkerUIAlreadyInvalidated = true;
                    }
                }
                Thread.Sleep(100);
            }
        }

        private void backgroundProgressWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (isNetworkTestingFinished && netWorkDelay != -1)
            {
                if (e.ProgressPercentage != loadProgress) // 进度发生改变，重置sameProgressRemainCount
                    sameProgressRemainCount = 0;
                loadProgress = e.ProgressPercentage; // 将加载进度访问权限提升至类内可用
            }
        }

        // 用ping百度的方法测试网络连接，返回ping值。如失败返回-1
        private int TestNetwork()
        {
            //return -1; // DEBUG

            Ping ping = new Ping();
            PingReply reply = null;
            try
            {
                reply = ping.Send("114.55.186.15", NETWORKTIMEOUT); // japi.juhe.cn的IP地址
            }
            catch (PingException) // 发生异常
            {
                return -1;
            }

            if (reply.Status == IPStatus.Success)
            {
                Console.WriteLine("Network Time: " + reply.RoundtripTime);
                return Convert.ToInt32(reply.RoundtripTime);
            }
            // 超时或失败
            return -1;
        }

        private void progressPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawRectangle(Pens.White, 0, 0, progressPanel.Width - 1, progressPanel.Height - 1);
            g.FillRectangle(Brushes.White, 0, 0, (float)loadProgress / 100 * progressPanel.Width, progressPanel.Height);
        }

        private void progressTimer_Tick(object sender, EventArgs e)
        {
            if (loadProgress == 100 && isPrepcalWebInfoServiceInitialized)
                progressTimer.Enabled = false;
            if (isNetworkTestingFinished && netWorkDelay == -1)
                return;
            progressPanel.Invalidate(); // 重绘进度条
            this.progressLabel.Text = loadProgress.ToString() + "%";
            if (isNetworkTestingFinished && netWorkDelay != -1)
            {
                if (!isPrepcalWebInfoServiceInitializating)
                    this.loadLabel.Text = "正在通过api读取当月第" + OnlineDateInfoDatabase.InitCurrentDay + "天的黄历";
                else
                {
                    this.loadLabel.Text = "正在检查更新...";
                    progressTimer.Enabled = false;
                }
            }
        }

        private bool isInfoTimerUIAlreadyInvalidated = false;

        private void infoTimer_Tick(object sender, EventArgs e)
        {
            if (loadProgress == 100)
                infoTimer.Enabled = false;
            if (isNetworkTestingFinished)
            {
                if (netWorkDelay == -1)
                {
                    this.hintLabel.Text = "          您的网络连接有问题噢\n          万年历只能实现部分功能";
                    if (isInfoTimerUIAlreadyInvalidated == false) // 只刷新一次
                    {
                        isInfoTimerUIAlreadyInvalidated = true;
                        backgroundPanel.Invalidate();
                    }
                }
                else if (netWorkDelay > SLOWPING)
                {
                    this.hintLabel.Text = "正在加载中……\n您的网络状况不佳，请稍等。";
                }
                else
                {
                    this.hintLabel.Text = "正在加载，请稍等";
                }
            }
        }


        private int tempCount;

        private void backgroundPanel_Paint(object sender, PaintEventArgs e)
        {
            tempCount++;
            Console.WriteLine("Painting Count: " + tempCount);

            Graphics g = e.Graphics;

            // 网络异常时重绘界面
            if (isNetworkTestingFinished && netWorkDelay == -1)
            {
                // 绘制三角形警示符
                Pen trianglePen = new Pen(Color.White, 2);
                Pen exclamtionPen = new Pen(Color.White, 2); // 感叹号画笔
                Point[] points = {
                        new Point(47, 52),
                        new Point(30, 78),
                        new Point(64, 78) };
                g.DrawPolygon(trianglePen, points);
                g.DrawLine(exclamtionPen, 47, 60, 47, 70);
                g.DrawLine(exclamtionPen, 47, 72, 47, 75);

                // 隐藏加载标签、进度条及百分比标签
                loadLabel.Visible = false;
                progressPanel.Visible = false;
                progressLabel.Visible = false;

                // 显示三个操作按钮
                okButtonPanel.Visible = true;
                retryButtonPanel.Visible = true;
                exitButtonPanel.Visible = true;

            }
            else if (isNetworkTestingFinished && netWorkDelay != -1)
            {
                // 清除所有绘图并填充背景色
                g.Clear(normalizedBlue);

                // 显示加载标签、进度条及百分比标签
                loadLabel.Visible = true;
                progressPanel.Visible = true;
                progressLabel.Visible = true;

                // 隐藏三个操作按钮
                okButtonPanel.Visible = false;
                retryButtonPanel.Visible = false;
                exitButtonPanel.Visible = false;
            }
        }

        private void retryNetWorkWithSleep()
        {
            isNetworkTestingFinished = false;
            Random rand = new Random();
            Stopwatch watch = new Stopwatch();
            watch.Start();
            netWorkDelay = TestNetwork();
            watch.Stop();
            Console.WriteLine("Network Delay: " + netWorkDelay);

            // DEBUG
            //netWorkDelay = 1;

            if (netWorkDelay == -1 || watch.ElapsedMilliseconds < 200)
                Thread.Sleep(rand.Next(250, 600)); // 网络测试在短时间内完成，随机提供延迟，改善UI
            isNetworkTestingFinished = true;
        }


        private readonly Point buttonStringPoint = new Point(16, 5);

        private readonly Pen buttonPen = new Pen(Color.White, 1);
        private readonly Font buttonFont = new Font("微软雅黑", 10);
        private readonly SolidBrush blueBackgroundBrush = new SolidBrush(normalizedBlue);

        private bool isMouseInOkRect = false;
        private bool isMouseInRetryRect = false;
        private bool isMouseInExitRect = false;

        private void okButtonPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (isMouseInOkRect)
            {
                g.DrawRectangle(buttonPen, 0, 0, okButtonPanel.Width - 1, okButtonPanel.Height - 1);
                g.FillRectangle(Brushes.White, okButtonPanel.ClientRectangle);
                g.DrawString("好的(Y)", buttonFont, blueBackgroundBrush, buttonStringPoint);
            }
            else
            {
                g.DrawRectangle(buttonPen, 0, 0, okButtonPanel.Width - 1, okButtonPanel.Height - 1);
                g.DrawString("好的(Y)", buttonFont, Brushes.White, buttonStringPoint);
            }
        }

        private void retryButtonPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (isMouseInRetryRect)
            {
                g.DrawRectangle(buttonPen, 0, 0, retryButtonPanel.Width - 1, retryButtonPanel.Height - 1);
                g.FillRectangle(Brushes.White, retryButtonPanel.ClientRectangle);
                g.DrawString("重试(R)", buttonFont, blueBackgroundBrush, buttonStringPoint);
            }
            else
            {
                g.DrawRectangle(buttonPen, 0, 0, retryButtonPanel.Width - 1, retryButtonPanel.Height - 1);
                g.DrawString("重试(R)", buttonFont, Brushes.White, buttonStringPoint);
            }
        }

        private void exitButtonPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (isMouseInExitRect)
            {
                g.DrawRectangle(buttonPen, 0, 0, exitButtonPanel.Width - 1, exitButtonPanel.Height - 1);
                g.FillRectangle(Brushes.White, exitButtonPanel.ClientRectangle);
                g.DrawString("退出(N)", buttonFont, blueBackgroundBrush, buttonStringPoint);
            }
            else
            {
                g.DrawRectangle(buttonPen, 0, 0, exitButtonPanel.Width - 1, exitButtonPanel.Height - 1);
                g.DrawString("退出(N)", buttonFont, Brushes.White, buttonStringPoint);
            }
        }

        private void okButtonPanel_MouseClick(object sender, MouseEventArgs e)
        {
            if (netWorkDelay != -1 || e.Button == MouseButtons.Right) // 仅当网络不通、按下左键时响应
                return;
            isTryingToEnterWithoutNetwork = true;

            // 如果初次运行时发生网络故障，记录故障信息
            if (ApplicationStatus.isFirstRun)
                File.Create(Path.Combine(ApplicationStatus.applicationDataFolderPath,
                    ApplicationStatus.firstRunNoInternetFlagFileName));

        }

        private void retryButtonPanel_MouseClick(object sender, MouseEventArgs e)
        {
            if (netWorkDelay != -1 || e.Button == MouseButtons.Right) // 仅当网络不通、按下左键时响应
                return;

            isMouseInRetryRect = false; // 优化ui，不高亮显示重试按钮
            retryButtonPanel.Invalidate();

            loadLabel.Visible = true;
            loadLabel.Text = "正在重试...";
            Task t = Task.Factory.StartNew(retryNetWorkWithSleep);
            t.Wait();
            if (netWorkDelay == -1)
            {
                loadLabel.Text = "无法访问网络 :(";
            }
            else
            {
                ApplicationStatus.isInternetConnected = true;
                backgroundPanel.Invalidate();
            }
        }

        private void exitButtonPanel_MouseClick(object sender, MouseEventArgs e)
        {
            if (netWorkDelay != -1 || e.Button == MouseButtons.Right) // 仅当网络不通、按下左键时响应
                return;

            // 如果初次运行时发生网络故障，记录故障信息
            if (ApplicationStatus.isFirstRun)
                File.Create(Path.Combine(ApplicationStatus.applicationDataFolderPath,
                    ApplicationStatus.firstRunNoInternetFlagFileName));

            System.Environment.Exit(0);
        }

        private void okButtonPanel_MouseEnter(object sender, EventArgs e)
        {
            isMouseInOkRect = true;
            okButtonPanel.Invalidate();
        }

        private void okButtonPanel_MouseLeave(object sender, EventArgs e)
        {
            isMouseInOkRect = false;
            okButtonPanel.Invalidate();
        }

        private void retryButtonPanel_MouseEnter(object sender, EventArgs e)
        {
            isMouseInRetryRect = true;
            retryButtonPanel.Invalidate();
        }

        private void retryButtonPanel_MouseLeave(object sender, EventArgs e)
        {
            isMouseInRetryRect = false;
            retryButtonPanel.Invalidate();
        }

        private void exitButtonPanel_MouseEnter(object sender, EventArgs e)
        {
            isMouseInExitRect = true;
            exitButtonPanel.Invalidate();
        }

        private void exitButtonPanel_MouseLeave(object sender, EventArgs e)
        {
            isMouseInExitRect = false;
            exitButtonPanel.Invalidate();
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
            if(e.Button == MouseButtons.Left)
            {
                // 如果初次运行时发生网络故障，记录故障信息
                if (ApplicationStatus.isFirstRun && netWorkDelay == -1)
                    File.Create(Path.Combine(ApplicationStatus.applicationDataFolderPath,
                        ApplicationStatus.firstRunNoInternetFlagFileName));

                System.Environment.Exit(0);
            }
        }

        private void LaunchForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'y')
                okButtonPanel_MouseClick(sender, new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
            else if (e.KeyChar == 'r')
                retryButtonPanel_MouseClick(sender, new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
            else if (e.KeyChar == 'n')
                exitButtonPanel_MouseClick(sender, new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
        }

        private void agreementTimer_Tick(object sender, EventArgs e)
        {
            if (isAgreementFormClosed)
            {
                // 启动后台工作线程、后台进度同步线程
                this.backgroundProgressWorker.WorkerReportsProgress = true;
                this.backgroundLoadWorker.RunWorkerAsync();
                this.backgroundProgressWorker.RunWorkerAsync();

                this.Visible = true;
                agreementTimer.Enabled = false;
            }
        }
    }

    public static class LaunchFormUtil
    {
        /// <summary>
        /// 获取客户端Ip信息
        /// </summary>
        /// <returns></returns>
        public static string GetClientIpInfo()
        {
            const string ipQueryUrl = "http://ip.chinaz.com/getip.aspx";
            return WebServices.GetWebClient(ipQueryUrl);
        }

        /// <summary>
        /// 发送统计邮件
        /// </summary>
        /// <param name="subject">邮件主题</param>
        /// <param name="body">邮件内容</param>
        public static void SendStatisticMail(string subject, string body = null)
        {
            //smtp.163.com
            string senderServerIp = "123.125.50.133";
            string toMailAddress = "prepcal@163.com";
            string fromMailAddress = "prepcal@163.com";
            string subjectInfo = subject;
            string bodyInfo = body;
            string mailUsername = "prepcal";
            string mailPassword = ""; // secret field
            string mailPort = "25";

            EmailService email = new EmailService(senderServerIp, toMailAddress, fromMailAddress,
                subjectInfo, bodyInfo, mailUsername, mailPassword, mailPort, false, false);
            email.Send();

            Console.WriteLine("Mail sent!");
        }
    }
}
