using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using RoomReservation.Application.DTOs.Equipment;
using RoomReservation.Application.DTOs.Reservation;
using RoomReservation.Application.DTOs.Reservation.CreateReservation;
using RoomReservation.Application.DTOs.Reservation.UpdateReservation;
using RoomReservation.Application.DTOs.Room;
using RoomReservation.Application.DTOs.Room.CreateRoom;
using RoomReservation.Application.Enums;
using RoomReservation.Application.Interfaces.Repositories;
using RoomReservation.Application.Interfaces.Services;
using RoomReservation.Application.Services;
using RoomReservation.Domain.Entities;
using RoomReservation.Domain.Enums;
using RoomReservation.Shared.Common;
using RoomReservation.Tests.ApplicationTests.Configurator;
using RoomReservation.Tests.ApplicationTests.Configurators;
using RoomReservation.Tests.ApplicationTests.Fixtures;
using Shouldly;

namespace RoomReservation.Tests;

public class RoomServiceTest
{
    private RoomService _service;
    private RoomSerivceTestsFixtures _fixture = null!;
    private RoomServiceMockConfigurator _roomServiceMockConfigurator = null!;

    [SetUp]
    public void Setup()
    {
        _fixture = new RoomSerivceTestsFixtures();
        _service = _fixture.CreateService();
        _roomServiceMockConfigurator = new RoomServiceMockConfigurator();
    }

    [Test]
    public async Task CreateAsync_ShouldReturnSuccessAndRoom_WhenRoomIsCreated()
    {
        var createRoomRequest = new CreateRoomRequest()
        {
            Name = "Room 1",
            Capacity = 10,
            RoomEquipments = [
                new()
                {
                    Quantity = 1,
                    EquipmentType = EquipmentTypeEnum.Projector
                }
            ],
            RoomLayout = RoomLayoutEnum.Boardroom,
            TableCount = 1,
            RoomReservationLimit = new RoomReservationLimitDto()
            {
                MaxTime = 60,
                MinTime = 30
            }

        };
        var room = new Room()
        {
            Id = 1,
            Name = "Room 1",
            Capacity = 10,
            RoomEquipments = [
                new()
                { Id =1,
                    EquipmentId = 1,
                    Quantity = 1,
                    RoomId=1
                }
            ],
            RoomLayout = RoomLayoutEnum.Boardroom,
            TableCount = 1,
            RoomReservationLimit = new RoomReservationLimit()
            {
                Id = 1,
                RoomId = 1,
                MaxTime = 60,
                MinTime = 30
            }
        };
        var roomDto = new RoomDto()
        {
            Id = 1,
            Name = "Room 1",
            Capacity = 10,
            RoomEquipments = [
                new()
                {
                    Id =1,
                    EquipmentType = EquipmentTypeEnum.Projector,
                    Quantity = 1,
                }
            ],
            RoomLayout = RoomLayoutEnum.Boardroom,
            TableCount = 1,
            RoomReservationLimit = new RoomReservationLimitDto()
            {
                MaxTime = 60,
                MinTime = 30
            }
        };


        _roomServiceMockConfigurator.SetupCreateRoomSuccessful(_fixture, createRoomRequest, room, roomDto);

        var result = await _service.CreateAsync(createRoomRequest);

        result.ShouldNotBeNull();
        result.IsSuccess.ShouldBeTrue();
        result.Data.ShouldNotBeNull();
        result.Data.Id.ShouldBe(1);
        result.FailureMessage.ShouldBeEmpty();
    }

    [Test]
    public async Task CreateAsync_ShouldReturnUnprocessableEntity_WhenCreateError()
    {
        var createRoomRequest = new CreateRoomRequest()
        {
            Name = "Room 1",
            Capacity = 10,
            RoomEquipments = [
               new()
                {
                    Quantity = 1,
                    EquipmentType = EquipmentTypeEnum.Projector
                }
           ],
            RoomLayout = RoomLayoutEnum.Boardroom,
            TableCount = 1,
            RoomReservationLimit = new RoomReservationLimitDto()
            {
                MaxTime = 60,
                MinTime = 30
            }

        };
        var room = new Room()
        {
            Id = 1,
            Name = "Room 1",
            Capacity = 10,
            RoomEquipments = [
                new()
                { Id =1,
                    EquipmentId = 1,
                    Quantity = 1,
                    RoomId=1
                }
            ],
            RoomLayout = RoomLayoutEnum.Boardroom,
            TableCount = 1,
            RoomReservationLimit = new RoomReservationLimit()
            {
                Id = 1,
                RoomId = 1,
                MaxTime = 60,
                MinTime = 30
            }
        };
        var roomDto = new RoomDto()
        {
            Id = 1,
            Name = "Room 1",
            Capacity = 10,
            RoomEquipments = [
                new()
                {
                    Id =1,
                    EquipmentType = EquipmentTypeEnum.Projector,
                    Quantity = 1,
                }
            ],
            RoomLayout = RoomLayoutEnum.Boardroom,
            TableCount = 1,
            RoomReservationLimit = new RoomReservationLimitDto()
            {
                MaxTime = 60,
                MinTime = 30
            }
        };

        _roomServiceMockConfigurator.SetupCreateRoomFailure(_fixture, createRoomRequest, room);

        var result = await _service.CreateAsync(createRoomRequest);

        result.ShouldNotBeNull();
        result.IsSuccess.ShouldBeFalse();
        result.FailureMessage.ShouldBe("Create room failure");
        result.StatusCode.ShouldBe(System.Net.HttpStatusCode.UnprocessableEntity);
    }

    [Test]
    public async Task DeleteAsync_ShouldReturnSuccess_WhenRoomDeleted()
    {
        var roomId = 1;

        _roomServiceMockConfigurator.SetupDeleteRoomSuccessful(_fixture, roomId);

        var result = await _service.DeleteAsync(roomId);

        result.ShouldNotBeNull();
        result.IsSuccess.ShouldBeTrue();
        result.FailureMessage.ShouldBeEmpty();
        result.StatusCode.ShouldBe(System.Net.HttpStatusCode.OK);
    }

    [Test]
    public async Task DeleteAsync_ShouldReturnUnprocessableEntity_WhenDeleteError()
    {
        var roomId = 1;

        _roomServiceMockConfigurator.SetupDeleteRoomFailure(_fixture, roomId);

        var result = await _service.DeleteAsync(roomId);

        result.ShouldNotBeNull();
        result.IsSuccess.ShouldBeFalse();
        result.FailureMessage.ShouldBe("Delete room failure");
        result.StatusCode.ShouldBe(System.Net.HttpStatusCode.UnprocessableEntity);
    }

    [Test]
    public async Task UpdateAsync_ShouldReturnSuccessAndRoom_WhenRoomIsUpdated()
    {
        var roomDto = new RoomDto()
        {
            Id = 1,
            Name = "Room 1",
            Capacity = 10,
            RoomEquipments = [
                new()
                {
                    Quantity = 1,
                    EquipmentType = EquipmentTypeEnum.Projector
                }
            ],
            RoomLayout = RoomLayoutEnum.Boardroom,
            TableCount = 1,
            RoomReservationLimit = new RoomReservationLimitDto()
            {
                MaxTime = 60,
                MinTime = 30
            }

        };
        var room = new Room()
        {
            Id = 1,
            Name = "Room 1",
            Capacity = 10,
            RoomEquipments = [
                new()
                {
                    Id = 1,
                    EquipmentId = 1,
                    Quantity = 1,
                    RoomId=1
                }
            ],
            RoomLayout = RoomLayoutEnum.Boardroom,
            TableCount = 1,
            RoomReservationLimit = new RoomReservationLimit()
            {
                Id = 1,
                RoomId = 1,
                MaxTime = 60,
                MinTime = 30
            }
        };

        _roomServiceMockConfigurator.SetupUpdateRoomSuccessful(_fixture, roomDto, room);

        var result = await _service.UpdateAsync(roomDto);

        result.ShouldNotBeNull();
        result.IsSuccess.ShouldBeTrue();
        result.Data.ShouldNotBeNull();
        result.Data.Id.ShouldBe(1);
        result.FailureMessage.ShouldBeEmpty();
    }

    [Test]
    public async Task UpdateAsync_ShouldReturnUnprocessableEntity_WhenUpdateError()
    {
        var room = new Room()
        {
            Id = 1,
            Name = "Room 1",
            Capacity = 10,
            RoomEquipments = [
                new()
                { Id =1,
                    EquipmentId = 1,
                    Quantity = 1,
                    RoomId=1
                }
            ],
            RoomLayout = RoomLayoutEnum.Boardroom,
            TableCount = 1,
            RoomReservationLimit = new RoomReservationLimit()
            {
                Id = 1,
                RoomId = 1,
                MaxTime = 60,
                MinTime = 30
            }
        };
        var roomDto = new RoomDto()
        {
            Id = 1,
            Name = "Room 1",
            Capacity = 10,
            RoomEquipments = [
                new()
                {
                    Id =1,
                    EquipmentType = EquipmentTypeEnum.Projector,
                    Quantity = 1,
                }
            ],
            RoomLayout = RoomLayoutEnum.Boardroom,
            TableCount = 1,
            RoomReservationLimit = new RoomReservationLimitDto()
            {
                MaxTime = 60,
                MinTime = 30
            }
        };

        _roomServiceMockConfigurator.SetupUpdateRoomFailure(_fixture, roomDto, room);

        var result = await _service.UpdateAsync(roomDto);

        result.ShouldNotBeNull();
        result.IsSuccess.ShouldBeFalse();
        result.FailureMessage.ShouldBe("Update room failure");
        result.StatusCode.ShouldBe(System.Net.HttpStatusCode.UnprocessableEntity);
    }

    [Test]
    public async Task GetListAsync_ShouldReturnSuccess()
    {
        var roomFilters = new RoomFilter()
        {
            Name = "Room 1",
            Capacity = 10,
            RoomLayout = RoomLayoutEnum.Boardroom,
            TableCount = 1
        };
        var rooms = new List<Room>()
        {
            new Room()
            {
                Id = 1,
                Name = "Room 1",
                Capacity = 10,
                RoomEquipments = [
                    new()
                    {
                        Id =1,
                        EquipmentId = 1,
                        Quantity = 1,
                        RoomId=1
                    }
                ],
                RoomLayout = RoomLayoutEnum.Boardroom,
                TableCount = 1,
                RoomReservationLimit = new RoomReservationLimit()
                {
                    Id = 1,
                    RoomId = 1,
                    MaxTime = 60,
                    MinTime = 30
                }
            }
        };
        var roomsDto = new List<RoomDto>() 
        {
            new RoomDto()
            {
                Id = 1,
                Name = "Room 1",
                Capacity = 10,
                RoomEquipments = [
                    new()
                    {
                        Id =1,
                        EquipmentType = EquipmentTypeEnum.Projector,
                        Quantity = 1,
                    }
                ],
                RoomLayout = RoomLayoutEnum.Boardroom,
                TableCount = 1,
                RoomReservationLimit = new RoomReservationLimitDto()
                {
                    MaxTime = 60,
                    MinTime = 30
                }
            }
        };
        _roomServiceMockConfigurator.SetupGetRoomListSuccessful(_fixture, rooms, roomsDto, roomFilters);

        var result = await _service.GetListAsync(roomFilters);

        result.ShouldNotBeNull();
        result.IsSuccess.ShouldBeTrue();
        result.FailureMessage.ShouldBeEmpty();
        result.StatusCode.ShouldBe(System.Net.HttpStatusCode.OK);
        result.Data.ShouldNotBeNull();
        result.Data.Count.ShouldBe(1);
    }

    [Test]
    public async Task GetListAsync_ShouldReturnFailure()
    {
        var roomFilters = new RoomFilter()
        {
            Name = "Room 1",
            Capacity = 10,
            RoomLayout = RoomLayoutEnum.Boardroom,
            TableCount = 1
        };
        _roomServiceMockConfigurator.SetupGetRoomListFailure(_fixture, roomFilters);

        var result = await _service.GetListAsync(roomFilters);

        result.ShouldNotBeNull();
        result.IsSuccess.ShouldBeFalse();
        result.FailureMessage.ShouldBe("Get room list failure");
        result.StatusCode.ShouldBe(System.Net.HttpStatusCode.UnprocessableEntity);
    }
    [Test]
    public async Task GetAvailibiltyRoomsAsync_ShouldReturnSuccess()
    {
        var roomFilters = new RoomAvalibilityRequest()
        {
            AvailableFrom = DateTime.Now,
            AvailableTo = DateTime.Now.AddHours(1),
            Name = "Room 1",
            Capacity = 10,
            RoomLayout = RoomLayoutEnum.Boardroom,
            TableCount = 1
        };
        var rooms = new List<Room>()
        {
            new Room()
            {
                Id = 1,
                Name = "Room 1",
                Capacity = 10,
                RoomEquipments = [
                    new()
                    {
                        Id =1,
                        EquipmentId = 1,
                        Quantity = 1,
                        RoomId=1
                    }
                ],
                RoomLayout = RoomLayoutEnum.Boardroom,
                TableCount = 1,
                RoomReservationLimit = new RoomReservationLimit()
                {
                    Id = 1,
                    RoomId = 1,
                    MaxTime = 60,
                    MinTime = 30
                }
            }
        };
        var roomsDto = new List<RoomDto>()
        {
            new RoomDto()
            {
                Id = 1,
                Name = "Room 1",
                Capacity = 10,
                RoomEquipments = [
                    new()
                    {
                        Id =1,
                        EquipmentType = EquipmentTypeEnum.Projector,
                        Quantity = 1,
                    }
                ],
                RoomLayout = RoomLayoutEnum.Boardroom,
                TableCount = 1,
                RoomReservationLimit = new RoomReservationLimitDto()
                {
                    MaxTime = 60,
                    MinTime = 30
                }
            }
        };
        _roomServiceMockConfigurator.SetupGetAvailbityRoomsSuccessful(_fixture, rooms, roomsDto, roomFilters);

        var result = await _service.GetAvalibilityRoomAsync(roomFilters);

        result.ShouldNotBeNull();
        result.IsSuccess.ShouldBeTrue();
        result.FailureMessage.ShouldBeEmpty();
        result.StatusCode.ShouldBe(System.Net.HttpStatusCode.OK);
        result.Data.ShouldNotBeNull();
        result.Data.Count.ShouldBe(1);
    }

    [Test]
    public async Task GetAvailibiltyRoomsAsync_ShouldReturnFailure()
    {
        var roomFilters = new RoomAvalibilityRequest()
        {
            Name = "Room 1",
            Capacity = 10,
            RoomLayout = RoomLayoutEnum.Boardroom,
            TableCount = 1
        };
        _roomServiceMockConfigurator.SetupAvailibityRoomsFailure(_fixture, roomFilters);

        var result = await _service.GetAvalibilityRoomAsync(roomFilters);

        result.ShouldNotBeNull();
        result.IsSuccess.ShouldBeFalse();
        result.FailureMessage.ShouldBe("Get room list failure");
        result.StatusCode.ShouldBe(System.Net.HttpStatusCode.UnprocessableEntity);
    }

}
