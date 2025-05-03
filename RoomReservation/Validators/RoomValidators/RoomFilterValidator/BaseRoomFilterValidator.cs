using FluentValidation;
using RoomReservation.Application.DTOs.Room;

namespace RoomReservation.API.Validators.RoomValidators.RoomFilterValidator
{
    public class BaseRoomFilterValidator<T> : AbstractValidator<T> where T : RoomFilter
    {
        public BaseRoomFilterValidator()
        {
            RuleFor(rf => rf.TableCount)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Table count must be greater or equal to 0.")
                .When(rf =>rf.TableCount is not null);

            RuleFor(rf => rf.Capacity)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Capacity must be greater or equal to 0.")
                .When(rf => rf.Capacity is not null); 

            RuleFor(rf => rf.Name)
                .Length(2, 300)
                .WithMessage("Name must be beetwen 2 - 300 characters")
                .When(rf => rf.Name is not null);

            RuleFor(rf => rf.RoomLayout)
                .IsInEnum().WithMessage("Room layout must be  Boardroom, Theater or Classroom");
            RuleForEach(rf => rf.EquipmentTypes).IsInEnum().WithMessage("Equipment must be Projector, Whiteboard, VideoConference, Screen")
                .When(rf => rf.EquipmentTypes is not null); ;
        }
    }
}
