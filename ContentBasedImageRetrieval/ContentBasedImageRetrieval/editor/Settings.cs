using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Xml.Serialization;

namespace ContentBasedImageRetrieval
{
    public class Settings
    {
        private static Settings _instance;
        private Canvas _canvas;
        [XmlIgnore] private Color _color;
        private EditModeEnum _mode;

        [XmlIgnore] private ObservableCollection<PreviousProjectItem> _previousProjectList;
        private decimal _width;

        public Settings()
        {
            Width = 10;
        }

        public static Settings Instance
        {
            get { return _instance ?? (_instance = new Settings()); }
            set { _instance = value; }
        }

        [XmlArray]
        public ObservableCollection<PreviousProjectItem> PreviousProjectList
        {
            get
            {
                return _previousProjectList
                       ?? (_previousProjectList
                           = new ObservableCollection<PreviousProjectItem>());
            }
            set { _previousProjectList = value; }
        }

        public Canvas Canvas
        {
            get { return _canvas ?? (_canvas = new Canvas()); }
            set
            {
                _canvas = value;
                HasCanvasChanged();
            }
        }

        [XmlElement(Type = typeof (SerializableColor))]
        public Color Color
        {
            get { return _color; }
            set
            {
                _color = value;
                HasColorChanged();
            }
        }

        public decimal Width
        {
            get { return _width; }
            set
            {
                _width = value;
                HasWidthChanged();
            }
        }

        public EditModeEnum Mode
        {
            get { return _mode; }
            set
            {
                _mode = value;
                HasEditModeChanged();
            }
        }

        // color changed event
        public void HasColorChanged()
        {
            RaiseColorChangeEvent();
        }

        private void RaiseColorChangeEvent()
        {
            ColorEventRaiser(new EventArgs());
        }

        public event EventHandler<EventArgs> ColorChanged;

        private void ColorEventRaiser(EventArgs e)
        {
            if (ColorChanged != null)
                ColorChanged(this, e);
        }

        // end of color changed event

        // Width changed event
        public void HasWidthChanged()
        {
            RaiseWidthEvent();
        }

        private void RaiseWidthEvent()
        {
            WidthEventRaiser(new EventArgs());
        }

        public event EventHandler<EventArgs> WidthChanged;

        private void WidthEventRaiser(EventArgs e)
        {
            if (WidthChanged != null)
                WidthChanged(this, e);
        }

        // end of Width changed event

        // canvas changed event
        public void HasCanvasChanged()
        {
            RaiseCanvasChangeEvent();
        }

        private void RaiseCanvasChangeEvent()
        {
            CanvasEventRaiser(new EventArgs());
        }

        public event EventHandler<EventArgs> CanvasChanged;

        private void CanvasEventRaiser(EventArgs e)
        {
            if (CanvasChanged != null)
                CanvasChanged(this, e);
        }

        // end of edit mode changed event


        // edit mode changed event
        public void HasEditModeChanged()
        {
            RaiseEditModeChangeEvent();
        }

        private void RaiseEditModeChangeEvent()
        {
            EditModeEventRaiser(new EventArgs());
        }

        public event EventHandler<EventArgs> EditModeChanged;

        private void EditModeEventRaiser(EventArgs e)
        {
            if (EditModeChanged != null)
                EditModeChanged(this, e);
        }

        // end of edit mode changed event
    }
}