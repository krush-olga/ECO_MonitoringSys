namespace MedicForm
{
    partial class FormulaChart
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
            this.chartGroup = new System.Windows.Forms.GroupBox();
            this.removeAll = new System.Windows.Forms.Button();
            this.addAll = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.addLabel = new System.Windows.Forms.Label();
            this.removeButt = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.addSeriaList = new System.Windows.Forms.ListBox();
            this.seriaList = new System.Windows.Forms.ListBox();
            this.funcLabel = new System.Windows.Forms.Label();
            this.funcComboBox = new System.Windows.Forms.ComboBox();
            this.diagrButt = new System.Windows.Forms.Button();
            this.graphButt = new System.Windows.Forms.Button();
            this.seriaInfo = new System.Windows.Forms.GroupBox();
            this.seriaDescription = new System.Windows.Forms.Label();
            this.seriaName = new System.Windows.Forms.Label();
            this.chartGroup.SuspendLayout();
            this.seriaInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // chartGroup
            // 
            this.chartGroup.Controls.Add(this.removeAll);
            this.chartGroup.Controls.Add(this.addAll);
            this.chartGroup.Controls.Add(this.label1);
            this.chartGroup.Controls.Add(this.addLabel);
            this.chartGroup.Controls.Add(this.removeButt);
            this.chartGroup.Controls.Add(this.addButton);
            this.chartGroup.Controls.Add(this.addSeriaList);
            this.chartGroup.Controls.Add(this.seriaList);
            this.chartGroup.Controls.Add(this.funcLabel);
            this.chartGroup.Controls.Add(this.funcComboBox);
            this.chartGroup.Controls.Add(this.diagrButt);
            this.chartGroup.Controls.Add(this.graphButt);
            this.chartGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.chartGroup.Location = new System.Drawing.Point(13, 15);
            this.chartGroup.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chartGroup.Name = "chartGroup";
            this.chartGroup.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chartGroup.Size = new System.Drawing.Size(432, 415);
            this.chartGroup.TabIndex = 37;
            this.chartGroup.TabStop = false;
            this.chartGroup.Text = "Графік одного показника";
            this.chartGroup.Enter += new System.EventHandler(this.chartGroup_Enter);
            // 
            // removeAll
            // 
            this.removeAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.removeAll.Location = new System.Drawing.Point(258, 340);
            this.removeAll.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.removeAll.Name = "removeAll";
            this.removeAll.Size = new System.Drawing.Size(89, 28);
            this.removeAll.TabIndex = 50;
            this.removeAll.Text = "Очистити";
            this.removeAll.UseVisualStyleBackColor = true;
            this.removeAll.Click += new System.EventHandler(this.removeAll_Click);
            // 
            // addAll
            // 
            this.addAll.Location = new System.Drawing.Point(96, 340);
            this.addAll.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.addAll.Name = "addAll";
            this.addAll.Size = new System.Drawing.Size(73, 28);
            this.addAll.TabIndex = 49;
            this.addAll.Text = "→→";
            this.addAll.UseVisualStyleBackColor = true;
            this.addAll.Click += new System.EventHandler(this.addAll_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(8, 79);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 20);
            this.label1.TabIndex = 41;
            this.label1.Text = "   ";
            this.label1.Visible = false;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // addLabel
            // 
            this.addLabel.AutoSize = true;
            this.addLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addLabel.Location = new System.Drawing.Point(5, 196);
            this.addLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.addLabel.Name = "addLabel";
            this.addLabel.Size = new System.Drawing.Size(322, 20);
            this.addLabel.TabIndex = 40;
            this.addLabel.Text = "Виберіть серії які треба відобразити";
            // 
            // removeButt
            // 
            this.removeButt.Location = new System.Drawing.Point(189, 279);
            this.removeButt.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.removeButt.Name = "removeButt";
            this.removeButt.Size = new System.Drawing.Size(49, 28);
            this.removeButt.TabIndex = 39;
            this.removeButt.Text = "←";
            this.removeButt.UseVisualStyleBackColor = true;
            this.removeButt.Click += new System.EventHandler(this.removeButt_Click);
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(189, 232);
            this.addButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(49, 28);
            this.addButton.TabIndex = 38;
            this.addButton.Text = "→";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // addSeriaList
            // 
            this.addSeriaList.FormattingEnabled = true;
            this.addSeriaList.ItemHeight = 20;
            this.addSeriaList.Location = new System.Drawing.Point(258, 219);
            this.addSeriaList.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.addSeriaList.Name = "addSeriaList";
            this.addSeriaList.Size = new System.Drawing.Size(159, 104);
            this.addSeriaList.TabIndex = 37;
            // 
            // seriaList
            // 
            this.seriaList.FormattingEnabled = true;
            this.seriaList.ItemHeight = 20;
            this.seriaList.Location = new System.Drawing.Point(9, 219);
            this.seriaList.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.seriaList.Name = "seriaList";
            this.seriaList.Size = new System.Drawing.Size(159, 104);
            this.seriaList.TabIndex = 36;
            this.seriaList.SelectedIndexChanged += new System.EventHandler(this.seriaList_SelectedIndexChanged);
            // 
            // funcLabel
            // 
            this.funcLabel.AutoSize = true;
            this.funcLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.funcLabel.Location = new System.Drawing.Point(8, 22);
            this.funcLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.funcLabel.Name = "funcLabel";
            this.funcLabel.Size = new System.Drawing.Size(172, 20);
            this.funcLabel.TabIndex = 35;
            this.funcLabel.Text = "Виберіть показник \r\n";
            // 
            // funcComboBox
            // 
            this.funcComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.funcComboBox.FormattingEnabled = true;
            this.funcComboBox.Location = new System.Drawing.Point(12, 46);
            this.funcComboBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.funcComboBox.Name = "funcComboBox";
            this.funcComboBox.Size = new System.Drawing.Size(405, 28);
            this.funcComboBox.TabIndex = 34;
            this.funcComboBox.SelectedIndexChanged += new System.EventHandler(this.funcComboBox_SelectedIndexChanged);
            this.funcComboBox.TextChanged += new System.EventHandler(this.funcComboBox_TextChanged);
            // 
            // diagrButt
            // 
            this.diagrButt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.diagrButt.Location = new System.Drawing.Point(40, 375);
            this.diagrButt.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.diagrButt.Name = "diagrButt";
            this.diagrButt.Size = new System.Drawing.Size(173, 28);
            this.diagrButt.TabIndex = 26;
            this.diagrButt.Text = "Діаграма";
            this.diagrButt.UseVisualStyleBackColor = true;
            this.diagrButt.Click += new System.EventHandler(this.diagrButt_Click);
            // 
            // graphButt
            // 
            this.graphButt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.graphButt.Location = new System.Drawing.Point(221, 375);
            this.graphButt.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.graphButt.Name = "graphButt";
            this.graphButt.Size = new System.Drawing.Size(173, 28);
            this.graphButt.TabIndex = 17;
            this.graphButt.Text = "Графік";
            this.graphButt.UseVisualStyleBackColor = true;
            this.graphButt.Click += new System.EventHandler(this.graphButt_Click);
            // 
            // seriaInfo
            // 
            this.seriaInfo.Controls.Add(this.seriaDescription);
            this.seriaInfo.Controls.Add(this.seriaName);
            this.seriaInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.seriaInfo.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.seriaInfo.Location = new System.Drawing.Point(453, 15);
            this.seriaInfo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.seriaInfo.Name = "seriaInfo";
            this.seriaInfo.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.seriaInfo.Size = new System.Drawing.Size(379, 415);
            this.seriaInfo.TabIndex = 38;
            this.seriaInfo.TabStop = false;
            this.seriaInfo.Text = "Інформація про серію";
            // 
            // seriaDescription
            // 
            this.seriaDescription.AutoSize = true;
            this.seriaDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.seriaDescription.Location = new System.Drawing.Point(8, 55);
            this.seriaDescription.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.seriaDescription.Name = "seriaDescription";
            this.seriaDescription.Size = new System.Drawing.Size(0, 20);
            this.seriaDescription.TabIndex = 1;
            // 
            // seriaName
            // 
            this.seriaName.AutoSize = true;
            this.seriaName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.seriaName.Location = new System.Drawing.Point(8, 22);
            this.seriaName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.seriaName.Name = "seriaName";
            this.seriaName.Size = new System.Drawing.Size(0, 20);
            this.seriaName.TabIndex = 0;
            // 
            // FormulaChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(845, 443);
            this.Controls.Add(this.seriaInfo);
            this.Controls.Add(this.chartGroup);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FormulaChart";
            this.Text = "Вікно побудови графіка";
            this.Load += new System.EventHandler(this.FormulaChart_Load);
            this.chartGroup.ResumeLayout(false);
            this.chartGroup.PerformLayout();
            this.seriaInfo.ResumeLayout(false);
            this.seriaInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox chartGroup;
        private System.Windows.Forms.Label addLabel;
        private System.Windows.Forms.Button removeButt;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.ListBox addSeriaList;
        private System.Windows.Forms.ListBox seriaList;
        private System.Windows.Forms.Label funcLabel;
        private System.Windows.Forms.ComboBox funcComboBox;
        private System.Windows.Forms.Button diagrButt;
        private System.Windows.Forms.Button graphButt;
        private System.Windows.Forms.GroupBox seriaInfo;
        private System.Windows.Forms.Label seriaDescription;
        private System.Windows.Forms.Label seriaName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button addAll;
        private System.Windows.Forms.Button removeAll;
    }
}