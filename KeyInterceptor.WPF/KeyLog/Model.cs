using KeyInterceptor.WPF.Time;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace KeyInterceptor.WPF.KeyLog
{
    class Model : INotifyPropertyChanged
    {
        public FontProperties FontProperties { get; set; }
        private readonly Timer _updateClockTimer;

        public ICommand ChangeFontCommand { get; set; }
        public Clock Clock { get; set; } = new Clock();

        public ObservableCollection<LogItem> Elements { get; set; }

        public Model(Func<bool> haveToShrink)
        {
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
            Task.Factory.StartNew(RunUpdate);
            _updateClockTimer = new Timer((s) => PropertyChanged?.BeginInvoke(this, new PropertyChangedEventArgs(nameof(Clock)), null, null), null, 10, 10);
        }

        internal void Add()
        {
            Add($"Hello {Clock.Now.Millisecond}", Brushes.Aqua, Brushes.Coral);
        }

        internal void ShrinkLog()
        {
            if(Elements.Count > 0)
                Elements.RemoveAt(0);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RunUpdate()
        {
            while (true)
            {
                Add($"Hello {Clock.Now.Millisecond}", Brushes.Aqua, Brushes.Coral);
                Thread.Sleep(1000);
            }
        }

        public void Add(string text, Brush foreBrush, Brush backBrush)
        {
            App.Current?.Dispatcher.Invoke(() =>
            {
                Elements.Add(new LogItem { Text = text, Color = foreBrush, BackColor = backBrush });
            });
        }
    }

    struct LogItem
    {
        public string Text { get; set; }
        public Brush Color { get; set; }
        public Brush BackColor { get; set; }
    }
}
