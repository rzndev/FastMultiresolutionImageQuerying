using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ContentBasedImageRetrieval
{
    public partial class NewProjectForm : Form
    {
        private const string DefaultProjectName = @"Image";

        private readonly string _rootFolder;

        private string _path;

        public NewProjectForm()
        {
            InitializeComponent();
            _rootFolder = Environment.ExpandEnvironmentVariables(
                EnvironmentVariables.GetProjectsPath);

            if (!Directory.Exists(_rootFolder))
                Directory.CreateDirectory(_rootFolder);

            textBoxProjectPath.Text = _rootFolder;
        }

        public string ProjectName { get; set; }
        public string ProjectPath { get; set; }

        public DialogResult Result { get; set; }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
            if (e.KeyCode == Keys.Enter)
            {
                Save();
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void Save()
        {
            string path = textBoxProjectPath.Text;
            if (IsValidFilename(path))
            {
                MessageBox.Show(this, @"Некорреткное наименование папки",
                    @"Создание проекта",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            string dir = Path.GetDirectoryName(path);
            if (dir != null && Directory.Exists(dir))
            {
                if (MessageBox.Show(this, @"Такая директория уже существует. Переписать?",
                    @"Создание проекта",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information) != DialogResult.Yes)
                    return;
            }

            ProjectName = textBoxProjectName.Text;
            ProjectPath = textBoxProjectPath.Text;

            Result = DialogResult.OK;
            Close();
        }
        private void buttonOpenFileDialog_Click(object sender, EventArgs e)
        {
            var dialog = new SaveFileDialog();
            dialog.Filter = @"(*.st) | *.st";
            if ((Result = dialog.ShowDialog()) == DialogResult.OK)
            {
                textBoxProjectPath.Text = dialog.FileName;
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private bool IsValidFilename(string testName)
        {
            var containsABadCharacter = new Regex("[" +
                                                  Regex.Escape(string.Join("", Path.GetInvalidFileNameChars())) + "]");

            if (containsABadCharacter.IsMatch(testName))
            {
                return false;
            }
            ;
            return true;
        }

        private void textBoxProjectName_TextChanged(object sender, EventArgs e)
        {
            if (checkBoxUseInProjName.Checked)
            {
                if (Path.IsPathRooted(textBoxProjectPath.Text))
                {
                    if (string.IsNullOrEmpty(_path))
                        _path = textBoxProjectPath.Text
                                + (checkBoxCreateProjFolder.Checked ? "{0}\\{0}" : "{0}")
                                + ".st";

                    string path = string.Format(_path, textBoxProjectName.Text);

                    if (IsValidFilename(path))
                    {
                        MessageBox.Show(this, @"Некорреткное наименование папки",
                            @"Наименаование папки проекта",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                    else
                    {
                        textBoxProjectPath.Text = path;
                    }
                }
            }
        }

        private void checkBoxUseInProjName_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxCreateProjFolder.Enabled = checkBoxUseInProjName.Checked;
        }

        private void NewProjectForm_Load(object sender, EventArgs e)
        {
            textBoxProjectName.Text = GetDefaultProjName();
            textBoxProjectName.Focus();
        }

        private string GetDefaultProjName()
        {
            var list = new List<int>();
            string[] dirs = Directory.GetDirectories(_rootFolder);

            // ReSharper disable LoopCanBeConvertedToQuery
            foreach (string dir in dirs)
                // ReSharper restore LoopCanBeConvertedToQuery
            {
                string name = Path.GetFileNameWithoutExtension(dir);
                if (name == null)
                    continue;

                if (name.StartsWith(DefaultProjectName))
                {
                    string pNum = name.Substring(DefaultProjectName.Length);
                    int i = 1;
                    if (int.TryParse(pNum, out i))
                        list.Add(i);
                }
            }
            if (list.Any())
            {
                int max = list.Max(x => x) + 1;
                return string.Format("{0}{1}", DefaultProjectName, max);
            }
            return DefaultProjectName + "1";
        }
    }
}