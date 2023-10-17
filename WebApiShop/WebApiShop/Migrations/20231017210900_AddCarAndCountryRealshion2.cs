using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiShop.Migrations
{
    /// <inheritdoc />
    public partial class AddCarAndCountryRealshion2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Peets_PeetId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_PeetId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "PeetId",
                table: "Employees");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PeetId",
                table: "Employees",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PeetId",
                table: "Employees",
                column: "PeetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Peets_PeetId",
                table: "Employees",
                column: "PeetId",
                principalTable: "Peets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
