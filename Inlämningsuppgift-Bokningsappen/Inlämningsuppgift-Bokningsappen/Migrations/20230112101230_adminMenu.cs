using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inlämningsuppgift_Bokningsappen.Migrations
{
    public partial class adminMenu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DayId",
                table: "Rooms",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GuestId",
                table: "Rooms",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Days",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Days", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Guests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guests", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_DayId",
                table: "Rooms",
                column: "DayId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_GuestId",
                table: "Rooms",
                column: "GuestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Days_DayId",
                table: "Rooms",
                column: "DayId",
                principalTable: "Days",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Guests_GuestId",
                table: "Rooms",
                column: "GuestId",
                principalTable: "Guests",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Days_DayId",
                table: "Rooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Guests_GuestId",
                table: "Rooms");

            migrationBuilder.DropTable(
                name: "Days");

            migrationBuilder.DropTable(
                name: "Guests");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_DayId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_GuestId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "DayId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "GuestId",
                table: "Rooms");
        }
    }
}
