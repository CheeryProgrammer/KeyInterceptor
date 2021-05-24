using Gma.System.MouseKeyHook;
using KeyInterceptor.WPF.KeyInterception;
using KeyInterceptor.WPF.KeyLog;
using KeyInterceptor.WPF.Settings;
using KeyInterceptor.WPF.Time;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace KeyInterceptor.WPF.ButtonsView
{
    /// <summary>
    /// Interaction logic for KeyViewWindow.xaml
    /// </summary>
    public partial class KeyViewWindow : Window
    {
        private IKeyboardMouseEvents _events;
        private ButtonsController _buttonsController;
        private LogViewWindow _logWindow;
        private Clock _clock;
        private ButtonViewSettings _viewSettings = new ButtonViewSettings("buttons.settings");
        private ButtonLogViewSettings _logSettings = new ButtonLogViewSettings("log.settings");
        private Dictionary<Key, string> _friendlyNames = new Dictionary<Key, string>();

        public KeyViewWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _events = Hook.GlobalEvents();
            _buttonsController = new ButtonsController();

            _events.KeyDown += Events_KeyDown;
            _events.KeyUp += Events_KeyUp;

            _clock = new Clock();

            _logWindow = new LogViewWindow(_events, _clock);
            _logWindow.Show();

            LoadButtons();
            LoadKeyFriendlyNames();
        }

        private void LoadKeyFriendlyNames()
        {
            foreach(var entryOptions in _logSettings.Read())
            {
                _friendlyNames[entryOptions.KeyCode] = entryOptions.FriendlyName;
            }
        }

        private void LoadButtons()
        {
            foreach(var btnOptions in _viewSettings.Read())
            {
                ButtonView newButtonView = ButtonView.Create(btnOptions);

                newButtonView.KeyBound += NewKeyBound;

                Canvas.Children.Add(newButtonView);

                ReactiveButton rButton = new ReactiveButton
                {
                    KeyCode = btnOptions.KeyCode,
                    View = newButtonView
                };

                _buttonsController.Bind(rButton);
            }
        }

        private void NewKeyBound(object sender, KeyBindingEventArgs e)
        {
            ReactiveButton rButton = new ReactiveButton
            {
                KeyCode = e.KeyCode,
                View = sender as ButtonView
            };
            rButton.View.SetToolTip(_friendlyNames.ContainsKey(e.KeyCode)
                ? _friendlyNames[e.KeyCode]
                : e.KeyCode.ToString());
            _buttonsController.Bind(rButton);
        }

        private void Events_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            DateTime timeStamp = _clock.Now;
            Key wpfKey = KeyInterop.KeyFromVirtualKey((int)e.KeyCode);
            if(_buttonsController.IsBound(wpfKey))
                KeyPressed(wpfKey, timeStamp);
        }

        private void Events_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            DateTime timeStamp = _clock.Now;
            Key wpfKey = KeyInterop.KeyFromVirtualKey((int)e.KeyCode);
            if (_buttonsController.IsBound(wpfKey))
                KeyReleased(wpfKey, timeStamp);
        }

        private void KeyPressed(Key keyCode, DateTime timeStamp)
        {
            _buttonsController.Press(keyCode);
            _logWindow.Press(timeStamp, keyCode);
        }

        private void KeyReleased(Key keyCode, DateTime timeStamp)
        {
            _buttonsController.Release(keyCode);
            _logWindow.Release(timeStamp, keyCode);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            _viewSettings.Write(_buttonsController.GetAll().Select(c => ToOptions(c)));
            base.OnClosing(e);
        }

        private ButtonViewOptions ToOptions(ReactiveButton btn)
        {
            ButtonViewOptions options = btn.View.GetOptions();
            options.KeyCode = btn.KeyCode;
            return options;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            _logWindow.Close();
        }

        private void NewButtonView_Click(object sender, RoutedEventArgs e)
        {
            ButtonViewOptions options = new ButtonViewOptions
            {
                PressedSkinSource = StaticResources.PressedImageSource,
                ReleasedSkinSource = StaticResources.ReleasedImageSource,
                FriendlyName = "Не назначена"
            };
            ButtonView newButtonView = ButtonView.Create(options);

            newButtonView.KeyBound += NewKeyBound;

            Canvas.Children.Add(newButtonView);
        }
    }
}
