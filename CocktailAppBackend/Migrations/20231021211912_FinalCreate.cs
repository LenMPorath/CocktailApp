using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CocktailAppBackend.Migrations
{
    /// <inheritdoc />
    public partial class FinalCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rating_Recipes_RatingId",
                table: "Rating");

            migrationBuilder.RenameColumn(
                name: "RatingId",
                table: "Rating",
                newName: "RatedRecipeId");

            migrationBuilder.RenameIndex(
                name: "IX_Rating_RatingId",
                table: "Rating",
                newName: "IX_Rating_RatedRecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_Recipes_RatedRecipeId",
                table: "Rating",
                column: "RatedRecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rating_Recipes_RatedRecipeId",
                table: "Rating");

            migrationBuilder.RenameColumn(
                name: "RatedRecipeId",
                table: "Rating",
                newName: "RatingId");

            migrationBuilder.RenameIndex(
                name: "IX_Rating_RatedRecipeId",
                table: "Rating",
                newName: "IX_Rating_RatingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_Recipes_RatingId",
                table: "Rating",
                column: "RatingId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
