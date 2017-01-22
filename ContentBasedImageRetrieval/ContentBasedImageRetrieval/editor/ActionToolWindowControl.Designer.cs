namespace ContentBasedImageRetrieval
{
    partial class ActionToolWindowControl
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
            this.buttonClose = new System.Windows.Forms.Button();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.panelMove = new System.Windows.Forms.Panel();
            this.panelRotate = new System.Windows.Forms.Panel();
            this.panelPaint = new System.Windows.Forms.Panel();
            this.panelErise = new System.Windows.Forms.Panel();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelHeader
            // 
            this.labelHeader.AutoSize = true;
            this.labelHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeader.Location = new System.Drawing.Point(3, 3);
            this.labelHeader.Name = "labelHeader";
            this.labelHeader.Size = new System.Drawing.Size(47, 13);
            this.labelHeader.TabIndex = 21;
            this.labelHeader.Text = "Режим";
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.BackColor = System.Drawing.Color.Transparent;
            this.buttonClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonClose.FlatAppearance.BorderSize = 0;
            this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClose.Location = new System.Drawing.Point(52, 0);
            this.buttonClose.Margin = new System.Windows.Forms.Padding(2);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(24, 23);
            this.buttonClose.TabIndex = 20;
            this.buttonClose.UseVisualStyleBackColor = false;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.panelPaint);
            this.flowLayoutPanel2.Controls.Add(this.panelErise);
            this.flowLayoutPanel2.Controls.Add(this.panelMove);
            this.flowLayoutPanel2.Controls.Add(this.panelRotate);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(6, 26);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(2);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(65, 66);
            this.flowLayoutPanel2.TabIndex = 22;
            // 
            // panelMove
            // 
            this.panelMove.BackColor = System.Drawing.Color.Transparent;
            this.panelMove.BackgroundImage = global::ContentBasedImageRetrieval.Properties.Resources.move23x23;
            this.panelMove.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panelMove.Location = new System.Drawing.Point(3, 34);
            this.panelMove.Name = "panelMove";
            this.panelMove.Padding = new System.Windows.Forms.Padding(3);
            this.panelMove.Size = new System.Drawing.Size(25, 25);
            this.panelMove.TabIndex = 16;
            this.panelMove.Click += new System.EventHandler(this.panelPaint_Click);
            this.panelMove.MouseEnter += new System.EventHandler(this.panelPaint_MouseEnter);
            this.panelMove.MouseLeave += new System.EventHandler(this.panelPaint_MouseLeave);
            // 
            // panelRotate
            // 
            this.panelRotate.BackColor = System.Drawing.Color.Transparent;
            this.panelRotate.BackgroundImage = global::ContentBasedImageRetrieval.Properties.Resources.rotatered23x23;
            this.panelRotate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panelRotate.Location = new System.Drawing.Point(34, 34);
            this.panelRotate.Name = "panelRotate";
            this.panelRotate.Padding = new System.Windows.Forms.Padding(3);
            this.panelRotate.Size = new System.Drawing.Size(25, 25);
            this.panelRotate.TabIndex = 17;
            this.panelRotate.Visible = false;
            this.panelRotate.Click += new System.EventHandler(this.panelPaint_Click);
            this.panelRotate.MouseEnter += new System.EventHandler(this.panelPaint_MouseEnter);
            this.panelRotate.MouseLeave += new System.EventHandler(this.panelPaint_MouseLeave);
            // 
            // panelPaint
            // 
            this.panelPaint.BackColor = System.Drawing.Color.Transparent;
            this.panelPaint.BackgroundImage = global::ContentBasedImageRetrieval.Properties.Resources.pencil23x23;
            this.panelPaint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panelPaint.Location = new System.Drawing.Point(3, 3);
            this.panelPaint.Name = "panelPaint";
            this.panelPaint.Padding = new System.Windows.Forms.Padding(3);
            this.panelPaint.Size = new System.Drawing.Size(25, 25);
            this.panelPaint.TabIndex = 14;
            this.panelPaint.Click += new System.EventHandler(this.panelPaint_Click);
            this.panelPaint.MouseEnter += new System.EventHandler(this.panelPaint_MouseEnter);
            this.panelPaint.MouseLeave += new System.EventHandler(this.panelPaint_MouseLeave);
            // 
            // panelErise
            // 
            this.panelErise.BackColor = System.Drawing.Color.Transparent;
            this.panelErise.BackgroundImage = global::ContentBasedImageRetrieval.Properties.Resources.Eraser23x23;
            this.panelErise.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panelErise.Location = new System.Drawing.Point(34, 3);
            this.panelErise.Name = "panelErise";
            this.panelErise.Padding = new System.Windows.Forms.Padding(3);
            this.panelErise.Size = new System.Drawing.Size(25, 25);
            this.panelErise.TabIndex = 15;
            this.panelErise.Click += new System.EventHandler(this.panelPaint_Click);
            this.panelErise.MouseEnter += new System.EventHandler(this.panelPaint_MouseEnter);
            this.panelErise.MouseLeave += new System.EventHandler(this.panelPaint_MouseLeave);
            // 
            // ActionToolWindowControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.labelHeader);
            this.Controls.Add(this.buttonClose);
            this.Name = "ActionToolWindowControl";
            this.Size = new System.Drawing.Size(78, 97);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelHeader;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Panel panelPaint;
        private System.Windows.Forms.Panel panelErise;
        private System.Windows.Forms.Panel panelMove;
        private System.Windows.Forms.Panel panelRotate;
    }
}
