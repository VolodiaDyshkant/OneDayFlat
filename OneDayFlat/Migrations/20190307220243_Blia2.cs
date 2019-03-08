using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OneDayFlat.Migrations
{
    public partial class Blia2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserFlat",
                columns: table => new
                {
                    UserFlatID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserID = table.Column<int>(nullable: false),
                    FlatID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFlat", x => x.UserFlatID);
                    table.ForeignKey(
                        name: "FK_UserFlat_Flat_FlatID",
                        column: x => x.FlatID,
                        principalTable: "Flat",
                        principalColumn: "RoomID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_UserFlat_User_UserForeignKey",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserForeigKey",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserFlat_FlatID",
                table: "UserFlat",
                column: "FlatID");

            migrationBuilder.CreateIndex(
                name: "IX_UserFlat_UserID",
                table: "UserFlat",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserFlat");
        }
    }
}
