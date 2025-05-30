using LibraryManagementSystem.Domain.DomainObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.UnitTest.Fake
{
    public class LibraryDBContextFake : LibraryDBContext
    {
        public LibraryDBContextFake() : base(new DbContextOptionsBuilder<LibraryDBContext>()
        .UseInMemoryDatabase(databaseName: $"AppointmentBookingTest-{Guid.NewGuid()}")
        .Options)
        { }
    }
}
