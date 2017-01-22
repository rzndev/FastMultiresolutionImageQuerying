using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Xml.Serialization;

namespace ContentBasedImageRetrieval
{
    [Serializable]
    public class Layer
    {
        [XmlIgnore] 
        private Bitmap _bmp;

        public Bitmap BackImage = null;

        [XmlIgnore] 
        private ObservableCollection<Strip> _strips;

        [XmlElement] 
        public Point Location { get; set; }

        [XmlElement] 
        public int OrderId { get; set; }

        [XmlElement] 
        public Guid Id { get; set; }

        [XmlElement] 
        public int Number { get; set; }

        [XmlElement] 
        public string Caption { get; set; }

        [XmlElement] 
        public bool Visible { get; set; }

        [XmlIgnore]
        public Bitmap Bmp
        {
            get
            {
                return _bmp ?? (_bmp = new Bitmap(
                    Settings.Instance.Canvas.Height,
                    Settings.Instance.Canvas.Width));
            }
            set { _bmp = value; }
        }

        //[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        //[XmlElement("Bmp")]
        //// ReSharper disable UnusedMember.Global
        //public byte[] BmpSerialized
        //// ReSharper restore UnusedMember.Global
        //{
        //    get
        //    {
        //        if (Bmp == null) return null;
        //        using (var ms = new MemoryStream())
        //        {
        //            Bmp.Save(ms, ImageFormat.Png);
        //            return ms.ToArray();
        //        }
        //    }
        //    set
        //    {
        //        if (value == null)
        //        {
        //            Bmp = null;
        //        }
        //        else
        //        {
        //            using (var ms = new MemoryStream(value))
        //            {
        //                Bmp = new Bitmap(ms);
        //            }
        //        }
        //    }
        //}

        [XmlIgnore]
        public Image Thumbnail
        {
            get
            {
                return Bmp.GetThumbnailImage(
                    Settings.Instance.Canvas.Thumbnail.Width,
                    Settings.Instance.Canvas.Thumbnail.Height,
                    null, new IntPtr());
            }
        }

        [XmlArray]
        [XmlArrayItem(typeof (Strip))]
        public ObservableCollection<Strip> Strips
        {
            get { return _strips ?? (_strips = new ObservableCollection<Strip>()); }
            // ReSharper disable UnusedMember.Global
            set { _strips = value; }
            // ReSharper restore UnusedMember.Global
        }
    }
}