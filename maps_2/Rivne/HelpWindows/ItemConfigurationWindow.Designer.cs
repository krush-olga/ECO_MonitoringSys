
namespace UserMap.HelpWindows
{
    partial class ItemConfigurationWindow
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

            if (dbManager != null)
            {
                dbManager.Dispose();
            }
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			this.EnvironmentsCheckedListBox = new System.Windows.Forms.CheckedListBox();
			this.m_AcceptButton = new System.Windows.Forms.Button();
			this.m_CancelButton = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.IssueGroupBox = new System.Windows.Forms.GroupBox();
			this.SeriesCheckedListBox = new System.Windows.Forms.CheckedListBox();
			this.IssuesCheckedListBox = new System.Windows.Forms.CheckedListBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.ObjectDescriptionTextBox = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.ObjectNameTextBox = new System.Windows.Forms.TextBox();
			this.IssueCheckBox = new System.Windows.Forms.CheckBox();
			this.EmissionGroupBox = new System.Windows.Forms.GroupBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.CurrentElementLable = new System.Windows.Forms.Label();
			this.YearNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.MonthNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.label12 = new System.Windows.Forms.Label();
			this.DayNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.label11 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.AvgEmissionValueTextBox = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.MaxEmissionValueTextBox = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.ElementsCheckedListBox = new System.Windows.Forms.CheckedListBox();
			this.label7 = new System.Windows.Forms.Label();
			this.EmissionCheckBox = new System.Windows.Forms.CheckBox();
			this.ButtonsToolTip = new System.Windows.Forms.ToolTip(this.components);
			this.startTutorial = new System.Windows.Forms.Label();
			this.IssueGroupBox.SuspendLayout();
			this.EmissionGroupBox.SuspendLayout();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.YearNumericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.MonthNumericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.DayNumericUpDown)).BeginInit();
			this.SuspendLayout();
			// 
			// EnvironmentsCheckedListBox
			// 
			this.EnvironmentsCheckedListBox.FormattingEnabled = true;
			this.EnvironmentsCheckedListBox.Location = new System.Drawing.Point(13, 19);
			this.EnvironmentsCheckedListBox.Name = "EnvironmentsCheckedListBox";
			this.EnvironmentsCheckedListBox.Size = new System.Drawing.Size(276, 64);
			this.EnvironmentsCheckedListBox.TabIndex = 6;
			this.EnvironmentsCheckedListBox.SelectedIndexChanged += new System.EventHandler(this.EnvironmentsCheckedListBox_SelectedIndexChanged);
			// 
			// m_AcceptButton
			// 
			this.m_AcceptButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.m_AcceptButton.Location = new System.Drawing.Point(230, 537);
			this.m_AcceptButton.Name = "m_AcceptButton";
			this.m_AcceptButton.Size = new System.Drawing.Size(80, 23);
			this.m_AcceptButton.TabIndex = 3;
			this.m_AcceptButton.Text = "Підтвердити";
			this.m_AcceptButton.UseVisualStyleBackColor = true;
			this.m_AcceptButton.Click += new System.EventHandler(this.m_AcceptButton_Click);
			// 
			// m_CancelButton
			// 
			this.m_CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.m_CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.m_CancelButton.Location = new System.Drawing.Point(316, 537);
			this.m_CancelButton.Name = "m_CancelButton";
			this.m_CancelButton.Size = new System.Drawing.Size(80, 23);
			this.m_CancelButton.TabIndex = 4;
			this.m_CancelButton.Text = "Відміна";
			this.ButtonsToolTip.SetToolTip(this.m_CancelButton, "На даний момент не працює");
			this.m_CancelButton.UseVisualStyleBackColor = true;
			this.m_CancelButton.Click += new System.EventHandler(this.m_CancelButton_Click);
			// 
			// label5
			// 
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label5.Location = new System.Drawing.Point(9, 563);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(156, 17);
			this.label5.TabIndex = 9;
			this.label5.Text = "* - обов\'язкові поля";
			// 
			// IssueGroupBox
			// 
			this.IssueGroupBox.Controls.Add(this.SeriesCheckedListBox);
			this.IssueGroupBox.Controls.Add(this.IssuesCheckedListBox);
			this.IssueGroupBox.Controls.Add(this.label2);
			this.IssueGroupBox.Controls.Add(this.label1);
			this.IssueGroupBox.Location = new System.Drawing.Point(12, 27);
			this.IssueGroupBox.Name = "IssueGroupBox";
			this.IssueGroupBox.Size = new System.Drawing.Size(603, 109);
			this.IssueGroupBox.TabIndex = 10;
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
			this.label1.Location = new System.Drawing.Point(6, 18);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(43, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Задача";
			// 
			// label4
			// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(127, 426);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(33, 13);
			this.label4.TabIndex = 8;
			this.label4.Text = "Опис";
			// 
			// ObjectDescriptionTextBox
			// 
			this.ObjectDescriptionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.ObjectDescriptionTextBox.Location = new System.Drawing.Point(130, 442);
			this.ObjectDescriptionTextBox.MaxLength = 200;
			this.ObjectDescriptionTextBox.Multiline = true;
			this.ObjectDescriptionTextBox.Name = "ObjectDescriptionTextBox";
			this.ObjectDescriptionTextBox.Size = new System.Drawing.Size(388, 89);
			this.ObjectDescriptionTextBox.TabIndex = 7;
			this.ObjectDescriptionTextBox.Click += new System.EventHandler(this.ObjectDescriptionTextBox_Click);
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(127, 387);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(46, 13);
			this.label3.TabIndex = 6;
			this.label3.Text = "Назва *";
			// 
			// ObjectNameTextBox
			// 
			this.ObjectNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.ObjectNameTextBox.Location = new System.Drawing.Point(130, 403);
			this.ObjectNameTextBox.MaxLength = 45;
			this.ObjectNameTextBox.Name = "ObjectNameTextBox";
			this.ObjectNameTextBox.Size = new System.Drawing.Size(388, 20);
			this.ObjectNameTextBox.TabIndex = 5;
			this.ObjectNameTextBox.Click += new System.EventHandler(this.ObjectNameTextBox_Click);
			// 
			// IssueCheckBox
			// 
			this.IssueCheckBox.AutoSize = true;
			this.IssueCheckBox.Location = new System.Drawing.Point(21, 22);
			this.IssueCheckBox.Name = "IssueCheckBox";
			this.IssueCheckBox.Size = new System.Drawing.Size(127, 17);
			this.IssueCheckBox.TabIndex = 11;
			this.IssueCheckBox.Text = "Прив\'язка по задачі";
			this.IssueCheckBox.UseVisualStyleBackColor = true;
			this.IssueCheckBox.CheckedChanged += new System.EventHandler(this.IssueCheckBox_CheckedChanged);
			// 
			// EmissionGroupBox
			// 
			this.EmissionGroupBox.Controls.Add(this.groupBox1);
			this.EmissionGroupBox.Controls.Add(this.ElementsCheckedListBox);
			this.EmissionGroupBox.Controls.Add(this.label7);
			this.EmissionGroupBox.Location = new System.Drawing.Point(12, 142);
			this.EmissionGroupBox.Name = "EmissionGroupBox";
			this.EmissionGroupBox.Size = new System.Drawing.Size(603, 212);
			this.EmissionGroupBox.TabIndex = 11;
			this.EmissionGroupBox.TabStop = false;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.CurrentElementLable);
			this.groupBox1.Controls.Add(this.EnvironmentsCheckedListBox);
			this.groupBox1.Controls.Add(this.YearNumericUpDown);
			this.groupBox1.Controls.Add(this.MonthNumericUpDown);
			this.groupBox1.Controls.Add(this.label12);
			this.groupBox1.Controls.Add(this.DayNumericUpDown);
			this.groupBox1.Controls.Add(this.label11);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.AvgEmissionValueTextBox);
			this.groupBox1.Controls.Add(this.label8);
			this.groupBox1.Controls.Add(this.label10);
			this.groupBox1.Controls.Add(this.MaxEmissionValueTextBox);
			this.groupBox1.Controls.Add(this.label9);
			this.groupBox1.Location = new System.Drawing.Point(298, 17);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(299, 188);
			this.groupBox1.TabIndex = 18;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Актуальне для елементу:";
			// 
			// CurrentElementLable
			// 
			this.CurrentElementLable.AutoSize = true;
			this.CurrentElementLable.Location = new System.Drawing.Point(140, 0);
			this.CurrentElementLable.Name = "CurrentElementLable";
			this.CurrentElementLable.Size = new System.Drawing.Size(16, 13);
			this.CurrentElementLable.TabIndex = 18;
			this.CurrentElementLable.Text = "...";
			this.CurrentElementLable.TextChanged += new System.EventHandler(this.CurrentElementLable_TextChanged);
			// 
			// YearNumericUpDown
			// 
			this.YearNumericUpDown.Location = new System.Drawing.Point(13, 102);
			this.YearNumericUpDown.Name = "YearNumericUpDown";
			this.YearNumericUpDown.Size = new System.Drawing.Size(100, 20);
			this.YearNumericUpDown.TabIndex = 3;
			// 
			// MonthNumericUpDown
			// 
			this.MonthNumericUpDown.Location = new System.Drawing.Point(140, 102);
			this.MonthNumericUpDown.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
			this.MonthNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.MonthNumericUpDown.Name = "MonthNumericUpDown";
			this.MonthNumericUpDown.Size = new System.Drawing.Size(60, 20);
			this.MonthNumericUpDown.TabIndex = 4;
			this.MonthNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(10, 127);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(96, 13);
			this.label12.TabIndex = 17;
			this.label12.Text = "Значення викидів";
			// 
			// DayNumericUpDown
			// 
			this.DayNumericUpDown.Location = new System.Drawing.Point(229, 102);
			this.DayNumericUpDown.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
			this.DayNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.DayNumericUpDown.Name = "DayNumericUpDown";
			this.DayNumericUpDown.Size = new System.Drawing.Size(60, 20);
			this.DayNumericUpDown.TabIndex = 5;
			this.DayNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(160, 146);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(100, 13);
			this.label11.TabIndex = 16;
			this.label11.Text = "Середнє значення";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(13, 86);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(22, 13);
			this.label6.TabIndex = 6;
			this.label6.Text = "Рік";
			// 
			// AvgEmissionValueTextBox
			// 
			this.AvgEmissionValueTextBox.Location = new System.Drawing.Point(164, 162);
			this.AvgEmissionValueTextBox.MaxLength = 20;
			this.AvgEmissionValueTextBox.Name = "AvgEmissionValueTextBox";
			this.AvgEmissionValueTextBox.Size = new System.Drawing.Size(125, 20);
			this.AvgEmissionValueTextBox.TabIndex = 15;
			this.AvgEmissionValueTextBox.Text = "0";
			this.AvgEmissionValueTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.EmissionValueTextBox_KeyPress);
			this.AvgEmissionValueTextBox.Leave += new System.EventHandler(this.EmissionValueTextBox_Leave);
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(137, 86);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(42, 13);
			this.label8.TabIndex = 7;
			this.label8.Text = "Місяць";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(10, 146);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(128, 13);
			this.label10.TabIndex = 14;
			this.label10.Text = "Максимальне значення";
			// 
			// MaxEmissionValueTextBox
			// 
			this.MaxEmissionValueTextBox.Location = new System.Drawing.Point(13, 162);
			this.MaxEmissionValueTextBox.MaxLength = 20;
			this.MaxEmissionValueTextBox.Name = "MaxEmissionValueTextBox";
			this.MaxEmissionValueTextBox.Size = new System.Drawing.Size(125, 20);
			this.MaxEmissionValueTextBox.TabIndex = 13;
			this.MaxEmissionValueTextBox.Text = "0";
			this.MaxEmissionValueTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.EmissionValueTextBox_KeyPress);
			this.MaxEmissionValueTextBox.Leave += new System.EventHandler(this.EmissionValueTextBox_Leave);
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(226, 86);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(34, 13);
			this.label9.TabIndex = 8;
			this.label9.Text = "День";
			// 
			// ElementsCheckedListBox
			// 
			this.ElementsCheckedListBox.FormattingEnabled = true;
			this.ElementsCheckedListBox.Location = new System.Drawing.Point(6, 36);
			this.ElementsCheckedListBox.Name = "ElementsCheckedListBox";
			this.ElementsCheckedListBox.Size = new System.Drawing.Size(276, 169);
			this.ElementsCheckedListBox.TabIndex = 8;
			this.ElementsCheckedListBox.SelectedIndexChanged += new System.EventHandler(this.ElementsCheckedListBox_SelectedIndexChanged);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(6, 20);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(51, 13);
			this.label7.TabIndex = 2;
			this.label7.Text = "Елемент";
			// 
			// EmissionCheckBox
			// 
			this.EmissionCheckBox.AutoSize = true;
			this.EmissionCheckBox.Location = new System.Drawing.Point(24, 141);
			this.EmissionCheckBox.Name = "EmissionCheckBox";
			this.EmissionCheckBox.Size = new System.Drawing.Size(140, 17);
			this.EmissionCheckBox.TabIndex = 12;
			this.EmissionCheckBox.Text = "Прив\'язка по викидам";
			this.EmissionCheckBox.UseVisualStyleBackColor = true;
			this.EmissionCheckBox.CheckedChanged += new System.EventHandler(this.EmissionCheckBox_CheckedChanged);
			// 
			// startTutorial
			// 
			this.startTutorial.AutoSize = true;
			this.startTutorial.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.startTutorial.Location = new System.Drawing.Point(606, 3);
			this.startTutorial.Name = "startTutorial";
			this.startTutorial.Size = new System.Drawing.Size(18, 20);
			this.startTutorial.TabIndex = 70;
			this.startTutorial.Text = "?";
			this.startTutorial.Click += new System.EventHandler(this.startTutorial_Click);
			this.startTutorial.MouseEnter += new System.EventHandler(this.startTutorial_MouseEnter);
			this.startTutorial.MouseLeave += new System.EventHandler(this.startTutorial_MouseLeave);
			// 
			// ItemConfigurationWindow
			// 
			this.AcceptButton = this.m_AcceptButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.m_CancelButton;
			this.ClientSize = new System.Drawing.Size(627, 589);
			this.Controls.Add(this.startTutorial);
			this.Controls.Add(this.EmissionCheckBox);
			this.Controls.Add(this.EmissionGroupBox);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.ObjectDescriptionTextBox);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.IssueCheckBox);
			this.Controls.Add(this.ObjectNameTextBox);
			this.Controls.Add(this.IssueGroupBox);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.m_CancelButton);
			this.Controls.Add(this.m_AcceptButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "ItemConfigurationWindow";
			this.Text = "Налаштування об\'єкту";
			this.Load += new System.EventHandler(this.ItemConfigurationWindow_Load);
			this.IssueGroupBox.ResumeLayout(false);
			this.IssueGroupBox.PerformLayout();
			this.EmissionGroupBox.ResumeLayout(false);
			this.EmissionGroupBox.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.YearNumericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.MonthNumericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.DayNumericUpDown)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button m_AcceptButton;
        private System.Windows.Forms.Button m_CancelButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox IssueGroupBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox ObjectDescriptionTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox IssueCheckBox;
        private System.Windows.Forms.TextBox ObjectNameTextBox;
        private System.Windows.Forms.GroupBox EmissionGroupBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox AvgEmissionValueTextBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox MaxEmissionValueTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown DayNumericUpDown;
        private System.Windows.Forms.NumericUpDown MonthNumericUpDown;
        private System.Windows.Forms.NumericUpDown YearNumericUpDown;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox EmissionCheckBox;
        private System.Windows.Forms.CheckedListBox EnvironmentsCheckedListBox;
        private System.Windows.Forms.CheckedListBox SeriesCheckedListBox;
        private System.Windows.Forms.CheckedListBox IssuesCheckedListBox;
        private System.Windows.Forms.CheckedListBox ElementsCheckedListBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label CurrentElementLable;
        private System.Windows.Forms.ToolTip ButtonsToolTip;
		private System.Windows.Forms.Label startTutorial;
	}
}