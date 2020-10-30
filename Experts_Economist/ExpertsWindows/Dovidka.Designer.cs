namespace Experts_Economist
{
    partial class Dovidka
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
            this.gdkDataGrid = new System.Windows.Forms.DataGridView();
            this.workMode = new System.Windows.Forms.GroupBox();
            this.deleteRadio = new System.Windows.Forms.RadioButton();
            this.editRadio = new System.Windows.Forms.RadioButton();
            this.addRadio = new System.Windows.Forms.RadioButton();
            this.searchRadio = new System.Windows.Forms.RadioButton();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.doneBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gdkDataGrid)).BeginInit();
            this.workMode.SuspendLayout();
            this.SuspendLayout();
            // 
            // gdkDataGrid
            // 
            this.gdkDataGrid.AllowUserToAddRows = false;
            this.gdkDataGrid.AllowUserToDeleteRows = false;
            this.gdkDataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gdkDataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gdkDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gdkDataGrid.Location = new System.Drawing.Point(12, 196);
            this.gdkDataGrid.Name = "gdkDataGrid";
            this.gdkDataGrid.ReadOnly = true;
            this.gdkDataGrid.RowTemplate.Height = 24;
            this.gdkDataGrid.Size = new System.Drawing.Size(747, 329);
            this.gdkDataGrid.TabIndex = 3;
            this.gdkDataGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gdkDataGrid_CellClick);
            this.gdkDataGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gdkDataGrid_CellContentClick);
            // 
            // workMode
            // 
            this.workMode.Controls.Add(this.deleteRadio);
            this.workMode.Controls.Add(this.editRadio);
            this.workMode.Controls.Add(this.addRadio);
            this.workMode.Controls.Add(this.searchRadio);
            this.workMode.Location = new System.Drawing.Point(12, 12);
            this.workMode.Name = "workMode";
            this.workMode.Padding = new System.Windows.Forms.Padding(10);
            this.workMode.Size = new System.Drawing.Size(165, 178);
            this.workMode.TabIndex = 9;
            this.workMode.TabStop = false;
            this.workMode.Text = "Режим роботи";
            this.workMode.Visible = false;
            // 
            // deleteRadio
            // 
            this.deleteRadio.AutoSize = true;
            this.deleteRadio.Location = new System.Drawing.Point(13, 149);
            this.deleteRadio.Name = "deleteRadio";
            this.deleteRadio.Size = new System.Drawing.Size(102, 21);
            this.deleteRadio.TabIndex = 3;
            this.deleteRadio.TabStop = true;
            this.deleteRadio.Text = "Видалення";
            this.deleteRadio.UseVisualStyleBackColor = true;
            this.deleteRadio.CheckedChanged += new System.EventHandler(this.handleRadioButtonChange);
            // 
            // editRadio
            // 
            this.editRadio.AutoSize = true;
            this.editRadio.Location = new System.Drawing.Point(13, 106);
            this.editRadio.Name = "editRadio";
            this.editRadio.Size = new System.Drawing.Size(113, 21);
            this.editRadio.TabIndex = 2;
            this.editRadio.TabStop = true;
            this.editRadio.Text = "Редагування";
            this.editRadio.UseVisualStyleBackColor = true;
            this.editRadio.CheckedChanged += new System.EventHandler(this.handleRadioButtonChange);
            // 
            // addRadio
            // 
            this.addRadio.AutoSize = true;
            this.addRadio.Location = new System.Drawing.Point(13, 63);
            this.addRadio.Name = "addRadio";
            this.addRadio.Size = new System.Drawing.Size(103, 21);
            this.addRadio.TabIndex = 1;
            this.addRadio.TabStop = true;
            this.addRadio.Text = "Додавання";
            this.addRadio.UseVisualStyleBackColor = true;
            this.addRadio.CheckedChanged += new System.EventHandler(this.handleRadioButtonChange);
            // 
            // searchRadio
            // 
            this.searchRadio.AutoSize = true;
            this.searchRadio.Location = new System.Drawing.Point(14, 20);
            this.searchRadio.Name = "searchRadio";
            this.searchRadio.Size = new System.Drawing.Size(72, 21);
            this.searchRadio.TabIndex = 0;
            this.searchRadio.TabStop = true;
            this.searchRadio.Text = "Пошук";
            this.searchRadio.UseVisualStyleBackColor = true;
            this.searchRadio.CheckedChanged += new System.EventHandler(this.handleRadioButtonChange);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(183, 22);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(10, 20, 10, 10);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(576, 137);
            this.flowLayoutPanel1.TabIndex = 10;
            // 
            // doneBtn
            // 
            this.doneBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.doneBtn.Location = new System.Drawing.Point(183, 158);
            this.doneBtn.Name = "doneBtn";
            this.doneBtn.Size = new System.Drawing.Size(576, 31);
            this.doneBtn.TabIndex = 11;
            this.doneBtn.Text = "Виконати";
            this.doneBtn.UseVisualStyleBackColor = true;
            this.doneBtn.Click += new System.EventHandler(this.doneBtn_Click);
            // 
            // Dovidka
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(772, 537);
            this.Controls.Add(this.doneBtn);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.workMode);
            this.Controls.Add(this.gdkDataGrid);
            this.MinimumSize = new System.Drawing.Size(740, 584);
            this.Name = "Dovidka";
            this.Text = "Довідка - ГДК";
            this.Load += new System.EventHandler(this.DovidkaGDK_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gdkDataGrid)).EndInit();
            this.workMode.ResumeLayout(false);
            this.workMode.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView gdkDataGrid;
        private System.Windows.Forms.GroupBox workMode;
        private System.Windows.Forms.RadioButton deleteRadio;
        private System.Windows.Forms.RadioButton editRadio;
        private System.Windows.Forms.RadioButton addRadio;
        private System.Windows.Forms.RadioButton searchRadio;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button doneBtn;
    }
}