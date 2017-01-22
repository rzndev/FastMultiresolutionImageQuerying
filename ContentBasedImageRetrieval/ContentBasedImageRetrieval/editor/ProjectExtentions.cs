using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace ContentBasedImageRetrieval
{
    public static class ProjectExtentions
    {
        public static void AddLayer(this ObservableCollection<Layer> layers, Bitmap bmp = null)
        {
            var layer = new Layer();
            layer.SetBmp(bmp);
            layer.SetGuid();
            layer.SetNumber();
            layer.SetOrder();
            layer.SetCaption();
            layer.SetVisible();

            layers.Add(layer);
            layer.SetActive();
        }

        public static void Close(this Project project)
        {
            project.Layers.Clear();
            project.HasProjectClosed();
            Project.Instance = null;
        }

        public static void Initialize(this Project project, string name, string path)
        {
            project.Name = name;
            project.ProjectGuid = Guid.NewGuid();
            project.CreateDate = DateTime.Now;
            project.Path = path;
            project.Save();
        }

        public static void RemoveAllLayers(this ObservableCollection<Layer> layers)
        {
            layers.Clear();
        }

        #region Initialize

        private static void SetVisible(this Layer layer)
        {
            layer.Visible = true;
        }

        private static void SetBmp(this Layer layer, Bitmap bmp)
        {
            layer.Bmp = new Bitmap(
                    Settings.Instance.Canvas.Width,
                    Settings.Instance.Canvas.Height);
            if (null == bmp)
            {
                layer.BackImage = null;
            }
            else
            {
                layer.BackImage = new Bitmap(bmp.Width, bmp.Height);
                Settings.Instance.Canvas.Width = bmp.Width;
                Settings.Instance.Canvas.Height = bmp.Height;
                using (Graphics g = Graphics.FromImage(layer.BackImage))
                {
                    g.Clear(Color.Transparent);
                    g.DrawImage(bmp,
                            new Rectangle(
                                0,
                                0,
                                bmp.Width,
                                bmp.Height));
                }
            }
        }

        private static void SetActive(this Layer layer)
        {
            Project.Instance.ActiveLayerGuid = layer.Id;
        }

        private static void SetCaption(this Layer layer, string name = "")
        {
            layer.Caption = string.IsNullOrEmpty(name)
                ? string.Format("Слой {0}", layer.Number)
                : name;
        }

        private static void SetNumber(this Layer layer)
        {
            if (Project.Instance.Layers.Any())
            {
                layer.Number =
                    Project.Instance.Layers
                        .Max(x =>
                        {
                            Layer l = x;
                            return l != null ? l.Number : 0;
                        }) + 1;
            }
            else
            {
                layer.Number = 0;
            }
        }

        private static void SetOrder(this Layer layer, int? orderId = null)
        {
            if (orderId == null)
            {
                layer.OrderId = 0;

                foreach (Layer item in Project.Instance.Layers)
                    item.OrderId = item.OrderId + 1;
            }
        }
        #region save n load
        public static Project Load(this Project project, string path = "")
        {
            var x = new XmlSerializer(typeof(Project));
            using (var fs = new FileStream(path, FileMode.Open))
                project = (Project)x.Deserialize(fs);
            project.Path = path;
            project.IsChanged = false;
            return project;
        }

        public static void Save(this Project project)
        {
            if (project == null)
                throw new NullReferenceException();

            project.LastChangesDate = DateTime.Now;

            string dir = System.IO.Path.GetDirectoryName(project.Path);
            if (dir != null && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            if (File.Exists(project.Path))
                File.WriteAllText(project.Path, string.Empty);

            var x = new XmlSerializer(project.GetType());
            using (var fs = new FileStream(project.Path, FileMode.OpenOrCreate))
                x.Serialize(fs, project);

            project.IsChanged = false;
        }
        #endregion
        private static void SetGuid(this Layer layer)
        {
            layer.Id = Guid.NewGuid();
        }

        #endregion
    }
}