
namespace Maps.HelpWindows
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
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.EnvironmentCheckBox = new System.Windows.Forms.CheckBox();
            this.EnvironmentGroupBox = new System.Windows.Forms.GroupBox();
            this.EnvironmentsComboBox = new System.Windows.Forms.ComboBox();
            this.m_AcceptButton = new System.Windows.Forms.Button();
            this.m_CancelButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.IssueGroupBox = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SeriesComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.IssuesComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ObjectDescriptionTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ObjectNameTextBox = new System.Windows.Forms.TextBox();
            this.IssueCheckBox = new System.Windows.Forms.CheckBox();
            this.EnvironmentGroupBox.SuspendLayout();
            this.IssueGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // EnvironmentCheckBox
            // 
            this.EnvironmentCheckBox.AutoSize = true;
            this.EnvironmentCheckBox.Location = new System.Drawing.Point(21, 134);
            this.EnvironmentCheckBox.Name = "EnvironmentCheckBox";
            this.EnvironmentCheckBox.Size = new System.Drawing.Size(158, 17);
            this.EnvironmentCheckBox.TabIndex = 0;
            this.EnvironmentCheckBox.Text = "Прив\'язка по середовищу";
            this.EnvironmentCheckBox.UseVisualStyleBackColor = true;
            this.EnvironmentCheckBox.CheckedChanged += new System.EventHandler(this.EnvironmentCheckBox_CheckedChanged);
            // 
            // EnvironmentGroupBox
            // 
            this.EnvironmentGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.EnvironmentGroupBox.Controls.Add(this.EnvironmentsComboBox);
            this.EnvironmentGroupBox.Location = new System.Drawing.Point(12, 136);
            this.EnvironmentGroupBox.Name = "EnvironmentGroupBox";
            this.EnvironmentGroupBox.Size = new System.Drawing.Size(292, 51);
            this.EnvironmentGroupBox.TabIndex = 2;
            this.EnvironmentGroupBox.TabStop = false;
            // 
            // EnvironmentsComboBox
            // 
            this.EnvironmentsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EnvironmentsComboBox.FormattingEnabled = true;
            this.EnvironmentsComboBox.Location = new System.Drawing.Point(6, 19);
            this.EnvironmentsComboBox.Name = "EnvironmentsComboBox";
            this.EnvironmentsComboBox.Size = new System.Drawing.Size(276, 21);
            this.EnvironmentsComboBox.TabIndex = 5;
            // 
            // m_AcceptButton
            // 
            this.m_AcceptButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_AcceptButton.Location = new System.Drawing.Point(73, 345);
            this.m_AcceptButton.Name = "m_AcceptButton";
            this.m_AcceptButton.Size = new System.Drawing.Size(80, 23);
            this.m_AcceptButton.TabIndex = 3;
            this.m_AcceptButton.Text = "Підтвердити";
            this.m_AcceptButton.UseVisualStyleBackColor = true;
            this.m_AcceptButton.Click += new System.EventHandler(this.m_AcceptButton_Click);
            // 
            // m_CancelButton
            // 
            this.m_CancelButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_CancelButton.Location = new System.Drawing.Point(159, 345);
            this.m_CancelButton.Name = "m_CancelButton";
            this.m_CancelButton.Size = new System.Drawing.Size(80, 23);
            this.m_CancelButton.TabIndex = 4;
            this.m_CancelButton.Text = "Відміна";
            this.m_CancelButton.UseVisualStyleBackColor = true;
            this.m_CancelButton.Click += new System.EventHandler(this.m_CancelButton_Click);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(9, 371);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(156, 17);
            this.label5.TabIndex = 9;
            this.label5.Text = "* - обов\'язкові поля";
            // 
            // IssueGroupBox
            // 
            this.IssueGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.IssueGroupBox.Controls.Add(this.label2);
            this.IssueGroupBox.Controls.Add(this.SeriesComboBox);
            this.IssueGroupBox.Controls.Add(this.label1);
            this.IssueGroupBox.Controls.Add(this.IssuesComboBox);
            this.IssueGroupBox.Location = new System.Drawing.Point(12, 12);
            this.IssueGroupBox.Name = "IssueGroupBox";
            this.IssueGroupBox.Size = new System.Drawing.Size(292, 118);
            this.IssueGroupBox.TabIndex = 10;
            this.IssueGroupBox.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Серія";
            // 
            // SeriesComboBox
            // 
            this.SeriesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SeriesComboBox.FormattingEnabled = true;
            this.SeriesComboBox.Location = new System.Drawing.Point(6, 86);
            this.SeriesComboBox.Name = "SeriesComboBox";
            this.SeriesComboBox.Size = new System.Drawing.Size(276, 21);
            this.SeriesComboBox.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Задача";
            // 
            // IssuesComboBox
            // 
            this.IssuesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.IssuesComboBox.FormattingEnabled = true;
            this.IssuesComboBox.Location = new System.Drawing.Point(6, 41);
            this.IssuesComboBox.Name = "IssuesComboBox";
            this.IssuesComboBox.Size = new System.Drawing.Size(276, 21);
            this.IssuesComboBox.TabIndex = 1;
            this.IssuesComboBox.SelectedIndexChanged += new System.EventHandler(this.IssuesComboBox_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 234);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Опис";
            // 
            // ObjectDescriptionTextBox
            // 
            this.ObjectDescriptionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ObjectDescriptionTextBox.Location = new System.Drawing.Point(18, 250);
            this.ObjectDescriptionTextBox.Multiline = true;
            this.ObjectDescriptionTextBox.Name = "ObjectDescriptionTextBox";
            this.ObjectDescriptionTextBox.Size = new System.Drawing.Size(275, 89);
            this.ObjectDescriptionTextBox.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 195);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Назва *";
            // 
            // ObjectNameTextBox
            // 
            this.ObjectNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ObjectNameTextBox.Location = new System.Drawing.Point(18, 211);
            this.ObjectNameTextBox.Name = "ObjectNameTextBox";
            this.ObjectNameTextBox.Size = new System.Drawing.Size(275, 20);
            this.ObjectNameTextBox.TabIndex = 5;
            // 
            // IssueCheckBox
            // 
            this.IssueCheckBox.AutoSize = true;
            this.IssueCheckBox.Location = new System.Drawing.Point(21, 11);
            this.IssueCheckBox.Name = "IssueCheckBox";
            this.IssueCheckBox.Size = new System.Drawing.Size(127, 17);
            this.IssueCheckBox.TabIndex = 11;
            this.IssueCheckBox.Text = "Прив\'язка по задачі";
            this.IssueCheckBox.UseVisualStyleBackColor = true;
            this.IssueCheckBox.CheckedChanged += new System.EventHandler(this.IssueCheckBox_CheckedChanged);
            // 
            // ItemConfigurationWindow
            // 
            this.AcceptButton = this.m_AcceptButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_CancelButton;
            this.ClientSize = new System.Drawing.Size(316, 397);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.EnvironmentCheckBox);
            this.Controls.Add(this.ObjectDescriptionTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.IssueCheckBox);
            this.Controls.Add(this.ObjectNameTextBox);
            this.Controls.Add(this.IssueGroupBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.m_CancelButton);
            this.Controls.Add(this.m_AcceptButton);
            this.Controls.Add(this.EnvironmentGroupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ItemConfigurationWindow";
            this.Text = "Налаштування маркеру";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ItemConfigurationWindow_FormClosing);
            this.Load += new System.EventHandler(this.ItemConfigurationWindow_Load);
            this.EnvironmentGroupBox.ResumeLayout(false);
            this.IssueGroupBox.ResumeLayout(false);
            this.IssueGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox EnvironmentCheckBox;
        private System.Windows.Forms.GroupBox EnvironmentGroupBox;
        private System.Windows.Forms.Button m_AcceptButton;
        private System.Windows.Forms.Button m_CancelButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox IssueGroupBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox ObjectDescriptionTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox SeriesComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox IssuesComboBox;
        private System.Windows.Forms.CheckBox IssueCheckBox;
        private System.Windows.Forms.ComboBox EnvironmentsComboBox;
        private System.Windows.Forms.TextBox ObjectNameTextBox;
    }
}