using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodRecipeAPI.Migrations
{
    public partial class initialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    score = table.Column<double>(type: "float", nullable: false),
                    comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecipeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ingredients = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    servings = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    instructions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RateId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.id);
                    table.ForeignKey(
                        name: "FK_Recipes_Rates_RateId",
                        column: x => x.RateId,
                        principalTable: "Rates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rates_RecipeId",
                table: "Rates",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_RateId",
                table: "Recipes",
                column: "RateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rates_Recipes_RecipeId",
                table: "Rates",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rates_Recipes_RecipeId",
                table: "Rates");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "Rates");
        }
    }
}
