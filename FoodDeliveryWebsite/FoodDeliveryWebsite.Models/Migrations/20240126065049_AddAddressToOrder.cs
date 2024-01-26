using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodDeliveryWebsite.Models.Migrations
{
    /// <inheritdoc />
    public partial class AddAddressToOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "addressid",
                table: "order",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_order_addressid",
                table: "order",
                column: "addressid");

            migrationBuilder.AddForeignKey(
                name: "FK_order_address_addressid",
                table: "order",
                column: "addressid",
                principalTable: "address",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_order_address_addressid",
                table: "order");

            migrationBuilder.DropIndex(
                name: "IX_order_addressid",
                table: "order");

            migrationBuilder.DropColumn(
                name: "addressid",
                table: "order");
        }
    }
}
