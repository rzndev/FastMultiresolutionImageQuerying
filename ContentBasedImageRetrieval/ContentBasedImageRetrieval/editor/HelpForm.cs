using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace ContentBasedImageRetrieval
{
    public partial class HelpForm : Form
    {
        public HelpForm()
        {
            InitializeComponent();

            Assembly thisExe = Assembly.GetExecutingAssembly();
            using (Stream stream = thisExe.GetManifestResourceStream("Strata.help.html"))
            using (var reader = new StreamReader(stream))
            {
                webBrowser1.DocumentText = reader.ReadToEnd();
            }
        }
    }
}