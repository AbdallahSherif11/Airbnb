using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airbnb.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeViewToHouseViewInHouseTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "View",
                table: "Houses",
                newName: "HouseView");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HouseView",
                table: "Houses",
                newName: "View");
        }
    }
}
