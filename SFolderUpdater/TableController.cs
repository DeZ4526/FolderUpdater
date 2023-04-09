using SUpdater.FolderUpdater.RemoteInformators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

namespace SUpdater.FolderUpdater
{
	static class TableController
	{
		public static IRemoteInformator informator = new HTTPInformator();

		public static FileInfoHash[] GetRemoteFileInfo(string name)
			=> informator.GetRemoteTable(name);

		public static FileInfoHash[] GetLocalFileInfo(string name)
		{
			List<string> ls = GetRecursFiles(Environment.CurrentDirectory + "\\" + name);
			List<FileInfoHash> result = new List<FileInfoHash>();
			foreach (string fname in ls)
			{
				result.Add(new FileInfoHash('.' + fname.Replace(Environment.CurrentDirectory, "").Replace('\\', '/'), GetHash(fname) + "\n"));
			}
			return result.ToArray();
		}
		
		private static string GetHash(string input)
		{
			using (var md5 = MD5.Create())
			{
				using (var stream = File.OpenRead(input))
				{
					var hash = md5.ComputeHash(stream);
					return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
				}
			}
		}
		private static List<string> GetRecursFiles(string start_path)
		{
			List<string> ls = new List<string>();
			try
			{
				string[] folders = Directory.GetDirectories(start_path);
				foreach (string folder in folders)
				{
					ls.AddRange(GetRecursFiles(folder));
				}
				string[] files = Directory.GetFiles(start_path);
				foreach (string filename in files)
				{
					ls.Add(filename);
				}
			}
			catch { }
			return ls;
		}
	}
}
