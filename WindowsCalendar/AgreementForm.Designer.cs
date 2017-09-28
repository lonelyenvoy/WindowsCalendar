namespace WindowsCalendar
{
    partial class AgreementForm
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
            this.backgroundPanel = new WindowsCalendar.OptimizedPanel();
            this.viewHomepageButtonPanel = new WindowsCalendar.CustomizedButtonPanel();
            this.agreeButtonPanel = new WindowsCalendar.CustomizedButtonPanel();
            this.exitButtonPanel = new WindowsCalendar.CustomizedButtonPanel();
            this.backgroundPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // backgroundPanel
            // 
            this.backgroundPanel.AllowDragWindow = true;
            this.backgroundPanel.Controls.Add(this.viewHomepageButtonPanel);
            this.backgroundPanel.Controls.Add(this.agreeButtonPanel);
            this.backgroundPanel.Controls.Add(this.exitButtonPanel);
            this.backgroundPanel.Location = new System.Drawing.Point(0, 0);
            this.backgroundPanel.Name = "backgroundPanel";
            this.backgroundPanel.Size = new System.Drawing.Size(340, 312);
            this.backgroundPanel.TabIndex = 0;
            this.backgroundPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.backgroundPanel_Paint);
            // 
            // viewHomepageButtonPanel
            // 
            this.viewHomepageButtonPanel.AllowDragWindow = true;
            this.viewHomepageButtonPanel.ButtonText = "";
            this.viewHomepageButtonPanel.Location = new System.Drawing.Point(12, 277);
            this.viewHomepageButtonPanel.Name = "viewHomepageButtonPanel";
            this.viewHomepageButtonPanel.Size = new System.Drawing.Size(85, 23);
            this.viewHomepageButtonPanel.Style = WindowsCalendar.ButtonPanelStyle.LightBackground;
            this.viewHomepageButtonPanel.TabIndex = 2;
            this.viewHomepageButtonPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.viewHomepageButtonPanel_MouseClick);
            // 
            // agreeButtonPanel
            // 
            this.agreeButtonPanel.AllowDragWindow = true;
            this.agreeButtonPanel.ButtonText = "";
            this.agreeButtonPanel.Location = new System.Drawing.Point(152, 277);
            this.agreeButtonPanel.Name = "agreeButtonPanel";
            this.agreeButtonPanel.Size = new System.Drawing.Size(85, 23);
            this.agreeButtonPanel.Style = WindowsCalendar.ButtonPanelStyle.LightBackground;
            this.agreeButtonPanel.TabIndex = 1;
            this.agreeButtonPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.agreeButtonPanel_MouseClick);
            // 
            // exitButtonPanel
            // 
            this.exitButtonPanel.AllowDragWindow = true;
            this.exitButtonPanel.ButtonText = "";
            this.exitButtonPanel.Location = new System.Drawing.Point(243, 277);
            this.exitButtonPanel.Name = "exitButtonPanel";
            this.exitButtonPanel.Size = new System.Drawing.Size(85, 23);
            this.exitButtonPanel.Style = WindowsCalendar.ButtonPanelStyle.LightBackground;
            this.exitButtonPanel.TabIndex = 0;
            this.exitButtonPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.exitButtonPanel_MouseClick);
            // 
            // AgreementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 312);
            this.Controls.Add(this.backgroundPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AgreementForm";
            this.Text = "AgreementForm";
            this.Load += new System.EventHandler(this.AgreementForm_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AgreementForm_KeyPress);
            this.backgroundPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private OptimizedPanel backgroundPanel;
        private CustomizedButtonPanel exitButtonPanel;
        private CustomizedButtonPanel agreeButtonPanel;
        private CustomizedButtonPanel viewHomepageButtonPanel;
    }
}