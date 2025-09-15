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
    public class SpaceshipServices: ISpaceshipServices
    {
        private readonly ShopContext _context;

        //makeing constructor
        public SpaceshipServices
            (
                ShopContext context
            )
        {
            _context = context;
        }
        public async Task<Spaceships> Create(SpaceshipDto dto)
        {
            Spaceships spaceships = new Spaceships();

            spaceships.Id = Guid.NewGuid();
            spaceships.Name = dto.Name;
            spaceships.Classification = dto.Classification;
            spaceships.BuildDate = dto.BuildDate;
            spaceships.Crew = dto.Crew;
            spaceships.EnginePower = dto.EnginePower;
            spaceships.CreatedAt = DateTime.Now;
            spaceships.ModifiedAt = DateTime.Now;

            await _context.Spaceships.AddAsync(spaceships);
            await _context.SaveChangesAsync();

            return spaceships;

        }

        public async Task<Spaceships> DetailAsync(Guid id)
        {
            var result = await _context.Spaceships
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }

        public async Task<Spaceships> Delete(Guid id)
        {
            var result = await _context.Spaceships
                 .FirstOrDefaultAsync(x => x.Id == id);
            _context.Spaceships.Remove(result);
            await _context.SaveChangesAsync();

            return result;
            //leida üles konkreetne soovitud rida, mida soovite kutsuda

            //kui rida on leitud, siis eemaldage andmebaasist
        }
    }
}
