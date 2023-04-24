using System.Collections.Generic;

namespace SUpdater.FolderUpdater
{
	public static class TableComparator
	{
		/// <summary>
		/// Поиск новых файлов на локальном ПК
		/// </summary>
		/// <param name="remote">Файлы на удаленном сервере</param>
		/// <param name="local">Файлы на локаьном ПК</param>
		/// <returns></returns>
		public static FileInfoHash[] GetNewLocalFiles(FileInfoHash[] remote, FileInfoHash[] local)
		{
			List<FileInfoHash> differences = new List<FileInfoHash>();
			for (int i = 0; i < local.Length; i++)
			{
				if (!Compare(local[i]))
					differences.Add(local[i]);
			}
			return differences.ToArray();

			bool Compare(FileInfoHash _local)
			{
				for (int i = 0; i < remote.Length; i++)
				{
					if (remote[i].Name == _local.Name)
					{
						return true;
					}
				}
				return false;
			}
		}
		/// <summary>
		/// Поиск недостоющих или недостоверных файлов на локальном ПК
		/// </summary>
		/// <param name="remote">Файлы на удаленном сервере</param>
		/// <param name="local">Файлы на локаьном ПК</param>
		/// <returns></returns>
		public static FileInfoHash[] GetDifferences(FileInfoHash[] remote, FileInfoHash[] local)
		{
			List<FileInfoHash> differences = new List<FileInfoHash>();
			for(int i = 0; i < remote.Length; i++)
			{
				if (!Compare(remote[i]))
				{
					differences.Add(new FileInfoHash(remote[i].Name, remote[i].MD5));
				}
			}
			return differences.ToArray();
			bool Compare(FileInfoHash _remote)
			{
				for (int i = 0; i < local.Length; i++)
				{
					if (local[i].Name == _remote.Name && local[i].MD5  == _remote.MD5 )
					{
						return true;
					}
				}
				return false;
			}
		}
	}
}
