namespace oprForm
{
    partial class AddTemplateForm
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
			this.label1 = new System.Windows.Forms.Label();
			this.materialListGrid = new System.Windows.Forms.DataGridView();
			this.Resource = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.nameTB = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.descTB = new System.Windows.Forms.TextBox();
			this.saveToDBBtn = new System.Windows.Forms.Button();
			this.resourcesLB = new System.Windows.Forms.ListBox();
			this.contextMenuRes = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.додатиНовийРесурсToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.переглянутиДетальнуІнформаціюToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.label4 = new System.Windows.Forms.Label();
			this.addButton = new System.Windows.Forms.Button();
			this.removeButton = new System.Windows.Forms.Button();
			this.btnRes = new System.Windows.Forms.Button();
			this.txtBxRes = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.startTutorial = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.materialListGrid)).BeginInit();
			this.contextMenuRes.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.BackColor = System.Drawing.SystemColors.Control;
			this.label1.Location = new System.Drawing.Point(13, 17);
			this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(85, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Назва шаблону";
			// 
			// materialListGrid
			// 
			this.materialListGrid.AllowUserToAddRows = false;
			this.materialListGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.materialListGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.materialListGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Resource});
			this.materialListGrid.Location = new System.Drawing.Point(181, 92);
			this.materialListGrid.Name = "materialListGrid";
			this.materialListGrid.ReadOnly = true;
			this.materialListGrid.Size = new System.Drawing.Size(383, 278);
			this.materialListGrid.TabIndex = 5;
			// 
			// Resource
			// 
			this.Resource.HeaderText = "Ресурс";
			this.Resource.Name = "Resource";
			this.Resource.ReadOnly = true;
			// 
			// nameTB
			// 
			this.nameTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.nameTB.Location = new System.Drawing.Point(102, 14);
			this.nameTB.Margin = new System.Windows.Forms.Padding(2);
			this.nameTB.Name = "nameTB";
			this.nameTB.Size = new System.Drawing.Size(443, 20);
			this.nameTB.TabIndex = 6;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(17, 45);
			this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(33, 13);
			this.label2.TabIndex = 7;
			this.label2.Text = "Опис";
			// 
			// descTB
			// 
			this.descTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.descTB.Location = new System.Drawing.Point(102, 42);
			this.descTB.Margin = new System.Windows.Forms.Padding(2);
			this.descTB.Name = "descTB";
			this.descTB.Size = new System.Drawing.Size(443, 20);
			this.descTB.TabIndex = 8;
			// 
			// saveToDBBtn
			// 
			this.saveToDBBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.saveToDBBtn.Location = new System.Drawing.Point(182, 412);
			this.saveToDBBtn.Margin = new System.Windows.Forms.Padding(2);
			this.saveToDBBtn.Name = "saveToDBBtn";
			this.saveToDBBtn.Size = new System.Drawing.Size(383, 30);
			this.saveToDBBtn.TabIndex = 9;
			this.saveToDBBtn.Text = "Зберегти шаблон";
			this.saveToDBBtn.UseVisualStyleBackColor = true;
			this.saveToDBBtn.Click += new System.EventHandler(this.addBtn_Click);
			// 
			// resourcesLB
			// 
			this.resourcesLB.ContextMenuStrip = this.contextMenuRes;
			this.resourcesLB.FormattingEnabled = true;
			this.resourcesLB.Location = new System.Drawing.Point(15, 113);
			this.resourcesLB.Margin = new System.Windows.Forms.Padding(2);
			this.resourcesLB.Name = "resourcesLB";
			this.resourcesLB.Size = new System.Drawing.Size(160, 329);
			this.resourcesLB.TabIndex = 10;
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
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(178, 74);
			this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(139, 13);
			this.label4.TabIndex = 12;
			this.label4.Text = "Перелік ресурсів шаблону";
			// 
			// addButton
			// 
			this.addButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.addButton.Location = new System.Drawing.Point(181, 371);
			this.addButton.Margin = new System.Windows.Forms.Padding(2);
			this.addButton.Name = "addButton";
			this.addButton.Size = new System.Drawing.Size(190, 25);
			this.addButton.TabIndex = 13;
			this.addButton.Text = "Додати ресурс до переліку";
			this.addButton.UseVisualStyleBackColor = true;
			this.addButton.Click += new System.EventHandler(this.addButton_Click);
			// 
			// removeButton
			// 
			this.removeButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.removeButton.Location = new System.Drawing.Point(375, 371);
			this.removeButton.Margin = new System.Windows.Forms.Padding(2);
			this.removeButton.Name = "removeButton";
			this.removeButton.Size = new System.Drawing.Size(190, 25);
			this.removeButton.TabIndex = 14;
			this.removeButton.Text = "Видалити ресурс з переліку";
			this.removeButton.UseVisualStyleBackColor = true;
			this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
			// 
			// btnRes
			// 
			this.btnRes.Image = global::oprForm.Properties.Resources.imgonline_com_ua_Resize_Ojr6EQ8xE2H;
			this.btnRes.Location = new System.Drawing.Point(153, 89);
			this.btnRes.Name = "btnRes";
			this.btnRes.Size = new System.Drawing.Size(23, 22);
			this.btnRes.TabIndex = 18;
			this.btnRes.UseVisualStyleBackColor = true;
			// 
			// txtBxRes
			// 
			this.txtBxRes.Location = new System.Drawing.Point(15, 90);
			this.txtBxRes.Name = "txtBxRes";
			this.txtBxRes.Size = new System.Drawing.Size(138, 20);
			this.txtBxRes.TabIndex = 17;
			this.txtBxRes.Tag = "";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(17, 73);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(111, 13);
			this.label5.TabIndex = 16;
			this.label5.Text = "Всі доступні ресурси";
			// 
			// startTutorial
			// 
			this.startTutorial.AutoSize = true;
			this.startTutorial.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.startTutorial.Location = new System.Drawing.Point(558, 3);
			this.startTutorial.Name = "startTutorial";
			this.startTutorial.Size = new System.Drawing.Size(18, 20);
			this.startTutorial.TabIndex = 26;
			this.startTutorial.Text = "?";
			this.startTutorial.Click += new System.EventHandler(this.startTutorial_Click);
			this.startTutorial.MouseEnter += new System.EventHandler(this.startTutorial_MouseEnter);
			this.startTutorial.MouseLeave += new System.EventHandler(this.startTutorial_MouseLeave);
			// 
			// AddTemplateForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(579, 449);
			this.Controls.Add(this.startTutorial);
			this.Controls.Add(this.btnRes);
			this.Controls.Add(this.txtBxRes);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.removeButton);
			this.Controls.Add(this.addButton);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.resourcesLB);
			this.Controls.Add(this.saveToDBBtn);
			this.Controls.Add(this.descTB);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.nameTB);
			this.Controls.Add(this.materialListGrid);
			this.Controls.Add(this.label1);
			this.Margin = new System.Windows.Forms.Padding(2);
			this.Name = "AddTemplateForm";
			this.Padding = new System.Windows.Forms.Padding(8);
			this.Text = "Додання нового шаблону заходу";
			this.Load += new System.EventHandler(this.AddTemplateForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.materialListGrid)).EndInit();
			this.contextMenuRes.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView materialListGrid;
        private System.Windows.Forms.TextBox nameTB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox descTB;
        private System.Windows.Forms.Button saveToDBBtn;
        private System.Windows.Forms.ListBox resourcesLB;
        private System.Windows.Forms.DataGridViewTextBoxColumn Resource;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.Button btnRes;
        private System.Windows.Forms.TextBox txtBxRes;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ContextMenuStrip contextMenuRes;
        private System.Windows.Forms.ToolStripMenuItem додатиНовийРесурсToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem переглянутиДетальнуІнформаціюToolStripMenuItem;
		private System.Windows.Forms.Label startTutorial;
	}
}