namespace DarcUI;

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
        components = new System.ComponentModel.Container();
        ToolStripMenuItem groupByToolStripMenuItem;
        ToolStripSeparator toolStripMenuItem1;
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
        tableLayoutPanel1 = new TableLayoutPanel();
        propertyGrid1 = new ExtendedPropertyGrid();
        treeView1 = new TreeView();
        contextMenuStrip1 = new ContextMenuStrip(components);
        groupByOption1 = new ToolStripMenuItem();
        groupByOption2 = new ToolStripMenuItem();
        tabControl = new TabControl();
        tabPageSubscriptions = new TabPage();
        tabPageLog = new TabPage();
        rtbCommandLog = new RichTextBox();
        toolStrip1 = new ToolStrip();
        tsbtnRefresh = new ToolStripButton();
        groupByToolStripMenuItem = new ToolStripMenuItem();
        toolStripMenuItem1 = new ToolStripSeparator();
        tableLayoutPanel1.SuspendLayout();
        contextMenuStrip1.SuspendLayout();
        tabControl.SuspendLayout();
        tabPageSubscriptions.SuspendLayout();
        tabPageLog.SuspendLayout();
        toolStrip1.SuspendLayout();
        SuspendLayout();
        // 
        // groupByToolStripMenuItem
        // 
        groupByToolStripMenuItem.Enabled = false;
        groupByToolStripMenuItem.Name = "groupByToolStripMenuItem";
        groupByToolStripMenuItem.Size = new Size(260, 22);
        groupByToolStripMenuItem.Text = "Group by";
        // 
        // toolStripMenuItem1
        // 
        toolStripMenuItem1.Name = "toolStripMenuItem1";
        toolStripMenuItem1.Size = new Size(257, 6);
        // 
        // tableLayoutPanel1
        // 
        tableLayoutPanel1.ColumnCount = 2;
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableLayoutPanel1.Controls.Add(propertyGrid1, 1, 0);
        tableLayoutPanel1.Controls.Add(treeView1, 0, 0);
        tableLayoutPanel1.Dock = DockStyle.Fill;
        tableLayoutPanel1.Location = new Point(8, 8);
        tableLayoutPanel1.Name = "tableLayoutPanel1";
        tableLayoutPanel1.RowCount = 1;
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 451F));
        tableLayoutPanel1.Size = new Size(1062, 560);
        tableLayoutPanel1.TabIndex = 0;
        // 
        // propertyGrid1
        // 
        propertyGrid1.Dock = DockStyle.Fill;
        propertyGrid1.Location = new Point(534, 3);
        propertyGrid1.Name = "propertyGrid1";
        propertyGrid1.Size = new Size(525, 554);
        propertyGrid1.TabIndex = 2;
        propertyGrid1.NewClicked += propertyGrid1_NewClicked;
        propertyGrid1.DeleteClicked += propertyGrid1_DeleteClicked;
        propertyGrid1.TriggerClicked += propertyGrid1_TriggerClicked;
        propertyGrid1.PropertyValueChanged += propertyGrid1_PropertyValueChanged;
        // 
        // treeView1
        // 
        treeView1.ContextMenuStrip = contextMenuStrip1;
        treeView1.Dock = DockStyle.Fill;
        treeView1.HideSelection = false;
        treeView1.Location = new Point(3, 3);
        treeView1.Name = "treeView1";
        treeView1.Size = new Size(525, 554);
        treeView1.TabIndex = 4;
        treeView1.AfterSelect += treeView1_AfterSelect;
        // 
        // contextMenuStrip1
        // 
        contextMenuStrip1.Items.AddRange(new ToolStripItem[] { groupByToolStripMenuItem, toolStripMenuItem1, groupByOption1, groupByOption2 });
        contextMenuStrip1.Name = "contextMenuStrip1";
        contextMenuStrip1.Size = new Size(261, 76);
        // 
        // groupByOption1
        // 
        groupByOption1.Checked = true;
        groupByOption1.CheckState = CheckState.Checked;
        groupByOption1.Name = "groupByOption1";
        groupByOption1.Size = new Size(260, 22);
        groupByOption1.Text = "Channel > Source > Repo > Branch";
        groupByOption1.Click += groupByOption1_Click;
        // 
        // groupByOption2
        // 
        groupByOption2.Name = "groupByOption2";
        groupByOption2.Size = new Size(260, 22);
        groupByOption2.Text = "Channel > Repo > Branch > Source";
        groupByOption2.Click += groupByOption2_Click;
        // 
        // tabControl
        // 
        tabControl.Controls.Add(tabPageSubscriptions);
        tabControl.Controls.Add(tabPageLog);
        tabControl.Dock = DockStyle.Fill;
        tabControl.ItemSize = new Size(58, 32);
        tabControl.Location = new Point(8, 25);
        tabControl.Name = "tabControl";
        tabControl.SelectedIndex = 0;
        tabControl.Size = new Size(1086, 616);
        tabControl.TabIndex = 0;
        // 
        // tabPageSubscriptions
        // 
        tabPageSubscriptions.Controls.Add(tableLayoutPanel1);
        tabPageSubscriptions.Location = new Point(4, 36);
        tabPageSubscriptions.Name = "tabPageSubscriptions";
        tabPageSubscriptions.Padding = new Padding(8);
        tabPageSubscriptions.Size = new Size(1078, 576);
        tabPageSubscriptions.TabIndex = 0;
        tabPageSubscriptions.Text = "Subscriptions";
        tabPageSubscriptions.UseVisualStyleBackColor = true;
        // 
        // tabPageLog
        // 
        tabPageLog.Controls.Add(rtbCommandLog);
        tabPageLog.Location = new Point(4, 36);
        tabPageLog.Name = "tabPageLog";
        tabPageLog.Padding = new Padding(3);
        tabPageLog.Size = new Size(1078, 576);
        tabPageLog.TabIndex = 1;
        tabPageLog.Text = "Command Log";
        tabPageLog.UseVisualStyleBackColor = true;
        // 
        // rtbCommandLog
        // 
        rtbCommandLog.BorderStyle = BorderStyle.FixedSingle;
        rtbCommandLog.Dock = DockStyle.Fill;
        rtbCommandLog.Location = new Point(3, 3);
        rtbCommandLog.Name = "rtbCommandLog";
        rtbCommandLog.ReadOnly = true;
        rtbCommandLog.Size = new Size(1072, 570);
        rtbCommandLog.TabIndex = 0;
        rtbCommandLog.Text = "";
        // 
        // toolStrip1
        // 
        toolStrip1.Items.AddRange(new ToolStripItem[] { tsbtnRefresh });
        toolStrip1.Location = new Point(8, 0);
        toolStrip1.Name = "toolStrip1";
        toolStrip1.Size = new Size(1086, 25);
        toolStrip1.TabIndex = 1;
        toolStrip1.Text = "toolStrip1";
        // 
        // tsbtnRefresh
        // 
        tsbtnRefresh.Image = (Image)resources.GetObject("tsbtnRefresh.Image");
        tsbtnRefresh.ImageTransparentColor = Color.Magenta;
        tsbtnRefresh.Name = "tsbtnRefresh";
        tsbtnRefresh.Size = new Size(66, 22);
        tsbtnRefresh.Text = "&Refresh";
        tsbtnRefresh.Click += tsbtnRefresh_Click;
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(96F, 96F);
        AutoScaleMode = AutoScaleMode.Dpi;
        ClientSize = new Size(1102, 649);
        Controls.Add(tabControl);
        Controls.Add(toolStrip1);
        DoubleBuffered = true;
        Icon = (Icon)resources.GetObject("$this.Icon");
        Name = "MainForm";
        Padding = new Padding(8, 0, 8, 8);
        Text = "Darc UI";
        tableLayoutPanel1.ResumeLayout(false);
        contextMenuStrip1.ResumeLayout(false);
        tabControl.ResumeLayout(false);
        tabPageSubscriptions.ResumeLayout(false);
        tabPageLog.ResumeLayout(false);
        toolStrip1.ResumeLayout(false);
        toolStrip1.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private System.Windows.Forms.TabControl tabControl;
    private System.Windows.Forms.TabPage tabPageSubscriptions;
    private ExtendedPropertyGrid propertyGrid1;
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

