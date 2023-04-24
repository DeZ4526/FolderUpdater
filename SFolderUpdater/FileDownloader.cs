using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SUpdater.Downloader
{
	static class FileDownloader
	{
		private static double downloadSpeed;
		public static double DownloadSpeed 
		{ 
			get => downloadSpeed; 
			private set
			{
				OnChangeSpeed?.Invoke(value);
				downloadSpeed = value;

			}
		}

		public static async Task DownloadUriWithThrottling(Uri uri, string path, double speedKbps)
		{
			var req = WebRequest.CreateHttp(uri);
			new FileInfo(path).Directory.Create();
			using (var resp = await req.GetResponseAsync())
			using (var stream = resp.GetResponseStream())
			using (var outfile = File.OpenWrite(path))
			{
				var startTime = DateTime.Now;
				
				long totalDownloaded = 0;
				var buffer = new byte[0x10000];
				while (true)
				{
					var _TimeStartSpeed = DateTime.Now;
					var actuallyRead = await stream.ReadAsync(buffer, 0, buffer.Length);


					if (actuallyRead == 0) // end of stream
						return;
					await outfile.WriteAsync(buffer, 0, actuallyRead);
					totalDownloaded += actuallyRead;

					// recalc speed and wait
					var expectedTime = totalDownloaded / 1024.0 / speedKbps;
					var actualTime = (DateTime.Now - startTime).TotalSeconds;


					var _TimeEndSpeed = DateTime.Now;
					var _TimeSpeed = (DateTime.Now - startTime).TotalSeconds;
					DownloadSpeed = actuallyRead / _TimeSpeed;
					if (expectedTime > actualTime)
						await Task.Delay(TimeSpan.FromSeconds(expectedTime - actualTime));
				}
			}
		}
		public static event Action<double> OnChangeSpeed;
	}
}
