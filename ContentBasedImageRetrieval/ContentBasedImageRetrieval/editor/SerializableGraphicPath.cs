using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

namespace ContentBasedImageRetrieval
{
    [Serializable]
    public class GraphicsPathData 
    {
        // ReSharper disable FieldCanBeMadeReadOnly.Local
        // ReSharper disable InconsistentNaming
        byte[] types;
        PointF[] points;
        FillMode fillMode;
        // ReSharper restore InconsistentNaming
        // ReSharper restore FieldCanBeMadeReadOnly.Local

        public GraphicsPathData(GraphicsPath gp)
        {
            this.types = gp.PathTypes;
            this.points = gp.PathPoints;
            this.fillMode = gp.FillMode;
        }

        public static System.IO.Stream Serialize(GraphicsPathData gpd)
        {
            var ms = new System.IO.MemoryStream();
            var bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            bf.Serialize(ms, gpd);
            ms.Flush();
            return ms;
        }

        public static GraphicsPathData Deserialize(System.IO.Stream stream)
        {
            var bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

            object obj = bf.Deserialize(stream);
            var gpd = obj as GraphicsPathData;
            return gpd;
        }

        public static GraphicsPath GetGraphicsPath(GraphicsPathData gpd)
        {
            return new GraphicsPath(gpd.points, gpd.types, gpd.fillMode);
        }

        public static GraphicsPath GetGraphicsPath(System.IO.Stream gpdStream)
        {
            return GetGraphicsPath(Deserialize(gpdStream));
        }
    }
 
}
