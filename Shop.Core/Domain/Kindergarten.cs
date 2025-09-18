using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.Domain
{
    public class Kindergarten
    {
        public Guid Id { get; set; }
        public string GroupName { get; set; }
        public int ChidlrenCount { get; set; }
        public string KindergartenName { get; set; }
        public string TeacherName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
