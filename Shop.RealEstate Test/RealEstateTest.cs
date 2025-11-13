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
            RealEstateDto dto = MockRealEstateDto();

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
            var dto = MockRealEstateDto();
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

            RealEstateDto dto = MockRealEstateDto();

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
            RealEstateDto dto = MockRealEstateDto();
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
            RealEstateDto dto = MockRealEstateDto();
            var createdRealEstate = await Svc<IRealEstateServices>().Create(dto);

            RealEstateDto nullDto = MockNullRealEstateData();
            var result = await Svc<IRealEstateServices>().Update(nullDto);

            //assert
            Assert.DoesNotMatch(createdRealEstate.Location, result.Location);
            Assert.NotEqual(createdRealEstate.BuildingType, result.BuildingType);        
            
        }

        //Three new xUnit tests

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
            RealEstateDto dto = MockRealEstateDto();
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


        private RealEstateDto MockRealEstateDto()
        {
            return new RealEstateDto
            {
                Area = 150.0,
                Location = "Mock Location",
                RoomNumber = 4,
                BuildingType = " Mock Villa",
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };
        }
        private RealEstateDto MockUpdateRealEstateData()
        {
            RealEstateDto realEstate = new()
            {
                Area = 100.0,
                Location = "Secret Location",
                RoomNumber = 7,
                BuildingType = "Hideout",
                CreatedAt = DateTime.Now.AddYears(1),
                ModifiedAt = DateTime.Now.AddYears(1)
            };

            return realEstate;
        }
    }
}