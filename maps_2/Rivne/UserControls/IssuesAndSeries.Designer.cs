
namespace UserMap.UserControls
{
    partial class IssuesAndSeries
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
            this.IssueGroupBox = new System.Windows.Forms.GroupBox();
            this.SeriesCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.IssuesCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.IssueGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // IssueGroupBox
            // 
            this.IssueGroupBox.Controls.Add(this.SeriesCheckedListBox);
            this.IssueGroupBox.Controls.Add(this.IssuesCheckedListBox);
            this.IssueGroupBox.Controls.Add(this.label2);
            this.IssueGroupBox.Controls.Add(this.label1);
            this.IssueGroupBox.Location = new System.Drawing.Point(3, 3);
            this.IssueGroupBox.Name = "IssueGroupBox";
            this.IssueGroupBox.Size = new System.Drawing.Size(603, 109);
            this.IssueGroupBox.TabIndex = 11;
            this.IssueGroupBox.TabStop = false;
            // 
            // SeriesCheckedListBox
            // 
            this.SeriesCheckedListBox.CheckOnClick = true;
            this.SeriesCheckedListBox.FormattingEnabled = true;
            this.SeriesCheckedListBox.Location = new System.Drawing.Point(317, 34);
            this.SeriesCheckedListBox.Name = "SeriesCheckedListBox";
            this.SeriesCheckedListBox.Size = new System.Drawing.Size(276, 64);
            this.SeriesCheckedListBox.TabIndex = 7;
            // 
            // IssuesCheckedListBox
            // 
            this.IssuesCheckedListBox.FormattingEnabled = true;
            this.IssuesCheckedListBox.Location = new System.Drawing.Point(6, 34);
            this.IssuesCheckedListBox.Name = "IssuesCheckedListBox";
            this.IssuesCheckedListBox.Size = new System.Drawing.Size(276, 64);
            this.IssuesCheckedListBox.TabIndex = 7;
            this.IssuesCheckedListBox.SelectedIndexChanged += new System.EventHandler(this.IssuesCheckedListBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(317, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Серія";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Задача";
            // 
            // IssuesAndSeries
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.IssueGroupBox);
            this.Name = "IssuesAndSeries";
            this.Size = new System.Drawing.Size(612, 116);
            this.IssueGroupBox.ResumeLayout(false);
            this.IssueGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox IssueGroupBox;
        private System.Windows.Forms.CheckedListBox SeriesCheckedListBox;
        private System.Windows.Forms.CheckedListBox IssuesCheckedListBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}
