namespace Maps
{
    partial class CompareForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            this.CompareDataGridView = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.RowColorComboBox = new System.Windows.Forms.ComboBox();
            this.ColorUpdateButton = new System.Windows.Forms.Button();
            this.CompareColorDataGridView = new System.Windows.Forms.DataGridView();
            this.ColorBorder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Color = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.CompareChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label3 = new System.Windows.Forms.Label();
            this.LegendLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.CompareDataGridView)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CompareColorDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CompareChart)).BeginInit();
            this.SuspendLayout();
            // 
            // CompareDataGridView
            // 
            this.CompareDataGridView.AllowUserToAddRows = false;
            this.CompareDataGridView.AllowUserToDeleteRows = false;
            this.CompareDataGridView.AllowUserToOrderColumns = true;
            this.CompareDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CompareDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.CompareDataGridView.Location = new System.Drawing.Point(12, 44);
            this.CompareDataGridView.Name = "CompareDataGridView";
            this.CompareDataGridView.ReadOnly = true;
            this.CompareDataGridView.Size = new System.Drawing.Size(386, 228);
            this.CompareDataGridView.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(122, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(162, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Таблиця для порівняння даних";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.RowColorComboBox);
            this.groupBox1.Controls.Add(this.ColorUpdateButton);
            this.groupBox1.Controls.Add(this.CompareColorDataGridView);
            this.groupBox1.Location = new System.Drawing.Point(404, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(258, 245);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Кольори для розмежування";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(223, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Виберіть, для якого рядка відображується";
            // 
            // RowColorComboBox
            // 
            this.RowColorComboBox.FormattingEnabled = true;
            this.RowColorComboBox.Location = new System.Drawing.Point(9, 38);
            this.RowColorComboBox.Name = "RowColorComboBox";
            this.RowColorComboBox.Size = new System.Drawing.Size(243, 21);
            this.RowColorComboBox.TabIndex = 10;
            this.RowColorComboBox.SelectedIndexChanged += new System.EventHandler(this.RowColorComboBox_SelectedIndexChanged);
            // 
            // ColorUpdateButton
            // 
            this.ColorUpdateButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ColorUpdateButton.Location = new System.Drawing.Point(9, 216);
            this.ColorUpdateButton.Name = "ColorUpdateButton";
            this.ColorUpdateButton.Size = new System.Drawing.Size(243, 23);
            this.ColorUpdateButton.TabIndex = 9;
            this.ColorUpdateButton.Text = "Змінити поточний колір";
            this.ColorUpdateButton.UseVisualStyleBackColor = true;
            this.ColorUpdateButton.Click += new System.EventHandler(this.UpdateColorButton_Click);
            // 
            // CompareColorDataGridView
            // 
            this.CompareColorDataGridView.AllowUserToAddRows = false;
            this.CompareColorDataGridView.AllowUserToDeleteRows = false;
            this.CompareColorDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CompareColorDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CompareColorDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColorBorder,
            this.Color});
            this.CompareColorDataGridView.Location = new System.Drawing.Point(9, 65);
            this.CompareColorDataGridView.Name = "CompareColorDataGridView";
            this.CompareColorDataGridView.ReadOnly = true;
            this.CompareColorDataGridView.Size = new System.Drawing.Size(243, 145);
            this.CompareColorDataGridView.TabIndex = 8;
            this.CompareColorDataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.CompareColorDataGridView_CellDoubleClick);
            // 
            // ColorBorder
            // 
            this.ColorBorder.HeaderText = "Границя кольора";
            this.ColorBorder.Name = "ColorBorder";
            this.ColorBorder.ReadOnly = true;
            this.ColorBorder.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Color
            // 
            this.Color.HeaderText = "Колір";
            this.Color.Name = "Color";
            this.Color.ReadOnly = true;
            this.Color.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Color.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // CompareChart
            // 
            this.CompareChart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea2.AxisX.IsLabelAutoFit = false;
            chartArea2.AxisX.LabelStyle.Angle = -45;
            chartArea2.AxisX.Title = "Дати, за які викиди були взяті";
            chartArea2.AxisY.Title = "Значення викидів";
            chartArea2.Name = "MainArea";
            this.CompareChart.ChartAreas.Add(chartArea2);
            this.CompareChart.Location = new System.Drawing.Point(12, 278);
            this.CompareChart.MinimumSize = new System.Drawing.Size(650, 205);
            this.CompareChart.Name = "CompareChart";
            this.CompareChart.Size = new System.Drawing.Size(650, 205);
            this.CompareChart.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Легенда:";
            // 
            // LegendLabel
            // 
            this.LegendLabel.AutoSize = true;
            this.LegendLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LegendLabel.Location = new System.Drawing.Point(68, 9);
            this.LegendLabel.Name = "LegendLabel";
            this.LegendLabel.Size = new System.Drawing.Size(49, 13);
            this.LegendLabel.TabIndex = 6;
            this.LegendLabel.Text = "Legend";
            // 
            // CompareForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 493);
            this.Controls.Add(this.LegendLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.CompareChart);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CompareDataGridView);
            this.MinimumSize = new System.Drawing.Size(690, 530);
            this.Name = "CompareForm";
            this.Text = "Порівняння";
            ((System.ComponentModel.ISupportInitialize)(this.CompareDataGridView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CompareColorDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CompareChart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView CompareDataGridView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView CompareColorDataGridView;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColorBorder;
        private System.Windows.Forms.DataGridViewTextBoxColumn Color;
        private System.Windows.Forms.Button ColorUpdateButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox RowColorComboBox;
        private System.Windows.Forms.DataVisualization.Charting.Chart CompareChart;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label LegendLabel;
    }
}