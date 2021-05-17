
namespace HelpModule.Forms
{
	partial class ListScenarioForm
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
            this.SplitContainer = new System.Windows.Forms.SplitContainer();
            this.scenarios = new System.Windows.Forms.ListBox();
            this.searchQuery = new System.Windows.Forms.TextBox();
            this.searchButton = new System.Windows.Forms.Button();
            this.next = new System.Windows.Forms.Button();
            this.prev = new System.Windows.Forms.Button();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.SecondWizardPage2Picture1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer)).BeginInit();
            this.SplitContainer.Panel1.SuspendLayout();
            this.SplitContainer.Panel2.SuspendLayout();
            this.SplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SecondWizardPage2Picture1)).BeginInit();
            this.SuspendLayout();
            // 
            // SplitContainer
            // 
            this.SplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitContainer.Location = new System.Drawing.Point(0, 0);
            this.SplitContainer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SplitContainer.Name = "SplitContainer";
            // 
            // SplitContainer.Panel1
            // 
            this.SplitContainer.Panel1.Controls.Add(this.scenarios);
            this.SplitContainer.Panel1.Controls.Add(this.searchQuery);
            this.SplitContainer.Panel1.Controls.Add(this.searchButton);
            // 
            // SplitContainer.Panel2
            // 
            this.SplitContainer.Panel2.Controls.Add(this.next);
            this.SplitContainer.Panel2.Controls.Add(this.prev);
            this.SplitContainer.Panel2.Controls.Add(this.webBrowser1);
            this.SplitContainer.Size = new System.Drawing.Size(1615, 863);
            this.SplitContainer.SplitterDistance = 357;
            this.SplitContainer.TabIndex = 1;
            // 
            // scenarios
            // 
            this.scenarios.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.scenarios.FormattingEnabled = true;
            this.scenarios.ItemHeight = 17;
            this.scenarios.Items.AddRange(new object[] {
            "Сценарій роботи системи",
            "Сценарій створення маркерів та прив‘язки маркерів до задач",
            "Сценарій створення полігонів на мапі",
            "Сценарій роботи з областями на мапі"});
            this.scenarios.Location = new System.Drawing.Point(12, 132);
            this.scenarios.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.scenarios.Name = "scenarios";
            this.scenarios.Size = new System.Drawing.Size(275, 344);
            this.scenarios.TabIndex = 5;
            this.scenarios.SelectedIndexChanged += new System.EventHandler(this.scenarios_SelectedIndexChanged);
            // 
            // searchQuery
            // 
            this.searchQuery.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.searchQuery.Location = new System.Drawing.Point(13, 31);
            this.searchQuery.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.searchQuery.Name = "searchQuery";
            this.searchQuery.Size = new System.Drawing.Size(275, 24);
            this.searchQuery.TabIndex = 4;
            this.searchQuery.TextChanged += new System.EventHandler(this.searchQuery_TextChanged);
            this.searchQuery.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.searchQuery_KeyPress);
            // 
            // searchButton
            // 
            this.searchButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.searchButton.Location = new System.Drawing.Point(93, 73);
            this.searchButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(104, 37);
            this.searchButton.TabIndex = 3;
            this.searchButton.Text = "Пошук";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // next
            // 
            this.next.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.next.Location = new System.Drawing.Point(1069, 791);
            this.next.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.next.Name = "next";
            this.next.Size = new System.Drawing.Size(128, 42);
            this.next.TabIndex = 2;
            this.next.Text = "Далі";
            this.next.UseVisualStyleBackColor = true;
            this.next.Click += new System.EventHandler(this.next_Click);
            // 
            // prev
            // 
            this.prev.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.prev.Location = new System.Drawing.Point(909, 791);
            this.prev.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.prev.Name = "prev";
            this.prev.Size = new System.Drawing.Size(128, 42);
            this.prev.TabIndex = 1;
            this.prev.Text = "Назад";
            this.prev.UseVisualStyleBackColor = true;
            this.prev.Click += new System.EventHandler(this.prev_Click);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(4, 4);
            this.webBrowser1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(27, 25);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(1245, 780);
            this.webBrowser1.TabIndex = 0;
            // 
            // SecondWizardPage2Picture1
            // 
            this.SecondWizardPage2Picture1.Location = new System.Drawing.Point(0, 0);
            this.SecondWizardPage2Picture1.Name = "SecondWizardPage2Picture1";
            this.SecondWizardPage2Picture1.Size = new System.Drawing.Size(100, 50);
            this.SecondWizardPage2Picture1.TabIndex = 0;
            this.SecondWizardPage2Picture1.TabStop = false;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 0;
            // 
            // ListScenarioForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1615, 863);
            this.Controls.Add(this.SplitContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "ListScenarioForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Список сценаріїв";
            this.SplitContainer.Panel1.ResumeLayout(false);
            this.SplitContainer.Panel1.PerformLayout();
            this.SplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer)).EndInit();
            this.SplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SecondWizardPage2Picture1)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer SplitContainer;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.PictureBox SecondWizardPage2Picture1;
		private System.Windows.Forms.Button searchButton;
		private System.Windows.Forms.ListBox scenarios;
		private System.Windows.Forms.TextBox searchQuery;
		private System.Windows.Forms.Button next;
		private System.Windows.Forms.Button prev;
		private System.Windows.Forms.WebBrowser webBrowser1;
	}
}