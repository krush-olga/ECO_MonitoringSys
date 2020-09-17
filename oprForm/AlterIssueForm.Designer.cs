namespace oprForm
{
    partial class AlterIssueForm
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
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.addBtn = new System.Windows.Forms.Button();
            this.nameTB = new System.Windows.Forms.TextBox();
            this.descrTB = new System.Windows.Forms.TextBox();
            this.deleteBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 30;
            this.label4.Text = "Назва";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 29;
            this.label3.Text = "Опис";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(411, 255);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(109, 24);
            this.button1.TabIndex = 45;
            this.button1.Text = "Вiдмiнити";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // addBtn
            // 
            this.addBtn.Location = new System.Drawing.Point(56, 255);
            this.addBtn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.addBtn.Name = "addBtn";
            this.addBtn.Size = new System.Drawing.Size(109, 24);
            this.addBtn.TabIndex = 44;
            this.addBtn.Text = "Зберегти";
            this.addBtn.UseVisualStyleBackColor = true;
            this.addBtn.Click += new System.EventHandler(this.addBtn_Click);
            // 
            // nameTB
            // 
            this.nameTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nameTB.Location = new System.Drawing.Point(56, 38);
            this.nameTB.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.nameTB.Name = "nameTB";
            this.nameTB.Size = new System.Drawing.Size(465, 20);
            this.nameTB.TabIndex = 46;
            // 
            // descrTB
            // 
            this.descrTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.descrTB.Location = new System.Drawing.Point(56, 71);
            this.descrTB.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.descrTB.Multiline = true;
            this.descrTB.Name = "descrTB";
            this.descrTB.Size = new System.Drawing.Size(465, 171);
            this.descrTB.TabIndex = 47;
            // 
            // deleteBtn
            // 
            this.deleteBtn.Location = new System.Drawing.Point(232, 255);
            this.deleteBtn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.deleteBtn.Name = "deleteBtn";
            this.deleteBtn.Size = new System.Drawing.Size(110, 24);
            this.deleteBtn.TabIndex = 49;
            this.deleteBtn.Text = "Видалити";
            this.deleteBtn.UseVisualStyleBackColor = true;
            this.deleteBtn.Click += new System.EventHandler(this.deleteBtn_Click);
            // 
            // AlterIssueForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(559, 299);
            this.Controls.Add(this.deleteBtn);
            this.Controls.Add(this.descrTB);
            this.Controls.Add(this.nameTB);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.addBtn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "AlterIssueForm";
            this.Padding = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.Text = "Змiнити задачу";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button addBtn;
        private System.Windows.Forms.TextBox nameTB;
        private System.Windows.Forms.TextBox descrTB;
        private System.Windows.Forms.Button deleteBtn;
    }
}