using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiShop.Migrations
{
    /// <inheritdoc />
    public partial class AddNullableCountry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Countries_CountryId",
                table: "Cars");

            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                table: "Cars",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Countries_CountryId",
                table: "Cars",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Countries_CountryId",
                table: "Cars");

            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                table: "Cars",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Countries_CountryId",
                table: "Cars",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
