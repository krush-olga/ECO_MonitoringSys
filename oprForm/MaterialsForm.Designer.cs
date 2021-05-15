namespace oprForm
{
    partial class MaterialsForm
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
			this.resDGV = new System.Windows.Forms.DataGridView();
			this.nameCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.priceCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.unitsCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.descCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.saveBtn = new System.Windows.Forms.Button();
			this.delBtn = new System.Windows.Forms.Button();
			this.addBtn = new System.Windows.Forms.Button();
			this.nameTB = new System.Windows.Forms.TextBox();
			this.measureTB = new System.Windows.Forms.TextBox();
			this.descriptionTB = new System.Windows.Forms.TextBox();
			this.priceTB = new System.Windows.Forms.TextBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.startTutorial = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.resDGV)).BeginInit();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// resDGV
			// 
			this.resDGV.AllowUserToAddRows = false;
			this.resDGV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.resDGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.resDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.resDGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameCol,
            this.priceCol,
            this.unitsCol,
            this.descCol});
			this.resDGV.Location = new System.Drawing.Point(8, 222);
			this.resDGV.Margin = new System.Windows.Forms.Padding(2);
			this.resDGV.Name = "resDGV";
			this.resDGV.ReadOnly = true;
			this.resDGV.RowTemplate.Height = 28;
			this.resDGV.Size = new System.Drawing.Size(644, 274);
			this.resDGV.TabIndex = 0;
			this.resDGV.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.resDGV_CellClick);
			// 
			// nameCol
			// 
			this.nameCol.FillWeight = 35F;
			this.nameCol.HeaderText = "Назва";
			this.nameCol.Name = "nameCol";
			this.nameCol.ReadOnly = true;
			// 
			// priceCol
			// 
			this.priceCol.FillWeight = 15F;
			this.priceCol.HeaderText = "Ціна";
			this.priceCol.Name = "priceCol";
			this.priceCol.ReadOnly = true;
			// 
			// unitsCol
			// 
			this.unitsCol.FillWeight = 15F;
			this.unitsCol.HeaderText = "Одиниця виміру";
			this.unitsCol.Name = "unitsCol";
			this.unitsCol.ReadOnly = true;
			// 
			// descCol
			// 
			this.descCol.FillWeight = 35F;
			this.descCol.HeaderText = "Опис";
			this.descCol.Name = "descCol";
			this.descCol.ReadOnly = true;
			// 
			// saveBtn
			// 
			this.saveBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.saveBtn.Location = new System.Drawing.Point(576, 94);
			this.saveBtn.Margin = new System.Windows.Forms.Padding(2);
			this.saveBtn.Name = "saveBtn";
			this.saveBtn.Size = new System.Drawing.Size(76, 24);
			this.saveBtn.TabIndex = 1;
			this.saveBtn.Text = "Редагувати";
			this.saveBtn.UseVisualStyleBackColor = true;
			this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
			// 
			// delBtn
			// 
			this.delBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.delBtn.Location = new System.Drawing.Point(576, 124);
			this.delBtn.Margin = new System.Windows.Forms.Padding(2);
			this.delBtn.Name = "delBtn";
			this.delBtn.Size = new System.Drawing.Size(76, 24);
			this.delBtn.TabIndex = 2;
			this.delBtn.Text = "Видалити";
			this.toolTip1.SetToolTip(this.delBtn, "Видаляться ресурси, що виділені в таблиці.\r\nДля виділення натисніть на зліва пуст" +
        "у клінику\r\nодного чи декількох рядків.");
			this.delBtn.UseVisualStyleBackColor = true;
			this.delBtn.Click += new System.EventHandler(this.delBtn_Click);
			// 
			// addBtn
			// 
			this.addBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.addBtn.Location = new System.Drawing.Point(576, 63);
			this.addBtn.Margin = new System.Windows.Forms.Padding(2);
			this.addBtn.Name = "addBtn";
			this.addBtn.Size = new System.Drawing.Size(76, 24);
			this.addBtn.TabIndex = 3;
			this.addBtn.Text = "Додати";
			this.addBtn.UseVisualStyleBackColor = true;
			this.addBtn.Click += new System.EventHandler(this.addBtn_Click);
			// 
			// nameTB
			// 
			this.nameTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.nameTB.Location = new System.Drawing.Point(99, 12);
			this.nameTB.Margin = new System.Windows.Forms.Padding(2);
			this.nameTB.Name = "nameTB";
			this.nameTB.Size = new System.Drawing.Size(454, 20);
			this.nameTB.TabIndex = 4;
			// 
			// measureTB
			// 
			this.measureTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.measureTB.Location = new System.Drawing.Point(99, 40);
			this.measureTB.Margin = new System.Windows.Forms.Padding(2);
			this.measureTB.Name = "measureTB";
			this.measureTB.Size = new System.Drawing.Size(454, 20);
			this.measureTB.TabIndex = 5;
			// 
			// descriptionTB
			// 
			this.descriptionTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.descriptionTB.Location = new System.Drawing.Point(99, 100);
			this.descriptionTB.Margin = new System.Windows.Forms.Padding(2);
			this.descriptionTB.Multiline = true;
			this.descriptionTB.Name = "descriptionTB";
			this.descriptionTB.Size = new System.Drawing.Size(455, 71);
			this.descriptionTB.TabIndex = 6;
			// 
			// priceTB
			// 
			this.priceTB.Location = new System.Drawing.Point(99, 67);
			this.priceTB.Margin = new System.Windows.Forms.Padding(2);
			this.priceTB.Name = "priceTB";
			this.priceTB.Size = new System.Drawing.Size(454, 20);
			this.priceTB.TabIndex = 7;
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.Controls.Add(this.label4);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.nameTB);
			this.panel1.Controls.Add(this.priceTB);
			this.panel1.Controls.Add(this.measureTB);
			this.panel1.Controls.Add(this.descriptionTB);
			this.panel1.Location = new System.Drawing.Point(9, 10);
			this.panel1.Margin = new System.Windows.Forms.Padding(2);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(563, 185);
			this.panel1.TabIndex = 8;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(12, 100);
			this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(33, 13);
			this.label4.TabIndex = 11;
			this.label4.Text = "Опис";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 43);
			this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(83, 13);
			this.label3.TabIndex = 10;
			this.label3.Text = "Одиниці виміру";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 70);
			this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(71, 13);
			this.label2.TabIndex = 9;
			this.label2.Text = "Ціна за 1 од.";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 12);
			this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(82, 13);
			this.label1.TabIndex = 8;
			this.label1.Text = "Назва ресурсу";
			// 
			// startTutorial
			// 
			this.startTutorial.AutoSize = true;
			this.startTutorial.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.startTutorial.Location = new System.Drawing.Point(641, 3);
			this.startTutorial.Name = "startTutorial";
			this.startTutorial.Size = new System.Drawing.Size(18, 20);
			this.startTutorial.TabIndex = 31;
			this.startTutorial.Text = "?";
			this.startTutorial.Click += new System.EventHandler(this.startTutorial_Click);
			this.startTutorial.MouseEnter += new System.EventHandler(this.startTutorial_MouseEnter);
			this.startTutorial.MouseLeave += new System.EventHandler(this.startTutorial_MouseLeave);
			// 
			// MaterialsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(662, 507);
			this.Controls.Add(this.startTutorial);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.delBtn);
			this.Controls.Add(this.resDGV);
			this.Controls.Add(this.addBtn);
			this.Controls.Add(this.saveBtn);
			this.Margin = new System.Windows.Forms.Padding(2);
			this.Name = "MaterialsForm";
			this.Text = "Ресурси";
			((System.ComponentModel.ISupportInitialize)(this.resDGV)).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView resDGV;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.Button delBtn;
        private System.Windows.Forms.Button addBtn;
        private System.Windows.Forms.TextBox nameTB;
        private System.Windows.Forms.TextBox measureTB;
        private System.Windows.Forms.TextBox descriptionTB;
        private System.Windows.Forms.TextBox priceTB;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn priceCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn unitsCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn descCol;
        private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Label startTutorial;
	}
}