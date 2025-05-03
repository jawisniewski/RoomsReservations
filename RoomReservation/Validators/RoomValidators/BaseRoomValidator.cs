using FluentValidation;
using RoomReservation.API.Validators.RoomValidators.RoomReservationLimitsValidator;
using RoomReservation.Application.DTOs;
using RoomReservation.Application.DTOs.Equipment;
using RoomReservation.Application.DTOs.Room;
using RoomReservation.Application.DTOs.Room.CreateRoom;
using RoomReservation.Application.Enums;

namespace RoomReservation.API.Validators.Room
{
    public class BaseRoomValidator<T> : AbstractValidator<T> where T : RoomBaseDto
    {
        public BaseRoomValidator()
        {
            checkName();
            CheckTableCount();
            CheckRoomLayout();
            checkRoomLimits();
        }

        private void checkRoomLimits()
        {
            When(x => x.RoomReservationLimit != null, () => {
                RuleFor(x => x.RoomReservationLimit).SetValidator(new RoomReservationLimitsValidator()!);
            });
        }

        private void checkName()
        {
            RuleFor(e => e.Name)
            .NotEmpty()
                .WithMessage("Name is required.")
            .Length(2, 300)
                .WithMessage("Name must be beetwen 2 - 300 characters")
                .When(rf => rf.Name is not null); ;
        }

        private void CheckTableCount()
        {
            RuleFor(e => e.TableCount)
                .Equal(0)
                    .When(r => r.RoomLayout == Domain.Enums.RoomLayoutEnum.Theater)
                    .WithMessage("Theater layout cannot have tables.");
            RuleFor(e => e.TableCount)
                .GreaterThan(0)
                    .When(r => r.RoomLayout == Domain.Enums.RoomLayoutEnum.Boardroom)
                    .WithMessage("Boardroom layout requires at least one table.");
        }

        private void CheckRoomLayout()
        {
            When(r => r.RoomLayout == Domain.Enums.RoomLayoutEnum.Classroom, () =>
            {
                RuleFor(r => r.Capacity)
                    .Must(c => c % 2 == 0)
                    .WithMessage("Classroom layout requires even capacity.");

                RuleFor(r => r.TableCount)
                    .GreaterThan(1)
                    .WithMessage("Classroom layout requires more than one table.");
            });
        }

 
    }
}
