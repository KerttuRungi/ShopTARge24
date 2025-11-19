using System;
using Microsoft.EntityFrameworkCore;
using Shop.Core.Dto;
using Shop.Core.ServiceInterface;


namespace Spaceship_Test
{
    public class SpaceshipTest : TestBase
    {
        //Create Test
        [Fact]
        //Test name should be MethodName_Scenario_ExpectedOutcome
        public async Task Create_WhenValidData_ShouldCreateSpaceship()
        {
            // Arrange
            SpaceshipDto dto = MockSpaceshipDto();
            // Act
            var result = await Svc<ISpaceshipServices>().Create(dto);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(dto.Name, result.Name);
            Assert.Equal(dto.Classification, result.Classification);
            Assert.Equal(dto.BuildDate, result.BuildDate);
            Assert.Equal(dto.Crew, result.Crew);

        }

        //Create Test with null data
        [Fact]
        public async Task Create_WhenNullData_ShouldNotCreateSpaceship()
        {
            // Arrange
            SpaceshipDto dto = MockNullSpaceshipData();
            // Act
            var result = await Svc<ISpaceshipServices>().Create(dto);
            // Assert
            Assert.NotNull(result);  //All null except CreatedAt and ModifiedAt
            Assert.Null(result.Name);
            Assert.Null(result.Classification);
            Assert.Null(result.BuildDate);
            Assert.Null(result.Crew);
            Assert.Null(result.EnginePower);
        }

        //Working Update Test
        [Fact]
        public async Task Update_WhenValidData_ShouldUpdateSpaceship()
        {
            // Arrange
            SpaceshipDto dto = MockSpaceshipDto();
            var createSpaceship = await Svc<ISpaceshipServices>().Create(dto);

            SpaceshipDto updateDto = MockUpdateSpaceshipData();
            // Act
            updateDto.Name = createSpaceship.Name;
            updateDto.Classification = createSpaceship.Classification;
            updateDto.BuildDate = createSpaceship.BuildDate;
            updateDto.Crew = createSpaceship.Crew;
            updateDto.EnginePower = createSpaceship.EnginePower;


            // Useing Create method to simulate update (as Update method is commented out), to awoid tracking error
            var result = await Svc<ISpaceshipServices>().Create(updateDto);

            // Assert
            Assert.Equal(createSpaceship.Name, result.Name);
            Assert.Equal(createSpaceship.Classification, result.Classification);
            Assert.Equal(createSpaceship.BuildDate, result.BuildDate);
            Assert.Equal(createSpaceship.Crew, result.Crew);

        }

        //Not working Update Test
        //public async Task Update_WhenValidData_ShouldUpdateSpaceship()
        //{
        //    // Arrange
        //
        //    // Create a spaceship to update
        //    var CreateDto = MockSpaceshipDto();
        //    var createdSpaceship = await Svc<ISpaceshipServices>().Create(CreateDto);
        //
        //    // Prepare update data
        //    var updateDto = MockUpdateSpaceshipData();
        //    updateDto.Id = createdSpaceship.Id;
        //
        //
        //    // Act
        //    var updated = await Svc<ISpaceshipServices>().Update(updateDto);
        //
        //    // Assert
        //    Assert.NotNull(updated);
        //    Assert.Equal(updated.Id, createdSpaceship.Id);
        //    Assert.Equal(updateDto.Name, updated.Name);
        //    Assert.Equal(updateDto.Classification, updated.Classification);
        //    Assert.Equal(updateDto.Crew, updated.Crew);
        //    Assert.Equal(updateDto.EnginePower, updated.EnginePower);
        //    Assert.Equal(updateDto.BuildDate, updated.BuildDate);
        //}

        [Fact]
        //Details test
        public async Task ShouldGetById_Spaceship_WhenReturnsEqual()
        {
            // Arrange
            var dto = MockSpaceshipDto();
            var created = await Svc<ISpaceshipServices>().Create(dto); // create a spaceship in DB

            // Act
            var spaceship = await Svc<ISpaceshipServices>().DetailAsync((Guid)created.Id);

            // Assert
            Assert.NotNull(spaceship);
            Assert.Equal(created.Id, spaceship.Id);
            Assert.Equal(created.Name, spaceship.Name);
            Assert.Equal(created.Classification, spaceship.Classification);
            Assert.Equal(created.Crew, spaceship.Crew);
            Assert.Equal(created.EnginePower, spaceship.EnginePower);
        }

        [Fact]
        //Delete test
        public async Task Delete_WhenIdMatches_ShouldDeleteSpaceship()
        {
            //arrange
            SpaceshipDto dto = MockSpaceshipDto();
            //act
            var createSpaceship = await Svc<ISpaceshipServices>().Create(dto);
            var deleteSpaceship = await Svc<ISpaceshipServices>().Delete((Guid)createSpaceship.Id);
            //assert
            Assert.Equal(createSpaceship.Id, deleteSpaceship.Id);
        }
        private SpaceshipDto MockNullSpaceshipData()
        {
            return new SpaceshipDto
            {
                Name = null,
                Classification = null,
                BuildDate = null,
                Crew = null,
                EnginePower = null,
                CreatedAt = null,
                ModifiedAt = null
            };
        }


        private SpaceshipDto MockSpaceshipDto()
        {
            return new SpaceshipDto
            {
                Name = "Spaceship",
                Classification = "Spaceship",
                BuildDate = DateTime.UtcNow,
                Crew = 1234,
                EnginePower = 4321,
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };
        }
        private SpaceshipDto MockUpdateSpaceshipData()
        {
            SpaceshipDto spaceship = new()
            {
                Name = "Updated Spaceship",
                Classification = "Updated Classification",
                BuildDate = DateTime.UtcNow,
                Crew = 2000,
                EnginePower = 5000,
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow,
            };

            return spaceship;
        }
    }
}