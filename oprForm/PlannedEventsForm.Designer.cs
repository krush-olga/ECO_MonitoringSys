namespace oprForm
{
	partial class PlannedEventsForm
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
			this.eventsLB = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.addBtn = new System.Windows.Forms.Button();
			this.eventListGrid = new System.Windows.Forms.DataGridView();
			this.Resource = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.descTB = new System.Windows.Forms.TextBox();
			this.evNameTB = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.addGB = new System.Windows.Forms.GroupBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.issuesCB = new System.Windows.Forms.ComboBox();
			this.onlyExpCB = new System.Windows.Forms.CheckBox();
			this.resLB = new System.Windows.Forms.ListBox();
			this.label3 = new System.Windows.Forms.Label();
			this.txtBxTemplate = new System.Windows.Forms.TextBox();
			this.btnSearchTemplate = new System.Windows.Forms.Button();
			this.btnRes = new System.Windows.Forms.Button();
			this.txtBxRes = new System.Windows.Forms.TextBox();
			this.startTutorial = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.eventListGrid)).BeginInit();
			this.addGB.SuspendLayout();
			this.SuspendLayout();
			// 
			// eventsLB
			// 
			this.eventsLB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.eventsLB.FormattingEnabled = true;
			this.eventsLB.Location = new System.Drawing.Point(12, 59);
			this.eventsLB.Name = "eventsLB";
			this.eventsLB.Size = new System.Drawing.Size(160, 316);
			this.eventsLB.TabIndex = 0;
			this.eventsLB.SelectedIndexChanged += new System.EventHandler(this.eventsLB_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 5);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(136, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Перелік шаблонів заходiв";
			// 
			// addBtn
			// 
			this.addBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.addBtn.Location = new System.Drawing.Point(745, 17);
			this.addBtn.Name = "addBtn";
			this.addBtn.Size = new System.Drawing.Size(74, 70);
			this.addBtn.TabIndex = 3;
			this.addBtn.Text = "Додати захід";
			this.addBtn.UseVisualStyleBackColor = true;
			this.addBtn.Click += new System.EventHandler(this.button1_Click);
			// 
			// eventListGrid
			// 
			this.eventListGrid.AllowUserToAddRows = false;
			this.eventListGrid.AllowUserToDeleteRows = false;
			this.eventListGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.eventListGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.eventListGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.eventListGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Resource,
            this.Description,
            this.Value});
			this.eventListGrid.Location = new System.Drawing.Point(346, 36);
			this.eventListGrid.Name = "eventListGrid";
			this.eventListGrid.Size = new System.Drawing.Size(485, 341);
			this.eventListGrid.TabIndex = 4;
			this.eventListGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.commitValue);
			// 
			// Resource
			// 
			this.Resource.HeaderText = "Ресурс";
			this.Resource.Name = "Resource";
			// 
			// Description
			// 
			this.Description.HeaderText = "Опис";
			this.Description.Name = "Description";
			// 
			// Value
			// 
			this.Value.HeaderText = "Кількість";
			this.Value.Name = "Value";
			// 
			// descTB
			// 
			this.descTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.descTB.Location = new System.Drawing.Point(81, 42);
			this.descTB.Name = "descTB";
			this.descTB.Size = new System.Drawing.Size(653, 20);
			this.descTB.TabIndex = 5;
			this.descTB.Click += new System.EventHandler(this.descTB_Click);
			// 
			// evNameTB
			// 
			this.evNameTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.evNameTB.Location = new System.Drawing.Point(81, 17);
			this.evNameTB.Name = "evNameTB";
			this.evNameTB.Size = new System.Drawing.Size(653, 20);
			this.evNameTB.TabIndex = 6;
			this.evNameTB.Click += new System.EventHandler(this.evNameTB_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(349, 20);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(130, 13);
			this.label2.TabIndex = 7;
			this.label2.Text = "Перелік ресурсів заходу";
			// 
			// addGB
			// 
			this.addGB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.addGB.Controls.Add(this.label6);
			this.addGB.Controls.Add(this.label5);
			this.addGB.Controls.Add(this.label4);
			this.addGB.Controls.Add(this.descTB);
			this.addGB.Controls.Add(this.evNameTB);
			this.addGB.Controls.Add(this.issuesCB);
			this.addGB.Controls.Add(this.addBtn);
			this.addGB.Location = new System.Drawing.Point(12, 386);
			this.addGB.Margin = new System.Windows.Forms.Padding(2);
			this.addGB.Name = "addGB";
			this.addGB.Padding = new System.Windows.Forms.Padding(2);
			this.addGB.Size = new System.Drawing.Size(828, 91);
			this.addGB.TabIndex = 8;
			this.addGB.TabStop = false;
			this.addGB.Text = "Додання нового заходу";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(5, 70);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(43, 13);
			this.label6.TabIndex = 10;
			this.label6.Text = "Задача";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(5, 45);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(70, 13);
			this.label5.TabIndex = 9;
			this.label5.Text = "Опис заходу";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(5, 20);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(76, 13);
			this.label4.TabIndex = 8;
			this.label4.Text = "Назва заходу";
			// 
			// issuesCB
			// 
			this.issuesCB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.issuesCB.FormattingEnabled = true;
			this.issuesCB.Location = new System.Drawing.Point(81, 66);
			this.issuesCB.Margin = new System.Windows.Forms.Padding(2);
			this.issuesCB.Name = "issuesCB";
			this.issuesCB.Size = new System.Drawing.Size(653, 21);
			this.issuesCB.TabIndex = 7;
			// 
			// onlyExpCB
			// 
			this.onlyExpCB.AutoSize = true;
			this.onlyExpCB.Location = new System.Drawing.Point(12, 20);
			this.onlyExpCB.Margin = new System.Windows.Forms.Padding(2);
			this.onlyExpCB.Name = "onlyExpCB";
			this.onlyExpCB.Size = new System.Drawing.Size(158, 17);
			this.onlyExpCB.TabIndex = 9;
			this.onlyExpCB.Text = "Лише поточного експерта";
			this.onlyExpCB.UseVisualStyleBackColor = true;
			this.onlyExpCB.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
			// 
			// resLB
			// 
			this.resLB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.resLB.FormattingEnabled = true;
			this.resLB.Location = new System.Drawing.Point(179, 59);
			this.resLB.Margin = new System.Windows.Forms.Padding(2);
			this.resLB.Name = "resLB";
			this.resLB.Size = new System.Drawing.Size(160, 316);
			this.resLB.TabIndex = 10;
			this.resLB.DoubleClick += new System.EventHandler(this.resLB_DoubleClick);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(181, 20);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(111, 13);
			this.label3.TabIndex = 11;
			this.label3.Text = "Всі доступні ресурси";
			// 
			// txtBxTemplate
			// 
			this.txtBxTemplate.Location = new System.Drawing.Point(12, 37);
			this.txtBxTemplate.Name = "txtBxTemplate";
			this.txtBxTemplate.Size = new System.Drawing.Size(136, 20);
			this.txtBxTemplate.TabIndex = 12;
			this.txtBxTemplate.Tag = "";
			// 
			// btnSearchTemplate
			// 
			this.btnSearchTemplate.Image = global::oprForm.Properties.Resources.imgonline_com_ua_Resize_Ojr6EQ8xE2H;
			this.btnSearchTemplate.Location = new System.Drawing.Point(149, 36);
			this.btnSearchTemplate.Name = "btnSearchTemplate";
			this.btnSearchTemplate.Size = new System.Drawing.Size(23, 22);
			this.btnSearchTemplate.TabIndex = 13;
			this.btnSearchTemplate.Tag = "1";
			this.btnSearchTemplate.UseVisualStyleBackColor = true;
			this.btnSearchTemplate.Click += new System.EventHandler(this.FindButton_Click);
			// 
			// btnRes
			// 
			this.btnRes.Image = global::oprForm.Properties.Resources.imgonline_com_ua_Resize_Ojr6EQ8xE2H;
			this.btnRes.Location = new System.Drawing.Point(317, 36);
			this.btnRes.Name = "btnRes";
			this.btnRes.Size = new System.Drawing.Size(23, 22);
			this.btnRes.TabIndex = 15;
			this.btnRes.Tag = "2";
			this.btnRes.UseVisualStyleBackColor = true;
			this.btnRes.Click += new System.EventHandler(this.FindButton_Click);
			// 
			// txtBxRes
			// 
			this.txtBxRes.Location = new System.Drawing.Point(179, 37);
			this.txtBxRes.Name = "txtBxRes";
			this.txtBxRes.Size = new System.Drawing.Size(138, 20);
			this.txtBxRes.TabIndex = 14;
			this.txtBxRes.Tag = "";
			// 
			// startTutorial
			// 
			this.startTutorial.AutoSize = true;
			this.startTutorial.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.startTutorial.Location = new System.Drawing.Point(820, 1);
			this.startTutorial.Name = "startTutorial";
			this.startTutorial.Size = new System.Drawing.Size(18, 20);
			this.startTutorial.TabIndex = 71;
			this.startTutorial.Text = "?";
			this.startTutorial.Click += new System.EventHandler(this.startTutorial_Click);
			this.startTutorial.MouseEnter += new System.EventHandler(this.startTutorial_MouseEnter);
			this.startTutorial.MouseLeave += new System.EventHandler(this.startTutorial_MouseLeave);
			// 
			// PlannedEventsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(841, 482);
			this.Controls.Add(this.startTutorial);
			this.Controls.Add(this.btnRes);
			this.Controls.Add(this.txtBxRes);
			this.Controls.Add(this.btnSearchTemplate);
			this.Controls.Add(this.txtBxTemplate);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.resLB);
			this.Controls.Add(this.onlyExpCB);
			this.Controls.Add(this.addGB);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.eventListGrid);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.eventsLB);
			this.Name = "PlannedEventsForm";
			this.Text = "Додання нового заходу";
			this.Load += new System.EventHandler(this.PlannedEventsForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.eventListGrid)).EndInit();
			this.addGB.ResumeLayout(false);
			this.addGB.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListBox eventsLB;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button addBtn;
		private System.Windows.Forms.DataGridView eventListGrid;
		private System.Windows.Forms.TextBox descTB;
		private System.Windows.Forms.TextBox evNameTB;
		private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox addGB;
        private System.Windows.Forms.CheckBox onlyExpCB;
        private System.Windows.Forms.ComboBox issuesCB;
        private System.Windows.Forms.ListBox resLB;
        private System.Windows.Forms.DataGridViewTextBoxColumn Resource;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBxTemplate;
        private System.Windows.Forms.Button btnSearchTemplate;
        private System.Windows.Forms.Button btnRes;
        private System.Windows.Forms.TextBox txtBxRes;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label startTutorial;
	}
}