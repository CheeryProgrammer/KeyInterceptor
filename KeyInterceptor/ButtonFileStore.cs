using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace KeyInterceptor
{
	class ButtonFileStore
	{
		private const string ImagesDirectory = "Images";
		private const char _settingsDelimiter = '|';
		private readonly string _fileName;

		public ButtonFileStore(string fileName)
		{
			_fileName = fileName;
			if (!Directory.Exists(ImagesDirectory))
				Directory.CreateDirectory(ImagesDirectory);
		}

		public void Save(IEnumerable<ButtonView> views)
		{
			File.WriteAllLines(_fileName, views.Select(Serialize).ToArray());
		}

		private string Serialize(ButtonView view)
		{
			string imagePath = Path.Combine(ImagesDirectory, Path.GetFileName(view.ImagePath ?? ""));
			string activeImagePath = Path.Combine(ImagesDirectory, Path.GetFileName(view.ActiveImagePath ?? ""));
			return $"{view.KeyCode}" +
				$"|{view.Location.X}" +
				$"|{view.Location.Y}" +
				$"|{view.Width}" +
				$"|{view.Height}" +
				$"|{imagePath}" +
				$"|{activeImagePath}";
		}

		public IEnumerable<ButtonView> Load()
		{
			if (!File.Exists(_fileName))
				yield break;

			string[] lines = File.ReadAllLines(_fileName);
			for (int i = 0; i < lines.Length; i++)
			{
				yield return ParseButtonView(lines[i]);
			}
		}

		private ButtonView ParseButtonView(string line)
		{
			var parts = line.Split(_settingsDelimiter);
			Keys? keyCode = string.IsNullOrWhiteSpace(parts[0]) ? null : (Keys?)Enum.Parse(typeof(Keys), parts[0]);
			int x = int.Parse(parts[1]);
			int y = int.Parse(parts[2]);
			int width = int.Parse(parts[3]);
			int height = int.Parse(parts[4]);
			string image = string.IsNullOrWhiteSpace(parts[5]) ? null : parts[5];
			string activeImage = string.IsNullOrWhiteSpace(parts[6]) ? null : parts[6];
			return new ButtonView(keyCode, x, y, width, height, image, activeImage);
		}
	}
}
