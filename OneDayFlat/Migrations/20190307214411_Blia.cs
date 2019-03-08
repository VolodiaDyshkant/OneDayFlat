using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OneDayFlat.Migrations
{
    public partial class Blia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Calendar",
                columns: table => new
                {
                    CalendarID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CurrentTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calendar", x => x.CalendarID);
                });

            migrationBuilder.CreateTable(
                name: "Day",
                columns: table => new
                {
                    DayForeignKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Booked = table.Column<bool>(nullable: false),
                    CalendarID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Day", x => x.DayForeignKey);
                    table.ForeignKey(
                        name: "FK_Day_Calendar_CalendarID",
                        column: x => x.CalendarID,
                        principalTable: "Calendar",
                        principalColumn: "CalendarID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Flat",
                columns: table => new
                {
                    RoomID = table.Column<int>(nullable: false),
                    City = table.Column<string>(nullable: true),
                    OwnerName = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Image = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flat", x => x.RoomID);
                    table.ForeignKey(
                        name: "FK_Flat_Calendar_RoomID",
                        column: x => x.RoomID,
                        principalTable: "Calendar",
                        principalColumn: "CalendarID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Login = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    Number = table.Column<string>(nullable: true),
                    DayForeignkey = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_User_Day_DayForeignkey",
                        column: x => x.DayForeignkey,
                        principalTable: "Day",
                        principalColumn: "DayForeignkey",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Day_CalendarID",
                table: "Day",
                column: "CalendarID");

            migrationBuilder.CreateIndex(
                name: "IX_User_DayForeignkey",
                table: "User",
                column: "DayForeignkey",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Flat");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Day");

            migrationBuilder.DropTable(
                name: "Calendar");
        }
    }
}
