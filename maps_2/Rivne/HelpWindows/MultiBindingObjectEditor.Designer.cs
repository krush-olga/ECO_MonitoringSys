
namespace UserMap.HelpWindows
{
    partial class MultiBindingObjectEditor
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

                try
                {
                    base.Dispose(disposing);
                }
                catch { }
            }

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
            this.SaveToBDButton = new System.Windows.Forms.Button();
            this.RestoreButton = new System.Windows.Forms.Button();
            this.CalcResToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.RefreshCalcResButton = new System.Windows.Forms.Button();
            this.EmissionTabPage = new System.Windows.Forms.TabPage();
            this.AddEmissionButton = new System.Windows.Forms.Button();
            this.ChangeEmissionButton = new System.Windows.Forms.Button();
            this.DeleteEmissionButton = new System.Windows.Forms.Button();
            this.CurrentEmisionGroupBox = new System.Windows.Forms.GroupBox();
            this.EnvironmentsComboBox = new System.Windows.Forms.ComboBox();
            this.YearNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.MonthNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.DayNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.ElementsComboBox = new System.Windows.Forms.ComboBox();
            this.AvgValueTextBox = new System.Windows.Forms.TextBox();
            this.MaxValueTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.EmissionsDataGridView = new System.Windows.Forms.DataGridView();
            this.IssueTabPage = new System.Windows.Forms.TabPage();
            this.label8 = new System.Windows.Forms.Label();
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
            this.ContentContainerTabControl = new System.Windows.Forms.TabControl();
            this.EmissionTabPage.SuspendLayout();
            this.CurrentEmisionGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.YearNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MonthNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DayNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EmissionsDataGridView)).BeginInit();
            this.IssueTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CalculationsResultDataGridView)).BeginInit();
            this.IssueGroupBox.SuspendLayout();
            this.ContentContainerTabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // SaveToBDButton
            // 
            this.SaveToBDButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveToBDButton.Enabled = false;
            this.SaveToBDButton.Location = new System.Drawing.Point(659, 456);
            this.SaveToBDButton.Name = "SaveToBDButton";
            this.SaveToBDButton.Size = new System.Drawing.Size(166, 23);
            this.SaveToBDButton.TabIndex = 72;
            this.SaveToBDButton.Text = "Зберегти до бази даних";
            this.SaveToBDButton.UseVisualStyleBackColor = true;
            this.SaveToBDButton.Click += new System.EventHandler(this.SaveToBDButton_Click);
            // 
            // RestoreButton
            // 
            this.RestoreButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.RestoreButton.Enabled = false;
            this.RestoreButton.Location = new System.Drawing.Point(487, 456);
            this.RestoreButton.Name = "RestoreButton";
            this.RestoreButton.Size = new System.Drawing.Size(166, 23);
            this.RestoreButton.TabIndex = 73;
            this.RestoreButton.Text = "Відмінити усі дії";
            this.RestoreButton.UseVisualStyleBackColor = true;
            this.RestoreButton.Click += new System.EventHandler(this.RestoreEmissionsButton_Click);
            // 
            // RefreshCalcResButton
            // 
            this.RefreshCalcResButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RefreshCalcResButton.Location = new System.Drawing.Point(694, 127);
            this.RefreshCalcResButton.Name = "RefreshCalcResButton";
            this.RefreshCalcResButton.Size = new System.Drawing.Size(127, 23);
            this.RefreshCalcResButton.TabIndex = 74;
            this.RefreshCalcResButton.Text = "Оновити дані";
            this.CalcResToolTip.SetToolTip(this.RefreshCalcResButton, "Дані оновлюються згідно вибраних серій\r\n");
            this.RefreshCalcResButton.UseVisualStyleBackColor = true;
            this.RefreshCalcResButton.Click += new System.EventHandler(this.RefreshCalcResButton_Click);
            // 
            // EmissionTabPage
            // 
            this.EmissionTabPage.Controls.Add(this.AddEmissionButton);
            this.EmissionTabPage.Controls.Add(this.ChangeEmissionButton);
            this.EmissionTabPage.Controls.Add(this.DeleteEmissionButton);
            this.EmissionTabPage.Controls.Add(this.CurrentEmisionGroupBox);
            this.EmissionTabPage.Controls.Add(this.EmissionsDataGridView);
            this.EmissionTabPage.Location = new System.Drawing.Point(4, 22);
            this.EmissionTabPage.Name = "EmissionTabPage";
            this.EmissionTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.EmissionTabPage.Size = new System.Drawing.Size(831, 428);
            this.EmissionTabPage.TabIndex = 0;
            this.EmissionTabPage.Text = "Викиди";
            this.EmissionTabPage.UseVisualStyleBackColor = true;
            // 
            // AddEmissionButton
            // 
            this.AddEmissionButton.Enabled = false;
            this.AddEmissionButton.Location = new System.Drawing.Point(575, 11);
            this.AddEmissionButton.Name = "AddEmissionButton";
            this.AddEmissionButton.Size = new System.Drawing.Size(100, 23);
            this.AddEmissionButton.TabIndex = 71;
            this.AddEmissionButton.Text = "Додати";
            this.AddEmissionButton.UseVisualStyleBackColor = true;
            this.AddEmissionButton.Click += new System.EventHandler(this.AddEmissionButton_Click);
            // 
            // ChangeEmissionButton
            // 
            this.ChangeEmissionButton.Enabled = false;
            this.ChangeEmissionButton.Location = new System.Drawing.Point(575, 55);
            this.ChangeEmissionButton.Name = "ChangeEmissionButton";
            this.ChangeEmissionButton.Size = new System.Drawing.Size(100, 23);
            this.ChangeEmissionButton.TabIndex = 70;
            this.ChangeEmissionButton.Text = "Змінити";
            this.ChangeEmissionButton.UseVisualStyleBackColor = true;
            this.ChangeEmissionButton.Click += new System.EventHandler(this.ChangeEmissionButton_Click);
            // 
            // DeleteEmissionButton
            // 
            this.DeleteEmissionButton.Enabled = false;
            this.DeleteEmissionButton.Location = new System.Drawing.Point(575, 103);
            this.DeleteEmissionButton.Name = "DeleteEmissionButton";
            this.DeleteEmissionButton.Size = new System.Drawing.Size(100, 23);
            this.DeleteEmissionButton.TabIndex = 69;
            this.DeleteEmissionButton.Text = "Видалити";
            this.DeleteEmissionButton.UseVisualStyleBackColor = true;
            this.DeleteEmissionButton.Click += new System.EventHandler(this.DeleteEmissionButton_Click);
            // 
            // CurrentEmisionGroupBox
            // 
            this.CurrentEmisionGroupBox.Controls.Add(this.EnvironmentsComboBox);
            this.CurrentEmisionGroupBox.Controls.Add(this.YearNumericUpDown);
            this.CurrentEmisionGroupBox.Controls.Add(this.label1);
            this.CurrentEmisionGroupBox.Controls.Add(this.MonthNumericUpDown);
            this.CurrentEmisionGroupBox.Controls.Add(this.label2);
            this.CurrentEmisionGroupBox.Controls.Add(this.DayNumericUpDown);
            this.CurrentEmisionGroupBox.Controls.Add(this.label3);
            this.CurrentEmisionGroupBox.Controls.Add(this.ElementsComboBox);
            this.CurrentEmisionGroupBox.Controls.Add(this.AvgValueTextBox);
            this.CurrentEmisionGroupBox.Controls.Add(this.MaxValueTextBox);
            this.CurrentEmisionGroupBox.Controls.Add(this.label5);
            this.CurrentEmisionGroupBox.Controls.Add(this.label13);
            this.CurrentEmisionGroupBox.Controls.Add(this.label12);
            this.CurrentEmisionGroupBox.Controls.Add(this.label4);
            this.CurrentEmisionGroupBox.Enabled = false;
            this.CurrentEmisionGroupBox.Location = new System.Drawing.Point(6, 6);
            this.CurrentEmisionGroupBox.Name = "CurrentEmisionGroupBox";
            this.CurrentEmisionGroupBox.Size = new System.Drawing.Size(563, 121);
            this.CurrentEmisionGroupBox.TabIndex = 68;
            this.CurrentEmisionGroupBox.TabStop = false;
            this.CurrentEmisionGroupBox.Text = "Поточний викид";
            // 
            // EnvironmentsComboBox
            // 
            this.EnvironmentsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EnvironmentsComboBox.FormattingEnabled = true;
            this.EnvironmentsComboBox.Location = new System.Drawing.Point(7, 37);
            this.EnvironmentsComboBox.Name = "EnvironmentsComboBox";
            this.EnvironmentsComboBox.Size = new System.Drawing.Size(150, 21);
            this.EnvironmentsComboBox.TabIndex = 61;
            this.EnvironmentsComboBox.SelectedIndexChanged += new System.EventHandler(this.EnvironmentsComboBox_SelectedIndexChanged);
            // 
            // YearNumericUpDown
            // 
            this.YearNumericUpDown.Location = new System.Drawing.Point(172, 37);
            this.YearNumericUpDown.Name = "YearNumericUpDown";
            this.YearNumericUpDown.Size = new System.Drawing.Size(100, 20);
            this.YearNumericUpDown.TabIndex = 65;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 39;
            this.label1.Text = "Елемент";
            // 
            // MonthNumericUpDown
            // 
            this.MonthNumericUpDown.Location = new System.Drawing.Point(278, 37);
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
            this.MonthNumericUpDown.TabIndex = 66;
            this.MonthNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(422, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 13);
            this.label2.TabIndex = 40;
            this.label2.Text = "Розмiр викиду середнiй";
            // 
            // DayNumericUpDown
            // 
            this.DayNumericUpDown.Location = new System.Drawing.Point(344, 37);
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
            this.DayNumericUpDown.TabIndex = 67;
            this.DayNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 13);
            this.label3.TabIndex = 41;
            this.label3.Text = "Середовище заруднення";
            // 
            // ElementsComboBox
            // 
            this.ElementsComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.ElementsComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ElementsComboBox.FormattingEnabled = true;
            this.ElementsComboBox.Location = new System.Drawing.Point(7, 84);
            this.ElementsComboBox.Name = "ElementsComboBox";
            this.ElementsComboBox.Size = new System.Drawing.Size(397, 21);
            this.ElementsComboBox.TabIndex = 63;
            this.ElementsComboBox.SelectedIndexChanged += new System.EventHandler(this.ElementsComboBox_SelectedIndexChanged);
            // 
            // AvgValueTextBox
            // 
            this.AvgValueTextBox.Location = new System.Drawing.Point(425, 37);
            this.AvgValueTextBox.Name = "AvgValueTextBox";
            this.AvgValueTextBox.Size = new System.Drawing.Size(124, 20);
            this.AvgValueTextBox.TabIndex = 44;
            this.AvgValueTextBox.Text = "0";
            this.AvgValueTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            // 
            // MaxValueTextBox
            // 
            this.MaxValueTextBox.Location = new System.Drawing.Point(425, 85);
            this.MaxValueTextBox.Name = "MaxValueTextBox";
            this.MaxValueTextBox.Size = new System.Drawing.Size(124, 20);
            this.MaxValueTextBox.TabIndex = 45;
            this.MaxValueTextBox.Text = "0";
            this.MaxValueTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(425, 66);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(129, 13);
            this.label5.TabIndex = 46;
            this.label5.Text = "Максимальний разовий";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(344, 21);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(34, 13);
            this.label13.TabIndex = 57;
            this.label13.Text = "День";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(169, 19);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(22, 13);
            this.label12.TabIndex = 55;
            this.label12.Text = "Рiк";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(275, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 56;
            this.label4.Text = "Мiсяць";
            // 
            // EmissionsDataGridView
            // 
            this.EmissionsDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.EmissionsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.EmissionsDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.EmissionsDataGridView.Location = new System.Drawing.Point(6, 133);
            this.EmissionsDataGridView.Name = "EmissionsDataGridView";
            this.EmissionsDataGridView.ReadOnly = true;
            this.EmissionsDataGridView.Size = new System.Drawing.Size(815, 287);
            this.EmissionsDataGridView.TabIndex = 20;
            this.EmissionsDataGridView.TabStop = false;
            this.EmissionsDataGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.EmissionsDataGridView_CellFormatting);
            this.EmissionsDataGridView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.EmissionsDataGridView_DataError);
            this.EmissionsDataGridView.SelectionChanged += new System.EventHandler(this.EmissionsDataGridView_SelectionChanged);
            // 
            // IssueTabPage
            // 
            this.IssueTabPage.Controls.Add(this.label8);
            this.IssueTabPage.Controls.Add(this.RefreshCalcResButton);
            this.IssueTabPage.Controls.Add(this.CalculationsResultDataGridView);
            this.IssueTabPage.Controls.Add(this.IssueGroupBox);
            this.IssueTabPage.Location = new System.Drawing.Point(4, 22);
            this.IssueTabPage.Name = "IssueTabPage";
            this.IssueTabPage.Size = new System.Drawing.Size(831, 428);
            this.IssueTabPage.TabIndex = 1;
            this.IssueTabPage.Text = "Задачі";
            this.IssueTabPage.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 137);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(169, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "Результати розрахунків по серії";
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
            this.CalculationsResultDataGridView.TabIndex = 21;
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
            this.IssueGroupBox.TabIndex = 11;
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
            // ContentContainerTabControl
            // 
            this.ContentContainerTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ContentContainerTabControl.Controls.Add(this.IssueTabPage);
            this.ContentContainerTabControl.Controls.Add(this.EmissionTabPage);
            this.ContentContainerTabControl.Location = new System.Drawing.Point(0, 0);
            this.ContentContainerTabControl.Name = "ContentContainerTabControl";
            this.ContentContainerTabControl.SelectedIndex = 0;
            this.ContentContainerTabControl.Size = new System.Drawing.Size(839, 454);
            this.ContentContainerTabControl.TabIndex = 0;
            this.ContentContainerTabControl.SelectedIndexChanged += new System.EventHandler(this.ContentContainerTabControl_TabIndexChanged);
            // 
            // MultiBindingObjectEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(839, 485);
            this.Controls.Add(this.RestoreButton);
            this.Controls.Add(this.SaveToBDButton);
            this.Controls.Add(this.ContentContainerTabControl);
            this.MinimumSize = new System.Drawing.Size(855, 400);
            this.Name = "MultiBindingObjectEditor";
            this.Text = "Додаткова інформація ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MultiBindingObjectEditor_FormClosing);
            this.Load += new System.EventHandler(this.MultiBindingObjectEditor_Load);
            this.EmissionTabPage.ResumeLayout(false);
            this.CurrentEmisionGroupBox.ResumeLayout(false);
            this.CurrentEmisionGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.YearNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MonthNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DayNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EmissionsDataGridView)).EndInit();
            this.IssueTabPage.ResumeLayout(false);
            this.IssueTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CalculationsResultDataGridView)).EndInit();
            this.IssueGroupBox.ResumeLayout(false);
            this.IssueGroupBox.PerformLayout();
            this.ContentContainerTabControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button SaveToBDButton;
        private System.Windows.Forms.Button RestoreButton;
        private System.Windows.Forms.ToolTip CalcResToolTip;
        private System.Windows.Forms.TabPage EmissionTabPage;
        private System.Windows.Forms.Button AddEmissionButton;
        private System.Windows.Forms.Button ChangeEmissionButton;
        private System.Windows.Forms.Button DeleteEmissionButton;
        private System.Windows.Forms.GroupBox CurrentEmisionGroupBox;
        private System.Windows.Forms.ComboBox EnvironmentsComboBox;
        private System.Windows.Forms.NumericUpDown YearNumericUpDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown MonthNumericUpDown;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown DayNumericUpDown;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox ElementsComboBox;
        private System.Windows.Forms.TextBox AvgValueTextBox;
        private System.Windows.Forms.TextBox MaxValueTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView EmissionsDataGridView;
        private System.Windows.Forms.TabPage IssueTabPage;
        private System.Windows.Forms.Button RefreshCalcResButton;
        private System.Windows.Forms.DataGridView CalculationsResultDataGridView;
        private System.Windows.Forms.GroupBox IssueGroupBox;
        private System.Windows.Forms.CheckedListBox SeriesCheckedListBox;
        private System.Windows.Forms.CheckedListBox IssuesCheckedListBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TabControl ContentContainerTabControl;
        private System.Windows.Forms.DataGridViewTextBoxColumn SeriesName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Formula;
        private System.Windows.Forms.DataGridViewTextBoxColumn Result;
        private System.Windows.Forms.DataGridViewTextBoxColumn FormulaDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.Label label8;
    }
}