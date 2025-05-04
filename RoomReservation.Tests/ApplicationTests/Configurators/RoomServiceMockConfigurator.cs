using RoomReservation.Application.DTOs.Reservation.CreateReservation;
using RoomReservation.Application.DTOs.Reservation;
using RoomReservation.Domain.Entities;
using RoomReservation.Shared.Common;
using RoomReservation.Tests.ApplicationTests.Fixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoomReservation.Application.DTOs.Room.CreateRoom;
using RoomReservation.Application.DTOs.Room;

namespace RoomReservation.Tests.ApplicationTests.Configurators
{
    public class RoomServiceMockConfigurator
    {
        public void SetupCreateSuccessfulRoom(
            RoomSerivceTestsFixtures fixture,
            CreateRoomRequest createRoomRequest,
            Result<Room> reservationResult,
            Result<RoomDto> reservationResultDto)
        {
            fixture.MapperMock.Setup(m => m.Map<Reservation>(createReservationRequest)).Returns(reservationRequest);
            fixture.RoomRepositoryMock.Setup(r => r.IsRoomAvailableAsync(
                createReservationRequest.RoomId,
                createReservationRequest.StartDate,
                createReservationRequest.EndDate,
                null)).ReturnsAsync(Result.Success());

            fixture.ReservationRepositoryMock.Setup(r => r.UserHasReservationAsync(
                userId,
                createReservationRequest.StartDate,
                createReservationRequest.EndDate, null)).ReturnsAsync(false);

            fixture.ReservationRepositoryMock.Setup(r => r.CreateAsync(reservationRequest)).ReturnsAsync(reservationResult);
            fixture.MapperMock.Setup(m => m.Map<Result<ReservationDto>>(reservationResult)).Returns(reservationResultDto);
        }
    }
}
