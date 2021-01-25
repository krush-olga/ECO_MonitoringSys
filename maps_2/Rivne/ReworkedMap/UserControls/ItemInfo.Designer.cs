
namespace Maps.UserControls
{
    partial class ItemInfo
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
            this.GeneralInfoGroupBox = new System.Windows.Forms.GroupBox();
            this.ExpertLabel = new System.Windows.Forms.Label();
            this.CreatorNameLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.DescriptionTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ObjectTypeLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.NameTextBox = new System.Windows.Forms.TextBox();
            this.ChangeButton = new System.Windows.Forms.Button();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.HideButton = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.GeneralInfoGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // GeneralInfoGroupBox
            // 
            this.GeneralInfoGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GeneralInfoGroupBox.Controls.Add(this.ExpertLabel);
            this.GeneralInfoGroupBox.Controls.Add(this.CreatorNameLabel);
            this.GeneralInfoGroupBox.Controls.Add(this.label5);
            this.GeneralInfoGroupBox.Controls.Add(this.label4);
            this.GeneralInfoGroupBox.Controls.Add(this.label3);
            this.GeneralInfoGroupBox.Controls.Add(this.DescriptionTextBox);
            this.GeneralInfoGroupBox.Controls.Add(this.label2);
            this.GeneralInfoGroupBox.Controls.Add(this.ObjectTypeLabel);
            this.GeneralInfoGroupBox.Controls.Add(this.label1);
            this.GeneralInfoGroupBox.Controls.Add(this.NameTextBox);
            this.GeneralInfoGroupBox.Location = new System.Drawing.Point(3, 28);
            this.GeneralInfoGroupBox.Name = "GeneralInfoGroupBox";
            this.GeneralInfoGroupBox.Size = new System.Drawing.Size(262, 205);
            this.GeneralInfoGroupBox.TabIndex = 0;
            this.GeneralInfoGroupBox.TabStop = false;
            this.GeneralInfoGroupBox.Text = "Головна інформація";
            // 
            // ExpertLabel
            // 
            this.ExpertLabel.AutoSize = true;
            this.ExpertLabel.Location = new System.Drawing.Point(62, 181);
            this.ExpertLabel.Name = "ExpertLabel";
            this.ExpertLabel.Size = new System.Drawing.Size(37, 13);
            this.ExpertLabel.TabIndex = 9;
            this.ExpertLabel.Text = "Expert";
            // 
            // CreatorNameLabel
            // 
            this.CreatorNameLabel.AutoSize = true;
            this.CreatorNameLabel.Location = new System.Drawing.Point(97, 162);
            this.CreatorNameLabel.Name = "CreatorNameLabel";
            this.CreatorNameLabel.Size = new System.Drawing.Size(69, 13);
            this.CreatorNameLabel.TabIndex = 8;
            this.CreatorNameLabel.Text = "CreatorName";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 181);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Експерт:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 162);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Ким створенний:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Опис";
            // 
            // DescriptionTextBox
            // 
            this.DescriptionTextBox.AcceptsReturn = true;
            this.DescriptionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DescriptionTextBox.Location = new System.Drawing.Point(6, 91);
            this.DescriptionTextBox.Multiline = true;
            this.DescriptionTextBox.Name = "DescriptionTextBox";
            this.DescriptionTextBox.ReadOnly = true;
            this.DescriptionTextBox.Size = new System.Drawing.Size(250, 65);
            this.DescriptionTextBox.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Назва";
            // 
            // ObjectTypeLabel
            // 
            this.ObjectTypeLabel.AutoSize = true;
            this.ObjectTypeLabel.Location = new System.Drawing.Point(73, 16);
            this.ObjectTypeLabel.Name = "ObjectTypeLabel";
            this.ObjectTypeLabel.Size = new System.Drawing.Size(62, 13);
            this.ObjectTypeLabel.TabIndex = 2;
            this.ObjectTypeLabel.Text = "ObjectType";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Тип об\'єкту: ";
            // 
            // NameTextBox
            // 
            this.NameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.NameTextBox.Location = new System.Drawing.Point(6, 50);
            this.NameTextBox.Name = "NameTextBox";
            this.NameTextBox.ReadOnly = true;
            this.NameTextBox.Size = new System.Drawing.Size(250, 20);
            this.NameTextBox.TabIndex = 0;
            // 
            // ChangeButton
            // 
            this.ChangeButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.ChangeButton.Enabled = false;
            this.ChangeButton.Location = new System.Drawing.Point(32, 237);
            this.ChangeButton.Name = "ChangeButton";
            this.ChangeButton.Size = new System.Drawing.Size(75, 23);
            this.ChangeButton.TabIndex = 1;
            this.ChangeButton.Text = "Змінити";
            this.ChangeButton.UseVisualStyleBackColor = true;
            // 
            // DeleteButton
            // 
            this.DeleteButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.DeleteButton.Location = new System.Drawing.Point(148, 237);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(75, 23);
            this.DeleteButton.TabIndex = 2;
            this.DeleteButton.Text = "Видалити";
            this.DeleteButton.UseVisualStyleBackColor = true;
            // 
            // HideButton
            // 
            this.HideButton.BackColor = System.Drawing.SystemColors.Window;
            this.HideButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.HideButton.Location = new System.Drawing.Point(3, 3);
            this.HideButton.Name = "HideButton";
            this.HideButton.Size = new System.Drawing.Size(23, 22);
            this.HideButton.TabIndex = 4;
            this.HideButton.Text = ">";
            this.HideButton.UseVisualStyleBackColor = false;
            this.HideButton.Click += new System.EventHandler(this.HideButton_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(29, 6);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(236, 15);
            this.label6.TabIndex = 5;
            this.label6.Text = "Додаткова інформація про об\'єкт";
            // 
            // ItemInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label6);
            this.Controls.Add(this.HideButton);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.ChangeButton);
            this.Controls.Add(this.GeneralInfoGroupBox);
            this.Name = "ItemInfo";
            this.Size = new System.Drawing.Size(268, 263);
            this.GeneralInfoGroupBox.ResumeLayout(false);
            this.GeneralInfoGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox GeneralInfoGroupBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ChangeButton;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Label ExpertLabel;
        private System.Windows.Forms.TextBox DescriptionTextBox;
        private System.Windows.Forms.Label ObjectTypeLabel;
        private System.Windows.Forms.TextBox NameTextBox;
        private System.Windows.Forms.Label CreatorNameLabel;
        private System.Windows.Forms.Button HideButton;
        private System.Windows.Forms.Label label6;
    }
}
