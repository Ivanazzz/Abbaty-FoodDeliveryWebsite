using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodDeliveryWebsite.Models.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderItemsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "userid",
                table: "orderitem",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_orderitem_userid",
                table: "orderitem",
                column: "userid");

            migrationBuilder.AddForeignKey(
                name: "FK_orderitem_user_userid",
                table: "orderitem",
                column: "userid",
                principalTable: "user",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orderitem_user_userid",
                table: "orderitem");

            migrationBuilder.DropIndex(
                name: "IX_orderitem_userid",
                table: "orderitem");

            migrationBuilder.DropColumn(
                name: "userid",
                table: "orderitem");
        }
    }
}
