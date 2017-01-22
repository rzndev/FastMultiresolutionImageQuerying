namespace ContentBasedImageRetrieval
{
    partial class LayersToolWindowControl
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
            this.labelHeader = new System.Windows.Forms.Label();
            this.dgvLayers = new System.Windows.Forms.DataGridView();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panelAddLayer = new System.Windows.Forms.Panel();
            this.panelRemoveLayer = new System.Windows.Forms.Panel();
            this.panelSettings = new System.Windows.Forms.Panel();
            this.buttonClose = new System.Windows.Forms.Button();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SmallImage = new System.Windows.Forms.DataGridViewImageColumn();
            this.Caption = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Visible = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.OrderBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLayers)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelHeader
            // 
            this.labelHeader.AutoSize = true;
            this.labelHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeader.Location = new System.Drawing.Point(2, 3);
            this.labelHeader.Name = "labelHeader";
            this.labelHeader.Size = new System.Drawing.Size(36, 13);
            this.labelHeader.TabIndex = 23;
            this.labelHeader.Text = "Слои";
            // 
            // dgvLayers
            // 
            this.dgvLayers.AllowUserToAddRows = false;
            this.dgvLayers.AllowUserToDeleteRows = false;
            this.dgvLayers.BackgroundColor = System.Drawing.Color.White;
            this.dgvLayers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLayers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.SmallImage,
            this.Caption,
            this.Visible,
            this.OrderBy});
            this.dgvLayers.Location = new System.Drawing.Point(5, 28);
            this.dgvLayers.MultiSelect = false;
            this.dgvLayers.Name = "dgvLayers";
            this.dgvLayers.ReadOnly = true;
            this.dgvLayers.Size = new System.Drawing.Size(240, 347);
            this.dgvLayers.TabIndex = 24;
            this.dgvLayers.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLayers_CellClick);
            this.dgvLayers.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLayers_CellContentClick);
            this.dgvLayers.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgvLayers_RowsAdded);
            this.dgvLayers.SelectionChanged += new System.EventHandler(this.dgvLayers_SelectionChanged);
            this.dgvLayers.DragDrop += new System.Windows.Forms.DragEventHandler(this.dgvLayers_DragDrop);
            this.dgvLayers.DragOver += new System.Windows.Forms.DragEventHandler(this.dgvLayers_DragOver);
            this.dgvLayers.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgvLayers_MouseDown);
            this.dgvLayers.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dgvLayers_MouseMove);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.panelAddLayer);
            this.flowLayoutPanel1.Controls.Add(this.panelRemoveLayer);
            this.flowLayoutPanel1.Controls.Add(this.panelSettings);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(157, 381);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(95, 31);
            this.flowLayoutPanel1.TabIndex = 27;
            // 
            // panelAddLayer
            // 
            this.panelAddLayer.BackColor = System.Drawing.Color.Transparent;
            this.panelAddLayer.BackgroundImage = global::ContentBasedImageRetrieval.Properties.Resources.add23x23;
            this.panelAddLayer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panelAddLayer.Location = new System.Drawing.Point(3, 3);
            this.panelAddLayer.Name = "panelAddLayer";
            this.panelAddLayer.Padding = new System.Windows.Forms.Padding(3);
            this.panelAddLayer.Size = new System.Drawing.Size(25, 25);
            this.panelAddLayer.TabIndex = 25;
            this.panelAddLayer.Click += new System.EventHandler(this.panelAddLayer_Click);
            this.panelAddLayer.MouseEnter += new System.EventHandler(this.panelAddLayer_MouseEnter);
            this.panelAddLayer.MouseLeave += new System.EventHandler(this.panelAddLayer_MouseLeave);
            // 
            // panelRemoveLayer
            // 
            this.panelRemoveLayer.BackColor = System.Drawing.Color.Transparent;
            this.panelRemoveLayer.BackgroundImage = global::ContentBasedImageRetrieval.Properties.Resources.RecycleBin23x23;
            this.panelRemoveLayer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panelRemoveLayer.Location = new System.Drawing.Point(34, 3);
            this.panelRemoveLayer.Name = "panelRemoveLayer";
            this.panelRemoveLayer.Padding = new System.Windows.Forms.Padding(3);
            this.panelRemoveLayer.Size = new System.Drawing.Size(25, 25);
            this.panelRemoveLayer.TabIndex = 26;
            this.panelRemoveLayer.Click += new System.EventHandler(this.panelRemoveLayer_Click);
            this.panelRemoveLayer.MouseEnter += new System.EventHandler(this.panelAddLayer_MouseEnter);
            this.panelRemoveLayer.MouseLeave += new System.EventHandler(this.panelAddLayer_MouseLeave);
            // 
            // panelSettings
            // 
            this.panelSettings.BackColor = System.Drawing.Color.Transparent;
            this.panelSettings.BackgroundImage = global::ContentBasedImageRetrieval.Properties.Resources.settings23x23;
            this.panelSettings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panelSettings.Location = new System.Drawing.Point(65, 3);
            this.panelSettings.Name = "panelSettings";
            this.panelSettings.Padding = new System.Windows.Forms.Padding(3);
            this.panelSettings.Size = new System.Drawing.Size(25, 25);
            this.panelSettings.TabIndex = 27;
            this.panelSettings.Click += new System.EventHandler(this.panelSettings_Click);
            this.panelSettings.MouseEnter += new System.EventHandler(this.panelAddLayer_MouseEnter);
            this.panelSettings.MouseLeave += new System.EventHandler(this.panelAddLayer_MouseLeave);
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.BackColor = System.Drawing.Color.Transparent;
            this.buttonClose.BackgroundImage = global::ContentBasedImageRetrieval.Properties.Resources.icon_close_small;
            this.buttonClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonClose.FlatAppearance.BorderSize = 0;
            this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClose.Location = new System.Drawing.Point(230, 0);
            this.buttonClose.Margin = new System.Windows.Forms.Padding(2);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(24, 23);
            this.buttonClose.TabIndex = 22;
            this.buttonClose.UseVisualStyleBackColor = false;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // Id
            // 
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            // 
            // SmallImage
            // 
            this.SmallImage.HeaderText = "Img";
            this.SmallImage.Name = "SmallImage";
            this.SmallImage.ReadOnly = true;
            this.SmallImage.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Caption
            // 
            this.Caption.HeaderText = "Caption";
            this.Caption.Name = "Caption";
            this.Caption.ReadOnly = true;
            // 
            // Visible
            // 
            this.Visible.HeaderText = "Visible";
            this.Visible.Name = "Visible";
            this.Visible.ReadOnly = true;
            // 
            // OrderBy
            // 
            this.OrderBy.HeaderText = "OrderBy";
            this.OrderBy.Name = "OrderBy";
            this.OrderBy.ReadOnly = true;
            this.OrderBy.Visible = false;
            // 
            // LayersToolWindowControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.dgvLayers);
            this.Controls.Add(this.labelHeader);
            this.Controls.Add(this.buttonClose);
            this.Name = "LayersToolWindowControl";
            this.Size = new System.Drawing.Size(255, 415);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLayers)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelHeader;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.DataGridView dgvLayers;
        private System.Windows.Forms.Panel panelAddLayer;
        private System.Windows.Forms.Panel panelRemoveLayer;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panelSettings;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewImageColumn SmallImage;
        private System.Windows.Forms.DataGridViewTextBoxColumn Caption;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Visible;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrderBy;
    }
}
