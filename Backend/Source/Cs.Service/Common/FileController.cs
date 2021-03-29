using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Cs.Application.Files;
using Cs.Application.Files.Models;
using Cs.Common.Models;
using Cs.Service.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using File = Cs.Domain.Files.File;

namespace Cs.Service.Common
{
    public class FileController : BaseController
    {
        private readonly IFileService _files;

        public FileController(IFileService files)
        {
            _files = files;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var file = _files.Get(id);

            return File(file.Content, "application/x-msdownload", file.Filename);
        }
        
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Post(ICollection<IFormFile> files)
        {
            var filesToSave = new List<File>();
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var fileToSave = new File {Filename = file.FileName, Format = file.ContentType};
                    
                    using (var stream = new MemoryStream())
                    {
                        await file.CopyToAsync(stream);
                        fileToSave.Content = stream.ToArray();
                    }
                    
                    filesToSave.Add(fileToSave);
                }
            }

            var fileViewModels = _files.Add(filesToSave.ToArray());

            var result = new ListResult<FileViewModel>(fileViewModels.ToList(), fileViewModels.Length);
            
            return Json(ServiceResult<ListResult<FileViewModel>>.Ok(result));
        }
    }
}