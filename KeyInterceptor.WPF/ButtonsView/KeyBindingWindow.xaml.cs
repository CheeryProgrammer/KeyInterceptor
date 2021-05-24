using System.Windows;
using System.Windows.Input;

namespace KeyInterceptor.WPF.ButtonsView
{
    /// <summary>
    /// Interaction logic for KeyBindingWindow.xaml
    /// </summary>
    public partial class KeyBindingWindow : Window
    {
        public Key KeyCode { get; private set; }

        public KeyBindingWindow()
        {
            InitializeComponent();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            KeyCode = e.Key;
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
