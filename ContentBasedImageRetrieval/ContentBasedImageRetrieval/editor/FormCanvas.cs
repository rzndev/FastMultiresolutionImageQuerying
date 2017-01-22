using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ContentBasedImageRetrieval
{


    public partial class FormCanvas : Form
    {
        private bool _askClosePrevention;
        string debugpath = "";
        // @"C:\Users\Andrey\AppData\Roaming\Strata\Projects\StrataProject49\StrataProject49.st";

        public FormFastMultiresolutionImageQuerying frm = null;

        public FormCanvas()
        {
            InitializeComponent();

            Settings.Instance.EditModeChanged += Instance_EditModeChanged;
            Settings.Instance.ColorChanged += Instance_ColorChanged;
            Settings.Instance.WidthChanged += Instance_WidthChanged;
            Settings.Instance.PreviousProjectList.CollectionChanged
                += PreviousProjectList_CollectionChanged;

            Settings.Instance.Load();

            SetLayersForm(true);
            SetColorsForm(true);
            SetBrushWidth(true);
            SetEditModeForm(true);
            SetOutput(false);
            SetEnabledMenu(false);
            SetWorkArea(false);
            SetTitleDefault();
        }

        private void WriteLine(string message)
        {
            WriteStatus(message);
            eventMonitor.WriteLine(message);
        }

        private void Instance_WidthChanged(object sender, EventArgs e)
        {
            WriteLine(string.Format("Задана ширина карандаша: {0}",
                Settings.Instance.Width));
        }

        private void Instance_ColorChanged(object sender, EventArgs e)
        {
            WriteLine(string.Format("Задан цвет: {0}",
                Settings.Instance.Color.Name));
        }

        private void Instance_EditModeChanged(object sender, EventArgs e)
        {
            WriteLine(string.Format("Задан режим работы: {0}",
                Enum.GetName(typeof (EditModeEnum),
                    Settings.Instance.Mode)));
        }

        private void mnuOptions_Click(object sender, EventArgs e)
        {
            var form = new OptionsForm();
            form.ShowDialog();
        }

        internal void SetMnuLayersChecked(bool isChecked)
        {
            mnuLayers.Checked = isChecked;
        }

        private void mnuLayers_Click(object sender, EventArgs e)
        {
            SetLayersForm(!mnuLayers.Checked);
        }

        private void SetLayersForm(bool show)
        {
            if (show)
            {
                SetMnuLayersChecked(true);

                LayersToolWindowControl.Instance.Anchor =
                    (AnchorStyles.Top | AnchorStyles.Right);

                LayersToolWindowControl.Instance.Location =
                    new Point(
                        ClientSize.Width -
                        LayersToolWindowControl.Instance.Width - 10,
                        30);

                if (Controls.Cast<Control>().All(x =>
                    (x as LayersToolWindowControl) == null))
                {
                    Controls.Add(LayersToolWindowControl.Instance);
                }
            }
            else
            {
                var control = (Controls.Cast<Control>().FirstOrDefault(x =>
                    (x as LayersToolWindowControl) != null));

                if (control != null)
                {
                    Controls.Remove(control);
                    SetMnuLayersChecked(false);    
                }
            }
        }

        private void mnuHelpMore_Click(object sender, EventArgs e)
        {
            var form = new HelpForm();
            form.Show();
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void mnuEditMode_Click(object sender, EventArgs e)
        {
            SetEditModeForm(!mnuMode.Checked);
        }

        private void SetEditModeForm(bool show)
        {
            if (show)
            {
                SetMnuActionChecked(true);

                ActionToolWindowControl.Instance.Anchor =
                    (AnchorStyles.Top | AnchorStyles.Left);

                ActionToolWindowControl.Instance.Location =
                    new Point(10, 30);

                if (Controls.Cast<Control>().All(x =>
                    (x as ActionToolWindowControl) == null))
                {
                    Controls.Add(ActionToolWindowControl.Instance);
                }
            }
            else
            {
                var control = Controls.Cast<Control>().
                    FirstOrDefault(x =>
                    (x as ActionToolWindowControl) != null);

                if(control != null)
                {
                    Controls.Remove(control);
                    SetMnuActionChecked(false);
                }                
            }
        }

        internal void SetMnuActionChecked(bool isChecked)
        {
            mnuMode.Checked = isChecked;
        }

        private void mnuColors_Click(object sender, EventArgs e)
        {
            SetColorsForm(!mnuPalette.Checked);
        }

        private void SetOutput(bool show)
        {
            eventMonitor.Dock = DockStyle.Bottom;
            eventMonitor.Visible = show;
        }

        private void SetColorsForm(bool show)
        {
            ColorsControl.Instance.Visible = show;
            SetMnuColorsChecked(show);

            if (show)
            {
                ColorsControl.Instance.Anchor =
                    (AnchorStyles.Top | AnchorStyles.Left);

                ColorsControl.Instance.Location =
                    new Point(
                        10,
                        ColorsControl.Instance.Height + 10);

                if (Controls.Cast<Control>().All(x =>
                    (x as ColorsControl) == null))
                {
                    Controls.Add(ColorsControl.Instance);
                }
            }
        }

        internal void SetMnuColorsChecked(bool show)
        {
            mnuPalette.Checked = show;
        }

        private void mnuBrush_Click(object sender, EventArgs e)
        {
            SetBrushWidth(!mnuPencil.Checked);
        }

        private void SetBrushWidth(bool show)
        {
            PencilToolWindowControl.Instance.Visible = show;
            SetMnuPencilChecked(show);

            if (show)
            {
                PencilToolWindowControl.Instance.Anchor =
                    (AnchorStyles.Top | AnchorStyles.Right);

                PencilToolWindowControl.Instance.Location =
                    new Point(
                        ClientSize.Width -
                        PencilToolWindowControl.Instance.Width - 10,
                        LayersToolWindowControl.Instance.Height + 30 + 10);

                if (Controls.Cast<Control>().All(x =>
                    (x as PencilToolWindowControl) == null))
                {
                    Controls.Add(PencilToolWindowControl.Instance);
                }
            }
        }

        internal void SetMnuPencilChecked(bool show)
        {
            mnuPencil.Checked = show;
        }

        private void mnuAbout_Click(object sender, EventArgs e)
        {
            var form = new AboutForm();
            form.ShowDialog();
        }

        private void MainForm_HelpRequested(object sender,
            HelpEventArgs hlpevent)
        {
            var form = new HelpForm();
            form.Show();

            hlpevent.Handled = true;
        }

        public void ShowStartForm()
        {
            //var f = new StartForm
            //{
            //    StartPosition = FormStartPosition.CenterScreen,
            //    Dock = DockStyle.Top
            //};
            //f.ShowDialog(this);

            //switch (f.StartFormDialogResult)
            //{
            //    case StartFormDialogResultEnum.CreateProject:
            //        CreateProjectDialog();
            //        break;
            //    case StartFormDialogResultEnum.OpenProject:
            //        OpenProjectDialog();
            //        break;
            //    case StartFormDialogResultEnum.ShowAbout:
            //        var about = new AboutForm();
            //        about.ShowDialog();
            //        break;
            //    case StartFormDialogResultEnum.ShowHelp:
            //        var help = new HelpForm();
            //        help.Show();
            //        break;
            //}
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(debugpath))
            {
                OpenProject(debugpath);
            }
            else
            {
                ShowStartForm();    
            }
        }


        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_askClosePrevention)
                return;

            if (Project.Instance != null && Project.Instance.IsChanged)
            {
                DialogResult result = MessageBox.Show(
                    @"Сохранить изменения перед закрытием редактора?",
                    @"Завершение работы",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                    _askClosePrevention = true;
                    LayersToolWindowControl.Instance = null;
                    ActionToolWindowControl.Instance = null;
                    ColorsControl.Instance = null;
                    Settings.Instance = null;
                    PencilToolWindowControl.Instance = null;
                    WorkArea.Instance = null;
                    Project.Instance.Close();
                    return;
                    //Project.Instance = null;
                    //Application.Exit();
                }

                if (result == DialogResult.Yes)
                {
                    SaveProject();
                    SaveSettings();
                    LayersToolWindowControl.Instance = null;
                    ActionToolWindowControl.Instance = null;
                    ColorsControl.Instance = null;
                    Settings.Instance = null;
                    PencilToolWindowControl.Instance = null;
                    
                    WorkArea.Instance = null;
                    Project.Instance.Close();
                    return;
                    //Project.Instance = null;
                    //Application.Exit();
                }
                if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
            }
            LayersToolWindowControl.Instance = null;
            ActionToolWindowControl.Instance = null;
            ColorsControl.Instance = null;
            Settings.Instance = null;
            PencilToolWindowControl.Instance = null;
            WorkArea.Instance = null;
            Project.Instance.Close();
        }

        private void mnuCloseProject_Click(object sender, EventArgs e)
        {
            if (CloseProject())
            {
                SetEnabledMenu(false);
            }
        }

        private bool CloseProject()
        {
            if (Project.Instance == null)
                return true;

            try
            {
                Cursor = Cursors.WaitCursor;

                string name = Project.Instance.Name;

                if (Project.Instance.IsChanged)
                {
                    DialogResult result = MessageBox.Show(
                        @"Сохранить изменения проекта?",
                        @"Сохранение проекта",
                        MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Question);

                    if (result == DialogResult.Cancel)
                        return false;

                    if (result == DialogResult.Yes)
                    {
                        Project.Instance.Save();

                        WriteLine(string.Format(
                            @"Проект ""{0}"" сохранён..",
                            Project.Instance.Name));
                    }
                }

                SetWorkArea(false);

                Project.Instance.Close();
                
                WriteLine(string.Format(
                    @"Проект ""{0}"" закрыт..", 
                    name));

                return true;
            }
            catch (Exception exc)
            {
                eventLogMain.WriteEntry(
                    string.Format("Ошибка сохранения файла проекта: {0}"
                                  + Environment.NewLine + Environment.NewLine + "{1}",
                        Project.Instance.Path, Environment.NewLine, exc),
                    EventLogEntryType.Error);

                MessageBox.Show(@"При сохранении файла проекта возникла ошибка. "
                                + Environment.NewLine + @"Подробности в Event Viewer. (F1)",
                    @"Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }

            return false;
        }

        private void SaveFileAs(ImageFormat format, string filename)
        {
            Bitmap bitmap = null;
            try
            {
                bitmap = BitmapTools.CombineBitmap(format,
                    Settings.Instance.Canvas.Height,
                    Settings.Instance.Canvas.Width);

                bitmap.Save(filename, format);
            }
                // ReSharper disable RedundantCatchClause
            catch (Exception exc)
            {
                throw;
            }
                // ReSharper restore RedundantCatchClause
            finally
            {
                if (bitmap != null)
                    bitmap.Dispose();
            }
        }

        private void mnuSaveAs_Click(object sender, EventArgs e)
        {
            try
            {
                saveFileDialog1.FileName =
                    string.Format("{0}_Image", Project.Instance.Name);

                saveFileDialog1.InitialDirectory =
                    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string extension = Path.GetExtension(saveFileDialog1.FileName);
                    if (string.IsNullOrEmpty(extension))
                        throw new NullReferenceException("Extention Is not selected");

                    switch (extension.ToLower())
                    {
                        case ".jpg":
                            SaveFileAs(ImageFormat.Jpeg,
                                saveFileDialog1.FileName);
                            break;

                        case ".png":
                            SaveFileAs(ImageFormat.Png,
                                saveFileDialog1.FileName);
                            break;

                        case ".gif":
                            SaveFileAs(ImageFormat.Gif,
                                saveFileDialog1.FileName);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(extension);
                    }
                }

                WriteLine(string.Format("Файл {0} сохранён",
                    saveFileDialog1.FileName));
            }
            catch (Exception exc)
            {
                eventLogMain.WriteEntry(
                    string.Format(@"Ошибка сохранения файла: {0}", exc),
                    EventLogEntryType.Error);

                MessageBox.Show(
                    @"При сохранении файла возникла ошибка. "
                    + Environment.NewLine +
                    @"Подробности в Event Viewer. Помощь (F1): Просмотр ошибок.",
                    @"Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void mnuSave_Click(object sender, EventArgs e)
        {
            SaveProject();
        }

        private void MainForm_HelpButtonClicked(object sender,
            CancelEventArgs e)
        {
            var form = new HelpForm();
            form.Show();
            e.Cancel = true;
        }

        private void Layers_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (Layer layer in e.NewItems.Cast<Layer>())
                    WriteLine(string.Format("Добавлен слой: {0}", layer.Caption));
            }
            if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                WriteLine("Все слои удалены.");
            }
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (Layer layer in e.OldItems.Cast<Layer>())
                    WriteLine(string.Format("Удалён слой: {0}", layer.Caption));
            }
        }

        private void mnuOpenProject_Click(object sender, EventArgs e)
        {
            OpenProjectDialog();
        }

        private void OpenProjectDialog()
        {
            if (CloseProject())
            {
                var dialog = new OpenFileDialog {Filter = @"(*.st) | *.st"};

                if (Directory.Exists(EnvironmentVariables.GetProjectsPath))
                    Directory.CreateDirectory(EnvironmentVariables.GetProjectsPath);

                dialog.InitialDirectory = EnvironmentVariables.GetProjectsPath;

                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    OpenProject(dialog.FileName);
                }
            }
        }

        private void PreviousProjectList_CollectionChanged(object sender,
            NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                mnuHistory.DropDownItems.Clear();
                var i = 1;
                ToolStripItem[] list = 
                    Settings.Instance.PreviousProjectList
                        .OrderByDescending(x => x.DateUpd)
                        .Select(x=> new ToolStripMenuItem(string.Format("{0}: {1}",i++, x.ProjectPath)))
                        .Cast<ToolStripItem>().ToArray();

                foreach (var ts in list)
                    ts.Click += mnuHistoryItem_Click;
                
                mnuHistory.DropDownItems.AddRange(list);
            }
            else if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                mnuHistory.DropDownItems.Clear();
            }
        }

        /// <summary>
        /// создать проект из bitmap изображения
        /// </summary>
        /// <param name="bmp"></param>
        public void CreateProjectFromBitmap(Bitmap bmp)
        {
            Project.Instance = new Project();
            Project.Instance.Initialize("default.st", System.IO.Path.GetDirectoryName(Application.ExecutablePath)+"\\default.st");
            SetProjectEvents();
            
            
            Project.Instance.Layers.AddLayer(bmp);
            WorkArea.Instance.Width = bmp.Width;
            WorkArea.Instance.Height = bmp.Height;
            Project.Instance.HasBitmapChanged(Project.Instance.CurrentLayer);
            //Project.Instance.HasBitmapChanged(Project.Instance.CurrentLayer);
            
            //Project.Instance.Bmp = bmp;

            WriteLine(string.Format("Проект {0} создан..",
                Project.Instance.Name));

            Project.Instance.HasProjectLoaded();
            WorkArea.Instance.ReDrawAll();
        }

        /// <summary>
        /// сохранить отредактированное изображение
        /// </summary>
        public void SaveEditedImage()
        {
            Bitmap bitmap = null;
            try
            {
                bitmap = BitmapTools.CombineBitmap(ImageFormat.Png,
                    Settings.Instance.Canvas.Width,
                    Settings.Instance.Canvas.Height);

                using (Graphics g = Graphics.FromImage(frm.current_image))
                {
                    g.Clear(Color.White);
                    g.DrawImage(bitmap,
                            new Rectangle(
                                0,
                                0,
                                bitmap.Width,
                                bitmap.Height));

                }

                //frm.current_image = bitmap;
                frm.SetImage(frm.current_image);
            }
                // ReSharper disable RedundantCatchClause
            catch (Exception exc)
            {
                throw;
            }
                // ReSharper restore RedundantCatchClause
            finally
            {
                if (bitmap != null)
                    bitmap.Dispose();
            }
        
        }


        private void OpenProject(string path)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                Project.Instance = Project.Instance.Load(path);
                SetProjectEvents();
                Project.Instance.HasProjectLoaded();
                WorkArea.Instance.ReDrawAll();
            }
            catch (Exception exc)
            {
                eventLogMain.WriteEntry(
                    string.Format("Ошибка чтения файла проекта: {0}{1}",
                        Environment.NewLine + path
                        + Environment.NewLine + Environment.NewLine,
                        exc),
                    EventLogEntryType.Error);

                MessageBox.Show(@"При открытии файла проекта возникла ошибка. "
                                + Environment.NewLine + @"Подробности в Event Viewer. (F1)",
                    @"Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }
        }

        private void SetHistory()
        {
            Settings.Instance.PreviousProjectList.AddItem(Project.Instance.Path);
            Settings.Instance.Save();
        }

        private void SetWorkArea(bool show)
        {
            if (show)
            {
                WorkArea.Instance.Location = new Point(
                    ((Width - WorkArea.Instance.Width) / 2),
                    ((Height - WorkArea.Instance.Height) / 2) - 80);

                Controls.Add(WorkArea.Instance);                
            }
            else
            {
                Control wa = Controls.Cast<Control>()
                    .FirstOrDefault(x => x as WorkArea != null);

                if (wa != null)
                    Controls.Remove(wa);
            }
        }

        private void mnuHistoryItem_Click(object sender, EventArgs e)
        {
            var fileName = "";
            if (CloseProject())
            {
                try
                {
                    fileName = ((ToolStripMenuItem)sender).Text;
                    fileName = (fileName.Split(new string[] { ": " },
                        StringSplitOptions.RemoveEmptyEntries))[1];
                    if (!File.Exists(fileName))
                    {
                        eventLogMain.WriteEntry(
                        string.Format(string.Format(@"Файл проекта: ""{0}"" "
                            + Environment.NewLine + @"- не обнаружен."
                            + Environment.NewLine + @"Не возможно открыть проект."
                            , fileName)
                            + Environment.NewLine + Environment.NewLine + "{1}",
                            Project.Instance.Path, Environment.NewLine),
                        EventLogEntryType.Information);

                        MessageBox.Show(string.Format(@"Файл проекта: ""{0}"" "
                            + Environment.NewLine + @"- не обнаружен."
                            + Environment.NewLine + @"Не возможно открыть проект."
                            , fileName),
                        Environment.NewLine + @"Указанный файл отсутвует",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    }
                    else
                    {
                        OpenProject(fileName);
                    }
                }
                catch (Exception exc)
                {
                    eventLogMain.WriteEntry(
                    string.Format(string.Format(@"Файл проекта: ""{0}"" "
                        + Environment.NewLine + @"- не обнаружен."
                        + Environment.NewLine + @"Не возможно открыть проект."
                        , fileName)
                        + Environment.NewLine + Environment.NewLine + "{1}",
                        Project.Instance.Path, Environment.NewLine),
                    EventLogEntryType.Error);
                }
            }
        }

        private void Instance_ActiveLayerChanged(object sender, EventArgs e)
        {
            Layer layer = Project.Instance.Layers.FirstOrDefault(x => x.Id ==
                                                                      Project.Instance.ActiveLayerGuid);

            if (layer != null)
                WriteLine(string.Format(@"Выделен слой ""{0}"".", layer.Caption));
        }

        private void Instance_BitmapChanged(object sender, LayerChangeEventArgs e)
        {
            WriteLine(string.Format(@"Обновлён слой ""{0}"".",
                e.GetState.Caption));
        }

        private void Instance_ProjectChanged(object sender, LayerChangeEventArgs e)
        {
            WriteLine("Проект был изменён.");
        }

        private void Instance_OrderChanged(object sender, EventArgs e)
        {
            WriteLine("Порядок слоёв изменился.");
        }

        private void SaveSettings()
        {
            try
            {
                Settings.Instance.Save();
            }
            catch (Exception exc)
            {
                eventLogMain.WriteEntry(
                    string.Format(@"Ошибка сохранения файла настроек проекта: {0}{1}",
                        Environment.NewLine
                        + Project.Instance.Path
                        + Environment.NewLine
                        + Environment.NewLine,
                        exc),
                    EventLogEntryType.Error);
            }
        }

        private void SaveProject()
        {
            if (Project.Instance != null && Project.Instance.IsChanged)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;
                    SaveEditedImage();
                    //Project.Instance.Save();

                    WriteLine(string.Format("Проект {0} сохранён..",
                        Project.Instance.Name));
                }
                catch (Exception exc)
                {
                    eventLogMain.WriteEntry(
                        string.Format(@"Ошибка сохранения файла проекта: {0}{1}",
                            Environment.NewLine
                            + Project.Instance.Path
                            + Environment.NewLine
                            + Environment.NewLine,
                            exc),
                        EventLogEntryType.Error);

                    MessageBox.Show(
                        @"При сохранении файла проекта возникла ошибка. "
                        + Environment.NewLine +
                        @"Подробности в Event Viewer. Помощь (F1): Просмотр ошибок.",
                        @"Ошибка", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                finally
                {
                    Cursor = Cursors.Arrow;
                }
            }
        }

        private void SetEnabledMenu(bool enable)
        {
            mnuSave.Enabled = enable;
            mnuSaveAs.Enabled = enable;
            mnuProj.Enabled = enable;
            mnuOptions.Enabled = enable;
            mnuClose.Enabled = enable;
            mnuNewLayer.Enabled = enable;
        }

        private void mnuNewProj_Click(object sender, EventArgs e)
        {
            CreateProjectDialog();
        }

        private void CreateProjectForm_Closing(object sender, CancelEventArgs e)
        {
            var form = ((NewProjectForm) sender);
            if (form.Result == DialogResult.OK)
            {
                CreateProject(form.ProjectName, form.ProjectPath);
            }
        }

        private void mnuNewLayer_Click(object sender, EventArgs e)
        {
            if (Project.Instance != null)
                Project.Instance.Layers.AddLayer();
            else
                ShowStartForm();
        }

        private void WriteStatus(string message)
        {
            toolStripStatusLabelMain.Text = message;
        }

        private void CreateProjectDialog()
        {
            var form = new NewProjectForm();
            form.Closing += CreateProjectForm_Closing;
            form.ShowDialog(this);
        }

        private void SetProjectEvents()
        {
            Project.Instance.Layers.CollectionChanged += Layers_CollectionChanged;
            Project.Instance.OrderChanged += Instance_OrderChanged;
            Project.Instance.ProjectChanged += Instance_ProjectChanged;
            Project.Instance.BitmapChanged += Instance_BitmapChanged;
            Project.Instance.ActiveLayerChanged += Instance_ActiveLayerChanged;
            Project.Instance.ProjectLoaded += Instance_ProjectLoaded;
            Project.Instance.ProjectClosed += Instance_ProjectClosed;

            WorkArea.Instance.SetProjectEvents();
            LayersToolWindowControl.Instance.SetProjectEvents();
            ActionToolWindowControl.Instance.SetProjectEvents();
        }

        void Instance_ProjectClosed(object sender, EventArgs e)
        {
            eventMonitor.Clear();
            SetTitleDefault();
        }

        void Instance_ProjectLoaded(object sender, EventArgs e)
        {
            SetLayersForm(true);
            SetColorsForm(true);
            SetBrushWidth(true);
            SetEditModeForm(true);
            SetOutput(false);
            SetEnabledMenu(true);
            SetWorkArea(true);

            SetHistory();
            SetTitle();

            WriteLine(string.Format("Открыт проект {0} ..",
                Project.Instance.Name));

            Project.Instance.IsChanged = false;
        }

        private void CreateProject(string name, string path)
        {
            Project.Instance = new Project();
            Project.Instance.Initialize(name, path);
            SetProjectEvents();

            Project.Instance.Layers.AddLayer();

            WriteLine(string.Format("Проект {0} создан..",
                Project.Instance.Name));

            Project.Instance.HasProjectLoaded();
        }

        private void SetTitleDefault()
        {
            Text = EnvironmentVariables.GetProjectName(); 
        }

        private void SetTitle()
        {
            Text = string.Format("{0} - {1} ({2})",
                EnvironmentVariables.GetProjectName(),
                Project.Instance.Name,
                Project.Instance.Path);
        }

        private void mnuConsole_Click(object sender, EventArgs e)
        {
            SetOutput(!eventMonitor.Visible);
            mnuOutput.Checked = eventMonitor.Visible;
        }
    }
}