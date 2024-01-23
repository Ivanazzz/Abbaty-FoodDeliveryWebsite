using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodDeliveryWebsite.Models.Migrations
{
    /// <inheritdoc />
    public partial class OrderIdAndDiscountIdNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orderitem_order_orderid",
                table: "orderitem");

            migrationBuilder.DropColumn(
                name: "status",
                table: "order");

            migrationBuilder.AlterColumn<int>(
                name: "orderid",
                table: "orderitem",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_orderitem_order_orderid",
                table: "orderitem",
                column: "orderid",
                principalTable: "order",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orderitem_order_orderid",
                table: "orderitem");

            migrationBuilder.AlterColumn<int>(
                name: "orderid",
                table: "orderitem",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "order",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_orderitem_order_orderid",
                table: "orderitem",
                column: "orderid",
                principalTable: "order",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
