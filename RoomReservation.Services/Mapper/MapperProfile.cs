using AutoMapper;
using RoomReservation.Application.DTOs.Equipment;
using RoomReservation.Application.DTOs.Reservation;
using RoomReservation.Application.DTOs.Reservation.CreateReservation;
using RoomReservation.Application.DTOs.Reservation.UpdateReservation;
using RoomReservation.Application.DTOs.Room;
using RoomReservation.Application.DTOs.Room.CreateRoom;
using RoomReservation.Domain.Entities;
using RoomReservation.Shared.Common;
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
            CreateMap<RoomReservationLimitDto, RoomReservationLimit>().ReverseMap();
            CreateMap<RoomDto, Room>().ReverseMap();
            CreateMap<RoomEquipmentDto, RoomsEquipments>()
                .ForMember(dest => dest.EquipmentId, opt => opt.MapFrom(src => src.EquipmentType)).ReverseMap();
            CreateMap<RoomDto, Room>().ReverseMap();
            CreateMap<Result<RoomDto>, Result<Room>>().ReverseMap();
            CreateMap<Result<List<RoomDto>>, Result<List<Room>>>().ReverseMap();
            CreateMap<Result<ReservationDto>, Result<Reservation>>().ReverseMap();
            CreateMap<Result<List<ReservationDto>>, Result<List<Reservation>>>().ReverseMap();
            CreateMap<CreateReservationRequest, Reservation>().ReverseMap();
            CreateMap<UpdateReservationRequest, Reservation>().ReverseMap();
            CreateMap<ReservationDto, Reservation>().ReverseMap();
        }
    }
}
