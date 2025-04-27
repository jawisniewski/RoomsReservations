using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoomReservation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addTableCountColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TableCount",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TableCount",
                table: "Rooms");
        }
    }
}
