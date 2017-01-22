using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace ContentBasedImageRetrieval
{
    public class Project
    {
        #region elements

        [XmlIgnore] private Bitmap _bmp;
        //[XmlIgnore] GraphicsPath _gPath;

        [XmlIgnore]
        public Layer CurrentLayer
        {
            get { return Layers.FirstOrDefault(x => x.Id == ActiveLayerGuid); }
            set { ActiveLayerGuid = value.Id; }
        }

        [XmlElement]
        public string Name { get; set; }

        [XmlElement]
        public Guid ProjectGuid { get; set; }

        [XmlElement]
        public DateTime? CreateDate { get; set; }

        [XmlElement]
        public DateTime? LastChangesDate { get; set; }

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
        //public byte[] BmpSerialized
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

        [XmlElement]
        public bool IsTracking { get; set; }

        [XmlElement]
        public bool IsMoving { get; set; }

        [XmlElement]
        public bool IsDoErase { get; set; }

        [XmlElement]
        public Strip CurrentStrip { get; set; }

        [XmlElement]
        public int X { get; set; }

        [XmlElement]
        public int Y { get; set; }

        [XmlIgnore]
        public PointF[] MovePoints { get; set; }

        // GraphicsPath //

        [XmlIgnore]
        public GraphicsPath GPath { get; set; }

        //[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        //[XmlElement("GPath")]
        //public byte[] GPathSerialized
        //{
        //    get
        //    {
        //        if (GPath == null) return null;
        //        using (var ms = new MemoryStream())
        //            return ((System.IO.MemoryStream)GraphicsPathData
        //                .Serialize(new GraphicsPathData(GPath))).ToArray();
        //    }
        //    set
        //    {
        //        if (value == null)
        //            GPath = null;
        //        else
        //            using (var ms = new MemoryStream(value))
        //                GPath = GraphicsPathData.GetGraphicsPath(ms);
        //    }
        //}

        
        //{
        //    get
        //    {
        //        if (PathPoints.Count() > 0 && PathTypes.Count() > 0)
        //            return _gPath ?? (_gPath = new GraphicsPath(PathPoints, PathTypes));
        //        return _gPath ?? (_gPath = new GraphicsPath());
        //    }
        //    set
        //    {
        //        PathTypes = value.PathTypes;
        //        PathPoints = value.PathPoints;
        //    }
        //}

        //private PointF[] _pathPoints;

        //public PointF[] PathPoints
        //{
        //    get { return _pathPoints ?? (_pathPoints = new PointF[0] { }); }
        //    set { _pathPoints = value; }
        //}

        //private Byte[] _pathTypes;

        //public Byte[] PathTypes
        //{
        //    get { return _pathTypes ?? (_pathTypes = new Byte[0] { }); }
        //    set { _pathTypes = value; }
        //}
        // end of GraphicsPath //

        [XmlElement]
        public ObservableCollection<Layer> Layers
        {
            get { return _layers ?? (_layers = new ObservableCollection<Layer>()); }
            set { _layers = value; }
        }

        #endregion

        #region xml ignore 

        [XmlIgnore] private Guid _activeLayerGuid;
        [XmlIgnore] private ObservableCollection<Layer> _layers;

        [XmlIgnore] private Guid _prevActiveLayerGuid;

        [XmlIgnore]
        public static Project Instance { get; set; }

        [XmlIgnore]
        public string Path { get; set; }

        [XmlElement]
        public Guid ActiveLayerGuid
        {
            get { return _activeLayerGuid; }
            set
            {
                if (_prevActiveLayerGuid != value)
                {
                    _prevActiveLayerGuid = value;
                    _activeLayerGuid = value;
                    HasActiveLayerChanged();
                }
            }
        }

        #endregion

        #region  has bitmap changed event

        public void HasBitmapChanged(Layer layer)
        {
            RaiseBitmapChangeEvent(layer);
        }

        private void RaiseBitmapChangeEvent(Layer layer)
        {
            BitmapEventRaiser(new LayerChangeEventArgs(layer));
        }

        public event EventHandler<LayerChangeEventArgs> BitmapChanged;

        private void BitmapEventRaiser(LayerChangeEventArgs e)
        {
            if (BitmapChanged != null)
                BitmapChanged(this, e);
        }

        #endregion

        #region has active layer changed event

        private void HasActiveLayerChanged()
        {
            RaiseActiveLayerChangeEvent();
        }

        private void RaiseActiveLayerChangeEvent()
        {
            EventRaiser(new EventArgs());
        }

        public event EventHandler<EventArgs> ActiveLayerChanged;

        private void EventRaiser(EventArgs e)
        {
            if (ActiveLayerChanged != null)
                ActiveLayerChanged(this, e);
        }

        #endregion

        #region has order changes event

        public void HasOrderChanged()
        {
            IsChanged = true;
            RaiseOrderChangeEvent();
        }

        private void RaiseOrderChangeEvent()
        {
            OrderEventRaiser(new EventArgs());
        }

        public event EventHandler<EventArgs> OrderChanged;

        private void OrderEventRaiser(EventArgs e)
        {
            if (OrderChanged != null)
                OrderChanged(this, e);
        }

        #endregion

        #region has changes event

        [XmlIgnore]
        public bool IsChanged { get; set; }

        public void HasChanged(Layer layer)
        {
            IsChanged = true;
            RaiseChangeEvent(layer);
        }

        private void RaiseChangeEvent(Layer layer)
        {
            EventRaiser(new LayerChangeEventArgs(layer));
        }

        public event EventHandler<LayerChangeEventArgs> ProjectChanged;

        private void EventRaiser(LayerChangeEventArgs e)
        {
            if (ProjectChanged != null)
                ProjectChanged(this, e);
        }

        #endregion

        #region loaded event

        public void HasProjectLoaded()
        {
            RaiseProjectLoadedEvent();
        }

        private void RaiseProjectLoadedEvent()
        {
            ProjectLoadedRaiser(new EventArgs());
        }

        public event EventHandler<EventArgs> ProjectLoaded;

        private void ProjectLoadedRaiser(EventArgs e)
        {
            if (ProjectLoaded != null)
                ProjectLoaded(this, e);
        }

        #endregion

        #region closed event

        public void HasProjectClosed()
        {
            RaiseProjectClosedEvent();
        }

        private void RaiseProjectClosedEvent()
        {
            ProjectClosedRaiser(new EventArgs());
        }

        public event EventHandler<EventArgs> ProjectClosed;

        private void ProjectClosedRaiser(EventArgs e)
        {
            if (ProjectClosed != null)
                ProjectClosed(this, e);
        }

        #endregion

        // EVENTS
    }
}