using KeyInterceptor.WPF.Settings;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
    
namespace KeyInterceptor.WPF.ButtonsView
{
    /// <summary>
    /// Interaction logic for KeyButton.xaml
    /// </summary>
    public partial class ButtonView : UserControl, INotifyPropertyChanged
    {
        private ButtonViewModel _model;
        private ButtonViewModel Model
        {
            get
            {
                return _model;
            }
            set
            {
                DataContext = _model = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ButtonViewModel.ReleasedSkinSource)));
            }
        }

        public event EventHandler<KeyBindingEventArgs> KeyBound;
        public event PropertyChangedEventHandler PropertyChanged;

        private Point? _startMovingPosition;

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            _startMovingPosition = e.GetPosition(Parent as IInputElement);
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            _startMovingPosition = null;
            base.OnMouseLeftButtonUp(e);
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            _startMovingPosition = null;
            base.OnMouseLeave(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (_startMovingPosition.HasValue)
            {
                Point newPosition = e.GetPosition(Parent as IInputElement);
                var deltaX = newPosition.X - _startMovingPosition.Value.X;
                var deltaY = newPosition.Y - _startMovingPosition.Value.Y;
                Move(deltaX, deltaY);
                _startMovingPosition = newPosition;
            }
        }

        private void Move(double deltaX, double deltaY)
        {
            double newX = Canvas.GetLeft(this) + deltaX;
            double newY = Canvas.GetTop(this) + deltaY;
            Canvas.SetLeft(this, double.IsNaN(newX) ? 0.0 : newX);
            Canvas.SetTop(this, double.IsNaN(newY) ? 0.0 : newY);
        }

        public ButtonView()
        {
            InitializeComponent();

            DataContext = _model = new ButtonViewModel(this);

            PressedSkin.Visibility = Visibility.Visible;
            ReleasedSkin.Visibility = Visibility.Visible;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //Release();
        }

        public void Press()
        {
            PressedSkin.Visibility = Visibility.Visible;
            ReleasedSkin.Visibility = Visibility.Collapsed;
        }

        public void Release()
        {
            PressedSkin.Visibility = Visibility.Collapsed;
            ReleasedSkin.Visibility = Visibility.Visible;
        }

        internal void SetToolTip(string tooltipText)
        {
            Model.ToolTipText = tooltipText;
        }

        private void BindKey_Click(object sender, RoutedEventArgs e)
        {
            KeyBindingWindow keyBindingWindow = new KeyBindingWindow();
            if (keyBindingWindow.ShowDialog() ?? false)
            {
                KeyBound?.Invoke(this, new KeyBindingEventArgs(keyBindingWindow.KeyCode));
            }
        }

        public static ButtonView Create(ButtonViewOptions options)
        {
            ButtonView buttonView = new ButtonView();

            buttonView.Model.X = options.X;
            buttonView.Model.Y = options.Y;
            buttonView.Model.PressedSkinSource = options.PressedSkinSource;
            buttonView.Model.ReleasedSkinSource = options.ReleasedSkinSource;
            buttonView.Model.Width = options.Width;
            buttonView.Model.Height = options.Height;
            buttonView.Model.ToolTipText = string.IsNullOrWhiteSpace(options.FriendlyName)
                ? options.KeyCode.ToString()
                : options.FriendlyName;


            return buttonView;
        }

        internal ButtonViewOptions GetOptions()
        {
            return new ButtonViewOptions
            {
                X = Model.X,
                Y = Model.Y,
                Width = Model.Width,
                Height = Model.Height,
                PressedSkinSource = Model.PressedSkinSource,
                ReleasedSkinSource = Model.ReleasedSkinSource,
                FriendlyName = Model.ToolTipText,
            };
        }
    }
}
