<h2 align="center">
    Exemplo - SharpZipLib
</h2>
<p align="center">
    <a href="https://github.com/ortegavan/exemplo-zip/commits/">
        <img alt="GitHub last commit" src="https://img.shields.io/github/last-commit/ortegavan/exemplo-zip?style=flat-square">
    </a>
    <a href="https://github.com/prettier">
        <img alt="code style: prettier" src="https://img.shields.io/badge/code_style-prettier-ff69b4.svg?style=flat-square">
    </a>      
</p>

Exemplo simples que mostra como compactar e descompactar arquivos utilizando a [**SharpZipLib**](https://github.com/icsharpcode/SharpZipLib).

Para instalar a SharpZipLib, execute o comando abaixo ou utilize o NuGet Package Manager:

```
dotnet add package SharpZipLib
```

Importe os namespaces abaixo na classe onde vai escrever os métodos de compactação/descompactação:

```c#
using System.IO;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
```

Para compactar arquivos, utilize:

```c#
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
```

Para descompactar um arquivo, utilize:

```c#
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
```

Para compactar arquivos executando o exemplo que está na pasta `bin\debug\net5.0` utilize:

```
zip zip [pasta de origem]
```

Onde `pasta de origem` é a pasta que contém os arquivos a serem compactados.

Para descompactar arquivos, utilize:

```
zip unzip [arquivo de origem] [pasta de destino]
```

Onde `arquivo de origem` é o arquivo .zip e `pasta de destino` é o local onde os arquivos serão descompactados.

