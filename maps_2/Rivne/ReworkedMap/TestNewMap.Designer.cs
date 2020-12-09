namespace Maps
{
    partial class TestNewMap
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

            Helpers.ImageCache.Clear();

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
            this.MapObjectContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.DeleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PanelSideMenu = new System.Windows.Forms.Panel();
            this._ElementsSideMenuPanel = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.PolygonNameTextBox = new System.Windows.Forms.TextBox();
            this.MarkerButton = new System.Windows.Forms.Button();
            this.TubeButton = new System.Windows.Forms.Button();
            this.PolygonButton = new System.Windows.Forms.Button();
            this.ShowButton = new System.Windows.Forms.Button();
            this.LoadButton = new System.Windows.Forms.Button();
            this.HideButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ClearButton = new System.Windows.Forms.Button();
            this.LayoutTextBox = new System.Windows.Forms.TextBox();
            this.ElementsSideMenuPanel = new System.Windows.Forms.Panel();
            this.AddItemTabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.MarkerSettingsButton = new System.Windows.Forms.Button();
            this.ShowAllExpertMarkerButton = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.OwnershipTypeComboBox = new System.Windows.Forms.ComboBox();
            this.EconomicActivityComboBox = new System.Windows.Forms.ComboBox();
            this.MarkerPictureBox = new System.Windows.Forms.PictureBox();
            this.ClearAllMarkersButton = new System.Windows.Forms.Button();
            this.ShowCurrentUserMarkerButton = new System.Windows.Forms.Button();
            this.ShowAllMarkersButton = new System.Windows.Forms.Button();
            this.SaveMarkerButton = new System.Windows.Forms.Button();
            this.AddMarkerButton = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.PolygonColorTypeComboBox = new System.Windows.Forms.ComboBox();
            this.PolygonColorPictureBox = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TransparentNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.PolygonSettingsButton = new System.Windows.Forms.Button();
            this.PolygonSaveButton = new System.Windows.Forms.Button();
            this.PolygonDrawButton = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.expertButtonTube = new System.Windows.Forms.Button();
            this.btnDeleteTube = new System.Windows.Forms.Button();
            this.btnClearTube = new System.Windows.Forms.Button();
            this.btnShowExpertsTubes = new System.Windows.Forms.Button();
            this.btnShowAllTubes = new System.Windows.Forms.Button();
            this.btnCancelTube = new System.Windows.Forms.Button();
            this.btnSaveTube = new System.Windows.Forms.Button();
            this.btnStartTube = new System.Windows.Forms.Button();
            this.ElementsSideMenuButton = new System.Windows.Forms.Button();
            this.FiltrationSideMenuPanel = new System.Windows.Forms.Panel();
            this.ShowAllLayoutButton = new System.Windows.Forms.Button();
            this.IssuesCheckBox = new System.Windows.Forms.CheckBox();
            this.EnvironmentsCheckBox = new System.Windows.Forms.CheckBox();
            this.HideAllLayoutButton = new System.Windows.Forms.Button();
            this.IssuesGroupBox = new System.Windows.Forms.GroupBox();
            this.IssuesComboBox = new System.Windows.Forms.ComboBox();
            this.EnvironmentsGroupBox = new System.Windows.Forms.GroupBox();
            this.EnvironmentComboBox = new System.Windows.Forms.ComboBox();
            this.ShowLayoutButton = new System.Windows.Forms.Button();
            this.FiltrationSideMenuButton = new System.Windows.Forms.Button();
            this.CompareSideMenuPanel = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.ComprasionItemsComboBox = new System.Windows.Forms.ComboBox();
            this.DeleteCompareItemButton = new System.Windows.Forms.Button();
            this.CompareButton = new System.Windows.Forms.Button();
            this.CompareSideMenuButton = new System.Windows.Forms.Button();
            this.FindSideMenuPanel = new System.Windows.Forms.Panel();
            this.FindByGroupBox = new System.Windows.Forms.GroupBox();
            this.tbSearch = new System.Windows.Forms.TextBox();
            this.cbSearch = new System.Windows.Forms.ComboBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnNormAll = new System.Windows.Forms.Button();
            this.CityGroupBox = new System.Windows.Forms.GroupBox();
            this.CitiesComboBox = new System.Windows.Forms.ComboBox();
            this.GoToCityButton = new System.Windows.Forms.Button();
            this.CoordinatesFindGroupBox = new System.Windows.Forms.GroupBox();
            this.LongitudeTextBox = new System.Windows.Forms.TextBox();
            this.FindByLngLtdButton = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.LattituteTextBox = new System.Windows.Forms.TextBox();
            this.FindSideMenuButton = new System.Windows.Forms.Button();
            this.CurrentActionStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.MainToolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.MainStatusStrip = new System.Windows.Forms.StatusStrip();
            this.DebugToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.HelpStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.ZoomPlus = new System.Windows.Forms.Button();
            this.ZoomMinus = new System.Windows.Forms.Button();
            this.CollapseButton = new System.Windows.Forms.Button();
            this.gMapControl = new GMap.NET.WindowsForms.GMapControl();
            this.MapObjectContextMenuStrip.SuspendLayout();
            this.PanelSideMenu.SuspendLayout();
            this._ElementsSideMenuPanel.SuspendLayout();
            this.ElementsSideMenuPanel.SuspendLayout();
            this.AddItemTabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MarkerPictureBox)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PolygonColorPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TransparentNumericUpDown)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.FiltrationSideMenuPanel.SuspendLayout();
            this.IssuesGroupBox.SuspendLayout();
            this.EnvironmentsGroupBox.SuspendLayout();
            this.CompareSideMenuPanel.SuspendLayout();
            this.FindSideMenuPanel.SuspendLayout();
            this.FindByGroupBox.SuspendLayout();
            this.CityGroupBox.SuspendLayout();
            this.CoordinatesFindGroupBox.SuspendLayout();
            this.MainStatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // MapObjectContextMenuStrip
            // 
            this.MapObjectContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DeleteToolStripMenuItem});
            this.MapObjectContextMenuStrip.Name = "contextMenuStrip1";
            this.MapObjectContextMenuStrip.Size = new System.Drawing.Size(119, 26);
            // 
            // DeleteToolStripMenuItem
            // 
            this.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem";
            this.DeleteToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.DeleteToolStripMenuItem.Text = "Удалить";
            this.DeleteToolStripMenuItem.Click += new System.EventHandler(this.DeleteToolStripMenuItem_Click);
            // 
            // PanelSideMenu
            // 
            this.PanelSideMenu.AutoScroll = true;
            this.PanelSideMenu.Controls.Add(this._ElementsSideMenuPanel);
            this.PanelSideMenu.Controls.Add(this.ElementsSideMenuPanel);
            this.PanelSideMenu.Controls.Add(this.ElementsSideMenuButton);
            this.PanelSideMenu.Controls.Add(this.FiltrationSideMenuPanel);
            this.PanelSideMenu.Controls.Add(this.FiltrationSideMenuButton);
            this.PanelSideMenu.Controls.Add(this.CompareSideMenuPanel);
            this.PanelSideMenu.Controls.Add(this.CompareSideMenuButton);
            this.PanelSideMenu.Controls.Add(this.FindSideMenuPanel);
            this.PanelSideMenu.Controls.Add(this.FindSideMenuButton);
            this.PanelSideMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.PanelSideMenu.Location = new System.Drawing.Point(0, 0);
            this.PanelSideMenu.Name = "PanelSideMenu";
            this.PanelSideMenu.Size = new System.Drawing.Size(310, 679);
            this.PanelSideMenu.TabIndex = 12;
            // 
            // _ElementsSideMenuPanel
            // 
            this._ElementsSideMenuPanel.Controls.Add(this.label5);
            this._ElementsSideMenuPanel.Controls.Add(this.PolygonNameTextBox);
            this._ElementsSideMenuPanel.Controls.Add(this.MarkerButton);
            this._ElementsSideMenuPanel.Controls.Add(this.TubeButton);
            this._ElementsSideMenuPanel.Controls.Add(this.PolygonButton);
            this._ElementsSideMenuPanel.Controls.Add(this.ShowButton);
            this._ElementsSideMenuPanel.Controls.Add(this.LoadButton);
            this._ElementsSideMenuPanel.Controls.Add(this.HideButton);
            this._ElementsSideMenuPanel.Controls.Add(this.label1);
            this._ElementsSideMenuPanel.Controls.Add(this.ClearButton);
            this._ElementsSideMenuPanel.Controls.Add(this.LayoutTextBox);
            this._ElementsSideMenuPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._ElementsSideMenuPanel.Location = new System.Drawing.Point(0, 929);
            this._ElementsSideMenuPanel.Name = "_ElementsSideMenuPanel";
            this._ElementsSideMenuPanel.Size = new System.Drawing.Size(293, 228);
            this._ElementsSideMenuPanel.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(133, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(132, 29);
            this.label5.TabIndex = 12;
            this.label5.Text = "Название полигона/трубопровода";
            this.label5.Visible = false;
            // 
            // PolygonNameTextBox
            // 
            this.PolygonNameTextBox.Location = new System.Drawing.Point(147, 90);
            this.PolygonNameTextBox.Name = "PolygonNameTextBox";
            this.PolygonNameTextBox.Size = new System.Drawing.Size(97, 20);
            this.PolygonNameTextBox.TabIndex = 11;
            this.PolygonNameTextBox.Visible = false;
            // 
            // MarkerButton
            // 
            this.MarkerButton.Location = new System.Drawing.Point(9, 6);
            this.MarkerButton.Name = "MarkerButton";
            this.MarkerButton.Size = new System.Drawing.Size(97, 23);
            this.MarkerButton.TabIndex = 2;
            this.MarkerButton.Text = "Маркер";
            this.MarkerButton.UseVisualStyleBackColor = true;
            this.MarkerButton.Click += new System.EventHandler(this.MarkerButton_Click);
            // 
            // TubeButton
            // 
            this.TubeButton.Location = new System.Drawing.Point(9, 64);
            this.TubeButton.Name = "TubeButton";
            this.TubeButton.Size = new System.Drawing.Size(97, 23);
            this.TubeButton.TabIndex = 4;
            this.TubeButton.Text = "Трубопровод";
            this.TubeButton.UseVisualStyleBackColor = true;
            this.TubeButton.Click += new System.EventHandler(this.TubeButton_Click);
            // 
            // PolygonButton
            // 
            this.PolygonButton.Location = new System.Drawing.Point(9, 35);
            this.PolygonButton.Name = "PolygonButton";
            this.PolygonButton.Size = new System.Drawing.Size(97, 23);
            this.PolygonButton.TabIndex = 3;
            this.PolygonButton.Text = "Полигон";
            this.PolygonButton.UseVisualStyleBackColor = true;
            this.PolygonButton.Click += new System.EventHandler(this.PolygonButton_Click);
            // 
            // ShowButton
            // 
            this.ShowButton.Location = new System.Drawing.Point(147, 145);
            this.ShowButton.Name = "ShowButton";
            this.ShowButton.Size = new System.Drawing.Size(97, 23);
            this.ShowButton.TabIndex = 10;
            this.ShowButton.Text = "Показать";
            this.ShowButton.UseVisualStyleBackColor = true;
            this.ShowButton.Click += new System.EventHandler(this.ShowButton_Click);
            // 
            // LoadButton
            // 
            this.LoadButton.Location = new System.Drawing.Point(9, 93);
            this.LoadButton.Name = "LoadButton";
            this.LoadButton.Size = new System.Drawing.Size(97, 23);
            this.LoadButton.TabIndex = 5;
            this.LoadButton.Text = "Загрузить";
            this.LoadButton.UseVisualStyleBackColor = true;
            this.LoadButton.Click += new System.EventHandler(this.LoadButton_Click);
            // 
            // HideButton
            // 
            this.HideButton.Location = new System.Drawing.Point(147, 116);
            this.HideButton.Name = "HideButton";
            this.HideButton.Size = new System.Drawing.Size(97, 23);
            this.HideButton.TabIndex = 9;
            this.HideButton.Text = "Скрыть";
            this.HideButton.UseVisualStyleBackColor = true;
            this.HideButton.Click += new System.EventHandler(this.HideButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(147, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Слой";
            // 
            // ClearButton
            // 
            this.ClearButton.Location = new System.Drawing.Point(147, 174);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(97, 23);
            this.ClearButton.TabIndex = 6;
            this.ClearButton.Text = "Очистить всё";
            this.ClearButton.UseVisualStyleBackColor = true;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // LayoutTextBox
            // 
            this.LayoutTextBox.Location = new System.Drawing.Point(147, 25);
            this.LayoutTextBox.Name = "LayoutTextBox";
            this.LayoutTextBox.Size = new System.Drawing.Size(97, 20);
            this.LayoutTextBox.TabIndex = 7;
            // 
            // ElementsSideMenuPanel
            // 
            this.ElementsSideMenuPanel.Controls.Add(this.AddItemTabControl);
            this.ElementsSideMenuPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.ElementsSideMenuPanel.Location = new System.Drawing.Point(0, 648);
            this.ElementsSideMenuPanel.Name = "ElementsSideMenuPanel";
            this.ElementsSideMenuPanel.Size = new System.Drawing.Size(293, 281);
            this.ElementsSideMenuPanel.TabIndex = 10;
            // 
            // AddItemTabControl
            // 
            this.AddItemTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AddItemTabControl.Controls.Add(this.tabPage1);
            this.AddItemTabControl.Controls.Add(this.tabPage2);
            this.AddItemTabControl.Controls.Add(this.tabPage3);
            this.AddItemTabControl.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AddItemTabControl.Location = new System.Drawing.Point(3, 6);
            this.AddItemTabControl.Name = "AddItemTabControl";
            this.AddItemTabControl.SelectedIndex = 0;
            this.AddItemTabControl.Size = new System.Drawing.Size(290, 267);
            this.AddItemTabControl.TabIndex = 58;
            this.AddItemTabControl.SelectedIndexChanged += new System.EventHandler(this.AddItemTabControl_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Highlight;
            this.tabPage1.Controls.Add(this.MarkerSettingsButton);
            this.tabPage1.Controls.Add(this.ShowAllExpertMarkerButton);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.OwnershipTypeComboBox);
            this.tabPage1.Controls.Add(this.EconomicActivityComboBox);
            this.tabPage1.Controls.Add(this.MarkerPictureBox);
            this.tabPage1.Controls.Add(this.ClearAllMarkersButton);
            this.tabPage1.Controls.Add(this.ShowCurrentUserMarkerButton);
            this.tabPage1.Controls.Add(this.ShowAllMarkersButton);
            this.tabPage1.Controls.Add(this.SaveMarkerButton);
            this.tabPage1.Controls.Add(this.AddMarkerButton);
            this.tabPage1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabPage1.ForeColor = System.Drawing.Color.White;
            this.tabPage1.Location = new System.Drawing.Point(4, 27);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(282, 236);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Маркер";
            // 
            // MarkerSettingsButton
            // 
            this.MarkerSettingsButton.Enabled = false;
            this.MarkerSettingsButton.FlatAppearance.BorderSize = 0;
            this.MarkerSettingsButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.MarkerSettingsButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(65)))), ((int)(((byte)(92)))));
            this.MarkerSettingsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MarkerSettingsButton.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MarkerSettingsButton.Location = new System.Drawing.Point(188, 103);
            this.MarkerSettingsButton.Name = "MarkerSettingsButton";
            this.MarkerSettingsButton.Size = new System.Drawing.Size(95, 42);
            this.MarkerSettingsButton.TabIndex = 72;
            this.MarkerSettingsButton.Text = "Налаштування маркеру";
            this.MarkerSettingsButton.UseVisualStyleBackColor = true;
            this.MarkerSettingsButton.Click += new System.EventHandler(this.MarkerSettingsButton_Click);
            // 
            // ShowAllExpertMarkerButton
            // 
            this.ShowAllExpertMarkerButton.FlatAppearance.BorderSize = 0;
            this.ShowAllExpertMarkerButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.ShowAllExpertMarkerButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(65)))), ((int)(((byte)(92)))));
            this.ShowAllExpertMarkerButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ShowAllExpertMarkerButton.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ShowAllExpertMarkerButton.Location = new System.Drawing.Point(9, 142);
            this.ShowAllExpertMarkerButton.Name = "ShowAllExpertMarkerButton";
            this.ShowAllExpertMarkerButton.Size = new System.Drawing.Size(93, 41);
            this.ShowAllExpertMarkerButton.TabIndex = 69;
            this.ShowAllExpertMarkerButton.Text = "Вiдобразити по експерту";
            this.ShowAllExpertMarkerButton.UseVisualStyleBackColor = true;
            this.ShowAllExpertMarkerButton.Click += new System.EventHandler(this.ShowAllExpertMarkerButton_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label9.Location = new System.Drawing.Point(6, 3);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(162, 15);
            this.label9.TabIndex = 68;
            this.label9.Text = "Вид економічної діяльності";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label8.Location = new System.Drawing.Point(6, 49);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(104, 15);
            this.label8.TabIndex = 66;
            this.label8.Text = "Форма власності";
            // 
            // OwnershipTypeComboBox
            // 
            this.OwnershipTypeComboBox.DisplayMember = "1";
            this.OwnershipTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.OwnershipTypeComboBox.FormattingEnabled = true;
            this.OwnershipTypeComboBox.Location = new System.Drawing.Point(6, 67);
            this.OwnershipTypeComboBox.Name = "OwnershipTypeComboBox";
            this.OwnershipTypeComboBox.Size = new System.Drawing.Size(186, 23);
            this.OwnershipTypeComboBox.TabIndex = 65;
            this.OwnershipTypeComboBox.ValueMember = "1";
            // 
            // EconomicActivityComboBox
            // 
            this.EconomicActivityComboBox.DisplayMember = "1";
            this.EconomicActivityComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EconomicActivityComboBox.FormattingEnabled = true;
            this.EconomicActivityComboBox.Location = new System.Drawing.Point(6, 21);
            this.EconomicActivityComboBox.Name = "EconomicActivityComboBox";
            this.EconomicActivityComboBox.Size = new System.Drawing.Size(186, 23);
            this.EconomicActivityComboBox.TabIndex = 67;
            this.EconomicActivityComboBox.ValueMember = "1";
            this.EconomicActivityComboBox.SelectedIndexChanged += new System.EventHandler(this.EconomicActivityComboBox_SelectedIndexChanged);
            // 
            // MarkerPictureBox
            // 
            this.MarkerPictureBox.Location = new System.Drawing.Point(201, 19);
            this.MarkerPictureBox.Name = "MarkerPictureBox";
            this.MarkerPictureBox.Size = new System.Drawing.Size(71, 71);
            this.MarkerPictureBox.TabIndex = 2;
            this.MarkerPictureBox.TabStop = false;
            // 
            // ClearAllMarkersButton
            // 
            this.ClearAllMarkersButton.FlatAppearance.BorderSize = 0;
            this.ClearAllMarkersButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.ClearAllMarkersButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(65)))), ((int)(((byte)(92)))));
            this.ClearAllMarkersButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ClearAllMarkersButton.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ClearAllMarkersButton.Location = new System.Drawing.Point(96, 189);
            this.ClearAllMarkersButton.Name = "ClearAllMarkersButton";
            this.ClearAllMarkersButton.Size = new System.Drawing.Size(96, 41);
            this.ClearAllMarkersButton.TabIndex = 64;
            this.ClearAllMarkersButton.Text = "Очистити всі";
            this.ClearAllMarkersButton.UseVisualStyleBackColor = true;
            this.ClearAllMarkersButton.Click += new System.EventHandler(this.ClearAllMarkersButton_Click);
            // 
            // ShowCurrentUserMarkerButton
            // 
            this.ShowCurrentUserMarkerButton.FlatAppearance.BorderSize = 0;
            this.ShowCurrentUserMarkerButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.ShowCurrentUserMarkerButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(65)))), ((int)(((byte)(92)))));
            this.ShowCurrentUserMarkerButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ShowCurrentUserMarkerButton.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ShowCurrentUserMarkerButton.Location = new System.Drawing.Point(86, 142);
            this.ShowCurrentUserMarkerButton.Name = "ShowCurrentUserMarkerButton";
            this.ShowCurrentUserMarkerButton.Size = new System.Drawing.Size(117, 41);
            this.ShowCurrentUserMarkerButton.TabIndex = 62;
            this.ShowCurrentUserMarkerButton.Text = "Вiдобразити додані вами";
            this.ShowCurrentUserMarkerButton.UseVisualStyleBackColor = true;
            this.ShowCurrentUserMarkerButton.Click += new System.EventHandler(this.ShowCurrentUserMarkerButton_Click);
            // 
            // ShowAllMarkersButton
            // 
            this.ShowAllMarkersButton.FlatAppearance.BorderSize = 0;
            this.ShowAllMarkersButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.ShowAllMarkersButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(65)))), ((int)(((byte)(92)))));
            this.ShowAllMarkersButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ShowAllMarkersButton.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ShowAllMarkersButton.Location = new System.Drawing.Point(197, 142);
            this.ShowAllMarkersButton.Name = "ShowAllMarkersButton";
            this.ShowAllMarkersButton.Size = new System.Drawing.Size(85, 41);
            this.ShowAllMarkersButton.TabIndex = 61;
            this.ShowAllMarkersButton.Text = "Вiдобразити всi";
            this.ShowAllMarkersButton.UseVisualStyleBackColor = true;
            this.ShowAllMarkersButton.Click += new System.EventHandler(this.ShowAllMarkersButton_Click);
            // 
            // SaveMarkerButton
            // 
            this.SaveMarkerButton.Enabled = false;
            this.SaveMarkerButton.FlatAppearance.BorderSize = 0;
            this.SaveMarkerButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.SaveMarkerButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(65)))), ((int)(((byte)(92)))));
            this.SaveMarkerButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SaveMarkerButton.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SaveMarkerButton.Location = new System.Drawing.Point(102, 113);
            this.SaveMarkerButton.Name = "SaveMarkerButton";
            this.SaveMarkerButton.Size = new System.Drawing.Size(75, 23);
            this.SaveMarkerButton.TabIndex = 59;
            this.SaveMarkerButton.Text = "Зберегти";
            this.SaveMarkerButton.UseVisualStyleBackColor = true;
            this.SaveMarkerButton.Click += new System.EventHandler(this.SaveMarkerButton_Click);
            // 
            // AddMarkerButton
            // 
            this.AddMarkerButton.FlatAppearance.BorderSize = 0;
            this.AddMarkerButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.AddMarkerButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(65)))), ((int)(((byte)(92)))));
            this.AddMarkerButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddMarkerButton.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AddMarkerButton.Location = new System.Drawing.Point(14, 113);
            this.AddMarkerButton.Name = "AddMarkerButton";
            this.AddMarkerButton.Size = new System.Drawing.Size(75, 23);
            this.AddMarkerButton.TabIndex = 58;
            this.AddMarkerButton.Text = "Додати";
            this.AddMarkerButton.UseVisualStyleBackColor = true;
            this.AddMarkerButton.Click += new System.EventHandler(this.AddMarkerButton_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Highlight;
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.PolygonColorTypeComboBox);
            this.tabPage2.Controls.Add(this.PolygonColorPictureBox);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.TransparentNumericUpDown);
            this.tabPage2.Controls.Add(this.PolygonSettingsButton);
            this.tabPage2.Controls.Add(this.PolygonSaveButton);
            this.tabPage2.Controls.Add(this.PolygonDrawButton);
            this.tabPage2.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabPage2.ForeColor = System.Drawing.Color.White;
            this.tabPage2.Location = new System.Drawing.Point(4, 27);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(282, 236);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Полiгон";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label4.Location = new System.Drawing.Point(5, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(145, 15);
            this.label4.TabIndex = 70;
            this.label4.Text = "Вибор кольору полігона";
            // 
            // PolygonColorTypeComboBox
            // 
            this.PolygonColorTypeComboBox.DisplayMember = "1";
            this.PolygonColorTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PolygonColorTypeComboBox.FormattingEnabled = true;
            this.PolygonColorTypeComboBox.Items.AddRange(new object[] {
            "Замалювання",
            "Границя"});
            this.PolygonColorTypeComboBox.Location = new System.Drawing.Point(7, 29);
            this.PolygonColorTypeComboBox.Name = "PolygonColorTypeComboBox";
            this.PolygonColorTypeComboBox.Size = new System.Drawing.Size(139, 23);
            this.PolygonColorTypeComboBox.TabIndex = 69;
            this.PolygonColorTypeComboBox.ValueMember = "1";
            this.PolygonColorTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.PolygonColorTypeComboBox_SelectedIndexChanged);
            // 
            // PolygonColorPictureBox
            // 
            this.PolygonColorPictureBox.Enabled = false;
            this.PolygonColorPictureBox.Location = new System.Drawing.Point(158, 6);
            this.PolygonColorPictureBox.Name = "PolygonColorPictureBox";
            this.PolygonColorPictureBox.Size = new System.Drawing.Size(113, 65);
            this.PolygonColorPictureBox.TabIndex = 51;
            this.PolygonColorPictureBox.TabStop = false;
            this.PolygonColorPictureBox.Click += new System.EventHandler(this.PolygonColorPictureBox_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 15);
            this.label3.TabIndex = 50;
            this.label3.Text = "Прозорість";
            // 
            // TransparentNumericUpDown
            // 
            this.TransparentNumericUpDown.Location = new System.Drawing.Point(12, 152);
            this.TransparentNumericUpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.TransparentNumericUpDown.Name = "TransparentNumericUpDown";
            this.TransparentNumericUpDown.Size = new System.Drawing.Size(77, 23);
            this.TransparentNumericUpDown.TabIndex = 49;
            this.TransparentNumericUpDown.ValueChanged += new System.EventHandler(this.TransparentNumericUpDown_ValueChanged);
            // 
            // PolygonSettingsButton
            // 
            this.PolygonSettingsButton.BackColor = System.Drawing.SystemColors.Highlight;
            this.PolygonSettingsButton.Enabled = false;
            this.PolygonSettingsButton.FlatAppearance.BorderSize = 0;
            this.PolygonSettingsButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.Fuchsia;
            this.PolygonSettingsButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.PolygonSettingsButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(65)))), ((int)(((byte)(92)))));
            this.PolygonSettingsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PolygonSettingsButton.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PolygonSettingsButton.Location = new System.Drawing.Point(180, 77);
            this.PolygonSettingsButton.Name = "PolygonSettingsButton";
            this.PolygonSettingsButton.Size = new System.Drawing.Size(98, 47);
            this.PolygonSettingsButton.TabIndex = 47;
            this.PolygonSettingsButton.Text = "Налаштування полігону";
            this.PolygonSettingsButton.UseVisualStyleBackColor = false;
            this.PolygonSettingsButton.Click += new System.EventHandler(this.PolygonSettingsButton_Click);
            // 
            // PolygonSaveButton
            // 
            this.PolygonSaveButton.BackColor = System.Drawing.SystemColors.Highlight;
            this.PolygonSaveButton.FlatAppearance.BorderSize = 0;
            this.PolygonSaveButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.Fuchsia;
            this.PolygonSaveButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.PolygonSaveButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(65)))), ((int)(((byte)(92)))));
            this.PolygonSaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PolygonSaveButton.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PolygonSaveButton.Location = new System.Drawing.Point(102, 84);
            this.PolygonSaveButton.Name = "PolygonSaveButton";
            this.PolygonSaveButton.Size = new System.Drawing.Size(77, 33);
            this.PolygonSaveButton.TabIndex = 46;
            this.PolygonSaveButton.Text = "Зберегти";
            this.PolygonSaveButton.UseVisualStyleBackColor = false;
            // 
            // PolygonDrawButton
            // 
            this.PolygonDrawButton.BackColor = System.Drawing.SystemColors.Highlight;
            this.PolygonDrawButton.FlatAppearance.BorderSize = 0;
            this.PolygonDrawButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.Fuchsia;
            this.PolygonDrawButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.PolygonDrawButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(65)))), ((int)(((byte)(92)))));
            this.PolygonDrawButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PolygonDrawButton.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PolygonDrawButton.Location = new System.Drawing.Point(12, 84);
            this.PolygonDrawButton.Name = "PolygonDrawButton";
            this.PolygonDrawButton.Size = new System.Drawing.Size(77, 33);
            this.PolygonDrawButton.TabIndex = 45;
            this.PolygonDrawButton.Text = "Почати";
            this.PolygonDrawButton.UseVisualStyleBackColor = false;
            this.PolygonDrawButton.Click += new System.EventHandler(this.PolygonDrawButton_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.SystemColors.Highlight;
            this.tabPage3.Controls.Add(this.expertButtonTube);
            this.tabPage3.Controls.Add(this.btnDeleteTube);
            this.tabPage3.Controls.Add(this.btnClearTube);
            this.tabPage3.Controls.Add(this.btnShowExpertsTubes);
            this.tabPage3.Controls.Add(this.btnShowAllTubes);
            this.tabPage3.Controls.Add(this.btnCancelTube);
            this.tabPage3.Controls.Add(this.btnSaveTube);
            this.tabPage3.Controls.Add(this.btnStartTube);
            this.tabPage3.Location = new System.Drawing.Point(4, 27);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(282, 236);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Водопровід";
            // 
            // expertButtonTube
            // 
            this.expertButtonTube.FlatAppearance.BorderSize = 0;
            this.expertButtonTube.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.expertButtonTube.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(65)))), ((int)(((byte)(92)))));
            this.expertButtonTube.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.expertButtonTube.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.expertButtonTube.ForeColor = System.Drawing.Color.White;
            this.expertButtonTube.Location = new System.Drawing.Point(37, 157);
            this.expertButtonTube.Name = "expertButtonTube";
            this.expertButtonTube.Size = new System.Drawing.Size(97, 39);
            this.expertButtonTube.TabIndex = 11;
            this.expertButtonTube.Text = "Додані даним експертом";
            this.expertButtonTube.UseVisualStyleBackColor = true;
            // 
            // btnDeleteTube
            // 
            this.btnDeleteTube.FlatAppearance.BorderSize = 0;
            this.btnDeleteTube.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnDeleteTube.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(65)))), ((int)(((byte)(92)))));
            this.btnDeleteTube.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteTube.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnDeleteTube.ForeColor = System.Drawing.Color.White;
            this.btnDeleteTube.Location = new System.Drawing.Point(161, 165);
            this.btnDeleteTube.Name = "btnDeleteTube";
            this.btnDeleteTube.Size = new System.Drawing.Size(81, 23);
            this.btnDeleteTube.TabIndex = 10;
            this.btnDeleteTube.Text = "Видалити";
            this.btnDeleteTube.UseVisualStyleBackColor = true;
            // 
            // btnClearTube
            // 
            this.btnClearTube.FlatAppearance.BorderSize = 0;
            this.btnClearTube.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnClearTube.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(65)))), ((int)(((byte)(92)))));
            this.btnClearTube.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearTube.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnClearTube.ForeColor = System.Drawing.Color.White;
            this.btnClearTube.Location = new System.Drawing.Point(201, 111);
            this.btnClearTube.Name = "btnClearTube";
            this.btnClearTube.Size = new System.Drawing.Size(75, 23);
            this.btnClearTube.TabIndex = 9;
            this.btnClearTube.Text = "Очистити";
            this.btnClearTube.UseVisualStyleBackColor = true;
            // 
            // btnShowExpertsTubes
            // 
            this.btnShowExpertsTubes.FlatAppearance.BorderSize = 0;
            this.btnShowExpertsTubes.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnShowExpertsTubes.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(65)))), ((int)(((byte)(92)))));
            this.btnShowExpertsTubes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowExpertsTubes.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnShowExpertsTubes.ForeColor = System.Drawing.Color.White;
            this.btnShowExpertsTubes.Location = new System.Drawing.Point(100, 111);
            this.btnShowExpertsTubes.Name = "btnShowExpertsTubes";
            this.btnShowExpertsTubes.Size = new System.Drawing.Size(95, 23);
            this.btnShowExpertsTubes.TabIndex = 8;
            this.btnShowExpertsTubes.Text = "Додані вами";
            this.btnShowExpertsTubes.UseVisualStyleBackColor = true;
            // 
            // btnShowAllTubes
            // 
            this.btnShowAllTubes.FlatAppearance.BorderSize = 0;
            this.btnShowAllTubes.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnShowAllTubes.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(65)))), ((int)(((byte)(92)))));
            this.btnShowAllTubes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowAllTubes.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnShowAllTubes.ForeColor = System.Drawing.Color.White;
            this.btnShowAllTubes.Location = new System.Drawing.Point(17, 111);
            this.btnShowAllTubes.Name = "btnShowAllTubes";
            this.btnShowAllTubes.Size = new System.Drawing.Size(75, 23);
            this.btnShowAllTubes.TabIndex = 3;
            this.btnShowAllTubes.Text = "Всі";
            this.btnShowAllTubes.UseVisualStyleBackColor = true;
            // 
            // btnCancelTube
            // 
            this.btnCancelTube.Enabled = false;
            this.btnCancelTube.FlatAppearance.BorderSize = 0;
            this.btnCancelTube.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnCancelTube.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(65)))), ((int)(((byte)(92)))));
            this.btnCancelTube.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelTube.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnCancelTube.ForeColor = System.Drawing.Color.White;
            this.btnCancelTube.Location = new System.Drawing.Point(194, 47);
            this.btnCancelTube.Name = "btnCancelTube";
            this.btnCancelTube.Size = new System.Drawing.Size(96, 23);
            this.btnCancelTube.TabIndex = 2;
            this.btnCancelTube.Text = "Відміна";
            this.btnCancelTube.UseVisualStyleBackColor = true;
            // 
            // btnSaveTube
            // 
            this.btnSaveTube.Enabled = false;
            this.btnSaveTube.FlatAppearance.BorderSize = 0;
            this.btnSaveTube.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnSaveTube.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(65)))), ((int)(((byte)(92)))));
            this.btnSaveTube.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveTube.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnSaveTube.ForeColor = System.Drawing.Color.White;
            this.btnSaveTube.Location = new System.Drawing.Point(112, 47);
            this.btnSaveTube.Name = "btnSaveTube";
            this.btnSaveTube.Size = new System.Drawing.Size(75, 23);
            this.btnSaveTube.TabIndex = 1;
            this.btnSaveTube.Text = "Зберегти";
            this.btnSaveTube.UseVisualStyleBackColor = true;
            // 
            // btnStartTube
            // 
            this.btnStartTube.FlatAppearance.BorderSize = 0;
            this.btnStartTube.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnStartTube.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(65)))), ((int)(((byte)(92)))));
            this.btnStartTube.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStartTube.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnStartTube.ForeColor = System.Drawing.Color.White;
            this.btnStartTube.Location = new System.Drawing.Point(17, 47);
            this.btnStartTube.Name = "btnStartTube";
            this.btnStartTube.Size = new System.Drawing.Size(75, 23);
            this.btnStartTube.TabIndex = 0;
            this.btnStartTube.Text = "Почати";
            this.btnStartTube.UseVisualStyleBackColor = true;
            // 
            // ElementsSideMenuButton
            // 
            this.ElementsSideMenuButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.ElementsSideMenuButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ElementsSideMenuButton.Location = new System.Drawing.Point(0, 618);
            this.ElementsSideMenuButton.Name = "ElementsSideMenuButton";
            this.ElementsSideMenuButton.Size = new System.Drawing.Size(293, 30);
            this.ElementsSideMenuButton.TabIndex = 9;
            this.ElementsSideMenuButton.Tag = "4";
            this.ElementsSideMenuButton.Text = "Управління елементів";
            this.ElementsSideMenuButton.UseVisualStyleBackColor = true;
            this.ElementsSideMenuButton.Click += new System.EventHandler(this.SideMenuButton_Click);
            // 
            // FiltrationSideMenuPanel
            // 
            this.FiltrationSideMenuPanel.Controls.Add(this.ShowAllLayoutButton);
            this.FiltrationSideMenuPanel.Controls.Add(this.IssuesCheckBox);
            this.FiltrationSideMenuPanel.Controls.Add(this.EnvironmentsCheckBox);
            this.FiltrationSideMenuPanel.Controls.Add(this.HideAllLayoutButton);
            this.FiltrationSideMenuPanel.Controls.Add(this.IssuesGroupBox);
            this.FiltrationSideMenuPanel.Controls.Add(this.EnvironmentsGroupBox);
            this.FiltrationSideMenuPanel.Controls.Add(this.ShowLayoutButton);
            this.FiltrationSideMenuPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.FiltrationSideMenuPanel.Location = new System.Drawing.Point(0, 397);
            this.FiltrationSideMenuPanel.Name = "FiltrationSideMenuPanel";
            this.FiltrationSideMenuPanel.Size = new System.Drawing.Size(293, 221);
            this.FiltrationSideMenuPanel.TabIndex = 8;
            // 
            // ShowAllLayoutButton
            // 
            this.ShowAllLayoutButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ShowAllLayoutButton.Location = new System.Drawing.Point(6, 190);
            this.ShowAllLayoutButton.Name = "ShowAllLayoutButton";
            this.ShowAllLayoutButton.Size = new System.Drawing.Size(282, 25);
            this.ShowAllLayoutButton.TabIndex = 83;
            this.ShowAllLayoutButton.Text = "Зобразити усі";
            this.ShowAllLayoutButton.UseVisualStyleBackColor = true;
            this.ShowAllLayoutButton.Click += new System.EventHandler(this.ShowAllLayoutButton_Click);
            // 
            // IssuesCheckBox
            // 
            this.IssuesCheckBox.AutoSize = true;
            this.IssuesCheckBox.Location = new System.Drawing.Point(6, 62);
            this.IssuesCheckBox.Name = "IssuesCheckBox";
            this.IssuesCheckBox.Size = new System.Drawing.Size(62, 17);
            this.IssuesCheckBox.TabIndex = 80;
            this.IssuesCheckBox.Text = "Задача";
            this.IssuesCheckBox.UseVisualStyleBackColor = true;
            // 
            // EnvironmentsCheckBox
            // 
            this.EnvironmentsCheckBox.AutoSize = true;
            this.EnvironmentsCheckBox.Location = new System.Drawing.Point(6, 6);
            this.EnvironmentsCheckBox.Name = "EnvironmentsCheckBox";
            this.EnvironmentsCheckBox.Size = new System.Drawing.Size(90, 17);
            this.EnvironmentsCheckBox.TabIndex = 78;
            this.EnvironmentsCheckBox.Text = "Середовище";
            this.EnvironmentsCheckBox.UseVisualStyleBackColor = true;
            // 
            // HideAllLayoutButton
            // 
            this.HideAllLayoutButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.HideAllLayoutButton.Location = new System.Drawing.Point(6, 159);
            this.HideAllLayoutButton.Name = "HideAllLayoutButton";
            this.HideAllLayoutButton.Size = new System.Drawing.Size(282, 25);
            this.HideAllLayoutButton.TabIndex = 82;
            this.HideAllLayoutButton.Text = "Сховати усі";
            this.HideAllLayoutButton.UseVisualStyleBackColor = true;
            this.HideAllLayoutButton.Click += new System.EventHandler(this.HideAllLayoutButton_Click);
            // 
            // IssuesGroupBox
            // 
            this.IssuesGroupBox.Controls.Add(this.IssuesComboBox);
            this.IssuesGroupBox.Location = new System.Drawing.Point(3, 64);
            this.IssuesGroupBox.Name = "IssuesGroupBox";
            this.IssuesGroupBox.Size = new System.Drawing.Size(282, 50);
            this.IssuesGroupBox.TabIndex = 81;
            this.IssuesGroupBox.TabStop = false;
            // 
            // IssuesComboBox
            // 
            this.IssuesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.IssuesComboBox.FormattingEnabled = true;
            this.IssuesComboBox.Location = new System.Drawing.Point(6, 19);
            this.IssuesComboBox.Name = "IssuesComboBox";
            this.IssuesComboBox.Size = new System.Drawing.Size(269, 21);
            this.IssuesComboBox.TabIndex = 1;
            // 
            // EnvironmentsGroupBox
            // 
            this.EnvironmentsGroupBox.Controls.Add(this.EnvironmentComboBox);
            this.EnvironmentsGroupBox.Location = new System.Drawing.Point(3, 8);
            this.EnvironmentsGroupBox.Name = "EnvironmentsGroupBox";
            this.EnvironmentsGroupBox.Size = new System.Drawing.Size(282, 50);
            this.EnvironmentsGroupBox.TabIndex = 79;
            this.EnvironmentsGroupBox.TabStop = false;
            // 
            // EnvironmentComboBox
            // 
            this.EnvironmentComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EnvironmentComboBox.FormattingEnabled = true;
            this.EnvironmentComboBox.Location = new System.Drawing.Point(8, 19);
            this.EnvironmentComboBox.Name = "EnvironmentComboBox";
            this.EnvironmentComboBox.Size = new System.Drawing.Size(269, 21);
            this.EnvironmentComboBox.TabIndex = 1;
            // 
            // ShowLayoutButton
            // 
            this.ShowLayoutButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ShowLayoutButton.Location = new System.Drawing.Point(6, 119);
            this.ShowLayoutButton.Name = "ShowLayoutButton";
            this.ShowLayoutButton.Size = new System.Drawing.Size(282, 25);
            this.ShowLayoutButton.TabIndex = 77;
            this.ShowLayoutButton.Text = "Відобразити вибране";
            this.ShowLayoutButton.UseVisualStyleBackColor = true;
            this.ShowLayoutButton.Click += new System.EventHandler(this.ShowLayoutButton_Click);
            // 
            // FiltrationSideMenuButton
            // 
            this.FiltrationSideMenuButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.FiltrationSideMenuButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FiltrationSideMenuButton.Location = new System.Drawing.Point(0, 367);
            this.FiltrationSideMenuButton.Name = "FiltrationSideMenuButton";
            this.FiltrationSideMenuButton.Size = new System.Drawing.Size(293, 30);
            this.FiltrationSideMenuButton.TabIndex = 7;
            this.FiltrationSideMenuButton.Tag = "3";
            this.FiltrationSideMenuButton.Text = "Фільтрація об\'єктів";
            this.FiltrationSideMenuButton.UseVisualStyleBackColor = true;
            this.FiltrationSideMenuButton.Click += new System.EventHandler(this.SideMenuButton_Click);
            // 
            // CompareSideMenuPanel
            // 
            this.CompareSideMenuPanel.Controls.Add(this.label2);
            this.CompareSideMenuPanel.Controls.Add(this.ComprasionItemsComboBox);
            this.CompareSideMenuPanel.Controls.Add(this.DeleteCompareItemButton);
            this.CompareSideMenuPanel.Controls.Add(this.CompareButton);
            this.CompareSideMenuPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.CompareSideMenuPanel.Location = new System.Drawing.Point(0, 299);
            this.CompareSideMenuPanel.Name = "CompareSideMenuPanel";
            this.CompareSideMenuPanel.Size = new System.Drawing.Size(293, 68);
            this.CompareSideMenuPanel.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 89;
            this.label2.Text = "Маркери";
            // 
            // ComprasionItemsComboBox
            // 
            this.ComprasionItemsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComprasionItemsComboBox.FormattingEnabled = true;
            this.ComprasionItemsComboBox.Location = new System.Drawing.Point(66, 11);
            this.ComprasionItemsComboBox.Name = "ComprasionItemsComboBox";
            this.ComprasionItemsComboBox.Size = new System.Drawing.Size(133, 21);
            this.ComprasionItemsComboBox.TabIndex = 86;
            // 
            // DeleteCompareItemButton
            // 
            this.DeleteCompareItemButton.Location = new System.Drawing.Point(212, 9);
            this.DeleteCompareItemButton.Name = "DeleteCompareItemButton";
            this.DeleteCompareItemButton.Size = new System.Drawing.Size(73, 23);
            this.DeleteCompareItemButton.TabIndex = 87;
            this.DeleteCompareItemButton.Text = "Видалити";
            this.DeleteCompareItemButton.UseVisualStyleBackColor = true;
            // 
            // CompareButton
            // 
            this.CompareButton.Location = new System.Drawing.Point(6, 38);
            this.CompareButton.Name = "CompareButton";
            this.CompareButton.Size = new System.Drawing.Size(279, 23);
            this.CompareButton.TabIndex = 88;
            this.CompareButton.Text = "Порівняти";
            this.CompareButton.UseVisualStyleBackColor = true;
            // 
            // CompareSideMenuButton
            // 
            this.CompareSideMenuButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.CompareSideMenuButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CompareSideMenuButton.Location = new System.Drawing.Point(0, 269);
            this.CompareSideMenuButton.Name = "CompareSideMenuButton";
            this.CompareSideMenuButton.Size = new System.Drawing.Size(293, 30);
            this.CompareSideMenuButton.TabIndex = 4;
            this.CompareSideMenuButton.Tag = "2";
            this.CompareSideMenuButton.Text = "Порівняння";
            this.CompareSideMenuButton.UseVisualStyleBackColor = true;
            this.CompareSideMenuButton.Click += new System.EventHandler(this.SideMenuButton_Click);
            // 
            // FindSideMenuPanel
            // 
            this.FindSideMenuPanel.Controls.Add(this.FindByGroupBox);
            this.FindSideMenuPanel.Controls.Add(this.CityGroupBox);
            this.FindSideMenuPanel.Controls.Add(this.CoordinatesFindGroupBox);
            this.FindSideMenuPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.FindSideMenuPanel.Location = new System.Drawing.Point(0, 30);
            this.FindSideMenuPanel.Name = "FindSideMenuPanel";
            this.FindSideMenuPanel.Size = new System.Drawing.Size(293, 239);
            this.FindSideMenuPanel.TabIndex = 1;
            // 
            // FindByGroupBox
            // 
            this.FindByGroupBox.Controls.Add(this.tbSearch);
            this.FindByGroupBox.Controls.Add(this.cbSearch);
            this.FindByGroupBox.Controls.Add(this.btnSearch);
            this.FindByGroupBox.Controls.Add(this.btnNormAll);
            this.FindByGroupBox.Location = new System.Drawing.Point(6, 124);
            this.FindByGroupBox.Name = "FindByGroupBox";
            this.FindByGroupBox.Size = new System.Drawing.Size(282, 110);
            this.FindByGroupBox.TabIndex = 88;
            this.FindByGroupBox.TabStop = false;
            this.FindByGroupBox.Text = "Пошук по";
            // 
            // tbSearch
            // 
            this.tbSearch.Location = new System.Drawing.Point(6, 50);
            this.tbSearch.Name = "tbSearch";
            this.tbSearch.Size = new System.Drawing.Size(273, 20);
            this.tbSearch.TabIndex = 72;
            // 
            // cbSearch
            // 
            this.cbSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSearch.FormattingEnabled = true;
            this.cbSearch.Items.AddRange(new object[] {
            "Назві",
            "Задачі",
            "Розрахунку",
            "Формулі"});
            this.cbSearch.Location = new System.Drawing.Point(6, 19);
            this.cbSearch.Name = "cbSearch";
            this.cbSearch.Size = new System.Drawing.Size(273, 21);
            this.cbSearch.TabIndex = 74;
            // 
            // btnSearch
            // 
            this.btnSearch.Enabled = false;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(65)))), ((int)(((byte)(92)))));
            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnSearch.Location = new System.Drawing.Point(6, 76);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(79, 23);
            this.btnSearch.TabIndex = 75;
            this.btnSearch.Text = "Пошук";
            this.btnSearch.UseVisualStyleBackColor = true;
            // 
            // btnNormAll
            // 
            this.btnNormAll.Location = new System.Drawing.Point(118, 76);
            this.btnNormAll.Name = "btnNormAll";
            this.btnNormAll.Size = new System.Drawing.Size(161, 23);
            this.btnNormAll.TabIndex = 77;
            this.btnNormAll.Text = "Всі результати нормування";
            this.btnNormAll.UseVisualStyleBackColor = true;
            // 
            // CityGroupBox
            // 
            this.CityGroupBox.Controls.Add(this.CitiesComboBox);
            this.CityGroupBox.Controls.Add(this.GoToCityButton);
            this.CityGroupBox.Location = new System.Drawing.Point(6, 3);
            this.CityGroupBox.Name = "CityGroupBox";
            this.CityGroupBox.Size = new System.Drawing.Size(282, 49);
            this.CityGroupBox.TabIndex = 86;
            this.CityGroupBox.TabStop = false;
            this.CityGroupBox.Text = "Перейти до міста";
            // 
            // CitiesComboBox
            // 
            this.CitiesComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.CitiesComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.CitiesComboBox.FormattingEnabled = true;
            this.CitiesComboBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.CitiesComboBox.Location = new System.Drawing.Point(3, 19);
            this.CitiesComboBox.Name = "CitiesComboBox";
            this.CitiesComboBox.Size = new System.Drawing.Size(198, 21);
            this.CitiesComboBox.TabIndex = 76;
            // 
            // GoToCityButton
            // 
            this.GoToCityButton.Location = new System.Drawing.Point(205, 18);
            this.GoToCityButton.Name = "GoToCityButton";
            this.GoToCityButton.Size = new System.Drawing.Size(74, 23);
            this.GoToCityButton.TabIndex = 56;
            this.GoToCityButton.Text = "Перейти";
            this.GoToCityButton.UseVisualStyleBackColor = true;
            this.GoToCityButton.Click += new System.EventHandler(this.GoToCityButton_Click);
            // 
            // CoordinatesFindGroupBox
            // 
            this.CoordinatesFindGroupBox.Controls.Add(this.LongitudeTextBox);
            this.CoordinatesFindGroupBox.Controls.Add(this.FindByLngLtdButton);
            this.CoordinatesFindGroupBox.Controls.Add(this.label6);
            this.CoordinatesFindGroupBox.Controls.Add(this.label7);
            this.CoordinatesFindGroupBox.Controls.Add(this.LattituteTextBox);
            this.CoordinatesFindGroupBox.Location = new System.Drawing.Point(6, 58);
            this.CoordinatesFindGroupBox.Name = "CoordinatesFindGroupBox";
            this.CoordinatesFindGroupBox.Size = new System.Drawing.Size(282, 60);
            this.CoordinatesFindGroupBox.TabIndex = 87;
            this.CoordinatesFindGroupBox.TabStop = false;
            this.CoordinatesFindGroupBox.Text = "Пошук по координатам";
            // 
            // LongitudeTextBox
            // 
            this.LongitudeTextBox.Location = new System.Drawing.Point(103, 35);
            this.LongitudeTextBox.MaxLength = 9;
            this.LongitudeTextBox.Name = "LongitudeTextBox";
            this.LongitudeTextBox.Size = new System.Drawing.Size(70, 20);
            this.LongitudeTextBox.TabIndex = 66;
            // 
            // FindByLngLtdButton
            // 
            this.FindByLngLtdButton.Location = new System.Drawing.Point(202, 35);
            this.FindByLngLtdButton.Name = "FindByLngLtdButton";
            this.FindByLngLtdButton.Size = new System.Drawing.Size(77, 20);
            this.FindByLngLtdButton.TabIndex = 58;
            this.FindByLngLtdButton.Text = "Пошук";
            this.FindByLngLtdButton.UseVisualStyleBackColor = true;
            this.FindByLngLtdButton.Click += new System.EventHandler(this.FindByLngLtdButton_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 13);
            this.label6.TabIndex = 61;
            this.label6.Text = "широта";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(100, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 13);
            this.label7.TabIndex = 62;
            this.label7.Text = "довгота";
            // 
            // LattituteTextBox
            // 
            this.LattituteTextBox.Location = new System.Drawing.Point(6, 35);
            this.LattituteTextBox.MaxLength = 9;
            this.LattituteTextBox.Name = "LattituteTextBox";
            this.LattituteTextBox.Size = new System.Drawing.Size(70, 20);
            this.LattituteTextBox.TabIndex = 65;
            // 
            // FindSideMenuButton
            // 
            this.FindSideMenuButton.BackColor = System.Drawing.SystemColors.Control;
            this.FindSideMenuButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.FindSideMenuButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FindSideMenuButton.Location = new System.Drawing.Point(0, 0);
            this.FindSideMenuButton.Name = "FindSideMenuButton";
            this.FindSideMenuButton.Size = new System.Drawing.Size(293, 30);
            this.FindSideMenuButton.TabIndex = 0;
            this.FindSideMenuButton.Tag = "1";
            this.FindSideMenuButton.Text = "Пошук";
            this.FindSideMenuButton.UseVisualStyleBackColor = false;
            this.FindSideMenuButton.Click += new System.EventHandler(this.SideMenuButton_Click);
            // 
            // CurrentActionStatusLabel
            // 
            this.CurrentActionStatusLabel.Name = "CurrentActionStatusLabel";
            this.CurrentActionStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // MainToolStripProgressBar
            // 
            this.MainToolStripProgressBar.Name = "MainToolStripProgressBar";
            this.MainToolStripProgressBar.Size = new System.Drawing.Size(100, 16);
            this.MainToolStripProgressBar.Visible = false;
            // 
            // MainStatusStrip
            // 
            this.MainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CurrentActionStatusLabel,
            this.MainToolStripProgressBar,
            this.DebugToolStripStatusLabel,
            this.HelpStatusLabel});
            this.MainStatusStrip.Location = new System.Drawing.Point(0, 679);
            this.MainStatusStrip.Name = "MainStatusStrip";
            this.MainStatusStrip.Size = new System.Drawing.Size(984, 22);
            this.MainStatusStrip.TabIndex = 11;
            this.MainStatusStrip.Text = "statusStrip1";
            // 
            // DebugToolStripStatusLabel
            // 
            this.DebugToolStripStatusLabel.Margin = new System.Windows.Forms.Padding(50, 3, 0, 2);
            this.DebugToolStripStatusLabel.Name = "DebugToolStripStatusLabel";
            this.DebugToolStripStatusLabel.Size = new System.Drawing.Size(44, 17);
            this.DebugToolStripStatusLabel.Text = "DEBUG";
            // 
            // HelpStatusLabel
            // 
            this.HelpStatusLabel.Margin = new System.Windows.Forms.Padding(350, 3, 0, 2);
            this.HelpStatusLabel.Name = "HelpStatusLabel";
            this.HelpStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // ZoomPlus
            // 
            this.ZoomPlus.Location = new System.Drawing.Point(316, 30);
            this.ZoomPlus.Name = "ZoomPlus";
            this.ZoomPlus.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ZoomPlus.Size = new System.Drawing.Size(24, 23);
            this.ZoomPlus.TabIndex = 67;
            this.ZoomPlus.Text = "+";
            this.ZoomPlus.UseVisualStyleBackColor = true;
            this.ZoomPlus.Click += new System.EventHandler(this.ZoomPlus_Click);
            // 
            // ZoomMinus
            // 
            this.ZoomMinus.Location = new System.Drawing.Point(316, 59);
            this.ZoomMinus.Name = "ZoomMinus";
            this.ZoomMinus.Size = new System.Drawing.Size(24, 23);
            this.ZoomMinus.TabIndex = 68;
            this.ZoomMinus.Text = "-";
            this.ZoomMinus.UseVisualStyleBackColor = true;
            this.ZoomMinus.Click += new System.EventHandler(this.ZoomMinus_Click);
            // 
            // CollapseButton
            // 
            this.CollapseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CollapseButton.Location = new System.Drawing.Point(316, 1);
            this.CollapseButton.Name = "CollapseButton";
            this.CollapseButton.Size = new System.Drawing.Size(24, 23);
            this.CollapseButton.TabIndex = 69;
            this.CollapseButton.Text = "_";
            this.CollapseButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.CollapseButton.UseVisualStyleBackColor = true;
            this.CollapseButton.Click += new System.EventHandler(this.CollapseButton_Click);
            // 
            // gMapControl
            // 
            this.gMapControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gMapControl.Bearing = 0F;
            this.gMapControl.CanDragMap = true;
            this.gMapControl.EmptyTileColor = System.Drawing.Color.Navy;
            this.gMapControl.GrayScaleMode = false;
            this.gMapControl.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gMapControl.LevelsKeepInMemmory = 5;
            this.gMapControl.Location = new System.Drawing.Point(316, 0);
            this.gMapControl.MarkersEnabled = true;
            this.gMapControl.MaxZoom = 2;
            this.gMapControl.MinZoom = 2;
            this.gMapControl.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionWithoutCenter;
            this.gMapControl.Name = "gMapControl";
            this.gMapControl.NegativeMode = false;
            this.gMapControl.PolygonsEnabled = true;
            this.gMapControl.RetryLoadTile = 0;
            this.gMapControl.RoutesEnabled = true;
            this.gMapControl.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gMapControl.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gMapControl.ShowTileGridLines = false;
            this.gMapControl.Size = new System.Drawing.Size(668, 676);
            this.gMapControl.TabIndex = 1;
            this.gMapControl.Zoom = 0D;
            this.gMapControl.OnMarkerClick += new GMap.NET.WindowsForms.MarkerClick(this.gMapControl_OnMarkerClick);
            this.gMapControl.OnPolygonClick += new GMap.NET.WindowsForms.PolygonClick(this.gMapControl_OnPolygonClick);
            this.gMapControl.OnRouteClick += new GMap.NET.WindowsForms.RouteClick(this.gMapControl_OnRouteClick);
            this.gMapControl.DoubleClick += new System.EventHandler(this.gMapControl_DoubleClick);
            this.gMapControl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gMapControl_KeyDown);
            this.gMapControl.KeyUp += new System.Windows.Forms.KeyEventHandler(this.gMapControl_KeyUp);
            this.gMapControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gMapControl_MouseDown);
            this.gMapControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gMapControl_MouseMove);
            this.gMapControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gMapControl_MouseUp);
            // 
            // TestNewMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 701);
            this.Controls.Add(this.CollapseButton);
            this.Controls.Add(this.ZoomMinus);
            this.Controls.Add(this.ZoomPlus);
            this.Controls.Add(this.PanelSideMenu);
            this.Controls.Add(this.MainStatusStrip);
            this.Controls.Add(this.gMapControl);
            this.MinimumSize = new System.Drawing.Size(700, 540);
            this.Name = "TestNewMap";
            this.Text = "Карта";
            this.MapObjectContextMenuStrip.ResumeLayout(false);
            this.PanelSideMenu.ResumeLayout(false);
            this._ElementsSideMenuPanel.ResumeLayout(false);
            this._ElementsSideMenuPanel.PerformLayout();
            this.ElementsSideMenuPanel.ResumeLayout(false);
            this.AddItemTabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MarkerPictureBox)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PolygonColorPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TransparentNumericUpDown)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.FiltrationSideMenuPanel.ResumeLayout(false);
            this.FiltrationSideMenuPanel.PerformLayout();
            this.IssuesGroupBox.ResumeLayout(false);
            this.EnvironmentsGroupBox.ResumeLayout(false);
            this.CompareSideMenuPanel.ResumeLayout(false);
            this.CompareSideMenuPanel.PerformLayout();
            this.FindSideMenuPanel.ResumeLayout(false);
            this.FindByGroupBox.ResumeLayout(false);
            this.FindByGroupBox.PerformLayout();
            this.CityGroupBox.ResumeLayout(false);
            this.CoordinatesFindGroupBox.ResumeLayout(false);
            this.CoordinatesFindGroupBox.PerformLayout();
            this.MainStatusStrip.ResumeLayout(false);
            this.MainStatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip MapObjectContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem DeleteToolStripMenuItem;
        private System.Windows.Forms.Panel PanelSideMenu;
        private System.Windows.Forms.Button FindSideMenuButton;
        private System.Windows.Forms.Button ElementsSideMenuButton;
        private System.Windows.Forms.Panel FiltrationSideMenuPanel;
        private System.Windows.Forms.Button FiltrationSideMenuButton;
        private System.Windows.Forms.Panel CompareSideMenuPanel;
        private System.Windows.Forms.Button CompareSideMenuButton;
        private System.Windows.Forms.Panel FindSideMenuPanel;
        private System.Windows.Forms.GroupBox FindByGroupBox;
        private System.Windows.Forms.TextBox tbSearch;
        private System.Windows.Forms.ComboBox cbSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnNormAll;
        private System.Windows.Forms.GroupBox CityGroupBox;
        private System.Windows.Forms.ComboBox CitiesComboBox;
        private System.Windows.Forms.Button GoToCityButton;
        private System.Windows.Forms.GroupBox CoordinatesFindGroupBox;
        private System.Windows.Forms.TextBox LongitudeTextBox;
        private System.Windows.Forms.Button FindByLngLtdButton;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox LattituteTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox ComprasionItemsComboBox;
        private System.Windows.Forms.Button DeleteCompareItemButton;
        private System.Windows.Forms.Button CompareButton;
        private System.Windows.Forms.ComboBox EnvironmentComboBox;
        private System.Windows.Forms.Panel _ElementsSideMenuPanel;
        private System.Windows.Forms.Button MarkerButton;
        private System.Windows.Forms.Button TubeButton;
        private System.Windows.Forms.Button PolygonButton;
        private System.Windows.Forms.Button ShowButton;
        private System.Windows.Forms.Button LoadButton;
        private System.Windows.Forms.Button HideButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ClearButton;
        private System.Windows.Forms.TextBox LayoutTextBox;
        private System.Windows.Forms.Panel ElementsSideMenuPanel;
        private System.Windows.Forms.TabControl AddItemTabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button ShowAllExpertMarkerButton;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox OwnershipTypeComboBox;
        private System.Windows.Forms.ComboBox EconomicActivityComboBox;
        private System.Windows.Forms.PictureBox MarkerPictureBox;
        private System.Windows.Forms.Button ClearAllMarkersButton;
        private System.Windows.Forms.Button ShowCurrentUserMarkerButton;
        private System.Windows.Forms.Button ShowAllMarkersButton;
        private System.Windows.Forms.Button SaveMarkerButton;
        private System.Windows.Forms.Button AddMarkerButton;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button PolygonDrawButton;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button expertButtonTube;
        private System.Windows.Forms.Button btnDeleteTube;
        private System.Windows.Forms.Button btnClearTube;
        private System.Windows.Forms.Button btnShowExpertsTubes;
        private System.Windows.Forms.Button btnShowAllTubes;
        private System.Windows.Forms.Button btnCancelTube;
        private System.Windows.Forms.Button btnSaveTube;
        private System.Windows.Forms.Button btnStartTube;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox PolygonNameTextBox;
        private System.Windows.Forms.GroupBox EnvironmentsGroupBox;
        private System.Windows.Forms.CheckBox EnvironmentsCheckBox;
        private System.Windows.Forms.Button ShowAllLayoutButton;
        private System.Windows.Forms.Button HideAllLayoutButton;
        private System.Windows.Forms.GroupBox IssuesGroupBox;
        private System.Windows.Forms.ComboBox IssuesComboBox;
        private System.Windows.Forms.CheckBox IssuesCheckBox;
        private System.Windows.Forms.Button ShowLayoutButton;
        private System.Windows.Forms.ToolStripStatusLabel CurrentActionStatusLabel;
        private System.Windows.Forms.ToolStripProgressBar MainToolStripProgressBar;
        private System.Windows.Forms.StatusStrip MainStatusStrip;
        private System.Windows.Forms.Button ZoomPlus;
        private System.Windows.Forms.Button ZoomMinus;
        private System.Windows.Forms.Button CollapseButton;
        private GMap.NET.WindowsForms.GMapControl gMapControl;
        private System.Windows.Forms.Button MarkerSettingsButton;
        private System.Windows.Forms.ToolStripStatusLabel DebugToolStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel HelpStatusLabel;
        private System.Windows.Forms.PictureBox PolygonColorPictureBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown TransparentNumericUpDown;
        private System.Windows.Forms.Button PolygonSettingsButton;
        private System.Windows.Forms.Button PolygonSaveButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox PolygonColorTypeComboBox;
    }
}