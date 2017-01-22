using System.Drawing;
using System.Xml.Serialization;

namespace ContentBasedImageRetrieval
{
    public class Canvas
    {
        [XmlIgnore]
        public Size Size
        {
            get { return new Size {Height = Height, Width = Width}; }
            set
            {
                Height = value.Height;
                Width = value.Width;
            }
        }

        public int Height { get; set; }
        public int Width { get; set; }

        public Thumbnail Thumbnail { get; set; }
    }
}