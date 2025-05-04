using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RoomReservation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class reservationSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "Id", "EndDate", "RoomId", "StartDate", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 6, 6, 12, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2025, 6, 6, 11, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, new DateTime(2025, 6, 7, 12, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2025, 6, 7, 11, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 3, new DateTime(2025, 6, 7, 13, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2025, 6, 7, 12, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 4, new DateTime(2025, 6, 7, 12, 50, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2025, 6, 7, 12, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 5, new DateTime(2025, 6, 7, 13, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2025, 6, 7, 12, 0, 0, 0, DateTimeKind.Unspecified), 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
