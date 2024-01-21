using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wsbMvcApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMealModel1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meal_User_CreatedByUserId1",
                table: "Meal");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropIndex(
                name: "IX_Meal_CreatedByUserId1",
                table: "Meal");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Meal");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId1",
                table: "Meal");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "Meal",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId1",
                table: "Meal",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Meal_CreatedByUserId1",
                table: "Meal",
                column: "CreatedByUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Meal_User_CreatedByUserId1",
                table: "Meal",
                column: "CreatedByUserId1",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
