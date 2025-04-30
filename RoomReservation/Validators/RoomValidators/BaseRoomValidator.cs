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
            CheckEquipments();
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
            .MaximumLength(300)
                .WithMessage("Name must not exceed 300 characters.");
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

        private void CheckEquipments()
        {
            RuleFor(e => e.RoomsEquipments.Count)
                .NotEqual(0)
                    .WithMessage("Equipments list must be specified");

            RuleFor(e => e)
                .Must(e => RequireEquipment(e.RoomsEquipments))
                    .WithMessage("Equipments list require at least one projector, whiteboard or video conference");
            RuleFor(e => e)
                .Must(e => VideoConferenceRequires(e.RoomsEquipments))
                    .When(e => e.RoomsEquipments.Any(eq => eq.EquipmentType == EquipmentTypeEnum.VideoConference && eq.Quantity > 0))
                    .WithMessage("Video confrence require at least one projector or screen");
        }

        private bool RequireEquipment(List<RoomEquipmentDto> equipments)
        {
            return equipments.Any(e =>
                (
                    e.EquipmentType == EquipmentTypeEnum.Projector ||
                    e.EquipmentType == EquipmentTypeEnum.Whiteboard ||
                    e.EquipmentType == EquipmentTypeEnum.VideoConference
                )
                && e.Quantity > 0);
        }
        private bool VideoConferenceRequires(List<RoomEquipmentDto> equipments)
        {
            var hasProjector = equipments.Any(e => (e.EquipmentType == EquipmentTypeEnum.Projector || e.EquipmentType == EquipmentTypeEnum.Screen) &&
                e.Quantity > 0);

            return hasProjector;
        }
    }
}
