using FluentValidation;
using RoomReservation.Application.DTOs.Equipment;
using RoomReservation.Application.DTOs.Room.CreateRoom;
using RoomReservation.Application.Enums;

namespace RoomReservation.API.Validators.Room
{
    public class CreateRoomValidator : BaseRoomValidator<CreateRoomRequest>
    {
        public CreateRoomValidator() : base()
        {
            CheckEquipments();
        }
        private void CheckEquipments()
        {
            RuleFor(e => e.RoomEquipments.Count)
                .NotEqual(0)
                    .WithMessage("Equipments list must be specified");

            RuleFor(e => e)
                .Must(e => RequireEquipment(e.RoomEquipments))
                    .WithMessage("Equipments list require at least one projector, whiteboard or video conference");
            RuleFor(e => e)
                .Must(e => VideoConferenceRequires(e.RoomEquipments))
                    .When(e => e.RoomEquipments.Any(eq => eq.EquipmentType == EquipmentTypeEnum.VideoConference && eq.Quantity > 0))
                    .WithMessage("Video confrence require at least one projector or screen");
        }

        private bool RequireEquipment(List<CreateRoomEquipmentRequest> equipments)
        {
            return equipments.Any(e =>
                (
                    e.EquipmentType == EquipmentTypeEnum.Projector ||
                    e.EquipmentType == EquipmentTypeEnum.Whiteboard ||
                    e.EquipmentType == EquipmentTypeEnum.VideoConference
                )
                && e.Quantity > 0);
        }
        private bool VideoConferenceRequires(List<CreateRoomEquipmentRequest> equipments)
        {
            var hasProjector = equipments.Any(e => (e.EquipmentType == EquipmentTypeEnum.Projector || e.EquipmentType == EquipmentTypeEnum.Screen) &&
                e.Quantity > 0);

            return hasProjector;
        }
    }
}
