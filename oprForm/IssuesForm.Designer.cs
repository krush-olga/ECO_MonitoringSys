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
            this.label3 = new System.Windows.Forms.Label();
            this.descrLbl = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nameLbl = new System.Windows.Forms.Label();
            this.addBtn = new System.Windows.Forms.Button();
            this.alterBtn = new System.Windows.Forms.Button();
            this.dateLbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // issuesLB
            // 
            this.issuesLB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.issuesLB.FormattingEnabled = true;
            this.issuesLB.HorizontalScrollbar = true;
            this.issuesLB.Location = new System.Drawing.Point(9, 28);
            this.issuesLB.Name = "issuesLB";
            this.issuesLB.Size = new System.Drawing.Size(370, 342);
            this.issuesLB.TabIndex = 14;
            this.issuesLB.SelectedIndexChanged += new System.EventHandler(this.issuesLB_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Список";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(413, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Опис";
            // 
            // descrLbl
            // 
            this.descrLbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.descrLbl.AutoSize = true;
            this.descrLbl.Location = new System.Drawing.Point(465, 73);
            this.descrLbl.Name = "descrLbl";
            this.descrLbl.Size = new System.Drawing.Size(0, 13);
            this.descrLbl.TabIndex = 20;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(413, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 13);
            this.label4.TabIndex = 21;
            this.label4.Text = "Ім\'я";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(411, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Дата";
            // 
            // nameLbl
            // 
            this.nameLbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nameLbl.AutoSize = true;
            this.nameLbl.Location = new System.Drawing.Point(465, 28);
            this.nameLbl.Name = "nameLbl";
            this.nameLbl.Size = new System.Drawing.Size(0, 13);
            this.nameLbl.TabIndex = 23;
            // 
            // addBtn
            // 
            this.addBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.addBtn.AutoSize = true;
            this.addBtn.Location = new System.Drawing.Point(12, 401);
            this.addBtn.Margin = new System.Windows.Forms.Padding(2);
            this.addBtn.Name = "addBtn";
            this.addBtn.Size = new System.Drawing.Size(64, 34);
            this.addBtn.TabIndex = 25;
            this.addBtn.Text = "Додати";
            this.addBtn.UseVisualStyleBackColor = true;
            this.addBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // alterBtn
            // 
            this.alterBtn.Location = new System.Drawing.Point(97, 401);
            this.alterBtn.Margin = new System.Windows.Forms.Padding(2);
            this.alterBtn.Name = "alterBtn";
            this.alterBtn.Size = new System.Drawing.Size(64, 34);
            this.alterBtn.TabIndex = 26;
            this.alterBtn.Text = "Змiнити";
            this.alterBtn.UseVisualStyleBackColor = true;
            this.alterBtn.Visible = false;
            this.alterBtn.Click += new System.EventHandler(this.button2_Click);
            // 
            // dateLbl
            // 
            this.dateLbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dateLbl.AutoSize = true;
            this.dateLbl.Location = new System.Drawing.Point(463, 50);
            this.dateLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.dateLbl.Name = "dateLbl";
            this.dateLbl.Size = new System.Drawing.Size(0, 13);
            this.dateLbl.TabIndex = 29;
            // 
            // IssuesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(728, 448);
            this.Controls.Add(this.dateLbl);
            this.Controls.Add(this.alterBtn);
            this.Controls.Add(this.addBtn);
            this.Controls.Add(this.nameLbl);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.descrLbl);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.issuesLB);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "IssuesForm";
            this.Text = "Задачі";
            this.Load += new System.EventHandler(this.IssuesForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox issuesLB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label descrLbl;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label nameLbl;
        private System.Windows.Forms.Button addBtn;
        private System.Windows.Forms.Button alterBtn;
        private System.Windows.Forms.Label dateLbl;
    }
}