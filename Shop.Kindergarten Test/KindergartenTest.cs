using System;
using Microsoft.EntityFrameworkCore;
using Shop.Core.Dto;
using Shop.Core.ServiceInterface;
using Shop.Kindergarten_Test;

namespace Shop.Kindergarten_Test
{
    public class KindergartenTest : TestBase
    {
        // Create Kindergarten test
        [Fact]
        public async Task Create_WhenValidData_ShouldCreateKindergarten()
        {
            KindergartenDto dto = MockKindergartenDto();
            // Act
            var result = await Svc<IKindergartenServices>().Create(dto);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(dto.GroupName, result.GroupName);
            Assert.Equal(dto.ChidlrenCount, result.ChidlrenCount);
            Assert.Equal(dto.KindergartenName, result.KindergartenName);
            Assert.Equal(dto.TeacherName, result.TeacherName);

        }

        // Create Kindergarten test with null data
        [Fact]
        public async Task Create_WhenNullData_ShouldNotCreateKindergarten()
        {
            KindergartenDto dto = MockKindergartenNullData();

            var result = await Svc<IKindergartenServices>().Create(dto);

            Assert.NotNull(result);
            Assert.Null(result.GroupName);
            Assert.Equal(0, result.ChidlrenCount);
            Assert.Null(result.KindergartenName);
            Assert.Null(result.TeacherName);
        }

        [Fact]
        // Update Test
        public async Task Update_WhenValidData_ShouldUpdateKindergarten2()
        {
            // Arrange
       
            // Create a spaceship to update
            var CreateDto = MockKindergartenDto();
            var createdkindergarten = await Svc<IKindergartenServices>().Create(CreateDto);
        
            // Prepare update data
           var updateDto = MockUpdateKindergartenData();
           updateDto.Id = createdkindergarten.Id;
        
        
            // Act
            var updated = await Svc<IKindergartenServices>().Update(updateDto);
        
            // Assert
            Assert.NotNull(updated);
            Assert.Equal(createdkindergarten.Id, updated.Id);
            Assert.Equal(updateDto.GroupName, updated.GroupName);
            Assert.Equal(updateDto.ChidlrenCount, updated.ChidlrenCount);
            Assert.Equal(updateDto.KindergartenName, updated.KindergartenName);
            Assert.Equal(updateDto.TeacherName, updated.TeacherName);
        }

        [Fact]
        //Details test
        public async Task ShouldGetById_Kindergarten_WhenReturnsEqual()
        {
            // Arrange
            var dto = MockKindergartenDto();
            var created = await Svc<IKindergartenServices>().Create(dto); // create a spaceship in DB

            // Act
            var kindergarten = await Svc<IKindergartenServices>().DetailAsync((Guid)created.Id);

            // Assert
            Assert.NotNull(kindergarten);
            Assert.Equal(created.Id, kindergarten.Id);
            Assert.Equal(created.GroupName, kindergarten.GroupName);
            Assert.Equal(created.ChidlrenCount, kindergarten.ChidlrenCount);
            Assert.Equal(created.KindergartenName, kindergarten.KindergartenName);
            Assert.Equal(created.TeacherName, kindergarten.TeacherName);
        }

        [Fact]
        //Delete test
        public async Task Delete_WhenIdMatches_ShouldDeleteKindergarten()
        {
            //arrange
            KindergartenDto dto = MockKindergartenDto();
            //act
            var createKindergarten = await Svc<IKindergartenServices>().Create(dto);
            var deleteKindergarten = await Svc<IKindergartenServices>().Delete((Guid)createKindergarten.Id);
            //assert
            Assert.Equal(createKindergarten.Id, deleteKindergarten.Id);
        
        }



        private KindergartenDto MockKindergartenNullData()
        {
            return new KindergartenDto
            {
                GroupName = null,
                ChidlrenCount = 0,
                KindergartenName = null,
                TeacherName = null,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
        }

        private KindergartenDto MockKindergartenDto()
        {
            return new KindergartenDto
            {
                GroupName = "Test Kindergarten",
                ChidlrenCount = 25,
                KindergartenName = "Happy Kids",
                TeacherName = "Ms. Smith",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
        }
        private KindergartenDto MockUpdateKindergartenData()
        {
            KindergartenDto kindergarten = new()
            {
                Id = Guid.NewGuid(),
                GroupName = "Updated Kindergarten",
                ChidlrenCount = 30,
                KindergartenName = "Joyful Kids",
                TeacherName = "Mr. Johnson",
                CreatedAt = DateTime.UtcNow.AddDays(-10),
                UpdatedAt = DateTime.UtcNow
            };

            return kindergarten;
        }
    }
}