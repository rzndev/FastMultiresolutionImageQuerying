namespace ContentBasedImageRetrieval
{
    partial class FormCanvas
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
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNew = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNewProj = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNewLayer = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOpenProject = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSave = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuClose = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuView = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLayers = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPalette = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPencil = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMode = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOutput = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuProj = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelpText = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStripMain = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelMain = new System.Windows.Forms.ToolStripStatusLabel();
            this.eventLogMain = new System.Diagnostics.EventLog();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.eventMonitor = new ContentBasedImageRetrieval.EventMonitor();
            this.menuStripMain.SuspendLayout();
            this.statusStripMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eventLogMain)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStripMain
            // 
            this.menuStripMain.BackColor = System.Drawing.SystemColors.Control;
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuView,
            this.mnuProj,
            this.mnuHelp});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(1040, 24);
            this.menuStripMain.TabIndex = 0;
            // 
            // mnuFile
            // 
            this.mnuFile.BackColor = System.Drawing.SystemColors.Control;
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuNew,
            this.mnuOpen,
            this.mnuSave,
            this.mnuSaveAs,
            this.toolStripMenuItem1,
            this.mnuClose,
            this.toolStripMenuItem2,
            this.mnuHistory,
            this.toolStripMenuItem4,
            this.mnuExit});
            this.mnuFile.ForeColor = System.Drawing.Color.Black;
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(45, 20);
            this.mnuFile.Text = "Файл";
            // 
            // mnuNew
            // 
            this.mnuNew.BackColor = System.Drawing.SystemColors.Control;
            this.mnuNew.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuNewProj,
            this.mnuNewLayer});
            this.mnuNew.ForeColor = System.Drawing.Color.Black;
            this.mnuNew.Name = "mnuNew";
            this.mnuNew.Size = new System.Drawing.Size(250, 22);
            this.mnuNew.Text = "Новый";
            // 
            // mnuNewProj
            // 
            this.mnuNewProj.Name = "mnuNewProj";
            this.mnuNewProj.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.mnuNewProj.Size = new System.Drawing.Size(150, 22);
            this.mnuNewProj.Text = "Проект";
            this.mnuNewProj.Click += new System.EventHandler(this.mnuNewProj_Click);
            // 
            // mnuNewLayer
            // 
            this.mnuNewLayer.Enabled = false;
            this.mnuNewLayer.Name = "mnuNewLayer";
            this.mnuNewLayer.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.mnuNewLayer.Size = new System.Drawing.Size(150, 22);
            this.mnuNewLayer.Text = "Слой";
            this.mnuNewLayer.Click += new System.EventHandler(this.mnuNewLayer_Click);
            // 
            // mnuOpen
            // 
            this.mnuOpen.BackColor = System.Drawing.SystemColors.Control;
            this.mnuOpen.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuOpenProject});
            this.mnuOpen.ForeColor = System.Drawing.Color.Black;
            this.mnuOpen.Name = "mnuOpen";
            this.mnuOpen.Size = new System.Drawing.Size(250, 22);
            this.mnuOpen.Text = "Открыть";
            // 
            // mnuOpenProject
            // 
            this.mnuOpenProject.Name = "mnuOpenProject";
            this.mnuOpenProject.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.mnuOpenProject.Size = new System.Drawing.Size(151, 22);
            this.mnuOpenProject.Text = "Проект";
            this.mnuOpenProject.Click += new System.EventHandler(this.mnuOpenProject_Click);
            // 
            // mnuSave
            // 
            this.mnuSave.BackColor = System.Drawing.SystemColors.Control;
            this.mnuSave.Enabled = false;
            this.mnuSave.ForeColor = System.Drawing.Color.Black;
            this.mnuSave.Name = "mnuSave";
            this.mnuSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.mnuSave.Size = new System.Drawing.Size(250, 22);
            this.mnuSave.Text = "Сохранить";
            this.mnuSave.Click += new System.EventHandler(this.mnuSave_Click);
            // 
            // mnuSaveAs
            // 
            this.mnuSaveAs.BackColor = System.Drawing.SystemColors.Control;
            this.mnuSaveAs.Enabled = false;
            this.mnuSaveAs.ForeColor = System.Drawing.Color.Black;
            this.mnuSaveAs.Name = "mnuSaveAs";
            this.mnuSaveAs.ShortcutKeys = ((System.Windows.Forms.Keys)((((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.mnuSaveAs.Size = new System.Drawing.Size(250, 22);
            this.mnuSaveAs.Text = "Сохранить как ..";
            this.mnuSaveAs.Click += new System.EventHandler(this.mnuSaveAs_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripMenuItem1.ForeColor = System.Drawing.Color.Black;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(247, 6);
            // 
            // mnuClose
            // 
            this.mnuClose.BackColor = System.Drawing.SystemColors.Control;
            this.mnuClose.Enabled = false;
            this.mnuClose.ForeColor = System.Drawing.Color.Black;
            this.mnuClose.Name = "mnuClose";
            this.mnuClose.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.Q)));
            this.mnuClose.Size = new System.Drawing.Size(250, 22);
            this.mnuClose.Text = "Закрыть проект";
            this.mnuClose.Click += new System.EventHandler(this.mnuCloseProject_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripMenuItem2.ForeColor = System.Drawing.Color.Black;
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(247, 6);
            // 
            // mnuHistory
            // 
            this.mnuHistory.Name = "mnuHistory";
            this.mnuHistory.Size = new System.Drawing.Size(250, 22);
            this.mnuHistory.Text = "Последние открытые проекты";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(247, 6);
            // 
            // mnuExit
            // 
            this.mnuExit.BackColor = System.Drawing.SystemColors.Control;
            this.mnuExit.ForeColor = System.Drawing.Color.Black;
            this.mnuExit.Name = "mnuExit";
            this.mnuExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.mnuExit.Size = new System.Drawing.Size(250, 22);
            this.mnuExit.Text = "Выход";
            this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
            // 
            // mnuView
            // 
            this.mnuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuLayers,
            this.mnuPalette,
            this.mnuPencil,
            this.mnuMode,
            this.mnuOutput});
            this.mnuView.ForeColor = System.Drawing.Color.Black;
            this.mnuView.Name = "mnuView";
            this.mnuView.Size = new System.Drawing.Size(38, 20);
            this.mnuView.Text = "Вид";
            // 
            // mnuLayers
            // 
            this.mnuLayers.Name = "mnuLayers";
            this.mnuLayers.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.L)));
            this.mnuLayers.Size = new System.Drawing.Size(194, 22);
            this.mnuLayers.Text = "Слои";
            this.mnuLayers.Click += new System.EventHandler(this.mnuLayers_Click);
            // 
            // mnuPalette
            // 
            this.mnuPalette.Name = "mnuPalette";
            this.mnuPalette.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.C)));
            this.mnuPalette.Size = new System.Drawing.Size(194, 22);
            this.mnuPalette.Text = "Палитра";
            this.mnuPalette.Click += new System.EventHandler(this.mnuColors_Click);
            // 
            // mnuPencil
            // 
            this.mnuPencil.Name = "mnuPencil";
            this.mnuPencil.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.P)));
            this.mnuPencil.Size = new System.Drawing.Size(194, 22);
            this.mnuPencil.Text = "Карандаш";
            this.mnuPencil.Click += new System.EventHandler(this.mnuBrush_Click);
            // 
            // mnuMode
            // 
            this.mnuMode.Name = "mnuMode";
            this.mnuMode.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.M)));
            this.mnuMode.Size = new System.Drawing.Size(194, 22);
            this.mnuMode.Text = "Режим ";
            this.mnuMode.Click += new System.EventHandler(this.mnuEditMode_Click);
            // 
            // mnuOutput
            // 
            this.mnuOutput.Name = "mnuOutput";
            this.mnuOutput.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.R)));
            this.mnuOutput.Size = new System.Drawing.Size(194, 22);
            this.mnuOutput.Text = "События";
            this.mnuOutput.Click += new System.EventHandler(this.mnuConsole_Click);
            // 
            // mnuProj
            // 
            this.mnuProj.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuOptions});
            this.mnuProj.Enabled = false;
            this.mnuProj.ForeColor = System.Drawing.Color.Black;
            this.mnuProj.Name = "mnuProj";
            this.mnuProj.Size = new System.Drawing.Size(56, 20);
            this.mnuProj.Text = "Проект";
            // 
            // mnuOptions
            // 
            this.mnuOptions.Enabled = false;
            this.mnuOptions.Name = "mnuOptions";
            this.mnuOptions.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.O)));
            this.mnuOptions.Size = new System.Drawing.Size(192, 22);
            this.mnuOptions.Text = "Свойства";
            this.mnuOptions.Click += new System.EventHandler(this.mnuOptions_Click);
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHelpText,
            this.toolStripMenuItem3,
            this.mnuAbout});
            this.mnuHelp.ForeColor = System.Drawing.Color.Black;
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(59, 20);
            this.mnuHelp.Text = "Помощь";
            // 
            // mnuHelpText
            // 
            this.mnuHelpText.Name = "mnuHelpText";
            this.mnuHelpText.Size = new System.Drawing.Size(177, 22);
            this.mnuHelpText.Text = "Помощь";
            this.mnuHelpText.Click += new System.EventHandler(this.mnuHelpMore_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(174, 6);
            // 
            // mnuAbout
            // 
            this.mnuAbout.Name = "mnuAbout";
            this.mnuAbout.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.mnuAbout.Size = new System.Drawing.Size(177, 22);
            this.mnuAbout.Text = "О программе";
            this.mnuAbout.Click += new System.EventHandler(this.mnuAbout_Click);
            // 
            // statusStripMain
            // 
            this.statusStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelMain});
            this.statusStripMain.Location = new System.Drawing.Point(0, 514);
            this.statusStripMain.Name = "statusStripMain";
            this.statusStripMain.Size = new System.Drawing.Size(1040, 22);
            this.statusStripMain.TabIndex = 1;
            this.statusStripMain.Text = "statusStrip1";
            // 
            // toolStripStatusLabelMain
            // 
            this.toolStripStatusLabelMain.BackColor = System.Drawing.SystemColors.Window;
            this.toolStripStatusLabelMain.Name = "toolStripStatusLabelMain";
            this.toolStripStatusLabelMain.Size = new System.Drawing.Size(0, 17);
            // 
            // eventLogMain
            // 
            this.eventLogMain.Log = "Application";
            this.eventLogMain.Source = "Strata";
            this.eventLogMain.SynchronizingObject = this;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 371);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(1040, 3);
            this.splitter1.TabIndex = 3;
            this.splitter1.TabStop = false;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "PNG files (*.png)|*.png";
            this.saveFileDialog1.Filter = "PNG files (*.png)|*.png|JPG files (*.jpg)|*.jpg|GIF files (*.gif)|*.gif";
            // 
            // eventMonitor
            // 
            this.eventMonitor.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.eventMonitor.Location = new System.Drawing.Point(0, 374);
            this.eventMonitor.Name = "eventMonitor";
            this.eventMonitor.Size = new System.Drawing.Size(1040, 140);
            this.eventMonitor.TabIndex = 2;
            // 
            // FormCanvas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(1040, 536);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.eventMonitor);
            this.Controls.Add(this.statusStripMain);
            this.Controls.Add(this.menuStripMain);
            this.MainMenuStrip = this.menuStripMain;
            this.Name = "FormCanvas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Image Editor";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.MainForm_HelpButtonClicked);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.MainForm_HelpRequested);
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.statusStripMain.ResumeLayout(false);
            this.statusStripMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eventLogMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion


        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuNew;
        private System.Windows.Forms.ToolStripMenuItem mnuNewProj;
        private System.Windows.Forms.ToolStripMenuItem mnuNewLayer;
        private System.Windows.Forms.ToolStripMenuItem mnuOpen;
        private System.Windows.Forms.ToolStripMenuItem mnuOpenProject;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuClose;
        private System.Windows.Forms.ToolStripMenuItem mnuExit;
        private System.Windows.Forms.StatusStrip statusStripMain;
        private System.Windows.Forms.ToolStripMenuItem mnuView;
        private System.Windows.Forms.ToolStripMenuItem mnuPalette;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem mnuSave;
        private System.Windows.Forms.ToolStripMenuItem mnuLayers;
        private System.Windows.Forms.ToolStripMenuItem mnuProj;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private System.Windows.Forms.ToolStripMenuItem mnuAbout;
        private System.Windows.Forms.ToolStripMenuItem mnuHelpText;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem mnuPencil;
        private System.Windows.Forms.ToolStripMenuItem mnuMode;
        private System.Windows.Forms.ToolStripMenuItem mnuOptions;
        private System.Windows.Forms.ToolStripMenuItem mnuSaveAs;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelMain;
        private System.Diagnostics.EventLog eventLogMain;
        private EventMonitor eventMonitor;
        private System.Windows.Forms.ToolStripMenuItem mnuOutput;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem mnuHistory;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
    }
}

