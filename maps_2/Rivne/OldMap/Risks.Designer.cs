namespace Odessa
{
    partial class Risks
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.One = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Two = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tree = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Four = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.One,
            this.Two,
            this.Tree,
            this.Four});
            this.dataGridView1.Location = new System.Drawing.Point(12, 39);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(446, 509);
            this.dataGridView1.TabIndex = 0;
            // 
            // One
            // 
            this.One.HeaderText = "Елемент";
            this.One.Name = "One";
            this.One.ReadOnly = true;
            // 
            // Two
            // 
            this.Two.HeaderText = "Середнє значення";
            this.Two.Name = "Two";
            this.Two.ReadOnly = true;
            // 
            // Tree
            // 
            this.Tree.HeaderText = "ГДК";
            this.Tree.Name = "Tree";
            this.Tree.ReadOnly = true;
            // 
            // Four
            // 
            this.Four.HeaderText = "Коефіцієнт ризику";
            this.Four.Name = "Four";
            this.Four.ReadOnly = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Зараз обрано маркер : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(145, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "label2";
            // 
            // Risks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 560);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Risks";
            this.Text = "Risks";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn One;
        private System.Windows.Forms.DataGridViewTextBoxColumn Two;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tree;
        private System.Windows.Forms.DataGridViewTextBoxColumn Four;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}