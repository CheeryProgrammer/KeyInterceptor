using System.Collections.Generic;
using System.IO;

namespace KeyInterceptor
{
	class KeyNamesMap: Dictionary<string,string>
	{
		public KeyNamesMap(): base()
		{
			if (File.Exists("key_names.txt"))
			{
				try
				{
					var lines = File.ReadAllLines("key_names.txt");
					foreach(var line in lines)
					{
						var parts = line.Split('|');
						Add(parts[0], parts[1]);
					}
				}
				catch
				{

				}
			}
			else
			{
				File.WriteAllLines("key_names.txt", new[] { "Space|Space" });
			}
		}

		public string Map(string systemKeyName)
		{
			if (TryGetValue(systemKeyName, out string mappedName))
				return mappedName;

			return systemKeyName;
		}
	}
}
