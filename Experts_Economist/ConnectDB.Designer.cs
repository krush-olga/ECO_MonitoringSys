namespace Experts_Economist
{
    partial class ConnectDB
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
            this.label3 = new System.Windows.Forms.Label();
            this.serverTB = new System.Windows.Forms.TextBox();
            this.passwordLB = new System.Windows.Forms.Label();
            this.loginLB = new System.Windows.Forms.Label();
            this.passwordTB = new System.Windows.Forms.TextBox();
            this.loginTB = new System.Windows.Forms.TextBox();
            this.connectBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.databaseTB = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(29, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 25);
            this.label3.TabIndex = 10;
            this.label3.Text = "Сервер";
            // 
            // serverTB
            // 
            this.serverTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.serverTB.Location = new System.Drawing.Point(34, 68);
            this.serverTB.Name = "serverTB";
            this.serverTB.Size = new System.Drawing.Size(398, 28);
            this.serverTB.TabIndex = 9;
            // 
            // passwordLB
            // 
            this.passwordLB.AutoSize = true;
            this.passwordLB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.passwordLB.Location = new System.Drawing.Point(31, 283);
            this.passwordLB.Name = "passwordLB";
            this.passwordLB.Size = new System.Drawing.Size(80, 25);
            this.passwordLB.TabIndex = 19;
            this.passwordLB.Text = "Пароль";
            // 
            // loginLB
            // 
            this.loginLB.AutoSize = true;
            this.loginLB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.loginLB.Location = new System.Drawing.Point(31, 205);
            this.loginLB.Name = "loginLB";
            this.loginLB.Size = new System.Drawing.Size(61, 25);
            this.loginLB.TabIndex = 18;
            this.loginLB.Text = "Логін";
            // 
            // passwordTB
            // 
            this.passwordTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.passwordTB.Location = new System.Drawing.Point(34, 312);
            this.passwordTB.Name = "passwordTB";
            this.passwordTB.Size = new System.Drawing.Size(398, 28);
            this.passwordTB.TabIndex = 17;
            this.passwordTB.UseSystemPasswordChar = true;
            // 
            // loginTB
            // 
            this.loginTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.loginTB.Location = new System.Drawing.Point(34, 236);
            this.loginTB.Name = "loginTB";
            this.loginTB.Size = new System.Drawing.Size(398, 28);
            this.loginTB.TabIndex = 16;
            // 
            // connectBtn
            // 
            this.connectBtn.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.connectBtn.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.connectBtn.Location = new System.Drawing.Point(0, 411);
            this.connectBtn.Name = "connectBtn";
            this.connectBtn.Size = new System.Drawing.Size(461, 50);
            this.connectBtn.TabIndex = 20;
            this.connectBtn.Text = "Підключитися";
            this.connectBtn.UseVisualStyleBackColor = true;
            this.connectBtn.Click += new System.EventHandler(this.exportBtn_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(31, 113);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 25);
            this.label1.TabIndex = 22;
            this.label1.Text = "База даних";
            // 
            // databaseTB
            // 
            this.databaseTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.databaseTB.Location = new System.Drawing.Point(36, 154);
            this.databaseTB.Name = "databaseTB";
            this.databaseTB.Size = new System.Drawing.Size(396, 28);
            this.databaseTB.TabIndex = 21;
            // 
            // ConnectDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(461, 461);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.databaseTB);
            this.Controls.Add(this.connectBtn);
            this.Controls.Add(this.passwordLB);
            this.Controls.Add(this.loginLB);
            this.Controls.Add(this.passwordTB);
            this.Controls.Add(this.loginTB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.serverTB);
            this.Name = "ConnectDB";
            this.Text = "ConnectDB";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox serverTB;
        private System.Windows.Forms.Label passwordLB;
        private System.Windows.Forms.Label loginLB;
        private System.Windows.Forms.TextBox passwordTB;
        private System.Windows.Forms.TextBox loginTB;
        private System.Windows.Forms.Button connectBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox databaseTB;
    }
}