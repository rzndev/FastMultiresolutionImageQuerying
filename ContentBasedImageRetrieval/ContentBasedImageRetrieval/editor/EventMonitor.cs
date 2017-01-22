using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ContentBasedImageRetrieval
{
    public partial class EventMonitor : UserControl
    {
        private const bool ShowTime = false; // Option
        private const int MaxLength = 50;
        private readonly ObservableCollection<MessageItem> _messages;

        public EventMonitor()
        {
            InitializeComponent();
            _messages = new ObservableCollection<MessageItem>();
            _messages.CollectionChanged += _messages_CollectionChanged;
        }

        private void _messages_CollectionChanged(object sender,
            NotifyCollectionChangedEventArgs e)
        {
            string alltext = string.Join(Environment.NewLine,
                _messages.OrderByDescending(x => x.EventTime)
                    .Take(MaxLength)
                    .Select(x => x.ToString()));

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    textBoxMessagges.Text = alltext;

                    break;
                case NotifyCollectionChangedAction.Reset:
                    textBoxMessagges.Text = string.Empty;
                    break;
            }
        }

        public void Clear()
        {
            _messages.Clear();
        }

        public void WriteLine(string message)
        {
            _messages.Add(new MessageItem(message));
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            _messages.Clear();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new SaveFileDialog();
            string path = Environment.ExpandEnvironmentVariables(@"%APPDATA%\LOG\");

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            saveFileDialog.InitialDirectory = path;
            saveFileDialog.Filter = String.Format("Лог файл|*.{0}", "TXT");
            saveFileDialog.DefaultExt = "TXT";
            saveFileDialog.AddExtension = true;
            DialogResult result = saveFileDialog.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                string txt =
                    string.Join(Environment.NewLine,
                        _messages.OrderByDescending(x => x.EventTime)
                            .Select(x => x.ToString()).ToArray());

                string filepath = Path.Combine(path, "log.txt");

                using (StreamWriter sw = File.Exists(filepath)
                    ? File.AppendText(filepath)
                    : File.CreateText(filepath))
                {
                    sw.Write(txt);
                    sw.Close();
                }
            }
        }

        private void toolStripButtonHide_Click(object sender, EventArgs e)
        {
            Visible = false;
        }

        private class MessageItem
        {
            public MessageItem(string message)
            {
                EventTime = DateTime.Now;
                Message = message;
            }

            public DateTime EventTime { get; private set; }
            private string Message { get; set; }

            public override string ToString()
            {
                // ReSharper disable ConditionIsAlwaysTrueOrFalse
                // ReSharper disable UnreachableCode
                return ShowTime
                    ? string.Format("{0:yyyy.MM.dd HH24:mm:ss}:> {1}", EventTime, Message)
                    : string.Format(":> {0}", Message);
                // ReSharper restore UnreachableCode
                // ReSharper restore ConditionIsAlwaysTrueOrFalse
            }
        }
    }
}