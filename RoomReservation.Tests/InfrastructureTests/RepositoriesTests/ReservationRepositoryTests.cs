using RoomReservation.Application.DTOs.Reservation.UpdateReservation;
using RoomReservation.Domain.Entities;
using RoomReservation.Domain.Enums;
using RoomReservation.Infrastructure.Repositories;
using RoomReservation.Tests.InfrastructureTests.Fixtures;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservation.Tests.InfrastructureTests.RepositoriesTests
{
    public class ReservationRepositoryTests
    {
        private ReservationRepositoryTestsFixtures _fixture = null!;
        private ReservationRepository _reservationRepository = null!;
        [SetUp]
        public void Setup()
        {
            _fixture = new ReservationRepositoryTestsFixtures();
            _reservationRepository = _fixture.CreateRepository();
        }

        [Test]
        public async Task CreateAsync_ShouldReturnSuccessAndReservation_WhenIsCreated()
        {
            _reservationRepository = _fixture.CreateRepository();
            var reservation = new Reservation()
            {
                EndDate = DateTime.Now.AddMinutes(60),
                StartDate = DateTime.Now.AddMinutes(1),
                RoomId = 1,
                UserId = 1,
                Id = 0
            };

            var result = await _reservationRepository.CreateAsync(reservation);

            result.ShouldNotBeNull();
            result.IsSuccess.ShouldBeTrue();
            result.Data.ShouldNotBeNull();
            result.Data.Id.ShouldBe(1);
            result.FailureMessage.ShouldBeEmpty();
        }

        [Test]
        public async Task UpdateAsync_ShouldReturnSuccessAndReservation_WhenIsUpdated()
        {
            _reservationRepository = _fixture.CreateRepository();
            var reservation = new Reservation()
            {
                EndDate = DateTime.Now.AddMinutes(60),
                StartDate = DateTime.Now.AddMinutes(1),
                RoomId = 1,
                UserId = 1,
                Id = 0
            };
            var updateReservationRequest = new UpdateReservationRequest()
            {
                EndDate = DateTime.Now.AddMinutes(120),
                StartDate = DateTime.Now.AddMinutes(1),
                RoomId = 1,
                Id = 1
            };
            var userId = 1;

            await _reservationRepository.CreateAsync(reservation);

            var result = await _reservationRepository.UpdateAsync(updateReservationRequest, userId);

            result.ShouldNotBeNull();
            result.IsSuccess.ShouldBeTrue();
            result.Data.ShouldNotBeNull();
            result.Data.Id.ShouldBe(1);
            result.FailureMessage.ShouldBeEmpty();
        }

        [Test]
        public async Task UpdateAsync_ShouldReturnForbiden_WhenTryToUpdateAnotherUserReservation()
        {
            _reservationRepository = _fixture.CreateRepository();
            var reservation = new Reservation()
            {
                EndDate = DateTime.Now.AddMinutes(60),
                StartDate = DateTime.Now.AddMinutes(1),
                RoomId = 1,
                UserId = 1,
                Id = 0
            };
            var updateReservationRequest = new UpdateReservationRequest()
            {
                EndDate = DateTime.Now.AddMinutes(120),
                StartDate = DateTime.Now.AddMinutes(1),
                RoomId = 1,
                Id = 1
            };
            var userId = 2;

            await _reservationRepository.CreateAsync(reservation);

            var result = await _reservationRepository.UpdateAsync(updateReservationRequest, userId);

            result.ShouldNotBeNull();
            result.IsSuccess.ShouldBeFalse();
            result.StatusCode.ShouldBe(System.Net.HttpStatusCode.Forbidden);
        }

        [Test]
        public async Task UpdateAsync_ShouldReturnNotFound_WhenReservationNotExists()
        {
            _reservationRepository = _fixture.CreateRepository();
           
            var updateReservationRequest = new UpdateReservationRequest()
            {
                EndDate = DateTime.Now.AddMinutes(120),
                StartDate = DateTime.Now.AddMinutes(1),
                RoomId = 1,
                Id = 1
            };

            var userId = 1;
            var result = await _reservationRepository.UpdateAsync(updateReservationRequest, userId);

            result.ShouldNotBeNull();
            result.IsSuccess.ShouldBeFalse();
            result.StatusCode.ShouldBe(System.Net.HttpStatusCode.NotFound);
        }

        [Test]
        public async Task DeleteAsync_ShouldBeSuceess_WhenReservationDeleted()
        {
            _reservationRepository = _fixture.CreateRepository();
            var reservation = new Reservation()
            {
                EndDate = DateTime.Now.AddMinutes(60),
                StartDate = DateTime.Now.AddMinutes(1),
                RoomId = 1,
                UserId = 1,
                Id = 0
            };
            var reservationId = 1;
            var userId = 1;

            await _reservationRepository.CreateAsync(reservation);
            var result = await _reservationRepository.DeleteAsync(reservationId, userId);

            result.ShouldNotBeNull();
            result.IsSuccess.ShouldBeTrue();
  
        }

        [Test]
        public async Task DeleteAsync_ShouldBeSuceessAndEmptyList_WhenReservationDeleted()
        {
            _reservationRepository = _fixture.CreateRepository();
            var reservation = new Reservation()
            {
                EndDate = DateTime.Now.AddMinutes(60),
                StartDate = DateTime.Now.AddMinutes(1),
                RoomId = 1,
                UserId = 1,
                Id = 0
            };
            var reservationId = 1;
            var userId = 1;

            await _reservationRepository.CreateAsync(reservation);
            await _reservationRepository.DeleteAsync(reservationId, userId);

            var reservations = await _reservationRepository.GetListAsync();

            reservations.ShouldNotBeNull();
            reservations.Data.ShouldNotBeNull();
            reservations.Data.Count.ShouldBe(0);
        }

        [Test]
        public async Task DeleteAsync_ShouldReturnNotFound_WhenReservationNotExists() 
        {
            _reservationRepository = _fixture.CreateRepository();

            var reservationId = 1;
            var userId = 1;

            var result = await _reservationRepository.DeleteAsync(reservationId, userId);

            result.ShouldNotBeNull();
            result.IsSuccess.ShouldBeFalse();
            result.StatusCode.ShouldBe(System.Net.HttpStatusCode.NotFound);
        }

        [Test]
        public async Task DeleteAsync_ShouldReturnForbiden_WhenUserTryToRemoveAnotherUserReservation()
        {
            _reservationRepository = _fixture.CreateRepository();

            var reservation = new Reservation()
            {
                EndDate = DateTime.Now.AddMinutes(60),
                StartDate = DateTime.Now.AddMinutes(1),
                RoomId = 1,
                UserId = 1,
                Id = 0
            };

            var reservationId = 1;
            var userId = 2;

            await _reservationRepository.CreateAsync(reservation);

            var result = await _reservationRepository.DeleteAsync(reservationId, userId);

            result.ShouldNotBeNull();
            result.IsSuccess.ShouldBeFalse();
            result.StatusCode.ShouldBe(System.Net.HttpStatusCode.Forbidden);
        }
        [Test]
        public async Task GetListAsync_ShouldReturnSuccess_WhenReservationExists()
        {
            _reservationRepository = _fixture.CreateRepository();

            var reservation = new Reservation()
            {
                EndDate = DateTime.Now.AddMinutes(60),
                StartDate = DateTime.Now.AddMinutes(1),
                RoomId = 1,
                UserId = 1,
                Id = 0
            };
            var reservationTwo = new Reservation()
            {
                EndDate = DateTime.Now.AddMinutes(60),
                StartDate = DateTime.Now.AddMinutes(1),
                RoomId = 1,
                UserId = 1,
                Id = 0
            };

            await _reservationRepository.CreateAsync(reservation);
            await _reservationRepository.CreateAsync(reservationTwo);

            var result = await _reservationRepository.GetListAsync();

            result.ShouldNotBeNull();
            result.IsSuccess.ShouldBeTrue();
            result.Data.ShouldNotBeNull();
        }
    }
}
