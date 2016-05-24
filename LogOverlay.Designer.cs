namespace NSFW.TimingEditor
{
    partial class LogOverlay
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
            this.headerListBox = new System.Windows.Forms.CheckedListBox();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.xAxisLabel = new System.Windows.Forms.Label();
            this.yAxisLabel = new System.Windows.Forms.Label();
            this.xAxisComboBox = new System.Windows.Forms.ComboBox();
            this.yAxisComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // headerListBox
            // 
            this.headerListBox.CheckOnClick = true;
            this.headerListBox.FormattingEnabled = true;
            this.headerListBox.Location = new System.Drawing.Point(3, 62);
            this.headerListBox.Name = "headerListBox";
            this.headerListBox.Size = new System.Drawing.Size(269, 199);
            this.headerListBox.Sorted = true;
            this.headerListBox.TabIndex = 0;
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(49, 268);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(147, 268);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // xAxisLabel
            // 
            this.xAxisLabel.AutoSize = true;
            this.xAxisLabel.Location = new System.Drawing.Point(0, 9);
            this.xAxisLabel.Name = "xAxisLabel";
            this.xAxisLabel.Size = new System.Drawing.Size(39, 13);
            this.xAxisLabel.TabIndex = 3;
            this.xAxisLabel.Text = "X-Axis:";
            // 
            // yAxisLabel
            // 
            this.yAxisLabel.AutoSize = true;
            this.yAxisLabel.Location = new System.Drawing.Point(0, 36);
            this.yAxisLabel.Name = "yAxisLabel";
            this.yAxisLabel.Size = new System.Drawing.Size(39, 13);
            this.yAxisLabel.TabIndex = 4;
            this.yAxisLabel.Text = "Y-Axis:";
            // 
            // xAxisComboBox
            // 
            this.xAxisComboBox.FormattingEnabled = true;
            this.xAxisComboBox.Location = new System.Drawing.Point(38, 6);
            this.xAxisComboBox.Name = "xAxisComboBox";
            this.xAxisComboBox.Size = new System.Drawing.Size(234, 21);
            this.xAxisComboBox.TabIndex = 5;
            // 
            // yAxisComboBox
            // 
            this.yAxisComboBox.FormattingEnabled = true;
            this.yAxisComboBox.Location = new System.Drawing.Point(38, 33);
            this.yAxisComboBox.Name = "yAxisComboBox";
            this.yAxisComboBox.Size = new System.Drawing.Size(234, 21);
            this.yAxisComboBox.TabIndex = 6;
            // 
            // LogOverlay
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(274, 302);
            this.ControlBox = false;
            this.Controls.Add(this.yAxisComboBox);
            this.Controls.Add(this.xAxisComboBox);
            this.Controls.Add(this.yAxisLabel);
            this.Controls.Add(this.xAxisLabel);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.headerListBox);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(290, 340);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(290, 340);
            this.Name = "LogOverlay";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select Parameters";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox headerListBox;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Label xAxisLabel;
        private System.Windows.Forms.Label yAxisLabel;
        private System.Windows.Forms.ComboBox xAxisComboBox;
        private System.Windows.Forms.ComboBox yAxisComboBox;
    }
}