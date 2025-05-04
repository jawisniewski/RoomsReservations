using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using RoomReservation.Infrastructure.Context;
using RoomReservation.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservation.Tests.InfrastructureTests.Fixtures
{
    public class ReservationRepositoryTestsFixtures
    {
        public Mock<ILogger<ReservationRepository>> LoggerMock { get; } = new();

        public ReservationRepository CreateRepository()
        {
            return new ReservationRepository(
                CreateInMemoryContext(),
                LoggerMock.Object
            );
        }
        private AppDbContext CreateInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new AppDbContext(options);
        }
    }
}
