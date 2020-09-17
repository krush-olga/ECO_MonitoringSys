namespace oprForm
{
    partial class AddIssueForm
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
            this.addBtn = new System.Windows.Forms.Button();
            this.nameTB = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.descrTB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.issuesLB = new System.Windows.Forms.ListBox();
            this.button2 = new System.Windows.Forms.Button();
            this.TemaTB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // addBtn
            // 
            this.addBtn.Location = new System.Drawing.Point(73, 367);
            this.addBtn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.addBtn.Name = "addBtn";
            this.addBtn.Size = new System.Drawing.Size(109, 24);
            this.addBtn.TabIndex = 40;
            this.addBtn.Text = "Додати";
            this.addBtn.UseVisualStyleBackColor = true;
            this.addBtn.Click += new System.EventHandler(this.addBtn_Click);
            // 
            // nameTB
            // 
            this.nameTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nameTB.Location = new System.Drawing.Point(73, 37);
            this.nameTB.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.nameTB.Name = "nameTB";
            this.nameTB.Size = new System.Drawing.Size(481, 20);
            this.nameTB.TabIndex = 38;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 40);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 13);
            this.label7.TabIndex = 37;
            this.label7.Text = "Назва";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 69);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(33, 13);
            this.label8.TabIndex = 36;
            this.label8.Text = "Опис";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(274, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 13);
            this.label5.TabIndex = 35;
            this.label5.Text = "Нова Задача";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(295, 367);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(109, 24);
            this.button1.TabIndex = 43;
            this.button1.Text = "Вiдмiнити";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // descrTB
            // 
            this.descrTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.descrTB.Location = new System.Drawing.Point(73, 67);
            this.descrTB.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.descrTB.Multiline = true;
            this.descrTB.Name = "descrTB";
            this.descrTB.Size = new System.Drawing.Size(481, 249);
            this.descrTB.TabIndex = 39;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(586, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 45;
            this.label1.Text = "Список задач";
            // 
            // issuesLB
            // 
            this.issuesLB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.issuesLB.FormattingEnabled = true;
            this.issuesLB.HorizontalScrollbar = true;
            this.issuesLB.Location = new System.Drawing.Point(576, 37);
            this.issuesLB.Name = "issuesLB";
            this.issuesLB.Size = new System.Drawing.Size(110, 277);
            this.issuesLB.TabIndex = 44;
            this.issuesLB.SelectedIndexChanged += new System.EventHandler(this.issuesLB_SelectedIndexChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(505, 367);
            this.button2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(109, 24);
            this.button2.TabIndex = 46;
            this.button2.Text = "Змінити";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // TemaTB
            // 
            this.TemaTB.Location = new System.Drawing.Point(73, 333);
            this.TemaTB.Name = "TemaTB";
            this.TemaTB.Size = new System.Drawing.Size(481, 20);
            this.TemaTB.TabIndex = 47;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1, 336);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 48;
            this.label2.Text = "Тематика";
            // 
            // AddIssueForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(698, 416);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TemaTB);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.issuesLB);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.addBtn);
            this.Controls.Add(this.descrTB);
            this.Controls.Add(this.nameTB);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "AddIssueForm";
            this.Text = "Нова задача";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button addBtn;
        private System.Windows.Forms.TextBox nameTB;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox descrTB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox issuesLB;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox TemaTB;
        private System.Windows.Forms.Label label2;
    }
}