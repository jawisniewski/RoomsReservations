using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using RoomReservation.Application.Interfaces.Repositories;
using RoomReservation.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservation.Tests.ApplicationTests.Fixtures
{
    public class RoomSerivceTestsFixtures
    {
        public Mock<IMapper> MapperMock { get; } = new();
        public Mock<IRoomRepository> RoomRepositoryMock { get; } = new();
        public Mock<ILogger<RoomService>> LoggerMock { get; } = new();

        public RoomService CreateService()
        {
            return new RoomService(
                LoggerMock.Object,
                RoomRepositoryMock.Object,
                MapperMock.Object
            );
        }
    }
}
