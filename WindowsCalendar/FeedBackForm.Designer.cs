namespace WindowsCalendar
{
    partial class FeedbackForm
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
            this.contactsTextBox = new WindowsCalendar.WatermarkedTextBox();
            this.feedbackContentTextBox = new WindowsCalendar.WatermarkedTextBox();
            this.closeButtonPanel = new WindowsCalendar.OptimizedPanel();
            this.submitButtonPanel = new WindowsCalendar.CustomizedButtonPanel();
            this.cancelButtonPanel = new WindowsCalendar.CustomizedButtonPanel();
            this.SuspendLayout();
            // 
            // contactsTextBox
            // 
            this.contactsTextBox.BackGroundText = "";
            this.contactsTextBox.Location = new System.Drawing.Point(12, 181);
            this.contactsTextBox.Name = "contactsTextBox";
            this.contactsTextBox.Size = new System.Drawing.Size(260, 21);
            this.contactsTextBox.TabIndex = 1;
            // 
            // feedbackContentTextBox
            // 
            this.feedbackContentTextBox.AcceptsReturn = true;
            this.feedbackContentTextBox.AcceptsTab = true;
            this.feedbackContentTextBox.BackGroundText = "";
            this.feedbackContentTextBox.Location = new System.Drawing.Point(12, 40);
            this.feedbackContentTextBox.Multiline = true;
            this.feedbackContentTextBox.Name = "feedbackContentTextBox";
            this.feedbackContentTextBox.Size = new System.Drawing.Size(260, 135);
            this.feedbackContentTextBox.TabIndex = 0;
            // 
            // closeButtonPanel
            // 
            this.closeButtonPanel.AllowDragWindow = true;
            this.closeButtonPanel.Location = new System.Drawing.Point(259, 12);
            this.closeButtonPanel.Name = "closeButtonPanel";
            this.closeButtonPanel.Size = new System.Drawing.Size(13, 13);
            this.closeButtonPanel.TabIndex = 9;
            this.closeButtonPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.closeButtonPanel_Paint);
            this.closeButtonPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.closeButtonPanel_MouseClick);
            this.closeButtonPanel.MouseEnter += new System.EventHandler(this.closeButtonPanel_MouseEnter);
            this.closeButtonPanel.MouseLeave += new System.EventHandler(this.closeButtonPanel_MouseLeave);
            // 
            // submitButtonPanel
            // 
            this.submitButtonPanel.AllowDragWindow = true;
            this.submitButtonPanel.ButtonText = "";
            this.submitButtonPanel.Location = new System.Drawing.Point(96, 226);
            this.submitButtonPanel.Name = "submitButtonPanel";
            this.submitButtonPanel.Size = new System.Drawing.Size(85, 23);
            this.submitButtonPanel.Style = WindowsCalendar.ButtonPanelStyle.LightBackground;
            this.submitButtonPanel.TabIndex = 11;
            this.submitButtonPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.submitButtonPanel_MouseClick);
            // 
            // cancelButtonPanel
            // 
            this.cancelButtonPanel.AllowDragWindow = true;
            this.cancelButtonPanel.ButtonText = "";
            this.cancelButtonPanel.Location = new System.Drawing.Point(187, 226);
            this.cancelButtonPanel.Name = "cancelButtonPanel";
            this.cancelButtonPanel.Size = new System.Drawing.Size(85, 23);
            this.cancelButtonPanel.Style = WindowsCalendar.ButtonPanelStyle.LightBackground;
            this.cancelButtonPanel.TabIndex = 10;
            this.cancelButtonPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cancelButtonPanel_MouseClick);
            // 
            // FeedbackForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.submitButtonPanel);
            this.Controls.Add(this.cancelButtonPanel);
            this.Controls.Add(this.closeButtonPanel);
            this.Controls.Add(this.feedbackContentTextBox);
            this.Controls.Add(this.contactsTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FeedbackForm";
            this.ShowInTaskbar = false;
            this.Text = "FeedBackForm";
            this.Load += new System.EventHandler(this.FeedbackForm_Load);
            this.Shown += new System.EventHandler(this.FeedbackForm_Shown);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FeedbackForm_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FeedbackForm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FeedbackForm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FeedbackForm_MouseUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private WatermarkedTextBox contactsTextBox;
        private WatermarkedTextBox feedbackContentTextBox;
        private OptimizedPanel closeButtonPanel;
        private CustomizedButtonPanel cancelButtonPanel;
        private CustomizedButtonPanel submitButtonPanel;
    }
}