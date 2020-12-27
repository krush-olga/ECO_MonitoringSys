namespace Maps.OldMap
{
    partial class Calculations
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
            this.cbCalc = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbParams = new System.Windows.Forms.ComboBox();
            this.Confirm = new System.Windows.Forms.Button();
            this.Decline = new System.Windows.Forms.Button();
            this.cbIssues = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.name = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.description = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // cbCalc
            // 
            this.cbCalc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCalc.FormattingEnabled = true;
            this.cbCalc.Location = new System.Drawing.Point(48, 105);
            this.cbCalc.Name = "cbCalc";
            this.cbCalc.Size = new System.Drawing.Size(254, 21);
            this.cbCalc.TabIndex = 1;
            this.cbCalc.SelectedIndexChanged += new System.EventHandler(this.cbCalc_SelectedIndexChanged);
            this.cbCalc.TextChanged += new System.EventHandler(this.cbCalc_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Серію розрахунків";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 137);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Параметри";
            // 
            // cbParams
            // 
            this.cbParams.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbParams.Enabled = false;
            this.cbParams.FormattingEnabled = true;
            this.cbParams.Location = new System.Drawing.Point(48, 158);
            this.cbParams.Name = "cbParams";
            this.cbParams.Size = new System.Drawing.Size(254, 21);
            this.cbParams.TabIndex = 2;
            // 
            // Confirm
            // 
            this.Confirm.Enabled = false;
            this.Confirm.Location = new System.Drawing.Point(48, 349);
            this.Confirm.Name = "Confirm";
            this.Confirm.Size = new System.Drawing.Size(99, 23);
            this.Confirm.TabIndex = 3;
            this.Confirm.Text = "Підтвердити";
            this.Confirm.UseVisualStyleBackColor = true;
            this.Confirm.Click += new System.EventHandler(this.Confirm_Click);
            // 
            // Decline
            // 
            this.Decline.Location = new System.Drawing.Point(203, 349);
            this.Decline.Name = "Decline";
            this.Decline.Size = new System.Drawing.Size(99, 23);
            this.Decline.TabIndex = 4;
            this.Decline.Text = "Відмінити";
            this.Decline.UseVisualStyleBackColor = true;
            this.Decline.Click += new System.EventHandler(this.Decline_Click);
            // 
            // cbIssues
            // 
            this.cbIssues.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbIssues.Location = new System.Drawing.Point(48, 52);
            this.cbIssues.Name = "cbIssues";
            this.cbIssues.Size = new System.Drawing.Size(254, 21);
            this.cbIssues.TabIndex = 0;
            this.cbIssues.SelectedIndexChanged += new System.EventHandler(this.cbIssues_SelectedIndexChanged);
            this.cbIssues.TextChanged += new System.EventHandler(this.cbIssues_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(48, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Задача";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(45, 189);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Назва";
            // 
            // name
            // 
            this.name.Location = new System.Drawing.Point(48, 211);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(254, 20);
            this.name.TabIndex = 9;
            this.name.TextChanged += new System.EventHandler(this.name_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(45, 242);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Опис";
            // 
            // description
            // 
            this.description.Location = new System.Drawing.Point(48, 264);
            this.description.Multiline = true;
            this.description.Name = "description";
            this.description.Size = new System.Drawing.Size(254, 72);
            this.description.TabIndex = 11;
            // 
            // Calculations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 388);
            this.Controls.Add(this.description);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.name);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbIssues);
            this.Controls.Add(this.Decline);
            this.Controls.Add(this.Confirm);
            this.Controls.Add(this.cbParams);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbCalc);
            this.Name = "Calculations";
            this.Text = "Збереження об\'єкту";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbCalc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbParams;
        private System.Windows.Forms.Button Confirm;
        private System.Windows.Forms.Button Decline;
        private System.Windows.Forms.ComboBox cbIssues;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox description;
    }
}