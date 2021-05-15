namespace oprForm
{
    partial class AlterEventForm
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
			this.resLB = new System.Windows.Forms.ListBox();
			this.contextMenuRes = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.додатиНовийРесурсToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.переглядІнформаціїПроРесурсToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.alterGB = new System.Windows.Forms.GroupBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.descTB = new System.Windows.Forms.TextBox();
			this.evNameTB = new System.Windows.Forms.TextBox();
			this.issuesCB = new System.Windows.Forms.ComboBox();
			this.delBtn = new System.Windows.Forms.Button();
			this.addBtn = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.eventListGrid = new System.Windows.Forms.DataGridView();
			this.Resource = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.label1 = new System.Windows.Forms.Label();
			this.eventsLB = new System.Windows.Forms.ListBox();
			this.label3 = new System.Windows.Forms.Label();
			this.btnSearchTemplate = new System.Windows.Forms.Button();
			this.txtBxTemplate = new System.Windows.Forms.TextBox();
			this.btnRes = new System.Windows.Forms.Button();
			this.txtBxRes = new System.Windows.Forms.TextBox();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.findIssueCB = new System.Windows.Forms.ComboBox();
			this.startTutorial = new System.Windows.Forms.Label();
			this.contextMenuRes.SuspendLayout();
			this.alterGB.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.eventListGrid)).BeginInit();
			this.SuspendLayout();
			// 
			// resLB
			// 
			this.resLB.ContextMenuStrip = this.contextMenuRes;
			this.resLB.FormattingEnabled = true;
			this.resLB.Location = new System.Drawing.Point(183, 47);
			this.resLB.Margin = new System.Windows.Forms.Padding(2);
			this.resLB.Name = "resLB";
			this.resLB.Size = new System.Drawing.Size(160, 303);
			this.resLB.TabIndex = 17;
			this.toolTip1.SetToolTip(this.resLB, "Для додання нового ресурсу \r\nдо переліку рисурсів заходу \r\nнеобхідно двічі натисн" +
        "ути на ресурсі");
			this.resLB.DoubleClick += new System.EventHandler(this.addMaterial);
			// 
			// contextMenuRes
			// 
			this.contextMenuRes.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.додатиНовийРесурсToolStripMenuItem,
            this.переглядІнформаціїПроРесурсToolStripMenuItem});
			this.contextMenuRes.Name = "contextMenuRes";
			this.contextMenuRes.Size = new System.Drawing.Size(256, 48);
			// 
			// додатиНовийРесурсToolStripMenuItem
			// 
			this.додатиНовийРесурсToolStripMenuItem.Name = "додатиНовийРесурсToolStripMenuItem";
			this.додатиНовийРесурсToolStripMenuItem.Size = new System.Drawing.Size(255, 22);
			this.додатиНовийРесурсToolStripMenuItem.Text = "Створити новий ресурс ";
			// 
			// переглядІнформаціїПроРесурсToolStripMenuItem
			// 
			this.переглядІнформаціїПроРесурсToolStripMenuItem.Name = "переглядІнформаціїПроРесурсToolStripMenuItem";
			this.переглядІнформаціїПроРесурсToolStripMenuItem.Size = new System.Drawing.Size(255, 22);
			this.переглядІнформаціїПроРесурсToolStripMenuItem.Text = "Перегляд інформації про ресурс";
			// 
			// alterGB
			// 
			this.alterGB.Controls.Add(this.label6);
			this.alterGB.Controls.Add(this.label5);
			this.alterGB.Controls.Add(this.label4);
			this.alterGB.Controls.Add(this.descTB);
			this.alterGB.Controls.Add(this.evNameTB);
			this.alterGB.Controls.Add(this.issuesCB);
			this.alterGB.Controls.Add(this.delBtn);
			this.alterGB.Controls.Add(this.addBtn);
			this.alterGB.Location = new System.Drawing.Point(17, 360);
			this.alterGB.Margin = new System.Windows.Forms.Padding(2);
			this.alterGB.Name = "alterGB";
			this.alterGB.Padding = new System.Windows.Forms.Padding(2);
			this.alterGB.Size = new System.Drawing.Size(804, 90);
			this.alterGB.TabIndex = 15;
			this.alterGB.TabStop = false;
			this.alterGB.Text = "Відомості про захід";
			this.alterGB.Visible = false;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(8, 68);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(43, 13);
			this.label6.TabIndex = 13;
			this.label6.Text = "Задача";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(8, 42);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(70, 13);
			this.label5.TabIndex = 12;
			this.label5.Text = "Опис заходу";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(8, 19);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(76, 13);
			this.label4.TabIndex = 11;
			this.label4.Text = "Назва заходу";
			// 
			// descTB
			// 
			this.descTB.Location = new System.Drawing.Point(90, 39);
			this.descTB.Multiline = true;
			this.descTB.Name = "descTB";
			this.descTB.Size = new System.Drawing.Size(603, 20);
			this.descTB.TabIndex = 5;
			// 
			// evNameTB
			// 
			this.evNameTB.Location = new System.Drawing.Point(90, 16);
			this.evNameTB.Multiline = true;
			this.evNameTB.Name = "evNameTB";
			this.evNameTB.Size = new System.Drawing.Size(603, 20);
			this.evNameTB.TabIndex = 6;
			// 
			// issuesCB
			// 
			this.issuesCB.FormattingEnabled = true;
			this.issuesCB.Location = new System.Drawing.Point(90, 63);
			this.issuesCB.Margin = new System.Windows.Forms.Padding(2);
			this.issuesCB.Name = "issuesCB";
			this.issuesCB.Size = new System.Drawing.Size(603, 21);
			this.issuesCB.TabIndex = 7;
			// 
			// delBtn
			// 
			this.delBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.delBtn.Location = new System.Drawing.Point(700, 55);
			this.delBtn.Name = "delBtn";
			this.delBtn.Size = new System.Drawing.Size(100, 30);
			this.delBtn.TabIndex = 8;
			this.delBtn.Text = "Видалити захід";
			this.delBtn.UseVisualStyleBackColor = true;
			this.delBtn.Click += new System.EventHandler(this.delBtn_Click);
			// 
			// addBtn
			// 
			this.addBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.addBtn.Location = new System.Drawing.Point(700, 16);
			this.addBtn.Name = "addBtn";
			this.addBtn.Size = new System.Drawing.Size(100, 30);
			this.addBtn.TabIndex = 3;
			this.addBtn.Text = "Зберегти зміни";
			this.addBtn.UseVisualStyleBackColor = true;
			this.addBtn.Click += new System.EventHandler(this.addBtn_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(179, 9);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(169, 13);
			this.label2.TabIndex = 14;
			this.label2.Text = "Перелік всіх доступних ресурсів";
			// 
			// eventListGrid
			// 
			this.eventListGrid.AllowUserToAddRows = false;
			this.eventListGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.eventListGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.eventListGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Resource,
            this.Description,
            this.Value});
			this.eventListGrid.Location = new System.Drawing.Point(350, 24);
			this.eventListGrid.Name = "eventListGrid";
			this.eventListGrid.Size = new System.Drawing.Size(471, 325);
			this.eventListGrid.TabIndex = 13;
			this.eventListGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.commitValue);
			// 
			// Resource
			// 
			this.Resource.HeaderText = "Ресурси";
			this.Resource.Name = "Resource";
			// 
			// Description
			// 
			this.Description.HeaderText = "Опис";
			this.Description.Name = "Description";
			// 
			// Value
			// 
			this.Value.HeaderText = "Кількість";
			this.Value.Name = "Value";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(14, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(87, 13);
			this.label1.TabIndex = 12;
			this.label1.Text = "Перелік заходів";
			// 
			// eventsLB
			// 
			this.eventsLB.FormattingEnabled = true;
			this.eventsLB.Location = new System.Drawing.Point(17, 73);
			this.eventsLB.Name = "eventsLB";
			this.eventsLB.Size = new System.Drawing.Size(160, 277);
			this.eventsLB.TabIndex = 11;
			this.eventsLB.SelectedIndexChanged += new System.EventHandler(this.eventsLB_SelectedIndexChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(351, 9);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(130, 13);
			this.label3.TabIndex = 18;
			this.label3.Text = "Перелік ресурсів заходу";
			// 
			// btnSearchTemplate
			// 
			this.btnSearchTemplate.Location = new System.Drawing.Point(154, 24);
			this.btnSearchTemplate.Name = "btnSearchTemplate";
			this.btnSearchTemplate.Size = new System.Drawing.Size(23, 22);
			this.btnSearchTemplate.TabIndex = 20;
			this.btnSearchTemplate.Tag = "1";
			this.btnSearchTemplate.UseVisualStyleBackColor = true;
			this.btnSearchTemplate.Click += new System.EventHandler(this.FindButton_Click);
			// 
			// txtBxTemplate
			// 
			this.txtBxTemplate.Location = new System.Drawing.Point(17, 25);
			this.txtBxTemplate.Name = "txtBxTemplate";
			this.txtBxTemplate.Size = new System.Drawing.Size(135, 20);
			this.txtBxTemplate.TabIndex = 19;
			this.txtBxTemplate.Tag = "";
			// 
			// btnRes
			// 
			this.btnRes.Location = new System.Drawing.Point(321, 24);
			this.btnRes.Name = "btnRes";
			this.btnRes.Size = new System.Drawing.Size(23, 22);
			this.btnRes.TabIndex = 22;
			this.btnRes.Tag = "2";
			this.btnRes.UseVisualStyleBackColor = true;
			this.btnRes.Click += new System.EventHandler(this.FindButton_Click);
			// 
			// txtBxRes
			// 
			this.txtBxRes.Location = new System.Drawing.Point(183, 25);
			this.txtBxRes.Name = "txtBxRes";
			this.txtBxRes.Size = new System.Drawing.Size(138, 20);
			this.txtBxRes.TabIndex = 21;
			this.txtBxRes.Tag = "";
			// 
			// findIssueCB
			// 
			this.findIssueCB.FormattingEnabled = true;
			this.findIssueCB.Location = new System.Drawing.Point(17, 47);
			this.findIssueCB.Name = "findIssueCB";
			this.findIssueCB.Size = new System.Drawing.Size(160, 21);
			this.findIssueCB.TabIndex = 23;
			this.findIssueCB.Text = "Оберіть задачу";
			this.toolTip1.SetToolTip(this.findIssueCB, "Фільтрування заходів по задачам");
			this.findIssueCB.SelectedIndexChanged += new System.EventHandler(this.findIssueCB_SelectedIndexChanged);
			// 
			// startTutorial
			// 
			this.startTutorial.AutoSize = true;
			this.startTutorial.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.startTutorial.Location = new System.Drawing.Point(806, 2);
			this.startTutorial.Name = "startTutorial";
			this.startTutorial.Size = new System.Drawing.Size(18, 20);
			this.startTutorial.TabIndex = 24;
			this.startTutorial.Text = "?";
			this.startTutorial.Click += new System.EventHandler(this.startTutorial_Click);
			this.startTutorial.MouseEnter += new System.EventHandler(this.startTutorial_MouseEnter);
			this.startTutorial.MouseLeave += new System.EventHandler(this.startTutorial_MouseLeave);
			// 
			// AlterEventForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(827, 461);
			this.Controls.Add(this.startTutorial);
			this.Controls.Add(this.findIssueCB);
			this.Controls.Add(this.btnRes);
			this.Controls.Add(this.txtBxRes);
			this.Controls.Add(this.btnSearchTemplate);
			this.Controls.Add(this.txtBxTemplate);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.resLB);
			this.Controls.Add(this.alterGB);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.eventListGrid);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.eventsLB);
			this.Margin = new System.Windows.Forms.Padding(2);
			this.Name = "AlterEventForm";
			this.Text = "Редагування заходу";
			this.contextMenuRes.ResumeLayout(false);
			this.alterGB.ResumeLayout(false);
			this.alterGB.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.eventListGrid)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox resLB;
        private System.Windows.Forms.GroupBox alterGB;
        private System.Windows.Forms.ComboBox issuesCB;
        private System.Windows.Forms.TextBox descTB;
        private System.Windows.Forms.Button addBtn;
        private System.Windows.Forms.TextBox evNameTB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView eventListGrid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox eventsLB;
        private System.Windows.Forms.Button delBtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSearchTemplate;
        private System.Windows.Forms.TextBox txtBxTemplate;
        private System.Windows.Forms.Button btnRes;
        private System.Windows.Forms.TextBox txtBxRes;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Resource;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.ComboBox findIssueCB;
        private System.Windows.Forms.ContextMenuStrip contextMenuRes;
        private System.Windows.Forms.ToolStripMenuItem додатиНовийРесурсToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem переглядІнформаціїПроРесурсToolStripMenuItem;
		private System.Windows.Forms.Label startTutorial;
	}
}