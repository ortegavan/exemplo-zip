using System;

namespace zip
{
	class Program
	{
		static void Main(string[] args)
		{			
			if (args.Length == 2 && args[0].Equals("zip"))
			{
				Compactar(args[1]);
			}
			else if (args.Length == 3 && args[0].Equals("unzip"))
			{
				Descompactar(args[1], args[2]);
			}
			else
			{
				Console.WriteLine("Para compactar arquivos, use:");
				Console.WriteLine("zip zip [pasta de origem]");
				Console.WriteLine();
				Console.WriteLine("Para descompactar um arquivo, use:");
				Console.WriteLine("zip unzip [arquivo de origem] [pasta de destino]");
			}
		}

		static void Compactar(string pastaOrigem)
		{
			Console.WriteLine("Compactando {0}...", pastaOrigem);

			new Code().Compactar(pastaOrigem);

			Console.WriteLine("Concluído!");
		}

		static void Descompactar(string arquivoOrigem, string pastaDestino)
		{
			Console.WriteLine("Descompactando {0}...", arquivoOrigem);

			new Code().Descompactar(arquivoOrigem, pastaDestino);

			Console.WriteLine("Concluído!");
		}
	}
}