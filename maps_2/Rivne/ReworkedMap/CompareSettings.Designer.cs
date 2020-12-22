namespace Maps
{
    partial class CompareSettings
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
            this.CompareButton = new System.Windows.Forms.Button();
            this.EmissionTabPage = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.ElementsСomboBox = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.EndDateDTPicker = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.StartDateDTPicker = new System.Windows.Forms.DateTimePicker();
            this.ComprasionByTabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.ComprasionObjectGroupBox = new System.Windows.Forms.GroupBox();
            this.DeleteObjectButton = new System.Windows.Forms.Button();
            this.CompareObjectsListBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.EmissionTabPage.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.ComprasionByTabControl.SuspendLayout();
            this.ComprasionObjectGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // CompareButton
            // 
            this.CompareButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CompareButton.Location = new System.Drawing.Point(184, 240);
            this.CompareButton.Name = "CompareButton";
            this.CompareButton.Size = new System.Drawing.Size(187, 23);
            this.CompareButton.TabIndex = 4;
            this.CompareButton.Text = "Порівняти";
            this.CompareButton.UseVisualStyleBackColor = true;
            this.CompareButton.Click += new System.EventHandler(this.CompareButton_Click);
            // 
            // EmissionTabPage
            // 
            this.EmissionTabPage.Controls.Add(this.label4);
            this.EmissionTabPage.Controls.Add(this.ElementsСomboBox);
            this.EmissionTabPage.Controls.Add(this.groupBox1);
            this.EmissionTabPage.Location = new System.Drawing.Point(4, 22);
            this.EmissionTabPage.Name = "EmissionTabPage";
            this.EmissionTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.EmissionTabPage.Size = new System.Drawing.Size(183, 182);
            this.EmissionTabPage.TabIndex = 0;
            this.EmissionTabPage.Text = "Викиди";
            this.EmissionTabPage.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(3, 119);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(170, 33);
            this.label4.TabIndex = 9;
            this.label4.Text = "Наявні елементи, за якими порівнювати";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ElementsСomboBox
            // 
            this.ElementsСomboBox.FormattingEnabled = true;
            this.ElementsСomboBox.Location = new System.Drawing.Point(6, 154);
            this.ElementsСomboBox.Name = "ElementsСomboBox";
            this.ElementsСomboBox.Size = new System.Drawing.Size(167, 21);
            this.ElementsСomboBox.TabIndex = 8;
            this.ElementsСomboBox.Text = "Загрузка данных";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.EndDateDTPicker);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.StartDateDTPicker);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(167, 110);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Період порівняння";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Кінець";
            // 
            // EndDateDTPicker
            // 
            this.EndDateDTPicker.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.EndDateDTPicker.Location = new System.Drawing.Point(6, 84);
            this.EndDateDTPicker.Name = "EndDateDTPicker";
            this.EndDateDTPicker.Size = new System.Drawing.Size(155, 20);
            this.EndDateDTPicker.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Початок";
            // 
            // StartDateDTPicker
            // 
            this.StartDateDTPicker.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.StartDateDTPicker.Location = new System.Drawing.Point(6, 36);
            this.StartDateDTPicker.Name = "StartDateDTPicker";
            this.StartDateDTPicker.Size = new System.Drawing.Size(155, 20);
            this.StartDateDTPicker.TabIndex = 0;
            // 
            // ComprasionByTabControl
            // 
            this.ComprasionByTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ComprasionByTabControl.Controls.Add(this.EmissionTabPage);
            this.ComprasionByTabControl.Controls.Add(this.tabPage1);
            this.ComprasionByTabControl.Location = new System.Drawing.Point(184, 29);
            this.ComprasionByTabControl.Name = "ComprasionByTabControl";
            this.ComprasionByTabControl.SelectedIndex = 0;
            this.ComprasionByTabControl.Size = new System.Drawing.Size(191, 208);
            this.ComprasionByTabControl.TabIndex = 7;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(183, 182);
            this.tabPage1.TabIndex = 1;
            this.tabPage1.Text = "Результати розрахунків";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // ComprasionObjectGroupBox
            // 
            this.ComprasionObjectGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ComprasionObjectGroupBox.Controls.Add(this.DeleteObjectButton);
            this.ComprasionObjectGroupBox.Controls.Add(this.CompareObjectsListBox);
            this.ComprasionObjectGroupBox.Location = new System.Drawing.Point(3, 12);
            this.ComprasionObjectGroupBox.Name = "ComprasionObjectGroupBox";
            this.ComprasionObjectGroupBox.Size = new System.Drawing.Size(179, 257);
            this.ComprasionObjectGroupBox.TabIndex = 8;
            this.ComprasionObjectGroupBox.TabStop = false;
            this.ComprasionObjectGroupBox.Text = "Об\'єкти для порівняння";
            // 
            // DeleteObjectButton
            // 
            this.DeleteObjectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DeleteObjectButton.Location = new System.Drawing.Point(6, 228);
            this.DeleteObjectButton.Name = "DeleteObjectButton";
            this.DeleteObjectButton.Size = new System.Drawing.Size(166, 23);
            this.DeleteObjectButton.TabIndex = 6;
            this.DeleteObjectButton.Text = "Вилити поточний";
            this.DeleteObjectButton.UseVisualStyleBackColor = true;
            this.DeleteObjectButton.Click += new System.EventHandler(this.DeleteObjectButton_Click);
            // 
            // CompareObjectsListBox
            // 
            this.CompareObjectsListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.CompareObjectsListBox.FormattingEnabled = true;
            this.CompareObjectsListBox.Location = new System.Drawing.Point(6, 17);
            this.CompareObjectsListBox.Name = "CompareObjectsListBox";
            this.CompareObjectsListBox.Size = new System.Drawing.Size(166, 199);
            this.CompareObjectsListBox.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(185, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Порівнювати по";
            // 
            // CompareSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(381, 281);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ComprasionObjectGroupBox);
            this.Controls.Add(this.ComprasionByTabControl);
            this.Controls.Add(this.CompareButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "CompareSettings";
            this.Text = "Налаштування перед порівнянням";
            this.EmissionTabPage.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ComprasionByTabControl.ResumeLayout(false);
            this.ComprasionObjectGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button CompareButton;
        private System.Windows.Forms.TabPage EmissionTabPage;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox ElementsСomboBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker EndDateDTPicker;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker StartDateDTPicker;
        private System.Windows.Forms.TabControl ComprasionByTabControl;
        private System.Windows.Forms.GroupBox ComprasionObjectGroupBox;
        private System.Windows.Forms.Button DeleteObjectButton;
        private System.Windows.Forms.ListBox CompareObjectsListBox;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label1;
    }
}