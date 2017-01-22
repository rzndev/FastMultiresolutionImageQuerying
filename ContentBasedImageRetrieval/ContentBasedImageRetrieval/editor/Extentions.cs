using System;
using System.Drawing;

namespace ContentBasedImageRetrieval
{
    public static class Extentions
    {
        public static DateTime ToDateTime(this DateTime? val)
        {
            return val != null ? Convert.ToDateTime(val) : DateTime.MinValue;
        }

        public static string ToReachFormat(this DateTime? val)
        {
            return val != null ? val.ToDateTime().ToString("yyyy.MM.dd HH:mm") : string.Empty;
        }

        public static Bitmap Copy(this Bitmap original)
        {
            var copy = new Bitmap(original.Width, original.Height);
            using (Graphics graphics = Graphics.FromImage(copy))
            {
                var imageRectangle = new Rectangle(0, 0, copy.Width, copy.Height);
                graphics.DrawImage(original, imageRectangle, imageRectangle, GraphicsUnit.Pixel);
            }
            return copy;
        }
    }
}