using System;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Media;

namespace KeyInterceptor.WPF.ButtonsView
{
    public class ButtonViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ButtonView Owner { get; set; }
        
        private string _toolTipText;

        public ButtonViewModel(ButtonView owner)
        {
            Owner = owner;
        }

        public string ToolTipText
        {
            get
            {
                return _toolTipText;
            }
            set
            {
                _toolTipText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ToolTipText)));
            }
        }

        public double X
        {
            get => Canvas.GetLeft(Owner);
            set => Canvas.SetLeft(Owner, value);
        }

        public double Y
        {
            get => Canvas.GetTop(Owner);
            set => Canvas.SetTop(Owner, value);
        }


        private ImageSource _pressedSource;
        public ImageSource PressedSkinSource
        {
            get
            {
                return _pressedSource;
            }
            set
            {
                _pressedSource = value;
                Notify(nameof(PressedSkinSource));
            }
        }

        private ImageSource _releasedSource;
        public ImageSource ReleasedSkinSource
        {
            get
            {
                return _releasedSource;
            }
            set
            {
                _releasedSource = value;
                Notify(nameof(ReleasedSkinSource));
            }
        }

        public double Width { get; set; }

        public double Height { get; set; }

        private void Notify(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}