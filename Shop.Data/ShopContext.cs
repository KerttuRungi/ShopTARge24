using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.Core.Domain;

namespace Shop.Data
{
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options) 
            : base(options) { }
        public DbSet<Spaceships> Spaceships { get; set; }
        public DbSet<FileToApi> FileToApis { get; set; }

        public DbSet<RealEstate> RealEstate { get; set; }
        public DbSet<FileToDatabase> FileToDatabases { get; set; }
    }
}
