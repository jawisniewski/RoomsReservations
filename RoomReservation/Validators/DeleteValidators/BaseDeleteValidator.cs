using FluentValidation;
using RoomReservation.Application.DTOs;
using RoomReservation.Application.DTOs.Reservation;

namespace RoomReservation.API.Validators.DeleteValidators
{
    public class BaseDeleteValidator : AbstractValidator<BaseDeleteRequest> 
    {
        public BaseDeleteValidator()
        {
            RuleFor(r => r.Id)
                .GreaterThan(0)
                .WithMessage("Id must be specified and greater then 0")
                .NotEmpty()
                .WithMessage("Id must be specified and greater then 0")
                .NotNull()
                .WithMessage("Id must be specified and greater then 0");
        }
    }
}
