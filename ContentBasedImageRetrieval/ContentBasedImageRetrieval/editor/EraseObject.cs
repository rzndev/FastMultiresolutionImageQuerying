using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Xml.Serialization;

namespace ContentBasedImageRetrieval
{
    [Serializable]
    public class EraseObject : IDisposable
    {
        // GraphicsPath //
        [XmlIgnore]
        GraphicsPath _gPath;
 
        [XmlIgnore]
        public GraphicsPath ErasingPath
        {
            get
            {
                if (PathPoints.Count() > 0 && PathTypes.Count() > 0)
                    return _gPath ?? (_gPath = new GraphicsPath(PathPoints, PathTypes));
                return _gPath ?? (_gPath = new GraphicsPath());
            }
            set
            {
                if (((GraphicsPath)value).PointCount == 0)
                {
                    PathTypes = new Byte[0];
                    PathPoints = new PointF[0];
                }
                else
                {
                    PathTypes = value.PathTypes;
                    PathPoints = value.PathPoints;
                }
            }
        }

        private PointF[] _pathPoints;

        private PointF[] PathPoints
        {
            get { return _pathPoints ?? (_pathPoints = new PointF[0] { }); }
            set { _pathPoints = value; }
        }

        private Byte[] _pathTypes;

        private Byte[] PathTypes
        {
            get { return _pathTypes ?? (_pathTypes = new Byte[0] { }); }
            set { _pathTypes = value; }
        }
        // end of GraphicsPath //
         
        [XmlElement]
        public float ErasingWidth { get; set; }

        [XmlElement]
        public float ErasingRot { get; set; }

        public void Dispose()
        {
            if (ErasingPath != null)
                ErasingPath.Dispose();
        }
    }
}