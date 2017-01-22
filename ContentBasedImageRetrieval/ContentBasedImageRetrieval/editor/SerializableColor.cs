using System;
using System.Drawing;
using System.Xml.Serialization;

namespace ContentBasedImageRetrieval
{
    public class SerializableColor
    {
        private Color _color = Color.Transparent;

        public SerializableColor()
        {
        }

        public SerializableColor(Color c)
        {
            _color = c;
        }

        [XmlAttribute]
        public string Web
        {
            get { return ColorTranslator.ToHtml(_color); }
            set
            {
                try
                {
                    if (Alpha == 0xFF) // preserve named color value if possible
                        _color = ColorTranslator.FromHtml(value);
                    else
                        _color = Color.FromArgb(Alpha, ColorTranslator.FromHtml(value));
                }
                catch (Exception)
                {
                    _color = Color.Transparent;
                }
            }
        }

        [XmlAttribute]
        public byte Alpha
        {
            get { return _color.A; }
            set
            {
                if (value != _color.A) // avoid hammering named color if no alpha change
                    _color = Color.FromArgb(value, _color);
            }
        }


        public Color ToColor()
        {
            return _color;
        }

        public void FromColor(Color c)
        {
            _color = c;
        }

        public static implicit operator Color(SerializableColor x)
        {
            return x.ToColor();
        }

        public static implicit operator SerializableColor(Color c)
        {
            return new SerializableColor(c);
        }

        public bool ShouldSerializeAlpha()
        {
            return Alpha < 0xFF;
        }
    }
}