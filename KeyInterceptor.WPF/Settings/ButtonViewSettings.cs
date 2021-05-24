using System;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace KeyInterceptor.WPF.Settings
{
    class ButtonViewSettings : Settings<ButtonViewOptions>
    {
        private static readonly string[] _headers = new[]
        {
            "KeyCode",
            "X",
            "Y",
            "Width",
            "Height",
            "PressedImage",
            "ReleasedImage",
            "FriendlyName"
        };

        protected override string[] ColumnHeaders { get { return _headers; } }

        public ButtonViewSettings(string fileName) : base(fileName)
        { }

        protected override ButtonViewOptions ParseEntry(string sourceLine)
        {
            string[] parts = sourceLine.Split(Separator);
            Key keyCode = (Key)Enum.Parse(typeof(Key), parts[0]);
            double x = double.Parse(parts[1]);
            double y = double.Parse(parts[2]);
            double width = double.Parse(parts[3]);
            double height = double.Parse(parts[4]);
            ImageSource pressedImageSource = string.IsNullOrWhiteSpace(parts[5])
                ? StaticResources.PressedImageSource
                : CreateImageSource(parts[5]);
            ImageSource releasedImageSource = string.IsNullOrWhiteSpace(parts[6])
                ? StaticResources.ReleasedImageSource
                : CreateImageSource(parts[6]);
            string friendlyName = parts[7];
            return new ButtonViewOptions
            {
                KeyCode = keyCode,
                X = double.IsNaN(x) ? 0.0 : x,
                Y = double.IsNaN(y) ? 0.0 : y,
                Width = double.IsNaN(width) ? pressedImageSource.Width : width,
                Height = double.IsNaN(height) ? pressedImageSource.Height : height,
                PressedSkinSource = pressedImageSource,
                ReleasedSkinSource = releasedImageSource,
                FriendlyName = friendlyName,
            };
        }

        protected override string ToLine(ButtonViewOptions bvo)
        {
            string pressedImagePath = bvo.PressedSkinSource == StaticResources.PressedImageSource
                ? string.Empty
                : (bvo.PressedSkinSource as BitmapImage).UriSource.OriginalString;
            string releasedImagePath = bvo.ReleasedSkinSource == StaticResources.ReleasedImageSource
                ? string.Empty
                : (bvo.ReleasedSkinSource as BitmapImage).UriSource.OriginalString;

            return $"{bvo.KeyCode}{Separator}" +
                $"{bvo.X}{Separator}" +
                $"{bvo.Y}{Separator}" +
                $"{bvo.Width}{Separator}" +
                $"{bvo.Height}{Separator}" +
                $"{pressedImagePath}{Separator}" +
                $"{releasedImagePath}{Separator}" +
                $"{bvo.FriendlyName}";
        }

        private ImageSource CreateImageSource(string imagePath)
        {
            BitmapImage bmpSource = new BitmapImage();
            bmpSource.BeginInit();
            bmpSource.UriSource = new Uri(imagePath, UriKind.Relative);
            bmpSource.EndInit();
            return bmpSource;
        }
    }
}
