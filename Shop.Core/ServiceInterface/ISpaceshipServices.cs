using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Core.Domain;
using Shop.Core.Dto;

namespace Shop.Core.ServiceInterface
{
    public interface ISpaceshipServices
    {
        Task<Spaceships> Create(SpaceshipDto dto);
        Task<Spaceships> DetailAsync(Guid id);

        Task<Spaceships> Delete(Guid id);

        Task<Spaceships> Update(SpaceshipDto dto);
    }
}
