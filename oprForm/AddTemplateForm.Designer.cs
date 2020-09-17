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
            this.label1 = new System.Windows.Forms.Label();
            this.materialListGrid = new System.Windows.Forms.DataGridView();
            this.Resource = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameTB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.descTB = new System.Windows.Forms.TextBox();
            this.addBtn = new System.Windows.Forms.Button();
            this.resourcesLB = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.materialListGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(23, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Назва шаблону";
            // 
            // materialListGrid
            // 
            this.materialListGrid.AllowUserToAddRows = false;
            this.materialListGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.materialListGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.materialListGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Resource});
            this.materialListGrid.Location = new System.Drawing.Point(174, 146);
            this.materialListGrid.Margin = new System.Windows.Forms.Padding(4);
            this.materialListGrid.Name = "materialListGrid";
            this.materialListGrid.ReadOnly = true;
            this.materialListGrid.Size = new System.Drawing.Size(580, 360);
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
            this.nameTB.Location = new System.Drawing.Point(174, 33);
            this.nameTB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.nameTB.Name = "nameTB";
            this.nameTB.Size = new System.Drawing.Size(580, 22);
            this.nameTB.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "Опис";
            // 
            // descTB
            // 
            this.descTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.descTB.Location = new System.Drawing.Point(174, 64);
            this.descTB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.descTB.Name = "descTB";
            this.descTB.Size = new System.Drawing.Size(580, 22);
            this.descTB.TabIndex = 8;
            this.descTB.TextChanged += new System.EventHandler(this.descTB_TextChanged);
            // 
            // addBtn
            // 
            this.addBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.addBtn.Location = new System.Drawing.Point(174, 522);
            this.addBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.addBtn.Name = "addBtn";
            this.addBtn.Size = new System.Drawing.Size(580, 31);
            this.addBtn.TabIndex = 9;
            this.addBtn.Text = "Додати";
            this.addBtn.UseVisualStyleBackColor = true;
            this.addBtn.Click += new System.EventHandler(this.addBtn_Click);
            // 
            // resourcesLB
            // 
            this.resourcesLB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.resourcesLB.FormattingEnabled = true;
            this.resourcesLB.ItemHeight = 16;
            this.resourcesLB.Location = new System.Drawing.Point(22, 149);
            this.resourcesLB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.resourcesLB.Name = "resourcesLB";
            this.resourcesLB.Size = new System.Drawing.Size(131, 356);
            this.resourcesLB.TabIndex = 10;
            this.resourcesLB.DoubleClick += new System.EventHandler(this.resourcesLB_DoubleClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 17);
            this.label3.TabIndex = 11;
            this.label3.Text = "Ресурси";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(174, 113);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(117, 17);
            this.label4.TabIndex = 12;
            this.label4.Text = "Зразок шаблону";
            // 
            // AddTemplateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 592);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.resourcesLB);
            this.Controls.Add(this.addBtn);
            this.Controls.Add(this.descTB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nameTB);
            this.Controls.Add(this.materialListGrid);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "AddTemplateForm";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Text = "Додати шаблон";
            this.Load += new System.EventHandler(this.AddTemplateForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.materialListGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView materialListGrid;
        private System.Windows.Forms.TextBox nameTB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox descTB;
        private System.Windows.Forms.Button addBtn;
        private System.Windows.Forms.ListBox resourcesLB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Resource;
        private System.Windows.Forms.Label label4;
    }
}