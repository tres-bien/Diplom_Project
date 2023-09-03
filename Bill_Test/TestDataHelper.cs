using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bill_Test
{
    internal class TestDataHelper
    {
        public static Bill GetFakeBill()
        {
            return new Bill
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
        }
        public static List<Bill> GetFakeBillList()
        {
            return new List<Bill>
            {
                new Bill
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
                },
                new Bill
                {
                    Id = 2,
                    Name = "Test Bill 2",
                    Total = 75.0,
                    Members = new List<Member>
                    {
                        new Member
                        {
                            Id = 3,
                            FirstName = "Alice",
                            LastName = "Johnson",
                            AmountPaid = 25.0
                        }
                    }
                }
            };
        }
    }
}
