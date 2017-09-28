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

using System.Drawing.Drawing2D;
using System.Net;
using System.IO;
using System.IO.Compression;
using System.Text.RegularExpressions;
using System.Collections;
using System.Net.Mail;
using System.Net.Mime;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using IWshRuntimeLibrary;

namespace WindowsCalendar
{
    public delegate void MyEventHandler();
    public delegate void MyEventHandlerArgs(object o);

    /// <summary>
    /// 主窗口
    /// </summary>
    public partial class MainForm : Form
    {
        public MainForm()
        {
            // 设置双缓冲绘图方式
            //SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer 
                //| ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);

            // 确保窗口在屏幕中央显示
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
        }

        public event MyEventHandlerArgs MainFormLoadFinished; // 主窗口已加载完毕事件，在TransitionForm类中被处理

        private void OnMainFormLoadFinished(object o)
        {
            MainFormLoadFinished?.Invoke(this);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // 先隐藏窗口，待加载完毕后再显示
            //this.Visible = false;

            //Thread.Sleep(2000);

            // 初始化窗口样式，隐藏窗口标题以及任务栏显示
            //this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            //this.ShowInTaskbar = false;
            this.Icon = WindowsCalendar.Properties.Resources.icon;

            // 使主窗口接受热键
            this.KeyPreview = true;

            // 初始化 title
            this.Text = "中华万年历";

            // 初始化 titlePanel
            titlePanel.BackColor = Color.Transparent;

            // 初始化 calendarBoxPanel
            calendarBoxPanel.BackColor = Color.Transparent;

            // 初始化 dateSelectPanel
            dateSelectPanel.BackColor = Color.FromArgb(190, 152, 118, 101);

            // 初始化 dateLabel
            dateLabel.Font = new Font("微软雅黑", 14);
            dateLabel.ForeColor = Color.White;
            dateLabel.BackColor = Color.Transparent;

            // 初始化 weekPanel
            weekPanel.BackColor = Color.FromArgb(190, 176, 175, 175);

            // 初始化 calendarContentPanel
            calendarContentPanel.BackColor = Color.FromArgb(190, 255, 255, 255);
            //calendarContentPanel.AllowDragWindow = false;

            // 初始化 circlePanel
            circlePanel.BackColor = Color.Transparent;
            circlePanel.BackgroundImage = WindowsCalendar.Properties.Resources.circle;
            circlePanel.BackgroundImageLayout = ImageLayout.Zoom;

            // 初始化 detailedInfoPanel
            detailedInfoPanel.BackColor = Color.FromArgb(190, 255, 255, 255);

            // 初始化 suitAndAvoidPanel
            suitAndAvoidPanel.BackColor = Color.FromArgb(190, 255, 255, 255);

            // 初始化 lunarInfoPanel
            lunarInfoPanel.BackColor = Color.FromArgb(190, 255, 255, 255);

            // 初始化 smallMonthCalendar
            smallMonthCalendar.Visible = false;
            smallMonthCalendar.Left = 231;
            smallMonthCalendar.Top = 127;

            // 初始化 notepadTextBox
            notepadTextBox.Visible = false;
            notepadTextBox.Multiline = true;
            notepadTextBox.AcceptsReturn = true;
            notepadTextBox.AcceptsTab = true;
            notepadTextBox.ScrollBars = ScrollBars.Vertical;
            notepadTextBox.Width = 312;
            notepadTextBox.Height = 260;
            notepadTextBox.Left = lunarInfoPanel.Left;
            notepadTextBox.Top = lunarInfoPanel.Top;
            notepadTextBox.Font = new Font("微软雅黑", 9);
            notepadTextBox.BackGroundText = "用双手，记录点滴生活";

            // 初始化 weeklyCalendarContentPanel
            weeklyCalendarContentPanel.Visible = false;
            weeklyCalendarContentPanel.BackColor = Color.FromArgb(190, 255, 255, 255);
            weeklyCalendarContentPanel.Parent = calendarBoxPanel;
            weeklyCalendarContentPanel.Left = calendarContentPanel.Left;
            weeklyCalendarContentPanel.Top = calendarContentPanel.Top;
            weeklyCalendarContentPanel.Width = 580;
            weeklyCalendarContentPanel.Height = 361;

            // 初始化 weeklyCirclePanel
            weeklyCirclePanel.BackColor = Color.Transparent;
            weeklyCirclePanel.BackgroundImage = WindowsCalendar.Properties.Resources.circle;
            weeklyCirclePanel.BackgroundImageLayout = ImageLayout.Zoom;

            // 初始化所有的buttonPanel
            //switchCalendarModeButtonPanel
            //exportTextButtonPanel
            //exportTextSingleButtonPanel
            //exportTextAllButtonPanel
            //moreButtonPanel
            //changeBackgroundButtonPanel
            //createShortcutButtonPanel
            //checkUpdatesButtonPanel
            //viewHomepageButtonPanel
            //supportAuthorPanel
            //feedbackButtonPanel
            //helpButtonPanel

            switchCalendarModeButtonPanel.BackColor = Color.Transparent;
            exportTextButtonPanel.BackColor = Color.Transparent;
            exportTextSingleButtonPanel.BackColor = Color.Transparent;
            exportTextAllButtonPanel.BackColor = Color.Transparent;
            moreButtonPanel.BackColor = Color.Transparent;
            changeBackgroundButtonPanel.BackColor = Color.Transparent;
            createShortcutButtonPanel.BackColor = Color.Transparent;
            checkUpdatesButtonPanel.BackColor = Color.Transparent;
            viewHomepageButtonPanel.BackColor = Color.Transparent;
            supportAuthorPanel.BackColor = Color.Transparent;
            feedbackButtonPanel.BackColor = Color.Transparent;
            helpButtonPanel.BackColor = Color.Transparent;

            switchCalendarModeButtonPanel.Visible = true;
            exportTextButtonPanel.Visible = false;
            exportTextSingleButtonPanel.Visible = false;
            exportTextAllButtonPanel.Visible = false;
            moreButtonPanel.Visible = true;
            changeBackgroundButtonPanel.Visible = false;
            createShortcutButtonPanel.Visible = false;
            checkUpdatesButtonPanel.Visible = false;
            viewHomepageButtonPanel.Visible = false;
            supportAuthorPanel.Visible = false;
            feedbackButtonPanel.Visible = false;
            helpButtonPanel.Visible = true;

            switchCalendarModeButtonPanel.Location = new Point(701, 53);
            exportTextButtonPanel.Location = new Point(701, 53);

            exportTextSingleButtonPanel.Location = new Point(609, 53);
            exportTextAllButtonPanel.Location = new Point(609, 24);

            moreButtonPanel.Location = new Point(883, 53);

            changeBackgroundButtonPanel.Location = new Point(609, 24);
            createShortcutButtonPanel.Location = new Point(701, 24);
            checkUpdatesButtonPanel.Location = new Point(792, 24);
            viewHomepageButtonPanel.Location = new Point(609, 53);
            supportAuthorPanel.Location = new Point(701, 53);
            feedbackButtonPanel.Location = new Point(792, 53);

            helpButtonPanel.Location = new Point(792, 53);

            switchCalendarModeButtonPanel.ButtonText = "切换周历(S)";
            exportTextButtonPanel.ButtonText = "  导出文本";
            exportTextSingleButtonPanel.ButtonText = "    仅当天";
            exportTextAllButtonPanel.ButtonText = "  导出全部";
            moreButtonPanel.ButtonText = "   更多(M)";
            changeBackgroundButtonPanel.ButtonText = "更换背景(B)";
            createShortcutButtonPanel.ButtonText = "放到桌面(D)";
            checkUpdatesButtonPanel.ButtonText = "检查更新(U)";
            viewHomepageButtonPanel.ButtonText = "访问主页(P)";
            supportAuthorPanel.ButtonText = "支持作者(A)";
            feedbackButtonPanel.ButtonText = "意见反馈(F)";
            helpButtonPanel.ButtonText = "   帮助(H)";

            supportAuthorPanel.Style = ButtonPanelStyle.Highlighted;


            // 初始化 backgroundPanel
            backgroundPanel.Height = this.ClientRectangle.Height;
            backgroundPanel.Width = this.ClientRectangle.Width;

            string backgroundImageSavingFilePath = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    OfflineDateInfoStructure.APPLICATIONDATADIRECTORY, BACKGROUNDIMAGESAVINGFILENAME);

            if (System.IO.File.Exists(backgroundImageSavingFilePath))
            {
                backgroundPanel.BackgroundImage = Image.FromFile(backgroundImageSavingFilePath);
                backgroundPanel.BackgroundImageLayout = ImageLayout.Stretch;
                changeBackgroundButtonPanel.ButtonText = "默认背景(B)";
            }
            else
            {
                backgroundPanel.BackgroundImage = WindowsCalendar.Properties.Resources.background;
                backgroundPanel.BackgroundImageLayout = ImageLayout.Zoom;
                changeBackgroundButtonPanel.ButtonText = "更换背景(B)";
            }

            // 初始化 notifyIcon
            notifyIcon.Icon = WindowsCalendar.Properties.Resources.icon;
            notifyIcon.Visible = false;

            // 初始化 closeButtonPanel
            closeButtonPanel.BackColor = Color.Transparent;

            // 初始化 minimizeButtonPanel
            minimizeButtonPanel.BackColor = Color.Transparent;

            // 初始化 helpImagePanel
            helpImagePanel.Visible = false;
            helpImagePanel.Left = 0;
            helpImagePanel.Top = 0;
            helpImagePanel.Width = this.ClientRectangle.Width;
            helpImagePanel.Height = this.ClientRectangle.Height;

            // 初始化 nextHelpImageButtonPanel
            nextHelpImageButtonPanel.Visible = false;
            nextHelpImageButtonPanel.BackColor = Color.Transparent;
            nextHelpImageButtonPanel.Parent = this.helpImagePanel;
            nextHelpImageButtonPanel.ButtonText = "  下一步(N)";

            // 初始化 exitHelpModeButtonPanel
            exitHelpModeButtonPanel.Visible = false;
            nextHelpImageButtonPanel.BackColor = Color.Transparent;
            exitHelpModeButtonPanel.Parent = this.helpImagePanel;
            exitHelpModeButtonPanel.ButtonText = "跳过引导(E)";

            // 初始化 closeButton
            closeButton.Visible = false;
            closeButton.Text = "Close";
            closeButton.FlatStyle = FlatStyle.Flat;

            // 初始化 debugButoon
            debugButton.Visible = false;

            // 初始化 debugTimer
#if DEBUG
            //debugTimer.Enabled = true;
            //debugTimer.Interval = 500;
#endif
        }

        // 当前显示的月份改变事件，在MainForm和DynamicApiQueryQueue中被处理
        public static event MyEventHandlerArgs CurrentShowingMonthChanged;
        // 当前指定的日期改变事件，在MainForm中被处理
        public static event MyEventHandler CurrentSelectedDateChanged;

        private static void OnCurrentShowingMonthChanged(DateMonth dateMonth)
        {
            CurrentShowingMonthChanged?.Invoke(dateMonth);
        }

        private static void OnCurrentSelectedDateChanged()
        {
            CurrentSelectedDateChanged?.Invoke();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            Console.WriteLine("MainForm Shown!");

            // 读取OfflineDateInfoStructure，正常情况下不会失败，否则抛出异常
            if (!OfflineDateInfoStructure.LoadDateInfos())
                throw new InvalidOperationException();

            // OnlineDateInfoDatabase 事件布线
            MainForm.CurrentShowingMonthChanged += new MyEventHandlerArgs(CurrentShowingMonthChangedHandler);
            OnlineDateInfoDatabase.NewMonthQueried += new MyEventHandlerArgs(NewMonthQueriedHandler);
            // 事件布线
            MainForm.CurrentSelectedDateChanged += new MyEventHandler(CurrentSelectedDateChangedHandler);

            // 初始化notifyIcon
            InitializateNotifyIcon();

            // 如果网络状况良好，初始化OnlineDateInfoDatabase和DynamicApiQueryQueue
            if (ApplicationStatus.isInternetConnected)
            {
                OnlineDateInfoDatabase.Initialize();
                DynamicApiQueryQueue.Initialize(new DateMonth(currentSelectedDate.Year, currentSelectedDate.Month));
            }

            // 初始化NotepadStorage
            NotepadStorage.Initialize();
            //throw new NotImplementedException();

            // 启动PlusInCalendarContentPanelWiper任务
            Task plusInCalendarContentPanelWiperTask = Task.Factory.StartNew(PlusInCalendarContentPanelWiper);

            // 加载完毕，显示窗口
            this.Visible = true;
            OnMainFormLoadFinished(this);

            // 如果网络状况良好，异步检查更新
            if (ApplicationStatus.isInternetConnected)
            {
                //Task checkUpdateTask = Task.Factory.StartNew(() =>
                //{
                //    Thread.Sleep(3000); // 等待过渡窗口退出前台，主界面完全显示
                    checkUpdate(false);
                //});
            }

            // 如果是初次启动或刚升级新版本，显示新手引导界面
            if (ApplicationStatus.isFirstRun || ApplicationStatus.isVersionUpdated)
            {
                helpButtonPanel_MouseClick(sender, new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
            }
        }

        // CurrentShowingMonthChanged事件处理程序
        private void CurrentShowingMonthChangedHandler(object o)
        {
            DateMonth dateMonth = o as DateMonth;
            if (dateMonth == null)
                throw new ArgumentException();
            currentSelectedDate = new DateTime(dateMonth.Year, dateMonth.Month, currentSelectedDate.Day);

            OnCurrentSelectedDateChanged();
        }

        // CurrentSelectedDateChanged事件处理程序
        private void CurrentSelectedDateChangedHandler()
        {
            // 全部重绘
            dateSelectPanel.Invalidate();
            calendarContentPanel.Invalidate();
            detailedInfoPanel.Invalidate();
            suitAndAvoidPanel.Invalidate();
            lunarInfoPanel.Invalidate();
        }

        // newMonthQueried事件处理程序
        private void NewMonthQueriedHandler(object o)
        {
            calendarContentPanel.Invalidate();
            detailedInfoPanel.Invalidate();
        }

        private void PlusInCalendarContentPanelWiper()
        {
            while (true)
            {
                Thread.Sleep(500); // 每隔0.5秒刷新一次
                calendarContentPanel.Invalidate();
            }
        }

        private void Form1_TextChanged(object sender, EventArgs e)
        {
            //// 如果窗口标题不包含版权信息，则增加版权信息
            //if (this.Text.IndexOf(Copyright.info.Substring(0, 6)) == -1)
            //    this.Text += Copyright.info.PadLeft(Copyright.info.Length + 4);
        }

        // 背景画板
        private void backgroundPanel_Paint(object sender, PaintEventArgs e)
        {
            // 绘制GDI+字符串
            //Graphics g = e.Graphics;
            
            //// 设置颜色渐变起止位置
            //Point p1 = new Point(250, 250);
            //Point p2 = new Point(650, 285);
            //LinearGradientBrush brush = new LinearGradientBrush(p1, p2, Color.Red, Color.Green);
            //Font font = new Font("宋体", 9);
            //Point point = new Point(420, 30);
            //g.DrawString("这是使用GDI+书写的字符串", font, brush, point);
            ////g.FillRectangle(brush, 250, 250, 400, 35);
            ////SolidBrush normalBrush = new SolidBrush(Color.Black);
            ////g.DrawString("这是使用GDI+书写的字符串", font, normalBrush, point);
        }

        // 标题画板
        private void titlePanel_Paint(object sender, PaintEventArgs e)
        {
            string titleText = isMonthlyCalendar ? "中华万年历 - 月历" : "中华万年历 - 周历";
            RectangleF titleRect = new RectangleF(0, 0, 400, 200);
            //Font titleFont = new Font("黑体-简", 24, FontStyle.Bold);

            // 用资源目录下的黑体-简字体显示标题

            // 以下代码出处（有删改）：http://bbs.csdn.net/topics/340011740
            // 版权归原作者所有
            PrivateFontCollection privateFonts = new PrivateFontCollection();
            byte[] byteFont = new byte[WindowsCalendar.Properties.Resources.STHeiti_Light.Length];
            IntPtr MeAdd = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(byte)) * byteFont.Length);
            Marshal.Copy(WindowsCalendar.Properties.Resources.STHeiti_Light, 0, MeAdd, WindowsCalendar.Properties.Resources.STHeiti_Light.Length);
            privateFonts.AddMemoryFont(MeAdd, byteFont.Length);
            Font titleFont = new Font(privateFonts.Families[0], 24, FontStyle.Bold);

            Pen outlinePen = new Pen(Color.FromArgb(206, 119, 255), 2);
            MainFormUtil.DrawStrokedText(e, titleText, titleRect, titleFont, Brushes.White, outlinePen);
        }

        // 日期选择画板
        private void dateSelectPanel_Paint(object sender, PaintEventArgs e)
        {
            // 写入日期文本
            //string dateText = DateTime.Now; // 2016/8/18 11:49:42
            DateTime date = currentSelectedDate;
            string dateText = date.Year.ToString().PadLeft(4, '0') + "年"
                + date.Month.ToString().PadLeft(2, '0') + "月"
                + date.Day.ToString().PadLeft(2, '0') + "日";
            dateLabel.Text = dateText;

            Graphics g = e.Graphics;

            //如果在月历模式下，绘制按钮
            if (isMonthlyCalendar)
            {
                // 绘制中部三角形按钮
                PointF[] centralTrianglePoints = {
                new PointF(dateLabel.Location.X + dateLabel.Size.Width + 10, 12),
                new PointF(dateLabel.Location.X + dateLabel.Size.Width + 28, 12),
                new PointF(dateLabel.Location.X + dateLabel.Size.Width + 19, 24) };
                g.FillPolygon(Brushes.White, centralTrianglePoints);

                // 绘制左侧三角形按钮
                PointF[] leftTrianglePoints = {
                new PointF(11, 19),
                new PointF(24, 10),
                new PointF(24, 28)
                };
                g.FillPolygon(Brushes.White, leftTrianglePoints);

                // 绘制右侧三角形按钮
                PointF[] rightTrianglePoints = {
                new PointF(dateSelectPanel.Width - 11, dateSelectPanel.Height - 19 + 3),
                new PointF(dateSelectPanel.Width - 24, dateSelectPanel.Height - 28 + 3),
                new PointF(dateSelectPanel.Width - 24, dateSelectPanel.Height - 10 + 3)
                };
                g.FillPolygon(Brushes.White, rightTrianglePoints);
            }

            // 如果是周历模式，绘制当前周数
            if (!isMonthlyCalendar)
            {
                Font weekCountFont = new Font("微软雅黑", 14);
                int weekOfYear = DateTime.Today.DayOfYear / 7 + 1;
                g.DrawString("第" + weekOfYear.ToString() + "周", weekCountFont, Brushes.White, new Point(100, 7));
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            FadeOutToExit();
        }

        private void debugTimer_Tick(object sender, EventArgs e)
        {
            Console.WriteLine((MousePosition.X - this.Left) + "  " + (MousePosition.Y - this.Top));
        }

        /// <summary>
        /// 检查更新
        /// </summary>
        /// <param name="hasInfo">是否弹出更新提示</param>
        private void checkUpdate(bool hasInfo)
        {
            if (!ApplicationStatus.isInternetConnected || ApplicationStatus.latestVersion == "")
            {
                if (hasInfo)
                    MessageBox.Show("您的网络连接有问题噢，请检查后重试！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            // 异常：未初始化
            if (!PrepcalWebInfoService.IsInitialized)
                throw new InvalidOperationException();

            if (Convert.ToSingle(ApplicationStatus.version) < Convert.ToSingle(ApplicationStatus.latestVersion))
            {
                if (MessageBox.Show("发现新版本，是否前往更新？\n当前版本： Ver " + ApplicationStatus.version + "\n最新版本： Ver " + ApplicationStatus.latestVersion
                    + "\n为了确保万年历平稳运行，建议先删除旧版本。",
                    "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk)
                    == DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start(ApplicationStatus.updateDownloadUrl);
                    FadeOutToExit();
                }
            }
            else if (hasInfo)
            {
                if (Convert.ToSingle(ApplicationStatus.version) == Convert.ToSingle(ApplicationStatus.latestVersion))
                {
                    MessageBox.Show("当前已是最新版本 Ver " + ApplicationStatus.version + "，感谢您的关注。\n我们将持续为您提供高品质的服务！",
                        "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    MessageBox.Show("您正在使用最新内测版本 Ver " + ApplicationStatus.version + "，感谢您的关注。\n我们将持续为您提供高品质的服务！",
                        "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
        }

        // 日历内容画板
        private void calendarContentPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen frameworkPen = new Pen(Color.FromArgb(176, 175, 175), 2);
            // 绘制基础框架：列*7
            float currentSeparateWidth;
            for (int i = 0; i < 6; i++)
            {
                currentSeparateWidth = (float)calendarContentPanel.Width / 7 * (i + 1);
                g.DrawLine(frameworkPen,
                            new PointF(currentSeparateWidth, 0),
                            new PointF(currentSeparateWidth, (float)calendarContentPanel.Height));
            }
            // 绘制基础框架：行*6
            float currentSeparateHeight;
            for (int i = 0; i < 5; i++)
            {
                currentSeparateHeight = (float)calendarContentPanel.Height / 6 * (i + 1);
                g.DrawLine(frameworkPen,
                            new PointF(0, currentSeparateHeight),
                            new PointF((float)calendarContentPanel.Width, currentSeparateHeight));
            }

            // 加深今天的背景颜色
            // 仅当当前显示本月时绘制
            DateTime today = DateTime.Today;
            if (currentSelectedDate.Year == today.Year && currentSelectedDate.Month == today.Month)
            {
                int todayRow;
                int todayColumn;
                GetDayPositionInCalendarContentPanel(out todayRow, out todayColumn, DateTime.Today); // 得到今天在日历中的位置
                SolidBrush todayBackgroundBrush = new SolidBrush(Color.FromArgb(253, 245, 209));
                RectangleF todayRect = new RectangleF(
                    todayColumn * ((float)calendarContentPanel.Width / 7) + 2,
                    todayRow * ((float)calendarContentPanel.Height / 6) + 2,
                    (float)calendarContentPanel.Width / 7 - 3,
                    (float)calendarContentPanel.Height / 6 - 3);
                g.FillRectangle(todayBackgroundBrush, todayRect);
            }

            // 绘制日期数字
            int daysOfMonth = DateTime.DaysInMonth(currentSelectedDate.Year, currentSelectedDate.Month); // 该月的天数
            int weekOfMonthFirstDay = (int)DateTime.Parse(
                currentSelectedDate.Year.ToString() + "/" + currentSelectedDate.Month.ToString() + "/1"
                ).DayOfWeek; // 该月第一天星期几（0~6）

            PointF[] numberPositions = new PointF[daysOfMonth];
            int rowOfNumbers = 0; // 绘制时，日历表格中的当前行数
            int[] firstIndexsInRows = new int[6]; // 表示后5行的第一个索引数（day-1），仅需用到后五个元素，首元素恒为0
            int numbersInCurrentRow = 0; // 表示当前行的数字数量

            const float fixedNumberCrosswiseDistance = 27;
            const float fixedNumberLengthwaysDistance = 15;
            const float fixedNumberAdditionalCrosswiseDistanceForSingleFigure = 5; // 为单个数字（0~9）增加的横向宽度

            Font numberFont = new Font("微软雅黑", 15);
            SolidBrush numberNormalBrush = new SolidBrush(LaunchForm.normalizedGray);
            SolidBrush numberHolidayBrush = new SolidBrush(Color.FromArgb(238, 119, 0));

            // 根据本月的第一天是星期几来绘制第一个数字
            firstIndexsInRows[0] = numbersInCurrentRow = weekOfMonthFirstDay;
            for (int i = 0; i < daysOfMonth; i++)
            {
                if (numbersInCurrentRow >= 7) // 如果该行元素数量超过7个，则换行绘制
                {
                    rowOfNumbers++;
                    numbersInCurrentRow = 0;
                    firstIndexsInRows[rowOfNumbers] = i;
                }
                numberPositions[i].X = numbersInCurrentRow * ((float)calendarContentPanel.Width / 7) + fixedNumberCrosswiseDistance;
                numberPositions[i].Y = fixedNumberLengthwaysDistance + rowOfNumbers * ((float)calendarContentPanel.Height / 6);
                if (i < 9) // 为单个数字（0~9）增加横向宽度
                    numberPositions[i].X += fixedNumberAdditionalCrosswiseDistanceForSingleFigure;

                // 判断是否为节假日并用不同画刷绘制
                int weekOfCurrentDate = (int)DateTime.Parse(
                    currentSelectedDate.Year.ToString() + "/" + currentSelectedDate.Month.ToString() + "/" + (i + 1).ToString()
                    ).DayOfWeek;
                if (weekOfCurrentDate == 0 || weekOfCurrentDate == 6)
                    g.DrawString((i + 1).ToString(), numberFont, numberHolidayBrush, numberPositions[i]);
                else
                    g.DrawString((i + 1).ToString(), numberFont, numberNormalBrush, numberPositions[i]);

                // 增加本行的数字数量
                numbersInCurrentRow++;
            }

            // 绘制农历文字
            PointF[] lunarPositions = new PointF[daysOfMonth];
            int rowOfLunars = 0; // 绘制时，日历表格中的当前行数
            int lunarsInCurrentRow = 0; // 表示当前行的数字数量

            const float fixedLunarLengthwaysDistance = 40;
            const float fixedLunarCrosswiseDistance = 27;

            Font lunarFont = new Font("微软雅黑", 10);
            SolidBrush lunarBrush = new SolidBrush(LaunchForm.normalizedGray);
            SolidBrush lunarHolidayBrush = new SolidBrush(Color.FromArgb(238, 119, 0));

            // 根据本月的第一天是星期几来绘制第一个农历文字
            lunarsInCurrentRow = weekOfMonthFirstDay;
            for (int i = 0; i < daysOfMonth; i++)
            {
                if (lunarsInCurrentRow >= 7) // 如果该行元素数量超过7个，则换行绘制
                {
                    rowOfLunars++;
                    lunarsInCurrentRow = 0;
                    firstIndexsInRows[rowOfNumbers] = i;
                }
                lunarPositions[i].X = lunarsInCurrentRow * ((float)calendarContentPanel.Width / 7) + fixedLunarCrosswiseDistance;
                lunarPositions[i].Y = fixedLunarLengthwaysDistance + rowOfLunars * ((float)calendarContentPanel.Height / 6);

                DateTime currentDate = DateTime.Parse(
                    currentSelectedDate.Year.ToString() + "/" + currentSelectedDate.Month.ToString() + "/" + (i + 1).ToString()
                    );

                // 网络连接正常且数据库中该月已初始化时，使用在线数据库，并判断该天有节假日，如果是，则显示节假日而不显示农历文字；
                // 否则使用离线文件
                if (ApplicationStatus.isInternetConnected 
                    && OnlineDateInfoDatabase.searchMonth(currentDate.Year, currentDate.Month) != -1)
                {
                    string holidayInfo = OnlineDateInfoDatabase.GetHoliday(currentDate);
                    if (holidayInfo == "")
                    {
                        g.DrawString(OnlineDateInfoDatabase.GetLunarCalendarDay(currentDate), lunarFont, lunarBrush, lunarPositions[i]);
                    }
                    else
                    {
                        g.DrawString(holidayInfo, lunarFont, lunarHolidayBrush, lunarPositions[i]);
                    }
                }
                else if (currentDate >= OfflineDateInfoStructure.STARTDATE && currentDate <= OfflineDateInfoStructure.ENDDATE)
                {
                    g.DrawString(OfflineDateInfoStructure.GetLunarDay(currentDate), lunarFont, lunarBrush, lunarPositions[i]);
                }
                
                // 增加本行的农历文字数量
                lunarsInCurrentRow++;
            }

            // 移动circlePanel的位置到今天
            MoveCirclePanelToDate();

            // 在已有记事本数据的日期右上角绘制提示标记
            DateTime currentLoopDate = new DateTime(currentSelectedDate.Year, currentSelectedDate.Month, 1);
            int[,] daysInPanel = GetDaysInPanel();

            for (int i = 0; i < daysOfMonth; i++)
            {
                currentLoopDate = currentLoopDate.AddDays(1);
                if (NotepadHashtable.Exists(currentLoopDate))
                {
                    for (int j = 0; j < 6; j++)
                    {
                        for (int k = 0; k < 7; k++)
                        {
                            if (daysInPanel[j, k] == currentLoopDate.Day)
                            {
                                // 绘制三角形
                                PointF[] topRightTrianglePoints = {
                                    new Point(60, 0),
                                    new Point(82, 0),
                                    new Point(82, 22)
                                };

                                for (int m = 0; m < 3; m++)
                                {
                                    topRightTrianglePoints[m].X += (float)calendarContentPanel.ClientRectangle.Width / 7 * k;
                                    topRightTrianglePoints[m].Y += (float)calendarContentPanel.ClientRectangle.Height / 6 * j;
                                }

                                SolidBrush topRightMemoryTriangleBrush = new SolidBrush(Color.FromArgb(255, 189, 140)); // 255, 255, 140
                                g.FillPolygon(topRightMemoryTriangleBrush, topRightTrianglePoints);
                            }
                        }
                    }
                }
            }

            // 如果鼠标在日历画板上停留，绘制加号和三角形背景
            if (drawPlusInRow != -1 && drawPlusInColoum != -1)
            {
                // 绘制三角形
                PointF[] topRightTrianglePoints = {
                    new Point(60, 0),
                    new Point(82, 0),
                    new Point(82, 22)
                };
                for (int i = 0; i < 3; i++)
                {
                    topRightTrianglePoints[i].X += (float)calendarContentPanel.ClientRectangle.Width / 7 * drawPlusInColoum;
                    topRightTrianglePoints[i].Y += (float)calendarContentPanel.ClientRectangle.Height / 6 * drawPlusInRow;
                }

                SolidBrush topRightMouseMovingBrush = new SolidBrush(Color.FromArgb(255, 189, 140));
                g.FillPolygon(topRightMouseMovingBrush, topRightTrianglePoints);

                // 绘制加号
                const float fixedPlusCrosswiseDistance = 71;
                const float fixedPlusLengthwaysDistance = 3;
                const float fixedPlusWidth = 8;
                const float fixedPlusHeight = 8;

                PointF plusHorizontalStartPoint = new PointF(
                    (float)calendarContentPanel.ClientRectangle.Width / 7 * drawPlusInColoum + fixedPlusCrosswiseDistance,
                    (float)calendarContentPanel.ClientRectangle.Height / 6 * drawPlusInRow + fixedPlusLengthwaysDistance + fixedPlusHeight / 2
                    );
                PointF plusHorizontalEndPoint = new PointF(
                    (float)plusHorizontalStartPoint.X + fixedPlusWidth,
                    (float)plusHorizontalStartPoint.Y
                    );
                PointF plusVerticalStartPoint = new PointF(
                    (float)calendarContentPanel.ClientRectangle.Width / 7 * drawPlusInColoum + fixedPlusCrosswiseDistance + fixedPlusWidth / 2,
                    (float)calendarContentPanel.ClientRectangle.Height / 6 * drawPlusInRow + fixedPlusLengthwaysDistance
                    );
                PointF plusVerticalEndPoint = new PointF(
                    (float)plusVerticalStartPoint.X,
                    (float)plusVerticalStartPoint.Y + fixedPlusHeight
                    );

                g.DrawLine(Pens.White, plusHorizontalStartPoint, plusHorizontalEndPoint);
                g.DrawLine(Pens.White, plusVerticalStartPoint, plusVerticalEndPoint);
            }
        }

        private void weekPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // 绘制“周日”~“周六”文本
            const float translationDistance = 27;
            const float fixedLengthwaysDistance = 4; // previously = 8
            string[] sampleText = { "周日", "周一", "周二", "周三", "周四", "周五", "周六" };
            float[] separatePosition = new float[7];
            Font font = new Font("微软雅黑", 10) ;
            for (int i = 0; i < 7; i++)
            {
                separatePosition[i] = (float)calendarContentPanel.Width / 7 * i;
                g.DrawString(sampleText[i], 
                            font, 
                            Brushes.White, 
                            new PointF(translationDistance + separatePosition[i],
                                        fixedLengthwaysDistance));
            }
        }

        private void debugButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine(OnlineDateInfoDatabase.GetLunarCalendarMonthDay(DateTime.Today));
            Console.WriteLine(OnlineDateInfoDatabase.GetLunarCalendarDay(DateTime.Today));
            //notepadTextBox.Visible = true;
            Console.WriteLine(Hardware.GetHardDiskID());
        }

        private bool isDetailedInfoPanelInited = false;
        private bool isSuitAndAvoidPanelInited = false;

        // calendarContentPanel中当前选中日期
        private DateTime currentSelectedDate = DateTime.Today;

        private void detailedInfoPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (isDetailedInfoPanelInited == false)
            {
                // 画两个小的半圆
                GraphicsPath path = new GraphicsPath();
                path.AddEllipse(8, -8, 16, 16);
                path.AddEllipse(124, -8, 16, 16);

                //新建一个区域对象
                Region region = new Region(this.detailedInfoPanel.ClientRectangle);
                //减去两个半圆路径
                region.Xor(path);
                //赋给Panel的区域对象
                this.detailedInfoPanel.Region = region;

                isDetailedInfoPanelInited = true;
            }

            // 绘制日期与星期几
            string dayOfWeek;
            switch ((int)currentSelectedDate.DayOfWeek)
            {
                case 0: dayOfWeek = "日"; break;
                case 1: dayOfWeek = "一"; break;
                case 2: dayOfWeek = "二"; break;
                case 3: dayOfWeek = "三"; break;
                case 4: dayOfWeek = "四"; break;
                case 5: dayOfWeek = "五"; break;
                case 6: dayOfWeek = "六"; break;
                default: throw new ArgumentOutOfRangeException();
            }

            // 为单个数字增加横向偏移宽度
            float fixedCrosswiseWidth = 0;
            if (currentSelectedDate.Month.ToString().Length == 1)
                fixedCrosswiseWidth += 5;
            if (currentSelectedDate.Day.ToString().Length == 1)
                fixedCrosswiseWidth += 5;

            string dateAndWeek = currentSelectedDate.Year + "年" + currentSelectedDate.Month + "月"
                + currentSelectedDate.Day + "日" + " " + "星期" + dayOfWeek;
            Font dateFont = new Font("微软雅黑", 9);
            g.DrawString(dateAndWeek, dateFont, new SolidBrush(LaunchForm.normalizedGray), 6 + fixedCrosswiseWidth, 20);

            // 绘制农历日期文字，网络连接正常且在线数据库中该月已初始化时使用数据库，否则使用离线文件
            if (ApplicationStatus.isInternetConnected 
                && OnlineDateInfoDatabase.searchMonth(currentSelectedDate.Year, currentSelectedDate.Month) != -1)
            {
                g.DrawString(
                    OnlineDateInfoDatabase.GetLunarCalendarDay(currentSelectedDate),
                    new Font("微软雅黑", 20),
                    Brushes.Orange,
                    new Point(42, 45)
                    );
            }
            else if (currentSelectedDate >= OfflineDateInfoStructure.STARTDATE && currentSelectedDate <= OfflineDateInfoStructure.ENDDATE)
            {
                g.DrawString(
                    OfflineDateInfoStructure.GetLunarDay(currentSelectedDate),
                    new Font("微软雅黑", 20),
                    Brushes.Orange,
                    new Point(42, 45)
                    );
            }

            // 绘制农历年份+生肖文字，网络连接正常常且在线数据库中该月已初始化时使用数据库，否则使用离线文件
            string lunarYearCalendar;
            string animalsYearCalendar;
            string mixedString;

            SolidBrush grayBrush = new SolidBrush(LaunchForm.normalizedGray);

            if (ApplicationStatus.isInternetConnected 
                && OnlineDateInfoDatabase.searchMonth(currentSelectedDate.Year, currentSelectedDate.Month) != -1)
            {
                lunarYearCalendar = OnlineDateInfoDatabase.GetLunarYearCalendar(currentSelectedDate);
                animalsYearCalendar = OnlineDateInfoDatabase.GetAnimalsYearCalendar(currentSelectedDate);

                // 数据有效时才绘制
                if (lunarYearCalendar.IndexOf("年") != -1)
                {
                    mixedString = lunarYearCalendar.Insert(lunarYearCalendar.IndexOf("年"), animalsYearCalendar);
                    g.DrawString(
                        mixedString,
                        new Font("微软雅黑", 10),
                        grayBrush,
                        new Point(32, 90)
                        );
                }
            }
            else if (currentSelectedDate >= OfflineDateInfoStructure.STARTDATE && currentSelectedDate <= OfflineDateInfoStructure.ENDDATE)
            {
                lunarYearCalendar = OfflineDateInfoStructure.GetSuiCi(currentSelectedDate).Substring(0, 3);
                animalsYearCalendar = OfflineDateInfoStructure.GetAnimal(currentSelectedDate);

                mixedString = lunarYearCalendar.Insert(lunarYearCalendar.IndexOf("年"), animalsYearCalendar);
                g.DrawString(
                    mixedString,
                    new Font("微软雅黑", 10),
                    grayBrush,
                    new Point(32, 90)
                    );
            }

            // 绘制农历月份文字，网络连接正常且在线数据库中该月已初始化时使用数据库，否则使用离线文件
            if (ApplicationStatus.isInternetConnected 
                    && OnlineDateInfoDatabase.searchMonth(currentSelectedDate.Year, currentSelectedDate.Month) != -1)
            {
                g.DrawString(
                    OnlineDateInfoDatabase.GetLunarCalendarMonth(currentSelectedDate),
                    new Font("微软雅黑", 10),
                    grayBrush,
                    new Point(87, 90)
                    );
            }
            else if (currentSelectedDate >= OfflineDateInfoStructure.STARTDATE && currentSelectedDate <= OfflineDateInfoStructure.ENDDATE)
            {
                g.DrawString(
                    OfflineDateInfoStructure.GetLunarMonth(currentSelectedDate),
                    new Font("微软雅黑", 10),
                    grayBrush,
                    new Point(87, 90)
                    );
            }

            // 绘制岁次
            if (currentSelectedDate >= OfflineDateInfoStructure.STARTDATE && currentSelectedDate <= OfflineDateInfoStructure.ENDDATE)
            {
                string pattern = @"\w+年 (?<result>\w+月 \w+日)";
                Regex reg = new Regex(pattern, RegexOptions.ExplicitCapture);
                string suiciWithoutYear = reg.Match(OfflineDateInfoStructure.GetSuiCi(currentSelectedDate)).Groups["result"].Value;

                g.DrawString(
                    suiciWithoutYear,
                    new Font("微软雅黑", 10),
                    grayBrush,
                    new Point(32, 110)
                    );
            }

            // 如果所选日期不在在线数据库有效范围内，显示“暂无信息”文本
            if (currentSelectedDate.CompareTo(OnlineDateInfoDatabase.MINDATE) < 0
                || currentSelectedDate.CompareTo(OnlineDateInfoDatabase.MAXDATE) > 0)
            {
                g.DrawString(
                    "暂无信息",
                    new Font("微软雅黑", 10),
                    grayBrush,
                    new Point(48, 76)
                );
            }
        }

        private void suitAndAvoidPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            if (isSuitAndAvoidPanelInited == false)
            {
                // 画两个小的半圆
                GraphicsPath path = new GraphicsPath();
                path.AddEllipse(8, -8, 16, 16);
                path.AddEllipse(124, -8, 16, 16);

                //新建一个区域对象
                Region reg = new Region(this.suitAndAvoidPanel.ClientRectangle);
                //减去两个半圆路径
                reg.Xor(path);
                //赋给Panel的区域对象
                this.suitAndAvoidPanel.Region = reg;

                isSuitAndAvoidPanelInited = true;
            }

            // “宜”方块和“忌”方块的矩形坐标
            Rectangle yiRect = new Rectangle(8, 15, 30, 30);
            Rectangle jiRect = new Rectangle(8, 85, 30, 30);
            Rectangle yiTextRect = new Rectangle(43, 15, 103, 55);
            Rectangle jiTextRect = new Rectangle(43, 85, 103, 45);

            SolidBrush grayBrush = new SolidBrush(LaunchForm.normalizedGray);

            // 绘制“宜”方块
            g.FillRectangle(Brushes.LimeGreen, yiRect);
            g.DrawString("宜", new Font("微软雅黑", 11), Brushes.White, yiRect.X + 5, yiRect.Y + 5);

            // 绘制“忌”方块
            g.FillRectangle(Brushes.IndianRed, jiRect);
            g.DrawString("忌", new Font("微软雅黑", 11), Brushes.White, jiRect.X + 5, jiRect.Y + 5);

            if (currentSelectedDate >= OfflineDateInfoStructure.STARTDATE && currentSelectedDate <= OfflineDateInfoStructure.ENDDATE)
            {
                // 绘制宜的文本
                g.DrawString(OfflineDateInfoStructure.GetSuit(currentSelectedDate),
                new Font("微软雅黑", 8), grayBrush, yiTextRect);

                // 绘制忌的文本
                g.DrawString(OfflineDateInfoStructure.GetAvoid(currentSelectedDate),
                    new Font("微软雅黑", 8), grayBrush, jiTextRect);
            }
            else
            {
                g.DrawString("暂无信息",
                new Font("微软雅黑", 8), grayBrush, yiTextRect);

                g.DrawString("暂无信息",
                    new Font("微软雅黑", 8), grayBrush, jiTextRect);
            }
        }

        private void lunarInfoPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Font universalFont = new Font("微软雅黑", 10);
            SolidBrush grayBrush = new SolidBrush(LaunchForm.normalizedGray);

            if (currentSelectedDate >= OfflineDateInfoStructure.STARTDATE && currentSelectedDate <= OfflineDateInfoStructure.ENDDATE)
            {
                // 绘制冲+煞
                g.DrawString("冲:", universalFont, grayBrush, 60, 10);
                g.DrawString(OfflineDateInfoStructure.GetChong(currentSelectedDate) + "  "
                    + OfflineDateInfoStructure.GetSha(currentSelectedDate),
                    universalFont, grayBrush, new RectangleF(90, 10, 225, 20));

                // 绘制五行
                g.DrawString("五行:", universalFont, grayBrush, 46, 32);
                g.DrawString(OfflineDateInfoStructure.GetWuxing(currentSelectedDate),
                    universalFont, grayBrush, new RectangleF(90, 32, 225, 20));

                // 绘制星座
                g.DrawString("星座:", universalFont, grayBrush, 46, 54);
                g.DrawString(OfflineDateInfoStructure.GetStar(currentSelectedDate),
                    universalFont, grayBrush, new RectangleF(90, 54, 225, 20));

                // 绘制胎神占方
                g.DrawString("胎神占方:", universalFont, grayBrush, 20, 76);
                g.DrawString(OfflineDateInfoStructure.GetTaishen(currentSelectedDate),
                    universalFont, grayBrush, new RectangleF(90, 76, 225, 20));

                // 绘制诸神方位
                g.DrawString("诸神方位:", universalFont, grayBrush, 20, 98);
                g.DrawString("财神 " + OfflineDateInfoStructure.GetCaiShen(currentSelectedDate)
                    + "    喜神 " + OfflineDateInfoStructure.GetXiShen(currentSelectedDate)
                    + "    福神 " + OfflineDateInfoStructure.GetFuShen(currentSelectedDate),
                    universalFont, grayBrush, new RectangleF(90, 98, 225, 20));

                // 绘制值日天神
                g.DrawString("值日天神:", universalFont, grayBrush, 20, 120);
                g.DrawString(OfflineDateInfoStructure.GetZhiRi(currentSelectedDate),
                    universalFont, grayBrush, new RectangleF(90, 120, 225, 20));

                // 绘制凶神
                g.DrawString("凶神:", universalFont, grayBrush, 46, 142);
                g.DrawString(OfflineDateInfoStructure.GetXiongShen(currentSelectedDate),
                    universalFont, grayBrush, new RectangleF(90, 142, 225, 20));

                // 绘制吉神宜趋
                g.DrawString("吉神宜趋:", universalFont, grayBrush, 20, 164);
                g.DrawString(OfflineDateInfoStructure.GetJiShen(currentSelectedDate),
                    universalFont, grayBrush, new RectangleF(90, 164, 225, 20));

                // 绘制吉日
                g.DrawString("吉日:", universalFont, grayBrush, 46, 186);
                g.DrawString(OfflineDateInfoStructure.GetLuckyDay(currentSelectedDate),
                    universalFont, grayBrush, new RectangleF(90, 186, 225, 20));
            }
            else
            {
                g.DrawString("暂无信息", universalFont, grayBrush, 125, 120);
            }
        }

        // 取得表中日期天数的数组
        private int[,] GetDaysInPanel()
        {
            int[,] daysInPanel = new int[6, 7]; // 存放表格中每个单元格显示的数字，无数字为0

            //DateTime date = DateTime.Today;
            int weekOfMonthFirstDay = (int)DateTime.Parse(
                currentSelectedDate.Year.ToString() + "/" + currentSelectedDate.Month.ToString() + "/1"
                ).DayOfWeek; // 该月第一天星期几（0~6）
            int daysOfMonth = DateTime.DaysInMonth(currentSelectedDate.Year, currentSelectedDate.Month); // 该月的天数

            int dayCount = 1;
            for (int i = 0; i < 6; i++)
            {
                for (int j = (i == 0 ? weekOfMonthFirstDay : 0); j < 7; j++)
                {
                    if (dayCount <= daysOfMonth)
                    {
                        daysInPanel[i, j] = dayCount++; // 将对应的日期存入daysInPanel数组
                    }
                    else
                        break;
                }
            }
            return daysInPanel;
        }

        private static DateTime currentTextEditingDate = DateTime.Today; // 当前正在编辑的文本框对应的日期
        public static DateTime CurrentTextEditingDate
        {
            get { return currentTextEditingDate; }
        }

        private void calendarContentPanel_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            // 判断鼠标点击了哪一天
            int row = (int)(e.Location.Y / ((float)calendarContentPanel.Height / 6));
            int coloum = (int)(e.Location.X / ((float)calendarContentPanel.Width / 7));

            int[,] daysInPanel = GetDaysInPanel();

            if (daysInPanel[row, coloum] != 0) // 点击的单元格有元素存在
            {
                // 重置当前选中的日期
                currentSelectedDate = new DateTime(currentSelectedDate.Year, currentSelectedDate.Month, daysInPanel[row, coloum]);
                OnCurrentSelectedDateChanged();
            }

            // 判断鼠标是否点击了日期框右上角的加号（三角形）
            // 如果点击则显示notepadTextBox，点击其他位置将隐藏
            PointF[] topRightTrianglePoints = {
                    new Point(60, 0),
                    new Point(82, 0),
                    new Point(82, 22)
                };

            for (int i = 0; i < 3; i++)
            {
                topRightTrianglePoints[i].X += (float)calendarContentPanel.ClientRectangle.Width / 7 * drawPlusInColoum;
                topRightTrianglePoints[i].Y += (float)calendarContentPanel.ClientRectangle.Height / 6 * drawPlusInRow;
            }
            GraphicsPath topRightTrianglePath = new GraphicsPath();
            topRightTrianglePath.AddPolygon(topRightTrianglePoints);
            Region topRightTriangleRegion = new Region(topRightTrianglePath);

            // 点击了加号，且文本框处于隐藏状态（即首次开启记事本文本框）
            if (topRightTriangleRegion.IsVisible(e.Location) && !notepadTextBox.Visible)
            {
                // 取得当前在编辑的文本所对应日历中的日期格子并存入currentTextEditingDate
                currentTextEditingDate = new DateTime(
                    currentSelectedDate.Year, currentSelectedDate.Month, daysInPanel[drawPlusInRow, drawPlusInColoum]);
                
                // 检查NotepadHashtable类中当前日期是否有数据，如有则显示
                if (NotepadHashtable.Exists(currentTextEditingDate))
                    notepadTextBox.Text = NotepadHashtable.GetText(currentTextEditingDate);

                // 显示文本框
                notepadTextBox.Visible = true;

                // 文本框取得焦点
                notepadTextBox.Focus();

                // 隐藏切换周(月)历按钮，显示导出文本按钮
                switchCalendarModeButtonPanel.Visible = false;
                exportTextButtonPanel.Visible = true;
            }
            // 点击了加号，且文本框处于可见状态（即切换日期）
            else if (topRightTriangleRegion.IsVisible(e.Location) && notepadTextBox.Visible)
            {
                if (notepadTextBox.Text.TrimEnd(' ') != "")
                {
                    // 检查NotepadHashtable类中【旧】日期是否有数据，如有则更新；
                    // 如没有，则将notepadTextBox的当前文本存储到NotepadHashtable类
                    if (NotepadHashtable.Exists(currentTextEditingDate))
                    {
                        NotepadHashtable.UpdateText(currentTextEditingDate, notepadTextBox.Text);
                    }
                    else
                    {
                        NotepadHashtable.AddText(currentTextEditingDate, notepadTextBox.Text);
                    }
                }
                else
                {
                    // 删除哈希表中的数据
                    if (NotepadHashtable.Exists(currentTextEditingDate))
                        NotepadHashtable.RemoveText(currentTextEditingDate);
                }

                // 取得【新】日期并存入currentTextEditingDate
                currentTextEditingDate = new DateTime(
                    currentSelectedDate.Year, currentSelectedDate.Month, daysInPanel[drawPlusInRow, drawPlusInColoum]);

                // 检查NotepadHashtable类中【新】日期是否有数据，如有则显示；
                // 如没有，则清空文本框
                if (NotepadHashtable.Exists(currentTextEditingDate))
                    notepadTextBox.Text = NotepadHashtable.GetText(currentTextEditingDate);
                else
                    notepadTextBox.Text = "";

                // 隐藏切换周(月)历按钮，显示导出文本按钮
                switchCalendarModeButtonPanel.Visible = false;
                exportTextButtonPanel.Visible = true;
            }
            // 没有点击加号，且文本框处于可见状态（即关闭记事本文本框）
            else if (!topRightTriangleRegion.IsVisible(e.Location) && notepadTextBox.Visible)
            {
                // 隐藏文本框
                notepadTextBox.Visible = false;

                if (notepadTextBox.Text.TrimEnd(' ') != "")
                {
                    // 检查NotepadHashtable类中当前日期是否有数据，如有则更新；
                    // 如没有，则将notepadTextBox的当前文本存储到NotepadHashtable类
                    if (NotepadHashtable.Exists(currentTextEditingDate))
                    {
                        NotepadHashtable.UpdateText(currentTextEditingDate, notepadTextBox.Text);
                    }
                    else
                    {
                        NotepadHashtable.AddText(currentTextEditingDate, notepadTextBox.Text);
                    }
                }
                else
                {
                    // 删除哈希表中的数据
                    if (NotepadHashtable.Exists(currentTextEditingDate))
                        NotepadHashtable.RemoveText(currentTextEditingDate);
                }

                notepadTextBox.Text = ""; // 清空当前文本

                // 显示切换周(月)历按钮，隐藏导出文本按钮
                switchCalendarModeButtonPanel.Visible = true;
                exportTextButtonPanel.Visible = false;
                exportTextSingleButtonPanel.Visible = false;
                exportTextAllButtonPanel.Visible = false;

                // 主窗口重新获取焦点
                this.Focus();
            }
            // 没有点击加号，且文本框处于隐藏状态（无效事件）
            else
            {

            }
            // 重绘
            notepadTextBox.Invalidate();
        }

        private void GetDayPositionInCalendarContentPanel(out int dayInTableRow, out int dayInTableColumn, DateTime? date = null)
        {
            DateTime trueDate = (date.HasValue) ? date.Value : currentSelectedDate; // 取得真实日期

            int weekOfMonthFirstDay = (int)DateTime.Parse(
                trueDate.Year.ToString() + "/" + trueDate.Month.ToString() + "/1"
                ).DayOfWeek; // 该月第一天星期几（0~6）

            // 计算date（默认为currentSelectedDate）在表格中的位置

            if (trueDate.Day <= 7 - weekOfMonthFirstDay)
            {
                dayInTableRow = 0;
                dayInTableColumn = trueDate.Day - 1 + weekOfMonthFirstDay;
            }
            else
            {
                dayInTableRow = (trueDate.Day + weekOfMonthFirstDay - 1) / 7;
                dayInTableColumn = ((trueDate.Day - 1) % 7 + weekOfMonthFirstDay) % 7;
            }
        }

        // 基于date（默认为currentSelectedDate）移动circlePanel的位置
        private void MoveCirclePanelToDate(DateTime? date = null)
        {
            const float fixedCircleLengthwaysDistance = 3; // 纵向偏移宽度，向下为正
            const float fixedCircleCrosswiseDistance = 2; // 横向偏移宽度，向右为正

            int dayInTableRow; // 行数
            int dayInTableColumn; // 列数
            GetDayPositionInCalendarContentPanel(out dayInTableRow, out dayInTableColumn, date);

            circlePanel.Left = (int)(dayInTableColumn * ((float)calendarContentPanel.Width / 7) + fixedCircleCrosswiseDistance);
            circlePanel.Top = (int)(dayInTableRow * ((float)calendarContentPanel.Height / 6) + fixedCircleLengthwaysDistance);
        }

        private void dateSelectPanel_MouseClick(object sender, MouseEventArgs e)
        {
            // 仅当按下左键，且当前处于月历模式下时才处理事件
            if (e.Button != MouseButtons.Left || !isMonthlyCalendar)
                return;

            // 左侧三角形按钮路径
            PointF[] leftTrianglePoints = {
                new PointF(11, 19),
                new PointF(24, 10),
                new PointF(24, 28)
            };

            GraphicsPath leftTrianglePath = new GraphicsPath();
            leftTrianglePath.AddPolygon(leftTrianglePoints);
            Region leftTriangleRegion = new Region(leftTrianglePath);

            // 中部三角形按钮路径
            PointF[] centralTrianglePoints = {
                new PointF(dateLabel.Location.X + dateLabel.Size.Width + 10, 12),
                new PointF(dateLabel.Location.X + dateLabel.Size.Width + 28, 12),
                new PointF(dateLabel.Location.X + dateLabel.Size.Width + 19, 24) };

            GraphicsPath centralTrianglePath = new GraphicsPath();
            centralTrianglePath.AddPolygon(centralTrianglePoints);
            Region centralTriangleRegion = new Region(centralTrianglePath);

            // 右侧三角形按钮路径
            PointF[] rightTrianglePoints = {
                new PointF(dateSelectPanel.Width - 11, dateSelectPanel.Height - 19 + 3),
                new PointF(dateSelectPanel.Width - 24, dateSelectPanel.Height - 28 + 3),
                new PointF(dateSelectPanel.Width - 24, dateSelectPanel.Height - 10 + 3)
            };

            GraphicsPath rightTrianglePath = new GraphicsPath();
            rightTrianglePath.AddPolygon(rightTrianglePoints);
            Region rightTriangleRegion = new Region(rightTrianglePath);


            // 判断按下了哪个按钮并处理事件
            // 按下左侧三角形或右侧三角形后，需要将currentSelectedDate向前或向后移一个月
            if (leftTriangleRegion.IsVisible(e.X, e.Y))
            {
                currentSelectedDate = currentSelectedDate.AddMonths(-1);
                OnCurrentShowingMonthChanged(new DateMonth(currentSelectedDate));
            }
            else if (centralTriangleRegion.IsVisible(e.X, e.Y))
            {
                dateLabel_MouseClick(sender, e);
            }
            else if (rightTriangleRegion.IsVisible(e.X, e.Y))
            {
                currentSelectedDate = currentSelectedDate.AddMonths(1);
                OnCurrentShowingMonthChanged(new DateMonth(currentSelectedDate));
            }
        }

        private int drawPlusInRow = -1;
        private int drawPlusInColoum = -1;

        private void calendarContentPanel_MouseMove(object sender, MouseEventArgs e)
        {
            // 当且仅当鼠标在日历面板范围内才响应
            if (e.X > 0 && e.X < calendarContentPanel.Width && e.Y > 0 && e.Y < calendarContentPanel.Height)
            {
                int[,] daysInPanel = GetDaysInPanel();

                // 判断鼠标移到了哪一天
                int row = (int)(e.Y / ((float)calendarContentPanel.Height / 6));
                int coloum = (int)(e.X / ((float)calendarContentPanel.Width / 7));

                // 在相应位置画加号
                if (daysInPanel[row, coloum] != 0)
                {
                    drawPlusInRow = row;
                    drawPlusInColoum = coloum;
                    calendarContentPanel.Invalidate();
                }
                else
                {
                    // 重置Plus的位置
                    drawPlusInRow = -1;
                    drawPlusInColoum = -1;
                }
            }
        }

        private void calendarContentPanel_MouseLeave(object sender, EventArgs e)
        {
            // 重置Plus的位置
            drawPlusInRow = -1;
            drawPlusInColoum = -1;
        }

        private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            Console.WriteLine("KeyPress: " + e.KeyChar);
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine("KeyDown: " + e.KeyCode);
            //MessageBox.Show("KeyCode:" + e.KeyCode + ",\r\n KeyData:" + e.KeyData + ",\r\n KeyValue:" + e.KeyValue);
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Left:
                    // 向左移一天
                    // 在周历模式下不响应
                    if (isMonthlyCalendar)
                    {
                        DateTime newDate = currentSelectedDate.AddDays(-1);
                        if (newDate.Month != currentSelectedDate.Month)
                        {
                            currentSelectedDate = newDate;
                            OnCurrentShowingMonthChanged(new DateMonth(newDate.Year, newDate.Month));
                        }
                        else
                        {
                            currentSelectedDate = newDate;
                        }
                        OnCurrentSelectedDateChanged();
                    }
                    break;
                case Keys.Right:
                    // 向右移一天
                    // 在周历模式下不响应
                    if (isMonthlyCalendar)
                    {
                        DateTime newDate = currentSelectedDate.AddDays(1);
                        if (newDate.Month != currentSelectedDate.Month)
                        {
                            currentSelectedDate = newDate;
                            OnCurrentShowingMonthChanged(new DateMonth(newDate.Year, newDate.Month));
                        }
                        else
                        {
                            currentSelectedDate = newDate;
                        }
                        OnCurrentSelectedDateChanged();
                        OnCurrentSelectedDateChanged();
                    }
                    break;
                case Keys.Up:
                    // 向上移一天（-7天）
                    // 在周历模式下不响应
                    if (isMonthlyCalendar)
                    {
                        DateTime newDate = currentSelectedDate.AddDays(-7);
                        if (newDate.Month != currentSelectedDate.Month)
                        {
                            currentSelectedDate = newDate;
                            OnCurrentShowingMonthChanged(new DateMonth(newDate.Year, newDate.Month));
                        }
                        else
                        {
                            currentSelectedDate = newDate;
                        }
                        OnCurrentSelectedDateChanged();
                    }
                    break;
                case Keys.Down:
                    // 向下移一天（+7天）
                    // 在周历模式下不响应
                    if (isMonthlyCalendar)
                    {
                        DateTime newDate = currentSelectedDate.AddDays(7);
                        if (newDate.Month != currentSelectedDate.Month)
                        {
                            currentSelectedDate = newDate;
                            OnCurrentShowingMonthChanged(new DateMonth(newDate.Year, newDate.Month));
                        }
                        else
                        {
                            currentSelectedDate = newDate;
                        }
                        OnCurrentSelectedDateChanged();
                    }
                    break;
                case Keys.PageUp:
                    // 向左移一个月份
                    // 在周历模式下不响应
                    if (isMonthlyCalendar)
                    {
                        currentSelectedDate = currentSelectedDate.AddMonths(-1);
                        OnCurrentShowingMonthChanged(new DateMonth(currentSelectedDate));
                    }
                    break;
                case Keys.PageDown:
                    // 向右移一个月份
                    // 在周历模式下不响应
                    if (isMonthlyCalendar)
                    {
                        currentSelectedDate = currentSelectedDate.AddMonths(1);
                        OnCurrentShowingMonthChanged(new DateMonth(currentSelectedDate));
                    }
                    break;
                case Keys.Home:
                    // 重定位到今天
                    // 在周历模式下不响应
                    if (isMonthlyCalendar)
                    {
                        currentSelectedDate = DateTime.Today;
                        OnCurrentShowingMonthChanged(new DateMonth(currentSelectedDate.Year, currentSelectedDate.Month));
                        OnCurrentSelectedDateChanged();
                    }
                    break;
                case Keys.S:
                    // 按下切换周(月)历按钮
                    if (switchCalendarModeButtonPanel.Visible)
                    {
                        switchCalendarModeButtonPanel_MouseClick(new object(), new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
                        switchCalendarModeButtonPanel.Invalidate();
                    }
                    break;
                case Keys.H:
                    // 按下帮助按钮
                    if (helpButtonPanel.Visible)
                        helpButtonPanel_MouseClick(new object(), new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
                    break;
                case Keys.M:
                    // 按下更多按钮
                    moreButtonPanel_MouseClick(new object(), new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
                    moreButtonPanel.Invalidate();
                    break;
                case Keys.B:
                    // 按下更换背景按钮
                    if (changeBackgroundButtonPanel.Visible)
                        changeBackgroundButtonPanel_MouseClick(new object(), new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
                    break;
                case Keys.D:
                    // 按下放到桌面按钮
                    if (createShortcutButtonPanel.Visible)
                        createShortcutButtonPanel_MouseClick(new object(), new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
                    break;
                case Keys.U:
                    // 按下检查更新按钮
                    if (checkUpdatesButtonPanel.Visible)
                        checkUpdatesButtonPanel_MouseClick(new object(), new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
                    break;
                case Keys.P:
                    // 按下访问主页按钮
                    if (viewHomepageButtonPanel.Visible)
                        viewHomepageButtonPanel_MouseClick(new object(), new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
                    break;
                case Keys.A:
                    // 按下支持作者按钮
                    if (supportAuthorPanel.Visible)
                        supportAuthorPanel_MouseClick(new object(), new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
                    break;
                case Keys.F:
                    // 按下意见反馈按钮
                    if (feedbackButtonPanel.Visible)
                        feedbackButtonPanel_MouseClick(new object(), new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
                    break;
                case Keys.N:
                    // 按下帮助模式下下一步按钮
                    if (isInHelpMode)
                        nextHelpImageButtonPanel_MouseClick(new object(), new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
                    break;
                case Keys.E:
                    // 按下帮助模式下跳过引导按钮
                    if (isInHelpMode)
                        exitHelpModeButtonPanel_MouseClick(new object(), new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
                    break;
            }
            return true;
        }

        private void circlePanel_MouseMove(object sender, MouseEventArgs e)
        {
            // 判断鼠标移到了哪一天
            int row = (int)(circlePanel.Location.Y / ((float)calendarContentPanel.Height / 6));
            int coloum = (int)(circlePanel.Location.X / ((float)calendarContentPanel.Width / 7));

            // 在相应位置画加号
            drawPlusInRow = row;
            drawPlusInColoum = coloum;
            calendarContentPanel.Invalidate();
        }

        private void circlePanel_MouseClick(object sender, MouseEventArgs e)
        {
            // 对e进行父极对象化，即将e的坐标重置为相对calendarContentPanel的坐标
            MouseEventArgs newE = new MouseEventArgs(
                e.Button, e.Clicks, e.X + circlePanel.Location.X, e.Y + circlePanel.Location.Y, e.Delta);

            // 将鼠标点击事件向下传递
            calendarContentPanel_MouseClick(sender, newE);
        }

        private bool isMonthlyCalendar = true; // 为假时切换周历

        // 点击标题，切换周历与月历显示
        private void titlePanel_MouseClick(object sender, MouseEventArgs e)
        {
            // 切换至周历
            if (isMonthlyCalendar)
            {
                isMonthlyCalendar = false;
                weeklyCalendarContentPanel.Visible = true;
                calendarContentPanel.Visible = false;
                weeklyCalendarContentPanel.Invalidate();
                switchCalendarModeButtonPanel.ButtonText = "切换月历(S)";
            }
            // 切换至月历
            else
            {
                isMonthlyCalendar = true;
                calendarContentPanel.Visible = true;
                weeklyCalendarContentPanel.Visible = false;
                calendarContentPanel.Invalidate();
                switchCalendarModeButtonPanel.ButtonText = "切换周历(S)";
            }
            // 刷新标题
            titlePanel.Invalidate();
            dateSelectPanel.Invalidate();
            // 刷新切换周(月)历按钮
            switchCalendarModeButtonPanel.Invalidate();
        }

        private void weeklyCalendarContentPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen frameworkPen = new Pen(Color.FromArgb(176, 175, 175), 2);

            // 跳转到今天
            currentSelectedDate = DateTime.Today;

            // 绘制时间流动效果
            int dayInTableRow;
            int dayInTableColumn;
            GetDayPositionInCalendarContentPanel(out dayInTableRow, out dayInTableColumn);

            LinearGradientBrush timeBrush = new LinearGradientBrush(
                new PointF(0, (float)weeklyCalendarContentPanel.ClientRectangle.Height / 6),
                new PointF(0, weeklyCalendarContentPanel.ClientRectangle.Height), Color.LightSeaGreen, Color.PaleVioletRed);

            for (int i = 0; i < dayInTableColumn; i++)
            {
                RectangleF dayRectangle = new RectangleF(
                    0 + i * (float)weeklyCalendarContentPanel.ClientRectangle.Width / 7,
                    (float)weeklyCalendarContentPanel.ClientRectangle.Height / 6,
                    (float)weeklyCalendarContentPanel.ClientRectangle.Width / 7,
                    (float)weeklyCalendarContentPanel.ClientRectangle.Height / 6 * 5
                    );
                g.FillRectangle(timeBrush, dayRectangle);
            }

            // 计算今日时间的流逝量
            TimeSpan todayTimeElapsed = DateTime.Now - currentSelectedDate;
            float todayTimeElapsedRate = (float)todayTimeElapsed.TotalSeconds / 86400;

            RectangleF todayRectangle = new RectangleF(
                    dayInTableColumn * (float)weeklyCalendarContentPanel.ClientRectangle.Width / 7,
                    (float)weeklyCalendarContentPanel.ClientRectangle.Height / 6,
                    (float)weeklyCalendarContentPanel.ClientRectangle.Width / 7,
                    (float)weeklyCalendarContentPanel.ClientRectangle.Height / 6 * 5 * todayTimeElapsedRate
                    );
            g.FillRectangle(timeBrush, todayRectangle);

            // 在当前时间绘制直线
            Pen timeLinePen = new Pen(Color.LightGoldenrodYellow, 2);

            g.DrawLine(timeLinePen,
                new PointF(
                    todayRectangle.X, 
                    (float)weeklyCalendarContentPanel.ClientRectangle.Height / 6 
                    + (float)weeklyCalendarContentPanel.ClientRectangle.Height / 6 * 5 * todayTimeElapsedRate),
                new PointF(
                    todayRectangle.X + todayRectangle.Width,
                    (float)weeklyCalendarContentPanel.ClientRectangle.Height / 6 
                    + (float)weeklyCalendarContentPanel.ClientRectangle.Height / 6 * 5 * todayTimeElapsedRate)
                );

            // 绘制基础框架：列*7
            float currentSeparateWidth;
            for (int i = 0; i < 6; i++)
            {
                currentSeparateWidth = (float)weeklyCalendarContentPanel.Width / 7 * (i + 1);
                g.DrawLine(frameworkPen,
                            new PointF(currentSeparateWidth, 0),
                            new PointF(currentSeparateWidth, (float)weeklyCalendarContentPanel.Height));
            }
            // 绘制基础框架：行*1
            float currentSeparateHeight;
            for (int i = 0; i < 1; i++)
            {
                currentSeparateHeight = (float)calendarContentPanel.Height / 6 * (i + 1);
                g.DrawLine(frameworkPen,
                            new PointF(0, currentSeparateHeight),
                            new PointF((float)weeklyCalendarContentPanel.Width, currentSeparateHeight));
            }

            // 加深今天的背景颜色
            int todayRow;
            int todayColumn;
            GetDayPositionInCalendarContentPanel(out todayRow, out todayColumn, DateTime.Today); // 得到今天在日历中的位置
            SolidBrush todayBackgroundBrush = new SolidBrush(Color.FromArgb(253, 245, 209));
            RectangleF todayRect = new RectangleF(
                todayColumn * ((float)weeklyCalendarContentPanel.Width / 7) + 2,
                0 * ((float)weeklyCalendarContentPanel.Height / 6) + 2,
                (float)weeklyCalendarContentPanel.Width / 7 - 3,
                (float)weeklyCalendarContentPanel.Height / 6 - 3);
            g.FillRectangle(todayBackgroundBrush, todayRect);


            // 绘制日期数字
            const float fixedNumberCrosswiseDistance = 27;
            const float fixedNumberLengthwaysDistance = 15;
            const float fixedNumberAdditionalCrosswiseDistanceForSingleFigure = 5; // 为单个数字（0~9）增加的横向宽度

            Font numberFont = new Font("微软雅黑", 15);
            SolidBrush numberNormalBrush = new SolidBrush(LaunchForm.normalizedGray);
            SolidBrush numberHolidayBrush = new SolidBrush(Color.FromArgb(238, 119, 0));

            int[,] daysInPanel = GetDaysInPanel();

            for (int i = 0; i < 7; i++)
            {
                string dateString = daysInPanel[dayInTableRow, i] == 0 ? "" : daysInPanel[dayInTableRow, i].ToString();
                PointF numberPosition = new PointF(
                        (float)weeklyCalendarContentPanel.ClientRectangle.Width / 7 * i + fixedNumberCrosswiseDistance,
                        fixedNumberLengthwaysDistance
                        );
                if (daysInPanel[dayInTableRow, i] < 10) // 单个数字
                    numberPosition.X += fixedNumberAdditionalCrosswiseDistanceForSingleFigure;

                // 判断是否为节假日并用不同画刷绘制
                if (dateString != "")
                {
                    int weekOfCurrentDate = (int)DateTime.Parse(
                        currentSelectedDate.Year.ToString() + "/" + currentSelectedDate.Month.ToString() + "/" + dateString
                        ).DayOfWeek;
                    if (weekOfCurrentDate == 0 || weekOfCurrentDate == 6)
                        g.DrawString(dateString, numberFont, numberHolidayBrush, numberPosition);
                    else
                        g.DrawString(dateString, numberFont, numberNormalBrush, numberPosition);
                }
            }


            // 绘制农历文字
            const float fixedLunarLengthwaysDistance = 40;
            const float fixedLunarCrosswiseDistance = 27;

            Font lunarFont = new Font("微软雅黑", 10);
            SolidBrush lunarBrush = new SolidBrush(LaunchForm.normalizedGray);
            SolidBrush lunarHolidayBrush = new SolidBrush(Color.FromArgb(238, 119, 0));

            for (int i = 0; i < 7; i++)
            {
                string dateString = daysInPanel[dayInTableRow, i] == 0 ? "" : daysInPanel[dayInTableRow, i].ToString();
                PointF lunarPosition = new PointF(
                        (float)weeklyCalendarContentPanel.ClientRectangle.Width / 7 * i + fixedLunarCrosswiseDistance,
                        fixedLunarLengthwaysDistance
                        );

                // 判断是否为节假日并用不同画刷绘制
                if (dateString != "")
                {
                    DateTime currentDate = new DateTime(currentSelectedDate.Year, currentSelectedDate.Month, daysInPanel[dayInTableRow, i]);
                    if (ApplicationStatus.isInternetConnected && OnlineDateInfoDatabase.searchMonth(currentDate.Year, currentDate.Month) != -1)
                    {
                        string holidayInfo = OnlineDateInfoDatabase.GetHoliday(currentDate);
                        if (holidayInfo == "")
                        {
                            g.DrawString(OnlineDateInfoDatabase.GetLunarCalendarDay(currentDate), lunarFont, lunarBrush, lunarPosition);
                        }
                        else
                        {
                            g.DrawString(holidayInfo, lunarFont, lunarHolidayBrush, lunarPosition);
                        }
                    }
                    else if (currentDate >= OfflineDateInfoStructure.STARTDATE && currentDate <= OfflineDateInfoStructure.ENDDATE)
                    {
                        g.DrawString(OfflineDateInfoStructure.GetLunarDay(currentDate), lunarFont, lunarBrush, lunarPosition);
                    }
                }
            }

            // 移动weeklyCirclePanel的位置到今天
            const float fixedCircleLengthwaysDistance = 3; // 纵向偏移宽度，向下为正
            const float fixedCircleCrosswiseDistance = 2; // 横向偏移宽度，向右为正

            weeklyCirclePanel.Left = (int)(dayInTableColumn * ((float)calendarContentPanel.Width / 7) + fixedCircleCrosswiseDistance);
            weeklyCirclePanel.Top = (int)(0 * ((float)calendarContentPanel.Height / 6) + fixedCircleLengthwaysDistance);

        }

        private void supportAuthorPanel_MouseClick(object sender, MouseEventArgs e)
        {
            // 只允许打开一个关于窗口
            if (AboutForm.IsShown == false)
            {
                AboutForm aboutForm = new AboutForm();
                aboutForm.Show();
            }
        }

        private void switchCalendarModeButtonPanel_MouseClick(object sender, MouseEventArgs e)
        {
            titlePanel_MouseClick(sender, e);
        }

        private void exportTextButtonPanel_MouseClick(object sender, MouseEventArgs e)
        {
            if (!exportTextSingleButtonPanel.Visible)
            {
                exportTextSingleButtonPanel.Visible = true;
                exportTextAllButtonPanel.Visible = true;
            }
            else
            {
                exportTextSingleButtonPanel.Visible = false;
                exportTextAllButtonPanel.Visible = false;
            }
        }

        private void moreButtonPanel_MouseClick(object sender, MouseEventArgs e)
        {
            if (!changeBackgroundButtonPanel.Visible)
            {
                switchCalendarModeButtonPanel.Visible = false;
                helpButtonPanel.Visible = false;

                exportTextButtonPanel.Visible = false;
                exportTextSingleButtonPanel.Visible = false;
                exportTextAllButtonPanel.Visible = false;

                changeBackgroundButtonPanel.Visible = true;
                createShortcutButtonPanel.Visible = true;
                checkUpdatesButtonPanel.Visible = true;
                viewHomepageButtonPanel.Visible = true;
                supportAuthorPanel.Visible = true;
                feedbackButtonPanel.Visible = true;
                moreButtonPanel.ButtonText = "   收起 >>";
            }
            else
            {
                switchCalendarModeButtonPanel.Visible = true;
                helpButtonPanel.Visible = true;

                changeBackgroundButtonPanel.Visible = false;
                createShortcutButtonPanel.Visible = false;
                checkUpdatesButtonPanel.Visible = false;
                viewHomepageButtonPanel.Visible = false;
                supportAuthorPanel.Visible = false;
                feedbackButtonPanel.Visible = false;
                moreButtonPanel.ButtonText = "   更多(M)";
            }
        }

        private void viewHomepageButtonPanel_MouseClick(object sender, MouseEventArgs e)
        {
            System.Diagnostics.Process.Start(ApplicationStatus.homepage);
        }

        private void checkUpdatesButtonPanel_MouseClick(object sender, MouseEventArgs e)
        {
            //Task checkUpdateTask = Task.Factory.StartNew(delegate
            //{    
            checkUpdate(true);
            //});
        }

        /// <summary>
        /// 创建桌面快捷方式
        /// </summary>
        private void CreateShortcut()
        {
            //获取当前系统用户桌面目录
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            FileInfo fileDesktop = new FileInfo(desktopPath + "\\中华万年历.lnk");

            if (!fileDesktop.Exists)
            {
                WshShell shell = new WshShell();
                IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(
                      Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) +
                       "\\" + "中华万年历.lnk"
                       );
                shortcut.TargetPath = Application.ExecutablePath;
                shortcut.WorkingDirectory = System.Environment.CurrentDirectory;
                shortcut.WindowStyle = 1;
                shortcut.Description = "中华万年历";
                shortcut.IconLocation = Application.ExecutablePath;
                shortcut.Save();
            }
        }

        private void createShortcutButtonPanel_MouseClick(object sender, MouseEventArgs e)
        {
            CreateShortcut();
        }

        private void feedbackButtonPanel_MouseClick(object sender, MouseEventArgs e)
        {
            if (!ApplicationStatus.isInternetConnected)
            {
                MessageBox.Show("您的网络连接有问题噢，\n必须在网络连接正常时才可以反馈。请检查后重试！", 
                    "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            FeedbackForm feedbackForm = new FeedbackForm();
            feedbackForm.ShowDialog();
        }

        private const string BACKGROUNDIMAGESAVINGFILENAME = "background";

        private void changeBackgroundButtonPanel_MouseClick(object sender, MouseEventArgs e)
        {
            string backgroundImageSavingFilePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                OfflineDateInfoStructure.APPLICATIONDATADIRECTORY, BACKGROUNDIMAGESAVINGFILENAME);

            if (!System.IO.File.Exists(backgroundImageSavingFilePath))
            {
                OpenFileDialog fd = new OpenFileDialog();
                fd.Filter = "图片文件 (*.jpg;*.jpeg;*.png;*.gif;*.bmp)|*.jpg;*.jpeg;*.png;*.gif;*.bmp"; //过滤文件类型
                fd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);//设定初始目录
                DialogResult r = fd.ShowDialog();
                if (r == DialogResult.OK)
                {
                    Image image = Image.FromFile(fd.FileName);
                    backgroundPanel.BackgroundImageLayout = ImageLayout.Stretch;
                    backgroundPanel.BackgroundImage = image;

                    image.Save(backgroundImageSavingFilePath);

                    changeBackgroundButtonPanel.ButtonText = "默认背景(B)";
                }
            }
            else
            {
                backgroundPanel.BackgroundImageLayout = ImageLayout.Zoom;
                // 解除文件占用
                backgroundPanel.BackgroundImage.Dispose();
                backgroundPanel.BackgroundImage = WindowsCalendar.Properties.Resources.background;
                System.IO.File.Delete(backgroundImageSavingFilePath);

                changeBackgroundButtonPanel.ButtonText = "更换背景(B)";
            }
        }

        private void dateLabel_MouseClick(object sender, MouseEventArgs e)
        {
            // 点击左键，且当前是月历模式才响应
            if (e.Button != MouseButtons.Left || !isMonthlyCalendar)
                return;

            if (!smallMonthCalendar.Visible)
            {
                // 导航到当前日历选中的日期
                smallMonthCalendar.SelectionStart = currentSelectedDate;
                smallMonthCalendar.Visible = true;
            }
            else
            {
                smallMonthCalendar.Visible = false;
                // 主窗口重新获取焦点，防止按键无响应或响应错误
                this.Focus();
            }
        }

        private void smallMonthCalendar_DateChanged(object sender, DateRangeEventArgs e)
        {
            currentSelectedDate = e.Start;
            OnCurrentSelectedDateChanged();
            OnCurrentShowingMonthChanged(new DateMonth(currentSelectedDate.Year, currentSelectedDate.Month));
        }

        private MenuItem showDlgMenu = new MenuItem("显示中华万年历");
        private MenuItem viewHomepageMenu = new MenuItem("访问主页");
        private MenuItem exitMenu = new MenuItem("退出");

        private void InitializateNotifyIcon()
        {
            this.showDlgMenu.Click += new EventHandler(showDlgMenu_Click);
            this.viewHomepageMenu.Click += new EventHandler(viewHomepageMenu_Click);
            this.exitMenu.Click += new EventHandler(exitMenu_Click);

            this.notifyIcon.Text = "中华万年历";
            this.notifyIcon.ContextMenu = new System.Windows.Forms.ContextMenu();
            this.notifyIcon.ContextMenu.MenuItems.Add(this.showDlgMenu);
            this.notifyIcon.ContextMenu.MenuItems.Add(this.viewHomepageMenu);
            this.notifyIcon.ContextMenu.MenuItems.Add(this.exitMenu);
        }

        void showDlgMenu_Click(object sender, EventArgs e)
        {
            this.Visible = true;
            this.ShowInTaskbar = true;
            this.WindowState = FormWindowState.Normal;
            this.notifyIcon.Visible = false;
        }

        void viewHomepageMenu_Click(object sender, EventArgs e)
        {
            viewHomepageButtonPanel_MouseClick(sender, new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
        }

        void exitMenu_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            this.ShowInTaskbar = false;
            this.notifyIcon.Visible = false;
            System.Environment.Exit(0);
        }

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                showDlgMenu_Click(sender, new EventArgs());
            }
        }

        private bool isMouseInCloseButtonPanel = false;
        private bool isMouseInMinimizeButtonPanel = false;

        private void closeButtonPanel_Paint(object sender, PaintEventArgs e)
        {
            // 画一个叉
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.White);
            pen.Width = isMouseInCloseButtonPanel ? 2 : 1;

            g.DrawLine(pen, 0, 0, 12, 12);
            g.DrawLine(pen, 12, 0, 0, 12);
        }

        private void closeButtonPanel_MouseEnter(object sender, EventArgs e)
        {
            isMouseInCloseButtonPanel = true;
            closeButtonPanel.Invalidate();
        }

        private void closeButtonPanel_MouseLeave(object sender, EventArgs e)
        {
            isMouseInCloseButtonPanel = false;
            closeButtonPanel.Invalidate();
        }

        private void closeButtonPanel_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                FadeOutToExit();
            }
        }

        private void minimizeButtonPanel_Paint(object sender, PaintEventArgs e)
        {
            // 画一条横线
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.White);
            pen.Width = isMouseInMinimizeButtonPanel ? 2 : 1;

            g.DrawLine(pen, 0, 6, 12, 6);
        }

        private void minimizeButtonPanel_MouseEnter(object sender, EventArgs e)
        {
            isMouseInMinimizeButtonPanel = true;
            minimizeButtonPanel.Invalidate();
        }

        private void minimizeButtonPanel_MouseLeave(object sender, EventArgs e)
        {
            isMouseInMinimizeButtonPanel = false;
            minimizeButtonPanel.Invalidate();
        }

        private void minimizeButtonPanel_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.notifyIcon.Visible = true;
                this.WindowState = FormWindowState.Minimized;
                this.ShowInTaskbar = false;
            }
        }

        private void FadeOutToExit()
        {

            Task fadeOutTask = Task.Factory.StartNew(() =>
            {
                this.BeginInvoke(new MethodInvoker(delegate {
                    this.Opacity = 1;
                    while ((this.Opacity -= 0.06) > 0)
                        Thread.Sleep(10);
                    this.Visible = false;
                    this.ShowInTaskbar = false;
                    this.notifyIcon.Visible = false;
                    this.Dispose(true);
                    System.Environment.Exit(0);
                }));
            });
        }

        private void exportTextSingleButtonPanel_MouseClick(object sender, MouseEventArgs e)
        {
            SaveFileDialog fd = new SaveFileDialog();
            fd.FileName = "*.txt";
            fd.Filter = "文本文件 (*.txt)|*.txt"; //过滤文件类型
            fd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);//设定初始目录
            DialogResult r = fd.ShowDialog();
            if (r == DialogResult.OK)
            {
                string savingText = NotepadHashtable.GetText(currentSelectedDate);
                FileStream fs = new FileStream(fd.FileName, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(savingText);
                sw.Close();
            }
        }

        private void exportTextAllButtonPanel_MouseClick(object sender, MouseEventArgs e)
        {
            SaveFileDialog fd = new SaveFileDialog();
            fd.FileName = "*.txt";
            fd.Filter = "文本文件 (*.txt)|*.txt"; //过滤文件类型
            fd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);//设定初始目录
            DialogResult r = fd.ShowDialog();
            if (r == DialogResult.OK)
            {
                string savingText = NotepadHashtable.GetAllDateAndText();
                FileStream fs = new FileStream(fd.FileName, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(savingText);
                sw.Close();
            }
        }

        private bool isInHelpMode = false;
        private int currentHelpModePage = 0;

        private void helpButtonPanel_MouseClick(object sender, MouseEventArgs e)
        {
            if (helpImagePanel.Visible == false)
            {
                helpImagePanel.BackgroundImage = WindowsCalendar.Properties.Resources.guidance_page_1;
                helpImagePanel.Visible = true;
                nextHelpImageButtonPanel.ButtonText = "  下一步(N)";
                nextHelpImageButtonPanel.Visible = true;
                exitHelpModeButtonPanel.Visible = true;
                isInHelpMode = true;
                currentHelpModePage = 1;
            }
        }

        private void nextHelpImageButtonPanel_MouseClick(object sender, MouseEventArgs e)
        {
            if (isInHelpMode)
            {
                switch (currentHelpModePage)
                {
                    case 1:
                        helpImagePanel.BackgroundImage = WindowsCalendar.Properties.Resources.guidance_page_2;
                        currentHelpModePage = 2;
                        break;
                    case 2:
                        helpImagePanel.BackgroundImage = WindowsCalendar.Properties.Resources.guidance_page_3;
                        currentHelpModePage = 3;
                        break;
                    case 3:
                        helpImagePanel.BackgroundImage = WindowsCalendar.Properties.Resources.guidance_page_4;
                        currentHelpModePage = 4;
                        break;
                    case 4:
                        helpImagePanel.BackgroundImage = WindowsCalendar.Properties.Resources.guidance_page_5;
                        currentHelpModePage = 5;
                        nextHelpImageButtonPanel.ButtonText = "    完成(N)";
                        break;
                    case 5:
                        nextHelpImageButtonPanel.Visible = false;
                        exitHelpModeButtonPanel.Visible = false;
                        helpImagePanel.Visible = false;
                        helpImagePanel.BackgroundImage = null;
                        isInHelpMode = false;
                        currentHelpModePage = 0;
                        break;
                }
            }
        }

        private void exitHelpModeButtonPanel_MouseClick(object sender, MouseEventArgs e)
        {
            if (isInHelpMode)
            {
                nextHelpImageButtonPanel.Visible = false;
                exitHelpModeButtonPanel.Visible = false;
                helpImagePanel.Visible = false;
                helpImagePanel.BackgroundImage = null;
                isInHelpMode = false;
                currentHelpModePage = 0;
            }
        }
    }
}
