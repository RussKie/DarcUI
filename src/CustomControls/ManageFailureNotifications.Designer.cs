
namespace DarcUI.CustomControls
{
    partial class ManageFailureNotifications
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
            this.pnlMain = new System.Windows.Forms.Panel();
            this.txtTags = new System.Windows.Forms.TextBox();
            this.pnlControls = new System.Windows.Forms.FlowLayoutPanel();
            this.btnOk = new System.Windows.Forms.Button();
            this.pnlMain.SuspendLayout();
            this.pnlControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pnlMain.Controls.Add(this.txtTags);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(8);
            this.pnlMain.Size = new System.Drawing.Size(430, 40);
            this.pnlMain.TabIndex = 0;
            // 
            // txtTags
            // 
            this.txtTags.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTags.Location = new System.Drawing.Point(8, 8);
            this.txtTags.MaxLength = 500;
            this.txtTags.Name = "txtTags";
            this.txtTags.Size = new System.Drawing.Size(414, 23);
            this.txtTags.TabIndex = 0;
            // 
            // pnlControls
            // 
            this.pnlControls.AutoSize = true;
            this.pnlControls.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlControls.Controls.Add(this.btnOk);
            this.pnlControls.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlControls.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.pnlControls.Location = new System.Drawing.Point(0, 40);
            this.pnlControls.Name = "pnlControls";
            this.pnlControls.Padding = new System.Windows.Forms.Padding(4);
            this.pnlControls.Size = new System.Drawing.Size(430, 37);
            this.pnlControls.TabIndex = 1;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(344, 7);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "&Update";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // ManageFailureNotifications
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 77);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlControls);
            this.Icon = global::DarcUI.Properties.Resources.dotnet;
            this.Name = "ManageFailureNotifications";
            this.Text = "Manage failure notification tags";
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.pnlControls.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.FlowLayoutPanel pnlControls;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TextBox txtTags;
    }
}
