using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace KeyInterceptor.WPF.KeyLog
{
    class KeyPressingTracker
    {
        public event EventHandler<KeyReleasedEventArgs> ButtonReleased;

        private readonly Dictionary<Key, DateTime> _pressedKeys = new Dictionary<Key, DateTime>();
   
        public void Press(Key keyCode, DateTime timestamp)
        {
            if (_pressedKeys.ContainsKey(keyCode))
                return;

            _pressedKeys.Add(keyCode, timestamp);
        }

        public void Release(Key keyCode, DateTime timestamp)
        {
            if(_pressedKeys.TryGetValue(keyCode, out DateTime startTime))
            {
                _pressedKeys.Remove(keyCode);
                ButtonReleased?.Invoke(
                    this,
                    new KeyReleasedEventArgs
                        (   
                            keyCode,
                            //timestamp.Subtract(startTime).TotalMilliseconds,
                            TimeSpan.FromTicks(timestamp.Ticks - startTime.Ticks).TotalMilliseconds,
                            startTime
                        )
                    );
            }
        }
    }
}
