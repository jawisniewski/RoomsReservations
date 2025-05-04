using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using RoomReservation.Application.Interfaces.Repositories;
using RoomReservation.Application.Services;
using RoomReservation.Infrastructure.Context;
using RoomReservation.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservation.Tests.InfrastructureTests.Fixtures
{
    public class RoomRepositoryTestsFixtures
    {
        public Mock<ILogger<RoomRepository>> LoggerMock { get; } = new();

        public RoomRepository CreateRepository()
        {
            return new RoomRepository(
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
