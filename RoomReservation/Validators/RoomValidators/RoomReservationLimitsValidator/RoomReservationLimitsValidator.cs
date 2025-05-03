using FluentValidation;
using RoomReservation.Application.DTOs.Room;

namespace RoomReservation.API.Validators.RoomValidators.RoomReservationLimitsValidator
{
    public class RoomReservationLimitsValidator : AbstractValidator<RoomReservationLimitDto>
    {
        public RoomReservationLimitsValidator()
        {
            RuleFor(rr => rr.MinTime)
                .LessThanOrEqualTo(rr => rr.MaxTime)
                .When(rr => rr.MaxTime != null && rr.MinTime != null)
                    .WithMessage(rr => $"Minimum time of reservation must be lower then maximum time. {rr.MaxTime}");
            RuleFor(rr => rr.MinTime)
                .GreaterThanOrEqualTo(0)
                    .WithMessage("Minimum reservation time must be equal or higher then 0 minutes");

            RuleFor(rr => rr.MaxTime)
                .GreaterThan(0)
                    .WithMessage("Maximum reservation time must be higher then 0 minutes");
            RuleFor(rr => rr.MaxTime)
                .GreaterThan(rr => rr.MinTime)
                    .When(rr => rr.MaxTime != null && rr.MinTime != null)
                    .WithMessage(rr=> $"Maximum time of reservation must be higher then minimum time -{rr.MinTime}.");
        }
    }
}
