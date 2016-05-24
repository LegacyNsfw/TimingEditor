namespace NSFW.TimingEditor
{
    partial class TimingForm
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
            this.tableList = new System.Windows.Forms.ListBox();
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.horizontalPanel = new System.Windows.Forms.Panel();
            this.verticalPanel = new System.Windows.Forms.Panel();
            this.copyButton = new System.Windows.Forms.Button();
            this.pasteButton = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.smoothButton = new System.Windows.Forms.Button();
            this.logOverlayButton = new System.Windows.Forms.Button();
            this.redoButton = new System.Windows.Forms.Button();
            this.undoButton = new System.Windows.Forms.Button();
            this.smoothComboBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableList
            // 
            this.tableList.FormattingEnabled = true;
            this.tableList.IntegralHeight = false;
            this.tableList.Location = new System.Drawing.Point(12, 12);
            this.tableList.Name = "tableList";
            this.tableList.Size = new System.Drawing.Size(200, 99);
            this.tableList.TabIndex = 0;
            this.tableList.SelectedIndexChanged += new System.EventHandler(this.tableList_SelectedIndexChanged);
            this.tableList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tableList_MouseDown);
            // 
            // dataGrid
            // 
            this.dataGrid.AllowUserToAddRows = false;
            this.dataGrid.AllowUserToDeleteRows = false;
            this.dataGrid.AllowUserToResizeColumns = false;
            this.dataGrid.AllowUserToResizeRows = false;
            this.dataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGrid.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid.Location = new System.Drawing.Point(218, 218);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.Size = new System.Drawing.Size(517, 332);
            this.dataGrid.TabIndex = 1;
            this.dataGrid.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGrid_CellBeginEdit);
            this.dataGrid.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrid_CellEnter);
            this.dataGrid.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrid_CellLeave);
            this.dataGrid.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrid_CellMouseEnter);
            this.dataGrid.CellStateChanged += new System.Windows.Forms.DataGridViewCellStateChangedEventHandler(this.dataGrid_CellStateChanged);
            this.dataGrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrid_CellValueChanged);
            this.dataGrid.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGrid_ColumnHeaderMouseClick);
            this.dataGrid.CurrentCellChanged += new System.EventHandler(this.dataGrid_CurrentCellChanged);
            this.dataGrid.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGrid_EditingControlShowing);
            this.dataGrid.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGrid_RowHeaderMouseClick);
            this.dataGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGrid_KeyDown);
            this.dataGrid.Leave += new System.EventHandler(this.dataGrid_Leave);
            // 
            // horizontalPanel
            // 
            this.horizontalPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.horizontalPanel.BackColor = System.Drawing.SystemColors.Window;
            this.horizontalPanel.Location = new System.Drawing.Point(218, 12);
            this.horizontalPanel.Name = "horizontalPanel";
            this.horizontalPanel.Size = new System.Drawing.Size(517, 200);
            this.horizontalPanel.TabIndex = 2;
            this.horizontalPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.horizontalPanel_MouseDown);
            // 
            // verticalPanel
            // 
            this.verticalPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.verticalPanel.BackColor = System.Drawing.SystemColors.Window;
            this.verticalPanel.Location = new System.Drawing.Point(12, 218);
            this.verticalPanel.Name = "verticalPanel";
            this.verticalPanel.Size = new System.Drawing.Size(200, 332);
            this.verticalPanel.TabIndex = 3;
            this.verticalPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.verticalPanel_MouseDown);
            // 
            // copyButton
            // 
            this.copyButton.Location = new System.Drawing.Point(12, 117);
            this.copyButton.Name = "copyButton";
            this.copyButton.Size = new System.Drawing.Size(95, 23);
            this.copyButton.TabIndex = 4;
            this.copyButton.Text = "&Copy";
            this.copyButton.UseVisualStyleBackColor = true;
            this.copyButton.Click += new System.EventHandler(this.copyButton_Click);
            // 
            // pasteButton
            // 
            this.pasteButton.Location = new System.Drawing.Point(125, 117);
            this.pasteButton.Name = "pasteButton";
            this.pasteButton.Size = new System.Drawing.Size(87, 23);
            this.pasteButton.TabIndex = 5;
            this.pasteButton.Text = "&Paste";
            this.pasteButton.UseVisualStyleBackColor = true;
            this.pasteButton.Click += new System.EventHandler(this.pasteButton_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 559);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(747, 22);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(59, 17);
            this.toolStripStatusLabel1.Text = "statusText";
            // 
            // smoothButton
            // 
            this.smoothButton.Location = new System.Drawing.Point(93, 189);
            this.smoothButton.Name = "smoothButton";
            this.smoothButton.Size = new System.Drawing.Size(51, 23);
            this.smoothButton.TabIndex = 7;
            this.smoothButton.Text = "&Smooth";
            this.smoothButton.UseVisualStyleBackColor = true;
            this.smoothButton.Click += new System.EventHandler(this.smoothButton_Click);
            // 
            // logOverlayButton
            // 
            this.logOverlayButton.Location = new System.Drawing.Point(93, 161);
            this.logOverlayButton.Name = "logOverlayButton";
            this.logOverlayButton.Size = new System.Drawing.Size(119, 23);
            this.logOverlayButton.TabIndex = 8;
            this.logOverlayButton.Text = "Log &Overlay";
            this.logOverlayButton.UseVisualStyleBackColor = true;
            this.logOverlayButton.Click += new System.EventHandler(this.logOverlayButton_Click);
            // 
            // redoButton
            // 
            this.redoButton.Location = new System.Drawing.Point(12, 161);
            this.redoButton.Name = "redoButton";
            this.redoButton.Size = new System.Drawing.Size(75, 23);
            this.redoButton.TabIndex = 9;
            this.redoButton.Text = "&Redo";
            this.redoButton.UseVisualStyleBackColor = true;
            this.redoButton.Click += new System.EventHandler(this.redoButton_Click);
            // 
            // undoButton
            // 
            this.undoButton.Location = new System.Drawing.Point(12, 189);
            this.undoButton.Name = "undoButton";
            this.undoButton.Size = new System.Drawing.Size(75, 23);
            this.undoButton.TabIndex = 10;
            this.undoButton.Text = "&Undo";
            this.undoButton.UseVisualStyleBackColor = true;
            this.undoButton.Click += new System.EventHandler(this.undoButton_Click);
            // 
            // smoothComboBox
            // 
            this.smoothComboBox.FormattingEnabled = true;
            this.smoothComboBox.Items.AddRange(new object[] {
            "Table",
            "Horiz",
            "Vert"});
            this.smoothComboBox.Location = new System.Drawing.Point(150, 190);
            this.smoothComboBox.Name = "smoothComboBox";
            this.smoothComboBox.Size = new System.Drawing.Size(62, 21);
            this.smoothComboBox.TabIndex = 11;
            this.smoothComboBox.SelectedIndexChanged += new System.EventHandler(this.smoothComboBox_SelectedIndexChanged);
            this.smoothComboBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.smoothComboBox_MouseDown);
            // 
            // TimingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(747, 581);
            this.Controls.Add(this.smoothComboBox);
            this.Controls.Add(this.undoButton);
            this.Controls.Add(this.redoButton);
            this.Controls.Add(this.logOverlayButton);
            this.Controls.Add(this.smoothButton);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.pasteButton);
            this.Controls.Add(this.copyButton);
            this.Controls.Add(this.verticalPanel);
            this.Controls.Add(this.horizontalPanel);
            this.Controls.Add(this.dataGrid);
            this.Controls.Add(this.tableList);
            this.Name = "TimingForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResizeBegin += new System.EventHandler(this.TimingForm_ResizeBegin);
            this.LocationChanged += new System.EventHandler(this.TimingForm_LocationChanged);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TimingForm_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox tableList;
        private System.Windows.Forms.DataGridView dataGrid;
        private System.Windows.Forms.Panel horizontalPanel;
        private System.Windows.Forms.Panel verticalPanel;
        private System.Windows.Forms.Button copyButton;
        private System.Windows.Forms.Button pasteButton;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Button smoothButton;
        private System.Windows.Forms.Button logOverlayButton;
        private System.Windows.Forms.Button redoButton;
        private System.Windows.Forms.Button undoButton;
        private System.Windows.Forms.ComboBox smoothComboBox;

    }
}

