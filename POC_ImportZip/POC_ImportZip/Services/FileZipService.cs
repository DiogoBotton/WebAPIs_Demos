using ICSharpCode.SharpZipLib.Zip;
using POC_ImportZip.Models;
using POC_ImportZip.Services.Interfaces;
using System.IO.Compression;

namespace POC_ImportZip.Services;

public class FileZipService : IFileZipService
{
    public async Task<List<InfosZip>> ImportZipWithSharpZipLibAndDecompressZip(IFormFile zipFile)
    {
        List<InfosZip> zipInfos = new List<InfosZip>();

        // Utiliza MemoryStream para ler arquivo ZIP sem a necessidade de descompacta-lo dentro do projeto
        using MemoryStream ms = new MemoryStream();

        // Copia o conteúdo de zipFile para a memoryStream
        await zipFile.CopyToAsync(ms);
        // Altera posição da stream para não dar erro no momento de leitura
        ms.Position = 0;

        // Esta classe da biblioteca SharpZipLib descomprime o arquivo
        // Necessário para fazer validações ou salvar o arquivo em núvem, do contrário, o arquivo não será salvo da forma correta (por exemplo: a imagem será cortada)
        using ZipInputStream zipStream = new ZipInputStream(ms);

        ZipEntry entry;
        // Navega entre os arquivos do zip (enquanto houver um próximo arquivo != null)
        while ((entry = zipStream.GetNextEntry()) != null)
        {
            // MemoryStream que armazenará arquivo atual
            using MemoryStream entryMemoryStream = new MemoryStream();

            // Buffer para armazenar temporariamente os bytes do arquivo e armazena-lo na memoryStream
            // Explicação Buffer: É uma região de memória física utilizada para armazenar temporariamente os dados enquanto eles estão sendo movidos de um lugar para outro.
            byte[] buffer = new byte[4096];
            int bytesRead;
            while ((bytesRead = zipStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                entryMemoryStream.Write(buffer, 0, bytesRead);
            }

            // Adquire primeira posição da stream
            entryMemoryStream.Seek(0, SeekOrigin.Begin);
            try
            {
                // Carrega imagem e retorna alguns dados sobre cada imagem dentro do zip
                using (var img = await SixLabors.ImageSharp.Image.LoadAsync(entryMemoryStream))
                {
                    zipInfos.Add(new InfosZip(entryMemoryStream.ToArray(), entry.Name, img.Width, img.Height, entryMemoryStream.Length));
                }
            }
            catch (Exception)
            {
                zipInfos.Add(new InfosZip(null, $"Arquivo {entry.Name} está corrompido.", 0, 0, 0));
            }
        }

        return zipInfos;
    }

    public async Task<List<InfosZip>> ImportZipWithSystem(IFormFile zipFile)
    {
        List<InfosZip> zipInfos = new List<InfosZip>();

        // Utiliza MemoryStream para ler arquivo ZIP sem a necessidade de descompacta-lo dentro do projeto
        using MemoryStream ms = new MemoryStream();

        // Copia o conteúdo de zipFile para a memoryStream
        await zipFile.CopyToAsync(ms);

        using ZipArchive zip = new ZipArchive(ms);

        foreach (var entry in zip.Entries)
        {
            using Stream entryStream = entry.Open();

            // Define array de bytes com o comprimento do arquivo atual (imagem atual)
            byte[] bytesImg = new byte[entry.Length];
            // Popula array de bytes baseado no arquivo
            await entryStream.ReadAsync(bytesImg, 0, bytesImg.Length);

            // Abre os bytes da imagem em memória
            using var imgMemoryStream = new MemoryStream(bytesImg);
            try
            {
                // Carrega imagem e retorna alguns dados sobre cada imagem dentro do zip
                using (var img = await SixLabors.ImageSharp.Image.LoadAsync(imgMemoryStream))
                {
                    zipInfos.Add(new InfosZip(imgMemoryStream.ToArray(), entry.Name, img.Width, img.Height, imgMemoryStream.Length));
                }
            }
            catch (Exception)
            {
                zipInfos.Add(new InfosZip(null, $"Arquivo {entry.Name} está corrompido.", 0, 0, 0));
            }
        }

        return zipInfos;
    }

    // Fonte: https://houseofcat.io/guides/csharp/net/compression

    // because the bytes are here, and the streams are built here... this async is virtually useless
    // and does nothing to help with performance nor will it really ever await.
    public async Task<byte[]> CompressAsync(byte[] data)
    {
        using var compressedStream = new MemoryStream();
        using (var gzipStream = new GZipStream(compressedStream, CompressionLevel.Optimal, false))
        {
            await gzipStream
                .WriteAsync(data)
                .ConfigureAwait(false);
        }

        return compressedStream.ToArray();
    }

    // in this case, we have the input data, but we maybe waiting to write based on the stream status,
    // so writeasync could block depending on what the caller is doing with the stream.
    public async Task CompressAsync(Stream outputStream, byte[] data)
    {
        // Add a little safety check.
        if (!outputStream.CanWrite) throw new InvalidOperationException($"{nameof(outputStream)} is not available for writing to.");

        using (var gzipStream = new GZipStream(outputStream, CompressionLevel.Optimal, false))
        {
            await gzipStream
                .WriteAsync(data)
                .ConfigureAwait(false);
        }
    }

    // Métodos abaixo não funcionaram com arquivos zips, mas talvez sirvam para outras coisas
    // Exemplo de Descompressão de dados binarios (array de bytes)

    // Descompressão que retorna o array de bytes descompromido
    private async Task<byte[]> DecompressAsync(byte[] compressedData)
    {
        using var uncompressedStream = new MemoryStream();

        using var compressedStream = new MemoryStream(compressedData);
        using (var gZipStream = new GZipStream(compressedStream, CompressionMode.Decompress, false))
        {
            await gZipStream.CopyToAsync(uncompressedStream);
        }

        return uncompressedStream.ToArray();
    }

    // Descompressão que retorna a stream descomprimida baseada no array de bytes comprimido
    private async Task DecompressAsync(Stream outputStream, byte[] compressedData)
    {
        if (!outputStream.CanWrite) throw new InvalidOperationException($"{nameof(outputStream)} is not available for writing to.");

        using var compressedStream = new MemoryStream(compressedData);
        using (var gZipStream = new GZipStream(compressedStream, CompressionMode.Decompress, false))
        {
            await gZipStream.CopyToAsync(outputStream).ConfigureAwait(false);
        }
    }
}
