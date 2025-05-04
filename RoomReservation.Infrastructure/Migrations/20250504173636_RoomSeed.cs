using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RoomReservation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RoomSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Capacity", "Name", "RoomLayout", "TableCount" },
                values: new object[,]
                {
                    { 1, 30, "Klasa", 2, 20 },
                    { 2, 10, "Sala konferencyjna 5", 0, 4 },
                    { 3, 100, "Scena", 1, 0 }
                });

            migrationBuilder.InsertData(
                table: "RoomsEquipments",
                columns: new[] { "Id", "EquipmentId", "Quantity", "RoomId" },
                values: new object[,]
                {
                    { 1, 1, 0, 1 },
                    { 2, 2, 0, 1 },
                    { 3, 3, 0, 1 },
                    { 4, 4, 0, 1 },
                    { 5, 1, 0, 2 },
                    { 6, 3, 0, 2 },
                    { 7, 2, 0, 3 }
                });

            migrationBuilder.InsertData(
                table: "RoomsReservationLimits",
                columns: new[] { "Id", "MaxTime", "MinTime", "RoomId" },
                values: new object[,]
                {
                    { 1, 90, 30, 1 },
                    { 2, 0, 0, 2 },
                    { 3, 60, 0, 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RoomsEquipments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RoomsEquipments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RoomsEquipments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "RoomsEquipments",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "RoomsEquipments",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "RoomsEquipments",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "RoomsEquipments",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "RoomsReservationLimits",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RoomsReservationLimits",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RoomsReservationLimits",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
