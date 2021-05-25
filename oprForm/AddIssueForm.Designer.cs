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
			this.components = new System.ComponentModel.Container();
			this.addBtn = new System.Windows.Forms.Button();
			this.nameTB = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.descrTB = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.issuesLB = new System.Windows.Forms.ListBox();
			this.button2 = new System.Windows.Forms.Button();
			this.TemaTB = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.bDelete = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.cmbBxTema = new System.Windows.Forms.ComboBox();
			this.toolTipTema = new System.Windows.Forms.ToolTip(this.components);
			this.SuspendLayout();
			// 
			// addBtn
			// 
			this.addBtn.Location = new System.Drawing.Point(325, 225);
			this.addBtn.Margin = new System.Windows.Forms.Padding(2);
			this.addBtn.Name = "addBtn";
			this.addBtn.Size = new System.Drawing.Size(90, 25);
			this.addBtn.TabIndex = 40;
			this.addBtn.Text = "Додати";
			this.addBtn.UseVisualStyleBackColor = true;
			this.addBtn.Click += new System.EventHandler(this.addBtn_Click);
			// 
			// nameTB
			// 
			this.nameTB.Location = new System.Drawing.Point(325, 25);
			this.nameTB.Margin = new System.Windows.Forms.Padding(2);
			this.nameTB.Name = "nameTB";
			this.nameTB.Size = new System.Drawing.Size(323, 20);
			this.nameTB.TabIndex = 38;
			this.nameTB.Click += new System.EventHandler(this.nameTB_Click);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(322, 9);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(39, 13);
			this.label7.TabIndex = 37;
			this.label7.Text = "Назва";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(322, 56);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(33, 13);
			this.label8.TabIndex = 36;
			this.label8.Text = "Опис";
			// 
			// descrTB
			// 
			this.descrTB.Location = new System.Drawing.Point(325, 71);
			this.descrTB.Margin = new System.Windows.Forms.Padding(2);
			this.descrTB.Multiline = true;
			this.descrTB.Name = "descrTB";
			this.descrTB.Size = new System.Drawing.Size(323, 91);
			this.descrTB.TabIndex = 39;
			this.descrTB.Click += new System.EventHandler(this.descrTB_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(79, 13);
			this.label1.TabIndex = 45;
			this.label1.Text = "Перелік задач";
			// 
			// issuesLB
			// 
			this.issuesLB.FormattingEnabled = true;
			this.issuesLB.HorizontalScrollbar = true;
			this.issuesLB.Location = new System.Drawing.Point(15, 25);
			this.issuesLB.Name = "issuesLB";
			this.issuesLB.Size = new System.Drawing.Size(300, 225);
			this.issuesLB.TabIndex = 44;
			this.issuesLB.SelectedIndexChanged += new System.EventHandler(this.issuesLB_SelectedIndexChanged);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(441, 225);
			this.button2.Margin = new System.Windows.Forms.Padding(2);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(90, 25);
			this.button2.TabIndex = 46;
			this.button2.Text = "Змінити";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// TemaTB
			// 
			this.TemaTB.Location = new System.Drawing.Point(323, 191);
			this.TemaTB.Name = "TemaTB";
			this.TemaTB.Size = new System.Drawing.Size(301, 20);
			this.TemaTB.TabIndex = 47;
			this.TemaTB.Visible = false;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(322, 175);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(57, 13);
			this.label2.TabIndex = 48;
			this.label2.Text = "Тематика";
			// 
			// bDelete
			// 
			this.bDelete.Location = new System.Drawing.Point(558, 225);
			this.bDelete.Margin = new System.Windows.Forms.Padding(2);
			this.bDelete.Name = "bDelete";
			this.bDelete.Size = new System.Drawing.Size(90, 25);
			this.bDelete.TabIndex = 49;
			this.bDelete.Text = "Видалити";
			this.bDelete.UseVisualStyleBackColor = true;
			this.bDelete.Click += new System.EventHandler(this.bDelete_Click);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(625, 189);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(23, 23);
			this.button1.TabIndex = 50;
			this.button1.Text = "+";
			this.toolTipTema.SetToolTip(this.button1, "Для додання нової тематики:\r\n1) введіть нову назву у полі випадаючого списку;\r\n2)" +
        " натисніть на кнопку \"+\"");
			this.button1.UseVisualStyleBackColor = true;
			// 
			// cmbBxTema
			// 
			this.cmbBxTema.FormattingEnabled = true;
			this.cmbBxTema.Location = new System.Drawing.Point(321, 190);
			this.cmbBxTema.Name = "cmbBxTema";
			this.cmbBxTema.Size = new System.Drawing.Size(301, 21);
			this.cmbBxTema.TabIndex = 51;
			this.cmbBxTema.Click += new System.EventHandler(this.cmbBxTema_Click);
			// 
			// AddIssueForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(661, 261);
			this.Controls.Add(this.cmbBxTema);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.bDelete);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.TemaTB);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.issuesLB);
			this.Controls.Add(this.addBtn);
			this.Controls.Add(this.descrTB);
			this.Controls.Add(this.nameTB);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label8);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Margin = new System.Windows.Forms.Padding(2);
			this.Name = "AddIssueForm";
			this.Text = "Редагування задач";
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button addBtn;
        private System.Windows.Forms.TextBox nameTB;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox descrTB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox issuesLB;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox TemaTB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button bDelete;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cmbBxTema;
        private System.Windows.Forms.ToolTip toolTipTema;
    }
}