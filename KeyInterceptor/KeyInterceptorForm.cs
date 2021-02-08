using Gma.System.MouseKeyHook;
using System;
using System.Collections.Generic;
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


		public event KeyEventHandler KeyDown;
		public event KeyEventHandler KeyUp;

		private IKeyboardMouseEvents m_GlobalHook;

		public KeyInterceptorForm()
		{
			InitializeComponent();
		}

		private void KeyInterceptorForm_Load(object sender, EventArgs e)
		{
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

			_logForm = new LogForm();
			_logForm.Show();
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
			_logForm.BeginInvoke((Action)(()=>
			{
				_logForm.Append($"{e.PressedTimestamp:HH:mm:ss.fff}: {e.KeyCode} ({e.Duration} ms)");
			}));
		}

		private void PressButtonView(object sender, KeyEventArgs e)
		{
			if (_keyCodeToButtonViews.TryGetValue(e.KeyCode, out List<ButtonView> views))
			{
				views.ForEach(btnView => btnView.Press());
			}
		}

		private void ReleaseButtonView(object sender, KeyEventArgs e)
		{
			if (_keyCodeToButtonViews.TryGetValue(e.KeyCode, out List<ButtonView> views))
			{
				views.ForEach(btnView => btnView.UnPress());
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
				openFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
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
				openFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
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
			_logForm.Close();

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
			File.WriteAllText("main_settings.txt", $"{posX}|{posY}|{width}|{height}");
		}

		private void LoadFormSettings()
		{
			if (File.Exists("main_settings.txt"))
			{
				var parts = File.ReadAllText("main_settings.txt").Split('|');
				Location = new System.Drawing.Point(int.Parse(parts[0]), int.Parse(parts[1]));
				Width = int.Parse(parts[2]);
				Height = int.Parse(parts[3]);
			}
		}

		private void ShowLogToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_logForm.Dispose();
			_logForm = new LogForm();
			_logForm.Show();
		}
	}
}
