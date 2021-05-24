using KeyInterceptor.WPF.Settings;
using KeyInterceptor.WPF.Time;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace KeyInterceptor.WPF.KeyLog
{
    class LogViewModel : INotifyPropertyChanged
    {
        public FontProperties FontProperties { get; set; }
        private readonly Timer _updateClockTimer;
        private readonly Brush _backgroundBrush = Brushes.White;
        private readonly KeyPressingTracker _pressingTracker;
        private readonly ButtonLogViewSettings _logSettings;
        private Dictionary<Key, LogEntryOptions> _logEntryOptions;

        public ICommand ChangeFontCommand { get; set; }
        public Clock Clock { get; set; }

        public ObservableCollection<LogItem> Elements { get; set; }

        public LogViewModel(Clock clock)
        {
            Clock = clock;

            Elements = new ObservableCollection<LogItem>
            {
                new LogItem{Text = "Hello", Color = Brushes.Aqua, BackColor = Brushes.Coral },
                new LogItem{Text = "World" , Color = Brushes.Aquamarine, BackColor = Brushes.DarkRed}
            };
            FontProperties = new FontProperties()
            {
                FontColor = Brushes.Black
            };
            ChangeFontCommand = new ChangeFontCommand(FontProperties);
            _pressingTracker = new KeyPressingTracker();
            _pressingTracker.ButtonReleased += PressingTracker_ButtonReleased;
            _logSettings = new ButtonLogViewSettings("log.settings");

            LoadSettings();

            Task.Factory.StartNew(RunUpdate);
            _updateClockTimer = new Timer((s) => PropertyChanged?.BeginInvoke(this, new PropertyChangedEventArgs(nameof(Clock)), null, null), null, 10, 10);
        }

        private void LoadSettings()
        {
            _logEntryOptions = _logSettings.Read()
                .ToDictionary
                (
                    o => o.KeyCode,
                    o => o
                );
        }

        private void PressingTracker_ButtonReleased(object sender, KeyReleasedEventArgs e)
        {
            string name = e.KeyCode.ToString();
            Brush fontBrush = Brushes.Black;
            if (_logEntryOptions.TryGetValue(e.KeyCode, out LogEntryOptions entryOptions))
            {
                name = entryOptions.FriendlyName;
                fontBrush = entryOptions.FontBrush;
            }
            string text = $"{e.PressedTimeStamp:HH:mm:ss.fff} {name} {Math.Round(e.DurationMs)} ms";
            /*App.Current?.Dispatcher.Invoke(() =>
            {*/
                Elements.Add(new LogItem { Text = text, Color = fontBrush, BackColor = _backgroundBrush });
            /*});*/
        }

        internal void ShrinkLog()
        {
            if(Elements.Count > 0)
                Elements.RemoveAt(0);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RunUpdate()
        {
        }

        public void Press(Key keyCode, DateTime timestamp)
        {
            _pressingTracker.Press(keyCode, timestamp);
        }

        public void Release(Key keyCode, DateTime timestamp)
        {
            _pressingTracker.Release(keyCode, timestamp);
        }
    }

    struct LogItem
    {
        public string Text { get; set; }
        public Brush Color { get; set; }
        public Brush BackColor { get; set; }
    }
}
