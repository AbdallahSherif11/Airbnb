using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airbnb.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedLatandLonforHouse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Latitude",
                table: "Houses",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Longitude",
                table: "Houses",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Houses");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Houses");
        }
    }
}
