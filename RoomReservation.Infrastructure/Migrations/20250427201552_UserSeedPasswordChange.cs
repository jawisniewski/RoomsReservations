using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoomReservation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserSeedPasswordChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "IdioP+/nL+5jf8yWL3tVYWMa5g6sgNIs5w4JFQNXuEs=");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "Ix7MfReNpfIpg7xXlZk5bWwTmkV5h64e4AJtiEMtanI=");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "Password",
                value: "VOoS58R1AHocCK0wA764P3UC5yn2xjOL5ZfEggL9MAU=");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "tbmfBSR3dPEPMxiU08g3TQ1ebvxztI6VKZ3Cql5p3c4=");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "DXbA4LtE2GyS9KQgNTWgZ8uBfO9ovzAQW0sc2c7TYl0=");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "Password",
                value: "HiJh9yCo9Yg9g6+nCh4Dc47Glm5RMKNDMaT07jY7Htc=");
        }
    }
}
