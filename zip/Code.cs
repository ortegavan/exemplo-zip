using System;
using System.IO;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;

namespace zip
{
	public class Code
	{
		public void Compactar(string pastaOrigem)
		{
			var destino = string.Concat(pastaOrigem, ".zip");
			var arquivos = Directory.GetFiles(pastaOrigem);
			using (var saida = new ZipOutputStream(File.Create(destino)))
			{				
				var buffer = new byte[4096];
				foreach (var arquivo in arquivos)
				{
					var entrada = new ZipEntry(Path.GetFileName(arquivo));
					entrada.DateTime = DateTime.Now;
					saida.PutNextEntry(entrada);
					using (var stream = File.OpenRead(arquivo))
					{
						int bytes;
						do
						{
							bytes = stream.Read(buffer, 0, buffer.Length);
							saida.Write(buffer, 0, bytes);
						} while (bytes > 0);
					}
				}
				saida.Finish();
				saida.Close();
			}
		}

		public void Descompactar(string arquivoOrigem, string pastaDestino)
		{
			ZipFile zip = null;
			try
			{
				zip = new ZipFile(File.OpenRead(arquivoOrigem));
				foreach (ZipEntry arquivo in zip)
				{
					var buffer = new byte[4096];
					var entrada = zip.GetInputStream(arquivo);
					var destino = Path.Combine(pastaDestino, arquivo.Name);
					using (var saida = File.Create(destino))
					{
						StreamUtils.Copy(entrada, saida, buffer);
					}
				}
			}
			finally
			{
				if (zip != null)
				{
					zip.IsStreamOwner = true;
					zip.Close();
				}
			}
		}
	}
}
