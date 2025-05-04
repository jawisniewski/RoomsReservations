using RoomReservation.Application.DTOs.Reservation.CreateReservation;
using RoomReservation.Application.DTOs.Reservation;
using RoomReservation.Domain.Entities;
using RoomReservation.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoomReservation.Tests.ApplicationTests.Fixtures;
using Moq;
using RoomReservation.Application.DTOs.Reservation.UpdateReservation;

namespace RoomReservation.Tests.ApplicationTests.Configurator
{
    public class ReservationServiceMockConfigurator
    {
        public void SetupCreateSuccessfulReservation(
            ReservationServiceTestsFixtures fixture,
            CreateReservationRequest createReservationRequest,
            int userId,
            Reservation reservationRequest,
            Result<Reservation> reservationResult,
            Result<ReservationDto> reservationResultDto)
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
        public void SetupUnprocessablyResultWhenRoomUnavailable(
            ReservationServiceTestsFixtures fixture,
            BaseReservationDto baseReservationRequest,
            int userId,
            Reservation reservationRequest,
            int? reservationId = null)
        {
            fixture.MapperMock.Setup(m => m.Map<Reservation>(baseReservationRequest)).Returns(reservationRequest);
            fixture.RoomRepositoryMock.Setup(r => r.IsRoomAvailableAsync(
                baseReservationRequest.RoomId,
                baseReservationRequest.StartDate,
                baseReservationRequest.EndDate,
                reservationId)).ReturnsAsync(Result.Failure("Room is unavailable", System.Net.HttpStatusCode.UnprocessableEntity));


        }
        public void SetupUnprocessablyResultWhenUserHasAReservation(
            ReservationServiceTestsFixtures fixture,
            BaseReservationDto baseReservationRequest,
            int userId,
            Reservation reservationRequest,
            int? reservationId = null)
        {
            fixture.MapperMock.Setup(m => m.Map<Reservation>(baseReservationRequest)).Returns(reservationRequest);
            fixture.RoomRepositoryMock.Setup(r => r.IsRoomAvailableAsync(
                baseReservationRequest.RoomId,
                baseReservationRequest.StartDate,
                baseReservationRequest.EndDate,
                reservationId)).ReturnsAsync(Result.Success());
            fixture.ReservationRepositoryMock.Setup(r => r.UserHasReservationAsync(
                userId,
                baseReservationRequest.StartDate,
                baseReservationRequest.EndDate, reservationId)).ReturnsAsync(true);

        }
        public void SetupUpdateSuccessfulReservation(
            ReservationServiceTestsFixtures fixture,
            UpdateReservationRequest updateReservationRequest,
            int userId,
            Reservation reservationRequest,
            Result<Reservation> reservationResult,
            Result<ReservationDto> reservationResultDto)
        {
            fixture.MapperMock.Setup(m => m.Map<Reservation>(updateReservationRequest)).Returns(reservationRequest);
            fixture.RoomRepositoryMock.Setup(r => r.IsRoomAvailableAsync(
                updateReservationRequest.RoomId,
                updateReservationRequest.StartDate,
                updateReservationRequest.EndDate,
                updateReservationRequest.Id)).ReturnsAsync(Result.Success());

            fixture.ReservationRepositoryMock.Setup(r => r.UserHasReservationAsync(
                userId,
                updateReservationRequest.StartDate,
                updateReservationRequest.EndDate, null)).ReturnsAsync(false);

            fixture.ReservationRepositoryMock.Setup(r => r.UpdateAsync(updateReservationRequest, userId)).ReturnsAsync(reservationResult);
            fixture.MapperMock.Setup(m => m.Map<Result<ReservationDto>>(reservationResult)).Returns(reservationResultDto);
        }

        public void SetupDeleteSuccessfulReservation(
            ReservationServiceTestsFixtures fixture,
            int reservationId,
            int userId)
        {
            fixture.ReservationRepositoryMock.Setup(r => r.DeleteAsync(
                reservationId,
                userId)).ReturnsAsync(Result.Success());
        }

        public void SetupDeleteBadRequestReservation(
            ReservationServiceTestsFixtures fixture,
            int reservationId,
            int userId)
        {
            fixture.ReservationRepositoryMock.Setup(r => r.DeleteAsync(
                reservationId,
                userId)).ReturnsAsync(Result.Failure("Delete room failure", System.Net.HttpStatusCode.BadRequest));
        }
        public void SetupGetReservationsSuccessful(
            ReservationServiceTestsFixtures fixture,
            Result<List<Reservation>> reservationListResult,
            Result<List<ReservationDto>> reservationResultDto)
        {          
            fixture.ReservationRepositoryMock.Setup(r => r.GetListAsync()).ReturnsAsync(reservationListResult);
            fixture.MapperMock.Setup(m => m.Map<Result<List<ReservationDto>>>(reservationListResult)).Returns(reservationResultDto);
        }
        public void SetupGetReservationsFailure(
            ReservationServiceTestsFixtures fixture,
            Result<List<Reservation>> reservationListResult,
            Result<List<ReservationDto>> reservationResultDto)
        {
            fixture.ReservationRepositoryMock.Setup(r => r.GetListAsync()).ReturnsAsync(reservationListResult);
            fixture.MapperMock.Setup(m => m.Map<Result<List<ReservationDto>>>(reservationListResult)).Returns(reservationResultDto);
        }
    }
}
