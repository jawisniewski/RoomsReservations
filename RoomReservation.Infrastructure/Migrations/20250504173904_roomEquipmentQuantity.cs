using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoomReservation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class roomEquipmentQuantity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "RoomsEquipments",
                keyColumn: "Id",
                keyValue: 1,
                column: "Quantity",
                value: 2);

            migrationBuilder.UpdateData(
                table: "RoomsEquipments",
                keyColumn: "Id",
                keyValue: 3,
                column: "Quantity",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RoomsEquipments",
                keyColumn: "Id",
                keyValue: 4,
                column: "Quantity",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RoomsEquipments",
                keyColumn: "Id",
                keyValue: 5,
                column: "Quantity",
                value: 3);

            migrationBuilder.UpdateData(
                table: "RoomsEquipments",
                keyColumn: "Id",
                keyValue: 6,
                column: "Quantity",
                value: 4);

            migrationBuilder.UpdateData(
                table: "RoomsEquipments",
                keyColumn: "Id",
                keyValue: 7,
                column: "Quantity",
                value: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "RoomsEquipments",
                keyColumn: "Id",
                keyValue: 1,
                column: "Quantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "RoomsEquipments",
                keyColumn: "Id",
                keyValue: 3,
                column: "Quantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "RoomsEquipments",
                keyColumn: "Id",
                keyValue: 4,
                column: "Quantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "RoomsEquipments",
                keyColumn: "Id",
                keyValue: 5,
                column: "Quantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "RoomsEquipments",
                keyColumn: "Id",
                keyValue: 6,
                column: "Quantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "RoomsEquipments",
                keyColumn: "Id",
                keyValue: 7,
                column: "Quantity",
                value: 0);
        }
    }
}
