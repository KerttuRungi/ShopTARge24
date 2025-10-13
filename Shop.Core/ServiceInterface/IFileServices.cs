using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Core.Domain;
using Shop.Core.Dto;

namespace Shop.Core.ServiceInterface
{
    public interface IFileServices
    {
        void FilesToApi(SpaceshipDto dto, Spaceships domain);
        Task<FileToApi> RemoveImageFromApi(FileToApiDto dto);

        Task<List<FileToApi>> RemoveImagesFromApi(FileToApiDto[] dtos);
        void UploadFilesToDatabase(RealEstateDto dto, RealEstate domain);
        void UploadFilesToDatabaseKindergarten(KindergartenDto dto, Kindergarten domain);
        public void DeleteFilesFromDatabaseKindergarten(Guid kindergartenId);
    }
}

