
namespace DarcUI.CustomControls;

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
        pnlMain = new Panel();
        txtTags = new TextBox();
        pnlControls = new FlowLayoutPanel();
        btnOk = new Button();
        pnlMain.SuspendLayout();
        pnlControls.SuspendLayout();
        SuspendLayout();
        // 
        // pnlMain
        // 
        pnlMain.BackColor = SystemColors.ControlLight;
        pnlMain.Controls.Add(txtTags);
        pnlMain.Dock = DockStyle.Fill;
        pnlMain.Location = new Point(0, 0);
        pnlMain.Name = "pnlMain";
        pnlMain.Padding = new Padding(8);
        pnlMain.Size = new Size(430, 40);
        pnlMain.TabIndex = 0;
        // 
        // txtTags
        // 
        txtTags.Dock = DockStyle.Fill;
        txtTags.Location = new Point(8, 8);
        txtTags.MaxLength = 500;
        txtTags.Name = "txtTags";
        txtTags.Size = new Size(414, 23);
        txtTags.TabIndex = 0;
        // 
        // pnlControls
        // 
        pnlControls.AutoSize = true;
        pnlControls.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        pnlControls.Controls.Add(btnOk);
        pnlControls.Dock = DockStyle.Bottom;
        pnlControls.FlowDirection = FlowDirection.RightToLeft;
        pnlControls.Location = new Point(0, 40);
        pnlControls.Name = "pnlControls";
        pnlControls.Padding = new Padding(4);
        pnlControls.Size = new Size(430, 37);
        pnlControls.TabIndex = 1;
        // 
        // btnOk
        // 
        btnOk.DialogResult = DialogResult.OK;
        btnOk.Location = new Point(344, 7);
        btnOk.Name = "btnOk";
        btnOk.Size = new Size(75, 23);
        btnOk.TabIndex = 0;
        btnOk.Text = "&Update";
        btnOk.UseVisualStyleBackColor = true;
        // 
        // ManageFailureNotifications
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(430, 77);
        Controls.Add(pnlMain);
        Controls.Add(pnlControls);
        Icon = Properties.Resources.dotnet;
        Name = "ManageFailureNotifications";
        Text = "Manage failure notification tags";
        pnlMain.ResumeLayout(false);
        pnlMain.PerformLayout();
        pnlControls.ResumeLayout(false);
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private System.Windows.Forms.Panel pnlMain;
    private System.Windows.Forms.FlowLayoutPanel pnlControls;
    private System.Windows.Forms.Button btnOk;
    private System.Windows.Forms.TextBox txtTags;
}
