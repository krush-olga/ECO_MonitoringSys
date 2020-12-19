namespace Experts_Economist
{
    partial class ImportDB
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
            this.pathTB = new System.Windows.Forms.TextBox();
            this.tableCB = new System.Windows.Forms.ComboBox();
            this.doneButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.delimeterTB = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tableColumnsListView = new System.Windows.Forms.ListBox();
            this.userColumnsListView = new System.Windows.Forms.ListBox();
            this.mapList = new System.Windows.Forms.TextBox();
            this.mapButton = new System.Windows.Forms.Button();
            this.clearButton = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // pathTB
            // 
            this.pathTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pathTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pathTB.Location = new System.Drawing.Point(28, 49);
            this.pathTB.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pathTB.Name = "pathTB";
            this.pathTB.ReadOnly = true;
            this.pathTB.Size = new System.Drawing.Size(304, 24);
            this.pathTB.TabIndex = 0;
            // 
            // tableCB
            // 
            this.tableCB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tableCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tableCB.FormattingEnabled = true;
            this.tableCB.Location = new System.Drawing.Point(28, 107);
            this.tableCB.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tableCB.Name = "tableCB";
            this.tableCB.Size = new System.Drawing.Size(335, 25);
            this.tableCB.TabIndex = 1;
            this.tableCB.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // doneButton
            // 
            this.doneButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.doneButton.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.doneButton.Location = new System.Drawing.Point(0, 483);
            this.doneButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.doneButton.Name = "doneButton";
            this.doneButton.Padding = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.doneButton.Size = new System.Drawing.Size(385, 53);
            this.doneButton.TabIndex = 2;
            this.doneButton.Text = "Імпортувати";
            this.doneButton.UseVisualStyleBackColor = true;
            this.doneButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(28, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Імпортувати з";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(28, 84);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(186, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Імпортувати у таблицю";
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(335, 49);
            this.button2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(26, 24);
            this.button2.TabIndex = 5;
            this.button2.Text = "...";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // delimeterTB
            // 
            this.delimeterTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.delimeterTB.Location = new System.Drawing.Point(98, 151);
            this.delimeterTB.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.delimeterTB.Name = "delimeterTB";
            this.delimeterTB.Size = new System.Drawing.Size(30, 24);
            this.delimeterTB.TabIndex = 6;
            this.delimeterTB.Text = ";";
            this.delimeterTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.delimeterTB.TextChanged += new System.EventHandler(this.delimeterTB_TextChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(28, 151);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "Delimiter";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // tableColumnsListView
            // 
            this.tableColumnsListView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tableColumnsListView.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tableColumnsListView.FormattingEnabled = true;
            this.tableColumnsListView.ItemHeight = 17;
            this.tableColumnsListView.Location = new System.Drawing.Point(28, 179);
            this.tableColumnsListView.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tableColumnsListView.Name = "tableColumnsListView";
            this.tableColumnsListView.Size = new System.Drawing.Size(163, 106);
            this.tableColumnsListView.TabIndex = 10;
            // 
            // userColumnsListView
            // 
            this.userColumnsListView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.userColumnsListView.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.userColumnsListView.FormattingEnabled = true;
            this.userColumnsListView.ItemHeight = 17;
            this.userColumnsListView.Location = new System.Drawing.Point(194, 179);
            this.userColumnsListView.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.userColumnsListView.Name = "userColumnsListView";
            this.userColumnsListView.Size = new System.Drawing.Size(163, 106);
            this.userColumnsListView.TabIndex = 11;
            // 
            // mapList
            // 
            this.mapList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mapList.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.mapList.Location = new System.Drawing.Point(28, 350);
            this.mapList.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.mapList.Multiline = true;
            this.mapList.Name = "mapList";
            this.mapList.ReadOnly = true;
            this.mapList.Size = new System.Drawing.Size(330, 117);
            this.mapList.TabIndex = 12;
            this.mapList.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // mapButton
            // 
            this.mapButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.mapButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.mapButton.Location = new System.Drawing.Point(28, 305);
            this.mapButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.mapButton.Name = "mapButton";
            this.mapButton.Padding = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.mapButton.Size = new System.Drawing.Size(162, 41);
            this.mapButton.TabIndex = 13;
            this.mapButton.Text = "Зв\'язати обрані";
            this.mapButton.UseVisualStyleBackColor = true;
            this.mapButton.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // clearButton
            // 
            this.clearButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.clearButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.clearButton.Location = new System.Drawing.Point(194, 305);
            this.clearButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.clearButton.Name = "clearButton";
            this.clearButton.Padding = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.clearButton.Size = new System.Drawing.Size(162, 41);
            this.clearButton.TabIndex = 14;
            this.clearButton.Text = "Очистити";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 150;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // radioButton1
            // 
            this.radioButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButton1.AutoSize = true;
            this.radioButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioButton1.Location = new System.Drawing.Point(263, 151);
            this.radioButton1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(47, 24);
            this.radioButton1.TabIndex = 15;
            this.radioButton1.Text = "tsv";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            this.radioButton1.Click += new System.EventHandler(this.radioButton1_Click);
            // 
            // radioButton2
            // 
            this.radioButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButton2.AutoSize = true;
            this.radioButton2.Checked = true;
            this.radioButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioButton2.Location = new System.Drawing.Point(311, 151);
            this.radioButton2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(50, 24);
            this.radioButton2.TabIndex = 15;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "csv";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            this.radioButton2.Click += new System.EventHandler(this.radioButton2_Click);
            // 
            // ImportDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(385, 536);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.mapButton);
            this.Controls.Add(this.mapList);
            this.Controls.Add(this.userColumnsListView);
            this.Controls.Add(this.tableColumnsListView);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.delimeterTB);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.doneButton);
            this.Controls.Add(this.tableCB);
            this.Controls.Add(this.pathTB);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MinimumSize = new System.Drawing.Size(288, 45);
            this.Name = "ImportDB";
            this.Text = "ImportDB";
            this.Load += new System.EventHandler(this.ImportDB_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox pathTB;
        private System.Windows.Forms.ComboBox tableCB;
        private System.Windows.Forms.Button doneButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox delimeterTB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox tableColumnsListView;
        private System.Windows.Forms.ListBox userColumnsListView;
        private System.Windows.Forms.TextBox mapList;
        private System.Windows.Forms.Button mapButton;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
    }
}