
namespace UserMap.UserControls
{
    partial class MedStatUserControl
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            if (dbManager != null)
            {
                dbManager.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.MedStatParamLable = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.MedStatParamDataGridView = new System.Windows.Forms.DataGridView();
            this.MedStatDataGridView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.MedStatParamDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MedStatDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // MedStatParamLable
            // 
            this.MedStatParamLable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MedStatParamLable.AutoSize = true;
            this.MedStatParamLable.Location = new System.Drawing.Point(424, 7);
            this.MedStatParamLable.Name = "MedStatParamLable";
            this.MedStatParamLable.Size = new System.Drawing.Size(182, 13);
            this.MedStatParamLable.TabIndex = 47;
            this.MedStatParamLable.Text = "Параметри по вибраній статистиці";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(14, 7);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(111, 13);
            this.label9.TabIndex = 46;
            this.label9.Text = "Медична статистика";
            // 
            // MedStatParamDataGridView
            // 
            this.MedStatParamDataGridView.AllowUserToAddRows = false;
            this.MedStatParamDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MedStatParamDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MedStatParamDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.MedStatParamDataGridView.Location = new System.Drawing.Point(427, 23);
            this.MedStatParamDataGridView.MinimumSize = new System.Drawing.Size(400, 200);
            this.MedStatParamDataGridView.Name = "MedStatParamDataGridView";
            this.MedStatParamDataGridView.ReadOnly = true;
            this.MedStatParamDataGridView.Size = new System.Drawing.Size(400, 249);
            this.MedStatParamDataGridView.TabIndex = 45;
            this.MedStatParamDataGridView.TabStop = false;
            this.MedStatParamDataGridView.Tag = "2";
            this.MedStatParamDataGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.MedStatDataGridView_CellFormatting);
            // 
            // MedStatDataGridView
            // 
            this.MedStatDataGridView.AllowUserToAddRows = false;
            this.MedStatDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MedStatDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.MedStatDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MedStatDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.MedStatDataGridView.Location = new System.Drawing.Point(14, 23);
            this.MedStatDataGridView.MinimumSize = new System.Drawing.Size(400, 200);
            this.MedStatDataGridView.Name = "MedStatDataGridView";
            this.MedStatDataGridView.ReadOnly = true;
            this.MedStatDataGridView.Size = new System.Drawing.Size(400, 249);
            this.MedStatDataGridView.TabIndex = 44;
            this.MedStatDataGridView.TabStop = false;
            this.MedStatDataGridView.Tag = "1";
            this.MedStatDataGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.MedStatDataGridView_CellFormatting);
            this.MedStatDataGridView.SelectionChanged += new System.EventHandler(this.MedStatDataGridView_SelectionChanged);
            // 
            // MedStatUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.MedStatParamLable);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.MedStatParamDataGridView);
            this.Controls.Add(this.MedStatDataGridView);
            this.MinimumSize = new System.Drawing.Size(840, 285);
            this.Name = "MedStatUserControl";
            this.Size = new System.Drawing.Size(840, 285);
            this.Load += new System.EventHandler(this.MedStatUserControl_Load);
            this.Resize += new System.EventHandler(this.MedStatUserControlTabControl_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.MedStatParamDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MedStatDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label MedStatParamLable;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridView MedStatParamDataGridView;
        private System.Windows.Forms.DataGridView MedStatDataGridView;
    }
}
