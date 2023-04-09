using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace SUpdater.FolderUpdater.RemoteInformators
{
	class HTTPInformator : IRemoteInformator
	{
		public string Address { get; private set; }

		public FileInfoHash[] GetRemoteTable(string name)
		{
			List<FileInfoHash> result = new List<FileInfoHash>();
			var _source = GetAllSourceCode(Address + "?id=" + name);
			string[] files = _source.Split('>');

			for (int i = 0; i < files.Length - 1; i++)
			{
				try
				{
					string[] info = files[i].Split('|');
					result.Add(new FileInfoHash(info[0], info[1]));
				}
				catch
				{
					break;
				}
			}
			return result.ToArray();
		}
		private string GetAllSourceCode(string url)
		{
			Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
			var wc = new WebClient();
			wc.Encoding = Encoding.GetEncoding(1251);

			
			var html = wc.DownloadString(url);


			return html;
		}
		public void SetAddress(string info)
			=> Address = info;

		public string GetAddress()
			=> Address;
	}
}
