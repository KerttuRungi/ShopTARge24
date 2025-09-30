using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.Core.Domain;
using Shop.Core.ServiceInterface;
using Shop.Data;
using Shop.Core.Dto;

namespace Shop.ApplicationServices.Services
{
    public class RealEstateServices : IRealEstateServices
    {
        private readonly ShopContext _context;

    public RealEstateServices
    (
            ShopContext context
        )
    {
        _context = context;
    }
    public async Task<RealEstate> Create(RealEstateDto dto)
    {
        RealEstate realEstate = new RealEstate();
            realEstate.Id = Guid.NewGuid();
            realEstate.Area = dto.Area;
            realEstate.Location = dto.Location;
            realEstate.RoomNumber = dto.RoomNumber;
            realEstate.BuildingType = dto.BuildingType;
            realEstate.CreatedAt = DateTime.Now;
            realEstate.ModifiedAt = DateTime.Now;

        await _context.RealEstate.AddAsync(realEstate);
        await _context.SaveChangesAsync();

        return realEstate;

    }
        public async Task<RealEstate> Update(RealEstateDto dto)
        {
            RealEstate realEstate = new RealEstate();

            realEstate.Id = dto.Id;
            realEstate.Area = dto.Area;
            realEstate.Location = dto.Location;
            realEstate.RoomNumber = dto.RoomNumber;
            realEstate.BuildingType = dto.BuildingType;
            realEstate.CreatedAt = dto.CreatedAt;
            realEstate.ModifiedAt = DateTime.Now;

            _context.RealEstate.Update(realEstate);
            await _context.SaveChangesAsync();

            return realEstate;
        }
        public async Task<RealEstate> DetailAsync(Guid id)
        {
            var result = await _context.RealEstate
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }


    }
}
