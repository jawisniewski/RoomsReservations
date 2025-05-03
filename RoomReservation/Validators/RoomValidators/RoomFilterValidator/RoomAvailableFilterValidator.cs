using FluentValidation;
using RoomReservation.Application.DTOs.Room;
using System.Globalization;

namespace RoomReservation.API.Validators.RoomValidators.RoomFilterValidator
{
    public class RoomAvailableFilterValidator : BaseRoomFilterValidator<RoomAvalibilityRequest>
    {
        public RoomAvailableFilterValidator() : base()
        {
            RuleFor(x => x.AvailableFrom)
                .GreaterThan(DateTime.Now)
                    .WithMessage($"Available from must be after now - {DateTime.Now}");
            RuleFor(x => x.AvailableTo)
                .GreaterThan(DateTime.Now)
                    .WithMessage($"Available to must be after now - {DateTime.Now}");
            RuleFor(x => x.AvailableTo)
                .GreaterThan(ra => ra.AvailableFrom)
                    .WithMessage(ra => $"Available to must by after available from - {ra.AvailableFrom}")
                    .When(ra => ra.AvailableTo > DateTime.MinValue);
        }
    }
}
