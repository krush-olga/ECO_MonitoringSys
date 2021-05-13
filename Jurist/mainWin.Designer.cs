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
            this.базаДанихЗаконодавчихДокументівToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Search = new System.Windows.Forms.ToolStripMenuItem();
            this.AddingDoc = new System.Windows.Forms.ToolStripMenuItem();
            this.проблемыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.заходиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.переглядТаОцінкаЗаходівToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.запропонуватиЗахідToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.шаблониToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.редагуванняШаблонівToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.доданняНовихШаблонівToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.довідкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.проМодульToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.видаленняДокументуЗБазиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.базаДанихЗаконодавчихДокументівToolStripMenuItem,
            this.проблемыToolStripMenuItem,
            this.заходиToolStripMenuItem,
            this.довідкаToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1068, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // базаДанихЗаконодавчихДокументівToolStripMenuItem
            // 
            this.базаДанихЗаконодавчихДокументівToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Search,
            this.AddingDoc,
            this.видаленняДокументуЗБазиToolStripMenuItem});
            this.базаДанихЗаконодавчихДокументівToolStripMenuItem.Name = "базаДанихЗаконодавчихДокументівToolStripMenuItem";
            this.базаДанихЗаконодавчихДокументівToolStripMenuItem.Size = new System.Drawing.Size(219, 20);
            this.базаДанихЗаконодавчихДокументівToolStripMenuItem.Text = "База даних законодавчих документів";
            // 
            // Search
            // 
            this.Search.Name = "Search";
            this.Search.Size = new System.Drawing.Size(244, 22);
            this.Search.Text = "Пошук та перегляд документів";
            this.Search.Click += new System.EventHandler(this.search_Click);
            // 
            // AddingDoc
            // 
            this.AddingDoc.Name = "AddingDoc";
            this.AddingDoc.Size = new System.Drawing.Size(244, 22);
            this.AddingDoc.Text = "Додання нового документу";
            this.AddingDoc.Click += new System.EventHandler(this.AddingDoc_Click);
            // 
            // проблемыToolStripMenuItem
            // 
            this.проблемыToolStripMenuItem.Name = "проблемыToolStripMenuItem";
            this.проблемыToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.проблемыToolStripMenuItem.Text = "Задачі";
            this.проблемыToolStripMenuItem.Click += new System.EventHandler(this.проблемыToolStripMenuItem_Click);
            // 
            // заходиToolStripMenuItem
            // 
            this.заходиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.переглядТаОцінкаЗаходівToolStripMenuItem,
            this.запропонуватиЗахідToolStripMenuItem,
            this.шаблониToolStripMenuItem});
            this.заходиToolStripMenuItem.Name = "заходиToolStripMenuItem";
            this.заходиToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.заходиToolStripMenuItem.Text = "Заходи";
            // 
            // переглядТаОцінкаЗаходівToolStripMenuItem
            // 
            this.переглядТаОцінкаЗаходівToolStripMenuItem.Name = "переглядТаОцінкаЗаходівToolStripMenuItem";
            this.переглядТаОцінкаЗаходівToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.переглядТаОцінкаЗаходівToolStripMenuItem.Text = "Перегляд та оцінка заходів";
            this.переглядТаОцінкаЗаходівToolStripMenuItem.Click += new System.EventHandler(this.estimate_Click);
            // 
            // запропонуватиЗахідToolStripMenuItem
            // 
            this.запропонуватиЗахідToolStripMenuItem.Name = "запропонуватиЗахідToolStripMenuItem";
            this.запропонуватиЗахідToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.запропонуватиЗахідToolStripMenuItem.Text = "Запропонувати захід";
            this.запропонуватиЗахідToolStripMenuItem.Click += new System.EventHandler(this.offer_Click);
            // 
            // шаблониToolStripMenuItem
            // 
            this.шаблониToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.редагуванняШаблонівToolStripMenuItem,
            this.доданняНовихШаблонівToolStripMenuItem});
            this.шаблониToolStripMenuItem.Name = "шаблониToolStripMenuItem";
            this.шаблониToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.шаблониToolStripMenuItem.Text = "Шаблони";
            // 
            // редагуванняШаблонівToolStripMenuItem
            // 
            this.редагуванняШаблонівToolStripMenuItem.Name = "редагуванняШаблонівToolStripMenuItem";
            this.редагуванняШаблонівToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.редагуванняШаблонівToolStripMenuItem.Text = "Редагування шаблонів";
            this.редагуванняШаблонівToolStripMenuItem.Click += new System.EventHandler(this.alterTemplateToolStripMenuItem_Click);
            // 
            // доданняНовихШаблонівToolStripMenuItem
            // 
            this.доданняНовихШаблонівToolStripMenuItem.Name = "доданняНовихШаблонівToolStripMenuItem";
            this.доданняНовихШаблонівToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.доданняНовихШаблонівToolStripMenuItem.Text = "Додання нових шаблонів";
            this.доданняНовихШаблонівToolStripMenuItem.Click += new System.EventHandler(this.newTemplateToolStripMenuItem_Click);
            // 
            // довідкаToolStripMenuItem
            // 
            this.довідкаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.проМодульToolStripMenuItem1});
            this.довідкаToolStripMenuItem.Name = "довідкаToolStripMenuItem";
            this.довідкаToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.довідкаToolStripMenuItem.Text = "Довідка";
            // 
            // проМодульToolStripMenuItem1
            // 
            this.проМодульToolStripMenuItem1.Name = "проМодульToolStripMenuItem1";
            this.проМодульToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.проМодульToolStripMenuItem1.Text = "Про модуль";
            this.проМодульToolStripMenuItem1.Click += new System.EventHandler(this.About_Click);
            // 
            // видаленняДокументуЗБазиToolStripMenuItem
            // 
            this.видаленняДокументуЗБазиToolStripMenuItem.Name = "видаленняДокументуЗБазиToolStripMenuItem";
            this.видаленняДокументуЗБазиToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.видаленняДокументуЗБазиToolStripMenuItem.Text = "Видалення документу ";
            this.видаленняДокументуЗБазиToolStripMenuItem.Click += new System.EventHandler(this.DeletingDoc_Click);
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
		private System.Windows.Forms.ToolStripMenuItem проблемыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem базаДанихЗаконодавчихДокументівToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Search;
        private System.Windows.Forms.ToolStripMenuItem AddingDoc;
        private System.Windows.Forms.ToolStripMenuItem заходиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem переглядТаОцінкаЗаходівToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem запропонуватиЗахідToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem довідкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem проМодульToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem шаблониToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem редагуванняШаблонівToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem доданняНовихШаблонівToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem видаленняДокументуЗБазиToolStripMenuItem;
    }
}