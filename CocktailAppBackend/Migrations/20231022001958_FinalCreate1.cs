using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CocktailAppBackend.Migrations
{
    /// <inheritdoc />
    public partial class FinalCreate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rating_Auths_RatedById",
                table: "Rating");

            migrationBuilder.DropForeignKey(
                name: "FK_Rating_Recipes_RatedRecipeId",
                table: "Rating");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rating",
                table: "Rating");

            migrationBuilder.RenameTable(
                name: "Rating",
                newName: "Ratings");

            migrationBuilder.RenameIndex(
                name: "IX_Rating_RatedRecipeId",
                table: "Ratings",
                newName: "IX_Ratings_RatedRecipeId");

            migrationBuilder.RenameIndex(
                name: "IX_Rating_RatedById",
                table: "Ratings",
                newName: "IX_Ratings_RatedById");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ratings",
                table: "Ratings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Auths_RatedById",
                table: "Ratings",
                column: "RatedById",
                principalTable: "Auths",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Recipes_RatedRecipeId",
                table: "Ratings",
                column: "RatedRecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Auths_RatedById",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Recipes_RatedRecipeId",
                table: "Ratings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ratings",
                table: "Ratings");

            migrationBuilder.RenameTable(
                name: "Ratings",
                newName: "Rating");

            migrationBuilder.RenameIndex(
                name: "IX_Ratings_RatedRecipeId",
                table: "Rating",
                newName: "IX_Rating_RatedRecipeId");

            migrationBuilder.RenameIndex(
                name: "IX_Ratings_RatedById",
                table: "Rating",
                newName: "IX_Rating_RatedById");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rating",
                table: "Rating",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_Auths_RatedById",
                table: "Rating",
                column: "RatedById",
                principalTable: "Auths",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_Recipes_RatedRecipeId",
                table: "Rating",
                column: "RatedRecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
