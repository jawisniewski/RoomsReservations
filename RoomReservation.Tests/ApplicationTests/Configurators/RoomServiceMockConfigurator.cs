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
using Moq;

namespace RoomReservation.Tests.ApplicationTests.Configurators
{
    public class RoomServiceMockConfigurator
    {
        public void SetupCreateRoomSuccessful(
            RoomSerivceTestsFixtures fixture,
            CreateRoomRequest createRoomRequest,
            Room room,
            RoomDto roomDto)
        {
            var roomResult = Result<Room>.Success(room);
            var roomResultDto = Result<RoomDto>.Success(roomDto);
            fixture.MapperMock.Setup(m => m.Map<Room>(createRoomRequest)).Returns(room);
            fixture.RoomRepositoryMock.Setup(r => r.CreateAsync(room)).ReturnsAsync(roomResult);
            fixture.MapperMock.Setup(m => m.Map<Result<RoomDto>>(roomResult)).Returns(roomResultDto);
        }
        public void SetupCreateRoomFailure(
            RoomSerivceTestsFixtures fixture,
            CreateRoomRequest createRoomRequest,
            Room room)
        {
            var failureResult = Result<Room>.Failure("Create room failure", System.Net.HttpStatusCode.UnprocessableEntity);
            var failureResultDto = Result<RoomDto>.Failure("Create room failure", System.Net.HttpStatusCode.UnprocessableEntity);

            fixture.MapperMock.Setup(m => m.Map<Room>(createRoomRequest)).Returns(room);
            fixture.RoomRepositoryMock.Setup(r => r.CreateAsync(room)).ReturnsAsync(failureResult);
            fixture.MapperMock.Setup(m => m.Map<Result<RoomDto>>(failureResult)).Returns(failureResultDto);
        }

        public void SetupDeleteRoomSuccessful(
            RoomSerivceTestsFixtures fixture,
            int roomId)
        {
            var roomResult = Result.Success();
            fixture.RoomRepositoryMock.Setup(r => r.DeleteAsync(roomId)).ReturnsAsync(roomResult);
        }

        public void SetupDeleteRoomFailure(
            RoomSerivceTestsFixtures fixture, 
            int roomId)
        {
            var roomResult = Result.Failure("Delete room failure", System.Net.HttpStatusCode.UnprocessableEntity);
            fixture.RoomRepositoryMock.Setup(r => r.DeleteAsync(roomId)).ReturnsAsync(roomResult);
        }

        public void SetupUpdateRoomSuccessful(
            RoomSerivceTestsFixtures fixture,
            RoomDto roomDto,
            Room room)
        {
            var roomResult = Result<Room>.Success(room);
            var roomResultDto = Result<RoomDto>.Success(roomDto);

            fixture.MapperMock.Setup(m => m.Map<Room>(roomDto)).Returns(room);
            fixture.RoomRepositoryMock.Setup(r => r.UpdateAsync(roomDto)).ReturnsAsync(roomResult);
            fixture.MapperMock.Setup(m => m.Map<Result<RoomDto>>(roomResult)).Returns(roomResultDto);
        }

        public void SetupUpdateRoomFailure(
            RoomSerivceTestsFixtures fixture,
            RoomDto roomDto,
            Room room)
        {
            var failureResult = Result<Room>.Failure("Update room failure", System.Net.HttpStatusCode.UnprocessableEntity);
            var failureResultDto = Result<RoomDto>.Failure("Update room failure", System.Net.HttpStatusCode.UnprocessableEntity);

            fixture.MapperMock.Setup(m => m.Map<Room>(roomDto)).Returns(room);
            fixture.RoomRepositoryMock.Setup(r => r.UpdateAsync(roomDto)).ReturnsAsync(failureResult);
            fixture.MapperMock.Setup(m => m.Map<Result<RoomDto>>(failureResult)).Returns(failureResultDto);
        }

        public void SetupGetRoomListSuccessful(
            RoomSerivceTestsFixtures fixture,
            List<Room> rooms,
            List<RoomDto> roomsDto,
            RoomFilter filter)
        {
            var roomsResult = Result<List<Room>>.Success(rooms);
            var roomsDtoResult = Result<List<RoomDto>>.Success(roomsDto);
            fixture.RoomRepositoryMock.Setup(r => r.GetListAsync(filter)).ReturnsAsync(roomsResult);
            fixture.MapperMock.Setup(m => m.Map<Result<List<RoomDto>>>(roomsResult)).Returns(roomsDtoResult);
        }

        

        public void SetupGetRoomListFailure(
            RoomSerivceTestsFixtures fixture,
            RoomFilter filter)
        {
            var roomsResult = Result<List<Room>>.Failure("Get room list failure", System.Net.HttpStatusCode.UnprocessableEntity);
            var roomsDtoResult = Result<List<RoomDto>>.Failure("Get room list failure" , System.Net.HttpStatusCode.UnprocessableEntity);
            fixture.RoomRepositoryMock.Setup(r => r.GetListAsync(filter)).ReturnsAsync(roomsResult);
            fixture.MapperMock.Setup(m => m.Map<Result<List<RoomDto>>>(roomsResult)).Returns(roomsDtoResult);
        }

        public void SetupGetAvailbityRoomsSuccessful(
            RoomSerivceTestsFixtures fixture,
            List<Room> rooms,
            List<RoomDto> roomsDto,
            RoomAvalibilityRequest filter)
        {
            var roomsResult = Result<List<Room>>.Success(rooms);
            var roomsDtoResult = Result<List<RoomDto>>.Success(roomsDto);
            fixture.RoomRepositoryMock.Setup(r => r.GetAvalibilityRoomAsync(filter)).ReturnsAsync(roomsResult);
            fixture.MapperMock.Setup(m => m.Map<Result<List<RoomDto>>>(roomsResult)).Returns(roomsDtoResult);
        }

        public void SetupAvailibityRoomsFailure(
            RoomSerivceTestsFixtures fixture,
            RoomAvalibilityRequest filter)
        {
            var roomsResult = Result<List<Room>>.Failure("Get room list failure", System.Net.HttpStatusCode.UnprocessableEntity);
            var roomsDtoResult = Result<List<RoomDto>>.Failure("Get room list failure", System.Net.HttpStatusCode.UnprocessableEntity);
            fixture.RoomRepositoryMock.Setup(r => r.GetAvalibilityRoomAsync(filter)).ReturnsAsync(roomsResult);
            fixture.MapperMock.Setup(m => m.Map<Result<List<RoomDto>>>(roomsResult)).Returns(roomsDtoResult);

        }
    }
}
