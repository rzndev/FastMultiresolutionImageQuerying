using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Xml.Serialization;

namespace ContentBasedImageRetrieval

{
    [Serializable]
    public class Strip : IDisposable
    {
        public int OrderId { get; set; }

        // GraphicsPath //
        //[XmlIgnore]
        //GraphicsPath _gPath;

        //[XmlIgnore]
        //public GraphicsPath GPath
        //{
        //    get
        //    {
        //        if (PathPoints.Count() > 0 && PathTypes.Count() > 0)
        //            return _gPath ?? (_gPath = new GraphicsPath(PathPoints, PathTypes));
        //        return _gPath ?? (_gPath = new GraphicsPath());
        //    }
        //    set
        //    {
        //        if (((GraphicsPath)value).PointCount == 0)
        //        {
        //            PathTypes = new Byte[0];
        //            PathPoints = new PointF[0];
        //        }
        //        else
        //        {
        //            PathTypes = value.PathTypes;
        //            PathPoints = value.PathPoints;
        //        }
        //    }
        //}

        [XmlIgnore]
        public GraphicsPath GPath { get; set; }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [XmlElement("GPath")]
        public byte[] GPathSerialized
        {
            get
            {
                if (GPath == null) return null;
                using (var ms = new MemoryStream())
                    return ((System.IO.MemoryStream)GraphicsPathData
                        .Serialize(new GraphicsPathData(GPath))).ToArray();
            }
            set
            {
                if (value == null)
                    GPath = null;
                else
                    using (var ms = new MemoryStream(value))
                        GPath = GraphicsPathData.GetGraphicsPath(ms);
            }
        }

        //private PointF[] _pathPoints;

        //private PointF[] PathPoints
        //{
        //    get { return _pathPoints ?? (_pathPoints = new PointF[0] { }); }
        //    set { _pathPoints = value; }
        //}

        //private Byte[] _pathTypes;

        //private Byte[] PathTypes
        //{
        //    get { return _pathTypes ?? (_pathTypes = new Byte[0] { }); }
        //    set { _pathTypes = value; }
        //}

        // end of GraphicsPath //

        [XmlElement(Type = typeof(SerializableColor))]
        public Color FColor { get; set; }

        [XmlElement]
        public float FWidth { get; set; }

        [XmlElement]
        public bool IsSelected { get; set; }

        [XmlElement]
        public RectangleF FLocation { get; set; }

        [XmlIgnore]
        //[XmlArray]
        //[XmlArrayItem(typeof(Strata.EraseObject))]
        public List<EraseObject> ErasingObjects { get; set; }

        [XmlElement]
        public float Rotation { get; set; }

        public void Dispose()
        {
            GPath.Dispose();

            if (ErasingObjects != null)
                for (var i = ErasingObjects.Count - 1; i >= 0; i--)
                    ErasingObjects[i].Dispose();
        }

        public void Render(Graphics g, Pen pen)
        {
            using (var fPath = new GraphicsPath())
            {
                fPath.FillMode = FillMode.Winding;
                if (ErasingObjects != null)
                {
                    for (int i = 0; i < ErasingObjects.Count; i++)
                    {
                        using (var gP = (GraphicsPath)ErasingObjects[i].ErasingPath.Clone())
                        {
                            using (var p = new Pen(Color.Black, ErasingObjects[i].ErasingWidth)
                            {
                                EndCap = LineCap.Round,
                                StartCap = LineCap.Round,
                                LineJoin = LineJoin.Round
                            })
                            {
                                gP.Widen(p);

                                var m = new Matrix(1, 0, 0, 1, 0, 0);

                                // ReSharper disable CompareOfFloatsByEqualityOperator
                                if (ErasingObjects[i].ErasingRot != 0)
                                // ReSharper restore CompareOfFloatsByEqualityOperator
                                {
                                    m.RotateAt(-ErasingObjects[i].ErasingRot, 
                                        new Point(0, 0), MatrixOrder.Prepend);
                                }

                                gP.Transform(m);

                                fPath.AddPath(gP, false);
                            }
                        }
                    }
                }

                using (var gP = (GraphicsPath)GPath.Clone())
                {
                    gP.Transform(new Matrix(1, 0, 0, 1, FLocation.X, FLocation.Y));
                    if (this.IsSelected)
                        using (var p = new Pen(Color.Black, Math.Max(FWidth, 10))
                        {
                            EndCap = LineCap.Round,
                            StartCap = LineCap.Round,
                            LineJoin = LineJoin.Round
                        })
                            gP.Widen(p);
                    else
                        gP.Widen(pen);

                    fPath.Transform(new Matrix(1, 0, 0, 1, FLocation.X, FLocation.Y));
                    var con = g.BeginContainer();

                    // ReSharper disable CompareOfFloatsByEqualityOperator
                    if (Rotation != 0f)
                    // ReSharper restore CompareOfFloatsByEqualityOperator
                    {
                        var mx = g.Transform;

                        mx.RotateAt(this.Rotation, 
                            new PointF(FLocation.X, FLocation.Y),
                            MatrixOrder.Append);

                        g.Transform = mx;
                    }

                    using (var reg = new Region(gP))
                    {
                        reg.Exclude(fPath);
                        g.Clip = reg;
                        g.SmoothingMode = SmoothingMode.AntiAlias; 
                        g.DrawPath(pen, gP);
                    }

                    g.EndContainer(con);
                }
            }
        }
    }
}