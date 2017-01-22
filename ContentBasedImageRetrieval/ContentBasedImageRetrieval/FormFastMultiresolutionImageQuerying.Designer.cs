namespace ContentBasedImageRetrieval
{
    partial class FormFastMultiresolutionImageQuerying
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxImagePath = new System.Windows.Forms.TextBox();
            this.buttonOpenFileDialog = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.buttonAddToDatabase = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.buttonRemoveImageFromDatabase = new System.Windows.Forms.Button();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.buttonMatch = new System.Windows.Forms.Button();
            this.buttonDirectory = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Изображение";
            // 
            // textBoxImagePath
            // 
            this.textBoxImagePath.Location = new System.Drawing.Point(103, 5);
            this.textBoxImagePath.Name = "textBoxImagePath";
            this.textBoxImagePath.Size = new System.Drawing.Size(325, 20);
            this.textBoxImagePath.TabIndex = 1;
            // 
            // buttonOpenFileDialog
            // 
            this.buttonOpenFileDialog.Location = new System.Drawing.Point(434, 5);
            this.buttonOpenFileDialog.Name = "buttonOpenFileDialog";
            this.buttonOpenFileDialog.Size = new System.Drawing.Size(32, 20);
            this.buttonOpenFileDialog.TabIndex = 4;
            this.buttonOpenFileDialog.Text = "...";
            this.buttonOpenFileDialog.UseVisualStyleBackColor = true;
            this.buttonOpenFileDialog.Click += new System.EventHandler(this.buttonOpenFileDialog_Click);
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(543, 26);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(92, 23);
            this.buttonOk.TabIndex = 5;
            this.buttonOk.Text = "Загрузить";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(15, 53);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(512, 512);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // buttonAddToDatabase
            // 
            this.buttonAddToDatabase.Location = new System.Drawing.Point(543, 66);
            this.buttonAddToDatabase.Name = "buttonAddToDatabase";
            this.buttonAddToDatabase.Size = new System.Drawing.Size(92, 23);
            this.buttonAddToDatabase.TabIndex = 7;
            this.buttonAddToDatabase.Text = "Добавить";
            this.buttonAddToDatabase.UseVisualStyleBackColor = true;
            this.buttonAddToDatabase.Click += new System.EventHandler(this.buttonAddToDatabase_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Наименование:";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(103, 27);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(323, 20);
            this.textBoxName.TabIndex = 9;
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(641, 53);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(633, 512);
            this.listView1.TabIndex = 10;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Location = new System.Drawing.Point(641, 26);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(75, 23);
            this.buttonRefresh.TabIndex = 11;
            this.buttonRefresh.Text = "Обновить";
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // buttonRemoveImageFromDatabase
            // 
            this.buttonRemoveImageFromDatabase.Location = new System.Drawing.Point(722, 27);
            this.buttonRemoveImageFromDatabase.Name = "buttonRemoveImageFromDatabase";
            this.buttonRemoveImageFromDatabase.Size = new System.Drawing.Size(75, 23);
            this.buttonRemoveImageFromDatabase.TabIndex = 12;
            this.buttonRemoveImageFromDatabase.Text = "Удалить";
            this.buttonRemoveImageFromDatabase.UseVisualStyleBackColor = true;
            this.buttonRemoveImageFromDatabase.Click += new System.EventHandler(this.buttonRemoveImageFromDatabase_Click);
            // 
            // buttonEdit
            // 
            this.buttonEdit.Location = new System.Drawing.Point(543, 104);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(92, 23);
            this.buttonEdit.TabIndex = 13;
            this.buttonEdit.Text = "Редактировать";
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // buttonMatch
            // 
            this.buttonMatch.Location = new System.Drawing.Point(543, 145);
            this.buttonMatch.Name = "buttonMatch";
            this.buttonMatch.Size = new System.Drawing.Size(92, 23);
            this.buttonMatch.TabIndex = 14;
            this.buttonMatch.Text = "Соответсвие";
            this.buttonMatch.UseVisualStyleBackColor = true;
            this.buttonMatch.Click += new System.EventHandler(this.buttonMatch_Click);
            // 
            // buttonDirectory
            // 
            this.buttonDirectory.Location = new System.Drawing.Point(543, 186);
            this.buttonDirectory.Name = "buttonDirectory";
            this.buttonDirectory.Size = new System.Drawing.Size(92, 23);
            this.buttonDirectory.TabIndex = 15;
            this.buttonDirectory.Text = "Каталог";
            this.buttonDirectory.UseVisualStyleBackColor = true;
            this.buttonDirectory.Click += new System.EventHandler(this.buttonDirectory_Click);
            // 
            // FormFastMultiresolutionImageQuerying
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1302, 584);
            this.Controls.Add(this.buttonDirectory);
            this.Controls.Add(this.buttonMatch);
            this.Controls.Add(this.buttonEdit);
            this.Controls.Add(this.buttonRemoveImageFromDatabase);
            this.Controls.Add(this.buttonRefresh);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonAddToDatabase);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonOpenFileDialog);
            this.Controls.Add(this.textBoxImagePath);
            this.Controls.Add(this.label1);
            this.Name = "FormFastMultiresolutionImageQuerying";
            this.Text = "Fast Multiresolution Image Querying";
            this.Load += new System.EventHandler(this.FormFastMultiresolutionImageQuerying_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxImagePath;
        private System.Windows.Forms.Button buttonOpenFileDialog;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button buttonAddToDatabase;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.Button buttonRemoveImageFromDatabase;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Button buttonMatch;
        private System.Windows.Forms.Button buttonDirectory;
    }
}

