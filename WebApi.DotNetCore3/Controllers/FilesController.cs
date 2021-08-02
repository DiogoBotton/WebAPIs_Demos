using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DotNetCore3.Services;

namespace WebApi.DotNetCore3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IFilesService _files;

        public FilesController(IFilesService files)
        {
            _files = files;
        }

        [HttpGet("{type}")]
        public IActionResult DownloadFile(string type)
        {
            try
            {
                string contentType = "";
                string pathFile = "";
                switch (type)
                {
                    case "xlsx":
                        contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        pathFile = @"ModelFiles\ModeloXlsx.xlsx";
                        break;
                    case "csv":
                        contentType = "text/csv";
                        pathFile = @"ModelFiles\ModeloCsv.csv";
                        break;
                    case "xml":
                        contentType = "application/xml";
                        pathFile = @"ModelFiles\model.xml";
                        break;
                    default:
                        return StatusCode(404, "Tipo de arquivo inválido");
                }

                var dataBytes = _files.GetFile(pathFile);

                return File(dataBytes, contentType, pathFile.Replace(@"ModelFiles\", ""));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Houve algum erro ao liberar download do arquivo. {ex.Message}");
            }
        }
    }
}
