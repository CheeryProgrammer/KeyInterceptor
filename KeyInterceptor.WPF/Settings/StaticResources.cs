using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace KeyInterceptor.WPF.Settings
{
    static class StaticResources
    {
        private const string PressedResourcePath = "pack://application:,,/Media/PressedButton.bmp";
        private const string ReleasedResourcePath = "pack://application:,,/Media/ReleasedButton.bmp";


        public static ImageSource PressedImageSource;
        public static ImageSource ReleasedImageSource;

        static StaticResources()
        {
            PressedImageSource = LoadImageFromResource(PressedResourcePath);
            ReleasedImageSource = LoadImageFromResource(ReleasedResourcePath);
        }

        private static ImageSource LoadImageFromResource(string resourcePath)
        {
            return new BitmapImage(new Uri(resourcePath));
        }
    }
}
