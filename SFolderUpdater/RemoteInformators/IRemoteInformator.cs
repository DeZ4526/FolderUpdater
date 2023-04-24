namespace SUpdater.FolderUpdater.RemoteInformators
{
	public interface IRemoteInformator
	{
		void SetAddress(string info);
		string GetAddress();
		FileInfoHash[] GetRemoteTable(string name);
	}
}
