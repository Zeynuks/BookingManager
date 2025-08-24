using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Global_Fix_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Room_RoomId1",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_RoomId1",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "RoomId1",
                table: "Reservation");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoomId1",
                table: "Reservation",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_RoomId1",
                table: "Reservation",
                column: "RoomId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Room_RoomId1",
                table: "Reservation",
                column: "RoomId1",
                principalTable: "Room",
                principalColumn: "Id");
        }
    }
}
