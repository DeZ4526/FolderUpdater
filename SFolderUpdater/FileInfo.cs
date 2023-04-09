namespace SUpdater.FolderUpdater
{
	class FileInfoHash
	{
		public string Name;
		public string MD5;

		public FileInfoHash(string name, string mD5)
		{
			Name = name.Trim();
			MD5 = mD5.Trim();
		}
		public override string ToString()
		{
			return Name + "|" + MD5;
		}
	}
}
