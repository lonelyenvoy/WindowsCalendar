namespace WindowsCalendar
{
    partial class AboutForm
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
            this.qrDonatePanel = new WindowsCalendar.OptimizedPanel();
            this.closeButtonPanel = new WindowsCalendar.OptimizedPanel();
            this.supportButtonPanel = new WindowsCalendar.CustomizedButtonPanel();
            this.SuspendLayout();
            // 
            // qrDonatePanel
            // 
            this.qrDonatePanel.AllowDragWindow = true;
            this.qrDonatePanel.Location = new System.Drawing.Point(0, 35);
            this.qrDonatePanel.Name = "qrDonatePanel";
            this.qrDonatePanel.Size = new System.Drawing.Size(300, 180);
            this.qrDonatePanel.TabIndex = 13;
            // 
            // closeButtonPanel
            // 
            this.closeButtonPanel.AllowDragWindow = true;
            this.closeButtonPanel.Location = new System.Drawing.Point(275, 12);
            this.closeButtonPanel.Name = "closeButtonPanel";
            this.closeButtonPanel.Size = new System.Drawing.Size(13, 13);
            this.closeButtonPanel.TabIndex = 9;
            this.closeButtonPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.closeButtonPanel_Paint);
            this.closeButtonPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.closeButtonPanel_MouseClick);
            this.closeButtonPanel.MouseEnter += new System.EventHandler(this.closeButtonPanel_MouseEnter);
            this.closeButtonPanel.MouseLeave += new System.EventHandler(this.closeButtonPanel_MouseLeave);
            // 
            // supportButtonPanel
            // 
            this.supportButtonPanel.AllowDragWindow = true;
            this.supportButtonPanel.ButtonText = "";
            this.supportButtonPanel.Location = new System.Drawing.Point(188, 224);
            this.supportButtonPanel.Name = "supportButtonPanel";
            this.supportButtonPanel.Size = new System.Drawing.Size(100, 40);
            this.supportButtonPanel.Style = WindowsCalendar.ButtonPanelStyle.LightBackground;
            this.supportButtonPanel.TabIndex = 10;
            this.supportButtonPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.supportButtonPanel_MouseClick);
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 276);
            this.Controls.Add(this.qrDonatePanel);
            this.Controls.Add(this.supportButtonPanel);
            this.Controls.Add(this.closeButtonPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AboutForm";
            this.ShowInTaskbar = false;
            this.Text = "About";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AboutForm_FormClosed);
            this.Load += new System.EventHandler(this.AboutForm_Load);
            this.Shown += new System.EventHandler(this.AboutForm_Shown);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.AboutForm_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.AboutForm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.AboutForm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.AboutForm_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion
        private OptimizedPanel closeButtonPanel;
        private CustomizedButtonPanel supportButtonPanel;
        private OptimizedPanel qrDonatePanel;
    }
}