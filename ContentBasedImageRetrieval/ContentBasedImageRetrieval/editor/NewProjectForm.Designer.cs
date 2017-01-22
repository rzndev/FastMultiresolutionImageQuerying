namespace ContentBasedImageRetrieval
{
    partial class NewProjectForm
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
            this.textBoxProjectName = new System.Windows.Forms.TextBox();
            this.labelName = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.textBoxProjectPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonOpenFileDialog = new System.Windows.Forms.Button();
            this.checkBoxUseInProjName = new System.Windows.Forms.CheckBox();
            this.checkBoxCreateProjFolder = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // textBoxProjectName
            // 
            this.textBoxProjectName.Location = new System.Drawing.Point(15, 26);
            this.textBoxProjectName.Name = "textBoxProjectName";
            this.textBoxProjectName.Size = new System.Drawing.Size(288, 20);
            this.textBoxProjectName.TabIndex = 1;
            this.textBoxProjectName.TextChanged += new System.EventHandler(this.textBoxProjectName_TextChanged);
            this.textBoxProjectName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_KeyDown);
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(12, 9);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(57, 13);
            this.labelName.TabIndex = 8;
            this.labelName.Text = "Название";
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(422, 122);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Location = new System.Drawing.Point(503, 122);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 6;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // textBoxProjectPath
            // 
            this.textBoxProjectPath.Location = new System.Drawing.Point(15, 66);
            this.textBoxProjectPath.Name = "textBoxProjectPath";
            this.textBoxProjectPath.Size = new System.Drawing.Size(525, 20);
            this.textBoxProjectPath.TabIndex = 2;
            this.textBoxProjectPath.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Путь";
            // 
            // buttonOpenFileDialog
            // 
            this.buttonOpenFileDialog.Location = new System.Drawing.Point(546, 65);
            this.buttonOpenFileDialog.Name = "buttonOpenFileDialog";
            this.buttonOpenFileDialog.Size = new System.Drawing.Size(32, 20);
            this.buttonOpenFileDialog.TabIndex = 3;
            this.buttonOpenFileDialog.Text = "...";
            this.buttonOpenFileDialog.UseVisualStyleBackColor = true;
            this.buttonOpenFileDialog.Click += new System.EventHandler(this.buttonOpenFileDialog_Click);
            // 
            // checkBoxUseInProjName
            // 
            this.checkBoxUseInProjName.AutoSize = true;
            this.checkBoxUseInProjName.Checked = true;
            this.checkBoxUseInProjName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxUseInProjName.Location = new System.Drawing.Point(15, 104);
            this.checkBoxUseInProjName.Name = "checkBoxUseInProjName";
            this.checkBoxUseInProjName.Size = new System.Drawing.Size(203, 17);
            this.checkBoxUseInProjName.TabIndex = 4;
            this.checkBoxUseInProjName.Text = "Использовать в названии проекта";
            this.checkBoxUseInProjName.UseVisualStyleBackColor = true;
            this.checkBoxUseInProjName.CheckedChanged += new System.EventHandler(this.checkBoxUseInProjName_CheckedChanged);
            // 
            // checkBoxCreateProjFolder
            // 
            this.checkBoxCreateProjFolder.AutoSize = true;
            this.checkBoxCreateProjFolder.Checked = true;
            this.checkBoxCreateProjFolder.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCreateProjFolder.Location = new System.Drawing.Point(15, 127);
            this.checkBoxCreateProjFolder.Name = "checkBoxCreateProjFolder";
            this.checkBoxCreateProjFolder.Size = new System.Drawing.Size(144, 17);
            this.checkBoxCreateProjFolder.TabIndex = 5;
            this.checkBoxCreateProjFolder.Text = "Создать папку проекта";
            this.checkBoxCreateProjFolder.UseVisualStyleBackColor = true;
            // 
            // NewProjectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 157);
            this.Controls.Add(this.checkBoxCreateProjFolder);
            this.Controls.Add(this.checkBoxUseInProjName);
            this.Controls.Add(this.buttonOpenFileDialog);
            this.Controls.Add(this.textBoxProjectPath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxProjectName);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "NewProjectForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Новый проект";
            this.Load += new System.EventHandler(this.NewProjectForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxProjectName;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.TextBox textBoxProjectPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonOpenFileDialog;
        private System.Windows.Forms.CheckBox checkBoxUseInProjName;
        private System.Windows.Forms.CheckBox checkBoxCreateProjFolder;
    }
}