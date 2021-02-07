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
		private KeyPressListener _keyListener;
		private Dictionary<Keys, List<ButtonView>> _keyCodeToButtonViews = new Dictionary<Keys, List<ButtonView>>();
		private ButtonFileStore _settings = new ButtonFileStore("settings.txt");

		public KeyInterceptorForm()
		{
			InitializeComponent();
		}

		private void KeyInterceptorForm_Load(object sender, EventArgs e)
		{
			_keyListener = new KeyPressListener();
			_keyListener.KeyDown += PressButtonView;
			_keyListener.KeyUp += ReleaseButtonView;
			try
			{
				LoadButtons();
			}
			catch(Exception ex)
			{
				MessageBox.Show(this, $"Не удалось загрузить настройки: {ex.Message}{Environment.NewLine}" +
					$"Файл настроек будет удален", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
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
				Controls.Add(buttonView);
			}
		}

		private void PressButtonView(object sender, KeyEventArgs e)
		{
			if (_keyCodeToButtonViews.TryGetValue(e.KeyCode, out List<ButtonView> views))
			{
				views.ForEach(btnView => btnView.BeginInvoke((Action)btnView.Press));
			}
		}

		private void ReleaseButtonView(object sender, KeyEventArgs e)
		{
			if (_keyCodeToButtonViews.TryGetValue(e.KeyCode, out List<ButtonView> views))
			{
				views.ForEach(btnView => btnView.BeginInvoke((Action)btnView.UnPress));
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
				_keyListener.KeyDown += bindingForm.RegisterButton;
				bindingForm.ShowDialog(this);
				_keyListener.KeyDown += bindingForm.RegisterButton;
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

		private void KeyInterceptorForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			var buttons = Controls.OfType<ButtonView>();
			_settings.Save(buttons);
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
	}
}
