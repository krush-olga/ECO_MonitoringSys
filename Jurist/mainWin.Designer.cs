namespace experts_jurist
{
    partial class mainWin
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.RozrahTSM = new System.Windows.Forms.ToolStripMenuItem();
            this.ResultTSM = new System.Windows.Forms.ToolStripMenuItem();
            this.RedaktTSM = new System.Windows.Forms.ToolStripMenuItem();
            this.TemplateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.проМодульToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.проблемыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileBaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NewTemplateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ChangeTemplateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RozrahTSM,
            this.ResultTSM,
            this.RedaktTSM,
            this.TemplateToolStripMenuItem,
            this.проМодульToolStripMenuItem,
            this.проблемыToolStripMenuItem,
            this.fileBaseToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1068, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // RozrahTSM
            // 
            this.RozrahTSM.Name = "RozrahTSM";
            this.RozrahTSM.Size = new System.Drawing.Size(133, 20);
            this.RozrahTSM.Text = "Запропонувати захід";
            this.RozrahTSM.Click += new System.EventHandler(this.offer_Click);
            // 
            // ResultTSM
            // 
            this.ResultTSM.Name = "ResultTSM";
            this.ResultTSM.Size = new System.Drawing.Size(166, 20);
            this.ResultTSM.Text = "Перегляд та оцінка заходів";
            this.ResultTSM.Click += new System.EventHandler(this.estimate_Click);
            // 
            // RedaktTSM
            // 
            this.RedaktTSM.Name = "RedaktTSM";
            this.RedaktTSM.Size = new System.Drawing.Size(61, 20);
            this.RedaktTSM.Text = "Пошук ";
            this.RedaktTSM.Click += new System.EventHandler(this.search_Click);
            // 
            // TemplateToolStripMenuItem
            // 
            this.TemplateToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewTemplateToolStripMenuItem,
            this.ChangeTemplateToolStripMenuItem});
            this.TemplateToolStripMenuItem.Name = "TemplateToolStripMenuItem";
            this.TemplateToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.TemplateToolStripMenuItem.Text = "Шаблони";
            // 
            // проМодульToolStripMenuItem
            // 
            this.проМодульToolStripMenuItem.Name = "проМодульToolStripMenuItem";
            this.проМодульToolStripMenuItem.Size = new System.Drawing.Size(86, 20);
            this.проМодульToolStripMenuItem.Text = "Про модуль";
            this.проМодульToolStripMenuItem.Click += new System.EventHandler(this.проМодульToolStripMenuItem_Click);
            // 
            // проблемыToolStripMenuItem
            // 
            this.проблемыToolStripMenuItem.Name = "проблемыToolStripMenuItem";
            this.проблемыToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.проблемыToolStripMenuItem.Text = "Задачі";
            this.проблемыToolStripMenuItem.Click += new System.EventHandler(this.проблемыToolStripMenuItem_Click);
            // 
            // fileBaseToolStripMenuItem
            // 
            this.fileBaseToolStripMenuItem.Name = "fileBaseToolStripMenuItem";
            this.fileBaseToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
            this.fileBaseToolStripMenuItem.Text = "Файлова база";
            this.fileBaseToolStripMenuItem.Click += new System.EventHandler(this.fileBaseToolStripMenuItem_Click);
            // 
            // NewTemplateToolStripMenuItem
            // 
            this.NewTemplateToolStripMenuItem.Name = "NewTemplateToolStripMenuItem";
            this.NewTemplateToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.NewTemplateToolStripMenuItem.Text = "Новий шаблон";
            this.NewTemplateToolStripMenuItem.Click += new System.EventHandler(this.newTemplateToolStripMenuItem_Click);
            // 
            // ChangeTemplateToolStripMenuItem
            // 
            this.ChangeTemplateToolStripMenuItem.Name = "ChangeTemplateToolStripMenuItem";
            this.ChangeTemplateToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.ChangeTemplateToolStripMenuItem.Text = "Змінити шаблон";
            this.ChangeTemplateToolStripMenuItem.Click += new System.EventHandler(this.alterTemplateToolStripMenuItem_Click);
            // 
            // mainWin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1068, 423);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "mainWin";
            this.Text = "Головна";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.mainWin_Load);
            this.Shown += new System.EventHandler(this.mainWin_Shown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem RozrahTSM;
        private System.Windows.Forms.ToolStripMenuItem ResultTSM;
        private System.Windows.Forms.ToolStripMenuItem RedaktTSM;
        private System.Windows.Forms.ToolStripMenuItem проМодульToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem TemplateToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem проблемыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileBaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NewTemplateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ChangeTemplateToolStripMenuItem;
    }
}