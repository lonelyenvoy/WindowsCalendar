namespace WindowsCalendar
{
    partial class LaunchForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.switchFormTimer = new System.Windows.Forms.Timer(this.components);
            this.backgroundLoadWorker = new System.ComponentModel.BackgroundWorker();
            this.backgroundProgressWorker = new System.ComponentModel.BackgroundWorker();
            this.progressTimer = new System.Windows.Forms.Timer(this.components);
            this.infoTimer = new System.Windows.Forms.Timer(this.components);
            this.backgroundPanel = new WindowsCalendar.OptimizedPanel();
            this.closeButtonPanel = new WindowsCalendar.OptimizedPanel();
            this.exitButtonPanel = new WindowsCalendar.OptimizedPanel();
            this.retryButtonPanel = new WindowsCalendar.OptimizedPanel();
            this.okButtonPanel = new WindowsCalendar.OptimizedPanel();
            this.progressPanel = new WindowsCalendar.OptimizedPanel();
            this.loadLabel = new System.Windows.Forms.Label();
            this.progressLabel = new System.Windows.Forms.Label();
            this.hintLabel = new System.Windows.Forms.Label();
            this.agreementTimer = new System.Windows.Forms.Timer(this.components);
            this.backgroundPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // switchFormTimer
            // 
            this.switchFormTimer.Tick += new System.EventHandler(this.switchFormTimer_Tick);
            // 
            // backgroundLoadWorker
            // 
            this.backgroundLoadWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundLoadWorker_DoWork);
            // 
            // backgroundProgressWorker
            // 
            this.backgroundProgressWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundProgressWorker_DoWork);
            this.backgroundProgressWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundProgressWorker_ProgressChanged);
            // 
            // progressTimer
            // 
            this.progressTimer.Tick += new System.EventHandler(this.progressTimer_Tick);
            // 
            // infoTimer
            // 
            this.infoTimer.Tick += new System.EventHandler(this.infoTimer_Tick);
            // 
            // backgroundPanel
            // 
            this.backgroundPanel.AllowDragWindow = true;
            this.backgroundPanel.Controls.Add(this.closeButtonPanel);
            this.backgroundPanel.Controls.Add(this.exitButtonPanel);
            this.backgroundPanel.Controls.Add(this.retryButtonPanel);
            this.backgroundPanel.Controls.Add(this.okButtonPanel);
            this.backgroundPanel.Controls.Add(this.progressPanel);
            this.backgroundPanel.Controls.Add(this.loadLabel);
            this.backgroundPanel.Controls.Add(this.progressLabel);
            this.backgroundPanel.Controls.Add(this.hintLabel);
            this.backgroundPanel.Location = new System.Drawing.Point(0, 0);
            this.backgroundPanel.Name = "backgroundPanel";
            this.backgroundPanel.Size = new System.Drawing.Size(280, 250);
            this.backgroundPanel.TabIndex = 4;
            this.backgroundPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.backgroundPanel_Paint);
            // 
            // closeButtonPanel
            // 
            this.closeButtonPanel.AllowDragWindow = true;
            this.closeButtonPanel.Location = new System.Drawing.Point(255, 12);
            this.closeButtonPanel.Name = "closeButtonPanel";
            this.closeButtonPanel.Size = new System.Drawing.Size(13, 13);
            this.closeButtonPanel.TabIndex = 8;
            this.closeButtonPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.closeButtonPanel_Paint);
            this.closeButtonPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.closeButtonPanel_MouseClick);
            this.closeButtonPanel.MouseEnter += new System.EventHandler(this.closeButtonPanel_MouseEnter);
            this.closeButtonPanel.MouseLeave += new System.EventHandler(this.closeButtonPanel_MouseLeave);
            // 
            // exitButtonPanel
            // 
            this.exitButtonPanel.AllowDragWindow = true;
            this.exitButtonPanel.Location = new System.Drawing.Point(185, 165);
            this.exitButtonPanel.Name = "exitButtonPanel";
            this.exitButtonPanel.Size = new System.Drawing.Size(80, 26);
            this.exitButtonPanel.TabIndex = 7;
            this.exitButtonPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.exitButtonPanel_Paint);
            this.exitButtonPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.exitButtonPanel_MouseClick);
            this.exitButtonPanel.MouseEnter += new System.EventHandler(this.exitButtonPanel_MouseEnter);
            this.exitButtonPanel.MouseLeave += new System.EventHandler(this.exitButtonPanel_MouseLeave);
            // 
            // retryButtonPanel
            // 
            this.retryButtonPanel.AllowDragWindow = true;
            this.retryButtonPanel.Location = new System.Drawing.Point(100, 165);
            this.retryButtonPanel.Name = "retryButtonPanel";
            this.retryButtonPanel.Size = new System.Drawing.Size(80, 26);
            this.retryButtonPanel.TabIndex = 6;
            this.retryButtonPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.retryButtonPanel_Paint);
            this.retryButtonPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.retryButtonPanel_MouseClick);
            this.retryButtonPanel.MouseEnter += new System.EventHandler(this.retryButtonPanel_MouseEnter);
            this.retryButtonPanel.MouseLeave += new System.EventHandler(this.retryButtonPanel_MouseLeave);
            // 
            // okButtonPanel
            // 
            this.okButtonPanel.AllowDragWindow = true;
            this.okButtonPanel.Location = new System.Drawing.Point(15, 165);
            this.okButtonPanel.Name = "okButtonPanel";
            this.okButtonPanel.Size = new System.Drawing.Size(80, 26);
            this.okButtonPanel.TabIndex = 5;
            this.okButtonPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.okButtonPanel_Paint);
            this.okButtonPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.okButtonPanel_MouseClick);
            this.okButtonPanel.MouseEnter += new System.EventHandler(this.okButtonPanel_MouseEnter);
            this.okButtonPanel.MouseLeave += new System.EventHandler(this.okButtonPanel_MouseLeave);
            // 
            // progressPanel
            // 
            this.progressPanel.AllowDragWindow = true;
            this.progressPanel.Location = new System.Drawing.Point(75, 169);
            this.progressPanel.Name = "progressPanel";
            this.progressPanel.Size = new System.Drawing.Size(100, 23);
            this.progressPanel.TabIndex = 4;
            this.progressPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.progressPanel_Paint);
            // 
            // loadLabel
            // 
            this.loadLabel.Location = new System.Drawing.Point(0, 113);
            this.loadLabel.Name = "loadLabel";
            this.loadLabel.Size = new System.Drawing.Size(280, 20);
            this.loadLabel.TabIndex = 3;
            this.loadLabel.Text = "正在连接服务器...";
            this.loadLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // progressLabel
            // 
            this.progressLabel.AutoSize = true;
            this.progressLabel.Location = new System.Drawing.Point(183, 170);
            this.progressLabel.Name = "progressLabel";
            this.progressLabel.Size = new System.Drawing.Size(17, 12);
            this.progressLabel.TabIndex = 2;
            this.progressLabel.Text = "0%";
            // 
            // hintLabel
            // 
            this.hintLabel.Location = new System.Drawing.Point(0, 45);
            this.hintLabel.Name = "hintLabel";
            this.hintLabel.Size = new System.Drawing.Size(280, 40);
            this.hintLabel.TabIndex = 0;
            this.hintLabel.Text = "正在加载，请稍等";
            this.hintLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // agreementTimer
            // 
            this.agreementTimer.Tick += new System.EventHandler(this.agreementTimer_Tick);
            // 
            // LaunchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(280, 250);
            this.Controls.Add(this.backgroundPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LaunchForm";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.LaunchForm_Load);
            this.Shown += new System.EventHandler(this.LaunchForm_Shown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LaunchForm_KeyPress);
            this.backgroundPanel.ResumeLayout(false);
            this.backgroundPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label hintLabel;
        private System.Windows.Forms.Timer switchFormTimer;
        private System.ComponentModel.BackgroundWorker backgroundLoadWorker;
        private System.ComponentModel.BackgroundWorker backgroundProgressWorker;
        private System.Windows.Forms.Label progressLabel;
        private System.Windows.Forms.Label loadLabel;
        private System.Windows.Forms.Timer progressTimer;
        private WindowsCalendar.OptimizedPanel backgroundPanel;
        private WindowsCalendar.OptimizedPanel progressPanel;
        private System.Windows.Forms.Timer infoTimer;
        private OptimizedPanel okButtonPanel;
        private OptimizedPanel retryButtonPanel;
        private OptimizedPanel exitButtonPanel;
        private OptimizedPanel closeButtonPanel;
        private System.Windows.Forms.Timer agreementTimer;
    }
}