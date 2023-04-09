namespace SUpdater.FolderUpdater.RemoteInformators
{
	internal interface IRemoteInformator
	{
		void SetAddress(string info);
		string GetAddress();
		FileInfoHash[] GetRemoteTable(string name);
	}
}
