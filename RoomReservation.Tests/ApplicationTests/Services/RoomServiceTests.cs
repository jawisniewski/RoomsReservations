using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using RoomReservation.Application.DTOs.Equipment;
using RoomReservation.Application.DTOs.Reservation;
using RoomReservation.Application.DTOs.Reservation.CreateReservation;
using RoomReservation.Application.DTOs.Reservation.UpdateReservation;
using RoomReservation.Application.DTOs.Room;
using RoomReservation.Application.DTOs.Room.CreateRoom;
using RoomReservation.Application.Interfaces.Repositories;
using RoomReservation.Application.Interfaces.Services;
using RoomReservation.Application.Services;
using RoomReservation.Domain.Entities;
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
    private RoomServiceMockConfigurator _reservetionServiceMockConfigurator = null!;

    [SetUp]
    public void Setup()
    {
        _fixture = new RoomSerivceTestsFixtures();
        _service = _fixture.CreateService();
        _reservetionServiceMockConfigurator = new RoomServiceMockConfigurator();
    }

    [Test]
    public async Task CreateAsync_ShouldReturnSucess_And_Roomn_WhenRoomIsCreated()
    {
        var roomDto = new CreateRoomRequest()
        {
            Name = "Room 1",
            Capacity = 10,
            RoomEquipments = new List<CreateRoomEquipmentRequest>(),
            RoomLayout = Domain.Enums.RoomLayoutEnum.Boardroom,
            TableCount = 1,
            RoomReservationLimit = new RoomReservationLimitDto()
            {
                MaxTime = 60,
                MinTime = 30
            }

        };
        var reservationResult = Result<Reservation>.Success(reservation);
        var rservationResultDto = Result<ReservationDto>.Success(reservationDto);

        _reservetionServiceMockConfigurator.SetupCreateSuccessfulReservation(_fixture, createReservationRequest, userId, reservationRequest, reservationResult, rservationResultDto);

        var result = await _service.CreateAsync(roomDto);

        result.ShouldNotBeNull();
        result.IsSuccess.ShouldBeTrue();
        result.Data.ShouldNotBeNull();
        result.Data.Id.ShouldBe(1);
        result.FailureMessage.ShouldBeEmpty();
    }

    [Test]
    public async Task CreateAsync_ShouldReturnUnprocessableEntity_WhenRoomIsUnavailable()
    {
        var createReservationRequest = new CreateReservationRequest
        {
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddMinutes(60),
            RoomId = 3
        };
        var userId = 1;
        var reservationRequest = new Reservation()
        {
            Id = 0,
            EndDate = createReservationRequest.EndDate,
            StartDate = createReservationRequest.StartDate,
            RoomId = createReservationRequest.RoomId,
            UserId = userId
        };

        _reservetionServiceMockConfigurator.SetupUnprocessablyResultWhenRoomUnavailable(_fixture, createReservationRequest, userId, reservationRequest);

        var result = await _service.CreateAsync(createReservationRequest, userId);

        result.ShouldNotBeNull();
        result.IsSuccess.ShouldBeFalse();
        result.FailureMessage.ShouldBe("Room is unavailable");
        result.StatusCode.ShouldBe(System.Net.HttpStatusCode.UnprocessableEntity);
    }

    [Test]
    public async Task CreateAsync_ShouldReturnConflict_WhenUserIsNotAllowedToChangeReservation()
    {
        var createReservationRequest = new CreateReservationRequest
        {
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddMinutes(60),
            RoomId = 3
        };
        var userId = 1;
        var reservationRequest = new Reservation()
        {
            Id = 0,
            EndDate = createReservationRequest.EndDate,
            StartDate = createReservationRequest.StartDate,
            RoomId = createReservationRequest.RoomId,
            UserId = userId
        };

        _reservetionServiceMockConfigurator.SetupUnprocessablyResultWhenUserHasAReservation(_fixture, createReservationRequest, userId, reservationRequest);

        var result = await _service.CreateAsync(createReservationRequest, userId);

        result.ShouldNotBeNull();
        result.IsSuccess.ShouldBeFalse();
        result.FailureMessage.ShouldBe($"User {userId} has already a reservation");
        result.StatusCode.ShouldBe(System.Net.HttpStatusCode.Conflict);
    }

    [Test]
    public async Task UpdateAsync_ShouldReturnSucess_And_Reservation_WhenReservationIsUpdated()
    {
        var updateReservationRequest = new UpdateReservationRequest
        {
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddMinutes(60),
            RoomId = 1,
            Id = 1
        };
        var userId = 1;
        var reservationRequest = new Reservation()
        {
            Id = 1,
            EndDate = updateReservationRequest.EndDate,
            StartDate = updateReservationRequest.StartDate,
            RoomId = updateReservationRequest.RoomId,
            UserId = userId
        };
        var reservation = new Reservation()
        {
            Id = 1,
            EndDate = updateReservationRequest.EndDate,
            StartDate = updateReservationRequest.StartDate,
            RoomId = updateReservationRequest.RoomId,
            UserId = userId
        };
        var reservationDto = new ReservationDto()
        {
            Id = 1,
            EndDate = updateReservationRequest.EndDate,
            StartDate = updateReservationRequest.StartDate,
            RoomId = updateReservationRequest.RoomId,
            UserId = userId
        };

        var reservationResult = Result<Reservation>.Success(reservation);
        var rservationResultDto = Result<ReservationDto>.Success(reservationDto);

        _reservetionServiceMockConfigurator.SetupUpdateSuccessfulReservation(_fixture, updateReservationRequest, userId, reservationRequest, reservationResult, rservationResultDto);

        var result = (Result<ReservationDto>)await _service.UpdateAsync(updateReservationRequest, userId);

        result.ShouldNotBeNull();
        result.IsSuccess.ShouldBeTrue();
        result.Data.ShouldNotBeNull();
        result.Data.Id.ShouldBe(1);
        result.FailureMessage.ShouldBeEmpty();
    }

    [Test]
    public async Task UpdateAsync_ShouldReturnUnprocessableEntity_WhenRoomIsUnavailable()
    {
        var updateReservationRequest = new UpdateReservationRequest
        {
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddMinutes(60),
            RoomId = 3,
            Id = 1
        };
        var userId = 1;
        var reservationRequest = new Reservation()
        {
            Id = 0,
            EndDate = updateReservationRequest.EndDate,
            StartDate = updateReservationRequest.StartDate,
            RoomId = updateReservationRequest.RoomId,
            UserId = userId
        };

        _reservetionServiceMockConfigurator.SetupUnprocessablyResultWhenRoomUnavailable(_fixture, updateReservationRequest, userId, reservationRequest, updateReservationRequest.Id);

        var result = await _service.UpdateAsync(updateReservationRequest, userId);

        result.ShouldNotBeNull();
        result.IsSuccess.ShouldBeFalse();
        result.FailureMessage.ShouldBe("Room is unavailable");
        result.StatusCode.ShouldBe(System.Net.HttpStatusCode.UnprocessableEntity);
    }

    [Test]
    public async Task UpdateAsync_ShouldReturnConflict_WhenUserIsNotAllowedToChangeReservation()
    {
        var updateReservationRequest = new UpdateReservationRequest
        {
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddMinutes(60),
            RoomId = 3,
            Id = 1
        };
        var userId = 1;
        var reservationRequest = new Reservation()
        {
            Id = 0,
            EndDate = updateReservationRequest.EndDate,
            StartDate = updateReservationRequest.StartDate,
            RoomId = updateReservationRequest.RoomId,
            UserId = userId
        };

        _reservetionServiceMockConfigurator.SetupUnprocessablyResultWhenUserHasAReservation(_fixture, updateReservationRequest, userId, reservationRequest, updateReservationRequest.Id);

        var result = await _service.UpdateAsync(updateReservationRequest, userId);

        result.ShouldNotBeNull();
        result.IsSuccess.ShouldBeFalse();
        result.FailureMessage.ShouldBe($"User {userId} has already a reservation");
        result.StatusCode.ShouldBe(System.Net.HttpStatusCode.Conflict);
    }

    [Test]
    public async Task DeleteAsync_ShouldReturnSuccess_WhenReservationIsDeleted()
    {
        var reservationId = 1;
        var userId = 1;

        _reservetionServiceMockConfigurator.SetupDeleteSuccessfulReservation(_fixture, reservationId, userId);

        var result = await _service.DeleteAsync(reservationId, userId);

        result.ShouldNotBeNull();
        result.IsSuccess.ShouldBeTrue();
        result.FailureMessage.ShouldBeEmpty();
    }

    [Test]
    public async Task DeleteAsync_ShouldReturnBadRequest_WhenRepositoryCantDeleteUser()
    {
        var reservationId = 1;
        var userId = 1;

        _reservetionServiceMockConfigurator.SetupDeleteBadRequestReservation(_fixture, reservationId, userId);

        var result = await _service.DeleteAsync(reservationId, userId);

        result.ShouldNotBeNull();
        result.IsSuccess.ShouldBeFalse();
        result.FailureMessage.ShouldBe("Delete room failure");
    }

    [Test]
    public async Task GetListAsync_ShouldReturnList()
    {
        var reservations = new List<Reservation>()
        {
            new Reservation()
            {
                EndDate = DateTime.Now.AddMinutes(60),
                StartDate = DateTime.Now,
                RoomId = 1,
                UserId = 1,
                Id = 1
            },
            new Reservation()
            {
                EndDate = DateTime.Now.AddMinutes(90),
                StartDate = DateTime.Now,
                RoomId = 2,
                UserId = 2,
                Id = 2
            }
        }; var reservationsDto = new List<ReservationDto>()
        {
            new ReservationDto()
            {
                EndDate = DateTime.Now.AddMinutes(60),
                StartDate = DateTime.Now,
                RoomId = 1,
                UserId = 1,
                Id = 1
            },
            new ReservationDto()
            {
                EndDate = DateTime.Now.AddMinutes(90),
                StartDate = DateTime.Now,
                RoomId = 2,
                UserId = 2,
                Id = 2
            }
        };

        var reservationsResult = Result<List<Reservation>>.Success(reservations);
        var rservationsResultDto = Result<List<ReservationDto>>.Success(reservationsDto);
        _reservetionServiceMockConfigurator.SetupGetReservationsSuccessful(_fixture, reservationsResult, rservationsResultDto);

        var result = await _service.GetListAsync();

        result.ShouldNotBeNull();
        result.IsSuccess.ShouldBeTrue();
        result.FailureMessage.ShouldBeEmpty();
        result.Data.ShouldNotBeNull();
        result.Data.Count.ShouldBe(2);
    }
}
