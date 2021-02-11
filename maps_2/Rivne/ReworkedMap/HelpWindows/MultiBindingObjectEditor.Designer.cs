
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
                base.Dispose(disposing);
            }
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ContentContainerTabControl = new System.Windows.Forms.TabControl();
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
            this.SaveToBDButton = new System.Windows.Forms.Button();
            this.RestoreEmissionsButton = new System.Windows.Forms.Button();
            this.ContentContainerTabControl.SuspendLayout();
            this.EmissionTabPage.SuspendLayout();
            this.CurrentEmisionGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.YearNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MonthNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DayNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EmissionsDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // ContentContainerTabControl
            // 
            this.ContentContainerTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ContentContainerTabControl.Controls.Add(this.EmissionTabPage);
            this.ContentContainerTabControl.Location = new System.Drawing.Point(0, 0);
            this.ContentContainerTabControl.Name = "ContentContainerTabControl";
            this.ContentContainerTabControl.SelectedIndex = 0;
            this.ContentContainerTabControl.Size = new System.Drawing.Size(839, 454);
            this.ContentContainerTabControl.TabIndex = 0;
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
            // 
            // MaxValueTextBox
            // 
            this.MaxValueTextBox.Location = new System.Drawing.Point(425, 85);
            this.MaxValueTextBox.Name = "MaxValueTextBox";
            this.MaxValueTextBox.Size = new System.Drawing.Size(124, 20);
            this.MaxValueTextBox.TabIndex = 45;
            this.MaxValueTextBox.Text = "0";
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
            // RestoreEmissionsButton
            // 
            this.RestoreEmissionsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.RestoreEmissionsButton.Enabled = false;
            this.RestoreEmissionsButton.Location = new System.Drawing.Point(487, 456);
            this.RestoreEmissionsButton.Name = "RestoreEmissionsButton";
            this.RestoreEmissionsButton.Size = new System.Drawing.Size(166, 23);
            this.RestoreEmissionsButton.TabIndex = 73;
            this.RestoreEmissionsButton.Text = "Відмінити усі дії";
            this.RestoreEmissionsButton.UseVisualStyleBackColor = true;
            this.RestoreEmissionsButton.Click += new System.EventHandler(this.RestoreEmissionsButton_Click);
            // 
            // MultiBindingObjectEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(839, 485);
            this.Controls.Add(this.RestoreEmissionsButton);
            this.Controls.Add(this.SaveToBDButton);
            this.Controls.Add(this.ContentContainerTabControl);
            this.MinimumSize = new System.Drawing.Size(855, 400);
            this.Name = "MultiBindingObjectEditor";
            this.Text = "Додаткова інформація ";
            this.Load += new System.EventHandler(this.MultiBindingObjectEditor_Load);
            this.ContentContainerTabControl.ResumeLayout(false);
            this.EmissionTabPage.ResumeLayout(false);
            this.CurrentEmisionGroupBox.ResumeLayout(false);
            this.CurrentEmisionGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.YearNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MonthNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DayNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EmissionsDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl ContentContainerTabControl;
        private System.Windows.Forms.TabPage EmissionTabPage;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox MaxValueTextBox;
        private System.Windows.Forms.TextBox AvgValueTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox EnvironmentsComboBox;
        private System.Windows.Forms.ComboBox ElementsComboBox;
        private System.Windows.Forms.DataGridView EmissionsDataGridView;
        private System.Windows.Forms.NumericUpDown YearNumericUpDown;
        private System.Windows.Forms.NumericUpDown MonthNumericUpDown;
        private System.Windows.Forms.NumericUpDown DayNumericUpDown;
        private System.Windows.Forms.GroupBox CurrentEmisionGroupBox;
        private System.Windows.Forms.Button AddEmissionButton;
        private System.Windows.Forms.Button ChangeEmissionButton;
        private System.Windows.Forms.Button DeleteEmissionButton;
        private System.Windows.Forms.Button SaveToBDButton;
        private System.Windows.Forms.Button RestoreEmissionsButton;
    }
}