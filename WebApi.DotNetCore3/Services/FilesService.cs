using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.DotNetCore3.Services
{
    public class FilesService : IFilesService
    {
        public byte[] GetFile(string path)
        {
            var bytes = File.ReadAllBytes(path);
            return bytes;
        }
    }
}
