
namespace UserMap.HelpWindows
{
    partial class MultiBindingObjectEditor
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

                try
                {
                    base.Dispose(disposing);
                }
                catch { }
            }
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.SaveToBDButton = new System.Windows.Forms.Button();
            this.RestoreButton = new System.Windows.Forms.Button();
            this.CalcResToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.ContentContainerTabControl = new System.Windows.Forms.TabControl();
            this.SuspendLayout();
            // 
            // SaveToBDButton
            // 
            this.SaveToBDButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveToBDButton.Enabled = false;
            this.SaveToBDButton.Location = new System.Drawing.Point(659, 456);
            this.SaveToBDButton.Name = "SaveToBDButton";
            this.SaveToBDButton.Size = new System.Drawing.Size(166, 23);
            this.SaveToBDButton.TabIndex = 72;
            this.SaveToBDButton.Text = "Зберегти до бази даних";
            this.SaveToBDButton.UseVisualStyleBackColor = true;
            this.SaveToBDButton.Click += new System.EventHandler(this.SaveToBDButton_Click);
            // 
            // RestoreButton
            // 
            this.RestoreButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.RestoreButton.Enabled = false;
            this.RestoreButton.Location = new System.Drawing.Point(487, 456);
            this.RestoreButton.Name = "RestoreButton";
            this.RestoreButton.Size = new System.Drawing.Size(166, 23);
            this.RestoreButton.TabIndex = 73;
            this.RestoreButton.Text = "Відмінити усі дії";
            this.RestoreButton.UseVisualStyleBackColor = true;
            this.RestoreButton.Click += new System.EventHandler(this.RestoreEmissionsButton_Click);
            // 
            // ContentContainerTabControl
            // 
            this.ContentContainerTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ContentContainerTabControl.Location = new System.Drawing.Point(0, 0);
            this.ContentContainerTabControl.Name = "ContentContainerTabControl";
            this.ContentContainerTabControl.SelectedIndex = 0;
            this.ContentContainerTabControl.Size = new System.Drawing.Size(839, 454);
            this.ContentContainerTabControl.TabIndex = 0;
            this.ContentContainerTabControl.SelectedIndexChanged += new System.EventHandler(this.ContentContainerTabControl_TabIndexChanged);
            // 
            // MultiBindingObjectEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(839, 485);
            this.Controls.Add(this.RestoreButton);
            this.Controls.Add(this.SaveToBDButton);
            this.Controls.Add(this.ContentContainerTabControl);
            this.MinimumSize = new System.Drawing.Size(855, 400);
            this.Name = "MultiBindingObjectEditor";
            this.Text = "Додаткова інформація ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MultiBindingObjectEditor_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button SaveToBDButton;
        private System.Windows.Forms.Button RestoreButton;
        private System.Windows.Forms.ToolTip CalcResToolTip;
        private System.Windows.Forms.TabControl ContentContainerTabControl;
    }
}