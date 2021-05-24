using KeyInterceptor.WPF.ButtonsView;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace KeyInterceptor.WPF.KeyInterception
{
    class ButtonsController
    {
        private Dictionary<Key, List<ReactiveButton>> _keyCodetoButtons = new Dictionary<Key, List<ReactiveButton>>();

        public void Press(Key keyCode)
        {
            if(_keyCodetoButtons.ContainsKey(keyCode))
                _keyCodetoButtons[keyCode].ForEach(b => b.Press());
        }

        public void Release(Key keyCode)
        {
            if (_keyCodetoButtons.ContainsKey(keyCode))
                _keyCodetoButtons[keyCode].ForEach(b => b.Release());
        }

        public void Bind(ReactiveButton btn)
        {
            Unbind(btn.View);

            if(_keyCodetoButtons.TryGetValue(btn.KeyCode, out List<ReactiveButton> btnList))
            {
                btnList.Add(btn);
            }
            else
            {
                _keyCodetoButtons[btn.KeyCode] = new List<ReactiveButton> { btn };
            }
        }

        public void Unbind(ButtonView view)
        {
            foreach (List<ReactiveButton> btns in _keyCodetoButtons.Values)
            {
                btns.RemoveAll(b => b.View == view);
            }
        }

        internal IEnumerable<ReactiveButton> GetAll()
        {
            return _keyCodetoButtons.Values.SelectMany(btns => btns);
        }

        internal bool IsBound(Key keyCode)
        {
            return _keyCodetoButtons.ContainsKey(keyCode);
        }
    }
}
