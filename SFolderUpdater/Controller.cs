using System;
using System.IO;

namespace SUpdater.FolderUpdater
{
	static class Controller
	{
		static FileInfoHash[] RemoteFiles = new FileInfoHash[1];
		static FileInfoHash[] LocalFiles = new FileInfoHash[1];
		public static string PathToFolderLocal;
		public static double MaxSpeed = 10000;

		private static void UpdateTables(string name)
		{
			RemoteFiles = TableController.GetRemoteFileInfo(name);
			LocalFiles = TableController.GetLocalFileInfo(name, PathToFolderLocal);
		}
		public static void SetAddress(string info)
		{
			TableController.informator.SetAddress(info);
		}
		public static async void Update(string name)
		{
			UpdateTables(name);
			FileInfoHash[] file = TableComparator.GetDifferences(RemoteFiles, LocalFiles);
			if (file != null) 
			{
				OnStartDownload?.Invoke(file.Length);
				foreach (var item in file)
				{
					await Downloader.FileDownloader.DownloadUriWithThrottling(new Uri(TableController.informator.GetAddress() + item.Name), item.Name, MaxSpeed);
					OnFileDownload?.Invoke(item);
				}
				OnEndDownload?.Invoke();
			}
		}
		public static int RemoveDifferences(string name)
		{
			UpdateTables(name);
			int result = 0;
			FileInfoHash[] differences = TableComparator.GetNewLocalFiles(RemoteFiles, LocalFiles);
			for (int i = 0; i < differences.Length; i++)
			{
				try
				{
					File.Delete(differences[i].Name);
					result++;
				}
				catch { }
			}
			return result;
		}
		public static event Action<int>? OnStartDownload;
		public static event Action? OnEndDownload;
		public static event Action<FileInfoHash>? OnFileDownload;

	}
}
