namespace Experts_Economist
{
    partial class ExportDB
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
			this.components = new System.ComponentModel.Container();
			this.button1 = new System.Windows.Forms.Button();
			this.exportBtn = new System.Windows.Forms.Button();
			this.exportPathTB = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.schemaCB = new System.Windows.Forms.ComboBox();
			this.loginTB = new System.Windows.Forms.TextBox();
			this.passwordTB = new System.Windows.Forms.TextBox();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.неПриховуватиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.неПриховуватиToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.loginLB = new System.Windows.Forms.Label();
			this.passwordLB = new System.Windows.Forms.Label();
			this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.label1 = new System.Windows.Forms.Label();
			this.startTutorial = new System.Windows.Forms.Label();
			this.contextMenuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(298, 55);
			this.button1.Margin = new System.Windows.Forms.Padding(2);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(26, 24);
			this.button1.TabIndex = 0;
			this.button1.Text = "...";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// exportBtn
			// 
			this.exportBtn.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.exportBtn.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.exportBtn.Location = new System.Drawing.Point(0, 334);
			this.exportBtn.Margin = new System.Windows.Forms.Padding(2);
			this.exportBtn.Name = "exportBtn";
			this.exportBtn.Size = new System.Drawing.Size(346, 41);
			this.exportBtn.TabIndex = 1;
			this.exportBtn.Text = "Експортувати";
			this.exportBtn.UseVisualStyleBackColor = true;
			this.exportBtn.Click += new System.EventHandler(this.button2_Click);
			// 
			// exportPathTB
			// 
			this.exportPathTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.exportPathTB.Location = new System.Drawing.Point(26, 55);
			this.exportPathTB.Margin = new System.Windows.Forms.Padding(2);
			this.exportPathTB.Name = "exportPathTB";
			this.exportPathTB.Size = new System.Drawing.Size(269, 24);
			this.exportPathTB.TabIndex = 2;
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label3.Location = new System.Drawing.Point(22, 22);
			this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(172, 20);
			this.label3.TabIndex = 8;
			this.label3.Text = "Експортувати у папку";
			this.label3.Click += new System.EventHandler(this.label3_Click);
			// 
			// schemaCB
			// 
			this.schemaCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.schemaCB.FormattingEnabled = true;
			this.schemaCB.Location = new System.Drawing.Point(26, 128);
			this.schemaCB.Margin = new System.Windows.Forms.Padding(2);
			this.schemaCB.Name = "schemaCB";
			this.schemaCB.Size = new System.Drawing.Size(300, 28);
			this.schemaCB.TabIndex = 9;
			// 
			// loginTB
			// 
			this.loginTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.loginTB.Location = new System.Drawing.Point(26, 198);
			this.loginTB.Margin = new System.Windows.Forms.Padding(2);
			this.loginTB.Name = "loginTB";
			this.loginTB.Size = new System.Drawing.Size(300, 24);
			this.loginTB.TabIndex = 10;
			// 
			// passwordTB
			// 
			this.passwordTB.ContextMenuStrip = this.contextMenuStrip1;
			this.passwordTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.passwordTB.Location = new System.Drawing.Point(26, 260);
			this.passwordTB.Margin = new System.Windows.Forms.Padding(2);
			this.passwordTB.Name = "passwordTB";
			this.passwordTB.Size = new System.Drawing.Size(300, 24);
			this.passwordTB.TabIndex = 11;
			this.passwordTB.UseSystemPasswordChar = true;
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.неПриховуватиToolStripMenuItem,
            this.неПриховуватиToolStripMenuItem1});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(163, 48);
			this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
			// 
			// неПриховуватиToolStripMenuItem
			// 
			this.неПриховуватиToolStripMenuItem.Name = "неПриховуватиToolStripMenuItem";
			this.неПриховуватиToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
			this.неПриховуватиToolStripMenuItem.Text = "Приховати";
			this.неПриховуватиToolStripMenuItem.Visible = false;
			this.неПриховуватиToolStripMenuItem.Click += new System.EventHandler(this.неПриховуватиToolStripMenuItem_Click);
			// 
			// неПриховуватиToolStripMenuItem1
			// 
			this.неПриховуватиToolStripMenuItem1.Name = "неПриховуватиToolStripMenuItem1";
			this.неПриховуватиToolStripMenuItem1.Size = new System.Drawing.Size(162, 22);
			this.неПриховуватиToolStripMenuItem1.Text = "Не приховувати";
			this.неПриховуватиToolStripMenuItem1.Click += new System.EventHandler(this.неПриховуватиToolStripMenuItem1_Click);
			// 
			// loginLB
			// 
			this.loginLB.AutoSize = true;
			this.loginLB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.loginLB.Location = new System.Drawing.Point(23, 173);
			this.loginLB.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.loginLB.Name = "loginLB";
			this.loginLB.Size = new System.Drawing.Size(49, 20);
			this.loginLB.TabIndex = 14;
			this.loginLB.Text = "Логін";
			// 
			// passwordLB
			// 
			this.passwordLB.AutoSize = true;
			this.passwordLB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.passwordLB.Location = new System.Drawing.Point(23, 236);
			this.passwordLB.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.passwordLB.Name = "passwordLB";
			this.passwordLB.Size = new System.Drawing.Size(67, 20);
			this.passwordLB.TabIndex = 15;
			this.passwordLB.Text = "Пароль";
			// 
			// folderBrowserDialog
			// 
			this.folderBrowserDialog.Description = "Оберіть папку для експорту";
			this.folderBrowserDialog.SelectedPath = ".";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.Location = new System.Drawing.Point(23, 97);
			this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(56, 20);
			this.label1.TabIndex = 16;
			this.label1.Text = "Схема";
			// 
			// startTutorial
			// 
			this.startTutorial.AutoSize = true;
			this.startTutorial.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.startTutorial.Location = new System.Drawing.Point(316, 9);
			this.startTutorial.Name = "startTutorial";
			this.startTutorial.Size = new System.Drawing.Size(18, 20);
			this.startTutorial.TabIndex = 24;
			this.startTutorial.Text = "?";
			this.startTutorial.Click += new System.EventHandler(this.startTutorial_Click);
			this.startTutorial.MouseEnter += new System.EventHandler(this.startTutorial_MouseEnter);
			this.startTutorial.MouseLeave += new System.EventHandler(this.startTutorial_MouseLeave);
			// 
			// ExportDB
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(346, 375);
			this.Controls.Add(this.startTutorial);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.passwordLB);
			this.Controls.Add(this.loginLB);
			this.Controls.Add(this.passwordTB);
			this.Controls.Add(this.loginTB);
			this.Controls.Add(this.schemaCB);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.exportPathTB);
			this.Controls.Add(this.exportBtn);
			this.Controls.Add(this.button1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Margin = new System.Windows.Forms.Padding(2);
			this.Name = "ExportDB";
			this.Text = "ExportDB";
			this.contextMenuStrip1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button exportBtn;
        private System.Windows.Forms.TextBox exportPathTB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox schemaCB;
        private System.Windows.Forms.TextBox loginTB;
        private System.Windows.Forms.TextBox passwordTB;
        private System.Windows.Forms.Label loginLB;
        private System.Windows.Forms.Label passwordLB;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem неПриховуватиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem неПриховуватиToolStripMenuItem1;
		private System.Windows.Forms.Label startTutorial;
	}
}