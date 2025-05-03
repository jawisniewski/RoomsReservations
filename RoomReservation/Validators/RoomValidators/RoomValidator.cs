using FluentValidation;
using RoomReservation.Application.DTOs.Equipment;
using RoomReservation.Application.DTOs.Room;
using RoomReservation.Application.DTOs.Room.CreateRoom;
using RoomReservation.Application.Enums;

namespace RoomReservation.API.Validators.Room
{
    public class RoomValidator : BaseRoomValidator<RoomDto>
    {
        public RoomValidator() : base()
        {
            CheckEquipments();
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
