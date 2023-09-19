using POC_ImportZip.Models;
using POC_ImportZip.Services.Interfaces;
using System.IO.Compression;

namespace POC_ImportZip.Services;

public class FileZipService : IFileZipService
{
    public async Task<List<InfosZip>> ImportZip(IFormFile zipFile)
    {
        List<InfosZip> zipInfos = new List<InfosZip>();

        // Utiliza MemoryStream para ler arquivo ZIP sem a necessidade de descompacta-lo dentro do projeto
        using (MemoryStream ms = new MemoryStream())
        {
            // Copia o conteúdo de zipFile para a memoryStream
            await zipFile.CopyToAsync(ms);

            using (ZipArchive zip = new ZipArchive(ms))
            {
                foreach (var entry in zip.Entries)
                {
                    using (Stream entryStream = entry.Open())
                    {
                        // Define array de bytes com o comprimento do arquivo atual (imagem atual)
                        byte[] bytesImg = new byte[entry.Length];
                        // Popula array de bytes baseado no arquivo
                        await entryStream.ReadAsync(bytesImg, 0, bytesImg.Length);

                        // Abre os bytes da imagem em memória
                        using (var imgMemoryStream = new MemoryStream(bytesImg))
                        try
                        {
                            // Carrega imagem e retorna alguns dados sobre cada imagem dentro do zip
                            using (var img = await SixLabors.ImageSharp.Image.LoadAsync(imgMemoryStream))
                            {
                                zipInfos.Add(new InfosZip(entry.Name, img.Width, img.Height, imgMemoryStream.Length));
                            }
                        }
                        catch (Exception)
                        {
                            zipInfos.Add(new InfosZip($"Arquivo {entry.Name} está corrompido.", 0, 0, 0));
                        }
                    }
                }
            }
        }

        return zipInfos;
    }
}
