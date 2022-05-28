using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudCustomers.Models;

namespace CloudCustomers.UnitTests.Fixtures
{
    public static class UsersFixture
    {
        public static List<User> GetTestUsers()
        {
            return new ()
            {
                new () { Id = 1, Name = "User 1", Email = "user1@example.com", Address =new Address { City = "Pune", Street = "Dhayari", ZipCode = "411041" } },
                new () { Id = 2, Name = "User 2", Email = "user2@example.com", Address =new Address { City = "Pune 1", Street = "Dhayari 1", ZipCode = "411042" } },
                new () { Id = 3, Name = "User 3", Email = "user3@example.com", Address =new Address { City = "Pune 2", Street = "Dhayari 2", ZipCode = "411043" } },
                new () { Id = 4, Name = "User 4", Email = "user4@example.com", Address =new Address { City = "Pune 3", Street = "Dhayari 3", ZipCode = "411044" } },
                new () { Id = 5, Name = "User 5", Email = "user5@example.com", Address =new Address { City = "Pune 4", Street = "Dhayari 4", ZipCode = "411045" } },
                new () { Id = 6, Name = "User 6", Email = "user6@example.com", Address =new Address { City = "Pune 5", Street = "Dhayari 5", ZipCode = "411046" } },
                new () { Id = 7, Name = "User 7", Email = "user7@example.com", Address =new Address { City = "Pune 6", Street = "Dhayari 6", ZipCode = "411047" } },
            };
        }
    }
}
