using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.Core.Domain; 
using Shop.Core.Dto;
using Shop.Core.ServiceInterface;
using Shop.Data;

namespace Shop.ApplicationServices.Services
{
    public class KindergartenServices : IKindergartenServices
    {
        private readonly ShopContext _context;

        //makeing constructor
        public KindergartenServices
            (
                ShopContext context
            )
        {
            _context = context;
        }
        public async Task<Kindergarten> Create(KindergartenDto dto)
        {
            Shop.Core.Domain.Kindergarten kindergarten = new Shop.Core.Domain.Kindergarten(); // Fully qualify the type
            kindergarten.Id = Guid.NewGuid();
            kindergarten.GroupName = dto.GroupName;
            kindergarten.ChidlrenCount = dto.ChidlrenCount;
            kindergarten.KindergartenName = dto.KindergartenName;
            kindergarten.TeacherName = dto.TeacherName;
            kindergarten.CreatedAt = DateTime.Now;
            kindergarten.UpdatedAt = DateTime.Now;

            await _context.Kindergarten.AddAsync(kindergarten);
            await _context.SaveChangesAsync();

            return kindergarten;
        }

        public async Task<Kindergarten> DetailAsync(Guid id)
        {
            var result = await _context.Kindergarten
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }

        public async Task<Kindergarten> Update(KindergartenDto dto)
        {
            Shop.Core.Domain.Kindergarten kindergarten = new Shop.Core.Domain.Kindergarten(); // Fully qualify the type

            kindergarten.Id = dto.Id;
            kindergarten.GroupName = dto.GroupName;
            kindergarten.ChidlrenCount = dto.ChidlrenCount;
            kindergarten.KindergartenName = dto.KindergartenName;
            kindergarten.TeacherName = dto.TeacherName;
            kindergarten.CreatedAt = dto.CreatedAt;
            kindergarten.UpdatedAt = DateTime.Now;
            _context.Kindergarten.Update(kindergarten); 
            await _context.SaveChangesAsync();

            return kindergarten;
        }

        public async Task<Kindergarten> Delete(Guid id)
        {
            var result = await _context.Kindergarten
                 .FirstOrDefaultAsync(x => x.Id == id);
            _context.Kindergarten.Remove(result);
            await _context.SaveChangesAsync();

            return result;
        }
    }
}