
namespace UserMap.UserControls
{
    partial class IssueSeriesUC
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
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.label8 = new System.Windows.Forms.Label();
            this.RefreshCalcResButton = new System.Windows.Forms.Button();
            this.CalculationsResultDataGridView = new System.Windows.Forms.DataGridView();
            this.SeriesName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Formula = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Result = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FormulaDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IssueGroupBox = new System.Windows.Forms.GroupBox();
            this.SeriesCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.IssuesCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.CalculationsResultDataGridView)).BeginInit();
            this.IssueGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 137);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(169, 13);
            this.label8.TabIndex = 75;
            this.label8.Text = "Результати розрахунків по серії";
            // 
            // RefreshCalcResButton
            // 
            this.RefreshCalcResButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RefreshCalcResButton.Location = new System.Drawing.Point(694, 127);
            this.RefreshCalcResButton.Name = "RefreshCalcResButton";
            this.RefreshCalcResButton.Size = new System.Drawing.Size(127, 23);
            this.RefreshCalcResButton.TabIndex = 78;
            this.RefreshCalcResButton.Text = "Оновити дані";
            this.RefreshCalcResButton.UseVisualStyleBackColor = true;
            this.RefreshCalcResButton.Click += new System.EventHandler(this.RefreshCalcResButton_Click);
            // 
            // CalculationsResultDataGridView
            // 
            this.CalculationsResultDataGridView.AllowUserToAddRows = false;
            this.CalculationsResultDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CalculationsResultDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CalculationsResultDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SeriesName,
            this.Formula,
            this.Result,
            this.FormulaDescription,
            this.Date});
            this.CalculationsResultDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.CalculationsResultDataGridView.Location = new System.Drawing.Point(6, 156);
            this.CalculationsResultDataGridView.Name = "CalculationsResultDataGridView";
            this.CalculationsResultDataGridView.ReadOnly = true;
            this.CalculationsResultDataGridView.Size = new System.Drawing.Size(815, 260);
            this.CalculationsResultDataGridView.TabIndex = 77;
            this.CalculationsResultDataGridView.TabStop = false;
            // 
            // SeriesName
            // 
            this.SeriesName.HeaderText = "Назва серії";
            this.SeriesName.Name = "SeriesName";
            this.SeriesName.ReadOnly = true;
            // 
            // Formula
            // 
            this.Formula.HeaderText = "Формула, по якій розрахували";
            this.Formula.Name = "Formula";
            this.Formula.ReadOnly = true;
            // 
            // Result
            // 
            this.Result.HeaderText = "Результат";
            this.Result.Name = "Result";
            this.Result.ReadOnly = true;
            // 
            // FormulaDescription
            // 
            this.FormulaDescription.HeaderText = "Опис формули";
            this.FormulaDescription.Name = "FormulaDescription";
            this.FormulaDescription.ReadOnly = true;
            // 
            // Date
            // 
            this.Date.HeaderText = "Дата";
            this.Date.Name = "Date";
            this.Date.ReadOnly = true;
            // 
            // IssueGroupBox
            // 
            this.IssueGroupBox.Controls.Add(this.SeriesCheckedListBox);
            this.IssueGroupBox.Controls.Add(this.IssuesCheckedListBox);
            this.IssueGroupBox.Controls.Add(this.label6);
            this.IssueGroupBox.Controls.Add(this.label7);
            this.IssueGroupBox.Location = new System.Drawing.Point(3, 3);
            this.IssueGroupBox.Name = "IssueGroupBox";
            this.IssueGroupBox.Size = new System.Drawing.Size(597, 120);
            this.IssueGroupBox.TabIndex = 76;
            this.IssueGroupBox.TabStop = false;
            // 
            // SeriesCheckedListBox
            // 
            this.SeriesCheckedListBox.CheckOnClick = true;
            this.SeriesCheckedListBox.FormattingEnabled = true;
            this.SeriesCheckedListBox.Location = new System.Drawing.Point(317, 34);
            this.SeriesCheckedListBox.Name = "SeriesCheckedListBox";
            this.SeriesCheckedListBox.Size = new System.Drawing.Size(276, 79);
            this.SeriesCheckedListBox.TabIndex = 7;
            this.SeriesCheckedListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.SeriesCheckedListBox_ItemCheck);
            // 
            // IssuesCheckedListBox
            // 
            this.IssuesCheckedListBox.FormattingEnabled = true;
            this.IssuesCheckedListBox.Location = new System.Drawing.Point(6, 34);
            this.IssuesCheckedListBox.Name = "IssuesCheckedListBox";
            this.IssuesCheckedListBox.Size = new System.Drawing.Size(276, 79);
            this.IssuesCheckedListBox.TabIndex = 7;
            this.IssuesCheckedListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.IssuesCheckedListBox_ItemCheck);
            this.IssuesCheckedListBox.SelectedIndexChanged += new System.EventHandler(this.IssuesCheckedListBox_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(317, 18);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Серія";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Задача";
            // 
            // IssueSeriesUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label8);
            this.Controls.Add(this.RefreshCalcResButton);
            this.Controls.Add(this.CalculationsResultDataGridView);
            this.Controls.Add(this.IssueGroupBox);
            this.Name = "IssueSeriesUC";
            this.Size = new System.Drawing.Size(825, 419);
            this.Load += new System.EventHandler(this.IssueSeriesUC_Load);
            ((System.ComponentModel.ISupportInitialize)(this.CalculationsResultDataGridView)).EndInit();
            this.IssueGroupBox.ResumeLayout(false);
            this.IssueGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button RefreshCalcResButton;
        private System.Windows.Forms.DataGridView CalculationsResultDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn SeriesName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Formula;
        private System.Windows.Forms.DataGridViewTextBoxColumn Result;
        private System.Windows.Forms.DataGridViewTextBoxColumn FormulaDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.GroupBox IssueGroupBox;
        private System.Windows.Forms.CheckedListBox SeriesCheckedListBox;
        private System.Windows.Forms.CheckedListBox IssuesCheckedListBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
    }
}
