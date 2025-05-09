﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoomReservation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addUniqueRoomName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Rooms_Name",
                table: "Rooms",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Rooms_Name",
                table: "Rooms");
        }
    }
}
