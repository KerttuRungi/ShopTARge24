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
using Microsoft.EntityFrameworkCore;
using System.Xml;
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
        public void UploadFilesToDatabaseKindergarten(KindergartenDto dto, Kindergarten domain)
        {
            if (dto.Files != null && dto.Files.Count > 0)
            {
                foreach (var file in dto.Files)
                {
                    using (var target = new MemoryStream())
                    {
                        FileToDatabaseKindergarten files = new FileToDatabaseKindergarten()
                        {
                            Id = Guid.NewGuid(),
                            ImageTitle = file.FileName,
                            KindergartenId = domain.Id
                        };

                        file.CopyTo(target);
                        files.ImageData = target.ToArray();

                        _context.FileToDatabases.Add(files);

                    }
                }
            }

        }
    }
}
