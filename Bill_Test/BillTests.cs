global using Diplom_Project;
using Diplom_Project.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Options;
using Moq;
using Moq.EntityFrameworkCore;
using MockQueryable.Moq;
using System.Text.Json;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;

namespace Bill_Test
{
    public class BillTests
    {
        [Fact]
        public async Task AddBill_SuccessAsync()
        {
            // Arrange
            var billServiceMock = new Mock<IBillService>();
            var loggereMock = new Mock<ILogger<BillController>>();

            Bill savedBill = null!;
            var newBill = new Bill
            {
                Id = 1,
                Name = "Test Bill",
                Total = 100.0,
                Members = new List<Member>
                {
                    new Member
                    {
                        Id = 1,
                        FirstName = "John",
                        LastName = "Doe",
                        AmountPaid = 50.0
                    },
                    new Member
                    {
                        Id = 2,
                        FirstName = "Jane",
                        LastName = "Smith",
                        AmountPaid = 50.0
                    }
                }
            };
            //billServiceMock.Setup(service => service.CreateBill(newBill)).ReturnsAsync(newBill);
            billServiceMock.Setup(service => service.CreateBill(It.IsAny<Bill>())).Callback((Bill bill) => savedBill = bill);

            var billService = new BillController(billServiceMock.Object, loggereMock.Object);

            // Act
            await billService.CreateBill(newBill);

            // Assert
            billServiceMock.Verify(x => x.CreateBill(It.IsAny<Bill>()), Times.Once());
            Assert.Same(newBill, savedBill);
            Assert.NotNull(savedBill);
        }

        [Fact]
        public async Task AddBill_EmptyName_NotSavedAsync()
        {
            var billServiceMock = new Mock<IBillService>();
            var loggereMock = new Mock<ILogger<BillController>>();

            Bill savedBill = null!;
            //billServiceMock.Setup(service => service.CreateBill(savedBill)).ReturnsAsync(savedBill);
            billServiceMock.Setup(service => service.CreateBill(It.IsAny<Bill>())).Callback((Bill bill) => savedBill = bill);
            var billService = new BillController(billServiceMock.Object, loggereMock.Object);

            var newBill = new Bill
            {
                Name = "",
                Total = 0,
                Members = null!
            };

            // Act
            await billService.CreateBill(newBill);

            // Assert
            billServiceMock.Verify(x => x.CreateBill(It.IsAny<Bill>()), Times.Never());
            Assert.Null(savedBill);
        }

        [Fact]
        public async Task GetAllBills_SuccessAsync()
        {
            // Arrange
            var billServiceMock = new Mock<IBillService>();
            var loggereMock = new Mock<ILogger<BillController>>();
            billServiceMock.Setup(x => x.GetAllBills()).ReturnsAsync(TestDataHelper.GetFakeBillList);

            // Act
            var billService = new BillController(billServiceMock.Object, loggereMock.Object);
            var createdBill = await billService.GetAllBills();

            // Assert
            billServiceMock.Verify(x => x.GetAllBills(), Times.Once());
            Assert.NotNull(createdBill);
            Assert.Equal(2, createdBill.Value!.Count());

            var bill1 = createdBill.Value!.FirstOrDefault(b => b.Id == 1);
            Assert.NotNull(bill1);
            Assert.Equal("Test Bill", bill1.Name);
            Assert.Equal(2, bill1.Members.Count);

            var bill2 = createdBill.Value!.FirstOrDefault(b => b.Id == 2);
            Assert.NotNull(bill2);
            Assert.Equal("Test Bill 2", bill2.Name);
            Assert.Single(bill2.Members);
        }
    }
}
