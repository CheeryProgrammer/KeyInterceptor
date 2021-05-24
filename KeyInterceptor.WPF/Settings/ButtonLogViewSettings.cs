using System;
using System.Windows.Input;
using System.Windows.Media;

namespace KeyInterceptor.WPF.Settings
{
    class ButtonLogViewSettings : Settings<LogEntryOptions>
    {
        private static readonly string[] _headers = new[]
        {
            "KeyCode",
            "FontColor",
            "FriendlyName"
        };

        public ButtonLogViewSettings(string fileName) : base(fileName)
        { }

        protected override string[] ColumnHeaders => _headers;

        protected override LogEntryOptions ParseEntry(string line)
        {
            string[] parts = line.Split(Separator);
            Key keyCode = (Key)Enum.Parse(typeof(Key), parts[0]);
            Brush fontBrush = parts[1].ToBrush();
            string friendlyName = parts[2];

            return new LogEntryOptions
            {
                KeyCode = keyCode,
                FontBrush = fontBrush,
                FriendlyName = friendlyName
            };
        }

        protected override string ToLine(LogEntryOptions entry)
        {
            return $"{entry.KeyCode}{Separator}{entry.FontBrush.ToCsvString()}";
        }
    }
}
