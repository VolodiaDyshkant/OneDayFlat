using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OneDayFlat.Migrations
{
    public partial class AddImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Day_Calendar_CalendarID",
                table: "Day");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "UserID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Flat");

            migrationBuilder.AlterColumn<int>(
                name: "CalendarID",
                table: "Day",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    ImageID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Path = table.Column<string>(nullable: true),
                    FlatID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.ImageID);
                    table.ForeignKey(
                        name: "FK_Image_Flat_FlatID",
                        column: x => x.FlatID,
                        principalTable: "Flat",
                        principalColumn: "RoomID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Image_FlatID",
                table: "Image",
                column: "FlatID");

            migrationBuilder.AddForeignKey(
                name: "FK_Day_Calendar_CalendarID",
                table: "Day",
                column: "CalendarID",
                principalTable: "Calendar",
                principalColumn: "CalendarID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Day_Calendar_CalendarID",
                table: "Day");

            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Flat",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CalendarID",
                table: "Day",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "admin" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "user" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserID", "DayForeignKey", "Login", "Name", "Number", "Password", "RoleID" },
                values: new object[] { 1, null, "admin@gmail.com", null, null, "123456", 1 });

            migrationBuilder.AddForeignKey(
                name: "FK_Day_Calendar_CalendarID",
                table: "Day",
                column: "CalendarID",
                principalTable: "Calendar",
                principalColumn: "CalendarID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
