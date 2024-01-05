using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodDeliveryWebsite.Models.Migrations
{
    /// <inheritdoc />
    public partial class ChangeAddressUserRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_address_addressid",
                table: "user");

            migrationBuilder.DropIndex(
                name: "IX_user_addressid",
                table: "user");

            migrationBuilder.DropColumn(
                name: "addressid",
                table: "user");

            migrationBuilder.AddColumn<int>(
                name: "userid",
                table: "address",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_address_userid",
                table: "address",
                column: "userid");

            migrationBuilder.AddForeignKey(
                name: "FK_address_user_userid",
                table: "address",
                column: "userid",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_address_user_userid",
                table: "address");

            migrationBuilder.DropIndex(
                name: "IX_address_userid",
                table: "address");

            migrationBuilder.DropColumn(
                name: "userid",
                table: "address");

            migrationBuilder.AddColumn<int>(
                name: "addressid",
                table: "user",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_user_addressid",
                table: "user",
                column: "addressid");

            migrationBuilder.AddForeignKey(
                name: "FK_user_address_addressid",
                table: "user",
                column: "addressid",
                principalTable: "address",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
