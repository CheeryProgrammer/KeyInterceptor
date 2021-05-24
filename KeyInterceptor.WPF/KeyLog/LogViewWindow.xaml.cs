using Gma.System.MouseKeyHook;
using KeyInterceptor.WPF.Time;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace KeyInterceptor.WPF.KeyLog
{
    /// <summary>
    /// Interaction logic for LogViewWindow.xaml
    /// </summary>
    public partial class LogViewWindow : Window
	{
        private readonly IKeyboardMouseEvents _events;
        private readonly LogViewModel _model;

        public LogViewWindow(IKeyboardMouseEvents events, Clock clock)
		{
			InitializeComponent();
			DataContext = _model = new LogViewModel(clock);
            _events = events;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _events.KeyDown += KeyBoard_KeyDown;
        }

        private void KeyBoard_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
        }

        private void ShrinkableStackPanel_Overflew(object sender, FreePanelSpaceEventArgs e)
        {
            if (ClockIsOverlapped())
                (DataContext as LogViewModel).ShrinkLog();
        }

        private bool ClockIsOverlapped()
        {
            return Clock.DesiredSize.Height < Clock.ActualHeight;
        }

        internal void Press(DateTime timeStamp, Key keyCode)
        {
            _model.Press(keyCode, timeStamp);
        }

        internal void Release(DateTime timeStamp, Key keyCode)
        {
            _model.Release(keyCode, timeStamp);
        }
    }
}
