using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airbnb.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixRelationShipWithWishList2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Houses_AspNetUsers_HostId",
                table: "Houses");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Houses_HouseId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_WishLists_AspNetUsers_GuestId",
                table: "WishLists");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "WishLists",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Messages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Images",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_WishLists_HouseId",
                table: "WishLists",
                column: "HouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Houses_AspNetUsers_HostId",
                table: "Houses",
                column: "HostId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Houses_HouseId",
                table: "Images",
                column: "HouseId",
                principalTable: "Houses",
                principalColumn: "HouseId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WishLists_AspNetUsers_GuestId",
                table: "WishLists",
                column: "GuestId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WishLists_Houses_HouseId",
                table: "WishLists",
                column: "HouseId",
                principalTable: "Houses",
                principalColumn: "HouseId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Houses_AspNetUsers_HostId",
                table: "Houses");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Houses_HouseId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_WishLists_AspNetUsers_GuestId",
                table: "WishLists");

            migrationBuilder.DropForeignKey(
                name: "FK_WishLists_Houses_HouseId",
                table: "WishLists");

            migrationBuilder.DropIndex(
                name: "IX_WishLists_HouseId",
                table: "WishLists");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "WishLists");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Images");

            migrationBuilder.AddForeignKey(
                name: "FK_Houses_AspNetUsers_HostId",
                table: "Houses",
                column: "HostId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Houses_HouseId",
                table: "Images",
                column: "HouseId",
                principalTable: "Houses",
                principalColumn: "HouseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WishLists_AspNetUsers_GuestId",
                table: "WishLists",
                column: "GuestId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
