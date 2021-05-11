using Gma.System.MouseKeyHook;
using KeyInterceptor.WPF.ButtonsView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KeyInterceptor.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IKeyboardMouseEvents _events;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _events = Hook.GlobalEvents();
            _events.KeyDown += Events_KeyDown;
            _events.KeyUp += Events_Up;
            var log = new LogForm();
            log.Show();
            InitButtons();
        }

        private void InitButtons()
        {
            Canvas.Children.Add(
                new KeyButton
                {
                    PressedSkinPath = "pack://application:,,,/KeyInterceptor.WPF;component/Media/ActiveButtonDefault.bmp",
                    ReleasedSkinPath = "pack://application:,,,/KeyInterceptor.WPF;component/Media/ButtonDefault.bmp"
                }
                );
        }

        private void Events_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            KeyPressed(e.KeyCode);
        }

        private void KeyPressed(Keys keyCode)
        {
            foreach (KeyButton btn in Canvas.Children)
                btn.Press();
        }

        private void Events_Up(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            KeyReleased(e.KeyCode);
        }

        private void KeyReleased(Keys keyCode)
        {
            foreach (KeyButton btn in Canvas.Children)
                btn.Release();
        }
    }
}
