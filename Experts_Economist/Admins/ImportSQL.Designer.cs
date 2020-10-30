namespace Experts_Economist
{
    partial class ImportSQL
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
            this.label1 = new System.Windows.Forms.Label();
            this.passwordLB = new System.Windows.Forms.Label();
            this.loginLB = new System.Windows.Forms.Label();
            this.passwordTB = new System.Windows.Forms.TextBox();
            this.loginTB = new System.Windows.Forms.TextBox();
            this.schemaCB = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.importPathTB = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.importBtn = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.приховатиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.неПриховуватиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(29, 120);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 25);
            this.label1.TabIndex = 25;
            this.label1.Text = "У схему";
            // 
            // passwordLB
            // 
            this.passwordLB.AutoSize = true;
            this.passwordLB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.passwordLB.Location = new System.Drawing.Point(29, 292);
            this.passwordLB.Name = "passwordLB";
            this.passwordLB.Size = new System.Drawing.Size(80, 25);
            this.passwordLB.TabIndex = 24;
            this.passwordLB.Text = "Пароль";
            // 
            // loginLB
            // 
            this.loginLB.AutoSize = true;
            this.loginLB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.loginLB.Location = new System.Drawing.Point(29, 214);
            this.loginLB.Name = "loginLB";
            this.loginLB.Size = new System.Drawing.Size(61, 25);
            this.loginLB.TabIndex = 23;
            this.loginLB.Text = "Логін";
            // 
            // passwordTB
            // 
            this.passwordTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.passwordTB.Location = new System.Drawing.Point(32, 321);
            this.passwordTB.Name = "passwordTB";
            this.passwordTB.Size = new System.Drawing.Size(398, 28);
            this.passwordTB.TabIndex = 22;
            this.passwordTB.UseSystemPasswordChar = true;
            // 
            // loginTB
            // 
            this.loginTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.loginTB.Location = new System.Drawing.Point(32, 245);
            this.loginTB.Name = "loginTB";
            this.loginTB.Size = new System.Drawing.Size(398, 28);
            this.loginTB.TabIndex = 21;
            // 
            // schemaCB
            // 
            this.schemaCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.schemaCB.FormattingEnabled = true;
            this.schemaCB.Location = new System.Drawing.Point(32, 158);
            this.schemaCB.Name = "schemaCB";
            this.schemaCB.Size = new System.Drawing.Size(398, 33);
            this.schemaCB.TabIndex = 20;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(27, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(147, 25);
            this.label3.TabIndex = 19;
            this.label3.Text = "Імпортувати з";
            // 
            // importPathTB
            // 
            this.importPathTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.importPathTB.Location = new System.Drawing.Point(32, 69);
            this.importPathTB.Name = "importPathTB";
            this.importPathTB.Size = new System.Drawing.Size(357, 28);
            this.importPathTB.TabIndex = 18;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(395, 69);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(35, 30);
            this.button1.TabIndex = 17;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // importBtn
            // 
            this.importBtn.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.importBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.importBtn.Location = new System.Drawing.Point(0, 411);
            this.importBtn.Name = "importBtn";
            this.importBtn.Size = new System.Drawing.Size(461, 50);
            this.importBtn.TabIndex = 26;
            this.importBtn.Text = "Import";
            this.importBtn.UseVisualStyleBackColor = true;
            this.importBtn.Click += new System.EventHandler(this.importBtn_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            this.openFileDialog.Filter = "SQL Files (*.sql)|*.sql|All Files (*.*)|*.*";
            this.openFileDialog.InitialDirectory = ".";
            this.openFileDialog.Title = "Оберіть файл для імпортування";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.приховатиToolStripMenuItem,
            this.неПриховуватиToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(191, 52);
            // 
            // приховатиToolStripMenuItem
            // 
            this.приховатиToolStripMenuItem.Name = "приховатиToolStripMenuItem";
            this.приховатиToolStripMenuItem.Size = new System.Drawing.Size(190, 24);
            this.приховатиToolStripMenuItem.Text = "Приховати";
            this.приховатиToolStripMenuItem.Visible = false;
            this.приховатиToolStripMenuItem.Click += new System.EventHandler(this.приховатиToolStripMenuItem_Click);
            // 
            // неПриховуватиToolStripMenuItem
            // 
            this.неПриховуватиToolStripMenuItem.Name = "неПриховуватиToolStripMenuItem";
            this.неПриховуватиToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.неПриховуватиToolStripMenuItem.Text = "Не приховувати";
            this.неПриховуватиToolStripMenuItem.Click += new System.EventHandler(this.неПриховуватиToolStripMenuItem_Click);
            // 
            // ImportSQL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(461, 461);
            this.Controls.Add(this.importBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.passwordLB);
            this.Controls.Add(this.loginLB);
            this.Controls.Add(this.passwordTB);
            this.Controls.Add(this.loginTB);
            this.Controls.Add(this.schemaCB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.importPathTB);
            this.Controls.Add(this.button1);
            this.Name = "ImportSQL";
            this.Text = "ImportSQL";
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label passwordLB;
        private System.Windows.Forms.Label loginLB;
        private System.Windows.Forms.TextBox passwordTB;
        private System.Windows.Forms.TextBox loginTB;
        private System.Windows.Forms.ComboBox schemaCB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox importPathTB;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button importBtn;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem приховатиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem неПриховуватиToolStripMenuItem;
    }
}