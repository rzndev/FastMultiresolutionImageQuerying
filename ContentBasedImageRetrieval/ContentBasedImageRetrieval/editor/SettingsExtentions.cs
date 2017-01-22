using System;
using System.Drawing;
using System.IO;
using System.Xml.Serialization;

namespace ContentBasedImageRetrieval
{
    public static class SettingsExtentions
    {
        private const string FileNameTpl = "settings.config";
        // ReSharper disable FieldCanBeMadeReadOnly.Local
        private static string _fileName = null;
        // ReSharper restore FieldCanBeMadeReadOnly.Local

        private static string FileName
        {
            get
            {
                return _fileName ??
                       (Path.Combine(EnvironmentVariables.GetSettingsPath,
                           FileNameTpl));
            }
        }

        public static void Load(this Settings settings)
        {
            if (!File.Exists(FileName))
            {
                // default settings
                settings.Canvas = new Canvas
                {
                    Height = 64,
                    Width = 64,
                    Thumbnail = new Thumbnail
                    {
                        Height = 50,
                        Width = 50
                    }
                };

                settings.Color = Color.Blue;
                settings.Width = 27;
                settings.Mode = EditModeEnum.Draw;
                return;
            }

            // saved settings
            var x = new XmlSerializer(settings.GetType());
            using (var fs = new FileStream(FileName, FileMode.Open))
            {
                var s = (Settings) x.Deserialize(fs);
                settings.Canvas = s.Canvas;
                settings.Color = s.Color;
                settings.Width = s.Width;
                settings.Mode = s.Mode;

                foreach (PreviousProjectItem item in s.PreviousProjectList)
                    settings.PreviousProjectList.Add(item);
            }
        }

        public static void Save(this Settings settings)
        {
            if (settings == null)
                throw new NullReferenceException();

            string dir = Path.GetDirectoryName(FileName);
            if (dir != null && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            if (File.Exists(FileName))
                File.WriteAllText(FileName, string.Empty);

            var x = new XmlSerializer(settings.GetType());
            using (var fs = new FileStream(FileName, FileMode.OpenOrCreate))
                x.Serialize(fs, settings);
        }
    }
}