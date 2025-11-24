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
        private readonly IFileServices _fileServices;

        public RealEstateServices
    (
            ShopContext context,
            IFileServices fileServices
        )
    {
        _context = context;
        _fileServices = fileServices;
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

            if (dto.Files != null)
            {
                _fileServices.UploadFilesToDatabase(dto, realEstate);
            }

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
        public async Task<RealEstate> Delete(Guid id)
        {
            var result = await _context.RealEstate
                 .FirstOrDefaultAsync(x => x.Id == id);

            var images = await _context.FileToDatabases
                .Where(x => x.RealEstateId == id)
                .Select(y => new FileToDatabaseDto
                {
                    Id = y.Id,
                    ImageTitle = y.ImageTitle,
                    RealEstateId = y.RealEstateId,
                }).ToArrayAsync();

            await _fileServices.RemoveImagesFromDatabase(images);

            _context.RealEstate.Remove(result);
            await _context.SaveChangesAsync();

            return result;

        }


    }
}
