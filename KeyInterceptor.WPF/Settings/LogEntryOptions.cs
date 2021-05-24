using System.Windows.Input;
using System.Windows.Media;

namespace KeyInterceptor.WPF.Settings
{
    internal class LogEntryOptions
    {
        public Key KeyCode { get; internal set; }
        public Brush FontBrush { get; internal set; }
        public string FriendlyName { get; internal set; }

        public static Brush DefaultFontBrush = Brushes.Black;
    }
}