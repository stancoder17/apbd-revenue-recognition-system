using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Revenue_recognition_system.Migrations
{
    /// <inheritdoc />
    public partial class AddPESELandKRSUniqueIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Clients_KRS",
                table: "Clients",
                column: "KRS",
                unique: true,
                filter: "[KRS] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_PESEL",
                table: "Clients",
                column: "PESEL",
                unique: true,
                filter: "[PESEL] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Clients_KRS",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_PESEL",
                table: "Clients");
        }
    }
}
