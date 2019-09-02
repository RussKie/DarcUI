namespace DarcUI
{
    partial class Form1
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
            System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.tlpnlSubscriptions = new System.Windows.Forms.TableLayoutPanel();
            this.listView1 = new System.Windows.Forms.ListView();
            this.cboSubscriptionTo = new System.Windows.Forms.ComboBox();
            this.lblSubscriptionTo = new System.Windows.Forms.Label();
            this.cboSubscriptionFrom = new System.Windows.Forms.ComboBox();
            this.lblSubscriptionFrom = new System.Windows.Forms.Label();
            this.btnSubscriptionRefresh = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageSubscriptions = new System.Windows.Forms.TabPage();
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            tableLayoutPanel1.SuspendLayout();
            this.tlpnlSubscriptions.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPageSubscriptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(this.propertyGrid1, 1, 1);
            tableLayoutPanel1.Controls.Add(this.tlpnlSubscriptions, 0, 1);
            tableLayoutPanel1.Controls.Add(this.btnSubscriptionRefresh, 0, 0);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(8, 8);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new System.Drawing.Size(504, 337);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Location = new System.Drawing.Point(255, 41);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(246, 293);
            this.propertyGrid1.TabIndex = 2;
            // 
            // tlpnlSubscriptions
            // 
            this.tlpnlSubscriptions.ColumnCount = 1;
            this.tlpnlSubscriptions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpnlSubscriptions.Controls.Add(this.listView1, 0, 4);
            this.tlpnlSubscriptions.Controls.Add(this.cboSubscriptionTo, 0, 3);
            this.tlpnlSubscriptions.Controls.Add(this.lblSubscriptionTo, 0, 2);
            this.tlpnlSubscriptions.Controls.Add(this.cboSubscriptionFrom, 0, 1);
            this.tlpnlSubscriptions.Controls.Add(this.lblSubscriptionFrom, 0, 0);
            this.tlpnlSubscriptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpnlSubscriptions.Enabled = false;
            this.tlpnlSubscriptions.Location = new System.Drawing.Point(3, 41);
            this.tlpnlSubscriptions.Name = "tlpnlSubscriptions";
            this.tlpnlSubscriptions.RowCount = 5;
            this.tlpnlSubscriptions.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpnlSubscriptions.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpnlSubscriptions.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpnlSubscriptions.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpnlSubscriptions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpnlSubscriptions.Size = new System.Drawing.Size(246, 293);
            this.tlpnlSubscriptions.TabIndex = 0;
            // 
            // listView1
            // 
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(3, 83);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(240, 207);
            this.listView1.TabIndex = 4;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // cboSubscriptionTo
            // 
            this.cboSubscriptionTo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboSubscriptionTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSubscriptionTo.FormattingEnabled = true;
            this.cboSubscriptionTo.Location = new System.Drawing.Point(3, 56);
            this.cboSubscriptionTo.Name = "cboSubscriptionTo";
            this.cboSubscriptionTo.Size = new System.Drawing.Size(240, 21);
            this.cboSubscriptionTo.TabIndex = 3;
            // 
            // lblSubscriptionTo
            // 
            this.lblSubscriptionTo.AutoSize = true;
            this.lblSubscriptionTo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSubscriptionTo.Location = new System.Drawing.Point(3, 40);
            this.lblSubscriptionTo.Name = "lblSubscriptionTo";
            this.lblSubscriptionTo.Size = new System.Drawing.Size(240, 13);
            this.lblSubscriptionTo.TabIndex = 2;
            this.lblSubscriptionTo.Text = "To:";
            // 
            // cboSubscriptionFrom
            // 
            this.cboSubscriptionFrom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboSubscriptionFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSubscriptionFrom.FormattingEnabled = true;
            this.cboSubscriptionFrom.Location = new System.Drawing.Point(3, 16);
            this.cboSubscriptionFrom.Name = "cboSubscriptionFrom";
            this.cboSubscriptionFrom.Size = new System.Drawing.Size(240, 21);
            this.cboSubscriptionFrom.TabIndex = 1;
            // 
            // lblSubscriptionFrom
            // 
            this.lblSubscriptionFrom.AutoSize = true;
            this.lblSubscriptionFrom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSubscriptionFrom.Location = new System.Drawing.Point(3, 0);
            this.lblSubscriptionFrom.Name = "lblSubscriptionFrom";
            this.lblSubscriptionFrom.Size = new System.Drawing.Size(240, 13);
            this.lblSubscriptionFrom.TabIndex = 0;
            this.lblSubscriptionFrom.Text = "From:";
            // 
            // btnSubscriptionRefresh
            // 
            this.btnSubscriptionRefresh.Location = new System.Drawing.Point(3, 3);
            this.btnSubscriptionRefresh.Name = "btnSubscriptionRefresh";
            this.btnSubscriptionRefresh.Size = new System.Drawing.Size(100, 32);
            this.btnSubscriptionRefresh.TabIndex = 0;
            this.btnSubscriptionRefresh.Text = "&Refresh";
            this.btnSubscriptionRefresh.UseVisualStyleBackColor = true;
            this.btnSubscriptionRefresh.Click += new System.EventHandler(this.btnSubscriptionRefresh_Click);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageSubscriptions);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.ItemSize = new System.Drawing.Size(58, 32);
            this.tabControl.Location = new System.Drawing.Point(8, 8);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(528, 393);
            this.tabControl.TabIndex = 0;
            // 
            // tabPageSubscriptions
            // 
            this.tabPageSubscriptions.Controls.Add(tableLayoutPanel1);
            this.tabPageSubscriptions.Location = new System.Drawing.Point(4, 36);
            this.tabPageSubscriptions.Name = "tabPageSubscriptions";
            this.tabPageSubscriptions.Padding = new System.Windows.Forms.Padding(8);
            this.tabPageSubscriptions.Size = new System.Drawing.Size(520, 353);
            this.tabPageSubscriptions.TabIndex = 0;
            this.tabPageSubscriptions.Text = "Subscriptions";
            this.tabPageSubscriptions.UseVisualStyleBackColor = true;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(544, 409);
            this.Controls.Add(this.tabControl);
            this.DoubleBuffered = true;
            this.Name = "Form2";
            this.Padding = new System.Windows.Forms.Padding(8);
            this.Text = "Form2";
            tableLayoutPanel1.ResumeLayout(false);
            this.tlpnlSubscriptions.ResumeLayout(false);
            this.tlpnlSubscriptions.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabPageSubscriptions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageSubscriptions;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.TableLayoutPanel tlpnlSubscriptions;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ComboBox cboSubscriptionTo;
        private System.Windows.Forms.Label lblSubscriptionTo;
        private System.Windows.Forms.ComboBox cboSubscriptionFrom;
        private System.Windows.Forms.Label lblSubscriptionFrom;
        private System.Windows.Forms.Button btnSubscriptionRefresh;
    }
}

