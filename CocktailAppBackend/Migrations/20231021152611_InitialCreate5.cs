using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CocktailAppBackend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Auths_CreatedByUserId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "RecipeDetail");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "Orders",
                newName: "AuthId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_CreatedByUserId",
                table: "Orders",
                newName: "IX_Orders_AuthId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Auths_AuthId",
                table: "Orders",
                column: "AuthId",
                principalTable: "Auths",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Auths_AuthId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "AuthId",
                table: "Orders",
                newName: "CreatedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_AuthId",
                table: "Orders",
                newName: "IX_Orders_CreatedByUserId");

            migrationBuilder.CreateTable(
                name: "RecipeDetail",
                columns: table => new
                {
                    RecipesId = table.Column<int>(type: "int", nullable: false),
                    IngredientsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeDetail", x => new { x.RecipesId, x.IngredientsId });
                    table.ForeignKey(
                        name: "FK_RecipeDetail_Ingredients_IngredientsId",
                        column: x => x.IngredientsId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeDetail_Recipes_RecipesId",
                        column: x => x.RecipesId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeDetail_IngredientsId",
                table: "RecipeDetail",
                column: "IngredientsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Auths_CreatedByUserId",
                table: "Orders",
                column: "CreatedByUserId",
                principalTable: "Auths",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
