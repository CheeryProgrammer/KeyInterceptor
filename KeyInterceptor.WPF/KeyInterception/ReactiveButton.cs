using KeyInterceptor.WPF.ButtonsView;
using System.Windows.Input;

namespace KeyInterceptor.WPF.KeyInterception
{
    internal class ReactiveButton
    {
        public Key KeyCode { get; internal set; }
        public ButtonView View { get; internal set; }

        internal void Press()
        {
            View.Press();
        }

        internal void Release()
        {
            View.Release();
        }
    }
}