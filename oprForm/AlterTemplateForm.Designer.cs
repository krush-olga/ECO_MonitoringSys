namespace oprForm
{
    partial class AlterTemplateForm
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
			this.materialListGrid = new System.Windows.Forms.DataGridView();
			this.Resource = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.label1 = new System.Windows.Forms.Label();
			this.resourcesLB = new System.Windows.Forms.ListBox();
			this.contextMenuRes = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.додатиНовийРесурсToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.переглянутиДетальнуІнформаціюToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addBtn = new System.Windows.Forms.Button();
			this.descTB = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.nameTB = new System.Windows.Forms.TextBox();
			this.templatesLB = new System.Windows.Forms.ListBox();
			this.addGB = new System.Windows.Forms.GroupBox();
			this.button1 = new System.Windows.Forms.Button();
			this.btnRes = new System.Windows.Forms.Button();
			this.txtBxRes = new System.Windows.Forms.TextBox();
			this.btnSearchTemplate = new System.Windows.Forms.Button();
			this.txtBxTemplate = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.startTutorial = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.materialListGrid)).BeginInit();
			this.contextMenuRes.SuspendLayout();
			this.addGB.SuspendLayout();
			this.SuspendLayout();
			// 
			// materialListGrid
			// 
			this.materialListGrid.AllowUserToAddRows = false;
			this.materialListGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.materialListGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Resource});
			this.materialListGrid.Location = new System.Drawing.Point(344, 34);
			this.materialListGrid.Name = "materialListGrid";
			this.materialListGrid.ReadOnly = true;
			this.materialListGrid.Size = new System.Drawing.Size(275, 325);
			this.materialListGrid.TabIndex = 13;
			// 
			// Resource
			// 
			this.Resource.HeaderText = "Ресурс";
			this.Resource.Name = "Resource";
			this.Resource.ReadOnly = true;
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(4, 20);
			this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(85, 13);
			this.label1.TabIndex = 12;
			this.label1.Text = "Назва шаблону";
			// 
			// resourcesLB
			// 
			this.resourcesLB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.resourcesLB.ContextMenuStrip = this.contextMenuRes;
			this.resourcesLB.FormattingEnabled = true;
			this.resourcesLB.Location = new System.Drawing.Point(180, 56);
			this.resourcesLB.Margin = new System.Windows.Forms.Padding(2);
			this.resourcesLB.Name = "resourcesLB";
			this.resourcesLB.Size = new System.Drawing.Size(160, 303);
			this.resourcesLB.TabIndex = 18;
			this.resourcesLB.DoubleClick += new System.EventHandler(this.resourcesLB_DoubleClick);
			// 
			// contextMenuRes
			// 
			this.contextMenuRes.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.додатиНовийРесурсToolStripMenuItem,
            this.переглянутиДетальнуІнформаціюToolStripMenuItem});
			this.contextMenuRes.Name = "contextMenuRes";
			this.contextMenuRes.Size = new System.Drawing.Size(256, 48);
			// 
			// додатиНовийРесурсToolStripMenuItem
			// 
			this.додатиНовийРесурсToolStripMenuItem.Name = "додатиНовийРесурсToolStripMenuItem";
			this.додатиНовийРесурсToolStripMenuItem.Size = new System.Drawing.Size(255, 22);
			this.додатиНовийРесурсToolStripMenuItem.Text = "Створити новий ресурс ";
			// 
			// переглянутиДетальнуІнформаціюToolStripMenuItem
			// 
			this.переглянутиДетальнуІнформаціюToolStripMenuItem.Name = "переглянутиДетальнуІнформаціюToolStripMenuItem";
			this.переглянутиДетальнуІнформаціюToolStripMenuItem.Size = new System.Drawing.Size(255, 22);
			this.переглянутиДетальнуІнформаціюToolStripMenuItem.Text = "Перегляд інформації про ресурс";
			// 
			// addBtn
			// 
			this.addBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.addBtn.Location = new System.Drawing.Point(494, 15);
			this.addBtn.Margin = new System.Windows.Forms.Padding(2);
			this.addBtn.Name = "addBtn";
			this.addBtn.Size = new System.Drawing.Size(105, 25);
			this.addBtn.TabIndex = 17;
			this.addBtn.Text = "Зберегти зміни";
			this.addBtn.UseVisualStyleBackColor = true;
			this.addBtn.Click += new System.EventHandler(this.addBtn_Click);
			// 
			// descTB
			// 
			this.descTB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.descTB.Location = new System.Drawing.Point(93, 47);
			this.descTB.Margin = new System.Windows.Forms.Padding(2);
			this.descTB.Name = "descTB";
			this.descTB.Size = new System.Drawing.Size(397, 20);
			this.descTB.TabIndex = 16;
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(4, 50);
			this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(79, 13);
			this.label2.TabIndex = 15;
			this.label2.Text = "Опис шаблону";
			// 
			// nameTB
			// 
			this.nameTB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.nameTB.Location = new System.Drawing.Point(93, 17);
			this.nameTB.Margin = new System.Windows.Forms.Padding(2);
			this.nameTB.Name = "nameTB";
			this.nameTB.Size = new System.Drawing.Size(397, 20);
			this.nameTB.TabIndex = 14;
			// 
			// templatesLB
			// 
			this.templatesLB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.templatesLB.FormattingEnabled = true;
			this.templatesLB.Location = new System.Drawing.Point(16, 56);
			this.templatesLB.Margin = new System.Windows.Forms.Padding(2);
			this.templatesLB.Name = "templatesLB";
			this.templatesLB.Size = new System.Drawing.Size(160, 303);
			this.templatesLB.TabIndex = 20;
			this.templatesLB.SelectedIndexChanged += new System.EventHandler(this.templatesLB_SelectedIndexChanged);
			// 
			// addGB
			// 
			this.addGB.Controls.Add(this.button1);
			this.addGB.Controls.Add(this.label2);
			this.addGB.Controls.Add(this.nameTB);
			this.addGB.Controls.Add(this.descTB);
			this.addGB.Controls.Add(this.addBtn);
			this.addGB.Controls.Add(this.label1);
			this.addGB.Location = new System.Drawing.Point(15, 365);
			this.addGB.Margin = new System.Windows.Forms.Padding(2);
			this.addGB.Name = "addGB";
			this.addGB.Padding = new System.Windows.Forms.Padding(2);
			this.addGB.Size = new System.Drawing.Size(604, 79);
			this.addGB.TabIndex = 22;
			this.addGB.TabStop = false;
			this.addGB.Visible = false;
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.Location = new System.Drawing.Point(494, 44);
			this.button1.Margin = new System.Windows.Forms.Padding(2);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(105, 25);
			this.button1.TabIndex = 18;
			this.button1.Text = "Видалити шаблон";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// btnRes
			// 
			this.btnRes.Image = global::oprForm.Properties.Resources.imgonline_com_ua_Resize_Ojr6EQ8xE2H;
			this.btnRes.Location = new System.Drawing.Point(319, 33);
			this.btnRes.Name = "btnRes";
			this.btnRes.Size = new System.Drawing.Size(23, 22);
			this.btnRes.TabIndex = 29;
			this.btnRes.UseVisualStyleBackColor = true;
			// 
			// txtBxRes
			// 
			this.txtBxRes.Location = new System.Drawing.Point(181, 34);
			this.txtBxRes.Name = "txtBxRes";
			this.txtBxRes.Size = new System.Drawing.Size(138, 20);
			this.txtBxRes.TabIndex = 28;
			this.txtBxRes.Tag = "";
			// 
			// btnSearchTemplate
			// 
			this.btnSearchTemplate.Image = global::oprForm.Properties.Resources.imgonline_com_ua_Resize_Ojr6EQ8xE2H;
			this.btnSearchTemplate.Location = new System.Drawing.Point(153, 33);
			this.btnSearchTemplate.Name = "btnSearchTemplate";
			this.btnSearchTemplate.Size = new System.Drawing.Size(23, 22);
			this.btnSearchTemplate.TabIndex = 27;
			this.btnSearchTemplate.UseVisualStyleBackColor = true;
			// 
			// txtBxTemplate
			// 
			this.txtBxTemplate.Location = new System.Drawing.Point(15, 34);
			this.txtBxTemplate.Name = "txtBxTemplate";
			this.txtBxTemplate.Size = new System.Drawing.Size(138, 20);
			this.txtBxTemplate.TabIndex = 26;
			this.txtBxTemplate.Tag = "";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(184, 16);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(111, 13);
			this.label5.TabIndex = 25;
			this.label5.Text = "Всі доступні ресурси";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(341, 16);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(176, 13);
			this.label6.TabIndex = 24;
			this.label6.Text = "Перелік ресурсів шаблону заходу";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(17, 17);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(136, 13);
			this.label7.TabIndex = 23;
			this.label7.Text = "Перелік шаблонів заходiв";
			// 
			// startTutorial
			// 
			this.startTutorial.AutoSize = true;
			this.startTutorial.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.startTutorial.Location = new System.Drawing.Point(616, 3);
			this.startTutorial.Name = "startTutorial";
			this.startTutorial.Size = new System.Drawing.Size(18, 20);
			this.startTutorial.TabIndex = 69;
			this.startTutorial.Text = "?";
			this.startTutorial.Click += new System.EventHandler(this.startTutorial_Click);
			this.startTutorial.MouseEnter += new System.EventHandler(this.startTutorial_MouseEnter);
			this.startTutorial.MouseLeave += new System.EventHandler(this.startTutorial_MouseLeave);
			// 
			// AlterTemplateForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(637, 449);
			this.Controls.Add(this.startTutorial);
			this.Controls.Add(this.btnRes);
			this.Controls.Add(this.txtBxRes);
			this.Controls.Add(this.btnSearchTemplate);
			this.Controls.Add(this.txtBxTemplate);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.addGB);
			this.Controls.Add(this.templatesLB);
			this.Controls.Add(this.materialListGrid);
			this.Controls.Add(this.resourcesLB);
			this.Margin = new System.Windows.Forms.Padding(2);
			this.Name = "AlterTemplateForm";
			this.Padding = new System.Windows.Forms.Padding(8);
			this.Text = "Редагування шаблонів заходів";
			((System.ComponentModel.ISupportInitialize)(this.materialListGrid)).EndInit();
			this.contextMenuRes.ResumeLayout(false);
			this.addGB.ResumeLayout(false);
			this.addGB.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView materialListGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Resource;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox resourcesLB;
        private System.Windows.Forms.Button addBtn;
        private System.Windows.Forms.TextBox descTB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox nameTB;
        private System.Windows.Forms.ListBox templatesLB;
        private System.Windows.Forms.GroupBox addGB;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnRes;
        private System.Windows.Forms.TextBox txtBxRes;
        private System.Windows.Forms.Button btnSearchTemplate;
        private System.Windows.Forms.TextBox txtBxTemplate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ContextMenuStrip contextMenuRes;
        private System.Windows.Forms.ToolStripMenuItem додатиНовийРесурсToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem переглянутиДетальнуІнформаціюToolStripMenuItem;
		private System.Windows.Forms.Label startTutorial;
	}
}