using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Core.Domain;
using Shop.Core.Dto;
using Shop.Core.ServiceInterface;
using Microsoft.Extensions.Hosting;
using Shop.Data;

namespace Shop.ApplicationServices.Services
{
    public class FileServices : IFileServices
    {
        private readonly IHostEnvironment _webHost;
        private readonly ShopContext _context;

        public FileServices
            (
                IHostEnvironment webHost,
                ShopContext context
            )
        {
            _webHost = webHost;
            _context = context;
        }
        public void FilesToApi(SpaceshipDto dto, Spaceships domain)
        {
            if (dto.Files != null && dto.Files.Count > 0)
            {
                if (!Directory.Exists(_webHost.ContentRootPath + "\\multibleFileUpload\\"))
                {
                    Directory.CreateDirectory(_webHost.ContentRootPath +"\\multibleFileUpload\\");
                }

                foreach (var file in dto.Files) {
                    {
                        string uploadsFolder = Path.Combine(_webHost.ContentRootPath, "multibleFileUpload");
                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            file.CopyTo(fileStream);

                            FileToApi path = new FileToApi
                            {
                                Id = Guid.NewGuid(),
                                ExistingFilePath = uniqueFileName,
                                SpaceshipId = domain.Id

                            };
                            _context.FileToApis.Add(path);
                        }
                    }
            }
        }
    }
}
}