global using Diplom_Project;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Options;
using Moq;

namespace Bill_Test
{
    public class BillTests
    {
        [Fact]
        public async Task AddBillToDB_SuccessAsync()
        {
            // Arrange
            var billServiceMock = new Mock<IBillService>();
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
            billServiceMock.Setup(service => service.CreateBill(It.IsAny<Bill>())).Callback((Bill bill) => newBill = bill);

            var billService = billServiceMock.Object;

            // Act
            var createdBill = await billService.CreateBill(newBill);

            // Assert
            billServiceMock.Verify(x => x.CreateBill(It.IsAny<Bill>()), Times.Once());
            Assert.Same(newBill, createdBill);
        }

        [Fact]
        public async Task AddBillToDB_EmptyName_NotSavedAsync()
        {
            // Arrange
            var billServiceMock = new Mock<IBillService>();

            //Bill bill = null;

            //billServiceMock.Setup(service => service.CreateBill(It.IsAny<Bill>()))
            //       .ReturnsAsync((Bill bill) => bill);

            //billServiceMock.Setup(service => service.CreateBill(It.IsAny<Bill>())).Callback((Bill bill) => bill = bill);
            //var billService = billServiceMock.Object;

            var newBill = new Bill
            {
                Id = 1,
                Name = "",
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
            billServiceMock.Setup(service => service.CreateBill(It.IsAny<Bill>())).Callback((Bill bill) => newBill = bill);

            var billService = billServiceMock.Object;

            // Act
            var createdBill = await billService.CreateBill(newBill);

            // Assert
            billServiceMock.Verify(x => x.CreateBill(It.IsAny<Bill>()), Times.Never());
            Assert.Null(createdBill);
        }
    }
}
