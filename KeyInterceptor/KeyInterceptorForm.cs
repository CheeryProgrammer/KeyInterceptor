using Gma.System.MouseKeyHook;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace KeyInterceptor
{
	public partial class KeyInterceptorForm : Form
	{
		private ButtonView _selectedButton;
		private Dictionary<Keys, List<ButtonView>> _keyCodeToButtonViews = new Dictionary<Keys, List<ButtonView>>();
		private ButtonFileStore _settings = new ButtonFileStore("settings.txt");
		private LogForm _logForm;
		private KeyNamesMap _keyMap = new KeyNamesMap();

		public event KeyEventHandler KeyDown;
		public event KeyEventHandler KeyUp;

		private IKeyboardMouseEvents m_GlobalHook;

		private IClock _clock;

		private ToolStripMenuItem _transparencyMenu;
		private FormBackgroundColorizer _backgroundColorizer;

		public KeyInterceptorForm()
		{
			InitializeComponent();
		}

		private void KeyInterceptorForm_Load(object sender, EventArgs e)
		{
			_transparencyMenu = this.contextMenu.Items[3] as ToolStripMenuItem;
			_backgroundColorizer = new FormBackgroundColorizer(this);
			_clock = new Clock();

			_logForm = new LogForm(_clock);
            _logForm.Show();

            try
			{
				LoadButtons();
			}
			catch(Exception ex)
			{
				MessageBox.Show(this, $"Не удалось загрузить настройки: {ex.Message}{Environment.NewLine}" +
					$"Файл настроек будет удален", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			LoadFormSettings();

			Subscribe();
        }

		private void LoadButtons()
		{
			var buttons = _settings.Load();
			foreach (var buttonView in buttons)
			{
				if (buttonView.KeyCode.HasValue)
				{
					if (_keyCodeToButtonViews.TryGetValue(buttonView.KeyCode.Value, out List<ButtonView> views))
						views.Add(buttonView);
					else
						_keyCodeToButtonViews.Add(buttonView.KeyCode.Value, new List<ButtonView> { buttonView });
				}
				buttonView.MouseDown += NewButton_MouseDown;
				buttonView.KeyReleased += LogKey;
				Controls.Add(buttonView);
			}
		}

		public void Subscribe()
		{
			// Note: for the application hook, use the Hook.AppEvents() instead
			m_GlobalHook = Hook.GlobalEvents();

			m_GlobalHook.KeyDown += PressButtonView;
			m_GlobalHook.KeyUp += ReleaseButtonView;
		}

		private void LogKey(object sender, KeyReleasedEventArgs e)
		{
			if (_logForm == null || _logForm.IsDisposed)
				return;

			_logForm.BeginInvoke((Action)(()=>
			{
				var logItem = new LogEntry
				{
					Brush = e.Brush,
					Text = $"{e.PressedTimestamp:HH:mm:ss.fff} {_keyMap.Map(e.KeyCode.ToString())} {Math.Round(e.Duration)} ms"
				};
				_logForm.Append(ref logItem);
			}));
		}

		private void PressButtonView(object sender, KeyEventArgs e)
		{
			var pressTimeStamp = _clock.Now;
			if (_keyCodeToButtonViews.TryGetValue(e.KeyCode, out List<ButtonView> views))
			{
				views.ForEach(btnView => btnView.Press(pressTimeStamp));
			}
		}

		private void ReleaseButtonView(object sender, KeyEventArgs e)
		{
			var unpressTimeStamp = _clock.Now;
			if (_keyCodeToButtonViews.TryGetValue(e.KeyCode, out List<ButtonView> views))
			{
				views.ForEach(btnView => btnView.UnPress(unpressTimeStamp));
			}
		}

		private void KeyInterceptorForm_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				contextMenu.Show(this, e.Location);
		}

		private void NewButton_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				_selectedButton = sender as ButtonView;
				var location = _selectedButton.Location;
				location.Offset(e.Location);
				buttonContextMenu.Show(this, location);
			}
		}

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
        }

        private void NoBordersToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
			if (menuItem.Checked)
			{
				this.FormBorderStyle = FormBorderStyle.Sizable;
				menuItem.Checked = false;
			}
			else
			{
				this.FormBorderStyle = FormBorderStyle.None;
				menuItem.Checked = true;
			}
		}

		private void AddButtonToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var newButton = new ButtonView();
			newButton.MouseDown += NewButton_MouseDown;
			newButton.KeyReleased += LogKey;
			this.Controls.Add(newButton);
		}

		private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void BindViewToKeyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (KeyBindingForm bindingForm = new KeyBindingForm())
			{
				m_GlobalHook.KeyDown += bindingForm.RegisterButton;
				bindingForm.ShowDialog(this);
				m_GlobalHook.KeyDown += bindingForm.RegisterButton;
				if (bindingForm.DialogResult == DialogResult.OK)
				{
					if (_selectedButton.KeyCode.HasValue
						&& _keyCodeToButtonViews.TryGetValue(_selectedButton.KeyCode.Value, out List<ButtonView> v))
					{
						v.Remove(_selectedButton);
					}

					if (bindingForm.KeyCode.HasValue)
					{
						_selectedButton.KeyCode = bindingForm.KeyCode;

						if (_keyCodeToButtonViews.TryGetValue(bindingForm.KeyCode.Value, out List<ButtonView> views))
						{
							views.Add(_selectedButton);
						}
						else
						{
							_keyCodeToButtonViews.Add(bindingForm.KeyCode.Value, new List<ButtonView> { _selectedButton });
						}
					}
				}
			}
		}

		private void ChangeViewImageToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (var openFileDialog = new OpenFileDialog())
			{
				openFileDialog.InitialDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Images");
				openFileDialog.Filter = "Image|*.bmp;*.png";
				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					try
					{
						var sourcePath = openFileDialog.FileName;
						_selectedButton.SetImage(sourcePath);
					}
					catch(Exception ex)
					{
						MessageBox.Show(this, $"Не удалось открыть изображение: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
			}
		}

		private void ChangeViewActiveImageToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (var openFileDialog = new OpenFileDialog())
			{
				openFileDialog.InitialDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Images");
				openFileDialog.Filter = "Image|*.bmp;*.png";
				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					try
					{
						var sourcePath = openFileDialog.FileName;
						_selectedButton.SetActiveImage(sourcePath);
					}
					catch (Exception ex)
					{
						MessageBox.Show(this, $"Не удалось открыть изображение: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
			}
		}

		private void RemoveButtonToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Controls.Remove(_selectedButton);
			_keyCodeToButtonViews.Values.ToList().ForEach(list => list.Remove(_selectedButton));
		}

		private void KeyInterceptorForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			_logForm?.Close();
			_clock?.Dispose();

			m_GlobalHook.KeyDown -= PressButtonView;
			m_GlobalHook.KeyUp -= ReleaseButtonView;

			//It is recommened to dispose it
			m_GlobalHook.Dispose();

			var buttons = Controls.OfType<ButtonView>();
			_settings.Save(buttons);
			SaveFormSettings();
		}

		private void SaveFormSettings()
		{
			string posX = Location.X.ToString();
			string posY = Location.Y.ToString();
			string width = Width.ToString();
			string height = Height.ToString();
			string backColor = _backgroundColorizer.BackgroundColor.ToArgb().ToString();
			string topMost = TopMost.ToString();
			bool setTransparency = _backgroundColorizer.IsTransparent;
			File.WriteAllText("main_settings.txt", $"{posX}|{posY}|{width}|{height}|{backColor}|{topMost}|{setTransparency}");
		}

		private void LoadFormSettings()
		{
			if (File.Exists("main_settings.txt"))
			{
				var parts = File.ReadAllText("main_settings.txt").Split('|');
				Location = new Point(int.Parse(parts[0]), int.Parse(parts[1]));
				Width = int.Parse(parts[2]);
				Height = int.Parse(parts[3]);
				_backgroundColorizer.SetColor(Color.FromArgb(int.Parse(parts[4])));
				TopMost = bool.Parse(parts[5]);
				_backgroundColorizer.SetTransparency(bool.Parse(parts[6]));
				_transparencyMenu.Checked = _backgroundColorizer.IsTransparent;
			}
		}

        private void ShowLogToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_logForm.Dispose();
			_logForm = new LogForm(_clock);
			_logForm.Show();
		}

		private void ChangeBackColorToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_backgroundColorizer.SetColor(PickColor(BackColor));
		}

		private Color PickColor(Color sourceColor)
		{
			using (var colorDialog = new ColorDialog())
			{
				colorDialog.Color = sourceColor;

				if (DialogResult.OK == colorDialog.ShowDialog())
				{
					return colorDialog.Color;
				}

				return sourceColor;
			}
		}

		private void SwitchTopMostToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TopMost = !TopMost;
			if(sender is ToolStripMenuItem menuItem)
			{
				menuItem.Checked = TopMost;
			}
		}

        private void SetTransparentBackgroundToolStripMenuItem_Click(object sender, EventArgs e)
        {
			_backgroundColorizer.SetTransparency(!_backgroundColorizer.IsTransparent);
			_transparencyMenu.Checked = _backgroundColorizer.IsTransparent;
        }
    }
}
