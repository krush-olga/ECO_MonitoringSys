using System.Drawing;

namespace HelpModule.Controls
{
	partial class ToolTipContent
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.LinkLabel = new System.Windows.Forms.LinkLabel();
			this.Message = new HelpModule.Controls.GrowLabel();
			this.SuspendLayout();
			// 
			// LinkLabel
			// 
			this.LinkLabel.AutoSize = true;
			this.LinkLabel.Location = new System.Drawing.Point(3, 71);
			this.LinkLabel.Name = "LinkLabel";
			this.LinkLabel.Size = new System.Drawing.Size(30, 13);
			this.LinkLabel.TabIndex = 1;
			this.LinkLabel.TabStop = true;
			this.LinkLabel.Text = "Далі";
			// 
			// Message
			// 
			this.Message.AutoSize = true;
			this.Message.Location = new System.Drawing.Point(4, 3);
			this.Message.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.Message.MaximumSize = new System.Drawing.Size(170, 0);
			this.Message.Name = "Message";
			this.Message.Size = new System.Drawing.Size(33, 13);
			this.Message.TabIndex = 14;
			this.Message.Text = "Опис";
			this.Message.TextChanged += new System.EventHandler(this.Message_TextChanged);
			// 
			// ToolTipContent
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.Message);
			this.Controls.Add(this.LinkLabel);
			this.MaximumSize = new System.Drawing.Size(180, 900);
			this.Name = "ToolTipContent";
			this.Size = new System.Drawing.Size(180, 88);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		public System.Windows.Forms.LinkLabel LinkLabel;
		public HelpModule.Controls.GrowLabel Message;
	}
}
