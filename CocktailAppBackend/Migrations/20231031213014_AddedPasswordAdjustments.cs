using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CocktailAppBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddedPasswordAdjustments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Auths",
                newName: "PasswordSalt");

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Auths",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Auths");

            migrationBuilder.RenameColumn(
                name: "PasswordSalt",
                table: "Auths",
                newName: "Password");
        }
    }
}
