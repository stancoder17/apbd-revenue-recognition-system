using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Revenue_recognition_system.Migrations
{
    /// <inheritdoc />
    public partial class DeleteSupportExtensionTableAndAddAdditionalSupportYearsAttributeToContract : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentTargets_SupportExtensions_SupportExtensionId",
                table: "PaymentTargets");

            migrationBuilder.DropTable(
                name: "SupportExtensions");

            migrationBuilder.DropIndex(
                name: "IX_PaymentTargets_SupportExtensionId",
                table: "PaymentTargets");

            migrationBuilder.AddColumn<int>(
                name: "AdditionalSupportYears",
                table: "Contracts",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdditionalSupportYears",
                table: "Contracts");

            migrationBuilder.CreateTable(
                name: "SupportExtensions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Contract_Id = table.Column<int>(type: "int", nullable: false),
                    NumberOfYears = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(7,2)", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportExtensions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupportExtensions_Contracts_Contract_Id",
                        column: x => x.Contract_Id,
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTargets_SupportExtensionId",
                table: "PaymentTargets",
                column: "SupportExtensionId");

            migrationBuilder.CreateIndex(
                name: "IX_SupportExtensions_Contract_Id",
                table: "SupportExtensions",
                column: "Contract_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentTargets_SupportExtensions_SupportExtensionId",
                table: "PaymentTargets",
                column: "SupportExtensionId",
                principalTable: "SupportExtensions",
                principalColumn: "Id");
        }
    }
}
