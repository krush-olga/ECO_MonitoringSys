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
			this.startTutorial = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label3.Location = new System.Drawing.Point(22, 22);
			this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(65, 20);
			this.label3.TabIndex = 10;
			this.label3.Text = "Сервер";
			// 
			// serverTB
			// 
			this.serverTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.serverTB.Location = new System.Drawing.Point(26, 55);
			this.serverTB.Margin = new System.Windows.Forms.Padding(2);
			this.serverTB.Name = "serverTB";
			this.serverTB.Size = new System.Drawing.Size(300, 24);
			this.serverTB.TabIndex = 9;
			// 
			// passwordLB
			// 
			this.passwordLB.AutoSize = true;
			this.passwordLB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.passwordLB.Location = new System.Drawing.Point(23, 230);
			this.passwordLB.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.passwordLB.Name = "passwordLB";
			this.passwordLB.Size = new System.Drawing.Size(67, 20);
			this.passwordLB.TabIndex = 19;
			this.passwordLB.Text = "Пароль";
			// 
			// loginLB
			// 
			this.loginLB.AutoSize = true;
			this.loginLB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.loginLB.Location = new System.Drawing.Point(23, 167);
			this.loginLB.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.loginLB.Name = "loginLB";
			this.loginLB.Size = new System.Drawing.Size(49, 20);
			this.loginLB.TabIndex = 18;
			this.loginLB.Text = "Логін";
			// 
			// passwordTB
			// 
			this.passwordTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.passwordTB.Location = new System.Drawing.Point(26, 254);
			this.passwordTB.Margin = new System.Windows.Forms.Padding(2);
			this.passwordTB.Name = "passwordTB";
			this.passwordTB.Size = new System.Drawing.Size(300, 24);
			this.passwordTB.TabIndex = 17;
			this.passwordTB.UseSystemPasswordChar = true;
			// 
			// loginTB
			// 
			this.loginTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.loginTB.Location = new System.Drawing.Point(26, 192);
			this.loginTB.Margin = new System.Windows.Forms.Padding(2);
			this.loginTB.Name = "loginTB";
			this.loginTB.Size = new System.Drawing.Size(300, 24);
			this.loginTB.TabIndex = 16;
			// 
			// connectBtn
			// 
			this.connectBtn.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.connectBtn.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.connectBtn.Location = new System.Drawing.Point(0, 334);
			this.connectBtn.Margin = new System.Windows.Forms.Padding(2);
			this.connectBtn.Name = "connectBtn";
			this.connectBtn.Size = new System.Drawing.Size(346, 41);
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
			this.label1.Location = new System.Drawing.Point(23, 92);
			this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(95, 20);
			this.label1.TabIndex = 22;
			this.label1.Text = "База даних";
			// 
			// databaseTB
			// 
			this.databaseTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.databaseTB.Location = new System.Drawing.Point(27, 125);
			this.databaseTB.Margin = new System.Windows.Forms.Padding(2);
			this.databaseTB.Name = "databaseTB";
			this.databaseTB.Size = new System.Drawing.Size(298, 24);
			this.databaseTB.TabIndex = 21;
			// 
			// startTutorial
			// 
			this.startTutorial.AutoSize = true;
			this.startTutorial.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.startTutorial.Location = new System.Drawing.Point(316, 9);
			this.startTutorial.Name = "startTutorial";
			this.startTutorial.Size = new System.Drawing.Size(18, 20);
			this.startTutorial.TabIndex = 23;
			this.startTutorial.Text = "?";
			this.startTutorial.Click += new System.EventHandler(this.startTutorial_Click);
			this.startTutorial.MouseEnter += new System.EventHandler(this.startTutorial_MouseEnter);
			this.startTutorial.MouseLeave += new System.EventHandler(this.startTutorial_MouseLeave);
			// 
			// ConnectDB
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(346, 375);
			this.Controls.Add(this.startTutorial);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.databaseTB);
			this.Controls.Add(this.connectBtn);
			this.Controls.Add(this.passwordLB);
			this.Controls.Add(this.loginLB);
			this.Controls.Add(this.passwordTB);
			this.Controls.Add(this.loginTB);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.serverTB);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Margin = new System.Windows.Forms.Padding(2);
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
		private System.Windows.Forms.Label startTutorial;
	}
}