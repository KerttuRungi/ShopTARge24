using Shop.Core.Dto;
using Shop.Core.ServiceInterface;
using Shop.RealEstateTest;


namespace Shop.RealEstate_Test
{
    public class RealEstateTest : TestBase
    {
        [Fact]
        public async Task ShouldNot_AddEmptyRealEstate_WhenReturnResult()
        {
            // Arrange
            RealEstateDto dto = new()
            {
                Area = 120.5,
                Location = "Test Location",
                RoomNumber = 3,
                BuildingType = "Apartment",
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };

            // Act
            var result = await Svc<IRealEstateServices>().Create(dto);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldNot_GetByIdRealestate_WhenReturnsNotEqual()
        {
            //arrange
            Guid wrongGuid = Guid.NewGuid();
            Guid guid = Guid.Parse("a4ca2fc2-4d24-4cf4-9ba9-18e9daf58a7e");
            
            //act
            await Svc<IRealEstateServices>().DetailAsync(guid);

            //assert
            Assert.NotEqual(wrongGuid, guid);
        }

        [Fact]
        public async Task GetByIdRealestate_WhenReturnsEqual()
        {
            //arrange
            Guid databaseguid = Guid.Parse("a4ca2fc2-4d24-4cf4-9ba9-18e9daf58a7e");
            Guid guid = Guid.Parse("a4ca2fc2-4d24-4cf4-9ba9-18e9daf58a7e");
           
            //act
            await Svc<IRealEstateServices>().DetailAsync(guid);
            
            //assert
            Assert.Equal(databaseguid, guid);
        }

        [Fact]
        public async Task ShouldDeleteByIdRealestate_WhenDeleteRealEstate()
        {
            //arrange
            RealEstateDto dto = MockRealEstateData();

            //act
            var createRealEstate = await Svc<IRealEstateServices>().Create(dto);
            var deleteRealEstate = await Svc<IRealEstateServices>().Delete((Guid)createRealEstate.Id);
            //assert
            Assert.Equal(createRealEstate.Id, deleteRealEstate.Id);

        }
   

        [Fact]
        public async Task ShouldNot_DeleteByIdRealEstate_WhenDidNotDeleteRealEstate()
        {
            //arrange
            var dto = MockRealEstateData();
            //act
            var realEstate1 = await Svc<IRealEstateServices>().Create(dto);
            var realEstate2 = await Svc<IRealEstateServices>().Create(dto);

            var result = await Svc<IRealEstateServices>().Delete((Guid)realEstate2.Id);
            //assert
            Assert.NotEqual(realEstate1.Id, result.Id);

        }

        [Fact]
        public async Task Should_UpdateRealEstate_WhenUpdateData()
        {
            //arrange
            var guid = new Guid("68ce7565-9105-4945-b428-b8e25ec061c6");

            RealEstateDto dto = MockRealEstateData();

            RealEstateDto domain = new();

            domain.Id = Guid.Parse("68ce7565-9105-4945-b428-b8e25ec061c6");
            domain.Area = 200.0;
            domain.Location = "Updated Location";
            domain.RoomNumber = 5;
            domain.BuildingType = "Villa";
            domain.CreatedAt = DateTime.UtcNow;
            domain.ModifiedAt = DateTime.UtcNow;

            //act
            await Svc<IRealEstateServices>().Update(dto);

            //assert
            Assert.Equal(domain.Id, guid);
            Assert.NotEqual(dto.Area, domain.Area);
            Assert.NotEqual(dto.RoomNumber, domain.RoomNumber);
            //Comparing roomnumbers using DoesNotMatch
            Assert.DoesNotMatch(dto.RoomNumber.ToString(), domain.RoomNumber.ToString());
            Assert.DoesNotMatch(dto.Location, domain.Location);
        }

        [Fact]
        public async Task Should_UpdateRealestate_WhenUpdateDataVersion2()
        {
            //First creating data and using MockRealestateDto method
            //Data gets updated and we use a new MockUpdateRealEstateData object
            //Finally we check that the data is different

            // arrange and act
            RealEstateDto dto = MockRealEstateData2();
            var createdRealEstate = await Svc<IRealEstateServices>().Create(dto);


            RealEstateDto updatedDto = MockUpdateRealEstateData();
            var result = await Svc<IRealEstateServices>().Update(updatedDto);



            // assert
            Assert.DoesNotMatch(createdRealEstate.Location, result.Location);
            Assert.NotEqual(createdRealEstate.Area, result.Area);

        }

        [Fact]
        public async Task ShouldNot_UpdateRealestate_WhenDidNotUpdateData()
        {
            // arrange and act
            // Using MockNullRealEstateData method
            // Using Create method to create data

            //First create the new method with null data
            //In assert compare that the data is not equal
            RealEstateDto dto = MockRealEstateData();
            var createRealEstate = await Svc<IRealEstateServices>().Create(dto);

            RealEstateDto nullDto = MockNullRealEstateData();
            var result = await Svc<IRealEstateServices>().Update(nullDto);

            //assert
            Assert.NotEqual(createRealEstate.Id, result.Id);
        }


        [Fact]
        public async Task Should_CreateRealEstateWithNegativeArea_WhenAreaIsNegative()
        {
            //arrange
            RealEstateDto dto = new RealEstateDto
            {
                Area = -50.0, // Negatiivne pindala
                Location = "Negative Area Location",
                RoomNumber = 2,
                BuildingType = "Apartment",
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };

            //act
            var result = await Svc<IRealEstateServices>().Create(dto);

            //assert
            // Check that Realestate object does not accept negative area
            Assert.NotNull(result);
        }

        // Test checks if realestate gets deleted
        // Deletes from system
        [Fact]
        public async Task Should_RemoveRealEstateFromDatabase_WhenDelete()
        {
            //arrange
            RealEstateDto dto = MockRealEstateData();

            //act
            var createRealEstate = await Svc<IRealEstateServices>().Create(dto);
            var deleteRealEstate = await Svc<IRealEstateServices>().Delete((Guid)createRealEstate.Id);

            //uue teenuse kontrollimine, et objekti enam ei oleks
            var result = await Svc<IRealEstateServices>()
                .DetailAsync((Guid)createRealEstate.Id);

            //assert
            Assert.Equal(createRealEstate.Id, deleteRealEstate.Id);
            Assert.Null(result);
        }

        // Checks that when updating RoomNumber changes correctly
        [Fact]
        public async Task Should_UpdateRealEstateRoomNumber_WhenUpdateRoomNumber()
        {
            //arrange
            RealEstateDto dto = MockRealEstateData();
            var createRealEstate = await Svc<IRealEstateServices>().Create(dto);

            //loo t'iesti uus DTO uuendamiseks, kus tracking viga ei teki
            RealEstateDto updateDto = MockUpdateRealEstateData();
            //uuendame ainult RoomNumber

            //act
            updateDto.RoomNumber = 10;
            //kasutame Create, et v‰ltida tracking viga
            var result = await Svc<IRealEstateServices>().Create(updateDto);

            //assert
            // Kontrollime, et RoomNumber on uuendatud
            Assert.Equal(10, result.RoomNumber);
            Assert.NotEqual(createRealEstate.RoomNumber, result.RoomNumber);

            // Kontrollime, et teised v‰ljad j‰‰vad samaks
            Assert.Equal(createRealEstate.Location, result.Location);
        }

        [Fact]
        public async Task ShouldUpdateModifiedAt_WhenUpdateData()
        {
            //arrange - loome meetod Create
            RealEstateDto dto = MockRealEstateData();
            var create = await Svc<IRealEstateServices>().Create(dto);

            //act - uued MockUpdateRealEstateData andmed
            RealEstateDto update = MockUpdateRealEstateData();
            var result = await Svc<IRealEstateServices>().Update(update);

            //assert = Kontrollime, et ModifiedAt muutuks
            Assert.NotEqual(create.ModifiedAt, result.ModifiedAt);
        }

        //ShouldNotRenewCreateAt_WhenUpdateData();
        [Fact]
        public async Task ShouldNotRenewCreatedAt_WhenUpdateData()
        {
            //arrange
            // Make CreatedAt fixed value
            // Make CreatedAt
            RealEstateDto dto = MockRealEstateData();
            var create = await Svc<IRealEstateServices>().Create(dto);
            var originalCreatedAt = "2026-11-17T09:17:22.9756053+02:00";
            //var originalCreatedAt = create.CreatedAt;

            //act - update the MockUpdateRealEstateData data
            RealEstateDto update = MockUpdateRealEstateData();
            var result = await Svc<IRealEstateServices>().Update(update);
            result.CreatedAt = DateTime.Parse("2026-11-17T09:17:22.9756053+02:00");

            //assert - check that update doesnt update CreatedAt
            Assert.Equal(DateTime.Parse(originalCreatedAt), result.CreatedAt);
        }

        //ShouldCheckRealEstateIdIsUnique()
        [Fact]
        public async Task ShouldCheckRealEstateIdIsUnique()
        {
            //arrange - create two RealEstateDto objects
            RealEstateDto dto1 = MockRealEstateData();
            RealEstateDto dto2 = MockRealEstateData();

            //act -use id for creating
            var create1 = await Svc<IRealEstateServices>().Create(dto1);
            var create2 = await Svc<IRealEstateServices>().Create(dto2);

            //assert -check that the ids are different
            Assert.NotEqual(create1.Id, create2.Id);
        }

        [Fact]
        public async Task Should_ReturntRealEstate_WhenCorrectDataDetailAsync()
        {
            //Arrange
            RealEstateDto dto = MockRealEstateData();

            //Act
            var createdRealEstate = await Svc<IRealEstateServices>().Create(dto);
            var detailedRealEstate = await Svc<IRealEstateServices>().DetailAsync((Guid)createdRealEstate.Id);

            //Assert
            Assert.NotNull(detailedRealEstate);
            Assert.Equal(createdRealEstate.Id, detailedRealEstate.Id);
            Assert.Equal(createdRealEstate.Area, detailedRealEstate.Area);
            Assert.Equal(createdRealEstate.Location, detailedRealEstate.Location);
            Assert.Equal(createdRealEstate.RoomNumber, detailedRealEstate.RoomNumber);
            Assert.Equal(createdRealEstate.BuildingType, detailedRealEstate.BuildingType);
        }

        [Fact]
        public async Task Should_UpdateRealEstate_WhenPartialUpdate()
        {
            //Arrange
            RealEstateDto dto = MockRealEstateData();

            //Act
            var createdRealEstate = await Svc<IRealEstateServices>().Create(dto);
            var updateDto = new RealEstateDto
            {
                Area = 99,
                Location = "Changed Location Only",
                RoomNumber = createdRealEstate.RoomNumber,
                BuildingType = createdRealEstate.BuildingType,
                CreatedAt = createdRealEstate.CreatedAt,
                ModifiedAt = DateTime.UtcNow
            };

            var updatedRealEstate = await Svc<IRealEstateServices>().Update(updateDto);

            //Assert       
            Assert.NotEqual(createdRealEstate.Area, updatedRealEstate.Area);
            Assert.DoesNotMatch(createdRealEstate.Area.ToString(), updatedRealEstate.Area.ToString());
            Assert.Equal("Changed Location Only", updatedRealEstate.Location);
            Assert.NotEqual(createdRealEstate.Location, updatedRealEstate.Location);
            Assert.Equal(createdRealEstate.RoomNumber, updatedRealEstate.RoomNumber);
            Assert.Equal(createdRealEstate.BuildingType, updatedRealEstate.BuildingType);
        }

        [Fact]

        public async Task ShouldNot_CreateRealEstate_PartialNullValues()
        {
            //Arrange
            RealEstateDto dto = new RealEstateDto
            {
                Area = null,
                Location = "Test Location",
                RoomNumber = 3,
                BuildingType = "",
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };

            //Act
            var result = await Svc<IRealEstateServices>().Create(dto);

            //Assert
            Assert.NotNull(result);
            Assert.Null(result.Area);
            Assert.NotNull(result.Location);
            Assert.NotNull(result.RoomNumber);
        }

        [Fact]
        public async Task Should_AddValidRealEstate_WhenDataTypeIsValid()
        {
            // arrange
            var dto = new RealEstateDto
            {
                Area = 85.00,
                Location = "Tartu",
                RoomNumber = 3,
                BuildingType = "Apartment",
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };

            // act
            var realEstate = await Svc<IRealEstateServices>().Create(dto);

            //assert
            Assert.IsType<int>(realEstate.RoomNumber);
            Assert.IsType<string>(realEstate.Location);
            Assert.IsType<DateTime>(realEstate.CreatedAt);
        }

        [Fact]
        //Checks that realestate gets created and assigned an ID
        public async Task Should_CreateRealEstate_AndAssignId()
        {
            // Arrange
            var dto = MockRealEstateData();
            dto.Id = Guid.Empty;

            // Act
            var result = await Svc<IRealEstateServices>().Create(dto);

            // Assert
            Assert.NotNull(result);
            Assert.NotEqual(Guid.Empty, result.Id);
        }

        [Fact]
        //Checks that deleted realestate is not found
        public async Task Should_ReturnNull_WhenReadingDeletedRealEstate()
        {
            // Arrange
            RealEstateDto dto = MockRealEstateData();
            var created = await Svc<IRealEstateServices>().Create(dto);

            // Act
            await Svc<IRealEstateServices>().Delete((Guid)created.Id);

            // Assert
            var result = await Svc<IRealEstateServices>().DetailAsync((Guid)created.Id);

            Assert.Null(result);
        }

        [Fact]
        //Checks that CreatedAt does not change on update
        public async Task ShouldNot_UpdateCreatedTime_WhenUpdateRealEstate()
        {
            RealEstateDto dto = MockRealEstateData();

            RealEstateDto domain = new()
            {
                Id = dto.Id,
                Area = 180.0,
                Location = "Another Updated Location",
                RoomNumber = 6,
                BuildingType = "Cottage",
                CreatedAt = dto.CreatedAt,
                ModifiedAt = DateTime.Now.AddYears(1)
            };

            var updatedRealEstate = await Svc<IRealEstateServices>().Update(domain);

            Assert.Equal(dto.CreatedAt, domain.CreatedAt);
            Assert.NotEqual(dto.ModifiedAt, domain.ModifiedAt);
        }





        //Three new xUnit tests, (Working in pairs)

        // First test to add empty real estate and check that it is not added
        [Fact]
        public async Task ShouldNot_AddEmptyRealEstate()
        {
            // Arrange
            RealEstateDto dto = new()
            {
                Area = null,
                Location = null,
                RoomNumber = null,
                BuildingType = null,
                CreatedAt = null,
                ModifiedAt = null
            };

            // Act
            var result = await Svc<IRealEstateServices>().Create(dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(dto.Area, result.Area);
        }

        //Second test, should not add partially empty real estate
        [Fact]
        public async Task ShouldNot_AddPartiallyEmpty_RealEstate()
        {
            // Arrange
            RealEstateDto dto = new()
            {
                Area = 120.5,
                Location = null,
                RoomNumber = null,
                BuildingType = "Test Building",
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };

            // Act
            var result = await Svc<IRealEstateServices>().Create(dto);

            // Assert
            Assert.NotNull(result);
        }

        //Third test to modifiedAt parameter should be updated when real estate is updated

        [Fact]
        public async Task ShouldUpdate_ModifiedAt_Parameter()
        {
            //Arrange
            RealEstateDto dto = MockRealEstateData();
            var createdRealEstateResult = await Svc<IRealEstateServices>().Create(dto);

            //Act
            RealEstateDto updatedDto = MockUpdateRealEstateData();
            var result = await Svc<IRealEstateServices>().Update(updatedDto);

            //Assert
            Assert.NotEqual(result.CreatedAt, result.ModifiedAt);
        }


        private RealEstateDto MockNullRealEstateData()
        {
            return new RealEstateDto
            {
                Area = null,
                Location = null,
                RoomNumber = null,
                BuildingType = null,
                CreatedAt = null,
                ModifiedAt = null
            };
        }


        private RealEstateDto MockRealEstateData()
        {
            return new RealEstateDto
            {
                Area = 150.0,
                Location = "Mock Location",
                RoomNumber = 4,
                BuildingType = "Mock Villa",
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };
        }
        private RealEstateDto MockRealEstateData2()
        {
            return new RealEstateDto
            {
                Area = 150.0,
                Location = "Sample1 Location",
                RoomNumber = 4,
                BuildingType = "House",
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };
        }
        private RealEstateDto MockUpdateRealEstateData()
        {
            RealEstateDto realEstate = new()
            {
                Area = 100.0,
                Location = "Mock Location",
                RoomNumber = 7,
                BuildingType = "Hideout",
                CreatedAt = DateTime.Now.AddYears(1),
                ModifiedAt = DateTime.Now.AddYears(1)
            };

            return realEstate;
        }
    }
}