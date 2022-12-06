namespace DarcUI
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
            System.Windows.Forms.ToolStripMenuItem groupByToolStripMenuItem;
            System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.propertyGrid1 = new DarcUI.AddRemovePropertyGrid();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.groupByOption1 = new System.Windows.Forms.ToolStripMenuItem();
            this.groupByOption2 = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageSubscriptions = new System.Windows.Forms.TabPage();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbtnRefresh = new System.Windows.Forms.ToolStripButton();
            this.tabPageLog = new System.Windows.Forms.TabPage();
            this.rtbCommandLog = new System.Windows.Forms.RichTextBox();
            groupByToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.tableLayoutPanel1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPageSubscriptions.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tabPageLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupByToolStripMenuItem
            // 
            groupByToolStripMenuItem.Enabled = false;
            groupByToolStripMenuItem.Name = "groupByToolStripMenuItem";
            groupByToolStripMenuItem.Size = new System.Drawing.Size(260, 22);
            groupByToolStripMenuItem.Text = "Group by";
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new System.Drawing.Size(257, 6);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.propertyGrid1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.treeView1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(8, 8);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 451F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1062, 560);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Location = new System.Drawing.Point(534, 3);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(525, 554);
            this.propertyGrid1.TabIndex = 2;
            this.propertyGrid1.NewClicked += new System.EventHandler(this.propertyGrid1_NewClicked);
            this.propertyGrid1.DeleteClicked += new System.EventHandler(this.propertyGrid1_DeleteClicked);
            this.propertyGrid1.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid1_PropertyValueChanged);
            // 
            // treeView1
            // 
            this.treeView1.ContextMenuStrip = this.contextMenuStrip1;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.HideSelection = false;
            this.treeView1.Location = new System.Drawing.Point(3, 3);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(525, 554);
            this.treeView1.TabIndex = 4;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            groupByToolStripMenuItem,
            toolStripMenuItem1,
            this.groupByOption1,
            this.groupByOption2});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(261, 76);
            // 
            // groupByOption1
            // 
            this.groupByOption1.Checked = true;
            this.groupByOption1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.groupByOption1.Name = "groupByOption1";
            this.groupByOption1.Size = new System.Drawing.Size(260, 22);
            this.groupByOption1.Text = "Channel > Source > Repo > Branch";
            this.groupByOption1.Click += new System.EventHandler(this.groupByOption1_Click);
            // 
            // groupByOption2
            // 
            this.groupByOption2.Name = "groupByOption2";
            this.groupByOption2.Size = new System.Drawing.Size(260, 22);
            this.groupByOption2.Text = "Channel > Repo > Branch > Source";
            this.groupByOption2.Click += new System.EventHandler(this.groupByOption2_Click);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageSubscriptions);
            this.tabControl.Controls.Add(this.tabPageLog);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.ItemSize = new System.Drawing.Size(58, 32);
            this.tabControl.Location = new System.Drawing.Point(8, 25);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1086, 616);
            this.tabControl.TabIndex = 0;
            // 
            // tabPageSubscriptions
            // 
            this.tabPageSubscriptions.Controls.Add(this.tableLayoutPanel1);
            this.tabPageSubscriptions.Location = new System.Drawing.Point(4, 36);
            this.tabPageSubscriptions.Name = "tabPageSubscriptions";
            this.tabPageSubscriptions.Padding = new System.Windows.Forms.Padding(8);
            this.tabPageSubscriptions.Size = new System.Drawing.Size(1078, 576);
            this.tabPageSubscriptions.TabIndex = 0;
            this.tabPageSubscriptions.Text = "Subscriptions";
            this.tabPageSubscriptions.UseVisualStyleBackColor = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnRefresh});
            this.toolStrip1.Location = new System.Drawing.Point(8, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1086, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbtnRefresh
            // 
            this.tsbtnRefresh.Image = global::DarcUI.Properties.Resources.refresh;
            this.tsbtnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnRefresh.Name = "tsbtnRefresh";
            this.tsbtnRefresh.Size = new System.Drawing.Size(66, 22);
            this.tsbtnRefresh.Text = "&Refresh";
            this.tsbtnRefresh.Click += new System.EventHandler(this.tsbtnRefresh_Click);
            // 
            // tabPageLog
            // 
            this.tabPageLog.Controls.Add(this.rtbCommandLog);
            this.tabPageLog.Location = new System.Drawing.Point(4, 36);
            this.tabPageLog.Name = "tabPageLog";
            this.tabPageLog.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLog.Size = new System.Drawing.Size(1078, 576);
            this.tabPageLog.TabIndex = 1;
            this.tabPageLog.Text = "Command Log";
            this.tabPageLog.UseVisualStyleBackColor = true;
            // 
            // rtbCommandLog
            // 
            this.rtbCommandLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtbCommandLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbCommandLog.Location = new System.Drawing.Point(3, 3);
            this.rtbCommandLog.Name = "rtbCommandLog";
            this.rtbCommandLog.ReadOnly = true;
            this.rtbCommandLog.Size = new System.Drawing.Size(1072, 570);
            this.rtbCommandLog.TabIndex = 0;
            this.rtbCommandLog.Text = "";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1102, 649);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.toolStrip1);
            this.DoubleBuffered = true;
            this.Icon = global::DarcUI.Properties.Resources.dotnet;
            this.Name = "MainForm";
            this.Padding = new System.Windows.Forms.Padding(8, 0, 8, 8);
            this.Text = "Darc UI";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabPageSubscriptions.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabPageLog.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageSubscriptions;
        private AddRemovePropertyGrid propertyGrid1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbtnRefresh;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem groupByOption1;
        private System.Windows.Forms.ToolStripMenuItem groupByOption2;
        private TabPage tabPageLog;
        private RichTextBox rtbCommandLog;
    }
}

