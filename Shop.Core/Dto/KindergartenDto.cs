using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Shop.Core.Domain;

namespace Shop.Core.Dto
{
    public class KindergartenDto
    {
        public Guid Id { get; set; }
        public string GroupName { get; set; }
        public int ChidlrenCount { get; set; }
        public string KindergartenName { get; set; }
        public string TeacherName { get; set; }
        public List<IFormFile> Files { get; set; }
        public IEnumerable<FileToDatabaseDto> Image { get; set; }
            = new List<FileToDatabaseDto>();
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
