using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Revenue_recognition_system.Migrations
{
    /// <inheritdoc />
    public partial class RenameAddressTableToAddresses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Address",
                newName: "Addresses"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Addresses",
                newName: "Address"
            );
        }
    }
}
