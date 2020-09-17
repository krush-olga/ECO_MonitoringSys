namespace oprForm
{
    partial class IssuesForm
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
            this.issuesLB = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.eventDescLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.descrLbl = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nameLbl = new System.Windows.Forms.Label();
            this.dateLbl = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.nameTB = new System.Windows.Forms.TextBox();
            this.descrTB = new System.Windows.Forms.TextBox();
            this.addBtn = new System.Windows.Forms.Button();
            this.seriesTB = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // issuesLB
            // 
            this.issuesLB.FormattingEnabled = true;
            this.issuesLB.ItemHeight = 20;
            this.issuesLB.Location = new System.Drawing.Point(13, 43);
            this.issuesLB.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.issuesLB.Name = "issuesLB";
            this.issuesLB.Size = new System.Drawing.Size(164, 404);
            this.issuesLB.TabIndex = 14;
            this.issuesLB.SelectedIndexChanged += new System.EventHandler(this.issuesLB_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 7);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 20);
            this.label1.TabIndex = 15;
            this.label1.Text = "Список";
            // 
            // eventDescLabel
            // 
            this.eventDescLabel.AutoSize = true;
            this.eventDescLabel.Location = new System.Drawing.Point(141, 454);
            this.eventDescLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.eventDescLabel.Name = "eventDescLabel";
            this.eventDescLabel.Size = new System.Drawing.Size(0, 20);
            this.eventDescLabel.TabIndex = 18;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(199, 88);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 20);
            this.label3.TabIndex = 19;
            this.label3.Text = "Опис";
            // 
            // descrLbl
            // 
            this.descrLbl.AutoSize = true;
            this.descrLbl.Location = new System.Drawing.Point(301, 88);
            this.descrLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.descrLbl.Name = "descrLbl";
            this.descrLbl.Size = new System.Drawing.Size(0, 20);
            this.descrLbl.TabIndex = 20;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(199, 43);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 20);
            this.label4.TabIndex = 21;
            this.label4.Text = "Имя";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(199, 136);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 20);
            this.label2.TabIndex = 22;
            this.label2.Text = "Дата";
            // 
            // nameLbl
            // 
            this.nameLbl.AutoSize = true;
            this.nameLbl.Location = new System.Drawing.Point(301, 43);
            this.nameLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.nameLbl.Name = "nameLbl";
            this.nameLbl.Size = new System.Drawing.Size(0, 20);
            this.nameLbl.TabIndex = 23;
            // 
            // dateLbl
            // 
            this.dateLbl.AutoSize = true;
            this.dateLbl.Location = new System.Drawing.Point(301, 136);
            this.dateLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.dateLbl.Name = "dateLbl";
            this.dateLbl.Size = new System.Drawing.Size(0, 20);
            this.dateLbl.TabIndex = 24;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(355, 223);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(130, 20);
            this.label5.TabIndex = 25;
            this.label5.Text = "Нова Проблема";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(199, 312);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 20);
            this.label8.TabIndex = 26;
            this.label8.Text = "Опис";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(199, 267);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(40, 20);
            this.label7.TabIndex = 27;
            this.label7.Text = "Имя";
            // 
            // nameTB
            // 
            this.nameTB.Location = new System.Drawing.Point(305, 261);
            this.nameTB.Name = "nameTB";
            this.nameTB.Size = new System.Drawing.Size(178, 26);
            this.nameTB.TabIndex = 29;
            // 
            // descrTB
            // 
            this.descrTB.Location = new System.Drawing.Point(305, 306);
            this.descrTB.Name = "descrTB";
            this.descrTB.Size = new System.Drawing.Size(178, 26);
            this.descrTB.TabIndex = 30;
            // 
            // addBtn
            // 
            this.addBtn.Location = new System.Drawing.Point(359, 397);
            this.addBtn.Name = "addBtn";
            this.addBtn.Size = new System.Drawing.Size(98, 50);
            this.addBtn.TabIndex = 32;
            this.addBtn.Text = "Додати";
            this.addBtn.UseVisualStyleBackColor = true;
            this.addBtn.Click += new System.EventHandler(this.addBtn_Click);
            // 
            // seriesTB
            // 
            this.seriesTB.Location = new System.Drawing.Point(305, 351);
            this.seriesTB.Name = "seriesTB";
            this.seriesTB.Size = new System.Drawing.Size(178, 26);
            this.seriesTB.TabIndex = 34;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(199, 357);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 20);
            this.label6.TabIndex = 33;
            this.label6.Text = "Cерiя";
            // 
            // IssuesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(648, 497);
            this.Controls.Add(this.seriesTB);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.addBtn);
            this.Controls.Add(this.descrTB);
            this.Controls.Add(this.nameTB);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dateLbl);
            this.Controls.Add(this.nameLbl);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.descrLbl);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.eventDescLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.issuesLB);
            this.Name = "IssuesForm";
            this.Text = "IssuesForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox issuesLB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label eventDescLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label descrLbl;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label nameLbl;
        private System.Windows.Forms.Label dateLbl;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox nameTB;
        private System.Windows.Forms.TextBox descrTB;
        private System.Windows.Forms.Button addBtn;
        private System.Windows.Forms.TextBox seriesTB;
        private System.Windows.Forms.Label label6;
    }
}