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
    public class ReservationServiceTestsFixtures
    {
            public Mock<IMapper> MapperMock { get; } = new();
            public Mock<IRoomRepository> RoomRepositoryMock { get; } = new();
            public Mock<IReservationRepository> ReservationRepositoryMock { get; } = new();
            public Mock<ILogger<ReservationService>> LoggerMock { get; } = new();

            public ReservationService CreateService()
            {
                return new ReservationService(
                    MapperMock.Object,
                    ReservationRepositoryMock.Object,
                    RoomRepositoryMock.Object,
                    LoggerMock.Object
                );
            }        
    }
}
