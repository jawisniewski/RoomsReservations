using AutoMapper;
using RoomReservation.Application.DTOs.Room.CreateRoom;
using RoomReservation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservation.Application.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CreateRoomRequest, Room>().ReverseMap();
            CreateMap<CreateRoomEquipmentRequest, Equipment>().ReverseMap();
            CreateMap<CreateRoomReservationLimitRequest, RoomReservationLimit>().ReverseMap();
        }
    }
}
