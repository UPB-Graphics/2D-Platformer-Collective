using System;
using System.Collections.Generic;
using System.IO;

using UnityEngine;

public class Persistance
{
	private static string savefile = "/savefile.ini";

	public static void Save(string key, string value)
	{
		string filename = Application.persistentDataPath + savefile;
		string line = $"{key}:{value}";
		bool found = false;

		List<string> lines;
		try {
			using (StreamReader sr = new StreamReader(filename))
			{
				lines = new List<string>(
					sr.ReadToEnd().Split(
						new string[] { "\r\n", "\n" },
						StringSplitOptions.None
					)
				);

				for (int i = 0; i < lines.Count; ++i) {
					if (!lines[i].StartsWith(key)) continue;

					lines[i] = line;
					found = true;
					break;
				}

				if (!found)
					lines.Add(line);
			}
		} catch (FileNotFoundException) {
			lines = new List<string> { line };
		}

		using (StreamWriter sw = new StreamWriter(filename))
			sw.Write(string.Join("\n", lines));
	}
	public static string Load(string key)
	{
		foreach (string line in File.ReadLines(Application.persistentDataPath + savefile))
			if (line.StartsWith(key))
				return line.Substring(key.Length + 1);

		return null;
	}
}
