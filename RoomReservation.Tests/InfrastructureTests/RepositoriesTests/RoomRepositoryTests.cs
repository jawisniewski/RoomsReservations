using Microsoft.EntityFrameworkCore;
using RoomReservation.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.InMemory;
using RoomReservation.Tests.ApplicationTests.Configurators;
using RoomReservation.Tests.ApplicationTests.Fixtures;
using RoomReservation.Application.DTOs.Room.CreateRoom;
using RoomReservation.Application.DTOs.Room;
using RoomReservation.Domain.Entities;
using RoomReservation.Domain.Enums;
using RoomReservation.Application.Enums;
using RoomReservation.Infrastructure.Repositories;
using RoomReservation.Tests.InfrastructureTests.Fixtures;
using Shouldly;
using System.Net;
namespace RoomReservation.Tests.InfrastructureTests.RepositoriesTests
{
    public class RoomRepositoryTests
    {

        private RoomRepositoryTestsFixtures _fixture = null!;
        private RoomRepository _roomRepository = null!;
        [SetUp]
        public void Setup()
        {
            _fixture = new RoomRepositoryTestsFixtures();
            _roomRepository = _fixture.CreateRepository();
        }
        [Test]
        public async Task CreateAsync_ShouldReturnSuccessAndRoom_WhenRoomIsCreated()
        {
            _roomRepository = _fixture.CreateRepository();
            var room = new Room()
            {
                Id = 0,
                Name = "Room 1",
                Capacity = 10,
                RoomEquipments = [
                    new()
                    {
                        Id = 0,
                        EquipmentId = 1,
                        Quantity = 1,
                        RoomId=1
                    }
                ],
                RoomLayout = RoomLayoutEnum.Boardroom,
                TableCount = 1,
                RoomReservationLimit = new RoomReservationLimit()
                {
                    Id = 0,
                    RoomId = 1,
                    MaxTime = 60,
                    MinTime = 30
                }
            };
            var result = await _roomRepository.CreateAsync(room);

            result.ShouldNotBeNull();
            result.IsSuccess.ShouldBeTrue();
            result.Data.ShouldNotBeNull();
            result.Data.Id.ShouldBe(1);
            result.FailureMessage.ShouldBeEmpty();
        }
        [Test]
        public async Task CreateAsync_ShouldReturnSuccessAndRoom_WhenTwoRoomIsCreated()
        {
            _roomRepository = _fixture.CreateRepository();
            var room = new Room()
            {
                Id = 0,
                Name = "Room 1",
                Capacity = 10,
                RoomEquipments = [
                    new()
                    {
                        Id = 0,
                        EquipmentId = 1,
                        Quantity = 1,
                        RoomId=1
                    }
                ],
                RoomLayout = RoomLayoutEnum.Boardroom,
                TableCount = 1,
                RoomReservationLimit = new RoomReservationLimit()
                {
                    Id = 0,
                    RoomId = 1,
                    MaxTime = 60,
                    MinTime = 30
                }
            };

            var roomTwo = new Room()
            {
                Id = 0,
                Name = "Room 2",
                Capacity = 10,
                RoomEquipments = [
                    new()
                    {
                        Id = 0,
                        EquipmentId = 1,
                        Quantity = 1,
                        RoomId=1
                    }
                ],
                RoomLayout = RoomLayoutEnum.Boardroom,
                TableCount = 1,
                RoomReservationLimit = new RoomReservationLimit()
                {
                    Id = 0,
                    RoomId = 1,
                    MaxTime = 60,
                    MinTime = 30
                }
            };
            var result = await _roomRepository.CreateAsync(room);
            var resultTwo = await _roomRepository.CreateAsync(roomTwo);

            result.ShouldNotBeNull();
            result.IsSuccess.ShouldBeTrue();
            result.Data.ShouldNotBeNull();
            result.Data.Id.ShouldBe(1);
            result.FailureMessage.ShouldBeEmpty();
            resultTwo.ShouldNotBeNull();
            resultTwo.IsSuccess.ShouldBeTrue();
            resultTwo.Data.ShouldNotBeNull();
            resultTwo.Data.Id.ShouldBe(2);
        }

        [Test]
        public async Task CreateAsync_ShouldReturnConflict_WhenTwoRoomHaveSameName()
        {
            _roomRepository = _fixture.CreateRepository();
            var room = new Room()
            {
                Id = 0,
                Name = "Room 1",
                Capacity = 10,
                RoomEquipments = [
                    new()
                    {
                        Id = 0,
                        EquipmentId = 1,
                        Quantity = 1,
                        RoomId=1
                    }
                ],
                RoomLayout = RoomLayoutEnum.Boardroom,
                TableCount = 1,
                RoomReservationLimit = new RoomReservationLimit()
                {
                    Id = 0,
                    RoomId = 1,
                    MaxTime = 60,
                    MinTime = 30
                }
            };

            var roomTwo = new Room()
            {
                Id = 0,
                Name = "Room 1",
                Capacity = 10,
                RoomEquipments = [
                    new()
                    {
                        Id = 0,
                        EquipmentId = 1,
                        Quantity = 1,
                        RoomId=1
                    }
                ],
                RoomLayout = RoomLayoutEnum.Boardroom,
                TableCount = 1,
                RoomReservationLimit = new RoomReservationLimit()
                {
                    Id = 0,
                    RoomId = 1,
                    MaxTime = 60,
                    MinTime = 30
                }
            };
            var result = await _roomRepository.CreateAsync(room);
            var resultTwo = await _roomRepository.CreateAsync(roomTwo);

            result.ShouldNotBeNull();
            result.IsSuccess.ShouldBeTrue();
            result.Data.ShouldNotBeNull();
            result.Data.Id.ShouldBe(1);
            result.FailureMessage.ShouldBeEmpty();
            resultTwo.ShouldNotBeNull();
            resultTwo.IsSuccess.ShouldBeFalse();
            resultTwo.StatusCode.ShouldBe(HttpStatusCode.Conflict);
        }
        [Test]
        public async Task UpdateAsync_ShouldReturnSuccessAndRoom_WhenRoomIsUpdated()
        {
            _roomRepository = _fixture.CreateRepository();
            var createRoom = new Room()
            {
                Id = 0,
                Name = "Room 1",
                Capacity = 10,
                RoomEquipments = [
                    new()
                    {
                        Id = 0,
                        EquipmentId = 1,
                        Quantity = 1,
                        RoomId=1
                    }
                ],
                RoomLayout = RoomLayoutEnum.Boardroom,
                TableCount = 1,
                RoomReservationLimit = new RoomReservationLimit()
                {
                    Id = 0,
                    RoomId = 1,
                    MaxTime = 60,
                    MinTime = 30
                }
            };
            var room = new RoomDto()
            {
                Id = 1,
                Name = "Room 1",
                Capacity = 10,
                RoomEquipments = [
                    new()
                    {
                        Id = 1,
                        EquipmentType = EquipmentTypeEnum.Projector,
                        Quantity = 1
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
            var createResult = await _roomRepository.CreateAsync(createRoom);
            var result = await _roomRepository.UpdateAsync(room);

            result.ShouldNotBeNull();
            result.IsSuccess.ShouldBeTrue();
            result.Data.ShouldNotBeNull();
            result.Data.Id.ShouldBe(1);
            result.FailureMessage.ShouldBeEmpty();
        }

        [Test]
        public async Task UpdateAsync_ShouldReturnConflict_WhenTwoRoomHaveSameName()
        {
            _roomRepository = _fixture.CreateRepository();
            var room = new RoomDto()
            {
                Id = 1,
                Name = "Room 1",
                Capacity = 10,
                RoomEquipments = [
                new()
                {
                    Id = 1,
                    EquipmentType = EquipmentTypeEnum.Projector,
                    Quantity = 1
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

            var roomTwo = new RoomDto()
            {
                Id = 2,
                Name = "Room 1",
                Capacity = 10,
                RoomEquipments = [
                new()
                {
                    Id = 2,
                    EquipmentType = EquipmentTypeEnum.Projector,
                    Quantity = 1
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
            var createRoom = new Room()
            {
                Id = 0,
                Name = "Room 1",
                Capacity = 10,
                RoomEquipments = [
                new()
                    {
                        Id = 0,
                        EquipmentId = 1,
                        Quantity = 1,
                        RoomId=1
                    }
                ],
                RoomLayout = RoomLayoutEnum.Boardroom,
                TableCount = 1,
                RoomReservationLimit = new RoomReservationLimit()
                {
                    Id = 0,
                    RoomId = 1,
                    MaxTime = 60,
                    MinTime = 30
                }
            };
            var createResult = await _roomRepository.CreateAsync(createRoom);
            var result = await _roomRepository.UpdateAsync(room);
            var resultTwo = await _roomRepository.UpdateAsync(roomTwo);

            result.ShouldNotBeNull();
            result.IsSuccess.ShouldBeTrue();
            result.Data.ShouldNotBeNull();
            result.Data.Id.ShouldBe(1);
            result.FailureMessage.ShouldBeEmpty();
            resultTwo.ShouldNotBeNull();
            resultTwo.IsSuccess.ShouldBeFalse();
            resultTwo.StatusCode.ShouldBe(HttpStatusCode.Conflict);
        }

        [Test]
        public async Task UpdateAsync_ShouldReturnNotFound_WhenRoomNotExists()
        {
            _roomRepository = _fixture.CreateRepository();
            var room = new RoomDto()
            {
                Id = 1,
                Name = "Room 1",
                Capacity = 10,
                RoomEquipments = [
                    new()
                    {
                        Id = 1,
                        EquipmentType = EquipmentTypeEnum.Projector,
                        Quantity = 1
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

            var result = await _roomRepository.UpdateAsync(room);

            result.ShouldNotBeNull();
            result.IsSuccess.ShouldBeFalse();
            result.Data.ShouldBeNull();
            result.FailureMessage.ShouldNotBeEmpty();
            result.FailureMessage.ShouldBe($"Room not found {room.Id}");
            result.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }
        [Test]
        public async Task UpdateAsync_ShouldSetRoomReservationLimit_WhenRoomIsUpdated()
        {
            _roomRepository = _fixture.CreateRepository();
            var createRoom = new Room()
            {
                Id = 0,
                Name = "Room 1",
                Capacity = 10,
                RoomEquipments = [
                    new()
                    {
                        Id = 0,
                        EquipmentId = 1,
                        Quantity = 1,
                        RoomId=1
                    }
                ],
                RoomLayout = RoomLayoutEnum.Boardroom,
                TableCount = 1,
                RoomReservationLimit = new RoomReservationLimit()
                {
                    Id = 0,
                    RoomId = 1,
                    MaxTime = 60,
                    MinTime = 30
                }
            };
            var room = new RoomDto()
            {
                Id = 1,
                Name = "Room 1",
                Capacity = 10,
                RoomEquipments = [
                    new()
                    {
                        Id = 1,
                        EquipmentType = EquipmentTypeEnum.Projector,
                        Quantity = 1
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
            var createResult = await _roomRepository.CreateAsync(createRoom);
            var result = await _roomRepository.UpdateAsync(room);

            result.ShouldNotBeNull();
            result.IsSuccess.ShouldBeTrue();
            result.Data.ShouldNotBeNull();
            result.Data.RoomReservationLimit.ShouldNotBeNull();
            result.Data.RoomReservationLimit.MaxTime.ShouldBe(60);
            result.Data.RoomReservationLimit.MinTime.ShouldBe(30);
        }

        [Test]
        public async Task DeleteAsync_ShouldBeTrue_WhenRoomIsDeleted()
        {
            _roomRepository = _fixture.CreateRepository();
            var createRoom = new Room()
            {
                Id = 0,
                Name = "Room 1",
                Capacity = 10,
                RoomEquipments = [
                    new()
                    {
                        Id = 0,
                        EquipmentId = 1,
                        Quantity = 1,
                        RoomId=1
                    }
                ],
                RoomLayout = RoomLayoutEnum.Boardroom,
                TableCount = 1,
                RoomReservationLimit = new RoomReservationLimit()
                {
                    Id = 0,
                    RoomId = 1,
                    MaxTime = 60,
                    MinTime = 30
                }
            };

            var createResult = await _roomRepository.CreateAsync(createRoom);
            var result = await _roomRepository.DeleteAsync(1);

            result.ShouldNotBeNull();
            result.IsSuccess.ShouldBeTrue();
        }

        [Test]
        public async Task DeleteAsync_ShouldReturnNotFound_WhenRoomNotFound()
        {
            _roomRepository = _fixture.CreateRepository();

            var result = await _roomRepository.DeleteAsync(1);

            result.ShouldNotBeNull();
            result.IsSuccess.ShouldBeFalse();
            result.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Test]
        public async Task GetListAsync_ShouldReturnTwoRooms_WhenFiltersEmpty()
        {
            _roomRepository = _fixture.CreateRepository();
            var createRoom = new Room()
            {
                Id = 0,
                Name = "Room 1",
                Capacity = 10,
                RoomEquipments = [
                    new()
                    {
                        Id = 0,
                        EquipmentId = 1,
                        Quantity = 1,
                        RoomId=1
                    }
                ],
                RoomLayout = RoomLayoutEnum.Boardroom,
                TableCount = 1,
                RoomReservationLimit = new RoomReservationLimit()
                {
                    Id = 0,
                    RoomId = 1,
                    MaxTime = 60,
                    MinTime = 30
                }
            }; var createRoom2 = new Room()
            {
                Id = 0,
                Name = "Room 2",
                Capacity = 10,
                RoomEquipments = [
                    new()
                    {
                        Id = 0,
                        EquipmentId = 1,
                        Quantity = 1,
                        RoomId=1
                    }
                ],
                RoomLayout = RoomLayoutEnum.Boardroom,
                TableCount = 1,
                RoomReservationLimit = new RoomReservationLimit()
                {
                    Id = 0,
                    RoomId = 1,
                    MaxTime = 60,
                    MinTime = 30
                }
            };
            await _roomRepository.CreateAsync(createRoom);
            await _roomRepository.CreateAsync(createRoom2);
            var result = await _roomRepository.GetListAsync(new RoomFilter());

            result.ShouldNotBeNull();
            result.IsSuccess.ShouldBeTrue();
            result.Data.ShouldNotBeNull();
            result.Data.Count.ShouldBe(2);
        }
        [Test]
        public async Task GetListAsync_ShouldReturnEmptyList_WhenFiltersEmpty()
        {
            _roomRepository = _fixture.CreateRepository();
            var createRoom = new Room()
            {
                Id = 0,
                Name = "Room 1",
                Capacity = 10,
                RoomEquipments = [
                    new()
                    {
                        Id = 0,
                        EquipmentId = 1,
                        Quantity = 1,
                        RoomId=1
                    }
                ],
                RoomLayout = RoomLayoutEnum.Boardroom,
                TableCount = 1,
                RoomReservationLimit = new RoomReservationLimit()
                {
                    Id = 0,
                    RoomId = 1,
                    MaxTime = 60,
                    MinTime = 30
                }
            }; var createRoom2 = new Room()
            {
                Id = 0,
                Name = "Room 2",
                Capacity = 10,
                RoomEquipments = [
                    new()
                    {
                        Id = 0,
                        EquipmentId = 1,
                        Quantity = 1,
                        RoomId=1
                    }
                ],
                RoomLayout = RoomLayoutEnum.Boardroom,
                TableCount = 1,
                RoomReservationLimit = new RoomReservationLimit()
                {
                    Id = 0,
                    RoomId = 1,
                    MaxTime = 60,
                    MinTime = 30
                }
            };
            var result = await _roomRepository.GetListAsync(new RoomFilter());

            result.ShouldNotBeNull();
            result.IsSuccess.ShouldBeTrue();
            result.Data.ShouldNotBeNull();
            result.Data.Count.ShouldBe(0);
        }

        [Test]
        public async Task GetListAsync_ShouldReturnOneElement_WhenFiltersByName()
        {
            _roomRepository = _fixture.CreateRepository();
            var createRoom = new Room()
            {
                Id = 0,
                Name = "Room 1",
                Capacity = 10,
                RoomEquipments = [
                    new()
                    {
                        Id = 0,
                        EquipmentId = 1,
                        Quantity = 1,
                        RoomId=1
                    }
                ],
                RoomLayout = RoomLayoutEnum.Boardroom,
                TableCount = 1,
                RoomReservationLimit = new RoomReservationLimit()
                {
                    Id = 0,
                    RoomId = 1,
                    MaxTime = 60,
                    MinTime = 30
                }
            }; var createRoom2 = new Room()
            {
                Id = 0,
                Name = "Room 2",
                Capacity = 10,
                RoomEquipments = [
                    new()
                    {
                        Id = 0,
                        EquipmentId = 1,
                        Quantity = 1,
                        RoomId=1
                    }
                ],
                RoomLayout = RoomLayoutEnum.Boardroom,
                TableCount = 1,
                RoomReservationLimit = new RoomReservationLimit()
                {
                    Id = 0,
                    RoomId = 1,
                    MaxTime = 60,
                    MinTime = 30
                }
            };

            await _roomRepository.CreateAsync(createRoom);
            await _roomRepository.CreateAsync(createRoom2);

            var result = await _roomRepository.GetListAsync(new RoomFilter() { Name = "Room 2" });

            result.ShouldNotBeNull();
            result.IsSuccess.ShouldBeTrue();
            result.Data.ShouldNotBeNull();
            result.Data.Count.ShouldBe(1);
        }
    }
}
