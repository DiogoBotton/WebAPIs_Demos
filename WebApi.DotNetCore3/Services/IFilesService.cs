using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.DotNetCore3.Services
{
    public interface IFilesService
    {
        byte[] GetFile(string path);
    }
}
