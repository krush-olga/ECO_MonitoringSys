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
            this.materialListGrid = new System.Windows.Forms.DataGridView();
            this.Resource = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.resourcesLB = new System.Windows.Forms.ListBox();
            this.addBtn = new System.Windows.Forms.Button();
            this.descTB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.nameTB = new System.Windows.Forms.TextBox();
            this.templatesLB = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.addGB = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.materialListGrid)).BeginInit();
            this.addGB.SuspendLayout();
            this.SuspendLayout();
            // 
            // materialListGrid
            // 
            this.materialListGrid.AllowUserToAddRows = false;
            this.materialListGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.materialListGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Resource});
            this.materialListGrid.Location = new System.Drawing.Point(316, 38);
            this.materialListGrid.Margin = new System.Windows.Forms.Padding(4);
            this.materialListGrid.Name = "materialListGrid";
            this.materialListGrid.ReadOnly = true;
            this.materialListGrid.Size = new System.Drawing.Size(490, 404);
            this.materialListGrid.TabIndex = 13;
            this.materialListGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.materialListGrid_CellContentClick);
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
            this.label1.Location = new System.Drawing.Point(5, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 17);
            this.label1.TabIndex = 12;
            this.label1.Text = "Назва шаблону";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(165, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 17);
            this.label3.TabIndex = 19;
            this.label3.Text = "Ресурси";
            // 
            // resourcesLB
            // 
            this.resourcesLB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.resourcesLB.FormattingEnabled = true;
            this.resourcesLB.ItemHeight = 16;
            this.resourcesLB.Location = new System.Drawing.Point(168, 38);
            this.resourcesLB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.resourcesLB.Name = "resourcesLB";
            this.resourcesLB.Size = new System.Drawing.Size(131, 404);
            this.resourcesLB.TabIndex = 18;
            this.resourcesLB.DoubleClick += new System.EventHandler(this.resourcesLB_DoubleClick);
            // 
            // addBtn
            // 
            this.addBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.addBtn.Location = new System.Drawing.Point(696, 19);
            this.addBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.addBtn.Name = "addBtn";
            this.addBtn.Size = new System.Drawing.Size(84, 26);
            this.addBtn.TabIndex = 17;
            this.addBtn.Text = "Зберегти";
            this.addBtn.UseVisualStyleBackColor = true;
            this.addBtn.Click += new System.EventHandler(this.addBtn_Click);
            // 
            // descTB
            // 
            this.descTB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.descTB.Location = new System.Drawing.Point(163, 49);
            this.descTB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.descTB.Name = "descTB";
            this.descTB.Size = new System.Drawing.Size(470, 22);
            this.descTB.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 17);
            this.label2.TabIndex = 15;
            this.label2.Text = "Опис";
            // 
            // nameTB
            // 
            this.nameTB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.nameTB.Location = new System.Drawing.Point(163, 19);
            this.nameTB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.nameTB.Name = "nameTB";
            this.nameTB.Size = new System.Drawing.Size(470, 22);
            this.nameTB.TabIndex = 14;
            // 
            // templatesLB
            // 
            this.templatesLB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.templatesLB.FormattingEnabled = true;
            this.templatesLB.ItemHeight = 16;
            this.templatesLB.Location = new System.Drawing.Point(20, 38);
            this.templatesLB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.templatesLB.Name = "templatesLB";
            this.templatesLB.Size = new System.Drawing.Size(142, 404);
            this.templatesLB.TabIndex = 20;
            this.templatesLB.SelectedIndexChanged += new System.EventHandler(this.templatesLB_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 17);
            this.label4.TabIndex = 21;
            this.label4.Text = "Шаблони";
            // 
            // addGB
            // 
            this.addGB.Controls.Add(this.button1);
            this.addGB.Controls.Add(this.label2);
            this.addGB.Controls.Add(this.nameTB);
            this.addGB.Controls.Add(this.descTB);
            this.addGB.Controls.Add(this.addBtn);
            this.addGB.Controls.Add(this.label1);
            this.addGB.Location = new System.Drawing.Point(20, 449);
            this.addGB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.addGB.Name = "addGB";
            this.addGB.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.addGB.Size = new System.Drawing.Size(786, 97);
            this.addGB.TabIndex = 22;
            this.addGB.TabStop = false;
            this.addGB.Visible = false;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(696, 49);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(84, 26);
            this.button1.TabIndex = 18;
            this.button1.Text = "Видалити";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // AlterTemplateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(827, 553);
            this.Controls.Add(this.addGB);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.templatesLB);
            this.Controls.Add(this.materialListGrid);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.resourcesLB);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "AlterTemplateForm";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Text = "Змінити шаблон";
            this.Load += new System.EventHandler(this.AlterTemplateForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.materialListGrid)).EndInit();
            this.addGB.ResumeLayout(false);
            this.addGB.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView materialListGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Resource;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox resourcesLB;
        private System.Windows.Forms.Button addBtn;
        private System.Windows.Forms.TextBox descTB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox nameTB;
        private System.Windows.Forms.ListBox templatesLB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox addGB;
        private System.Windows.Forms.Button button1;
    }
}