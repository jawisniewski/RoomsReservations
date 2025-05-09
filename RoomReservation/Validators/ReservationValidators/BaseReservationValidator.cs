﻿using RoomReservation.API.Validators.Room;
using RoomReservation.Application.DTOs.Reservation;
using RoomReservation.Application.DTOs.Room;
using FluentValidation;

namespace RoomReservation.API.Validators.ReservationValidators
{
    public class BaseReservationValidator<T> : AbstractValidator<T> where T : BaseReservationDto
    {
        public BaseReservationValidator()
        {
            RuleFor(r => r.StartDate)
                .GreaterThan(DateTime.Now)
                    .WithMessage($"Reservation starting date must be after now - {DateTime.Now}");
            RuleFor(r => r.StartDate)
                .LessThan(rr => rr.EndDate)
                    .WithMessage(r=> $"Reservation starting date must be before ending date - {r.EndDate}");

            RuleFor(rr => rr.RoomId)
                .GreaterThan(0)
                    .WithMessage("Room id must be specified and greater then 0");
        }
    }
}
