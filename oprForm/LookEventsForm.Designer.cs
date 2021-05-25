namespace oprForm
{
	partial class LookEventsForm
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

            db.Dispose();

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
			this.eventListGrid = new System.Windows.Forms.DataGridView();
			this.Resource = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.unitsCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.priceCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.totalPriceCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.onlyDisCB = new System.Windows.Forms.CheckBox();
			this.lawyerCheck = new System.Windows.Forms.CheckBox();
			this.dmCheck = new System.Windows.Forms.CheckBox();
			this.label8 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.expertsLB = new System.Windows.Forms.ListBox();
			this.EventDocsFilterButton = new System.Windows.Forms.Button();
			this.docsLB = new System.Windows.Forms.ListBox();
			this.IssueDocsFilterButton = new System.Windows.Forms.Button();
			this.disaproveBtn = new System.Windows.Forms.Button();
			this.approveBtn = new System.Windows.Forms.Button();
			this.button5 = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.eventsLB = new System.Windows.Forms.ListBox();
			this.label15 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.button2 = new System.Windows.Forms.Button();
			this.findIssueCondTB = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.approveGB = new System.Windows.Forms.GroupBox();
			this.previousBtn = new System.Windows.Forms.Button();
			this.nextIssueBtn = new System.Windows.Forms.Button();
			this.TemaTextBox = new System.Windows.Forms.TextBox();
			this.label12 = new System.Windows.Forms.Label();
			this.issueListBtn = new System.Windows.Forms.Button();
			this.issueDescTB = new System.Windows.Forms.TextBox();
			this.issueTB = new System.Windows.Forms.TextBox();
			this.issueCostTB = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.issueLbl = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.issuesLB = new System.Windows.Forms.ListBox();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.cmbBxEventName = new System.Windows.Forms.ComboBox();
			this.txtBxTask = new System.Windows.Forms.TextBox();
			this.label13 = new System.Windows.Forms.Label();
			this.EventCostTextBox = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.EventNameTextBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.EventDescTextBox = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.DocumentsSortToolTip = new System.Windows.Forms.ToolTip(this.components);
			this.DocContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.OpenInBrowserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.startTutorial = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.eventListGrid)).BeginInit();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.approveGB.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.DocContextMenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// eventListGrid
			// 
			this.eventListGrid.AllowUserToAddRows = false;
			this.eventListGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.eventListGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
			this.eventListGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.eventListGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Resource,
            this.Description,
            this.Value,
            this.unitsCol,
            this.priceCol,
            this.totalPriceCol});
			this.eventListGrid.Location = new System.Drawing.Point(7, 175);
			this.eventListGrid.Margin = new System.Windows.Forms.Padding(4);
			this.eventListGrid.Name = "eventListGrid";
			this.eventListGrid.ReadOnly = true;
			this.eventListGrid.Size = new System.Drawing.Size(846, 339);
			this.eventListGrid.TabIndex = 10;
			// 
			// Resource
			// 
			this.Resource.HeaderText = "Назва";
			this.Resource.Name = "Resource";
			this.Resource.ReadOnly = true;
			// 
			// Description
			// 
			this.Description.HeaderText = "Опис";
			this.Description.Name = "Description";
			this.Description.ReadOnly = true;
			// 
			// Value
			// 
			this.Value.HeaderText = "Кількість";
			this.Value.Name = "Value";
			this.Value.ReadOnly = true;
			// 
			// unitsCol
			// 
			this.unitsCol.HeaderText = "Одиниці виміру";
			this.unitsCol.Name = "unitsCol";
			this.unitsCol.ReadOnly = true;
			// 
			// priceCol
			// 
			this.priceCol.HeaderText = "Ціна";
			this.priceCol.Name = "priceCol";
			this.priceCol.ReadOnly = true;
			// 
			// totalPriceCol
			// 
			this.totalPriceCol.HeaderText = "Вартість";
			this.totalPriceCol.Name = "totalPriceCol";
			this.totalPriceCol.ReadOnly = true;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.tabControl1.Location = new System.Drawing.Point(12, 18);
			this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(868, 534);
			this.tabControl1.TabIndex = 31;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.groupBox1);
			this.tabPage1.Controls.Add(this.label15);
			this.tabPage1.Controls.Add(this.label14);
			this.tabPage1.Controls.Add(this.button2);
			this.tabPage1.Controls.Add(this.findIssueCondTB);
			this.tabPage1.Controls.Add(this.label10);
			this.tabPage1.Controls.Add(this.approveGB);
			this.tabPage1.Controls.Add(this.issuesLB);
			this.tabPage1.Location = new System.Drawing.Point(4, 24);
			this.tabPage1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.tabPage1.Size = new System.Drawing.Size(860, 506);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Головна";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.groupBox2);
			this.groupBox1.Controls.Add(this.label8);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.expertsLB);
			this.groupBox1.Controls.Add(this.EventDocsFilterButton);
			this.groupBox1.Controls.Add(this.docsLB);
			this.groupBox1.Controls.Add(this.IssueDocsFilterButton);
			this.groupBox1.Controls.Add(this.disaproveBtn);
			this.groupBox1.Controls.Add(this.approveBtn);
			this.groupBox1.Controls.Add(this.button5);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.eventsLB);
			this.groupBox1.Location = new System.Drawing.Point(8, 199);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(838, 316);
			this.groupBox1.TabIndex = 42;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Відомості про заходи";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.onlyDisCB);
			this.groupBox2.Controls.Add(this.lawyerCheck);
			this.groupBox2.Controls.Add(this.dmCheck);
			this.groupBox2.Location = new System.Drawing.Point(11, 20);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(621, 57);
			this.groupBox2.TabIndex = 0;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Фільтрування заходів";
			// 
			// onlyDisCB
			// 
			this.onlyDisCB.AutoSize = true;
			this.onlyDisCB.Location = new System.Drawing.Point(7, 21);
			this.onlyDisCB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.onlyDisCB.Name = "onlyDisCB";
			this.onlyDisCB.Size = new System.Drawing.Size(137, 19);
			this.onlyDisCB.TabIndex = 22;
			this.onlyDisCB.Text = "Всі не затверждені";
			this.onlyDisCB.UseVisualStyleBackColor = true;
			this.onlyDisCB.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
			// 
			// lawyerCheck
			// 
			this.lawyerCheck.AutoSize = true;
			this.lawyerCheck.Enabled = false;
			this.lawyerCheck.Location = new System.Drawing.Point(205, 21);
			this.lawyerCheck.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.lawyerCheck.Name = "lawyerCheck";
			this.lawyerCheck.Size = new System.Drawing.Size(163, 19);
			this.lawyerCheck.TabIndex = 32;
			this.lawyerCheck.Text = "Підтвердження юриста";
			this.lawyerCheck.UseVisualStyleBackColor = true;
			// 
			// dmCheck
			// 
			this.dmCheck.AutoSize = true;
			this.dmCheck.Enabled = false;
			this.dmCheck.Location = new System.Drawing.Point(406, 21);
			this.dmCheck.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.dmCheck.Name = "dmCheck";
			this.dmCheck.Size = new System.Drawing.Size(178, 19);
			this.dmCheck.TabIndex = 31;
			this.dmCheck.Text = "Підтвердження аналітика";
			this.dmCheck.UseVisualStyleBackColor = true;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(14, 93);
			this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(113, 15);
			this.label8.TabIndex = 27;
			this.label8.Text = "Перелік експертів";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(521, 93);
			this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(143, 15);
			this.label7.TabIndex = 24;
			this.label7.Text = "Перелік юр. документів";
			// 
			// expertsLB
			// 
			this.expertsLB.FormattingEnabled = true;
			this.expertsLB.ItemHeight = 15;
			this.expertsLB.Location = new System.Drawing.Point(11, 112);
			this.expertsLB.Margin = new System.Windows.Forms.Padding(4);
			this.expertsLB.Name = "expertsLB";
			this.expertsLB.Size = new System.Drawing.Size(200, 199);
			this.expertsLB.TabIndex = 25;
			this.expertsLB.SelectedIndexChanged += new System.EventHandler(this.expertsLB_SelectedIndexChanged);
			// 
			// EventDocsFilterButton
			// 
			this.EventDocsFilterButton.Enabled = false;
			this.EventDocsFilterButton.Location = new System.Drawing.Point(671, 88);
			this.EventDocsFilterButton.Name = "EventDocsFilterButton";
			this.EventDocsFilterButton.Size = new System.Drawing.Size(75, 23);
			this.EventDocsFilterButton.TabIndex = 37;
			this.EventDocsFilterButton.Text = "По заходу";
			this.DocumentsSortToolTip.SetToolTip(this.EventDocsFilterButton, "Відображає документи для вибраного заходу");
			this.EventDocsFilterButton.UseVisualStyleBackColor = true;
			this.EventDocsFilterButton.Click += new System.EventHandler(this.EventDocsSortButton_Click);
			// 
			// docsLB
			// 
			this.docsLB.FormattingEnabled = true;
			this.docsLB.ItemHeight = 15;
			this.docsLB.Location = new System.Drawing.Point(524, 112);
			this.docsLB.Margin = new System.Windows.Forms.Padding(4);
			this.docsLB.Name = "docsLB";
			this.docsLB.Size = new System.Drawing.Size(300, 199);
			this.docsLB.TabIndex = 23;
			this.docsLB.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.docsLB_MouseDoubleClick);
			this.docsLB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.docsLB_MouseDown);
			// 
			// IssueDocsFilterButton
			// 
			this.IssueDocsFilterButton.Enabled = false;
			this.IssueDocsFilterButton.Location = new System.Drawing.Point(748, 88);
			this.IssueDocsFilterButton.Name = "IssueDocsFilterButton";
			this.IssueDocsFilterButton.Size = new System.Drawing.Size(75, 23);
			this.IssueDocsFilterButton.TabIndex = 37;
			this.IssueDocsFilterButton.Text = "По задачі";
			this.DocumentsSortToolTip.SetToolTip(this.IssueDocsFilterButton, "Відображає документи пов\'язані зі всією задачею");
			this.IssueDocsFilterButton.UseVisualStyleBackColor = true;
			this.IssueDocsFilterButton.Click += new System.EventHandler(this.IssueDocsSortButton_Click);
			// 
			// disaproveBtn
			// 
			this.disaproveBtn.Location = new System.Drawing.Point(734, 27);
			this.disaproveBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.disaproveBtn.Name = "disaproveBtn";
			this.disaproveBtn.Size = new System.Drawing.Size(90, 50);
			this.disaproveBtn.TabIndex = 15;
			this.disaproveBtn.Text = "Відхилити захід";
			this.disaproveBtn.UseVisualStyleBackColor = true;
			this.disaproveBtn.Click += new System.EventHandler(this.disaproveBtn_Click);
			// 
			// approveBtn
			// 
			this.approveBtn.Location = new System.Drawing.Point(638, 27);
			this.approveBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.approveBtn.Name = "approveBtn";
			this.approveBtn.Size = new System.Drawing.Size(90, 50);
			this.approveBtn.TabIndex = 14;
			this.approveBtn.Text = "Підтвердити захід";
			this.approveBtn.UseVisualStyleBackColor = true;
			this.approveBtn.Click += new System.EventHandler(this.approveBtn_Click);
			// 
			// button5
			// 
			this.button5.Location = new System.Drawing.Point(317, 89);
			this.button5.Margin = new System.Windows.Forms.Padding(4);
			this.button5.Name = "button5";
			this.button5.Size = new System.Drawing.Size(22, 22);
			this.button5.TabIndex = 38;
			this.button5.Text = "+";
			this.button5.UseVisualStyleBackColor = true;
			this.button5.Click += new System.EventHandler(this.Button5_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(217, 93);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(99, 15);
			this.label1.TabIndex = 9;
			this.label1.Text = "Перелік заходiв";
			// 
			// eventsLB
			// 
			this.eventsLB.FormattingEnabled = true;
			this.eventsLB.ItemHeight = 15;
			this.eventsLB.Location = new System.Drawing.Point(216, 112);
			this.eventsLB.Margin = new System.Windows.Forms.Padding(4);
			this.eventsLB.Name = "eventsLB";
			this.eventsLB.Size = new System.Drawing.Size(300, 199);
			this.eventsLB.TabIndex = 8;
			this.eventsLB.SelectedIndexChanged += new System.EventHandler(this.eventsLB_SelectedIndexChanged);
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.Location = new System.Drawing.Point(212, 36);
			this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(228, 15);
			this.label15.TabIndex = 41;
			this.label15.Text = "Загальні відомості про обрану задачу:";
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(8, 11);
			this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(112, 15);
			this.label14.TabIndex = 40;
			this.label14.Text = "Пошук по задачам";
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(674, 7);
			this.button2.Margin = new System.Windows.Forms.Padding(4);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(23, 23);
			this.button2.TabIndex = 23;
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.Button2_Click);
			// 
			// findIssueCondTB
			// 
			this.findIssueCondTB.Location = new System.Drawing.Point(131, 8);
			this.findIssueCondTB.Margin = new System.Windows.Forms.Padding(4);
			this.findIssueCondTB.Name = "findIssueCondTB";
			this.findIssueCondTB.Size = new System.Drawing.Size(542, 21);
			this.findIssueCondTB.TabIndex = 33;
			this.findIssueCondTB.Click += new System.EventHandler(this.findIssueCondTB_Click);
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(7, 36);
			this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(156, 15);
			this.label10.TabIndex = 28;
			this.label10.Text = "Результати пошуку задач:";
			// 
			// approveGB
			// 
			this.approveGB.Controls.Add(this.previousBtn);
			this.approveGB.Controls.Add(this.nextIssueBtn);
			this.approveGB.Controls.Add(this.TemaTextBox);
			this.approveGB.Controls.Add(this.label12);
			this.approveGB.Controls.Add(this.issueListBtn);
			this.approveGB.Controls.Add(this.issueDescTB);
			this.approveGB.Controls.Add(this.issueTB);
			this.approveGB.Controls.Add(this.issueCostTB);
			this.approveGB.Controls.Add(this.label9);
			this.approveGB.Controls.Add(this.issueLbl);
			this.approveGB.Controls.Add(this.label6);
			this.approveGB.Controls.Add(this.label3);
			this.approveGB.Location = new System.Drawing.Point(215, 48);
			this.approveGB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.approveGB.Name = "approveGB";
			this.approveGB.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.approveGB.Size = new System.Drawing.Size(631, 146);
			this.approveGB.TabIndex = 20;
			this.approveGB.TabStop = false;
			// 
			// previousBtn
			// 
			this.previousBtn.Enabled = false;
			this.previousBtn.Location = new System.Drawing.Point(404, 105);
			this.previousBtn.Name = "previousBtn";
			this.previousBtn.Size = new System.Drawing.Size(36, 28);
			this.previousBtn.TabIndex = 36;
			this.previousBtn.Text = "<";
			this.previousBtn.UseVisualStyleBackColor = true;
			this.previousBtn.Click += new System.EventHandler(this.PreviousIssueClick);
			// 
			// nextIssueBtn
			// 
			this.nextIssueBtn.Enabled = false;
			this.nextIssueBtn.Location = new System.Drawing.Point(446, 105);
			this.nextIssueBtn.Name = "nextIssueBtn";
			this.nextIssueBtn.Size = new System.Drawing.Size(36, 28);
			this.nextIssueBtn.TabIndex = 35;
			this.nextIssueBtn.Text = ">";
			this.nextIssueBtn.UseVisualStyleBackColor = true;
			this.nextIssueBtn.Click += new System.EventHandler(this.NextIssueClick);
			// 
			// TemaTextBox
			// 
			this.TemaTextBox.Location = new System.Drawing.Point(77, 109);
			this.TemaTextBox.Margin = new System.Windows.Forms.Padding(4);
			this.TemaTextBox.Name = "TemaTextBox";
			this.TemaTextBox.ReadOnly = true;
			this.TemaTextBox.Size = new System.Drawing.Size(225, 21);
			this.TemaTextBox.TabIndex = 34;
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(8, 112);
			this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(64, 15);
			this.label12.TabIndex = 33;
			this.label12.Text = "Тематика";
			// 
			// issueListBtn
			// 
			this.issueListBtn.Location = new System.Drawing.Point(527, 90);
			this.issueListBtn.Margin = new System.Windows.Forms.Padding(4);
			this.issueListBtn.Name = "issueListBtn";
			this.issueListBtn.Size = new System.Drawing.Size(89, 43);
			this.issueListBtn.TabIndex = 32;
			this.issueListBtn.Text = "Редагування задач";
			this.issueListBtn.UseVisualStyleBackColor = true;
			this.issueListBtn.Click += new System.EventHandler(this.IssueListClick);
			// 
			// issueDescTB
			// 
			this.issueDescTB.Location = new System.Drawing.Point(77, 46);
			this.issueDescTB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.issueDescTB.Name = "issueDescTB";
			this.issueDescTB.ReadOnly = true;
			this.issueDescTB.Size = new System.Drawing.Size(546, 21);
			this.issueDescTB.TabIndex = 26;
			// 
			// issueTB
			// 
			this.issueTB.Location = new System.Drawing.Point(77, 18);
			this.issueTB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.issueTB.Name = "issueTB";
			this.issueTB.ReadOnly = true;
			this.issueTB.Size = new System.Drawing.Size(546, 21);
			this.issueTB.TabIndex = 25;
			// 
			// issueCostTB
			// 
			this.issueCostTB.Location = new System.Drawing.Point(77, 76);
			this.issueCostTB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.issueCostTB.Name = "issueCostTB";
			this.issueCostTB.ReadOnly = true;
			this.issueCostTB.Size = new System.Drawing.Size(225, 21);
			this.issueCostTB.TabIndex = 24;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(7, 82);
			this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(62, 15);
			this.label9.TabIndex = 23;
			this.label9.Text = "Вартість ";
			// 
			// issueLbl
			// 
			this.issueLbl.AutoSize = true;
			this.issueLbl.Location = new System.Drawing.Point(173, 46);
			this.issueLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.issueLbl.Name = "issueLbl";
			this.issueLbl.Size = new System.Drawing.Size(0, 15);
			this.issueLbl.TabIndex = 21;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(5, 18);
			this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(49, 15);
			this.label6.TabIndex = 20;
			this.label6.Text = "Задача";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(7, 49);
			this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(36, 15);
			this.label3.TabIndex = 13;
			this.label3.Text = "Опис";
			// 
			// issuesLB
			// 
			this.issuesLB.FormattingEnabled = true;
			this.issuesLB.ItemHeight = 15;
			this.issuesLB.Location = new System.Drawing.Point(8, 55);
			this.issuesLB.Margin = new System.Windows.Forms.Padding(4);
			this.issuesLB.Name = "issuesLB";
			this.issuesLB.Size = new System.Drawing.Size(200, 139);
			this.issuesLB.TabIndex = 26;
			this.issuesLB.SelectedIndexChanged += new System.EventHandler(this.issuesLB_SelectedIndexChanged);
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.groupBox3);
			this.tabPage2.Controls.Add(this.label5);
			this.tabPage2.Controls.Add(this.eventListGrid);
			this.tabPage2.Location = new System.Drawing.Point(4, 24);
			this.tabPage2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.tabPage2.Size = new System.Drawing.Size(860, 506);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Ресурси";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.cmbBxEventName);
			this.groupBox3.Controls.Add(this.txtBxTask);
			this.groupBox3.Controls.Add(this.label13);
			this.groupBox3.Controls.Add(this.EventCostTextBox);
			this.groupBox3.Controls.Add(this.label4);
			this.groupBox3.Controls.Add(this.EventNameTextBox);
			this.groupBox3.Controls.Add(this.label2);
			this.groupBox3.Controls.Add(this.label11);
			this.groupBox3.Controls.Add(this.EventDescTextBox);
			this.groupBox3.Location = new System.Drawing.Point(7, 6);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(846, 147);
			this.groupBox3.TabIndex = 13;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Відомості про захід";
			// 
			// cmbBxEventName
			// 
			this.cmbBxEventName.FormattingEnabled = true;
			this.cmbBxEventName.Location = new System.Drawing.Point(112, 54);
			this.cmbBxEventName.Name = "cmbBxEventName";
			this.cmbBxEventName.Size = new System.Drawing.Size(728, 23);
			this.cmbBxEventName.TabIndex = 18;
			// 
			// txtBxTask
			// 
			this.txtBxTask.Enabled = false;
			this.txtBxTask.Location = new System.Drawing.Point(112, 23);
			this.txtBxTask.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.txtBxTask.Name = "txtBxTask";
			this.txtBxTask.ReadOnly = true;
			this.txtBxTask.Size = new System.Drawing.Size(728, 21);
			this.txtBxTask.TabIndex = 17;
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(8, 26);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(100, 15);
			this.label13.TabIndex = 16;
			this.label13.Text = "Поточна задача";
			// 
			// EventCostTextBox
			// 
			this.EventCostTextBox.Enabled = false;
			this.EventCostTextBox.Location = new System.Drawing.Point(112, 119);
			this.EventCostTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.EventCostTextBox.Name = "EventCostTextBox";
			this.EventCostTextBox.Size = new System.Drawing.Size(297, 21);
			this.EventCostTextBox.TabIndex = 14;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(8, 57);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(84, 15);
			this.label4.TabIndex = 1;
			this.label4.Text = "Назва заходу";
			// 
			// EventNameTextBox
			// 
			this.EventNameTextBox.Enabled = false;
			this.EventNameTextBox.Location = new System.Drawing.Point(112, 54);
			this.EventNameTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.EventNameTextBox.Name = "EventNameTextBox";
			this.EventNameTextBox.Size = new System.Drawing.Size(728, 21);
			this.EventNameTextBox.TabIndex = 15;
			this.EventNameTextBox.Visible = false;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(8, 90);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(77, 15);
			this.label2.TabIndex = 0;
			this.label2.Text = "Опис заходу";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(8, 122);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(100, 15);
			this.label11.TabIndex = 13;
			this.label11.Text = "Вартість заходу";
			// 
			// EventDescTextBox
			// 
			this.EventDescTextBox.Enabled = false;
			this.EventDescTextBox.Location = new System.Drawing.Point(112, 87);
			this.EventDescTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.EventDescTextBox.Name = "EventDescTextBox";
			this.EventDescTextBox.Size = new System.Drawing.Size(728, 21);
			this.EventDescTextBox.TabIndex = 14;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(6, 156);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(53, 15);
			this.label5.TabIndex = 12;
			this.label5.Text = "Ресурси";
			// 
			// DocContextMenuStrip
			// 
			this.DocContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenInBrowserToolStripMenuItem});
			this.DocContextMenuStrip.Name = "DocContextMenuStrip";
			this.DocContextMenuStrip.Size = new System.Drawing.Size(182, 26);
			// 
			// OpenInBrowserToolStripMenuItem
			// 
			this.OpenInBrowserToolStripMenuItem.Name = "OpenInBrowserToolStripMenuItem";
			this.OpenInBrowserToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
			this.OpenInBrowserToolStripMenuItem.Text = "Відкрити у браузері";
			this.OpenInBrowserToolStripMenuItem.Click += new System.EventHandler(this.OpenInBrowserToolStripMenuItem_Click);
			// 
			// startTutorial
			// 
			this.startTutorial.AutoSize = true;
			this.startTutorial.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.startTutorial.Location = new System.Drawing.Point(863, 3);
			this.startTutorial.Name = "startTutorial";
			this.startTutorial.Size = new System.Drawing.Size(18, 20);
			this.startTutorial.TabIndex = 69;
			this.startTutorial.Text = "?";
			this.startTutorial.Click += new System.EventHandler(this.startTutorial_Click);
			this.startTutorial.MouseEnter += new System.EventHandler(this.startTutorial_MouseEnter);
			this.startTutorial.MouseLeave += new System.EventHandler(this.startTutorial_MouseLeave);
			// 
			// LookEventsForm
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(884, 561);
			this.Controls.Add(this.startTutorial);
			this.Controls.Add(this.tabControl1);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.MaximumSize = new System.Drawing.Size(900, 600);
			this.MinimumSize = new System.Drawing.Size(900, 600);
			this.Name = "LookEventsForm";
			this.Text = "Перегляд задач";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.LookEventsForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.eventListGrid)).EndInit();
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.approveGB.ResumeLayout(false);
			this.approveGB.PerformLayout();
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.DocContextMenuStrip.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.DataGridView eventListGrid;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox EventCostTextBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox EventNameTextBox;
        private System.Windows.Forms.TextBox EventDescTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox findIssueCondTB;
        private System.Windows.Forms.CheckBox lawyerCheck;
        private System.Windows.Forms.CheckBox dmCheck;
        private System.Windows.Forms.ListBox eventsLB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox approveGB;
        private System.Windows.Forms.Button previousBtn;
        private System.Windows.Forms.Button nextIssueBtn;
        private System.Windows.Forms.TextBox TemaTextBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button issueListBtn;
        private System.Windows.Forms.TextBox issueDescTB;
        private System.Windows.Forms.TextBox issueTB;
        private System.Windows.Forms.TextBox issueCostTB;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label issueLbl;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox onlyDisCB;
        private System.Windows.Forms.Button approveBtn;
        private System.Windows.Forms.Button disaproveBtn;
        private System.Windows.Forms.ListBox issuesLB;
        private System.Windows.Forms.ListBox docsLB;
        private System.Windows.Forms.ListBox expertsLB;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button EventDocsFilterButton;
        private System.Windows.Forms.ToolTip DocumentsSortToolTip;
        private System.Windows.Forms.Button IssueDocsFilterButton;
        private System.Windows.Forms.ContextMenuStrip DocContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem OpenInBrowserToolStripMenuItem;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cmbBxEventName;
        private System.Windows.Forms.TextBox txtBxTask;
        private System.Windows.Forms.DataGridViewTextBoxColumn Resource;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.DataGridViewTextBoxColumn unitsCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn priceCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalPriceCol;
		private System.Windows.Forms.Label startTutorial;
	}
}