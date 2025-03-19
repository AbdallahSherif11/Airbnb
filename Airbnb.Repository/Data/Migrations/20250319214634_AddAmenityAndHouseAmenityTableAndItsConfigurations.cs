using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airbnb.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAmenityAndHouseAmenityTableAndItsConfigurations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_AspNetUsers_GuestId",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Houses_HouseId",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_AspNetUsers_GuestId",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_Booking_BookingId",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_Houses_HouseId",
                table: "Review");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Review",
                table: "Review");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Booking",
                table: "Booking");

            migrationBuilder.RenameTable(
                name: "Review",
                newName: "Reviews");

            migrationBuilder.RenameTable(
                name: "Booking",
                newName: "Bookings");

            migrationBuilder.RenameIndex(
                name: "IX_Review_HouseId",
                table: "Reviews",
                newName: "IX_Reviews_HouseId");

            migrationBuilder.RenameIndex(
                name: "IX_Review_GuestId",
                table: "Reviews",
                newName: "IX_Reviews_GuestId");

            migrationBuilder.RenameIndex(
                name: "IX_Review_BookingId",
                table: "Reviews",
                newName: "IX_Reviews_BookingId");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_HouseId",
                table: "Bookings",
                newName: "IX_Bookings_HouseId");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_GuestId",
                table: "Bookings",
                newName: "IX_Bookings_GuestId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews",
                column: "ReviewId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bookings",
                table: "Bookings",
                column: "BookingId");

            migrationBuilder.CreateTable(
                name: "Amenities",
                columns: table => new
                {
                    AmenityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Amenities", x => x.AmenityId);
                });

            migrationBuilder.CreateTable(
                name: "HouseAmenities",
                columns: table => new
                {
                    HouseId = table.Column<int>(type: "int", nullable: false),
                    AmenityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HouseAmenities", x => new { x.HouseId, x.AmenityId });
                    table.ForeignKey(
                        name: "FK_HouseAmenities_Amenities_AmenityId",
                        column: x => x.AmenityId,
                        principalTable: "Amenities",
                        principalColumn: "AmenityId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HouseAmenities_Houses_HouseId",
                        column: x => x.HouseId,
                        principalTable: "Houses",
                        principalColumn: "HouseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HouseAmenities_AmenityId",
                table: "HouseAmenities",
                column: "AmenityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_AspNetUsers_GuestId",
                table: "Bookings",
                column: "GuestId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Houses_HouseId",
                table: "Bookings",
                column: "HouseId",
                principalTable: "Houses",
                principalColumn: "HouseId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_AspNetUsers_GuestId",
                table: "Reviews",
                column: "GuestId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Bookings_BookingId",
                table: "Reviews",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "BookingId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Houses_HouseId",
                table: "Reviews",
                column: "HouseId",
                principalTable: "Houses",
                principalColumn: "HouseId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_AspNetUsers_GuestId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Houses_HouseId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_AspNetUsers_GuestId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Bookings_BookingId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Houses_HouseId",
                table: "Reviews");

            migrationBuilder.DropTable(
                name: "HouseAmenities");

            migrationBuilder.DropTable(
                name: "Amenities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bookings",
                table: "Bookings");

            migrationBuilder.RenameTable(
                name: "Reviews",
                newName: "Review");

            migrationBuilder.RenameTable(
                name: "Bookings",
                newName: "Booking");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_HouseId",
                table: "Review",
                newName: "IX_Review_HouseId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_GuestId",
                table: "Review",
                newName: "IX_Review_GuestId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_BookingId",
                table: "Review",
                newName: "IX_Review_BookingId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_HouseId",
                table: "Booking",
                newName: "IX_Booking_HouseId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_GuestId",
                table: "Booking",
                newName: "IX_Booking_GuestId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Review",
                table: "Review",
                column: "ReviewId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Booking",
                table: "Booking",
                column: "BookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_AspNetUsers_GuestId",
                table: "Booking",
                column: "GuestId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Houses_HouseId",
                table: "Booking",
                column: "HouseId",
                principalTable: "Houses",
                principalColumn: "HouseId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Review_AspNetUsers_GuestId",
                table: "Review",
                column: "GuestId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Booking_BookingId",
                table: "Review",
                column: "BookingId",
                principalTable: "Booking",
                principalColumn: "BookingId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Houses_HouseId",
                table: "Review",
                column: "HouseId",
                principalTable: "Houses",
                principalColumn: "HouseId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
