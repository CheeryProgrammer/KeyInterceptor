using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace KeyInterceptor.WPF.ButtonsView
{
    /// <summary>
    /// Interaction logic for KeyButton.xaml
    /// </summary>
    public partial class KeyButton : UserControl
    {
        public string PressedSkinPath
        {
            get
            {
                return PressedSkin.Source.ToString();
            }
            set
            {
                SetSource(PressedSkin, value);
            }
        }

        public string ReleasedSkinPath
        {
            get
            {
                return ReleasedSkin.Source.ToString();
            }
            set
            {
                SetSource(ReleasedSkin, value);
            }
        }

        private void SetSource(Image image, string path)
        {
            BitmapImage bmp = new BitmapImage(new Uri(path));
            image.Source = bmp;
        }

        public KeyButton()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Release();
        }

        public void Press()
        {
            PressedSkin.Visibility = Visibility.Visible;
            ReleasedSkin.Visibility = Visibility.Collapsed;
        }

        public void Release()
        {
            PressedSkin.Visibility = Visibility.Collapsed;
            ReleasedSkin.Visibility = Visibility.Visible;
        }
    }
}
