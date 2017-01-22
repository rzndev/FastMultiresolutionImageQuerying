using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace ContentBasedImageRetrieval
{
    public partial class OptionsForm : Form
    {
        public OptionsForm()
        {
            InitializeComponent();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void OptionsForm_Load(object sender, EventArgs e)
        {
            if (Project.Instance != null)
            {
                var fi = new FileInfo(Project.Instance.Path);
                if (fi.Exists)
                {
                    labelFileSize.Text =
                        fi.Length >= 0x400 
                            ? string.Format("{0} ({1} B)", StrFormatByteSize(fi.Length), fi.Length)
                            : StrFormatByteSize(fi.Length); 
                }

                textBoxProjName.Text = Project.Instance.Name;
                textBoxProjFullPath.Text = Project.Instance.Path;
                labelCreateDate.Text = Project.Instance.CreateDate.ToReachFormat();
                labelLastChangesDate.Text = Project.Instance.LastChangesDate.ToReachFormat();
                labelLayerCount.Text = Convert.ToString(Project.Instance.Layers != null &&
                                                         Project.Instance.Layers.Count != 0
                    ? Project.Instance.Layers.Count
                    : 0);
            }
        }

        private void OptionsForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }


        /// <summary>
        /// Converts a numeric value into a string that represents the number expressed as a size value in bytes, kilobytes, megabytes, or gigabytes, depending on the size.
        /// </summary>
        /// <param name="filelength">The numeric value to be converted.</param>
        /// <returns>the converted string</returns>
        public static string StrFormatByteSize(long filesize)
        {
            StringBuilder sb = new StringBuilder(11);
            StrFormatByteSize(filesize, sb, sb.Capacity);
            return sb.ToString();
        }

        [DllImport("Shlwapi.dll", CharSet = CharSet.Auto)]
        private static extern long StrFormatByteSize(
                long fileSize
                , [MarshalAs(UnmanagedType.LPTStr)] StringBuilder buffer
                , int bufferSize);
    }
}