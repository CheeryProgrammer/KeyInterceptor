using System.Windows.Input;
using System.Windows.Media;

namespace KeyInterceptor.WPF.Settings
{
    public class ButtonViewOptions
    {
        public Key KeyCode { get; set; }

        public double X { get; set; }
        public double Y { get; set; }

        public double Width { get; set; }
        public double Height { get; set; }

        public ImageSource PressedSkinSource { get; set; }
        public ImageSource ReleasedSkinSource { get; set; }

        public string FriendlyName { get; set; }
    }
}
