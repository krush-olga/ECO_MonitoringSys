
namespace UserMap.UserControls
{
    partial class MainMarkerInfoUC
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
            this.EconomicActivityGroupBox = new System.Windows.Forms.GroupBox();
            this.NewEALabel = new System.Windows.Forms.Label();
            this.PreviouesEALabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.EconomicActivityComboBox = new System.Windows.Forms.ComboBox();
            this.OwnerShipGroupBox = new System.Windows.Forms.GroupBox();
            this.NewOwnershipLabel = new System.Windows.Forms.Label();
            this.PreviousOwnershipLabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.OwnerShipComboBox = new System.Windows.Forms.ComboBox();
            this.EconomicActivityGroupBox.SuspendLayout();
            this.OwnerShipGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // EconomicActivityGroupBox
            // 
            this.EconomicActivityGroupBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.EconomicActivityGroupBox.Controls.Add(this.NewEALabel);
            this.EconomicActivityGroupBox.Controls.Add(this.PreviouesEALabel);
            this.EconomicActivityGroupBox.Controls.Add(this.label4);
            this.EconomicActivityGroupBox.Controls.Add(this.label2);
            this.EconomicActivityGroupBox.Controls.Add(this.label1);
            this.EconomicActivityGroupBox.Controls.Add(this.EconomicActivityComboBox);
            this.EconomicActivityGroupBox.Location = new System.Drawing.Point(3, 3);
            this.EconomicActivityGroupBox.Name = "EconomicActivityGroupBox";
            this.EconomicActivityGroupBox.Size = new System.Drawing.Size(281, 144);
            this.EconomicActivityGroupBox.TabIndex = 0;
            this.EconomicActivityGroupBox.TabStop = false;
            this.EconomicActivityGroupBox.Text = "Вид економічної діяльності";
            // 
            // NewEALabel
            // 
            this.NewEALabel.AutoSize = true;
            this.NewEALabel.Location = new System.Drawing.Point(7, 119);
            this.NewEALabel.Name = "NewEALabel";
            this.NewEALabel.Size = new System.Drawing.Size(10, 13);
            this.NewEALabel.TabIndex = 2;
            this.NewEALabel.Text = "-";
            // 
            // PreviouesEALabel
            // 
            this.PreviouesEALabel.AutoSize = true;
            this.PreviouesEALabel.Location = new System.Drawing.Point(7, 83);
            this.PreviouesEALabel.Name = "PreviouesEALabel";
            this.PreviouesEALabel.Size = new System.Drawing.Size(16, 13);
            this.PreviouesEALabel.TabIndex = 2;
            this.PreviouesEALabel.Text = "...";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(180, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Новий вид економічної діяльності:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(192, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Минулий вид економічної діяльності:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(190, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Список видів економічної діяльності";
            // 
            // EconomicActivityComboBox
            // 
            this.EconomicActivityComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.EconomicActivityComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.EconomicActivityComboBox.FormattingEnabled = true;
            this.EconomicActivityComboBox.Location = new System.Drawing.Point(9, 37);
            this.EconomicActivityComboBox.Name = "EconomicActivityComboBox";
            this.EconomicActivityComboBox.Size = new System.Drawing.Size(266, 21);
            this.EconomicActivityComboBox.TabIndex = 0;
            this.EconomicActivityComboBox.Tag = "1";
            this.EconomicActivityComboBox.SelectedIndexChanged += new System.EventHandler(this.ComboBox_SelectedIndexChanged);
            // 
            // OwnerShipGroupBox
            // 
            this.OwnerShipGroupBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.OwnerShipGroupBox.Controls.Add(this.NewOwnershipLabel);
            this.OwnerShipGroupBox.Controls.Add(this.PreviousOwnershipLabel);
            this.OwnerShipGroupBox.Controls.Add(this.label6);
            this.OwnerShipGroupBox.Controls.Add(this.label7);
            this.OwnerShipGroupBox.Controls.Add(this.label8);
            this.OwnerShipGroupBox.Controls.Add(this.OwnerShipComboBox);
            this.OwnerShipGroupBox.Location = new System.Drawing.Point(290, 3);
            this.OwnerShipGroupBox.Name = "OwnerShipGroupBox";
            this.OwnerShipGroupBox.Size = new System.Drawing.Size(281, 144);
            this.OwnerShipGroupBox.TabIndex = 0;
            this.OwnerShipGroupBox.TabStop = false;
            this.OwnerShipGroupBox.Text = "Форма власності";
            // 
            // NewOwnershipLabel
            // 
            this.NewOwnershipLabel.AutoSize = true;
            this.NewOwnershipLabel.Location = new System.Drawing.Point(7, 119);
            this.NewOwnershipLabel.Name = "NewOwnershipLabel";
            this.NewOwnershipLabel.Size = new System.Drawing.Size(10, 13);
            this.NewOwnershipLabel.TabIndex = 2;
            this.NewOwnershipLabel.Text = "-";
            // 
            // PreviousOwnershipLabel
            // 
            this.PreviousOwnershipLabel.AutoSize = true;
            this.PreviousOwnershipLabel.Location = new System.Drawing.Point(7, 83);
            this.PreviousOwnershipLabel.Name = "PreviousOwnershipLabel";
            this.PreviousOwnershipLabel.Size = new System.Drawing.Size(16, 13);
            this.PreviousOwnershipLabel.TabIndex = 2;
            this.PreviousOwnershipLabel.Text = "...";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 106);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(180, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Новий вид економічної діяльності:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 70);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(192, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Минулий вид економічної діяльності:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 21);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(190, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "Список видів економічної діяльності";
            // 
            // OwnerShipComboBox
            // 
            this.OwnerShipComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.OwnerShipComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.OwnerShipComboBox.FormattingEnabled = true;
            this.OwnerShipComboBox.Location = new System.Drawing.Point(9, 37);
            this.OwnerShipComboBox.Name = "OwnerShipComboBox";
            this.OwnerShipComboBox.Size = new System.Drawing.Size(266, 21);
            this.OwnerShipComboBox.TabIndex = 0;
            this.OwnerShipComboBox.Tag = "2";
            this.OwnerShipComboBox.SelectedIndexChanged += new System.EventHandler(this.ComboBox_SelectedIndexChanged);
            // 
            // MainMarkerInfoUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.OwnerShipGroupBox);
            this.Controls.Add(this.EconomicActivityGroupBox);
            this.Name = "MainMarkerInfoUC";
            this.Size = new System.Drawing.Size(585, 153);
            this.Load += new System.EventHandler(this.MainMarkerInfoUC_Load);
            this.EconomicActivityGroupBox.ResumeLayout(false);
            this.EconomicActivityGroupBox.PerformLayout();
            this.OwnerShipGroupBox.ResumeLayout(false);
            this.OwnerShipGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox EconomicActivityGroupBox;
        private System.Windows.Forms.ComboBox EconomicActivityComboBox;
        private System.Windows.Forms.Label NewEALabel;
        private System.Windows.Forms.Label PreviouesEALabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox OwnerShipGroupBox;
        private System.Windows.Forms.Label NewOwnershipLabel;
        private System.Windows.Forms.Label PreviousOwnershipLabel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox OwnerShipComboBox;
    }
}
