using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airbnb.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedRelationShipBetweenUserandHouse_OneToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "HostId",
                table: "Houses",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Houses_HostId",
                table: "Houses",
                column: "HostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Houses_AspNetUsers_HostId",
                table: "Houses",
                column: "HostId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Houses_AspNetUsers_HostId",
                table: "Houses");

            migrationBuilder.DropIndex(
                name: "IX_Houses_HostId",
                table: "Houses");

            migrationBuilder.AlterColumn<int>(
                name: "HostId",
                table: "Houses",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
