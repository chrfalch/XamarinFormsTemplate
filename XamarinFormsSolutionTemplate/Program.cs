using System;
using System.IO;

namespace XamarinFormsSolutionTemplate
{
	class MainClass
	{
		static readonly string TemplateName = "Test.NewSolution";
		static readonly string TemplatePath = "/Users/christianfalch/Dropbox/Projects/Islandssoftware/Dev/xamarin/XamarinFormsSolutionTemplate/Template";

		static string NewName = "com.triplogr.app";
		static string OutputPath = "/Users/christianfalch/Dropbox/Projects/Islandssoftware/Dev/xamarin/XamarinFormsSolutionTemplate/Results";

		public static void Main (string[] args)
		{			
			var templatePath = TemplatePath;
			Console.WriteLine ("Template-path: " + templatePath);

			EnumerateDirectories (templatePath, templatePath);
		}

		/// <summary>
		/// Enumerates the directories.
		/// </summary>
		/// <param name="path">Path.</param>
		static void EnumerateDirectories (string path, string rootPath)
		{
			// Copy directory with new name
			var newPath = path
				.Replace (TemplatePath, OutputPath)
				.Replace (TemplateName, NewName);

			Console.WriteLine (newPath);

			// Copy Files
			CopyFilesInDirectory(path, newPath, rootPath);

			foreach (var dir in Directory.GetDirectories (path)) 
			{
				// Skip 
				if (ShouldSkipDir (dir))
					continue;
								
				// Subdirs
				EnumerateDirectories(dir, rootPath);
			}
		}

		/// <summary>
		/// Copies the files in directory.
		/// </summary>
		/// <param name="dir">Dir.</param>
		/// <param name="newDir">New dir.</param>
		static void CopyFilesInDirectory (string dir, string newDir, string rootPath)
		{
			Directory.CreateDirectory (newDir);

			foreach (var file in Directory.GetFiles(dir)) 
			{
				if (ShouldSkipFile (file))
					continue;

				var newFile = file
					.Replace (TemplatePath, OutputPath)
					.Replace (TemplateName, NewName);

				Console.WriteLine (newFile);

				if (ShouldCopyRaw (file)) 
				{
					File.Copy (file, newFile);
				} 
				else 
				{
					// Open the file and replace occurences 
					using (var inputFile = File.OpenText (file)) 
					{
						var inputFileContents = File.OpenText (file).ReadToEnd ();
						var occurences = 0;

						while (inputFileContents.Contains (TemplateName)) 
						{
							inputFileContents = inputFileContents.Replace (TemplateName, NewName);
							occurences++;
						}

						if (occurences > 0)
							Console.WriteLine ("Replaced " + occurences + " items.");

						// Save
						using (var outputFile = File.CreateText (newFile)) 
						{
							outputFile.Write (inputFileContents);
							outputFile.Close ();	
						}
					}
				}
			}
		}

		/// <summary>
		/// Shoulds the copy raw.
		/// </summary>
		/// <returns><c>true</c>, if copy raw was shoulded, <c>false</c> otherwise.</returns>
		/// <param name="file">File.</param>
		static bool ShouldCopyRaw (string file)
		{
			var ext = Path.GetExtension (file).ToLowerInvariant ();
			if (ext == ".png" || ext == ".ttf")
				return true;

			return false;
		}

		/// <summary>
		/// Shoulds the skip file.
		/// </summary>
		/// <returns><c>true</c>, if skip file was shoulded, <c>false</c> otherwise.</returns>
		/// <param name="file">File.</param>
		static bool ShouldSkipFile (string file)
		{
			var filename = Path.GetFileName (file).ToLowerInvariant();
			if (filename == ".ds_store" || 
				Path.GetExtension(filename).ToLowerInvariant() == ".userprefs" ||
				filename == "package.config")
				return true;

			return false;
		}

		/// <summary>
		/// Shoulds the skip.
		/// </summary>
		/// <returns><c>true</c>, if skip was shoulded, <c>false</c> otherwise.</returns>
		/// <param name="dir">Dir.</param>
		static bool ShouldSkipDir (string dir)
		{
			var dirName = Path.GetFileName (dir).ToLowerInvariant ();
			if (dirName == "bin" || dirName == "obj" || dirName == "packages")
				return true;

			return false;
		}
	}
}
