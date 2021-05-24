using System.Windows.Input;

namespace KeyInterceptor.WPF.ButtonsView
{
    public class KeyBindingEventArgs
    {
        public Key KeyCode { get; }

        public KeyBindingEventArgs(Key keyCode)
        {
            KeyCode = keyCode;
        }
    }
}