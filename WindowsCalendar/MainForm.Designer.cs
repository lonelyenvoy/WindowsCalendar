namespace WindowsCalendar
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.debugTimer = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.backgroundPanel = new WindowsCalendar.OptimizedPanel();
            this.exitHelpModeButtonPanel = new WindowsCalendar.CustomizedButtonPanel();
            this.nextHelpImageButtonPanel = new WindowsCalendar.CustomizedButtonPanel();
            this.helpImagePanel = new WindowsCalendar.OptimizedPanel();
            this.notepadTextBox = new WindowsCalendar.WatermarkedTextBox();
            this.minimizeButtonPanel = new WindowsCalendar.OptimizedPanel();
            this.closeButton = new System.Windows.Forms.Button();
            this.closeButtonPanel = new WindowsCalendar.OptimizedPanel();
            this.smallMonthCalendar = new System.Windows.Forms.MonthCalendar();
            this.exportTextAllButtonPanel = new WindowsCalendar.CustomizedButtonPanel();
            this.exportTextSingleButtonPanel = new WindowsCalendar.CustomizedButtonPanel();
            this.exportTextButtonPanel = new WindowsCalendar.CustomizedButtonPanel();
            this.switchCalendarModeButtonPanel = new WindowsCalendar.CustomizedButtonPanel();
            this.moreButtonPanel = new WindowsCalendar.CustomizedButtonPanel();
            this.feedbackButtonPanel = new WindowsCalendar.CustomizedButtonPanel();
            this.supportAuthorPanel = new WindowsCalendar.CustomizedButtonPanel();
            this.viewHomepageButtonPanel = new WindowsCalendar.CustomizedButtonPanel();
            this.helpButtonPanel = new WindowsCalendar.CustomizedButtonPanel();
            this.checkUpdatesButtonPanel = new WindowsCalendar.CustomizedButtonPanel();
            this.createShortcutButtonPanel = new WindowsCalendar.CustomizedButtonPanel();
            this.changeBackgroundButtonPanel = new WindowsCalendar.CustomizedButtonPanel();
            this.weeklyCalendarContentPanel = new WindowsCalendar.OptimizedPanel();
            this.weeklyCirclePanel = new WindowsCalendar.OptimizedPanel();
            this.lunarInfoPanel = new WindowsCalendar.OptimizedPanel();
            this.suitAndAvoidPanel = new WindowsCalendar.OptimizedPanel();
            this.detailedInfoPanel = new WindowsCalendar.OptimizedPanel();
            this.debugButton = new System.Windows.Forms.Button();
            this.calendarBoxPanel = new WindowsCalendar.OptimizedPanel();
            this.weekPanel = new WindowsCalendar.OptimizedPanel();
            this.calendarContentPanel = new WindowsCalendar.OptimizedPanel();
            this.circlePanel = new WindowsCalendar.OptimizedPanel();
            this.dateSelectPanel = new WindowsCalendar.OptimizedPanel();
            this.dateLabel = new System.Windows.Forms.Label();
            this.titlePanel = new WindowsCalendar.OptimizedPanel();
            this.backgroundPanel.SuspendLayout();
            this.weeklyCalendarContentPanel.SuspendLayout();
            this.calendarBoxPanel.SuspendLayout();
            this.calendarContentPanel.SuspendLayout();
            this.dateSelectPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // debugTimer
            // 
            this.debugTimer.Tick += new System.EventHandler(this.debugTimer_Tick);
            // 
            // notifyIcon
            // 
            this.notifyIcon.Text = "notifyIcon";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseClick);
            // 
            // backgroundPanel
            // 
            this.backgroundPanel.AllowDragWindow = true;
            this.backgroundPanel.Controls.Add(this.exitHelpModeButtonPanel);
            this.backgroundPanel.Controls.Add(this.nextHelpImageButtonPanel);
            this.backgroundPanel.Controls.Add(this.helpImagePanel);
            this.backgroundPanel.Controls.Add(this.notepadTextBox);
            this.backgroundPanel.Controls.Add(this.minimizeButtonPanel);
            this.backgroundPanel.Controls.Add(this.closeButton);
            this.backgroundPanel.Controls.Add(this.closeButtonPanel);
            this.backgroundPanel.Controls.Add(this.smallMonthCalendar);
            this.backgroundPanel.Controls.Add(this.exportTextAllButtonPanel);
            this.backgroundPanel.Controls.Add(this.exportTextSingleButtonPanel);
            this.backgroundPanel.Controls.Add(this.exportTextButtonPanel);
            this.backgroundPanel.Controls.Add(this.switchCalendarModeButtonPanel);
            this.backgroundPanel.Controls.Add(this.moreButtonPanel);
            this.backgroundPanel.Controls.Add(this.feedbackButtonPanel);
            this.backgroundPanel.Controls.Add(this.supportAuthorPanel);
            this.backgroundPanel.Controls.Add(this.viewHomepageButtonPanel);
            this.backgroundPanel.Controls.Add(this.helpButtonPanel);
            this.backgroundPanel.Controls.Add(this.checkUpdatesButtonPanel);
            this.backgroundPanel.Controls.Add(this.createShortcutButtonPanel);
            this.backgroundPanel.Controls.Add(this.changeBackgroundButtonPanel);
            this.backgroundPanel.Controls.Add(this.weeklyCalendarContentPanel);
            this.backgroundPanel.Controls.Add(this.lunarInfoPanel);
            this.backgroundPanel.Controls.Add(this.suitAndAvoidPanel);
            this.backgroundPanel.Controls.Add(this.detailedInfoPanel);
            this.backgroundPanel.Controls.Add(this.debugButton);
            this.backgroundPanel.Controls.Add(this.calendarBoxPanel);
            this.backgroundPanel.Controls.Add(this.titlePanel);
            this.backgroundPanel.Location = new System.Drawing.Point(0, 0);
            this.backgroundPanel.Name = "backgroundPanel";
            this.backgroundPanel.Size = new System.Drawing.Size(997, 561);
            this.backgroundPanel.TabIndex = 3;
            this.backgroundPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.backgroundPanel_Paint);
            // 
            // exitHelpModeButtonPanel
            // 
            this.exitHelpModeButtonPanel.AllowDragWindow = true;
            this.exitHelpModeButtonPanel.ButtonText = "";
            this.exitHelpModeButtonPanel.Location = new System.Drawing.Point(792, 528);
            this.exitHelpModeButtonPanel.Name = "exitHelpModeButtonPanel";
            this.exitHelpModeButtonPanel.Size = new System.Drawing.Size(85, 23);
            this.exitHelpModeButtonPanel.Style = WindowsCalendar.ButtonPanelStyle.LightBackground;
            this.exitHelpModeButtonPanel.TabIndex = 36;
            this.exitHelpModeButtonPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.exitHelpModeButtonPanel_MouseClick);
            // 
            // nextHelpImageButtonPanel
            // 
            this.nextHelpImageButtonPanel.AllowDragWindow = true;
            this.nextHelpImageButtonPanel.ButtonText = "";
            this.nextHelpImageButtonPanel.Location = new System.Drawing.Point(883, 528);
            this.nextHelpImageButtonPanel.Name = "nextHelpImageButtonPanel";
            this.nextHelpImageButtonPanel.Size = new System.Drawing.Size(85, 23);
            this.nextHelpImageButtonPanel.Style = WindowsCalendar.ButtonPanelStyle.LightBackground;
            this.nextHelpImageButtonPanel.TabIndex = 35;
            this.nextHelpImageButtonPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.nextHelpImageButtonPanel_MouseClick);
            // 
            // helpImagePanel
            // 
            this.helpImagePanel.AllowDragWindow = true;
            this.helpImagePanel.Location = new System.Drawing.Point(13, 14);
            this.helpImagePanel.Name = "helpImagePanel";
            this.helpImagePanel.Size = new System.Drawing.Size(31, 52);
            this.helpImagePanel.TabIndex = 34;
            // 
            // notepadTextBox
            // 
            this.notepadTextBox.BackGroundText = "";
            this.notepadTextBox.Location = new System.Drawing.Point(376, 5);
            this.notepadTextBox.Multiline = true;
            this.notepadTextBox.Name = "notepadTextBox";
            this.notepadTextBox.Size = new System.Drawing.Size(45, 84);
            this.notepadTextBox.TabIndex = 8;
            this.notepadTextBox.Visible = false;
            // 
            // minimizeButtonPanel
            // 
            this.minimizeButtonPanel.AllowDragWindow = true;
            this.minimizeButtonPanel.Location = new System.Drawing.Point(926, 24);
            this.minimizeButtonPanel.Name = "minimizeButtonPanel";
            this.minimizeButtonPanel.Size = new System.Drawing.Size(13, 13);
            this.minimizeButtonPanel.TabIndex = 33;
            this.minimizeButtonPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.minimizeButtonPanel_Paint);
            this.minimizeButtonPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.minimizeButtonPanel_MouseClick);
            this.minimizeButtonPanel.MouseEnter += new System.EventHandler(this.minimizeButtonPanel_MouseEnter);
            this.minimizeButtonPanel.MouseLeave += new System.EventHandler(this.minimizeButtonPanel_MouseLeave);
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(145, 1);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 1;
            this.closeButton.Text = "button1";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // closeButtonPanel
            // 
            this.closeButtonPanel.AllowDragWindow = true;
            this.closeButtonPanel.Location = new System.Drawing.Point(955, 24);
            this.closeButtonPanel.Name = "closeButtonPanel";
            this.closeButtonPanel.Size = new System.Drawing.Size(13, 13);
            this.closeButtonPanel.TabIndex = 32;
            this.closeButtonPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.closeButtonPanel_Paint);
            this.closeButtonPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.closeButtonPanel_MouseClick);
            this.closeButtonPanel.MouseEnter += new System.EventHandler(this.closeButtonPanel_MouseEnter);
            this.closeButtonPanel.MouseLeave += new System.EventHandler(this.closeButtonPanel_MouseLeave);
            // 
            // smallMonthCalendar
            // 
            this.smallMonthCalendar.Location = new System.Drawing.Point(30, 262);
            this.smallMonthCalendar.Name = "smallMonthCalendar";
            this.smallMonthCalendar.TabIndex = 2;
            this.smallMonthCalendar.Visible = false;
            this.smallMonthCalendar.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.smallMonthCalendar_DateChanged);
            // 
            // exportTextAllButtonPanel
            // 
            this.exportTextAllButtonPanel.AllowDragWindow = true;
            this.exportTextAllButtonPanel.ButtonText = "";
            this.exportTextAllButtonPanel.Location = new System.Drawing.Point(427, 68);
            this.exportTextAllButtonPanel.Name = "exportTextAllButtonPanel";
            this.exportTextAllButtonPanel.Size = new System.Drawing.Size(85, 23);
            this.exportTextAllButtonPanel.Style = WindowsCalendar.ButtonPanelStyle.LightBackground;
            this.exportTextAllButtonPanel.TabIndex = 31;
            this.exportTextAllButtonPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.exportTextAllButtonPanel_MouseClick);
            // 
            // exportTextSingleButtonPanel
            // 
            this.exportTextSingleButtonPanel.AllowDragWindow = true;
            this.exportTextSingleButtonPanel.ButtonText = "";
            this.exportTextSingleButtonPanel.Location = new System.Drawing.Point(427, 60);
            this.exportTextSingleButtonPanel.Name = "exportTextSingleButtonPanel";
            this.exportTextSingleButtonPanel.Size = new System.Drawing.Size(85, 23);
            this.exportTextSingleButtonPanel.Style = WindowsCalendar.ButtonPanelStyle.LightBackground;
            this.exportTextSingleButtonPanel.TabIndex = 30;
            this.exportTextSingleButtonPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.exportTextSingleButtonPanel_MouseClick);
            // 
            // exportTextButtonPanel
            // 
            this.exportTextButtonPanel.AllowDragWindow = true;
            this.exportTextButtonPanel.ButtonText = "";
            this.exportTextButtonPanel.Location = new System.Drawing.Point(427, 53);
            this.exportTextButtonPanel.Name = "exportTextButtonPanel";
            this.exportTextButtonPanel.Size = new System.Drawing.Size(85, 23);
            this.exportTextButtonPanel.Style = WindowsCalendar.ButtonPanelStyle.LightBackground;
            this.exportTextButtonPanel.TabIndex = 29;
            this.exportTextButtonPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.exportTextButtonPanel_MouseClick);
            // 
            // switchCalendarModeButtonPanel
            // 
            this.switchCalendarModeButtonPanel.AllowDragWindow = true;
            this.switchCalendarModeButtonPanel.ButtonText = "";
            this.switchCalendarModeButtonPanel.Location = new System.Drawing.Point(427, 24);
            this.switchCalendarModeButtonPanel.Name = "switchCalendarModeButtonPanel";
            this.switchCalendarModeButtonPanel.Size = new System.Drawing.Size(85, 23);
            this.switchCalendarModeButtonPanel.Style = WindowsCalendar.ButtonPanelStyle.LightBackground;
            this.switchCalendarModeButtonPanel.TabIndex = 28;
            this.switchCalendarModeButtonPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.switchCalendarModeButtonPanel_MouseClick);
            // 
            // moreButtonPanel
            // 
            this.moreButtonPanel.AllowDragWindow = true;
            this.moreButtonPanel.ButtonText = "";
            this.moreButtonPanel.Location = new System.Drawing.Point(883, 53);
            this.moreButtonPanel.Name = "moreButtonPanel";
            this.moreButtonPanel.Size = new System.Drawing.Size(85, 23);
            this.moreButtonPanel.Style = WindowsCalendar.ButtonPanelStyle.LightBackground;
            this.moreButtonPanel.TabIndex = 27;
            this.moreButtonPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.moreButtonPanel_MouseClick);
            // 
            // feedbackButtonPanel
            // 
            this.feedbackButtonPanel.AllowDragWindow = true;
            this.feedbackButtonPanel.ButtonText = "";
            this.feedbackButtonPanel.Location = new System.Drawing.Point(701, 53);
            this.feedbackButtonPanel.Name = "feedbackButtonPanel";
            this.feedbackButtonPanel.Size = new System.Drawing.Size(85, 23);
            this.feedbackButtonPanel.Style = WindowsCalendar.ButtonPanelStyle.LightBackground;
            this.feedbackButtonPanel.TabIndex = 26;
            this.feedbackButtonPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.feedbackButtonPanel_MouseClick);
            // 
            // supportAuthorPanel
            // 
            this.supportAuthorPanel.AllowDragWindow = true;
            this.supportAuthorPanel.ButtonText = "";
            this.supportAuthorPanel.Location = new System.Drawing.Point(609, 53);
            this.supportAuthorPanel.Name = "supportAuthorPanel";
            this.supportAuthorPanel.Size = new System.Drawing.Size(85, 23);
            this.supportAuthorPanel.Style = WindowsCalendar.ButtonPanelStyle.LightBackground;
            this.supportAuthorPanel.TabIndex = 25;
            this.supportAuthorPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.supportAuthorPanel_MouseClick);
            // 
            // viewHomepageButtonPanel
            // 
            this.viewHomepageButtonPanel.AllowDragWindow = true;
            this.viewHomepageButtonPanel.ButtonText = "";
            this.viewHomepageButtonPanel.Location = new System.Drawing.Point(518, 53);
            this.viewHomepageButtonPanel.Name = "viewHomepageButtonPanel";
            this.viewHomepageButtonPanel.Size = new System.Drawing.Size(85, 23);
            this.viewHomepageButtonPanel.Style = WindowsCalendar.ButtonPanelStyle.LightBackground;
            this.viewHomepageButtonPanel.TabIndex = 24;
            this.viewHomepageButtonPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.viewHomepageButtonPanel_MouseClick);
            // 
            // helpButtonPanel
            // 
            this.helpButtonPanel.AllowDragWindow = true;
            this.helpButtonPanel.ButtonText = "";
            this.helpButtonPanel.Location = new System.Drawing.Point(792, 53);
            this.helpButtonPanel.Name = "helpButtonPanel";
            this.helpButtonPanel.Size = new System.Drawing.Size(85, 23);
            this.helpButtonPanel.Style = WindowsCalendar.ButtonPanelStyle.LightBackground;
            this.helpButtonPanel.TabIndex = 23;
            this.helpButtonPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.helpButtonPanel_MouseClick);
            // 
            // checkUpdatesButtonPanel
            // 
            this.checkUpdatesButtonPanel.AllowDragWindow = true;
            this.checkUpdatesButtonPanel.ButtonText = "";
            this.checkUpdatesButtonPanel.Location = new System.Drawing.Point(701, 24);
            this.checkUpdatesButtonPanel.Name = "checkUpdatesButtonPanel";
            this.checkUpdatesButtonPanel.Size = new System.Drawing.Size(85, 23);
            this.checkUpdatesButtonPanel.Style = WindowsCalendar.ButtonPanelStyle.LightBackground;
            this.checkUpdatesButtonPanel.TabIndex = 22;
            this.checkUpdatesButtonPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.checkUpdatesButtonPanel_MouseClick);
            // 
            // createShortcutButtonPanel
            // 
            this.createShortcutButtonPanel.AllowDragWindow = true;
            this.createShortcutButtonPanel.ButtonText = "";
            this.createShortcutButtonPanel.Location = new System.Drawing.Point(609, 24);
            this.createShortcutButtonPanel.Name = "createShortcutButtonPanel";
            this.createShortcutButtonPanel.Size = new System.Drawing.Size(85, 23);
            this.createShortcutButtonPanel.Style = WindowsCalendar.ButtonPanelStyle.LightBackground;
            this.createShortcutButtonPanel.TabIndex = 21;
            this.createShortcutButtonPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.createShortcutButtonPanel_MouseClick);
            // 
            // changeBackgroundButtonPanel
            // 
            this.changeBackgroundButtonPanel.AllowDragWindow = true;
            this.changeBackgroundButtonPanel.ButtonText = "";
            this.changeBackgroundButtonPanel.Location = new System.Drawing.Point(518, 24);
            this.changeBackgroundButtonPanel.Name = "changeBackgroundButtonPanel";
            this.changeBackgroundButtonPanel.Size = new System.Drawing.Size(85, 23);
            this.changeBackgroundButtonPanel.Style = WindowsCalendar.ButtonPanelStyle.LightBackground;
            this.changeBackgroundButtonPanel.TabIndex = 20;
            this.changeBackgroundButtonPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.changeBackgroundButtonPanel_MouseClick);
            // 
            // weeklyCalendarContentPanel
            // 
            this.weeklyCalendarContentPanel.AllowDragWindow = true;
            this.weeklyCalendarContentPanel.Controls.Add(this.weeklyCirclePanel);
            this.weeklyCalendarContentPanel.Location = new System.Drawing.Point(321, 5);
            this.weeklyCalendarContentPanel.Name = "weeklyCalendarContentPanel";
            this.weeklyCalendarContentPanel.Size = new System.Drawing.Size(49, 82);
            this.weeklyCalendarContentPanel.TabIndex = 9;
            this.weeklyCalendarContentPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.weeklyCalendarContentPanel_Paint);
            // 
            // weeklyCirclePanel
            // 
            this.weeklyCirclePanel.AllowDragWindow = true;
            this.weeklyCirclePanel.Location = new System.Drawing.Point(3, 3);
            this.weeklyCirclePanel.Name = "weeklyCirclePanel";
            this.weeklyCirclePanel.Size = new System.Drawing.Size(82, 62);
            this.weeklyCirclePanel.TabIndex = 4;
            // 
            // lunarInfoPanel
            // 
            this.lunarInfoPanel.AllowDragWindow = true;
            this.lunarInfoPanel.Location = new System.Drawing.Point(655, 262);
            this.lunarInfoPanel.Name = "lunarInfoPanel";
            this.lunarInfoPanel.Size = new System.Drawing.Size(312, 260);
            this.lunarInfoPanel.TabIndex = 6;
            this.lunarInfoPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.lunarInfoPanel_Paint);
            // 
            // suitAndAvoidPanel
            // 
            this.suitAndAvoidPanel.AllowDragWindow = true;
            this.suitAndAvoidPanel.Location = new System.Drawing.Point(818, 93);
            this.suitAndAvoidPanel.Name = "suitAndAvoidPanel";
            this.suitAndAvoidPanel.Size = new System.Drawing.Size(150, 140);
            this.suitAndAvoidPanel.TabIndex = 5;
            this.suitAndAvoidPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.suitAndAvoidPanel_Paint);
            // 
            // detailedInfoPanel
            // 
            this.detailedInfoPanel.AllowDragWindow = true;
            this.detailedInfoPanel.Location = new System.Drawing.Point(654, 93);
            this.detailedInfoPanel.Name = "detailedInfoPanel";
            this.detailedInfoPanel.Size = new System.Drawing.Size(150, 140);
            this.detailedInfoPanel.TabIndex = 4;
            this.detailedInfoPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.detailedInfoPanel_Paint);
            // 
            // debugButton
            // 
            this.debugButton.Location = new System.Drawing.Point(226, 1);
            this.debugButton.Name = "debugButton";
            this.debugButton.Size = new System.Drawing.Size(75, 23);
            this.debugButton.TabIndex = 3;
            this.debugButton.Text = "Debug";
            this.debugButton.UseVisualStyleBackColor = true;
            this.debugButton.Click += new System.EventHandler(this.debugButton_Click);
            // 
            // calendarBoxPanel
            // 
            this.calendarBoxPanel.AllowDragWindow = true;
            this.calendarBoxPanel.Controls.Add(this.weekPanel);
            this.calendarBoxPanel.Controls.Add(this.calendarContentPanel);
            this.calendarBoxPanel.Controls.Add(this.dateSelectPanel);
            this.calendarBoxPanel.Location = new System.Drawing.Point(50, 93);
            this.calendarBoxPanel.Name = "calendarBoxPanel";
            this.calendarBoxPanel.Size = new System.Drawing.Size(580, 430);
            this.calendarBoxPanel.TabIndex = 1;
            // 
            // weekPanel
            // 
            this.weekPanel.AllowDragWindow = true;
            this.weekPanel.Location = new System.Drawing.Point(0, 35);
            this.weekPanel.Name = "weekPanel";
            this.weekPanel.Size = new System.Drawing.Size(580, 26);
            this.weekPanel.TabIndex = 0;
            this.weekPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.weekPanel_Paint);
            // 
            // calendarContentPanel
            // 
            this.calendarContentPanel.AllowDragWindow = true;
            this.calendarContentPanel.Controls.Add(this.circlePanel);
            this.calendarContentPanel.Location = new System.Drawing.Point(0, 61);
            this.calendarContentPanel.Name = "calendarContentPanel";
            this.calendarContentPanel.Size = new System.Drawing.Size(580, 361);
            this.calendarContentPanel.TabIndex = 1;
            this.calendarContentPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.calendarContentPanel_Paint);
            this.calendarContentPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.calendarContentPanel_MouseClick);
            this.calendarContentPanel.MouseLeave += new System.EventHandler(this.calendarContentPanel_MouseLeave);
            this.calendarContentPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.calendarContentPanel_MouseMove);
            // 
            // circlePanel
            // 
            this.circlePanel.AllowDragWindow = true;
            this.circlePanel.Location = new System.Drawing.Point(3, 3);
            this.circlePanel.Name = "circlePanel";
            this.circlePanel.Size = new System.Drawing.Size(82, 62);
            this.circlePanel.TabIndex = 3;
            this.circlePanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.circlePanel_MouseClick);
            this.circlePanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.circlePanel_MouseMove);
            // 
            // dateSelectPanel
            // 
            this.dateSelectPanel.AllowDragWindow = true;
            this.dateSelectPanel.Controls.Add(this.dateLabel);
            this.dateSelectPanel.Location = new System.Drawing.Point(0, 0);
            this.dateSelectPanel.Name = "dateSelectPanel";
            this.dateSelectPanel.Size = new System.Drawing.Size(580, 35);
            this.dateSelectPanel.TabIndex = 0;
            this.dateSelectPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.dateSelectPanel_Paint);
            this.dateSelectPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dateSelectPanel_MouseClick);
            // 
            // dateLabel
            // 
            this.dateLabel.AutoSize = true;
            this.dateLabel.Font = new System.Drawing.Font("等线", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dateLabel.Location = new System.Drawing.Point(198, 7);
            this.dateLabel.Name = "dateLabel";
            this.dateLabel.Size = new System.Drawing.Size(158, 21);
            this.dateLabel.TabIndex = 0;
            this.dateLabel.Text = "0000年00月00日";
            this.dateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.dateLabel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dateLabel_MouseClick);
            // 
            // titlePanel
            // 
            this.titlePanel.AllowDragWindow = true;
            this.titlePanel.Location = new System.Drawing.Point(50, 30);
            this.titlePanel.Name = "titlePanel";
            this.titlePanel.Size = new System.Drawing.Size(251, 36);
            this.titlePanel.TabIndex = 0;
            this.titlePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.titlePanel_Paint);
            this.titlePanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.titlePanel_MouseClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(997, 561);
            this.Controls.Add(this.backgroundPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.TextChanged += new System.EventHandler(this.Form1_TextChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MainForm_KeyPress);
            this.backgroundPanel.ResumeLayout(false);
            this.backgroundPanel.PerformLayout();
            this.weeklyCalendarContentPanel.ResumeLayout(false);
            this.calendarBoxPanel.ResumeLayout(false);
            this.calendarContentPanel.ResumeLayout(false);
            this.dateSelectPanel.ResumeLayout(false);
            this.dateSelectPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button closeButton;
        private OptimizedPanel backgroundPanel;
        private System.Windows.Forms.Timer debugTimer;
        private OptimizedPanel titlePanel;
        private OptimizedPanel calendarBoxPanel;
        private OptimizedPanel dateSelectPanel;
        private System.Windows.Forms.Label dateLabel;
        private OptimizedPanel calendarContentPanel;
        private OptimizedPanel weekPanel;
        private System.Windows.Forms.MonthCalendar smallMonthCalendar;
        private System.Windows.Forms.Button debugButton;
        private OptimizedPanel detailedInfoPanel;
        private OptimizedPanel suitAndAvoidPanel;
        private OptimizedPanel circlePanel;
        private OptimizedPanel lunarInfoPanel;
        private WatermarkedTextBox notepadTextBox;
        private OptimizedPanel weeklyCalendarContentPanel;
        private OptimizedPanel weeklyCirclePanel;
        private CustomizedButtonPanel changeBackgroundButtonPanel;
        private CustomizedButtonPanel moreButtonPanel;
        private CustomizedButtonPanel feedbackButtonPanel;
        private CustomizedButtonPanel supportAuthorPanel;
        private CustomizedButtonPanel viewHomepageButtonPanel;
        private CustomizedButtonPanel helpButtonPanel;
        private CustomizedButtonPanel checkUpdatesButtonPanel;
        private CustomizedButtonPanel createShortcutButtonPanel;
        private CustomizedButtonPanel exportTextButtonPanel;
        private CustomizedButtonPanel switchCalendarModeButtonPanel;
        private CustomizedButtonPanel exportTextAllButtonPanel;
        private CustomizedButtonPanel exportTextSingleButtonPanel;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private OptimizedPanel minimizeButtonPanel;
        private OptimizedPanel closeButtonPanel;
        private OptimizedPanel helpImagePanel;
        private CustomizedButtonPanel nextHelpImageButtonPanel;
        private CustomizedButtonPanel exitHelpModeButtonPanel;
    }
}

